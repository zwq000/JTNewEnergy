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

        /// <summary>
        /// 根据 <see cref="JTNEPackage.MsgId"/> 获取 body 类型
        /// </summary>
        /// <param name="msgId"></param>
        /// <returns></returns>
        internal static Type GetBodyTypeByMsgId (byte msgId) => map.TryGetValue (msgId, out Type type) ? type : null;

        private static void InitMap () {
            foreach (JTNEMsgId msgId in Enum.GetValues (typeof (JTNEMsgId))) {
                JTNEBodiesTypeAttribute attr = msgId.GetAttribute<JTNEBodiesTypeAttribute> ();
                map.Add ((byte) msgId, attr?.BodyType);
            }
        }

        internal static void SetMap<TJTNEBodies> (byte msgId) where TJTNEBodies : JTNEBodies {
            if (!map.ContainsKey (msgId))
                map.Add (msgId, typeof (TJTNEBodies));
        }

        internal static void ReplaceMap<TJTNEBodies> (byte msgId) where TJTNEBodies : JTNEBodies {
            if (!map.ContainsKey (msgId))
                map.Add (msgId, typeof (TJTNEBodies));
            else
                map[msgId] = typeof (TJTNEBodies);
        }
    }
}