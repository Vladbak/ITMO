using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        private IEnumerable<T> Select<T>(IEnumerable<T> sequence, T key) where T : struct, IComparable<T>
        {
            Sub;

            if (typeof(T) == typeof(byte))
            {
            }

            if (typeof(T) == typeof(short))
            {
            }

            if (typeof(T) == typeof(int))
            {
            }

            if (typeof(T) == typeof(long))
            {
            }

            var left = 0;
            var right = sequence.Count() - 1;
            int mid;

            while (sequence.ElementAt(left).CompareTo(key) == -1 && sequence.ElementAt(right).CompareTo(key) == 1)
            {
            }
        }

        public T Add<T>(T a, T v)
        {
        }

        private delegate T Subtract<T>(T a, T b);

        private delegate T Multiply<T>(T a, T b);

        private delegate T Divide<T>(T a, T b);
    }
}