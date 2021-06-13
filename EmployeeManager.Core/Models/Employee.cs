using EmployeeManager.Core.Contracts.Services;
using EmployeeManager.Core.DBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Core.Models
{
    public class Employee : IData
    {
        #region Private Fields

        private String _firstName;
        
        private String _sureName;

        private String _middleName;

        #endregion

        #region Public Properties

        public string FirstName { get; set; }

        public string SureName
        {
            get; set;
        }

        public string MiddleName { get; set; }

        public Sex Sex { get;  set; }
        
        public DateTime Birth { get;  set; }

        public string Position { get;  set; }

        public Employee Chief { get;  set; }

        public string ChiefId { get;  set; }

        public Department Department { get;  set; }

        public string DepartmentId { get;  set; }

        public int Salary { get;  set; }
        
        public List<History> History { get;  set; }
        public string Name {
            get
            {
                return $"{SureName} {FirstName} {MiddleName}";
            }
        }
        public string Description { get; set; }
        public string Id { get; set; }

        #endregion


        #region Constructor
        public Employee(
                        String firstName,
                        String sureName,
                        String middleName,
                        Sex sex,
                        String position,
                        String description,
                        DateTime birth,
                        Department department = null,
                        Employee chief = null,
                        String id = null
            
            )
        {
            this.FirstName = firstName;
            this.SureName = sureName;
            this.MiddleName = middleName;
            this.Sex = sex;
            if (id != null)
            {
                this.Id = id;
            }
            else
            {
                this.Id = Guid.NewGuid().ToString();
            }

            this.Position = position;
            this.Description = description;
            this.Department = department;
            this.Chief = chief;
            this.Birth = birth;
        }

        //public Employee(EmployeeDB empDB) : this(empDB.FirstName, empDB.SureName, empDB.MiddleName, SexParser.Parse(empDB.Sex), empDB.Position, empDB.Description, DateTime.Parse(empDB.Birth),);

        public Employee()
        {
            Id = Guid.NewGuid().ToString();
        }
        
        #endregion

        /*public static bool operator ==(Employee e1,Employee e2)
        {
            return e1.Id == e2.Id;
        }

        public static bool operator !=(Employee e1, Employee e2)
        {
            return !(e1 == e2);
        }*/
    }
}
