using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiApp.DatabaseUitility
{
    public class Common
    {
        public readonly IDatabaseContext _databaseContext;

        public Common(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public async Task<MySqlCommand> CreateMasterCommandAsync(CommandType commandType, string commandText)
        {
            MySqlCommand cmd = null;
            try
            {
                await OpenMasterConnectionAsync().ConfigureAwait(false);
                cmd = _databaseContext.MasterConnection.CreateCommand();
                cmd.CommandType = commandType;
                cmd.CommandText = commandText;
            }
            catch(Exception ex)
            {

            }
            return cmd;
        }
        public async Task<MySqlCommand> CreateTenantCommandAsync(CommandType commandType, string commandText)
        {
            MySqlCommand cmd = null;
            try
            {
                await OpenTenantConnectionAsync().ConfigureAwait(false);
                cmd = _databaseContext.TenantConnection.CreateCommand();
                cmd.CommandType = commandType;
                cmd.CommandText = commandText;
            }
            catch (Exception ex)
            {

            }
            return cmd;
        }
        public void BindInputParameters(MySqlCommand cmd, IDictionary<string, object> parameters)
        {
            foreach (var parameter in parameters)
            {
                cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
            }
        }
        public void BindNewInputParameters(MySqlCommand cmd, List<MySqlParameter> parameters)
        {
            try
            {
                cmd.Parameters.AddRange(parameters.ToArray());
            }
            catch(Exception ex)
            {

            }
            
        }
        public void BindOuputParameters(MySqlCommand cmd, string outputParameter, MySqlDbType dbType)
        {
            cmd.Parameters.Add(new MySqlParameter(outputParameter, dbType));
            cmd.Parameters[outputParameter].Direction = ParameterDirection.Output;
        }
        public async Task CloseMasterConnectionAsync()
        {
            if (_databaseContext.MasterConnection.State == ConnectionState.Open)
            {
                await _databaseContext.MasterConnection.CloseAsync().ConfigureAwait(false);
            }
        }
        public async Task OpenMasterConnectionAsync()
        {
            if (_databaseContext.MasterConnection.State == ConnectionState.Closed)
            {
                await _databaseContext.MasterConnection.OpenAsync().ConfigureAwait(false);
            }
        }
        public async Task CloseTenantConnectionAsync()
        {
            if (_databaseContext.TenantConnection.State == ConnectionState.Open)
            {
                await _databaseContext.TenantConnection.CloseAsync().ConfigureAwait(false);
            }
        }
        public async Task OpenTenantConnectionAsync()
        {
            if (_databaseContext.TenantConnection.State == ConnectionState.Closed)
            {
                await _databaseContext.TenantConnection.OpenAsync().ConfigureAwait(false);
            }
        }
        public async Task<DataTable> ExecuteReader(MySqlCommand cmd)
        {
            DataTable dtData = new DataTable();
            try
            {
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        dtData.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                //await CloseConnectionAsync().ConfigureAwait(false);
            }
            
            return dtData;
        }
        public async Task<int> ExecuteNonQuery(MySqlCommand cmd)
        {
            int affectedrows = 0;
            try
            {
                affectedrows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                //await CloseConnectionAsync().ConfigureAwait(false);
            }
            return affectedrows;
        }
        public async Task<int> ExecuteScaler(MySqlCommand cmd)
        {
            int affectedrows = 0;
            try
            {
                affectedrows = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
            }
            finally
            {
                //await CloseConnectionAsync().ConfigureAwait(false);
            }
            return affectedrows;
        }
        public async Task<DataTable> ExecuteProcedure(MySqlCommand cmd)
        {
            DataTable dtData = new DataTable();
            try
            {
                using (MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter(cmd))
                {
                    sqlDataAdapter.Fill(dtData);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                //await CloseConnectionAsync().ConfigureAwait(false);
            }

            return dtData;
        }
    }
}
