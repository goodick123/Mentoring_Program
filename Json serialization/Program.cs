using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Json_serialization.Models;

namespace Json_serialization
{
    class Program
    {
        static async Task Main(string[] args)
        {
            List<Employee> employees = new List<Employee> { new Employee { EmpoyeeName = "Alex" }, new Employee { EmpoyeeName = "Ilya" } };
            Department department = new Department() { DepartmentName = "Dept", Employees = employees };

            JsonSerializerOptions options = new JsonSerializerOptions { };
            await SerializeAsync(department, options);
            Deserialize();
        }


        static async Task SerializeAsync(Department department, JsonSerializerOptions options)
        {
            string fileName = "Department.json";
            using FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, department);
            await createStream.DisposeAsync();
        }

        static void Deserialize()
        {
            string fileName = "Department.json";
            string jsonString = File.ReadAllText(fileName);
            Department department = JsonSerializer.Deserialize<Department>(jsonString)!;

            Console.WriteLine("{0}\nEmployees: ", department.DepartmentName);
            foreach (var item in department.Employees)
            {
                Console.WriteLine("{0}", item.EmpoyeeName);
            }
        }
    }
}
