using System;
using System.Collections.Generic;

/*
 * PROPERTIES vs INDEXERS - Key Differences
 * ========================================
 * 
 * PROPERTIES:
 * - Accessed using a NAME (e.g., employee.Name, product.Price)
 * - Encapsulate SINGLE ATTRIBUTES (like Name, Age, Price)
 * - No parameters in getter/setter (only 'get' and 'set' keywords)
 * - Can be STATIC or INSTANCE members
 * - Declared with a name: public string Name { get; set; }
 * 
 * INDEXERS:
 * - Accessed using an INDEX/KEY (e.g., employee[0], product["John"])
 * - Encapsulate COLLECTIONS (like Attendance, Ratings, Marks)
 * - Both get and set use PARAMETERS (e.g., int index, string key)
 * - Must be INSTANCE members (cannot be static)
 * - Declared with 'this' keyword: public int this[int index] { get; set; }
 */

// Example 1: Student with Properties and Indexers
class Student
{
    // PROPERTIES - Access by name, single attributes
    public string Name { get; set; }          // Property - Name
    public int Age { get; set; }              // Property - Age
    public string Email { get; set; }         // Property - Email
    public static int TotalStudents { get; set; }  // Static Property

    private Dictionary<int, int> marks = new Dictionary<int, int>();
    private Dictionary<string, string> contactInfo = new Dictionary<string, string>();

    public Student(string name, int age, string email)
    {
        Name = name;
        Age = age;
        Email = email;
        TotalStudents++;
    }

    // INDEXER - Access by index, collection of marks
    // Syntax: public return_type this[parameter_type param] { get; set; }
    public int this[int subject]
    {
        get
        {
            if (marks.ContainsKey(subject))
                return marks[subject];
            return 0;
        }
        set { marks[subject] = value; }
    }

    // INDEXER - Access by key, collection of contact info
    public string this[string infoType]
    {
        get
        {
            if (contactInfo.ContainsKey(infoType))
                return contactInfo[infoType];
            return "Not found";
        }
        set { contactInfo[infoType] = value; }
    }

    public void DisplayStudentInfo()
    {
        Console.WriteLine($"\n--- Student Details (Properties) ---");
        Console.WriteLine($"Name: {Name}");        // Using Property
        Console.WriteLine($"Age: {Age}");          // Using Property
        Console.WriteLine($"Email: {Email}");      // Using Property
        Console.WriteLine($"Total Students: {TotalStudents}");  // Using Static Property

        Console.WriteLine($"\n--- Subject Marks (Indexer[int]) ---");
        for (int i = 1; i <= 3; i++)
        {
            Console.WriteLine($"Subject {i}: {this[i]} marks");  // Using Indexer with int
        }

        Console.WriteLine($"\n--- Contact Info (Indexer[string]) ---");
        Console.WriteLine($"Phone: {this["Phone"]}");           // Using Indexer with string
        Console.WriteLine($"Address: {this["Address"]}");       // Using Indexer with string
    }
}

// Example 2: Company with Properties and Indexers
class Company
{
    // PROPERTIES - Single attributes
    public string CompanyName { get; set; }
    public string CEO { get; set; }
    public decimal Revenue { get; set; }
    public static int TotalCompanies { get; set; }

    private List<string> employees = new List<string>();
    private Dictionary<string, decimal> departmentBudget = new Dictionary<string, decimal>();

    public Company(string name, string ceo, decimal revenue)
    {
        CompanyName = name;
        CEO = ceo;
        Revenue = revenue;
        TotalCompanies++;
    }

    // INDEXER - Access employees by position
    public string this[int employeePosition]
    {
        get
        {
            if (employeePosition >= 0 && employeePosition < employees.Count)
                return employees[employeePosition];
            return "No employee at this position";
        }
        set
        {
            while (employees.Count <= employeePosition)
                employees.Add(null);
            employees[employeePosition] = value;
        }
    }

    // INDEXER - Access department budgets by name
    public decimal this[string departmentName]
    {
        get
        {
            if (departmentBudget.ContainsKey(departmentName))
                return departmentBudget[departmentName];
            return 0;
        }
        set { departmentBudget[departmentName] = value; }
    }

