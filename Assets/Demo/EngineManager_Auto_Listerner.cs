using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cn_rongcloud_im_unity;
using Newtonsoft.Json;

/// <summary>
/// 自动生成的监听
/// </summary>
namespace cn_rongcloud_im_wapper_unity_example
{
    public partial class EngineManager
    {
        public void setEngineListener()
        {
            if (engine == null)
            {
                return;
            }
    
            engine.OnMessageReceived = delegate(RCIMMessage message, int left, bool offline, bool hasPackage)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onMessageReceived";
                result["message"] = message;
                result["left"] = left;
                result["offline"] = offline;
                result["hasPackage"] = hasPackage;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onMessageReceived_call
            engine.OnMessageReceived = delegate(RCIMMessage message,int left,bool offline,bool hasPackage)
            {
                //...
            };
            //callback_onMessageReceived_call
            */
    
            engine.OnConnectionStatusChanged = delegate(RCIMConnectionStatus status)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onConnectionStatusChanged";
                result["status"] = status;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onConnectionStatusChanged_call
            engine.OnConnectionStatusChanged = delegate(RCIMConnectionStatus status)
            {
                //...
            };
            //callback_onConnectionStatusChanged_call
            */
    
            engine.OnConversationTopStatusSynced =
                delegate(RCIMConversationType type, string targetId, string channelId, bool top)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onConversationTopStatusSynced";
                result["type"] = type;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["top"] = top;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onConversationTopStatusSynced_call
            engine.OnConversationTopStatusSynced = delegate(RCIMConversationType type,string targetId,string channelId,bool
            top)
            {
                //...
            };
            //callback_onConversationTopStatusSynced_call
            */
    
            engine.OnConversationNotificationLevelSynced =
                delegate(RCIMConversationType type, string targetId, string channelId, RCIMPushNotificationLevel level)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onConversationNotificationLevelSynced";
                result["type"] = type;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["level"] = level;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onConversationNotificationLevelSynced_call
            engine.OnConversationNotificationLevelSynced = delegate(RCIMConversationType type,string targetId,string
            channelId,RCIMPushNotificationLevel level)
            {
                //...
            };
            //callback_onConversationNotificationLevelSynced_call
            */
    
            engine.OnRemoteMessageRecalled = delegate(RCIMMessage message)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onRemoteMessageRecalled";
                result["message"] = message;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onRemoteMessageRecalled_call
            engine.OnRemoteMessageRecalled = delegate(RCIMMessage message)
            {
                //...
            };
            //callback_onRemoteMessageRecalled_call
            */
    
            engine.OnPrivateReadReceiptReceived = delegate(string targetId, string channelId, long timestamp)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onPrivateReadReceiptReceived";
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["timestamp"] = timestamp;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onPrivateReadReceiptReceived_call
            engine.OnPrivateReadReceiptReceived = delegate(string targetId,string channelId,long timestamp)
            {
                //...
            };
            //callback_onPrivateReadReceiptReceived_call
            */
    
            engine.OnRemoteMessageExpansionUpdated = delegate(Dictionary<string, string> expansion, RCIMMessage message)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onRemoteMessageExpansionUpdated";
                result["expansion"] = expansion;
                result["message"] = message;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onRemoteMessageExpansionUpdated_call
            engine.OnRemoteMessageExpansionUpdated = delegate(Dictionary<string, string> expansion,RCIMMessage message)
            {
                //...
            };
            //callback_onRemoteMessageExpansionUpdated_call
            */
    
            engine.OnRemoteMessageExpansionForKeyRemoved = delegate(RCIMMessage message, List<string> keys)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onRemoteMessageExpansionForKeyRemoved";
                result["message"] = message;
                result["keys"] = keys;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onRemoteMessageExpansionForKeyRemoved_call
            engine.OnRemoteMessageExpansionForKeyRemoved = delegate(RCIMMessage message,List<string> keys)
            {
                //...
            };
            //callback_onRemoteMessageExpansionForKeyRemoved_call
            */
    
            engine.OnChatRoomMemberChanged = delegate(string targetId, List<RCIMChatRoomMemberAction> actions)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onChatRoomMemberChanged";
                result["targetId"] = targetId;
                result["actions"] = actions;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onChatRoomMemberChanged_call
            engine.OnChatRoomMemberChanged = delegate(string targetId,List<RCIMChatRoomMemberAction> actions)
            {
                //...
            };
            //callback_onChatRoomMemberChanged_call
            */
    
            engine.OnTypingStatusChanged = delegate(RCIMConversationType type, string targetId, string channelId,
                                                    List<RCIMTypingStatus> userTypingStatus)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onTypingStatusChanged";
                result["type"] = type;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["userTypingStatus"] = userTypingStatus;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onTypingStatusChanged_call
            engine.OnTypingStatusChanged = delegate(RCIMConversationType type,string targetId,string
            channelId,List<RCIMTypingStatus> userTypingStatus)
            {
                //...
            };
            //callback_onTypingStatusChanged_call
            */
    
            engine.OnConversationReadStatusSyncMessageReceived =
                delegate(RCIMConversationType type, string targetId, long timestamp)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onConversationReadStatusSyncMessageReceived";
                result["type"] = type;
                result["targetId"] = targetId;
                result["timestamp"] = timestamp;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onConversationReadStatusSyncMessageReceived_call
            engine.OnConversationReadStatusSyncMessageReceived = delegate(RCIMConversationType type,string targetId,long
            timestamp)
            {
                //...
            };
            //callback_onConversationReadStatusSyncMessageReceived_call
            */
    
            engine.OnChatRoomEntriesSynced = delegate(string roomId)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onChatRoomEntriesSynced";
                result["roomId"] = roomId;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onChatRoomEntriesSynced_call
            engine.OnChatRoomEntriesSynced = delegate(string roomId)
            {
                //...
            };
            //callback_onChatRoomEntriesSynced_call
            */
    
            engine.OnChatRoomEntriesChanged =
                delegate(RCIMChatRoomEntriesOperationType operationType, string roomId, Dictionary<string, string> entries)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onChatRoomEntriesChanged";
                result["operationType"] = operationType;
                result["roomId"] = roomId;
                result["entries"] = entries;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onChatRoomEntriesChanged_call
            engine.OnChatRoomEntriesChanged = delegate(RCIMChatRoomEntriesOperationType operationType,string
            roomId,Dictionary<string, string> entries)
            {
                //...
            };
            //callback_onChatRoomEntriesChanged_call
            */
    
            engine.OnRemoteUltraGroupMessageExpansionUpdated = delegate(List<RCIMMessage> messages)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onRemoteUltraGroupMessageExpansionUpdated";
                result["messages"] = messages;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onRemoteUltraGroupMessageExpansionUpdated_call
            engine.OnRemoteUltraGroupMessageExpansionUpdated = delegate(List<RCIMMessage> messages)
            {
                //...
            };
            //callback_onRemoteUltraGroupMessageExpansionUpdated_call
            */
    
            engine.OnRemoteUltraGroupMessageModified = delegate(List<RCIMMessage> messages)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onRemoteUltraGroupMessageModified";
                result["messages"] = messages;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onRemoteUltraGroupMessageModified_call
            engine.OnRemoteUltraGroupMessageModified = delegate(List<RCIMMessage> messages)
            {
                //...
            };
            //callback_onRemoteUltraGroupMessageModified_call
            */
    
            engine.OnRemoteUltraGroupMessageRecalled = delegate(List<RCIMMessage> messages)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onRemoteUltraGroupMessageRecalled";
                result["messages"] = messages;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onRemoteUltraGroupMessageRecalled_call
            engine.OnRemoteUltraGroupMessageRecalled = delegate(List<RCIMMessage> messages)
            {
                //...
            };
            //callback_onRemoteUltraGroupMessageRecalled_call
            */
    
            engine.OnUltraGroupReadTimeReceived = delegate(string targetId, string channelId, long timestamp)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onUltraGroupReadTimeReceived";
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["timestamp"] = timestamp;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onUltraGroupReadTimeReceived_call
            engine.OnUltraGroupReadTimeReceived = delegate(string targetId,string channelId,long timestamp)
            {
                //...
            };
            //callback_onUltraGroupReadTimeReceived_call
            */
    
            engine.OnUltraGroupTypingStatusChanged = delegate(List<RCIMUltraGroupTypingStatusInfo> info)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onUltraGroupTypingStatusChanged";
                result["info"] = info;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onUltraGroupTypingStatusChanged_call
            engine.OnUltraGroupTypingStatusChanged = delegate(List<RCIMUltraGroupTypingStatusInfo> info)
            {
                //...
            };
            //callback_onUltraGroupTypingStatusChanged_call
            */
    
            engine.OnMessageBlocked = delegate(RCIMBlockedMessageInfo info)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onMessageBlocked";
                result["info"] = info;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onMessageBlocked_call
            engine.OnMessageBlocked = delegate(RCIMBlockedMessageInfo info)
            {
                //...
            };
            //callback_onMessageBlocked_call
            */
    
            engine.OnChatRoomStatusChanged = delegate(string targetId, RCIMChatRoomStatus status)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onChatRoomStatusChanged";
                result["targetId"] = targetId;
                result["status"] = status;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onChatRoomStatusChanged_call
            engine.OnChatRoomStatusChanged = delegate(string targetId,RCIMChatRoomStatus status)
            {
                //...
            };
            //callback_onChatRoomStatusChanged_call
            */
    
            engine.OnGroupMessageReadReceiptRequestReceived = delegate(string targetId, string messageUId)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onGroupMessageReadReceiptRequestReceived";
                result["targetId"] = targetId;
                result["messageUId"] = messageUId;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onGroupMessageReadReceiptRequestReceived_call
            engine.OnGroupMessageReadReceiptRequestReceived = delegate(string targetId,string messageUId)
            {
                //...
            };
            //callback_onGroupMessageReadReceiptRequestReceived_call
            */
    
            engine.OnGroupMessageReadReceiptResponseReceived =
                delegate(string targetId, string messageUId, Dictionary<string, long> respondUserIds)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onGroupMessageReadReceiptResponseReceived";
                result["targetId"] = targetId;
                result["messageUId"] = messageUId;
                result["respondUserIds"] = respondUserIds;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onGroupMessageReadReceiptResponseReceived_call
            engine.OnGroupMessageReadReceiptResponseReceived = delegate(string targetId,string messageUId,Dictionary<string,
            long> respondUserIds)
            {
                //...
            };
            //callback_onGroupMessageReadReceiptResponseReceived_call
            */
    
            engine.OnConnected = delegate(int code, string userId)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onConnected";
                result["code"] = code;
                result["userId"] = userId;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onConnected_call
            engine.OnConnected = delegate(int code,string userId)
            {
                //...
            };
            //callback_onConnected_call
            */
    
            engine.OnDatabaseOpened = delegate(int code)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onDatabaseOpened";
                result["code"] = code;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onDatabaseOpened_call
            engine.OnDatabaseOpened = delegate(int code)
            {
                //...
            };
            //callback_onDatabaseOpened_call
            */
    
            engine.OnConversationLoaded = delegate(int code, RCIMConversationType type, string targetId, string channelId,
                                                   RCIMConversation conversation)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onConversationLoaded";
                result["code"] = code;
                result["type"] = type;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["conversation"] = conversation;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onConversationLoaded_call
            engine.OnConversationLoaded = delegate(int code,RCIMConversationType type,string targetId,string
            channelId,RCIMConversation conversation)
            {
                //...
            };
            //callback_onConversationLoaded_call
            */
    
            engine.OnConversationsLoaded =
                delegate(int code, List<RCIMConversationType> conversationTypes, string channelId, long startTime,
                         int count, List<RCIMConversation> conversations)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onConversationsLoaded";
                result["code"] = code;
                result["conversationTypes"] = conversationTypes;
                result["channelId"] = channelId;
                result["startTime"] = startTime;
                result["count"] = count;
                result["conversations"] = conversations;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onConversationsLoaded_call
            engine.OnConversationsLoaded = delegate(int code,List<RCIMConversationType> conversationTypes,string
            channelId,long startTime,int count,List<RCIMConversation> conversations)
            {
                //...
            };
            //callback_onConversationsLoaded_call
            */
    
            engine.OnConversationRemoved = delegate(int code, RCIMConversationType type, string targetId, string channelId)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onConversationRemoved";
                result["code"] = code;
                result["type"] = type;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onConversationRemoved_call
            engine.OnConversationRemoved = delegate(int code,RCIMConversationType type,string targetId,string channelId)
            {
                //...
            };
            //callback_onConversationRemoved_call
            */
    
            engine.OnConversationsRemoved =
                delegate(int code, List<RCIMConversationType> conversationTypes, string channelId)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onConversationsRemoved";
                result["code"] = code;
                result["conversationTypes"] = conversationTypes;
                result["channelId"] = channelId;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onConversationsRemoved_call
            engine.OnConversationsRemoved = delegate(int code,List<RCIMConversationType> conversationTypes,string channelId)
            {
                //...
            };
            //callback_onConversationsRemoved_call
            */
    
            engine.OnTotalUnreadCountLoaded = delegate(int code, string channelId, int count)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onTotalUnreadCountLoaded";
                result["code"] = code;
                result["channelId"] = channelId;
                result["count"] = count;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onTotalUnreadCountLoaded_call
            engine.OnTotalUnreadCountLoaded = delegate(int code,string channelId,int count)
            {
                //...
            };
            //callback_onTotalUnreadCountLoaded_call
            */
    
            engine.OnUnreadCountLoaded =
                delegate(int code, RCIMConversationType type, string targetId, string channelId, int count)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onUnreadCountLoaded";
                result["code"] = code;
                result["type"] = type;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["count"] = count;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onUnreadCountLoaded_call
            engine.OnUnreadCountLoaded = delegate(int code,RCIMConversationType type,string targetId,string channelId,int
            count)
            {
                //...
            };
            //callback_onUnreadCountLoaded_call
            */
    
            engine.OnUnreadCountByConversationTypesLoaded =
                delegate(int code, List<RCIMConversationType> conversationTypes, string channelId, bool contain, int count)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onUnreadCountByConversationTypesLoaded";
                result["code"] = code;
                result["conversationTypes"] = conversationTypes;
                result["channelId"] = channelId;
                result["contain"] = contain;
                result["count"] = count;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onUnreadCountByConversationTypesLoaded_call
            engine.OnUnreadCountByConversationTypesLoaded = delegate(int code,List<RCIMConversationType>
            conversationTypes,string channelId,bool contain,int count)
            {
                //...
            };
            //callback_onUnreadCountByConversationTypesLoaded_call
            */
    
            engine.OnUnreadMentionedCountLoaded =
                delegate(int code, RCIMConversationType type, string targetId, string channelId, int count)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onUnreadMentionedCountLoaded";
                result["code"] = code;
                result["type"] = type;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["count"] = count;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onUnreadMentionedCountLoaded_call
            engine.OnUnreadMentionedCountLoaded = delegate(int code,RCIMConversationType type,string targetId,string
            channelId,int count)
            {
                //...
            };
            //callback_onUnreadMentionedCountLoaded_call
            */
    
            engine.OnUltraGroupAllUnreadCountLoaded = delegate(int code, int count)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onUltraGroupAllUnreadCountLoaded";
                result["code"] = code;
                result["count"] = count;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onUltraGroupAllUnreadCountLoaded_call
            engine.OnUltraGroupAllUnreadCountLoaded = delegate(int code,int count)
            {
                //...
            };
            //callback_onUltraGroupAllUnreadCountLoaded_call
            */
    
            engine.OnUltraGroupAllUnreadMentionedCountLoaded = delegate(int code, int count)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onUltraGroupAllUnreadMentionedCountLoaded";
                result["code"] = code;
                result["count"] = count;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onUltraGroupAllUnreadMentionedCountLoaded_call
            engine.OnUltraGroupAllUnreadMentionedCountLoaded = delegate(int code,int count)
            {
                //...
            };
            //callback_onUltraGroupAllUnreadMentionedCountLoaded_call
            */
    
            engine.OnUltraGroupConversationsSynced = delegate()
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onUltraGroupConversationsSynced";
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onUltraGroupConversationsSynced_call
            engine.OnUltraGroupConversationsSynced = delegate()
            {
                //...
            };
            //callback_onUltraGroupConversationsSynced_call
            */
    
            engine.OnUnreadCountCleared =
                delegate(int code, RCIMConversationType type, string targetId, string channelId, long timestamp)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onUnreadCountCleared";
                result["code"] = code;
                result["type"] = type;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["timestamp"] = timestamp;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onUnreadCountCleared_call
            engine.OnUnreadCountCleared = delegate(int code,RCIMConversationType type,string targetId,string channelId,long
            timestamp)
            {
                //...
            };
            //callback_onUnreadCountCleared_call
            */
    
            engine.OnDraftMessageSaved =
                delegate(int code, RCIMConversationType type, string targetId, string channelId, string draft)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onDraftMessageSaved";
                result["code"] = code;
                result["type"] = type;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["draft"] = draft;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onDraftMessageSaved_call
            engine.OnDraftMessageSaved = delegate(int code,RCIMConversationType type,string targetId,string channelId,string
            draft)
            {
                //...
            };
            //callback_onDraftMessageSaved_call
            */
    
            engine.OnDraftMessageCleared = delegate(int code, RCIMConversationType type, string targetId, string channelId)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onDraftMessageCleared";
                result["code"] = code;
                result["type"] = type;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onDraftMessageCleared_call
            engine.OnDraftMessageCleared = delegate(int code,RCIMConversationType type,string targetId,string channelId)
            {
                //...
            };
            //callback_onDraftMessageCleared_call
            */
    
            engine.OnDraftMessageLoaded =
                delegate(int code, RCIMConversationType type, string targetId, string channelId, string draft)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onDraftMessageLoaded";
                result["code"] = code;
                result["type"] = type;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["draft"] = draft;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onDraftMessageLoaded_call
            engine.OnDraftMessageLoaded = delegate(int code,RCIMConversationType type,string targetId,string
            channelId,string draft)
            {
                //...
            };
            //callback_onDraftMessageLoaded_call
            */
    
            engine.OnBlockedConversationsLoaded = delegate(int code, List<RCIMConversationType> conversationTypes,
                                                           string channelId, List<RCIMConversation> conversations)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onBlockedConversationsLoaded";
                result["code"] = code;
                result["conversationTypes"] = conversationTypes;
                result["channelId"] = channelId;
                result["conversations"] = conversations;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onBlockedConversationsLoaded_call
            engine.OnBlockedConversationsLoaded = delegate(int code,List<RCIMConversationType> conversationTypes,string
            channelId,List<RCIMConversation> conversations)
            {
                //...
            };
            //callback_onBlockedConversationsLoaded_call
            */
    
            engine.OnConversationTopStatusChanged =
                delegate(int code, RCIMConversationType type, string targetId, string channelId, bool top)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onConversationTopStatusChanged";
                result["code"] = code;
                result["type"] = type;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["top"] = top;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onConversationTopStatusChanged_call
            engine.OnConversationTopStatusChanged = delegate(int code,RCIMConversationType type,string targetId,string
            channelId,bool top)
            {
                //...
            };
            //callback_onConversationTopStatusChanged_call
            */
    
            engine.OnConversationTopStatusLoaded =
                delegate(int code, RCIMConversationType type, string targetId, string channelId, bool top)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onConversationTopStatusLoaded";
                result["code"] = code;
                result["type"] = type;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["top"] = top;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onConversationTopStatusLoaded_call
            engine.OnConversationTopStatusLoaded = delegate(int code,RCIMConversationType type,string targetId,string
            channelId,bool top)
            {
                //...
            };
            //callback_onConversationTopStatusLoaded_call
            */
    
            engine.OnConversationReadStatusSynced =
                delegate(int code, RCIMConversationType type, string targetId, string channelId, long timestamp)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onConversationReadStatusSynced";
                result["code"] = code;
                result["type"] = type;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["timestamp"] = timestamp;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onConversationReadStatusSynced_call
            engine.OnConversationReadStatusSynced = delegate(int code,RCIMConversationType type,string targetId,string
            channelId,long timestamp)
            {
                //...
            };
            //callback_onConversationReadStatusSynced_call
            */
    
            engine.OnMessageAttached = delegate(RCIMMessage message)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onMessageAttached";
                result["message"] = message;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onMessageAttached_call
            engine.OnMessageAttached = delegate(RCIMMessage message)
            {
                //...
            };
            //callback_onMessageAttached_call
            */
    
            engine.OnMessageSent = delegate(int code, RCIMMessage message)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onMessageSent";
                result["code"] = code;
                result["message"] = message;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onMessageSent_call
            engine.OnMessageSent = delegate(int code,RCIMMessage message)
            {
                //...
            };
            //callback_onMessageSent_call
            */
    
            engine.OnMediaMessageAttached = delegate(RCIMMediaMessage message)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onMediaMessageAttached";
                result["message"] = message;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onMediaMessageAttached_call
            engine.OnMediaMessageAttached = delegate(RCIMMediaMessage message)
            {
                //...
            };
            //callback_onMediaMessageAttached_call
            */
    
            engine.OnMediaMessageSending = delegate(RCIMMediaMessage message, int progress)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onMediaMessageSending";
                result["message"] = message;
                result["progress"] = progress;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onMediaMessageSending_call
            engine.OnMediaMessageSending = delegate(RCIMMediaMessage message,int progress)
            {
                //...
            };
            //callback_onMediaMessageSending_call
            */
    
            engine.OnSendingMediaMessageCanceled = delegate(int code, RCIMMediaMessage message)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onSendingMediaMessageCanceled";
                result["code"] = code;
                result["message"] = message;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onSendingMediaMessageCanceled_call
            engine.OnSendingMediaMessageCanceled = delegate(int code,RCIMMediaMessage message)
            {
                //...
            };
            //callback_onSendingMediaMessageCanceled_call
            */
    
            engine.OnMediaMessageSent = delegate(int code, RCIMMediaMessage message)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onMediaMessageSent";
                result["code"] = code;
                result["message"] = message;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onMediaMessageSent_call
            engine.OnMediaMessageSent = delegate(int code,RCIMMediaMessage message)
            {
                //...
            };
            //callback_onMediaMessageSent_call
            */
    
            engine.OnMediaMessageDownloading = delegate(RCIMMediaMessage message, int progress)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onMediaMessageDownloading";
                result["message"] = message;
                result["progress"] = progress;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onMediaMessageDownloading_call
            engine.OnMediaMessageDownloading = delegate(RCIMMediaMessage message,int progress)
            {
                //...
            };
            //callback_onMediaMessageDownloading_call
            */
    
            engine.OnMediaMessageDownloaded = delegate(int code, RCIMMediaMessage message)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onMediaMessageDownloaded";
                result["code"] = code;
                result["message"] = message;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onMediaMessageDownloaded_call
            engine.OnMediaMessageDownloaded = delegate(int code,RCIMMediaMessage message)
            {
                //...
            };
            //callback_onMediaMessageDownloaded_call
            */
    
            engine.OnDownloadingMediaMessageCanceled = delegate(int code, RCIMMediaMessage message)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onDownloadingMediaMessageCanceled";
                result["code"] = code;
                result["message"] = message;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onDownloadingMediaMessageCanceled_call
            engine.OnDownloadingMediaMessageCanceled = delegate(int code,RCIMMediaMessage message)
            {
                //...
            };
            //callback_onDownloadingMediaMessageCanceled_call
            */
    
            engine.OnMessagesLoaded = delegate(int code, RCIMConversationType type, string targetId, string channelId,
                                               long sentTime, RCIMTimeOrder order, List<RCIMMessage> messages)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onMessagesLoaded";
                result["code"] = code;
                result["type"] = type;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["sentTime"] = sentTime;
                result["order"] = order;
                result["messages"] = messages;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onMessagesLoaded_call
            engine.OnMessagesLoaded = delegate(int code,RCIMConversationType type,string targetId,string channelId,long
            sentTime,RCIMTimeOrder order,List<RCIMMessage> messages)
            {
                //...
            };
            //callback_onMessagesLoaded_call
            */
    
            engine.OnUnreadMentionedMessagesLoaded =
                delegate(int code, RCIMConversationType type, string targetId, string channelId, List<RCIMMessage> messages)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onUnreadMentionedMessagesLoaded";
                result["code"] = code;
                result["type"] = type;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["messages"] = messages;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onUnreadMentionedMessagesLoaded_call
            engine.OnUnreadMentionedMessagesLoaded = delegate(int code,RCIMConversationType type,string targetId,string
            channelId,List<RCIMMessage> messages)
            {
                //...
            };
            //callback_onUnreadMentionedMessagesLoaded_call
            */
    
            engine.OnFirstUnreadMessageLoaded =
                delegate(int code, RCIMConversationType type, string targetId, string channelId, RCIMMessage message)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onFirstUnreadMessageLoaded";
                result["code"] = code;
                result["type"] = type;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["message"] = message;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onFirstUnreadMessageLoaded_call
            engine.OnFirstUnreadMessageLoaded = delegate(int code,RCIMConversationType type,string targetId,string
            channelId,RCIMMessage message)
            {
                //...
            };
            //callback_onFirstUnreadMessageLoaded_call
            */
    
            engine.OnMessageInserted = delegate(int code, RCIMMessage message)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onMessageInserted";
                result["code"] = code;
                result["message"] = message;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onMessageInserted_call
            engine.OnMessageInserted = delegate(int code,RCIMMessage message)
            {
                //...
            };
            //callback_onMessageInserted_call
            */
    
            engine.OnMessagesInserted = delegate(int code, List<RCIMMessage> messages)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onMessagesInserted";
                result["code"] = code;
                result["messages"] = messages;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onMessagesInserted_call
            engine.OnMessagesInserted = delegate(int code,List<RCIMMessage> messages)
            {
                //...
            };
            //callback_onMessagesInserted_call
            */
    
            engine.OnMessagesCleared =
                delegate(int code, RCIMConversationType type, string targetId, string channelId, long timestamp)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onMessagesCleared";
                result["code"] = code;
                result["type"] = type;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["timestamp"] = timestamp;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onMessagesCleared_call
            engine.OnMessagesCleared = delegate(int code,RCIMConversationType type,string targetId,string channelId,long
            timestamp)
            {
                //...
            };
            //callback_onMessagesCleared_call
            */
    
            engine.OnLocalMessagesDeleted = delegate(int code, List<RCIMMessage> messages)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onLocalMessagesDeleted";
                result["code"] = code;
                result["messages"] = messages;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onLocalMessagesDeleted_call
            engine.OnLocalMessagesDeleted = delegate(int code,List<RCIMMessage> messages)
            {
                //...
            };
            //callback_onLocalMessagesDeleted_call
            */
    
            engine.OnMessagesDeleted =
                delegate(int code, RCIMConversationType type, string targetId, string channelId, List<RCIMMessage> messages)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onMessagesDeleted";
                result["code"] = code;
                result["type"] = type;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["messages"] = messages;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onMessagesDeleted_call
            engine.OnMessagesDeleted = delegate(int code,RCIMConversationType type,string targetId,string
            channelId,List<RCIMMessage> messages)
            {
                //...
            };
            //callback_onMessagesDeleted_call
            */
    
            engine.OnMessageRecalled = delegate(int code, RCIMMessage message)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onMessageRecalled";
                result["code"] = code;
                result["message"] = message;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onMessageRecalled_call
            engine.OnMessageRecalled = delegate(int code,RCIMMessage message)
            {
                //...
            };
            //callback_onMessageRecalled_call
            */
    
            engine.OnPrivateReadReceiptMessageSent = delegate(int code, string targetId, string channelId, long timestamp)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onPrivateReadReceiptMessageSent";
                result["code"] = code;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["timestamp"] = timestamp;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onPrivateReadReceiptMessageSent_call
            engine.OnPrivateReadReceiptMessageSent = delegate(int code,string targetId,string channelId,long timestamp)
            {
                //...
            };
            //callback_onPrivateReadReceiptMessageSent_call
            */
    
            engine.OnMessageExpansionUpdated = delegate(int code, string messageUId, Dictionary<string, string> expansion)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onMessageExpansionUpdated";
                result["code"] = code;
                result["messageUId"] = messageUId;
                result["expansion"] = expansion;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onMessageExpansionUpdated_call
            engine.OnMessageExpansionUpdated = delegate(int code,string messageUId,Dictionary<string, string> expansion)
            {
                //...
            };
            //callback_onMessageExpansionUpdated_call
            */
    
            engine.OnMessageExpansionForKeysRemoved = delegate(int code, string messageUId, List<string> keys)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onMessageExpansionForKeysRemoved";
                result["code"] = code;
                result["messageUId"] = messageUId;
                result["keys"] = keys;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onMessageExpansionForKeysRemoved_call
            engine.OnMessageExpansionForKeysRemoved = delegate(int code,string messageUId,List<string> keys)
            {
                //...
            };
            //callback_onMessageExpansionForKeysRemoved_call
            */
    
            engine.OnMessageReceiveStatusChanged = delegate(int code, long messageId)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onMessageReceiveStatusChanged";
                result["code"] = code;
                result["messageId"] = messageId;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onMessageReceiveStatusChanged_call
            engine.OnMessageReceiveStatusChanged = delegate(int code,long messageId)
            {
                //...
            };
            //callback_onMessageReceiveStatusChanged_call
            */
    
            engine.OnMessageSentStatusChanged = delegate(int code, long messageId)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onMessageSentStatusChanged";
                result["code"] = code;
                result["messageId"] = messageId;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onMessageSentStatusChanged_call
            engine.OnMessageSentStatusChanged = delegate(int code,long messageId)
            {
                //...
            };
            //callback_onMessageSentStatusChanged_call
            */
    
            engine.OnChatRoomJoined = delegate(int code, string targetId)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onChatRoomJoined";
                result["code"] = code;
                result["targetId"] = targetId;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onChatRoomJoined_call
            engine.OnChatRoomJoined = delegate(int code,string targetId)
            {
                //...
            };
            //callback_onChatRoomJoined_call
            */
    
            engine.OnChatRoomJoining = delegate(string targetId)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onChatRoomJoining";
                result["targetId"] = targetId;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onChatRoomJoining_call
            engine.OnChatRoomJoining = delegate(string targetId)
            {
                //...
            };
            //callback_onChatRoomJoining_call
            */
    
            engine.OnChatRoomLeft = delegate(int code, string targetId)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onChatRoomLeft";
                result["code"] = code;
                result["targetId"] = targetId;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onChatRoomLeft_call
            engine.OnChatRoomLeft = delegate(int code,string targetId)
            {
                //...
            };
            //callback_onChatRoomLeft_call
            */
    
            engine.OnChatRoomMessagesLoaded = delegate(int code, string targetId, List<RCIMMessage> messages, long syncTime)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onChatRoomMessagesLoaded";
                result["code"] = code;
                result["targetId"] = targetId;
                result["messages"] = messages;
                result["syncTime"] = syncTime;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onChatRoomMessagesLoaded_call
            engine.OnChatRoomMessagesLoaded = delegate(int code,string targetId,List<RCIMMessage> messages,long syncTime)
            {
                //...
            };
            //callback_onChatRoomMessagesLoaded_call
            */
    
            engine.OnChatRoomEntryAdded = delegate(int code, string targetId, string key)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onChatRoomEntryAdded";
                result["code"] = code;
                result["targetId"] = targetId;
                result["key"] = key;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onChatRoomEntryAdded_call
            engine.OnChatRoomEntryAdded = delegate(int code,string targetId,string key)
            {
                //...
            };
            //callback_onChatRoomEntryAdded_call
            */
    
            engine.OnChatRoomEntriesAdded = delegate(int code, string targetId, Dictionary<string, string> entries,
                                                     Dictionary<string, int> errorEntries)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onChatRoomEntriesAdded";
                result["code"] = code;
                result["targetId"] = targetId;
                result["entries"] = entries;
                result["errorEntries"] = errorEntries;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onChatRoomEntriesAdded_call
            engine.OnChatRoomEntriesAdded = delegate(int code,string targetId,Dictionary<string, string>
            entries,Dictionary<string, int> errorEntries)
            {
                //...
            };
            //callback_onChatRoomEntriesAdded_call
            */
    
            engine.OnChatRoomEntryLoaded = delegate(int code, string targetId, Dictionary<string, string> entry)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onChatRoomEntryLoaded";
                result["code"] = code;
                result["targetId"] = targetId;
                result["entry"] = entry;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onChatRoomEntryLoaded_call
            engine.OnChatRoomEntryLoaded = delegate(int code,string targetId,Dictionary<string, string> entry)
            {
                //...
            };
            //callback_onChatRoomEntryLoaded_call
            */
    
            engine.OnChatRoomAllEntriesLoaded = delegate(int code, string targetId, Dictionary<string, string> entries)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onChatRoomAllEntriesLoaded";
                result["code"] = code;
                result["targetId"] = targetId;
                result["entries"] = entries;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onChatRoomAllEntriesLoaded_call
            engine.OnChatRoomAllEntriesLoaded = delegate(int code,string targetId,Dictionary<string, string> entries)
            {
                //...
            };
            //callback_onChatRoomAllEntriesLoaded_call
            */
    
            engine.OnChatRoomEntryRemoved = delegate(int code, string targetId, string key)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onChatRoomEntryRemoved";
                result["code"] = code;
                result["targetId"] = targetId;
                result["key"] = key;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onChatRoomEntryRemoved_call
            engine.OnChatRoomEntryRemoved = delegate(int code,string targetId,string key)
            {
                //...
            };
            //callback_onChatRoomEntryRemoved_call
            */
    
            engine.OnChatRoomEntriesRemoved = delegate(int code, string targetId, List<string> keys)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onChatRoomEntriesRemoved";
                result["code"] = code;
                result["targetId"] = targetId;
                result["keys"] = keys;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onChatRoomEntriesRemoved_call
            engine.OnChatRoomEntriesRemoved = delegate(int code,string targetId,List<string> keys)
            {
                //...
            };
            //callback_onChatRoomEntriesRemoved_call
            */
    
            engine.OnBlacklistAdded = delegate(int code, string userId)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onBlacklistAdded";
                result["code"] = code;
                result["userId"] = userId;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onBlacklistAdded_call
            engine.OnBlacklistAdded = delegate(int code,string userId)
            {
                //...
            };
            //callback_onBlacklistAdded_call
            */
    
            engine.OnBlacklistRemoved = delegate(int code, string userId)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onBlacklistRemoved";
                result["code"] = code;
                result["userId"] = userId;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onBlacklistRemoved_call
            engine.OnBlacklistRemoved = delegate(int code,string userId)
            {
                //...
            };
            //callback_onBlacklistRemoved_call
            */
    
            engine.OnBlacklistStatusLoaded = delegate(int code, string userId, RCIMBlacklistStatus status)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onBlacklistStatusLoaded";
                result["code"] = code;
                result["userId"] = userId;
                result["status"] = status;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onBlacklistStatusLoaded_call
            engine.OnBlacklistStatusLoaded = delegate(int code,string userId,RCIMBlacklistStatus status)
            {
                //...
            };
            //callback_onBlacklistStatusLoaded_call
            */
    
            engine.OnBlacklistLoaded = delegate(int code, List<string> userIds)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onBlacklistLoaded";
                result["code"] = code;
                result["userIds"] = userIds;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onBlacklistLoaded_call
            engine.OnBlacklistLoaded = delegate(int code,List<string> userIds)
            {
                //...
            };
            //callback_onBlacklistLoaded_call
            */
    
            engine.OnMessagesSearched = delegate(int code, RCIMConversationType type, string targetId, string channelId,
                                                 string keyword, long startTime, int count, List<RCIMMessage> messages)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onMessagesSearched";
                result["code"] = code;
                result["type"] = type;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["keyword"] = keyword;
                result["startTime"] = startTime;
                result["count"] = count;
                result["messages"] = messages;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onMessagesSearched_call
            engine.OnMessagesSearched = delegate(int code,RCIMConversationType type,string targetId,string channelId,string
            keyword,long startTime,int count,List<RCIMMessage> messages)
            {
                //...
            };
            //callback_onMessagesSearched_call
            */
    
            engine.OnMessagesSearchedByTimeRange =
                delegate(int code, RCIMConversationType type, string targetId, string channelId, string keyword,
                         long startTime, long endTime, int offset, int count, List<RCIMMessage> messages)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onMessagesSearchedByTimeRange";
                result["code"] = code;
                result["type"] = type;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["keyword"] = keyword;
                result["startTime"] = startTime;
                result["endTime"] = endTime;
                result["offset"] = offset;
                result["count"] = count;
                result["messages"] = messages;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onMessagesSearchedByTimeRange_call
            engine.OnMessagesSearchedByTimeRange = delegate(int code,RCIMConversationType type,string targetId,string
            channelId,string keyword,long startTime,long endTime,int offset,int count,List<RCIMMessage> messages)
            {
                //...
            };
            //callback_onMessagesSearchedByTimeRange_call
            */
    
            engine.OnMessagesSearchedByUserId =
                delegate(int code, string userId, RCIMConversationType type, string targetId, string channelId,
                         long startTime, int count, List<RCIMMessage> messages)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onMessagesSearchedByUserId";
                result["code"] = code;
                result["userId"] = userId;
                result["type"] = type;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["startTime"] = startTime;
                result["count"] = count;
                result["messages"] = messages;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onMessagesSearchedByUserId_call
            engine.OnMessagesSearchedByUserId = delegate(int code,string userId,RCIMConversationType type,string
            targetId,string channelId,long startTime,int count,List<RCIMMessage> messages)
            {
                //...
            };
            //callback_onMessagesSearchedByUserId_call
            */
    
            engine.OnConversationsSearched = delegate(int code, List<RCIMConversationType> conversationTypes,
                                                      string channelId, List<RCIMMessageType> messageTypes, string keyword,
                                                      List<RCIMSearchConversationResult> conversations)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onConversationsSearched";
                result["code"] = code;
                result["conversationTypes"] = conversationTypes;
                result["channelId"] = channelId;
                result["messageTypes"] = messageTypes;
                result["keyword"] = keyword;
                result["conversations"] = conversations;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onConversationsSearched_call
            engine.OnConversationsSearched = delegate(int code,List<RCIMConversationType> conversationTypes,string
            channelId,List<RCIMMessageType> messageTypes,string keyword,List<RCIMSearchConversationResult> conversations)
            {
                //...
            };
            //callback_onConversationsSearched_call
            */
    
            engine.OnGroupReadReceiptRequestSent = delegate(int code, RCIMMessage message)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onGroupReadReceiptRequestSent";
                result["code"] = code;
                result["message"] = message;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onGroupReadReceiptRequestSent_call
            engine.OnGroupReadReceiptRequestSent = delegate(int code,RCIMMessage message)
            {
                //...
            };
            //callback_onGroupReadReceiptRequestSent_call
            */
    
            engine.OnGroupReadReceiptResponseSent =
                delegate(int code, string targetId, string channelId, List<RCIMMessage> messages)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onGroupReadReceiptResponseSent";
                result["code"] = code;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["messages"] = messages;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onGroupReadReceiptResponseSent_call
            engine.OnGroupReadReceiptResponseSent = delegate(int code,string targetId,string channelId,List<RCIMMessage>
            messages)
            {
                //...
            };
            //callback_onGroupReadReceiptResponseSent_call
            */
    
            engine.OnNotificationQuietHoursChanged =
                delegate(int code, string startTime, int spanMinutes, RCIMPushNotificationQuietHoursLevel level)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onNotificationQuietHoursChanged";
                result["code"] = code;
                result["startTime"] = startTime;
                result["spanMinutes"] = spanMinutes;
                result["level"] = level;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onNotificationQuietHoursChanged_call
            engine.OnNotificationQuietHoursChanged = delegate(int code,string startTime,int
            spanMinutes,RCIMPushNotificationQuietHoursLevel level)
            {
                //...
            };
            //callback_onNotificationQuietHoursChanged_call
            */
    
            engine.OnNotificationQuietHoursRemoved = delegate(int code)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onNotificationQuietHoursRemoved";
                result["code"] = code;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onNotificationQuietHoursRemoved_call
            engine.OnNotificationQuietHoursRemoved = delegate(int code)
            {
                //...
            };
            //callback_onNotificationQuietHoursRemoved_call
            */
    
            engine.OnNotificationQuietHoursLoaded =
                delegate(int code, string startTime, int spanMinutes, RCIMPushNotificationQuietHoursLevel level)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onNotificationQuietHoursLoaded";
                result["code"] = code;
                result["startTime"] = startTime;
                result["spanMinutes"] = spanMinutes;
                result["level"] = level;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onNotificationQuietHoursLoaded_call
            engine.OnNotificationQuietHoursLoaded = delegate(int code,string startTime,int
            spanMinutes,RCIMPushNotificationQuietHoursLevel level)
            {
                //...
            };
            //callback_onNotificationQuietHoursLoaded_call
            */
    
            engine.OnConversationNotificationLevelChanged = delegate(int code, RCIMConversationType type, string targetId,
                                                                     string channelId, RCIMPushNotificationLevel level)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onConversationNotificationLevelChanged";
                result["code"] = code;
                result["type"] = type;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["level"] = level;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onConversationNotificationLevelChanged_call
            engine.OnConversationNotificationLevelChanged = delegate(int code,RCIMConversationType type,string
            targetId,string channelId,RCIMPushNotificationLevel level)
            {
                //...
            };
            //callback_onConversationNotificationLevelChanged_call
            */
    
            engine.OnConversationNotificationLevelLoaded = delegate(int code, RCIMConversationType type, string targetId,
                                                                    string channelId, RCIMPushNotificationLevel level)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onConversationNotificationLevelLoaded";
                result["code"] = code;
                result["type"] = type;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["level"] = level;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onConversationNotificationLevelLoaded_call
            engine.OnConversationNotificationLevelLoaded = delegate(int code,RCIMConversationType type,string
            targetId,string channelId,RCIMPushNotificationLevel level)
            {
                //...
            };
            //callback_onConversationNotificationLevelLoaded_call
            */
    
            engine.OnConversationTypeNotificationLevelChanged =
                delegate(int code, RCIMConversationType type, RCIMPushNotificationLevel level)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onConversationTypeNotificationLevelChanged";
                result["code"] = code;
                result["type"] = type;
                result["level"] = level;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onConversationTypeNotificationLevelChanged_call
            engine.OnConversationTypeNotificationLevelChanged = delegate(int code,RCIMConversationType
            type,RCIMPushNotificationLevel level)
            {
                //...
            };
            //callback_onConversationTypeNotificationLevelChanged_call
            */
    
            engine.OnConversationTypeNotificationLevelLoaded =
                delegate(int code, RCIMConversationType type, RCIMPushNotificationLevel level)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onConversationTypeNotificationLevelLoaded";
                result["code"] = code;
                result["type"] = type;
                result["level"] = level;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onConversationTypeNotificationLevelLoaded_call
            engine.OnConversationTypeNotificationLevelLoaded = delegate(int code,RCIMConversationType
            type,RCIMPushNotificationLevel level)
            {
                //...
            };
            //callback_onConversationTypeNotificationLevelLoaded_call
            */
    
            engine.OnUltraGroupDefaultNotificationLevelChanged =
                delegate(int code, string targetId, RCIMPushNotificationLevel level)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onUltraGroupDefaultNotificationLevelChanged";
                result["code"] = code;
                result["targetId"] = targetId;
                result["level"] = level;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onUltraGroupDefaultNotificationLevelChanged_call
            engine.OnUltraGroupDefaultNotificationLevelChanged = delegate(int code,string targetId,RCIMPushNotificationLevel
            level)
            {
                //...
            };
            //callback_onUltraGroupDefaultNotificationLevelChanged_call
            */
    
            engine.OnUltraGroupDefaultNotificationLevelLoaded =
                delegate(int code, string targetId, RCIMPushNotificationLevel level)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onUltraGroupDefaultNotificationLevelLoaded";
                result["code"] = code;
                result["targetId"] = targetId;
                result["level"] = level;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onUltraGroupDefaultNotificationLevelLoaded_call
            engine.OnUltraGroupDefaultNotificationLevelLoaded = delegate(int code,string targetId,RCIMPushNotificationLevel
            level)
            {
                //...
            };
            //callback_onUltraGroupDefaultNotificationLevelLoaded_call
            */
    
            engine.OnUltraGroupChannelDefaultNotificationLevelChanged =
                delegate(int code, string targetId, string channelId, RCIMPushNotificationLevel level)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onUltraGroupChannelDefaultNotificationLevelChanged";
                result["code"] = code;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["level"] = level;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onUltraGroupChannelDefaultNotificationLevelChanged_call
            engine.OnUltraGroupChannelDefaultNotificationLevelChanged = delegate(int code,string targetId,string
            channelId,RCIMPushNotificationLevel level)
            {
                //...
            };
            //callback_onUltraGroupChannelDefaultNotificationLevelChanged_call
            */
    
            engine.OnUltraGroupChannelDefaultNotificationLevelLoaded =
                delegate(int code, string targetId, string channelId, RCIMPushNotificationLevel level)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onUltraGroupChannelDefaultNotificationLevelLoaded";
                result["code"] = code;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["level"] = level;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onUltraGroupChannelDefaultNotificationLevelLoaded_call
            engine.OnUltraGroupChannelDefaultNotificationLevelLoaded = delegate(int code,string targetId,string
            channelId,RCIMPushNotificationLevel level)
            {
                //...
            };
            //callback_onUltraGroupChannelDefaultNotificationLevelLoaded_call
            */
    
            engine.OnPushContentShowStatusChanged = delegate(int code, bool showContent)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onPushContentShowStatusChanged";
                result["code"] = code;
                result["showContent"] = showContent;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onPushContentShowStatusChanged_call
            engine.OnPushContentShowStatusChanged = delegate(int code,bool showContent)
            {
                //...
            };
            //callback_onPushContentShowStatusChanged_call
            */
    
            engine.OnPushLanguageChanged = delegate(int code, string language)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onPushLanguageChanged";
                result["code"] = code;
                result["language"] = language;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onPushLanguageChanged_call
            engine.OnPushLanguageChanged = delegate(int code,string language)
            {
                //...
            };
            //callback_onPushLanguageChanged_call
            */
    
            engine.OnPushReceiveStatusChanged = delegate(int code, bool receive)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onPushReceiveStatusChanged";
                result["code"] = code;
                result["receive"] = receive;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onPushReceiveStatusChanged_call
            engine.OnPushReceiveStatusChanged = delegate(int code,bool receive)
            {
                //...
            };
            //callback_onPushReceiveStatusChanged_call
            */
    
            engine.OnMessageCountLoaded =
                delegate(int code, RCIMConversationType type, string targetId, string channelId, int count)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onMessageCountLoaded";
                result["code"] = code;
                result["type"] = type;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["count"] = count;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onMessageCountLoaded_call
            engine.OnMessageCountLoaded = delegate(int code,RCIMConversationType type,string targetId,string channelId,int
            count)
            {
                //...
            };
            //callback_onMessageCountLoaded_call
            */
    
            engine.OnTopConversationsLoaded = delegate(int code, List<RCIMConversationType> conversationTypes,
                                                       string channelId, List<RCIMConversation> conversations)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onTopConversationsLoaded";
                result["code"] = code;
                result["conversationTypes"] = conversationTypes;
                result["channelId"] = channelId;
                result["conversations"] = conversations;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onTopConversationsLoaded_call
            engine.OnTopConversationsLoaded = delegate(int code,List<RCIMConversationType> conversationTypes,string
            channelId,List<RCIMConversation> conversations)
            {
                //...
            };
            //callback_onTopConversationsLoaded_call
            */
    
            engine.OnGroupMessageToDesignatedUsersAttached = delegate(RCIMMessage message)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onGroupMessageToDesignatedUsersAttached";
                result["message"] = message;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onGroupMessageToDesignatedUsersAttached_call
            engine.OnGroupMessageToDesignatedUsersAttached = delegate(RCIMMessage message)
            {
                //...
            };
            //callback_onGroupMessageToDesignatedUsersAttached_call
            */
    
            engine.OnGroupMessageToDesignatedUsersSent = delegate(int code, RCIMMessage message)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onGroupMessageToDesignatedUsersSent";
                result["code"] = code;
                result["message"] = message;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onGroupMessageToDesignatedUsersSent_call
            engine.OnGroupMessageToDesignatedUsersSent = delegate(int code,RCIMMessage message)
            {
                //...
            };
            //callback_onGroupMessageToDesignatedUsersSent_call
            */
    
            engine.OnUltraGroupReadStatusSynced = delegate(int code, string targetId, string channelId, long timestamp)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onUltraGroupReadStatusSynced";
                result["code"] = code;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["timestamp"] = timestamp;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onUltraGroupReadStatusSynced_call
            engine.OnUltraGroupReadStatusSynced = delegate(int code,string targetId,string channelId,long timestamp)
            {
                //...
            };
            //callback_onUltraGroupReadStatusSynced_call
            */
    
            engine.OnConversationsLoadedForAllChannel =
                delegate(int code, RCIMConversationType type, string targetId, List<RCIMConversation> conversations)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onConversationsLoadedForAllChannel";
                result["code"] = code;
                result["type"] = type;
                result["targetId"] = targetId;
                result["conversations"] = conversations;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onConversationsLoadedForAllChannel_call
            engine.OnConversationsLoadedForAllChannel = delegate(int code,RCIMConversationType type,string
            targetId,List<RCIMConversation> conversations)
            {
                //...
            };
            //callback_onConversationsLoadedForAllChannel_call
            */
    
            engine.OnUltraGroupUnreadMentionedCountLoaded = delegate(int code, string targetId, int count)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onUltraGroupUnreadMentionedCountLoaded";
                result["code"] = code;
                result["targetId"] = targetId;
                result["count"] = count;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onUltraGroupUnreadMentionedCountLoaded_call
            engine.OnUltraGroupUnreadMentionedCountLoaded = delegate(int code,string targetId,int count)
            {
                //...
            };
            //callback_onUltraGroupUnreadMentionedCountLoaded_call
            */
    
            engine.OnUltraGroupUnreadCountLoaded = delegate(int code, string targetId, int count)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onUltraGroupUnreadCountLoaded";
                result["code"] = code;
                result["targetId"] = targetId;
                result["count"] = count;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onUltraGroupUnreadCountLoaded_call
            engine.OnUltraGroupUnreadCountLoaded = delegate(int code,string targetId,int count)
            {
                //...
            };
            //callback_onUltraGroupUnreadCountLoaded_call
            */
    
            engine.OnUltraGroupMessageModified = delegate(int code, string messageUId)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onUltraGroupMessageModified";
                result["code"] = code;
                result["messageUId"] = messageUId;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onUltraGroupMessageModified_call
            engine.OnUltraGroupMessageModified = delegate(int code,string messageUId)
            {
                //...
            };
            //callback_onUltraGroupMessageModified_call
            */
    
            engine.OnUltraGroupMessageRecalled = delegate(int code, RCIMMessage message, bool deleteRemote)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onUltraGroupMessageRecalled";
                result["code"] = code;
                result["message"] = message;
                result["deleteRemote"] = deleteRemote;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onUltraGroupMessageRecalled_call
            engine.OnUltraGroupMessageRecalled = delegate(int code,RCIMMessage message,bool deleteRemote)
            {
                //...
            };
            //callback_onUltraGroupMessageRecalled_call
            */
    
            engine.OnUltraGroupMessagesCleared =
                delegate(int code, string targetId, string channelId, long timestamp, RCIMMessageOperationPolicy policy)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onUltraGroupMessagesCleared";
                result["code"] = code;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["timestamp"] = timestamp;
                result["policy"] = policy;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onUltraGroupMessagesCleared_call
            engine.OnUltraGroupMessagesCleared = delegate(int code,string targetId,string channelId,long
            timestamp,RCIMMessageOperationPolicy policy)
            {
                //...
            };
            //callback_onUltraGroupMessagesCleared_call
            */
    
            engine.OnUltraGroupMessagesClearedForAllChannel = delegate(int code, string targetId, long timestamp)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onUltraGroupMessagesClearedForAllChannel";
                result["code"] = code;
                result["targetId"] = targetId;
                result["timestamp"] = timestamp;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onUltraGroupMessagesClearedForAllChannel_call
            engine.OnUltraGroupMessagesClearedForAllChannel = delegate(int code,string targetId,long timestamp)
            {
                //...
            };
            //callback_onUltraGroupMessagesClearedForAllChannel_call
            */
    
            engine.OnUltraGroupTypingStatusSent =
                delegate(int code, string targetId, string channelId, RCIMUltraGroupTypingStatus typingStatus)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onUltraGroupTypingStatusSent";
                result["code"] = code;
                result["targetId"] = targetId;
                result["channelId"] = channelId;
                result["typingStatus"] = typingStatus;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onUltraGroupTypingStatusSent_call
            engine.OnUltraGroupTypingStatusSent = delegate(int code,string targetId,string
            channelId,RCIMUltraGroupTypingStatus typingStatus)
            {
                //...
            };
            //callback_onUltraGroupTypingStatusSent_call
            */
    
            engine.OnBatchRemoteUltraGroupMessagesLoaded =
                delegate(int code, List<RCIMMessage> matchedMessages, List<RCIMMessage> notMatchedMessages)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onBatchRemoteUltraGroupMessagesLoaded";
                result["code"] = code;
                result["matchedMessages"] = matchedMessages;
                result["notMatchedMessages"] = notMatchedMessages;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onBatchRemoteUltraGroupMessagesLoaded_call
            engine.OnBatchRemoteUltraGroupMessagesLoaded = delegate(int code,List<RCIMMessage>
            matchedMessages,List<RCIMMessage> notMatchedMessages)
            {
                //...
            };
            //callback_onBatchRemoteUltraGroupMessagesLoaded_call
            */
    
            engine.OnUltraGroupMessageExpansionUpdated =
                delegate(int code, Dictionary<string, string> expansion, string messageUId)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onUltraGroupMessageExpansionUpdated";
                result["code"] = code;
                result["expansion"] = expansion;
                result["messageUId"] = messageUId;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onUltraGroupMessageExpansionUpdated_call
            engine.OnUltraGroupMessageExpansionUpdated = delegate(int code,Dictionary<string, string> expansion,string
            messageUId)
            {
                //...
            };
            //callback_onUltraGroupMessageExpansionUpdated_call
            */
    
            engine.OnUltraGroupMessageExpansionForKeysRemoved = delegate(int code, string messageUId, List<string> keys)
            {
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "onUltraGroupMessageExpansionForKeysRemoved";
                result["code"] = code;
                result["messageUId"] = messageUId;
                result["keys"] = keys;
                ResultHandle?.Invoke(result);
            };
            /*
            //callback_onUltraGroupMessageExpansionForKeysRemoved_call
            engine.OnUltraGroupMessageExpansionForKeysRemoved = delegate(int code,string messageUId,List<string> keys)
            {
                //...
            };
            //callback_onUltraGroupMessageExpansionForKeysRemoved_call
            */
        }
    }
    
