using System;
using System.Collections.Generic;

namespace Binary_serialization.Models
{
    [Serializable]
    public class Department
    {
        public string DepartmentName { get; set; }

        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
