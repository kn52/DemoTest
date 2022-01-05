using DemoTest.DemoS;
using Newtonsoft.Json;
using System;

namespace DemoTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Models.DemoTest demoTest = new Models.DemoTest() 
            { 
                demo0 = "0",
                demo1 = "1",
                demo2 = "2",
                demo3 = "3"
            };
            var sk = new { ab = demoTest, bd = "aashish" };
            var k = JsonConvert.SerializeObject(sk);

            //================Methods================
            Console.WriteLine("===============OPS===============");
            LinqOps.Operations();

            //BlobStorageOperations.GetFromBlobStorageContainer();
            //BlobStorageOperations.UploadToBlobStorageContainer();

            //SendMailWithMailKit.SendEmailProcess();

            //SendEmail.SendEmailProcess("");

            //HtmlWithXmlFile.FromHtmlWithXmlFile();

            //HtmlWIthTextFile.FormHtmlWithFile();

            //HtmlFormation.FormHtml();

            //PercentEncode.Signature();

            //GenerateGuid.GenerateUniqueGuid();

            //FunctionReturn.getRandomNumber();

            //Sha512Generator.SHA512HashGenerator();
        }
    }
}
