﻿using System;
using System.Text;
using JTNE.Protocol.Enums;
using JTNE.Protocol.Interfaces;
using JTNE.Protocol.Internal;
using JTNE.Protocol.MessageBody;

namespace JTNE.Protocol {
    /// <summary>
    /// 
    /// </summary>
    public class JTNEGlobalConfigs {
        private static readonly Lazy<JTNEGlobalConfigs> instance = new Lazy<JTNEGlobalConfigs> (() => new JTNEGlobalConfigs ());

        private JTNEGlobalConfigs () {
            Encoding.RegisterProvider (CodePagesEncodingProvider.Instance);
            Encoding = Encoding.UTF8;
            SkipCRCCode = false;
            DeviceMsgSNDistributed = new DefaultDeviceMsgSNDistributedImpl ();
            PlatformMsgSNDistributed = new DefaultPlatformMsgSNDistributedImpl ();
        }

        /// <summary>
        /// 字符串编码
        /// </summary>
        public Encoding Encoding { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static JTNEGlobalConfigs Instance {
            get {
                return instance.Value;
            }
        }
        /// <summary>
        /// 设备流水号
        /// </summary>
        public IDeviceMsgSNDistributed DeviceMsgSNDistributed { get; private set; }
        /// <summary>
        /// 平台流水号
        /// </summary>
        public IPlatformMsgSNDistributed PlatformMsgSNDistributed { get; private set; }

        /// <summary>
        /// 跳过校验码
        /// 测试的时候需要手动修改值，避免验证
        /// 默认：false
        /// </summary>
        public bool SkipCRCCode { get; private set; }

        /// <summary>
        /// 消息数据体加密算法
        /// RSA=>IJTNEEncryptImpl
        /// AES=>IJTNEEncryptImpl
        /// </summary>
        public Func<JTNEEncryptMethod, IJTNEEncrypt> DataBodiesEncrypt { get; private set; }

        /// <summary>
        /// 平台登入加密算法
        /// RSA=>IJTNEEncryptImpl
        /// AES=>IJTNEEncryptImpl
        /// </summary>
        public Func<JTNEEncryptMethod, IJTNEEncrypt> PlatformLoginEncrypt { get; private set; }

        /// <summary>
        /// 注册自定义消息
        /// </summary>
        /// <typeparam name="TJTNEBodies"></typeparam>
        /// <param name="customMsgId"></param>
        /// <returns></returns>
        public JTNEGlobalConfigs Register_CustomMsgId<TJTNEBodies> (byte customMsgId)
        where TJTNEBodies : JTNEBodies {
            JTNEMsgIdFactory.SetMap<TJTNEBodies> (customMsgId);
            return instance.Value;
        }

        /// <summary>
        /// 重写消息
        /// </summary>
        /// <typeparam name="TJTNEBodies"></typeparam>
        /// <param name="overwriteMsgId"></param>
        /// <returns></returns>
        public JTNEGlobalConfigs Overwrite_MsgId<TJTNEBodies> (byte overwriteMsgId)
        where TJTNEBodies : JTNEBodies {
            JTNEMsgIdFactory.ReplaceMap<TJTNEBodies> (overwriteMsgId);
            return instance.Value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeCode">自定义类型编码</param>
        /// <param name="type">继承JTNE.Protocol.MessageBody.JTNE_0x02_CustomBody</param>
        /// <returns></returns>
        public JTNEGlobalConfigs Register_JTNE0x02CustomBody (byte typeCode, Type type) {
            if (!JTNE_0x02_CustomBody.CustomTypeCodes.ContainsKey (typeCode)) {
                JTNE_0x02_CustomBody.CustomTypeCodes.Add (typeCode, type);
            }
            return instance.Value;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeCode">自定义类型编码</param>
        /// <param name="type">继承JTNE.Protocol.MessageBody.JTNE_0x81_Body</param>
        /// <returns></returns>
        public JTNEGlobalConfigs Register_JTNE0x81CustomBody (byte typeCode, Type type) {
            if (!JTNE_0x81_Body.JTNE_0x81Method.ContainsKey (typeCode)) {
                JTNE_0x81_Body.JTNE_0x81Method.Add (typeCode, type);
            }
            return instance.Value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dependerId">自定义类型编码</param>
        /// <param name="dependedId">继承JTNE.Protocol.MessageBody.JTNE_0x81_Body</param>
        /// <returns></returns>
        public JTNEGlobalConfigs Register_JTNE0x81CustomDepenedBody (byte dependerId, byte dependedId) {
            if (!JTNE_0x81_Body.JTNE_0x81LengthOfADependOnValueOfB.ContainsKey (dependerId)) {
                JTNE_0x81_Body.JTNE_0x81LengthOfADependOnValueOfB.Add (dependerId, dependedId);
            }
            return instance.Value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeCode">自定义类型编码</param>
        /// <param name="type">继承JTNE.Protocol.MessageBody.JTNE_0x81_Body</param>
        /// <returns></returns>
        public JTNEGlobalConfigs Register_JTNE0x82CustomBody (byte typeCode, Type type) {
            if (!JTNE_0x82_Body.JTNE_0x82Method.ContainsKey (typeCode)) {
                JTNE_0x82_Body.JTNE_0x82Method.Add (typeCode, type);
            }
            return instance.Value;
        }
        /// <summary>
        /// 设置跳过校验码
        /// 场景：测试的时候，可能需要手动改数据，所以测试的时候有用
        /// </summary>
        /// <param name="skipCRCCode"></param>
        /// <returns></returns>
        public JTNEGlobalConfigs SetSkipCRCCode (bool skipCRCCode) {
            instance.Value.SkipCRCCode = skipCRCCode;
            return instance.Value;
        }
        /// <summary>
        /// 设置设备流水号
        /// </summary>
        /// <param name="deviceMsgSNDistributed"></param>
        /// <returns></returns>
        public JTNEGlobalConfigs SetDeviceMsgSNDistributed (IDeviceMsgSNDistributed deviceMsgSNDistributed) {
            instance.Value.DeviceMsgSNDistributed = deviceMsgSNDistributed;
            return instance.Value;
        }
        /// <summary>
        /// 设置平台流水号
        /// </summary>
        /// <param name="platformMsgSNDistributed"></param>
        /// <returns></returns>
        public JTNEGlobalConfigs SetPlatformMsgSNDistributed (IPlatformMsgSNDistributed platformMsgSNDistributed) {
            instance.Value.PlatformMsgSNDistributed = platformMsgSNDistributed;
            return instance.Value;
        }
        /// <summary>
        /// 设置消息数据体加密算法
        /// </summary>
        /// <param name="dataBodiesEncrypt"></param>
        /// <returns></returns>
        public JTNEGlobalConfigs SetDataBodiesEncrypt (Func<JTNEEncryptMethod, IJTNEEncrypt> dataBodiesEncrypt) {
            instance.Value.DataBodiesEncrypt = dataBodiesEncrypt;
            return instance.Value;
        }
        /// <summary>
        /// 设置平台登入加密算法
        /// </summary>
        /// <param name="platformLoginEncrypt"></param>
        /// <returns></returns>
        public JTNEGlobalConfigs SetPlatformLoginEncrypt (Func<JTNEEncryptMethod, IJTNEEncrypt> platformLoginEncrypt) {
            instance.Value.PlatformLoginEncrypt = platformLoginEncrypt;
            return instance.Value;
        }
    }
}