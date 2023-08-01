#if UNITY_IOS
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public partial class RCIMIOSEngine
    {
#region IRCIMIWListener
        [MonoPInvokeCallback(typeof(OnMessageReceived))]
        private static void on_message_received(IntPtr message, int left, bool offline, bool hasPackage)
        {
            RCIMMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log(
                "OnMessageReceived", $"message_cls={message_cls},left={left},offline={offline},hasPackage={hasPackage}");
            instance?.OnMessageReceived?.Invoke(message_cls, left, offline, hasPackage);
        }
    
        [MonoPInvokeCallback(typeof(OnConnectionStatusChanged))]
        private static void on_connection_status_changed(RCIMConnectionStatus status)
        {
            RCUnityLogger.getInstance().log("OnConnectionStatusChanged", $"status={status}");
            instance?.OnConnectionStatusChanged?.Invoke(status);
        }
    
        [MonoPInvokeCallback(typeof(OnConversationTopStatusSynced))]
        private static void on_conversation_top_status_synced(RCIMConversationType type, string targetId, string channelId,
                                                              bool top)
        {
            RCUnityLogger.getInstance().log("OnConversationTopStatusSynced",
                                            $"type={type},targetId={targetId},channelId={channelId},top={top}");
            instance?.OnConversationTopStatusSynced?.Invoke(type, targetId, channelId, top);
        }
    
        [MonoPInvokeCallback(typeof(OnConversationNotificationLevelSynced))]
        private static void on_conversation_notification_level_synced(RCIMConversationType type, string targetId,
                                                                      string channelId, RCIMPushNotificationLevel level)
        {
            RCUnityLogger.getInstance().log("OnConversationNotificationLevelSynced",
                                            $"type={type},targetId={targetId},channelId={channelId},level={level}");
            instance?.OnConversationNotificationLevelSynced?.Invoke(type, targetId, channelId, level);
        }
    
        [MonoPInvokeCallback(typeof(OnRemoteMessageRecalled))]
        private static void on_remote_message_recalled(IntPtr message)
        {
            RCIMMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnRemoteMessageRecalled", $"message_cls={message_cls}");
            instance?.OnRemoteMessageRecalled?.Invoke(message_cls);
        }
    
        [MonoPInvokeCallback(typeof(OnPrivateReadReceiptReceived))]
        private static void on_private_read_receipt_received(string targetId, string channelId, long timestamp)
        {
            RCUnityLogger.getInstance().log("OnPrivateReadReceiptReceived",
                                            $"targetId={targetId},channelId={channelId},timestamp={timestamp}");
            instance?.OnPrivateReadReceiptReceived?.Invoke(targetId, channelId, timestamp);
        }
    
        [MonoPInvokeCallback(typeof(OnRemoteMessageExpansionUpdated))]
        private static void on_remote_message_expansion_updated(ref ios_c_list expansion, IntPtr message)
        {
            var expansion_dic = NativeUtils.GetObjectMapByStruct<string, string>(ref expansion);
            RCIMMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnRemoteMessageExpansionUpdated",
                                            $"expansion_dic={expansion_dic},message_cls={message_cls}");
            instance?.OnRemoteMessageExpansionUpdated?.Invoke(expansion_dic, message_cls);
        }
    
        [MonoPInvokeCallback(typeof(OnRemoteMessageExpansionForKeyRemoved))]
        private static void on_remote_message_expansion_for_key_removed(IntPtr message, ref ios_c_list keys)
        {
            RCIMMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMessageWapper(ref cmessage);
            }
            List<String> keys_list = new List<String>();
            IntPtr[] keys_clist = NativeUtils.GetObjectListByStruct<IntPtr>(ref keys);
            for (int i = 0; i < keys_clist.Length; i++)
            {
                keys_list.Add(Marshal.PtrToStringAnsi(keys_clist[i]));
            }
            RCUnityLogger.getInstance().log("OnRemoteMessageExpansionForKeyRemoved",
                                            $"message_cls={message_cls},keys_list={keys_list}");
            instance?.OnRemoteMessageExpansionForKeyRemoved?.Invoke(message_cls, keys_list);
        }
    
        [MonoPInvokeCallback(typeof(OnChatRoomMemberChanged))]
        private static void on_chat_room_member_changed(string targetId, ref ios_c_list actions)
        {
            List<RCIMChatRoomMemberAction> actions_list = new List<RCIMChatRoomMemberAction>();
            im_chat_room_member_action[] actions_clist =
                NativeUtils.GetObjectListByStruct<im_chat_room_member_action>(ref actions);
            for (int i = 0; i < actions_clist.Length; i++)
            {
                actions_list.Add(NativeConvert.fromChatRoomMemberAction(ref actions_clist[i]));
            }
            RCUnityLogger.getInstance().log("OnChatRoomMemberChanged", $"targetId={targetId},actions_list={actions_list}");
            instance?.OnChatRoomMemberChanged?.Invoke(targetId, actions_list);
        }
    
        [MonoPInvokeCallback(typeof(OnTypingStatusChanged))]
        private static void on_typing_status_changed(RCIMConversationType type, string targetId, string channelId,
                                                     ref ios_c_list userTypingStatus)
        {
            List<RCIMTypingStatus> userTypingStatus_list = new List<RCIMTypingStatus>();
            im_typing_status[] userTypingStatus_clist =
                NativeUtils.GetObjectListByStruct<im_typing_status>(ref userTypingStatus);
            for (int i = 0; i < userTypingStatus_clist.Length; i++)
            {
                userTypingStatus_list.Add(NativeConvert.fromTypingStatus(ref userTypingStatus_clist[i]));
            }
            RCUnityLogger.getInstance().log(
                "OnTypingStatusChanged",
                $"type={type},targetId={targetId},channelId={channelId},userTypingStatus_list={userTypingStatus_list}");
            instance?.OnTypingStatusChanged?.Invoke(type, targetId, channelId, userTypingStatus_list);
        }
    
        [MonoPInvokeCallback(typeof(OnConversationReadStatusSyncMessageReceived))]
        private static void on_conversation_read_status_sync_message_received(RCIMConversationType type, string targetId,
                                                                              long timestamp)
        {
            RCUnityLogger.getInstance().log("OnConversationReadStatusSyncMessageReceived",
                                            $"type={type},targetId={targetId},timestamp={timestamp}");
            instance?.OnConversationReadStatusSyncMessageReceived?.Invoke(type, targetId, timestamp);
        }
    
        [MonoPInvokeCallback(typeof(OnChatRoomEntriesSynced))]
        private static void on_chat_room_entries_synced(string roomId)
        {
            RCUnityLogger.getInstance().log("OnChatRoomEntriesSynced", $"roomId={roomId}");
            instance?.OnChatRoomEntriesSynced?.Invoke(roomId);
        }
    
        [MonoPInvokeCallback(typeof(OnChatRoomEntriesChanged))]
        private static void on_chat_room_entries_changed(RCIMChatRoomEntriesOperationType operationType, string roomId,
                                                         ref ios_c_list entries)
        {
            var entries_dic = NativeUtils.GetObjectMapByStruct<string, string>(ref entries);
            RCUnityLogger.getInstance().log("OnChatRoomEntriesChanged",
                                            $"operationType={operationType},roomId={roomId},entries_dic={entries_dic}");
            instance?.OnChatRoomEntriesChanged?.Invoke(operationType, roomId, entries_dic);
        }
    
        [MonoPInvokeCallback(typeof(OnRemoteUltraGroupMessageExpansionUpdated))]
        private static void on_remote_ultra_group_message_expansion_updated(ref ios_c_list messages)
        {
            List<RCIMMessage> messages_list = new List<RCIMMessage>();
            ios_class_warpper[] messages_clist = NativeUtils.GetObjectListByStruct<ios_class_warpper>(ref messages);
            for (int i = 0; i < messages_clist.Length; i++)
            {
                messages_list.Add(NativeConvert.fromMessageWapper(ref messages_clist[i]));
            }
            RCUnityLogger.getInstance().log("OnRemoteUltraGroupMessageExpansionUpdated", $"messages_list={messages_list}");
            instance?.OnRemoteUltraGroupMessageExpansionUpdated?.Invoke(messages_list);
        }
    
        [MonoPInvokeCallback(typeof(OnRemoteUltraGroupMessageModified))]
        private static void on_remote_ultra_group_message_modified(ref ios_c_list messages)
        {
            List<RCIMMessage> messages_list = new List<RCIMMessage>();
            ios_class_warpper[] messages_clist = NativeUtils.GetObjectListByStruct<ios_class_warpper>(ref messages);
            for (int i = 0; i < messages_clist.Length; i++)
            {
                messages_list.Add(NativeConvert.fromMessageWapper(ref messages_clist[i]));
            }
            RCUnityLogger.getInstance().log("OnRemoteUltraGroupMessageModified", $"messages_list={messages_list}");
            instance?.OnRemoteUltraGroupMessageModified?.Invoke(messages_list);
        }
    
        [MonoPInvokeCallback(typeof(OnRemoteUltraGroupMessageRecalled))]
        private static void on_remote_ultra_group_message_recalled(ref ios_c_list messages)
        {
            List<RCIMMessage> messages_list = new List<RCIMMessage>();
            ios_class_warpper[] messages_clist = NativeUtils.GetObjectListByStruct<ios_class_warpper>(ref messages);
            for (int i = 0; i < messages_clist.Length; i++)
            {
                messages_list.Add(NativeConvert.fromMessageWapper(ref messages_clist[i]));
            }
            RCUnityLogger.getInstance().log("OnRemoteUltraGroupMessageRecalled", $"messages_list={messages_list}");
            instance?.OnRemoteUltraGroupMessageRecalled?.Invoke(messages_list);
        }
    
        [MonoPInvokeCallback(typeof(OnUltraGroupReadTimeReceived))]
        private static void on_ultra_group_read_time_received(string targetId, string channelId, long timestamp)
        {
            RCUnityLogger.getInstance().log("OnUltraGroupReadTimeReceived",
                                            $"targetId={targetId},channelId={channelId},timestamp={timestamp}");
            instance?.OnUltraGroupReadTimeReceived?.Invoke(targetId, channelId, timestamp);
        }
    
        [MonoPInvokeCallback(typeof(OnUltraGroupTypingStatusChanged))]
        private static void on_ultra_group_typing_status_changed(ref ios_c_list info)
        {
            List<RCIMUltraGroupTypingStatusInfo> info_list = new List<RCIMUltraGroupTypingStatusInfo>();
            im_ultra_group_typing_status_info[] info_clist =
                NativeUtils.GetObjectListByStruct<im_ultra_group_typing_status_info>(ref info);
            for (int i = 0; i < info_clist.Length; i++)
            {
                info_list.Add(NativeConvert.fromUltraGroupTypingStatusInfo(ref info_clist[i]));
            }
            RCUnityLogger.getInstance().log("OnUltraGroupTypingStatusChanged", $"info_list={info_list}");
            instance?.OnUltraGroupTypingStatusChanged?.Invoke(info_list);
        }
    
        [MonoPInvokeCallback(typeof(OnMessageBlocked))]
        private static void on_message_blocked(IntPtr info)
        {
            RCIMBlockedMessageInfo info_cls = null;
            if (info != IntPtr.Zero)
            {
                var cinfo = NativeUtils.GetStructByPtr<im_blocked_message_info>(info);
                info_cls = NativeConvert.fromBlockedMessageInfo(ref cinfo);
            }
            RCUnityLogger.getInstance().log("OnMessageBlocked", $"info_cls={info_cls}");
            instance?.OnMessageBlocked?.Invoke(info_cls);
        }
    
        [MonoPInvokeCallback(typeof(OnChatRoomStatusChanged))]
        private static void on_chat_room_status_changed(string targetId, RCIMChatRoomStatus status)
        {
            RCUnityLogger.getInstance().log("OnChatRoomStatusChanged", $"targetId={targetId},status={status}");
            instance?.OnChatRoomStatusChanged?.Invoke(targetId, status);
        }
    
        [MonoPInvokeCallback(typeof(OnGroupMessageReadReceiptRequestReceived))]
        private static void on_group_message_read_receipt_request_received(string targetId, string messageUId)
        {
            RCUnityLogger.getInstance().log("OnGroupMessageReadReceiptRequestReceived",
                                            $"targetId={targetId},messageUId={messageUId}");
            instance?.OnGroupMessageReadReceiptRequestReceived?.Invoke(targetId, messageUId);
        }
    
        [MonoPInvokeCallback(typeof(OnGroupMessageReadReceiptResponseReceived))]
        private static void on_group_message_read_receipt_response_received(string targetId, string messageUId,
                                                                            ref ios_c_list respondUserIds)
        {
            var respondUserIds_dic = NativeUtils.GetObjectMapByStruct<string, long>(ref respondUserIds);
            RCUnityLogger.getInstance().log(
                "OnGroupMessageReadReceiptResponseReceived",
                $"targetId={targetId},messageUId={messageUId},respondUserIds_dic={respondUserIds_dic}");
            instance?.OnGroupMessageReadReceiptResponseReceived?.Invoke(targetId, messageUId, respondUserIds_dic);
        }
    
        [MonoPInvokeCallback(typeof(OnConnected))]
        private static void on_connected(int code, string userId)
        {
            RCUnityLogger.getInstance().log("OnConnected", $"code={code},userId={userId}");
            instance?.OnConnected?.Invoke(code, userId);
        }
    
        [MonoPInvokeCallback(typeof(OnDatabaseOpened))]
        private static void on_database_opened(int code)
        {
            RCUnityLogger.getInstance().log("OnDatabaseOpened", $"code={code}");
            instance?.OnDatabaseOpened?.Invoke(code);
        }
    
        [MonoPInvokeCallback(typeof(OnConversationLoaded))]
        private static void on_conversation_loaded(int code, RCIMConversationType type, string targetId, string channelId,
                                                   IntPtr conversation)
        {
            RCIMConversation conversation_cls = null;
            if (conversation != IntPtr.Zero)
            {
                var cconversation = NativeUtils.GetStructByPtr<im_conversation>(conversation);
                conversation_cls = NativeConvert.fromConversation(ref cconversation);
            }
            RCUnityLogger.getInstance().log(
                "OnConversationLoaded",
                $"code={code},type={type},targetId={targetId},channelId={channelId},conversation_cls={conversation_cls}");
            instance?.OnConversationLoaded?.Invoke(code, type, targetId, channelId, conversation_cls);
        }
    
        [MonoPInvokeCallback(typeof(OnConversationsLoaded))]
        private static void on_conversations_loaded(int code, ref ios_c_list conversationTypes, string channelId,
                                                    long startTime, int count, ref ios_c_list conversations)
        {
            List<RCIMConversationType> conversationTypes_list = new List<RCIMConversationType>();
            int[] conversationTypes_clist = NativeUtils.GetObjectListByStruct<int>(ref conversationTypes);
            for (int i = 0; i < conversationTypes_clist.Length; i++)
            {
                conversationTypes_list.Add((RCIMConversationType)(conversationTypes_clist[i]));
            }
            List<RCIMConversation> conversations_list = new List<RCIMConversation>();
            im_conversation[] conversations_clist = NativeUtils.GetObjectListByStruct<im_conversation>(ref conversations);
            for (int i = 0; i < conversations_clist.Length; i++)
            {
                conversations_list.Add(NativeConvert.fromConversation(ref conversations_clist[i]));
            }
            RCUnityLogger.getInstance().log(
                "OnConversationsLoaded",
                $"code={code},conversationTypes_list={conversationTypes_list},channelId={channelId},startTime={startTime},count={count},conversations_list={conversations_list}");
            instance?.OnConversationsLoaded?.Invoke(code, conversationTypes_list, channelId, startTime, count,
                                                    conversations_list);
        }
    
        [MonoPInvokeCallback(typeof(OnConversationRemoved))]
        private static void on_conversation_removed(int code, RCIMConversationType type, string targetId, string channelId)
        {
            RCUnityLogger.getInstance().log("OnConversationRemoved",
                                            $"code={code},type={type},targetId={targetId},channelId={channelId}");
            instance?.OnConversationRemoved?.Invoke(code, type, targetId, channelId);
        }
    
        [MonoPInvokeCallback(typeof(OnConversationsRemoved))]
        private static void on_conversations_removed(int code, ref ios_c_list conversationTypes, string channelId)
        {
            List<RCIMConversationType> conversationTypes_list = new List<RCIMConversationType>();
            int[] conversationTypes_clist = NativeUtils.GetObjectListByStruct<int>(ref conversationTypes);
            for (int i = 0; i < conversationTypes_clist.Length; i++)
            {
                conversationTypes_list.Add((RCIMConversationType)(conversationTypes_clist[i]));
            }
            RCUnityLogger.getInstance().log(
                "OnConversationsRemoved",
                $"code={code},conversationTypes_list={conversationTypes_list},channelId={channelId}");
            instance?.OnConversationsRemoved?.Invoke(code, conversationTypes_list, channelId);
        }
    
        [MonoPInvokeCallback(typeof(OnTotalUnreadCountLoaded))]
        private static void on_total_unread_count_loaded(int code, string channelId, int count)
        {
            RCUnityLogger.getInstance().log("OnTotalUnreadCountLoaded", $"code={code},channelId={channelId},count={count}");
            instance?.OnTotalUnreadCountLoaded?.Invoke(code, channelId, count);
        }
    
        [MonoPInvokeCallback(typeof(OnUnreadCountLoaded))]
        private static void on_unread_count_loaded(int code, RCIMConversationType type, string targetId, string channelId,
                                                   int count)
        {
            RCUnityLogger.getInstance().log(
                "OnUnreadCountLoaded", $"code={code},type={type},targetId={targetId},channelId={channelId},count={count}");
            instance?.OnUnreadCountLoaded?.Invoke(code, type, targetId, channelId, count);
        }
    
        [MonoPInvokeCallback(typeof(OnUnreadCountByConversationTypesLoaded))]
        private static void on_unread_count_by_conversation_types_loaded(int code, ref ios_c_list conversationTypes,
                                                                         string channelId, bool contain, int count)
        {
            List<RCIMConversationType> conversationTypes_list = new List<RCIMConversationType>();
            int[] conversationTypes_clist = NativeUtils.GetObjectListByStruct<int>(ref conversationTypes);
            for (int i = 0; i < conversationTypes_clist.Length; i++)
            {
                conversationTypes_list.Add((RCIMConversationType)(conversationTypes_clist[i]));
            }
            RCUnityLogger.getInstance().log(
                "OnUnreadCountByConversationTypesLoaded",
                $"code={code},conversationTypes_list={conversationTypes_list},channelId={channelId},contain={contain},count={count}");
            instance?.OnUnreadCountByConversationTypesLoaded?.Invoke(code, conversationTypes_list, channelId, contain,
                                                                     count);
        }
    
        [MonoPInvokeCallback(typeof(OnUnreadMentionedCountLoaded))]
        private static void on_unread_mentioned_count_loaded(int code, RCIMConversationType type, string targetId,
                                                             string channelId, int count)
        {
            RCUnityLogger.getInstance().log(
                "OnUnreadMentionedCountLoaded",
                $"code={code},type={type},targetId={targetId},channelId={channelId},count={count}");
            instance?.OnUnreadMentionedCountLoaded?.Invoke(code, type, targetId, channelId, count);
        }
    
        [MonoPInvokeCallback(typeof(OnUltraGroupAllUnreadCountLoaded))]
        private static void on_ultra_group_all_unread_count_loaded(int code, int count)
        {
            RCUnityLogger.getInstance().log("OnUltraGroupAllUnreadCountLoaded", $"code={code},count={count}");
            instance?.OnUltraGroupAllUnreadCountLoaded?.Invoke(code, count);
        }
    
        [MonoPInvokeCallback(typeof(OnUltraGroupAllUnreadMentionedCountLoaded))]
        private static void on_ultra_group_all_unread_mentioned_count_loaded(int code, int count)
        {
            RCUnityLogger.getInstance().log("OnUltraGroupAllUnreadMentionedCountLoaded", $"code={code},count={count}");
            instance?.OnUltraGroupAllUnreadMentionedCountLoaded?.Invoke(code, count);
        }
    
        [MonoPInvokeCallback(typeof(OnUltraGroupConversationsSynced))]
        private static void on_ultra_group_conversations_synced()
        {
            RCUnityLogger.getInstance().log("OnUltraGroupConversationsSynced", $"");
            instance?.OnUltraGroupConversationsSynced?.Invoke();
        }
    
        [MonoPInvokeCallback(typeof(OnUnreadCountCleared))]
        private static void on_unread_count_cleared(int code, RCIMConversationType type, string targetId, string channelId,
                                                    long timestamp)
        {
            RCUnityLogger.getInstance().log(
                "OnUnreadCountCleared",
                $"code={code},type={type},targetId={targetId},channelId={channelId},timestamp={timestamp}");
            instance?.OnUnreadCountCleared?.Invoke(code, type, targetId, channelId, timestamp);
        }
    
        [MonoPInvokeCallback(typeof(OnDraftMessageSaved))]
        private static void on_draft_message_saved(int code, RCIMConversationType type, string targetId, string channelId,
                                                   string draft)
        {
            RCUnityLogger.getInstance().log(
                "OnDraftMessageSaved", $"code={code},type={type},targetId={targetId},channelId={channelId},draft={draft}");
            instance?.OnDraftMessageSaved?.Invoke(code, type, targetId, channelId, draft);
        }
    
        [MonoPInvokeCallback(typeof(OnDraftMessageCleared))]
        private static void on_draft_message_cleared(int code, RCIMConversationType type, string targetId, string channelId)
        {
            RCUnityLogger.getInstance().log("OnDraftMessageCleared",
                                            $"code={code},type={type},targetId={targetId},channelId={channelId}");
            instance?.OnDraftMessageCleared?.Invoke(code, type, targetId, channelId);
        }
    
        [MonoPInvokeCallback(typeof(OnDraftMessageLoaded))]
        private static void on_draft_message_loaded(int code, RCIMConversationType type, string targetId, string channelId,
                                                    string draft)
        {
            RCUnityLogger.getInstance().log(
                "OnDraftMessageLoaded", $"code={code},type={type},targetId={targetId},channelId={channelId},draft={draft}");
            instance?.OnDraftMessageLoaded?.Invoke(code, type, targetId, channelId, draft);
        }
    
        [MonoPInvokeCallback(typeof(OnBlockedConversationsLoaded))]
        private static void on_blocked_conversations_loaded(int code, ref ios_c_list conversationTypes, string channelId,
                                                            ref ios_c_list conversations)
        {
            List<RCIMConversationType> conversationTypes_list = new List<RCIMConversationType>();
            int[] conversationTypes_clist = NativeUtils.GetObjectListByStruct<int>(ref conversationTypes);
            for (int i = 0; i < conversationTypes_clist.Length; i++)
            {
                conversationTypes_list.Add((RCIMConversationType)(conversationTypes_clist[i]));
            }
            List<RCIMConversation> conversations_list = new List<RCIMConversation>();
            im_conversation[] conversations_clist = NativeUtils.GetObjectListByStruct<im_conversation>(ref conversations);
            for (int i = 0; i < conversations_clist.Length; i++)
            {
                conversations_list.Add(NativeConvert.fromConversation(ref conversations_clist[i]));
            }
            RCUnityLogger.getInstance().log(
                "OnBlockedConversationsLoaded",
                $"code={code},conversationTypes_list={conversationTypes_list},channelId={channelId},conversations_list={conversations_list}");
            instance?.OnBlockedConversationsLoaded?.Invoke(code, conversationTypes_list, channelId, conversations_list);
        }
    
        [MonoPInvokeCallback(typeof(OnConversationTopStatusChanged))]
        private static void on_conversation_top_status_changed(int code, RCIMConversationType type, string targetId,
                                                               string channelId, bool top)
        {
            RCUnityLogger.getInstance().log("OnConversationTopStatusChanged",
                                            $"code={code},type={type},targetId={targetId},channelId={channelId},top={top}");
            instance?.OnConversationTopStatusChanged?.Invoke(code, type, targetId, channelId, top);
        }
    
        [MonoPInvokeCallback(typeof(OnConversationTopStatusLoaded))]
        private static void on_conversation_top_status_loaded(int code, RCIMConversationType type, string targetId,
                                                              string channelId, bool top)
        {
            RCUnityLogger.getInstance().log("OnConversationTopStatusLoaded",
                                            $"code={code},type={type},targetId={targetId},channelId={channelId},top={top}");
            instance?.OnConversationTopStatusLoaded?.Invoke(code, type, targetId, channelId, top);
        }
    
        [MonoPInvokeCallback(typeof(OnConversationReadStatusSynced))]
        private static void on_conversation_read_status_synced(int code, RCIMConversationType type, string targetId,
                                                               string channelId, long timestamp)
        {
            RCUnityLogger.getInstance().log(
                "OnConversationReadStatusSynced",
                $"code={code},type={type},targetId={targetId},channelId={channelId},timestamp={timestamp}");
            instance?.OnConversationReadStatusSynced?.Invoke(code, type, targetId, channelId, timestamp);
        }
    
        [MonoPInvokeCallback(typeof(OnMessageAttached))]
        private static void on_message_attached(IntPtr message)
        {
            RCIMMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnMessageAttached", $"message_cls={message_cls}");
            instance?.OnMessageAttached?.Invoke(message_cls);
        }
    
        [MonoPInvokeCallback(typeof(OnMessageSent))]
        private static void on_message_sent(int code, IntPtr message)
        {
            RCIMMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnMessageSent", $"code={code},message_cls={message_cls}");
            instance?.OnMessageSent?.Invoke(code, message_cls);
        }
    
        [MonoPInvokeCallback(typeof(OnMediaMessageAttached))]
        private static void on_media_message_attached(IntPtr message)
        {
            RCIMMediaMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMediaMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnMediaMessageAttached", $"message_cls={message_cls}");
            instance?.OnMediaMessageAttached?.Invoke(message_cls);
        }
    
        [MonoPInvokeCallback(typeof(OnMediaMessageSending))]
        private static void on_media_message_sending(IntPtr message, int progress)
        {
            RCIMMediaMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMediaMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnMediaMessageSending", $"message_cls={message_cls},progress={progress}");
            instance?.OnMediaMessageSending?.Invoke(message_cls, progress);
        }
    
        [MonoPInvokeCallback(typeof(OnSendingMediaMessageCanceled))]
        private static void on_sending_media_message_canceled(int code, IntPtr message)
        {
            RCIMMediaMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMediaMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnSendingMediaMessageCanceled", $"code={code},message_cls={message_cls}");
            instance?.OnSendingMediaMessageCanceled?.Invoke(code, message_cls);
        }
    
        [MonoPInvokeCallback(typeof(OnMediaMessageSent))]
        private static void on_media_message_sent(int code, IntPtr message)
        {
            RCIMMediaMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMediaMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnMediaMessageSent", $"code={code},message_cls={message_cls}");
            instance?.OnMediaMessageSent?.Invoke(code, message_cls);
        }
    
        [MonoPInvokeCallback(typeof(OnMediaMessageDownloading))]
        private static void on_media_message_downloading(IntPtr message, int progress)
        {
            RCIMMediaMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMediaMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnMediaMessageDownloading", $"message_cls={message_cls},progress={progress}");
            instance?.OnMediaMessageDownloading?.Invoke(message_cls, progress);
        }
    
        [MonoPInvokeCallback(typeof(OnMediaMessageDownloaded))]
        private static void on_media_message_downloaded(int code, IntPtr message)
        {
            RCIMMediaMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMediaMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnMediaMessageDownloaded", $"code={code},message_cls={message_cls}");
            instance?.OnMediaMessageDownloaded?.Invoke(code, message_cls);
        }
    
        [MonoPInvokeCallback(typeof(OnDownloadingMediaMessageCanceled))]
        private static void on_downloading_media_message_canceled(int code, IntPtr message)
        {
            RCIMMediaMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMediaMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnDownloadingMediaMessageCanceled", $"code={code},message_cls={message_cls}");
            instance?.OnDownloadingMediaMessageCanceled?.Invoke(code, message_cls);
        }
    
        [MonoPInvokeCallback(typeof(OnMessagesLoaded))]
        private static void on_messages_loaded(int code, RCIMConversationType type, string targetId, string channelId,
                                               long sentTime, RCIMTimeOrder order, ref ios_c_list messages)
        {
            List<RCIMMessage> messages_list = new List<RCIMMessage>();
            ios_class_warpper[] messages_clist = NativeUtils.GetObjectListByStruct<ios_class_warpper>(ref messages);
            for (int i = 0; i < messages_clist.Length; i++)
            {
                messages_list.Add(NativeConvert.fromMessageWapper(ref messages_clist[i]));
            }
            RCUnityLogger.getInstance().log(
                "OnMessagesLoaded",
                $"code={code},type={type},targetId={targetId},channelId={channelId},sentTime={sentTime},order={order},messages_list={messages_list}");
            instance?.OnMessagesLoaded?.Invoke(code, type, targetId, channelId, sentTime, order, messages_list);
        }
    
        [MonoPInvokeCallback(typeof(OnUnreadMentionedMessagesLoaded))]
        private static void on_unread_mentioned_messages_loaded(int code, RCIMConversationType type, string targetId,
                                                                string channelId, ref ios_c_list messages)
        {
            List<RCIMMessage> messages_list = new List<RCIMMessage>();
            ios_class_warpper[] messages_clist = NativeUtils.GetObjectListByStruct<ios_class_warpper>(ref messages);
            for (int i = 0; i < messages_clist.Length; i++)
            {
                messages_list.Add(NativeConvert.fromMessageWapper(ref messages_clist[i]));
            }
            RCUnityLogger.getInstance().log(
                "OnUnreadMentionedMessagesLoaded",
                $"code={code},type={type},targetId={targetId},channelId={channelId},messages_list={messages_list}");
            instance?.OnUnreadMentionedMessagesLoaded?.Invoke(code, type, targetId, channelId, messages_list);
        }
    
        [MonoPInvokeCallback(typeof(OnFirstUnreadMessageLoaded))]
        private static void on_first_unread_message_loaded(int code, RCIMConversationType type, string targetId,
                                                           string channelId, IntPtr message)
        {
            RCIMMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log(
                "OnFirstUnreadMessageLoaded",
                $"code={code},type={type},targetId={targetId},channelId={channelId},message_cls={message_cls}");
            instance?.OnFirstUnreadMessageLoaded?.Invoke(code, type, targetId, channelId, message_cls);
        }
    
        [MonoPInvokeCallback(typeof(OnMessageInserted))]
        private static void on_message_inserted(int code, IntPtr message)
        {
            RCIMMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnMessageInserted", $"code={code},message_cls={message_cls}");
            instance?.OnMessageInserted?.Invoke(code, message_cls);
        }
    
        [MonoPInvokeCallback(typeof(OnMessagesInserted))]
        private static void on_messages_inserted(int code, ref ios_c_list messages)
        {
            List<RCIMMessage> messages_list = new List<RCIMMessage>();
            ios_class_warpper[] messages_clist = NativeUtils.GetObjectListByStruct<ios_class_warpper>(ref messages);
            for (int i = 0; i < messages_clist.Length; i++)
            {
                messages_list.Add(NativeConvert.fromMessageWapper(ref messages_clist[i]));
            }
            RCUnityLogger.getInstance().log("OnMessagesInserted", $"code={code},messages_list={messages_list}");
            instance?.OnMessagesInserted?.Invoke(code, messages_list);
        }
    
        [MonoPInvokeCallback(typeof(OnMessagesCleared))]
        private static void on_messages_cleared(int code, RCIMConversationType type, string targetId, string channelId,
                                                long timestamp)
        {
            RCUnityLogger.getInstance().log(
                "OnMessagesCleared",
                $"code={code},type={type},targetId={targetId},channelId={channelId},timestamp={timestamp}");
            instance?.OnMessagesCleared?.Invoke(code, type, targetId, channelId, timestamp);
        }
    
        [MonoPInvokeCallback(typeof(OnLocalMessagesDeleted))]
        private static void on_local_messages_deleted(int code, ref ios_c_list messages)
        {
            List<RCIMMessage> messages_list = new List<RCIMMessage>();
            ios_class_warpper[] messages_clist = NativeUtils.GetObjectListByStruct<ios_class_warpper>(ref messages);
            for (int i = 0; i < messages_clist.Length; i++)
            {
                messages_list.Add(NativeConvert.fromMessageWapper(ref messages_clist[i]));
            }
            RCUnityLogger.getInstance().log("OnLocalMessagesDeleted", $"code={code},messages_list={messages_list}");
            instance?.OnLocalMessagesDeleted?.Invoke(code, messages_list);
        }
    
        [MonoPInvokeCallback(typeof(OnMessagesDeleted))]
        private static void on_messages_deleted(int code, RCIMConversationType type, string targetId, string channelId,
                                                ref ios_c_list messages)
        {
            List<RCIMMessage> messages_list = new List<RCIMMessage>();
            ios_class_warpper[] messages_clist = NativeUtils.GetObjectListByStruct<ios_class_warpper>(ref messages);
            for (int i = 0; i < messages_clist.Length; i++)
            {
                messages_list.Add(NativeConvert.fromMessageWapper(ref messages_clist[i]));
            }
            RCUnityLogger.getInstance().log(
                "OnMessagesDeleted",
                $"code={code},type={type},targetId={targetId},channelId={channelId},messages_list={messages_list}");
            instance?.OnMessagesDeleted?.Invoke(code, type, targetId, channelId, messages_list);
        }
    
        [MonoPInvokeCallback(typeof(OnMessageRecalled))]
        private static void on_message_recalled(int code, IntPtr message)
        {
            RCIMMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnMessageRecalled", $"code={code},message_cls={message_cls}");
            instance?.OnMessageRecalled?.Invoke(code, message_cls);
        }
    
        [MonoPInvokeCallback(typeof(OnPrivateReadReceiptMessageSent))]
        private static void on_private_read_receipt_message_sent(int code, string targetId, string channelId,
                                                                 long timestamp)
        {
            RCUnityLogger.getInstance().log("OnPrivateReadReceiptMessageSent",
                                            $"code={code},targetId={targetId},channelId={channelId},timestamp={timestamp}");
            instance?.OnPrivateReadReceiptMessageSent?.Invoke(code, targetId, channelId, timestamp);
        }
    
        [MonoPInvokeCallback(typeof(OnMessageExpansionUpdated))]
        private static void on_message_expansion_updated(int code, string messageUId, ref ios_c_list expansion)
        {
            var expansion_dic = NativeUtils.GetObjectMapByStruct<string, string>(ref expansion);
            RCUnityLogger.getInstance().log("OnMessageExpansionUpdated",
                                            $"code={code},messageUId={messageUId},expansion_dic={expansion_dic}");
            instance?.OnMessageExpansionUpdated?.Invoke(code, messageUId, expansion_dic);
        }
    
        [MonoPInvokeCallback(typeof(OnMessageExpansionForKeysRemoved))]
        private static void on_message_expansion_for_keys_removed(int code, string messageUId, ref ios_c_list keys)
        {
            List<String> keys_list = new List<String>();
            IntPtr[] keys_clist = NativeUtils.GetObjectListByStruct<IntPtr>(ref keys);
            for (int i = 0; i < keys_clist.Length; i++)
            {
                keys_list.Add(Marshal.PtrToStringAnsi(keys_clist[i]));
            }
            RCUnityLogger.getInstance().log("OnMessageExpansionForKeysRemoved",
                                            $"code={code},messageUId={messageUId},keys_list={keys_list}");
            instance?.OnMessageExpansionForKeysRemoved?.Invoke(code, messageUId, keys_list);
        }
    
        [MonoPInvokeCallback(typeof(OnMessageReceiveStatusChanged))]
        private static void on_message_receive_status_changed(int code, long messageId)
        {
            RCUnityLogger.getInstance().log("OnMessageReceiveStatusChanged", $"code={code},messageId={messageId}");
            instance?.OnMessageReceiveStatusChanged?.Invoke(code, messageId);
        }
    
        [MonoPInvokeCallback(typeof(OnMessageSentStatusChanged))]
        private static void on_message_sent_status_changed(int code, long messageId)
        {
            RCUnityLogger.getInstance().log("OnMessageSentStatusChanged", $"code={code},messageId={messageId}");
            instance?.OnMessageSentStatusChanged?.Invoke(code, messageId);
        }
    
        [MonoPInvokeCallback(typeof(OnChatRoomJoined))]
        private static void on_chat_room_joined(int code, string targetId)
        {
            RCUnityLogger.getInstance().log("OnChatRoomJoined", $"code={code},targetId={targetId}");
            instance?.OnChatRoomJoined?.Invoke(code, targetId);
        }
    
        [MonoPInvokeCallback(typeof(OnChatRoomJoining))]
        private static void on_chat_room_joining(string targetId)
        {
            RCUnityLogger.getInstance().log("OnChatRoomJoining", $"targetId={targetId}");
            instance?.OnChatRoomJoining?.Invoke(targetId);
        }
    
        [MonoPInvokeCallback(typeof(OnChatRoomLeft))]
        private static void on_chat_room_left(int code, string targetId)
        {
            RCUnityLogger.getInstance().log("OnChatRoomLeft", $"code={code},targetId={targetId}");
            instance?.OnChatRoomLeft?.Invoke(code, targetId);
        }
    
        [MonoPInvokeCallback(typeof(OnChatRoomMessagesLoaded))]
        private static void on_chat_room_messages_loaded(int code, string targetId, ref ios_c_list messages, long syncTime)
        {
            List<RCIMMessage> messages_list = new List<RCIMMessage>();
            ios_class_warpper[] messages_clist = NativeUtils.GetObjectListByStruct<ios_class_warpper>(ref messages);
            for (int i = 0; i < messages_clist.Length; i++)
            {
                messages_list.Add(NativeConvert.fromMessageWapper(ref messages_clist[i]));
            }
            RCUnityLogger.getInstance().log(
                "OnChatRoomMessagesLoaded",
                $"code={code},targetId={targetId},messages_list={messages_list},syncTime={syncTime}");
            instance?.OnChatRoomMessagesLoaded?.Invoke(code, targetId, messages_list, syncTime);
        }
    
        [MonoPInvokeCallback(typeof(OnChatRoomEntryAdded))]
        private static void on_chat_room_entry_added(int code, string targetId, string key)
        {
            RCUnityLogger.getInstance().log("OnChatRoomEntryAdded", $"code={code},targetId={targetId},key={key}");
            instance?.OnChatRoomEntryAdded?.Invoke(code, targetId, key);
        }
    
        [MonoPInvokeCallback(typeof(OnChatRoomEntriesAdded))]
        private static void on_chat_room_entries_added(int code, string targetId, ref ios_c_list entries,
                                                       ref ios_c_list errorEntries)
        {
            var entries_dic = NativeUtils.GetObjectMapByStruct<string, string>(ref entries);
            var errorEntries_dic = NativeUtils.GetObjectMapByStruct<string, int>(ref errorEntries);
            RCUnityLogger.getInstance().log(
                "OnChatRoomEntriesAdded",
                $"code={code},targetId={targetId},entries_dic={entries_dic},errorEntries_dic={errorEntries_dic}");
            instance?.OnChatRoomEntriesAdded?.Invoke(code, targetId, entries_dic, errorEntries_dic);
        }
    
        [MonoPInvokeCallback(typeof(OnChatRoomEntryLoaded))]
        private static void on_chat_room_entry_loaded(int code, string targetId, ref ios_c_list entry)
        {
            var entry_dic = NativeUtils.GetObjectMapByStruct<string, string>(ref entry);
            RCUnityLogger.getInstance().log("OnChatRoomEntryLoaded",
                                            $"code={code},targetId={targetId},entry_dic={entry_dic}");
            instance?.OnChatRoomEntryLoaded?.Invoke(code, targetId, entry_dic);
        }
    
        [MonoPInvokeCallback(typeof(OnChatRoomAllEntriesLoaded))]
        private static void on_chat_room_all_entries_loaded(int code, string targetId, ref ios_c_list entries)
        {
            var entries_dic = NativeUtils.GetObjectMapByStruct<string, string>(ref entries);
            RCUnityLogger.getInstance().log("OnChatRoomAllEntriesLoaded",
                                            $"code={code},targetId={targetId},entries_dic={entries_dic}");
            instance?.OnChatRoomAllEntriesLoaded?.Invoke(code, targetId, entries_dic);
        }
    
        [MonoPInvokeCallback(typeof(OnChatRoomEntryRemoved))]
        private static void on_chat_room_entry_removed(int code, string targetId, string key)
        {
            RCUnityLogger.getInstance().log("OnChatRoomEntryRemoved", $"code={code},targetId={targetId},key={key}");
            instance?.OnChatRoomEntryRemoved?.Invoke(code, targetId, key);
        }
    
        [MonoPInvokeCallback(typeof(OnChatRoomEntriesRemoved))]
        private static void on_chat_room_entries_removed(int code, string targetId, ref ios_c_list keys)
        {
            List<String> keys_list = new List<String>();
            IntPtr[] keys_clist = NativeUtils.GetObjectListByStruct<IntPtr>(ref keys);
            for (int i = 0; i < keys_clist.Length; i++)
            {
                keys_list.Add(Marshal.PtrToStringAnsi(keys_clist[i]));
            }
            RCUnityLogger.getInstance().log("OnChatRoomEntriesRemoved",
                                            $"code={code},targetId={targetId},keys_list={keys_list}");
            instance?.OnChatRoomEntriesRemoved?.Invoke(code, targetId, keys_list);
        }
    
        [MonoPInvokeCallback(typeof(OnBlacklistAdded))]
        private static void on_blacklist_added(int code, string userId)
        {
            RCUnityLogger.getInstance().log("OnBlacklistAdded", $"code={code},userId={userId}");
            instance?.OnBlacklistAdded?.Invoke(code, userId);
        }
    
        [MonoPInvokeCallback(typeof(OnBlacklistRemoved))]
        private static void on_blacklist_removed(int code, string userId)
        {
            RCUnityLogger.getInstance().log("OnBlacklistRemoved", $"code={code},userId={userId}");
            instance?.OnBlacklistRemoved?.Invoke(code, userId);
        }
    
        [MonoPInvokeCallback(typeof(OnBlacklistStatusLoaded))]
        private static void on_blacklist_status_loaded(int code, string userId, RCIMBlacklistStatus status)
        {
            RCUnityLogger.getInstance().log("OnBlacklistStatusLoaded", $"code={code},userId={userId},status={status}");
            instance?.OnBlacklistStatusLoaded?.Invoke(code, userId, status);
        }
    
        [MonoPInvokeCallback(typeof(OnBlacklistLoaded))]
        private static void on_blacklist_loaded(int code, ref ios_c_list userIds)
        {
            List<String> userIds_list = new List<String>();
            IntPtr[] userIds_clist = NativeUtils.GetObjectListByStruct<IntPtr>(ref userIds);
            for (int i = 0; i < userIds_clist.Length; i++)
            {
                userIds_list.Add(Marshal.PtrToStringAnsi(userIds_clist[i]));
            }
            RCUnityLogger.getInstance().log("OnBlacklistLoaded", $"code={code},userIds_list={userIds_list}");
            instance?.OnBlacklistLoaded?.Invoke(code, userIds_list);
        }
    
        [MonoPInvokeCallback(typeof(OnMessagesSearched))]
        private static void on_messages_searched(int code, RCIMConversationType type, string targetId, string channelId,
                                                 string keyword, long startTime, int count, ref ios_c_list messages)
        {
            List<RCIMMessage> messages_list = new List<RCIMMessage>();
            ios_class_warpper[] messages_clist = NativeUtils.GetObjectListByStruct<ios_class_warpper>(ref messages);
            for (int i = 0; i < messages_clist.Length; i++)
            {
                messages_list.Add(NativeConvert.fromMessageWapper(ref messages_clist[i]));
            }
            RCUnityLogger.getInstance().log(
                "OnMessagesSearched",
                $"code={code},type={type},targetId={targetId},channelId={channelId},keyword={keyword},startTime={startTime},count={count},messages_list={messages_list}");
            instance?.OnMessagesSearched?.Invoke(code, type, targetId, channelId, keyword, startTime, count, messages_list);
        }
    
        [MonoPInvokeCallback(typeof(OnMessagesSearchedByTimeRange))]
        private static void on_messages_searched_by_time_range(int code, RCIMConversationType type, string targetId,
                                                               string channelId, string keyword, long startTime,
                                                               long endTime, int offset, int count, ref ios_c_list messages)
        {
            List<RCIMMessage> messages_list = new List<RCIMMessage>();
            ios_class_warpper[] messages_clist = NativeUtils.GetObjectListByStruct<ios_class_warpper>(ref messages);
            for (int i = 0; i < messages_clist.Length; i++)
            {
                messages_list.Add(NativeConvert.fromMessageWapper(ref messages_clist[i]));
            }
            RCUnityLogger.getInstance().log(
                "OnMessagesSearchedByTimeRange",
                $"code={code},type={type},targetId={targetId},channelId={channelId},keyword={keyword},startTime={startTime},endTime={endTime},offset={offset},count={count},messages_list={messages_list}");
            instance?.OnMessagesSearchedByTimeRange?.Invoke(code, type, targetId, channelId, keyword, startTime, endTime,
                                                            offset, count, messages_list);
        }
    
        [MonoPInvokeCallback(typeof(OnMessagesSearchedByUserId))]
        private static void on_messages_searched_by_user_id(int code, string userId, RCIMConversationType type,
                                                            string targetId, string channelId, long startTime, int count,
                                                            ref ios_c_list messages)
        {
            List<RCIMMessage> messages_list = new List<RCIMMessage>();
            ios_class_warpper[] messages_clist = NativeUtils.GetObjectListByStruct<ios_class_warpper>(ref messages);
            for (int i = 0; i < messages_clist.Length; i++)
            {
                messages_list.Add(NativeConvert.fromMessageWapper(ref messages_clist[i]));
            }
            RCUnityLogger.getInstance().log(
                "OnMessagesSearchedByUserId",
                $"code={code},userId={userId},type={type},targetId={targetId},channelId={channelId},startTime={startTime},count={count},messages_list={messages_list}");
            instance?.OnMessagesSearchedByUserId?.Invoke(code, userId, type, targetId, channelId, startTime, count,
                                                         messages_list);
        }
    
        [MonoPInvokeCallback(typeof(OnConversationsSearched))]
        private static void on_conversations_searched(int code, ref ios_c_list conversationTypes, string channelId,
                                                      ref ios_c_list messageTypes, string keyword,
                                                      ref ios_c_list conversations)
        {
            List<RCIMConversationType> conversationTypes_list = new List<RCIMConversationType>();
            int[] conversationTypes_clist = NativeUtils.GetObjectListByStruct<int>(ref conversationTypes);
            for (int i = 0; i < conversationTypes_clist.Length; i++)
            {
                conversationTypes_list.Add((RCIMConversationType)(conversationTypes_clist[i]));
            }
            List<RCIMMessageType> messageTypes_list = new List<RCIMMessageType>();
            int[] messageTypes_clist = NativeUtils.GetObjectListByStruct<int>(ref messageTypes);
            for (int i = 0; i < messageTypes_clist.Length; i++)
            {
                messageTypes_list.Add((RCIMMessageType)(messageTypes_clist[i]));
            }
            List<RCIMSearchConversationResult> conversations_list = new List<RCIMSearchConversationResult>();
            im_search_conversation_result[] conversations_clist =
                NativeUtils.GetObjectListByStruct<im_search_conversation_result>(ref conversations);
            for (int i = 0; i < conversations_clist.Length; i++)
            {
                conversations_list.Add(NativeConvert.fromSearchConversationResult(ref conversations_clist[i]));
            }
            RCUnityLogger.getInstance().log(
                "OnConversationsSearched",
                $"code={code},conversationTypes_list={conversationTypes_list},channelId={channelId},messageTypes_list={messageTypes_list},keyword={keyword},conversations_list={conversations_list}");
            instance?.OnConversationsSearched?.Invoke(code, conversationTypes_list, channelId, messageTypes_list, keyword,
                                                      conversations_list);
        }
    
        [MonoPInvokeCallback(typeof(OnGroupReadReceiptRequestSent))]
        private static void on_group_read_receipt_request_sent(int code, IntPtr message)
        {
            RCIMMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnGroupReadReceiptRequestSent", $"code={code},message_cls={message_cls}");
            instance?.OnGroupReadReceiptRequestSent?.Invoke(code, message_cls);
        }
    
        [MonoPInvokeCallback(typeof(OnGroupReadReceiptResponseSent))]
        private static void on_group_read_receipt_response_sent(int code, string targetId, string channelId,
                                                                ref ios_c_list messages)
        {
            List<RCIMMessage> messages_list = new List<RCIMMessage>();
            ios_class_warpper[] messages_clist = NativeUtils.GetObjectListByStruct<ios_class_warpper>(ref messages);
            for (int i = 0; i < messages_clist.Length; i++)
            {
                messages_list.Add(NativeConvert.fromMessageWapper(ref messages_clist[i]));
            }
            RCUnityLogger.getInstance().log(
                "OnGroupReadReceiptResponseSent",
                $"code={code},targetId={targetId},channelId={channelId},messages_list={messages_list}");
            instance?.OnGroupReadReceiptResponseSent?.Invoke(code, targetId, channelId, messages_list);
        }
    
        [MonoPInvokeCallback(typeof(OnNotificationQuietHoursChanged))]
        private static void on_notification_quiet_hours_changed(int code, string startTime, int spanMinutes,
                                                                RCIMPushNotificationQuietHoursLevel level)
        {
            RCUnityLogger.getInstance().log("OnNotificationQuietHoursChanged",
                                            $"code={code},startTime={startTime},spanMinutes={spanMinutes},level={level}");
            instance?.OnNotificationQuietHoursChanged?.Invoke(code, startTime, spanMinutes, level);
        }
    
        [MonoPInvokeCallback(typeof(OnNotificationQuietHoursRemoved))]
        private static void on_notification_quiet_hours_removed(int code)
        {
            RCUnityLogger.getInstance().log("OnNotificationQuietHoursRemoved", $"code={code}");
            instance?.OnNotificationQuietHoursRemoved?.Invoke(code);
        }
    
        [MonoPInvokeCallback(typeof(OnNotificationQuietHoursLoaded))]
        private static void on_notification_quiet_hours_loaded(int code, string startTime, int spanMinutes,
                                                               RCIMPushNotificationQuietHoursLevel level)
        {
            RCUnityLogger.getInstance().log("OnNotificationQuietHoursLoaded",
                                            $"code={code},startTime={startTime},spanMinutes={spanMinutes},level={level}");
            instance?.OnNotificationQuietHoursLoaded?.Invoke(code, startTime, spanMinutes, level);
        }
    
        [MonoPInvokeCallback(typeof(OnConversationNotificationLevelChanged))]
        private static void on_conversation_notification_level_changed(int code, RCIMConversationType type, string targetId,
                                                                       string channelId, RCIMPushNotificationLevel level)
        {
            RCUnityLogger.getInstance().log(
                "OnConversationNotificationLevelChanged",
                $"code={code},type={type},targetId={targetId},channelId={channelId},level={level}");
            instance?.OnConversationNotificationLevelChanged?.Invoke(code, type, targetId, channelId, level);
        }
    
        [MonoPInvokeCallback(typeof(OnConversationNotificationLevelLoaded))]
        private static void on_conversation_notification_level_loaded(int code, RCIMConversationType type, string targetId,
                                                                      string channelId, RCIMPushNotificationLevel level)
        {
            RCUnityLogger.getInstance().log(
                "OnConversationNotificationLevelLoaded",
                $"code={code},type={type},targetId={targetId},channelId={channelId},level={level}");
            instance?.OnConversationNotificationLevelLoaded?.Invoke(code, type, targetId, channelId, level);
        }
    
        [MonoPInvokeCallback(typeof(OnConversationTypeNotificationLevelChanged))]
        private static void on_conversation_type_notification_level_changed(int code, RCIMConversationType type,
                                                                            RCIMPushNotificationLevel level)
        {
            RCUnityLogger.getInstance().log("OnConversationTypeNotificationLevelChanged",
                                            $"code={code},type={type},level={level}");
            instance?.OnConversationTypeNotificationLevelChanged?.Invoke(code, type, level);
        }
    
        [MonoPInvokeCallback(typeof(OnConversationTypeNotificationLevelLoaded))]
        private static void on_conversation_type_notification_level_loaded(int code, RCIMConversationType type,
                                                                           RCIMPushNotificationLevel level)
        {
            RCUnityLogger.getInstance().log("OnConversationTypeNotificationLevelLoaded",
                                            $"code={code},type={type},level={level}");
            instance?.OnConversationTypeNotificationLevelLoaded?.Invoke(code, type, level);
        }
    
        [MonoPInvokeCallback(typeof(OnUltraGroupDefaultNotificationLevelChanged))]
        private static void on_ultra_group_default_notification_level_changed(int code, string targetId,
                                                                              RCIMPushNotificationLevel level)
        {
            RCUnityLogger.getInstance().log("OnUltraGroupDefaultNotificationLevelChanged",
                                            $"code={code},targetId={targetId},level={level}");
            instance?.OnUltraGroupDefaultNotificationLevelChanged?.Invoke(code, targetId, level);
        }
    
        [MonoPInvokeCallback(typeof(OnUltraGroupDefaultNotificationLevelLoaded))]
        private static void on_ultra_group_default_notification_level_loaded(int code, string targetId,
                                                                             RCIMPushNotificationLevel level)
        {
            RCUnityLogger.getInstance().log("OnUltraGroupDefaultNotificationLevelLoaded",
                                            $"code={code},targetId={targetId},level={level}");
            instance?.OnUltraGroupDefaultNotificationLevelLoaded?.Invoke(code, targetId, level);
        }
    
        [MonoPInvokeCallback(typeof(OnUltraGroupChannelDefaultNotificationLevelChanged))]
        private static void on_ultra_group_channel_default_notification_level_changed(int code, string targetId,
                                                                                      string channelId,
                                                                                      RCIMPushNotificationLevel level)
        {
            RCUnityLogger.getInstance().log("OnUltraGroupChannelDefaultNotificationLevelChanged",
                                            $"code={code},targetId={targetId},channelId={channelId},level={level}");
            instance?.OnUltraGroupChannelDefaultNotificationLevelChanged?.Invoke(code, targetId, channelId, level);
        }
    
        [MonoPInvokeCallback(typeof(OnUltraGroupChannelDefaultNotificationLevelLoaded))]
        private static void on_ultra_group_channel_default_notification_level_loaded(int code, string targetId,
                                                                                     string channelId,
                                                                                     RCIMPushNotificationLevel level)
        {
            RCUnityLogger.getInstance().log("OnUltraGroupChannelDefaultNotificationLevelLoaded",
                                            $"code={code},targetId={targetId},channelId={channelId},level={level}");
            instance?.OnUltraGroupChannelDefaultNotificationLevelLoaded?.Invoke(code, targetId, channelId, level);
        }
    
        [MonoPInvokeCallback(typeof(OnPushContentShowStatusChanged))]
        private static void on_push_content_show_status_changed(int code, bool showContent)
        {
            RCUnityLogger.getInstance().log("OnPushContentShowStatusChanged", $"code={code},showContent={showContent}");
            instance?.OnPushContentShowStatusChanged?.Invoke(code, showContent);
        }
    
        [MonoPInvokeCallback(typeof(OnPushLanguageChanged))]
        private static void on_push_language_changed(int code, string language)
        {
            RCUnityLogger.getInstance().log("OnPushLanguageChanged", $"code={code},language={language}");
            instance?.OnPushLanguageChanged?.Invoke(code, language);
        }
    
        [MonoPInvokeCallback(typeof(OnPushReceiveStatusChanged))]
        private static void on_push_receive_status_changed(int code, bool receive)
        {
            RCUnityLogger.getInstance().log("OnPushReceiveStatusChanged", $"code={code},receive={receive}");
            instance?.OnPushReceiveStatusChanged?.Invoke(code, receive);
        }
    
        [MonoPInvokeCallback(typeof(OnMessageCountLoaded))]
        private static void on_message_count_loaded(int code, RCIMConversationType type, string targetId, string channelId,
                                                    int count)
        {
            RCUnityLogger.getInstance().log(
                "OnMessageCountLoaded", $"code={code},type={type},targetId={targetId},channelId={channelId},count={count}");
            instance?.OnMessageCountLoaded?.Invoke(code, type, targetId, channelId, count);
        }
    
        [MonoPInvokeCallback(typeof(OnTopConversationsLoaded))]
        private static void on_top_conversations_loaded(int code, ref ios_c_list conversationTypes, string channelId,
                                                        ref ios_c_list conversations)
        {
            List<RCIMConversationType> conversationTypes_list = new List<RCIMConversationType>();
            int[] conversationTypes_clist = NativeUtils.GetObjectListByStruct<int>(ref conversationTypes);
            for (int i = 0; i < conversationTypes_clist.Length; i++)
            {
                conversationTypes_list.Add((RCIMConversationType)(conversationTypes_clist[i]));
            }
            List<RCIMConversation> conversations_list = new List<RCIMConversation>();
            im_conversation[] conversations_clist = NativeUtils.GetObjectListByStruct<im_conversation>(ref conversations);
            for (int i = 0; i < conversations_clist.Length; i++)
            {
                conversations_list.Add(NativeConvert.fromConversation(ref conversations_clist[i]));
            }
            RCUnityLogger.getInstance().log(
                "OnTopConversationsLoaded",
                $"code={code},conversationTypes_list={conversationTypes_list},channelId={channelId},conversations_list={conversations_list}");
            instance?.OnTopConversationsLoaded?.Invoke(code, conversationTypes_list, channelId, conversations_list);
        }
    
        [MonoPInvokeCallback(typeof(OnGroupMessageToDesignatedUsersAttached))]
        private static void on_group_message_to_designated_users_attached(IntPtr message)
        {
            RCIMMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnGroupMessageToDesignatedUsersAttached", $"message_cls={message_cls}");
            instance?.OnGroupMessageToDesignatedUsersAttached?.Invoke(message_cls);
        }
    
        [MonoPInvokeCallback(typeof(OnGroupMessageToDesignatedUsersSent))]
        private static void on_group_message_to_designated_users_sent(int code, IntPtr message)
        {
            RCIMMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnGroupMessageToDesignatedUsersSent",
                                            $"code={code},message_cls={message_cls}");
            instance?.OnGroupMessageToDesignatedUsersSent?.Invoke(code, message_cls);
        }
    
        [MonoPInvokeCallback(typeof(OnUltraGroupReadStatusSynced))]
        private static void on_ultra_group_read_status_synced(int code, string targetId, string channelId, long timestamp)
        {
            RCUnityLogger.getInstance().log("OnUltraGroupReadStatusSynced",
                                            $"code={code},targetId={targetId},channelId={channelId},timestamp={timestamp}");
            instance?.OnUltraGroupReadStatusSynced?.Invoke(code, targetId, channelId, timestamp);
        }
    
        [MonoPInvokeCallback(typeof(OnConversationsLoadedForAllChannel))]
        private static void on_conversations_loaded_for_all_channel(int code, RCIMConversationType type, string targetId,
                                                                    ref ios_c_list conversations)
        {
            List<RCIMConversation> conversations_list = new List<RCIMConversation>();
            im_conversation[] conversations_clist = NativeUtils.GetObjectListByStruct<im_conversation>(ref conversations);
            for (int i = 0; i < conversations_clist.Length; i++)
            {
                conversations_list.Add(NativeConvert.fromConversation(ref conversations_clist[i]));
            }
            RCUnityLogger.getInstance().log(
                "OnConversationsLoadedForAllChannel",
                $"code={code},type={type},targetId={targetId},conversations_list={conversations_list}");
            instance?.OnConversationsLoadedForAllChannel?.Invoke(code, type, targetId, conversations_list);
        }
    
        [MonoPInvokeCallback(typeof(OnUltraGroupUnreadMentionedCountLoaded))]
        private static void on_ultra_group_unread_mentioned_count_loaded(int code, string targetId, int count)
        {
            RCUnityLogger.getInstance().log("OnUltraGroupUnreadMentionedCountLoaded",
                                            $"code={code},targetId={targetId},count={count}");
            instance?.OnUltraGroupUnreadMentionedCountLoaded?.Invoke(code, targetId, count);
        }
    
        [MonoPInvokeCallback(typeof(OnUltraGroupUnreadCountLoaded))]
        private static void on_ultra_group_unread_count_loaded(int code, string targetId, int count)
        {
            RCUnityLogger.getInstance().log("OnUltraGroupUnreadCountLoaded",
                                            $"code={code},targetId={targetId},count={count}");
            instance?.OnUltraGroupUnreadCountLoaded?.Invoke(code, targetId, count);
        }
    
        [MonoPInvokeCallback(typeof(OnUltraGroupMessageModified))]
        private static void on_ultra_group_message_modified(int code, string messageUId)
        {
            RCUnityLogger.getInstance().log("OnUltraGroupMessageModified", $"code={code},messageUId={messageUId}");
            instance?.OnUltraGroupMessageModified?.Invoke(code, messageUId);
        }
    
        [MonoPInvokeCallback(typeof(OnUltraGroupMessageRecalled))]
        private static void on_ultra_group_message_recalled(int code, IntPtr message, bool deleteRemote)
        {
            RCIMMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnUltraGroupMessageRecalled",
                                            $"code={code},message_cls={message_cls},deleteRemote={deleteRemote}");
            instance?.OnUltraGroupMessageRecalled?.Invoke(code, message_cls, deleteRemote);
        }
    
        [MonoPInvokeCallback(typeof(OnUltraGroupMessagesCleared))]
        private static void on_ultra_group_messages_cleared(int code, string targetId, string channelId, long timestamp,
                                                            RCIMMessageOperationPolicy policy)
        {
            RCUnityLogger.getInstance().log(
                "OnUltraGroupMessagesCleared",
                $"code={code},targetId={targetId},channelId={channelId},timestamp={timestamp},policy={policy}");
            instance?.OnUltraGroupMessagesCleared?.Invoke(code, targetId, channelId, timestamp, policy);
        }
    
        [MonoPInvokeCallback(typeof(OnUltraGroupMessagesClearedForAllChannel))]
        private static void on_ultra_group_messages_cleared_for_all_channel(int code, string targetId, long timestamp)
        {
            RCUnityLogger.getInstance().log("OnUltraGroupMessagesClearedForAllChannel",
                                            $"code={code},targetId={targetId},timestamp={timestamp}");
            instance?.OnUltraGroupMessagesClearedForAllChannel?.Invoke(code, targetId, timestamp);
        }
    
        [MonoPInvokeCallback(typeof(OnUltraGroupTypingStatusSent))]
        private static void on_ultra_group_typing_status_sent(int code, string targetId, string channelId,
                                                              RCIMUltraGroupTypingStatus typingStatus)
        {
            RCUnityLogger.getInstance().log(
                "OnUltraGroupTypingStatusSent",
                $"code={code},targetId={targetId},channelId={channelId},typingStatus={typingStatus}");
            instance?.OnUltraGroupTypingStatusSent?.Invoke(code, targetId, channelId, typingStatus);
        }
    
        [MonoPInvokeCallback(typeof(OnBatchRemoteUltraGroupMessagesLoaded))]
        private static void on_batch_remote_ultra_group_messages_loaded(int code, ref ios_c_list matchedMessages,
                                                                        ref ios_c_list notMatchedMessages)
        {
            List<RCIMMessage> matchedMessages_list = new List<RCIMMessage>();
            ios_class_warpper[] matchedMessages_clist =
                NativeUtils.GetObjectListByStruct<ios_class_warpper>(ref matchedMessages);
            for (int i = 0; i < matchedMessages_clist.Length; i++)
            {
                matchedMessages_list.Add(NativeConvert.fromMessageWapper(ref matchedMessages_clist[i]));
            }
            List<RCIMMessage> notMatchedMessages_list = new List<RCIMMessage>();
            ios_class_warpper[] notMatchedMessages_clist =
                NativeUtils.GetObjectListByStruct<ios_class_warpper>(ref notMatchedMessages);
            for (int i = 0; i < notMatchedMessages_clist.Length; i++)
            {
                notMatchedMessages_list.Add(NativeConvert.fromMessageWapper(ref notMatchedMessages_clist[i]));
            }
            RCUnityLogger.getInstance().log(
                "OnBatchRemoteUltraGroupMessagesLoaded",
                $"code={code},matchedMessages_list={matchedMessages_list},notMatchedMessages_list={notMatchedMessages_list}");
            instance?.OnBatchRemoteUltraGroupMessagesLoaded?.Invoke(code, matchedMessages_list, notMatchedMessages_list);
        }
    
        [MonoPInvokeCallback(typeof(OnUltraGroupMessageExpansionUpdated))]
        private static void on_ultra_group_message_expansion_updated(int code, ref ios_c_list expansion, string messageUId)
        {
            var expansion_dic = NativeUtils.GetObjectMapByStruct<string, string>(ref expansion);
            RCUnityLogger.getInstance().log("OnUltraGroupMessageExpansionUpdated",
                                            $"code={code},expansion_dic={expansion_dic},messageUId={messageUId}");
            instance?.OnUltraGroupMessageExpansionUpdated?.Invoke(code, expansion_dic, messageUId);
        }
    
        [MonoPInvokeCallback(typeof(OnUltraGroupMessageExpansionForKeysRemoved))]
        private static void on_ultra_group_message_expansion_for_keys_removed(int code, string messageUId,
                                                                              ref ios_c_list keys)
        {
            List<String> keys_list = new List<String>();
            IntPtr[] keys_clist = NativeUtils.GetObjectListByStruct<IntPtr>(ref keys);
            for (int i = 0; i < keys_clist.Length; i++)
            {
                keys_list.Add(Marshal.PtrToStringAnsi(keys_clist[i]));
            }
            RCUnityLogger.getInstance().log("OnUltraGroupMessageExpansionForKeysRemoved",
                                            $"code={code},messageUId={messageUId},keys_list={keys_list}");
            instance?.OnUltraGroupMessageExpansionForKeysRemoved?.Invoke(code, messageUId, keys_list);
        }
    
