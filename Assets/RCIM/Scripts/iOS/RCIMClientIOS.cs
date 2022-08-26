//
//  Copyright © 2021 RongCloud. All rights reserved.
//

#if UNITY_IOS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

namespace cn_rongcloud_im_unity
{
    public partial class RCIMClientIOS : RCIMClient
    {
        private string UserId { get; set; }

        public RCIMClientIOS()
        {
            NativeIOS.im_init_client_listener(
                OnImConnectionStatusChangedListener,
                OnImReceivedMessageListener,
                OnImBlockedMessageInfoListener,
                OnImRecallMessageDidReceived,
                OnImMessageExpansionDidUpdateListener,
                OnImMessageExpansionDidRemoveListener,
                OnImTypingStatusChangedListener,
                OnImChatRoomKvDidSyncedListener,
                OnImChatRoomKvDidUpdatedListener,
                OnImChatRoomKvDidRemovedListener,
                OnImChatRoomMemberActionChangedListener,
                OnImChatRoomStatusChanged_Joining_Listener,
                OnImChatRoomStatusChanged_Joined_Listener,
                OnImChatRoomStatusChanged_Reset_Listener,
                OnImChatRoomStatusChanged_Quited_Listener,
                OnImChatRoomStatusChanged_Destroyed_Listener,
                OnImChatRoomStatusChanged_Error_Listener,
                OnImTagChangedListener,
                OnImConversationTagChangedListener,
                OnImReadReceiptRequestListener,
                OnImReadReceiptResponseListener,
                OnImReadReceiptReceivedListener,
                OnMediaMessageSendProgressListener,
                OnMediaMessageSendCancelListener
            );
        }

        #region override RCIMClient methods

        public override void Init(string appKey)
        {
            NativeIOS.im_init(appKey);
        }

        public override void Connect(string token, OperationCallbackWithResult<string> callback)
        {
            on_general_string_callback_proxy proxy;
            proxy.callback = OnConnect;
            proxy.handle = RCIMUtils.AddCallback(callback);

            Debug.Log($"im_connect wiRCth handle:{proxy.handle}");
            NativeIOS.im_connect(token, ref proxy);
        }

        public override void Logout()
        {
            NativeIOS.im_logout();
        }

        public override void Disconnect()
        {
            NativeIOS.im_disconnect();
        }

        public override string GetCurrentUserID()
        {
            return UserId;
        }

        public override void SendMessage(RCConversationType type, string targetId, RCMessageContent content,
            bool disableNotification = false)
        {
            Debug.Log($"SendMessage targetId:{targetId}, content:{content}, objName:{content.ObjectName}");
            im_message imMsg = RCIMUtils.createImMessage(type, targetId, content, disableNotification);
            NativeIOS.im_send_message(ref imMsg, OnSendMessageCallback);
            RCIMUtils.FreeImMessageContent(ref imMsg);
        }

        public override void SendMessage(RCMessage message, string pushContent = null, string pushData = null)
        {
            im_message imMsg = RCIMUtils.convertRCMessage(message);
            if (!string.IsNullOrEmpty(pushContent) || !string.IsNullOrEmpty(pushData))
            {
                im_push_config imPushConfig = default;
                imPushConfig.pushContent = pushContent;
                imPushConfig.pushData = pushData;
                imMsg.setPushConfig(imPushConfig);
            }

            NativeIOS.im_send_message(ref imMsg, OnSendMessageCallback);
            RCIMUtils.FreeImMessageContent(ref imMsg);
        }

        public override void SendMessageCarriesPush(RCConversationType type, string targetId, RCMessageContent content,
            string pushContent, string pushData, bool disableNotification = false)
        {
            Debug.Log($"SendMessageCarriesPush targetId:{targetId}, content:{content}, objName:{content.ObjectName}," +
                      $" pushContent:{pushContent}, pushData:{pushData}");
            im_message imMsg = RCIMUtils.createImMessage(type, targetId, content, disableNotification);
            if (!string.IsNullOrEmpty(pushContent) || !string.IsNullOrEmpty(pushData))
            {
                im_push_config imPushConfig = default;
                imPushConfig.pushContent = pushContent;
                imPushConfig.pushData = pushData;
                imMsg.setPushConfig(imPushConfig);
            }

            NativeIOS.im_send_message(ref imMsg, OnSendMessageCallback);
            RCIMUtils.FreeImMessageContent(ref imMsg);
        }

