using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMMessage
    {
        /// <summary>
        /// 会话类型
        /// </summary>
        public RCIMConversationType conversationType { get; set; }
    
        /// <summary>
        /// 消息的类型
        /// </summary>
        public RCIMMessageType messageType { get; set; }
    
        /// <summary>
        /// 会话 ID
        /// </summary>
        public string targetId { get; set; }
    
        /// <summary>
        /// 所属会话的业务标识，长度限制 20 字符
        /// </summary>
        public string channelId { get; set; }
    
        /// <summary>
        /// 本地数据库中存储的消息的唯一 ID 值。发送新消息时无需指定该
        /// ID，否则会导致消息入库失败。在失败重发消息时，可以填入已入库的消息的 ID，请确保使用上一次发送失败的消息实例
        /// </summary>
        public int messageId { get; set; }
    
        /// <summary>
        /// 服务器消息唯一 ID（在同一个 Appkey 下全局唯一）
        /// </summary>
        public string messageUId { get; set; }
    
        /// <summary>
        /// 是否是离线消息，只在接收消息的回调方法中有效，如果消息为离线消息，则为 YES ，其他情况均为 NO
        /// </summary>
        public bool offLine { get; set; }
    
        /// <summary>
        /// 群阅读回执状态
        /// </summary>
        public RCIMGroupReadReceiptInfo groupReadReceiptInfo { get; set; }
    
        /// <summary>
        /// 消息的接收时间（Unix 时间戳、毫秒）
        /// </summary>
        public long receivedTime { get; set; }
    
        /// <summary>
        /// 消息的发送时间（Unix 时间戳、毫秒）
        /// </summary>
        public long sentTime { get; set; }
    
        /// <summary>
        /// 消息的接收状态
        /// </summary>
        public RCIMReceivedStatus receivedStatus { get; set; }
    
        /// <summary>
        /// 消息的发送状态
        /// </summary>
        public RCIMSentStatus sentStatus { get; set; }
    
        /// <summary>
        /// 消息的发送者 ID
        /// </summary>
        public string senderUserId { get; set; }
    
        /// <summary>
        /// 消息的方向
        /// </summary>
        public RCIMMessageDirection direction { get; set; }
    
        /// <summary>
        /// 消息携带的用户信息
        /// </summary>
        public RCIMUserInfo userInfo { get; set; }
    
        /// <summary>
        /// 消息的 @ 信息
        /// </summary>
        public RCIMMentionedInfo mentionedInfo { get; set; }
    
        /// <summary>
        /// 消息推送配置
        /// </summary>
        public RCIMMessagePushOptions pushOptions { get; set; }
    
        /// <summary>
        /// 消息的附加字段
        /// </summary>
        public string extra { get; set; }
    
        /// <summary>
        /// 消息扩展信息列表，该属性在消息发送时确定，发送之后不能再做修改。扩展信息只支持单聊和群组，其它会话类型不能设置扩展信息。默认消息扩展字典
        /// key 长度不超过 32 ，value 长度不超过 4096 ，单次设置扩展数量最大为 20，消息的扩展总数不能超过 300
        /// </summary>
        public Dictionary<string, string> expansion { get; set; }
    
        public override String ToString()
        {
            return $"conversationType:{conversationType} messageType:{messageType} targetId:{targetId} channelId:{channelId} messageId:{messageId} messageUId:{messageUId} offLine:{offLine} groupReadReceiptInfo:{groupReadReceiptInfo} receivedTime:{receivedTime} sentTime:{sentTime} receivedStatus:{receivedStatus} sentStatus:{sentStatus} senderUserId:{senderUserId} direction:{direction} userInfo:{userInfo} mentionedInfo:{mentionedInfo} pushOptions:{pushOptions} extra:{extra} expansion:{expansion}";
        }
    }
}
