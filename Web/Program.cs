using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Services.EntitiesServices.DepartmentImageServices;
using Services.EntitiesServices.DepartmentServices;
using Services.EntitiesServices.PositionServices;
using Services.EntitiesServices.SliderServices;
using Services.EntitiesServices.TeacherServices;
using Services.MapperServices;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

//Here is ConnectionString
var connection = builder.Configuration.GetConnectionString("ConnectionDb");
builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(connection));

//Here is Services
builder.Services.AddScoped<IPositionService,PositionService>();
builder.Services.AddScoped<ITeacherService,TeacherService>();
builder.Services.AddScoped<IDepartmentImageService,DepartmentImageService>();
builder.Services.AddScoped<IDepartmentService,DepartmentService>();
builder.Services.AddScoped<ISliderService, SliderService>();

//Here is MapperServices
builder.Services.AddAutoMapper(typeof(IMapperService));

builder.Services.AddControllersWithViews();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
