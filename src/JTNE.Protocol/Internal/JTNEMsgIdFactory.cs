using System;
using System.Collections.Generic;
using System.Text;
using JTNE.Protocol.Attributes;
using JTNE.Protocol.Enums;
using JTNE.Protocol.Extensions;

namespace JTNE.Protocol.Internal {
    internal static class JTNEMsgIdFactory {
        private static readonly Dictionary<byte, Type> map = new Dictionary<byte, Type> ();

        static JTNEMsgIdFactory () {
            InitMap ();
        }

        internal static Type GetBodiesImplTypeByMsgId (byte msgId) => map.TryGetValue (msgId, out Type type) ? type : null;

        private static void InitMap () {
            foreach (JTNEMsgId msgId in Enum.GetValues (typeof (JTNEMsgId))) {
                JTNEBodiesTypeAttribute jT808BodiesTypeAttribute = msgId.GetAttribute<JTNEBodiesTypeAttribute> ();
                map.Add ((byte) msgId, jT808BodiesTypeAttribute?.JT808BodiesType);
            }
        }

        internal static void SetMap<TJTNEBodies> (byte msgId)
        where TJTNEBodies : JTNEBodies {
            if (!map.ContainsKey (msgId))
                map.Add (msgId, typeof (TJTNEBodies));
        }

        internal static void ReplaceMap<TJTNEBodies> (byte msgId)
        where TJTNEBodies : JTNEBodies {
            if (!map.ContainsKey (msgId))
                map.Add (msgId, typeof (TJTNEBodies));
            else
                map[msgId] = typeof (TJTNEBodies);
        }
    }
}