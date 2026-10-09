using UMS.BLL.InterFace;
using UMS.BLL.Repository;
using UMS.PL.MappingProfile;

namespace UMS.PL.Extension
{
    public static class AppServicesExtensions
    {
        public static IServiceCollection AddAppService (this IServiceCollection Services)
        {
            // Add services to the container.
            Services.AddControllersWithViews();
                        // ── AppService ────────

             Services.AddAutoMapper(m=>m.AddProfile(new EmployeeProfile()));
             Services.AddAutoMapper(m=>m.AddProfile(new DepartmentProfile()));

             Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
             Services.AddScoped<IunitOfWork, UnitOfWork>();

            return Services;
        }
    }
}
