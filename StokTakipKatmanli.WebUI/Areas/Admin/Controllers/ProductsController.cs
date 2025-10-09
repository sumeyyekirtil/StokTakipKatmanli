using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StokTakipKatmanli.Core.Entities;
using StokTakipKatmanli.Data;
using StokTakipKatmanli.WebUI.Tools;

namespace StokTakipKatmanli.WebUI.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")]
	public class ProductsController : Controller
	{
		private readonly DatabaseContext _context;

		public ProductsController(DatabaseContext context)
		{
			_context = context;
		}

		// GET: ProductsController
		public ActionResult Index() //razor view - list
		{
			//return View(_context.Products.Include("Category")); //categori ismi yazdırmak için listede join işlemi yapıldı 1.yol
			return View(_context.Products.Include(c => c.Category)); //2.yol
		}

		// GET: ProductsController/Details/5
		public ActionResult Details(int id) //razor view - details
		{
			return View(_context.Products.Find(id));
		}

		// GET: ProductsController/Create
		public ActionResult Create() //razor view - create
		{
			ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name"); //dropdown için kategorileri getir
			return View();
		}

		// POST: ProductsController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Product collection, IFormFile? Image)
		{
			if (!ModelState.IsValid)
				return View(collection);
			try
			{
				if (Image is not null)
				{
					collection.Image = FileHelper.FileLoader(Image);
				}
				_context.Products.Add(collection);
				_context.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				ModelState.AddModelError("", "Hata Oluştu!");
			}
			ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name"); //get metodunda dönen meton post olunca boş dönmemesi için burada tekrarlıyoruz
			return View(collection);
		}

		// GET: ProductsController/Edit/5
		public ActionResult Edit(int id) //razor view - edit
		{
			ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name");
			return View(_context.Products.Find(id));
		}

		// POST: ProductsController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, Product collection, IFormFile? Image, bool resmiSil)
		{
			if (ModelState.IsValid)
			{
				try
				{
					if (resmiSil == true)
					{
						FileHelper.FileRemover(collection.Image); //resmi db silmek için
						collection.Image = string.Empty;
					}
					if (Image is not null)
					{
						collection.Image = FileHelper.FileLoader(Image);
					}
					_context.Products.Update(collection);
					_context.SaveChanges();
					return RedirectToAction(nameof(Index));
				}
				catch
				{
					ModelState.AddModelError("", "Hata Oluştu!");
				}
			}
			ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name");
			return View(collection);
		}

		// GET: ProductsController/Delete/5
		public ActionResult Delete(int id) //razor view - delete
		{
			return View(_context.Products.Find(id));
		}

		// POST: ProductsController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, Product collection, IFormFile? Image)
		{
			try
			{
				if (!string.IsNullOrEmpty(collection.Image)) //tekli işlemde gerek yok
					FileHelper.FileRemover(collection.Image);
				_context.Products.Remove(collection);
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
