using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMReferenceMessage : RCIMMessage
    {
        /// <summary>
        /// 引用文本
        /// </summary>
        public string text { get; set; }
    
        /// <summary>
        /// 被引用的消息
        /// </summary>
        public RCIMMessage referenceMessage { get; set; }
    
        public override String ToString()
        {
            return $"{base.ToString()} text:{text} referenceMessage:{referenceMessage}";
        }
    }
}
