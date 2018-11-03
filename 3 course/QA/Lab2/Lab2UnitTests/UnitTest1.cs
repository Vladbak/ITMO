using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab2test;

namespace Lab2UnitTests
{
    [TestClass]
    public class SelectionTests
    {
        [TestMethod]
        public void FindByteElement()
        {
            int size = 500000000;
            byte[] list = new byte[size];
            int NumberOfIterations = (int)(size / 255) - 1;
            for (int i = 0; i < NumberOfIterations; i++)
            {
                for (byte b = 0; b < 255; b++)
                    list[i * 255 + b] = b;
            }
            Array.Sort(list);

            int actualResult = Program.Select<byte>(list, 0);

            Assert.IsTrue(actualResult >= 0 && actualResult < NumberOfIterations);
        }

        [TestMethod]
        public void FindShortElement()
        {
            short[] list = new short[500000000];
            int NumberOfIterations = 15000;
            for (int i = 0; i < NumberOfIterations; i++)
            {
                for (short s = 0; s < Int16.MaxValue; s++)
                    list[i * Int16.MaxValue + s] = s;
            }
            Array.Sort(list);

            int actualResult = Program.Select<short>(list, 0);

            Assert.IsTrue(actualResult >= 0 && actualResult < NumberOfIterations);
        }

        [TestMethod]
        public void FindIntElement()
        {
            int[] list = CreateIntArray();

            int expectedResult = 444444444;
            int actualResult = Program.Select<int>(list, list[expectedResult]);

            Assert.AreEqual(expectedResult, actualResult);
        }

        private int[] CreateIntArray()
        {
            int Million = 1000000;
            int NumberOfMilElements = 500;
            int NumberToSkip = 40;

            int[] list = new int[NumberOfMilElements * Million];
            int i = 0, j = 0;

            for (; i < Million * NumberToSkip; i++)
            {
                list[j++] = i;
            }
            i += Million * NumberToSkip;

            for (; i < Million * (NumberOfMilElements + NumberToSkip); i++)
            {
                list[j++] = i;
            }
            return list;
        }

        [TestMethod]
        public void FindLongElement()
        {
            int size = 100000000;
            long[] list = new long[size];

            for (long l = 0; l < size; l++)
            {
                list[l] = l;
            }

            int expectedResult = 999;
            int actualResult = Program.Select<long>(list, list[expectedResult]);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ElementNotInTheList()
        {
            int size = 500000000;
            int[] list = new int[size];
            int i = 0;
            int j = 0;
            int MissingValue = 500000;
            for (; i < MissingValue; i++)
            {
                list[j++] = i;
            };
            i++;
            for (; i < size; i++)
            {
                list[j++] = i;
            }

            int key = MissingValue;
            int expectedResult = -1;
            int actualResult = Program.Select<int>(list, key);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
