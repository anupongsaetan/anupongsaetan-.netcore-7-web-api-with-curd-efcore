
using Microsoft.EntityFrameworkCore;
using WebApplicationEfCore.Models;

namespace WebApplicationEfCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApiDbContext>(options =>
            {
                options.UseSqlServer("Server=localhost;Database=EfCoreDB;User Id=sa;Password=P@ssw0rd;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}