using StoreManagement.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using StoreManagement.Model;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace StoreManagement.Dal
{
    public class ProductRepository : IProductRepository
    {
        private StoreManagementContext _context;

        public ProductRepository(DbContextOptions<StoreManagementContext> opts)
        {
            _context = new StoreManagementContext(opts);
        }

        public long Count()
        {
            return _context.Product.LongCount();
        }

        public void Create(Product add)
        {
            if (add != null)
            {
                try
                {
                    _context.Product.Add(add);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void Delete(Product delete)
        {
            if (delete != null)
            {
                try
                {
                    _context.Product.Remove(delete);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Product Get(Expression<Func<Product, bool>> condition)
        {
            if (condition != null)
            {
                var singleProduct = _context.Product.Where(condition).Include(p => p.Supplier).FirstOrDefault();
                return singleProduct;

            }
            return null;
        }

        public IList<Product> GetAll()
        {
            return _context.Product.Include(prod=> prod.Supplier).ToList();
        }

        public async Task<IList<Product>> GetAllAsync()
        {
            return await _context.Product.Include(prod => prod.Supplier).ToListAsync();
        }

        public async Task<Product> GetAsync(Expression<Func<Product, bool>> condition)
        {
            if (condition != null)
            {
                var singleProd = _context.Product.Include(p => p.Supplier).Where(condition).FirstOrDefaultAsync();
                return await singleProd;

            }
            return null;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(Product update)
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
