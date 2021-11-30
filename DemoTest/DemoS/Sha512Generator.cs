using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DemoTest.DemoS
{
    public class Sha512Generator
    {
        public static void SHA512HashGenerator()
        {
            string input = "Onlinecn6hiH" + "|" + decimal.Parse("469000.00").ToString("F4")
                + "|" + "Bidesi" + "|" + "bidesi@easyrewardz.com" + "||||||" + "OnlinehgGlFiR0";
            StringBuilder hash = new StringBuilder();
            SHA512 shaM = new SHA512Managed();
            byte[] bytes = shaM.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2").ToLower());
            }
            Console.WriteLine("Sha512 Generated: "+ hash.ToString());
        }
    }
}
