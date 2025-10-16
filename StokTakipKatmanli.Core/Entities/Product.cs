﻿using System.ComponentModel.DataAnnotations;

namespace StokTakipKatmanli.Core.Entities
{
	public class Product : IEntity
	{
		public int Id { get; set; }

		[Display(Name = "Ürün Adı"), StringLength(50), Required(ErrorMessage = "{0} Boş Geçilemez!")]
		public string Name { get; set; }
		[Display(Name = "Ürün Açıklama"), DataType(DataType.MultilineText)]
		public string? Description { get; set; }

		[Display(Name = "Ürün Resmi"), StringLength(100)]
		public string? Image { get; set; }

		[Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)] //ScaffoldColumn : false sayfa oluştururken bu kolon oluşmasın
		public DateTime CreateDate { get; set; } = DateTime.Now;

		[Display(Name = "Durum")]
		public bool IsActive { get; set; }

		[Display(Name = "Anasayfa")]
		public bool IsHome { get; set; }

		[Display(Name = "Stok"), MinLength(0)]
		public int Stock { get; set; }

		[Display(Name = "Fiyat")]
		public decimal Price { get; set; }

		[Display(Name = "Kategori")]
		public int CategoryId { get; set; }

		[Display(Name = "Kategori")]
		public Category? Category { get; set; } //navigation property
		public IList<ProductImage>? ProductImages { get; set; }
		//bir ürünün birden fazla resmi olabilir - olmayabilir?
	}
}
