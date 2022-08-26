//
//  Copyright © 2021 RongCloud. All rights reserved.
//

using System;
using System.Collections.Generic;
using UnityEngine;

namespace cn_rongcloud_im_unity
{
    [Serializable]
    public enum RCConversationType
    {
        // 单聊会话类型
        Private = 1,
        Group = 3,
        // 聊天室会话类型
        ChatRoom = 4,
        System = 6,
    }

    [Serializable]
    public enum RCSentStatus
    {
        Sending = 10,
        Failed = 20,
        Sent = 30,
        Received = 40,
        Read = 50,
        Destoryed = 60,
        Canceled = 70
    }

    [Serializable]
    public enum RCMessageDirection
    {
        Send = 1,
        Receive = 2,
    }

    public class RCReceivedStatus
    {
        public static readonly int Read = 0x1;
        public static readonly int Listened = 0x2;
        public static readonly int Downloaded = 0x4;
        public static readonly int Retrieved = 0x8;
        public static readonly int MultipReceived = 0x10;

        public int Flag { get; private set; }
        public bool IsRead { get { return (Flag & Read) == Read; } }

        public bool IsListened { get { return (Flag & Listened) == Listened; } }
        public bool IsDownloaded { get { return (Flag & Downloaded) == Downloaded; } }
        public bool IsRetrieved { get { return (Flag & Retrieved) == Retrieved; } }
        public bool IsMultipleReceived { get { return (Flag & MultipReceived) == MultipReceived; } }

        internal RCReceivedStatus()
        {

        }

        internal RCReceivedStatus(int flag) : this()
        {
            this.Flag = flag;
        }

        public override string ToString()
        {
            return $"Flag {Flag}";
        }
    }

    public class RCChatRoomInfo
    {
        public string ChatRoomId { get; set; }
        public RCChatRoomMemberOrder Order { get; set; }
        public IList<RCChatRoomMemberInfo> Members { get; set; }
        public int TotalMemberCount { get; set; }

        public RCChatRoomInfo(string chatRoomId, RCChatRoomMemberOrder memberOrder, IList<RCChatRoomMemberInfo> members, int totalMemberCount)
        {
            this.ChatRoomId = chatRoomId;
            this.Order = memberOrder;
            this.Members = members;
            this.TotalMemberCount = totalMemberCount;
        }
    }

    public class RCChatRoomMemberInfo
    {
        public string UserId { get; set; }
        public Int64 JoinTime { get; set; }

        public RCChatRoomMemberInfo(string userId, Int64 joinTime)
        {
            this.UserId = userId;
            this.JoinTime = joinTime;
        }
    }

    public enum RCChatRoomDestroyType
    {
        /// <summary>
        /// 未知原因
        /// </summary>
        Unknown = -1,
        /// <summary>
        /// 由用户销毁
        /// </summary>
        Manual = 0,
        /// <summary>
        /// 系统自动销毁
        /// </summary>
        Auto = 3,
    }

    public enum RCConversationNotificationStatus
    {
        DoNotDisturb = 0,
        Notify = 1,
    }

    public enum RCChatRoomMemberOrder
    {
        Asc = 1,
        Desc = 2,
    }

    public enum RCBlackListStatus
    {
        InBlackList = 0,
        NotInBlackList = 1,
    }

    public enum RCMentionedType
    {
        All = 1,
        Users = 2,
    }

    public enum RCConnectionStatus
    {
        Unknown = -1,

        /// <summary>
        /// 连接成功
        /// </summary>
        Connected = 0,

        /// <summary>
        /// 连接中
        /// </summary>
        Connecting = 1,

        /// <summary>
        /// 用户账户在其他设备登录，本机会被踢掉线
        /// </summary>
        KickedByOtherClient = 2,

        /// <summary>
        /// 网络不可用
        /// </summary>
        NetworkUnavailable = 3,

