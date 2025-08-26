using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using UyutMiniApp.Data.IRepositories;
using UyutMiniApp.Domain.Entities;
using UyutMiniApp.Domain.Enums;
using UyutMiniApp.Service.Exceptions;

namespace UyutMiniApp.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlingMiddleware> logger;
        private readonly IConfiguration configuration;
        private readonly IServiceScopeFactory scopeFactory;
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IConfiguration configuration, IServiceScopeFactory scopeFactory)
        {
            this.next = next;
            this.logger = logger;
            this.configuration = configuration;
            this.scopeFactory = scopeFactory;
        }
        public async Task InvokeAsync(HttpContext context, IAuthorizationPolicyProvider policyProvider)
        {
            try
            {
                var endpoint = context.GetEndpoint();

                // If there's no matched endpoint (e.g., 404), just continue.
                if (endpoint is null)
                {
                    await this.next.Invoke(context);
                    return;
                }
                var allowAnon = endpoint.Metadata.GetMetadata<IAllowAnonymous>();

                string value = context?.User?.Claims.FirstOrDefault(p => p.Type == "Id")?.Value;

                bool canParse = Guid.TryParse(value, out Guid id);
                Guid? userId = canParse ? id : null;
                var role = context?.User.FindFirst(ClaimTypes.Role)?.Value;


                using var scope = scopeFactory.CreateScope();
                var userRepository = scope.ServiceProvider.GetRequiredService<IGenericRepository<User>>();
                var courierRepository = scope.ServiceProvider.GetRequiredService<IGenericRepository<Courier>>();

                if (allowAnon is not null)
                    await this.next.Invoke(context);

                else if (configuration["IsWorking"] == "true" || role == "Admin")
                {
                    if (role == "Courier")
                    {
                        await this.next.Invoke(context);
                        return;
                    }
                    var userRole = (Role)Enum.Parse(typeof(Role), role);
                    if (await userRepository.GetAsync(u => u.Id == userId && u.Role == userRole, isTracking: false) is not null)
                        await this.next.Invoke(context);

                    else
                        throw new HttpStatusCodeException(401, "Unauthorized");
                }
                else if (configuration["IsWorking"] == "false")
                    throw new HttpStatusCodeException(400, "Cafe is closed right now comeback later");
                else
                    throw new HttpStatusCodeException(401, "Unauthorized");
            }
            catch (HttpStatusCodeException ex)
            {
                await this.HandleException(context, ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());

                await this.HandleException(context, 500, ex.Message);
            }
        }

        public async Task HandleException(HttpContext context, int code, string message)
        {
            context.Response.StatusCode = code;
            await context.Response.WriteAsJsonAsync(new
            {
                Code = code,
                Message = message
            });
        }
    }
}