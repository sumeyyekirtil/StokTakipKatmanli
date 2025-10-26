using StokTakipKatmanli.Core.Entities;

namespace StokTakipKatmanli.WebAPIUsing.Models
{
	//bir view de 2 model kullanmak için açılan model merge yeni model
	public class HomePageViewModel
	{
		//sayfada 1 de ürün listesi, 1 slider listesi kullanacağız
		public IEnumerable<Slider>? Sliders { get; set; } //boş gelebileceği için ?
		public IEnumerable<Product>? Products { get; set; }
	}
}
