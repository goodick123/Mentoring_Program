using Own.Models;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Own
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleClass simpleClass = new SimpleClass() { Name = "Alex", Age = 22};

            Serialize(simpleClass);
            Deserialize();
        }

        static void Serialize(SimpleClass simpleClass)
        {
            FileStream fs = new FileStream("simpleClass.dat", FileMode.OpenOrCreate);
            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                formatter.Serialize(fs, simpleClass);
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
            SimpleClass simpleClass = null;
            FileStream fs = new FileStream("simpleClass.dat", FileMode.OpenOrCreate);

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                simpleClass = (SimpleClass)formatter.Deserialize(fs);
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

            Console.WriteLine("Name: {0}\nAge: {1}", simpleClass.Name, simpleClass.Age);
        }
    }
}
