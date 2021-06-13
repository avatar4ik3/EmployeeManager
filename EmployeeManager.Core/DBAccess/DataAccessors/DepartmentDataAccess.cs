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
    public static class DepartmentDataAccess
    {
        private static DataAccess<DepartmentDB> _dataAccess = new DataAccess<DepartmentDB>();
        #region Departments Helpers
        private async static Task ExecuteAsync(String StoredProcedureName, params DataAccessArgument[] arguments)
        {
            await _dataAccess.ExecuteAsync("Departments", StoredProcedureName, arguments);
        }
        private static DepartmentDB Read(SqlDataReader reader)
        {
            DepartmentDB depDB = new DepartmentDB();
            depDB.Id = reader["Id"].ToString();
            depDB.Name = reader["Name"].ToString();
            depDB.Description = reader["Description"].ToString();
            depDB.HeadId = reader["HeadId"].ToString();
            return depDB;
        }

        private async static Task<IEnumerable<DepartmentDB>> GetAsync( String StoredProcedureName, params DataAccessArgument[] arguments)
        {
            return await _dataAccess.GetAsync(Read, "Departments", StoredProcedureName, arguments);
            
        }

        #endregion

        #region Emoloyees Static Data Access Methods
        #region Departments Get

        public static async Task<int> Count()
        {
            return await _dataAccess.GetCountOfAsync("Departments");
        }
        public async static Task<DepartmentDB> GetByIdAsync( String DepartmentId)
        {
            var list = await GetAsync("GetById", new DataAccessArgument("Id", DepartmentId, SqlDbType.NVarChar));
            if(list.Count() == 0) {
                return null;
            }
            return list.First();
        }

        public async static Task<IEnumerable<DepartmentDB>> GetAllAsync()
        {
            return await GetAsync("GetAll");
        }

        public async static Task<IEnumerable<DepartmentDB>> GetByHeadIdAsync(String HeadId)
        {
            return await GetAsync("GetByHeadId",new DataAccessArgument("HeadId",HeadId,SqlDbType.NVarChar));
        }



        #endregion

        #region Departments Change

        public async static Task ChangeAll(string id, string name, string description, string headId)
        {
            await ExecuteAsync("ChangeAll",
                new DataAccessArgument("Id",id,SqlDbType.NVarChar),
                new DataAccessArgument("Name",name,SqlDbType.NVarChar),
                new DataAccessArgument("Description",description,SqlDbType.NVarChar),
                new DataAccessArgument("HeadId",headId,SqlDbType.NVarChar)
                );
        }
        

        
        public async static Task ChangeDescriptionAsync( String Description)
        {
            await ExecuteAsync("ChangeDescription", new DataAccessArgument("Description", Description, SqlDbType.NVarChar));
        }

        public async static Task ChangeNameAsync(String Name)
        {
            await ExecuteAsync("ChangeName", new DataAccessArgument("Name", Name, SqlDbType.NVarChar));
        }

        public async static Task ChangeHeadIdAsync( String HeadId)
        {
            await ExecuteAsync("ChangeHeadId", new DataAccessArgument("HeadId", HeadId, SqlDbType.NVarChar));
        }
        #endregion

        public async static Task InsertAsync( DepartmentDB depDB)
        {
            await ExecuteAsync("Insert", 
                new DataAccessArgument("Id", depDB.Id, SqlDbType.NVarChar),
                new DataAccessArgument("Name", depDB.Name, SqlDbType.NVarChar),
                new DataAccessArgument("Description", depDB.Description, SqlDbType.NVarChar),
                new DataAccessArgument("HeadId", depDB.HeadId, SqlDbType.NVarChar));
        }
        public static async Task Delete(String id)
        {
            await ExecuteAsync("DeleteById", new DataAccessArgument("Id", id, SqlDbType.NVarChar));
        }
        #endregion
    }
}
