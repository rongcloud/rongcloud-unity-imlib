using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMTextMessage : RCIMMessage
    {
        /// <summary>
        /// 文本内容
        /// </summary>
        public string text { get; set; }
    
        public override String ToString()
        {
            return $"{base.ToString()} text:{text}";
        }
    }
}
