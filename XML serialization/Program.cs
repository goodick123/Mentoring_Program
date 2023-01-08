using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using XML_serialization.Models;

namespace XML_serialization
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee> { new Employee { EmpoyeeName = "Alex" }, new Employee { EmpoyeeName = "Ilya"} };
            Department department = new Department() { DepartmentName = "Dept", Employees = employees };

            Serialize(department);
            Deserialize();
        }

        static void Serialize(Department department)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Department));
            using (FileStream fs = new FileStream("Department.xml", FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, department);
            }
        }

        static void Deserialize()
        {
            Department department = null;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Department));
            using (FileStream fs = new FileStream("Department.xml", FileMode.OpenOrCreate))
            {
                department = xmlSerializer.Deserialize(fs) as Department;
            }

            Console.WriteLine("{0}\nEmployees: ", department.DepartmentName);
            foreach (var item in department.Employees)
            {
                Console.WriteLine("{0}", item.EmpoyeeName);
            }
        }
    }
}
