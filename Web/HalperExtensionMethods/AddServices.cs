using Domain.Entities;
using Services.AccountServices;
using Services.EntitiesServices.DepartmentImageServices;
using Services.EntitiesServices.DepartmentServices;
using Services.EntitiesServices.PositionServices;
using Services.EntitiesServices.SliderServices;
using Services.EntitiesServices.TeacherServices;
using Services.MapperServices;

namespace Web.HalperExtensionMethods
{
    public static class AddServices
    {
        public static void AddServicesToCointainer(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentImageService, DepartmentImageService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<AccountService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<ITeacherService, TeacherService>();


            services.AddAutoMapper(typeof(IMapperService));

        }
    }
}
