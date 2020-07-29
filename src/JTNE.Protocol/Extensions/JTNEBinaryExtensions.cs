using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;

namespace JTNE.Protocol.Extensions {
     internal static class JTNEBinaryExtensions {
        public static int ReadInt32 (this ReadOnlySpan<byte> source, ref int offset) {
            int value = ((source[offset] << 24) | (source[offset + 1] << 16) | (source[offset + 2] << 8) | source[offset + 3]);
            offset = offset + 4;
            return value;
        }

        public static uint ReadUInt32 (this ReadOnlySpan<byte> source, ref int offset) {
            uint value = (uint) ((source[offset] << 24) | (source[offset + 1] << 16) | (source[offset + 2] << 8) | source[offset + 3]);
            offset = offset + 4;
            return value;
        }

        public static ushort ReadUInt16 (this ReadOnlySpan<byte> source, ref int offset) {
            //ushort value = (ushort)((read[offset] << 8) | (read[offset + 1]));
            var value = BinaryPrimitives.ReadUInt16BigEndian (source.Slice (offset, 2));
            offset = offset + 2;
            return value;
        }

        public static byte ReadByte (this ReadOnlySpan<byte> source, ref int offset) {
            byte value = source[offset];
            offset = offset + 1;
            return value;
        }

        public static byte[] ReadBytes (this ReadOnlySpan<byte> source, ref int offset, int len) {
            ReadOnlySpan<byte> temp = source.Slice (offset, len);
            offset = offset + len;
            return temp.ToArray ();
        }

        public static byte[] ReadBytes (this ReadOnlySpan<byte> source, ref int offset) {
            ReadOnlySpan<byte> temp = source.Slice (offset);
            offset = offset + temp.Length;
            return temp.ToArray ();
        }

        public static int WriteUInt32 (this byte[] write, int offset, uint data) {
            write[offset] = (byte) (data >> 24);
            write[offset + 1] = (byte) (data >> 16);
            write[offset + 2] = (byte) (data >> 8);
            write[offset + 3] = (byte) data;
            return 4;
        }

        public static int WriteInt32 (this byte[] write, int offset, int data) {
            write[offset] = (byte) (data >> 24);
            write[offset + 1] = (byte) (data >> 16);
            write[offset + 2] = (byte) (data >> 8);
            write[offset + 3] = (byte) data;
            return 4;
        }

        public static int WriteUInt16 (this byte[] write, int offset, ushort data) {
            write[offset] = (byte) (data >> 8);
            write[offset + 1] = (byte) data;
            return 2;
        }

        public static int WriteByte (this byte[] write, int offset, byte data) {
            write[offset] = data;
            return 1;
        }

        public static int WriteBytes (this byte[] write, int offset, byte[] data) {
            Array.Copy (data, 0, write, offset, data.Length);
            return data.Length;
        }

        public static IEnumerable<byte> ToBytes (this string data, Encoding coding) {
            return coding.GetBytes (data);
        }

        public static IEnumerable<byte> ToBytes (this string data) {
            return ToBytes (data, JTNEGlobalConfigs.Instance.Encoding);
        }

        public static IEnumerable<byte> ToBytes (this int data, int len) {
            List<byte> bytes = new List<byte> ();
            int n = 1;
            for (int i = 0; i < len; i++) {
                bytes.Add ((byte) (data >> 8 * (len - n)));
                n++;
            }
            return bytes;
        }

        /// <summary>
        /// 经纬度
        /// </summary>
        /// <param name="latlng"></param>
        /// <returns></returns>
        public static double ToLatLng (this int latlng) {
            return Math.Round (latlng / Math.Pow (10, 6), 6);
        }
    }
}