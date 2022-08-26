//
//  Copyright © 2021 RongCloud. All rights reserved.
//

#if UNITY_IOS
using System;
using System.Runtime.InteropServices;

namespace cn_rongcloud_im_unity
{
    public class NativeIOS
    {
        const string MyLibName = "__Internal";

        #region iOS API

        #region 初始化和连接

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_init_client_listener(
            OnImConnectionStatusChangedHandler onImConnectionStatusChanged,
            OnImMessageReceivedHandler onReceivedImMessage,
            OnImBlockedMessageInfoHandler onBlockedMessageInfo,
            OnImRecallMessageHandler onRecallMessageDidReceived,
            OnImMessageExpansionDidUpdateHandler onMessageExpansionDidUpdate,
            OnImMessageExpansionDidRemoveHandler onMessageExpansionDidRemove,
            OnImTypingStatusChangedHandler onImTypingStatusChanged,
            OnImChatRoomKvDidSyncedHandler onChatRoomKvDidSynced,
            OnImChatRoomKvAddRemovedHandler onChatRoomKvDidUpdated,
            OnImChatRoomKvAddRemovedHandler onChatRoomKvDidRemoved,
            OnImChatRoomMemberActionChangedHandler onChatRoomMemberActionChanged,

            OnImChatRoomStatusChanged_Joining_Handler onChatRoomStatusChanged_Joining,
            OnImChatRoomStatusChanged_Joined_Handler onChatRoomStatusChanged_Joined,
            OnImChatRoomStatusChanged_Reset_Handler onChatRoomStatusChanged_Reset,
            OnImChatRoomStatusChanged_Quited_Handler onChatRoomStatusChanged_Quited,
            OnImChatRoomStatusChanged_Destroyed_Handler onChatRoomStatusChanged_Destroyed,
            OnImChatRoomStatusChanged_Error_Handler onChatRoomStatusChanged_Error,

            OnImCallbackVoid onImTagChanged,
            OnImCallbackVoid onImConversationTagChanged,
            OnImReadReceiptRequestHandler onImReadReceiptRequest,
            OnImReadReceiptResponseHandler onImReadReceiptResponse,
            OnImReadReceiptReceivedHandler onImReadReceiptReceived,
            OnImMediaMessageSendProgressHandler onImMediaMessageSendProgress,
            OnImMediaMessageSendCancelHandler onImMediaMessageSendCancel);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_init(string app_key);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_logout();

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_connect(string token, ref on_general_string_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_connection_status();

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_disconnect();

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_set_server_info(string naviServer, string fileServer);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_set_current_user_info(ref im_user_info imUserInfo);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_set_reconnect_kick_enable(bool kick);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern long im_get_delta_time();

        #endregion

        #region 消息

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_send_message(ref im_message msg, OnSendMessageHandler sendMessageCallback);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_send_typing_status(RCConversationType type, string targetId,
            string typingContent);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_send_directional_message(int type, string targetId, string[] userIdArray,
            int userIdsLen, ref im_message imMsg,
            OnSendMessageHandler sendMessageCallback);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_send_media_message(ref im_message imMsg, string pushContent, string pushData,
            OnSendMessageHandler callback);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_text_message_draft(RCConversationType type, string targetId,
            ref on_general_string_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_save_text_message_draft(RCConversationType type, string targetId,
            string textDraft, ref on_general_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_clear_text_message_draft(RCConversationType type, string targetId,
            ref on_general_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_set_offline_message_duration(int duration,
            ref on_general_long_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_offline_message_duration(ref on_general_long_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_chatroom_messages(string targetId, Int64 recordTime,
            int order, int count, ref on_result_chatroom_history_messages_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_history_message(RCConversationType type, string targetId, long sentTime,
            int beforeCount, int afterCount, ref on_general_intptr_list_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_history_messages(RCConversationType type, string targetId,
            Int64 lastMessageId, int count, ref on_general_intptr_list_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_messages(RCConversationType type, string targetId, Int64 recordTime,
            int order, int count, ref on_general_intptr_list_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_message_by_uid(string messageUid,
            ref on_general_intptr_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_message(Int64 messageId, ref on_general_intptr_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_insert_incoming_message(RCConversationType type, string targetId,
            string senderId, int receivedStatus, IntPtr imMsgContentPtr, string objectName, Int64 sentTime,
            ref on_general_intptr_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_insert_outgoing_message(RCConversationType type, string targetId,
            RCSentStatus sentStatus, IntPtr imMsgContentPtr, string objectName, Int64 sendTime,
            ref on_general_intptr_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_batch_insert_message(ref im_message[] imMessages, int length,
            ref on_general_bool_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_send_read_receipt_message(RCConversationType type, string targetId,
            long timestamp, ref on_general_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_send_read_receipt_request(ref im_message imMsg,
            ref on_general_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_send_read_receipt_response(RCConversationType type, string targetId,
            ref im_message[] imMsgArray, int imMsgArrayLen, ref on_general_callback_proxy p);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_clear_history_messages(RCConversationType type, string targetId, long recordTime,
            bool clearRemote, ref on_general_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void
            im_recall_message(ref im_message imMsg, string pushContent, ref on_general_intptr_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_search_messages(RCConversationType type, string targetId, string keyword,
            int count, long beginTime, ref on_general_intptr_list_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_download_media_message(ref im_message imMsg,
            OnImDownloadMediaProgress onProgress, OnImDownloadMediaCompleted onSucceed,
            OnImDownloadMediaFailed onFailed, OnImDownloadMediaCanceled onCanceled);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_cancel_download_media_message(long messageId);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_message_begin_destruct(ref im_message imMsg);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_message_stop_destruct(ref im_message imMsg);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_remote_history_messages(RCConversationType type, string targetId,
            long dateTime, int count, ref on_general_intptr_list_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_delete_remote_messages(RCConversationType type, string targetId,
            im_message[] imMsgs, int imMsgsLen, ref on_general_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_clear_messages(RCConversationType type, string targetId,
            ref on_general_bool_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_delete_messages(Int64[] messageIds, int count,
            ref on_general_bool_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_set_message_extra(Int64 messageId, string extra,
            ref on_general_bool_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_set_message_received_status(Int64 messageId, int status,
            ref on_general_bool_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_set_message_sent_status(Int64 messageId, RCSentStatus sentStatus,
            ref on_general_bool_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_first_unread_message(RCConversationType type, string targetId,
            ref on_general_intptr_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_update_message_expansion(string messageUid, string[] keys, string[] values,
            int kvLength, ref on_general_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_remove_message_expansion_for_key(string messageUid, string[] keyArray,
            int keysLength, ref on_general_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_image_compress_config(double maxSize, double minSize, double quality);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_typing_update_seconds(int typingUpdateSeconds);

        #endregion

        #region 未读数

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_unread_count_target_id(RCConversationType type, string targetId,
            ref on_general_int_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_unread_count_by_tag(string tagId, bool containsDND,
            ref on_general_int_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_unread_count_conversation_types(RCConversationType[] conversationTypes,
            int length, bool containsDND, ref on_general_int_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_total_unread_count(ref on_general_int_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_clear_messages_unread_status(RCConversationType type, string targetId,
            ref on_general_callback_proxy context);


        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_unread_mentioned_messages(RCConversationType type, string targetId,
            ref on_general_intptr_list_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_sync_conversation_read_status(RCConversationType type, string targetId,
            Int64 timeStamp, ref on_general_callback_proxy context);

        #endregion

        #region 会话

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_conversation(int type, string targetId,
            ref on_general_intptr_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_remove_conversation(int type, string targetId,
            ref on_general_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_conversation_list(RCConversationType[] conversationTypes, int length,
            ref on_general_intptr_list_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_conversation_list_by_page(RCConversationType[] conversationTypes, int length,
            long timestamp, int count, ref on_general_intptr_list_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_top_conversation_list(RCConversationType[] conversationTypes, int length,
            ref on_general_intptr_list_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_conversation_list_from_tag_by_page(string tagId, long timestamp, int count,
            ref on_general_intptr_list_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_blocked_conversation_list(RCConversationType[] conversationTypes, int length,
            ref on_general_intptr_list_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_clear_conversations(RCConversationType[] conversationTypes, int length,
            ref on_general_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_set_conversation_notification_status(RCConversationType type, string targetId,
            RCConversationNotificationStatus notificationStatus,
            ref on_general_int_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_conversation_notification_status(RCConversationType type, string targetId,
            ref on_general_int_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_set_conversation_to_top(RCConversationType type, string targetId, bool isTop,
            ref on_general_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_set_conversation_to_top_in_tag(RCConversationType type, string targetId,
            string tagId, bool isTop, ref on_general_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_conversation_top_status_in_tag(RCConversationType type, string targetId,
            string tagId, ref on_general_bool_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_search_conversations(string keyword, RCConversationType[] convTypes, int convTypesLength ,
            string[] objectNames, int objectNamesLength, ref on_general_callback_proxy context);

        #endregion


        #region 黑名单

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_add_to_black_list(string userId, ref on_general_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_remove_from_black_list(string userId, ref on_general_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_black_list_status(string userId, ref on_general_int_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_black_list(ref on_general_intptr_list_callback_proxy context);

        #endregion

        #region 通知提醒

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_set_notification_quiet_hours(string startTime, int spanMinutes,
            ref on_general_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_remove_notification_quiet_hours(ref on_general_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_notification_quiet_hours(
            ref on_result_notification_quiet_info_callback_proxy context);

        #endregion

        #region 标签

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_add_tag(ref im_tag_info tagInfo, ref on_general_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_remove_tag(ref im_tag_info tagInfo, ref on_general_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_update_tag(ref im_tag_info tagInfo, ref on_general_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_tags(ref on_general_intptr_list_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_add_conversations_to_tag(IntPtr conversationIds, int conversationIdsLen,
            string tagId, ref on_general_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_remove_conversations_from_tag(string tagId, IntPtr convIds, int count,
            ref on_general_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_remove_tags_from_conversation();

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_tags_from_conversation(RCConversationType type, string targetId,
            ref on_general_intptr_list_callback_proxy context);

        #endregion

        #region 聊天室

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_join_chatroom(string roomId, ref on_general_callback_proxy callback,
            int msgCount);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_join_exist_chatroom(string roomId, int msgCount,
            ref on_general_callback_proxy callback);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_quit_chatroom(string roomId, ref on_general_callback_proxy callback);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_chatroom_info(string roomId, int memberCount, int order,
            ref on_general_intptr_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_set_chatroom_entry(bool is_force, string room_id, string key, string value,
            bool is_send_notification, bool auto_delete, string extra, ref on_general_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_chatroom_entry(string room_id, string key,
            ref on_result_chat_room_entry_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_get_all_chatroom_entries(string room_id,
            ref on_result_chat_room_all_entries_callback_proxy context);

        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_remove_chatroom_entry(bool is_force, string room_id, string key,
            bool is_send_notification, string extra, ref on_general_callback_proxy context);


        [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_set_devicetoken(string deviceToken);
        
        #endregion

        #endregion
    }
}
#endif