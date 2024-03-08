using Microsoft.EntityFrameworkCore;
using NowaitechDomain.ExcelDbContext;
using System.Reflection;

namespace NowaitechDomain
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<EntityDbContext>
               (
                   options => options
                   .UseNpgsql
                   (
                       builder.Configuration.GetConnectionString("NowaiTechDatabase"),
                       b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)
                   )
                   .UseLazyLoadingProxies()
                   .LogTo(Console.WriteLine)
               );

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors("myCors");

            app.MapControllers();

            app.Run();
        }
    }
}