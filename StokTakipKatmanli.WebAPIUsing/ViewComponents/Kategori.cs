using Microsoft.AspNetCore.Mvc;
using StokTakipKatmanli.Core.Entities;

namespace StokTakipKatmanli.WebAPIUsing.ViewComponents
{
	public class Kategori : ViewComponent
	{
		static string _apiAdres = "http://localhost:5045/Api/Categories";
		HttpClient _httpClient = new();

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var model = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres);
			return View(model);
		}
	}
}