        public override RCConnectionStatus GetConnectionStatus()
        {
            int status = NativeIOS.im_get_connection_status();
            return RCIMUtils.convertConnectionStatusIOS(status);
        }

        public override void GetConversation(RCConversationType type, string targetId,
            OperationCallbackWithResult<RCConversation> callback)
        {
            on_general_intptr_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGetConversationCallback;
            NativeIOS.im_get_conversation((int) type, targetId, ref p);
        }

        public override void RemoveConversation(RCConversationType type, string targetId, OperationCallback callback)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;
            NativeIOS.im_remove_conversation((int) type, targetId, ref p);
        }

        public override void GetConversationList(OperationCallbackWithResult<IList<RCConversation>> callback,
            params RCConversationType[] conversationTypes)
        {
            on_general_intptr_list_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGetConversationListCallback;
            int length = conversationTypes.Length;
            NativeIOS.im_get_conversation_list(conversationTypes, length, ref p);
        }

        public override void GetConversationListByPage(OperationCallbackWithResult<IList<RCConversation>> callback,
            long timestamp, int count, params RCConversationType[] conversationTypes)
        {
            on_general_intptr_list_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGetConversationListCallback;
            int length = conversationTypes.Length;
            NativeIOS.im_get_conversation_list_by_page(conversationTypes, length, timestamp, count, ref p);
        }

        public override void GetBlockedConversationList(OperationCallbackWithResult<IList<RCConversation>> callback,
            params RCConversationType[] conversationTypes)
        {
            on_general_intptr_list_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGetConversationListCallback;
            int length = conversationTypes.Length;
            NativeIOS.im_get_blocked_conversation_list(conversationTypes, length, ref p);
        }

        public override void GetTopConversationList(OperationCallbackWithResult<IList<RCConversation>> callback,
            params RCConversationType[] conversationTypes)
        {
            on_general_intptr_list_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGetConversationListCallback;
            int length = conversationTypes.Length;
            NativeIOS.im_get_top_conversation_list(conversationTypes, length, ref p);
        }

