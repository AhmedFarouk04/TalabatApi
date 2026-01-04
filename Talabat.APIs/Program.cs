
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Threading.Tasks;
using Talabat.APIs.Extensions;
using Talabat.APIs.Helper;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositories;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.APIs.Middlewares;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region Configure Service work with Dependency Injection
            builder.Services.AddControllers();
            //--- DataBases
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }


          );
            builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
            {
             var connection = builder.Configuration.GetConnectionString("Redis");
                    return ConnectionMultiplexer.Connect(connection);
               });
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
            options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")); }
            );
           
            //----Extenstions Services 
            builder.Services.AddAppExtentionServices();
            builder.Services.AddIdentityServices(builder.Configuration);


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerServices();

            //builder.Services.AddScoped<IGenericRepository<Product>, IGenericRepository<Product>>();
            //builder.Services.AddScoped<IGenericRepository<ProductBrand>, IGenericRepository<ProductBrand>>();
            //builder.Services.AddScoped<IGenericRepository<ProductType>, IGenericRepository<ProductType>>();




            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", policy =>
                {
                    policy
                        .WithOrigins(builder.Configuration["FrontUrl"])
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                        
                });
            });

            #endregion

            var app = builder.Build();

            #region Update-Database inside Main
            var scope = app.Services.CreateScope();//all services scoped
            var services = scope.ServiceProvider;//DI
            //loogerfactory
            var LoogerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var DbContext = services.GetRequiredService<StoreContext>();//Ask Clr to create Object
                await DbContext.Database.MigrateAsync();//update database
                await StoreContextSeed.SeedAsync(DbContext);
                var identityDbContext = services.GetRequiredService<AppIdentityDbContext>();

                await identityDbContext.Database.MigrateAsync();
                var userManger = services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUsersAsync(userManger);  
            }
            catch (Exception ex)
            {

                var Logger = LoogerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "an error Ocured  during apply the Migrations");

            }
            //i need object of dbcontext  fresh جاهز 
            // explixtity 
            //StoreContext DbContect = new StoreContext();//need object (error)
            //await DbContect.Database.MigrateAsync();//updata-database

            #endregion

            #region Configure request into Piplines 
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlware();  
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePagesWithRedirects("/errors/{0}");
            app.UseCors("MyPolicy");  
            app.UseAuthentication();   
            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            app.Run();

            #endregion

        }
    }
}
   