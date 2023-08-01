using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMBlockedMessageInfo
    {
        /// <summary>
        /// 封禁的会话类型
        /// </summary>
        public RCIMConversationType conversationType { get; set; }
    
        /// <summary>
        /// 封禁的会话 ID
        /// </summary>
        public string targetId { get; set; }
    
        /// <summary>
        /// 封禁的消息 Uid
        /// </summary>
        public string blockedMsgUId { get; set; }
    
        /// <summary>
        /// 封禁的类型
        /// </summary>
        public RCIMMessageBlockType blockType { get; set; }
    
        /// <summary>
        /// 封禁的附加信息
        /// </summary>
        public string extra { get; set; }
    
        public override String ToString()
        {
            return $"conversationType:{conversationType} targetId:{targetId} blockedMsgUId:{blockedMsgUId} blockType:{blockType} extra:{extra}";
        }
    }
}
