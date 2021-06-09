using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy
{
	public class Mathematical : Sorting
	{
		#region Prime
		//01
		public bool IsPrime(int n)
		{
			if (n <= 1)
				return false;

			for (int i = 2; i < n; i++)
			{
				if (n % i == 0)
					return false;
			}

			return true;
		}


		public void InputIsPrime()
		{
			Console.Write("Enter Your Number = ");
			int n = int.Parse(Console.ReadLine());
			bool isPrime = IsPrime(n);
			if (isPrime)
				Console.WriteLine(n + " Is Prime Number");
			else
				Console.WriteLine(n + " Is Not Prime Number");

			Console.ReadLine();
		}

		bool CheckIsPrime(int n)
		{
			for (int i = 2; i < n; i++)
			{
				if (n % i == 0)
				{
					return false;
				}
			}
			return true;
		}

		//02

		public void PrintPrime()
		{
			Console.Write("Enter Your Number Maximum = ");
			int n = int.Parse(Console.ReadLine());
			int counter = 7;
			if (n > 8)
			{
				if (n % 2 == 0)
					counter = n - 3;
				else
					counter = n - 2;
			}
			else
				counter = n - 1;
			

			for (int i = 2; i <= n; i++)
			{
				if (CheckIsPrime(i))
				{
					Console.Write(i + (i < counter ? "," : " "));
				}
			}

			Console.ReadLine();
		}



		#endregion

		#region Fibonaci


		//03
		public void PrintFibonaciNumber()
		{
			Console.Write("Enter Your Number Maximum = ");
			int a = 0;
			int b = 1;
			int c = 0;

			int n = int.Parse(Console.ReadLine());
			Console.Write("{0}{1}", a,b);
			//0,1,1,2,3,5,8,13,21,34

			for (int i = 2; i < n; i++)
			{
				c = a + b;
				Console.Write(c); // 1,
				a = b; //1
				b= c;//1
			}
			Console.ReadLine();
		}


		#endregion

		#region Factorial

		//05
		public void FactorialNumber()
		{
			int factorial = 1;
			Console.Write("Enter a Number = ");
			int n = int.Parse(Console.ReadLine());
			for (int i = 1; i <= n; i++)
			{
				factorial = factorial * i;
			}
			Console.Write($"Factorial of {n}  is: {factorial}");
			Console.ReadLine();
		}

		//06
		public void PrintFactorilNumber()
		{
			int factorial = 1;
			Console.Write("Enter a Number = ");
			int n = int.Parse(Console.ReadLine());
			int x = n;
			string deret = string.Empty;
			while (n !=1)
			{
				factorial = factorial * n;
				n--;
				deret += "*" + n;
			}

			Console.Write(x + "" + deret + $" Factorial of {x}  is: {factorial}");
			//Console.Write($"Factorial of {x}  is: {factorial}");
			Console.ReadLine();
		}


		#endregion

		#region Percent

		public void CalculatePercentage()
		{
			Console.Write("1. Masukan Jumlah Siswa Berkacamata = ");
			int studentKacamata = int.Parse(Console.ReadLine());

			Console.Write("2. Masukan Jumlah Siswa Yang Tidak Berkacamata = ");
			int studentNotKacamata = int.Parse(Console.ReadLine());

			var option = Console.ReadLine();
		}

		#endregion

	}
}
