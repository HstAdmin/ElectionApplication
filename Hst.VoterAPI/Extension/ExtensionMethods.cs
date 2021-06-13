
using Hst.Business.Services;
using Hst.Business.Services.Interfaces;
using Hst.Business.UnitOfWork;

using Hst.Model.Common;
using Hst.Persistance.Infrastructure;
using Hst.Persistance.IRepository;
using Hst.Persistance.Reposiotry;
using Hst.Persistance.SQLConnection;
using Hst.Utility.EmailHelper;
using Hst.Utility.SmsHelper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Hst.VoterAPI.Extension
{
    public static class ExtensionMethods
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors((options) =>
            {
                options.AddPolicy("CorsPolicy", (builder) =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });
        }

        public static void ConfigureJWT(this IServiceCollection services, byte[] key)
        {
            services.AddAuthentication(x=>
                    x.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        public static void ConfigureDependency(this IServiceCollection services)
        {
            services.AddTransient<IConnectionfactory, SqlConnectionFactory>();
            services.AddScoped<IOrgRepository, OrgRepository>();
            services.AddScoped<IOrgService, OrgService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<Logger.ILoggerManager, Logger.LoggerManager>();
            services.AddScoped<IEmailHelper, EmailHelper>();
            services.AddScoped<ISmsHelper, SmsHelper>();
          
        }


        public static string GetEnumDescription(ErrorCode errorCode)
        {
            string errorMessage = string.Empty;
            var type = errorCode.GetType();
            var name = Enum.GetName(type, errorCode);
            if (name == null)
            {
                name = Enum.GetName(type, ErrorCode.Inserted);
            }
            var field = type.GetField(name);
            var fds = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
            foreach (DescriptionAttribute fd in fds)
            {
                errorMessage = fd.Description;
            }

            return errorMessage;
        }

    }
}
