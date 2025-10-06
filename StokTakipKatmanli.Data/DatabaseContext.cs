using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using StokTakipKatmanli.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

//add referance -> core layered
namespace StokTakipKatmanli.Data
{
	// PM -> add-migration InitialCreate -> update-database (default project - data)
	//Microsoft.EntityFrameworkCore.Design -> WebUI
	//Sql server , tools entity framework -> Data import
	public class DatabaseContext : DbContext //DbContext ile aynı isimle class açılmıyor
	{
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }
		public DbSet<Slider> Sliders { get; set; }
		public DbSet<User> Users { get; set; }

		//override on ile iki modeli entegre edildi db bağlantı için
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{

			optionsBuilder.UseSqlServer(@"Server=ASUS-PRO; database=StokTakipLayered; integrated security=true; TrustServerCertificate=True;").ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning)); //hata almamak için
		}

		//örnek veri eklemesi yapılır
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasData(
				new User()
				{
					Id = 1,
					CreateDate = DateTime.Now,
					UserName = "Test",
					Name = "Selim",
					Surname = "Sel",
					Email = "test@gmail.com",
					IsActive = true,
					IsAdmin = true,
					Password = "123"
				}
			);
		}
	}
}
