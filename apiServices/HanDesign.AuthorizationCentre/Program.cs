using HanDesign.AspNetCore.Extensions;
using HanDesign.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
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
            builder.Services.AddSwaggerGen();
           
            builder.Services.AddDbContext<IAuthorizationContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("ctwContext")));
            //����jwt����
            builder.Services.UserJwt(builder.Configuration);
            builder.Services.ConfigurationSwagger(apiServiceName,apiServiceVersion);

            //builder.Services.AddCors(options =>
            //{
            //    options.AddDefaultPolicy(builde =>
            //    {
            //        builde.WithOrigins(
            //        builder.Configuration["App:CorsOrigins"]
            //        .Split(",", StringSplitOptions.RemoveEmptyEntries) .ToArray())
            //                       .SetIsOriginAllowedToAllowWildcardSubdomains()
            //                       .AllowAnyHeader()
            //                       .AllowAnyMethod()
            //                       .AllowCredentials();
            //    });
            // });
            var app = builder.Build();
            //app.UseCors();
            #region �Զ�������
            app.UseSwagger(apiServiceName,apiServiceVersion);
            #endregion
            app.UseHttpsRedirection();
            //���������֤
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}