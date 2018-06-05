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

namespace StoreManagement.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductRepository _context;

        public ProductsController()
        {
            _context = new ProductRepository();
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
            var list = _context.GetSuppliers();
            SelectList sList = new SelectList(list, "Id", "Name");
            ViewBag.Suppliers = sList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Name, Description, Image, ProductCode, IdSupplier")] Product prod)
        {
            if (ModelState.IsValid)
            {
                //prod.Supplier = new Supplier() { Id = prod.IdSupplier };
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
            return View(prod);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductCode,Name,Description,Image,IdSupplier")] Product prod)
        {
            if (id != prod.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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