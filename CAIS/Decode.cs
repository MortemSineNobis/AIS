using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Text;

namespace CAIS
{
    public class Decode
    {
        public static string decode_into_bit_array(string msg)
        {
            string bits = string.Empty;
            foreach (var item in msg)
            {
                var c = item;
                if (c < 0x30 || c > 0x77 || (0x57 < c && c < 0x6))
                {
                    //var resourceManager = new ResourceManager("FULLY_QUALIFIED_NAME_OF_RESOURCE_FILE", Assembly.GetExecutingAssembly());
                    throw new ValueException($"Invalid character: '{c}'", msg, c);
                }
                c -= c < 0x60 ? (char)0x30 : (char)0x38;
                c &= (char)0x3F;
                bits += Convert.ToString(c, 2).PadLeft(6, '0');
            }
            return bits;
        }

        public static int GetType(string data)
        {
            Utils dec = new Utils(data);
            return dec.GetInt(0, 6);
        }
        
        public static PosReportClassA MSG1(string data)
        {
            PosReportClassA res = new PosReportClassA();
            Utils dec = new Utils(data);

            res.type = dec.GetInt(0, 6);
            res.repeat = dec.GetInt(6, 8);
            res.mmsi = dec.GetMMSI(8, 38);
            res.status = dec.GetInt(38, 42);
            res.turn = dec.GetInt(42, 50, true);
            res.speed = dec.GetInt(50, 60) / 10.0;
            res.accuracy = dec.GetSmallInt(60);
            res.lon = dec.GetInt(61, 89, true) / 600000.0;
            res.lat = dec.GetInt(89, 116, true) / 600000.0;
            res.course = dec.GetInt(116, 128) * 0.1;
            res.heading = dec.GetInt(128, 137);
            res.second = dec.GetInt(137, 143);
            res.maneuver = dec.GetInt(143, 145);
            res.raim = dec.GetSmallInt(148);
            res.radio = dec.GetInt(149, 168);

            return res;
        }

        public static BaseStationReport MSG4(string data)
        {
            BaseStationReport res = new BaseStationReport();
            Utils dec = new Utils(data);

            res.type = dec.GetInt(0, 6);
            res.repeat = dec.GetInt(6, 8);
            res.mmsi = dec.GetMMSI(8, 38);
            res.year = dec.GetInt(38, 52);
            res.month = dec.GetInt(52, 56);
            res.day = dec.GetInt(56, 61);
            res.hour = dec.GetInt(61, 66);
            res.minute = dec.GetInt(66, 72);
            res.second = dec.GetInt(72, 78);
            res.accuracy = dec.GetSmallInt(78);
            res.lon = dec.GetInt(79, 107, true) / 600000.0;
            res.lat = dec.GetInt(107, 134, true) / 600000.0;
            res.epfd = dec.GetInt(134, 138);
            res.raim = dec.GetSmallInt(148);
            res.radio = dec.GetInt(149, 168);

            return res;
        }

        public static StaticAndVoyageRelatedData MSG5(string data)
        {
            StaticAndVoyageRelatedData res = new StaticAndVoyageRelatedData();
            Utils dec = new Utils(data);

            res.type = dec.GetInt(0, 6);
            res.repeat = dec.GetInt(6, 8);
            res.mmsi = dec.GetMMSI(8, 38);
            res.ais_version = dec.GetInt(38, 40);
            res.imo = dec.GetInt(40, 70);
            res.callsign = dec.GetString(70, 112);
            var l = data.Length < 231 ? data.Length : 232;
            res.shipname = dec.GetString(112, l);
            res.shiptype = dec.GetInt(232, 240);
            res.to_bow = dec.GetInt(240, 249);
            res.to_stern = dec.GetInt(249, 258);
            res.to_port = dec.GetInt(258, 264);
            res.to_starboard = dec.GetInt(264, 270);
            res.epfd = dec.GetInt(270, 274);
            res.month = dec.GetInt(274, 278);
            res.day = dec.GetInt(278, 283);
            res.hour = dec.GetInt(283, 288);
            res.minute = dec.GetInt(288, 294);
            res.draught = dec.GetInt(294, 302) / 10.0;
            var l1 = 422;
            if (data.Length < 420) return res;
            else
                if (data.Length == 420) l1 = 420;
            res.destination = dec.GetString(302, l1);
            res.dte = dec.GetSmallInt(l1);

            return res;
        }

