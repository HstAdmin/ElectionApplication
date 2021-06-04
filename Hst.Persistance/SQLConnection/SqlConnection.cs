using Hst.Persistance.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Hst.Persistance.SQLConnection
{
    public class SqlConnectionFactory : IConnectionfactory
    {
        private SqlConnection _connection = null;
        readonly IConfiguration _configuration=null;

        public SqlConnectionFactory(IConfiguration  configuration)
        {
            _configuration = configuration;
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
            }
        }

        public IDbConnection GetConnection()
        {
            try
            {
                string connectionString = _configuration.GetSection("appSettings").GetSection("ConnectionString").Value;
                _connection = new SqlConnection(connectionString);
                _connection.Open();
                return _connection;
            }
            catch(SqlException ex)
            {
                _connection=null;
                throw ex;
            }
        }
    }
}
