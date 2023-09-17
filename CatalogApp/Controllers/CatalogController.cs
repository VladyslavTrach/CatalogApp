using CatalogApp.Data;
using CatalogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Xml;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

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
            var mainCatalogs = _context.Catalogs.Where(c => c.ParentalDirectoryName == "None").ToList();

            if (mainCatalogs == null)
            {
                return NotFound();
            }
           
            return View(mainCatalogs);
        }

        public IActionResult ViewFolder(string name)
        {
            var mainCatalog = _context.Catalogs.FirstOrDefault(c => c.Name == name);

            if (mainCatalog == null)
            {
                return NotFound();
            }

            var subCatalogs = _context.Catalogs.Where(c => c.ParentalDirectoryName == name).ToList();

            List<CatalogModel> mainCatalogWithSubCatalogs = new List<CatalogModel>();

            mainCatalogWithSubCatalogs.Add(mainCatalog);

            mainCatalogWithSubCatalogs.AddRange(subCatalogs);

            if(mainCatalogWithSubCatalogs.Count < 2)
                return View("EmptyFolder");

            return View(mainCatalogWithSubCatalogs);
        }

        public IActionResult EmptyFolder()
        {
            return View();
        }

        public IActionResult ExportToJson()
        {
            var catalogData = _context.Catalogs.ToList();

            var jsonData = JsonConvert.SerializeObject(catalogData, Formatting.Indented);

            System.IO.File.WriteAllText("catalog_data.txt", jsonData);

            return Content("Data exported to catalog_data.txt");
        }

        public IActionResult ImportFromJson()
        {
            var jsonData = System.IO.File.ReadAllText("catalog_data.txt");

            var catalogData = JsonConvert.DeserializeObject<List<CatalogModel>>(jsonData);

            foreach(var catalog in catalogData)
            {
                var newCatalog = new CatalogModel
                {
                    Name = catalog.Name,
                    ParentalDirectoryName = catalog.ParentalDirectoryName
                };

                _context.Catalogs.Add(newCatalog);

            }

            _context.SaveChanges();


            return Content("Data imported successfully.");
        }


    }
}

