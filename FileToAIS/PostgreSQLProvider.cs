using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAIS;
using Npgsql;

namespace FileToAIS
{
    public class PostgreSQLProvider
    {
        string Host { get; set; } = "localhost";
        string Username { get; set; } = "postgres";
        string Password { get; set; } = "toor";
        string Database { get; set; } = "AIS";
        int Port { get; set; } = 5432;
        NpgsqlConnection connection;
        NpgsqlCommand cmd;
        public PostgreSQLProvider()
        {
            string cs = $"Host={Host};Port={Port};Username={Username};Password={Password};Database={Database}";
            connection = new NpgsqlConnection(cs);
            connection.Open();
            cmd = new NpgsqlCommand(null, connection);
        }
        public PostgreSQLProvider(string? Host = null, string? Username = null, string? Password = null, string? Database = null)
        {
            if (!string.IsNullOrEmpty(Host))
                this.Host = Host;
            if (!string.IsNullOrEmpty(Username))
                this.Username = Username;
            if (!string.IsNullOrEmpty(Password))
                this.Password = Password;
            if (!string.IsNullOrEmpty(Database))
                this.Database = Database;
            string cs = $"Host={Host};Port={Port};Username={Username};Password={Password};Database={Database}";
            connection = new NpgsqlConnection(cs);
            connection.Open();
            cmd = new NpgsqlCommand(null, connection);
        }
        
        public void test()
        {
            //Console.WriteLine(GetVesselID("roles", 123, 0032321, "sda", 0));
            var sql = $"SELECT id FROM vessels WHERE mmsi = {213123123}";
            using var cmd = new NpgsqlCommand(sql, connection);

            var res = cmd.ExecuteReader();
            while (res.Read())
            {
                Console.WriteLine("while");
                Console.WriteLine(res.GetInt32(res.GetOrdinal("id")));
            }
            Console.WriteLine($"PostgreSQL version:  {res.GetInt32(res.GetOrdinal("id"))}");
            Console.ReadKey();
        }

        public long GetVesselID(string mmsi)
        {
            NpgsqlCommand cmd1;
            string cs = $"Host={Host};Port={Port};Username={Username};Password={Password};Database={Database}";
            using (NpgsqlConnection connection1 = new NpgsqlConnection(cs))
            {
                connection1.Open();
                cmd1 = new NpgsqlCommand(null, connection);
                long id = -1;
                var sql = $"SELECT id FROM vessels WHERE mmsi = {mmsi}";
                cmd1.CommandText = sql;
                {
                    using (NpgsqlDataReader res = cmd1.ExecuteReader())
                        while (res.Read())
                        {
                            id = res.GetInt64(res.GetOrdinal("id"));
                            //Console.WriteLine(id);
                            return id;
                        }
                }
            }
            //await System.Threading.Tasks.Task.Delay(1000);
            return CreateVessel(mmsi);
        }

        private long CreateVessel(string mmsi)
        {
            long id = -1;
            var sql = $"INSERT INTO public.vessels (mmsi) " +
                $"VALUES('{mmsi}') RETURNING id;";
            cmd.CommandText = sql;
            {
                var res = cmd.ExecuteScalar();
                id = Convert.ToInt64(res);
                return id;
            }
        }

