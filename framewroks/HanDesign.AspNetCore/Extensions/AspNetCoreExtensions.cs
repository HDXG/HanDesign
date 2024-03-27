using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HanDesign.AspNetCore.Extensions
{
    //AspNetCore拓展类
    public static class AspNetCoreExtensions
    {
        /// <summary>
        /// AutoFac注入拓展类
        /// </summary>
        /// <param name="hostbuilder">Microsoft.Extensions.Hosting程序集下</param>
        /// <param name="names">模块名称内容</param>
        public static void AutoFac(this IHostBuilder hostbuilder, params string[] names)
        {
            //  Autofac 用作 ASP.NET Core 应用程序的依赖注入容器
            hostbuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            hostbuilder.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                foreach (var name in names)
                {
                    containerBuilder.RegisterAssemblyTypes(Assembly.Load(name + ".Application"),Assembly.Load(name + ".Infrastructure"))
                        .Where(a => a.Name.EndsWith("AppService") || a.Name.EndsWith("Repository"))
                        .AsImplementedInterfaces();
                }
            });
        }
        /// <summary>
        /// 配置swagger拓展
        /// </summary>
        /// <param name="services">IServiceCollection 内容</param>
        /// <param name="apiServiceName">Api控制器项目中的xml名称(项目文件名称)</param>
        /// <param name="apiServiceVersion">当前版本号内容</param>
        public static void ConfigurationSwagger(this IServiceCollection services, string apiServiceName, string apiServiceVersion)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(apiServiceName, new OpenApiInfo() { Title = apiServiceName, Version = apiServiceVersion });

                options.CustomSchemaIds(type => type.FullName);

                var directoryInfo = new DirectoryInfo(AppContext.BaseDirectory);
                var fileInfos = directoryInfo.GetFileSystemInfos()
                    .Where(a => a.Extension == ".xml")
                    .Where(a => a.Name.EndsWith("Application.xml"));

                foreach (var info in fileInfos)
                {
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, info.Name);
                    options.IncludeXmlComments(xmlPath, true);
                }
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, apiServiceName + ".xml"), true);
                options.DocInclusionPredicate((doc, api) => true);

                options.AddSecurityDefinition("BearerToken", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "请输入Token!"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "BearerToken" }
                        },
                        new string[] {}
                    }
                });
            });
        }
        /// <summary>
        /// 添加进行使用swagger内容
        /// </summary>
        /// <param name="app"></param>
        /// <param name="apiServiceName">Api控制器项目中的xml名称</param>
        /// <param name="apiServiceVersion">当前版本号内容</param>
        public static void UseSwagger(this IApplicationBuilder app, string apiServiceName, string apiServiceVersion)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/swagger/{apiServiceName}/swagger.json", $"{apiServiceName} {apiServiceVersion}");
                options.DocExpansion(DocExpansion.None);
                options.DefaultModelsExpandDepth(-1);
            });
        }


        /// <summary>
        /// 配置swagger内容
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public  static void UserJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
            {
                //取出配置的私钥
                var SecretKeyDemo = Encoding.UTF8.GetBytes(configuration["AuthenticationDemo:SecretKeyDemo"]);
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    //验证发布者
                    ValidateIssuer = true,
                    ValidIssuer = configuration["AuthenticationDemo:IssuerDemo"],
                    //验证接收者
                    ValidateAudience = true,
                    ValidAudience = configuration["AuthenticationDemo:AudienceDemo"],
                    //验证是否过期
                    ValidateLifetime = true,
                    //验证私钥
                    IssuerSigningKey = new SymmetricSecurityKey(SecretKeyDemo)
                };
            });
        }
    }

}
