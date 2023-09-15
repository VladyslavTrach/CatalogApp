using Microsoft.AspNetCore.Mvc;

namespace CatalogApp.Controllers
{
	public class CatalogController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