        public static BinaryAddressedMessage MSG6(string data)
        {
            BinaryAddressedMessage res = new BinaryAddressedMessage();
            Utils dec = new Utils(data);

            res.type = dec.GetInt(0, 6);
            res.repeat = dec.GetInt(6, 8);
            res.mmsi = dec.GetMMSI(8, 38);
            res.seqno = dec.GetInt(38, 40);
            res.dest_mmsi = dec.GetMMSI(40, 70);
            res.retransmit = dec.GetSmallInt(70);
            res.dac = dec.GetInt(72, 82);
            res.fid = dec.GetInt(82, 88);
            res.data = dec.GetSubstring(88);

            return res;
        }

        public static BinaryAcknowledge MSG7(string data)
        {
            BinaryAcknowledge res = new BinaryAcknowledge();
            Utils dec = new Utils(data);

            res.type = dec.GetInt(0, 6);
            res.repeat = dec.GetInt(6, 8);
            res.mmsi = dec.GetMMSI(8, 38);
            res.mmsi1 = dec.GetMMSI(40, 70);
            res.mmsiseq1 = dec.GetInt(70, 72);
            res.mmsi2 = dec.GetMMSI(72, 102);
            res.mmsiseq2 = dec.GetInt(102, 104);
            res.mmsi3 = dec.GetMMSI(104, 134);
            res.mmsiseq3 = dec.GetInt(134, 136);
            res.mmsi4 = dec.GetMMSI(136, 166);
            res.mmsiseq4 = dec.GetInt(166, 168);

            return res;
        }

        public static BinaryBroadcastMessage MSG8(string data)
        {
            BinaryBroadcastMessage res = new BinaryBroadcastMessage();
            Utils dec = new Utils(data);

            res.type = dec.GetInt(0, 6);
            res.repeat = dec.GetInt(6, 8);
            res.mmsi = dec.GetMMSI(8, 38);
            res.dac = dec.GetInt(40, 50);
            res.fid = dec.GetInt(50, 56);
            res.data = dec.GetSubstring(56);

            return res;
        }

        public static StandardSARAircraftPositionReport MSG9(string data)
        {
            StandardSARAircraftPositionReport res = new StandardSARAircraftPositionReport();
            Utils dec = new Utils(data);

            res.type = dec.GetInt(0, 6);
            res.repeat = dec.GetInt(6, 8);
            res.mmsi = dec.GetMMSI(8, 38);
            res.alt = dec.GetInt(38, 50);
            res.speed = dec.GetInt(50, 60);
            res.accuracy = dec.GetSmallInt(60);
            res.lon = dec.GetInt(61, 89,true) / 600000.0;
            res.lat = dec.GetInt(89, 116, true) / 600000.0;
            res.course = dec.GetInt(116, 128) * 0.1;
            res.second = dec.GetInt(128, 134);
            res.dte = dec.GetSmallInt(142);
            res.assigned = dec.GetSmallInt(146);
            res.raim = dec.GetSmallInt(147);
            res.radio = dec.GetInt(148, 168);

            return res;
        }

        public static UTCDatelnquiry MSG10(string data)
        {
            UTCDatelnquiry res = new UTCDatelnquiry();
            Utils dec = new Utils(data);

            res.type = dec.GetInt(0, 6);
            res.repeat = dec.GetInt(6, 8);
            res.mmsi = dec.GetMMSI(8, 38);
            res.dest_mmsi = dec.GetMMSI(40, 70);

            return res;
        }

        public static UTCDateResponse MSG11(string data)
        {
            UTCDateResponse res = new UTCDateResponse();
            Utils dec = new Utils(data);

            res.type = dec.GetInt(0, 6);
            res.repeat = dec.GetInt(6, 8);
            res.mmsi = dec.GetMMSI(8, 38);
            res.year = dec.GetInt(38, 52);
            res.month = dec.GetInt(52, 56);
            res.day = dec.GetInt(56, 61);
            res.hour = dec.GetInt(61, 66);
            res.minute = dec.GetInt(66, 72);
            res.second = dec.GetInt(72, 78);
            res.accuracy = dec.GetSmallInt(78);
            res.lon = dec.GetInt(79, 107, true) / 600000.0;
            res.lat = dec.GetInt(107, 134, true) / 600000.0;
            res.epfd = dec.GetInt(134, 138);
            res.raim = dec.GetSmallInt(148);
            res.radio = dec.GetInt(149, data.Length);

            return res;
        }

