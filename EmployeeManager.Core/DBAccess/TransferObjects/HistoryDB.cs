using EmployeeManager.Core.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Core.DBAccess.TransferObjects
{
    public class HistoryDB : ITransferData
    {
        public String From { get; internal set; }

        public String EmployeeId { get; internal set; }

        public String Position { get; internal set; }

        public String Description { get; internal set; }

        public String DepartmentId { get; internal set; }

        public String ChiefId { get; internal set; }

        public String Salary { get; internal set; }

        public String Id { get; internal set; }

        public HistoryDB()
        {

        }
    }
}
