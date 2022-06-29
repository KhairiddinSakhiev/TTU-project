using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Services.EntitiesServices.DepartmentImageServices;
using Services.EntitiesServices.DepartmentServices;
using Services.EntitiesServices.SliderServices;
using Services.MapperServices;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("ConnectionDb");
builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(connection));

var t = builder.Environment;
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IDepartmentImageService,DepartmentImageService>();
builder.Services.AddScoped<IDepartmentService,DepartmentService>();
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddAutoMapper(typeof(IMapperService));


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