        public static AddressedSafetyRelatedMessage MSG12(string data)
        {
            AddressedSafetyRelatedMessage res = new AddressedSafetyRelatedMessage();
            Utils dec = new Utils(data);

            res.type = dec.GetInt(0, 6);
            res.repeat = dec.GetInt(6, 8);
            res.mmsi = dec.GetMMSI(8, 38);
            res.seqno = dec.GetInt(38, 40);
            res.dest_mmsi = dec.GetMMSI(40, 70);
            res.retransmit = dec.GetSmallInt(70);
            res.text = dec.GetString(72, data.Length);

            return res;
        }

        public static SafetyRelatedAcknowledgment MSG13(string data)
        {
            SafetyRelatedAcknowledgment res = new SafetyRelatedAcknowledgment();
            Utils dec = new Utils(data);

            res.type = dec.GetInt(0, 6);
            res.repeat = dec.GetInt(6, 8);
            res.mmsi = dec.GetMMSI(8, 38);
            res.mmsi1 = dec.GetMMSI(40, 70);
            res.mmsiseq1 = dec.GetInt(70, 72);
            res.mmsi2 = dec.GetMMSI(72, 102);
            res.mmsiseq2 = dec.GetInt(102, 104);
            res.mmsi3 = dec.GetMMSI(104, 134);
            res.mmsiseq3 = dec.GetInt(134, 136);
            res.mmsi4 = dec.GetMMSI(136, 166);
            res.mmsiseq4 = dec.GetInt(166, 168);

            return res;
        }

        public static SafetyRelatedBroadcastMessage MSG14(string data)
        {
            SafetyRelatedBroadcastMessage res = new SafetyRelatedBroadcastMessage();
            Utils dec = new Utils(data);

            res.type = dec.GetInt(0, 6);
            res.repeat = dec.GetInt(6, 8);
            res.mmsi = dec.GetMMSI(8, 38);
            res.text = dec.GetString(40, data.Length);

            return res;
        }

        public static Interrogation MSG15(string data)
        {
            Interrogation res = new Interrogation();
            Utils dec = new Utils(data);

            res.type = dec.GetInt(0, 6);
            res.repeat = dec.GetInt(6, 8);
            res.mmsi = dec.GetMMSI(8, 38);
            res.mmsi1 = dec.GetMMSI(40, 70);
            res.type1_1 = dec.GetInt(70, 76);
            res.offset1_1 = dec.GetInt(76, 88);
            res.type1_2 = dec.GetInt(90, 96);
            res.offset1_2 = dec.GetInt(96, 108);
            res.mmsi2 = dec.GetMMSI(110, 140);
            res.type2_1 = dec.GetInt(140, 146);
            res.offset2_1 = dec.GetInt(146, 157);

            return res;
        }

        public static AssignmentModeCommand MSG16(string data)
        {
            AssignmentModeCommand res = new AssignmentModeCommand();
            Utils dec = new Utils(data);

            res.type = dec.GetInt(0, 6);
            res.repeat = dec.GetInt(6, 8);
            res.mmsi = dec.GetMMSI(8, 38);
            res.mmsi1 = dec.GetMMSI(40, 70);
            res.offset1 = dec.GetInt(70, 82);
            res.increment1 = dec.GetInt(82, 92);
            res.mmsi2 = dec.GetMMSI(92, 122);
            res.offset2 = dec.GetInt(122, 134); 
            res.increment2 = dec.GetInt(134, 144);

            return res;
        }

        public static DGNSSBroadcastBinaryMessage MSG17(string data)
        {
            DGNSSBroadcastBinaryMessage res = new DGNSSBroadcastBinaryMessage();
            Utils dec = new Utils(data);

            res.type = dec.GetInt(0, 6);
            res.repeat = dec.GetInt(6, 8);
            res.mmsi = dec.GetMMSI(8, 38);
            res.lon = dec.GetInt(40, 58, true);
            res.lat = dec.GetInt(58, 75, true);
            res.data = dec.GetSubstring(80);

            return res;
        }

