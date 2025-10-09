using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StokTakipKatmanli.WebUI.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")]
	public class MainController : Controller
	{
		public IActionResult Index() //add - empty view
		{
			return View();
		}
	}
}
