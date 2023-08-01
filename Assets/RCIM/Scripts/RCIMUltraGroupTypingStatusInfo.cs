using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMUltraGroupTypingStatusInfo
    {
        /// <summary>
        /// 会话 ID
        /// </summary>
        public string targetId { get; set; }
    
        /// <summary>
        /// 所属会话的业务标识
        /// </summary>
        public string channelId { get; set; }
    
        /// <summary>
        /// 用户id
        /// </summary>
        public string userId { get; set; }
    
        /// <summary>
        /// 用户数
        /// </summary>
        public int userNums { get; set; }
    
        /// <summary>
        /// 输入状态
        /// </summary>
        public RCIMUltraGroupTypingStatus status { get; set; }
    
        /// <summary>
        /// 服务端收到用户操作的上行时间.
        /// </summary>
        public long timestamp { get; set; }
    
        public override String ToString()
        {
            return $"targetId:{targetId} channelId:{channelId} userId:{userId} userNums:{userNums} status:{status} timestamp:{timestamp}";
        }
    }
}
