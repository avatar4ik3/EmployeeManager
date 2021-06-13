
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManager.Core.Services
{
    /// <summary>
    /// The whole point of this class is to give app all info in database quick without dublicating objects in memory
    /// its supose only for getting info when programm starts and after that you can work with it
    /// however i wount be using it because it reads ALL info in database(and it takes a lot of memory if db is big)
    /// but it nice to have such thing available
    /// </summary>
    public class Mapper
    {
       /* #region Emplpyee
        public List<Employee> Employees { get; internal set; } = new List<Employee>();

        private List<EmployeeDB> EmployeeDBs { get; set; } = new List<EmployeeDB>();
        private Dictionary<String, EmployeeDB> IdToEmployeeDB { get; set; } = new Dictionary<string, EmployeeDB>();
        #endregion
        #region Department
        public List<Department> Departments { get; internal set; } = new List<Department>();
        private Dictionary<String, DepartmentDB> IdToDepartmentDB { get; set; } = new Dictionary<string, DepartmentDB>();
        private List<DepartmentDB> DepartmentDBs { get; set; } = new List<DepartmentDB>();
        #endregion
        #region History
        public List<History> Histories { get; internal set; } = new List<History>();
        private List<HistoryDB> HistoryDBs { get; set; } = new List<HistoryDB>();

        private List<KeyValuePair<String, HistoryDB>> EmployeeIdtoHistoryDB { get;  set; } = new List<KeyValuePair<string, HistoryDB>>();
        #endregion

        private void init()
        {
            List<Task<List<IData>>> list = new List<Task<List<IData>>>();
            list.Add(EmployeesDataAccess.GetAllAsync());
            list.Add(HistoryDataAccess.GetAllAsync());
            list.Add(DepartmentDataAccess.GetAllAsync());
            Task.WaitAll(list.ToArray());
            EmployeeDBs = list[0].Result.ConvertAll(el => (EmployeeDB) el);
            HistoryDBs = list[1].Result.ConvertAll(el => (HistoryDB) el);
            DepartmentDBs = list[2].Result.ConvertAll(el => (DepartmentDB)el);

        }
        private void FillIdToEmployeeDB()
        {
            
            foreach(var empBD in EmployeeDBs)
            {
                IdToEmployeeDB.Add(empBD.Id, empBD);
                Employees.Add(new Employee()
                {
                    FirstName = empBD.FirstName,
                    SureName = empBD.SureName,
                    MiddleName = empBD.MiddleName,
                    Id = empBD.Id,
                    Sex = SexParser.Parse(empBD.Sex),
                    Position = empBD.Position,
                    Description = empBD.Description,
                    Birth = DateTime.Parse(empBD.Birth),
                    Salary = int.Parse(empBD.Salary)
                }) ;
            }
        }

        private void FillIdToDepartmentDB()
        {
            foreach (var depDB in DepartmentDBs)
            {
                IdToDepartmentDB.Add(depDB.Id, depDB);
                Departments.Add(new Department()
                {
                    Name = depDB.Name,
                    Description = depDB.Description,
                    Id = depDB.Id
                });
            }
        }

        private void FillIdtoHistoryDB()
        {
            
            foreach (var hisBD in HistoryDBs)
            {
                EmployeeIdtoHistoryDB.Add(new KeyValuePair<string, HistoryDB>(hisBD.EmployeeId, hisBD));
                Histories.Add(new History() {
                    From = DateTime.Parse(hisBD.From),
                    Position = hisBD.Position,
                    Description = hisBD.Description,
                    Salary = int.Parse(hisBD.Salary),
                    Id = hisBD.Id
                }) ;
            }
        }

        private void MapEmployee()
        {
            foreach(var emp in Employees)
            {
                var depId = IdToEmployeeDB[emp.Id].DepartmentId;

                emp.Department = Departments.Find(x => x.Id == depId);

                var chiefId = IdToEmployeeDB[emp.Id].ChiefId;

                emp.Chief = Employees.Find(x => x.Id == chiefId);
            }
        }

        private void MapDepartment()
        {
            foreach(var dep in Departments)
            {
                var headId = IdToDepartmentDB[dep.Id].HeadId;
                dep.Head = Employees.Find(x => x.Id == headId);
            }
        }

        private void MapHistory()
        {
            foreach(var his in Histories)
            {
                var hisDB = EmployeeIdtoHistoryDB.Find(x => x.Value.Id == his.Id).Value;
                var empId = hisDB.EmployeeId;
                var chiefId = hisDB.ChiefId;
                var depId = hisDB.DepartmentId;
                his.Chief = Employees.Find(x => x.Id == chiefId);
                his.Department = Departments.Find(x => x.Id == depId);
                his.Employee = Employees.Find(x => x.Id == empId);
                his.Employee.History.Add(his);
            }
        }

        public Mapper()
        {
            init();
            FillIdToDepartmentDB();
            FillIdToEmployeeDB();
            FillIdtoHistoryDB();
            MapDepartment();
            MapEmployee();
            MapHistory();
        }*/
    }
}
