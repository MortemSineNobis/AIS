using System;

namespace CAIS
{
    public class NMEA
    {
        public int ais_id { get; set; }
        public string raw { get; set; }
        public TalkerIDClass.TalkerID talker { get; set; }
        public string type { get; set; }
        public int message_fragments { get; set; }
        public int fragment_number { get; set; }
        public int? message_id { get; set; }
        public string channel { get; set; }
        public string payload { get; set; }
        public int fill_bits { get; set; }
        public int checksum { get; set; }
        public string bit_array { get; set; }
        public NMEA()
        {

        }

        public NMEA(string raw)
        {
            this.checksum = -1;
            this.raw = raw;
            string[] values = raw.Split(',');
            
            string head = values[0];
            string message_fragments = values[1];
            string fragment_number = values[2];
            string message_id = values[3];
            string channel = values[4];
            string payload = values[5];
            string checksum = values[6];

            talker = TalkerIDClass.GetTalkerID(head.Substring(1, 2));
            type = head.Substring(3);
            this.message_fragments = Convert.ToInt32(message_fragments);
            this.fragment_number = Convert.ToInt32(fragment_number);
            if (!string.IsNullOrEmpty(message_id))
                this.message_id = Convert.ToInt32(message_id);
            else
                this.message_id = null;
            this.channel = channel;
            this.payload = payload;
            fill_bits = (int)checksum[0];
            this.checksum = Convert.ToInt32(checksum.Substring(2), 16);
            bit_array = Decode.decode_into_bit_array(payload);
            ais_id = Decode.GetType(bit_array);

            if (Decoder.UsingPostgree)
            {
                raw = raw.Replace("'", "''");
                payload = payload.Replace("'", "''");
            }
        }
    }

    
}
