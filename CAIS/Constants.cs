using System;
using System.Collections.Generic;
using System.Text;

namespace CAIS
{
    public enum AISType : int
    {
        NOT_IMPLEMENTED = 0,
        PositionReportClassA1 = 1,
        PositionReportClassA2 = 2,
        PositionReportClassA3 = 3,
        BaseStationReport = 4,
        StaticAndVoyageRelatedData = 5,
        BinaryAddressedMessage = 6,
        BinaryAcknowledge = 7,
        BinaryBroadcastMessage = 8,
        StandardSARAircraftPositionReport = 9,
        UTCDateInquiry = 10,
        UTCDateResponse = 11,
        AddressedSafetyRelatedMessage = 12,
        SafetyRelatedAcknowledgment = 13,
        SafetyRelatedBroadcastMessage = 14,
        Interrogation = 15,
        AssignmentModeCommand = 16,
        DGNSSBroadcastBinaryMessage = 17,
        StandardClassBCSPositionReport = 18,
        ExtendedClassBCSPositionReport = 19,
        DataLinkManagementMessage = 20,
        AidToNavigationReport = 21,
        ChannelManagement = 22,
        GroupAssignmentCommand = 23,
        StaticDataReport = 24,
        SingleSlotBinaryMessage = 25,
        MultipleSlotBinaryMessage = 26,
        LongRangeAISBroadcastMessage = 27
    }

    public class Keywords
    {
        public const string UNDEFINED = "Undefined";
        public const string stringRESERVED = "Reserved";
        public const string stringNULL = "N/A";
        public const string stringANSI_RED = "\x1b[31m";
        public const string stringANSI_RESET = "\x1b[0m";
    }
    
    public class TalkerIDClass
    {
        public enum TalkerID
        {
            Base_Station, 
            Dependent_Base_Station, 
            Mobile_Station, 
            Navigation_Station,
            Receiving_Station,
            Limited_Base_Station,
            Repeater_Station,
            Base_Station_Deprecated,
            Physical_Shore_Station,
            UNDEFINED
        }

        public const string Base_Station = "AB";
        public const string Dependent_Base_Station = "AD";
        public const string Mobile_Station = "AI";
        public const string Navigation_Station = "AN";
        public const string Receiving_Station = "AR";
        public const string Limited_Base_Station = "AS";
        public const string Transmitting_Station = "AT";
        public const string Repeater_Station = "AX";
        public const string Base_Station_Deprecated = "BS";
        public const string Physical_Shore_Station = "SA";
        public const string UNDEFINED = "UNDEFINED";

        public static TalkerID GetTalkerID(string value)
        {
            switch (value)
            {
                case Base_Station:
                    return TalkerID.Base_Station;
                case Dependent_Base_Station:
                    return TalkerID.Dependent_Base_Station;
                case Mobile_Station:
                    return TalkerID.Mobile_Station;
                case Navigation_Station:
                    return TalkerID.Navigation_Station;
                case Receiving_Station:
                    return TalkerID.Receiving_Station;
                case Limited_Base_Station:
                    return TalkerID.Limited_Base_Station;
                case Transmitting_Station:
                    return TalkerID.Limited_Base_Station;
                case Repeater_Station:
                    return TalkerID.Repeater_Station;
                case Base_Station_Deprecated:
                    return TalkerID.Base_Station_Deprecated;
                case Physical_Shore_Station:
                    return TalkerID.Physical_Shore_Station;
                default:
                    return TalkerID.UNDEFINED;
            }
        }

