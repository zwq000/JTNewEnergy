﻿using System;
using System.Collections.Generic;
using System.Text;
using JTNE.Protocol.Attributes;
using JTNE.Protocol.Formatters.MessageBodyFormatters;
using JTNE.Protocol.Metadata;

namespace JTNE.Protocol.MessageBody
{
    /// <summary>
    /// 驱动电机数据
    /// </summary>
    [JTNEFormatter(typeof(JTNE_0x02_0x02_Formatter))]
    public class JTNE_0x02_0x02 : JTNE_0x02_Body
    {
        public JTNE_0x02_0x02() : base(JTNE_0x02_0x02)
        {
        }

        /// <summary>
        /// 电机个数
        /// </summary>
        public byte ElectricalCount { get; set; }

        /// <summary>
        /// 电机信息集合
        /// </summary>
        public List<Electrical> Electricals { get; set; }
    }
}
