using DemoTest.DemoS;
using Microsoft.CodeAnalysis;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace DemoTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //var hash = new Sha512Generator().SHA512HashGenerator();
            //int rdvalue = new FunctionReturn().getRandomNumber();
            //new HtmlWIthTextFile().FormHtmlWithFile();
            new HtmlWithXmlFile().FromHtmlWithXmlFile();
        }

    }
}
