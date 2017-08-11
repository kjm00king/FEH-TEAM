using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Security.Cryptography;
using System.IO;
using System.IO.Compression;

namespace CML
{
    public class ComUtility
    {
        public static string UniqueID(string CharList = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ")
        {
            var t = DateTime.UtcNow;
            char[] charArray = CharList.ToCharArray();
            var result = new Stack<char>();

            var length = charArray.Length;

            long dgit = 1000000000000L +
                        t.Millisecond * 1000000000L +
                        t.DayOfYear * 1000000L +
                        t.Hour * 10000L +
                        t.Minute * 100L +
                        t.Second;

            while (dgit != 0)
            {
                result.Push(charArray[dgit % length]);
                dgit /= length;
            }
            return new string(result.ToArray());
        }

        public static long GetTimestamp()
        {
            return (long)((DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
        }

        public static DateTime GetTime(long milliseconds)
        {
            return new DateTime(1970, 1, 1).AddMilliseconds(milliseconds);
        }

        public static int QueryCount<T>(IEnumerable<T> query)
        {
            if (query.Any())
                return query.Count();
            else
                return 0;
        }

        public static List<T> QueryList<T>(IEnumerable<T> query)
        {
            if (query.Any())
                return query.ToList();
            else
                return new List<T>();
        }

        public static T QueryFirst<T>(IEnumerable<T> query, T defaultvalue)
        {
            if (query.Any())
                return query.First();
            else
                return defaultvalue;
        }

        public static T QueryFirst<T>(IEnumerable<T> query)
        {
            return QueryFirst<T>(query, (T)Activator.CreateInstance(typeof(T)));
        }

        public static T QueryMax<T>(IEnumerable<T> query, T defaultvalue)
        {
            if (query.Any())
                return query.Max();
            else
                return defaultvalue;
        }

        public static T QueryMin<T>(IEnumerable<T> query, T defaultvalue)
        {
            if (query.Any())
                return query.Min();
            else
                return defaultvalue;
        }

        public static int XInt(string content, int @default)
        {
            int rv = @default;
            if (int.TryParse(content, out rv))
                return rv;
            else
                return @default;
        }

        public static long XLong(string content, long @default)
        {
            long rv = @default;
            if (long.TryParse(content, out rv))
                return rv;
            else
                return @default;
        }

        public static float XFloat(string content, float @default)
        {
            float rv = @default;
            if (float.TryParse(content, out rv))
                return rv;
            else
                return @default;
        }

        public static double XDouble(string content, double @default)
        {
            double rv = @default;
            if (double.TryParse(content, out rv))
                return rv;
            else
                return @default;
        }

        public static bool XBool(string content, bool @default)
        {
            bool rv = @default;
            if (Boolean.TryParse(content, out rv))
                return rv;
            else
                return @default;
        }

        public static int? XNullInt(object content)
        {
            int rv = 0;
            if (Int32.TryParse(content.ToString(), out rv))
                return rv;
            else
                return null;
        }

        public static String ToJson(Object Data)
        {
            if (Data == null)
                return string.Empty;
            else
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(Data);
            }
        }

        public static T DCopy<T>(Object Data)
        {
            string json = ToJson(Data);
            return FromJson<T>(json);
        }

        public static T FromJson<T>(String input)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(input);
        }

        public static Dictionary<string, string> ToDict(String Data)
        {
            return FromJson<Dictionary<string, string>>(Data);
        }

        public static Dictionary<string, string> ToDict(object[,] objs)
        {
            Dictionary<string, string> tmp = new Dictionary<string, string>();

            int row = objs.GetLength(0);
            int col = objs.GetLength(1);

            if (col >= 2)
            {
                for (int i = 1; i <= row; i++)
                {
                    string Key = Convert.ToString(objs[i, 1]);
                    string Value = Convert.ToString(objs[i, 2]);

                    if (!string.IsNullOrEmpty(Key) && !tmp.Keys.Contains(Key))
                    {
                        tmp.Add(Key, Value);
                    }

                }
            }

            return tmp;
        }

        public static Dictionary<string, string>[] ToDictArray(object[,] objs)
        {
            List<Dictionary<string, string>> tmp = new List<Dictionary<string, string>>();

            int row = objs.GetLength(0);
            int col = objs.GetLength(1);

            if (row >= 2)
            {
                for (int i = 2; i <= row; i++)
                {
                    Dictionary<string, string> tmpR = new Dictionary<string, string>();

                    for (int j = 1; j <= col; j++)
                    {
                        string Key = Convert.ToString(objs[1, j]);
                        string Value = Convert.ToString(objs[i, j]);

                        if (!tmpR.Keys.Contains(Key))
                        {
                            tmpR.Add(Key, Value);
                        }
                    }

                    tmp.Add(tmpR);
                }
            }

            return tmp.ToArray();
        }

        public static string GetSHA1(string input)
        {
            return GetSHA1(Encoding.UTF8.GetBytes(input));
        }

        public static string GetSHA1(byte[] input)
        {
            SHA1 algorithm = SHA1.Create();
            byte[] buff = algorithm.ComputeHash(input);
            string output = string.Empty;
            for (int i = 0; i < buff.Length; i++)
            {
                output += buff[i].ToString("x2").ToLowerInvariant();
            }
            return output;
        }

        public static string GetBase64(string content, System.Text.Encoding code)
        {
            return GetBase64(code.GetBytes(content));
        }

        public static string GetBase64(string content)
        {
            return GetBase64(content, Encoding.UTF8);
        }

        public static string GetBase64(object obj)
        {
            if (obj == null)
                return string.Empty;
            else
                return GetBase64(obj.ToString());
        }

        public static string GetBase64(byte[] buff)
        {
            return Convert.ToBase64String(buff);
        }

        public static string GetBase64(MemoryStream ms)
        {
            return GetBase64(ms.GetBuffer());
        }

        public static string GetString(byte[] buff, System.Text.Encoding code)
        {
            return code.GetString(buff);
        }

        public static string GetString(byte[] buff)
        {
            return GetString(buff, Encoding.UTF8);
        }

        public static string GetString(string base64, System.Text.Encoding code)
        {
            try
            {
                return GetString(GetBytesFromBase64(base64), code);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetString(string base64)
        {
            return GetString(base64, Encoding.UTF8);
        }
        
        public static string GetString(MemoryStream ms, System.Text.Encoding code)
        {
            return code.GetString(ms.GetBuffer());
        }

        public static string GetString(MemoryStream ms)
        {
            return GetString(ms.GetBuffer(), Encoding.UTF8);
        }

        public static byte[] GetBytesFromTxt(string content, System.Text.Encoding code)
        {
            return code.GetBytes(content);
        }

        public static byte[] GetBytesFromTxt(string content)
        {
            return GetBytesFromTxt(content, Encoding.UTF8);
        }

        public static byte[] GetBytesFromBase64(string base64)
        {
            return Convert.FromBase64String(base64);
        }

        public static MemoryStream GetStreamFromTxt(string content, System.Text.Encoding code)
        {
            return new MemoryStream(GetBytesFromTxt(content, code));
        }

        public static MemoryStream GetStreamFromTxt(string content)
        {
            return GetStreamFromTxt(content, Encoding.UTF8);
        }

        public static MemoryStream GetStreamFromBase64(string content)
        {
            return new MemoryStream(GetBytesFromBase64(content));
        }

        public static byte[] DecompressByGzip(byte[] buff)
        {
            MemoryStream ms = new MemoryStream();

            using (GZipStream decompressionStream = new GZipStream(new MemoryStream(buff), CompressionMode.Decompress))
            {
                decompressionStream.CopyTo(ms);
            }

            return ms.GetBuffer();
        }

        public static byte[] CompressByGzip(byte[] buff)
        {
            MemoryStream ms = new MemoryStream();

            using (GZipStream compressionStream = new GZipStream(ms, CompressionMode.Compress))
            {
                new MemoryStream(buff).CopyTo(compressionStream);
            }

            return ms.GetBuffer();
        }

        public static string ToBase64Gzip(string txt)
        {
            byte[] buff = GetBytesFromTxt(txt);
            byte[] buffzip = CompressByGzip(buff);
            return GetBase64(buffzip);

        }

        public static string FromBase64Gzip(string base64zip)
        {
            byte[] buffzip =GetBytesFromBase64(base64zip);
            byte[] buff = DecompressByGzip(buffzip);
            return GetString(buff);
        }
    }
}
