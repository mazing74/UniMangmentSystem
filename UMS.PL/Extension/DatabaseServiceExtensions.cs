using Microsoft.EntityFrameworkCore;
using UMS.BLL.InterFace;
using UMS.BLL.Repository;
using UMS.DAL;
using UMS.PL.MappingProfile;

namespace UMS.PL.Extension
{
    public static class DatabaseServiceExtensions
    {

        public static IServiceCollection AddDatabaseServices(this IServiceCollection Services,IConfiguration Configuration)
        {
            Services.AddDbContext<UniDBContext>(Options =>
            {
                Options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            return Services;
        }
    }
}
