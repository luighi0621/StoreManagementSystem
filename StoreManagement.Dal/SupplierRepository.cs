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
    public class SupplierRepository : ISupplierRepository
    {
        private StoreManagementContext _context;

        public SupplierRepository(DbContextOptions<StoreManagementContext> opts)
        {
            _context = new StoreManagementContext(opts);
        }

        public long Count()
        {
            return _context.Supplier.LongCount();
        }

        public void Create(Supplier add)
        {
            if (add != null)
            {
                try
                {
                    _context.Supplier.Add(add);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void Delete(Supplier delete)
        {
            if (delete != null)
            {
                try
                {
                    _context.Product.RemoveRange(delete.Products);
                    //_context.SaveChanges();
                    _context.Supplier.Remove(delete);
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

        public Supplier Get(Expression<Func<Supplier, bool>> condition)
        {
            if (condition != null)
            {
                var singlesupplier = _context.Supplier.Where(condition).Include(s => s.Products).FirstOrDefault();
                return singlesupplier;

            }
            return null;
        }

        public IList<Supplier> GetAll()
        {
            return _context.Supplier.ToList();
        }

        public async Task<IList<Supplier>> GetAllAsync()
        {
            return await _context.Supplier.ToListAsync();
        }

        public async Task<Supplier> GetAsync(Expression<Func<Supplier, bool>> condition)
        {
            if (condition != null)
            {
                var singlesupplier = _context.Supplier.Where(condition).Include(s=> s.Products).FirstOrDefaultAsync();
                return await singlesupplier;

            }
            return null;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(Supplier update)
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
