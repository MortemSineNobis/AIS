using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIStoEarth
{
    public class Nmea
    {
        public int ais_id { get; set; }
        public string raw { get; set; }
        public string talker { get; set; }
        public string type { get; set; }
        public int message_fragments { get; set; }
        public int fragment_number { get; set; }
        public object message_id { get; set; }
        public string channel { get; set; }
        public string payload { get; set; }
        public int fill_bits { get; set; }
        public int checksum { get; set; }
        public string bit_array { get; set; }
    }

    public class Decoded
    {
        public int type { get; set; }
        public int repeat { get; set; }
        public string mmsi { get; set; }
    }

    public class Decoded1 : Decoded
    {
        public int type { get; set; }
        public int repeat { get; set; }
        public string mmsi { get; set; }
        public int status { get; set; }
        public int turn { get; set; }
        public double speed { get; set; }
        public int accuracy { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
        public double course { get; set; }
        public int heading { get; set; }
        public int second { get; set; }
        public int maneuver { get; set; }
        public int raim { get; set; }
        public int radio { get; set; }
    }

    public class Decoded2 : Decoded
    {
        public int type { get; set; }
        public int repeat { get; set; }
        public string mmsi { get; set; }
        public int status { get; set; }
        public int turn { get; set; }
        public double speed { get; set; }
        public int accuracy { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
        public double course { get; set; }
        public int heading { get; set; }
        public int second { get; set; }
        public int maneuver { get; set; }
        public int raim { get; set; }
        public int radio { get; set; }
    }

    public class Decoded3 : Decoded
    {
        public int type { get; set; }
        public int repeat { get; set; }
        public string mmsi { get; set; }
        public int status { get; set; }
        public int turn { get; set; }
        public double speed { get; set; }
        public int accuracy { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
        public double course { get; set; }
        public int heading { get; set; }
        public int second { get; set; }
        public int maneuver { get; set; }
        public int raim { get; set; }
        public int radio { get; set; }
    }

    public class Decoded4 : Decoded
    {
        public int type { get; set; }
        public int repeat { get; set; }
        public string mmsi { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public int hour { get; set; }
        public int minute { get; set; }
        public int second { get; set; }
        public int accuracy { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
        public int epfd { get; set; }
        public int raim { get; set; }
        public int radio { get; set; }
    }

    public class Decoded5 : Decoded
    {
        public int type { get; set; }
        public int repeat { get; set; }
        public string mmsi { get; set; }
        public int ais_version { get; set; }
        public int imo { get; set; }
        public string callsign { get; set; }
        public string shipname { get; set; }
        public int shiptype { get; set; }
        public int to_bow { get; set; }
        public int to_stern { get; set; }
        public int to_port { get; set; }
        public int to_starboard { get; set; }
        public int epfd { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public int hour { get; set; }
        public int minute { get; set; }
        public double draught { get; set; }
        public string destination { get; set; }
        public int dte { get; set; }
    }

    public class Decoded6 : Decoded
    {
        public int type { get; set; }
        public int repeat { get; set; }
        public string mmsi { get; set; }
        public int seqno { get; set; }
        public string dest_mmsi { get; set; }
        public int retransmit { get; set; }
        public int dac { get; set; }
        public int fid { get; set; }
        public string data { get; set; }
    }

    public class Decoded8 : Decoded
    {
        public int type { get; set; }
        public int repeat { get; set; }
        public string mmsi { get; set; }
        public int dac { get; set; }
        public int fid { get; set; }
        public string data { get; set; }
    }

    public class Decoded9 : Decoded
    {
        public int type { get; set; }
        public int repeat { get; set; }
        public string mmsi { get; set; }
        public int alt { get; set; }
        public int speed { get; set; }
        public int accuracy { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
        public double course { get; set; }
        public int second { get; set; }
        public int dte { get; set; }
        public int assigned { get; set; }
        public int raim { get; set; }
        public int radio { get; set; }
    }

    public class Decoded10 : Decoded
    {
        public int type { get; set; }
        public int repeat { get; set; }
        public string mmsi { get; set; }
        public string dest_mmsi { get; set; }
    }

    public class Decoded11 : Decoded
    {
        public int type { get; set; }
        public int repeat { get; set; }
        public string mmsi { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public int hour { get; set; }
        public int minute { get; set; }
        public int second { get; set; }
        public int accuracy { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
        public int epfd { get; set; }
        public int raim { get; set; }
        public int radio { get; set; }
    }


    public class Decoded16 : Decoded
    {
        public int type { get; set; }
        public int repeat { get; set; }
        public string mmsi { get; set; }
        public string mmsi1 { get; set; }
        public int offset1 { get; set; }
        public int increment1 { get; set; }
        public string mmsi2 { get; set; }
        public int offset2 { get; set; }
        public int increment2 { get; set; }
    }

    public class Decoded17 : Decoded
    {
        public int type { get; set; }
        public int repeat { get; set; }
        public string mmsi { get; set; }
        public int lon { get; set; }
        public int lat { get; set; }
        public string data { get; set; }
    }

    public class Decoded18 : Decoded
    {
        public int type { get; set; }
        public int repeat { get; set; }
        public string mmsi { get; set; }
        public double speed { get; set; }
        public int accuracy { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
        public double course { get; set; }
        public int heading { get; set; }
        public int second { get; set; }
        public int regional { get; set; }
        public int cs { get; set; }
        public int display { get; set; }
        public int dsc { get; set; }
        public int band { get; set; }
        public int msg22 { get; set; }
        public int assigned { get; set; }
        public int raim { get; set; }
        public int radio { get; set; }
    }

    public class Decoded19 : Decoded
    {
        public int type { get; set; }
        public int repeat { get; set; }
        public string mmsi { get; set; }
        public double speed { get; set; }
        public int accuracy { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
        public double course { get; set; }
        public int heading { get; set; }
        public int second { get; set; }
        public int regional { get; set; }
        public string shipname { get; set; }
        public int shiptype { get; set; }
        public int to_bow { get; set; }
        public int to_stern { get; set; }
        public int to_port { get; set; }
        public int to_starboard { get; set; }
        public int epfd { get; set; }
        public int raim { get; set; }
        public int dte { get; set; }
        public int assigned { get; set; }
    }


    public class Decoded20 : Decoded
    {
        public int type { get; set; }
        public int repeat { get; set; }
        public string mmsi { get; set; }
        public int offset1 { get; set; }
        public int number1 { get; set; }
        public int timeout1 { get; set; }
        public int increment1 { get; set; }
        public int offset2 { get; set; }
        public int number2 { get; set; }
        public int timeout2 { get; set; }
        public int increment2 { get; set; }
        public int offset3 { get; set; }
        public int number3 { get; set; }
        public int timeout3 { get; set; }
        public int increment3 { get; set; }
        public int offset4 { get; set; }
        public int number4 { get; set; }
        public int timeout4 { get; set; }
        public int increment4 { get; set; }
    }

    public class Decoded21 : Decoded
    {
        public int type { get; set; }
        public int repeat { get; set; }
        public string mmsi { get; set; }
        public int aid_type { get; set; }
        public string name { get; set; }
        public int accuracy { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
        public int to_bow { get; set; }
        public int to_stern { get; set; }
        public int to_port { get; set; }
        public int to_starboard { get; set; }
        public int epfd { get; set; }
        public int second { get; set; }
        public int off_position { get; set; }
        public int regional { get; set; }
        public int raim { get; set; }
        public int virtual_aid { get; set; }
        public int assigned { get; set; }
        public string name_extension { get; set; }
    }

    public class Decoded22 : Decoded
    {
        public int type { get; set; }
        public int repeat { get; set; }
        public string mmsi { get; set; }
        public int channel_a { get; set; }
        public int channel_b { get; set; }
        public int txrx { get; set; }
        public int power { get; set; }
        public int addressed { get; set; }
        public int band_a { get; set; }
        public int band_b { get; set; }
        public int zonesize { get; set; }
        public double ne_lon { get; set; }
        public double ne_lat { get; set; }
        public double sw_lon { get; set; }
        public double sw_lat { get; set; }
    }

    public class Decoded24 : Decoded
    {
        public int type { get; set; }
        public int repeat { get; set; }
        public string mmsi { get; set; }
        public int partno { get; set; }
        public int shiptype { get; set; }
        public string vendorid { get; set; }
        public int model { get; set; }
        public int serial { get; set; }
        public string callsign { get; set; }
        public int to_bow { get; set; }
        public int to_stern { get; set; }
        public int to_port { get; set; }
        public int to_starboard { get; set; }
        public string mothership_mmsi { get; set; }
    }

    public class Decoded24_2 : Decoded
    {
        public int type { get; set; }
        public int repeat { get; set; }
        public string mmsi { get; set; }
        public int partno { get; set; }
        public string shipname { get; set; }
    }

    public class Decoded25 : Decoded
    {
        public int type { get; set; }
        public int repeat { get; set; }
        public string mmsi { get; set; }
        public int addressed { get; set; }
        public int structured { get; set; }
        public string data { get; set; }
    }
}
