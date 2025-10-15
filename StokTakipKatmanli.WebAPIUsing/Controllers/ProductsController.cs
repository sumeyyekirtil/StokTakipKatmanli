using Microsoft.AspNetCore.Mvc;
using StokTakipKatmanli.Core.Entities;

namespace StokTakipKatmanli.WebAPIUsing.Controllers
{
	public class ProductsController : Controller
	{
		private readonly HttpClient _httpClient;
		string _apiAdres = "http://localhost:7205/api/";

		public ProductsController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<IActionResult> Index(string q = "")
		{
			var products = await _httpClient.GetFromJsonAsync<List<Product>>($"{_apiAdres}Products/GetProductsBySearch/{q}");
			return View(products);
		}
		public async Task<IActionResult> Detail(int? id)
		{
			if (id is null)
			{
				return BadRequest("Geçersiz İstek!");
			}
			var model = await _httpClient.GetFromJsonAsync<Product>($"{_apiAdres}Products/{id}");
			if (model == null)
			{
				return NotFound("Ürün Bulunamadı!");
			}
			var productImages = await _httpClient.GetFromJsonAsync<List<ProductImage>>($"{_apiAdres}ProductImages/GetProductImagesByProductId/{id}");
			model.ProductImages = productImages;
			return View(model);
		}
	}
}
