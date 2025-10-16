using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StokTakipKatmanli.Core.Entities;
using StokTakipKatmanli.WebAPIUsing.Tools;

namespace StokTakipKatmanli.WebAPIUsing.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Policy = "AdminPolicy")]
	public class ProductsController : Controller
	{
		async Task YukleAsync()
		{
			var kategoriler = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres2);
			ViewBag.CategoryId = new SelectList(kategoriler, "Id", "Name");
		}

		private readonly HttpClient _httpClient;
		public ProductsController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		static string _apiAdres = "http://localhost:5058/Api/Products";
		static string _apiAdres2 = "http://localhost:5058/Api/Categories";
		// GET: ProductsController
		public async Task<ActionResult> Index()
		{
			var model = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdres);
			return View(model);
		}

		// GET: ProductsController/Details/5
		public async Task<ActionResult> Details(int id)
		{
			var model = await _httpClient.GetFromJsonAsync<Product>($"{_apiAdres}/{id}");
			return View(model);
		}

		// GET: ProductsController/Create
		public async Task<ActionResult> Create()
		{
			await YukleAsync();
			return View();
		}

		// POST: ProductsController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateAsync(Product collection, IFormFile? Image)
		{
			if (ModelState.IsValid)
			{
				try
				{
					if (Image is not null)
						collection.Image = FileHelper.FileLoader(Image);
					var response = await _httpClient.PostAsJsonAsync(_apiAdres, collection);
					if (response.IsSuccessStatusCode)
					{
						return RedirectToAction(nameof(Index));
					}
					ModelState.AddModelError("", "Kayıt Başarısız!");
				}
				catch
				{
					ModelState.AddModelError("", "Hata Oluştu!");
				}
			}
			await YukleAsync();
			return View(collection);
		}

		// GET: ProductsController/Edit/5
		public async Task<ActionResult> Edit(int id)
		{
			await YukleAsync();
			var model = await _httpClient.GetFromJsonAsync<Product>($"{_apiAdres}/{id}");
			return View(model);
		}

		// POST: ProductsController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(int id, Product collection, IFormFile? Image, bool resmiSil)
		{
			if (ModelState.IsValid)
			{
				try
				{
					if (resmiSil == true)
					{
						if (!string.IsNullOrEmpty(collection.Image))
							FileHelper.FileRemover(collection.Image);
						collection.Image = string.Empty;
					}
					if (Image is not null)
						collection.Image = FileHelper.FileLoader(Image);
					var response = await _httpClient.PutAsJsonAsync(_apiAdres + "/" + id, collection);
					if (response.IsSuccessStatusCode)
					{
						return RedirectToAction(nameof(Index));
					}
					ModelState.AddModelError("", "Kayıt Başarısız!");
				}
				catch
				{
					ModelState.AddModelError("", "Hata Oluştu!");
				}
			}
			await YukleAsync();
			return View(collection);
		}

		// GET: ProductsController/Delete/5
		public async Task<ActionResult> Delete(int id)
		{
			var model = await _httpClient.GetFromJsonAsync<Product>($"{_apiAdres}/{id}");
			return View(model);
		}

		// POST: ProductsController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Delete(int id, Product collection)
		{
			try
			{
				var response = await _httpClient.DeleteAsync($"{_apiAdres}/{id}");
				if (response.IsSuccessStatusCode)
				{
					if (!string.IsNullOrEmpty(collection.Image))
						FileHelper.FileRemover(collection.Image);
					return RedirectToAction(nameof(Index));
				}
				ModelState.AddModelError("", "Kayıt Başarısız!");
			}
			catch
			{
				ModelState.AddModelError("", "Hata Oluştu!");
			}
			return View(collection);
		}
	}
}
