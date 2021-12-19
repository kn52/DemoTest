using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using WebApiApp.DatabaseUitility;

namespace WebApiApp.Services
{
    public class CommonService : Common
    {
        public CommonService(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<string> getConnectionString()
        {
            string cns = string.Empty;
            string program = "erreportingdemo";
            using var cmd = await CreateMasterCommandAsync(CommandType.StoredProcedure, "SP_storepay_validateProgramCode").ConfigureAwait(false);
            BindInputParameters(cmd, new Dictionary<string, object> { { "Program_code", program } });
            var data = await ExecuteProcedure(cmd).ConfigureAwait(false);
            cns = (data.Rows.Count > 0 && data.Rows[0]["Return"].ToString() != "0") ? data.Rows[0]["ConnectionString"].ToString() : null;

            return cns;
        }

        public async Task getClientDet()
        {
            using var cmd = await CreateTenantCommandAsync(CommandType.StoredProcedure, "USP_Fetch_Session_BillDetails").ConfigureAwait(false);
            BindInputParameters(cmd, new Dictionary<string, object> {
                { "p_TokenId", "10000.ud3QmS0YlDXr3PWfvR84IA==.puPui6gFMIRSjOVZfAnNfmjvG8l/IDXHhDJA3ehaFv4=" },
                { "p_StorepayRefNum", "1c5xe4pu1jag6fqh" }
            });
            var data = await ExecuteProcedure(cmd).ConfigureAwait(false);
            Console.WriteLine();
            foreach (DataRow row in data.Rows)
            {
                var details = new
                {
                    furl = (row["FUrl"] ?? "").ToString(),
                    surl = (row["SUrl"] ?? "").ToString(),
                    IsDiscountApplied = (row["IsDiscountApplied"] ?? "").ToString() == "1" ? true : false,
                    UDF1 = (row["Udf1"] ?? "").ToString(),
                    UDF2 = (row["Udf2"] ?? "").ToString(),
                    UDF3 = (row["Udf3"] ?? "").ToString(),
                    UDF4 = (row["Udf4"] ?? "").ToString(),
                    UDF5 = (row["Udf5"] ?? "").ToString(),
                    Language = (row["Language"] ?? "en").ToString(),
                    StorepayRefNum = (row["StorepayRefNumber"] ?? "").ToString(),
                    ShoppingBagNumber = (row["ShoppingBagNumber"] ?? "").ToString()
                };
            }
        }

        public async Task getNonQuery()
        {
            var cns = await getConnectionString().ConfigureAwait(false);
            _databaseContext.GetTenantConnection(cns);
            using var cmd = await CreateTenantCommandAsync(CommandType.StoredProcedure, "sp_updateTokenStatus").ConfigureAwait(false);
            BindInputParameters(cmd, new Dictionary<string, object> { 
                { "p_TokenId", "10000.tiWAejWA3WrGdMPEfc5Bew==.j+rEm421CDni+5ENeQiBUA4EzIPBtJhCQkU5AUCZkhE=" }, 
                { "p_Status", "Success" } 
            });
            var data = await ExecuteNonQuery(cmd).ConfigureAwait(false);
        }
    }
}
