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
            XmlSerializer formatter = new XmlSerializer(typeof(Character));
            WebClient client = new WebClient();
            int idOfCharacter;
            string urlAddress;
            bool checkCharacter = false;
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
            XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
            xmlReaderSettings.ConformanceLevel = ConformanceLevel.Fragment;
            using (XmlReader xr = XmlReader.Create("file.xml", xmlReaderSettings))
            {
                while (xr.Read())
                {
                    var newCharacter = (Character)formatter.Deserialize(xr);
                    if (newCharacter.Url.Substring(0, newCharacter.Url.Length - 1) == urlAddress)
                    {
                        Console.WriteLine(
                            $"{newCharacter.Name}\n" +
                            $"{newCharacter.Height}\n" +
                            $"{newCharacter.Mass}\n" +
                            $"{newCharacter.Hair_color}\n" +
                            $"{newCharacter.Skin_color}\n" +
                            $"{newCharacter.Eye_color}\n" +
                            $"{newCharacter.Birth_year}\n" +
                            $"{newCharacter.Gender}\n" +
                            $"{newCharacter.Homeworld}\n" +
                            $"{newCharacter.Films}\n" +
                            $"{newCharacter.Species}\n" +
                            $"{newCharacter.Vehicles}\n" +
                            $"{newCharacter.Starships}\n" +
                            $"{newCharacter.Created}\n" +
                            $"{newCharacter.Edited}\n" +
                            $"{newCharacter.Url}\n");
                        checkCharacter = true;
                    break;
                    }
                }
            }
            if (checkCharacter == false)
            {
                var jsonChar = client.DownloadString(urlAddress);
                var rez = (JsonConvert.DeserializeObject<Character>(jsonChar));
                Console.WriteLine(
                            $"{rez.Name}\n" +
                            $"{rez.Height}\n" +
                            $"{rez.Mass}\n" +
                            $"{rez.Hair_color}\n" +
                            $"{rez.Skin_color}\n" +
                            $"{rez.Eye_color}\n" +
                            $"{rez.Birth_year}\n" +
                            $"{rez.Gender}\n" +
                            $"{rez.Homeworld}\n" +
                            $"{rez.Films}\n" +
                            $"{rez.Species}\n" +
                            $"{rez.Vehicles}\n" +
                            $"{rez.Starships}\n" +
                            $"{rez.Created}\n" +
                            $"{rez.Edited}\n" +
                            $"{rez.Url}\n");
                using (StreamWriter sw = new StreamWriter("file.xml"))
                {
                    formatter.Serialize(sw, rez, new XmlSerializerNamespaces(new XmlQualifiedName[] { new XmlQualifiedName(string.Empty) }));
                    Console.WriteLine("Объект сериализован");
                }
            }
            Console.Read();
        }
    }
}
