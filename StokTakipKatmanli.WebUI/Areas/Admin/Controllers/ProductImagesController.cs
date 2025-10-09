using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StokTakipKatmanli.Core.Entities;
using StokTakipKatmanli.Data;
using StokTakipKatmanli.Data.Migrations;
using StokTakipKatmanli.WebUI.Tools;

namespace StokTakipKatmanli.WebUI.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")]
	public class ProductImagesController : Controller
	{
		private readonly DatabaseContext _context;

		public ProductImagesController(DatabaseContext context)
		{
			_context = context;
		}

		// GET: ProductImagesController
		public async Task<ActionResult> IndexAsync() //add razor view - list
		{
			return View(await _context.ProductImages.Include(p => p.Product).ToListAsync()); //method async
		}

		// GET: ProductImagesController/Details/5
		public async Task<ActionResult> DetailsAsync(int id) //add razor view - details
		{
			if (id == null)
			{
				return NotFound();
			}

			var productImage = await _context.ProductImages
				.FirstOrDefaultAsync(m => m.Id == id);
			if (productImage == null)
			{
				return NotFound();
			}
			return View(productImage);
		}

		// GET: ProductImagesController/Create
		public ActionResult Create() //add razor view - create
		{
			ViewBag.ProductId = new SelectList(_context.Products, "Id", "Name");
			return View();
		}

		// POST: ProductImagesController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateAsync(productImage productImage, IFormFile? Name)
		{
			if (Name is not null)
				//productImage.Name = FileHelper.FileLoader(Name);
			_context.Add(productImage);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		// GET: ProductImagesController/Edit/5
		public async Task<ActionResult> EditAsync(int id) //add razor view - edit
		{
			if (id == null)
			{
				return NotFound();
			}

			var productImage = await _context.ProductImages.FindAsync(id);
			if (productImage == null)
			{
				return NotFound();
			}
			return View(productImage);
		}

		// POST: ProductImagesController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditAsync(int id, [Bind("Id,ProductId,Name")] ProductImage productImage, IFormFile? Name)
		{
			if (id != productImage.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(productImage);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ProductImageExists(productImage.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(productImage);
		}

		// GET: ProductImagesController/Delete/5
		public async Task<ActionResult> DeleteAsync(int id) //add razor view - delete
		{
			if (id == null)
			{
				return NotFound();
			}
			var productImage = await _context.ProductImages
				.FirstOrDefaultAsync(m => m.Id == id);
			if (productImage == null)
			{
				return NotFound();
			}

			return View(productImage);
		}

		// POST: ProductImagesController/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteAsync(int id, ProductImage collection)
		{
			var productImage = await _context.ProductImages.FindAsync(id);
			if (productImage != null)
			{
				_context.ProductImages.Remove(productImage);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		//tanımlaması yapılan id ye göre bulan metot
		private bool ProductImageExists(int id)
		{
			return _context.ProductImages.Any(e => e.Id == id);
		}
	}
}
