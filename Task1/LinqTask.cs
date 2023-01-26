using System;
using System.Collections.Generic;
using System.Linq;
using Task1.DoNotChange;

namespace Task1
{
    public static class LinqTask
    {
        public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
        {
            return customers
                .Where(x => x.Orders.Select(x => x.Total).Sum() > limit);
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            return customers
                .Select(x => (x, suppliers.Where(s => s.Country == x.Country && s.City == x.City)));
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2UsingGroup(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            return customers
                .SelectMany(x => suppliers, (customer, supplier) => new { customer, supplier })
                .GroupBy(x => x.customer)
                .Select(c => (c.Key, c.Where(s => s.supplier.Country == s.customer.Country && s.customer.City == s.supplier.City).Select(x => x.supplier)));
        }

        public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            return customers
                .Where(x => x.Orders.Where(c => c.Total > limit).Count() > 0);
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
            IEnumerable<Customer> customers
        )
        {
            return customers
                .Where(x => x.Orders.Count() > 0)
                .Select(c => (c, c.Orders.Select(o => o.OrderDate).Min()));
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
            IEnumerable<Customer> customers
        )
        {
            return customers
                .Where(x => x.Orders.Count() > 0)
                .Select(c => (c, c.Orders.Select(o => o.OrderDate).Min()))
                .OrderBy(x => x.Item2.Year)
                .ThenBy(x => x.Item2.Month)
                .ThenByDescending(x => x.c.Orders.Select(o => o.Total).Sum())
                .ThenBy(x => x.c.CompanyName);
        }

        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            return customers
                .Where(x => string.IsNullOrEmpty(x.Region) || !x.PostalCode.All(char.IsDigit) || !x.Phone.Contains('('));
        }

        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {
            /* example of Linq7result

             category - Beverages
	            UnitsInStock - 39
		            price - 18.0000
		            price - 19.0000
	            UnitsInStock - 17
		            price - 18.0000
		            price - 19.0000
             */

            return products
                .GroupBy(x => x.Category)
                .Select(x => new Linq7CategoryGroup()
                {
                    Category = x.Key,
                    UnitsInStockGroup = x.
                        GroupBy(c => c.UnitsInStock).
                        Select(c => new Linq7UnitsInStockGroup()
                        {
                            UnitsInStock = c.Key,
                            Prices = c.Select(v => v.UnitPrice)
                        })
                }); ;
        }

        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive
        )
        {
            var cheapProducts = products
                .Where(x => x.UnitPrice > 0 && x.UnitPrice <= cheap)
                .Select(x => (cheap, x))
                .GroupBy(x => x.cheap)
                .Select(x => (x.Key, x.Select(c => c.x).AsEnumerable()));

            var middleProducts = products
                .Where(x => x.UnitPrice > cheap && x.UnitPrice <= middle)
                .Select(x => (middle, x))
                .GroupBy(x => x.middle)
                .Select(x => (x.Key, x.Select(c => c.x).AsEnumerable()));

            var expensiveProducts = products
                .Where(x => x.UnitPrice > middle && x.UnitPrice <= expensive)
                .Select(x => (expensive, x))
                .GroupBy(x => x.expensive)
                .Select(x => (x.Key, x.Select(c => c.x).AsEnumerable()));

            return cheapProducts.Union(middleProducts).Union(expensiveProducts);
        }

        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers
        )
        {
            return customers
                .GroupBy(x => x.City)
                .Select(c => (c.Key, (int)Math.Round(c.Select(x => x.Orders.Select(c => c.Total).Sum()).Sum() / c.Count()), c.Select(x => x.Orders.Count()).Sum() / c.Count()));
        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            return suppliers
                .Select(x => x.Country)
                .OrderBy(x => x.Length)
                .ThenBy(x => x)
                .Distinct()
                .Aggregate((current, next) => current + next);
        }
    }
}