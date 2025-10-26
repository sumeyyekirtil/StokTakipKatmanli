using StokTakipKatmanli.Data;
using StokTakipKatmanli.Service.Abstract;
using StokTakipKatmanli.Service.Concrete;

namespace StokTakipKatmanli.WebAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
			builder.Services.AddOpenApi();
			builder.Services.AddDbContext<DatabaseContext>(); //vt bağlantısı

			builder.Services.AddScoped<IUserService, UserService>();
			builder.Services.AddScoped(typeof(IService<>), typeof(Service<>)); // Generic Servis
			builder.Services.AddScoped<ICategoryService, CategoryService>();
			builder.Services.AddScoped<IProductService, ProductService>();

			builder.Services.AddCors(options =>
			{
				options.AddPolicy("default", policy =>
				{
					policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod(); // cors hatasýna takýlan tüm istekleri kabul et
				});
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.MapOpenApi();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.UseStaticFiles(); // api de statik dosyaları kullanmak için

			app.UseCors("default");


			app.MapControllers();

			app.Run();
		}
	}
}
