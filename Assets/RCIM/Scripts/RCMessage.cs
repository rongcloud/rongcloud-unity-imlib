using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace cn_rongcloud_im_unity
{
    public class RCMessage
    {
        public RCConversationType ConversationType { get; set; }
        public string TargetId { get; set; }

        public Int64 MessageId { get; set; }
        public string MessageUId { get; set; }
        public RCMessageDirection Direction { get; set; }

        public string SenderUserId { get; set; }

        public RCReceivedStatus ReceivedStatus { get; set; }
        public RCSentStatus SentStatus { get; set; }

        public Int64 SentTime { get; set; }
        public Int64 ReceivedTime {  get; set; }

        public string ObjectName { get; set; }
        public RCMessageContent Content { get; set; }

        public string Extra { get; set; }
        public bool CanIncludeExpansion { get; set; }
        public Dictionary<string, string> ExpansionDic { get; set; }

        public RCReadReceiptInfo ReadReceiptInfo { get; set; }
        public RCMessageConfig MessageConfig { get; set; }
        public RCMessagePushConfig MessagePushConfig { get; set; }

        public override string ToString()
        {
            return $"RCMessage Id：{MessageId} Uid: {MessageUId} {ObjectName} {ConversationType} TargetId: {TargetId} Content: [{Content} Direction: {Direction} SenderId: {SenderUserId} SentStatus: {SentStatus} ReceivedStatus: {ReceivedStatus} SentTime: {SentTime}] ";
        }
    }

    public class RCMessageConfig
    {
        public bool DisableNotification { get; set; }
    }

    public class RCMessagePushConfig
    {
        public String PushTitle { get; set; }
        public String PushContent { get; set; }
        public String PushData { get; set; }
        public bool ForceShowDetailContent { get; set; }
        public bool DisablePushTitle { get; set; }
        public string TemplateId { get; set; }

        public RCIOSConfig iOSConfig { get; set; }
        public RCAndroidConfig AndroidConfig { get; set; }
    }

    public class RCIOSConfig
    {
        /// <summary>
        /// iOS 平台通知栏分组 ID
        /// </summary>
        public string ThreadId { get; set; }
        /// <summary>
        /// iOS 平台通知覆盖 ID
        /// </summary>
        public string APNSCollapseId { get; set; }

        /// <summary>
        /// 如果不设置后台默认取消息类型字符串，如RC:TxtMsg
        /// </summary>
        public string Category { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string RichMediaUrl { get; set; }
    }

    public class RCAndroidConfig
    {
        public enum HwPushConfigImportance
        {
            Normal = 0,
            Low = 1
        }

        public enum VivoType
        {
            Operate = 0,
            System = 1
        }

        /// <summary>
        /// Android 平台 Push 唯一标识
        /// </summary>
        public string NotificationId { get; set; }
        /// <summary>
        /// 小米推送平台渠道 ID
        /// </summary>
        public string ChannelIdMi { get; set; }
        /// <summary>
        /// 华为推送平台渠道 ID
        /// </summary>
        public string ChannelIdHW    {  get; set;}

        /// <summary>
        /// 华为消息分类方式有两种：1、消息智能分类 2、消息自分类, "NORMAL" or "LOW"
        /// 默认情况下，所有消息一律通过通知消息智能分类功能进行分类,若申请自分类权益,应用的推送消息将根据NORMAL、LOW进行归类
        /// </summary>
        public HwPushConfigImportance ImportanceHW { get; set; }  = HwPushConfigImportance.Normal;
        
        /// <summary>
        /// OPPO 推送平台渠道 ID
        /// </summary>
        public string ChannelIdOPPO {  get; set; }
        /// <summary>
        /// VIVO 推送平台推送类型 ,目前可选值"0"(运营消息); "1"(系统消息)
        /// </summary>
        public VivoType TypeVivo {  get; set; }

        public string CollapseKeyFCM { get; set; }
        public string ImageUrlFCM { get; set; }
    }

    public class RCChatRoomHistoryMessageResult
    {
        public IList<RCMessage> Messages { get; set; }
        public Int64 SyncTime { get; set; }

        public RCChatRoomHistoryMessageResult()
        {

        }

        public RCChatRoomHistoryMessageResult(IList<RCMessage> messages, Int64 syncTime)
        {
            this.Messages = messages;
            this.SyncTime = syncTime;
        }
    }
}
