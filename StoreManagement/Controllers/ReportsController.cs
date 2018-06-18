using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Dal.Interfaces;
using System.Data;

namespace StoreManagement.Controllers
{
    public class ReportsController : Controller
    {
        private IProductRepository _productContext;
        private IOperationRepository _operationContext;

        public ReportsController(IProductRepository prodRepo, IOperationRepository opeRepo)
        {
            _productContext = prodRepo;
            _operationContext = opeRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductsBySupplier()
        {
            DataTable table = _productContext.ExecuteQuery("select s.Name, p.Name from product p, supplier s where p.IdSupplier = s.Id group by s.Name, p.Name order by s.Name, p.Name");
            //var listBySupplier = _productContext.GetAll().GroupBy(s => s.Supplier).OrderBy(s=> s.Key.Name);
            return View(table);
        }

        public IActionResult CustomersByLastname()
        {
            DataTable listByLastname = _productContext.ExecuteQuery("select Lastname, Firstname, Address, Email, Phone, CustomerCode from Customer order by Lastname");
            //var listByLastname = _customerContext.GetAll().OrderBy(c => c.Lastname);
            return View(listByLastname);
        }

        public IActionResult UsersCustomers()
        {
            //var listUsers = _userContext.GetAll();
            //var listCustomers = _customerContext.GetAll();
            ////var result = listUsers.Join(listCustomers, u => u.Firstname, c => c.Firstname, (u, c) => u);
            //var resutlQuery = from u in listUsers
            //                  join c in listCustomers on u.Firstname equals c.Firstname
            //                  where u.Firstname == c.Firstname && u.Lastname == c.Lastname
            //                  select u;
            string query = "select distinct u.Firstname, u.Lastname, u.Username, u.Email, u.PhoneNumber from [User] u" +
                " inner join [Customer] c on u.Firstname = c.Firstname " +
                " inner join [Customer] cus on u.Lastname = cus.Lastname";
            DataTable listByLastname = _productContext.ExecuteQuery(query);

            return View(listByLastname);
        }

        public IActionResult CustomersAddress()
        {
            //var list = _customerContext.GetAll();
            DataTable list = _productContext.ExecuteQuery("select Firstname, Lastname, Address, Email, Phone, CustomerCode from Customer");
            return View(list);
        }

        public IActionResult Operations()
        {
            var list = _operationContext.GetAll();
            //DataTable list = _productContext.ExecuteQuery("select Firstname, Lastname, Address, Email, Phone, CustomerCode from Customer");
            return View(list);
        }

        [HttpPost]
        public PartialViewResult FilterCustomersAjax(string filter)
        {
            string query = "select Firstname, Lastname, Address, Email, Phone, CustomerCode from Customer";
            //var list = _customerContext.GetAll();
            if (!string.IsNullOrEmpty(filter))
            {
                query = string.Format("{0} where LOWER(Address) like LOWER('%{1}%')", query, filter);
                //list = list.Where(cus => cus.Address.ToLower().Contains(filter.ToLower())).ToList();
            }
            DataTable filtering = _productContext.ExecuteQuery(query);
            return PartialView("AddressCustomerPartial", filtering);
        }
    }
}