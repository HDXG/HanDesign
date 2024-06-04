using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;
using Yarp.ReverseProxy.Configuration;

namespace HanDesign.Yarp.ReverseProxy
{
    public static class YarpReverseProxyExtensions
    {
        public static void AddYarpConfig(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var reverseProxyOptions = new ReverseProxyOptions();
            //获取  appsettings配置文件中的内容
            var configuartionSection = configuration.GetSection("ReverseProxyOptions");
            //通过配置文件绑定到对象
            configuartionSection.Bind(reverseProxyOptions);
            //配置reverseProxyOptions对象内容
            serviceCollection.Configure<ReverseProxyOptions>(configuartionSection);


            var routes = reverseProxyOptions.Routes.SelectMany(x => new RouteConfig[]
            { 
                x.GetRoteConfig(),
                x.GetSwaggerRouteConfig(),
            }).ToList();
            var clusters = reverseProxyOptions.Clusters.Select(x => x.GetClusterConfig()).ToList();
            //添加配置Yarp内容
            serviceCollection.AddReverseProxy().LoadFromMemory(routes, clusters);

        }
        /// <summary>
        /// 使用 Swagger
        /// </summary>
        /// <param name="app"></param>
        public static void UseYarpSwaggerUI(this IApplicationBuilder app)
        {
            var reverseProxyOptions = app.ApplicationServices.GetService<IOptions<ReverseProxyOptions>>()?.Value;

            app.UseSwaggerUI(options =>
            {
                foreach (var clusterGroup in reverseProxyOptions.Routes)
                {
                    options.SwaggerEndpoint($"/swagger/{clusterGroup.RouteId}/swagger.json", $"{clusterGroup.RouteId} ApiService");
                }

                options.DocExpansion(DocExpansion.None);
                options.DefaultModelsExpandDepth(-1);
            });
        }
    }

}
