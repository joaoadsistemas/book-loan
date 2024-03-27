using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLoan.API.Context;
using BookLoan.Application.Interfaces;
using BookLoan.Application.Mappings;
using BookLoan.Application.Services;
using BookLoan.Domain.Account;
using BookLoan.Domain.Interfaces;
using BookLoan.Infra.Data.Identity;
using BookLoan.Infra.Data.Repositories;
using Loan.Domain.Interfaces;
using Loan.Infra.Data.Repositories;
using LoanLoan.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BookLoan.Infra.Ioc
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {

            // DATABASE
            service.AddDbContext<SystemDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(SystemDbContext).Assembly.FullName));
            });


            // TOKEN JWT
            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var secretKey = configuration["JWT:SecretKey"] ?? throw new ArgumentException("Invalid Secret key");

                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
            //

            //Automapper
            service.AddAutoMapper(typeof(EntitiesToDTOMappingProfile));


            //Repositories
            service.AddScoped<IClientRepository, ClientRepository>();
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IBookRepository, BookRepository>();
            service.AddScoped<ILoanRepository, LoanRepository>();
            service.AddScoped<IAuthenticateRepository, AuthenticateRepository>();



            //Services
            service.AddScoped<IClientService, ClientService>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IBookService, BookService>();
            service.AddScoped<ILoanService, LoanService>();

            return service;


        }

    }
}
