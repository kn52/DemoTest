using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiApp.DatabaseUitility
{
    public class DatabaseContext : IDatabaseContext
    {
        private readonly MySqlConnection _masterconnection;
        private  MySqlConnection _tenantconnection;
        public MySqlConnection MasterConnection => _masterconnection;
        public MySqlConnection TenantConnection => _tenantconnection;
        public DatabaseContext(string connectionString)
        {
            _masterconnection = new MySqlConnection(connectionString);
        }
        public void Dispose()
        {
            _masterconnection.Dispose();
        }

        public void GetTenantConnection(string connectionString) 
        {
            _tenantconnection = new MySqlConnection(connectionString);
        } 
    }
}
