using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consoleapp1
{
    class Program
    {
        static void Main(string[] args)
        {

            string firstName;
            string lastName;
            string fullName;

            Console.WriteLine("Hello to my first console app!");

            Console.WriteLine("Enter your first name:");
            firstName = Console.ReadLine();

            Console.WriteLine("Enter your last name");
            lastName = Console.ReadLine();

            fullName = firstName + " " + lastName;
            Console.WriteLine("Welcome {0} to my first console app!", fullName);
        }
    }
}