using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy
{
	public class MenuOption : Mathematical
	{
		KOA koa;
		public MenuOption()
		{
			koa = new KOA();
		}
		public int DisplayMenu()
		{
			Console.WriteLine();
			Console.WriteLine("----------LIST CHOICE----------");
			Console.WriteLine();
			Console.WriteLine("0. Clear Console");
			Console.WriteLine("1. IS Prime Number ");
			Console.WriteLine("2. Display Prime Number");
			Console.WriteLine("3. Display Fibonaci Number");
			Console.WriteLine("4. Rekursif Fibonaci Number");
			Console.WriteLine("5. Factorial Number");
			Console.WriteLine("6. Print Factorial Number");
			Console.WriteLine("7. Menghitung Percen 1");
			Console.WriteLine("20. Check String Palindrome");
			Console.WriteLine("21. Check Number Palindrome");
			Console.WriteLine("22. Print Triangle Using For");
			Console.WriteLine("23. Print Triangle Using While");


			Console.WriteLine("100. Exit");
			Console.WriteLine("-------------------------------------");
			Console.Write("Enter Your Choice = ");

			var result = Console.ReadLine();
			return Convert.ToInt32(result);
		}
		public void Start()
		{
			bool endApp = false;
			int userInput = DisplayMenu();

			while (!endApp)
			{
				switch (userInput)
				{
					case 0:
						Console.Clear();
						break;
					case 1:
						InputIsPrime();
						break;
					case 2:
						PrintPrime();
						break;
					case 3:
						PrintFibonaciNumber();
						break;
					case 4:
						PrintFibonaciNumber();
						break;
					case 5:
						FactorialNumber();
						break;
					case 6:
						PrintFactorilNumber();
						break;
					case 20:
						koa.ChecStringkPalindrome();
						break;
					case 21:
						koa.CheckNumberPalindrome();
						break;
					case 22:
						koa.PrintTriangleUsingFor();
						break;
					case 23:
						koa.PrintTriangleUsingWhile();
						break;

				}

				Console.Write("Are you want try again ? [y/n] ");
				string answer = Console.ReadLine();
				if (answer == "y" || answer == "Y")
				{
					Console.Clear();
					endApp = true;
					Start();
				}
				else
				{
					endApp = false;
					Environment.Exit(0);
				}
			}

			Console.WriteLine("------------------------\n");
			Console.WriteLine("\n");
		}

		public bool askBool(string question)
		{
			bool boolToReturn = false;
			Console.Write(question);
			while (true)
			{
				string ans = Console.ReadLine();
				if (ans != null && ans == "y")
				{
					boolToReturn = true;
					break;
				}
				else if (ans != null && ans == "n")
				{
					boolToReturn = false;
					break;
				}
				else
				{
					Console.Write("Only y or n Allowed");
				}
			}
			return boolToReturn;
		}

		public int askInt(string question)
		{
			int intToReturn = 0;
			Console.Write(question);
			while (true)
			{
				string ans = Console.ReadLine();
				if (int.TryParse(ans, out intToReturn))
					break;
				else
					Console.Write("Only number Allowed");
			}
			return intToReturn;
		}
	}
}
