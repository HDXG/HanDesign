using HanDesign.Yarp.ReverseProxy;
namespace HanDesign.ApiService.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
               .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Yarp.ReverseProxy
            builder.Services.AddYarpConfig(builder.Configuration);
            var app = builder.Build();

            var isSunderMaintenance = app.Configuration.GetSection("AppOptions").GetValue<bool>("IsSunderMaintenance");

            if (isSunderMaintenance == false)
            {
                app.Map("/", () =>
                {
                    Results.Redirect("/swagger");
                });
                app.UseYarpSwaggerUI();
                app.MapReverseProxy();
            }

            app.UseHttpsRedirection();

            app.Use(async (context, next) =>
            {
                if (isSunderMaintenance)
                {
                    await context.Response.WriteAsJsonAsync(new {Message="Î¬»¤ÖÐ......" });
                }
                await next(context);
            });
            app.Run();
        }
    }
}
