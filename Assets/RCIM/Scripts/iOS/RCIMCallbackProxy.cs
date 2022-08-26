//
//  Copyright © 2021 RongCloud. All rights reserved.
//

#if UNITY_IOS
using System;
using System.Runtime.InteropServices;

namespace cn_rongcloud_im_unity
{
    #region delegate

    /// <summary>
    /// 通用回调，适用于只返回错误码的回调
    /// </summary>
    /// <param name="handle"></param>
    /// <param name="errCode"></param>
    internal delegate void OnGeneralHandler(Int64 handle, RCErrorCode errCode);

    /// <summary>
    /// 通用回调，返回对象的指针，根据回调函数处理的业务场景对指针进行转换，得到实际返回的实例对象
    /// </summary>
    internal delegate void OnGeneralHandlerIntPtr(Int64 handle, RCErrorCode errCode,
        IntPtr imObjectPtr);

    /// <summary>
    /// 通用回调，返回指向对象数组的指针，在回调函数里根据业务场景进行指针转换，得到具体类型的对象数组。
    /// </summary>
    /// <param name="handle"></param>
    /// <param name="errCode"></param>
    /// <param name="imObjectListPtr"></param>
    /// <param name="length"></param>
    internal delegate void OnGeneralHandlerIntPtrList(Int64 handle, RCErrorCode errCode,
        IntPtr imObjectListPtr, int length);

    /// <summary>
    /// 通用回调，适用于返回 int 类型结果的回调
    /// </summary>
    /// <param name="handle"></param>
    /// <param name="errCode"></param>
    /// <param name="count"></param>
    internal delegate void OnGeneralHandlerInt(Int64 handle, RCErrorCode errCode, int count);

    /// <summary>
    /// 通用回调，适用于返回 long 类型结果的回调
    /// </summary>
    /// <param name="handle"></param>
    /// <param name="errCode"></param>
    /// <param name="result"></param>
    internal delegate void OnGeneralHandlerLong(Int64 handle, RCErrorCode errCode, long result);

    /// <summary>
    /// 通用回调，适用于返回 bool 类型结果的回调 
    /// </summary>
    /// <param name="handle"></param>
    /// <param name="errCode"></param>
    /// <param name="result"></param>
    internal delegate void OnGeneralHandlerBool(Int64 handle, RCErrorCode errCode, bool result);

    /// <summary>
    /// 通用回调，适用于返回 string 类型结果的回调
    /// </summary>
    /// <param name="handle"></param>
    /// <param name="errCode"></param>
    /// <param name="result"></param>
    internal delegate void OnGeneralHandlerString(Int64 handle, RCErrorCode errCode, string result);

    /// <summary>
    /// 连接状态变化回调
    /// </summary>
    /// <param name="status"></param>
    internal delegate void OnImConnectionStatusChangedHandler(int status);

    internal enum im_send_message_callback_type
    {
        IM_SEND_MESSAGE = 0,
        IM_SEND_MESSAGE_ATTACHED = 1
    }

    /// <summary>
    /// 发送消息的回调
    /// </summary>
    /// <param name="type">回调函数类型，1 表示 atatached</param>
    /// <param name="msgId"></param>
    /// <param name="errCode"></param>
    /// <param name="msg"></param>
    internal delegate void OnSendMessageHandler(int type, long msgId, RCErrorCode errCode, ref im_message msg);

    /// <summary>
    /// 发送多媒体消息的进度回调
    /// </summary>
    internal delegate void OnImMediaMessageSendProgressHandler(ref im_message imMsg, int progress);

    /// <summary>
    /// 取消发送多媒体消息的回调
    /// </summary>
    internal delegate void OnImMediaMessageSendCancelHandler(ref im_message imMsg);

    /// <summary>
    /// 收到消息的回调
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="left"></param>
    /// <param name="offline"></param>
    /// <param name="hasMsg"></param>
    /// <param name="cmdLeft"></param>
    internal delegate void OnImMessageReceivedHandler(ref im_message msg, int left, bool offline, bool hasMsg,
        int cmdLeft);

    /// <summary>
    /// 敏感词被拦截消息的回调
    /// </summary>
    /// <param name="imBlockedMsgInfo">拦截的敏感词信息</param>
    internal delegate void OnImBlockedMessageInfoHandler(ref im_blocked_message_info imBlockedMsgInfo);
    