        public void PushMessage(Message mes)
        {
            if (mes == null) return;
            if (mes.MessageType == (AISType) 0) return;
            long vessel = GetVesselID(mes.Decoded.mmsi);
            string dopvalue = string.Empty;
            switch (mes.MessageType)
            {

                case AISType.PositionReportClassA1:
                case AISType.PositionReportClassA2:
                case AISType.PositionReportClassA3:
                    dopvalue = PushDecoded((PosReportClassA)mes.Decoded, vessel);
                    break;
                case AISType.BaseStationReport:
                    dopvalue = PushDecoded((BaseStationReport)mes.Decoded, vessel);
                    break;
                case AISType.StandardClassBCSPositionReport:
                    dopvalue = PushDecoded((PosReportClassB)mes.Decoded, vessel);
                    break;
                case AISType.StaticAndVoyageRelatedData:
                    dopvalue = PushDecoded((StaticAndVoyageRelatedData)mes.Decoded, vessel);
                    break;
                case AISType.BinaryAddressedMessage:
                    dopvalue = PushDecoded((BinaryAddressedMessage)mes.Decoded, vessel);
                    break;
                case AISType.BinaryBroadcastMessage:
                    dopvalue = PushDecoded((BinaryBroadcastMessage)mes.Decoded, vessel);
                    break;
                case AISType.StandardSARAircraftPositionReport:
                    dopvalue = PushDecoded((StandardSARAircraftPositionReport)mes.Decoded, vessel);
                    break;
                case AISType.UTCDateInquiry:
                    dopvalue = PushDecoded((UTCDatelnquiry)mes.Decoded, vessel);
                    break;
                case AISType.UTCDateResponse:
                    dopvalue = PushDecoded((UTCDateResponse)mes.Decoded, vessel);
                    break;
                case AISType.AssignmentModeCommand:
                    dopvalue = PushDecoded((AssignmentModeCommand)mes.Decoded, vessel);
                    break;
                case AISType.DGNSSBroadcastBinaryMessage:
                    dopvalue = PushDecoded((DGNSSBroadcastBinaryMessage)mes.Decoded, vessel);
                    break;
                case AISType.ExtendedClassBCSPositionReport:
                    dopvalue = PushDecoded((ExtendedPosReportClassВ)mes.Decoded, vessel);
                    break;
                case AISType.DataLinkManagementMessage:
                    dopvalue = PushDecoded((DataLinkManagementMessage)mes.Decoded, vessel);
                    break;
                case AISType.AidToNavigationReport:
                    dopvalue = PushDecoded((AidToNavigationReport)mes.Decoded, vessel);
                    break;
                case AISType.ChannelManagement:
                    dopvalue = PushDecoded((ChannelManagement)mes.Decoded, vessel);
                    break;
                case AISType.StaticDataReport:
                    dopvalue = PushDecoded((StaticDataReport)mes.Decoded, vessel);
                    break;
                case AISType.SingleSlotBinaryMessage:
                    dopvalue = PushDecoded((SingleSlotBinaryMessage)mes.Decoded, vessel);
                    break;
                case AISType.AddressedSafetyRelatedMessage:
                    dopvalue = PushDecoded((AddressedSafetyRelatedMessage)mes.Decoded, vessel);
                    break;
                case AISType.BinaryAcknowledge:
                    dopvalue = PushDecoded((BinaryAcknowledge)mes.Decoded, vessel);
                    break;
                case AISType.GroupAssignmentCommand:
                    dopvalue = PushDecoded((GroupAssignmentCommand)mes.Decoded, vessel);
                    break;
                case AISType.Interrogation:
                    dopvalue = PushDecoded((Interrogation)mes.Decoded, vessel);
                    break;
                case AISType.LongRangeAISBroadcastMessage:
                    dopvalue = PushDecoded((LongRangeAISBroadcastMessage)mes.Decoded, vessel);
                    break;
                case AISType.MultipleSlotBinaryMessage:
                    dopvalue = PushDecoded((MultipleSlotBinaryMessage)mes.Decoded, vessel);
                    break;
                case AISType.SafetyRelatedAcknowledgment:
                    dopvalue = PushDecoded((SafetyRelatedAcknowledgment)mes.Decoded, vessel);
                    break;
                case AISType.SafetyRelatedBroadcastMessage:
                    dopvalue = PushDecoded((SafetyRelatedBroadcastMessage)mes.Decoded, vessel);
                    break;
                default:
                    break;
            }
            PushNMEA(mes.NMEA, vessel,dopvalue);
        }
        public void PushNMEA(NMEA nmea, long vessel)
        {
            var sql = $"INSERT INTO public.nmea(\"type\", \"raw\", vessel) " +
                $"VALUES('{nmea.ais_id}', '{nmea.raw}', '{vessel}');";
            cmd.CommandText = sql;
            cmd.ExecuteScalar();
        }

