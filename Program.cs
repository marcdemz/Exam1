using System;
using System.Net;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace WeatherIntegration
{
    class Program
    {
        static void Main(string[] args)
        {
            string city = "London"; // Change city here
            string apiKey = "54df5fa781322c89569296a12c2c787e"; // Replace with your API key

            // JSON Integration
            string jsonUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}";
            string jsonResponse = GetRequest(jsonUrl);
            WeatherData jsonData = JsonConvert.DeserializeObject<WeatherData>(jsonResponse);

            // XML Integration
            string xmlUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&mode=xml";
            string xmlResponse = GetRequest(xmlUrl);
            XElement xmlData = XElement.Parse(xmlResponse);

            // Display results
            Console.WriteLine("JSON Response:");
            Console.WriteLine(jsonResponse);
            Console.WriteLine("\nDeserialized JSON:");
            Console.WriteLine($"City: {jsonData.Name}");
            Console.WriteLine($"Temperature: {jsonData.Main.Temp}");
            Console.WriteLine($"Id: {jsonData.Id}");
            Console.WriteLine($"Timezone: {jsonData.Timezone}");
            Console.WriteLine($"COD: {jsonData.Cod}");


            Console.WriteLine("\nXML Response:");
            Console.WriteLine(xmlResponse);
            Console.WriteLine("\nParsed XML:");
            Console.WriteLine($"City: {xmlData.Element("city").Attribute("name").Value}");
            Console.WriteLine($"Temperature: {xmlData.Element("temperature").Attribute("value").Value}");
            Console.WriteLine($"Min: {xmlData.Element("temperature").Attribute("min").Value}");
            Console.WriteLine($"Max: {xmlData.Element("temperature").Attribute("value").Value}");
            Console.WriteLine($"Unit: {xmlData.Element("temperature").Attribute("unit").Value}");

            Console.ReadLine();
        }

        static string GetRequest(string url)
        {
            using (WebClient client = new WebClient())
            {
                return client.DownloadString(url);
            }
        }
    }

    public class WeatherData
    {
        public string Name { get; set; }
        public MainData Main { get; set; }
        public decimal Timezone { get; set; }
        public decimal Id { get; set; }
        public decimal Cod { get; set; }
    }

    public class MainData
    {
        public float Temp { get; set; }
    }
}
