using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCorewithEF.Models
{
    public class Employees
    {
        [Key]
        public int EmployeeId { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public int Age { get; set; }
    }
}