        public override void ClearConversations(OperationCallback callback,
            params RCConversationType[] conversationTypes)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;
            int length = conversationTypes.Length;
            NativeIOS.im_clear_conversations(conversationTypes, length, ref p);
        }

        public override void GetTextMessageDraft(RCConversationType type, string targetId,
            OperationCallbackWithResult<string> callback)
        {
            on_general_string_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralStringCallback;
            NativeIOS.im_get_text_message_draft(type, targetId, ref p);
        }

        public override void SaveTextMessageDraft(RCConversationType type, string targetId, string textDraft,
            OperationCallback callback)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;
            NativeIOS.im_save_text_message_draft(type, targetId, textDraft, ref p);
        }

        public override void ClearTextMessageDraft(RCConversationType type, string targetId, OperationCallback callback)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;
            NativeIOS.im_clear_text_message_draft(type, targetId, ref p);
        }

        public override void GetUnreadCount(RCConversationType type, string targetId,
            OperationCallbackWithResult<int> callback)
        {
            on_general_int_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralIntCallback;
            NativeIOS.im_get_unread_count_target_id(type, targetId, ref p);
        }

        public override void GetUnreadCountByTag(string tagId, bool containsDND,
            OperationCallbackWithResult<int> callback)
        {
            on_general_int_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralIntCallback;
            NativeIOS.im_get_unread_count_by_tag(tagId, containsDND, ref p);
        }

        public override void GetUnreadCountByConversationTypes(bool containsDND,
            OperationCallbackWithResult<int> callback, params RCConversationType[] conversationTypes)
        {
            on_general_int_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralIntCallback;
            int length = conversationTypes.Length;
            NativeIOS.im_get_unread_count_conversation_types(conversationTypes, length, containsDND, ref p);
        }

        public override void GetTotalUnreadCount(OperationCallbackWithResult<int> callback)
        {
            on_general_int_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralIntCallback;
            NativeIOS.im_get_total_unread_count(ref p);
        }

        public override void ClearMessageUnreadStatus(RCConversationType type, string targetId,
            OperationCallback callback)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;
            NativeIOS.im_clear_messages_unread_status(type, targetId, ref p);
        }

        public override void SetConversationNotificationStatus(RCConversationType type, string targetId,
            RCConversationNotificationStatus notificationStatus,
            OperationCallbackWithResult<RCConversationNotificationStatus> callback)
        {
            on_general_int_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnSetConversationNotificationStatusCallback;
            NativeIOS.im_set_conversation_notification_status(type, targetId, notificationStatus, ref p);
        }

        public override void GetConversationNotificationStatus(RCConversationType type, string targetId,
            OperationCallbackWithResult<RCConversationNotificationStatus> callback)
        {
            on_general_int_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnSetConversationNotificationStatusCallback;
            NativeIOS.im_get_conversation_notification_status(type, targetId, ref p);
        }

        public override void SetConversationToTop(RCConversationType type, string targetId, bool isTop,
            OperationCallback callback)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;
            NativeIOS.im_set_conversation_to_top(type, targetId, isTop, ref p);
        }

        public override void SetConversationToTopInTag(RCConversationType type, string targetId, string tagId,
            bool isTop, OperationCallback callback)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;
            NativeIOS.im_set_conversation_to_top_in_tag(type, targetId, tagId, isTop, ref p);
        }

        public override void GetConversationTopStatusInTag(RCConversationType type, string targetId, string tagId,
            OperationCallbackWithResult<bool> callback)
        {
            on_general_bool_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralBoolCallback;
            NativeIOS.im_get_conversation_top_status_in_tag(type, targetId, tagId, ref p);
        }

        public override void SetOfflineMessageDuration(int duration, OperationCallbackWithResult<long> callback)
        {
            on_general_long_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralLongCallback;
            NativeIOS.im_set_offline_message_duration(duration, ref p);
        }

        public override void GetOfflineMessageDuration(OperationCallbackWithResult<long> callback)
        {
            on_general_long_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralLongCallback;
            NativeIOS.im_get_offline_message_duration(ref p);
        }

        public override void SetAndroidPushConfig(RCAndroidPushConfig androidPushConfig)
        {

        }

        public override void GetMessage(Int64 messageId, OperationCallbackWithResult<RCMessage> callback)
        {
            on_general_intptr_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGetMessageCallback;
            NativeIOS.im_get_message(messageId, ref p);
        }

        public override void GetMessageByUid(string messageUid, OperationCallbackWithResult<RCMessage> callback)
        {
            on_general_intptr_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGetMessageCallback;
            NativeIOS.im_get_message_by_uid(messageUid, ref p);
        }

        public override void CancelDownloadMediaMessage(RCMessage message)
        {
            if (message != null)
            {
                NativeIOS.im_cancel_download_media_message(message.MessageId);
            }
        }

        public override void BatchInsertMessage(IList<RCMessage> messageList,
            OperationCallbackWithResult<bool> callback)
        {
            var rcArrayMsg = new List<RCMessage>(messageList);
            var imListMsg = new List<im_message>();
            for (int i = 0; i < rcArrayMsg.Count; i++)
            {
                if (rcArrayMsg[i] != null)
                {
                    var imMsg = RCIMUtils.convertRCMessage(rcArrayMsg[i]);
                    imListMsg.Add(imMsg);
                }
            }

            im_message[] imArrayMsg = imListMsg.ToArray();
            int length = imArrayMsg.Length;

            on_general_bool_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralBoolCallback;
            NativeIOS.im_batch_insert_message(ref imArrayMsg, length, ref p);
            for (int j = 0; j < length; j++)
            {
                var imMsg = imArrayMsg[j];
                RCIMUtils.FreeImMessageContent(ref imMsg);
            }
        }

        public override void SearchConversations(string keyword, RCConversationType[] conversationTypes,
            string[] objectNames, OperationCallbackWithResult<IList<RCSearchConversationResult>> callback)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;
            NativeIOS.im_search_conversations(keyword, conversationTypes, conversationTypes.Length, objectNames, objectNames.Length, ref p);
        }

        public override void SetTypingUpdateInterval(int typingInterval)
        {
            NativeIOS.im_typing_update_seconds(typingInterval);
        }

        public override void SendTypingStatus(RCConversationType type, string targetId, string typingContent)
        {
            NativeIOS.im_send_typing_status(type, targetId, typingContent);
        }

        public override void SendDirectionalMessage(RCConversationType type, string targetId, IList<string> userIdList,
            RCMessageContent content, string pushContent = null, string pushData = null)
        {
            List<string> userIds = new List<string>(userIdList);
            string[] userIdArray = userIds.ToArray();
            int userIdsLen = userIdArray.Length;
            im_message imMsg = RCIMUtils.createImMessage(type, targetId, content);

            if (!string.IsNullOrEmpty(pushContent) || !string.IsNullOrEmpty(pushData))
            {
                im_push_config imPushConfig = default;
                imPushConfig.pushContent = pushContent;
                imPushConfig.pushData = pushData;
                imMsg.setPushConfig(imPushConfig);
            }

            NativeIOS.im_send_directional_message((int) type, targetId, userIdArray, userIdsLen, ref imMsg,
                OnSendMessageCallback);
            RCIMUtils.FreeImMessageContent(ref imMsg);
        }

        public override void SendMediaMessage(RCMessage message, string pushContent = null, string pushData = null)
        {
            im_message imMsg = RCIMUtils.convertRCMessage(message);
            NativeIOS.im_send_media_message(ref imMsg, pushContent, pushData, OnSendMessageCallback);
            RCIMUtils.FreeImMessageContent(ref imMsg);
        }

        public override void DownloadMediaMessage(RCMessage rcMsg)
        {
            im_message imMsg = RCIMUtils.convertRCMessage(rcMsg);
            NativeIOS.im_download_media_message(ref imMsg, OnDownloadMediaMessageProgress,
                OnDownloadMediaMessageCallbackSucceed, OnDownloadMediaMessageCallbackFailed,
                OnDownloadMediaMessageCallbackCanceled);
            RCIMUtils.FreeImMessageContent(ref imMsg);
        }

        public override void InsertIncomingMessage(RCConversationType type, string targetId, string senderId,
            RCReceivedStatus receivedStatus, RCMessageContent content, long sentTime,
            OperationCallbackWithResult<RCMessage> callback)
        {
            on_general_intptr_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGetMessageCallback;
            int recvStatus = receivedStatus.Flag;
            IntPtr imMsgContentPtr = RCIMUtils.convertConcreteRCMessageContent(content);
            NativeIOS.im_insert_incoming_message(type, targetId, senderId, recvStatus, imMsgContentPtr,
                content.ObjectName, sentTime, ref p);
        }

        public override void InsertOutgoingMessage(RCConversationType type, string targetId, RCSentStatus sentStatus,
            RCMessageContent content, long sendTime, OperationCallbackWithResult<RCMessage> callback)
        {
            on_general_intptr_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGetMessageCallback;
            IntPtr imMsgContentPtr = RCIMUtils.convertConcreteRCMessageContent(content);
            NativeIOS.im_insert_outgoing_message(type, targetId, sentStatus, imMsgContentPtr,
                content.ObjectName, sendTime, ref p);
        }

        public override void RecallMessage(RCMessage rcMsg, string pushContent,
            OperationCallbackWithResult<RCRecallNotificationMessage> callback)
        {
            on_general_intptr_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnRecallMessageCallback;

            im_message imMsg = RCIMUtils.convertRCMessage(rcMsg);
            NativeIOS.im_recall_message(ref imMsg, pushContent, ref p);
            RCIMUtils.FreeImMessageContent(ref imMsg);
        }

        public override void ForwardMessageByStep(RCConversationType type, string targetId, RCMessage message)
        {
            var msgContent = message.Content;
            msgContent.SendUserInfo = null;
            SendMessage(type, targetId, msgContent);
        }

        public override void GetChatRoomHistoryMessages(string targetId, long recordTime, int count, bool asc,
            OperationCallbackWithResult<RCChatRoomHistoryMessageResult> callback)
        {
            on_result_chatroom_history_messages_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnImGetChatRoomHistoryMessagesCallback;

            int order = asc ? 1 : 0;
            NativeIOS.im_get_chatroom_messages(targetId, recordTime, order, count, ref p);
        }

        public override void GetHistoryMessages(RCConversationType type, string targetId, Int64 lastMessageId,
            int count,
            OperationCallbackWithResult<IList<RCMessage>> callback)
        {
            on_general_intptr_list_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGetMessageListCallback;
            NativeIOS.im_get_history_messages(type, targetId, lastMessageId, count, ref p);
        }

        public override void GetHistoryMessages(RCConversationType type, string targetId, long sentTime,
            int beforeCount, int afterCount, OperationCallbackWithResult<IList<RCMessage>> callback)
        {
            on_general_intptr_list_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGetMessageListCallback;
            NativeIOS.im_get_history_message(type, targetId, sentTime, beforeCount, afterCount, ref p);
        }

        public override void GetRemoteHistoryMessages(RCConversationType type, string targetId, long dateTime,
            int count, OperationCallbackWithResult<IList<RCMessage>> callback)
        {
            on_general_intptr_list_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGetMessageListCallback;
            NativeIOS.im_get_remote_history_messages(type, targetId, dateTime, count, ref p);
        }

        public override void SearchMessages(RCConversationType type, string targetId, string keyword, int count,
            long beginTime, OperationCallbackWithResult<IList<RCMessage>> callback)
        {
            on_general_intptr_list_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGetMessageListCallback;
            NativeIOS.im_search_messages(type, targetId, keyword, count, beginTime, ref p);
        }

        public override void DeleteMessages(Int64[] messageIdList, OperationCallbackWithResult<bool> callback)
        {
            on_general_bool_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralBoolCallback;
            int count = messageIdList.Length;
            NativeIOS.im_delete_messages(messageIdList, count, ref p);
        }

        public override void ClearMessages(RCConversationType type, string targetId,
            OperationCallbackWithResult<bool> callback)
        {
            on_general_bool_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralBoolCallback;
            NativeIOS.im_clear_messages(type, targetId, ref p);
        }

        public override void ClearHistoryMessages(RCConversationType type, string targetId, long recordTime,
            bool clearRemote, OperationCallback callback)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;
            NativeIOS.im_clear_history_messages(type, targetId, recordTime, clearRemote, ref p);
        }

        public override void SetMessageReceivedStatus(Int64 messageId, RCReceivedStatus status,
            OperationCallbackWithResult<bool> callback)
        {
            on_general_bool_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralBoolCallback;

            int recvStatus = status.Flag;
            NativeIOS.im_set_message_received_status(messageId, recvStatus, ref p);
        }

        public override void SetMessageSentStatus(Int64 messageId, RCSentStatus sentStatus,
            OperationCallbackWithResult<bool> callback)
        {
            on_general_bool_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralBoolCallback;
            NativeIOS.im_set_message_sent_status(messageId, sentStatus, ref p);
        }

        public override void SendReadReceiptMessage(RCConversationType type, string targetId, long timestamp,
            OperationCallback callback)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;
            NativeIOS.im_send_read_receipt_message(type, targetId, timestamp, ref p);
        }

        public override void SendReadReceiptRequest(RCMessage message, OperationCallback callback)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;

            im_message imMsg = RCIMUtils.convertRCMessage(message);
            NativeIOS.im_send_read_receipt_request(ref imMsg, ref p);
            RCIMUtils.FreeImMessageContent(ref imMsg);
        }

        public override void SendReadReceiptResponse(RCConversationType type, string targetId,
            IList<RCMessage> messageList, OperationCallback callback)
        {
            on_general_callback_proxy p;
            p.callback = OnGeneralCallback;
            p.handle = RCIMUtils.AddCallback(callback);

            List<RCMessage> rcMsgList = new List<RCMessage>(messageList);
            List<im_message> imMsgList = new List<im_message>();
            foreach (var rcMsg in rcMsgList)
            {
                im_message imMsg = RCIMUtils.convertRCMessage(rcMsg);
                imMsgList.Add(imMsg);
            }

            im_message[] imMsgArray = imMsgList.ToArray();
            int imMsgsLen = imMsgArray.Length;
            NativeIOS.im_send_read_receipt_response(type, targetId, ref imMsgArray, imMsgsLen, ref p);
            for (int i = 0; i < imMsgsLen; i++)
            {
                im_message imMsg = imMsgArray[i];
                RCIMUtils.FreeImMessageContent(ref imMsg);
            }
        }

        public override void UpdateMessageExpansion(string messageUid, IDictionary<string, string> expansionDic,
            OperationCallback callback)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;

            var keyList = new List<string>();
            var valueList = new List<string>();
            foreach (var pairKV in expansionDic)
            {
                keyList.Add(pairKV.Key);
                valueList.Add(pairKV.Value);
            }

            string[] keyArray = keyList.ToArray();
            string[] valueArray = valueList.ToArray();
            int length = expansionDic.Count;

            NativeIOS.im_update_message_expansion(messageUid, keyArray, valueArray, length, ref p);
        }

        public override void RemoveMessageExpansionForKey(string messageUid, IList<string> keyList,
            OperationCallback callback)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;

            var keys = new List<string>(keyList);
            string[] keyArray = keys.ToArray();
            int length = keyArray.Length;
            NativeIOS.im_remove_message_expansion_for_key(messageUid, keyArray, length, ref p);
        }

        public override void GetFirstUnreadMessage(RCConversationType type, string targetId,
            OperationCallbackWithResult<RCMessage> callback)
        {
            on_general_intptr_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGetMessageCallback;
            NativeIOS.im_get_first_unread_message(type, targetId, ref p);
        }

        public override void GetUnreadMentionedMessages(RCConversationType type, string targetId,
            OperationCallbackWithResult<IList<RCMessage>> callback)
        {
            on_general_intptr_list_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGetMessageListCallback;
            NativeIOS.im_get_unread_mentioned_messages(type, targetId, ref p);
        }

        public override void SetMessageExtra(Int64 messageId, string extra, OperationCallbackWithResult<bool> callback)
        {
            on_general_bool_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralBoolCallback;
            NativeIOS.im_set_message_extra(messageId, extra, ref p);
        }

        public override void DeleteRemoteMessages(RCConversationType type, string targetId,
            IList<RCMessage> messageList, OperationCallback callback)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;

            List<RCMessage> rcMsgList = new List<RCMessage>(messageList);
            List<im_message> imMsgList = new List<im_message>();
            foreach (var rcMsg in rcMsgList)
            {
                im_message imMsg = RCIMUtils.convertRCMessage(rcMsg);
                imMsgList.Add(imMsg);
            }

            im_message[] imMsgArray = imMsgList.ToArray();
            int imMsgsLen = imMsgArray.Length;

            NativeIOS.im_delete_remote_messages(type, targetId, imMsgArray, imMsgsLen, ref p);
            for (int i = 0; i < imMsgsLen; i++)
            {
                im_message imMsg = imMsgArray[i];
                RCIMUtils.FreeImMessageContent(ref imMsg);
            }
        }

        public override void AddToBlackList(string userId, OperationCallback callback)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;
            NativeIOS.im_add_to_black_list(userId, ref p);
        }

        public override void RemoveFromBlackList(string userId, OperationCallback callback)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;
            NativeIOS.im_remove_from_black_list(userId, ref p);
        }

        public override void GetBlackList(OperationCallbackWithResult<IList<string>> callback)
        {
            on_general_intptr_list_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGetBlackListCallback;
            NativeIOS.im_get_black_list(ref p);
        }

        public override void GetBlackListStatus(string userId, OperationCallbackWithResult<RCBlackListStatus> callback)
        {
            on_general_int_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGetBlackListStatusCallback;
            NativeIOS.im_get_black_list_status(userId, ref p);
        }

        public override void SetKickReconnectedDevice(bool kick)
        {
            NativeIOS.im_set_reconnect_kick_enable(kick);
        }

        public override long GetDeltaTime()
        {
            return NativeIOS.im_get_delta_time();
        }

        public override void SyncConversationReadStatus(RCConversationType type, string targetId, long timestamp,
            OperationCallback callback)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;
            NativeIOS.im_sync_conversation_read_status(type, targetId, timestamp, ref p);
        }

        public override void GetMessages(RCConversationType type, string targetId, RCHistoryMessageOption option,
            OperationCallbackWithResult<IList<RCMessage>> callback)
        {
            on_general_intptr_list_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGetMessageListCallback;

            Int64 timeStamp = option.DateTime;
            int count = option.Count;
            int order = (int) option.PullOrder;
            NativeIOS.im_get_messages(type, targetId, timeStamp, order, count, ref p);
        }

        public override void JoinChatRoom(string roomId, OperationCallback callback, int msgCount = 10)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;
            NativeIOS.im_join_chatroom(roomId, ref p, msgCount);
        }

        public override void QuitChatRoom(string roomId, OperationCallback callback)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;
            NativeIOS.im_quit_chatroom(roomId, ref p);
        }

        public override void JoinExistChatRoom(string roomId, OperationCallback callback, int msgCount = 10)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;
            NativeIOS.im_join_exist_chatroom(roomId, msgCount, ref p);
        }

        public override void GetChatRoomInfo(string roomId, int memberCount, RCChatRoomMemberOrder order,
            OperationCallbackWithResult<RCChatRoomInfo> callback)
        {
            on_general_intptr_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGetChatRoomInfoCallback;
            NativeIOS.im_get_chatroom_info(roomId, memberCount, (int) order, ref p);
        }

        public override void SetChatRoomEntry(string roomId, string key, string value, bool sendNotification,
            bool autoRemove, string notificationExtra, OperationCallback callback)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;
            NativeIOS.im_set_chatroom_entry(false, roomId, key, value, sendNotification, autoRemove,
                notificationExtra, ref p);
        }

        public override void ForceSetChatRoomEntry(string roomId, string key, string value, bool sendNotification,
            bool autoRemove, string notificationExtra, OperationCallback callback)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;
            NativeIOS.im_set_chatroom_entry(true, roomId, key, value, sendNotification, autoRemove,
                notificationExtra, ref p);
        }

        public override void RemoveChatRoomEntry(string roomId, string key, bool sendNotification,
            string notificationExtra, OperationCallback callback)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;
            NativeIOS.im_remove_chatroom_entry(false, roomId, key, sendNotification, notificationExtra, ref p);
        }

        public override void ForceRemoveChatRoomEntry(string roomId, string key, bool sendNotification,
            string notificationExtra, OperationCallback callback)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;
            NativeIOS.im_remove_chatroom_entry(true, roomId, key, sendNotification, notificationExtra, ref p);
        }

        public override void GetChatRoomEntry(string roomId, string key,
            OperationCallbackWithResult<IDictionary<string, string>> callback)
        {
            on_result_chat_room_entry_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnImChatRoomGetEntryCallback;
            NativeIOS.im_get_chatroom_entry(roomId, key, ref p);
        }

        public override void GetChatRoomAllEntries(string roomId,
            OperationCallbackWithResult<IDictionary<string, string>> callback)
        {
            on_result_chat_room_all_entries_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnImChatRoomGetAllEntriesCallback;
            NativeIOS.im_get_all_chatroom_entries(roomId, ref p);
        }

        public override void GetNotificationQuietHours(
            OperationCallbackWithResult<RCNotificationQuietHourInfo> callback)
        {
            on_result_notification_quiet_info_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnImGetNotificationQuietHoursCallback;
            NativeIOS.im_get_notification_quiet_hours(ref p);
        }

        public override void SetNotificationQuietHours(string startTime, int spanMinutes, OperationCallback callback)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;
            NativeIOS.im_set_notification_quiet_hours(startTime, spanMinutes, ref p);
        }

        public override void RemoveNotificationQuietHours(OperationCallback callback)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;
            NativeIOS.im_remove_notification_quiet_hours(ref p);
        }

        public override void setDeviceToken(String deviceToken)
        {
            NativeIOS.im_set_devicetoken(deviceToken);
        }

        public override void AddTag(RCTagInfo tagInfo, OperationCallback callback)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;

            im_tag_info imTagInfo = new im_tag_info(tagInfo);
            NativeIOS.im_add_tag(ref imTagInfo, ref p);
        }

        public override void UpdateTag(RCTagInfo tagInfo, OperationCallback callback)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;

            im_tag_info imTagInfo = new im_tag_info(tagInfo);
            NativeIOS.im_update_tag(ref imTagInfo, ref p);
        }

        public override void RemoveTag(RCTagInfo tagInfo, OperationCallback callback)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;

            im_tag_info imTagInfo = new im_tag_info(tagInfo);
            NativeIOS.im_remove_tag(ref imTagInfo, ref p);
        }

        public override void GetTags(OperationCallbackWithResult<IList<RCTagInfo>> callback)
        {
            on_general_intptr_list_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGetTagsCallback;
            NativeIOS.im_get_tags(ref p);
        }

        public override void AddConversationsToTag(string tagId, IList<RCConversationIdentifier> conversations,
            OperationCallback callback)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;

            var rcConvIds = conversations.ToArray();
            int count = rcConvIds.Length;
            int convPtrSize = Marshal.SizeOf<im_conversation_identifier>();
            IntPtr ptr = Marshal.AllocHGlobal(convPtrSize * count);
            for (int i = 0; i < count; i++)
            {
                var imConvId = new im_conversation_identifier(rcConvIds[i]);
                Marshal.StructureToPtr(imConvId, ptr + (convPtrSize * i), false);
            }

            NativeIOS.im_add_conversations_to_tag(ptr, count, tagId, ref p);
            Marshal.FreeHGlobal(ptr);
        }

        public override void GetConversationsFromTagByPage(string tagId, long timestamp, int count,
            OperationCallbackWithResult<IList<RCConversation>> callback)
        {
            on_general_intptr_list_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGetConversationListCallback;
            NativeIOS.im_get_conversation_list_from_tag_by_page(tagId, timestamp, count, ref p);
        }

        public override void GetTagsFromConversation(RCConversationType type, string targetId,
            OperationCallbackWithResult<IList<RCConversationTagInfo>> callback)
        {
            on_general_intptr_list_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGetConversationTagInfoListCallback;
            NativeIOS.im_get_tags_from_conversation(type, targetId, ref p);
        }

        public override void RemoveConversationFromTag(string tagId,
            IList<RCConversationIdentifier> conversationIdentifiers, OperationCallback callback)
        {
            on_general_callback_proxy p;
            p.handle = RCIMUtils.AddCallback(callback);
            p.callback = OnGeneralCallback;

            var imConvIds = new List<im_conversation_identifier>();
            foreach (var rcConversationIdentifier in conversationIdentifiers)
            {
                var imConvId = new im_conversation_identifier(rcConversationIdentifier);
                imConvIds.Add(imConvId);
            }

            int count = conversationIdentifiers.Count;
            RCIMUtils.StructureListToPtr(imConvIds, out var ptr);

            NativeIOS.im_remove_conversations_from_tag(tagId, ptr, count, ref p);
        }

        public override void SetServerInfo(string naviServer, string fileServer)
        {
            NativeIOS.im_set_server_info(naviServer, fileServer);
        }

        public override void SetImageCompressConfig(double maxSize, double minSize, double quality)
        {
            NativeIOS.im_image_compress_config(maxSize, minSize, quality);
        }

        #endregion
    }
}

#endif