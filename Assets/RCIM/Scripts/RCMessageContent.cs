using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn_rongcloud_im_unity
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class MessageTag : Attribute
    {
        /// <summary>
        ///     空值，不表示任何意义，发送的自定义消息不会在会话页面和会话列表中展示
        /// </summary>
        public const int None = 0x0;

        /// <summary>
        ///     消息需要被存储到消息历史记录，并在之后可以通过接口查询，存储后会在会话界面中显示
        /// </summary>
        public const int IsPersisted = 0x1;

        /// <summary>
        ///     消息需要被记入未读消息数
        /// </summary>
        public const int IsCounted = 0x3;

        /// <summary>
        ///     状态消息, 不存储不计数
        /// </summary>
        public const int Status = 0x10;
        
        /// <summary>
        ///     消息对象名称
        ///     <p />
        ///     请不要以 "RC:" 开头， "RC:" 为官方保留前缀
        ///     @return 消息对象名称的返回值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        ///     消息的标识
        ///     <p>传入的值可以为 MessageTag.NONE、MessageTag.ISPERSISTED 或 MessageTag.ISCOUNTED</p>
        /// </summary>
        public int Flag { get; set; } = None;
    }

    public static class RCMessageContentExtensions
    {
        public static string GetObjectName(this RCMessageContent messageContent)
        {
            if (null == messageContent?.GetType()) return null;

            var attributes = Attribute.GetCustomAttributes(messageContent.GetType(), false);
            foreach (var attr in attributes)
            {
                if (attr.GetType() != typeof(MessageTag)) continue;
                if (attr is MessageTag msgTag) return msgTag.Value;
            }

            return null;
        }

        public static int GetFlag(this RCMessageContent messageContent)
        {
            if (null == messageContent?.GetType()) return MessageTag.None;

            var attributes = Attribute.GetCustomAttributes(messageContent.GetType(), false);
            foreach (var attr in attributes)
            {
                if (attr.GetType() != typeof(MessageTag)) continue;
                if (attr is MessageTag msgTag) return msgTag.Flag;
            }

            return MessageTag.None;
        }
    }

    public class RCMessageContent
    {
        public RCUserInfo SendUserInfo { get; internal set; }

        public RCMentionedInfo MentionedInfo { get; internal set; }

        public bool IsDestruct { get; internal set; }
        public Int64 DestructDuration { get; internal set; }

        public string Extra { get; internal set; }

        public string ObjectName { get; internal set; }

        internal RCMessageContent()
        {

        }

        public override string ToString()
        {
            return
                $"Content {ObjectName}, Extra: {Extra} Sender: {SendUserInfo} Mention: {MentionedInfo} IsDestruct: {IsDestruct} DestructDuration: {DestructDuration}";
        }
    }

    public class RCMediaMessageContent : RCMessageContent
    {
        /// <summary>
        /// 媒体内容的本地路径（此属性必须有值）
        /// </summary>
        public string LocalPath { get; set; }

        /// <summary>
        /// 媒体内容上传服务器后的网络地址（上传成功后 SDK 会为该属性赋值）
        /// </summary>
        public string MediaUrl { get; set; }

        /// <summary>
        ///  媒体内容的文件名（如不传使用 SDK 中 downloadMediaMessage 方法下载后会默认生成一个名称）
        /// </summary>
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()} MediaContent Name:{Name} LocalPath: {LocalPath} MediaUrl: {MediaUrl}";
        }
    }

    [MessageTag( Value = "RC:UnknownMsg", Flag = MessageTag.None)]
    public class RCUnknownMessage : RCMessageContent
    {
        public RCUnknownMessage()
        {
            this.ObjectName = "RC:UnknownMsg";
        }
    }
    
    [MessageTag( Value = "RC:TxtMsg", Flag = MessageTag.IsCounted)]
    public class RCTextMessage : RCMessageContent
    {
        public string Content { get; set; }

        public RCTextMessage()
        {
            this.ObjectName = "RC:TxtMsg";
        }

        public RCTextMessage(string content) : this()
        {
            this.Content = content;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Content: {Content}";
        }
    }

    [MessageTag( Value = "RC:ImgMsg", Flag = MessageTag.IsCounted| MessageTag.IsPersisted)]
    public class RCImageMessage : RCMediaMessageContent
    {
        /// <summary>
        /// 缩略图 Uri
        /// </summary>
        public string ThumbUri { get; set; }

        /// <summary>
        /// 网络图片地址（http://）
        /// </summary>
        public string RemoteUrl
        {
            get { return base.MediaUrl; }
            set { base.MediaUrl = value; }
        }

        /// <summary>
        /// Base64 数据
        /// </summary>
        public string Base64 { get; set; }

        /// <summary>
        /// 是否是原图
        /// </summary>
        public bool IsOriginal { get; set; }

        /// <summary>
        /// 是否上传失败
        /// </summary>
        public bool UploadFailed { get; internal set; }

        public RCImageMessage()
        {
            this.ObjectName = "RC:ImgMsg";
        }

        public RCImageMessage(string localPath, bool isFull) : this()
        {
            this.LocalPath = localPath;
            this.IsOriginal = isFull;
        }

        public override string ToString()
        {
            return
                $"{base.ToString()}, original: {IsOriginal} uploadFailed: {UploadFailed} thumb: {ThumbUri} remoteUrl: {RemoteUrl} base64: {Base64}";
        }
    }

    [MessageTag( Value = "RC:VcMsg", Flag = MessageTag.IsCounted| MessageTag.IsPersisted)]
    public class RCVoiceMessage : RCMessageContent
    {
        /// <summary>
        /// 音频文件的 Uri
        /// </summary>
        public string LocalPath { get; set; }

        /// <summary>
        /// 音消息的时长， 以秒为单位
        /// </summary>
        public int Duration { get; set; }

        public string Base64 { get; set; }

        public RCVoiceMessage()
        {
            this.ObjectName = "RC:VcMsg";
        }

        public RCVoiceMessage(string localPath, int duration) : this()
        {
            this.LocalPath = localPath;
            this.Duration = duration;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Duration: {Duration} LocalPath: {LocalPath} Base64: {Base64}";
        }
    }

    // [MessageTag( Value = "RC:LBSMsg", Flag = MessageTag.IsCounted| MessageTag.IsPersisted)]
    // public class RCLocationMessage : RCMessageContent
    // {
    //     public double Latitude { get; set; }
    //     public double Longitude { get; set; }

    //     public string POI { get; set; }
    //     public string Base64 { get; set; }
    //     public string ThumbUrl { get; set; }

    //     public RCLocationMessage()
    //     {
    //         this.ObjectName = "RC:LBSMsg";
    //     }

    //     public override string ToString()
    //     {
    //         return $"{base.ToString()}, POI: {POI} GPS: {Latitude},{Longitude} Thumb: {ThumbUrl} Base64: {Base64}";
    //     }
    // }

    [MessageTag( Value = "RC:HQVCMsg", Flag = MessageTag.IsCounted| MessageTag.IsPersisted)]
    public class RCHQVoiceMessage : RCMediaMessageContent
    {
        /// <summary>
        /// 语音消息的时长，以秒为单位
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// 高清语音消息远端下载 URL
        /// </summary>
        public string FileUrl
        {
            get { return base.MediaUrl; }
            set { base.MediaUrl = value; }
        }

        public RCHQVoiceMessage()
        {
            this.ObjectName = "RC:HQVCMsg";
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Duration: {Duration} FileUrl: {FileUrl}";
        }
    }

    [MessageTag( Value = "RC:SightMsg", Flag = MessageTag.IsCounted| MessageTag.IsPersisted)]
    public class RCSightMessage : RCMediaMessageContent
    {
        /// <summary>
        /// 缩略图 Uri
        /// </summary>
        public string ThumbUri { get; set; }

        /// <summary>
        /// 需要传递的 Base64 数据
        /// </summary>
        public string Base64 { get; set; }

        /// <summary>
        /// 小视频时长，以秒为单位
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// 小视频文件大小
        /// </summary>
        public Int64 Size { get; set; }

        public bool IsOriginal { get; set; }

        public RCSightMessage()
        {
            this.ObjectName = "RC:SightMsg";
        }

        public RCSightMessage(String localPath, int videoDuration) : this()
        {
            this.LocalPath = localPath;
            this.Duration = videoDuration;
        }

        public override string ToString()
        {
            return
                $"{base.ToString()}, Thumb: {ThumbUri} Duration: {Duration} IsOriginal: {IsOriginal} Size: {Size} Base64: {Base64}";
        }
    }

    [MessageTag( Value = "RC:GIFMsg", Flag = MessageTag.IsCounted| MessageTag.IsPersisted)]
    public class RCGifMessage : RCMediaMessageContent
    {
        /// <summary>
        /// 是否上传失败
        /// </summary>
        public bool UploadFailed { get; set; }

        /// <summary>
        /// GIF 图片宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// GIF 图片高度
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// GIF 图片数据大小
        /// </summary>
        public Int64 Size { get; set; }

        public RCGifMessage()
        {
            this.ObjectName = "RC:GIFMsg";
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Width: {Width} Height: {Height} Size: {Size} UploadFailed: {UploadFailed}";
        }
    }

    [MessageTag( Value = "RC:FileMsg", Flag = MessageTag.IsCounted| MessageTag.IsPersisted)]
    public class RCFileMessage : RCMediaMessageContent
    {
        /// <summary>
        /// 文件大小，单位为 Byte
        /// </summary>
        public Int64 Size { get; set; }

        /// <summary>
        /// 后缀名，默认是 bin
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 文件下载进度
        /// </summary>
        public int Progress { get; set; }

        public RCFileMessage()
        {
            this.ObjectName = "RC:FileMsg";
        }

        public RCFileMessage(string localPath) : this()
        {
#if UNITY_ANDROID
            LocalPath = $"file://{localPath}";
#elif UNITY_IOS
            LocalPath = localPath;
#endif
            var fileInfo = new FileInfo(localPath);
            if (fileInfo.Exists)
            {
                this.Name = fileInfo.Name;
                this.Size = fileInfo.Length;
                this.Type = fileInfo.Extension;
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Type: {Type} Size: {Size} Progress: {Progress}";
        }
    }

    [MessageTag( Value = "RC:ReferenceMsg", Flag = MessageTag.IsCounted| MessageTag.IsPersisted)]
    public class RCReferenceMessage : RCMediaMessageContent
    {
        public string Content { get; set; }
        public string ReferredMsgUserId { get; set; }
        public string ReferredMsgObjectName { get; set; }
        public RCMessageContent ReferredMsg { get; set; }
        public string ReferredMsgUid { get; set; }

        public RCReferenceMessage()
        {
            this.ObjectName = "RC:ReferenceMsg";
        }

        public RCReferenceMessage(string currUid, RCMessageContent referredMsg) : this()
        {
            this.ReferredMsgUserId = currUid;
            this.ReferredMsg = referredMsg;
            this.ReferredMsgObjectName = referredMsg?.ObjectName;
        }

        public override string ToString()
        {
            return
                $"{base.ToString()}, Content: {Content} ReferredMsgUserId: {ReferredMsgUserId} ReferredMsgObjectName: {ReferredMsgObjectName} ReferredMsg: {ReferredMsg} ReferredMsgUid: {ReferredMsgUid}";
        }
    }

    // imkit defined class
    // public class RCCombineMessage : RCMediaMessageContent
    // {
    //     public string Title { get; set; }
    //     public RCConversationType ConversationType { get; set; }
    //
    //     /// <summary>
    //     /// 单聊里最多有两个,群聊不记录
    //     /// </summary>
    //     public IList<String> NameList { get; set; }
    //
    //     /// <summary>
    //     /// 默认消息的内容
    //     /// </summary>
    //     public IList<String> SummaryList { get; set; }
    //
    //     public RCCombineMessage()
    //     {
    //         this.ObjectName = "RC:CombineMsg";
    //     }
    //
    //     public override string ToString()
    //     {
    //         var nameListString = string.Empty;
    //         if (NameList != null)
    //         {
    //             foreach (var item in NameList)
    //             {
    //                 nameListString += $"{item}, ";
    //             }
    //         }
    //
    //         var summaryListString = String.Empty;
    //         if (SummaryList != null)
    //         {
    //             foreach (var item in SummaryList)
    //             {
    //                 summaryListString += $"{item}, ";
    //             }
    //         }
    //
    //         return
    //             $"{base.ToString()}, Title: {Title} RCConversationType: {nameof(RCConversationType)} NameList: {nameListString} SummaryList: {summaryListString}";
    //     }
    // }

    [MessageTag( Value = "RC:ImgTextMsg", Flag = MessageTag.IsCounted| MessageTag.IsPersisted)]
    public class RCRichContentMessage : RCMessageContent
    {
        public string Title { get; set; }
        public string Digest { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }

        public RCRichContentMessage()
        {
            this.ObjectName = "RC:ImgTextMsg";
        }

        public override string ToString()
        {
            return
                $"{base.ToString()}, Title: {Title} Digest: {Digest} ImageUrl: {ImageUrl} Url: {Url}";
        }
    }

    [MessageTag( Value = "RC:GrpNtf", Flag = MessageTag.IsPersisted)]
    public class RCGroupNotificationMessage : RCMessageContent
    {
        /// <summary>
        /// 操作人 UserId，可以为空
        /// </summary>
        public string OperatorUserId { get; set; }

        /// <summary>
        /// 操作名，对应 GroupOperationXxxx，或任意字符串
        /// </summary>
        public string Operation { get; set; }

        /// <summary>
        /// 被操做人 UserId 或者操作数据（如改名后的名称）
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// 操作信息，可以为空，如：你被 xxx 踢出了群
        /// </summary>
        public string Message { get; set; }

        public RCGroupNotificationMessage()
        {
            this.ObjectName = "RC:GrpNtf";
        }

        public override string ToString()
        {
            return
                $"{base.ToString()}, OperatorUserId: {OperatorUserId} Operation: {Operation} Data: {Data} Message: {Message}";
        }
    }

    [MessageTag( Value = "RC:RcCmd", Flag = MessageTag.None)]
    public class RCRecallCommandMessage : RCMessageContent
    {
        public string TargetId { get; set; }
        public RCConversationType ConversationType { get; set; }
        public string MessageUid { get; set; }
        public Int64 SentTime { get; set; }
        public string ChannelId { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDelete { get; set; }
        internal  RCRecallCommandMessage()
        {
            this.ObjectName = "RC:RcCmd";
        }

        public override string ToString()
        {
            return
                $"{base.ToString()} TargetId: {TargetId} Type {ConversationType} MsgUid {MessageUid} sentTime {SentTime} channel {ChannelId} admin {IsAdmin} delete {IsDelete}";
        }
    }
  
    [MessageTag( Value = "RC:RcNtf", Flag = MessageTag.IsPersisted)]
    public class RCRecallNotificationMessage : RCMessageContent
    {
        public string OperatorId { get; internal set; }
        public Int64 RecallTime { get; internal set; }
        public Int64 RecallActionTime { get; internal set; }
        public string OriginalObjectName { get; internal set; }

        public bool IsAdmin { get; internal set; }
        public bool Delete { get; internal set; }
        public string RecallContent { get; internal set; }

        internal RCRecallNotificationMessage()
        {
            this.ObjectName = "RC:RcNtf";
        }

        public override string ToString()
        {
            return
                $"{base.ToString()}, OperatorId: {OperatorId} RecallTime: {RecallTime} RecallActionTime: {RecallActionTime} OriginalObjectName: {OriginalObjectName} IsAdmin: {IsAdmin} Delete: {Delete} RecallContent: {RecallContent}";
        }
    }

    [MessageTag(Value = "RC:ReadNtf", Flag = MessageTag.IsPersisted)]
    public class RCReadReceiptMessage : RCMessageContent
    {
        public Int64 LastMessageSentTime { get; internal set; }

        internal RCReadReceiptMessage()
        {
            this.ObjectName = "RC:ReadNtf";
        }

        public override string ToString()
        {
            return
                $"{base.ToString()}, LastMessageSentTime: {LastMessageSentTime}";
        }
    }

    [MessageTag( Value = "RC:chrmKVNotiMsg", Flag = MessageTag.None)]
    public class RCChatroomKVNotificationMessage : RCMessageContent
    {
        /// <summary>
        /// 聊天室存储消息键值
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 聊天室存储消息数据
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 聊天室存储消息类型
        /// </summary>
        public RCChatroomKVNotificationType Type { get; set; }

        public RCChatroomKVNotificationMessage()
        {
            this.ObjectName = "RC:chrmKVNotiMsg";
        }

        public override string ToString()
        {
            return
                $"{base.ToString()}, Type: {Type}  Key: {Key} Value: {Value}";
        }
    }

    [MessageTag( Value = "RC:ChrmMemChange", Flag = MessageTag.Status)]
    public class RCChatroomMemberActionMessage : RCMessageContent
    {
        public IList<RCChatroomMemberAction> MemberActions { get; set; }

        public RCChatroomMemberActionMessage()
        {
            this.ObjectName = "RC:ChrmMemChange";
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            if (MemberActions != null)
            {
                foreach (var item in MemberActions)
                {
                    builder.Append($"{item.UserId} {item.ActionType}");
                }
            }

            return
                $"{base.ToString()}, MemberActions: {builder}";
        }
    }

    /// <summary>
    /// 自定义消息类型
    /// </summary>
    public enum RCCustomMessageType
    {
        /// <summary>
        /// 命令消息，不存储、不计入未读计数
        /// </summary>
        Command,

        /// <summary>
        /// 存储消息，存储、不计入未读计数
        /// </summary>
        Storage,

        /// <summary>
        /// 普通消息，存储、计入未读计数
        /// </summary>
        Normal,

        /// <summary>
        /// 状态消息，不存储不计数
        /// </summary>
        Status
    }

    /// <summary>
    /// 自定义消息，不支持自定义媒体类型消息.
    /// 使用自定义消息，必须设置 type 指明自定义消息类型，另外还必须设置 objectName ，指明消息对象名称
    /// </summary>
    public class RCCustomMessage : RCMessageContent
    {
        /// <summary>
        /// 发送自定义消息，发送自定义消息必须设置类型
        /// </summary>
        public RCCustomMessageType CustomMessageType { get; private set; }

        /// <summary>
        /// 自定义消息数据集合
        /// </summary>
        public IDictionary<String, String> CustomFields { get; private set; } = new Dictionary<String, String>();

        private RCCustomMessage()
        {

        }

        public RCCustomMessage(String objectName, RCCustomMessageType type, IDictionary<String, string> fields) : this()
        {
            this.ObjectName = objectName;
            this.CustomMessageType = type;
            if (fields != null)
            {
                this.CustomFields = fields;
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("{ ");

            if (this.CustomFields != null && this.CustomFields.Count > 0)
            {
                foreach (var item in this.CustomFields)
                {
                    builder.Append($"{item.Key} : {item.Value} ,");
                }
            }

            builder.Append(" }");
            return
                $"{this.ObjectName} {this.CustomMessageType} {builder.ToString()}";
        }
    }

    public enum RCChatroomKVNotificationType
    {
        SetKey = 1,
        DeleteKey = 2
    }

    public enum RCChatroomMemberActionType
    {
        Unknown = -1,
        MemberJoin = 1,
        MemberQuit = 0
    }

    public class RCChatroomMemberAction
    {
        public string UserId { get; set; }
        public RCChatroomMemberActionType ActionType { get; set; }
    }
}