﻿using Microsoft.AspNetCore.Authorization;
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
    public class ProductImagesController : Controller
    {
        private readonly DatabaseContext _context;

        public ProductImagesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/ProductImages
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.ProductImages.Include(p => p.Product);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Admin/ProductImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productImage = await _context.ProductImages
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productImage == null)
            {
                return NotFound();
            }

            return View(productImage);
        }

        // GET: Admin/ProductImages/Create
        public IActionResult Create()
        {
			ViewBag.ProductId = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: Admin/ProductImages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductImage productImage, IFormFile? Name)
        {
            if (ModelState.IsValid)
            {
				if (Name is not null)
				{
					productImage.Name = FileHelper.FileLoader(Name);
				}
				_context.Add(productImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
			}
			ViewBag.ProductId = new SelectList(_context.Products, "Id", "Name");
			return View(productImage);
        }

        // GET: Admin/ProductImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
			ViewBag.ProductId = new SelectList(_context.Products, "Id", "Name");
			return View(productImage);
        }

        // POST: Admin/ProductImages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductImage productImage, IFormFile? Name)
        {
            if (id != productImage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
					if (Name is not null)
						productImage.Name = FileHelper.FileLoader(Name);
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
				ViewBag.ProductId = new SelectList(_context.Products, "Id", "Name");
				return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", productImage.ProductId);
            return View(productImage);
        }

        // GET: Admin/ProductImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productImage = await _context.ProductImages
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productImage == null)
            {
                return NotFound();
            }

            return View(productImage);
        }

        // POST: Admin/ProductImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productImage = await _context.ProductImages.FindAsync(id);
            if (productImage != null)
            {
                _context.ProductImages.Remove(productImage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductImageExists(int id)
        {
            return _context.ProductImages.Any(e => e.Id == id);
        }
    }
}
