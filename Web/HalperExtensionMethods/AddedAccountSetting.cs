using Microsoft.AspNetCore.Identity;
using Persistence.Data;

namespace Web.HalperExtensionMethods
{
    public static class AddedAccountSetting
    {
        public static void AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireLowercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Account/AccessDenied/";
                options.LoginPath = "/Account/Login/";
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
            });

        }
    }
}
