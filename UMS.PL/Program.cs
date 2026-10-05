using Microsoft.EntityFrameworkCore;
using UMS.BLL.InterFace;
using UMS.BLL.Repository;
using UMS.DAL;
using UMS.PL.MappingProfile;

namespace UMS.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add services to The Database.
            builder.Services.AddDbContext<UniDBContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            // ── AppService ────────
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            builder.Services.AddScoped<IEmpolyeeRepository, EmpolyeeRepository>();

            builder.Services.AddAutoMapper(m=>m.AddProfile(new EmployeeProfile()));
            builder.Services.AddAutoMapper(m=>m.AddProfile(new DepartmentProfile()));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
        
    }
}
