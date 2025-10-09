using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StokTakipKatmanli.Core.Entities;
using StokTakipKatmanli.Data;

namespace StokTakipKatmanli.WebUI.Areas.Admin.Controllers
{
	[Authorize(Policy = "AdminPolicy")] //Rol yetkilendirmeli kullanım: sadece admin yetkisi ile giriş yapanlar bu ekranlara erişsin.
	//[Authorize]
	[Area("Admin")]
	public class UsersController : Controller
	{
		private readonly DatabaseContext _context;

		public UsersController(DatabaseContext context)
		{
			_context = context;
		}

		// GET: UsersController
		public ActionResult Index() //add razor view - list
		{
			return View(_context.Users);
		}

		// GET: UsersController/Details/5
		public ActionResult Details(int id) //add razor view - details
		{
			return View(_context.Categories.Find(id));
		}

		// GET: UsersController/Create
		public ActionResult Create() //add razor view - create
		{
			return View();
		}

		// POST: UsersController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(User collection)
		{
			if (!ModelState.IsValid)
				return View(collection);
			try
			{
				_context.Users.Add(collection);
				_context.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				ModelState.AddModelError("", "Hata Oluştu!");
			}
			return View(collection);
		}

		// GET: UsersController/Edit/5
		public ActionResult Edit(int id) //add razor view - edit
		{
			return View(_context.Categories.Find(id));
		}

		// POST: UsersController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, User collection)
		{
			if (ModelState.IsValid)
			{
				try
				{
					_context.Users.Update(collection);
					_context.SaveChanges();
					return RedirectToAction(nameof(Index));
				}
				catch
				{
					ModelState.AddModelError("", "Hata Oluştu!");
				}
			}
			return View(collection);
		}

		// GET: UsersController/Delete/5
		public ActionResult Delete(int id) //add razor view - delete
		{
			return View(_context.Categories.Find(id));
		}

		// POST: UsersController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, User collection)
		{
			try
			{
				_context.Users.Remove(collection);
				_context.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