        public void PushNMEA(NMEA nmea, long vessel,string dopvalue)
        {
            var sql = $"INSERT INTO public.nmea(\"type\", \"raw\", vessel, \"data\") " +
                $"VALUES('{nmea.ais_id}', '{nmea.raw}', '{vessel}', '{dopvalue}');";
            cmd.CommandText = sql;
            var res = cmd.ExecuteScalar();
            Convert.ToString(res);
        }

        public string PushDecoded(PosReportClassA pos, long vessel)
        {
            string id = string.Empty;
            string sql = $"INSERT INTO public.\"PosReportClassA\" (" +
                $"vessel, status, turn, speed, accuracy, lon, lat, course, heading, \"second\", maneuver, raim, radio)	" +
                $"VALUES ('{vessel}', '{pos.status}', '{pos.turn}', '{pos.speed.ToString().Replace(',','.')}', " +
                $"'{pos.accuracy}', '{pos.lon.ToString().Replace(',', '.')}', '{pos.lat.ToString().Replace(',', '.')}', " +
                $"'{pos.course.ToString().Replace(',', '.')}', '{pos.heading}', '{pos.second}', '{pos.maneuver}', '{pos.raim}', " +
                $"'{pos.radio}') RETURNING id;";
            cmd.CommandText = sql;
            var res = cmd.ExecuteScalar();
            id = Convert.ToString(res);
            return id;
        }
        public string PushDecoded(BaseStationReport rep, long vessel)
        {
            string id = string.Empty;
            string sql = $"INSERT INTO public.\"BaseStationReport\"(" +
                "vessel, repeat, \"year\", \"month\", \"day\", \"hour\", \"minute\", \"second\", accuracy, lon, lat, epfd, raim, radio)" +
                $"VALUES( '{vessel}', '{rep.repeat}', '{rep.year}', '{rep.month}', '{rep.day}', '{rep.hour}', '{rep.minute}', " +
                $"'{rep.second}', '{rep.accuracy}', '{rep.lon.ToString().Replace(',', '.')}', '{rep.lat.ToString().Replace(',', '.')}', " +
                $"'{rep.epfd}', '{rep.raim}', '{rep.radio}') RETURNING id;";
            cmd.CommandText = sql;
            var res = cmd.ExecuteScalar();
            id = Convert.ToString(res);
            return id;
        }
        public string PushDecoded(PosReportClassB rep, long vessel)
        {
            string id = string.Empty;
            string sql = $"INSERT INTO public.\"PosReportClassB\"("+
                "vessel, speed, accuracy, lon, lat, course, heading, \"second\", regional, cs, display, dsc, band, " +
                "msg22, assigned, raim, radio)"+
	            $"VALUES('{vessel}', '{rep.speed.ToString().Replace(',', '.')}', '{rep.accuracy}', '{rep.lon.ToString().Replace(',', '.')}', " +
                $"'{rep.lat.ToString().Replace(',', '.')}', '{rep.course.ToString().Replace(',', '.')}', '{rep.heading}', '{rep.second}', " +
                $"'{rep.regional}', '{rep.cs}', '{rep.display}', '{rep.dsc}', '{rep.band}', " +
                $"'{rep.msg22}', '{rep.assigned}', '{rep.raim}', '{rep.radio}') RETURNING id;";
            cmd.CommandText = sql;
            var res = cmd.ExecuteScalar();
            id = Convert.ToString(res);
            return id;
        }
        public string PushDecoded(StaticAndVoyageRelatedData rep, long vessel)
        {
            string id = string.Empty;
            string sql = $"INSERT INTO public.\"StaticAndVoyageRelatedData\"("+
                "vessel, repeat, ais_version, imo, \"callsign\", \"shipname\", shiptype, to_bow, to_stern, to_port, to_starboard, " +
                "epfd, \"month\", \"day\", \"hour\", \"minute\", draught, \"destination\", dte)"+
	            $"VALUES('{vessel}', '{rep.repeat}', '{rep.ais_version}', '{rep.imo}', '{rep.callsign}', '{rep.shipname}', '{rep.shiptype}', " +
                $"'{rep.to_bow}', '{rep.to_stern}', '{rep.to_port}', '{rep.to_starboard}', '{rep.epfd}', '{rep.month}', '{rep.day}', '{rep.hour}', " +
                $"'{rep.minute}', '{rep.draught.ToString().Replace(',', '.')}', '{rep.destination}', '{rep.dte}') RETURNING id;";
            cmd.CommandText = sql;
            var res = cmd.ExecuteScalar();
            id = Convert.ToString(res);
            sql = $"UPDATE public.vessels " +
                $"SET name ='{rep.shipname}', imo ='{rep.imo}', callsign ='{rep.callsign}', \"type\" ='{rep.shiptype}' " +
                $"WHERE id = {vessel}; ";
            cmd.CommandText = sql;
            cmd.ExecuteScalar();
            return id;
        }
        public string PushDecoded(BinaryAddressedMessage rep, long vessel)
        {
            string id = string.Empty;
            string sql = $"INSERT INTO public.\"BinaryAddressedMessage\"(" +
                "vessel, repeat, seqno, dest_mmsi, retransmit, dac, fid, \"data\")" +
	            $"VALUES('{vessel}', '{rep.repeat}', '{rep.seqno}', '{rep.dest_mmsi ?? "0"}', '{rep.retransmit}', '{rep.dac}', '{rep.fid}', " +
                $"'{rep.data}') RETURNING id;";
            cmd.CommandText = sql;
            var res = cmd.ExecuteScalar();
            id = Convert.ToString(res);
            return id;
        }