        /// <summary>
        /// Token 不正确
        /// </summary>
        TokenIncorrect = 4,

        /// <summary>
        /// 用户被开发者后台封禁
        /// </summary>
        UserBlocked = 5,

        /// <summary>
        /// 用户主动调用 disconnect 或 logout 接口断开连接
        /// </summary>
        DisConnected = 6,

        /// <summary>
        /// 连接暂时挂起（多是由于网络问题导致），SDK 会在合适时机进行自动重连
        /// </summary>
        Suspend = 13,

        /// <summary>
        /// 自动连接超时，SDK 将不会继续连接，用户需要做超时处理，再自行调用 connectWithToken 接口进行连接
        /// </summary>
        Timeout = 14,
    }

    public enum RCErrorCode
    {
        /// <summary>
        /// 应用没有调用 connect() 方法，即调用业务, 请在连接成功后调用此方法
        /// </summary>
        AppNotConnect = -4,

        /// <summary>
        /// 取消暂停失败
        /// </summary>
        OperationMediaNotFound = -3,

        /// <summary>
        /// Android IPC 进程意外终止
        /// </summary>
        IpcDisConnect = -2,
        UnKnown = -1,

        /// 成功
        Succeed = 0,

        ///已被对方加入黑名单
        RejectedByBlackList = 405,

        ///发送消息频率过高，1秒最多允许发送5条消息
        SendMsgOverFrequency = 20604,

        /// <summary>
        /// 操作被禁止, 此错误码已被弃用
        /// </summary>
        OperationBlocked = 20605,

        /// <summary>
        /// 操作不支持, 仅私有云有效，服务端禁用了该操作
        /// </summary>
        OperationNotSupport = 20606,

        /// <summary>
        /// 请求超出了调用频率限制，请稍后再试。 <p>接口调用过于频繁，请稍后再试。</p>
        /// </summary>
        RequestOverFrequency = 20607,

        ///不在该群组中
        NotInGroup = 22406,

        ///在群组中被禁言
        ForbiddenInGroup = 22408,

        ///不在该聊天室中
        NotInChatRoom = 23406,

        /// <summary>
        /// 聊天室状态值不存在
        /// </summary>
        ChatRoomKeyNotExist = 23427,

        /// <summary>
        /// 聊天室批量设置或删除KV部分不成功
        /// </summary>
        ChatRoomKVStoreNotAllSuccess = 23428,

        /// <summary>
        /// 聊天室批量设置或删除KV数量超限（最多一次10条）
        /// </summary>
        ChatRoomKVStoreOutOfLimit = 23429,

        ///在聊天室中被禁言
        ForbiddenInChatRoom = 23408,

        ///AppKey 错误
        AppKeyError = 31002,

        ///token 无效，需要获取新的 token 连接 IM
        ///一般有已下两种原因
        ///一是token错误，请您检查客户端初始化使用的AppKey和您服务器获取token使用的AppKey是否一致；
        ///二是token过期，是因为您在开发者后台设置了token过期时间，您需要请求您的服务器重新获取token并再次用新的token建立连接。
        TokenIncorrect = 31004,

        /// AppKey 与 Token 不匹配，需要获取新的 token 连接 IM
        /// 原因同 [TokenIncorrect]
        NotAuthrorized = 31005,

        /// AppKey 被封禁或者已删除，请检查 AppKey 是否正确
        AppBlockedOrDeleted = 31008,

        /// 用户被封禁，请检查 Token 是否正确以及对应的 UserId 是否被封禁
        UserBlocked = 31009,
        
        /// <summary>
        /// 连接过于频繁
        /// </summary>
        ConnOverFrequency = 30015,
        
        /// <summary>
        /// 消息大小超限，消息体（序列化成 json 格式之后的内容）最大 128k bytes。
        /// </summary>
        MessageSizeOutOfLimit = 30016,

        /// 被其他端踢掉线
        KickByOtherClient = 31010,

