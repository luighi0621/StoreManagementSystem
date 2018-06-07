using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Dal.Interfaces;
using StoreManagement.Dal;
using StoreManagement.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace StoreManagement.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _context;
        private readonly ISupplierRepository _supplierContext;

        public ProductsController(IProductRepository prod, ISupplierRepository supp)
        {
            _context = prod;
            _supplierContext = supp;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var list = await _context.GetAllAsync();
            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(prod => prod.Supplier.Name.Contains(searchString)).ToList();
            }
            return View(list);
        }

        public IActionResult Create()
        {
            var list = _supplierContext.GetAll();
            SelectList sList = new SelectList(list, "Id", "Name");
            ViewBag.Suppliers = sList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Name, Description, ProductCode, IdSupplier")] Product prod, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                prod.Image = Common.ImageHelper.fileTobytes(image);
                _context.Create(prod);
                await _context.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prod);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prod = await _context.GetAsync(u => u.Id == id);
            if (prod == null)
            {
                return NotFound();
            }
            return View(prod);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prod = await _context.GetAsync(m => m.Id == id);
            if (prod == null)
            {
                return NotFound();
            }
            var list = _supplierContext.GetAll();
            SelectList sList = new SelectList(list, "Id", "Name");
            ViewBag.Suppliers = sList;
            return View(prod);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductCode,Name,Description,IdSupplier")] Product prod, IFormFile image)
        {
            if (id != prod.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    prod.Image = Common.ImageHelper.fileTobytes(image);
                    _context.Update(prod);
                    await _context.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(prod.Id))
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
            return View(prod);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prod = await _context.GetAsync(m => m.Id == id);
            if (prod == null)
            {
                return NotFound();
            }

            return View(prod);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prod = await _context.GetAsync(m => m.Id == id);
            _context.Delete(prod);
            await _context.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Get(e => e.Id == id) != null;
        }
    }
}