        public string PushDecoded(BinaryBroadcastMessage rep, long vessel)
        {
            string id = string.Empty;
            string sql = $"INSERT INTO public.\"BinaryBrodcastMessage\"(" +
                "vessel, repeat, dac, fid, \"data\")" +
                $"VALUES('{vessel}', '{rep.repeat}', '{rep.dac}', '{rep.fid}', '{rep.data}') RETURNING id;";
            cmd.CommandText = sql;
            var res = cmd.ExecuteScalar();
            id = Convert.ToString(res);
            return id;
        }

        public string PushDecoded(ChannelManagement rep, long vessel)
        {
            string id = string.Empty;
            string sql = $"INSERT INTO public.\"ChannelManagement\"(" +
                "vessel, repeat, channel_a, channel_b, txrx, \"power\", addressed, band_a, band_b, zonesize, ne_lon, ne_lat, sw_lon, sw_lat)" +
                $"VALUES('{vessel}', '{rep.repeat}', '{rep.channel_a}', '{rep.channel_b}', '{rep.txrx}', '{rep.power}', '{rep.addressed}', " +
                $"'{rep.band_a}', '{rep.band_b}', '{rep.zonesize}', '{rep.ne_lon.ToString().Replace(',', '.')}', " +
                $"'{rep.ne_lat.ToString().Replace(',', '.')}', '{rep.sw_lon.ToString().Replace(',', '.')}', " +
                $"'{rep.sw_lat.ToString().Replace(',', '.')}') RETURNING id;";
            cmd.CommandText = sql;
            var res = cmd.ExecuteScalar();
            id = Convert.ToString(res);
            return id;
        }

