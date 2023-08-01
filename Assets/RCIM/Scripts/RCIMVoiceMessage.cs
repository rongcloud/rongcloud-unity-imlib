using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMVoiceMessage : RCIMMediaMessage
    {
        /// <summary>
        /// 语音的长度，单位：秒
        /// </summary>
        public int duration { get; set; }
    
        public override String ToString()
        {
            return $"{base.ToString()} duration:{duration}";
        }
    }
}
