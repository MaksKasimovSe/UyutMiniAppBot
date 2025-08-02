using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using UyutMiniApp.Data.Contexts;
using UyutMiniApp.Domain.Enums;
using UyutMiniApp.Extensions;
using UyutMiniApp.Helpers;
using UyutMiniApp.Middlewares;
using UyutMiniApp.Service.Helpers;
using UyutMiniApp.Signalr;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

builder.Services.AddControllers();

builder.Services.AddDbContext<UyutMiniAppDbContext>(option =>
    option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSignalR();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddSwaggerService();
builder.Services.AddCustomServices();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(actionModelConvention: new RouteTokenTransformerConvention(
                                 new ConfigureApiUrlName()));
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
    options.AddPolicy("AllowSpecific",
        builder =>
        {
            builder.WithOrigins("https://uyut.kr")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
        });
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("User", policy => policy.RequireRole(
        Enum.GetName(Role.User)));
    options.AddPolicy("Admin", policy => policy.RequireRole(
        Enum.GetName(Role.Admin)));
    options.AddPolicy("Courier", policy => policy.RequireRole(
        "Courier"));
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();

EnvironmentHelper.WebRootPath = app.Services.GetRequiredService<IWebHostEnvironment>()?.WebRootPath;

if (app.Services.GetService<IHttpContextAccessor>() != null)
    HttpContextHelper.Accessor = app.Services.GetRequiredService<IHttpContextAccessor>();

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors("AllowSpecific");

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.MapHub<ChatHub>("/chathub").RequireAuthorization();
app.MapHub<OrderCheckHub>("/orderchekhub").RequireAuthorization();
app.MapHub<OrderProcessHub>("/orderprocesshub").RequireAuthorization();


app.UseWebSockets();

app.Run();
