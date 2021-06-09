using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace hackerrank.algorithms.easy
{
	class Program
	{


		static void Main(string[] args)
		{


			string nama = "ada";
			string result = "";

			for (int i = nama.Length - 1; i >= 0; i--)
			{
				result += nama[i];
			}

			if(nama == result)
			{
				Console.WriteLine("Ini Polindoreme");
			}
			else
			{
				Console.WriteLine("Bukan Polindoreme");
			}


			//string gelas1 = "TEH";
			//string gelas2 = "KOPI";

			//Console.WriteLine("gelas1 = " + gelas1);
			//Console.WriteLine("gelas2 = " + gelas2);


			//AlgorithmsEasy algo = new AlgorithmsEasy();

			//Console.WriteLine("1. solveMeFirst" +
			//Environment.NewLine + "2. simpleArraySum" +
			//Environment.NewLine + "3. simpleArraySum 3"
			//);
			//var ans = Console.ReadLine();
			//int choice = 0;
			//if (int.TryParse(ans, out choice))
			//{
			//	switch (choice)
			//	{
			//		case 1:
			//			Console.WriteLine(algo.solveMeFirst(4, 2));
			//			break;
			//		case 2:
			//			int[] arr = new int[6];
			//			arr[0] = 1;
			//			arr[1] = 2;
			//			arr[2] = 3;
			//			arr[3] = 4;
			//			arr[4] = 10;
			//			arr[5] = 11;
			//			Console.WriteLine(algo.SimpleArraySum1(arr));
			//			break;
			//		case 3:
			//			algo.SimpleArraySum3();
			//			break;

			//		default:
			//			Console.WriteLine("Wrong selection!!!" +
			//				Environment.NewLine + "Press any kay for exit");
			//			Console.ReadKey();
			//			break;
			//	}
			//}
			//else
			//{
			//	Console.WriteLine("You must type numeric value only!!!" +
			//		Environment.NewLine + "Press any kay for exit");
			//	Console.ReadKey();
			//}
		}

	}
}
