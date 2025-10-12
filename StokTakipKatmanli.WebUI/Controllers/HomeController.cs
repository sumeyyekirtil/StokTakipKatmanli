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
        private readonly IService<Slider> _serviceSlider; //generic service ile yapý solid politikasýna uygun hale getirilir (ilgili service kullanýmý)
        private readonly IProductService _productService;
        //hepsi seçilerek contructor eklenir
        
		public HomeController(IService<Slider> serviceSlider, IProductService productService)
		{
			_serviceSlider = serviceSlider;
			_productService = productService;
		}

        //homepageviewmodel added
        //eðer iki service ayný anda kullanýlacak ise -> view e 2 model ayný anda kullanamama durumundan dolayý -> çoklu abstract larý bir model e tanýmlayýp -> o model üzerinden view ve controller kontrolleri saðlanýr.
		public IActionResult Index()
        { //index te gösterilecek slide listesi için
            var model = new HomePageViewModel
            {
                Sliders = _serviceSlider.GetAll(),//_context.Slider.toList yerine geçer
                Products = _productService.GetProducts(p => p.IsActive && p.IsHome) //active ve gösterimde ise sayfada göster
            };
            return View(); //toList eklenmez ise foreach döngüsünde liste olmadýðý için çýktý vermez -> alýnan hata Invalid DbSet1 hatasýdýr
		}

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ContactUs() //view - empty
        {
            return View();
        }

        //mail iletiþimi için tools-mailhelper
        [HttpPost]
		public IActionResult ContactUs(string nameSurname, string email, string message)
		{
			string mesaj = $"Ad Soyad:  {nameSurname} <hr> E mail:  {email} <hr> Mesaj:  {message}";
            try
            {
                //bootstrap kullanýldý
				MailHelper.SendMail("mail@gmail.com", "Siteden email geldi", message);
				TempData["Message"] = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
                     <strong>Teþekkürler! Mesajýnýz Ýletildi!</strong>
                     <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
                     </div>";
			}
            catch (Exception)
            {
				TempData["Message"] = @"<div class=""alert alert-danger alert-dismissible fade show"" role=""alert"">
                     <strong>Hata Oluþtu! Mesaj gönderilemedi</strong>
                     <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
                     </div>";
			}
			return RedirectToAction("ContactUs"); //yeniden ayný view e git
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
