using StokTakipKatmanli.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

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

			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(); //oturum açma
																											   
            //InvalidOperationException: Unable to resolve service for type 'StokTakipKatmanli.Service.Abstract.IService`1[StokTakipKatmanli.Core.Entities.Slider]' while attempting to activate 'StokTakipKatmanli.WebUI.Controllers.HomeController'.
            //this correct solved -> addscoped, after IService


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
