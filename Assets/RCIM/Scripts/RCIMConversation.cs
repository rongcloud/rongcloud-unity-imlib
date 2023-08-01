using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMConversation
    {
        /// <summary>
        /// 获取会话类型
        /// </summary>
        public RCIMConversationType conversationType { get; set; }
    
        /// <summary>
        /// 会话 ID，单聊时为接收方 ID，群组会话中为群组 ID，聊天室会话中为聊天室 ID，系统会话为开发者指定的系统账号 Id
        /// </summary>
        public string targetId { get; set; }
    
        /// <summary>
        /// 频道 ID
        /// </summary>
        public string channelId { get; set; }
    
        /// <summary>
        /// 当前会话未读消息数量
        /// </summary>
        public int unreadCount { get; set; }
    
        /// <summary>
        /// 本会话里自己被 @ 的消息数量
        /// </summary>
        public int mentionedCount { get; set; }
    
        /// <summary>
        /// 本会话是否置顶
        /// </summary>
        public bool top { get; set; }
    
        /// <summary>
        /// 会话里保存的草稿信息
        /// </summary>
        public string draft { get; set; }
    
        /// <summary>
        /// 获取最后一条消息
        /// </summary>
        public RCIMMessage lastMessage { get; set; }
    
        /// <summary>
        /// 会话的通知级别
        /// </summary>
        public RCIMPushNotificationLevel notificationLevel { get; set; }
    
        /// <summary>
        /// 获取会话第一条未读消息的时间戳，仅对超级群生效
        /// </summary>
        public long firstUnreadMsgSendTime { get; set; }
    
        /// <summary>
        /// 获取会话最后的操作时间
        /// </summary>
        public long operationTime { get; set; }
    
        public override String ToString()
        {
            return $"conversationType:{conversationType} targetId:{targetId} channelId:{channelId} unreadCount:{unreadCount} mentionedCount:{mentionedCount} top:{top} draft:{draft} lastMessage:{lastMessage} notificationLevel:{notificationLevel} firstUnreadMsgSendTime:{firstUnreadMsgSendTime} operationTime:{operationTime}";
        }
    }
}
