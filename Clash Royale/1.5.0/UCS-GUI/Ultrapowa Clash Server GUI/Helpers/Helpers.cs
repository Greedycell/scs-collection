using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using CRS.Core;
using CRS.GameFiles;

namespace CRS.Helpers
{
    static class Helpers
    {
        public static uint ReadUInt32WithEndian(this BinaryReader br)
        {
            byte[] a32 = br.ReadBytes(4);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(a32);
            return BitConverter.ToUInt32(a32, 0);
        }

        public static long ReadInt64WithEndian(this BinaryReader br)
        {
            byte[] a64 = br.ReadBytes(8);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(a64);
            return BitConverter.ToInt64(a64, 0);
        }

        public static int ReadInt32WithEndian(this BinaryReader br)
        {
            byte[] a32 = br.ReadBytes(4);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(a32);
            return BitConverter.ToInt32(a32, 0);
        }

        public static ushort ReadUInt16WithEndian(this BinaryReader br)
        {
            byte[] a16 = br.ReadBytes(2);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(a16);
            return BitConverter.ToUInt16(a16, 0);
        }

        public static Data ReadDataReference(this BinaryReader br)
        {
            int id = br.ReadInt32WithEndian();
            return ObjectManager.DataTables.GetDataById(id);
        }

        public static byte[] ReadAllBytes(this BinaryReader br)
        {
            const int bufferSize = 4096;
            using(var ms = new MemoryStream())
            {
                byte[] buffer = new byte[bufferSize];
                int count;
                while ((count = br.Read(buffer, 0, buffer.Length)) != 0)
                    ms.Write(buffer, 0, count);
                return ms.ToArray();
            }
        }

        public static string ReadScString(this BinaryReader br)
        {
            int stringLength = br.ReadInt32WithEndian();
            string result;

            if (stringLength > -1)
            {
                if (stringLength > 0)
                {
                    byte[] astr = br.ReadBytes(stringLength);
                    result = Encoding.UTF8.GetString(astr);
                }
                else
                {
                    result = string.Empty;
                }
            }
            else
                result = null;
            return result;
        }

        public static void AddInt32(this List<byte> list, int data)
        {
            list.AddRange(BitConverter.GetBytes(data).Reverse());
        }

        public static void AddInt64(this List<byte> list, long data)
        {
            list.AddRange(BitConverter.GetBytes(data).Reverse());
        }

        //public static void AddString(this List<byte> list, string data)
        //{
        //    if (data == null)
        //        list.AddRange(BitConverter.GetBytes((int)-1).Reverse());
        //    else
        //    {
        //        list.AddRange(BitConverter.GetBytes(Encoding.UTF8.GetByteCount(data)).Reverse());
        //        list.AddRange(Encoding.UTF8.GetBytes(data));
        //    }
        //}

        //public static void AddDataSlots(this List<byte> list, List<DataSlot> data)
        //{
        //    list.AddInt32(data.Count);
        //    foreach (DataSlot dataSlot in data)
        //    {
        //        list.AddRange(dataSlot.Encode());
        //    }
        //}

        public static bool TryRemove<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> self, TKey key)
        {
            TValue ignored;
            return self.TryRemove(key, out ignored);
        }
    }

}
