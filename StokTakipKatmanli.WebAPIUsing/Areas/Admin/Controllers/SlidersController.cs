﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StokTakipKatmanli.Core.Entities;
using StokTakipKatmanli.WebAPIUsing.Tools;
using static System.Net.Mime.MediaTypeNames;

namespace StokTakipKatmanli.WebAPIUsing.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Policy = "AdminPolicy")]
	public class SlidersController : Controller
	{
		private readonly HttpClient _httpClient;

		public SlidersController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		static string _apiAdres = "https://localhost:7205/Api/Sliders";
		// GET: SlidersController
		public async Task<ActionResult> IndexAsync()
		{
			var model = await _httpClient.GetFromJsonAsync<List<Slider>>(_apiAdres);
			return View(model);
		}

		// GET: SlidersController/Details/5
		public async Task<ActionResult> DetailsAsync(int id)
		{
			var model = await _httpClient.GetFromJsonAsync<Slider>($"{_apiAdres}/{id}");
			return View(model);
		}

		// GET: SlidersController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: SlidersController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateAsync(Slider collection, IFormFile? Image)
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

		// GET: SlidersController/Edit/5
		public async Task<ActionResult> EditAsync(int id)
		{
			var model = await _httpClient.GetFromJsonAsync<Slider>($"{_apiAdres}/{id}");
			return View(model);
		}

		// POST: SlidersController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(int id, Slider collection, IFormFile? Image)
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

		// GET: SlidersController/Delete/5
		public async Task<ActionResult> DeleteAsync(int id)
		{
			var model = await _httpClient.GetFromJsonAsync<Slider>($"{_apiAdres}/{id}");
			return View(model);
		}

		// POST: SlidersController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteAsync(int id, Slider collection)
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
