using Microsoft.AspNetCore.Mvc;
using StokTakipKatmanli.Core.Entities;
using StokTakipKatmanli.WebAPIUsing.Models;
using StokTakipKatmanli.WebAPIUsing.Tools;
using System.Diagnostics;

namespace StokTakipKatmanli.WebAPIUsing.Controllers
{
	public class HomeController : Controller
	{
		private readonly HttpClient _httpClient;
		public HomeController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		static string _apiAdres = "http://localhost:5058/Api/";

		public async Task<IActionResult> Index()
		{
			var model = new HomePageViewModel
			{
				
			};
			model.Sliders = await _httpClient.GetFromJsonAsync<List<Slider>>(_apiAdres + "Sliders");
			model.Products = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdres + "Products/GetHomePageProducts");
			return View(model);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult ContactUs()
		{
			return View();
		}

		[HttpPost]
		public IActionResult ContactUs(string nameSurname, string email, string message)
		{
			string mesaj = $"Ad Soyad: {nameSurname} <hr> E mail: {email} <hr> Mesaj: {message} ";
			try
			{
				MailHelper.SendMail("mail@gmail.com", "Siteden email geldi", mesaj);
				TempData["Message"] = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
                <strong>Teşekkürler.. Mesajınız Gönderildi!</strong> 
                <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
                </div> ";
			}
			catch (Exception)
			{
				TempData["Message"] = @"<div class=""alert alert-danger alert-dismissible fade show"" role=""alert"">
                <strong>Hata Oluştu! Mesaj Gönderilemedi!</strong> 
                <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
                </div> ";
			}
			return RedirectToAction("ContactUs");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
