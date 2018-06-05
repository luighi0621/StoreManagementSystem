using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Dal.Interfaces;
using StoreManagement.Dal;
using StoreManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace StoreManagement.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly ISupplierRepository _context;

        public SuppliersController(ISupplierRepository supp)
        {
            _context = supp;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.GetAllAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Name, Description, SupplierCode")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _context.Create(supplier);
                await _context.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.GetAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.GetAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Firstname,Lastname,Login,Password")] Supplier sup)
        {
            if (id != sup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sup);
                    await _context.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists(sup.Id))
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
            return View(sup);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.GetAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.GetAsync(m => m.Id == id);
            _context.Delete(movie);
            await _context.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierExists(int id)
        {
            return _context.Get(e => e.Id == id) != null;
        }
    }
}