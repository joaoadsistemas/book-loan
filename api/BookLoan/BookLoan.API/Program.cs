using BookLoan.API.Context;
using BookLoan.API.Middleware;
using BookLoan.Infra.Ioc;
using Microsoft.EntityFrameworkCore;

namespace BookLoan.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // CLEAN ARCHITECTURE
            builder.Services.AddInfrastructure(builder.Configuration);
            //



            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddInfrastructureSwagger(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // MIDDLEWARE DE ERRO PERSONALIZADO 
            app.UseMiddleware<ExceptionMiddleware>();
            //


            app.UseHttpsRedirection();


            app.UseCors(builder =>
            {
                builder.WithOrigins("https://localhost:7235", "http://localhost:4200", "https://loanbook.azurewebsites.net")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });

            // INTEGRANDO FRONTEND NA API
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.MapControllers();
            app.MapFallbackToController("index", "Fallback");
            //

            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
}