        public string PushDecoded(DGNSSBroadcastBinaryMessage rep, long vessel)
        {
            string id = string.Empty;
            string sql = $"INSERT INTO public.\"DGNSSBroadcastBinaryMessage\"(" +
                "vessel, repeat, lon, lat, \"data\")" +
                $"VALUES('{vessel}', '{rep.repeat}', '{rep.lon}', '{rep.lat}', '{rep.data}') RETURNING id;";
            cmd.CommandText = sql;
            var res = cmd.ExecuteScalar();
            id = Convert.ToString(res);
            return id;
        }

        public string PushDecoded(DataLinkManagementMessage rep, long vessel)
        {
            string id = string.Empty;
            string sql = $"INSERT INTO public.\"DataLinkManagementMessage\"(" +
                "vessel, repeat, offset1, number1, timeout1, increment1, offset2, number2, timeout2, increment2, offset3, " +
                "number3, timeout3, increment3, offset4, number4, timeout4, increment4)" +
                $"VALUES('{vessel}', '{rep.repeat}', '{rep.offset1}', '{rep.number1}', '{rep.timeout1}', '{rep.increment1}', " +
                $"'{rep.offset2}', '{rep.number2}', '{rep.timeout2}', '{rep.increment2}', '{rep.offset3}', '{rep.number3}', '{rep.timeout3}', " +
                $"'{rep.increment3}', '{rep.offset4}', '{rep.number4}', '{rep.timeout4}', '{rep.increment4}') RETURNING id;";
            cmd.CommandText = sql;
            var res = cmd.ExecuteScalar();
            id = Convert.ToString(res);
            return id;
        }

        public string PushDecoded(ExtendedPosReportClassВ rep, long vessel)
        {
            string id = string.Empty;
            string sql = $"INSERT INTO public.\"ExtendedPosReportClassB\"(" +
                "vessel, repeat, speed, accuracy, lon, lat, course, heading, \"second\", regional, shipname, shiptype, to_bow, " +
                "to_stern, to_port, to_starboard, epfd, dte, assigned)" +
                $"VALUES('{vessel}', '{rep.repeat}', '{rep.speed.ToString().Replace(',', '.')}', '{rep.accuracy}', " +
                $"'{rep.lon.ToString().Replace(',', '.')}', '{rep.lat.ToString().Replace(',', '.')}', " +
                $"'{rep.course.ToString().Replace(',', '.')}', '{rep.heading}', '{rep.second}', '{rep.regional}', '{rep.shipname}', '{rep.shiptype}', " +
                $"'{rep.to_bow}', '{rep.to_stern}', '{rep.to_port}', '{rep.to_starboard}', '{rep.epfd}', '{rep.dte}', '{rep.assigned}') RETURNING id;";
            cmd.CommandText = sql;
            var res = cmd.ExecuteScalar();
            id = Convert.ToString(res);
            sql = $"UPDATE public.vessels " +
                $"SET name = '{rep.shipname}', type = '{rep.shiptype}'" +
                $"WHERE id = {vessel}; ";
            cmd.CommandText = sql;
            cmd.ExecuteScalar();
            return id;
        }

        public string PushDecoded(SingleSlotBinaryMessage rep, long vessel)
        {
            string id = string.Empty;
            string sql = $"INSERT INTO public.\"SingleSlotBinaryMessage\"(" +
                "vessel, repeat, addressed, structured, \"data\")" +
                $"VALUES('{vessel}', '{rep.repeat}', '{rep.addressed}', '{rep.structured}', '{rep.data}') RETURNING id;";
            cmd.CommandText = sql;
            var res = cmd.ExecuteScalar();
            id = Convert.ToString(res);
            return id;
        }

        public string PushDecoded(StandardSARAircraftPositionReport rep, long vessel)
        {
            string id = string.Empty;
            string sql = $"INSERT INTO public.\"StandardSARAircraftPositionReport\"(" +
                "vessel, repeat, alt, speed, accuracy, lon, lat, course, \"second\", dte, assigned, raim, radio)" +
                $"VALUES('{vessel}', '{rep.repeat}', '{rep.alt}', '{rep.speed}', '{rep.accuracy}', " +
                $"'{rep.lon.ToString().Replace(',', '.')}', '{rep.lat.ToString().Replace(',', '.')}', '{rep.course.ToString().Replace(',', '.')}', " +
                $"'{rep.second}', '{rep.dte}', '{rep.assigned}', '{rep.raim}', '{rep.radio}') RETURNING id;";
            cmd.CommandText = sql;
            var res = cmd.ExecuteScalar();
            id = Convert.ToString(res);
            return id;
        }

