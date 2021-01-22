using System;
using System.Collections.Generic;
using System.Linq;
using LinqTask1.DoNotChange;

namespace LinqTask1
{
    public static class LinqTask
    {
        public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
        {
            if (customers == null) throw new ArgumentNullException();
            return customers
                .Where(customer => customer
                .Orders.Sum(order => order.Total) > limit);
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            if (customers == null || suppliers == null) throw new ArgumentNullException();
            return customers.Select(customer => (
                customer, 
                suppliers.Where(supplier => string.Equals(customer.Country, supplier.Country, StringComparison.OrdinalIgnoreCase)
                        && string.Equals(customer.City, supplier.City, StringComparison.OrdinalIgnoreCase))
            ));
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2UsingGroup(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            if (customers == null || suppliers == null) throw new ArgumentNullException();
            var result = from c in customers
                         from s in suppliers
                         group s by c
                          into g
                         select (g.Key,g.Where(supplier => string.Equals(g.Key.Country, supplier.Country, StringComparison.OrdinalIgnoreCase)
                        && string.Equals(g.Key.City, supplier.City, StringComparison.OrdinalIgnoreCase)));
            return result;
        }

        public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            if (customers == null) throw new ArgumentNullException();
            return customers.Where(c => c.Orders.Any(o => o.Total > limit));
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
            IEnumerable<Customer> customers
        )
        {
            if (customers == null) throw new ArgumentNullException();
            return customers.Where(c=>c.Orders.Length > 0)
                .Select(c => (c, c.Orders
                .OrderBy(o => o.OrderDate)
                .First().OrderDate));
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
            IEnumerable<Customer> customers
        )
        {
            if (customers == null) throw new ArgumentNullException();

            return customers
                .Where(c => c.Orders.Length > 0)
                .Select(c => (c, c.Orders
                .OrderBy(o => o.OrderDate)
                .First().OrderDate))
                .OrderBy(x=>x.OrderDate.Year)
                .ThenBy(x=>x.OrderDate.Month)
                .ThenByDescending(x=>x.c.Orders.Sum(y=>y.Total))
                .ThenBy(x=>x.c.CustomerID);
        }

        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            if (customers == null) throw new ArgumentNullException();

            return customers.Where(c => !c.PostalCode.All(x => char.ToLower(x) >= '0' && char.ToLower(x) <= '9')
                || string.IsNullOrEmpty(c.Region)
                || !c.Phone.Contains('('));
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

            if (products == null) throw new ArgumentNullException();

            return products.GroupBy(p => p.Category).Select(item => new Linq7CategoryGroup
            {
                Category = item.Key,
                UnitsInStockGroup = item.GroupBy(p => p.UnitsInStock).Select(item => new Linq7UnitsInStockGroup
                {
                    UnitsInStock = item.Key,
                    Prices = item.OrderBy(x => x.UnitPrice).Select(x => x.UnitPrice)
                })
            });
        }

        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive
        )
        {
            if (products == null) throw new ArgumentNullException();

            return products.GroupBy(p => p.UnitPrice <= cheap ? cheap : p.UnitPrice <= middle ? middle : expensive)
                .Select(_ =>( 
                    _.Key,
                    _.AsEnumerable()
                ));
        }

        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers
        )
        {
            if (customers == null) throw new ArgumentNullException();
            return customers.GroupBy(c=>c.City)
                .Select(item => (item.Key,
                (int)item.Average(x=>decimal.Round(x.Orders.Sum(y=>y.Total))), 
                (int)item.Average(x=>x.Orders.Length)));
        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            if (suppliers == null) throw new ArgumentNullException();
            return suppliers
                .Select(s => s.Country)
                .Distinct()
                .OrderBy(s => s.Length)
                .ThenBy(s => s)
                .Aggregate((a,b) => a+=b);
        }
    }
}