using System;
using System.Collections.Generic;
using System.Text;

namespace CAIS
{
    class Utils
    {
        private const string Chars = "@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_ !\"#$%&'()*+,-./0123456789:;<=>?";
        public string Data { get; set; } = string.Empty;
        public static bool Postgres = false;
        public Utils(string data)
        {
            Data = data;
            Postgres = Decoder.UsingPostgree;
        }

        private int FromBytesSigned(string data) => Convert.ToInt32(data, 2);
        public int GetInt(int ix_low, int ix_high, bool signed = false)
        {
            var data = Data;
            //int shift = (8 - ((ix_high - ix_low) % 8)) % 8; ХЗ зачем
            int length = ix_high - ix_low;
            if (Data.Length < ix_high || ix_low > ix_high) return 0;
            data = data.Substring(ix_low, length);
            if (signed)
                if (data[0] == '1')
                    data = data.PadLeft(32, '1');
            
            //Console.WriteLine($"low = {ix_low}, high = {ix_high} | data = {data}");
            return FromBytesSigned(data);
        }
        public string GetMMSI(int ix_low, int ix_high)
        {
            var data = Data;
            //int length = ix_high - ix_low;
            //data = data.Substring(ix_low, length);
            int mmsi = GetInt(ix_low, ix_high);
            return mmsi.ToString().PadLeft(9, '0');
        }

        public string GetString(int ix_low, int ix_high)
        {
            string res = string.Empty;
            int length = ix_high - ix_low;
            if (Data.Length < ix_high || ix_low > ix_high) return string.Empty;
            var data = Data.Substring(ix_low, length).Split(6);
            foreach (var item in data)
            {
                int n = FromBytesSigned(item);
                if (n == 0)
                    break;
                res += Chars[n];
            }
            if (Postgres)
            {
                res = res.Replace("'", "''");
            }
            return res.Trim();
        }

        public int GetSmallInt(int index)
        {
            if (Data.Length < index + 1) return 0;
            return Data[index] - 48;
        }

        public string GetSubstring(int ix_low, int ix_high = -1)
        {
            string res = string.Empty;
            if (ix_high == -1)
            {
                if (Data.Length > ix_low)
                    res = Data.Substring(ix_low);
            }
            else
            {
                int length = ix_high - ix_low;
                if (Data.Length > ix_high)
                    res = Data.Substring(ix_low,length);
            }
            return res;
        }
    }
}
