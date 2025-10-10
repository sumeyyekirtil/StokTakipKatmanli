using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StokTakipKatmanli.Core.Entities;
using StokTakipKatmanli.Data;

namespace StokTakipKatmanli.WebUI.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")]
	public class SlidersController : Controller
	{
		private readonly DatabaseContext _context;

		public SlidersController(DatabaseContext context)
		{
			_context = context;
		}

		// GET: SlidersController
		public ActionResult Index() //add razor view - list
		{
			return View(_context.Sliders); //null referance exceptions hatası
		}

		// GET: SlidersController/Details/5
		public ActionResult Details(int id) //add razor view - details
		{
			var bilgi = _context.Sliders.Find(id);
			return View(bilgi);
		}

		// GET: SlidersController/Create
		public ActionResult Create() //add razor view - create
		{
			return View();
		}

		// POST: SlidersController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Slider collection, IFormFile? Image) //entities import, ımage import
		{
			if (ModelState.IsValid)
			{
				try
				{
					if (Image is not null)
					{
						string klasor = Directory.GetCurrentDirectory() + "/wwwroot/Images/"; //dosya yolu adresi
						using var stream = new FileStream(klasor + Image.FileName, FileMode.Create); //yeni dosya olarak yükle
						Image.CopyTo(stream); //konumu kopyala
						collection.Image = Image.FileName; //dosya adı ile kaydet
					}
					_context.Sliders.Add(collection);
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

		// GET: SlidersController/Edit/5
		public ActionResult Edit(int id) //add razor view - edit
		{
			var bilgi = _context.Sliders.Find(id);
			return View(bilgi);
		}

		// POST: SlidersController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, Slider collection, IFormFile? Image)
		{
			if (ModelState.IsValid)
			{
				try
				{
					if (Image is not null)
					{
						string klasor = Directory.GetCurrentDirectory() + "/wwwroot/Images/"; //dosya yolu adresi
						using var stream = new FileStream(klasor + Image.FileName, FileMode.Create); //yeni dosya olarak yükle
						Image.CopyTo(stream); //konumu kopyala
						collection.Image = Image.FileName; //dosya adı ile kaydet
					}
					_context.Sliders.Update(collection);
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

		// GET: SlidersController/Delete/5
		public ActionResult Delete(int id) //add razor view - delete
		{
			var bilgi = _context.Sliders.Find(id);
			return View(bilgi);
		}

		// POST: SlidersController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, Slider collection)
		{
			try
			{
				_context.Sliders.Remove(collection);
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
