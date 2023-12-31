using EcommerceProject.Web.UI.Service;
using EcommerceProject.Web.UI.Service.IService;
using EcommerceProject.Web.UI.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor(); //additional
builder.Services.AddHttpClient(); //additional
builder.Services.AddScoped<ITokenProvider, TokenProvider>(); //additional
builder.Services.AddHttpClient<ICouponService,CouponService>(); //additional
builder.Services.AddHttpClient<IAuthService, AuthService>(); //additional
SD.CouponAPIBase = builder.Configuration["ServiceUrls:CouponAPI"]; //additional
SD.AuthAPIBase = builder.Configuration["ServiceUrls:AuthAPI"]; //additional

builder.Services.AddScoped<IBaseService, BaseService>(); //additional
builder.Services.AddScoped<IAuthService, AuthService>(); //additional
builder.Services.AddScoped<ICouponService, CouponService>(); //additional
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(10);
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
