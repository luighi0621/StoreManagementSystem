using StoreManagement.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using StoreManagement.Model;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace StoreManagement.Dal
{
    public class OperationRepository : DbContext, IOperationRepository
    {
        private StoreManagementContext _context;

        public OperationRepository(DbContextOptions<StoreManagementContext> opts)
        {
            _context = new StoreManagementContext(opts);
        }

        public long Count()
        {
            return _context.Operation.LongCount();
        }

        public void Create(Operation add)
        {
            throw new NotSupportedException();
        }

        public void Delete(Operation delete)
        {
            throw new NotSupportedException();
        }

        public DataTable ExecuteQuery(string query)
        {
            throw new NotSupportedException();
        }

        public Operation Get(Expression<Func<Operation, bool>> condition)
        {
            throw new NotSupportedException();
        }

        public IList<Operation> GetAll()
        {
            return _context.Operation.ToList();
        }

        public async Task<IList<Operation>> GetAllAsync()
        {
            return await _context.Operation.ToListAsync();
        }

        public Task<Operation> GetAsync(Expression<Func<Operation, bool>> condition)
        {
            throw new NotSupportedException();
        }

        public Task<int> SaveAsync()
        {
            throw new NotSupportedException();
        }

        public void Update(Operation update)
        {
            throw new NotSupportedException();
        }
    }
}
