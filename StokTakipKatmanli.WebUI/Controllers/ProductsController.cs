using Microsoft.AspNetCore.Mvc;
using StokTakipKatmanli.Service.Abstract;

namespace StokTakipKatmanli.WebUI.Controllers
{
	//ürün için controller
	public class ProductsController : Controller
	{
		private readonly IProductService _productService;

		public ProductsController(IProductService productService)
		{
			_productService = productService;
		}

		public IActionResult Index(string q = "") // search name = q -> boş gelebilir //empty view
		{
			//şartlı yazma (where) yerine service şartı lambda ile yazılacak
			var model = _productService.GetProducts(p => p.IsActive && p.Name.Contains(q)); //q ile search de sorguya ekledik
			return View(model);
		}

		public IActionResult Detail(int? id) //empty view
		{
			if (id is null)
			{
				return BadRequest("Geçersiz İstek!");
			}
			var model = _productService.GetProductByCategoryAndProductImages(id.Value);
			if (model == null)
			{
				return NotFound("Ürün Bulunamadı!");
			}
			return View(model);
		}
	}
}
