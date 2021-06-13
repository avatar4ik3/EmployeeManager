using EmployeeManager.Core.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Core.DBAccess.TransferObjects
{
    public class EmployeeDB : ITransferData
    {

        #region Public Properties

        public String FirstName { get; set; }

        public String SureName { get; set; }

        public String MiddleName { get; set; }

        public String Sex { get; set; }

        public String Birth { get; set; }

        public String Id { get; set; }

        public String Position { get; set; }

        public String Description { get; set; }

        public String ChiefId { get; set; }

        public String DepartmentId { get; set; }

        public String Salary { get; set; }

        #endregion


        #region Constructor
        public EmployeeDB()
        {

        }
        #endregion
    }
}
