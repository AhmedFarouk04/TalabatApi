using System.Runtime.CompilerServices;

namespace Talabat.APIs.Extensions
{
    public  static class SwaggerServicesExtentions
    {

        public static IServiceCollection AddSwaggerServices(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();
            return Services;
        }
        public static WebApplication UseSwaggerMiddlware(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }
        

        
    }
}
