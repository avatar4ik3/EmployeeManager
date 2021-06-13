using EmployeeManager.Core.Contracts.Services;
using EmployeeManager.Core.DBAccess;
using System;
using System.Collections.Generic;

namespace EmployeeManager.Core.Models
{
    // Remove this class once your pages/features are using your data.
    // This is used by the SampleDataService.
    // It is the model class we use to display data on pages like Grid, Chart, and List Details.
    public class Department : IData
    {
        #region Public Properties
        public Employee Head { get;  set; }

        public string HeadId { get;  set; }

        /// <summary>
        /// List of employees who works in this department including DepartmentHead
        /// </summary>
        public List<Employee> employees { get;  set; } = new List<Employee>();

        public string Name { get; set; }

        public string Description { get; set; }
        public string Id { get; set; }

        #endregion

        #region Constructor
        /*public Department(Employee head,String name,String description)
        {
            Head = head;
            employees.Add(head);

            Name = name;
            Description = description;
            Id = Guid.NewGuid().ToString();
        }

        public Department(String headId,String id,String name,String description)
        {
            Head = head;
            employees.Add(EmployeeService.GetEmployeeById(headId));
            Id = id;
            Name = name;
            Description = description;
        }*/

        public Department()
        {
            Id = Guid.NewGuid().ToString();
        }

        #endregion

        /*public static bool operator !=(Department d1, Department d2)
        {
            return !(d1 == d2);
        }
        public static bool operator ==(Department d1, Department d2)
        {
            return d1.Id == d2.Id;
        }*/
    }
}
