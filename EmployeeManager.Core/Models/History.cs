using EmployeeManager.Core.Contracts.Services;
using EmployeeManager.Core.DBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Core.Models
{
    public class History : IData
    {
        public DateTime From { get; internal set; }

        public String Position { get; internal set; }

        public Department Department { get; internal set; }

        public String DepartmentId { get; internal set; }

        public Employee Chief { get; internal set; }

        public String ChiefId { get; internal set; }

        public Employee Employee { get; internal set; }

        public String EmployeeId { get; internal set; }

        public int Salary { get; internal set; }

        public string Name
        {
            get
            {
                return $"{From} about {EmployeeId}";
            }
        }

        public string Description { get; set; }
        public string Id { get; set; }

        public History(DateTime from,String position,String description,Department department,Employee chief,Employee employee,int salary)
        {
            From = from;
            Position = position;
            Description = description;
            Department = department;
            Chief = chief;
            Employee = employee;
            Salary = Salary;
            Id = Guid.NewGuid().ToString();
        }

        public History()
        {
            Id = Guid.NewGuid().ToString();
        }

        /*public static bool operator ==(History h1, History h2)
        {
            return h1.Id == h2.Id;
        }

        public static bool operator !=(History h1, History h2)
        {
            return !(h1 == h2);
        }*/
    }
}
