using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StoreManagement.Dal.Interfaces
{
    public interface IRepository<T>
    {
        void Create(T add);
        void Delete(T delete);
        void Update(T update);
        T Get(Expression<Func<T, bool>> condition);
        Task<T> GetAsync(Expression<Func<T, bool>> condition);
        IList<T> GetAll();
        Task<IList<T>> GetAllAsync();
        Task<int> SaveAsync();
        long Count();
        DataTable ExecuteQuery(string query);
    }

}
