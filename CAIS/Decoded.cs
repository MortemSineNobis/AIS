using System;
using System.Collections.Generic;
using System.Text;

namespace CAIS
{
    public class Decoded
    {
        public int type { get; set; }
        public int repeat { get; set; }
        public string mmsi { get; set; }
    }

    public class AidToNavigationReport : Decoded
    {
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

    public class BaseStationReport : Decoded
    {
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

    public class AssignmentModeCommand : Decoded
    {
        public string mmsi1 { get; set; }
        public int offset1 { get; set; }
        public int increment1 { get; set; }
        public string mmsi2 { get; set; }
        public int offset2 { get; set; }
        public int increment2 { get; set; }
    }

    public class BinaryAddressedMessage : Decoded
    {
        public int seqno { get; set; }
        public string dest_mmsi { get; set; }
        public int retransmit { get; set; }
        public int dac { get; set; }
        public int fid { get; set; }
        public string data { get; set; }
    }

    public class BinaryBroadcastMessage : Decoded
    {
        public int dac { get; set; }
        public int fid { get; set; }
        public string data { get; set; }
    }

    public class ChannelManagement : Decoded
    {
        public int channel_a { get; set; }
        public int channel_b { get; set; }
        public int txrx { get; set; }
        public int power { get; set; }
        public int addressed { get; set; }
        public int band_a { get; set; }
        public int band_b { get; set; }
        public int zonesize { get; set; }
        public string dest1 { get; set; }
        public string dest2 { get; set; }
        public double ne_lon { get; set; }
        public double ne_lat { get; set; }
        public double sw_lon { get; set; }
        public double sw_lat { get; set; }
    }

    public class DataLinkManagementMessage : Decoded
    {
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

    public class DGNSSBroadcastBinaryMessage : Decoded
    {
        public int lon { get; set; }
        public int lat { get; set; }
        public string data { get; set; }
    }

    public class ExtendedPosReportClassВ : Decoded
    {
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

    public class PosReportClassA : Decoded
    {
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

    public class PosReportClassB : Decoded
    {
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

    public class SingleSlotBinaryMessage : Decoded
    {
        public int addressed { get; set; }
        public int structured { get; set; }
        public string dest_mmsi { get; set; }
        public int app_id { get; set; }
        public string data { get; set; }
    }

    public class StandardSARAircraftPositionReport : Decoded
    {
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

    public class StaticAndVoyageRelatedData : Decoded
    {
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

    public class StaticDataReport : Decoded
    {
        public int partno { get; set; }
        public int shiptype { get; set; }
        public string shipname { get; set; }
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

    public class UTCDatelnquiry : Decoded
    {
        public string dest_mmsi { get; set; }
    }

    public class UTCDateResponse : Decoded
    {
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

    public class BinaryAcknowledge : Decoded
    {
        public string mmsi1 { get; set; }
        public int mmsiseq1 { get; set; }
        public string mmsi2 { get; set; }
        public int mmsiseq2 { get; set; }
        public string mmsi3 { get; set; }
        public int mmsiseq3 { get; set; }
        public string mmsi4 { get; set; }
        public int mmsiseq4 { get; set; }
    }

    public class AddressedSafetyRelatedMessage : Decoded
    {
        public int seqno { get; set; }
        public string dest_mmsi { get; set; }
        public int retransmit { get; set; }
        public string text { get; set; }
    }

    public class SafetyRelatedAcknowledgment : Decoded
    {
        public string mmsi1 { get; set; }
        public int mmsiseq1 { get; set; }
        public string mmsi2 { get; set; }
        public int mmsiseq2 { get; set; }
        public string mmsi3 { get; set; }
        public int mmsiseq3 { get; set; }
        public string mmsi4 { get; set; }
        public int mmsiseq4 { get; set; }
    }

    public class SafetyRelatedBroadcastMessage : Decoded
    {
        public string text { get; set; }
    }

    public class Interrogation : Decoded
    {
        public string mmsi1 { get; set; }
        public int type1_1 { get; set; }
        public int offset1_1 { get; set; }
        public int type1_2 { get; set; }
        public int offset1_2 { get; set; }
        public string mmsi2 { get; set; }
        public int type2_1 { get; set; }
        public int offset2_1 { get; set; }
    }


    public class GroupAssignmentCommand : Decoded
    {
        public double ne_lon { get; set; }
        public double ne_lat { get; set; }
        public double sw_lon { get; set; }
        public double sw_lat { get; set; }
        public int station_type { get; set; }
        public int shiptype { get; set; }
        public int txrx { get; set; }
        public int interval { get; set; }
        public int quiet { get; set; }
    }

    public class MultipleSlotBinaryMessage : Decoded
    {
        public int addressed { get; set; }
        public int structured { get; set; }
        public string dest_mmsi { get; set; }
        public int app_id { get; set; }
        public string data { get; set; }
        public int radio { get; set; }
    }

    public class LongRangeAISBroadcastMessage : Decoded
    {
        public int accuracy { get; set; }
        public int raim { get; set; }
        public int status { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
        public int speed { get; set; }
        public int course { get; set; }
        public int gnss { get; set; }
    }
}