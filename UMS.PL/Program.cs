using Microsoft.EntityFrameworkCore;
using UMS.BLL.InterFace;
using UMS.BLL.Repository;
using UMS.DAL;
using UMS.PL.Extension;
using UMS.PL.MappingProfile;

namespace UMS.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services Of Application
            builder.Services.AddAppService();

            // Add services to The Database.
            builder.Services.AddDatabaseServices(builder.Configuration);

            var app = builder.Build();

            app.UseMiddlewaresPipeline();
        }

    }
}
