using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Serilog;
using Services.EntitiesServices.Position;
using Web.HalperExtensionMethods;

var builder = WebApplication.CreateBuilder(args);



var connection = builder.Configuration.GetConnectionString("ConnectionDb");
builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(connection));

// Add services to the container.
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
