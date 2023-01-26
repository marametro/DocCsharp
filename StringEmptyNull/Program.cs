using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

class Program
{

    static void Main(string[] args)
    {
        Employee employee = new Employee();
        employee.FirstName = GetFirstName();

    }


    public class Employee
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public List<string> Skills { get; set; } = new List<string>();
    }

    private static string GetFirstName()
    {
        while (true)
        {
            WriteLine("Please enter first name");

            string firstName = ReadLine();

            if (string.IsNullOrWhiteSpace(firstName))
            {
                WriteLine("ERROR: Invalid first name");
            }
            else
            {
                return firstName;
            }
        }
    }

}