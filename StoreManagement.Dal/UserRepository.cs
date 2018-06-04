using Microsoft.EntityFrameworkCore;
using StoreManagement.Dal.Interfaces;
using StoreManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StoreManagement.Dal
{
    public class UserRepository : IUserRepository
    {
        private StoreManagementContext _context;

        public UserRepository()
        {
            _context = new StoreManagementContext();
        }

        public long Count()
        {
            return _context.User.LongCount();
        }

        public void Create(User add)
        {
            if (add != null)
            {
                try
                {
                    _context.User.Add(add);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        public void Delete(User delete)
        {
            if (delete != null)
            {
                try
                {
                    _context.User.Remove(delete);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        public User Get(Expression<Func<User, bool>> condition)
        {
            if (condition != null)
            {
                var singleUser = _context.User.Where(condition).FirstOrDefault();
                return singleUser;

            }
            return null;

        }

        public IList<User> GetAll()
        {
            return _context.User.ToList();
        }

        public async Task<IList<User>> GetAllAsync()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> condition)
        {
            if (condition != null)
            {
                var singleUser = _context.User.Where(condition).FirstOrDefaultAsync();
                return await singleUser;

            }
            return null;

        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(User update)
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
