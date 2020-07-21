using System;
using System.Collections.Generic;
using System.Text;
using JTNE.Protocol.Attributes;
using JTNE.Protocol.MessageBody;

namespace JTNE.Protocol.Enums {
    /// <summary>
    /// 命令单元
    /// </summary>
    public enum JTNEMsgId : byte {
        /// <summary>
        /// 车辆登入
        /// </summary>
        [JTNEBodiesType (typeof (JTNE_0x01))]
        Login = 0x01,
        /// <summary>
        /// 实时信息上传
        /// </summary>
        [JTNEBodiesType (typeof (JTNE_0x02))]
        UploadIM = 0x02,
        /// <summary>
        /// 补传信息上传
        /// </summary>
        UploadSup = 0x03,
        /// <summary>
        /// 车辆登出
        /// </summary>
        [JTNEBodiesType (typeof (JTNE_0x04))]
        Logout = 0x04,
        /// <summary>
        /// 平台登入
        /// </summary>
        [JTNEBodiesType (typeof (JTNE_0x05))]
        PlatformLogin = 0x05,
        /// <summary>
        /// 平台登出
        /// </summary>
        [JTNEBodiesType (typeof (JTNE_0x06))]
        PlatformLogout = 0x06,
        /// <summary>
        /// 心跳包
        /// </summary>
        [JTNEBodiesType (typeof (JTNE_0x07))]
        HeartBeat = 0x07,
        /// <summary>
        /// 终端校时
        /// </summary>
        [JTNEBodiesType (typeof (JTNE_0x08))]
        CheckTime = 0x08,
        /// <summary>
        /// 查询命令
        /// </summary>
        Query = 0x80,
        /// <summary>
        /// 设置命令
        /// </summary>
        Settings = 0x81,
        /// <summary>
        /// 控制命令
        /// </summary>
        Control = 0x82
    }
}