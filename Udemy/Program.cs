using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy
{
	class Program
	{

		static void Main(string[] args)
		{
			//MenuOption menu = new MenuOption();
			//menu.Start();

			//string str = "12/5/2021 12:00:00 AM";

			string date = "05/12/2021";

			DateTime dateTime;
			string[] validformats = new[] { "MM/dd/yyyy", "yyyy/MM/dd", "MM/dd/yyyy HH:mm:ss",
										"MM/dd/yyyy hh:mm tt", "yyyy-MM-dd HH:mm:ss, fff","dd/MM/yyyy" };

			CultureInfo provider = CultureInfo.InvariantCulture;

			if (DateTime.TryParseExact(date, validformats, provider,
										DateTimeStyles.None, out dateTime))
			{
				Console.WriteLine("The specified date is valid: " + dateTime);
			}
			else
			{
				Console.WriteLine("Unable to parse the specified date");
			}

			//int userInput = 0;
			//while (userInput != 5)
			//{
			//	userInput = menu.DisplayMenu();
			//}
			//Mathematical math = new Mathematical();
			//Console.Write("Masukan Bilangan = ");
			//int n = int.Parse(Console.ReadLine());
			//bool isPrime = math.IsPrime(n);
			//if (isPrime)
			//	Console.Write(n + " Adalah Bilangan Prinma");
			//else
			//	Console.Write(n + " Bukan Bilangan Prinma");



			Console.ReadLine();
		}
	}
}
