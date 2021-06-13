using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Core.Models
{
    public enum Sex
    {
        M,
        F   
    }

    public static class SexParser
    {
        public static Sex Parse(in String sex)
        {
            switch (sex)
            {
                case "M":   return Sex.M;
                case "F":   return Sex.F;
                default:    throw new ArgumentException($"No such Sex : {sex}");
            }
        }
    }
}
