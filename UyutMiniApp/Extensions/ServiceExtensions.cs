using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using UyutMiniApp.Data.IRepositories;
using UyutMiniApp.Data.Repositories;
using UyutMiniApp.Domain.Entities;
using UyutMiniApp.Service.Interfaces;
using UyutMiniApp.Service.Services;

namespace UyutMiniApp.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();
            services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
            services.AddScoped<IGenericRepository<Courier>, GenericRepository<Courier>>();
            services.AddScoped<IGenericRepository<CustomMeal>, GenericRepository<CustomMeal>>();
            services.AddScoped<IGenericRepository<DeliveryInfo>, GenericRepository<DeliveryInfo>>();
            services.AddScoped<IGenericRepository<Ingredient>, GenericRepository<Ingredient>>();
            services.AddScoped<IGenericRepository<MenuItem>, GenericRepository<MenuItem>>();
            services.AddScoped<IGenericRepository<Order>, GenericRepository<Order>>();
            services.AddScoped<IGenericRepository<SavedAddress>, GenericRepository<SavedAddress>>();
            services.AddScoped<IGenericRepository<Basket>, GenericRepository<Basket>>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICourierService, CourierService>();
            services.AddScoped<ICustomMealService, CustomMealService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IMenuItemService, MenuItemService>();
            services.AddScoped<IIngredientService, IngredientService>();
            services.AddScoped<IBasketService, BasketService>();
        }

        public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt");

            string key = jwtSettings.GetSection("Key").Value;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))

                };
            });
        }

        public static void AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(p =>
            {
                var xml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                p.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xml));
                p.ResolveConflictingActions(ad => ad.FirstOrDefault());
                p.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                });

                p.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }
    }
}
