using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoTest.DemoModel
{
    public class GenerateGuid
    {
        public static void GenerateUniqueGuid()
        {
            Guid guid = Guid.NewGuid();
            var gud = Convert.ToString(guid);
            Console.WriteLine("Guid number" + guid);
        }
    }
}
