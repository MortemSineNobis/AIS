using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using AisParser;
using AisParser.Messages;


namespace AIStoEarth
{
    class Program
    {
        static string path = @"C:\Users\bes-s\Desktop\testout";
        static string pathAIS = @"C:\Users\bes-s\Desktop\test";
        static void Main(string[] args)
        {
            if (!File.Exists(path))
                File.Create(path);
            if (!File.Exists(path))
            {
                Console.WriteLine("Файл AIS не найден!");
                Console.ReadKey();
                return;
            }
            List<string> dots = new List<string>();
            foreach (var item in File.ReadAllLines("start"))
                dots.Add(item);
            
            var AISitems = File.ReadAllLines(pathAIS);
            Parser parser = new Parser();
            foreach (var item in AISitems)
            {
                var message = parser.Parse(item) as PositionReportClassAMessage;
                if (message!=null)
                {
                    dots.Add("\t<Placemark>\n" +
                        "\t\t<styleUrl>#__managed_style_05053E8DAD1B117D28BB</styleUrl>\n"+
                        "\t\t\t<Point>\n" +
                        $"\t\t\t\t<coordinates> {message.Longitude}, {message.Latitude}, 0 </ coordinates >\n" +
                        "\t\t\t</Point>\n" +
                        "\t</Placemarks>");
                }

            }
            dots.Add("</Document>");
            dots.Add("</kml>");
            File.WriteAllLines(path, dots);
        }
    }
}
