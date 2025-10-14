using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StokTakipKatmanli.Core.Entities;
using StokTakipKatmanli.WebAPIUsing.Tools;
using static System.Net.Mime.MediaTypeNames;

namespace StokTakipKatmanli.WebAPIUsing.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Policy = "AdminPolicy")]
	public class CategoriesController : Controller
	{
		static string _apiAdres = "http://localhost:5058/api/Categories";
		HttpClient _httpClient = new HttpClient(); 
		// .net framework deki yapıyı kullanarak
												   
		// GET: CategoriesController
		public async Task<ActionResult> Index()
		{
			var model = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres);
			return View(model);
		}

		// GET: CategoriesController/Details/5
		public async Task<ActionResult> DetailsAsync(int id)
		{
			var model = await _httpClient.GetFromJsonAsync<Category>($"{_apiAdres}/{id}");
			return View(model);
		}

		// GET: CategoriesController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: CategoriesController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateAsync(Category collection, IFormFile? Image)
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
			return View(collection);
		}

		// GET: CategoriesController/Edit/5
		public async Task<ActionResult> EditAsync(int id)
		{
			var model = await _httpClient.GetFromJsonAsync<Category>($"{_apiAdres}/{id}");
			return View(model);
		}

		// POST: CategoriesController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditAsync(int id, Category collection, IFormFile? Image)
		{
			if (ModelState.IsValid)
			{
				try
				{
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
			return View(collection);
		}

		// GET: CategoriesController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: CategoriesController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteAsync(int id, Category collection, IFormFile? Image)
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