    public void DisplayCompanyInfo()
    {
        Console.WriteLine($"\n--- Company Details (Properties) ---");
        Console.WriteLine($"Company: {CompanyName}");      // Using Property
        Console.WriteLine($"CEO: {CEO}");                  // Using Property
        Console.WriteLine($"Revenue: ${Revenue}");         // Using Property
        Console.WriteLine($"Total Companies: {TotalCompanies}");  // Using Static Property

        Console.WriteLine($"\n--- Employees (Indexer[int]) ---");
        for (int i = 0; i < employees.Count; i++)
        {
            Console.WriteLine($"Position {i}: {this[i]}");  // Using Indexer with int
        }

        Console.WriteLine($"\n--- Department Budgets (Indexer[string]) ---");
        foreach (var dept in departmentBudget)
        {
            Console.WriteLine($"{dept.Key}: ${this[dept.Key]}");  // Using Indexer with string
        }
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║        PROPERTIES vs INDEXERS - Detailed Demonstration         ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");

        // STUDENT EXAMPLE
        Console.WriteLine("\n\n========== EXAMPLE 1: STUDENT CLASS ==========");
        Student student1 = new Student("Alice Johnson", 20, "alice@email.com");
        Student student2 = new Student("Bob Smith", 21, "bob@email.com");

        // Using PROPERTIES (by name, single attributes)
        student1.Name = "Alice Johnson";      // Property - no parameters
        student1.Age = 20;                    // Property - no parameters
        student1.Email = "alice@email.com";   // Property - no parameters

        // Using INDEXERS (by index/key, collections)
        student1[1] = 95;   // Indexer with int parameter - Subject 1 marks
        student1[2] = 87;   // Indexer with int parameter - Subject 2 marks
        student1[3] = 92;   // Indexer with int parameter - Subject 3 marks
        student1["Phone"] = "555-1234";       // Indexer with string parameter
        student1["Address"] = "123 Main St";  // Indexer with string parameter

        student1.DisplayStudentInfo();

        // COMPANY EXAMPLE
        Console.WriteLine("\n\n========== EXAMPLE 2: COMPANY CLASS ==========");
        Company company1 = new Company("TechCorp", "John Doe", 5000000);
        Company company2 = new Company("DataSystems", "Jane Smith", 8000000);

        // Using PROPERTIES (by name, single attributes)
        company1.CompanyName = "TechCorp";    // Property - no parameters
        company1.CEO = "John Doe";            // Property - no parameters
        company1.Revenue = 5000000;           // Property - no parameters

        // Using INDEXERS (by index/key, collections)
        company1[0] = "Manager - Alice";      // Indexer with int parameter - Employee position
        company1[1] = "Developer - Bob";      // Indexer with int parameter - Employee position
        company1[2] = "Designer - Charlie";   // Indexer with int parameter - Employee position
        company1["IT"] = 500000;              // Indexer with string parameter - Department budget
        company1["HR"] = 300000;              // Indexer with string parameter - Department budget
        company1["Sales"] = 700000;           // Indexer with string parameter - Department budget

        company1.DisplayCompanyInfo();

        // SUMMARY
        Console.WriteLine("\n\n╔════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                          KEY DIFFERENCES                         ║");
        Console.WriteLine("╠════════════════════════════════════════════════════════════════╣");
        Console.WriteLine("║ PROPERTIES          │ Accessed by NAME (student.Name)           ║");
        Console.WriteLine("║                     │ Single attributes                         ║");
        Console.WriteLine("║                     │ No parameters (only get/set)              ║");
        Console.WriteLine("║                     │ Can be STATIC or INSTANCE                 ║");
        Console.WriteLine("║─────────────────────┼──────────────────────────────────────────║");
        Console.WriteLine("║ INDEXERS            │ Accessed by INDEX/KEY (student[1])       ║");
        Console.WriteLine("║                     │ Collections of data                       ║");
        Console.WriteLine("║                     │ Both get/set have PARAMETERS              ║");
        Console.WriteLine("║                     │ Must be INSTANCE members (not static)    ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");
    }
}
