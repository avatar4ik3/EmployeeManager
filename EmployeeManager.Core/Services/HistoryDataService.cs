using EmployeeManager.Core.Contracts.Services;
using EmployeeManager.Core.DBAccess.DataAccessors;
using EmployeeManager.Core.DBAccess.TransferObjects;
using EmployeeManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace EmployeeManager.Core.Services
{
    public class HistoryDataService : IDataService<History, HistoryDB>
    {
        public static History ConverFromEmployee(Employee emp,DateTime? date)
        {
            if (date.HasValue)
            {
                return new History()
                {
                    //todo написать выбор даты назначения 
                    From = date.Value,
                    EmployeeId = emp.Id,
                    ChiefId = emp.ChiefId,
                    DepartmentId = emp.DepartmentId,
                    Position = emp.Position,
                    Description = emp.Description,
                    Salary = emp.Salary,
                };
            }
            throw new ArgumentNullException("Date is null");
        }

        public History ConvertFromTransferObject(HistoryDB hisDB)
        {
            return new History()
            {
                From = DateTime.Parse(hisDB.From),
                Position = hisDB.Position,
                Description = hisDB.Description,
                Salary = int.Parse(hisDB.Salary),
                Id = hisDB.Id,
                ChiefId = hisDB.ChiefId,
                DepartmentId = hisDB.DepartmentId,
                EmployeeId = hisDB.EmployeeId

            };
        }

        public HistoryDB ConvertToTransferObject(History data)
        {
            return new HistoryDB()
            {
                Id = data.Id,
                EmployeeId = data.EmployeeId,
                From = data.From.ToShortDateString(),
                Position = data.Position,
                Description = data.Description,
                DepartmentId = data.DepartmentId,
                ChiefId = data.ChiefId,
                Salary = data.Salary.ToString()
            };
        }

        public Task Delete(string id)
        {
            //will not be implemented
            throw new NotImplementedException();
        }




        internal async Task<History> GetHistoryByDepthAsync(HistoryDB hisDB, int currentLevel = 0, int depthLevel = 1)
        {
            var his = ConvertFromTransferObject(hisDB);
            if (his != null && currentLevel < depthLevel)
            {
                //todo разобраться до конца как собрать все таски в коллекцию и запустить их вместе. пока что пусть будет синхронно 
                /*List<Task<IData>> datas = new List<Task<IData>>();
                Task<IData> getChief = Task.Run(
                    new Func<IData>(
                         () =>
                         {
                             Task<Employee> t = new EmployeeDataService().GetEmployeeByDepthAsync(his.ChiefId, currentLevel + 1, depthLevel);
                             t.Start();
                             return t.Result;
                         }
                        )
                    );
                Task<IData> getDepartment = Task.Run(
                    new Func<IData>(
                        () =>
                        {
                            Task<Department> t = new DepartmentDataService().GetDepartmentByDepthAsync(his.DepartmnetId, currentLevel + 1, depthLevel);
                            t.Start();
                            return t.Result;
                        }
                        )
                    );
                Task<IData> getEmployee = Task.Run(
                   new Func<IData>(
                       () =>
                       {
                           Task<Employee> t = new EmployeeDataService().GetEmployeeByDepthAsync(his.EmployeeId, currentLevel + 1, depthLevel);
                           t.Start();
                           return t.Result;
                       }
                       )
                   );
                datas.Add(getChief);
                datas.Add(getDepartment);
                datas.Add(getEmployee);
                Task.WaitAll(datas.ToArray());
                his.Chief = datas[0].Result as Employee;
                his.Department = datas[1].Result as Department;
                his.Employee = datas[2].Result as Employee;*/
                his.Chief = await new EmployeeDataService().GetDataByDepthAsync(his.ChiefId, currentLevel + 1, depthLevel); 
                his.Employee = await new EmployeeDataService().GetDataByDepthAsync(his.EmployeeId, currentLevel + 1, depthLevel);
                his.Department = await new DepartmentDataService().GetDataByDepthAsync(his.DepartmentId, currentLevel + 1, depthLevel);
            }
            return his;
        }

        public Task<History> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<History>> GetListDetailsDataAsync(Comparison<History> comparison)
        {
            /*var histories = await HistoryDataAccess.GetAllAsync();
            var result = new List<History>();
            Parallel.ForEach<HistoryDB>(histories, async (el) => {
                result.Add(await GetHistoryByDepthAsync(el)); 
            
            });*/

            var list = (await HistoryDataAccess.GetAllAsync()).Select(el => ConvertFromTransferObject(el)).ToList(); 
            if(comparison != null)
            {
                list.Sort(comparison);
            }
            return list;
        }

        public async Task InsertAsync(History data)
        {
            await HistoryDataAccess.InsertAsync(ConvertToTransferObject(data));
        }

        public Task UpdateInfoAsync(History data)
        {
            throw new NotImplementedException();
        }

        public Task<History> GetDataByDepthAsync(string id, int currentLevel, int depthLevel)
        {
            throw new NotImplementedException();
        }
    }
}
