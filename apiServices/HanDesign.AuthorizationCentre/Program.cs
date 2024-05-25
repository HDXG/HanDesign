using Autofac.Core;
using HanDesign.AspNetCore.Extensions;
using HanDesign.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace HanDesign.AuthorizationCentre
{
    public class Program
    {
        const string apiServiceName = "HanDesign.AuthorizationCentre";
        const string apiServiceVersion = "V1";
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.AutoFac("HanDesign.Authorization");
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            #region 自定义配置
            builder.Services.AddDbContext<IAuthorizationContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("ctwContext")));
            //配置jwt内容
            builder.Services.UserJwt(builder.Configuration);
            builder.Services.ConfigurationSwagger(apiServiceName,apiServiceVersion);
            #endregion
            var app = builder.Build();
            #region 自定义配置
            app.UseSwagger(apiServiceName,apiServiceVersion);
            #endregion
            app.UseHttpsRedirection();
            //启用身份认证
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
