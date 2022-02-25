using System;
using System.Collections.Generic;
using System.Text;

namespace CAIS
{
    public static class Ex
    {
        public static T[] SubArray<T>(this T[] data, int start, int end)
        {
            List<T> result = new List<T>();
            for (int i = start; i < end; i++)
            {
                result.Add(data[i]);
            }
            return result.ToArray();
        }
        public static T[] Reverse<T>(this T[] data)
        {
            T[] result = new T[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                result[data.Length - 1 - i] = data[i];
            }
            return result;
        }

        public static string[] Split(this string data, int n)
        {
            List<string> res = new List<string>();
            for (int i = 0; i < data.Length; i+=6)
            {
                if (data.Length - i >= n)
                    res.Add(data.Substring(i, n));
                else
                    res.Add(data.Substring(i));
            }
            return res.ToArray();
        }
    }
}
