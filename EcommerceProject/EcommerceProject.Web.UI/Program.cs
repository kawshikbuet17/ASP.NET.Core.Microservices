using EcommerceProject.Web.UI.Service;
using EcommerceProject.Web.UI.Service.IService;
using EcommerceProject.Web.UI.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor(); //additional
builder.Services.AddHttpClient(); //additional
builder.Services.AddHttpClient<ICouponService,CouponService>(); //additional
builder.Services.AddHttpClient<IAuthService, AuthService>(); //additional
SD.CouponAPIBase = builder.Configuration["ServiceUrls:CouponAPI"]; //additional
SD.AuthAPIBase = builder.Configuration["ServiceUrls:AuthAPI"]; //additional

builder.Services.AddScoped<IBaseService, BaseService>(); //additional
builder.Services.AddScoped<IAuthService, AuthService>(); //additional
builder.Services.AddScoped<ICouponService, CouponService>(); //additional

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
