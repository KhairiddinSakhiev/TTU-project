using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Serilog;
using Services.EntitiesServices.DepartmentImageServices;
using Services.EntitiesServices.DepartmentServices;
using Services.EntitiesServices.NewsServices;
using Services.EntitiesServices.SliderServices;
using Services.EntitiesServices.StudentServices;
using Services.EntitiesServices.PositionServices;
using Services.EntitiesServices.TeacherServices;
using Services.MapperServices;
using Web.HalperExtensionMethods;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

var connection = builder.Configuration.GetConnectionString("ConnectionDb");
builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(connection));

builder.Services.AddScoped<IDepartmentImageService,DepartmentImageService>();
builder.Services.AddScoped<IDepartmentService,DepartmentService>();
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<IPositionService,PositionService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddAutoMapper(typeof(IMapperService));

builder.Services.AddControllersWithViews();
builder.Services.AddServicesToCointainer();


var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


builder.Services.AddIdentityServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HST`S value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();   
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
   // endpoints.MapAreaControllerRoute(
      endpoints.MapControllerRoute(
      name: "Admin",
     // areaName:"admin",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});
  app.UseStatusCodePagesWithReExecute("/Home/NotFound");
 
app.Run();
