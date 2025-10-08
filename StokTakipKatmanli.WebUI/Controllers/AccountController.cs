using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using StokTakipKatmanli.Core.Entities;
using StokTakipKatmanli.Service.Abstract;
using System.Security.Claims;

namespace StokTakipKatmanli.WebUI.Controllers
{
	public class AccountController : Controller
	{
		private readonly IUserService _userService;

		public AccountController(IUserService userService)
		{
			_userService = userService;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Login() //empty view
		{
			return View();
		}

		// Kullanıcı doğrulama işlemleri burada yapılacak (veritabanı kontrolü)
		[HttpPost]
		public IActionResult Login(string email, string password)
		{
			var user = _userService.GetUser(u => u.Email == email && u.Password == password);
			if (user != null)
			{

				// Giriş başarılı, kullanıcıyı yönlendir
				var haklar = new List<Claim>() //kullanıcı hakları tanımladık
				{
					new(ClaimTypes.Email, user.Email), //claim = hak (kullanıcıya tanımlanan haklar)
						new(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User") //giriş yapan kullanıcı admin yetkisiyle değilse user yetkisiyle giriş yapsın.
				};
				var kullaniciKimligi = new ClaimsIdentity(haklar, "Login"); //kullanıcı için bir kimlik oluşturduk
				ClaimsPrincipal claimsPrincipal = new(kullaniciKimligi); //bu sınıftan bir nesne oluşturup bilgilerde saklı haklar ile kural oluşturulabilir
				HttpContext.SignInAsync(claimsPrincipal); //yukarıdaki yetkilerle sisteme giriş yaptık
				return RedirectToAction("Index", "Home");
			}else
			{
				// Giriş başarısız, hata mesajı göster
				@TempData["Message"] = "<div class='alert alert-danger'>Giriş Başarısız</div>";
			}
			return RedirectToAction("Login", "Account");
		}

		public ActionResult LogOut() //çıkış yap aktivasyonu : layout
		{
			HttpContext.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		public IActionResult AccessDenied() //varsayılan metod yolu açtık - giriş engeli vermek için
		{
			return View();
		}

		public IActionResult Register() //empty view
		{
			// kayıt ol varsayılan metot
			return View();
		}

		[HttpPost]
		public IActionResult Register(User user)
		{
			if (ModelState.IsValid)
			{
				try
				{
					user.IsActive = true;
					user.IsAdmin = false;
					//_context.Users.Add(user);
					//_context.SaveChanges();
					_userService.AddUser(user);
					_userService.Save();
					TempData["Message"] = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
                     <strong>Kayıt İşlemi Başarılı! Giriş Yapabilirsiniz.!</strong>
                     <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
                     </div>";
					return RedirectToAction("Login", "Account");
				}
				catch (Exception)
				{
					ModelState.AddModelError("", "Kayıt sırasında bir hata oluştu!");
				}
			}
			return View(user);
		}
	}
}