#endregion
#region RCIMIWCancelSendingMediaMessageCallback
        [MonoPInvokeCallback(typeof(IOSCancelSendingMediaMessageProxyOnCancelSendingMediaMessageCalled))]
        private static void ios_cancel_sending_media_message_proxy_on_cancel_sending_media_message_called(int code,
                                                                                                          IntPtr message,
                                                                                                          Int64 handle)
        {
            RCIMMediaMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMediaMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnCancelSendingMediaMessageCalled", $"code={code},message_cls={message_cls}");
            var cb = NativeUtils.TakeCallback<OnCancelSendingMediaMessageCalledAction>(handle);
            cb?.Invoke(code, message_cls);
        }
        internal static ios_cancel_sending_media_message_proxy toIOSCancelSendingMediaMessageProxy(
            OnCancelSendingMediaMessageCalledAction listener)
        {
            ios_cancel_sending_media_message_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onCancelSendingMediaMessageCalled =
                ios_cancel_sending_media_message_proxy_on_cancel_sending_media_message_called;
            return proxy;
        }
#endregion
#region RCIMIWCancelDownloadingMediaMessageCallback
        [MonoPInvokeCallback(typeof(IOSCancelDownloadingMediaMessageProxyOnCancelDownloadingMediaMessageCalled))]
        private static void ios_cancel_downloading_media_message_proxy_on_cancel_downloading_media_message_called(
            int code, IntPtr message, Int64 handle)
        {
            RCIMMediaMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMediaMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnCancelDownloadingMediaMessageCalled",
                                            $"code={code},message_cls={message_cls}");
            var cb = NativeUtils.TakeCallback<OnCancelDownloadingMediaMessageCalledAction>(handle);
            cb?.Invoke(code, message_cls);
        }
        internal static ios_cancel_downloading_media_message_proxy toIOSCancelDownloadingMediaMessageProxy(
            OnCancelDownloadingMediaMessageCalledAction listener)
        {
            ios_cancel_downloading_media_message_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onCancelDownloadingMediaMessageCalled =
                ios_cancel_downloading_media_message_proxy_on_cancel_downloading_media_message_called;
            return proxy;
        }
