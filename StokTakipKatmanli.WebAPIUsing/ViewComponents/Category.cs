using Microsoft.AspNetCore.Mvc;
using StokTakipKatmanli.Core.Entities;

namespace StokTakipKatmanli.WebAPIUsing.ViewComponents
{
	//bu class ana sayfada category list şeklinde açılması için eklendi - viewcomponent yöntemi ile
	public class Category : ViewComponent //miras alma uygulandı
	{
		static string _apiAdres = "http://localhost:5058/api/Categories";
		HttpClient _httpClient = new();

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var model = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres);
			return View(model);
		}//active olan kategorileri göster
		 //bu class genelde gösterileceği için shared - components - add view - default added
	}
}