        public string PushDecoded(StaticDataReport rep, long vessel)
        {
            string id = string.Empty;
            string sql = $"INSERT INTO public.\"StaticDataReport\"(" +
                "vessel, repeat, partno, shiptype, vendorid, model, \"serial\", callsign, to_bow, to_stern, to_port, to_starboard, " +
                "mothership_mmsi)" +
                $"VALUES('{vessel}', '{rep.repeat}', '{rep.partno}', '{rep.shiptype}', '{rep.vendorid}', '{rep.model}', '{rep.serial}', " +
                $"'{rep.callsign}', '{rep.to_bow}', '{rep.to_stern}', '{rep.to_port}', '{rep.to_starboard}', '{rep.mothership_mmsi ?? "0"}') RETURNING id;";
            cmd.CommandText = sql;
            var res = cmd.ExecuteScalar();
            id = Convert.ToString(res);
            sql = $"UPDATE public.vessels " +
                $"SET callsign = '{rep.callsign}', type = '{rep.shiptype}' " +
                $"WHERE id = {vessel}; ";
            cmd.CommandText = sql;
            cmd.ExecuteScalar();
            return id;
        }

        public string PushDecoded(UTCDateResponse rep, long vessel)
        {
            string id = string.Empty;
            string sql = $"INSERT INTO public.\"UTCDateResponse\"(" +
                "vessel, repeat, \"year\", \"month\", \"day\", \"hour\", \"minute\", \"second\", accuracy, lon, lat, epfd, raim, radio)" +
                $"VALUES('{vessel}', '{rep.repeat}', '{rep.year}', '{rep.month}', '{rep.day}', '{rep.hour}', '{rep.minute}', " +
                $"'{rep.second}', '{rep.accuracy}', '{rep.lon.ToString().Replace(',', '.')}', '{rep.lat.ToString().Replace(',', '.')}', " +
                $"'{rep.epfd}', '{rep.raim}', '{rep.radio}') RETURNING id;";
            cmd.CommandText = sql;
            var res = cmd.ExecuteScalar();
            id = Convert.ToString(res);
            return id;
        }

        public string PushDecoded(UTCDatelnquiry rep, long vessel)
        {
            string id = string.Empty;
            string sql = $"INSERT INTO public.\"UTCDatelnquiry\"(" +
                "vessel, repeat, dest_mmsi)" +
                $"VALUES('{vessel}', '{rep.repeat}', '{rep.dest_mmsi}') RETURNING id;";
            cmd.CommandText = sql;
            var res = cmd.ExecuteScalar();
            id = Convert.ToString(res);
            return id;
        }

        public string PushDecoded(AssignmentModeCommand rep, long vessel)
        {
            string id = string.Empty;
            string sql = $"INSERT INTO public.\"AssignmentModeCommand\"(" +
                "vessel, repeat, mmsi, mmsi1, offset1, increment1, mmsi2, offset2, increment2)" +
                $"VALUES('{vessel}', '{rep.repeat}', '{rep.mmsi ?? "0"}', '{rep.mmsi1 ?? "0"}', '{rep.offset1}', " +
                $"'{rep.increment1}', '{rep.mmsi2 ?? "0"}', '{rep.offset2}', '{rep.increment2}') RETURNING id;";
            cmd.CommandText = sql;
            var res = cmd.ExecuteScalar();
            id = Convert.ToString(res);
            return id;
        }

