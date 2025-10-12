using Microsoft.AspNetCore.Authentication.Cookies;
using StokTakipKatmanli.Data;
using StokTakipKatmanli.Service.Abstract;
using StokTakipKatmanli.Service.Concrete;
using System.Security.Claims;

namespace StokTakipKatmanli.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

			builder.Services.AddDbContext<DatabaseContext>(); //uygulama cs dosyasýna eklendi baðlantý adresi için

            //session iþlemi + app.UseSession ile kullanýma açýlýr
			builder.Services.AddSession();

			//InvalidOperationException: Unable to resolve service for type 'StokTakipKatmanli.Service.Abstract.IService`1[StokTakipKatmanli.Core.Entities.Slider]' while attempting to activate 'StokTakipKatmanli.WebUI.Controllers.HomeController'.
			//birden fazla kez kullanýlan servisler için istek baþýna bir kez nesne oluþturduðundan kaynaklarý daha verimli kullanýr. AddTransient ise tamamen stateless iþlemler için mükemmeldir, çünkü her seferinde yeni bir nesne oluþturulmasý gerekiyorsa, bu yaklaþým en iyi performansý saðlar.
			//this correct solved -> addscoped, after IService - 3version =>
			builder.Services.AddScoped<ICategoryService, CategoryService>();
			builder.Services.AddScoped<IUserService, UserService>();
			builder.Services.AddTransient<IProductService, ProductService>();
			builder.Services.AddScoped(typeof(IService<>), typeof(Service<>)); //Generic Service


            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(); //oturum açma

            //altýnda olmalý
            //Authorization: Yetkilendirme: once servis olarak ekliyoruz
            builder.Services.AddAuthorization(x =>
            {
                x.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin")); //bundan sonra controller lara policy belirtmeliyiz
                x.AddPolicy("UserPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "User"));
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
            app.UseRouting();

            app.UseSession();

			app.UseAuthorization();

            app.MapStaticAssets();

            //endpoint - app changed (new scaffolding item - admin added) - controller - main
			app.MapControllerRoute(
			name: "areas",
			pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}");

			app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
