using StoreManagement.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using StoreManagement.Model;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Data;

namespace StoreManagement.Dal
{
    public class CustomerRepository : ICustomerRepository
    {
        private StoreManagementContext _context;

        public CustomerRepository(DbContextOptions<StoreManagementContext> opts)
        {
            _context = new StoreManagementContext(opts);
        }

        public long Count()
        {
            return _context.Customer.LongCount();
        }

        public void Create(Customer add)
        {
            if (add != null)
            {
                try
                {
                    _context.Customer.Add(add);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void Delete(Customer delete)
        {
            if (delete != null)
            {
                try
                {
                    _context.Customer.Remove(delete);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable ExecuteQuery(string query)
        {
            return null;
        }

        public Customer Get(Expression<Func<Customer, bool>> condition)
        {
            if (condition != null)
            {
                var singleUser = _context.Customer.Where(condition).FirstOrDefault();
                return singleUser;

            }
            return null;
        }

        public IList<Customer> GetAll()
        {
            return _context.Customer.ToList();
        }

        public async Task<IList<Customer>> GetAllAsync()
        {
            return await _context.Customer.ToListAsync();
        }

        public async Task<Customer> GetAsync(Expression<Func<Customer, bool>> condition)
        {
            if (condition != null)
            {
                var singleUser = _context.Customer.Where(condition).FirstOrDefaultAsync();
                return await singleUser;

            }
            return null;
        }

        public DbQuery<TQuery> Query<TQuery>() where TQuery : class
        {
            return _context.Query<TQuery>();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(Customer update)
        {
            try
            {
                _context.Entry(update).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
