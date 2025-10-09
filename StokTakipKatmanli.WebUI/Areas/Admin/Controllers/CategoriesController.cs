using Microsoft.AspNetCore.Authorization; //authorize using
using Microsoft.AspNetCore.Mvc;
using StokTakipKatmanli.Core.Entities; //category using
using StokTakipKatmanli.Data; //database using
using StokTakipKatmanli.WebUI.Tools; //filehelper using

namespace StokTakipKatmanli.WebUI.Areas.Admin.Controllers
{
	//[Authorize] Düzgün kullanım
	[Authorize(Policy = "AdminPolicy")] //Rol yetkilendirmeli kullanım: sadece admin yetkisi ile giriş yapanlar bu ekranlara erişsin.
	[Area("Admin")]
	public class CategoriesController : Controller
	{
		private readonly DatabaseContext _context;

		public CategoriesController(DatabaseContext context)
		{
			_context = context;
		}

		// GET: CategoriesController
		public ActionResult Index() //add - razor view - list
		{
			return View(_context.Categories);
		}

		// GET: CategoriesController/Details/5
		public ActionResult Details(int id) //add -razor view - details
		{
			return View(_context.Categories.Find(id));
		}

		// GET: CategoriesController/Create
		public ActionResult Create() //razor view - create
		{
			return View();
		}

		// POST: CategoriesController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Category collection, IFormFile? Image)
		{
			if (!ModelState.IsValid)
				return View(collection);
			try
			{
				if (Image is not null)
				{
					//adminde kullanmak için tool - helper eklendi
					collection.Image = FileHelper.FileLoader(Image);
				}
				_context.Categories.Add(collection);
				_context.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				ModelState.AddModelError("", "Hata Oluştu!");
			}
			return View(collection);
		}

		// GET: CategoriesController/Edit/5
		public ActionResult Edit(int id) //razor view - edit
		{
			return View(_context.Categories.Find(id));
		}

		// POST: CategoriesController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, Category collection, IFormFile? Image)
		{
			if (ModelState.IsValid)
			{
				try
				{
					if (Image is not null)
					{
						collection.Image = FileHelper.FileLoader(Image);
					}
					_context.Categories.Update(collection);
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

		// GET: CategoriesController/Delete/5
		public ActionResult Delete(int id) //razor view - delete
		{
			return View(_context.Categories.Find(id));
		}

		// POST: CategoriesController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, Category collection)
		{
			try
			{
				_context.Categories.Remove(collection);
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
