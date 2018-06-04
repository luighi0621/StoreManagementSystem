using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Dal.Interfaces;
using StoreManagement.Dal;
using StoreManagement.Model;

namespace StoreManagement.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserRepository _context;

        public UsersController()
        {
            _context = new UserRepository();
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
        public async Task<IActionResult> Create([Bind("Id, Firstname, Lastname, Login, Password")] User  user)
        {
            if (ModelState.IsValid)
            {
                _context.Create(user);
                await _context.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
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
    }
}