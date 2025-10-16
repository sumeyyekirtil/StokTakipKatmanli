using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StokTakipKatmanli.WebAPIUsing.Areas.Admin.Controllers
{
	//async method kabul edilirse metot ismi de "DetailsAsync" oluyor ve view e erişimi engelliyor ancak proje çalışmasında herhangi bir sorun olmuyor
	[Area("Admin")]
	[Authorize(Policy = "AdminPolicy")]
	public class MainController : Controller
	{
		public IActionResult Index() //add - empty view
		{
			return View();
		}
	}
}
