using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Core.Contracts.Services
{
    public interface IData
    {
        string Name { get; }

        string Description { get; set; }

        string Id { get; set; }

        Type GetType();
    }
}
