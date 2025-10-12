using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StokTakipKatmanli.Core.Entities;
using StokTakipKatmanli.Service.Abstract;
using StokTakipKatmanli.WebUI.Models;
using StokTakipKatmanli.WebUI.Tools;
using System.Diagnostics;

namespace StokTakipKatmanli.WebUI.Controllers
{
	[AllowAnonymous] //authorize using added
	public class HomeController : Controller
    {
        //default gelen constructor gerek yok ise silinebilir

        //abstract-entities using added
        private readonly IService<Slider> _serviceSlider; //generic service ile yap� solid politikas�na uygun hale getirilir (ilgili service kullan�m�)
        private readonly IProductService _productService;
        //hepsi se�ilerek contructor eklenir
        
		public HomeController(IService<Slider> serviceSlider, IProductService productService)
		{
			_serviceSlider = serviceSlider;
			_productService = productService;
		}

        //homepageviewmodel added
        //e�er iki service ayn� anda kullan�lacak ise -> view e 2 model ayn� anda kullanamama durumundan dolay� -> �oklu abstract lar� bir model e tan�mlay�p -> o model �zerinden view ve controller kontrolleri sa�lan�r.
		public IActionResult Index()
        { //index te g�sterilecek slide listesi i�in
            var model = new HomePageViewModel
            {
                Sliders = _serviceSlider.GetAll(),//_context.Slider.toList yerine ge�er
                Products = _productService.GetProducts(p => p.IsActive && p.IsHome) //active ve g�sterimde ise sayfada g�ster
            };
            return View(); //toList eklenmez ise foreach d�ng�s�nde liste olmad��� i�in ��kt� vermez -> al�nan hata Invalid DbSet1 hatas�d�r
		}

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ContactUs() //view - empty
        {
            return View();
        }

        //mail ileti�imi i�in tools-mailhelper
        [HttpPost]
		public IActionResult ContactUs(string nameSurname, string email, string message)
		{
			string mesaj = $"Ad Soyad:  {nameSurname} <hr> E mail:  {email} <hr> Mesaj:  {message}";
            try
            {
                //bootstrap kullan�ld�
				MailHelper.SendMail("mail@gmail.com", "Siteden email geldi", message);
				TempData["Message"] = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
                     <strong>Te�ekk�rler! Mesaj�n�z �letildi!</strong>
                     <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
                     </div>";
			}
            catch (Exception)
            {
				TempData["Message"] = @"<div class=""alert alert-danger alert-dismissible fade show"" role=""alert"">
                     <strong>Hata Olu�tu! Mesaj g�nderilemedi</strong>
                     <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
                     </div>";
			}
			return RedirectToAction("ContactUs"); //yeniden ayn� view e git
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
