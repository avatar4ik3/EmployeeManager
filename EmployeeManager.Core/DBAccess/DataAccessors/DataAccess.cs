using EmployeeManager.Core.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Core.DBAccess.DataAccessors
{

    
    public class DataAccess<T>
        where T : ITransferData
    {
        #region Helpers
        

        private void FillCommandWithArguments(SqlCommand cmd, params DataAccessArgument[] arguments)
        {
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (var arg in arguments)
            {
                cmd.Parameters.Add($"@{arg.Argument}", arg.Type).Value = arg.Value;
            }
        }
        public static String ConnectionString { get; set; } = @"Server=DESKTOP-AR4IRIJ\SQLEXPRESS;Database=EMDB;Trusted_Connection=True;";

        public delegate T ReadFromSqlDataReader(SqlDataReader reader);
        #endregion

        internal  async Task ExecuteAsync( String StoredProcedureNamespace,  String StoredProcedureName, params DataAccessArgument[] arguments)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand($"[dbo].[sp{StoredProcedureNamespace}_{StoredProcedureName}]", connection))
                {
                    
                    FillCommandWithArguments(cmd, arguments);
                    connection.Open();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        
        public  async Task<IEnumerable<T>> GetAsync(ReadFromSqlDataReader Read,  String StoredProcedureNamespace,String StoredProcedureName, params DataAccessArgument[] arguments)
        {
            var list = new List<T>();
            using (var connection = new System.Data.SqlClient.SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand($"[dbo].[sp{StoredProcedureNamespace}_{StoredProcedureName}]", connection))
                {
                    connection.Open();
                    FillCommandWithArguments(cmd, arguments);
                    
                    var reader =  await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        list.Add(Read(reader));
                    }
                }
            }
            return list;
        }

        public  async Task<int> GetCountOfAsync(String functionNamespace) {
            using (var connection = new System.Data.SqlClient.SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand($"[dbo].[fun{functionNamespace}_Count()]", connection))
                {
                    connection.Open();
                    return (int) await cmd.ExecuteScalarAsync();
                }
            }

        }

    }
}