    /// <summary>
    /// 撤回的消息回调，通过事件通知给客户端应用
    /// </summary>
    /// <param name="imMsg">底层通知返回的im_message结构，必须有值</param>
    /// <param name="recallNtfMsg">底层返回的im_recall_notification_message结构，必须有值</param>
    internal delegate void OnImRecallMessageHandler(ref im_message imMsg, ref im_recall_notification_message recallNtfMsg);

    /// <summary>
    /// 消息已读回执的回调 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="targetId"></param>
    /// <param name="senderId"></param>
    /// <param name="lastMsgSentTime">对方最后一条消息接受时间</param>
    internal delegate void OnImReadReceiptReceivedHandler(RCConversationType type, string targetId, string senderId, Int64 lastMsgSentTime);

    /// <summary>
    /// 请求消息已读回执（收到需要阅读时发送回执的请求，收到此请求后在会话页面已经展示该 messageUId 对应的消息或者调用
    ///  getHistoryMessages 获取消息的时候，包含此 messageUId 的消息，需要调用 SendReadReceiptResponse
    ///  接口发送消息阅读回执）
    /// </summary>
    /// <param name="type"></param>
    /// <param name="targetId"></param>
    /// <param name="messageUid">请求已读回执的消息ID</param>
    internal delegate void OnImReadReceiptRequestHandler(RCConversationType type, string targetId, string messageUid);

    /// <summary>
    /// 消息已读回执响应（收到阅读回执响应，可以按照 messageUId 更新消息的阅读数）
    /// </summary>
    /// <param name="type"></param>
    /// <param name="targetId"></param>
    /// <param name="messageUid">请求已读回执的消息ID</param>
    /// <param name="keysPtr">已读消息的 User 列表</param>
    /// <param name="valuesPtr">已读消息的 User 列表对应的状态列表</param>
    /// <param name="kvCount">列表长度</param>
    internal delegate void OnImReadReceiptResponseHandler(RCConversationType type, string targetId, string messageUid,
        IntPtr keysPtr, IntPtr valuesPtr, int kvCount);

    /// <summary>
    /// 消息扩展信息更改的回调
    /// </summary>
    /// <param name="keysPtr">消息扩展信息中更新的键列表指针</param>
    /// <param name="valuesPtr">消息扩展信息中更新的值列表指针</param>
    /// <param name="kvCount">键值对列表的长度</param>
    /// <param name="imMsg">消息</param>
    internal delegate void OnImMessageExpansionDidUpdateHandler(IntPtr keysPtr, IntPtr valuesPtr, int kvCount,
        ref im_message imMsg);

    /// <summary>
    /// 消息扩展信息删除的回调
    /// </summary>
    /// <param name="keysPtr">消息扩展信息中删除的键值对 key 列表指针</param>
    /// <param name="count">列表长度</param>
    /// <param name="imMsg">消息</param>
    internal delegate void OnImMessageExpansionDidRemoveHandler(IntPtr keysPtr, int count, ref im_message imMsg);

    /// <summary>
    /// 用户输入状态变化的回调
    /// </summary>
    /// <param name="conversationType">会话类型</param>
    /// <param name="targetId">会话目标ID</param>
    /// <param name="typeStatusListPtr">正在输入的RCUserTypingStatus列表指针（null 标示当前没有用户正在输入）</param>
    /// <param name="count">列表长度</param>
    internal delegate void OnImTypingStatusChangedHandler(RCConversationType conversationType, string targetId,
        IntPtr typeStatusListPtr, int count);

    /// <summary>
    /// 已设置的全局时间段消息提醒屏蔽回调
    /// </summary>
    /// <param name="handle"></param>
    /// <param name="errCode"></param>
    /// <param name="startTime">已设置的屏蔽开始时间</param>
    /// <param name="spansMin">已设置的屏蔽时间分钟数，0 < spansMin < 1440</param>
    internal delegate void OnResultImGetNotificationQuietHoursHandler(Int64 handle, RCErrorCode errCode,
        string startTime, int spansMin);

    /// <summary>
    /// 刚加入聊天室时 KV 同步完成的回调
    /// </summary>
    /// <param name="roomId"></param>
    internal delegate void OnImChatRoomKvDidSyncedHandler(string roomId);

