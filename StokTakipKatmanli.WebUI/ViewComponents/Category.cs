using Microsoft.AspNetCore.Mvc;
using StokTakipKatmanli.Data;

namespace StokTakipKatmanli.WebUI.ViewComponents
{
	//bu class ana sayfada category list şeklinde açılması için eklendi - viewcomponent yöntemi ile
	public class Category : ViewComponent //miras alma uygulandı
	{
		private readonly DatabaseContext _context;

		public Category(DatabaseContext context)
		{
			_context = context;
		}

		public IViewComponentResult Invoke()
		{
			return View(_context.Categories.Where(c => c.IsActive)); //active olan kategorileri göster
			//bu class genelde gösterileceği için shared - components - add view - default added
		}
	}
}
