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
            List<byte> list = new List<byte>();
            for (int i = 0; i < 10000; i++)
            {
                for (byte b = 0; b < 255; b++)
                    list.Add(b);
            }
            list.Sort();

            int actualResult = Program.Select<byte>(list, 0);

            Assert.IsTrue(actualResult >= 0 && actualResult < 10000);
        }

        [TestMethod]
        public void FindShortElement()
        {
            List<short> list = new List<short>();
            for (int i = 0; i < 1000; i++)
            {
                for (short s = 0; s < Int16.MaxValue; s++)
                    list.Add(s);
            }
            list.Sort();

            int actualResult = Program.Select<short>(list, 0);

            Assert.IsTrue(actualResult >= 0 && actualResult < 1000);
        }

        [TestMethod]
        public void FindIntElement()
        {
            List<int> list = new List<int>();
            for (int i = 0; i < 10000000; i++)
            {
                list.Add(i);
            }

            int key = 999;
            int expectedResult = key;
            int actualResult = Program.Select<int>(list, key);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void FindLongElement()
        {
            List<long> list = new List<long>();
            for (long l = 0; l < 1000000; l++)
            {
                list.Add(l);
            }

            int key = 999;
            int expectedResult = key;
            int actualResult = Program.Select<long>(list, key);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ElementNotInTheList()
        {
            List<int> list = new List<int>();
            int i = 0;
            int MissingValue = 500000;
            for (; i < MissingValue; i++)
            {
                list.Add(i);
            };
            i++;
            for (; i < 1000000; i++)
            {
                list.Add(i);
            }

            int key = MissingValue;
            int expectedResult = -1;
            int actualResult = Program.Select<int>(list, key);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
