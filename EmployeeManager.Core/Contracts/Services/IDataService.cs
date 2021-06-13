using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManager.Core.DBAccess;
using EmployeeManager.Core.Models;

namespace EmployeeManager.Core.Contracts.Services
{
    public interface IDataService<Data,Transfer>
        where Data : IData
        where Transfer : ITransferData
    {
        Task<IEnumerable<Data>> GetListDetailsDataAsync(Comparison<Data> comparison = null);

        Transfer ConvertToTransferObject(Data data);

        Data ConvertFromTransferObject(Transfer data);

        Task InsertAsync(Data data);

        Task UpdateInfoAsync(Data data);

        Task Delete(string id);

        Task<Data> GetByIdAsync(string id);

        Task<Data> GetDataByDepthAsync(string id, int currentLevel = 0, int depthLevel = 1);
    }
}
