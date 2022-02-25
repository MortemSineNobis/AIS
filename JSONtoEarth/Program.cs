using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONtoEarth
{
    class Program
    {
        static string path = @"C:\Users\bes-s\Desktop\testout2";
        static string pathJSON = @"C:\Users\bes-s\Desktop\AIS_out.txt";
        static void Main(string[] args)
        {
            double lon = double.NaN;
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
            using (StreamReader reader = new StreamReader(pathJSON))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("lon\""))
                    {
                        lon = Convert.ToDouble(line.Substring(line.IndexOf(':') + 1).TrimStart(' ').TrimEnd(',').Replace('.',','));
                    }
                    if (line.Contains("lat\"") && !double.IsNaN(lon))
                    {
                        double lat = Convert.ToDouble(line.Substring(line.IndexOf(':') + 1).TrimStart(' ').TrimEnd(',').Replace('.', ','));
                        dots.Add("\t<Placemark>\n" +
                        "\t\t<styleUrl>#__managed_style_05053E8DAD1B117D28BB</styleUrl>\n" +
                        "\t\t<Point>\n" +
                        $"\t\t\t<coordinates>{lon.ToString().Replace(',','.')},{lat.ToString().Replace(',', '.')},0</coordinates>\n" +
                        "\t\t</Point>\n" +
                        "\t</Placemark>");
                        lon = double.NaN;
                    }
                }
            }
            dots.Add("</Document>");
            dots.Add("</kml>");
            File.WriteAllLines(path, dots);
        }
    }
}
