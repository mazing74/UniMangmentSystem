using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using UMS.BLL.InterFace;
using UMS.BLL.Repository;
using UMS.DAL;
using UMS.DAL.Models;
using UMS.PL.MappingProfile;

namespace UMS.PL.Extension
{
    public static class AppServicesExtensions
    {
        public static IServiceCollection AddAppService(this IServiceCollection Services)
        {
            // Add services to the container.
            Services.AddControllersWithViews();
            // ── AppService ────────

            Services.AddAutoMapper(m => m.AddProfile(new EmployeeProfile()));
            Services.AddAutoMapper(m => m.AddProfile(new DepartmentProfile()));

            Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            Services.AddScoped<IunitOfWork, UnitOfWork>();

            //================Identity Sercices===========
            Services.AddIdentity<ApplicationUser, IdentityRole>(op =>
            {
                op.Password.RequiredLength = 4;
                op.Password.RequireNonAlphanumeric = false;
                op.Password.RequireUppercase = true;
                op.Password.RequireLowercase = true;
            }
            ).AddEntityFrameworkStores<UniDBContext>();
            Services.AddAuthentication();

            return Services;
        }
    }
}
