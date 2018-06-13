using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Dal.Interfaces;

namespace StoreManagement.Controllers
{
    public class ReportsController : Controller
    {
        private IUserRepository _userContext;
        private IProductRepository _productContext;
        private ICustomerRepository _customerContext;

        public ReportsController(IProductRepository prodRepo, ICustomerRepository cusRepo, IUserRepository userRepo)
        {
            _productContext = prodRepo;
            _customerContext = cusRepo;
            _userContext = userRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductsBySupplier()
        {
            var listBySupplier = _productContext.GetAll().GroupBy(s => s.Supplier).OrderBy(s=> s.Key.Name);
            return View(listBySupplier);
        }

        public IActionResult CustomersByLastname()
        {
            var listByLastname = _customerContext.GetAll().OrderBy(c => c.Lastname);
            return View(listByLastname);
        }

        public IActionResult UsersCustomers()
        {
            var listUsers = _userContext.GetAll();
            var listCustomers = _customerContext.GetAll();
            //var result = listUsers.Join(listCustomers, u => u.Firstname, c => c.Firstname, (u, c) => u);
            var resutlQuery = from u in listUsers
                              join c in listCustomers on u.Firstname equals c.Firstname
                              where u.Firstname == c.Firstname && u.Lastname == c.Lastname
                              select u;
            return View(resutlQuery);
        }

        public IActionResult CustomersAddress()
        {
            var list = _customerContext.GetAll();
            return View(list);
        }

        [HttpPost]
        public PartialViewResult FilterCustomersAjax(string filter)
        {
            var list = _customerContext.GetAll();
            if (!string.IsNullOrEmpty(filter))
            {
                list = list = list.Where(cus => cus.Address.ToLower().Contains(filter.ToLower())).ToList();
            }
            return PartialView("AddressCustomerPartial", list);
        }
    }
}