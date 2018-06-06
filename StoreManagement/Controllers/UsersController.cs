using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Dal.Interfaces;
using StoreManagement.Dal;
using StoreManagement.Model;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text;
using System.Security;

namespace StoreManagement.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _context;

        public UsersController(IUserRepository userrep)
        {
            _context = userrep;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            return View(await _context.GetAllAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Firstname, Lastname, Login, Password, AvatarImage")] User  user)
        {
            if (ModelState.IsValid)
            {
                var ss = Request.Form.Files[0];
                string imageString = string.Empty;
                using (var reader = new StreamReader(ss.OpenReadStream()))
                {
                    imageString = reader.ReadToEnd();
                }
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(imageString);
                user.AvatarImage = Convert.FromBase64String(Convert.ToBase64String(plainTextBytes));
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Firstname,Lastname,Login,Password")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
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

        private bool UserExists(int id)
        {
            return _context.Get(e => e.Id == id) != null;
        }
    }
}