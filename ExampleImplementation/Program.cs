namespace ExampleImplementation
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using AuthenticatedEncryption;
    using Newtonsoft.Json;

    public class Program
    {
        public static void Main(string[] args)
        {
            var domain = "INSERT YOUR DOMAIN HERE";
            var cryptKeyString = "INSERT YOUR CRYPT KEY HERE";
            var authKeyString = "INSERT YOUR AUTH KEY HERE";
            var order = new Order
                            {
                                Email = "INSERT CONSUMER EMAIL ADDRESS",
                                Name = "INSERT CONSUMER NAME",
                                Ref = "INSERT ORDER REFERENCE NUMBER",
                                Skus = new List<string> { "INSERT SKUS HERE" },
                                Tags = new List<string> { "INSERT TAGS HERE" }
                            };
            var jsonEncodedOrder = JsonConvert.SerializeObject(order);
            var cryptKey = Convert.FromBase64String(cryptKeyString);
            var authKey = Convert.FromBase64String(authKeyString);

            var cipherText = AuthenticatedEncryption.Encrypt(jsonEncodedOrder, cryptKey, authKey);
            var urlEncodedCipherText = WebUtility.UrlEncode(cipherText);

            Console.WriteLine($"https://www.trustpilot.com/evaluate-bgl/{domain}?p={urlEncodedCipherText}");
        }

        public class Order
        {
            public string Email { get; set; }

            public string Name { get; set; }

            public string Ref { get; set; }

            public List<string> Skus { get; set; }

            public List<string> Tags { get; set; }
        }
    }
}
