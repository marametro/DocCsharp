using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy
{
	public class KOA
	{
		public void ChecStringkPalindrome()
		{
			Console.Write("Enter a string to Check Palindrome : ");
			string name = Console.ReadLine();
			string reverse = string.Empty;

			for (int i = name.Length - 1; i >= 0; i--)
				reverse += name[i];

			if (name == reverse)
				Console.WriteLine($"{name} is Palindrome.");
			else
				Console.WriteLine($"{name} is not Palindrome");
		}

		public void CheckNumberPalindrome()
		{
            Console.Write("Enter a Number To Check Palindrome : ");
            int number = int.Parse(Console.ReadLine());
            int remineder, sum = 0;
            int temp = number;
            while (number > 0)
            {
                //Get the remainder by dividing the number with 10  
                remineder = number % 10;
                //multiply the sum with 10 and then add the remainder
                sum = (sum * 10) + remineder;
                //Get the quotient by dividing the number with 10 
                number = number / 10;
            }
            if (temp == sum)
            {
                Console.WriteLine($"Number {temp} is Palindrome.");
            }
            else
            {
                Console.WriteLine($"Number {temp} is not Palindrome");
            }
            Console.ReadKey();
        }

        public void PrintTriangleUsingFor()
        {
            Console.Write("Enter a Number = ");
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i <= n; i++)
            {
                for (int y = 0; y < i; y++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }

        public void PrintTriangleUsingWhile()
        {
            Console.Write("Enter a Number = ");
            int n = int.Parse(Console.ReadLine());
            int i = 0;
            while (i < n)
            {
                int y = 0;
                while (y <= i)
                {
                    Console.Write("*");
                    y++;
                }
                Console.WriteLine();
                i++;
            }

        }




    }
}
