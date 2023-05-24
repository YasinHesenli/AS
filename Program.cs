using Lumia.DAL;
using Lumia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Lumia
{
    

    
        public class Program
        {
            public static void Main(string[] args)
            {


                var builder = WebApplication.CreateBuilder(args);
                builder.Services.AddControllersWithViews();

                builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
                builder.Services.AddIdentity<AppUser,IdentityRole>(options =>

                {
                    options.Password.RequiredUniqueChars = 1;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireUppercase = true;


                    options.User.RequireUniqueEmail = true;

                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;
                }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
                var app = builder.Build();

            

                app.UseStaticFiles();
                app.UseRouting();
                app.UseAuthentication();
                app.UseAuthorization();



                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                                    name: "areas",

                                    pattern: "{area:exists}/{controller}/{action}/{id?}"

                                    );
                });

                app.MapControllerRoute(
                    name: "Default",

                    pattern: "{controller=home}/{action=index}/{id?}"

                    );

                app.Run();
            }
        }
    
}