        public static PosReportClassB MSG18(string data)
        {
            PosReportClassB res = new PosReportClassB();
            Utils dec = new Utils(data);

            res.type = dec.GetInt(0, 6);
            res.repeat = dec.GetInt(6, 8);
            res.mmsi = dec.GetMMSI(8, 38);
            res.speed = dec.GetInt(46, 56) * 0.1;
            res.accuracy = dec.GetSmallInt(56);
            res.lon = dec.GetInt(57, 85, true) / 600000.0;
            res.lat = dec.GetInt(85, 112, true) / 600000.0;
            res.course = dec.GetInt(112, 124) * 0.1;
            res.heading = dec.GetInt(124, 133);
            res.second = dec.GetInt(133, 139);
            res.regional = dec.GetInt(139, 141);
            res.cs = dec.GetSmallInt(141);
            res.display = dec.GetSmallInt(142);
            res.dsc = dec.GetSmallInt(143);
            res.band = dec.GetSmallInt(144);
            res.msg22 = dec.GetSmallInt(145);
            res.assigned = dec.GetSmallInt(146);
            res.raim = dec.GetSmallInt(147);
            var l = data.Length > 167 ? 167 : data.Length;
            res.radio = dec.GetInt(148, l);

            return res;
        }

        public static ExtendedPosReportClassВ MSG19(string data)
        {
            ExtendedPosReportClassВ res = new ExtendedPosReportClassВ();
            Utils dec = new Utils(data);

            res.type = dec.GetInt(0, 6);
            res.repeat = dec.GetInt(6, 8);
            res.mmsi = dec.GetMMSI(8, 38);
            res.speed = dec.GetInt(46, 56) * 0.1;
            res.accuracy = dec.GetSmallInt(56);
            res.lon = dec.GetInt(57, 85, true) / 600000.0;
            res.lat = dec.GetInt(85, 112, true) / 600000.0;
            res.course = dec.GetInt(112, 124) * 0.1;
            res.heading = dec.GetInt(124, 133);
            res.second = dec.GetInt(133, 139);
            res.regional = dec.GetInt(139, 143);
            res.shipname = dec.GetString(143, 263);
            res.shiptype = dec.GetInt(263, 271);
            res.to_bow = dec.GetInt(271, 280);
            res.to_stern = dec.GetInt(280, 289);
            res.to_port = dec.GetInt(289, 295);
            res.to_starboard = dec.GetInt(295, 301);
            res.epfd = dec.GetInt(301, 305);
            res.raim = dec.GetSmallInt(305);
            res.dte = dec.GetSmallInt(306);
            res.assigned = dec.GetSmallInt(307);

            return res;
        }

        public static DataLinkManagementMessage MSG20(string data)
        {
            DataLinkManagementMessage res = new DataLinkManagementMessage();
            Utils dec = new Utils(data);

            res.type = dec.GetInt(0, 6);
            res.repeat = dec.GetInt(6, 8);
            res.mmsi = dec.GetMMSI(8, 38);

            res.offset1 = dec.GetInt(40, 52);
            res.number1 = dec.GetInt(52, 56);
            res.timeout1 = dec.GetInt(56, 59);
            res.increment1 = dec.GetInt(59, 70);

            res.offset2 = dec.GetInt(70, 82);
            res.number2 = dec.GetInt(82, 86);
            res.timeout2 = dec.GetInt(86, 89);
            res.increment2 = dec.GetInt(89, 100);

            res.offset3 = dec.GetInt(100, 112);
            res.number3 = dec.GetInt(112, 116);
            res.timeout3 = dec.GetInt(116, 119);
            res.increment3 = dec.GetInt(110, 130);
            
            res.offset4 = dec.GetInt(130, 142);
            res.number4 = dec.GetInt(142, 146);
            res.timeout4 = dec.GetInt(146, 149);
            res.increment4 = dec.GetInt(149, 160);

            return res;
        }

        public static AidToNavigationReport MSG21(string data)
        {
            AidToNavigationReport res = new AidToNavigationReport();
            Utils dec = new Utils(data);

            res.type = dec.GetInt(0, 6);
            res.repeat = dec.GetInt(6, 8);
            res.mmsi = dec.GetMMSI(8, 38);

            res.aid_type = dec.GetInt(38, 43);
            res.name = dec.GetString(43, 163);
            res.accuracy = dec.GetSmallInt(163);
            res.lon = dec.GetInt(164, 192, true) / 600000.0;
            res.lat = dec.GetInt(192, 219, true) / 600000.0;
            res.to_bow = dec.GetInt(219, 228);
            res.to_stern = dec.GetInt(228, 237);
            res.to_port = dec.GetInt(237, 243);
            res.to_starboard = dec.GetInt(243, 249);
            res.epfd = dec.GetInt(249, 253);
            res.second = dec.GetInt(253, 259);
            res.off_position = dec.GetSmallInt(259);
            res.regional = dec.GetInt(260, 268);
            res.raim = dec.GetSmallInt(268);
            res.virtual_aid = dec.GetSmallInt(269);
            res.assigned = dec.GetSmallInt(270);
            res.name_extension = dec.GetString(272, data.Length);

            return res;
        }

