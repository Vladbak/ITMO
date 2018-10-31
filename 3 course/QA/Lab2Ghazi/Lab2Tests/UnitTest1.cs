using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System;

namespace Lab2Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ByteSort()
        {
            List<byte> bytelist = new List<byte>();
            for (int i=0; i < 100; i++)
            {
                for (byte b = 0; b < 255; b++)
                    bytelist.Add(b);
            }

            var actualResult = Lab2Ghazi.Program.Sort<byte>(bytelist).ToList();

            bytelist.Sort();
            var expectedResult = bytelist;
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ShortSort()
        {
            List<short> shortlist = new List<short>();
            for (int i = 0; i < 100; i++)
            {
                for (short b = 0; b < Int16.MaxValue; b++)
                    shortlist.Add(b);
            }

            var actualResult = Lab2Ghazi.Program.Sort<short>(shortlist).ToList();

            shortlist.Sort();
            var expectedResult = shortlist;
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void IntSort()
        {
            List<int> expectedResult = new List<int>();

            List<int> list = new List<int>();

            Random r = new Random();
            int Count = 10000;
            for (int i = 0; i < Count; i++)
            {
                list.Add(r.Next());
            }

            List<int> actualResult = Lab2Ghazi.Program.Sort<int>(list).ToList();

            list.Sort();
            expectedResult = list;

            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void LongSort()
        {
            List<long> expectedResult = new List<long>();

            List<long> list = new List<long>();

            Random r = new Random();
            int Count = 10000;
            for (int i = 0; i < Count; i++)
            {
                list.Add(r.Next() * 1000);
            }

            List<long> actualResult = Lab2Ghazi.Program.Sort<long>(list).ToList();

            list.Sort();
            expectedResult = list;

            CollectionAssert.AreEqual(expectedResult, actualResult);
        }
      
    }
}
