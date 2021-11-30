using DemoTest.DemoS;
using Microsoft.CodeAnalysis;
using System;

namespace DemoTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //================Methods================
            Console.WriteLine("==============================");
            SendMailWithMailKit.SendEmailProcess();

            //Console.WriteLine("==============================");
            //SendEmail.SendEmailProcess("");

            //Console.WriteLine("==============================");
            //HtmlWithXmlFile.FromHtmlWithXmlFile();
            
            //Console.WriteLine("==============================");
            //HtmlWIthTextFile.FormHtmlWithFile();

            //Console.WriteLine("==============================");
            //HtmlFormation.FormHtml();

            //Console.WriteLine("==============================");
            //PercentEncode.Signature();

            //Console.WriteLine("==============================");
            //GenerateGuid.GenerateUniqueGuid();
            
            //Console.WriteLine("==============================");
            //FunctionReturn.getRandomNumber();
            
            //Console.WriteLine("==============================");
            //Sha512Generator.SHA512HashGenerator();
        }

    }
}
