
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
			builder.Services.AddDbContext<DatabaseContext>(); //vt baðlantýsý

			builder.Services.AddScoped<IUserService, UserService>();

			builder.Services.AddScoped(typeof(IService<>), typeof(Service<>)); // Generic Servis
			builder.Services.AddScoped<ICategoryService, CategoryService>();
			builder.Services.AddTransient<IProductService, ProductService>();


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.MapOpenApi();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
