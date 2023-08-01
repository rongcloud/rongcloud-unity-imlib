#if UNITY_IOS
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class NativeIOS
    {
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_create_engine(string appkey, IntPtr options);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_engine_destory();
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_set_device_token(string token);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_connect(string token, int timeout, ref ios_connect_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_disconnect(bool receivePush);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr im_create_text_message(RCIMConversationType type, string targetId, string channelId,
                                                             string text);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_free_text_message(IntPtr ptr);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr im_create_image_message(RCIMConversationType type, string targetId, string channelId,
                                                              string path);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_free_image_message(IntPtr ptr);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr im_create_file_message(RCIMConversationType type, string targetId, string channelId,
                                                             string path);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_free_file_message(IntPtr ptr);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr im_create_sight_message(RCIMConversationType type, string targetId, string channelId,
                                                              string path, int duration);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_free_sight_message(IntPtr ptr);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr im_create_voice_message(RCIMConversationType type, string targetId, string channelId,
                                                              string path, int duration);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_free_voice_message(IntPtr ptr);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr im_create_reference_message(RCIMConversationType type, string targetId,
                                                                  string channelId, ref ios_class_warpper referenceMessage,
                                                                  string text);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_free_reference_message(IntPtr ptr);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr im_create_gif_message(RCIMConversationType type, string targetId, string channelId,
                                                            string path);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_free_gif_message(IntPtr ptr);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr im_create_custom_message(RCIMConversationType type, string targetId, string channelId,
                                                               RCIMCustomMessagePolicy policy, string messageIdentifier,
                                                               ref ios_c_map_item[] fields, int fields_count);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_free_custom_message(IntPtr ptr);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr im_create_location_message(RCIMConversationType type, string targetId,
                                                                 string channelId, double longitude, double latitude,
                                                                 string poiName, string thumbnailPath);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_free_location_message(IntPtr ptr);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_send_message(ref ios_class_warpper message, ref ios_send_message_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_send_media_message(ref ios_class_warpper message,
                                                         ref ios_send_media_message_proxy listener);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_cancel_sending_media_message(ref ios_class_warpper message,
                                                                   ref ios_cancel_sending_media_message_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_download_media_message(ref ios_class_warpper message,
                                                             ref ios_download_media_message_proxy listener);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_cancel_downloading_media_message(
            ref ios_class_warpper message, ref ios_cancel_downloading_media_message_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_conversation(RCIMConversationType type, string targetId, string channelId);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_conversation(RCIMConversationType type, string targetId, string channelId,
                                                       ref ios_get_conversation_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_conversations(RCIMConversationType[] conversationTypes,
                                                         int conversationTypes_count, string channelId, long startTime,
                                                         int count);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_conversations(RCIMConversationType[] conversationTypes,
                                                        int conversationTypes_count, string channelId, long startTime,
                                                        int count, ref ios_get_conversations_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_remove_conversation(RCIMConversationType type, string targetId, string channelId,
                                                          ref ios_remove_conversation_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_remove_conversations(RCIMConversationType[] conversationTypes,
                                                           int conversationTypes_count, string channelId,
                                                           ref ios_remove_conversations_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_unread_count(RCIMConversationType type, string targetId, string channelId);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_unread_count(RCIMConversationType type, string targetId, string channelId,
                                                       ref ios_get_unread_count_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_total_unread_count(string channelId);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_total_unread_count(string channelId,
                                                             ref ios_get_total_unread_count_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_unread_mentioned_count(RCIMConversationType type, string targetId,
                                                                  string channelId);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_unread_mentioned_count(RCIMConversationType type, string targetId,
                                                                 string channelId,
                                                                 ref ios_get_unread_mentioned_count_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_ultra_group_all_unread_count();
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_ultra_group_all_unread_count(
            ref ios_get_ultra_group_all_unread_count_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_ultra_group_all_unread_mentioned_count();
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_ultra_group_all_unread_mentioned_count(
            ref ios_get_ultra_group_all_unread_mentioned_count_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_ultra_group_unread_count(string targetId);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_ultra_group_unread_count(string targetId,
                                                                   ref ios_get_ultra_group_unread_count_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_ultra_group_unread_mentioned_count(string targetId);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_ultra_group_unread_mentioned_count(
            string targetId, ref ios_get_ultra_group_unread_mentioned_count_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_unread_count_by_conversation_types(RCIMConversationType[] conversationTypes,
                                                                              int conversationTypes_count, string channelId,
                                                                              bool contain);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_unread_count_by_conversation_types(
            RCIMConversationType[] conversationTypes, int conversationTypes_count, string channelId, bool contain,
            ref ios_get_unread_count_by_conversation_types_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_clear_unread_count(RCIMConversationType type, string targetId, string channelId,
                                                         long timestamp, ref ios_clear_unread_count_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_save_draft_message(RCIMConversationType type, string targetId, string channelId,
                                                         string draft, ref ios_save_draft_message_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_draft_message(RCIMConversationType type, string targetId, string channelId);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_draft_message(RCIMConversationType type, string targetId, string channelId,
                                                        ref ios_get_draft_message_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_clear_draft_message(RCIMConversationType type, string targetId, string channelId,
                                                          ref ios_clear_draft_message_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_blocked_conversations(RCIMConversationType[] conversationTypes,
                                                                 int conversationTypes_count, string channelId);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_blocked_conversations(RCIMConversationType[] conversationTypes,
                                                                int conversationTypes_count, string channelId,
                                                                ref ios_get_blocked_conversations_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_change_conversation_top_status(RCIMConversationType type, string targetId,
                                                                     string channelId, bool top,
                                                                     ref ios_change_conversation_top_status_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_conversation_top_status(RCIMConversationType type, string targetId,
                                                                   string channelId);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_conversation_top_status(RCIMConversationType type, string targetId,
                                                                  string channelId,
                                                                  ref ios_get_conversation_top_status_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_sync_conversation_read_status(RCIMConversationType type, string targetId,
                                                                    string channelId, long timestamp,
                                                                    ref ios_sync_conversation_read_status_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_send_typing_status(RCIMConversationType type, string targetId, string channelId,
                                                         string currentType);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_messages(RCIMConversationType type, string targetId, string channelId,
                                                    long sentTime, RCIMTimeOrder order, RCIMMessageOperationPolicy policy,
                                                    int count);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_messages(RCIMConversationType type, string targetId, string channelId,
                                                   long sentTime, RCIMTimeOrder order, RCIMMessageOperationPolicy policy,
                                                   int count, ref ios_get_messages_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_message_by_id(int messageId, ref ios_get_message_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_message_by_uid(string messageUId, ref ios_get_message_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_first_unread_message(RCIMConversationType type, string targetId,
                                                                string channelId);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_first_unread_message(RCIMConversationType type, string targetId, string channelId,
                                                               ref ios_get_first_unread_message_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_unread_mentioned_messages(RCIMConversationType type, string targetId,
                                                                     string channelId);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_unread_mentioned_messages(RCIMConversationType type, string targetId,
                                                                    string channelId,
                                                                    ref ios_get_unread_mentioned_messages_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_insert_message(ref ios_class_warpper message, ref ios_insert_message_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_insert_messages(ref ios_class_warpper[] messages, int messages_count,
                                                      ref ios_insert_messages_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_clear_messages(RCIMConversationType type, string targetId, string channelId,
                                                     long timestamp, RCIMMessageOperationPolicy policy,
                                                     ref ios_clear_messages_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_delete_local_messages(ref ios_class_warpper[] messages, int messages_count,
                                                            ref ios_delete_local_messages_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_delete_messages(RCIMConversationType type, string targetId, string channelId,
                                                      ref ios_class_warpper[] messages, int messages_count,
                                                      ref ios_delete_messages_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_recall_message(ref ios_class_warpper message, ref ios_recall_message_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_send_private_read_receipt_message(
            string targetId, string channelId, long timestamp, ref ios_send_private_read_receipt_message_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_send_group_read_receipt_request(
            ref ios_class_warpper message, ref ios_send_group_read_receipt_request_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_send_group_read_receipt_response(
            string targetId, string channelId, ref ios_class_warpper[] messages, int messages_count,
            ref ios_send_group_read_receipt_response_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_update_message_expansion(string messageUId, ref ios_c_map_item[] expansion,
                                                               int expansion_count,
                                                               ref ios_update_message_expansion_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_remove_message_expansion_for_keys(
            string messageUId, string[] keys, int keys_count, ref ios_remove_message_expansion_for_keys_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_change_message_sent_status(int messageId, RCIMSentStatus sentStatus,
                                                                 ref ios_change_message_sent_status_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_change_message_receive_status(int messageId, RCIMReceivedStatus receivedStatus,
                                                                    ref ios_change_message_received_status_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_join_chat_room(string targetId, int messageCount, bool autoCreate,
                                                     ref ios_join_chat_room_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_leave_chat_room(string targetId, ref ios_leave_chat_room_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_chat_room_messages(string targetId, long timestamp, RCIMTimeOrder order,
                                                              int count);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_chat_room_messages(string targetId, long timestamp, RCIMTimeOrder order,
                                                             int count, ref ios_get_chat_room_messages_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_add_chat_room_entry(string targetId, string key, string value, bool deleteWhenLeft,
                                                          bool overwrite, ref ios_add_chat_room_entry_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_add_chat_room_entries(string targetId, ref ios_c_map_item[] entries,
                                                            int entries_count, bool deleteWhenLeft, bool overwrite,
                                                            ref ios_add_chat_room_entries_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_chat_room_entry(string targetId, string key);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_chat_room_entry(string targetId, string key,
                                                          ref ios_get_chat_room_entry_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_chat_room_all_entries(string targetId);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_chat_room_all_entries(string targetId,
                                                                ref ios_get_chat_room_all_entries_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_remove_chat_room_entry(string targetId, string key, bool force,
                                                             ref ios_remove_chat_room_entry_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_remove_chat_room_entries(string targetId, string[] keys, int keys_count, bool force,
                                                               ref ios_remove_chat_room_entries_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_add_to_blacklist(string userId, ref ios_add_to_blacklist_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_remove_from_blacklist(string userId, ref ios_remove_from_blacklist_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_blacklist_status(string userId);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_blacklist_status(string userId, ref ios_get_blacklist_status_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_blacklist();
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_blacklist(ref ios_get_blacklist_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_search_messages(RCIMConversationType type, string targetId, string channelId,
                                                      string keyword, long startTime, int count,
                                                      ref ios_search_messages_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_search_messages_by_time_range(RCIMConversationType type, string targetId,
                                                                    string channelId, string keyword, long startTime,
                                                                    long endTime, int offset, int count,
                                                                    ref ios_search_messages_by_time_range_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_search_messages_by_user_id(string userId, RCIMConversationType type, string targetId,
                                                                 string channelId, long startTime, int count,
                                                                 ref ios_search_messages_by_user_id_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_search_conversations(RCIMConversationType[] conversationTypes,
                                                           int conversationTypes_count, string channelId,
                                                           RCIMMessageType[] messageTypes, int messageTypes_count,
                                                           string keyword, ref ios_search_conversations_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_change_notification_quiet_hours(
            string startTime, int spanMinutes, RCIMPushNotificationQuietHoursLevel level,
            ref ios_change_notification_quiet_hours_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_remove_notification_quiet_hours(
            ref ios_remove_notification_quiet_hours_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_notification_quiet_hours();
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_notification_quiet_hours(ref ios_get_notification_quiet_hours_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_change_conversation_notification_level(
            RCIMConversationType type, string targetId, string channelId, RCIMPushNotificationLevel level,
            ref ios_change_conversation_notification_level_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_conversation_notification_level(RCIMConversationType type, string targetId,
                                                                           string channelId);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_conversation_notification_level(
            RCIMConversationType type, string targetId, string channelId,
            ref ios_get_conversation_notification_level_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_change_conversation_type_notification_level(
            RCIMConversationType type, RCIMPushNotificationLevel level,
            ref ios_change_conversation_type_notification_level_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_conversation_type_notification_level(RCIMConversationType type);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_conversation_type_notification_level(
            RCIMConversationType type, ref ios_get_conversation_type_notification_level_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_change_ultra_group_default_notification_level(
            string targetId, RCIMPushNotificationLevel level,
            ref ios_change_ultra_group_default_notification_level_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_ultra_group_default_notification_level(string targetId);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_ultra_group_default_notification_level(
            string targetId, ref ios_get_ultra_group_default_notification_level_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_change_ultra_group_channel_default_notification_level(
            string targetId, string channelId, RCIMPushNotificationLevel level,
            ref ios_change_ultra_group_channel_default_notification_level_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_ultra_group_channel_default_notification_level(string targetId,
                                                                                          string channelId);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_ultra_group_channel_default_notification_level(
            string targetId, string channelId, ref ios_get_ultra_group_channel_default_notification_level_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_change_push_content_show_status(
            bool showContent, ref ios_change_push_content_show_status_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_change_push_language(string language, ref ios_change_push_language_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_change_push_receive_status(bool receive,
                                                                 ref ios_change_push_receive_status_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_send_group_message_to_designated_users(
            ref ios_class_warpper message, string[] userIds, int userIds_count,
            ref ios_send_group_message_to_designated_users_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_message_count(RCIMConversationType type, string targetId, string channelId);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_message_count(RCIMConversationType type, string targetId, string channelId,
                                                        ref ios_get_message_count_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_top_conversations(RCIMConversationType[] conversationTypes,
                                                             int conversationTypes_count, string channelId);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_top_conversations(RCIMConversationType[] conversationTypes,
                                                            int conversationTypes_count, string channelId,
                                                            ref ios_get_top_conversations_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_sync_ultra_group_read_status(string targetId, string channelId, long timestamp,
                                                                   ref ios_sync_ultra_group_read_status_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_conversations_for_all_channel(RCIMConversationType type, string targetId);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_conversations_for_all_channel(
            RCIMConversationType type, string targetId, ref ios_get_conversations_for_all_channel_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_modify_ultra_group_message(string messageUId, ref ios_class_warpper message,
                                                                 ref ios_modify_ultra_group_message_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_recall_ultra_group_message(ref ios_class_warpper message, bool deleteRemote,
                                                                 ref ios_recall_ultra_group_message_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_clear_ultra_group_messages(string targetId, string channelId, long timestamp,
                                                                 RCIMMessageOperationPolicy policy,
                                                                 ref ios_clear_ultra_group_messages_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_send_ultra_group_typing_status(string targetId, string channelId,
                                                                     RCIMUltraGroupTypingStatus typingStatus,
                                                                     ref ios_send_ultra_group_typing_status_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_clear_ultra_group_messages_for_all_channel(
            string targetId, long timestamp, ref ios_clear_ultra_group_messages_for_all_channel_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_load_batch_remote_ultra_group_messages(ref ios_class_warpper[] messages,
                                                                             int messages_count);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_get_batch_remote_ultra_group_messages(
            ref ios_class_warpper[] messages, int messages_count,
            ref ios_get_batch_remote_ultra_group_messages_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_update_ultra_group_message_expansion(
            string messageUId, ref ios_c_map_item[] expansion, int expansion_count,
            ref ios_update_ultra_group_message_expansion_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_remove_ultra_group_message_expansion_for_keys(
            string messageUId, string[] keys, int keys_count,
            ref ios_remove_ultra_group_message_expansion_for_keys_proxy callback);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int im_change_log_level(RCIMLogLevel level);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern long im_get_delta_time();
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void im_set_IRCIMIWListener(
            OnMessageReceived onMessageReceived, OnConnectionStatusChanged onConnectionStatusChanged,
            OnConversationTopStatusSynced onConversationTopStatusSynced,
            OnConversationNotificationLevelSynced onConversationNotificationLevelSynced,
            OnRemoteMessageRecalled onRemoteMessageRecalled, OnPrivateReadReceiptReceived onPrivateReadReceiptReceived,
            OnRemoteMessageExpansionUpdated onRemoteMessageExpansionUpdated,
            OnRemoteMessageExpansionForKeyRemoved onRemoteMessageExpansionForKeyRemoved,
            OnChatRoomMemberChanged onChatRoomMemberChanged, OnTypingStatusChanged onTypingStatusChanged,
            OnConversationReadStatusSyncMessageReceived onConversationReadStatusSyncMessageReceived,
            OnChatRoomEntriesSynced onChatRoomEntriesSynced, OnChatRoomEntriesChanged onChatRoomEntriesChanged,
            OnRemoteUltraGroupMessageExpansionUpdated onRemoteUltraGroupMessageExpansionUpdated,
            OnRemoteUltraGroupMessageModified onRemoteUltraGroupMessageModified,
            OnRemoteUltraGroupMessageRecalled onRemoteUltraGroupMessageRecalled,
            OnUltraGroupReadTimeReceived onUltraGroupReadTimeReceived,
            OnUltraGroupTypingStatusChanged onUltraGroupTypingStatusChanged, OnMessageBlocked onMessageBlocked,
            OnChatRoomStatusChanged onChatRoomStatusChanged,
            OnGroupMessageReadReceiptRequestReceived onGroupMessageReadReceiptRequestReceived,
            OnGroupMessageReadReceiptResponseReceived onGroupMessageReadReceiptResponseReceived, OnConnected onConnected,
            OnDatabaseOpened onDatabaseOpened, OnConversationLoaded onConversationLoaded,
            OnConversationsLoaded onConversationsLoaded, OnConversationRemoved onConversationRemoved,
            OnConversationsRemoved onConversationsRemoved, OnTotalUnreadCountLoaded onTotalUnreadCountLoaded,
            OnUnreadCountLoaded onUnreadCountLoaded,
            OnUnreadCountByConversationTypesLoaded onUnreadCountByConversationTypesLoaded,
            OnUnreadMentionedCountLoaded onUnreadMentionedCountLoaded,
            OnUltraGroupAllUnreadCountLoaded onUltraGroupAllUnreadCountLoaded,
            OnUltraGroupAllUnreadMentionedCountLoaded onUltraGroupAllUnreadMentionedCountLoaded,
            OnUltraGroupConversationsSynced onUltraGroupConversationsSynced, OnUnreadCountCleared onUnreadCountCleared,
            OnDraftMessageSaved onDraftMessageSaved, OnDraftMessageCleared onDraftMessageCleared,
            OnDraftMessageLoaded onDraftMessageLoaded, OnBlockedConversationsLoaded onBlockedConversationsLoaded,
            OnConversationTopStatusChanged onConversationTopStatusChanged,
            OnConversationTopStatusLoaded onConversationTopStatusLoaded,
            OnConversationReadStatusSynced onConversationReadStatusSynced, OnMessageAttached onMessageAttached,
            OnMessageSent onMessageSent, OnMediaMessageAttached onMediaMessageAttached,
            OnMediaMessageSending onMediaMessageSending, OnSendingMediaMessageCanceled onSendingMediaMessageCanceled,
            OnMediaMessageSent onMediaMessageSent, OnMediaMessageDownloading onMediaMessageDownloading,
            OnMediaMessageDownloaded onMediaMessageDownloaded,
            OnDownloadingMediaMessageCanceled onDownloadingMediaMessageCanceled, OnMessagesLoaded onMessagesLoaded,
            OnUnreadMentionedMessagesLoaded onUnreadMentionedMessagesLoaded,
            OnFirstUnreadMessageLoaded onFirstUnreadMessageLoaded, OnMessageInserted onMessageInserted,
            OnMessagesInserted onMessagesInserted, OnMessagesCleared onMessagesCleared,
            OnLocalMessagesDeleted onLocalMessagesDeleted, OnMessagesDeleted onMessagesDeleted,
            OnMessageRecalled onMessageRecalled, OnPrivateReadReceiptMessageSent onPrivateReadReceiptMessageSent,
            OnMessageExpansionUpdated onMessageExpansionUpdated,
            OnMessageExpansionForKeysRemoved onMessageExpansionForKeysRemoved,
            OnMessageReceiveStatusChanged onMessageReceiveStatusChanged,
            OnMessageSentStatusChanged onMessageSentStatusChanged, OnChatRoomJoined onChatRoomJoined,
            OnChatRoomJoining onChatRoomJoining, OnChatRoomLeft onChatRoomLeft,
            OnChatRoomMessagesLoaded onChatRoomMessagesLoaded, OnChatRoomEntryAdded onChatRoomEntryAdded,
            OnChatRoomEntriesAdded onChatRoomEntriesAdded, OnChatRoomEntryLoaded onChatRoomEntryLoaded,
            OnChatRoomAllEntriesLoaded onChatRoomAllEntriesLoaded, OnChatRoomEntryRemoved onChatRoomEntryRemoved,
            OnChatRoomEntriesRemoved onChatRoomEntriesRemoved, OnBlacklistAdded onBlacklistAdded,
            OnBlacklistRemoved onBlacklistRemoved, OnBlacklistStatusLoaded onBlacklistStatusLoaded,
            OnBlacklistLoaded onBlacklistLoaded, OnMessagesSearched onMessagesSearched,
            OnMessagesSearchedByTimeRange onMessagesSearchedByTimeRange,
            OnMessagesSearchedByUserId onMessagesSearchedByUserId, OnConversationsSearched onConversationsSearched,
            OnGroupReadReceiptRequestSent onGroupReadReceiptRequestSent,
            OnGroupReadReceiptResponseSent onGroupReadReceiptResponseSent,
            OnNotificationQuietHoursChanged onNotificationQuietHoursChanged,
            OnNotificationQuietHoursRemoved onNotificationQuietHoursRemoved,
            OnNotificationQuietHoursLoaded onNotificationQuietHoursLoaded,
            OnConversationNotificationLevelChanged onConversationNotificationLevelChanged,
            OnConversationNotificationLevelLoaded onConversationNotificationLevelLoaded,
            OnConversationTypeNotificationLevelChanged onConversationTypeNotificationLevelChanged,
            OnConversationTypeNotificationLevelLoaded onConversationTypeNotificationLevelLoaded,
            OnUltraGroupDefaultNotificationLevelChanged onUltraGroupDefaultNotificationLevelChanged,
            OnUltraGroupDefaultNotificationLevelLoaded onUltraGroupDefaultNotificationLevelLoaded,
            OnUltraGroupChannelDefaultNotificationLevelChanged onUltraGroupChannelDefaultNotificationLevelChanged,
            OnUltraGroupChannelDefaultNotificationLevelLoaded onUltraGroupChannelDefaultNotificationLevelLoaded,
            OnPushContentShowStatusChanged onPushContentShowStatusChanged, OnPushLanguageChanged onPushLanguageChanged,
            OnPushReceiveStatusChanged onPushReceiveStatusChanged, OnMessageCountLoaded onMessageCountLoaded,
            OnTopConversationsLoaded onTopConversationsLoaded,
            OnGroupMessageToDesignatedUsersAttached onGroupMessageToDesignatedUsersAttached,
            OnGroupMessageToDesignatedUsersSent onGroupMessageToDesignatedUsersSent,
            OnUltraGroupReadStatusSynced onUltraGroupReadStatusSynced,
            OnConversationsLoadedForAllChannel onConversationsLoadedForAllChannel,
            OnUltraGroupUnreadMentionedCountLoaded onUltraGroupUnreadMentionedCountLoaded,
            OnUltraGroupUnreadCountLoaded onUltraGroupUnreadCountLoaded,
            OnUltraGroupMessageModified onUltraGroupMessageModified,
            OnUltraGroupMessageRecalled onUltraGroupMessageRecalled,
            OnUltraGroupMessagesCleared onUltraGroupMessagesCleared,
            OnUltraGroupMessagesClearedForAllChannel onUltraGroupMessagesClearedForAllChannel,
            OnUltraGroupTypingStatusSent onUltraGroupTypingStatusSent,
            OnBatchRemoteUltraGroupMessagesLoaded onBatchRemoteUltraGroupMessagesLoaded,
            OnUltraGroupMessageExpansionUpdated onUltraGroupMessageExpansionUpdated,
            OnUltraGroupMessageExpansionForKeysRemoved onUltraGroupMessageExpansionForKeysRemoved);
    }
}
#endif
