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
    public class DepartmentDataService : IDataService<Department, DepartmentDB>
    {
        public Department ConvertFromTransferObject(DepartmentDB data)
        {
            if (data == null) return null;
            return new Department()
            {
                Name = data.Name,
                Description = data.Description,
                Id = data.Id,
                HeadId = data.HeadId
            };
        }

        public DepartmentDB ConvertToTransferObject(Department data)
        {
            return new DepartmentDB()
            {
                Name = data.Name,
                Description = data.Description,
                Id = data.Id,
                HeadId = data.HeadId
            };
        }

        public Task Delete(string id)
        {
            return DepartmentDataAccess.Delete(id);
        }

        internal async Task<Department> GetDepartmentNotFullByIdAsync(string id)
        {
            var depDB = await DepartmentDataAccess.GetByIdAsync(id);
            return ConvertFromTransferObject(depDB);
        }
        public async Task<Department> GetDepartmentByDepthAsync(DepartmentDB depDB, int currentLevel = 0, int depthLevel = 1)
        {
            var dep = ConvertFromTransferObject(depDB);
            if (dep != null && currentLevel < depthLevel && !string.IsNullOrEmpty(dep.HeadId.Trim()))
            {
                dep.Head = await new EmployeeDataService().GetDataByDepthAsync(dep.HeadId, currentLevel + 1, depthLevel);
            }
            return dep;
        }
        public async Task<Department> GetDataByDepthAsync(string id, int currentLevel = 0, int depthLevel = 1)
        {
            return await GetDepartmentByDepthAsync(await DepartmentDataAccess.GetByIdAsync(id), currentLevel, depthLevel);

        }

        public async Task<Department> GetByIdAsync(string id)
        {
            return await GetDataByDepthAsync(id,0,1);
        }

        public async Task<IEnumerable<Department>> GetListDetailsDataAsync(Comparison<Department> comparison)
        {
            //привязка свойств объекта к объекту
            var departments = await DepartmentDataAccess.GetAllAsync();
            var result = new List<Department>();
            List<Task<Employee>> tasks = new List<Task<Employee>>();
            foreach (var d in departments)
            {
                //todo таски через WhaitAll() работают бесконечно поэтому синхронно
                /*tasks.Add(GetEmployeeByDepthAsync(e));*/
                result.Add(await GetDepartmentByDepthAsync(d));
            }
            /*Task.WaitAll(tasks.ToArray());
            foreach(var task in tasks)
            {
                result.Add(task.Result);
            }*/
            if (comparison != null)
            {
                result.Sort((el1, el2) => el1.Name.CompareTo(el2.Name));
            }
            return result;
            

        }

        public async Task InsertAsync(Department data)
        {
            await DepartmentDataAccess.InsertAsync(ConvertToTransferObject(data));
        }

        public async Task UpdateInfoAsync(Department data)
        {
            await DepartmentDataAccess.ChangeAll(data.Id, data.Name, data.Description, data.HeadId);
        }
    }
}
