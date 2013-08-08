using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FizzBuzzTest
{

    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void FizzBuzz()
        {
            const int min = 1;
            const int max = 100;
            IList<string> result = new List<string>();

            for (int i = min; i <= max; i++)
            {
                bool multipleOf3 = i % 3 == 0;
                bool multipleOf5 = i % 5 == 0;

                string output = "";

                if (multipleOf3)
                {
                    output += "Fizz";
                }

                if (multipleOf5)
                {
                    output += "Buzz";
                }

                if (!multipleOf3 && !multipleOf5)
                {
                    output = i.ToString();
                }

                result.Add(output);
            }

            Assert.AreEqual(result.Count, 100);

        }
    }

}