        public string PushDecoded(AidToNavigationReport rep, long vessel)
        {
            string id = string.Empty;
            string sql = $"INSERT INTO public.\"AidToNavigationReport\"(" +
                "vessel, repeat, aid_type, \"name\", accuracy, lon, lat, to_bow, to_stern, to_port, to_starboard, epfd, " +
                "\"second\", off_position, regional, raim, virtual_aid, assigned, name_extension)" +
                $"VALUES('{vessel}', '{rep.repeat}', '{rep.aid_type}', '{rep.name}', '{rep.accuracy}', '{rep.lon.ToString().Replace(',', '.')}'," +
                $" '{rep.lat.ToString().Replace(',', '.')}', '{rep.to_bow}', " +
                $"'{rep.to_stern}', '{rep.to_port}', '{rep.to_starboard}', '{rep.epfd}', '{rep.second}', '{rep.off_position}', '{rep.regional}', " +
                $"'{rep.raim}', '{rep.virtual_aid}', '{rep.assigned}', '{rep.name_extension}') RETURNING id;";
            cmd.CommandText = sql;
            var res = cmd.ExecuteScalar();
            id = Convert.ToString(res);
            return id;
        }

        public string PushDecoded(AddressedSafetyRelatedMessage rep, long vessel)
        {
            string id = string.Empty;
            string sql = $"INSERT INTO public.\"AddressedSafetyRelatedMessage\"(" +
                "vessel, repeat, seqno, dest_mmsi, retransmit, \"text\") " +
                $"VALUES('{vessel}', '{rep.repeat}', '{rep.seqno}', '{rep.dest_mmsi ?? "0"}', '{rep.retransmit}', '{rep.text}') RETURNING id;";
            cmd.CommandText = sql;
            var res = cmd.ExecuteScalar();
            id = Convert.ToString(res);
            return id;
        }

        public string PushDecoded(BinaryAcknowledge rep, long vessel)
        {
            string id = string.Empty;
            string sql = $"INSERT INTO public.\"BinaryAcknowledge\"(" +
                "vessel, repeat, mmsi1, mmsiseq1, mmsi2, mmsiseq2, mmsi3, mmsiseq3, mmsi4, mmsiseq4) " +
                $"VALUES('{vessel}', '{rep.repeat}', '{rep.mmsi1 ?? "0"}', '{rep.mmsiseq1}', '{rep.mmsi2 ?? "0"}', '{rep.mmsiseq2}', " +
                $"'{rep.mmsi3 ?? "0"}', '{rep.mmsiseq3}', '{rep.mmsi4 ?? "0"}', '{rep.mmsiseq4}') RETURNING id;";
            cmd.CommandText = sql;
            var res = cmd.ExecuteScalar();
            id = Convert.ToString(res);
            return id;
        }

        public string PushDecoded(GroupAssignmentCommand rep, long vessel)
        {
            string id = string.Empty;
            string sql = $"INSERT INTO public.\"GroupAssignmentCommand\"(" +
                "vessel, repeat, ne_lon, ne_lat, sw_lon, sw_lat, station_type, shiptype, txrx, \"interval\", quiet) " +
                $"VALUES('{vessel}', '{rep.repeat}', '{rep.ne_lon.ToString().Replace(',', '.')}', " +
                $"'{rep.ne_lat.ToString().Replace(',', '.')}', '{rep.sw_lon.ToString().Replace(',', '.')}', " +
                $"'{rep.sw_lat.ToString().Replace(',', '.')}', '{rep.station_type}', '{rep.shiptype}', '{rep.txrx}'," +
                $"'{rep.interval}', '{rep.quiet}') RETURNING id;";
            cmd.CommandText = sql;
            var res = cmd.ExecuteScalar();
            id = Convert.ToString(res);
            return id;
        }

