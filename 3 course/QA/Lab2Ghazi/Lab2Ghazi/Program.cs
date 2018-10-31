using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2Ghazi
{
    public class Program
    {
        static void Main(string[] args)
        {
        }

        public static IEnumerable<T> Sort<T>(IEnumerable<T> sequence) where T : IComparable<T>
        {
            T[] array = sequence.ToArray();
            List<T> buf1 = new List<T>();
            List<T> buf2 = new List<T>();

            do
            {
                buf1.Clear();
                buf2.Clear();

                int i = 0;
                int next = i + 1;
                // very complicated logic, DO NOT INTERFERE!
                while (i < array.Length - 1 && next < array.Length)
                {
                    if (array[i].CompareTo(array[next]) < 1) // less or equal
                    {
                        buf1.Add(array[i]);
                        i = next;
                        next++;
                    }
                    else
                    {
                        buf2.Add(array[next]);
                        next++;
                    }
                }

                buf1.Add(array[i]);
                // end of complicated logic

                merge<T>(buf1.ToArray(), buf2.ToArray()).CopyTo(array, 0);
            } while (buf2.Count > 0);

            return array;
        }

        private static T[] merge<T>(T[] a, T[] b) where T : IComparable<T>
        {
            T[] result = new T[a.Length + b.Length];
            int i = 0, j = 0, k = 0;
            while (i < a.Length && j < b.Length)
            {
                if (a[i].CompareTo(b[j]) == -1)
                {
                    result[k] = a[i];
                    i++; k++;
                }
                else
                {
                    result[k] = b[j];
                    j++; k++;
                }
            };
            while (i < a.Length)
            { 
                // переписываем оставшиеся элементы первого пути (если второй кончился раньше)
                result[k] = a[i];
                i++; k++;
            };
            while (j < b.Length)
            {  // переписываем оставшиеся элементы второго пути (если первый кончился раньше)
                result[k] = b[j];
                j++; k++;
            };

            return result;
        }
    }
}
