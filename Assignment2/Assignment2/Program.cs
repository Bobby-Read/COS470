using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;


// Having trouble doing the config and test files, I may need to come see you to go over doing that again.
namespace Assignment2
{
    class Program
    {
        public static Dictionary<char, int> cents = new Dictionary<char, int>()
    {{'a',1},{'b',2},{'c',3},{'d',4},{'e',5},{'f',6},{'g',7},{'h',8},{'i',9},{'j',10},{'k',11},{'l',12},{'m',13},{'n',14},
     {'o',15},{'p',16},{'q',17},{'r',18},{'s',19},{'t',20},{'u',21},{'v',22},{'w',23},{'x',24},{'y',25},{'z',26}};

        static void Main(string[] args)
        {
            AddressList results = DeserializeJson();
            List<Feature> addresses = results.features;
            DollarAddress(addresses);

        }

        public static string Webrequest()
        {

            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["where"] = "MUNICIPALITY='South Berwick'";
            parameters["outfields"] = "ADDRESS_NUMBER,STREETNAME,SUFFIX";
            parameters["f"] = "pjson";

            var address = @"https://gis.maine.gov/arcgis/rest/services/Location/Maine_E911_Addresses_Roads_PSAP/MapServer/1/query?" + parameters;


            using (var client = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, address))
                {

                    var response = client.SendAsync(request).Result;
                    var content = response.Content.ReadAsStringAsync().Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"{content}: {response.StatusCode}");
                    }
                    return content;
                }
            }
        }

        public static AddressList DeserializeJson()
        {
            return JsonConvert.DeserializeObject<AddressList>(Webrequest());
        }

        public static void DollarAddress(List<Feature> list)
        {
            Console.WriteLine("The following are the dollar addresses in South Berwick");
            foreach (var f in list)
            {

                string street = f.attributes.STREETNAME + " " + f.attributes.SUFFIX;
                if (f.attributes.ADDRESS_NUMBER == WordScore(street))
                {
                    Console.WriteLine(f.attributes.ADDRESS_NUMBER + " " + street);
                }
            }

        }

        public static int WordScore(string word)
        {
            string lower = word.ToLower();
            lower.ToCharArray();
            int total = 0;
            for (int i = 0; i < word.Length; i++)
            {
                if (cents.ContainsKey(lower[i]))
                {
                    total = total + cents[lower[i]];
                }
            }
            return total;
        }

        public class Attribute
        {
            public int ADDRESS_NUMBER { get; set; }
            public string STREETNAME { get; set; }
            public string SUFFIX { get; set; }
            public string MUNICIPALITY { get; set; }
        }

        public class Feature
        {
            public Attribute attributes { get; set; }
        }

        public class AddressList
        {
            public List<Feature> features { get; set; }
        }
    }
}
