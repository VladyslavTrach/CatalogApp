using CatalogApp.Data;
using CatalogApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CatalogApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly DataContext _context;

		public HomeController(ILogger<HomeController> logger, DataContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
		{
			var mainCatalog = _context.Catalogs.Where(c => c.ParentalDirectoryName == "None").FirstOrDefault();
			return View(mainCatalog);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}