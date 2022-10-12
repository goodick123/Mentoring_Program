using System;
using System.Linq;
using Task1_Lib;

namespace Task1_Core
{
    class Program
    {
        public class Product
        {
            public Product(string name, double price)
            {
                Name = name;
                Price = price;
            }

            public string Name { get; set; }

            public double Price { get; set; }
        }
        static void Main(string[] args)
        {
            var products = new Product[]
            {
                new Product("Product 1", 10.0d),
                new Product("Product 2", 20.0d),
                new Product("Product 3", 30.0d),
            };
            var productToFind = products[2];
            Console.WriteLine(IndexOf(products, x=>x.Equals(productToFind)));
        }
        public static int IndexOf(Product[] products, Predicate<Product> predicate)
        {
            if (predicate is null || products is null)
                throw new ArgumentNullException();

            for (int i = 0; i < products.Length - 1; i++)
            {
                var product = products[i];
                if (predicate(product))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