        public static string GetString(TalkerID iD)
        {
            switch (iD)
            {
                case TalkerID.Base_Station:
                    return Base_Station;
                case TalkerID.Dependent_Base_Station:
                    return Dependent_Base_Station;
                case TalkerID.Mobile_Station:
                    return Mobile_Station;
                case TalkerID.Navigation_Station:
                    return Navigation_Station;
                case TalkerID.Receiving_Station:
                    return Receiving_Station;
                case TalkerID.Limited_Base_Station:
                    return Limited_Base_Station;
                case TalkerID.Repeater_Station:
                    return Receiving_Station;
                case TalkerID.Base_Station_Deprecated:
                    return Base_Station_Deprecated;
                case TalkerID.Physical_Shore_Station:
                    return Physical_Shore_Station;
                case TalkerID.UNDEFINED:
                    return UNDEFINED;
                default:
                    return UNDEFINED;
            }
        }
    }
    public enum NavigationStatus : int
    {
        UnderWayUsingEngine = 0,
        AtAnchor = 1,
        NotUnderCommand = 2,
        RestrictedManoeuverability = 3,
        ConstrainedByHerDraught = 4,
        Moored = 5,
        Aground = 6,
        EngagedInFishing = 7,
        UnderWaySailing = 8,
        AISSARTActive = 14,
        Undefined = 15
    }

    public enum ManeuverIndicator : int
    {
        NotAvailable = 0,
        NoSpecialManeuver = 1,
        SpecialManeuver = 2,
        UNDEFINED = 3
    }

    public enum EpfdType : int
    {
        Undefined = 0,
        GPS = 1,
        GLONASS = 2,
        GPS_GLONASS = 3,
        Loran_C = 4,
        Chayka = 5,
        IntegratedNavigationSystem = 6,
        Surveyed = 7,
        Galileo = 8
    }

    public class ShipType
    {
        public enum ShipTypeEnum : int
        {
            NotAvailable = 0,
            // 20's
            WIG = 20,
            WIG_HazardousCategory_A = 21,
            WIG_HazardousCategory_B = 22,
            WIG_HazardousCategory_C = 23,
            WIG_HazardousCategory_D = 24,
            WIG_Reserved = 25,
            // 30's
            Fishing = 30,
            Towing = 31,
            Towing_LengthOver200 = 32,
            DredgingOrUnderwaterOps = 33,
            DivingOps = 34,
            MilitaryOps = 35,
            Sailing = 36,
            PleasureCraft = 37,
            // 40's
            HSC = 40,
            HSC_HazardousCategory_A = 41,
            HSC_HazardousCategory_B = 42,
            HSC_HazardousCategory_C = 43,
            HSC_HazardousCategory_D = 44,
            HSC_Reserved = 45,
            HSC_NoAdditionalInformation = 49,
            // 50's
            PilotVessel = 50,
            SearchAndRescueVessel = 51,
            Tug = 52,
            PortTender = 53,
            AntiPollutionEquipment = 54,
            LawEnforcement = 55,
            SPARE = 56,
            MedicalTransport = 58,
            NonCombatShip = 59,
            // 60's
            Passenger = 60,
            Passenger_HazardousCategory_A = 61,
            Passenger_HazardousCategory_B = 62,
            Passenger_HazardousCategory_C = 63,
            Passenger_HazardousCategory_D = 64,
            Passenger_Reserved = 65,
            Passenger_NoAdditionalInformation = 69,
            // 70's
            Cargo = 70,
            Cargo_HazardousCategory_A = 71,
            Cargo_HazardousCategory_B = 72,
            Cargo_HazardousCategory_C = 73,
            Cargo_HazardousCategory_D = 74,
            Cargo_Reserved = 75,
            Cargo_NoAdditionalInformation = 79,
            // 80's
            Tanker = 80,
            Tanker_HazardousCategory_A = 81,
            Tanker_HazardousCategory_B = 82,
            Tanker_HazardousCategory_C = 83,
            Tanker_HazardousCategory_D = 84,
            Tanker_Reserved = 85,
            Tanker_NoAdditionalInformation = 89,
            // 90's
            OtherType = 90,
            OtherType_HazardousCategory_A = 91,
            OtherType_HazardousCategory_B = 92,
            OtherType_HazardousCategory_C = 93,
            OtherType_HazardousCategory_D = 94,
            OtherType_Reserved = 95,
            OtherType_NoAdditionalInformation = 99
        }
        
