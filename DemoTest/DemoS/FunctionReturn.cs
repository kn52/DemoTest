using System;

namespace DemoTest.DemoS
{
    public class FunctionReturn
    {
        public static void getRandomNumber()
        {
            var callfunc = getRandomMethod();
            var num = callfunc();
            Console.WriteLine("Random Number: " + num);
        }

        public static Func<int> getRandomMethod()
        {
            return GenerateRandomNumber;
        }

        public static int GenerateRandomNumber()
        {
            Random rd = new Random();
            return rd.Next(1, 100);
        }
    }
}
