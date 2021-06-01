using System;
using System.Collections.Generic;
using System.Text;

namespace Hst.Persistance.IRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity GetByID(int id, string query);
        IEnumerable<TEntity> GetAll(string query);
        IEnumerable<TEntity> GetAll(string query, object param);
        int Insert(TEntity obj, string query);
        void Update(TEntity obj, string query);
        void Delete(int id,string query);
    }


}
