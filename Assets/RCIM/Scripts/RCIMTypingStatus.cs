using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMTypingStatus
    {
        /// <summary>
        /// 当前正在输入的用户 ID
        /// </summary>
        public string userId { get; set; }
    
        /// <summary>
        /// 当前正在输入的消息类型名，为发送方调用发送接口时传入的 currentType
        /// </summary>
        public string contentType { get; set; }
    
        /// <summary>
        /// 输入时间
        /// </summary>
        public long sentTime { get; set; }
    
        public override String ToString()
        {
            return $"userId:{userId} contentType:{contentType} sentTime:{sentTime}";
        }
    }
}
