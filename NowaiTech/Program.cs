
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using NowaitechDomain.ExcelDbContext;
using NowaitechShared.Models.User;
using System.Reflection;
using System.Text;

namespace NowaiTechAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NowaiTech", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Here is enter the Bearer key!",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Name = HeaderNames.Authorization,
                    Scheme = "Bearer"
                });
                c.OperationFilter<SwaggerAuthenticateHeaderFilter>();
            });

            builder.Services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(
                o =>
                {
                    var jsonKey = builder.Configuration.GetSection("JWT:Key").Value;

                    if (jsonKey == null)
                    {
                        throw new ArgumentNullException($"{nameof(jsonKey)}JWT Key Is Null");
                    }

                    var key = Encoding.UTF8.GetBytes(jsonKey);


                    o.SaveToken = true;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration.GetSection("JWT:Issuer").Value,
                        ValidAudience = builder.Configuration.GetSection("JWT:Audience").Value,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true
                    };

                    o.Events = new JwtBearerEvents()
                    {
                        //OnMessageReceived = messageReceivedContext =>
                        //{
                        //    var authToken = messageReceivedContext.Token;

                        //    Console.WriteLine(authToken);

                        //    messageReceivedContext.Success();

                        //    return Task.CompletedTask;
                        //}
                    };
                });
            
            builder.Services.AddCors(
                cors => cors.AddPolicy("myCors", pb =>
                {
                    pb.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod().Build();
                })
                );
            builder.Services.AddLogging(
                log =>
                {
                    log.ClearProviders();
                    log.AddDebug();
                    log.AddConsole();
                }
                );

            builder.Services.AddAuthorization(
                opt =>
                {
                    opt.AddPolicy(UserTypeEnums.Admin.ToString(), policy => policy.RequireClaim("Role", "1"));
                    opt.AddPolicy(UserTypeEnums.RegularUser.ToString(), policy => policy.RequireClaim("Role", "2"));
                }
                );

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

            var app = builder.Build();

            //using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            //{
            //    var context = scope.ServiceProvider.GetRequiredService<EntityDbContext>();
            //    context.Database.EnsureCreated();
            //}

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("myCors");
            app.MapControllers();
            //app.UseMiddleware<WorkedMiddleware>();

            //app.UseMiddleware<ExceptionMiddleware>(); it not work, work on it!
            //app.UseMiddleware<AuthenticationsMiddleware>();  //its worked but check all endpoint which has token?
            //app.UseWorkedM();


            app.Run();
        }
    }
}