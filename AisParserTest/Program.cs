using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AisParser;
using AisParser.Messages;
using System.Threading.Tasks;

namespace AisParserTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string teststring = "!AIVDM,1,1,,A,13n@oD0PB@0IRqvQj@W;EppH088t19uvPT,0*3E";
            Parser parser = new Parser();
            var message = parser.Parse(teststring) as PositionReportClassAMessage;
            Console.WriteLine(message.MessageType);
            Console.WriteLine($"Repeat = {message.Repeat}");
            Console.WriteLine($"MMSI = {message.Mmsi}");
            Console.WriteLine($"NaviSTatus = {message.NavigationStatus}");
            Console.WriteLine($"RateOfTurn = {message.RateOfTurn}");
            Console.WriteLine($"Speed over ground = {message.SpeedOverGround}");
            Console.WriteLine($"Position accuracy = {message.PositionAccuracy}");
            Console.WriteLine($"Longtitude = {message.Longitude}");
            Console.WriteLine($"Latitude = {message.Latitude}");
            Console.WriteLine($"Course Over Ground = {message.CourseOverGround}");
            Console.WriteLine($"True Heading = {message.TrueHeading}");
            Console.WriteLine($"Timestamp = {message.Timestamp}");
            Console.WriteLine($"Maneuver Indicator = {message.ManeuverIndicator}");
            Console.WriteLine($"Spare = {message.Spare}");
            Console.WriteLine($"Raim = {message.Raim}");
            Console.WriteLine($"RadioStatus = {message.RadioStatus}");
            Console.ReadKey();
        }
    }
}
