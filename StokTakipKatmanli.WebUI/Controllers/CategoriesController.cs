using Microsoft.AspNetCore.Mvc;
using StokTakipKatmanli.Service.Abstract;

namespace StokTakipKatmanli.WebUI.Controllers
{
	public class CategoriesController : Controller
	{
		private readonly ICategoryService _categoryService;

		public CategoriesController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		public IActionResult Index(int id)
		{
			var model = _categoryService.GetCategoryByProduct(id);  //id ye göre gelenleri al
			return View(model);
		}
	}
}
