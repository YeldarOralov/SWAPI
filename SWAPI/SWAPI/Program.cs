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

namespace SWAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Character), );
            WebClient client = new WebClient();
            int idOfCharacter;
            string urlAddress;
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
                    Console.WriteLine("Ошибка! Нужно ввести число от 1 до 87");
                }
            }
            //using (FileStream fs = new FileStream("file.xml", FileMode.Open))
            //{
            //    Character newCharacter = (Character)formatter.Deserialize(fs);
            //    if (newCharacter.Url.Substring(0, newCharacter.Url.Length - 1) == urlAddress)
            //    {
            //        Console.WriteLine(
            //            $"{newCharacter.Name}\n" +
            //            $"{newCharacter.Height}\n" +
            //            $"{newCharacter.Mass}\n" +
            //            $"{newCharacter.Hair_color}\n" +
            //            $"{newCharacter.Skin_color}\n" +
            //            $"{newCharacter.Eye_color}\n" +
            //            $"{newCharacter.Birth_year}\n" +
            //            $"{newCharacter.Gender}\n" +
            //            $"{newCharacter.Homeworld}\n" +
            //            $"{newCharacter.Films}\n" +
            //            $"{newCharacter.Species}\n" +
            //            $"{newCharacter.Vehicles}\n" +
            //            $"{newCharacter.Starships}\n" +
            //            $"{newCharacter.Created}\n" +
            //            $"{newCharacter.Edited}\n" +
            //            $"{newCharacter.Url}\n");
            //    }
            //}





            var jsonChar = client.DownloadString(urlAddress);
            var rez = JsonConvert.DeserializeObject<Character>(jsonChar);
            using (FileStream fs = new FileStream("file.xml", FileMode.Append))
            {
                formatter.Serialize(fs, rez, new Xml rez.Url.Substring(rez.Url.Length-2, 1));

                Console.WriteLine("Объект сериализован");
            }


            Console.Read();
        }
    }
}
