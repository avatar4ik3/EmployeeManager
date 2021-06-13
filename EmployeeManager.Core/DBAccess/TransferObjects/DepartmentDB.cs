using EmployeeManager.Core.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Core.DBAccess.TransferObjects
{
    public class DepartmentDB : ITransferData
    {
        #region Public Properties
        public String HeadId { get; set; }
    
        public String Name { get; set; }

        public String Description { get; set; }

        public String Id { get; set; }
        #endregion

        #region Constructor

        public DepartmentDB()
        {

        }

        #endregion
    }
}
