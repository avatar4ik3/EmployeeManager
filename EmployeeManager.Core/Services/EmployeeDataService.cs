using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EmployeeManager.Core.Contracts.Services;
using EmployeeManager.Core.DBAccess;
using EmployeeManager.Core.DBAccess.DataAccessors;
using EmployeeManager.Core.DBAccess.TransferObjects;
using EmployeeManager.Core.Models;

namespace EmployeeManager.Core.Services
{
    // This class holds sample data used by some generated pages to show how they can be used.
    // TODO WTS: The following classes have been created to display sample data. Delete these files once your app is using real data.
    // 1. Contracts/Services/ISampleDataService.cs
    // 2. Services/SampleDataService.cs
    // 3. Models/SampleCompany.cs
    // 4. Models/SampleOrder.cs
    // 5. Models/SampleOrderDetail.cs
    public class EmployeeDataService : IDataService<Employee, EmployeeDB>
    {
        public EmployeeDataService()
        {
        }


        public Task Delete(string id)
        {
            return EmployeesDataAccess.Delete(id);
        }

        public async Task<Employee> getEmployeeNotFullByIdAsync(string id)
        {
            var empDB = await EmployeesDataAccess.GetByIdAsync(id);
            return ConvertFromTransferObject(empDB);
        }

        public async Task<Employee> GetEmployeeByDepthAsync(EmployeeDB empDB, int currentLevel = 0, int depthLevel = 1)
        {
            if (empDB == null) return null;
            var emp = ConvertFromTransferObject(empDB);
            if (emp != null && currentLevel < depthLevel)
            {
                //todo разобраться до конца как собрать все таски в коллекцию и запустить их вместе. пока что пусть будет синхронно 
                /*List<Task<IData>> datas = new List<Task<IData>>();
                Task<IData> getChief = Task.Run(
                    new Func<IData>(
                        () => {
                            Task<Employee> t = GetEmployeeByDepthAsync(emp.ChiefId, currentLevel + 1, depthLevel);
                            t.Wait();
                            return t.Result;
                        }
                        )
                    );
                Task<IData> getDepartment = Task.Run(
                    new Func<IData>(
                        () => {
                            Task<Department> t = new DepartmentDataService().GetDepartmentByDepthAsync(emp.DepartmentId, currentLevel + 1, depthLevel);
                            t.Wait();
                            return t.Result;
                        }
                        )
                    );
                datas.Add(getChief);
                datas.Add(getDepartment);
                Task.WaitAll(datas.ToArray());
                emp.Chief = datas[0].Result as Employee;
                emp.Department = datas[1].Result as Department;*/
                emp.Department = await new DepartmentDataService().GetDataByDepthAsync(emp.DepartmentId, currentLevel + 1, depthLevel);

                if (String.IsNullOrEmpty(emp.ChiefId))
                {
                    emp.Chief = null;
                }
                else
                {
                    emp.Chief = await GetDataByDepthAsync(emp.ChiefId, currentLevel + 1, depthLevel);
                }
            }
            return emp;
        }



        public async Task<Employee> GetDataByDepthAsync(string id, int currentLevel = 0, int depthLevel = 1)
        {
            var empDB = await EmployeesDataAccess.GetByIdAsync(id);
            if (empDB == null) return null;
            return await GetEmployeeByDepthAsync(empDB, currentLevel, depthLevel);
        }

        public async Task<Employee> GetByIdAsync(string id)
        {
            return await GetDataByDepthAsync(id);
        }

        public async Task<IEnumerable<Employee>> GetListDetailsDataAsync(Comparison<Employee> comparison = null)
        {
            //привязка свойств объекта к объекту
            var employees = await EmployeesDataAccess.GetAllAsync();
            var result = new List<Employee>();
            List<Task<Employee>> tasks = new List<Task<Employee>>();
            foreach(var e in employees)
            {
                //todo таски через WhaitAll() работают бесконечно поэтому синхронно
                /*tasks.Add(GetEmployeeByDepthAsync(e));*/
                result.Add(await GetEmployeeByDepthAsync(e));
            }
            /*Task.WaitAll(tasks.ToArray());
            foreach(var task in tasks)
            {
                result.Add(task.Result);
            }*/
            if(comparison != null)
            {
                result.Sort((el1, el2) => el1.Name.CompareTo(el2.Name));
            }
            return result;
            //без привязки
            //return (await EmployeesDataAccess.GetAllAsync()).Select(el => ConvertFromTransferObject(el));

        }


        public EmployeeDB ConvertToTransferObject(Employee data)
        {
            return new EmployeeDB()
            {
                Id = data.Id,
                FirstName = data.FirstName,
                SureName = data.SureName,
                MiddleName = data.MiddleName,
                Sex = data.Sex.ToString(),
                Position = data.Position,
                Description = data.Description,
                DepartmentId = data.DepartmentId,
                ChiefId = data.ChiefId,
                Salary = data.Salary.ToString(),
                //todo посмотреть как дата конвертируется
                Birth = data.Birth.ToShortDateString()
            };
        }

        public Employee ConvertFromTransferObject(EmployeeDB data)
        {
            return new Employee()
            {
                FirstName = data.FirstName.Trim(),
                SureName = data.SureName.Trim(),
                MiddleName = data.MiddleName.Trim(),
                Id = data.Id.Trim(),
                Sex = SexParser.Parse(data.Sex.Trim()),
                Position = data.Position.Trim(),
                Description = data.Description.Trim(),
                Birth = DateTime.Parse(data.Birth.Trim()),
                Salary = int.Parse(data.Salary.Trim()),
                DepartmentId = data.DepartmentId.Trim(),
                ChiefId = data.ChiefId.Trim()
                
            };
        }

        public async Task InsertAsync(Employee data)
        {
            var empDB = ConvertToTransferObject(data);
            await EmployeesDataAccess.InsertAsync(empDB);
        }

        public async Task UpdateInfoAsync(Employee data)
        {
            var empDB = ConvertToTransferObject(data);
            await EmployeesDataAccess.ChangeAllAsync(
                empDB.Id, empDB.FirstName, empDB.SureName, empDB.MiddleName, empDB.Sex.ToString(),
                empDB.Position, empDB.Description, empDB.DepartmentId, empDB.ChiefId,
                empDB.Birth, empDB.Salary
                );
        }

        public async Task Delete(Employee data)
        {
            await Delete(data.Id);
        }

        

        
    }
}
