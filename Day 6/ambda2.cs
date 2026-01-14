using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo_Collection_Stack_Queue
{
    // Lambdas are anonymous functions that simplify code, especially with LINQ and collections
    // This demo shows common patterns: Func, Action, Predicate, LINQ methods and block lambdas

    internal class LambdaLinqDemo
    {
        class Person
        {
            public string Name;
            public int Age;
            public string City;
            public override string ToString() => $"{Name} ({Age}) from {City}";
        }

        static void Main()
        {
            var people = new List<Person> {
                new Person{Name = "Alice", Age = 30, City = "NY"},
                new Person{Name = "Bob", Age = 17, City = "LA"},
                new Person{Name = "Charlie", Age = 25, City = "NY"},
                new Person{Name = "Diana", Age = 22, City = "LA"},
                new Person{Name = "Eve", Age = 40, City = "SF"},
            };

            // 1) Func example (predicate) - simple expression lambda
            Func<Person, bool> isAdult = p => p.Age >= 18;
            Console.WriteLine("Adults (using Func):");
            foreach (var p in people.Where(isAdult)) Console.WriteLine(" - " + p);

            // 2) LINQ chaining with lambdas (Where, OrderBy, Select)
            var nyAdultsNames = people
                .Where(p => p.City == "NY" && p.Age >= 18)
                .OrderBy(p => p.Age)
                .Select(p => p.Name.ToUpper())
                .ToList();
            Console.WriteLine("NY adults (names): " + string.Join(", ", nyAdultsNames));

            // 3) Useful LINQ helpers
            Console.WriteLine("Any teen? " + people.Any(p => p.Age < 20));
            Console.WriteLine("All from same city? " + people.All(p => p.City == "NY"));
            Console.WriteLine("Average age: " + people.Average(p => p.Age));
            Console.WriteLine("Total age of LA residents: " + people.Where(p => p.City == "LA").Sum(p => p.Age));

            // 4) GroupBy with a lambda key selector
            var grouped = people.GroupBy(p => p.City);
            Console.WriteLine("Grouped by City:");
            foreach (var g in grouped)
            {
                Console.WriteLine($" - {g.Key}: {string.Join(", ", g.Select(p => p.Name))}");
            }

            // 5) Block lambda with multiple statements
            Func<Person, string> describe = p =>
            {
                var status = p.Age >= 18 ? "adult" : "minor";
                return $"{p.Name} is an {status} aged {p.Age}";
            };
            Console.WriteLine(describe(people[1]));

            // 6) Passing a lambda to a custom method (higher-order function)
            var minors = Filter(people, p => p.Age < 18);
            Console.WriteLine("Minors via Filter: " + string.Join(", ", minors.Select(p => p.Name)));

            // 7) Action example
            Action<Person> print = p => Console.WriteLine("Person: " + p);
            Console.WriteLine("All people:");
            people.ForEach(print);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static IEnumerable<Person> Filter(IEnumerable<Person> source, Func<Person, bool> predicate)
        {
            foreach (var item in source)
                if (predicate(item))
                    yield return item;
        }
    }
}
