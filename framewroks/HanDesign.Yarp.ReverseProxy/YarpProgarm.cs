using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HanDesign.Yarp.ReverseProxy
{
    public  class YarpProgarm
    {
        public static void Run(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //添加json配置文件 读取配置
            builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
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
                    await context.Response.WriteAsJsonAsync(new { Message = "维护中......" });
                }
                await next(context);
            });
            app.Run();
        }
    }
}
