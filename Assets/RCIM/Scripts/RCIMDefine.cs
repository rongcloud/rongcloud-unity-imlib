using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public enum RCIMImportanceHW
    {
        /// <summary>
        /// 表示消息为服务与通讯类。消息提醒方式为锁屏+铃声+震动
        /// </summary>
        NORMAL,
    
        /// <summary>
        /// 表示消息为资讯营销类。消息提醒方式为静默通知，仅在下拉通知栏展示
        /// </summary>
        LOW
    }
    
    public enum RCIMMessageOperationPolicy
    {
        /// <summary>
        /// 本地
        /// </summary>
        LOCAL,
    
        /// <summary>
        /// 远端
        /// </summary>
        REMOTE,
    
        /// <summary>
        /// 本地和远端
        /// </summary>
        LOCAL_REMOTE
    }
    
    public enum RCIMVIVOPushType
    {
        /// <summary>
        /// 运营消息
        /// </summary>
        OPERATE,
    
        /// <summary>
        /// 系统消息
        /// </summary>
        SYSTEM
    }
    
    public enum RCIMSentStatus
    {
        /// <summary>
        /// 发送中
        /// </summary>
        SENDING,
    
        /// <summary>
        /// 发送失败
        /// </summary>
        FAILED,
    
        /// <summary>
        /// 已发送
        /// </summary>
        SENT,
    
        /// <summary>
        /// 对方已接收
        /// </summary>
        RECEIVED,
    
        /// <summary>
        /// 对方已读
        /// </summary>
        READ,
    
        /// <summary>
        /// 对方已销毁
        /// </summary>
        DESTROYED,
    
        /// <summary>
        /// 对方已取消
        /// </summary>
        CANCELED
    }
    
    public enum RCIMPushNotificationQuietHoursLevel
    {
        /// <summary>
        /// 未设置。如未设置，SDK 会依次查询消息所属群的用户级别免打扰设置及其他非用户级别设置，再判断是否需要推送通知
        /// </summary>
        NONE,
    
        /// <summary>
        /// 与融云服务端断开连接后，当前用户仅在指定时段内针对指定会话中提及（@）当前用户和全体群成员的消息接收通知
        /// </summary>
        MENTION_MESSAGE,
    
        /// <summary>
        /// 当前用户在指定时段内针对任何消息都不接收推送通知
        /// </summary>
        BLOCKED
    }
    
    public enum RCIMMessageDirection
    {
        /// <summary>
        /// 发送方
        /// </summary>
        SEND,
    
        /// <summary>
        /// 接收方
        /// </summary>
        RECEIVE
    }
    
    public enum RCIMReceivedStatus
    {
        /// <summary>
        /// 未读
        /// </summary>
        UNREAD,
    
        /// <summary>
        /// 已读
        /// </summary>
        READ,
    
        /// <summary>
        /// 已听
        /// </summary>
        LISTENED,
    
        /// <summary>
        /// 已下载
        /// </summary>
        DOWNLOADED,
    
        /// <summary>
        /// 该消息已经被其他登录的多端收取过。（即该消息已经被其他端收取过后。当前端才登录，并重新拉取了这条消息。客户可以通过这个状态更新
        /// UI，比如不再提示）
        /// </summary>
        RETRIEVED,
    
        /// <summary>
        /// 该消息是被多端同时收取的。（即其他端正同时登录，一条消息被同时发往多端。客户可以通过这个状态值更新自己的某些 UI
        /// 状态）
        /// </summary>
        MULTIPLE_RECEIVE
    }
    
    public enum RCIMChatRoomMemberActionType
    {
        /// <summary>
        /// 未知
        /// </summary>
        UNKNOWN,
    
        /// <summary>
        /// 已加入
        /// </summary>
        JOIN,
    
        /// <summary>
        /// 已离开
        /// </summary>
        LEAVE
    }
    
    public enum RCIMPushNotificationLevel
    {
        /// <summary>
        /// 与融云服务端断开连接后，当前用户可针对指定类型会话中的所有消息接收通知
        /// </summary>
        ALL_MESSAGE,
    
        /// <summary>
        /// 未设置。未设置时均为此初始状态
        /// </summary>
        NONE,
    
        /// <summary>
        /// 与融云服务端断开连接后，当前用户仅针对指定类型的会话中提及（@）当前用户和全体群成员的消息接收通知
        /// </summary>
        MENTION,
    
        /// <summary>
        /// 与融云服务端断开连接后，当前用户仅针对指定类型的会话中提及（@）当前用户的消息接收通知。例如：张三只会接收 “@张三
        /// Hello” 的消息的通知
        /// </summary>
        MENTION_USERS,
    
        /// <summary>
        /// 与融云服务端断开连接后，当前用户仅针对指定类型的会话中提及（@）全部群成员的消息接收通知
        /// </summary>
        MENTION_ALL,
    
        /// <summary>
        /// 当前用户针对指定类型的会话中的任何消息都不接收推送通知
        /// </summary>
        BLOCKED
    }
    
    public enum RCIMMessageType
    {
        /// <summary>
        /// 无效类型
        /// </summary>
        UNKNOWN,
    
        /// <summary>
        /// 自定义
        /// </summary>
        CUSTOM,
    
        /// <summary>
        /// 文本
        /// </summary>
        TEXT,
    
        /// <summary>
        /// 语音
        /// </summary>
        VOICE,
    
        /// <summary>
        /// 图片
        /// </summary>
        IMAGE,
    
        /// <summary>
        /// 文件
        /// </summary>
        FILE,
    
        /// <summary>
        /// 小视频
        /// </summary>
        SIGHT,
    
        /// <summary>
        /// GIF 图
        /// </summary>
        GIF,
    
        /// <summary>
        /// 撤回
        /// </summary>
        RECALL,
    
        /// <summary>
        /// 引用
        /// </summary>
        REFERENCE,
    
        /// <summary>
        /// 命令
        /// </summary>
        COMMAND,
    
        /// <summary>
        /// 命令通知
        /// </summary>
        COMMAND_NOTIFICATION,
    
        /// <summary>
        /// 位置消息
        /// </summary>
        LOCATION,
    
        /// <summary>
        /// 用户自定义消息
        /// </summary>
        USER_CUSTOM
    }
    
    public enum RCIMMessageBlockType
    {
        /// <summary>
        /// 未知
        /// </summary>
        UNKNOWN,
    
        /// <summary>
        /// 全局敏感词：命中了融云内置的全局敏感词
        /// </summary>
        GLOBAL,
    
        /// <summary>
        /// 自定义敏感词拦截：命中了客户在融云自定义的敏感词
        /// </summary>
        CUSTOM,
    
        /// <summary>
        /// 第三方审核拦截：命中了第三方（数美）或模板路由决定不下发的状态
        /// </summary>
        THIRD_PARTY
    }
    
    public enum RCIMTimeOrder
    {
        /// <summary>
        /// 时间递减
        /// </summary>
        BEFORE,
    
        /// <summary>
        /// 时间递增
        /// </summary>
        AFTER
    }
    
    public enum RCIMCustomMessagePolicy
    {
        /// <summary>
        /// 客户端不存储，支持离线消息机制，不计入未读消息数
        /// </summary>
        COMMAND,
    
        /// <summary>
        /// 客户端存储，支持离线消息机制，且存入服务端历史消息，计入未读消息数
        /// </summary>
        NORMAL,
    
        /// <summary>
        /// 客户端不存储，服务端不存储，不计入未读消息数
        /// </summary>
        STATUS,
    
        /// <summary>
        /// 客户端存储，支持离线消息机制，且存入服务端历史消息，不计入未读消息数
        /// </summary>
        STORAGE
    }
    
    public enum RCIMChatRoomStatus
    {
        /// <summary>
        /// 聊天室被重置
        /// </summary>
        RESET,
    
        /// <summary>
        /// 用户调用IM Server API 手动销毁聊天室
        /// </summary>
        DESTROY_MANUAL,
    
        /// <summary>
        /// IM Server 自动销毁聊天室
        /// </summary>
        DESTROY_AUTO
    }
    
    public enum RCIMConversationType
    {
        /// <summary>
        /// 暂不支持，SDK 保留类型，开发者不可使用
        /// </summary>
        INVALID,
    
        /// <summary>
        /// 单聊会话
        /// </summary>
        PRIVATE,
    
        /// <summary>
        /// 群聊会话
        /// </summary>
        GROUP,
    
        /// <summary>
        /// 聊天室会话
        /// </summary>
        CHATROOM,
    
        /// <summary>
        /// 系统会话
        /// </summary>
        SYSTEM,
    
        /// <summary>
        /// 超级群会话
        /// </summary>
        ULTRA_GROUP
    }
    
    public enum RCIMErrorCode
    {
        /// <summary>
        ///
        /// </summary>
        SUCCESS,
    
        /// <summary>
        ///
        /// </summary>
        PARAM_ERROR,
    
        /// <summary>
        ///
        /// </summary>
        ENGINE_DESTROYED,
    
        /// <summary>
        ///
        /// </summary>
        NATIVE_OPERATION_ERROR,
    
        /// <summary>
        ///
        /// </summary>
        RESULT_UNKNOWN
    }
    
    public enum RCIMUltraGroupTypingStatus
    {
        /// <summary>
        /// 正在输入文本
        /// </summary>
        TEXT
    }
    
    public enum RCIMMentionedType
    {
        /// <summary>
        /// @ 所有人
        /// </summary>
        ALL,
    
        /// <summary>
        /// @ 指定的人
        /// </summary>
        PART
    }
    
    public enum RCIMChatRoomEntriesOperationType
    {
        /// <summary>
        /// 更新操作
        /// </summary>
        Update,
    
        /// <summary>
        /// 删除操作
        /// </summary>
        Remove
    }
    
    public enum RCIMLogLevel
    {
        /// <summary>
        /// 不输出任何日志
        /// </summary>
        NONE,
    
        /// <summary>
        /// 只输出错误的日志
        /// </summary>
        ERROR,
    
        /// <summary>
        /// 输出错误和警告的日志
        /// </summary>
        WARN,
    
        /// <summary>
        /// 输出错误、警告和一般的日志
        /// </summary>
        INFO,
    
        /// <summary>
        /// 输出输出错误、警告和一般的日志以及 debug 日志
        /// </summary>
        DEBUG,
    
        /// <summary>
        /// 输出所有日志
        /// </summary>
        VERBOSE
    }
    
    public enum RCIMBlacklistStatus
    {
        /// <summary>
        /// 未知
        /// </summary>
        UNKNOWN,
    
        /// <summary>
        /// 在黑名单中
        /// </summary>
        IN_BLACKLIST,
    
        /// <summary>
        /// 不在黑名单
        /// </summary>
        NOT_IN_BLACKLIST
    }
    
    public enum RCIMConnectionStatus
    {
        /// <summary>
        /// 网络不可用
        /// </summary>
        NETWORK_UNAVAILABLE,
    
        /// <summary>
        /// 连接成功
        /// </summary>
        CONNECTED,
    
        /// <summary>
        /// 连接中
        /// </summary>
        CONNECTING,
    
        /// <summary>
        /// 未连接
        /// </summary>
        UNCONNECTED,
    
        /// <summary>
        /// 用户账户在其他设备登录，本机会被踢掉线
        /// </summary>
        KICKED_OFFLINE_BY_OTHER_CLIENT,
    
        /// <summary>
        /// Token 不正确
        /// </summary>
        TOKEN_INCORRECT,
    
        /// <summary>
        /// 用户被开发者后台封禁
        /// </summary>
        CONN_USER_BLOCKED,
    
        /// <summary>
        /// 用户主动调用 disconnect 或 logout 接口断开连接
        /// </summary>
        SIGN_OUT,
    
        /// <summary>
        /// 连接暂时挂起（多是由于网络问题导致），SDK 会在合适时机进行自动重连
        /// </summary>
        SUSPEND,
    
        /// <summary>
        /// 自动连接超时，SDK 将不会继续连接，用户需要做超时处理，再自行调用 connectWithToken 接口进行连接
        /// </summary>
        TIMEOUT,
    
        /// <summary>
        /// 异常情况
        /// </summary>
        UNKNOWN
    }
}
