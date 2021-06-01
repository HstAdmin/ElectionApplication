using Dapper;
using Hst.Persistance.Infrastructure;
using Hst.Persistance.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Hst.Persistance.Reposiotry
{
    public class GenericRepository<TEntity>: IGenericRepository<TEntity> where TEntity : class
    {
        readonly IConnectionfactory _connection = null;

        public GenericRepository(IConnectionfactory connectionfactory)
        {
            _connection = connectionfactory;
        }

        public void Delete(int id,string query)
        {
            using (var con = _connection.GetConnection()) 
            {
                con.Query(query, new { id = id });   
            }
        }

        public IEnumerable<TEntity> GetAll(string query)
        {
            using (var con = _connection.GetConnection())
            {
                return con.Query<TEntity>(query);
            }
        }

        public IEnumerable<TEntity> GetAll(string query,object param)
        {
            using (var con = _connection.GetConnection()) {
               return con.Query<TEntity>(query, param);
            }
        }

        public TEntity GetByID(int id, string query)
        {
            using (var con = _connection.GetConnection())
            {
                return con.Query<TEntity>(query, new {id=id }).FirstOrDefault();
            }
        }

        public int Insert(TEntity obj, string query)
        {
            using (var con = _connection.GetConnection())
            {
                return con.Execute(query, obj);
            }
        }

        public void Update(TEntity obj, string query)
        {
            using (var con = _connection.GetConnection())
            {
                 con.Execute(query, obj);
            }
        }
    }
}
