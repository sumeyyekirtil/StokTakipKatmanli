﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SH1ProjeUygulamasi.Core.Models;
using StokTakipKatmanli.Core.Entities;
using System.Security.Claims;

namespace StokTakipKatmanli.WebAPIUsing.Controllers
{
	public class AccountController : Controller
	{
		private readonly HttpClient _httpClient;
		public AccountController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		static string _apiAdres = "https://localhost:7205/api/Auth/";

		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(UserLoginModel userLoginModel)
		{
			if (ModelState.IsValid)
			{
				var sonuc = await _httpClient.PostAsJsonAsync(_apiAdres + "Login", userLoginModel);
				if (sonuc.IsSuccessStatusCode)
				{
					var user = await sonuc.Content.ReadFromJsonAsync<User>();

					var haklar = new List<Claim>() // kullanıcı hakları tanımladık
                    {
						new(ClaimTypes.Email, user.Email),
						new(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
					};
					var kullaniciKimligi = new ClaimsIdentity(haklar, "Login");
					ClaimsPrincipal claimsPrincipal = new(kullaniciKimligi);
					await HttpContext.SignInAsync(claimsPrincipal);

					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", "Giriş Başarısız!");
				}
			}
			return View(userLoginModel);
		}

		public IActionResult Logout()
		{
			HttpContext.SignOutAsync(); // çıkış yap
			return RedirectToAction("Index", "Home");
		}

		public IActionResult AccessDenied()
		{
			return View();
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(User user)
		{
			if (ModelState.IsValid)
			{
				try
				{
					user.IsActive = true;
					user.IsAdmin = false;
					user.CreateDate = DateTime.Now;
					var sonuc = await _httpClient.PostAsJsonAsync(_apiAdres + "Register", user);
					if (sonuc.IsSuccessStatusCode)
					{
						TempData["Message"] = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
                        <strong>Kayıt işlemi başarılı! Giriş yapabilirsiniz.</strong> 
                        <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
                        </div> ";
						return RedirectToAction("Login", "Account");
					}
					ModelState.AddModelError("", "Kayıt başarısız oldu!");
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
