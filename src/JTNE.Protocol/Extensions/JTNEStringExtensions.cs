﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Buffers.Binary;
using System.Buffers;

namespace JTNE.Protocol.Extensions
{
    public static partial class JTNEBinaryExtensions
    {
        public static string ReadString(this ReadOnlySpan<byte> read, ref int offset, int len)
        {
            string value = JTNEGlobalConfigs.Instance.Encoding.GetString(read.Slice(offset, len).ToArray());
            offset += len;
            return value.Trim('\0');
        }

        public static string ReadString(this ReadOnlySpan<byte> read, ref int offset)
        {
            string value = JTNEGlobalConfigs.Instance.Encoding.GetString(read.Slice(offset).ToArray());
            offset += value.Length;
            return value.Trim('\0');
        }

        public static int WriteStringLittle(this byte[] bytes, int offset, string data)
        {
            byte[] codeBytes = JTNEGlobalConfigs.Instance.Encoding.GetBytes(data);
            Array.Copy(codeBytes, 0, bytes, offset, codeBytes.Length);
            return codeBytes.Length;
        }

        public static int WriteStringLittle(this byte[] bytes, int offset, string data, int len)
        {
            byte[] tempBytes = null;
            if (string.IsNullOrEmpty(data))
            {
                tempBytes = new byte[0];
            }
            else
            {
                tempBytes = JTNEGlobalConfigs.Instance.Encoding.GetBytes(data);
            }
            byte[] rBytes = new byte[len];
            for (int i = 0; i < tempBytes.Length; i++)
            {
                if (i >= len) break;
                rBytes[i] = tempBytes[i];
            }
            Array.Copy(rBytes, 0, bytes, offset, rBytes.Length);
            return rBytes.Length;
        }

        public static int WriteStringPadLeftLittle(this byte[] bytes, int offset, string data, int len)
        {
            data = data.PadLeft(len, '\0');
            byte[] codeBytes = JTNEGlobalConfigs.Instance.Encoding.GetBytes(data);
            Array.Copy(codeBytes, 0, bytes, offset, codeBytes.Length);
            return codeBytes.Length;
        }

        public static int WriteStringPadRightLittle(this byte[] bytes, int offset, string data, int len)
        {
            data = data.PadRight(len, '\0');
            byte[] codeBytes = JTNEGlobalConfigs.Instance.Encoding.GetBytes(data);
            Array.Copy(codeBytes, 0, bytes, offset, codeBytes.Length);
            return codeBytes.Length;
        }
    }
}
