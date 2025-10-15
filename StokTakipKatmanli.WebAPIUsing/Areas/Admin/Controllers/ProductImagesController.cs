using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StokTakipKatmanli.Core.Entities;
using StokTakipKatmanli.WebAPIUsing.Tools;
using System.Xml.Linq;

namespace StokTakipKatmanli.WebAPIUsing.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Policy = "AdminPolicy")]
	public class ProductImagesController : Controller
	{
		private readonly HttpClient _httpClient;

		public ProductImagesController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		static string _apiAdres = "http://localhost:7205/api/ProductImages";
		static string _apiAdres2 = "http://localhost:7205/api/Products";
		
		async Task YukleAsync()
		{
			var liste = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdres2);
			ViewBag.ProductId = new SelectList(liste, "Id", "Name");
		}
		// GET: ProductImagesController
		public async Task<ActionResult> IndexAsync()
		{
			var model = await _httpClient.GetFromJsonAsync<List<ProductImage>>(_apiAdres);
			return View(model);
		}

		// GET: ProductImagesController/Details/5
		public async Task<ActionResult> DetailsAsync(int id)
		{
			var model = await _httpClient.GetFromJsonAsync<ProductImage>($"{_apiAdres}/{id}");
			return View(model);
		}

		// GET: ProductImagesController/Create
		public async Task<ActionResult> CreateAsync()
		{
			await YukleAsync();
			return View();
		}

		// POST: ProductImagesController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateAsync(ProductImage collection, IFormFile? Name)
		{
			if (ModelState.IsValid)
			{
				try
				{
					if (Name is not null)
						collection.Name = FileHelper.FileLoader(Name);
					var response = await _httpClient.PostAsJsonAsync(_apiAdres, collection);
					if (response.IsSuccessStatusCode)
					{
						return RedirectToAction(nameof(IndexAsync));
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

		// GET: ProductImagesController/Edit/5
		public async Task<ActionResult> EditAsync(int id)
		{
			await YukleAsync();
			var model = await _httpClient.GetFromJsonAsync<ProductImage>($"{_apiAdres}/{id}");
			return View(model);
		}

		// POST: ProductImagesController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditAsync(int id, ProductImage collection, IFormFile? Name)
		{
			if (ModelState.IsValid)
			{
				try
				{
					if (Name is not null)
						collection.Name = FileHelper.FileLoader(Name);
					var response = await _httpClient.PutAsJsonAsync(_apiAdres + "/" + id, collection);
					if (response.IsSuccessStatusCode)
					{
						return RedirectToAction(nameof(IndexAsync));
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

		// GET: ProductImagesController/Delete/5
		public async Task<ActionResult> DeleteAsync(int id)
		{
			var model = await _httpClient.GetFromJsonAsync<ProductImage>($"{_apiAdres}/{id}");
			return View(model);
		}

		// POST: ProductImagesController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteAsync(int id, ProductImage collection)
		{
			try
			{
				var response = await _httpClient.DeleteAsync($"{_apiAdres}/{id}");
				if (response.IsSuccessStatusCode)
				{
					if (!string.IsNullOrEmpty(collection.Name))
						FileHelper.FileRemover(collection.Name);
					return RedirectToAction(nameof(IndexAsync));
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
