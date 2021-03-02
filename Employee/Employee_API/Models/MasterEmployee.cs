using System;
using System.Collections.Generic;



namespace Employee_API.Models
{
    public partial class MasterEmployee
    {
        public int PkempId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeDesigination { get; set; }
        public DateTime EmployeeDob { get; set; }
        public string EmployeePhonenumber { get; set; }
    }
}
