using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2test
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter number to find");
            int argument;
            Int32.TryParse(Console.ReadLine(), out argument);

            List<int> list = new List<int>();
            int j = 0;
            for (int i = 0; i < 50000000; i++, j += 10)
            {
                list.Add(j);
            };
            for (int i = 0; i < 50000000; i++, j += 1)
            {
                list.Add(j);
            }

            Console.WriteLine(Select(list, argument));
        }

        public static int Select<T>(IEnumerable<T> sequence, T key) where T : struct, IComparable<T>
        {
            MathProviderConstructors.Construct();

            var leftIndex = 0;
            var rightIndex = sequence.Count() - 1;
            int midIndex;

            while (sequence.ElementAt(leftIndex).CompareTo(key) == -1 && sequence.ElementAt(rightIndex).CompareTo(key) == 1)
            {
                midIndex = leftIndex +
                    (int)((MathProvider<T>.Subtract(key, sequence.ElementAt(leftIndex)) * (decimal)(rightIndex - leftIndex)) /
                                MathProvider<T>.Subtract(sequence.ElementAt(rightIndex), sequence.ElementAt(leftIndex)));

                Console.WriteLine("Pivot: {0}", midIndex);

                var midElement = sequence.ElementAt(midIndex);

                if (midElement.CompareTo(key) == -1)
                    leftIndex = midIndex + 1;
                else if (midElement.CompareTo(key) == 1)
                    rightIndex = midIndex - 1;
                else
                    return midIndex;
            }

            if (sequence.ElementAt(leftIndex).CompareTo(key) == 0)
                return leftIndex;
            else if (sequence.ElementAt(rightIndex).CompareTo(key) == 0)
                return rightIndex;
            else
                return -1;
        }
    }
}