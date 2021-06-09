using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackerrank.algorithms.easy
{
    public class AlgorithmsEasy
    {

        public int solveMeFirst(int a, int b)
        {
            return a+b;  
        }

        public int SimpleArraySum1(int[] arrayParam)
        {
            int total = 0;
            if (arrayParam.Length > 0)
            {
                for (int i = 0; i < arrayParam.Length; i++)
                {
                    total = total + arrayParam[i];
                }
            }
            return total;
        }

        public int SimpleArraySum2(int[] arrayParam)
        {
            int total = 0;
            if (arrayParam.Length > 0)
            {
                foreach (var item in arrayParam)
                {
                    total = total + item;
                }
            }
            return total;
        }

        public void SimpleArraySum3()
        {
            int n = int.Parse(Console.ReadLine());
            int[] array = new int[n];
            int sumOfArray = 0;

            for (int i = 0; i < n; i++)
            {
                array[i] = int.Parse(Console.ReadLine());
                sumOfArray += array[i];
            }

            Console.Write(sumOfArray);

        }

        public List<int> compareTriplets(List<int> a, List<int> b)
        {
            int pointA = 0;
            int pointB = 0;

            int[] result = new int[2];

            for (int i = 0; i < a.Count; i++)
            {
                if(a[i] != b[i])
                {
                    if (a[i] > b[i])
                    {
                        pointA = pointA + 1;
                    }
                    else
                    {
                        pointB = pointB + 1;
                    }
                }
            }

            result[0] = pointA;
            result[1] = pointB;
            return result.ToList();
        }

    }
}
