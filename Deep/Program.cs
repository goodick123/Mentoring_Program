using Deep.Models;
using System;
using System.Collections.Generic;

namespace Deep_cloning_serialization
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee> { new Employee { EmpoyeeName = "Alex" }, new Employee { EmpoyeeName = "Ilya" } };
            Department department = new Department() { DepartmentName = "Dept", Employees = employees };

            Department department1 = (Department)department.Clone();

            Console.WriteLine("{0}\nEmployees: ", department1.DepartmentName);
            foreach (var item in department1.Employees)
            {
                Console.WriteLine("{0}", item.EmpoyeeName);
            }
        }
    }
}