    public class RCIMConnectListenerProxy : RCIMConnectListener
    {
        public void OnConnected(int code, string userId)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMConnectListenerProxy-onConnected";
            result["code"] = code;
            result["userId"] = userId;
            ResultHandle?.Invoke(result);
        }
        public void OnDatabaseOpened(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMConnectListenerProxy-onDatabaseOpened";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMConnectListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMSendMessageListenerProxy : RCIMSendMessageListener
    {
        public void OnMessageSaved(RCIMMessage message)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMSendMessageListenerProxy-onMessageSaved";
            result["message"] = message;
            ResultHandle?.Invoke(result);
        }
        public void OnMessageSent(int code, RCIMMessage message)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMSendMessageListenerProxy-onMessageSent";
            result["code"] = code;
            result["message"] = message;
            ResultHandle?.Invoke(result);
        }
        public RCIMSendMessageListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMSendMediaMessageListenerProxy : RCIMSendMediaMessageListener
    {
        public void OnMediaMessageSaved(RCIMMediaMessage message)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMSendMediaMessageListenerProxy-onMediaMessageSaved";
            result["message"] = message;
            ResultHandle?.Invoke(result);
        }
        public void OnMediaMessageSending(RCIMMediaMessage message, int progress)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMSendMediaMessageListenerProxy-onMediaMessageSending";
            result["message"] = message;
            result["progress"] = progress;
            ResultHandle?.Invoke(result);
        }
        public void OnSendingMediaMessageCanceled(RCIMMediaMessage message)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMSendMediaMessageListenerProxy-onSendingMediaMessageCanceled";
            result["message"] = message;
            ResultHandle?.Invoke(result);
        }
        public void OnMediaMessageSent(int code, RCIMMediaMessage message)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMSendMediaMessageListenerProxy-onMediaMessageSent";
            result["code"] = code;
            result["message"] = message;
            ResultHandle?.Invoke(result);
        }
        public RCIMSendMediaMessageListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMDownloadMediaMessageListenerProxy : RCIMDownloadMediaMessageListener
    {
        public void OnMediaMessageDownloading(RCIMMediaMessage message, int progress)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMDownloadMediaMessageListenerProxy-onMediaMessageDownloading";
            result["message"] = message;
            result["progress"] = progress;
            ResultHandle?.Invoke(result);
        }
        public void OnDownloadingMediaMessageCanceled(RCIMMediaMessage message)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMDownloadMediaMessageListenerProxy-onDownloadingMediaMessageCanceled";
            result["message"] = message;
            ResultHandle?.Invoke(result);
        }
        public void OnMediaMessageDownloaded(int code, RCIMMediaMessage message)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMDownloadMediaMessageListenerProxy-onMediaMessageDownloaded";
            result["code"] = code;
            result["message"] = message;
            ResultHandle?.Invoke(result);
        }
        public RCIMDownloadMediaMessageListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetConversationListenerProxy : RCIMGetConversationListener
    {
        public void OnSuccess(RCIMConversation t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetConversationListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetConversationListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetConversationListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetConversationsListenerProxy : RCIMGetConversationsListener
    {
        public void OnSuccess(List<RCIMConversation> t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetConversationsListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetConversationsListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetConversationsListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetUnreadCountListenerProxy : RCIMGetUnreadCountListener
    {
        public void OnSuccess(int t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetUnreadCountListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetUnreadCountListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetUnreadCountListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetTotalUnreadCountListenerProxy : RCIMGetTotalUnreadCountListener
    {
        public void OnSuccess(int t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetTotalUnreadCountListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetTotalUnreadCountListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetTotalUnreadCountListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetUnreadMentionedCountListenerProxy : RCIMGetUnreadMentionedCountListener
    {
        public void OnSuccess(int t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetUnreadMentionedCountListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetUnreadMentionedCountListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetUnreadMentionedCountListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetUltraGroupAllUnreadCountListenerProxy : RCIMGetUltraGroupAllUnreadCountListener
    {
        public void OnSuccess(int t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetUltraGroupAllUnreadCountListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetUltraGroupAllUnreadCountListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetUltraGroupAllUnreadCountListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetUltraGroupAllUnreadMentionedCountListenerProxy : RCIMGetUltraGroupAllUnreadMentionedCountListener
    {
        public void OnSuccess(int t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetUltraGroupAllUnreadMentionedCountListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetUltraGroupAllUnreadMentionedCountListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetUltraGroupAllUnreadMentionedCountListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetUltraGroupUnreadCountListenerProxy : RCIMGetUltraGroupUnreadCountListener
    {
        public void OnSuccess(int t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetUltraGroupUnreadCountListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetUltraGroupUnreadCountListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetUltraGroupUnreadCountListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetUltraGroupUnreadMentionedCountListenerProxy : RCIMGetUltraGroupUnreadMentionedCountListener
    {
        public void OnSuccess(int t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetUltraGroupUnreadMentionedCountListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetUltraGroupUnreadMentionedCountListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetUltraGroupUnreadMentionedCountListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetUnreadCountByConversationTypesListenerProxy : RCIMGetUnreadCountByConversationTypesListener
    {
        public void OnSuccess(int t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetUnreadCountByConversationTypesListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetUnreadCountByConversationTypesListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetUnreadCountByConversationTypesListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetDraftMessageListenerProxy : RCIMGetDraftMessageListener
    {
        public void OnSuccess(string t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetDraftMessageListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetDraftMessageListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetDraftMessageListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetBlockedConversationsListenerProxy : RCIMGetBlockedConversationsListener
    {
        public void OnSuccess(List<RCIMConversation> t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetBlockedConversationsListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetBlockedConversationsListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetBlockedConversationsListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetConversationTopStatusListenerProxy : RCIMGetConversationTopStatusListener
    {
        public void OnSuccess(Boolean t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetConversationTopStatusListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetConversationTopStatusListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetConversationTopStatusListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetMessagesListenerProxy : RCIMGetMessagesListener
    {
        public void OnSuccess(List<RCIMMessage> t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetMessagesListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetMessagesListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetMessagesListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetMessageListenerProxy : RCIMGetMessageListener
    {
        public void OnSuccess(RCIMMessage t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetMessageListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetMessageListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetMessageListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetFirstUnreadMessageListenerProxy : RCIMGetFirstUnreadMessageListener
    {
        public void OnSuccess(RCIMMessage t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetFirstUnreadMessageListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetFirstUnreadMessageListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetFirstUnreadMessageListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetUnreadMentionedMessagesListenerProxy : RCIMGetUnreadMentionedMessagesListener
    {
        public void OnSuccess(List<RCIMMessage> t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetUnreadMentionedMessagesListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetUnreadMentionedMessagesListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetUnreadMentionedMessagesListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetChatRoomMessagesListenerProxy : RCIMGetChatRoomMessagesListener
    {
        public void OnSuccess(List<RCIMMessage> t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetChatRoomMessagesListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetChatRoomMessagesListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetChatRoomMessagesListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetChatRoomEntryListenerProxy : RCIMGetChatRoomEntryListener
    {
        public void OnSuccess(Dictionary<string, string> t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetChatRoomEntryListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetChatRoomEntryListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetChatRoomEntryListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetChatRoomAllEntriesListenerProxy : RCIMGetChatRoomAllEntriesListener
    {
        public void OnSuccess(Dictionary<string, string> t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetChatRoomAllEntriesListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetChatRoomAllEntriesListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetChatRoomAllEntriesListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetBlacklistStatusListenerProxy : RCIMGetBlacklistStatusListener
    {
        public void OnSuccess(RCIMBlacklistStatus t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetBlacklistStatusListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetBlacklistStatusListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetBlacklistStatusListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetBlacklistListenerProxy : RCIMGetBlacklistListener
    {
        public void OnSuccess(List<string> t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetBlacklistListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetBlacklistListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetBlacklistListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMSearchMessagesListenerProxy : RCIMSearchMessagesListener
    {
        public void OnSuccess(List<RCIMMessage> t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMSearchMessagesListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMSearchMessagesListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMSearchMessagesListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMSearchMessagesByTimeRangeListenerProxy : RCIMSearchMessagesByTimeRangeListener
    {
        public void OnSuccess(List<RCIMMessage> t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMSearchMessagesByTimeRangeListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMSearchMessagesByTimeRangeListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMSearchMessagesByTimeRangeListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMSearchMessagesByUserIdListenerProxy : RCIMSearchMessagesByUserIdListener
    {
        public void OnSuccess(List<RCIMMessage> t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMSearchMessagesByUserIdListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMSearchMessagesByUserIdListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMSearchMessagesByUserIdListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMSearchConversationsListenerProxy : RCIMSearchConversationsListener
    {
        public void OnSuccess(List<RCIMSearchConversationResult> t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMSearchConversationsListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMSearchConversationsListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMSearchConversationsListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetNotificationQuietHoursListenerProxy : RCIMGetNotificationQuietHoursListener
    {
        public void OnSuccess(string startTime, int spanMinutes, RCIMPushNotificationQuietHoursLevel level)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetNotificationQuietHoursListenerProxy-onSuccess";
            result["startTime"] = startTime;
            result["spanMinutes"] = spanMinutes;
            result["level"] = level;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetNotificationQuietHoursListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetNotificationQuietHoursListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetConversationNotificationLevelListenerProxy : RCIMGetConversationNotificationLevelListener
    {
        public void OnSuccess(RCIMPushNotificationLevel t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetConversationNotificationLevelListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetConversationNotificationLevelListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetConversationNotificationLevelListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetConversationTypeNotificationLevelListenerProxy : RCIMGetConversationTypeNotificationLevelListener
    {
        public void OnSuccess(RCIMPushNotificationLevel t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetConversationTypeNotificationLevelListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetConversationTypeNotificationLevelListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetConversationTypeNotificationLevelListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetUltraGroupDefaultNotificationLevelListenerProxy : RCIMGetUltraGroupDefaultNotificationLevelListener
    {
        public void OnSuccess(RCIMPushNotificationLevel t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetUltraGroupDefaultNotificationLevelListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetUltraGroupDefaultNotificationLevelListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetUltraGroupDefaultNotificationLevelListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetUltraGroupChannelDefaultNotificationLevelListenerProxy
        : RCIMGetUltraGroupChannelDefaultNotificationLevelListener
    {
        public void OnSuccess(RCIMPushNotificationLevel t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetUltraGroupChannelDefaultNotificationLevelListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetUltraGroupChannelDefaultNotificationLevelListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetUltraGroupChannelDefaultNotificationLevelListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMSendGroupMessageToDesignatedUsersListenerProxy : RCIMSendGroupMessageToDesignatedUsersListener
    {
        public void OnMessageSaved(RCIMMessage message)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMSendGroupMessageToDesignatedUsersListenerProxy-onMessageSaved";
            result["message"] = message;
            ResultHandle?.Invoke(result);
        }
        public void OnMessageSent(int code, RCIMMessage message)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMSendGroupMessageToDesignatedUsersListenerProxy-onMessageSent";
            result["code"] = code;
            result["message"] = message;
            ResultHandle?.Invoke(result);
        }
        public RCIMSendGroupMessageToDesignatedUsersListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetMessageCountListenerProxy : RCIMGetMessageCountListener
    {
        public void OnSuccess(int t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetMessageCountListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetMessageCountListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetMessageCountListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetTopConversationsListenerProxy : RCIMGetTopConversationsListener
    {
        public void OnSuccess(List<RCIMConversation> t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetTopConversationsListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetTopConversationsListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetTopConversationsListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetConversationsForAllChannelListenerProxy : RCIMGetConversationsForAllChannelListener
    {
        public void OnSuccess(List<RCIMConversation> t)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetConversationsForAllChannelListenerProxy-onSuccess";
            result["t"] = t;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetConversationsForAllChannelListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetConversationsForAllChannelListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
    public class RCIMGetBatchRemoteUltraGroupMessagesListenerProxy : RCIMGetBatchRemoteUltraGroupMessagesListener
    {
        public void OnSuccess(List<RCIMMessage> matchedMessages, List<RCIMMessage> notMatchedMessages)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetBatchRemoteUltraGroupMessagesListenerProxy-onSuccess";
            result["matchedMessages"] = matchedMessages;
            result["notMatchedMessages"] = notMatchedMessages;
            ResultHandle?.Invoke(result);
        }
        public void OnError(int code)
        {
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "RCIMGetBatchRemoteUltraGroupMessagesListenerProxy-onError";
            result["code"] = code;
            ResultHandle?.Invoke(result);
        }
        public RCIMGetBatchRemoteUltraGroupMessagesListenerProxy(EngineManager.OnEventResultHandle handle)
        {
            ResultHandle = handle;
        }
        private EngineManager.OnEventResultHandle ResultHandle;
    }
    
}
