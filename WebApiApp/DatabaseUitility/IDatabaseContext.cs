using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiApp.DatabaseUitility
{
    public interface IDatabaseContext : IDisposable
    {
        MySqlConnection MasterConnection { get; }
        MySqlConnection TenantConnection { get; }
        void GetTenantConnection(string connectionString);
    }
}