#endregion
#region RCIMIWGetConversationCallback
        [MonoPInvokeCallback(typeof(IOSGetConversationProxyOnSuccess))]
        private static void ios_get_conversation_proxy_on_success(IntPtr t, Int64 handle)
        {
            RCIMConversation t_cls = null;
            if (t != IntPtr.Zero)
            {
                var ct = NativeUtils.GetStructByPtr<im_conversation>(t);
                t_cls = NativeConvert.fromConversation(ref ct);
            }
            RCUnityLogger.getInstance().log("OnSuccess", $"t_cls={t_cls}");
            var cb = NativeUtils.TakeCallback<RCIMGetConversationListener>(handle);
            cb?.OnSuccess(t_cls);
        }
        [MonoPInvokeCallback(typeof(IOSGetConversationProxyOnError))]
        private static void ios_get_conversation_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetConversationListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_conversation_proxy toIOSGetConversationProxy(RCIMGetConversationListener listener)
        {
            ios_get_conversation_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_conversation_proxy_on_success;
            proxy.onError = ios_get_conversation_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWGetConversationsCallback
        [MonoPInvokeCallback(typeof(IOSGetConversationsProxyOnSuccess))]
        private static void ios_get_conversations_proxy_on_success(ref ios_c_list t, Int64 handle)
        {
            List<RCIMConversation> t_list = new List<RCIMConversation>();
            im_conversation[] t_clist = NativeUtils.GetObjectListByStruct<im_conversation>(ref t);
            for (int i = 0; i < t_clist.Length; i++)
            {
                t_list.Add(NativeConvert.fromConversation(ref t_clist[i]));
            }
            RCUnityLogger.getInstance().log("OnSuccess", $"t_list={t_list}");
            var cb = NativeUtils.TakeCallback<RCIMGetConversationsListener>(handle);
            cb?.OnSuccess(t_list);
        }
        [MonoPInvokeCallback(typeof(IOSGetConversationsProxyOnError))]
        private static void ios_get_conversations_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetConversationsListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_conversations_proxy toIOSGetConversationsProxy(RCIMGetConversationsListener listener)
        {
            ios_get_conversations_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_conversations_proxy_on_success;
            proxy.onError = ios_get_conversations_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWRemoveConversationCallback
        [MonoPInvokeCallback(typeof(IOSRemoveConversationProxyOnConversationRemoved))]
        private static void ios_remove_conversation_proxy_on_conversation_removed(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnConversationRemoved", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnConversationRemovedAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_remove_conversation_proxy toIOSRemoveConversationProxy(OnConversationRemovedAction listener)
        {
            ios_remove_conversation_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onConversationRemoved = ios_remove_conversation_proxy_on_conversation_removed;
            return proxy;
        }
#endregion
#region RCIMIWRemoveConversationsCallback
        [MonoPInvokeCallback(typeof(IOSRemoveConversationsProxyOnConversationsRemoved))]
        private static void ios_remove_conversations_proxy_on_conversations_removed(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnConversationsRemoved", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnConversationsRemovedAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_remove_conversations_proxy toIOSRemoveConversationsProxy(OnConversationsRemovedAction listener)
        {
            ios_remove_conversations_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onConversationsRemoved = ios_remove_conversations_proxy_on_conversations_removed;
            return proxy;
        }
#endregion
#region RCIMIWGetUnreadCountCallback
        [MonoPInvokeCallback(typeof(IOSGetUnreadCountProxyOnSuccess))]
        private static void ios_get_unread_count_proxy_on_success(int t, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnSuccess", $"t={t}");
            var cb = NativeUtils.TakeCallback<RCIMGetUnreadCountListener>(handle);
            cb?.OnSuccess(t);
        }
        [MonoPInvokeCallback(typeof(IOSGetUnreadCountProxyOnError))]
        private static void ios_get_unread_count_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetUnreadCountListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_unread_count_proxy toIOSGetUnreadCountProxy(RCIMGetUnreadCountListener listener)
        {
            ios_get_unread_count_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_unread_count_proxy_on_success;
            proxy.onError = ios_get_unread_count_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWGetTotalUnreadCountCallback
        [MonoPInvokeCallback(typeof(IOSGetTotalUnreadCountProxyOnSuccess))]
        private static void ios_get_total_unread_count_proxy_on_success(int t, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnSuccess", $"t={t}");
            var cb = NativeUtils.TakeCallback<RCIMGetTotalUnreadCountListener>(handle);
            cb?.OnSuccess(t);
        }
        [MonoPInvokeCallback(typeof(IOSGetTotalUnreadCountProxyOnError))]
        private static void ios_get_total_unread_count_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetTotalUnreadCountListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_total_unread_count_proxy toIOSGetTotalUnreadCountProxy(
            RCIMGetTotalUnreadCountListener listener)
        {
            ios_get_total_unread_count_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_total_unread_count_proxy_on_success;
            proxy.onError = ios_get_total_unread_count_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWGetUnreadMentionedCountCallback
        [MonoPInvokeCallback(typeof(IOSGetUnreadMentionedCountProxyOnSuccess))]
        private static void ios_get_unread_mentioned_count_proxy_on_success(int t, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnSuccess", $"t={t}");
            var cb = NativeUtils.TakeCallback<RCIMGetUnreadMentionedCountListener>(handle);
            cb?.OnSuccess(t);
        }
        [MonoPInvokeCallback(typeof(IOSGetUnreadMentionedCountProxyOnError))]
        private static void ios_get_unread_mentioned_count_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetUnreadMentionedCountListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_unread_mentioned_count_proxy toIOSGetUnreadMentionedCountProxy(
            RCIMGetUnreadMentionedCountListener listener)
        {
            ios_get_unread_mentioned_count_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_unread_mentioned_count_proxy_on_success;
            proxy.onError = ios_get_unread_mentioned_count_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWGetUltraGroupAllUnreadCountCallback
        [MonoPInvokeCallback(typeof(IOSGetUltraGroupAllUnreadCountProxyOnSuccess))]
        private static void ios_get_ultra_group_all_unread_count_proxy_on_success(int t, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnSuccess", $"t={t}");
            var cb = NativeUtils.TakeCallback<RCIMGetUltraGroupAllUnreadCountListener>(handle);
            cb?.OnSuccess(t);
        }
        [MonoPInvokeCallback(typeof(IOSGetUltraGroupAllUnreadCountProxyOnError))]
        private static void ios_get_ultra_group_all_unread_count_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetUltraGroupAllUnreadCountListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_ultra_group_all_unread_count_proxy toIOSGetUltraGroupAllUnreadCountProxy(
            RCIMGetUltraGroupAllUnreadCountListener listener)
        {
            ios_get_ultra_group_all_unread_count_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_ultra_group_all_unread_count_proxy_on_success;
            proxy.onError = ios_get_ultra_group_all_unread_count_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWGetUltraGroupAllUnreadMentionedCountCallback
        [MonoPInvokeCallback(typeof(IOSGetUltraGroupAllUnreadMentionedCountProxyOnSuccess))]
        private static void ios_get_ultra_group_all_unread_mentioned_count_proxy_on_success(int t, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnSuccess", $"t={t}");
            var cb = NativeUtils.TakeCallback<RCIMGetUltraGroupAllUnreadMentionedCountListener>(handle);
            cb?.OnSuccess(t);
        }
        [MonoPInvokeCallback(typeof(IOSGetUltraGroupAllUnreadMentionedCountProxyOnError))]
        private static void ios_get_ultra_group_all_unread_mentioned_count_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetUltraGroupAllUnreadMentionedCountListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_ultra_group_all_unread_mentioned_count_proxy toIOSGetUltraGroupAllUnreadMentionedCountProxy(
            RCIMGetUltraGroupAllUnreadMentionedCountListener listener)
        {
            ios_get_ultra_group_all_unread_mentioned_count_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_ultra_group_all_unread_mentioned_count_proxy_on_success;
            proxy.onError = ios_get_ultra_group_all_unread_mentioned_count_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWGetUltraGroupUnreadCountCallback
        [MonoPInvokeCallback(typeof(IOSGetUltraGroupUnreadCountProxyOnSuccess))]
        private static void ios_get_ultra_group_unread_count_proxy_on_success(int t, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnSuccess", $"t={t}");
            var cb = NativeUtils.TakeCallback<RCIMGetUltraGroupUnreadCountListener>(handle);
            cb?.OnSuccess(t);
        }
        [MonoPInvokeCallback(typeof(IOSGetUltraGroupUnreadCountProxyOnError))]
        private static void ios_get_ultra_group_unread_count_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetUltraGroupUnreadCountListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_ultra_group_unread_count_proxy toIOSGetUltraGroupUnreadCountProxy(
            RCIMGetUltraGroupUnreadCountListener listener)
        {
            ios_get_ultra_group_unread_count_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_ultra_group_unread_count_proxy_on_success;
            proxy.onError = ios_get_ultra_group_unread_count_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWGetUltraGroupUnreadMentionedCountCallback
        [MonoPInvokeCallback(typeof(IOSGetUltraGroupUnreadMentionedCountProxyOnSuccess))]
        private static void ios_get_ultra_group_unread_mentioned_count_proxy_on_success(int t, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnSuccess", $"t={t}");
            var cb = NativeUtils.TakeCallback<RCIMGetUltraGroupUnreadMentionedCountListener>(handle);
            cb?.OnSuccess(t);
        }
        [MonoPInvokeCallback(typeof(IOSGetUltraGroupUnreadMentionedCountProxyOnError))]
        private static void ios_get_ultra_group_unread_mentioned_count_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetUltraGroupUnreadMentionedCountListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_ultra_group_unread_mentioned_count_proxy toIOSGetUltraGroupUnreadMentionedCountProxy(
            RCIMGetUltraGroupUnreadMentionedCountListener listener)
        {
            ios_get_ultra_group_unread_mentioned_count_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_ultra_group_unread_mentioned_count_proxy_on_success;
            proxy.onError = ios_get_ultra_group_unread_mentioned_count_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWGetUnreadCountByConversationTypesCallback
        [MonoPInvokeCallback(typeof(IOSGetUnreadCountByConversationTypesProxyOnSuccess))]
        private static void ios_get_unread_count_by_conversation_types_proxy_on_success(int t, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnSuccess", $"t={t}");
            var cb = NativeUtils.TakeCallback<RCIMGetUnreadCountByConversationTypesListener>(handle);
            cb?.OnSuccess(t);
        }
        [MonoPInvokeCallback(typeof(IOSGetUnreadCountByConversationTypesProxyOnError))]
        private static void ios_get_unread_count_by_conversation_types_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetUnreadCountByConversationTypesListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_unread_count_by_conversation_types_proxy toIOSGetUnreadCountByConversationTypesProxy(
            RCIMGetUnreadCountByConversationTypesListener listener)
        {
            ios_get_unread_count_by_conversation_types_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_unread_count_by_conversation_types_proxy_on_success;
            proxy.onError = ios_get_unread_count_by_conversation_types_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWClearUnreadCountCallback
        [MonoPInvokeCallback(typeof(IOSClearUnreadCountProxyOnUnreadCountCleared))]
        private static void ios_clear_unread_count_proxy_on_unread_count_cleared(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnUnreadCountCleared", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnUnreadCountClearedAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_clear_unread_count_proxy toIOSClearUnreadCountProxy(OnUnreadCountClearedAction listener)
        {
            ios_clear_unread_count_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onUnreadCountCleared = ios_clear_unread_count_proxy_on_unread_count_cleared;
            return proxy;
        }
#endregion
#region RCIMIWSaveDraftMessageCallback
        [MonoPInvokeCallback(typeof(IOSSaveDraftMessageProxyOnDraftMessageSaved))]
        private static void ios_save_draft_message_proxy_on_draft_message_saved(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnDraftMessageSaved", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnDraftMessageSavedAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_save_draft_message_proxy toIOSSaveDraftMessageProxy(OnDraftMessageSavedAction listener)
        {
            ios_save_draft_message_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onDraftMessageSaved = ios_save_draft_message_proxy_on_draft_message_saved;
            return proxy;
        }
#endregion
#region RCIMIWGetDraftMessageCallback
        [MonoPInvokeCallback(typeof(IOSGetDraftMessageProxyOnSuccess))]
        private static void ios_get_draft_message_proxy_on_success(string t, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnSuccess", $"t={t}");
            var cb = NativeUtils.TakeCallback<RCIMGetDraftMessageListener>(handle);
            cb?.OnSuccess(t);
        }
        [MonoPInvokeCallback(typeof(IOSGetDraftMessageProxyOnError))]
        private static void ios_get_draft_message_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetDraftMessageListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_draft_message_proxy toIOSGetDraftMessageProxy(RCIMGetDraftMessageListener listener)
        {
            ios_get_draft_message_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_draft_message_proxy_on_success;
            proxy.onError = ios_get_draft_message_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWClearDraftMessageCallback
        [MonoPInvokeCallback(typeof(IOSClearDraftMessageProxyOnDraftMessageCleared))]
        private static void ios_clear_draft_message_proxy_on_draft_message_cleared(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnDraftMessageCleared", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnDraftMessageClearedAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_clear_draft_message_proxy toIOSClearDraftMessageProxy(OnDraftMessageClearedAction listener)
        {
            ios_clear_draft_message_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onDraftMessageCleared = ios_clear_draft_message_proxy_on_draft_message_cleared;
            return proxy;
        }
#endregion
#region RCIMIWGetBlockedConversationsCallback
        [MonoPInvokeCallback(typeof(IOSGetBlockedConversationsProxyOnSuccess))]
        private static void ios_get_blocked_conversations_proxy_on_success(ref ios_c_list t, Int64 handle)
        {
            List<RCIMConversation> t_list = new List<RCIMConversation>();
            im_conversation[] t_clist = NativeUtils.GetObjectListByStruct<im_conversation>(ref t);
            for (int i = 0; i < t_clist.Length; i++)
            {
                t_list.Add(NativeConvert.fromConversation(ref t_clist[i]));
            }
            RCUnityLogger.getInstance().log("OnSuccess", $"t_list={t_list}");
            var cb = NativeUtils.TakeCallback<RCIMGetBlockedConversationsListener>(handle);
            cb?.OnSuccess(t_list);
        }
        [MonoPInvokeCallback(typeof(IOSGetBlockedConversationsProxyOnError))]
        private static void ios_get_blocked_conversations_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetBlockedConversationsListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_blocked_conversations_proxy toIOSGetBlockedConversationsProxy(
            RCIMGetBlockedConversationsListener listener)
        {
            ios_get_blocked_conversations_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_blocked_conversations_proxy_on_success;
            proxy.onError = ios_get_blocked_conversations_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWChangeConversationTopStatusCallback
        [MonoPInvokeCallback(typeof(IOSChangeConversationTopStatusProxyOnConversationTopStatusChanged))]
        private static void ios_change_conversation_top_status_proxy_on_conversation_top_status_changed(int code,
                                                                                                        Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnConversationTopStatusChanged", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnConversationTopStatusChangedAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_change_conversation_top_status_proxy toIOSChangeConversationTopStatusProxy(
            OnConversationTopStatusChangedAction listener)
        {
            ios_change_conversation_top_status_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onConversationTopStatusChanged =
                ios_change_conversation_top_status_proxy_on_conversation_top_status_changed;
            return proxy;
        }
#endregion
#region RCIMIWGetConversationTopStatusCallback
        [MonoPInvokeCallback(typeof(IOSGetConversationTopStatusProxyOnSuccess))]
        private static void ios_get_conversation_top_status_proxy_on_success(Boolean t, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnSuccess", $"t={t}");
            var cb = NativeUtils.TakeCallback<RCIMGetConversationTopStatusListener>(handle);
            cb?.OnSuccess(t);
        }
        [MonoPInvokeCallback(typeof(IOSGetConversationTopStatusProxyOnError))]
        private static void ios_get_conversation_top_status_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetConversationTopStatusListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_conversation_top_status_proxy toIOSGetConversationTopStatusProxy(
            RCIMGetConversationTopStatusListener listener)
        {
            ios_get_conversation_top_status_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_conversation_top_status_proxy_on_success;
            proxy.onError = ios_get_conversation_top_status_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWSyncConversationReadStatusCallback
        [MonoPInvokeCallback(typeof(IOSSyncConversationReadStatusProxyOnConversationReadStatusSynced))]
        private static void ios_sync_conversation_read_status_proxy_on_conversation_read_status_synced(int code,
                                                                                                       Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnConversationReadStatusSynced", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnConversationReadStatusSyncedAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_sync_conversation_read_status_proxy toIOSSyncConversationReadStatusProxy(
            OnConversationReadStatusSyncedAction listener)
        {
            ios_sync_conversation_read_status_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onConversationReadStatusSynced =
                ios_sync_conversation_read_status_proxy_on_conversation_read_status_synced;
            return proxy;
        }
#endregion
#region RCIMIWGetMessagesCallback
        [MonoPInvokeCallback(typeof(IOSGetMessagesProxyOnSuccess))]
        private static void ios_get_messages_proxy_on_success(ref ios_c_list t, Int64 handle)
        {
            List<RCIMMessage> t_list = new List<RCIMMessage>();
            ios_class_warpper[] t_clist = NativeUtils.GetObjectListByStruct<ios_class_warpper>(ref t);
            for (int i = 0; i < t_clist.Length; i++)
            {
                t_list.Add(NativeConvert.fromMessageWapper(ref t_clist[i]));
            }
            RCUnityLogger.getInstance().log("OnSuccess", $"t_list={t_list}");
            var cb = NativeUtils.TakeCallback<RCIMGetMessagesListener>(handle);
            cb?.OnSuccess(t_list);
        }
        [MonoPInvokeCallback(typeof(IOSGetMessagesProxyOnError))]
        private static void ios_get_messages_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetMessagesListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_messages_proxy toIOSGetMessagesProxy(RCIMGetMessagesListener listener)
        {
            ios_get_messages_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_messages_proxy_on_success;
            proxy.onError = ios_get_messages_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWGetMessageCallback
        [MonoPInvokeCallback(typeof(IOSGetMessageProxyOnSuccess))]
        private static void ios_get_message_proxy_on_success(IntPtr t, Int64 handle)
        {
            RCIMMessage t_cls = null;
            if (t != IntPtr.Zero)
            {
                var ct = NativeUtils.GetStructByPtr<ios_class_warpper>(t);
                t_cls = NativeConvert.fromMessageWapper(ref ct);
            }
            RCUnityLogger.getInstance().log("OnSuccess", $"t_cls={t_cls}");
            var cb = NativeUtils.TakeCallback<RCIMGetMessageListener>(handle);
            cb?.OnSuccess(t_cls);
        }
        [MonoPInvokeCallback(typeof(IOSGetMessageProxyOnError))]
        private static void ios_get_message_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetMessageListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_message_proxy toIOSGetMessageProxy(RCIMGetMessageListener listener)
        {
            ios_get_message_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_message_proxy_on_success;
            proxy.onError = ios_get_message_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWGetFirstUnreadMessageCallback
        [MonoPInvokeCallback(typeof(IOSGetFirstUnreadMessageProxyOnSuccess))]
        private static void ios_get_first_unread_message_proxy_on_success(IntPtr t, Int64 handle)
        {
            RCIMMessage t_cls = null;
            if (t != IntPtr.Zero)
            {
                var ct = NativeUtils.GetStructByPtr<ios_class_warpper>(t);
                t_cls = NativeConvert.fromMessageWapper(ref ct);
            }
            RCUnityLogger.getInstance().log("OnSuccess", $"t_cls={t_cls}");
            var cb = NativeUtils.TakeCallback<RCIMGetFirstUnreadMessageListener>(handle);
            cb?.OnSuccess(t_cls);
        }
        [MonoPInvokeCallback(typeof(IOSGetFirstUnreadMessageProxyOnError))]
        private static void ios_get_first_unread_message_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetFirstUnreadMessageListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_first_unread_message_proxy toIOSGetFirstUnreadMessageProxy(
            RCIMGetFirstUnreadMessageListener listener)
        {
            ios_get_first_unread_message_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_first_unread_message_proxy_on_success;
            proxy.onError = ios_get_first_unread_message_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWGetUnreadMentionedMessagesCallback
        [MonoPInvokeCallback(typeof(IOSGetUnreadMentionedMessagesProxyOnSuccess))]
        private static void ios_get_unread_mentioned_messages_proxy_on_success(ref ios_c_list t, Int64 handle)
        {
            List<RCIMMessage> t_list = new List<RCIMMessage>();
            ios_class_warpper[] t_clist = NativeUtils.GetObjectListByStruct<ios_class_warpper>(ref t);
            for (int i = 0; i < t_clist.Length; i++)
            {
                t_list.Add(NativeConvert.fromMessageWapper(ref t_clist[i]));
            }
            RCUnityLogger.getInstance().log("OnSuccess", $"t_list={t_list}");
            var cb = NativeUtils.TakeCallback<RCIMGetUnreadMentionedMessagesListener>(handle);
            cb?.OnSuccess(t_list);
        }
        [MonoPInvokeCallback(typeof(IOSGetUnreadMentionedMessagesProxyOnError))]
        private static void ios_get_unread_mentioned_messages_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetUnreadMentionedMessagesListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_unread_mentioned_messages_proxy toIOSGetUnreadMentionedMessagesProxy(
            RCIMGetUnreadMentionedMessagesListener listener)
        {
            ios_get_unread_mentioned_messages_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_unread_mentioned_messages_proxy_on_success;
            proxy.onError = ios_get_unread_mentioned_messages_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWInsertMessageCallback
        [MonoPInvokeCallback(typeof(IOSInsertMessageProxyOnMessageInserted))]
        private static void ios_insert_message_proxy_on_message_inserted(int code, IntPtr message, Int64 handle)
        {
            RCIMMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnMessageInserted", $"code={code},message_cls={message_cls}");
            var cb = NativeUtils.TakeCallback<OnMessageInsertedAction>(handle);
            cb?.Invoke(code, message_cls);
        }
        internal static ios_insert_message_proxy toIOSInsertMessageProxy(OnMessageInsertedAction listener)
        {
            ios_insert_message_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onMessageInserted = ios_insert_message_proxy_on_message_inserted;
            return proxy;
        }
#endregion
#region RCIMIWInsertMessagesCallback
        [MonoPInvokeCallback(typeof(IOSInsertMessagesProxyOnMessagesInserted))]
        private static void ios_insert_messages_proxy_on_messages_inserted(int code, ref ios_c_list messages, Int64 handle)
        {
            List<RCIMMessage> messages_list = new List<RCIMMessage>();
            ios_class_warpper[] messages_clist = NativeUtils.GetObjectListByStruct<ios_class_warpper>(ref messages);
            for (int i = 0; i < messages_clist.Length; i++)
            {
                messages_list.Add(NativeConvert.fromMessageWapper(ref messages_clist[i]));
            }
            RCUnityLogger.getInstance().log("OnMessagesInserted", $"code={code},messages_list={messages_list}");
            var cb = NativeUtils.TakeCallback<OnMessagesInsertedAction>(handle);
            cb?.Invoke(code, messages_list);
        }
        internal static ios_insert_messages_proxy toIOSInsertMessagesProxy(OnMessagesInsertedAction listener)
        {
            ios_insert_messages_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onMessagesInserted = ios_insert_messages_proxy_on_messages_inserted;
            return proxy;
        }
#endregion
#region RCIMIWClearMessagesCallback
        [MonoPInvokeCallback(typeof(IOSClearMessagesProxyOnMessagesCleared))]
        private static void ios_clear_messages_proxy_on_messages_cleared(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnMessagesCleared", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnMessagesClearedAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_clear_messages_proxy toIOSClearMessagesProxy(OnMessagesClearedAction listener)
        {
            ios_clear_messages_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onMessagesCleared = ios_clear_messages_proxy_on_messages_cleared;
            return proxy;
        }
#endregion
#region RCIMIWDeleteLocalMessagesCallback
        [MonoPInvokeCallback(typeof(IOSDeleteLocalMessagesProxyOnLocalMessagesDeleted))]
        private static void ios_delete_local_messages_proxy_on_local_messages_deleted(int code, ref ios_c_list messages,
                                                                                      Int64 handle)
        {
            List<RCIMMessage> messages_list = new List<RCIMMessage>();
            ios_class_warpper[] messages_clist = NativeUtils.GetObjectListByStruct<ios_class_warpper>(ref messages);
            for (int i = 0; i < messages_clist.Length; i++)
            {
                messages_list.Add(NativeConvert.fromMessageWapper(ref messages_clist[i]));
            }
            RCUnityLogger.getInstance().log("OnLocalMessagesDeleted", $"code={code},messages_list={messages_list}");
            var cb = NativeUtils.TakeCallback<OnLocalMessagesDeletedAction>(handle);
            cb?.Invoke(code, messages_list);
        }
        internal static ios_delete_local_messages_proxy toIOSDeleteLocalMessagesProxy(OnLocalMessagesDeletedAction listener)
        {
            ios_delete_local_messages_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onLocalMessagesDeleted = ios_delete_local_messages_proxy_on_local_messages_deleted;
            return proxy;
        }
#endregion
#region RCIMIWDeleteMessagesCallback
        [MonoPInvokeCallback(typeof(IOSDeleteMessagesProxyOnMessagesDeleted))]
        private static void ios_delete_messages_proxy_on_messages_deleted(int code, ref ios_c_list messages, Int64 handle)
        {
            List<RCIMMessage> messages_list = new List<RCIMMessage>();
            ios_class_warpper[] messages_clist = NativeUtils.GetObjectListByStruct<ios_class_warpper>(ref messages);
            for (int i = 0; i < messages_clist.Length; i++)
            {
                messages_list.Add(NativeConvert.fromMessageWapper(ref messages_clist[i]));
            }
            RCUnityLogger.getInstance().log("OnMessagesDeleted", $"code={code},messages_list={messages_list}");
            var cb = NativeUtils.TakeCallback<OnMessagesDeletedAction>(handle);
            cb?.Invoke(code, messages_list);
        }
        internal static ios_delete_messages_proxy toIOSDeleteMessagesProxy(OnMessagesDeletedAction listener)
        {
            ios_delete_messages_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onMessagesDeleted = ios_delete_messages_proxy_on_messages_deleted;
            return proxy;
        }
#endregion
#region RCIMIWRecallMessageCallback
        [MonoPInvokeCallback(typeof(IOSRecallMessageProxyOnMessageRecalled))]
        private static void ios_recall_message_proxy_on_message_recalled(int code, IntPtr message, Int64 handle)
        {
            RCIMMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnMessageRecalled", $"code={code},message_cls={message_cls}");
            var cb = NativeUtils.TakeCallback<OnMessageRecalledAction>(handle);
            cb?.Invoke(code, message_cls);
        }
        internal static ios_recall_message_proxy toIOSRecallMessageProxy(OnMessageRecalledAction listener)
        {
            ios_recall_message_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onMessageRecalled = ios_recall_message_proxy_on_message_recalled;
            return proxy;
        }
#endregion
#region RCIMIWSendPrivateReadReceiptMessageCallback
        [MonoPInvokeCallback(typeof(IOSSendPrivateReadReceiptMessageProxyOnPrivateReadReceiptMessageSent))]
        private static void ios_send_private_read_receipt_message_proxy_on_private_read_receipt_message_sent(int code,
                                                                                                             Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnPrivateReadReceiptMessageSent", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnPrivateReadReceiptMessageSentAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_send_private_read_receipt_message_proxy toIOSSendPrivateReadReceiptMessageProxy(
            OnPrivateReadReceiptMessageSentAction listener)
        {
            ios_send_private_read_receipt_message_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onPrivateReadReceiptMessageSent =
                ios_send_private_read_receipt_message_proxy_on_private_read_receipt_message_sent;
            return proxy;
        }
#endregion
#region RCIMIWSendGroupReadReceiptRequestCallback
        [MonoPInvokeCallback(typeof(IOSSendGroupReadReceiptRequestProxyOnGroupReadReceiptRequestSent))]
        private static void ios_send_group_read_receipt_request_proxy_on_group_read_receipt_request_sent(int code,
                                                                                                         IntPtr message,
                                                                                                         Int64 handle)
        {
            RCIMMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnGroupReadReceiptRequestSent", $"code={code},message_cls={message_cls}");
            var cb = NativeUtils.TakeCallback<OnGroupReadReceiptRequestSentAction>(handle);
            cb?.Invoke(code, message_cls);
        }
        internal static ios_send_group_read_receipt_request_proxy toIOSSendGroupReadReceiptRequestProxy(
            OnGroupReadReceiptRequestSentAction listener)
        {
            ios_send_group_read_receipt_request_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onGroupReadReceiptRequestSent =
                ios_send_group_read_receipt_request_proxy_on_group_read_receipt_request_sent;
            return proxy;
        }
#endregion
#region RCIMIWSendGroupReadReceiptResponseCallback
        [MonoPInvokeCallback(typeof(IOSSendGroupReadReceiptResponseProxyOnGroupReadReceiptResponseSent))]
        private static void ios_send_group_read_receipt_response_proxy_on_group_read_receipt_response_sent(
            int code, ref ios_c_list message, Int64 handle)
        {
            List<RCIMMessage> message_list = new List<RCIMMessage>();
            ios_class_warpper[] message_clist = NativeUtils.GetObjectListByStruct<ios_class_warpper>(ref message);
            for (int i = 0; i < message_clist.Length; i++)
            {
                message_list.Add(NativeConvert.fromMessageWapper(ref message_clist[i]));
            }
            RCUnityLogger.getInstance().log("OnGroupReadReceiptResponseSent", $"code={code},message_list={message_list}");
            var cb = NativeUtils.TakeCallback<OnGroupReadReceiptResponseSentAction>(handle);
            cb?.Invoke(code, message_list);
        }
        internal static ios_send_group_read_receipt_response_proxy toIOSSendGroupReadReceiptResponseProxy(
            OnGroupReadReceiptResponseSentAction listener)
        {
            ios_send_group_read_receipt_response_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onGroupReadReceiptResponseSent =
                ios_send_group_read_receipt_response_proxy_on_group_read_receipt_response_sent;
            return proxy;
        }
#endregion
#region RCIMIWUpdateMessageExpansionCallback
        [MonoPInvokeCallback(typeof(IOSUpdateMessageExpansionProxyOnMessageExpansionUpdated))]
        private static void ios_update_message_expansion_proxy_on_message_expansion_updated(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnMessageExpansionUpdated", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnMessageExpansionUpdatedAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_update_message_expansion_proxy toIOSUpdateMessageExpansionProxy(
            OnMessageExpansionUpdatedAction listener)
        {
            ios_update_message_expansion_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onMessageExpansionUpdated = ios_update_message_expansion_proxy_on_message_expansion_updated;
            return proxy;
        }
#endregion
#region RCIMIWRemoveMessageExpansionForKeysCallback
        [MonoPInvokeCallback(typeof(IOSRemoveMessageExpansionForKeysProxyOnMessageExpansionForKeysRemoved))]
        private static void ios_remove_message_expansion_for_keys_proxy_on_message_expansion_for_keys_removed(int code,
                                                                                                              Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnMessageExpansionForKeysRemoved", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnMessageExpansionForKeysRemovedAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_remove_message_expansion_for_keys_proxy toIOSRemoveMessageExpansionForKeysProxy(
            OnMessageExpansionForKeysRemovedAction listener)
        {
            ios_remove_message_expansion_for_keys_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onMessageExpansionForKeysRemoved =
                ios_remove_message_expansion_for_keys_proxy_on_message_expansion_for_keys_removed;
            return proxy;
        }
#endregion
#region RCIMIWChangeMessageSentStatusCallback
        [MonoPInvokeCallback(typeof(IOSChangeMessageSentStatusProxyOnMessageSentStatusChanged))]
        private static void ios_change_message_sent_status_proxy_on_message_sent_status_changed(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnMessageSentStatusChanged", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnMessageSentStatusChangedAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_change_message_sent_status_proxy toIOSChangeMessageSentStatusProxy(
            OnMessageSentStatusChangedAction listener)
        {
            ios_change_message_sent_status_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onMessageSentStatusChanged = ios_change_message_sent_status_proxy_on_message_sent_status_changed;
            return proxy;
        }
#endregion
#region RCIMIWChangeMessageReceivedStatusCallback
        [MonoPInvokeCallback(typeof(IOSChangeMessageReceivedStatusProxyOnMessageReceiveStatusChanged))]
        private static void ios_change_message_received_status_proxy_on_message_receive_status_changed(int code,
                                                                                                       Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnMessageReceiveStatusChanged", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnMessageReceiveStatusChangedAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_change_message_received_status_proxy toIOSChangeMessageReceivedStatusProxy(
            OnMessageReceiveStatusChangedAction listener)
        {
            ios_change_message_received_status_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onMessageReceiveStatusChanged =
                ios_change_message_received_status_proxy_on_message_receive_status_changed;
            return proxy;
        }
#endregion
#region RCIMIWJoinChatRoomCallback
        [MonoPInvokeCallback(typeof(IOSJoinChatRoomProxyOnChatRoomJoined))]
        private static void ios_join_chat_room_proxy_on_chat_room_joined(int code, string targetId, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnChatRoomJoined", $"code={code},targetId={targetId}");
            var cb = NativeUtils.TakeCallback<OnChatRoomJoinedAction>(handle);
            cb?.Invoke(code, targetId);
        }
        internal static ios_join_chat_room_proxy toIOSJoinChatRoomProxy(OnChatRoomJoinedAction listener)
        {
            ios_join_chat_room_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onChatRoomJoined = ios_join_chat_room_proxy_on_chat_room_joined;
            return proxy;
        }
#endregion
#region RCIMIWLeaveChatRoomCallback
        [MonoPInvokeCallback(typeof(IOSLeaveChatRoomProxyOnChatRoomLeft))]
        private static void ios_leave_chat_room_proxy_on_chat_room_left(int code, string targetId, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnChatRoomLeft", $"code={code},targetId={targetId}");
            var cb = NativeUtils.TakeCallback<OnChatRoomLeftAction>(handle);
            cb?.Invoke(code, targetId);
        }
        internal static ios_leave_chat_room_proxy toIOSLeaveChatRoomProxy(OnChatRoomLeftAction listener)
        {
            ios_leave_chat_room_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onChatRoomLeft = ios_leave_chat_room_proxy_on_chat_room_left;
            return proxy;
        }
#endregion
#region RCIMIWGetChatRoomMessagesCallback
        [MonoPInvokeCallback(typeof(IOSGetChatRoomMessagesProxyOnSuccess))]
        private static void ios_get_chat_room_messages_proxy_on_success(ref ios_c_list t, Int64 handle)
        {
            List<RCIMMessage> t_list = new List<RCIMMessage>();
            ios_class_warpper[] t_clist = NativeUtils.GetObjectListByStruct<ios_class_warpper>(ref t);
            for (int i = 0; i < t_clist.Length; i++)
            {
                t_list.Add(NativeConvert.fromMessageWapper(ref t_clist[i]));
            }
            RCUnityLogger.getInstance().log("OnSuccess", $"t_list={t_list}");
            var cb = NativeUtils.TakeCallback<RCIMGetChatRoomMessagesListener>(handle);
            cb?.OnSuccess(t_list);
        }
        [MonoPInvokeCallback(typeof(IOSGetChatRoomMessagesProxyOnError))]
        private static void ios_get_chat_room_messages_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetChatRoomMessagesListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_chat_room_messages_proxy toIOSGetChatRoomMessagesProxy(
            RCIMGetChatRoomMessagesListener listener)
        {
            ios_get_chat_room_messages_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_chat_room_messages_proxy_on_success;
            proxy.onError = ios_get_chat_room_messages_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWAddChatRoomEntryCallback
        [MonoPInvokeCallback(typeof(IOSAddChatRoomEntryProxyOnChatRoomEntryAdded))]
        private static void ios_add_chat_room_entry_proxy_on_chat_room_entry_added(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnChatRoomEntryAdded", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnChatRoomEntryAddedAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_add_chat_room_entry_proxy toIOSAddChatRoomEntryProxy(OnChatRoomEntryAddedAction listener)
        {
            ios_add_chat_room_entry_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onChatRoomEntryAdded = ios_add_chat_room_entry_proxy_on_chat_room_entry_added;
            return proxy;
        }
#endregion
#region RCIMIWAddChatRoomEntriesCallback
        [MonoPInvokeCallback(typeof(IOSAddChatRoomEntriesProxyOnChatRoomEntriesAdded))]
        private static void ios_add_chat_room_entries_proxy_on_chat_room_entries_added(int code, ref ios_c_list errors,
                                                                                       Int64 handle)
        {
            var errors_dic = NativeUtils.GetObjectMapByStruct<string, int>(ref errors);
            RCUnityLogger.getInstance().log("OnChatRoomEntriesAdded", $"code={code},errors_dic={errors_dic}");
            var cb = NativeUtils.TakeCallback<OnChatRoomEntriesAddedAction>(handle);
            cb?.Invoke(code, errors_dic);
        }
        internal static ios_add_chat_room_entries_proxy toIOSAddChatRoomEntriesProxy(OnChatRoomEntriesAddedAction listener)
        {
            ios_add_chat_room_entries_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onChatRoomEntriesAdded = ios_add_chat_room_entries_proxy_on_chat_room_entries_added;
            return proxy;
        }
#endregion
#region RCIMIWGetChatRoomEntryCallback
        [MonoPInvokeCallback(typeof(IOSGetChatRoomEntryProxyOnSuccess))]
        private static void ios_get_chat_room_entry_proxy_on_success(ref ios_c_list t, Int64 handle)
        {
            var t_dic = NativeUtils.GetObjectMapByStruct<string, string>(ref t);
            RCUnityLogger.getInstance().log("OnSuccess", $"t_dic={t_dic}");
            var cb = NativeUtils.TakeCallback<RCIMGetChatRoomEntryListener>(handle);
            cb?.OnSuccess(t_dic);
        }
        [MonoPInvokeCallback(typeof(IOSGetChatRoomEntryProxyOnError))]
        private static void ios_get_chat_room_entry_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetChatRoomEntryListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_chat_room_entry_proxy toIOSGetChatRoomEntryProxy(RCIMGetChatRoomEntryListener listener)
        {
            ios_get_chat_room_entry_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_chat_room_entry_proxy_on_success;
            proxy.onError = ios_get_chat_room_entry_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWGetChatRoomAllEntriesCallback
        [MonoPInvokeCallback(typeof(IOSGetChatRoomAllEntriesProxyOnSuccess))]
        private static void ios_get_chat_room_all_entries_proxy_on_success(ref ios_c_list t, Int64 handle)
        {
            var t_dic = NativeUtils.GetObjectMapByStruct<string, string>(ref t);
            RCUnityLogger.getInstance().log("OnSuccess", $"t_dic={t_dic}");
            var cb = NativeUtils.TakeCallback<RCIMGetChatRoomAllEntriesListener>(handle);
            cb?.OnSuccess(t_dic);
        }
        [MonoPInvokeCallback(typeof(IOSGetChatRoomAllEntriesProxyOnError))]
        private static void ios_get_chat_room_all_entries_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetChatRoomAllEntriesListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_chat_room_all_entries_proxy toIOSGetChatRoomAllEntriesProxy(
            RCIMGetChatRoomAllEntriesListener listener)
        {
            ios_get_chat_room_all_entries_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_chat_room_all_entries_proxy_on_success;
            proxy.onError = ios_get_chat_room_all_entries_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWRemoveChatRoomEntryCallback
        [MonoPInvokeCallback(typeof(IOSRemoveChatRoomEntryProxyOnChatRoomEntryRemoved))]
        private static void ios_remove_chat_room_entry_proxy_on_chat_room_entry_removed(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnChatRoomEntryRemoved", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnChatRoomEntryRemovedAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_remove_chat_room_entry_proxy toIOSRemoveChatRoomEntryProxy(
            OnChatRoomEntryRemovedAction listener)
        {
            ios_remove_chat_room_entry_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onChatRoomEntryRemoved = ios_remove_chat_room_entry_proxy_on_chat_room_entry_removed;
            return proxy;
        }
#endregion
#region RCIMIWRemoveChatRoomEntriesCallback
        [MonoPInvokeCallback(typeof(IOSRemoveChatRoomEntriesProxyOnChatRoomEntriesRemoved))]
        private static void ios_remove_chat_room_entries_proxy_on_chat_room_entries_removed(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnChatRoomEntriesRemoved", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnChatRoomEntriesRemovedAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_remove_chat_room_entries_proxy toIOSRemoveChatRoomEntriesProxy(
            OnChatRoomEntriesRemovedAction listener)
        {
            ios_remove_chat_room_entries_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onChatRoomEntriesRemoved = ios_remove_chat_room_entries_proxy_on_chat_room_entries_removed;
            return proxy;
        }
#endregion
#region RCIMIWAddToBlacklistCallback
        [MonoPInvokeCallback(typeof(IOSAddToBlacklistProxyOnBlacklistAdded))]
        private static void ios_add_to_blacklist_proxy_on_blacklist_added(int code, string userId, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnBlacklistAdded", $"code={code},userId={userId}");
            var cb = NativeUtils.TakeCallback<OnBlacklistAddedAction>(handle);
            cb?.Invoke(code, userId);
        }
        internal static ios_add_to_blacklist_proxy toIOSAddToBlacklistProxy(OnBlacklistAddedAction listener)
        {
            ios_add_to_blacklist_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onBlacklistAdded = ios_add_to_blacklist_proxy_on_blacklist_added;
            return proxy;
        }
#endregion
#region RCIMIWRemoveFromBlacklistCallback
        [MonoPInvokeCallback(typeof(IOSRemoveFromBlacklistProxyOnBlacklistRemoved))]
        private static void ios_remove_from_blacklist_proxy_on_blacklist_removed(int code, string userId, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnBlacklistRemoved", $"code={code},userId={userId}");
            var cb = NativeUtils.TakeCallback<OnBlacklistRemovedAction>(handle);
            cb?.Invoke(code, userId);
        }
        internal static ios_remove_from_blacklist_proxy toIOSRemoveFromBlacklistProxy(OnBlacklistRemovedAction listener)
        {
            ios_remove_from_blacklist_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onBlacklistRemoved = ios_remove_from_blacklist_proxy_on_blacklist_removed;
            return proxy;
        }
#endregion
#region RCIMIWGetBlacklistStatusCallback
        [MonoPInvokeCallback(typeof(IOSGetBlacklistStatusProxyOnSuccess))]
        private static void ios_get_blacklist_status_proxy_on_success(RCIMBlacklistStatus t, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnSuccess", $"t={t}");
            var cb = NativeUtils.TakeCallback<RCIMGetBlacklistStatusListener>(handle);
            cb?.OnSuccess(t);
        }
        [MonoPInvokeCallback(typeof(IOSGetBlacklistStatusProxyOnError))]
        private static void ios_get_blacklist_status_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetBlacklistStatusListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_blacklist_status_proxy toIOSGetBlacklistStatusProxy(RCIMGetBlacklistStatusListener listener)
        {
            ios_get_blacklist_status_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_blacklist_status_proxy_on_success;
            proxy.onError = ios_get_blacklist_status_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWGetBlacklistCallback
        [MonoPInvokeCallback(typeof(IOSGetBlacklistProxyOnSuccess))]
        private static void ios_get_blacklist_proxy_on_success(ref ios_c_list t, Int64 handle)
        {
            List<String> t_list = new List<String>();
            IntPtr[] t_clist = NativeUtils.GetObjectListByStruct<IntPtr>(ref t);
            for (int i = 0; i < t_clist.Length; i++)
            {
                t_list.Add(Marshal.PtrToStringAnsi(t_clist[i]));
            }
            RCUnityLogger.getInstance().log("OnSuccess", $"t_list={t_list}");
            var cb = NativeUtils.TakeCallback<RCIMGetBlacklistListener>(handle);
            cb?.OnSuccess(t_list);
        }
        [MonoPInvokeCallback(typeof(IOSGetBlacklistProxyOnError))]
        private static void ios_get_blacklist_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetBlacklistListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_blacklist_proxy toIOSGetBlacklistProxy(RCIMGetBlacklistListener listener)
        {
            ios_get_blacklist_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_blacklist_proxy_on_success;
            proxy.onError = ios_get_blacklist_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWSearchMessagesCallback
        [MonoPInvokeCallback(typeof(IOSSearchMessagesProxyOnSuccess))]
        private static void ios_search_messages_proxy_on_success(ref ios_c_list t, Int64 handle)
        {
            List<RCIMMessage> t_list = new List<RCIMMessage>();
            ios_class_warpper[] t_clist = NativeUtils.GetObjectListByStruct<ios_class_warpper>(ref t);
            for (int i = 0; i < t_clist.Length; i++)
            {
                t_list.Add(NativeConvert.fromMessageWapper(ref t_clist[i]));
            }
            RCUnityLogger.getInstance().log("OnSuccess", $"t_list={t_list}");
            var cb = NativeUtils.TakeCallback<RCIMSearchMessagesListener>(handle);
            cb?.OnSuccess(t_list);
        }
        [MonoPInvokeCallback(typeof(IOSSearchMessagesProxyOnError))]
        private static void ios_search_messages_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMSearchMessagesListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_search_messages_proxy toIOSSearchMessagesProxy(RCIMSearchMessagesListener listener)
        {
            ios_search_messages_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_search_messages_proxy_on_success;
            proxy.onError = ios_search_messages_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWSearchMessagesByTimeRangeCallback
        [MonoPInvokeCallback(typeof(IOSSearchMessagesByTimeRangeProxyOnSuccess))]
        private static void ios_search_messages_by_time_range_proxy_on_success(ref ios_c_list t, Int64 handle)
        {
            List<RCIMMessage> t_list = new List<RCIMMessage>();
            ios_class_warpper[] t_clist = NativeUtils.GetObjectListByStruct<ios_class_warpper>(ref t);
            for (int i = 0; i < t_clist.Length; i++)
            {
                t_list.Add(NativeConvert.fromMessageWapper(ref t_clist[i]));
            }
            RCUnityLogger.getInstance().log("OnSuccess", $"t_list={t_list}");
            var cb = NativeUtils.TakeCallback<RCIMSearchMessagesByTimeRangeListener>(handle);
            cb?.OnSuccess(t_list);
        }
        [MonoPInvokeCallback(typeof(IOSSearchMessagesByTimeRangeProxyOnError))]
        private static void ios_search_messages_by_time_range_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMSearchMessagesByTimeRangeListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_search_messages_by_time_range_proxy toIOSSearchMessagesByTimeRangeProxy(
            RCIMSearchMessagesByTimeRangeListener listener)
        {
            ios_search_messages_by_time_range_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_search_messages_by_time_range_proxy_on_success;
            proxy.onError = ios_search_messages_by_time_range_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWSearchMessagesByUserIdCallback
        [MonoPInvokeCallback(typeof(IOSSearchMessagesByUserIdProxyOnSuccess))]
        private static void ios_search_messages_by_user_id_proxy_on_success(ref ios_c_list t, Int64 handle)
        {
            List<RCIMMessage> t_list = new List<RCIMMessage>();
            ios_class_warpper[] t_clist = NativeUtils.GetObjectListByStruct<ios_class_warpper>(ref t);
            for (int i = 0; i < t_clist.Length; i++)
            {
                t_list.Add(NativeConvert.fromMessageWapper(ref t_clist[i]));
            }
            RCUnityLogger.getInstance().log("OnSuccess", $"t_list={t_list}");
            var cb = NativeUtils.TakeCallback<RCIMSearchMessagesByUserIdListener>(handle);
            cb?.OnSuccess(t_list);
        }
        [MonoPInvokeCallback(typeof(IOSSearchMessagesByUserIdProxyOnError))]
        private static void ios_search_messages_by_user_id_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMSearchMessagesByUserIdListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_search_messages_by_user_id_proxy toIOSSearchMessagesByUserIdProxy(
            RCIMSearchMessagesByUserIdListener listener)
        {
            ios_search_messages_by_user_id_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_search_messages_by_user_id_proxy_on_success;
            proxy.onError = ios_search_messages_by_user_id_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWSearchConversationsCallback
        [MonoPInvokeCallback(typeof(IOSSearchConversationsProxyOnSuccess))]
        private static void ios_search_conversations_proxy_on_success(ref ios_c_list t, Int64 handle)
        {
            List<RCIMSearchConversationResult> t_list = new List<RCIMSearchConversationResult>();
            im_search_conversation_result[] t_clist =
                NativeUtils.GetObjectListByStruct<im_search_conversation_result>(ref t);
            for (int i = 0; i < t_clist.Length; i++)
            {
                t_list.Add(NativeConvert.fromSearchConversationResult(ref t_clist[i]));
            }
            RCUnityLogger.getInstance().log("OnSuccess", $"t_list={t_list}");
            var cb = NativeUtils.TakeCallback<RCIMSearchConversationsListener>(handle);
            cb?.OnSuccess(t_list);
        }
        [MonoPInvokeCallback(typeof(IOSSearchConversationsProxyOnError))]
        private static void ios_search_conversations_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMSearchConversationsListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_search_conversations_proxy toIOSSearchConversationsProxy(
            RCIMSearchConversationsListener listener)
        {
            ios_search_conversations_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_search_conversations_proxy_on_success;
            proxy.onError = ios_search_conversations_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWChangeNotificationQuietHoursCallback
        [MonoPInvokeCallback(typeof(IOSChangeNotificationQuietHoursProxyOnNotificationQuietHoursChanged))]
        private static void ios_change_notification_quiet_hours_proxy_on_notification_quiet_hours_changed(int code,
                                                                                                          Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnNotificationQuietHoursChanged", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnNotificationQuietHoursChangedAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_change_notification_quiet_hours_proxy toIOSChangeNotificationQuietHoursProxy(
            OnNotificationQuietHoursChangedAction listener)
        {
            ios_change_notification_quiet_hours_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onNotificationQuietHoursChanged =
                ios_change_notification_quiet_hours_proxy_on_notification_quiet_hours_changed;
            return proxy;
        }
#endregion
#region RCIMIWRemoveNotificationQuietHoursCallback
        [MonoPInvokeCallback(typeof(IOSRemoveNotificationQuietHoursProxyOnNotificationQuietHoursRemoved))]
        private static void ios_remove_notification_quiet_hours_proxy_on_notification_quiet_hours_removed(int code,
                                                                                                          Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnNotificationQuietHoursRemoved", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnNotificationQuietHoursRemovedAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_remove_notification_quiet_hours_proxy toIOSRemoveNotificationQuietHoursProxy(
            OnNotificationQuietHoursRemovedAction listener)
        {
            ios_remove_notification_quiet_hours_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onNotificationQuietHoursRemoved =
                ios_remove_notification_quiet_hours_proxy_on_notification_quiet_hours_removed;
            return proxy;
        }
#endregion
#region RCIMIWGetNotificationQuietHoursCallback
        [MonoPInvokeCallback(typeof(IOSGetNotificationQuietHoursProxyOnSuccess))]
        private static void ios_get_notification_quiet_hours_proxy_on_success(string startTime, int spanMinutes,
                                                                              RCIMPushNotificationQuietHoursLevel level,
                                                                              Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnSuccess", $"startTime={startTime},spanMinutes={spanMinutes},level={level}");
            var cb = NativeUtils.TakeCallback<RCIMGetNotificationQuietHoursListener>(handle);
            cb?.OnSuccess(startTime, spanMinutes, level);
        }
        [MonoPInvokeCallback(typeof(IOSGetNotificationQuietHoursProxyOnError))]
        private static void ios_get_notification_quiet_hours_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetNotificationQuietHoursListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_notification_quiet_hours_proxy toIOSGetNotificationQuietHoursProxy(
            RCIMGetNotificationQuietHoursListener listener)
        {
            ios_get_notification_quiet_hours_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_notification_quiet_hours_proxy_on_success;
            proxy.onError = ios_get_notification_quiet_hours_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWChangeConversationNotificationLevelCallback
        [MonoPInvokeCallback(typeof(IOSChangeConversationNotificationLevelProxyOnConversationNotificationLevelChanged))]
        private static void ios_change_conversation_notification_level_proxy_on_conversation_notification_level_changed(
            int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnConversationNotificationLevelChanged", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnConversationNotificationLevelChangedAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_change_conversation_notification_level_proxy toIOSChangeConversationNotificationLevelProxy(
            OnConversationNotificationLevelChangedAction listener)
        {
            ios_change_conversation_notification_level_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onConversationNotificationLevelChanged =
                ios_change_conversation_notification_level_proxy_on_conversation_notification_level_changed;
            return proxy;
        }
#endregion
#region RCIMIWGetConversationNotificationLevelCallback
        [MonoPInvokeCallback(typeof(IOSGetConversationNotificationLevelProxyOnSuccess))]
        private static void ios_get_conversation_notification_level_proxy_on_success(RCIMPushNotificationLevel t,
                                                                                     Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnSuccess", $"t={t}");
            var cb = NativeUtils.TakeCallback<RCIMGetConversationNotificationLevelListener>(handle);
            cb?.OnSuccess(t);
        }
        [MonoPInvokeCallback(typeof(IOSGetConversationNotificationLevelProxyOnError))]
        private static void ios_get_conversation_notification_level_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetConversationNotificationLevelListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_conversation_notification_level_proxy toIOSGetConversationNotificationLevelProxy(
            RCIMGetConversationNotificationLevelListener listener)
        {
            ios_get_conversation_notification_level_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_conversation_notification_level_proxy_on_success;
            proxy.onError = ios_get_conversation_notification_level_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWChangeConversationTypeNotificationLevelCallback
        [MonoPInvokeCallback(
            typeof(IOSChangeConversationTypeNotificationLevelProxyOnConversationTypeNotificationLevelChanged))]
        private static void
        ios_change_conversation_type_notification_level_proxy_on_conversation_type_notification_level_changed(int code,
                                                                                                              Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnConversationTypeNotificationLevelChanged", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnConversationTypeNotificationLevelChangedAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_change_conversation_type_notification_level_proxy
        toIOSChangeConversationTypeNotificationLevelProxy(OnConversationTypeNotificationLevelChangedAction listener)
        {
            ios_change_conversation_type_notification_level_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onConversationTypeNotificationLevelChanged =
                ios_change_conversation_type_notification_level_proxy_on_conversation_type_notification_level_changed;
            return proxy;
        }
#endregion
#region RCIMIWGetConversationTypeNotificationLevelCallback
        [MonoPInvokeCallback(typeof(IOSGetConversationTypeNotificationLevelProxyOnSuccess))]
        private static void ios_get_conversation_type_notification_level_proxy_on_success(RCIMPushNotificationLevel t,
                                                                                          Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnSuccess", $"t={t}");
            var cb = NativeUtils.TakeCallback<RCIMGetConversationTypeNotificationLevelListener>(handle);
            cb?.OnSuccess(t);
        }
        [MonoPInvokeCallback(typeof(IOSGetConversationTypeNotificationLevelProxyOnError))]
        private static void ios_get_conversation_type_notification_level_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetConversationTypeNotificationLevelListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_conversation_type_notification_level_proxy toIOSGetConversationTypeNotificationLevelProxy(
            RCIMGetConversationTypeNotificationLevelListener listener)
        {
            ios_get_conversation_type_notification_level_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_conversation_type_notification_level_proxy_on_success;
            proxy.onError = ios_get_conversation_type_notification_level_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWChangeUltraGroupDefaultNotificationLevelCallback
        [MonoPInvokeCallback(
            typeof(IOSChangeUltraGroupDefaultNotificationLevelProxyOnUltraGroupDefaultNotificationLevelChanged))]
        private static void
        ios_change_ultra_group_default_notification_level_proxy_on_ultra_group_default_notification_level_changed(
            int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnUltraGroupDefaultNotificationLevelChanged", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnUltraGroupDefaultNotificationLevelChangedAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_change_ultra_group_default_notification_level_proxy
        toIOSChangeUltraGroupDefaultNotificationLevelProxy(OnUltraGroupDefaultNotificationLevelChangedAction listener)
        {
            ios_change_ultra_group_default_notification_level_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onUltraGroupDefaultNotificationLevelChanged =
                ios_change_ultra_group_default_notification_level_proxy_on_ultra_group_default_notification_level_changed;
            return proxy;
        }
#endregion
#region RCIMIWGetUltraGroupDefaultNotificationLevelCallback
        [MonoPInvokeCallback(typeof(IOSGetUltraGroupDefaultNotificationLevelProxyOnSuccess))]
        private static void ios_get_ultra_group_default_notification_level_proxy_on_success(RCIMPushNotificationLevel t,
                                                                                            Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnSuccess", $"t={t}");
            var cb = NativeUtils.TakeCallback<RCIMGetUltraGroupDefaultNotificationLevelListener>(handle);
            cb?.OnSuccess(t);
        }
        [MonoPInvokeCallback(typeof(IOSGetUltraGroupDefaultNotificationLevelProxyOnError))]
        private static void ios_get_ultra_group_default_notification_level_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetUltraGroupDefaultNotificationLevelListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_ultra_group_default_notification_level_proxy
        toIOSGetUltraGroupDefaultNotificationLevelProxy(RCIMGetUltraGroupDefaultNotificationLevelListener listener)
        {
            ios_get_ultra_group_default_notification_level_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_ultra_group_default_notification_level_proxy_on_success;
            proxy.onError = ios_get_ultra_group_default_notification_level_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWChangeUltraGroupChannelDefaultNotificationLevelCallback
        [MonoPInvokeCallback(
            typeof(IOSChangeUltraGroupChannelDefaultNotificationLevelProxyOnUltraGroupChannelDefaultNotificationLevelChanged))]
        private static void
        ios_change_ultra_group_channel_default_notification_level_proxy_on_ultra_group_channel_default_notification_level_changed(
            int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnUltraGroupChannelDefaultNotificationLevelChanged", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnUltraGroupChannelDefaultNotificationLevelChangedAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_change_ultra_group_channel_default_notification_level_proxy
        toIOSChangeUltraGroupChannelDefaultNotificationLevelProxy(
            OnUltraGroupChannelDefaultNotificationLevelChangedAction listener)
        {
            ios_change_ultra_group_channel_default_notification_level_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onUltraGroupChannelDefaultNotificationLevelChanged =
                ios_change_ultra_group_channel_default_notification_level_proxy_on_ultra_group_channel_default_notification_level_changed;
            return proxy;
        }
#endregion
#region RCIMIWGetUltraGroupChannelDefaultNotificationLevelCallback
        [MonoPInvokeCallback(typeof(IOSGetUltraGroupChannelDefaultNotificationLevelProxyOnSuccess))]
        private static void ios_get_ultra_group_channel_default_notification_level_proxy_on_success(
            RCIMPushNotificationLevel t, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnSuccess", $"t={t}");
            var cb = NativeUtils.TakeCallback<RCIMGetUltraGroupChannelDefaultNotificationLevelListener>(handle);
            cb?.OnSuccess(t);
        }
        [MonoPInvokeCallback(typeof(IOSGetUltraGroupChannelDefaultNotificationLevelProxyOnError))]
        private static void ios_get_ultra_group_channel_default_notification_level_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetUltraGroupChannelDefaultNotificationLevelListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_ultra_group_channel_default_notification_level_proxy
        toIOSGetUltraGroupChannelDefaultNotificationLevelProxy(
            RCIMGetUltraGroupChannelDefaultNotificationLevelListener listener)
        {
            ios_get_ultra_group_channel_default_notification_level_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_ultra_group_channel_default_notification_level_proxy_on_success;
            proxy.onError = ios_get_ultra_group_channel_default_notification_level_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWChangePushContentShowStatusCallback
        [MonoPInvokeCallback(typeof(IOSChangePushContentShowStatusProxyOnPushContentShowStatusChanged))]
        private static void ios_change_push_content_show_status_proxy_on_push_content_show_status_changed(int code,
                                                                                                          Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnPushContentShowStatusChanged", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnPushContentShowStatusChangedAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_change_push_content_show_status_proxy toIOSChangePushContentShowStatusProxy(
            OnPushContentShowStatusChangedAction listener)
        {
            ios_change_push_content_show_status_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onPushContentShowStatusChanged =
                ios_change_push_content_show_status_proxy_on_push_content_show_status_changed;
            return proxy;
        }
#endregion
#region RCIMIWChangePushLanguageCallback
        [MonoPInvokeCallback(typeof(IOSChangePushLanguageProxyOnPushLanguageChanged))]
        private static void ios_change_push_language_proxy_on_push_language_changed(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnPushLanguageChanged", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnPushLanguageChangedAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_change_push_language_proxy toIOSChangePushLanguageProxy(OnPushLanguageChangedAction listener)
        {
            ios_change_push_language_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onPushLanguageChanged = ios_change_push_language_proxy_on_push_language_changed;
            return proxy;
        }
#endregion
#region RCIMIWChangePushReceiveStatusCallback
        [MonoPInvokeCallback(typeof(IOSChangePushReceiveStatusProxyOnPushReceiveStatusChanged))]
        private static void ios_change_push_receive_status_proxy_on_push_receive_status_changed(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnPushReceiveStatusChanged", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnPushReceiveStatusChangedAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_change_push_receive_status_proxy toIOSChangePushReceiveStatusProxy(
            OnPushReceiveStatusChangedAction listener)
        {
            ios_change_push_receive_status_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onPushReceiveStatusChanged = ios_change_push_receive_status_proxy_on_push_receive_status_changed;
            return proxy;
        }
#endregion
#region RCIMIWSendGroupMessageToDesignatedUsersCallback
        [MonoPInvokeCallback(typeof(IOSSendGroupMessageToDesignatedUsersProxyOnMessageSaved))]
        private static void ios_send_group_message_to_designated_users_proxy_on_message_saved(IntPtr message, Int64 handle)
        {
            RCIMMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnMessageSaved", $"message_cls={message_cls}");
            var cb = NativeUtils.TakeCallback<RCIMSendGroupMessageToDesignatedUsersListener>(handle);
            cb?.OnMessageSaved(message_cls);
        }
        [MonoPInvokeCallback(typeof(IOSSendGroupMessageToDesignatedUsersProxyOnMessageSent))]
        private static void ios_send_group_message_to_designated_users_proxy_on_message_sent(int code, IntPtr message,
                                                                                             Int64 handle)
        {
            RCIMMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnMessageSent", $"code={code},message_cls={message_cls}");
            var cb = NativeUtils.TakeCallback<RCIMSendGroupMessageToDesignatedUsersListener>(handle);
            cb?.OnMessageSent(code, message_cls);
        }
        internal static ios_send_group_message_to_designated_users_proxy toIOSSendGroupMessageToDesignatedUsersProxy(
            RCIMSendGroupMessageToDesignatedUsersListener listener)
        {
            ios_send_group_message_to_designated_users_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onMessageSaved = ios_send_group_message_to_designated_users_proxy_on_message_saved;
            proxy.onMessageSent = ios_send_group_message_to_designated_users_proxy_on_message_sent;
            return proxy;
        }
#endregion
#region RCIMIWGetMessageCountCallback
        [MonoPInvokeCallback(typeof(IOSGetMessageCountProxyOnSuccess))]
        private static void ios_get_message_count_proxy_on_success(int t, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnSuccess", $"t={t}");
            var cb = NativeUtils.TakeCallback<RCIMGetMessageCountListener>(handle);
            cb?.OnSuccess(t);
        }
        [MonoPInvokeCallback(typeof(IOSGetMessageCountProxyOnError))]
        private static void ios_get_message_count_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetMessageCountListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_message_count_proxy toIOSGetMessageCountProxy(RCIMGetMessageCountListener listener)
        {
            ios_get_message_count_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_message_count_proxy_on_success;
            proxy.onError = ios_get_message_count_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWGetTopConversationsCallback
        [MonoPInvokeCallback(typeof(IOSGetTopConversationsProxyOnSuccess))]
        private static void ios_get_top_conversations_proxy_on_success(ref ios_c_list t, Int64 handle)
        {
            List<RCIMConversation> t_list = new List<RCIMConversation>();
            im_conversation[] t_clist = NativeUtils.GetObjectListByStruct<im_conversation>(ref t);
            for (int i = 0; i < t_clist.Length; i++)
            {
                t_list.Add(NativeConvert.fromConversation(ref t_clist[i]));
            }
            RCUnityLogger.getInstance().log("OnSuccess", $"t_list={t_list}");
            var cb = NativeUtils.TakeCallback<RCIMGetTopConversationsListener>(handle);
            cb?.OnSuccess(t_list);
        }
        [MonoPInvokeCallback(typeof(IOSGetTopConversationsProxyOnError))]
        private static void ios_get_top_conversations_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetTopConversationsListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_top_conversations_proxy toIOSGetTopConversationsProxy(
            RCIMGetTopConversationsListener listener)
        {
            ios_get_top_conversations_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_top_conversations_proxy_on_success;
            proxy.onError = ios_get_top_conversations_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWSyncUltraGroupReadStatusCallback
        [MonoPInvokeCallback(typeof(IOSSyncUltraGroupReadStatusProxyOnUltraGroupReadStatusSynced))]
        private static void ios_sync_ultra_group_read_status_proxy_on_ultra_group_read_status_synced(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnUltraGroupReadStatusSynced", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnUltraGroupReadStatusSyncedAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_sync_ultra_group_read_status_proxy toIOSSyncUltraGroupReadStatusProxy(
            OnUltraGroupReadStatusSyncedAction listener)
        {
            ios_sync_ultra_group_read_status_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onUltraGroupReadStatusSynced = ios_sync_ultra_group_read_status_proxy_on_ultra_group_read_status_synced;
            return proxy;
        }
#endregion
#region RCIMIWGetConversationsForAllChannelCallback
        [MonoPInvokeCallback(typeof(IOSGetConversationsForAllChannelProxyOnSuccess))]
        private static void ios_get_conversations_for_all_channel_proxy_on_success(ref ios_c_list t, Int64 handle)
        {
            List<RCIMConversation> t_list = new List<RCIMConversation>();
            im_conversation[] t_clist = NativeUtils.GetObjectListByStruct<im_conversation>(ref t);
            for (int i = 0; i < t_clist.Length; i++)
            {
                t_list.Add(NativeConvert.fromConversation(ref t_clist[i]));
            }
            RCUnityLogger.getInstance().log("OnSuccess", $"t_list={t_list}");
            var cb = NativeUtils.TakeCallback<RCIMGetConversationsForAllChannelListener>(handle);
            cb?.OnSuccess(t_list);
        }
        [MonoPInvokeCallback(typeof(IOSGetConversationsForAllChannelProxyOnError))]
        private static void ios_get_conversations_for_all_channel_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetConversationsForAllChannelListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_conversations_for_all_channel_proxy toIOSGetConversationsForAllChannelProxy(
            RCIMGetConversationsForAllChannelListener listener)
        {
            ios_get_conversations_for_all_channel_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_conversations_for_all_channel_proxy_on_success;
            proxy.onError = ios_get_conversations_for_all_channel_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWModifyUltraGroupMessageCallback
        [MonoPInvokeCallback(typeof(IOSModifyUltraGroupMessageProxyOnUltraGroupMessageModified))]
        private static void ios_modify_ultra_group_message_proxy_on_ultra_group_message_modified(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnUltraGroupMessageModified", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnUltraGroupMessageModifiedAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_modify_ultra_group_message_proxy toIOSModifyUltraGroupMessageProxy(
            OnUltraGroupMessageModifiedAction listener)
        {
            ios_modify_ultra_group_message_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onUltraGroupMessageModified = ios_modify_ultra_group_message_proxy_on_ultra_group_message_modified;
            return proxy;
        }
#endregion
#region RCIMIWRecallUltraGroupMessageCallback
        [MonoPInvokeCallback(typeof(IOSRecallUltraGroupMessageProxyOnUltraGroupMessageRecalled))]
        private static void ios_recall_ultra_group_message_proxy_on_ultra_group_message_recalled(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnUltraGroupMessageRecalled", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnUltraGroupMessageRecalledAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_recall_ultra_group_message_proxy toIOSRecallUltraGroupMessageProxy(
            OnUltraGroupMessageRecalledAction listener)
        {
            ios_recall_ultra_group_message_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onUltraGroupMessageRecalled = ios_recall_ultra_group_message_proxy_on_ultra_group_message_recalled;
            return proxy;
        }
#endregion
#region RCIMIWClearUltraGroupMessagesCallback
        [MonoPInvokeCallback(typeof(IOSClearUltraGroupMessagesProxyOnUltraGroupMessagesCleared))]
        private static void ios_clear_ultra_group_messages_proxy_on_ultra_group_messages_cleared(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnUltraGroupMessagesCleared", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnUltraGroupMessagesClearedAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_clear_ultra_group_messages_proxy toIOSClearUltraGroupMessagesProxy(
            OnUltraGroupMessagesClearedAction listener)
        {
            ios_clear_ultra_group_messages_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onUltraGroupMessagesCleared = ios_clear_ultra_group_messages_proxy_on_ultra_group_messages_cleared;
            return proxy;
        }
#endregion
#region RCIMIWSendUltraGroupTypingStatusCallback
        [MonoPInvokeCallback(typeof(IOSSendUltraGroupTypingStatusProxyOnUltraGroupTypingStatusSent))]
        private static void ios_send_ultra_group_typing_status_proxy_on_ultra_group_typing_status_sent(int code,
                                                                                                       Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnUltraGroupTypingStatusSent", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnUltraGroupTypingStatusSentAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_send_ultra_group_typing_status_proxy toIOSSendUltraGroupTypingStatusProxy(
            OnUltraGroupTypingStatusSentAction listener)
        {
            ios_send_ultra_group_typing_status_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onUltraGroupTypingStatusSent = ios_send_ultra_group_typing_status_proxy_on_ultra_group_typing_status_sent;
            return proxy;
        }
#endregion
#region RCIMIWClearUltraGroupMessagesForAllChannelCallback
        [MonoPInvokeCallback(typeof(IOSClearUltraGroupMessagesForAllChannelProxyOnUltraGroupMessagesClearedForAllChannel))]
        private static void
        ios_clear_ultra_group_messages_for_all_channel_proxy_on_ultra_group_messages_cleared_for_all_channel(int code,
                                                                                                             Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnUltraGroupMessagesClearedForAllChannel", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnUltraGroupMessagesClearedForAllChannelAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_clear_ultra_group_messages_for_all_channel_proxy toIOSClearUltraGroupMessagesForAllChannelProxy(
            OnUltraGroupMessagesClearedForAllChannelAction listener)
        {
            ios_clear_ultra_group_messages_for_all_channel_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onUltraGroupMessagesClearedForAllChannel =
                ios_clear_ultra_group_messages_for_all_channel_proxy_on_ultra_group_messages_cleared_for_all_channel;
            return proxy;
        }
#endregion
#region RCIMIWGetBatchRemoteUltraGroupMessagesCallback
        [MonoPInvokeCallback(typeof(IOSGetBatchRemoteUltraGroupMessagesProxyOnSuccess))]
        private static void ios_get_batch_remote_ultra_group_messages_proxy_on_success(ref ios_c_list matchedMessages,
                                                                                       ref ios_c_list notMatchedMessages,
                                                                                       Int64 handle)
        {
            List<RCIMMessage> matchedMessages_list = new List<RCIMMessage>();
            ios_class_warpper[] matchedMessages_clist =
                NativeUtils.GetObjectListByStruct<ios_class_warpper>(ref matchedMessages);
            for (int i = 0; i < matchedMessages_clist.Length; i++)
            {
                matchedMessages_list.Add(NativeConvert.fromMessageWapper(ref matchedMessages_clist[i]));
            }
            List<RCIMMessage> notMatchedMessages_list = new List<RCIMMessage>();
            ios_class_warpper[] notMatchedMessages_clist =
                NativeUtils.GetObjectListByStruct<ios_class_warpper>(ref notMatchedMessages);
            for (int i = 0; i < notMatchedMessages_clist.Length; i++)
            {
                notMatchedMessages_list.Add(NativeConvert.fromMessageWapper(ref notMatchedMessages_clist[i]));
            }
            RCUnityLogger.getInstance().log(
                "OnSuccess",
                $"matchedMessages_list={matchedMessages_list},notMatchedMessages_list={notMatchedMessages_list}");
            var cb = NativeUtils.TakeCallback<RCIMGetBatchRemoteUltraGroupMessagesListener>(handle);
            cb?.OnSuccess(matchedMessages_list, notMatchedMessages_list);
        }
        [MonoPInvokeCallback(typeof(IOSGetBatchRemoteUltraGroupMessagesProxyOnError))]
        private static void ios_get_batch_remote_ultra_group_messages_proxy_on_error(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnError", $"code={code}");
            var cb = NativeUtils.TakeCallback<RCIMGetBatchRemoteUltraGroupMessagesListener>(handle);
            cb?.OnError(code);
        }
        internal static ios_get_batch_remote_ultra_group_messages_proxy toIOSGetBatchRemoteUltraGroupMessagesProxy(
            RCIMGetBatchRemoteUltraGroupMessagesListener listener)
        {
            ios_get_batch_remote_ultra_group_messages_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onSuccess = ios_get_batch_remote_ultra_group_messages_proxy_on_success;
            proxy.onError = ios_get_batch_remote_ultra_group_messages_proxy_on_error;
            return proxy;
        }
#endregion
#region RCIMIWUpdateUltraGroupMessageExpansionCallback
        [MonoPInvokeCallback(typeof(IOSUpdateUltraGroupMessageExpansionProxyOnUltraGroupMessageExpansionUpdated))]
        private static void ios_update_ultra_group_message_expansion_proxy_on_ultra_group_message_expansion_updated(
            int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnUltraGroupMessageExpansionUpdated", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnUltraGroupMessageExpansionUpdatedAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_update_ultra_group_message_expansion_proxy toIOSUpdateUltraGroupMessageExpansionProxy(
            OnUltraGroupMessageExpansionUpdatedAction listener)
        {
            ios_update_ultra_group_message_expansion_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onUltraGroupMessageExpansionUpdated =
                ios_update_ultra_group_message_expansion_proxy_on_ultra_group_message_expansion_updated;
            return proxy;
        }
#endregion
#region RCIMIWRemoveUltraGroupMessageExpansionForKeysCallback
        [MonoPInvokeCallback(
            typeof(IOSRemoveUltraGroupMessageExpansionForKeysProxyOnUltraGroupMessageExpansionForKeysRemoved))]
        private static void
        ios_remove_ultra_group_message_expansion_for_keys_proxy_on_ultra_group_message_expansion_for_keys_removed(
            int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnUltraGroupMessageExpansionForKeysRemoved", $"code={code}");
            var cb = NativeUtils.TakeCallback<OnUltraGroupMessageExpansionForKeysRemovedAction>(handle);
            cb?.Invoke(code);
        }
        internal static ios_remove_ultra_group_message_expansion_for_keys_proxy
        toIOSRemoveUltraGroupMessageExpansionForKeysProxy(OnUltraGroupMessageExpansionForKeysRemovedAction listener)
        {
            ios_remove_ultra_group_message_expansion_for_keys_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onUltraGroupMessageExpansionForKeysRemoved =
                ios_remove_ultra_group_message_expansion_for_keys_proxy_on_ultra_group_message_expansion_for_keys_removed;
            return proxy;
        }
#endregion
    
#region RCIMIWConnectCallback
        [MonoPInvokeCallback(typeof(IOSConnectProxyOnConnected))]
        private static void ios_connect_proxy_on_connected(int code, string userId, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnConnected", $"code={code},userId={userId}");
            var cb = NativeUtils.TakeCallback<RCIMConnectListener>(handle);
            cb?.OnConnected(code, userId);
        }
        [MonoPInvokeCallback(typeof(IOSConnectProxyOnDatabaseOpened))]
        private static void ios_connect_proxy_on_database_opened(int code, Int64 handle)
        {
            RCUnityLogger.getInstance().log("OnDatabaseOpened", $"code={code}");
            var cb = NativeUtils.GetCallback<RCIMConnectListener>(handle);
            cb?.OnDatabaseOpened(code);
        }
        internal static ios_connect_proxy toIOSConnectProxy(RCIMConnectListener listener)
        {
            ios_connect_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onConnected = ios_connect_proxy_on_connected;
            proxy.onDatabaseOpened = ios_connect_proxy_on_database_opened;
            return proxy;
        }
#endregion
    
#region RCIMIWSendMessageCallback
        [MonoPInvokeCallback(typeof(IOSSendMessageProxyOnMessageSaved))]
        private static void ios_send_message_proxy_on_message_saved(IntPtr message, Int64 handle)
        {
            RCIMMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnMessageSaved", $"message_cls={message_cls}");
            var cb = NativeUtils.GetCallback<RCIMSendMessageListener>(handle);
            cb?.OnMessageSaved(message_cls);
        }
        [MonoPInvokeCallback(typeof(IOSSendMessageProxyOnMessageSent))]
        private static void ios_send_message_proxy_on_message_sent(int code, IntPtr message, Int64 handle)
        {
            RCIMMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnMessageSent", $"code={code},message_cls={message_cls}");
            var cb = NativeUtils.TakeCallback<RCIMSendMessageListener>(handle);
            cb?.OnMessageSent(code, message_cls);
        }
        internal static ios_send_message_proxy toIOSSendMessageProxy(RCIMSendMessageListener listener)
        {
            ios_send_message_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onMessageSaved = ios_send_message_proxy_on_message_saved;
            proxy.onMessageSent = ios_send_message_proxy_on_message_sent;
            return proxy;
        }
#endregion
    
#region RCIMIWSendMediaMessageListener
        [MonoPInvokeCallback(typeof(IOSSendMediaMessageProxyOnMediaMessageSaved))]
        private static void ios_send_media_message_proxy_on_media_message_saved(IntPtr message, Int64 handle)
        {
            RCIMMediaMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMediaMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnMediaMessageSaved", $"message_cls={message_cls}");
            var cb = NativeUtils.GetCallback<RCIMSendMediaMessageListener>(handle);
            cb?.OnMediaMessageSaved(message_cls);
        }
        [MonoPInvokeCallback(typeof(IOSSendMediaMessageProxyOnMediaMessageSending))]
        private static void ios_send_media_message_proxy_on_media_message_sending(IntPtr message, int progress,
                                                                                  Int64 handle)
        {
            RCIMMediaMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMediaMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnMediaMessageSending", $"message_cls={message_cls},progress={progress}");
            var cb = NativeUtils.GetCallback<RCIMSendMediaMessageListener>(handle);
            cb?.OnMediaMessageSending(message_cls, progress);
        }
        [MonoPInvokeCallback(typeof(IOSSendMediaMessageProxyOnSendingMediaMessageCanceled))]
        private static void ios_send_media_message_proxy_on_sending_media_message_canceled(IntPtr message, Int64 handle)
        {
            RCIMMediaMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMediaMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnSendingMediaMessageCanceled", $"message_cls={message_cls}");
            var cb = NativeUtils.TakeCallback<RCIMSendMediaMessageListener>(handle);
            cb?.OnSendingMediaMessageCanceled(message_cls);
        }
        [MonoPInvokeCallback(typeof(IOSSendMediaMessageProxyOnMediaMessageSent))]
        private static void ios_send_media_message_proxy_on_media_message_sent(int code, IntPtr message, Int64 handle)
        {
            RCIMMediaMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMediaMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnMediaMessageSent", $"code={code},message_cls={message_cls}");
            var cb = NativeUtils.TakeCallback<RCIMSendMediaMessageListener>(handle);
            cb?.OnMediaMessageSent(code, message_cls);
        }
        internal static ios_send_media_message_proxy toIOSSendMediaMessageProxy(RCIMSendMediaMessageListener listener)
        {
            ios_send_media_message_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onMediaMessageSaved = ios_send_media_message_proxy_on_media_message_saved;
            proxy.onMediaMessageSending = ios_send_media_message_proxy_on_media_message_sending;
            proxy.onSendingMediaMessageCanceled = ios_send_media_message_proxy_on_sending_media_message_canceled;
            proxy.onMediaMessageSent = ios_send_media_message_proxy_on_media_message_sent;
            return proxy;
        }
#endregion
    
#region RCIMIWDownloadMediaMessageListener
        [MonoPInvokeCallback(typeof(IOSDownloadMediaMessageProxyOnMediaMessageDownloading))]
        private static void ios_download_media_message_proxy_on_media_message_downloading(IntPtr message, int progress,
                                                                                          Int64 handle)
        {
            RCIMMediaMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMediaMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnMediaMessageDownloading", $"message_cls={message_cls},progress={progress}");
            var cb = NativeUtils.GetCallback<RCIMDownloadMediaMessageListener>(handle);
            cb?.OnMediaMessageDownloading(message_cls, progress);
        }
        [MonoPInvokeCallback(typeof(IOSDownloadMediaMessageProxyOnDownloadingMediaMessageCanceled))]
        private static void ios_download_media_message_proxy_on_downloading_media_message_canceled(IntPtr message,
                                                                                                   Int64 handle)
        {
            RCIMMediaMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMediaMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnDownloadingMediaMessageCanceled", $"message_cls={message_cls}");
            var cb = NativeUtils.TakeCallback<RCIMDownloadMediaMessageListener>(handle);
            cb?.OnDownloadingMediaMessageCanceled(message_cls);
        }
        [MonoPInvokeCallback(typeof(IOSDownloadMediaMessageProxyOnMediaMessageDownloaded))]
        private static void ios_download_media_message_proxy_on_media_message_downloaded(int code, IntPtr message,
                                                                                         Int64 handle)
        {
            RCIMMediaMessage message_cls = null;
            if (message != IntPtr.Zero)
            {
                var cmessage = NativeUtils.GetStructByPtr<ios_class_warpper>(message);
                message_cls = NativeConvert.fromMediaMessageWapper(ref cmessage);
            }
            RCUnityLogger.getInstance().log("OnMediaMessageDownloaded", $"code={code},message_cls={message_cls}");
            var cb = NativeUtils.TakeCallback<RCIMDownloadMediaMessageListener>(handle);
            cb?.OnMediaMessageDownloaded(code, message_cls);
        }
        internal static ios_download_media_message_proxy toIOSDownloadMediaMessageProxy(
            RCIMDownloadMediaMessageListener listener)
        {
            ios_download_media_message_proxy proxy;
            proxy.handle = NativeUtils.AddCallback(listener);
            proxy.onMediaMessageDownloading = ios_download_media_message_proxy_on_media_message_downloading;
            proxy.onDownloadingMediaMessageCanceled =
                ios_download_media_message_proxy_on_downloading_media_message_canceled;
            proxy.onMediaMessageDownloaded = ios_download_media_message_proxy_on_media_message_downloaded;
            return proxy;
        }
#endregion
    }
}
#endif