        /// SDK 没有初始化，使用任何 SDK 接口前必须先调用 init 接口
        ClientNotInit = 33001,

        /// 非法参数，请检查调用接口传入的参数
        InvalidParameter = 33003,

        /// 历史消息云存储功能未开通
        RoamingServiceUnAvailable = 33007,

        /// <summary>
        /// 聊天室被重置
        /// </summary>
        ChatRoomReset = 33009,

        /// <summary>
        /// 连接已存在
        /// </summary>
        ConnectionExist = 34001,

        /// 小视频消息时长超限，最长 10s
        SightMessageDurationLimitExceed = 34002,
    }

    public enum RCOperationStatus
    {
        Success = 0,
        Failed = 1,
    }

    public enum RCTimestampOrder
    {
        Desc = 0,
        Asc = 1,
    }

    public enum RCBlockType
    {
        /// 未知类型
        Unknown = 0,

        /// 全局敏感词：命中了融云内置的全局敏感词
        Global = 1,

        /// 自定义敏感词拦截：命中了客户在融云自定义的敏感词
        Custom = 2,

        /// 第三方审核拦截：命中了第三方（数美）或模板路由决定不下发的状态
        Thirdparty = 3,
    }
    public enum RCPullOrder
    {
        /// <summary>
        /// 降序，结合传入的时间戳参数，获取发送时间递增的消息
        /// </summary>
        Des = 0,
        /// <summary>
        /// 升序，结合传入的时间戳参数，获取发送时间递减的消息
        /// </summary>
        Asc = 1
    }
    public class RCHistoryMessageOption
    {
        /// <summary>
        /// 从该时间点开始获取消息。即：消息中的 sentTime；如果本地库中没有消息，第一次可传 0，否则传入最早消息的 sentTime，获取最新 count 条。
        /// </summary>
        public Int64 DateTime { get; set; }
        /// <summary>
        /// 要获取的消息数量，最多 20 条
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 拉取顺序
        /// </summary>
        public RCPullOrder PullOrder { get; set; }
    }

    public class RCNotificationQuietHourInfo
    {
        /// <summary>
        /// 消息通知免打扰时间, 格式 HH:MM:SS
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 间隔分钟数 0 &lt; spanMins &lt; 1440。
        /// </summary>
        public int SpanMinutes { get; set; }

        public RCNotificationQuietHourInfo(string startTime, int spanMinutes)
        {
            this.StartTime = startTime;
            this.SpanMinutes = spanMinutes;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}: StartTime {StartTime} SpanMinutes {SpanMinutes}";
        }
    }

    public class RCTagInfo
    {
        /// <summary>
        /// 标签唯一标识，字符型，长度不超过 10 个字符
        /// </summary>
        public string TagId { get; set; }
        /// <summary>
        /// 长度不超过 15 个字，标签名称可以重复
        /// </summary>
        public string TagName { get; set; }
        /// <summary>
        /// 匹配的会话个数
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 时间戳由协议栈提供
        /// </summary>
        public Int64 TimeStamp { get; set; }

        public RCTagInfo(string tagId, string tagName)
        {
            this.TagId = tagId;
            this.TagName = tagName;
        }

        public RCTagInfo(string tagId, string tagName, int count, Int64 timestamp)
        {
            this.TagId = tagId;
            this.TagName = tagName;
            this.Count = count;
            this.TimeStamp = timestamp;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}: id {TagId}, name {TagName}, count {Count}, ts {TimeStamp}";
        }
    }

    public class RCConversationTagInfo
    {
        public RCTagInfo TagInfo { get; set; }
        public bool IsTop { get; set; }

        public RCConversationTagInfo(RCTagInfo tagInfo, bool isTop)
        {
            this.TagInfo = tagInfo;
            this.IsTop = isTop;
        }

        public override string ToString()
        {
            return $"{TagInfo} isTopped {IsTop}";
        }
    }
}