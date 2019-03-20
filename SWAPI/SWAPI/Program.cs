using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Xml;

namespace SWAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Character>));
            WebClient client = new WebClient();
            int idOfCharacter;
            string urlAddress;
            bool checkCharacter = true;
            List<Character> SWAPI = new List<Character>();

            Console.WriteLine("Введите Id персонажа:");
            while (true)
            {
                urlAddress = "https://swapi.co/api/people/";
                if (int.TryParse(Console.ReadLine(), out idOfCharacter))
                {
                    urlAddress += idOfCharacter;
                    break;
                }
                else
                {
                    Console.WriteLine("Ошибка! Нужно ввести число.");
                }
            }
            if (checkCharacter)
            {
                using (StreamReader sr = new StreamReader("file.xml"))
                {
                    SWAPI = (List<Character>)formatter.Deserialize(sr);
                }
                foreach (Character x in SWAPI)
                {
                    if (x.Url.Substring(0, x.Url.Length - 1) == urlAddress)
                    {
                        Console.WriteLine(
                            $"{x.Name}\n" +
                            $"{x.Height}\n" +
                            $"{x.Mass}\n" +
                            $"{x.Hair_color}\n" +
                            $"{x.Skin_color}\n" +
                            $"{x.Eye_color}\n" +
                            $"{x.Birth_year}\n" +
                            $"{x.Gender}\n" +
                            $"{x.Homeworld}\n" +
                            $"{x.Films}\n" +
                            $"{x.Species}\n" +
                            $"{x.Vehicles}\n" +
                            $"{x.Starships}\n" +
                            $"{x.Created}\n" +
                            $"{x.Edited}\n" +
                            $"{x.Url}\n");
                        checkCharacter = false;
                        break;
                    }
                }
            }           
            if (checkCharacter)
            {
                var jsonChar = client.DownloadString(urlAddress);
                SWAPI.Add(JsonConvert.DeserializeObject<Character>(jsonChar));
                foreach (Character x in SWAPI)
                {
                    if (x.Url.Substring(0, x.Url.Length - 1) == urlAddress)
                    {
                        Console.WriteLine(
                            $"{x.Name}\n" +
                            $"{x.Height}\n" +
                            $"{x.Mass}\n" +
                            $"{x.Hair_color}\n" +
                            $"{x.Skin_color}\n" +
                            $"{x.Eye_color}\n" +
                            $"{x.Birth_year}\n" +
                            $"{x.Gender}\n" +
                            $"{x.Homeworld}\n" +
                            $"{x.Films}\n" +
                            $"{x.Species}\n" +
                            $"{x.Vehicles}\n" +
                            $"{x.Starships}\n" +
                            $"{x.Created}\n" +
                            $"{x.Edited}\n" +
                            $"{x.Url}\n");
                    }
                }
                using (StreamWriter sw = new StreamWriter("file.xml"))
                {
                    formatter.Serialize(sw, SWAPI, new XmlSerializerNamespaces(new XmlQualifiedName[] { new XmlQualifiedName(string.Empty) }));
                    Console.WriteLine("Объект сериализован");
                }
            }
            Console.Read();
        }
    }
}
