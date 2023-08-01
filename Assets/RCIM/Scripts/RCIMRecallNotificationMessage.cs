using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMRecallNotificationMessage : RCIMMessage
    {
        /// <summary>
        /// 是否是管理员操作
        /// </summary>
        public bool admin { get; set; }
    
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool deleted { get; set; }
    
        /// <summary>
        /// 被撤回的原始消息的发送时间（毫秒）
        /// </summary>
        public long recallTime { get; set; }
    
        /// <summary>
        /// 撤回动作的时间（毫秒）
        /// </summary>
        public long recallActionTime { get; set; }
    
        /// <summary>
        /// 被撤回的原消息
        /// </summary>
        public RCIMMessage originalMessage { get; set; }
    
        public override String ToString()
        {
            return $"{base.ToString()} admin:{admin} deleted:{deleted} recallTime:{recallTime} recallActionTime:{recallActionTime} originalMessage:{originalMessage}";
        }
    }
}
