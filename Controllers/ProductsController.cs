using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClothesStore.Data;
using ClothesStore.Models;
using ClothesStore.Services;

namespace ClothesStore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ClothesStoreContext _context;
        private readonly IUploader _uploader;

        public ProductsController(
            ClothesStoreContext context,
            IUploader uploader)
        {
            _context = context;
            _uploader = uploader;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await _context.Product.Include(p => p.Category).ToListAsync();
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            var categories = _context.Category.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Name,Description,Price")] Product product,
            string categoryId,
            IFormFile imageFile)
        {
            // check if the image file is null
            if (imageFile == null)
            {
                ModelState.AddModelError("ImageUrl", "Hình ảnh không được bỏ trống.");
                return View(product);
            }
            var category = _context.Category.Find(long.Parse(categoryId));

            if (category == null)
            {
                ModelState.AddModelError("CategoryId", "Danh mục không tồn tại.");
                return View(product);
            }

            product = await uploaderProductImage(product, imageFile);
            product.Category = category;

            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = new SelectList(_context.Category.ToList(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            long id,
            [Bind("Id,Name,Description,Price,ImageUrl")] Product product,
            IFormFile? imageFile)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (imageFile != null)
                    {
                        product = await uploaderProductImage(product, imageFile);
                    }
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(_context.Category.ToList(), "Id", "Name");
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(long id)
        {
            return _context.Product.Any(e => e.Id == id);
        }

        private async Task<Product> uploaderProductImage(Product product, IFormFile imageFile) 
        { 
            string randomFileName = Guid.NewGuid().ToString();
            product.ImageUrl = await _uploader.UploadAsync("products", randomFileName, imageFile);
            return product;
        }
    }
}
