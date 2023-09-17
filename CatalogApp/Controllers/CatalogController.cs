using CatalogApp.Data;
using CatalogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CatalogApp.Controllers
{
    public class CatalogController : Controller
    {
        private readonly DataContext _context;

        public CatalogController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Retrieve the main catalogs
            var mainCatalogs = _context.Catalogs.Where(c => c.ParentalDirectoryName == "None").ToList();

            if (mainCatalogs == null)
            {
                // Handle the case where the main catalog is not found.
                return NotFound();
            }
           
            return View(mainCatalogs);
        }

        public IActionResult ViewFolder(string name)
        {
            // Retrieve the main catalog
            var mainCatalog = _context.Catalogs.FirstOrDefault(c => c.Name == name);

            if (mainCatalog == null)
            {
                // Handle the case where the main catalog is not found.
                return NotFound();
            }

            // Retrieve sub-catalogs for the main catalog
            var subCatalogs = _context.Catalogs.Where(c => c.ParentalDirectoryName == name).ToList();

            // Create a list to hold the main catalog and its sub-catalogs
            List<CatalogModel> mainCatalogWithSubCatalogs = new List<CatalogModel>();

            // Add the main catalog to the list
            mainCatalogWithSubCatalogs.Add(mainCatalog);

            // Add the sub-catalogs to the list
            mainCatalogWithSubCatalogs.AddRange(subCatalogs);

            if(mainCatalogWithSubCatalogs.Count < 2)
                return View("EmptyFolder");

            return View(mainCatalogWithSubCatalogs);
        }

        public IActionResult EmptyFolder()
        {
            return View();
        }

    }
}



//// Retrieve the main catalog
//var mainCatalog = _context.Catalogs.FirstOrDefault(c => c.ParentalDirectoryName == "None");

//if (mainCatalog == null)
//{
//    // Handle the case where the main catalog is not found.
//    return NotFound();
//}

//// Retrieve sub-catalogs for the main catalog   
//var subCatalogs = _context.Catalogs.Where(c => c.ParentalDirectoryName == mainCatalog.Name).ToList();

//// Create a list to hold the main catalog and its sub-catalogs
//List<CatalogModel> mainCatalogWithSubCatalogs = new List<CatalogModel>();

//// Add the main catalog to the list
//mainCatalogWithSubCatalogs.Add(mainCatalog);

//// Add the sub-catalogs to the list
//mainCatalogWithSubCatalogs.AddRange(subCatalogs);

//return View(mainCatalogWithSubCatalogs);