using System;
using System.Collections.Generic;
using System.Text;
using JTNE.Protocol.Enums;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.MessageBody;

namespace JTNE.Protocol.Formatters.MessageBodyFormatters {
    public class JTNE_0x05_Formatter : IJTNEFormatter<JTNE_0x05> {
        public JTNE_0x05 Deserialize (ReadOnlySpan<byte> bytes, out int readSize) {
            int offset = 0;
            JTNE_0x05 jTNE_0X05 = new JTNE_0x05 ();
            jTNE_0X05.LoginTime = bytes.ReadDateTime6Bytes (ref offset);
            jTNE_0X05.LoginNum = bytes.ReadUInt16 (ref offset);
            jTNE_0X05.PlatformUserName = bytes.ReadString (ref offset, 12);
            jTNE_0X05.PlatformPassword = bytes.ReadString (ref offset, 20);
            jTNE_0X05.EncryptMethod = (JTNEEncryptMethod) bytes.ReadByte (ref offset);
            readSize = offset;
            return jTNE_0X05;
        }

        public int Serialize (ref byte[] bytes, int offset, JTNE_0x05 value) {
            offset += bytes.WriteDateTime6Bytes (offset, value.LoginTime);
            offset += bytes.WriteUInt16 (offset, value.LoginNum);
            offset += bytes.WriteStringLittle (offset, value.PlatformUserName, 12);
            offset += bytes.WriteStringLittle (offset, value.PlatformPassword, 20);
            offset += bytes.WriteByte (offset, (byte) value.EncryptMethod);
            return offset;
        }
    }
}