using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace BookLoan.Infra.Ioc
{
    public static class DependencyInjectionSwagger
    {

        public static IServiceCollection AddInfrastructureSwagger(this IServiceCollection service,
            IConfiguration configuration)
        {
            service.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JSON Web Token"
                });


                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
                        new List<string>() { }
                    }

                });

            });

            return service;
        }

    }
}
