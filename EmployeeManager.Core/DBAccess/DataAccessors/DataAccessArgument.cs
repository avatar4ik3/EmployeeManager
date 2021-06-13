using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EmployeeManager.Core.DBAccess.DataAccessors
{
    public class DataAccessArgument
    {
        public String Argument { get; set; }
        public String Value { get; set; }
        public SqlDbType Type { get; set; }

        public DataAccessArgument(String Argument, String Value, SqlDbType Type)
        {
            this.Argument = Argument;
            this.Value = Value;
            this.Type = Type;
        }
    }
}
