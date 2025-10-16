using System.ComponentModel.DataAnnotations;

namespace StokTakipKatmanli.Core.Entities
{
	public class User
	{
		//eğer sonradan ? nullable eklenirse add-migration Nullable -> update-database zorunlu!
		public int Id { get; set; }

		[Display(Name = "Kullanıcı Adı"), StringLength(50)]
		public string UserName { get; set; } //? koyulmazsa zorunlu alan olur dolayısıyla create de input olmalıdır yoksa kayıt yapılamaz!!

		[Display(Name = "Şifre"), StringLength(50), Required(ErrorMessage = "{0} Boş Geçilemez")]
		public string Password { get; set; }

		[StringLength(50), EmailAddress, Required(ErrorMessage = "{0} Boş Geçilemez")]
		public string Email { get; set; }

		[Display(Name = "Adı"), StringLength(50)]
		public string? Name { get; set; } 
		//? işareti bu alanın nullable geçilebilir olmasını sağlar

		[Display(Name = "Soyadı")]
		public string? Surname { get; set; }

		[Display(Name = "Aktif")]
		public bool IsActive { get; set; }

		[Display(Name = "Admin?")]
		public bool IsAdmin { get; set; }

		[Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
		public DateTime CreateDate { get; set; }
		public Guid? UserGuid { get; set; } //Jwt için property ler
		public string? RefreshToken { get; set; }
		public DateTime? RefreshTokenExpireDate { get; set; }
	}
}
