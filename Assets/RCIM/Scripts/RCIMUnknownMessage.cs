using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMUnknownMessage : RCIMMessage
    {
        /// <summary>
        /// 消息数据
        /// </summary>
        public string rawData { get; set; }
    
        /// <summary>
        /// 消息的标识
        /// </summary>
        public string objectName { get; set; }
    
        public override String ToString()
        {
            return $"{base.ToString()} rawData:{rawData} objectName:{objectName}";
        }
    }
}
