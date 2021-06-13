using EmployeeManager.Core.Contracts.Services;
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
    public static class EmployeesDataAccess
    {
        #region Employees Helpers

        private static DataAccess<EmployeeDB> _dataAccess = new DataAccess<EmployeeDB>();

        private static async Task ExecuteAsync( String StoredProcedureName, params DataAccessArgument[] arguments)
        {
            await _dataAccess.ExecuteAsync("Employees", StoredProcedureName, arguments);
        }

        private static EmployeeDB Read(SqlDataReader reader)
        {
            EmployeeDB empDb = new EmployeeDB();

            empDb.Birth = reader["Birth"].ToString();
            empDb.Id = reader["Id"].ToString();
            empDb.FirstName = reader["FirstName"].ToString();
            empDb.SureName = reader["SureName"].ToString();
            empDb.MiddleName = reader["MiddleName"].ToString();
            empDb.Position = reader["Position"].ToString();
            empDb.Description = reader["Description"].ToString();
            empDb.DepartmentId = reader["DepartmentId"].ToString();
            empDb.ChiefId = reader["ChiefId"].ToString();
            empDb.Salary = reader["Salary"].ToString();
            empDb.Sex = reader["Sex"].ToString();
            return empDb;
        }

        private static async Task<IEnumerable<EmployeeDB>> GetAsync( String StoredProcedureName, params DataAccessArgument[] arguments)
        {
            return await _dataAccess.GetAsync(Read, "Employees", StoredProcedureName, arguments);
        }

        #endregion


        #region Emoloyees Static Data Access Methods

        #region Employees Get

        public static async Task<int> Count()
        {
            return await _dataAccess.GetCountOfAsync("Employees");
        }

        public static async  Task<IEnumerable<EmployeeDB>> GetAllAsync()
        {
            return await GetAsync("GetAll");
        }

        public static async Task<IEnumerable<EmployeeDB>> GetByPositionAsync( String Position)
        {
            return await GetAsync("GetByPosition",new DataAccessArgument("Position",Position,SqlDbType.NVarChar));
        }

        public static async Task<IEnumerable<EmployeeDB>> GetByDescriptionAsync( String Description)
        {
            return await GetAsync("GetByDescription", new DataAccessArgument("Description", Description, SqlDbType.NVarChar));
        }

        public static async Task<EmployeeDB> GetByIdAsync(String EmployeeId)
        {
            var list = await GetAsync("GetById", new DataAccessArgument("Id", EmployeeId, SqlDbType.NVarChar));
            if(list.Count() == 0)
            {
                return null;
            }
            return list.First();
        }


        public static async Task<IEnumerable<EmployeeDB>> GetByDepartmentIdAsync( String DepartmentId)
        {
            return await GetAsync("GetByDepartmentId", new DataAccessArgument("DepartmentId", DepartmentId, SqlDbType.NVarChar));
        }

        public static async Task<IEnumerable<EmployeeDB>> GetByChiefIdAsync( String ChiefId)
        {
            return await GetAsync("GetByChiefId", new DataAccessArgument("ChiefId", ChiefId, SqlDbType.NVarChar));
        }


        

        #endregion

        public static async Task InsertAsync(EmployeeDB empDB)
        {
            await ExecuteAsync("Insert", 
                new DataAccessArgument("Id", empDB.Id, SqlDbType.NVarChar),
                new DataAccessArgument("FirstName", empDB.FirstName, SqlDbType.NVarChar),
                new DataAccessArgument("SureName", empDB.SureName, SqlDbType.NVarChar),
                new DataAccessArgument("MiddleName", empDB.MiddleName, SqlDbType.NVarChar),
                new DataAccessArgument("Sex", empDB.Sex, SqlDbType.NVarChar),
                new DataAccessArgument("Position", empDB.Position, SqlDbType.NVarChar),
                new DataAccessArgument("Description", empDB.Description, SqlDbType.NVarChar),
                new DataAccessArgument("DepartmentId", empDB.DepartmentId, SqlDbType.NVarChar),
                new DataAccessArgument("ChiefId", empDB.ChiefId, SqlDbType.NVarChar),
                new DataAccessArgument("Birth", empDB.Birth, SqlDbType.Date),
                new DataAccessArgument("Salary", empDB.Salary, SqlDbType.NVarChar));
            
        }

        public static async Task Delete(String id)
        {
            await ExecuteAsync("DeleteById", new DataAccessArgument("Id", id, SqlDbType.NVarChar));
        }

        #region Employees Change
        public static async Task ChangePositionDescriptionDepartmentIdChiefIdSalaryAsync( String EmployeeId, String Position,String Description,String DepartmentId,String ChiefId,String Salary)
        {
            await ExecuteAsync("ChangePositionDescriptionDepartmentIdChiefIdSalary", new DataAccessArgument("Position", Position, SqlDbType.NVarChar),
                new DataAccessArgument("Id", EmployeeId, SqlDbType.NVarChar),
                new DataAccessArgument("Description", Description, SqlDbType.NVarChar),
                new DataAccessArgument("DepartmentId", DepartmentId, SqlDbType.NVarChar),
                new DataAccessArgument("ChiefId", ChiefId, SqlDbType.NVarChar),
                new DataAccessArgument("Salary", Salary, SqlDbType.NVarChar)
                );
        }

        public static async Task ChangeAllAsync( String EmployeeId, String FirstName, String SureName, String MiddleName,String Sex,String Position,String Description, String DepartmentId, String ChiefId, String Birth, String Salary)
        {
            await ExecuteAsync("ChangeAll",
                new DataAccessArgument("Id", EmployeeId, SqlDbType.NVarChar),
                new DataAccessArgument("FirstName", FirstName, SqlDbType.NVarChar),
                new DataAccessArgument("SureName", SureName, SqlDbType.NVarChar),
                new DataAccessArgument("MiddleName", MiddleName, SqlDbType.NVarChar),
                new DataAccessArgument("Sex", Sex, SqlDbType.NVarChar),
                new DataAccessArgument("Position", Position, SqlDbType.NVarChar),
                new DataAccessArgument("Description", Description, SqlDbType.NVarChar),
                new DataAccessArgument("DepartmentId", DepartmentId, SqlDbType.NVarChar),
                new DataAccessArgument("ChiefId", ChiefId, SqlDbType.NVarChar),
                new DataAccessArgument("Birth", Birth, SqlDbType.Date),
                new DataAccessArgument("Salary", Salary, SqlDbType.NVarChar)
                );
        }

        public static async Task ChangePositionAsync( String EmployeeId, String Position)
        {
            await ExecuteAsync("ChangePosition", new DataAccessArgument("Position", Position, SqlDbType.NVarChar), new DataAccessArgument("Id", EmployeeId, SqlDbType.NVarChar));
        }

        public static async Task ChangeBirthAsync( String EmployeeId, String Birth)
        {
            await ExecuteAsync("ChangeBirth", new DataAccessArgument("Id", EmployeeId, SqlDbType.NVarChar), new DataAccessArgument("Birth", Birth, SqlDbType.Date));
        }

        public static async Task ChangeChiefId(String EmployeeId, String ChiefId)
        {
            await ExecuteAsync ("ChangeChiefId", new DataAccessArgument("Id", EmployeeId, SqlDbType.NVarChar), new DataAccessArgument("ChiefId", ChiefId, SqlDbType.NVarChar));
        }

        public static async Task ChangeDepartmentId(String EmployeeId, String DepartmentId)
        {
            await ExecuteAsync("ChangeDepartmentId", new DataAccessArgument("Id", EmployeeId, SqlDbType.NVarChar), new DataAccessArgument("DepartmentId", DepartmentId, SqlDbType.NVarChar));
        }

        public static async Task ChangeDescriptionAsync(String EmployeeId, String Description)
        {
            await ExecuteAsync("ChangeDescription", new DataAccessArgument("Id", EmployeeId, SqlDbType.NVarChar), new DataAccessArgument("Description", Description, SqlDbType.NVarChar));
        }

        public static async Task ChangeSexAsync( String EmployeeId, String Sex)
        {
            await ExecuteAsync("ChangeSex", new DataAccessArgument("Id", EmployeeId, SqlDbType.NVarChar), new DataAccessArgument("Sex",Sex,SqlDbType.NVarChar));
        }
        public static async Task ChangeNameAsync( String EmployeeId, String FirstName,String SureName, String MiddleName)
        {
            await ExecuteAsync("ChangeSex", 
                new DataAccessArgument("Id", EmployeeId, SqlDbType.NVarChar), 
                new DataAccessArgument("FirstName", FirstName, SqlDbType.NVarChar),
                new DataAccessArgument("SureName", SureName, SqlDbType.NVarChar),
                new DataAccessArgument("MiddleName", MiddleName, SqlDbType.NVarChar));
        }

        public static async Task ChangeSalaryAsync( String EmployeeId,  String Salary)
        {
            await ExecuteAsync("ChangeSalary", new DataAccessArgument("Id", EmployeeId, SqlDbType.NVarChar), new DataAccessArgument("Salary", Salary, SqlDbType.NVarChar));
        }

        public static async Task ChangeSalaryAllAsync( String Salary)
        {
            await ExecuteAsync("ChangeSalaryAll", new DataAccessArgument("Salary", Salary, SqlDbType.NVarChar));

        }
        #endregion


        #endregion
    }
}
