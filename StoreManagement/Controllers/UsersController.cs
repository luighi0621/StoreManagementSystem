using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Dal.Interfaces;
using StoreManagement.Dal;

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
    }
}