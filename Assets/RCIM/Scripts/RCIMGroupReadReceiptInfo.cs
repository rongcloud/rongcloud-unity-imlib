using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMGroupReadReceiptInfo
    {
        /// <summary>
        /// 是否为需要回执的消息。 true: 需要回执的消息。 false: 普通消息
        /// </summary>
        public bool readReceiptMessage { get; set; }
    
        /// <summary>
        /// 是否发送过消息回执响应。 仅对消息接收方有效
        /// </summary>
        public bool hasRespond { get; set; }
    
        /// <summary>
        /// 会话中响应过该消息回执的成员 userId 列表。 key: userId value: respondTime
        /// </summary>
        public Dictionary<string, long> respondUserIds { get; set; }
    
        public override String ToString()
        {
            return $"readReceiptMessage:{readReceiptMessage} hasRespond:{hasRespond} respondUserIds:{respondUserIds}";
        }
    }
}
