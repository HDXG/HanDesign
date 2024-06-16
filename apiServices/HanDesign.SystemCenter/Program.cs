using HanDesign.AspNetCore.Extensions;
using HanDesign.AspNetCore.Filter;
using HanDesign.System.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HanDesign.SystemCenter
{
    public class Program
    {
        const string apiServiceName = "HanDesign.SystemCenter";
        const string apiServiceVersion = "v1";
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region ÅäÖÃÄÚÈÝ
            builder.Host.AutoFac("HanDesign.System");
            builder.Services.AddControllers(option => {
                option.Filters.Add<HttpResponseExceptionFilter>();
                option.Filters.Add<HttpResponseSuccessFilter>();
            });
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ISystemContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("ctwContext")));
            builder.Services.ConfigurationSwagger(apiServiceName, apiServiceVersion);
            //builder.Services.AddCors(options =>
            //{
            //    options.AddDefaultPolicy(builde =>
            //    {
            //        builde.WithOrigins(
            //        builder.Configuration["App:CorsOrigins"]
            //        .Split(",", StringSplitOptions.RemoveEmptyEntries).ToArray())
            //                       .SetIsOriginAllowedToAllowWildcardSubdomains()
            //                       .AllowAnyHeader()
            //                       .AllowAnyMethod()
            //                       .AllowCredentials();
            //    });
            //});

            #endregion


            var app = builder.Build();
            //app.UseCors();
            app.UseSwagger(apiServiceName, apiServiceVersion);

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
