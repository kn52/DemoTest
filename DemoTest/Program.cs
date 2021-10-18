using DemoTest.DemoModel;
using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace DemoTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //GenerateGuid.GenerateUniqueGuid();
            //PercentEncode.Signature();
            //HtmlFormation.FormHtml();
            //string d1 = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            //string tp = "10-08-2021 00:00:00";
            //CultureInfo culture = new CultureInfo("en-US");
            //DateTime tempDate = Convert.ToDateTime(tp, culture);
            //DateTime d2 = DateTime.Parse(DateTime.Parse(d1).ToString("MM-dd-yyyy"));
            //DateTime d3 = DateTime.Parse(DateTime.Parse(d1).ToString("dd-MM-yyyy"));
            //var diff = (tempDate - d2).Days;
            //var diff2 = (tempDate - d3).Days;

            var hash = new Program().SHA512HashGenerator();
            Console.WriteLine(hash);
            Console.WriteLine();
        }

        public string SHA512HashGenerator()
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
            return hash.ToString();
        }
    }
}
