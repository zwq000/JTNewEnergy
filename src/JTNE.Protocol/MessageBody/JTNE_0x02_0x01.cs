using System;
using System.Collections.Generic;
using System.Text;
using JTNE.Protocol.Attributes;
using JTNE.Protocol.Formatters.MessageBodyFormatters;

namespace JTNE.Protocol.MessageBody {
    /// <summary>
    /// 整车数据
    /// </summary>
    [JTNEFormatter (typeof (JTNE_0x02_0x01_Formatter))]
    public class JTNE_0x02_0x01 : JTNE_0x02_Body {
        public JTNE_0x02_0x01 () : base (JTNE_0x02_0x01) { }

        /// <summary>
        /// 车辆状态
        /// </summary>
        public byte CarStatus { get; set; }
        /// <summary>
        /// 充放电状态
        /// </summary>
        public byte ChargeStatus { get; set; }
        /// <summary>
        /// 运行模式
        /// </summary>
        public byte OperationMode { get; set; }
        /// <summary>
        /// 车速 
        /// </summary>
        public JTNE0201_Speed Speed { get; set; }
        /// <summary>
        /// 累计里程
        /// </summary>
        public uint TotalDis { get; set; }
        /// <summary>
        /// 总电压 
        /// </summary>
        public ushort TotalVoltage { get; set; }
        /// <summary>
        /// 总电流 
        /// </summary>
        public ushort TotalTemp { get; set; }
        /// <summary>
        /// SOC 
        /// </summary>
        public byte SOC { get; set; }
        /// <summary>
        /// DC-DC 状态 
        /// </summary>
        public byte DCStatus { get; set; }
        /// <summary>
        /// 档位 
        /// </summary>
        public byte Stall { get; set; }
        /// <summary>
        /// 绝缘电阻
        /// </summary>
        public ushort Resistance { get; set; }
        /// <summary>
        /// 加速踏板行程值
        /// </summary>
        public byte Accelerator { get; set; }
        /// <summary>
        /// 制动踏板状态
        /// </summary>
        public byte Brakes { get; set; }
    }

    /// <summary>
    /// 车辆速度
    /// </summary>
    /// <remarks>
    /// 有效值 0~2200 (0~220km/h)
    /// 最小值 0
    /// 0xFFFE 表示异常
    /// 0xFFFF 表示无效
    /// 
    /// </remarks>
    public struct JTNE0201_Speed {
        /// <summary>
        /// 异常值
        /// </summary>
        const ushort Anomalous = 0xFFFE;

        /// <summary>
        /// 无效值
        /// </summary>
        const ushort Invalid = 0xFFFF;

        const ushort MAX_Value = 2200;

        const ushort MIN_Value = 0;
        private ushort _value;

        public JTNE0201_Speed (ushort value) {
            this._value = value;
        }

        /// <summary>
        /// 原始值
        /// </summary>
        /// <value></value>
        public ushort RawValue { get => _value; }

        /// <summary>
        /// 速度值 单位 km/h
        /// </summary>
        /// <value></value>
        public float Value { get => _value / 10f; }

        /// <summary>
        /// 是否可用
        /// </summary>
        /// <value></value>
        public bool IsValid { get => _value >= MIN_Value && _value <= MAX_Value; }

        public static implicit operator JTNE0201_Speed (ushort value) {
            return new JTNE0201_Speed (value);
        }
    }
}