    /// <summary>
    /// 聊天室 KV 变化的回调
    /// </summary>
    /// <param name="roomId"></param>
    /// <param name="keysPtr"></param>
    /// <param name="valuesPtr"></param>
    /// <param name="kvCount"></param>
    internal delegate void
        OnImChatRoomKvAddRemovedHandler(string roomId, IntPtr keysPtr, IntPtr valuesPtr, int kvCount);

    /// <summary>
    /// 有聊天室成员加入或退出的回调
    /// </summary>
    /// <param name="roomId"></param>
    /// <param name="memberActions"></param>
    /// <param name="count"></param>
    internal delegate void OnImChatRoomMemberActionChangedHandler(string roomId, IntPtr memberActions, int count);

    internal delegate void OnImChatRoomStatusChanged_Joining_Handler(string roomId);
    internal delegate void OnImChatRoomStatusChanged_Joined_Handler(string roomId);
    internal delegate void OnImChatRoomStatusChanged_Reset_Handler(string roomId);
    internal delegate void OnImChatRoomStatusChanged_Quited_Handler(string roomId);
    internal delegate void OnImChatRoomStatusChanged_Destroyed_Handler(string roomId, int destroyType);
    internal delegate void OnImChatRoomStatusChanged_Error_Handler(string roomId, int errorCode);

    /// <summary>
    /// 获取聊天室单个属性的回调
    /// </summary>
    /// <param name="handle"></param>
    /// <param name="errCode"></param>
    /// <param name="roomId"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    internal delegate void OnImChatRoomGetEntryHandler(Int64 handle, RCErrorCode errCode, string roomId,
        string key, string value);

    /// <summary>
    /// 获取聊天室所有自定义属性的回调
    /// </summary>
    /// <param name="handle"></param>
    /// <param name="errCode"></param>
    /// <param name="roomId"></param>
    /// <param name="keysPtr"></param>
    /// <param name="valuesPtr"></param>
    /// <param name="kvCount"></param>
    internal delegate void OnImChatRoomGetAllEntriesHandler(Int64 handle, RCErrorCode errCode, string roomId,
        IntPtr keysPtr, IntPtr valuesPtr, int kvCount);

    internal delegate void OnImGetChatRoomHistoryMessagesHandler(
        Int64 handle, RCErrorCode errCode, IntPtr chatRoomHistoryMessages);

    /// <summary>
    /// 通用回调，不返回任何值
    /// </summary>
    internal delegate void OnImCallbackVoid();

    internal delegate void OnImDownloadMediaProgress(ref im_message imMsg, int progress);

    internal delegate void OnImDownloadMediaCompleted(ref im_message imMsg, string mediaPath);

    internal delegate void OnImDownloadMediaFailed(ref im_message imMsg, RCErrorCode errCode);

    internal delegate void OnImDownloadMediaCanceled(ref im_message imMsg);

    #endregion

    #region callback proxy

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct on_general_callback_proxy
    {
        public Int64 handle;
        public OnGeneralHandler callback;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct on_general_int_callback_proxy
    {
        public Int64 handle;
        public OnGeneralHandlerInt callback;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct on_general_long_callback_proxy
    {
        public Int64 handle;
        public OnGeneralHandlerLong callback;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct on_general_bool_callback_proxy
    {
        public Int64 handle;
        public OnGeneralHandlerBool callback;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct on_general_string_callback_proxy
    {
        public Int64 handle;
        public OnGeneralHandlerString callback;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct on_general_intptr_callback_proxy
    {
        public Int64 handle;
        public OnGeneralHandlerIntPtr callback;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct on_general_intptr_list_callback_proxy
    {
        public Int64 handle;
        public OnGeneralHandlerIntPtrList callback;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct on_result_notification_quiet_info_callback_proxy
    {
        public Int64 handle;
        public OnResultImGetNotificationQuietHoursHandler callback;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct on_result_chat_room_entry_callback_proxy
    {
        public Int64 handle;
        public OnImChatRoomGetEntryHandler callback;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct on_result_chat_room_all_entries_callback_proxy
    {
        public Int64 handle;
        public OnImChatRoomGetAllEntriesHandler callback;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct on_result_chatroom_history_messages_callback_proxy
    {
        public Int64 handle;
        public OnImGetChatRoomHistoryMessagesHandler callback;
    }

    #endregion
}
#endif