using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CML
{
    public class ComFile
    {
        public static void New(string path, byte[] buff)
        {
            using (FileStream fs = File.Create(path))
            {
                fs.Write(buff, 0, buff.Length);
            }
        }

        public static void New(string path, string content)
        {
            New(path, content, Encoding.UTF8);
        }

        public static void New(string path, string content, Encoding encode)
        {
            using (StreamWriter sw = new StreamWriter(File.Create(path), encode))
            {
                sw.Write(content);
            }
        }

        public static void Renew(string path, string content)
        {
            Renew(path, content, Encoding.UTF8);
        }

        public static void Renew(string path, string content, Encoding encode)
        {
            if (File.Exists(path)) { File.Delete(path); }

            New(path, content, encode);
        }

        public static string Read(string path, Encoding encode)
        {
            string content = string.Empty;
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path, encode))
                {
                    content = sr.ReadToEnd();
                }
            }

            return content;
        }

        public static string Read(string path)
        {
            return Read(path, Encoding.UTF8);
        }

        public static void Write(string path, byte[] buff)
        {
            using (FileStream fs = new FileStream(path, System.IO.FileMode.OpenOrCreate))
            {
                Write(fs, buff);
            }
        }

        public static void Write(Stream sm, byte[] buff)
        {
            sm.Write(buff, 0, buff.Length);
            sm.SetLength(buff.Length);
        }

        public static void Write(string path, string content)
        {
            Write(path, CML.ComUtility.GetBytesFromTxt(content));
        }

        public static void Write(Stream sm, string content)
        {
            Write(sm, CML.ComUtility.GetBytesFromTxt(content));
        }
    }
}