        public ShipTypeEnum GetShipType(int value)
        {
            if (Enum.IsDefined(typeof(ShipTypeEnum), value))
                return (ShipTypeEnum)value;

            if (IS(24, 30, value)) 
                return ShipTypeEnum.WIG_Reserved;

            if (IS(44, 49, value)) 
                return ShipTypeEnum.HSC_Reserved;

            if (IS(55, 58, value)) 
                return ShipTypeEnum.SPARE;

            if (IS(64, 69, value)) 
                return ShipTypeEnum.Passenger_Reserved;

            if (IS(74, 79, value)) 
                return ShipTypeEnum.Cargo_Reserved;

            if (IS(84, 89, value))
                return ShipTypeEnum.Tanker_Reserved;

            if (IS(94 ,99, value))
                return ShipTypeEnum.OtherType_Reserved;
            return ShipTypeEnum.NotAvailable;
        }

        private bool IS(int a, int b, int value)
        {
            return (a < value) && (value < b);
        }
    }
    
    public enum DacFid : int
    {
        DangerousCargoIndication = 13,
        TidalWindow = 15,
        NumPersonsOnBoard = 17,
        ClearanceTimeToEnterPort = 19,
        BerthingData = 21,
        AreaNotice = 24,
        RouteInfoAddressed = 29,
        TextDescriptionAddressed = 31,
        ETA = 221,
        RTA = 222,
        AtoN_MonitoringData_UK = 245,
        AtoN_MonitoringData_ROI = 260
    }

    public enum NavAid : int
    {
        DEFAULT = 0,
        REFERENCE_POINT = 1,
        RACON = 2,
        FIXED = 3,
        FITTED = 4,
        SPARE = 5,
        LIGHT_NO_SECTORS = 6,
        LIGHT_SECTORS = 7,
        LEADING_LIGHT_FRONT = 8,
        LEADING_LIGHT_REAR = 9,
        BEACON_CARDINAL_N = 10,
        BEACON_CARDINAL_E = 11,
        BEACON_CARDINAL_S = 12,
        BEACON_CARDINAL_W = 13,
        BEACON_STARBOARD = 14,
        BEACON_CHANNEL_PORT_HAND = 15,
        BEACON_CHANNEL_STARBOARD_HAND = 16,
        BEACON_ISOLATED_DANGER = 17,
        BEACON_SAFE_WATER = 18,
        BEACON_SPECIAL_MARK = 19,
        CARDINAL_MARK_N = 20,
        CARDINAL_MARK_E = 21,
        CARDINAL_MARK_S = 22,
        CARDINAL_MARK_W = 23,
        PORT_HAND_MARK = 24,
        STARBOARD_HAND_MARK = 25,
        PREFERRED_HAND_PORT_HAND = 26,
        PREFERRED_HAND_STARBOARD_HAND = 27,
        ISOLATED_DANGER = 28,
        SAFE_WATER = 29,
        SPECIAL_MARK = 30,
        LIGHT_VESSEL = 31
    }

    public enum TransmitMode : int
    {
        TXA_TXB_RXA_RXB = 0,
        TXA_RXA_RXB = 1,
        TXB_RXA_RXB = 2,
        RESERVED = 3
    }

    public class StationType
    {
        public enum StationTypeEnum : int
        {
            ALL = 0,
            RESERVED = 1,
            CLASS_B_ALL = 2,
            SAR_AIRBORNE = 3,
            AID_NAV = 4,
            CLASS_B_SHIPBORNE = 5,
            REGIONAL = 6
        }

        public StationTypeEnum GetStationType(int value)
        {
            if (Enum.IsDefined(typeof(StationTypeEnum), value))
                return (StationTypeEnum)value;

            if (IS(6, 9, value))
                return StationTypeEnum.REGIONAL;
            if (IS(10, 15, value))
                return StationTypeEnum.RESERVED;
            return StationTypeEnum.ALL;
        }

        private bool IS(int a, int b, int value)
        {
            return (a <= value) && (value <= b);
        }
    }
    
    public enum StationIntervals : int
    {
        AUTONOMOUS_MODE = 0,
        MINUTES_10 = 1,
        MINUTES_6 = 2,
        MINUTES_3 = 3,
        MINUTES_1 = 4,
        SECONDS_30 = 5,
        SECONDS_15 = 6,
        SECONDS_10 = 7,
        SECONDS_5 = 8,
        NEXT_SHORT_REPORTER_INTERVAL = 9,
        NEXT_LONGER_REPORTING_INTERVAL = 10,
        RESERVED = 11
    }
}