        public static ChannelManagement MSG22(string data)
        {
            ChannelManagement res = new ChannelManagement();
            Utils dec = new Utils(data);

            res.type = dec.GetInt(0, 6);
            res.repeat = dec.GetInt(6, 8);
            res.mmsi = dec.GetMMSI(8, 38);

            res.channel_a = dec.GetInt(40, 52);
            res.channel_b = dec.GetInt(52, 64);
            res.txrx = dec.GetInt(64, 68);
            res.power = dec.GetSmallInt(68);
            res.addressed = dec.GetSmallInt(139);
            res.band_a = dec.GetSmallInt(140);
            res.band_b = dec.GetSmallInt(141);
            res.zonesize = dec.GetInt(142, 145);

            if (res.addressed == 1)
            {
                res.dest1 = dec.GetMMSI(69, 99);
                res.dest2 = dec.GetMMSI(104, 134);
            }
            else
            {
                res.ne_lon = dec.GetInt(69, 87, true) * 0.1;
                res.ne_lat = dec.GetInt(87, 104, true) * 0.1;
                res.sw_lon = dec.GetInt(104, 122, true) * 0.1;
                res.sw_lat = dec.GetInt(122, 139, true) * 0.1;
            }

            return res;
        }

        public static GroupAssignmentCommand MSG23(string data)
        {
            GroupAssignmentCommand res = new GroupAssignmentCommand();
            Utils dec = new Utils(data);

            res.type = dec.GetInt(0, 6);
            res.repeat = dec.GetInt(6, 8);
            res.mmsi = dec.GetMMSI(8, 38);

            res.ne_lon = dec.GetInt(40, 58, true) * 0.1;
            res.ne_lat = dec.GetInt(58, 75, true) * 0.1;
            res.sw_lon = dec.GetInt(75, 93, true) * 0.1;
            res.sw_lat = dec.GetInt(93, 110, true) * 0.1;

            res.station_type = dec.GetInt(110, 114);
            res.shiptype = dec.GetInt(114, 122);
            res.txrx = dec.GetInt(144, 146);
            res.interval = dec.GetInt(146, 150);
            res.quiet = dec.GetInt(150, 154);

            return res;
        }

        public static StaticDataReport MSG24(string data)
        {
            StaticDataReport res = new StaticDataReport();
            Utils dec = new Utils(data);

            res.type = dec.GetInt(0, 6);
            res.repeat = dec.GetInt(6, 8);
            res.mmsi = dec.GetMMSI(8, 38);

            res.partno = dec.GetInt(38, 40);
            
            if (res.partno == 0)
                res.shipname = dec.GetString(40, 160);
            else
            {
                res.shiptype = dec.GetInt(40, 48);
                res.vendorid = dec.GetString(48, 66);
                res.model = dec.GetInt(66, 70);
                res.serial = dec.GetInt(70, 90);
                res.callsign = dec.GetString(90, 132);
                res.to_bow = dec.GetInt(132, 141);
                res.to_stern = dec.GetInt(141, 150);
                res.to_port = dec.GetInt(150, 156);
                res.to_starboard = dec.GetInt(156, 162);
                res.mothership_mmsi = dec.GetMMSI(132, 162);
            }
            
            return res;
        }

        public static SingleSlotBinaryMessage MSG25(string data)
        {
            SingleSlotBinaryMessage res = new SingleSlotBinaryMessage();
            Utils dec = new Utils(data);

            res.type = dec.GetInt(0, 6);
            res.repeat = dec.GetInt(6, 8);
            res.mmsi = dec.GetMMSI(8, 38);

            res.addressed = dec.GetSmallInt(38);
            res.structured = dec.GetSmallInt(39);
            

            if (res.addressed == 1)
                res.dest_mmsi = dec.GetMMSI(40, 70);

            int lo_ix = res.addressed == 1 ? 40 : 70;
            int hi_ix = lo_ix + 16;

            if (res.structured == 1)
            {
                res.app_id = dec.GetInt(lo_ix, hi_ix);
                res.data = dec.GetSubstring(hi_ix);
            }
            else
                res.data = dec.GetSubstring(lo_ix);

            return res;
        }

