using EmployeeManager.Core.DBAccess.TransferObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Core.DBAccess.DataAccessors
{
    public static class HistoryDataAccess
    {
        private static DataAccess<HistoryDB> _dataAccess = new DataAccess<HistoryDB>();
        #region PositionHistory Helpers
        private static async Task ExecuteAsync( String StoredProcedureName, params DataAccessArgument[] arguments)
        {
            await _dataAccess.ExecuteAsync("PositionHistory", StoredProcedureName, arguments);
        }

        private static HistoryDB Read(SqlDataReader reader)
        {
            var positionHistoryDB = new HistoryDB();
            positionHistoryDB.From = reader["From"].ToString();
            positionHistoryDB.EmployeeId = reader["EmployeeId"].ToString();
            positionHistoryDB.Position = reader["Position"].ToString();
            positionHistoryDB.Description = reader["Description"].ToString();
            positionHistoryDB.DepartmentId = reader["DepartmentId"].ToString();
            positionHistoryDB.ChiefId = reader["ChiefId"].ToString();
            positionHistoryDB.Salary = reader["Salary"].ToString();
            positionHistoryDB.Id = reader["Id"].ToString();
            return positionHistoryDB;
        }

        private static async Task<IEnumerable<HistoryDB>> GetAsync(String StoredProcedureName, params DataAccessArgument[] arguments)
        {
            return await _dataAccess.GetAsync(Read, "PositionHistory", StoredProcedureName, arguments);
            
        }
        #endregion
        #region PositionHistory Static Data Access Methods
        #region Get
        public static async Task<int> Count()
        {
            return await _dataAccess.GetCountOfAsync("History");
        }
        public static async Task<IEnumerable<HistoryDB>> GetByEmployeeIdAsync(String EmployeeId)
        {

            return await GetAsync("GetByEmployeeId", new DataAccessArgument("EmployeeId", EmployeeId, SqlDbType.NVarChar));
        }

        public static async Task<IEnumerable<HistoryDB>> GetAllAsync()
        {
            return await GetAsync("GetAll");
        }
        #endregion

        #region Change
        /*public static void ChangeTo(in String HistoryId,in String To)
        {
            Execute("ChangeTo", 
                new DataAccessArgument("HistoryId", HistoryId, SqlDbType.NVarChar),
                new DataAccessArgument("To", To, SqlDbType.Date));
        }*/
        #endregion

        public static async Task InsertAsync( HistoryDB positionHistoryDB)
        {
            await ExecuteAsync("Insert",
                new DataAccessArgument("From", positionHistoryDB.From,SqlDbType.Date),
                new DataAccessArgument("Salary", positionHistoryDB.Salary, SqlDbType.NVarChar),
                new DataAccessArgument("EmployeeId", positionHistoryDB.EmployeeId, SqlDbType.NVarChar),
                new DataAccessArgument("Position", positionHistoryDB.Position, SqlDbType.NVarChar),
                new DataAccessArgument("Description", positionHistoryDB.Description, SqlDbType.NVarChar),
                new DataAccessArgument("DepartmentId", positionHistoryDB.DepartmentId, SqlDbType.NVarChar),
                new DataAccessArgument("ChiefId", positionHistoryDB.ChiefId, SqlDbType.NVarChar),
                new DataAccessArgument("Id", positionHistoryDB.Id, SqlDbType.NVarChar));
        }
        #endregion

    }
}
