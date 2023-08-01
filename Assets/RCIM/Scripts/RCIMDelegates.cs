using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    /// <summary>
    /// 收到消息的监听
    /// </summary>
    /// <param name="message">接收到的消息对象</param>
    /// <param name="left">
    /// 当客户端连接成功后，服务端会将所有补偿消息以消息包的形式下发给客户端，最多每 200 条消息为一个消息包，即一个 Package,
    /// 客户端接受到消息包后，会逐条解析并通知应用。left 为当前消息包（Package）里还剩余的消息条数
    /// </param>
    /// <param name="offline">消息是否离线消息</param>
    /// <param name="hasPackage">是否在服务端还存在未下发的消息包</param>
    public delegate void OnMessageReceivedDelegate(RCIMMessage message, int left, bool offline, bool hasPackage);
    
    /// <summary>
    /// 网络状态变化
    /// </summary>
    /// <param name="status">SDK 与融云服务器的连接状态</param>
    public delegate void OnConnectionStatusChangedDelegate(RCIMConnectionStatus status);
    
    /// <summary>
    /// 会话状态置顶多端同步监听
    /// </summary>
    /// <param name="type">会话类型</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">
    /// 频道 ID，仅支持超级群使用，其他会话类型传 null 即可。 频道 ID，仅支持超级群使用，其他会话类型传 null 即可
    /// </param>
    /// <param name="top">是否置顶</param>
    public delegate void OnConversationTopStatusSyncedDelegate(RCIMConversationType type, string targetId, string channelId,
                                                               bool top);
    
    /// <summary>
    /// 会话状态免打扰多端同步监听
    /// </summary>
    /// <param name="type">会话类型</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="level">当前会话通知的类型</param>
    public delegate void OnConversationNotificationLevelSyncedDelegate(RCIMConversationType type, string targetId,
                                                                       string channelId, RCIMPushNotificationLevel level);
    
    /// <summary>
    /// 撤回消息监听器
    /// </summary>
    /// <param name="message">原本的消息会变为撤回消息</param>
    public delegate void OnRemoteMessageRecalledDelegate(RCIMMessage message);
    
    /// <summary>
    /// 单聊中收到消息回执的回调
    /// </summary>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="timestamp">已阅读的最后一条消息的 sendTime</param>
    public delegate void OnPrivateReadReceiptReceivedDelegate(string targetId, string channelId, long timestamp);
    
    /// <summary>
    /// 消息扩展信息更改的回调
    /// </summary>
    /// <param name="expansion">
    /// 消息扩展信息中更新的键值对，只包含更新的键值对，不是全部的数据。如果想获取全部的键值对，请使用 message 的 expansion
    /// 属性
    /// </param>
    /// <param name="message">发生变化的消息</param>
    public delegate void OnRemoteMessageExpansionUpdatedDelegate(Dictionary<string, string> expansion, RCIMMessage message);
    
    /// <summary>
    /// 消息扩展信息删除的回调
    /// </summary>
    /// <param name="message">发生变化的消息</param>
    /// <param name="keys">消息扩展信息中删除的键值对 key 列表</param>
    public delegate void OnRemoteMessageExpansionForKeyRemovedDelegate(RCIMMessage message, List<string> keys);
    
    /// <summary>
    /// 聊天室用户进入、退出聊天室监听
    /// </summary>
    /// <param name="targetId">会话 ID</param>
    /// <param name="actions">发生的事件</param>
    public delegate void OnChatRoomMemberChangedDelegate(string targetId, List<RCIMChatRoomMemberAction> actions);
    
    /// <summary>
    /// 会话输入状态发生变化。对于单聊而言，当对方正在输入时，监听会触发一次；当对方不处于输入状态时，该监听还会触发一次，但回调里输入用户列表为空
    /// </summary>
    /// <param name="type">会话类型</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="userTypingStatus">发生状态变化的集合</param>
    public delegate void OnTypingStatusChangedDelegate(RCIMConversationType type, string targetId, string channelId,
                                                       List<RCIMTypingStatus> userTypingStatus);
    
    /// <summary>
    /// 同步消息未读状态监听接口。多端登录，收到其它端清除某一会话未读数通知的时候
    /// </summary>
    /// <param name="type">会话类型</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="timestamp">时间戳</param>
    public delegate void OnConversationReadStatusSyncMessageReceivedDelegate(RCIMConversationType type, string targetId,
                                                                             long timestamp);
    
    /// <summary>
    /// 聊天室 KV 同步完成的回调
    /// </summary>
    /// <param name="roomId">聊天室 ID</param>
    public delegate void OnChatRoomEntriesSyncedDelegate(string roomId);
    
    /// <summary>
    /// 聊天室 KV 发生变化的回调
    /// </summary>
    /// <param name="operationType">操作的类型</param>
    /// <param name="roomId">聊天室 ID</param>
    /// <param name="entries">发送变化的 KV</param>
    public delegate void OnChatRoomEntriesChangedDelegate(RCIMChatRoomEntriesOperationType operationType, string roomId,
                                                          Dictionary<string, string> entries);
    
    /// <summary>
    /// 超级群消息 kv 被更新
    /// </summary>
    /// <param name="messages">被更新的消息集合</param>
    public delegate void OnRemoteUltraGroupMessageExpansionUpdatedDelegate(List<RCIMMessage> messages);
    
    /// <summary>
    /// 超级群消息被更改
    /// </summary>
    /// <param name="messages">被更新的消息集合</param>
    public delegate void OnRemoteUltraGroupMessageModifiedDelegate(List<RCIMMessage> messages);
    
    /// <summary>
    /// 超级群消息被撤回
    /// </summary>
    /// <param name="messages">撤回的消息集合</param>
    public delegate void OnRemoteUltraGroupMessageRecalledDelegate(List<RCIMMessage> messages);
    
    /// <summary>
    /// 超级群已读的监听
    /// </summary>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="timestamp"></param>
    public delegate void OnUltraGroupReadTimeReceivedDelegate(string targetId, string channelId, long timestamp);
    
    /// <summary>
    /// 用户输入状态变化的回调。当客户端收到用户输入状态的变化时，会回调此接口，通知发生变化的会话以及当前正在输入的RCUltraGroupTypingStatusInfo列表
    /// </summary>
    /// <param name="info">
    /// 正在输入的RCUltraGroupTypingStatusInfo列表（nil标示当前没有用户正在输入）
    /// </param>
    public delegate void OnUltraGroupTypingStatusChangedDelegate(List<RCIMUltraGroupTypingStatusInfo> info);
    
    /// <summary>
    /// 发送含有敏感词消息被拦截的回调
    /// </summary>
    /// <param name="info">被拦截消息的相关信息</param>
    public delegate void OnMessageBlockedDelegate(RCIMBlockedMessageInfo info);
    
    /// <summary>
    /// 聊天室状态发生变化的监听
    /// </summary>
    /// <param name="targetId">会话 ID</param>
    /// <param name="status">聊天室变化的状态</param>
    public delegate void OnChatRoomStatusChangedDelegate(string targetId, RCIMChatRoomStatus status);
    
    /// <summary>
    /// 收到群聊已读回执请求的监听
    /// </summary>
    /// <param name="targetId">会话 ID</param>
    /// <param name="messageUId">消息的 messageUid</param>
    public delegate void OnGroupMessageReadReceiptRequestReceivedDelegate(string targetId, string messageUId);
    
    /// <summary>
    /// 收到群聊已读回执响应的监听
    /// </summary>
    /// <param name="targetId">会话 ID</param>
    /// <param name="messageUId">收到回执响应的消息的 messageUId</param>
    /// <param name="respondUserIds">
    /// 会话中响应了此消息的用户列表。其中 key： 用户 id ； value： 响应时间
    /// </param>
    public delegate void OnGroupMessageReadReceiptResponseReceivedDelegate(string targetId, string messageUId,
                                                                           Dictionary<string, long> respondUserIds);
    
    /// <summary>
    /// connect 的接口监听，收到链接结果的回调
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="userId">链接成功的用户 ID</param>
    public delegate void OnConnectedDelegate(int code, string userId);
    
    /// <summary>
    /// connect 的接口监听，数据库打开时发生的回调
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    public delegate void OnDatabaseOpenedDelegate(int code);
    
    /// <summary>
    /// loadConversation 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="type">会话类型</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="conversation">获取到的会话</param>
    public delegate void OnConversationLoadedDelegate(int code, RCIMConversationType type, string targetId,
                                                      string channelId, RCIMConversation conversation);
    
    /// <summary>
    /// loadConversations 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="conversationTypes">会话类型集合</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，</param>
    /// <param name="startTime">时间戳（毫秒）</param>
    /// <param name="count">查询的数量</param>
    /// <param name="conversations">查询到的会话集合</param>
    public delegate void OnConversationsLoadedDelegate(int code, List<RCIMConversationType> conversationTypes,
                                                       string channelId, long startTime, int count,
                                                       List<RCIMConversation> conversations);
    
    /// <summary>
    /// removeConversation 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="type">会话类型</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    public delegate void OnConversationRemovedDelegate(int code, RCIMConversationType type, string targetId,
                                                       string channelId);
    
    /// <summary>
    /// removeConversations 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="conversationTypes">会话类型集合</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    public delegate void OnConversationsRemovedDelegate(int code, List<RCIMConversationType> conversationTypes,
                                                        string channelId);
    
    /// <summary>
    /// loadTotalUnreadCount 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="count">未读数量</param>
    public delegate void OnTotalUnreadCountLoadedDelegate(int code, string channelId, int count);
    
    /// <summary>
    /// loadUnreadCount 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="type">会话类型</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="count">未读数量</param>
    public delegate void OnUnreadCountLoadedDelegate(int code, RCIMConversationType type, string targetId, string channelId,
                                                     int count);
    
    /// <summary>
    /// loadUnreadCountByConversationTypes 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="conversationTypes">会话类型集合</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="contain">是否包含免打扰消息的未读消息数</param>
    /// <param name="count">未读数量</param>
    public delegate void OnUnreadCountByConversationTypesLoadedDelegate(int code,
                                                                        List<RCIMConversationType> conversationTypes,
                                                                        string channelId, bool contain, int count);
    
    /// <summary>
    /// loadUnreadMentionedCount 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="type">会话类型</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="count">未读数量</param>
    public delegate void OnUnreadMentionedCountLoadedDelegate(int code, RCIMConversationType type, string targetId,
                                                              string channelId, int count);
    
    /// <summary>
    /// loadUltraGroupAllUnreadMentionedCount 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="count">未读数量</param>
    public delegate void OnUltraGroupAllUnreadCountLoadedDelegate(int code, int count);
    
    /// <summary>
    /// loadUltraGroupAllUnreadMentionedCount 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="count">未读数量</param>
    public delegate void OnUltraGroupAllUnreadMentionedCountLoadedDelegate(int code, int count);
    
    /// <summary>
    /// 超级群列表同步完成的回调
    /// </summary>
    public delegate void OnUltraGroupConversationsSyncedDelegate();
    
    /// <summary>
    /// clearUnreadCount 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="type">会话类型</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="timestamp">该会话已阅读的最后一条消息的发送时间戳</param>
    public delegate void OnUnreadCountClearedDelegate(int code, RCIMConversationType type, string targetId,
                                                      string channelId, long timestamp);
    
    /// <summary>
    /// saveDraftMessage 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="type">会话类型</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="draft">草稿信息</param>
    public delegate void OnDraftMessageSavedDelegate(int code, RCIMConversationType type, string targetId, string channelId,
                                                     string draft);
    
    /// <summary>
    /// clearDraftMessage 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="type">会话类型</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    public delegate void OnDraftMessageClearedDelegate(int code, RCIMConversationType type, string targetId,
                                                       string channelId);
    
    /// <summary>
    /// loadDraftMessage 的接口监听
    /// </summary>
    /// <param name="code">
    /// 接口回调的状态码，0 代表成功，非 0 代表出现异常 接口回调的状态码，0 代表成功，非 0 代表出现异常
    /// </param>
    /// <param name="type">会话类型</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="draft">草稿信息</param>
    public delegate void OnDraftMessageLoadedDelegate(int code, RCIMConversationType type, string targetId,
                                                      string channelId, string draft);
    
    /// <summary>
    /// loadBlockedConversations 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="conversationTypes">会话类型集合</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="conversations">获取到的会话集合</param>
    public delegate void OnBlockedConversationsLoadedDelegate(int code, List<RCIMConversationType> conversationTypes,
                                                              string channelId, List<RCIMConversation> conversations);
    
    /// <summary>
    /// changeConversationTopStatus 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="type">会话类型</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="top">是否置顶</param>
    public delegate void OnConversationTopStatusChangedDelegate(int code, RCIMConversationType type, string targetId,
                                                                string channelId, bool top);
    
    /// <summary>
    /// loadConversationTopStatus 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="type">会话类型</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="top">是否置顶</param>
    public delegate void OnConversationTopStatusLoadedDelegate(int code, RCIMConversationType type, string targetId,
                                                               string channelId, bool top);
    
    /// <summary>
    /// syncConversationReadStatus 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="type">会话类型</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="timestamp">会话中已读的最后一条消息的发送时间戳</param>
    public delegate void OnConversationReadStatusSyncedDelegate(int code, RCIMConversationType type, string targetId,
                                                                string channelId, long timestamp);
    
    /// <summary>
    /// sendMessage 的接口监听
    /// </summary>
    /// <param name="message">发送的消息</param>
    public delegate void OnMessageAttachedDelegate(RCIMMessage message);
    
    /// <summary>
    /// sendMessage 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="message">发送的消息</param>
    public delegate void OnMessageSentDelegate(int code, RCIMMessage message);
    
    /// <summary>
    /// sendMediaMessage 的接口监听
    /// </summary>
    /// <param name="message">发送的消息</param>
    public delegate void OnMediaMessageAttachedDelegate(RCIMMediaMessage message);
    
    /// <summary>
    /// sendMediaMessage 的接口监听
    /// </summary>
    /// <param name="message">发送的消息</param>
    /// <param name="progress">发送的进度</param>
    public delegate void OnMediaMessageSendingDelegate(RCIMMediaMessage message, int progress);
    
    /// <summary>
    /// cancelSendingMediaMessage 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="message">发送的消息</param>
    public delegate void OnSendingMediaMessageCanceledDelegate(int code, RCIMMediaMessage message);
    
    /// <summary>
    /// sendMediaMessage 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="message">发送的消息</param>
    public delegate void OnMediaMessageSentDelegate(int code, RCIMMediaMessage message);
    
    /// <summary>
    /// downloadMediaMessage 的接口监听
    /// </summary>
    /// <param name="message">下载的消息</param>
    /// <param name="progress">下载的进度</param>
    public delegate void OnMediaMessageDownloadingDelegate(RCIMMediaMessage message, int progress);
    
    /// <summary>
    /// downloadMediaMessage 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="message">下载的消息</param>
    public delegate void OnMediaMessageDownloadedDelegate(int code, RCIMMediaMessage message);
    
    /// <summary>
    /// cancelDownloadingMediaMessage 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="message">取消下载的消息</param>
    public delegate void OnDownloadingMediaMessageCanceledDelegate(int code, RCIMMediaMessage message);
    
    /// <summary>
    /// loadMessages 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="type">会话类型</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="sentTime">当前消息时间戳</param>
    /// <param name="order">
    /// 获取消息的方向。BEFORE：获取 sentTime 之前的消息 （时间递减），AFTER：获取 sentTime 之后的消息 （时间递增）
    /// </param>
    /// <param name="messages">获取到的消息集合</param>
    public delegate void OnMessagesLoadedDelegate(int code, RCIMConversationType type, string targetId, string channelId,
                                                  long sentTime, RCIMTimeOrder order, List<RCIMMessage> messages);
    
    /// <summary>
    /// loadUnreadMentionedMessages 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="type">会话类型</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="messages">获取到的消息集合</param>
    public delegate void OnUnreadMentionedMessagesLoadedDelegate(int code, RCIMConversationType type, string targetId,
                                                                 string channelId, List<RCIMMessage> messages);
    
    /// <summary>
    /// loadFirstUnreadMessage 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="type">会话类型</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="message">获取到的消息</param>
    public delegate void OnFirstUnreadMessageLoadedDelegate(int code, RCIMConversationType type, string targetId,
                                                            string channelId, RCIMMessage message);
    
    /// <summary>
    /// insertMessage 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="message">插入的消息</param>
    public delegate void OnMessageInsertedDelegate(int code, RCIMMessage message);
    
    /// <summary>
    /// insertMessages 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="messages">插入的消息集合</param>
    public delegate void OnMessagesInsertedDelegate(int code, List<RCIMMessage> messages);
    
    /// <summary>
    /// clearMessages 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="type">会话类型</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="timestamp">时间戳</param>
    public delegate void OnMessagesClearedDelegate(int code, RCIMConversationType type, string targetId, string channelId,
                                                   long timestamp);
    
    /// <summary>
    /// deleteLocalMessages 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="messages">删除的消息集合</param>
    public delegate void OnLocalMessagesDeletedDelegate(int code, List<RCIMMessage> messages);
    
    /// <summary>
    /// deleteMessages 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="type">会话类型</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="messages">删除的消息集合</param>
    public delegate void OnMessagesDeletedDelegate(int code, RCIMConversationType type, string targetId, string channelId,
                                                   List<RCIMMessage> messages);
    
    /// <summary>
    /// recallMessage 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="message">撤回的消息</param>
    public delegate void OnMessageRecalledDelegate(int code, RCIMMessage message);
    
    /// <summary>
    /// sendPrivateReadReceiptMessage 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="timestamp">时间戳</param>
    public delegate void OnPrivateReadReceiptMessageSentDelegate(int code, string targetId, string channelId,
                                                                 long timestamp);
    
    /// <summary>
    /// updateMessageExpansion 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="messageUId">消息的 messageUid</param>
    /// <param name="expansion">要更新的消息扩展信息键值对，类型是 HashMap</param>
    public delegate void OnMessageExpansionUpdatedDelegate(int code, string messageUId,
                                                           Dictionary<string, string> expansion);
    
    /// <summary>
    /// removeMessageExpansionForKeys 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="messageUId">消息的 messageUid</param>
    /// <param name="keys">
    /// 消息扩展信息中待删除的 key 的列表，类型是 ArrayList
    /// </param>
    public delegate void OnMessageExpansionForKeysRemovedDelegate(int code, string messageUId, List<string> keys);
    
    /// <summary>
    /// changeMessageReceiveStatus 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="messageId">消息的 messageId</param>
    public delegate void OnMessageReceiveStatusChangedDelegate(int code, long messageId);
    
    /// <summary>
    /// changeMessageSentStatus 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="messageId">消息的 messageId</param>
    public delegate void OnMessageSentStatusChangedDelegate(int code, long messageId);
    
    /// <summary>
    /// joinChatRoom 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="targetId">会话 ID</param>
    public delegate void OnChatRoomJoinedDelegate(int code, string targetId);
    
    /// <summary>
    /// 正在加入聊天室的回调
    /// </summary>
    /// <param name="targetId">聊天室 ID</param>
    public delegate void OnChatRoomJoiningDelegate(string targetId);
    
    /// <summary>
    /// leaveChatRoom 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="targetId">会话 ID</param>
    public delegate void OnChatRoomLeftDelegate(int code, string targetId);
    
    /// <summary>
    /// loadChatRoomMessages 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="messages">加载到的消息</param>
    /// <param name="syncTime">下次拉取的时间戳</param>
    public delegate void OnChatRoomMessagesLoadedDelegate(int code, string targetId, List<RCIMMessage> messages,
                                                          long syncTime);
    
    /// <summary>
    /// addChatRoomEntry 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="key">聊天室属性名称</param>
    public delegate void OnChatRoomEntryAddedDelegate(int code, string targetId, string key);
    
    /// <summary>
    /// addChatRoomEntries 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="entries">聊天室属性</param>
    /// <param name="errorEntries">发生错误的属性</param>
    public delegate void OnChatRoomEntriesAddedDelegate(int code, string targetId, Dictionary<string, string> entries,
                                                        Dictionary<string, int> errorEntries);
    
    /// <summary>
    /// loadChatRoomEntry 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="entry">获取到的属性</param>
    public delegate void OnChatRoomEntryLoadedDelegate(int code, string targetId, Dictionary<string, string> entry);
    
    /// <summary>
    /// loadChatRoomAllEntries 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="entries">获取到的属性集合</param>
    public delegate void OnChatRoomAllEntriesLoadedDelegate(int code, string targetId, Dictionary<string, string> entries);
    
    /// <summary>
    /// removeChatRoomEntry 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="key">聊天室属性键值</param>
    public delegate void OnChatRoomEntryRemovedDelegate(int code, string targetId, string key);
    
    /// <summary>
    /// removeChatRoomEntries 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="keys">聊天室属性键值集合</param>
    public delegate void OnChatRoomEntriesRemovedDelegate(int code, string targetId, List<string> keys);
    
    /// <summary>
    /// addToBlacklist 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="userId">用户 ID</param>
    public delegate void OnBlacklistAddedDelegate(int code, string userId);
    
    /// <summary>
    /// removeFromBlacklist 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="userId">用户 ID</param>
    public delegate void OnBlacklistRemovedDelegate(int code, string userId);
    
    /// <summary>
    /// loadBlacklistStatus 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="userId">用户 ID</param>
    /// <param name="status">当前状态</param>
    public delegate void OnBlacklistStatusLoadedDelegate(int code, string userId, RCIMBlacklistStatus status);
    
    /// <summary>
    /// loadBlacklist 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="userIds">用户 ID 集合</param>
    public delegate void OnBlacklistLoadedDelegate(int code, List<string> userIds);
    
    /// <summary>
    /// searchMessages 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="type">会话类型</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="keyword">搜索的关键字</param>
    /// <param name="startTime">查询 beginTime 之前的消息</param>
    /// <param name="count">查询的数量</param>
    /// <param name="messages">查询到的消息集合</param>
    public delegate void OnMessagesSearchedDelegate(int code, RCIMConversationType type, string targetId, string channelId,
                                                    string keyword, long startTime, int count, List<RCIMMessage> messages);
    
    /// <summary>
    /// searchMessagesByTimeRange 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="type">会话类型</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="keyword">搜索的关键字</param>
    /// <param name="startTime">开始时间</param>
    /// <param name="endTime">结束时间</param>
    /// <param name="offset">偏移量</param>
    /// <param name="count">查询的数量</param>
    /// <param name="messages">查询到的消息集合</param>
    public delegate void OnMessagesSearchedByTimeRangeDelegate(int code, RCIMConversationType type, string targetId,
                                                               string channelId, string keyword, long startTime,
                                                               long endTime, int offset, int count,
                                                               List<RCIMMessage> messages);
    
    /// <summary>
    /// searchMessagesByUserId 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="userId">用户 id</param>
    /// <param name="type">会话类型</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="startTime">查询记录的起始时间</param>
    /// <param name="count">查询的数量</param>
    /// <param name="messages">查询到的消息集合</param>
    public delegate void OnMessagesSearchedByUserIdDelegate(int code, string userId, RCIMConversationType type,
                                                            string targetId, string channelId, long startTime, int count,
                                                            List<RCIMMessage> messages);
    
    /// <summary>
    /// searchConversations 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="conversationTypes">会话类型集合</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="messageTypes">搜索的消息类型</param>
    /// <param name="keyword">搜索的关键字</param>
    /// <param name="conversations">查询到的会话集合</param>
    public delegate void OnConversationsSearchedDelegate(int code, List<RCIMConversationType> conversationTypes,
                                                         string channelId, List<RCIMMessageType> messageTypes,
                                                         string keyword, List<RCIMSearchConversationResult> conversations);
    
    /// <summary>
    /// sendGroupReadReceiptRequest 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="message">需要请求已读回执的消息</param>
    public delegate void OnGroupReadReceiptRequestSentDelegate(int code, RCIMMessage message);
    
    /// <summary>
    /// sendGroupReadReceiptResponse 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="messages">会话中需要发送已读回执的消息列表</param>
    public delegate void OnGroupReadReceiptResponseSentDelegate(int code, string targetId, string channelId,
                                                                List<RCIMMessage> messages);
    
    /// <summary>
    /// changeNotificationQuietHours 的接口回调
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="startTime">开始消息免打扰时间</param>
    /// <param name="spanMinutes">
    /// 需要消息免打扰分钟数，0 < spanMinutes < 1440（ 比如，您设置的起始时间是 00：00， 结束时间为 01:00，则 spanMinutes 为
    /// 60 分钟。设置为 1439 代表全天免打扰 （23  60 + 59 = 1439 ））
    /// </param>
    /// <param name="level">消息通知级别</param>
    public delegate void OnNotificationQuietHoursChangedDelegate(int code, string startTime, int spanMinutes,
                                                                 RCIMPushNotificationQuietHoursLevel level);
    
    /// <summary>
    /// removeNotificationQuietHours 的接口回调
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    public delegate void OnNotificationQuietHoursRemovedDelegate(int code);
    
    /// <summary>
    /// loadNotificationQuietHours 的接口回调
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="startTime">开始消息免打扰时间</param>
    /// <param name="spanMinutes">
    /// 已设置的屏蔽时间分钟数，0 < spanMinutes < 1440
    /// </param>
    /// <param name="level">消息通知级别</param>
    public delegate void OnNotificationQuietHoursLoadedDelegate(int code, string startTime, int spanMinutes,
                                                                RCIMPushNotificationQuietHoursLevel level);
    
    /// <summary>
    /// changeConversationNotificationLevel 的接口回调
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="type">会话类型</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="level">消息通知级别</param>
    public delegate void OnConversationNotificationLevelChangedDelegate(int code, RCIMConversationType type,
                                                                        string targetId, string channelId,
                                                                        RCIMPushNotificationLevel level);
    
    /// <summary>
    /// loadConversationNotificationLevel 的接口回调
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="type">会话类型</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="level">当前会话的消息通知级别</param>
    public delegate void OnConversationNotificationLevelLoadedDelegate(int code, RCIMConversationType type, string targetId,
                                                                       string channelId, RCIMPushNotificationLevel level);
    
    /// <summary>
    /// changeConversationTypeNotificationLevel 的接口回调
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="type">会话类型</param>
    /// <param name="level">消息通知级别</param>
    public delegate void OnConversationTypeNotificationLevelChangedDelegate(int code, RCIMConversationType type,
                                                                            RCIMPushNotificationLevel level);
    
    /// <summary>
    /// loadConversationTypeNotificationLevel 的接口回调
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="type">会话类型</param>
    /// <param name="level">消息通知级别</param>
    public delegate void OnConversationTypeNotificationLevelLoadedDelegate(int code, RCIMConversationType type,
                                                                           RCIMPushNotificationLevel level);
    
    /// <summary>
    /// changeUltraGroupDefaultNotificationLevel 的接口回调
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="level">消息通知级别</param>
    public delegate void OnUltraGroupDefaultNotificationLevelChangedDelegate(int code, string targetId,
                                                                             RCIMPushNotificationLevel level);
    
    /// <summary>
    /// loadUltraGroupDefaultNotificationLevel 的接口回调
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="level">消息通知级别</param>
    public delegate void OnUltraGroupDefaultNotificationLevelLoadedDelegate(int code, string targetId,
                                                                            RCIMPushNotificationLevel level);
    
    /// <summary>
    /// changeUltraGroupChannelDefaultNotificationLevel 的接口回调
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用</param>
    /// <param name="level">消息通知级别</param>
    public delegate void OnUltraGroupChannelDefaultNotificationLevelChangedDelegate(int code, string targetId,
                                                                                    string channelId,
                                                                                    RCIMPushNotificationLevel level);
    
    /// <summary>
    /// loadUltraGroupChannelDefaultNotificationLevel 的接口回调
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用</param>
    /// <param name="level">消息通知级别</param>
    public delegate void OnUltraGroupChannelDefaultNotificationLevelLoadedDelegate(int code, string targetId,
                                                                                   string channelId,
                                                                                   RCIMPushNotificationLevel level);
    
    /// <summary>
    /// changePushContentShowStatus 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="showContent">是否显示远程推送内容</param>
    public delegate void OnPushContentShowStatusChangedDelegate(int code, bool showContent);
    
    /// <summary>
    /// changePushLanguage 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="language">推送语言</param>
    public delegate void OnPushLanguageChangedDelegate(int code, string language);
    
    /// <summary>
    /// changePushReceiveStatus 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="receive">是否接收</param>
    public delegate void OnPushReceiveStatusChangedDelegate(int code, bool receive);
    
    /// <summary>
    /// loadMessageCount 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="type">会话类型</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="count">消息的数量</param>
    public delegate void OnMessageCountLoadedDelegate(int code, RCIMConversationType type, string targetId,
                                                      string channelId, int count);
    
    /// <summary>
    ///
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="conversationTypes">会话类型集合</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="conversations">加载的会话集合</param>
    public delegate void OnTopConversationsLoadedDelegate(int code, List<RCIMConversationType> conversationTypes,
                                                          string channelId, List<RCIMConversation> conversations);
    
    /// <summary>
    /// sendGroupMessageToDesignatedUsers 的接口回调。消息存入数据库的回调
    /// </summary>
    /// <param name="message">发送的消息内容</param>
    public delegate void OnGroupMessageToDesignatedUsersAttachedDelegate(RCIMMessage message);
    
    /// <summary>
    /// sendGroupMessageToDesignatedUsers 的接口回调。消息发送完成的回调
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="message">发送的消息内容</param>
    public delegate void OnGroupMessageToDesignatedUsersSentDelegate(int code, RCIMMessage message);
    
    /// <summary>
    /// syncUltraGroupReadStatus 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="timestamp">已读时间</param>
    public delegate void OnUltraGroupReadStatusSyncedDelegate(int code, string targetId, string channelId, long timestamp);
    
    /// <summary>
    /// loadConversationsForAllChannel 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="type">会话类型</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="conversations">获取到的会话集合</param>
    public delegate void OnConversationsLoadedForAllChannelDelegate(int code, RCIMConversationType type, string targetId,
                                                                    List<RCIMConversation> conversations);
    
    /// <summary>
    /// loadUltraGroupUnreadMentionedCount 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="count">未读数量</param>
    public delegate void OnUltraGroupUnreadMentionedCountLoadedDelegate(int code, string targetId, int count);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnUltraGroupUnreadCountLoadedDelegate(int code, string targetId, int count);
    
    /// <summary>
    /// modifyUltraGroupMessage 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="messageUId">消息的 messageUid</param>
    public delegate void OnUltraGroupMessageModifiedDelegate(int code, string messageUId);
    
    /// <summary>
    /// recallUltraGroupMessage 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="message">撤回的消息</param>
    /// <param name="deleteRemote">调用接口时传入的是否删除远端消息</param>
    public delegate void OnUltraGroupMessageRecalledDelegate(int code, RCIMMessage message, bool deleteRemote);
    
    /// <summary>
    /// clearUltraGroupMessages 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="timestamp">时间戳</param>
    /// <param name="policy">清除策略</param>
    public delegate void OnUltraGroupMessagesClearedDelegate(int code, string targetId, string channelId, long timestamp,
                                                             RCIMMessageOperationPolicy policy);
    
    /// <summary>
    /// clearUltraGroupMessagesForAllChannel 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="timestamp">时间戳</param>
    public delegate void OnUltraGroupMessagesClearedForAllChannelDelegate(int code, string targetId, long timestamp);
    
    /// <summary>
    /// sendUltraGroupTypingStatus 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="targetId">会话 ID</param>
    /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
    /// <param name="typingStatus">输入状态枚举</param>
    public delegate void OnUltraGroupTypingStatusSentDelegate(int code, string targetId, string channelId,
                                                              RCIMUltraGroupTypingStatus typingStatus);
    
    /// <summary>
    /// loadBatchRemoteUltraGroupMessages 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="matchedMessages">从服务获取的消息列表</param>
    /// <param name="notMatchedMessages">非法参数或者从服务没有拿到对应消息</param>
    public delegate void OnBatchRemoteUltraGroupMessagesLoadedDelegate(int code, List<RCIMMessage> matchedMessages,
                                                                       List<RCIMMessage> notMatchedMessages);
    
    /// <summary>
    /// updateUltraGroupMessageExpansion 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="expansion">更新的消息扩展信息键值对</param>
    /// <param name="messageUId">消息的 messageUid</param>
    public delegate void OnUltraGroupMessageExpansionUpdatedDelegate(int code, Dictionary<string, string> expansion,
                                                                     string messageUId);
    
    /// <summary>
    /// removeUltraGroupMessageExpansionForKeys 的接口监听
    /// </summary>
    /// <param name="code">接口回调的状态码，0 代表成功，非 0 代表出现异常</param>
    /// <param name="messageUId">消息的 messageUid</param>
    /// <param name="keys">消息扩展信息中待删除的 key 的列表</param>
    public delegate void OnUltraGroupMessageExpansionForKeysRemovedDelegate(int code, string messageUId, List<string> keys);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnCancelSendingMediaMessageCalledAction(int code, RCIMMediaMessage message);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnCancelDownloadingMediaMessageCalledAction(int code, RCIMMediaMessage message);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnConversationRemovedAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnConversationsRemovedAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnUnreadCountClearedAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnDraftMessageSavedAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnDraftMessageClearedAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnConversationTopStatusChangedAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnConversationReadStatusSyncedAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnMessageInsertedAction(int code, RCIMMessage message);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnMessagesInsertedAction(int code, List<RCIMMessage> messages);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnMessagesClearedAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnLocalMessagesDeletedAction(int code, List<RCIMMessage> messages);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnMessagesDeletedAction(int code, List<RCIMMessage> messages);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnMessageRecalledAction(int code, RCIMMessage message);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnPrivateReadReceiptMessageSentAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnGroupReadReceiptRequestSentAction(int code, RCIMMessage message);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnGroupReadReceiptResponseSentAction(int code, List<RCIMMessage> message);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnMessageExpansionUpdatedAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnMessageExpansionForKeysRemovedAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnMessageSentStatusChangedAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnMessageReceiveStatusChangedAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnChatRoomJoinedAction(int code, string targetId);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnChatRoomLeftAction(int code, string targetId);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnChatRoomEntryAddedAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnChatRoomEntriesAddedAction(int code, Dictionary<string, int> errors);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnChatRoomEntryRemovedAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnChatRoomEntriesRemovedAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnBlacklistAddedAction(int code, string userId);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnBlacklistRemovedAction(int code, string userId);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnNotificationQuietHoursChangedAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnNotificationQuietHoursRemovedAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnConversationNotificationLevelChangedAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnConversationTypeNotificationLevelChangedAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnUltraGroupDefaultNotificationLevelChangedAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnUltraGroupChannelDefaultNotificationLevelChangedAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnPushContentShowStatusChangedAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnPushLanguageChangedAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnPushReceiveStatusChangedAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnUltraGroupReadStatusSyncedAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnUltraGroupMessageModifiedAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnUltraGroupMessageRecalledAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnUltraGroupMessagesClearedAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnUltraGroupTypingStatusSentAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnUltraGroupMessagesClearedForAllChannelAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnUltraGroupMessageExpansionUpdatedAction(int code);
    
    /// <summary>
    ///
    /// </summary>
    public delegate void OnUltraGroupMessageExpansionForKeysRemovedAction(int code);
    
}