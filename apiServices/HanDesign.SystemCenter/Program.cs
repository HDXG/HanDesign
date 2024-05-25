using HanDesign.AspNetCore.Extensions;
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
            #region ≈‰÷√ƒ⁄»›
            builder.Host.AutoFac("HanDesign.System");
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ISystemContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("ctwContext")));
            builder.Services.ConfigurationSwagger(apiServiceName, apiServiceVersion);
            #endregion


            var app = builder.Build();

            app.UseSwagger(apiServiceName, apiServiceVersion);

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
