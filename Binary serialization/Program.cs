using Binary_serialization.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Binary_serialization
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee> { new Employee { EmpoyeeName = "Alex" }, new Employee { EmpoyeeName = "Ilya" } };
            Department department = new Department() { DepartmentName = "Dept", Employees = employees};

            Serialize(department);
            Deserialize();
        }

        static void Serialize(Department department)
        {
            FileStream fs = new FileStream("department.dat", FileMode.OpenOrCreate);
            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                formatter.Serialize(fs, department);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        static void Deserialize()
        {
            Department department = null;
            FileStream fs = new FileStream("department.dat", FileMode.OpenOrCreate);

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                department = (Department)formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }

            Console.WriteLine("{0}\nEmployees: ", department.DepartmentName);
            foreach (var item in department.Employees)
            {
                Console.WriteLine("{0}", item.EmpoyeeName);
            }
        }
    }
}
