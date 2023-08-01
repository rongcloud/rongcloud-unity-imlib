using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMCustomMessage : RCIMMessage
    {
        /// <summary>
        /// 自定义消息的标识符
        /// </summary>
        public string identifier { get; set; }
    
        /// <summary>
        /// 自定义的消息存储策略
        /// </summary>
        public RCIMCustomMessagePolicy policy { get; set; }
    
        /// <summary>
        /// 自定义消息的键值对
        /// </summary>
        public Dictionary<string, string> fields { get; set; }
    
        public override String ToString()
        {
            return $"{base.ToString()} identifier:{identifier} policy:{policy} fields:{fields}";
        }
    }
}