        public static MultipleSlotBinaryMessage MSG26(string data)
        {
            MultipleSlotBinaryMessage res = new MultipleSlotBinaryMessage();
            Utils dec = new Utils(data);

            int offset = data.Length - 20;

            res.type = dec.GetInt(0, 6);
            res.repeat = dec.GetInt(6, 8);
            res.mmsi = dec.GetMMSI(8, 38);

            res.addressed = dec.GetSmallInt(38);
            res.structured = dec.GetSmallInt(39);
            res.radio = dec.GetInt(offset, data.Length);

            if (res.addressed == 1)
                res.dest_mmsi = dec.GetMMSI(40, 70);

            int lo_ix = res.addressed == 1 ? 40 : 70;
            int hi_ix = lo_ix + 16;

            if (res.structured == 1)
            {
                res.app_id = dec.GetInt(lo_ix, hi_ix);
                res.data = dec.GetSubstring(hi_ix,offset-hi_ix);
            }
            else
                res.data = dec.GetSubstring(lo_ix,offset-hi_ix);

            return res;
        }

        public static LongRangeAISBroadcastMessage MSG27(string data)
        {
            LongRangeAISBroadcastMessage res = new LongRangeAISBroadcastMessage();
            Utils dec = new Utils(data);

            int offset = data.Length - 20;

            res.type = dec.GetInt(0, 6);
            res.repeat = dec.GetInt(6, 8);
            res.mmsi = dec.GetMMSI(8, 38);

            res.accuracy = dec.GetSmallInt(38);
            res.raim = dec.GetSmallInt(39);
            res.status = dec.GetInt(40, 44);
            res.lon = dec.GetInt(44, 62, true) / 600.0;
            res.lat = dec.GetInt(62, 79, true) / 600.0;
            res.speed = dec.GetInt(79, 85);
            res.course = dec.GetInt(85, 94);
            res.gnss = dec.GetSmallInt(94);
            
            return res;
        }

        public static Decoded DecodeData(string data, AISType type)
        {
            if (data.Length < 38) return null;
            switch (type)
            {
                case AISType.NOT_IMPLEMENTED:
                case AISType.PositionReportClassA1:
                case AISType.PositionReportClassA2:
                case AISType.PositionReportClassA3:
                    return MSG1(data);
                case AISType.BaseStationReport:
                    return MSG4(data);
                case AISType.StaticAndVoyageRelatedData:
                    return MSG5(data);
                case AISType.BinaryAddressedMessage:
                    return MSG6(data);
                case AISType.BinaryAcknowledge:
                    return MSG7(data);
                case AISType.BinaryBroadcastMessage:
                    return MSG8(data);
                case AISType.StandardSARAircraftPositionReport:
                    return MSG9(data);
                case AISType.UTCDateInquiry:
                    return MSG10(data);
                case AISType.UTCDateResponse:
                    return MSG11(data);
                case AISType.AddressedSafetyRelatedMessage:
                    return MSG12(data);
                case AISType.SafetyRelatedAcknowledgment:
                    return MSG13(data);
                case AISType.SafetyRelatedBroadcastMessage:
                    return MSG14(data);
                case AISType.Interrogation:
                    return MSG15(data);
                case AISType.AssignmentModeCommand:
                    return MSG16(data);
                case AISType.DGNSSBroadcastBinaryMessage:
                    return MSG17(data);
                case AISType.StandardClassBCSPositionReport:
                    return MSG18(data);
                case AISType.ExtendedClassBCSPositionReport:
                    return MSG19(data);
                case AISType.DataLinkManagementMessage:
                    return MSG20(data);
                case AISType.AidToNavigationReport:
                    return MSG21(data);
                case AISType.ChannelManagement:
                    return MSG22(data);
                case AISType.GroupAssignmentCommand:
                    return MSG23(data);
                case AISType.StaticDataReport:
                    return MSG24(data);
                case AISType.SingleSlotBinaryMessage:
                    return MSG25(data);
                case AISType.MultipleSlotBinaryMessage:
                    return MSG26(data);
                case AISType.LongRangeAISBroadcastMessage:
                    return MSG27(data);
                default:
                    return null;
            }
        }
    }
}
