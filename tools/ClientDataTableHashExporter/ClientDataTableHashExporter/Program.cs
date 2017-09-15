using System.Collections.Generic;
using System.IO;
using System;
using System.Text;

namespace ClientDataTableHashExporter
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = args[0];
            ulong hash = 0;
            List<string> files = SearchDir(path, "*.*");
            List<string> crcList = new List<string>();
            foreach (string fullname in files)
            {
                FileInfo fi = new FileInfo(fullname);
                string filename = fi.Name;
                //Console.WriteLine(filename);
                if (filename.StartsWith("t") && filename.EndsWith(".bytes"))
                {
                    ulong crc = ComputeHash(File.ReadAllBytes(fullname));
                    hash += crc;
                    crcList.Add(crc.ToString());
                    //Console.WriteLine(filename + "  " + crc);
                }
            }
            hash = ComputeHash(hash.ToString().ToCharArray());
            StringBuilder sb = new StringBuilder();
            sb.Append(hash);
            sb.Append("#");
            foreach (string crc in crcList)
            {
                sb.Append(crc);
                sb.Append("#");
            }
            File.WriteAllText(Path.Combine(path, "hash.txt"), sb.ToString());
        }
        static List<string> SearchDir(string path, string searchFor)
        {
            List<string> result = new List<string>();
            string[] directories = Directory.GetDirectories(path);
            for (int i = 0; i < directories.Length; ++i)
            {
                result.AddRange(SearchDir(directories[i], searchFor));
            }
            result.AddRange(Directory.GetFiles(path, searchFor));

            return result;
        }
        static uint ComputeHash(char[] s)
        {
            uint h = 0;
            for (int i = s.Length - 1; i >= 0; --i)
            {
                h = (h << 5) - h + s[i];
            }
            return h;
        }
        static ulong ComputeHash(byte[] s)
        {
            ulong hash = 0x9A9AA99A;
            for (int i = 0; i < s.Length; i++)
            {
                if ((i & 1) == 0)
                {
                    hash ^= ((hash << 7) ^ s[i] ^ (hash >> 3));
                }
                else
                {
                    hash ^= (~((hash << 11) ^ s[i] ^ (hash >> 5)));
                }
            }
            return hash;
        }
        
#if false
        static uint[] CRC32Table = null;
        static void MakeCRC32Table()
        {
            if (CRC32Table != null) return;
            CRC32Table = new uint[256];
            for (uint i = 0; i < 256; i++)
            {
                uint vCRC = i;
                for (int j = 0; j < 8; j++)
                {
                    if (vCRC % 2 == 0)
                        vCRC = (uint)(vCRC >> 1);
                    else
                        vCRC = (uint)((vCRC >> 1) ^ 0xEDB88320);
                }
                CRC32Table[i] = vCRC;
            }
        }
        static uint GenerateCRC(byte[] bytes)
        {
            MakeCRC32Table();

            long len = bytes.Length;
            byte ch;
            uint u_crc = 0xFFFFFFFF;
            for (long i = 0; i < len; i++)
            {
                ch = (byte)bytes[i];
                u_crc = ((u_crc >> 8) & 0x00FFFFFF) ^ CRC32Table[(u_crc ^ ch) & 0xFF];
            }
            u_crc = u_crc ^ 0xFFFFFFFF;
            return u_crc;
        }
        static uint GenerateCRC(char[] bytes)
        {
            MakeCRC32Table();

            long len = bytes.Length;
            char ch;
            uint u_crc = 0xFFFFFFFF;
            for (long i = 0; i < len; i++)
            {
                ch = (char)bytes[i];
                u_crc = ((u_crc >> 8) & 0x00FFFFFF) ^ CRC32Table[(u_crc ^ ch) & 0xFF];
            }
            u_crc = u_crc ^ 0xFFFFFFFF;
            return u_crc;
        }
#endif
    }
}
