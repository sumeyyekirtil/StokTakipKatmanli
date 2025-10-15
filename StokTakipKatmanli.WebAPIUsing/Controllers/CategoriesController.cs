using Microsoft.AspNetCore.Mvc;
using StokTakipKatmanli.Core.Entities;

namespace StokTakipKatmanli.WebAPIUsing.Controllers
{
	public class CategoriesController : Controller
	{
		private readonly HttpClient _httpClient;
		public CategoriesController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		static string _apiAdres = "https://localhost:7205/api/";

		public async Task<IActionResult> Index(int? id)
		{
			if (id == null) // eğer adres çubuğundan id gelmezse
			{
				return BadRequest(); // geriye geçersiz istek hatası dön anlamına gelir
			}
			var model = await _httpClient.GetFromJsonAsync<Category>($"{_apiAdres}Categories/{id}");
			if (model == null)
			{
				return NotFound();
			}
			var products = await _httpClient.GetFromJsonAsync<List<Product>>($"{_apiAdres}Products/GetProductsByCategoryId/{id}");
			if (products is not null)
			{
				model.Products = products;
			}
			return View(model);
		}
	}
}
