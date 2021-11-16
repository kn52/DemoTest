using System;
using System.Collections.Generic;
using System.Text;

namespace DemoTest.DemoS
{
    public class FunctionReturn
    {
        public int getRandomNumber()
        {
            var callfunc = getRandomMethod();
            return callfunc();
        }

        public Func<int> getRandomMethod()
        {
            return GenerateRandomNumber;
        }

        public int GenerateRandomNumber()
        {
            Random rd = new Random();
            return rd.Next(1, 100);
        }
    }
}
