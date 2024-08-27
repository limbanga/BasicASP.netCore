using Microsoft.AspNetCore.Mvc;
using ClothesStore.Models;
using ClothesStore.Data;

namespace ClothesStore.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ClothesStoreContext _context;
        public CategoriesController(
            ClothesStoreContext context
            )
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var list = _context.Category.ToList();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(
             [Bind("Id,Name")] Category model
            )
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            // save to database
            _context.Category.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
