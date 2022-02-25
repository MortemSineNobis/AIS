using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CAIS
{
    //class JSONDecode
    //{
    //    public static Message GetMessage(string json)
    //    {
    //        Message res = new Message();
    //        var document = JsonDocument.Parse(json);
    //        var root = document.RootElement;
    //        JsonElement nmeaJSON = root.GetProperty("nmea");
    //        JsonElement decodedJSON = root.GetProperty("decoded");
    //        res.NMEA = GetDecode<NMEA>(nmeaJSON);
    //        switch (res.NMEA.ais_id)
    //        {
    //            case 1:
    //            case 2:
    //            case 3:
    //                res.Decoded = GetDecode<PosReportClassA>(decodedJSON);
    //                res.MessageType = AISType.POS_CLASS_A1;
    //                break;
    //            case 4:
    //                res.Decoded = GetDecode<BaseStationReport>(decodedJSON);
    //                res.MessageType = AISType.BaseStationReport;
    //                break;
    //            case 5:
    //                res.Decoded = GetDecode<StaticAndVoyageRelatedData>(decodedJSON);
    //                res.MessageType = AISType.StaticandVoyageRelatedData;
    //                break;
    //            case 6:
    //                res.Decoded = GetDecode<BinaryAddressedMessage>(decodedJSON);
    //                res.MessageType = AISType.BinaryAddressedMessage;
    //                break;
    //            case 8:
    //                res.Decoded = GetDecode<BinaryBroadcastMessage>(decodedJSON);
    //                res.MessageType = AISType.BinaryBroadcastMessage;
    //                break;
    //            case 9:
    //                res.Decoded = GetDecode<StandardSARAircraftPositionReport>(decodedJSON);
    //                res.MessageType = AISType.StandardSARAircraftPositionReport;
    //                break;
    //            case 10:
    //                res.Decoded = GetDecode<UTCDatelnquiry>(decodedJSON);
    //                res.MessageType = AISType.UTCAndDateInquiry;
    //                break;
    //            case 11:
    //                res.Decoded = GetDecode<UTCDateResponse>(decodedJSON);
    //                res.MessageType = AISType.UTCAndDateResponse;
    //                break;
    //            case 16:
    //                res.Decoded = GetDecode<AssignmentModeCommand>(decodedJSON);
    //                res.MessageType = AISType.AssignmentModeCommand;
    //                break;
    //            case 17:
    //                res.Decoded = GetDecode<DGNSSBroadcastBinaryMessage>(decodedJSON);
    //                res.MessageType = AISType.DGNSSBinaryBroadcastMessage;
    //                break;
    //            case 18:
    //                res.Decoded = GetDecode<PosReportClassB>(decodedJSON);
    //                res.MessageType = AISType.PositionReportClassB;
    //                break;
    //            case 19:
    //                res.Decoded = GetDecode<ExtendedPosReportClassВ>(decodedJSON);
    //                res.MessageType = AISType.ExtendedClassBEquipmentPositionReport;
    //                break;
    //            case 20:
    //                res.Decoded = GetDecode<DataLinkManagementMessage>(decodedJSON);
    //                res.MessageType = AISType.DataLinkManagement;
    //                break;
    //            case 21:
    //                res.Decoded = GetDecode<AidToNavigationReport>(decodedJSON);
    //                res.MessageType = AISType.AidToNavigationReport;
    //                break;
    //            case 22:
    //                res.Decoded = GetDecode<ChannelManagement>(decodedJSON);
    //                res.MessageType = AISType.ChannelManagement;
    //                break;
    //            case 24:
    //                res.Decoded = GetDecode<StaticDataReport>(decodedJSON);
    //                res.MessageType = AISType.StaticDataReport;
    //                break;
    //            case 25:
    //                res.Decoded = GetDecode<SingleSlotBinaryMessage>(decodedJSON);
    //                res.MessageType = AISType.SingleSlotBinaryMessage;
    //                break;
    //            default:
    //                res.MessageType = AISType.OTHER;
    //                break;
    //        }
    //        return res;
    //    }

    //    public static TValue GetDecode<TValue>(JsonElement JSON)
    //    {
    //        TValue res = JsonSerializer.Deserialize<TValue>(JSON.ToString());
    //        return res;
    //    }
    //}
}
