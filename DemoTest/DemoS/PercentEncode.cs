using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DemoTest.DemoS
{
    public class PercentEncode
    {
        public static void Signature()
        {
            //string url = "https://sandbox.woohoo.in/rest/v3/catalog/categories";
            //string url = "https://sandbox.woohoo.in/rest/v3/catalog/categories/121";
            //string url = "https://sandbox.woohoo.in/rest/v3/catalog/categories/121/products";
            //string url = "https://sandbox.woohoo.in/rest/v3/catalog/categories/121/products?limit=500&offset=0";
            //string url = "https://sandbox.woohoo.in/rest/v3/catalog/categories/217/products?limit=500&offset=0";            //string url = "https://sandbox.woohoo.in/rest/v3/catalog/categories/217/products?limit=500&offset=0";
            string url = "https://sandbox.woohoo.in/rest/v3/orders";
            string body = "{" +
                  "address" + ": " + "{" +
                    "billToThis" + ": " + "true," +
                    "city" + ": " + "bangalore" + "," +
                    "country" + ": " + "IN" + "," +
                    "email" + ": " + "jhon.deo@gmail.com" + "," +
                    "firstname" + ": " + "Jhon" + "," +
                    "lastname" + ": " + "Deo" + "," +
                    "line1" + ": " + "address details1" + "," +
                    "line2" + ": " + "address details 2" + "," +
                    "postcode" + ": " + "560076" + "," +
                    "region" + ": " + "Karnataka" + "," +
                    "telephone" + ": " + "+919999999999" +
                  "}," +
                  "billing" + ": " + "{" +
                    "city" + ": " + "bangalore" + "," +
                    "country" + ": " + "IN" + "," +
                    "email" + ": " + "jhon.deo@gmail.com" + "," +
                    "firstname" + ": " + "Jhon " + "," +
                    "lastname" + ": " + "Deo" + "," +
                    "line1" + ": " + "address details1" + "," +
                    "line2" + ": " + "address details 2" + "," +
                    "postcode" + ": " + "560076" + "," +
                    "region" + ": " + "Karnataka" + "," +
                    "telephone" + ": " + "+919999999999"
                  + "}," +
                  "couponCode" + ": " + "DISC100" + "," +
                  "deliveryMode" + ": " + "API" + "," +
                  "payments" + ": " + "[" +
                    "{" +
                      "amount" + ": " + "1000," +
                      "code" + ": " + "svc" +
                    "}" +
                    "]," +
                  "products" + ": " + "["+
                    "{"+
                      "currency" + ": " + "356," +
                      "giftMessage" + ": " + "" + "," +
                      "price" + ": " + "1000," +
                      "qty" + ": " + "1," +
                      "sku" + ": " + "EGVGBTNS001" + "," +
                      "theme" + ": " + "bwi" +
                    "}" +
                    "]," +
                  "refno" + ": " + "001000000abc" + "," +
                  "syncOnly" + ": false"+
                "}";

            string secret = "4b0da6877c81fe75451feafe37828baa";
            string encodeUrl = Uri.EscapeDataString(url);
            string encodebody = "%7B%0A%20%20%22Address%22%3A%20%7B%0A%20%20%20%20%22billToThis%22%3A%20true%2C%0A%20%20%20%20%22city%22%3A%20%22bangalore%22%2C%0A%20%20%20%20%22country%22%3A%20%22IN%22%2C%0A%20%20%20%20%22email%22%3A%20%22bidesisahoo1%40gmail.com%22%2C%0A%20%20%20%20%22firstname%22%3A%20%22Bidesi%22%2C%0A%20%20%20%20%22lastname%22%3A%20%22Sahoo%22%2C%0A%20%20%20%20%22line1%22%3A%20%22address%20details1%22%2C%0A%20%20%20%20%22line2%22%3A%20%22address%20details%202%22%2C%0A%20%20%20%20%22postcode%22%3A%20%22560076%22%2C%0A%20%20%20%20%22region%22%3A%20%22Karnataka%22%2C%0A%20%20%20%20%22telephone%22%3A%20%22%2B917377969796%22%0A%20%20%7D%2C%0A%20%20%22Billing%22%3A%20%7B%0A%20%20%20%20%22city%22%3A%20%22bangalore%22%2C%0A%20%20%20%20%22country%22%3A%20%22IN%22%2C%0A%20%20%20%20%22email%22%3A%20%22bidesisahoo1%40gmail.com%22%2C%0A%20%20%20%20%22firstname%22%3A%20%22Bidesi%22%2C%0A%20%20%20%20%22lastname%22%3A%20%22Sahoo%22%2C%0A%20%20%20%20%22line1%22%3A%20%22address%20details1%22%2C%0A%20%20%20%20%22line2%22%3A%20%22address%20details%202%22%2C%0A%20%20%20%20%22postcode%22%3A%20%22560076%22%2C%0A%20%20%20%20%22region%22%3A%20%22Karnataka%22%2C%0A%20%20%20%20%22telephone%22%3A%20%22%2B917377969796%22%0A%20%20%7D%2C%0A%20%20%22couponCode%22%3A%20null%2C%0A%20%20%22deliveryMode%22%3A%20%22API%22%2C%0A%20%20%22Payments%22%3A%20%7B%0A%20%20%20%20%22amount%22%3A%201000%2C%0A%20%20%20%20%22code%22%3A%20%22svc%22%0A%20%20%7D%2C%0A%20%20%22Products%22%3A%20%7B%0A%20%20%20%20%22currency%22%3A%20356%2C%0A%20%20%20%20%22giftMessage%22%3A%20null%2C%0A%20%20%20%20%22price%22%3A%201000%2C%0A%20%20%20%20%22qty%22%3A%201%2C%0A%20%20%20%20%22sku%22%3A%20%22UBEFLOW%22%2C%0A%20%20%20%20%22theme%22%3A%20null%0A%20%20%7D%2C%0A%20%20%22refno%22%3A%20%22TXNTEST1%22%2C%0A%20%20%22syncOnly%22%3A%20true%0A%7D";

            //string getmethod = "GET";
            string postmethod = "POST";
            
            //var data = postmethod + "&" + encodeUrl;
            var data = postmethod + "&" + encodeUrl + "&" + encodebody;

            var hash = new StringBuilder();

            byte[] secretkeyBytes = Encoding.UTF8.GetBytes(secret);
            byte[] inputBytes = Encoding.UTF8.GetBytes(data);

            using (var hmac = new HMACSHA512(secretkeyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            Console.WriteLine(hash);
        }
    }
}