        public string PushDecoded(Interrogation rep, long vessel)
        {
            string id = string.Empty;
            string sql = $"INSERT INTO public.\"Interrogation\"(" +
                "vessel, repeat, mmsi1, type1_1, offset1_1, type1_2, offset1_2, mmsi2, type2_1, offset2_1) " +
                $"VALUES('{vessel}', '{rep.repeat}', '{rep.mmsi1}', '{rep.type1_1}', '{rep.offset1_1}', '{rep.type1_2}', '{rep.offset1_2}'," +
                $"'{rep.mmsi2}', '{rep.type2_1}', '{rep.offset2_1}') RETURNING id;";
            cmd.CommandText = sql;
            var res = cmd.ExecuteScalar();
            id = Convert.ToString(res);
            return id;
        }
        public string PushDecoded(LongRangeAISBroadcastMessage rep, long vessel)
        {
            string id = string.Empty;
            string sql = $"INSERT INTO public.\"LongRangeAISBroadcastMessage\"(" +
                "vessel, repeat, accuracy, raim, status, lon, lat, speed, course, gnss) " +
                $"VALUES('{vessel}', '{rep.repeat}', '{rep.accuracy}', '{rep.raim}', '{rep.status}', " +
                $"'{rep.lon.ToString().Replace(',', '.')}', '{rep.lat.ToString().Replace(',', '.')}'," +
                $"'{rep.speed}', '{rep.course}', '{rep.gnss}') RETURNING id;";
            cmd.CommandText = sql;
            var res = cmd.ExecuteScalar();
            id = Convert.ToString(res);
            return id;
        }

        public string PushDecoded(MultipleSlotBinaryMessage rep, long vessel)
        {
            string id = string.Empty;
            string sql = $"INSERT INTO public.\"MultipleSlotBinaryMessage\"(" +
                "vessel, repeat, addressed, structured, dest_mmsi, app_id, \"data\", radio) " +
                $"VALUES('{vessel}', '{rep.repeat}', '{rep.addressed}', '{rep.structured}', '{rep.dest_mmsi}', " +
                $"'{rep.app_id}', '{rep.data}', '{rep.radio}') RETURNING id;";
            cmd.CommandText = sql;
            var res = cmd.ExecuteScalar();
            id = Convert.ToString(res);
            return id;
        }

        public string PushDecoded(SafetyRelatedAcknowledgment rep, long vessel)
        {
            string id = string.Empty;
            string sql = $"INSERT INTO public.\"SafetyRelatedAcknowledgment\"(" +
                "vessel, repeat,  mmsi1, mmsiseq1, mmsi2, mmsiseq2, mmsi3, mmsiseq3, mmsi4, mmsiseq4) " +
                $"VALUES('{vessel}', '{rep.repeat}', '{rep.mmsi1}', '{rep.mmsiseq1}', '{rep.mmsi2}', '{rep.mmsiseq2}', " +
                $"'{rep.mmsi3}', '{rep.mmsiseq3}', '{rep.mmsi4}', '{rep.mmsiseq4}') RETURNING id;";
            cmd.CommandText = sql;
            var res = cmd.ExecuteScalar();
            id = Convert.ToString(res);
            return id;
        }

        public string PushDecoded(SafetyRelatedBroadcastMessage rep, long vessel)
        {
            string id = string.Empty;
            string sql = $"INSERT INTO public.\"SafetyRelatedBroadcastMessage\"(" +
                "vessel, repeat, \"text\") " +
                $"VALUES('{vessel}', '{rep.repeat}', '{rep.text}') RETURNING id;";
            cmd.CommandText = sql;
            var res = cmd.ExecuteScalar();
            id = Convert.ToString(res);
            return id;
        }

        public void PushError(string AISMessage, string ErrorMessage)
        {
            string sql = $"INSERT INTO public.\"ErrorsList\"(" +
                "\"raw\", \"ErrorMessage\") " +
                $"VALUES ('{AISMessage}', '{ErrorMessage}');";
            cmd.CommandText = sql;
            cmd.ExecuteScalar();
        }

        ~PostgreSQLProvider()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
    }
}
