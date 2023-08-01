#if UNITY_IOS
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Runtime.InteropServices;

namespace cn_rongcloud_im_unity
{
    public partial class RCIMIOSEngine : RCIMEngine
    {
    
        public RCIMIOSEngine(string appKey, RCIMEngineOptions options = null)
        {
            RCUnityLogger.getInstance().log("RCIMIOSEngine", $"appKey={appKey},options={options}");
            if (options != null)
            {
                im_engine_options coptions = NativeConvert.toEngineOptions(options);
                IntPtr ptr = NativeUtils.GetStructPointer(coptions);
                NativeIOS.im_create_engine(appKey, ptr);
                NativeConvert.freeEngineOptions(ref coptions);
                Marshal.FreeHGlobal(ptr);
            }
            else
            {
                NativeIOS.im_create_engine(appKey, IntPtr.Zero);
            }
            NativeIOS.im_set_IRCIMIWListener(
                on_message_received, on_connection_status_changed, on_conversation_top_status_synced,
                on_conversation_notification_level_synced, on_remote_message_recalled, on_private_read_receipt_received,
                on_remote_message_expansion_updated, on_remote_message_expansion_for_key_removed,
                on_chat_room_member_changed, on_typing_status_changed, on_conversation_read_status_sync_message_received,
                on_chat_room_entries_synced, on_chat_room_entries_changed, on_remote_ultra_group_message_expansion_updated,
                on_remote_ultra_group_message_modified, on_remote_ultra_group_message_recalled,
                on_ultra_group_read_time_received, on_ultra_group_typing_status_changed, on_message_blocked,
                on_chat_room_status_changed, on_group_message_read_receipt_request_received,
                on_group_message_read_receipt_response_received, on_connected, on_database_opened, on_conversation_loaded,
                on_conversations_loaded, on_conversation_removed, on_conversations_removed, on_total_unread_count_loaded,
                on_unread_count_loaded, on_unread_count_by_conversation_types_loaded, on_unread_mentioned_count_loaded,
                on_ultra_group_all_unread_count_loaded, on_ultra_group_all_unread_mentioned_count_loaded,
                on_ultra_group_conversations_synced, on_unread_count_cleared, on_draft_message_saved,
                on_draft_message_cleared, on_draft_message_loaded, on_blocked_conversations_loaded,
                on_conversation_top_status_changed, on_conversation_top_status_loaded, on_conversation_read_status_synced,
                on_message_attached, on_message_sent, on_media_message_attached, on_media_message_sending,
                on_sending_media_message_canceled, on_media_message_sent, on_media_message_downloading,
                on_media_message_downloaded, on_downloading_media_message_canceled, on_messages_loaded,
                on_unread_mentioned_messages_loaded, on_first_unread_message_loaded, on_message_inserted,
                on_messages_inserted, on_messages_cleared, on_local_messages_deleted, on_messages_deleted,
                on_message_recalled, on_private_read_receipt_message_sent, on_message_expansion_updated,
                on_message_expansion_for_keys_removed, on_message_receive_status_changed, on_message_sent_status_changed,
                on_chat_room_joined, on_chat_room_joining, on_chat_room_left, on_chat_room_messages_loaded,
                on_chat_room_entry_added, on_chat_room_entries_added, on_chat_room_entry_loaded,
                on_chat_room_all_entries_loaded, on_chat_room_entry_removed, on_chat_room_entries_removed,
                on_blacklist_added, on_blacklist_removed, on_blacklist_status_loaded, on_blacklist_loaded,
                on_messages_searched, on_messages_searched_by_time_range, on_messages_searched_by_user_id,
                on_conversations_searched, on_group_read_receipt_request_sent, on_group_read_receipt_response_sent,
                on_notification_quiet_hours_changed, on_notification_quiet_hours_removed,
                on_notification_quiet_hours_loaded, on_conversation_notification_level_changed,
                on_conversation_notification_level_loaded, on_conversation_type_notification_level_changed,
                on_conversation_type_notification_level_loaded, on_ultra_group_default_notification_level_changed,
                on_ultra_group_default_notification_level_loaded, on_ultra_group_channel_default_notification_level_changed,
                on_ultra_group_channel_default_notification_level_loaded, on_push_content_show_status_changed,
                on_push_language_changed, on_push_receive_status_changed, on_message_count_loaded,
                on_top_conversations_loaded, on_group_message_to_designated_users_attached,
                on_group_message_to_designated_users_sent, on_ultra_group_read_status_synced,
                on_conversations_loaded_for_all_channel, on_ultra_group_unread_mentioned_count_loaded,
                on_ultra_group_unread_count_loaded, on_ultra_group_message_modified, on_ultra_group_message_recalled,
                on_ultra_group_messages_cleared, on_ultra_group_messages_cleared_for_all_channel,
                on_ultra_group_typing_status_sent, on_batch_remote_ultra_group_messages_loaded,
                on_ultra_group_message_expansion_updated, on_ultra_group_message_expansion_for_keys_removed);
        }
    
        public override void Destroy()
        {
            RCUnityLogger.getInstance().log("Destroy");
            NativeIOS.im_engine_destory();
            base.Destroy();
        }
    
        public override void SetDeviceToken(String deviceToken)
        {
            RCUnityLogger.getInstance().log("SetDeviceToken", $"deviceToken={deviceToken}");
            if (deviceToken.Length > 0)
            {
                NativeIOS.im_set_device_token(deviceToken);
            }
        }
    
#region Override RCIMIWEngine Method
    
        public override int Connect(string token, int timeout, RCIMConnectListener callback = null)
        {
            RCUnityLogger.getInstance().log("Connect", $"token={token},timeout={timeout},callback={callback}");
            ios_connect_proxy ccallback = toIOSConnectProxy(callback);
            return NativeIOS.im_connect(token, timeout, ref ccallback);
        }
    
        public override int Disconnect(bool receivePush)
        {
            RCUnityLogger.getInstance().log("Disconnect", $"receivePush={receivePush}");
            return NativeIOS.im_disconnect(receivePush);
        }
    
        public override RCIMTextMessage CreateTextMessage(RCIMConversationType type, string targetId, string channelId,
                                                          string text)
        {
            RCUnityLogger.getInstance().log("CreateTextMessage",
                                            $"type={type},targetId={targetId},channelId={channelId},text={text}");
            IntPtr ptr = NativeIOS.im_create_text_message(type, targetId, channelId, text);
            RCIMTextMessage ret = null;
            if (ptr != IntPtr.Zero)
            {
                var cvalue = Marshal.PtrToStructure<im_text_message>(ptr);
                ret = NativeConvert.fromTextMessage(ref cvalue);
                NativeIOS.im_free_text_message(ptr);
            }
            return ret;
        }
    
        public override RCIMImageMessage CreateImageMessage(RCIMConversationType type, string targetId, string channelId,
                                                            string path)
        {
            RCUnityLogger.getInstance().log("CreateImageMessage",
                                            $"type={type},targetId={targetId},channelId={channelId},path={path}");
            IntPtr ptr = NativeIOS.im_create_image_message(type, targetId, channelId, path);
            RCIMImageMessage ret = null;
            if (ptr != IntPtr.Zero)
            {
                var cvalue = Marshal.PtrToStructure<im_image_message>(ptr);
                ret = NativeConvert.fromImageMessage(ref cvalue);
                NativeIOS.im_free_image_message(ptr);
            }
            return ret;
        }
    
        public override RCIMFileMessage CreateFileMessage(RCIMConversationType type, string targetId, string channelId,
                                                          string path)
        {
            RCUnityLogger.getInstance().log("CreateFileMessage",
                                            $"type={type},targetId={targetId},channelId={channelId},path={path}");
            IntPtr ptr = NativeIOS.im_create_file_message(type, targetId, channelId, path);
            RCIMFileMessage ret = null;
            if (ptr != IntPtr.Zero)
            {
                var cvalue = Marshal.PtrToStructure<im_file_message>(ptr);
                ret = NativeConvert.fromFileMessage(ref cvalue);
                NativeIOS.im_free_file_message(ptr);
            }
            return ret;
        }
    
        public override RCIMSightMessage CreateSightMessage(RCIMConversationType type, string targetId, string channelId,
                                                            string path, int duration)
        {
            RCUnityLogger.getInstance().log(
                "CreateSightMessage",
                $"type={type},targetId={targetId},channelId={channelId},path={path},duration={duration}");
            IntPtr ptr = NativeIOS.im_create_sight_message(type, targetId, channelId, path, duration);
            RCIMSightMessage ret = null;
            if (ptr != IntPtr.Zero)
            {
                var cvalue = Marshal.PtrToStructure<im_sight_message>(ptr);
                ret = NativeConvert.fromSightMessage(ref cvalue);
                NativeIOS.im_free_sight_message(ptr);
            }
            return ret;
        }
    
        public override RCIMVoiceMessage CreateVoiceMessage(RCIMConversationType type, string targetId, string channelId,
                                                            string path, int duration)
        {
            RCUnityLogger.getInstance().log(
                "CreateVoiceMessage",
                $"type={type},targetId={targetId},channelId={channelId},path={path},duration={duration}");
            IntPtr ptr = NativeIOS.im_create_voice_message(type, targetId, channelId, path, duration);
            RCIMVoiceMessage ret = null;
            if (ptr != IntPtr.Zero)
            {
                var cvalue = Marshal.PtrToStructure<im_voice_message>(ptr);
                ret = NativeConvert.fromVoiceMessage(ref cvalue);
                NativeIOS.im_free_voice_message(ptr);
            }
            return ret;
        }
    
        public override RCIMReferenceMessage CreateReferenceMessage(RCIMConversationType type, string targetId,
                                                                    string channelId, RCIMMessage referenceMessage,
                                                                    string text)
        {
            RCUnityLogger.getInstance().log(
                "CreateReferenceMessage",
                $"type={type},targetId={targetId},channelId={channelId},referenceMessage={referenceMessage},text={text}");
            ios_class_warpper referenceMessage_wapper = NativeConvert.toMessageWapper(referenceMessage);
            IntPtr ptr =
                NativeIOS.im_create_reference_message(type, targetId, channelId, ref referenceMessage_wapper, text);
            NativeConvert.freeMessageWapper(ref referenceMessage_wapper);
            RCIMReferenceMessage ret = null;
            if (ptr != IntPtr.Zero)
            {
                var cvalue = Marshal.PtrToStructure<im_reference_message>(ptr);
                ret = NativeConvert.fromReferenceMessage(ref cvalue);
                NativeIOS.im_free_reference_message(ptr);
            }
            return ret;
        }
    
        public override RCIMGIFMessage CreateGIFMessage(RCIMConversationType type, string targetId, string channelId,
                                                        string path)
        {
            RCUnityLogger.getInstance().log("CreateGIFMessage",
                                            $"type={type},targetId={targetId},channelId={channelId},path={path}");
            IntPtr ptr = NativeIOS.im_create_gif_message(type, targetId, channelId, path);
            RCIMGIFMessage ret = null;
            if (ptr != IntPtr.Zero)
            {
                var cvalue = Marshal.PtrToStructure<im_gif_message>(ptr);
                ret = NativeConvert.fromGIFMessage(ref cvalue);
                NativeIOS.im_free_gif_message(ptr);
            }
            return ret;
        }
    
        public override RCIMCustomMessage CreateCustomMessage(RCIMConversationType type, string targetId, string channelId,
                                                              RCIMCustomMessagePolicy policy, string messageIdentifier,
                                                              Dictionary<string, string> fields)
        {
            RCUnityLogger.getInstance().log(
                "CreateCustomMessage",
                $"type={type},targetId={targetId},channelId={channelId},policy={policy},messageIdentifier={messageIdentifier},fields={fields}");
            var cfields = new List<ios_c_map_item>();
            foreach (var item in fields)
            {
                ios_c_map_item cobject;
                cobject.key = item.Key.ToString();
                cobject.value = item.Value.ToString();
                cfields.Add(cobject);
            }
            ios_c_map_item[] fields_array = null;
            if (cfields.Count > 0)
            {
                fields_array = cfields.ToArray();
            }
            IntPtr ptr = NativeIOS.im_create_custom_message(type, targetId, channelId, policy, messageIdentifier,
                                                            ref fields_array, fields_array.Length);
            RCIMCustomMessage ret = null;
            if (ptr != IntPtr.Zero)
            {
                var cvalue = Marshal.PtrToStructure<im_custom_message>(ptr);
                ret = NativeConvert.fromCustomMessage(ref cvalue);
                NativeIOS.im_free_custom_message(ptr);
            }
            return ret;
        }
    
        public override RCIMLocationMessage CreateLocationMessage(RCIMConversationType type, string targetId,
                                                                  string channelId, double longitude, double latitude,
                                                                  string poiName, string thumbnailPath)
        {
            RCUnityLogger.getInstance().log(
                "CreateLocationMessage",
                $"type={type},targetId={targetId},channelId={channelId},longitude={longitude},latitude={latitude},poiName={poiName},thumbnailPath={thumbnailPath}");
            IntPtr ptr = NativeIOS.im_create_location_message(type, targetId, channelId, longitude, latitude, poiName,
                                                              thumbnailPath);
            RCIMLocationMessage ret = null;
            if (ptr != IntPtr.Zero)
            {
                var cvalue = Marshal.PtrToStructure<im_location_message>(ptr);
                ret = NativeConvert.fromLocationMessage(ref cvalue);
                NativeIOS.im_free_location_message(ptr);
            }
            return ret;
        }
    
        public override int SendMessage(RCIMMessage message, RCIMSendMessageListener callback = null)
        {
            RCUnityLogger.getInstance().log("SendMessage", $"message={message},callback={callback}");
            ios_class_warpper message_wapper = NativeConvert.toMessageWapper(message);
            ios_send_message_proxy ccallback = toIOSSendMessageProxy(callback);
            var ret = NativeIOS.im_send_message(ref message_wapper, ref ccallback);
            NativeConvert.freeMessageWapper(ref message_wapper);
            return ret;
        }
    
        public override int SendMediaMessage(RCIMMediaMessage message, RCIMSendMediaMessageListener listener = null)
        {
            RCUnityLogger.getInstance().log("SendMediaMessage", $"message={message},listener={listener}");
            ios_class_warpper message_wapper = NativeConvert.toMediaMessageWapper(message);
            ios_send_media_message_proxy clistener = toIOSSendMediaMessageProxy(listener);
            var ret = NativeIOS.im_send_media_message(ref message_wapper, ref clistener);
            NativeConvert.freeMediaMessageWapper(ref message_wapper);
            return ret;
        }
    
        public override int CancelSendingMediaMessage(RCIMMediaMessage message,
                                                      OnCancelSendingMediaMessageCalledAction callback = null)
        {
            RCUnityLogger.getInstance().log("CancelSendingMediaMessage", $"message={message},callback={callback}");
            ios_class_warpper message_wapper = NativeConvert.toMediaMessageWapper(message);
            ios_cancel_sending_media_message_proxy ccallback = toIOSCancelSendingMediaMessageProxy(callback);
            var ret = NativeIOS.im_cancel_sending_media_message(ref message_wapper, ref ccallback);
            NativeConvert.freeMediaMessageWapper(ref message_wapper);
            return ret;
        }
    
        public override int DownloadMediaMessage(RCIMMediaMessage message, RCIMDownloadMediaMessageListener listener = null)
        {
            RCUnityLogger.getInstance().log("DownloadMediaMessage", $"message={message},listener={listener}");
            ios_class_warpper message_wapper = NativeConvert.toMediaMessageWapper(message);
            ios_download_media_message_proxy clistener = toIOSDownloadMediaMessageProxy(listener);
            var ret = NativeIOS.im_download_media_message(ref message_wapper, ref clistener);
            NativeConvert.freeMediaMessageWapper(ref message_wapper);
            return ret;
        }
    
        public override int CancelDownloadingMediaMessage(RCIMMediaMessage message,
                                                          OnCancelDownloadingMediaMessageCalledAction callback = null)
        {
            RCUnityLogger.getInstance().log("CancelDownloadingMediaMessage", $"message={message},callback={callback}");
            ios_class_warpper message_wapper = NativeConvert.toMediaMessageWapper(message);
            ios_cancel_downloading_media_message_proxy ccallback = toIOSCancelDownloadingMediaMessageProxy(callback);
            var ret = NativeIOS.im_cancel_downloading_media_message(ref message_wapper, ref ccallback);
            NativeConvert.freeMediaMessageWapper(ref message_wapper);
            return ret;
        }
    
        public override int LoadConversation(RCIMConversationType type, string targetId, string channelId)
        {
            RCUnityLogger.getInstance().log("LoadConversation", $"type={type},targetId={targetId},channelId={channelId}");
            return NativeIOS.im_load_conversation(type, targetId, channelId);
        }
    
        public override int GetConversation(RCIMConversationType type, string targetId, string channelId,
                                            RCIMGetConversationListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetConversation",
                                            $"type={type},targetId={targetId},channelId={channelId},callback={callback}");
            ios_get_conversation_proxy ccallback = toIOSGetConversationProxy(callback);
            return NativeIOS.im_get_conversation(type, targetId, channelId, ref ccallback);
        }
    
        public override int LoadConversations(List<RCIMConversationType> conversationTypes, string channelId,
                                              long startTime, int count)
        {
            RCUnityLogger.getInstance().log(
                "LoadConversations",
                $"conversationTypes={conversationTypes},channelId={channelId},startTime={startTime},count={count}");
            return NativeIOS.im_load_conversations(conversationTypes.ToArray(), conversationTypes.Count, channelId,
                                                   startTime, count);
        }
    
        public override int GetConversations(List<RCIMConversationType> conversationTypes, string channelId, long startTime,
                                             int count, RCIMGetConversationsListener callback = null)
        {
            RCUnityLogger.getInstance().log(
                "GetConversations",
                $"conversationTypes={conversationTypes},channelId={channelId},startTime={startTime},count={count},callback={callback}");
            ios_get_conversations_proxy ccallback = toIOSGetConversationsProxy(callback);
            return NativeIOS.im_get_conversations(conversationTypes.ToArray(), conversationTypes.Count, channelId,
                                                  startTime, count, ref ccallback);
        }
    
        public override int RemoveConversation(RCIMConversationType type, string targetId, string channelId,
                                               OnConversationRemovedAction callback = null)
        {
            RCUnityLogger.getInstance().log("RemoveConversation",
                                            $"type={type},targetId={targetId},channelId={channelId},callback={callback}");
            ios_remove_conversation_proxy ccallback = toIOSRemoveConversationProxy(callback);
            return NativeIOS.im_remove_conversation(type, targetId, channelId, ref ccallback);
        }
    
        public override int RemoveConversations(List<RCIMConversationType> conversationTypes, string channelId,
                                                OnConversationsRemovedAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "RemoveConversations", $"conversationTypes={conversationTypes},channelId={channelId},callback={callback}");
            ios_remove_conversations_proxy ccallback = toIOSRemoveConversationsProxy(callback);
            return NativeIOS.im_remove_conversations(conversationTypes.ToArray(), conversationTypes.Count, channelId,
                                                     ref ccallback);
        }
    
        public override int LoadUnreadCount(RCIMConversationType type, string targetId, string channelId)
        {
            RCUnityLogger.getInstance().log("LoadUnreadCount", $"type={type},targetId={targetId},channelId={channelId}");
            return NativeIOS.im_load_unread_count(type, targetId, channelId);
        }
    
        public override int GetUnreadCount(RCIMConversationType type, string targetId, string channelId,
                                           RCIMGetUnreadCountListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetUnreadCount",
                                            $"type={type},targetId={targetId},channelId={channelId},callback={callback}");
            ios_get_unread_count_proxy ccallback = toIOSGetUnreadCountProxy(callback);
            return NativeIOS.im_get_unread_count(type, targetId, channelId, ref ccallback);
        }
    
        public override int LoadTotalUnreadCount(string channelId)
        {
            RCUnityLogger.getInstance().log("LoadTotalUnreadCount", $"channelId={channelId}");
            return NativeIOS.im_load_total_unread_count(channelId);
        }
    
        public override int GetTotalUnreadCount(string channelId, RCIMGetTotalUnreadCountListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetTotalUnreadCount", $"channelId={channelId},callback={callback}");
            ios_get_total_unread_count_proxy ccallback = toIOSGetTotalUnreadCountProxy(callback);
            return NativeIOS.im_get_total_unread_count(channelId, ref ccallback);
        }
    
        public override int LoadUnreadMentionedCount(RCIMConversationType type, string targetId, string channelId)
        {
            RCUnityLogger.getInstance().log("LoadUnreadMentionedCount",
                                            $"type={type},targetId={targetId},channelId={channelId}");
            return NativeIOS.im_load_unread_mentioned_count(type, targetId, channelId);
        }
    
        public override int GetUnreadMentionedCount(RCIMConversationType type, string targetId, string channelId,
                                                    RCIMGetUnreadMentionedCountListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetUnreadMentionedCount",
                                            $"type={type},targetId={targetId},channelId={channelId},callback={callback}");
            ios_get_unread_mentioned_count_proxy ccallback = toIOSGetUnreadMentionedCountProxy(callback);
            return NativeIOS.im_get_unread_mentioned_count(type, targetId, channelId, ref ccallback);
        }
    
        public override int LoadUltraGroupAllUnreadCount()
        {
            RCUnityLogger.getInstance().log("LoadUltraGroupAllUnreadCount", $"");
            return NativeIOS.im_load_ultra_group_all_unread_count();
        }
    
        public override int GetUltraGroupAllUnreadCount(RCIMGetUltraGroupAllUnreadCountListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetUltraGroupAllUnreadCount", $"callback={callback}");
            ios_get_ultra_group_all_unread_count_proxy ccallback = toIOSGetUltraGroupAllUnreadCountProxy(callback);
            return NativeIOS.im_get_ultra_group_all_unread_count(ref ccallback);
        }
    
        public override int LoadUltraGroupAllUnreadMentionedCount()
        {
            RCUnityLogger.getInstance().log("LoadUltraGroupAllUnreadMentionedCount", $"");
            return NativeIOS.im_load_ultra_group_all_unread_mentioned_count();
        }
    
        public override int GetUltraGroupAllUnreadMentionedCount(
            RCIMGetUltraGroupAllUnreadMentionedCountListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetUltraGroupAllUnreadMentionedCount", $"callback={callback}");
            ios_get_ultra_group_all_unread_mentioned_count_proxy ccallback =
                toIOSGetUltraGroupAllUnreadMentionedCountProxy(callback);
            return NativeIOS.im_get_ultra_group_all_unread_mentioned_count(ref ccallback);
        }
    
        public override int LoadUltraGroupUnreadCount(string targetId)
        {
            RCUnityLogger.getInstance().log("LoadUltraGroupUnreadCount", $"targetId={targetId}");
            return NativeIOS.im_load_ultra_group_unread_count(targetId);
        }
    
        public override int GetUltraGroupUnreadCount(string targetId, RCIMGetUltraGroupUnreadCountListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetUltraGroupUnreadCount", $"targetId={targetId},callback={callback}");
            ios_get_ultra_group_unread_count_proxy ccallback = toIOSGetUltraGroupUnreadCountProxy(callback);
            return NativeIOS.im_get_ultra_group_unread_count(targetId, ref ccallback);
        }
    
        public override int LoadUltraGroupUnreadMentionedCount(string targetId)
        {
            RCUnityLogger.getInstance().log("LoadUltraGroupUnreadMentionedCount", $"targetId={targetId}");
            return NativeIOS.im_load_ultra_group_unread_mentioned_count(targetId);
        }
    
        public override int GetUltraGroupUnreadMentionedCount(string targetId,
                                                              RCIMGetUltraGroupUnreadMentionedCountListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetUltraGroupUnreadMentionedCount",
                                            $"targetId={targetId},callback={callback}");
            ios_get_ultra_group_unread_mentioned_count_proxy ccallback =
                toIOSGetUltraGroupUnreadMentionedCountProxy(callback);
            return NativeIOS.im_get_ultra_group_unread_mentioned_count(targetId, ref ccallback);
        }
    
        public override int LoadUnreadCountByConversationTypes(List<RCIMConversationType> conversationTypes,
                                                               string channelId, bool contain)
        {
            RCUnityLogger.getInstance().log(
                "LoadUnreadCountByConversationTypes",
                $"conversationTypes={conversationTypes},channelId={channelId},contain={contain}");
            return NativeIOS.im_load_unread_count_by_conversation_types(conversationTypes.ToArray(),
                                                                        conversationTypes.Count, channelId, contain);
        }
    
        public override int GetUnreadCountByConversationTypes(List<RCIMConversationType> conversationTypes,
                                                              string channelId, bool contain,
                                                              RCIMGetUnreadCountByConversationTypesListener callback = null)
        {
            RCUnityLogger.getInstance().log(
                "GetUnreadCountByConversationTypes",
                $"conversationTypes={conversationTypes},channelId={channelId},contain={contain},callback={callback}");
            ios_get_unread_count_by_conversation_types_proxy ccallback =
                toIOSGetUnreadCountByConversationTypesProxy(callback);
            return NativeIOS.im_get_unread_count_by_conversation_types(conversationTypes.ToArray(), conversationTypes.Count,
                                                                       channelId, contain, ref ccallback);
        }
    
        public override int ClearUnreadCount(RCIMConversationType type, string targetId, string channelId, long timestamp,
                                             OnUnreadCountClearedAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "ClearUnreadCount",
                $"type={type},targetId={targetId},channelId={channelId},timestamp={timestamp},callback={callback}");
            ios_clear_unread_count_proxy ccallback = toIOSClearUnreadCountProxy(callback);
            return NativeIOS.im_clear_unread_count(type, targetId, channelId, timestamp, ref ccallback);
        }
    
        public override int SaveDraftMessage(RCIMConversationType type, string targetId, string channelId, string draft,
                                             OnDraftMessageSavedAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "SaveDraftMessage",
                $"type={type},targetId={targetId},channelId={channelId},draft={draft},callback={callback}");
            ios_save_draft_message_proxy ccallback = toIOSSaveDraftMessageProxy(callback);
            return NativeIOS.im_save_draft_message(type, targetId, channelId, draft, ref ccallback);
        }
    
        public override int LoadDraftMessage(RCIMConversationType type, string targetId, string channelId)
        {
            RCUnityLogger.getInstance().log("LoadDraftMessage", $"type={type},targetId={targetId},channelId={channelId}");
            return NativeIOS.im_load_draft_message(type, targetId, channelId);
        }
    
        public override int GetDraftMessage(RCIMConversationType type, string targetId, string channelId,
                                            RCIMGetDraftMessageListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetDraftMessage",
                                            $"type={type},targetId={targetId},channelId={channelId},callback={callback}");
            ios_get_draft_message_proxy ccallback = toIOSGetDraftMessageProxy(callback);
            return NativeIOS.im_get_draft_message(type, targetId, channelId, ref ccallback);
        }
    
        public override int ClearDraftMessage(RCIMConversationType type, string targetId, string channelId,
                                              OnDraftMessageClearedAction callback = null)
        {
            RCUnityLogger.getInstance().log("ClearDraftMessage",
                                            $"type={type},targetId={targetId},channelId={channelId},callback={callback}");
            ios_clear_draft_message_proxy ccallback = toIOSClearDraftMessageProxy(callback);
            return NativeIOS.im_clear_draft_message(type, targetId, channelId, ref ccallback);
        }
    
        public override int LoadBlockedConversations(List<RCIMConversationType> conversationTypes, string channelId)
        {
            RCUnityLogger.getInstance().log("LoadBlockedConversations",
                                            $"conversationTypes={conversationTypes},channelId={channelId}");
            return NativeIOS.im_load_blocked_conversations(conversationTypes.ToArray(), conversationTypes.Count, channelId);
        }
    
        public override int GetBlockedConversations(List<RCIMConversationType> conversationTypes, string channelId,
                                                    RCIMGetBlockedConversationsListener callback = null)
        {
            RCUnityLogger.getInstance().log(
                "GetBlockedConversations",
                $"conversationTypes={conversationTypes},channelId={channelId},callback={callback}");
            ios_get_blocked_conversations_proxy ccallback = toIOSGetBlockedConversationsProxy(callback);
            return NativeIOS.im_get_blocked_conversations(conversationTypes.ToArray(), conversationTypes.Count, channelId,
                                                          ref ccallback);
        }
    
        public override int ChangeConversationTopStatus(RCIMConversationType type, string targetId, string channelId,
                                                        bool top, OnConversationTopStatusChangedAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "ChangeConversationTopStatus",
                $"type={type},targetId={targetId},channelId={channelId},top={top},callback={callback}");
            ios_change_conversation_top_status_proxy ccallback = toIOSChangeConversationTopStatusProxy(callback);
            return NativeIOS.im_change_conversation_top_status(type, targetId, channelId, top, ref ccallback);
        }
    
        public override int LoadConversationTopStatus(RCIMConversationType type, string targetId, string channelId)
        {
            RCUnityLogger.getInstance().log("LoadConversationTopStatus",
                                            $"type={type},targetId={targetId},channelId={channelId}");
            return NativeIOS.im_load_conversation_top_status(type, targetId, channelId);
        }
    
        public override int GetConversationTopStatus(RCIMConversationType type, string targetId, string channelId,
                                                     RCIMGetConversationTopStatusListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetConversationTopStatus",
                                            $"type={type},targetId={targetId},channelId={channelId},callback={callback}");
            ios_get_conversation_top_status_proxy ccallback = toIOSGetConversationTopStatusProxy(callback);
            return NativeIOS.im_get_conversation_top_status(type, targetId, channelId, ref ccallback);
        }
    
        public override int SyncConversationReadStatus(RCIMConversationType type, string targetId, string channelId,
                                                       long timestamp, OnConversationReadStatusSyncedAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "SyncConversationReadStatus",
                $"type={type},targetId={targetId},channelId={channelId},timestamp={timestamp},callback={callback}");
            ios_sync_conversation_read_status_proxy ccallback = toIOSSyncConversationReadStatusProxy(callback);
            return NativeIOS.im_sync_conversation_read_status(type, targetId, channelId, timestamp, ref ccallback);
        }
    
        public override int SendTypingStatus(RCIMConversationType type, string targetId, string channelId,
                                             string currentType)
        {
            RCUnityLogger.getInstance().log(
                "SendTypingStatus", $"type={type},targetId={targetId},channelId={channelId},currentType={currentType}");
            return NativeIOS.im_send_typing_status(type, targetId, channelId, currentType);
        }
    
        public override int LoadMessages(RCIMConversationType type, string targetId, string channelId, long sentTime,
                                         RCIMTimeOrder order, RCIMMessageOperationPolicy policy, int count)
        {
            RCUnityLogger.getInstance().log(
                "LoadMessages",
                $"type={type},targetId={targetId},channelId={channelId},sentTime={sentTime},order={order},policy={policy},count={count}");
            return NativeIOS.im_load_messages(type, targetId, channelId, sentTime, order, policy, count);
        }
    
        public override int GetMessages(RCIMConversationType type, string targetId, string channelId, long sentTime,
                                        RCIMTimeOrder order, RCIMMessageOperationPolicy policy, int count,
                                        RCIMGetMessagesListener callback = null)
        {
            RCUnityLogger.getInstance().log(
                "GetMessages",
                $"type={type},targetId={targetId},channelId={channelId},sentTime={sentTime},order={order},policy={policy},count={count},callback={callback}");
            ios_get_messages_proxy ccallback = toIOSGetMessagesProxy(callback);
            return NativeIOS.im_get_messages(type, targetId, channelId, sentTime, order, policy, count, ref ccallback);
        }
    
        public override int GetMessageById(int messageId, RCIMGetMessageListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetMessageById", $"messageId={messageId},callback={callback}");
            ios_get_message_proxy ccallback = toIOSGetMessageProxy(callback);
            return NativeIOS.im_get_message_by_id(messageId, ref ccallback);
        }
    
        public override int GetMessageByUId(string messageUId, RCIMGetMessageListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetMessageByUId", $"messageUId={messageUId},callback={callback}");
            ios_get_message_proxy ccallback = toIOSGetMessageProxy(callback);
            return NativeIOS.im_get_message_by_uid(messageUId, ref ccallback);
        }
    
        public override int LoadFirstUnreadMessage(RCIMConversationType type, string targetId, string channelId)
        {
            RCUnityLogger.getInstance().log("LoadFirstUnreadMessage",
                                            $"type={type},targetId={targetId},channelId={channelId}");
            return NativeIOS.im_load_first_unread_message(type, targetId, channelId);
        }
    
        public override int GetFirstUnreadMessage(RCIMConversationType type, string targetId, string channelId,
                                                  RCIMGetFirstUnreadMessageListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetFirstUnreadMessage",
                                            $"type={type},targetId={targetId},channelId={channelId},callback={callback}");
            ios_get_first_unread_message_proxy ccallback = toIOSGetFirstUnreadMessageProxy(callback);
            return NativeIOS.im_get_first_unread_message(type, targetId, channelId, ref ccallback);
        }
    
        public override int LoadUnreadMentionedMessages(RCIMConversationType type, string targetId, string channelId)
        {
            RCUnityLogger.getInstance().log("LoadUnreadMentionedMessages",
                                            $"type={type},targetId={targetId},channelId={channelId}");
            return NativeIOS.im_load_unread_mentioned_messages(type, targetId, channelId);
        }
    
        public override int GetUnreadMentionedMessages(RCIMConversationType type, string targetId, string channelId,
                                                       RCIMGetUnreadMentionedMessagesListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetUnreadMentionedMessages",
                                            $"type={type},targetId={targetId},channelId={channelId},callback={callback}");
            ios_get_unread_mentioned_messages_proxy ccallback = toIOSGetUnreadMentionedMessagesProxy(callback);
            return NativeIOS.im_get_unread_mentioned_messages(type, targetId, channelId, ref ccallback);
        }
    
        public override int InsertMessage(RCIMMessage message, OnMessageInsertedAction callback = null)
        {
            RCUnityLogger.getInstance().log("InsertMessage", $"message={message},callback={callback}");
            ios_class_warpper message_wapper = NativeConvert.toMessageWapper(message);
            ios_insert_message_proxy ccallback = toIOSInsertMessageProxy(callback);
            var ret = NativeIOS.im_insert_message(ref message_wapper, ref ccallback);
            NativeConvert.freeMessageWapper(ref message_wapper);
            return ret;
        }
    
        public override int InsertMessages(List<RCIMMessage> messages, OnMessagesInsertedAction callback = null)
        {
            RCUnityLogger.getInstance().log("InsertMessages", $"messages={messages},callback={callback}");
            ios_class_warpper[] cmessages = null;
            if (messages.Count > 0)
            {
                cmessages = new ios_class_warpper[messages.Count];
                for (int i = 0; i < messages.Count; i++)
                {
                    cmessages[i] = NativeConvert.toMessageWapper(messages[i]);
                }
            }
            ios_insert_messages_proxy ccallback = toIOSInsertMessagesProxy(callback);
            var ret = NativeIOS.im_insert_messages(ref cmessages, messages.Count, ref ccallback);
            if (cmessages != null)
            {
                for (int i = 0; i < cmessages.Length; i++)
                {
                    NativeConvert.freeMessageWapper(ref cmessages[i]);
                }
            }
            return ret;
        }
    
        public override int ClearMessages(RCIMConversationType type, string targetId, string channelId, long timestamp,
                                          RCIMMessageOperationPolicy policy, OnMessagesClearedAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "ClearMessages",
                $"type={type},targetId={targetId},channelId={channelId},timestamp={timestamp},policy={policy},callback={callback}");
            ios_clear_messages_proxy ccallback = toIOSClearMessagesProxy(callback);
            return NativeIOS.im_clear_messages(type, targetId, channelId, timestamp, policy, ref ccallback);
        }
    
        public override int DeleteLocalMessages(List<RCIMMessage> messages, OnLocalMessagesDeletedAction callback = null)
        {
            RCUnityLogger.getInstance().log("DeleteLocalMessages", $"messages={messages},callback={callback}");
            ios_class_warpper[] cmessages = null;
            if (messages.Count > 0)
            {
                cmessages = new ios_class_warpper[messages.Count];
                for (int i = 0; i < messages.Count; i++)
                {
                    cmessages[i] = NativeConvert.toMessageWapper(messages[i]);
                }
            }
            ios_delete_local_messages_proxy ccallback = toIOSDeleteLocalMessagesProxy(callback);
            var ret = NativeIOS.im_delete_local_messages(ref cmessages, messages.Count, ref ccallback);
            if (cmessages != null)
            {
                for (int i = 0; i < cmessages.Length; i++)
                {
                    NativeConvert.freeMessageWapper(ref cmessages[i]);
                }
            }
            return ret;
        }
    
        public override int DeleteMessages(RCIMConversationType type, string targetId, string channelId,
                                           List<RCIMMessage> messages, OnMessagesDeletedAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "DeleteMessages",
                $"type={type},targetId={targetId},channelId={channelId},messages={messages},callback={callback}");
            ios_class_warpper[] cmessages = null;
            if (messages.Count > 0)
            {
                cmessages = new ios_class_warpper[messages.Count];
                for (int i = 0; i < messages.Count; i++)
                {
                    cmessages[i] = NativeConvert.toMessageWapper(messages[i]);
                }
            }
            ios_delete_messages_proxy ccallback = toIOSDeleteMessagesProxy(callback);
            var ret = NativeIOS.im_delete_messages(type, targetId, channelId, ref cmessages, messages.Count, ref ccallback);
            if (cmessages != null)
            {
                for (int i = 0; i < cmessages.Length; i++)
                {
                    NativeConvert.freeMessageWapper(ref cmessages[i]);
                }
            }
            return ret;
        }
    
        public override int RecallMessage(RCIMMessage message, OnMessageRecalledAction callback = null)
        {
            RCUnityLogger.getInstance().log("RecallMessage", $"message={message},callback={callback}");
            ios_class_warpper message_wapper = NativeConvert.toMessageWapper(message);
            ios_recall_message_proxy ccallback = toIOSRecallMessageProxy(callback);
            var ret = NativeIOS.im_recall_message(ref message_wapper, ref ccallback);
            NativeConvert.freeMessageWapper(ref message_wapper);
            return ret;
        }
    
        public override int SendPrivateReadReceiptMessage(string targetId, string channelId, long timestamp,
                                                          OnPrivateReadReceiptMessageSentAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "SendPrivateReadReceiptMessage",
                $"targetId={targetId},channelId={channelId},timestamp={timestamp},callback={callback}");
            ios_send_private_read_receipt_message_proxy ccallback = toIOSSendPrivateReadReceiptMessageProxy(callback);
            return NativeIOS.im_send_private_read_receipt_message(targetId, channelId, timestamp, ref ccallback);
        }
    
        public override int SendGroupReadReceiptRequest(RCIMMessage message,
                                                        OnGroupReadReceiptRequestSentAction callback = null)
        {
            RCUnityLogger.getInstance().log("SendGroupReadReceiptRequest", $"message={message},callback={callback}");
            ios_class_warpper message_wapper = NativeConvert.toMessageWapper(message);
            ios_send_group_read_receipt_request_proxy ccallback = toIOSSendGroupReadReceiptRequestProxy(callback);
            var ret = NativeIOS.im_send_group_read_receipt_request(ref message_wapper, ref ccallback);
            NativeConvert.freeMessageWapper(ref message_wapper);
            return ret;
        }
    
        public override int SendGroupReadReceiptResponse(string targetId, string channelId, List<RCIMMessage> messages,
                                                         OnGroupReadReceiptResponseSentAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "SendGroupReadReceiptResponse",
                $"targetId={targetId},channelId={channelId},messages={messages},callback={callback}");
            ios_class_warpper[] cmessages = null;
            if (messages.Count > 0)
            {
                cmessages = new ios_class_warpper[messages.Count];
                for (int i = 0; i < messages.Count; i++)
                {
                    cmessages[i] = NativeConvert.toMessageWapper(messages[i]);
                }
            }
            ios_send_group_read_receipt_response_proxy ccallback = toIOSSendGroupReadReceiptResponseProxy(callback);
            var ret = NativeIOS.im_send_group_read_receipt_response(targetId, channelId, ref cmessages, messages.Count,
                                                                    ref ccallback);
            if (cmessages != null)
            {
                for (int i = 0; i < cmessages.Length; i++)
                {
                    NativeConvert.freeMessageWapper(ref cmessages[i]);
                }
            }
            return ret;
        }
    
        public override int UpdateMessageExpansion(string messageUId, Dictionary<string, string> expansion,
                                                   OnMessageExpansionUpdatedAction callback = null)
        {
            RCUnityLogger.getInstance().log("UpdateMessageExpansion",
                                            $"messageUId={messageUId},expansion={expansion},callback={callback}");
            var cexpansion = new List<ios_c_map_item>();
            foreach (var item in expansion)
            {
                ios_c_map_item cobject;
                cobject.key = item.Key.ToString();
                cobject.value = item.Value.ToString();
                cexpansion.Add(cobject);
            }
            ios_c_map_item[] expansion_array = null;
            if (cexpansion.Count > 0)
            {
                expansion_array = cexpansion.ToArray();
            }
            ios_update_message_expansion_proxy ccallback = toIOSUpdateMessageExpansionProxy(callback);
            return NativeIOS.im_update_message_expansion(messageUId, ref expansion_array, expansion_array.Length,
                                                         ref ccallback);
        }
    
        public override int RemoveMessageExpansionForKeys(string messageUId, List<string> keys,
                                                          OnMessageExpansionForKeysRemovedAction callback = null)
        {
            RCUnityLogger.getInstance().log("RemoveMessageExpansionForKeys",
                                            $"messageUId={messageUId},keys={keys},callback={callback}");
            ios_remove_message_expansion_for_keys_proxy ccallback = toIOSRemoveMessageExpansionForKeysProxy(callback);
            return NativeIOS.im_remove_message_expansion_for_keys(messageUId, keys.ToArray(), keys.Count, ref ccallback);
        }
    
        public override int ChangeMessageSentStatus(int messageId, RCIMSentStatus sentStatus,
                                                    OnMessageSentStatusChangedAction callback = null)
        {
            RCUnityLogger.getInstance().log("ChangeMessageSentStatus",
                                            $"messageId={messageId},sentStatus={sentStatus},callback={callback}");
            ios_change_message_sent_status_proxy ccallback = toIOSChangeMessageSentStatusProxy(callback);
            return NativeIOS.im_change_message_sent_status(messageId, sentStatus, ref ccallback);
        }
    
        public override int ChangeMessageReceiveStatus(int messageId, RCIMReceivedStatus receivedStatus,
                                                       OnMessageReceiveStatusChangedAction callback = null)
        {
            RCUnityLogger.getInstance().log("ChangeMessageReceiveStatus",
                                            $"messageId={messageId},receivedStatus={receivedStatus},callback={callback}");
            ios_change_message_received_status_proxy ccallback = toIOSChangeMessageReceivedStatusProxy(callback);
            return NativeIOS.im_change_message_receive_status(messageId, receivedStatus, ref ccallback);
        }
    
        public override int JoinChatRoom(string targetId, int messageCount, bool autoCreate,
                                         OnChatRoomJoinedAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "JoinChatRoom",
                $"targetId={targetId},messageCount={messageCount},autoCreate={autoCreate},callback={callback}");
            ios_join_chat_room_proxy ccallback = toIOSJoinChatRoomProxy(callback);
            return NativeIOS.im_join_chat_room(targetId, messageCount, autoCreate, ref ccallback);
        }
    
        public override int LeaveChatRoom(string targetId, OnChatRoomLeftAction callback = null)
        {
            RCUnityLogger.getInstance().log("LeaveChatRoom", $"targetId={targetId},callback={callback}");
            ios_leave_chat_room_proxy ccallback = toIOSLeaveChatRoomProxy(callback);
            return NativeIOS.im_leave_chat_room(targetId, ref ccallback);
        }
    
        public override int LoadChatRoomMessages(string targetId, long timestamp, RCIMTimeOrder order, int count)
        {
            RCUnityLogger.getInstance().log("LoadChatRoomMessages",
                                            $"targetId={targetId},timestamp={timestamp},order={order},count={count}");
            return NativeIOS.im_load_chat_room_messages(targetId, timestamp, order, count);
        }
    
        public override int GetChatRoomMessages(string targetId, long timestamp, RCIMTimeOrder order, int count,
                                                RCIMGetChatRoomMessagesListener callback = null)
        {
            RCUnityLogger.getInstance().log(
                "GetChatRoomMessages",
                $"targetId={targetId},timestamp={timestamp},order={order},count={count},callback={callback}");
            ios_get_chat_room_messages_proxy ccallback = toIOSGetChatRoomMessagesProxy(callback);
            return NativeIOS.im_get_chat_room_messages(targetId, timestamp, order, count, ref ccallback);
        }
    
        public override int AddChatRoomEntry(string targetId, string key, string value, bool deleteWhenLeft, bool overwrite,
                                             OnChatRoomEntryAddedAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "AddChatRoomEntry",
                $"targetId={targetId},key={key},value={value},deleteWhenLeft={deleteWhenLeft},overwrite={overwrite},callback={callback}");
            ios_add_chat_room_entry_proxy ccallback = toIOSAddChatRoomEntryProxy(callback);
            return NativeIOS.im_add_chat_room_entry(targetId, key, value, deleteWhenLeft, overwrite, ref ccallback);
        }
    
        public override int AddChatRoomEntries(string targetId, Dictionary<string, string> entries, bool deleteWhenLeft,
                                               bool overwrite, OnChatRoomEntriesAddedAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "AddChatRoomEntries",
                $"targetId={targetId},entries={entries},deleteWhenLeft={deleteWhenLeft},overwrite={overwrite},callback={callback}");
            var centries = new List<ios_c_map_item>();
            foreach (var item in entries)
            {
                ios_c_map_item cobject;
                cobject.key = item.Key.ToString();
                cobject.value = item.Value.ToString();
                centries.Add(cobject);
            }
            ios_c_map_item[] entries_array = null;
            if (centries.Count > 0)
            {
                entries_array = centries.ToArray();
            }
            ios_add_chat_room_entries_proxy ccallback = toIOSAddChatRoomEntriesProxy(callback);
            return NativeIOS.im_add_chat_room_entries(targetId, ref entries_array, entries_array.Length, deleteWhenLeft,
                                                      overwrite, ref ccallback);
        }
    
        public override int LoadChatRoomEntry(string targetId, string key)
        {
            RCUnityLogger.getInstance().log("LoadChatRoomEntry", $"targetId={targetId},key={key}");
            return NativeIOS.im_load_chat_room_entry(targetId, key);
        }
    
        public override int GetChatRoomEntry(string targetId, string key, RCIMGetChatRoomEntryListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetChatRoomEntry", $"targetId={targetId},key={key},callback={callback}");
            ios_get_chat_room_entry_proxy ccallback = toIOSGetChatRoomEntryProxy(callback);
            return NativeIOS.im_get_chat_room_entry(targetId, key, ref ccallback);
        }
    
        public override int LoadChatRoomAllEntries(string targetId)
        {
            RCUnityLogger.getInstance().log("LoadChatRoomAllEntries", $"targetId={targetId}");
            return NativeIOS.im_load_chat_room_all_entries(targetId);
        }
    
        public override int GetChatRoomAllEntries(string targetId, RCIMGetChatRoomAllEntriesListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetChatRoomAllEntries", $"targetId={targetId},callback={callback}");
            ios_get_chat_room_all_entries_proxy ccallback = toIOSGetChatRoomAllEntriesProxy(callback);
            return NativeIOS.im_get_chat_room_all_entries(targetId, ref ccallback);
        }
    
        public override int RemoveChatRoomEntry(string targetId, string key, bool force,
                                                OnChatRoomEntryRemovedAction callback = null)
        {
            RCUnityLogger.getInstance().log("RemoveChatRoomEntry",
                                            $"targetId={targetId},key={key},force={force},callback={callback}");
            ios_remove_chat_room_entry_proxy ccallback = toIOSRemoveChatRoomEntryProxy(callback);
            return NativeIOS.im_remove_chat_room_entry(targetId, key, force, ref ccallback);
        }
    
        public override int RemoveChatRoomEntries(string targetId, List<string> keys, bool force,
                                                  OnChatRoomEntriesRemovedAction callback = null)
        {
            RCUnityLogger.getInstance().log("RemoveChatRoomEntries",
                                            $"targetId={targetId},keys={keys},force={force},callback={callback}");
            ios_remove_chat_room_entries_proxy ccallback = toIOSRemoveChatRoomEntriesProxy(callback);
            return NativeIOS.im_remove_chat_room_entries(targetId, keys.ToArray(), keys.Count, force, ref ccallback);
        }
    
        public override int AddToBlacklist(string userId, OnBlacklistAddedAction callback = null)
        {
            RCUnityLogger.getInstance().log("AddToBlacklist", $"userId={userId},callback={callback}");
            ios_add_to_blacklist_proxy ccallback = toIOSAddToBlacklistProxy(callback);
            return NativeIOS.im_add_to_blacklist(userId, ref ccallback);
        }
    
        public override int RemoveFromBlacklist(string userId, OnBlacklistRemovedAction callback = null)
        {
            RCUnityLogger.getInstance().log("RemoveFromBlacklist", $"userId={userId},callback={callback}");
            ios_remove_from_blacklist_proxy ccallback = toIOSRemoveFromBlacklistProxy(callback);
            return NativeIOS.im_remove_from_blacklist(userId, ref ccallback);
        }
    
        public override int LoadBlacklistStatus(string userId)
        {
            RCUnityLogger.getInstance().log("LoadBlacklistStatus", $"userId={userId}");
            return NativeIOS.im_load_blacklist_status(userId);
        }
    
        public override int GetBlacklistStatus(string userId, RCIMGetBlacklistStatusListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetBlacklistStatus", $"userId={userId},callback={callback}");
            ios_get_blacklist_status_proxy ccallback = toIOSGetBlacklistStatusProxy(callback);
            return NativeIOS.im_get_blacklist_status(userId, ref ccallback);
        }
    
        public override int LoadBlacklist()
        {
            RCUnityLogger.getInstance().log("LoadBlacklist", $"");
            return NativeIOS.im_load_blacklist();
        }
    
        public override int GetBlacklist(RCIMGetBlacklistListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetBlacklist", $"callback={callback}");
            ios_get_blacklist_proxy ccallback = toIOSGetBlacklistProxy(callback);
            return NativeIOS.im_get_blacklist(ref ccallback);
        }
    
        public override int SearchMessages(RCIMConversationType type, string targetId, string channelId, string keyword,
                                           long startTime, int count, RCIMSearchMessagesListener callback = null)
        {
            RCUnityLogger.getInstance().log(
                "SearchMessages",
                $"type={type},targetId={targetId},channelId={channelId},keyword={keyword},startTime={startTime},count={count},callback={callback}");
            ios_search_messages_proxy ccallback = toIOSSearchMessagesProxy(callback);
            return NativeIOS.im_search_messages(type, targetId, channelId, keyword, startTime, count, ref ccallback);
        }
    
        public override int SearchMessagesByTimeRange(RCIMConversationType type, string targetId, string channelId,
                                                      string keyword, long startTime, long endTime, int offset, int count,
                                                      RCIMSearchMessagesByTimeRangeListener callback = null)
        {
            RCUnityLogger.getInstance().log(
                "SearchMessagesByTimeRange",
                $"type={type},targetId={targetId},channelId={channelId},keyword={keyword},startTime={startTime},endTime={endTime},offset={offset},count={count},callback={callback}");
            ios_search_messages_by_time_range_proxy ccallback = toIOSSearchMessagesByTimeRangeProxy(callback);
            return NativeIOS.im_search_messages_by_time_range(type, targetId, channelId, keyword, startTime, endTime,
                                                              offset, count, ref ccallback);
        }
    
        public override int SearchMessagesByUserId(string userId, RCIMConversationType type, string targetId,
                                                   string channelId, long startTime, int count,
                                                   RCIMSearchMessagesByUserIdListener callback = null)
        {
            RCUnityLogger.getInstance().log(
                "SearchMessagesByUserId",
                $"userId={userId},type={type},targetId={targetId},channelId={channelId},startTime={startTime},count={count},callback={callback}");
            ios_search_messages_by_user_id_proxy ccallback = toIOSSearchMessagesByUserIdProxy(callback);
            return NativeIOS.im_search_messages_by_user_id(userId, type, targetId, channelId, startTime, count,
                                                           ref ccallback);
        }
    
        public override int SearchConversations(List<RCIMConversationType> conversationTypes, string channelId,
                                                List<RCIMMessageType> messageTypes, string keyword,
                                                RCIMSearchConversationsListener callback = null)
        {
            RCUnityLogger.getInstance().log(
                "SearchConversations",
                $"conversationTypes={conversationTypes},channelId={channelId},messageTypes={messageTypes},keyword={keyword},callback={callback}");
            ios_search_conversations_proxy ccallback = toIOSSearchConversationsProxy(callback);
            return NativeIOS.im_search_conversations(conversationTypes.ToArray(), conversationTypes.Count, channelId,
                                                     messageTypes.ToArray(), messageTypes.Count, keyword, ref ccallback);
        }
    
        public override int ChangeNotificationQuietHours(string startTime, int spanMinutes,
                                                         RCIMPushNotificationQuietHoursLevel level,
                                                         OnNotificationQuietHoursChangedAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "ChangeNotificationQuietHours",
                $"startTime={startTime},spanMinutes={spanMinutes},level={level},callback={callback}");
            ios_change_notification_quiet_hours_proxy ccallback = toIOSChangeNotificationQuietHoursProxy(callback);
            return NativeIOS.im_change_notification_quiet_hours(startTime, spanMinutes, level, ref ccallback);
        }
    
        public override int RemoveNotificationQuietHours(OnNotificationQuietHoursRemovedAction callback = null)
        {
            RCUnityLogger.getInstance().log("RemoveNotificationQuietHours", $"callback={callback}");
            ios_remove_notification_quiet_hours_proxy ccallback = toIOSRemoveNotificationQuietHoursProxy(callback);
            return NativeIOS.im_remove_notification_quiet_hours(ref ccallback);
        }
    
        public override int LoadNotificationQuietHours()
        {
            RCUnityLogger.getInstance().log("LoadNotificationQuietHours", $"");
            return NativeIOS.im_load_notification_quiet_hours();
        }
    
        public override int GetNotificationQuietHours(RCIMGetNotificationQuietHoursListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetNotificationQuietHours", $"callback={callback}");
            ios_get_notification_quiet_hours_proxy ccallback = toIOSGetNotificationQuietHoursProxy(callback);
            return NativeIOS.im_get_notification_quiet_hours(ref ccallback);
        }
    
        public override int ChangeConversationNotificationLevel(
            RCIMConversationType type, string targetId, string channelId, RCIMPushNotificationLevel level,
            OnConversationNotificationLevelChangedAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "ChangeConversationNotificationLevel",
                $"type={type},targetId={targetId},channelId={channelId},level={level},callback={callback}");
            ios_change_conversation_notification_level_proxy ccallback =
                toIOSChangeConversationNotificationLevelProxy(callback);
            return NativeIOS.im_change_conversation_notification_level(type, targetId, channelId, level, ref ccallback);
        }
    
        public override int LoadConversationNotificationLevel(RCIMConversationType type, string targetId, string channelId)
        {
            RCUnityLogger.getInstance().log("LoadConversationNotificationLevel",
                                            $"type={type},targetId={targetId},channelId={channelId}");
            return NativeIOS.im_load_conversation_notification_level(type, targetId, channelId);
        }
    
        public override int GetConversationNotificationLevel(RCIMConversationType type, string targetId, string channelId,
                                                             RCIMGetConversationNotificationLevelListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetConversationNotificationLevel",
                                            $"type={type},targetId={targetId},channelId={channelId},callback={callback}");
            ios_get_conversation_notification_level_proxy ccallback = toIOSGetConversationNotificationLevelProxy(callback);
            return NativeIOS.im_get_conversation_notification_level(type, targetId, channelId, ref ccallback);
        }
    
        public override int ChangeConversationTypeNotificationLevel(
            RCIMConversationType type, RCIMPushNotificationLevel level,
            OnConversationTypeNotificationLevelChangedAction callback = null)
        {
            RCUnityLogger.getInstance().log("ChangeConversationTypeNotificationLevel",
                                            $"type={type},level={level},callback={callback}");
            ios_change_conversation_type_notification_level_proxy ccallback =
                toIOSChangeConversationTypeNotificationLevelProxy(callback);
            return NativeIOS.im_change_conversation_type_notification_level(type, level, ref ccallback);
        }
    
        public override int LoadConversationTypeNotificationLevel(RCIMConversationType type)
        {
            RCUnityLogger.getInstance().log("LoadConversationTypeNotificationLevel", $"type={type}");
            return NativeIOS.im_load_conversation_type_notification_level(type);
        }
    
        public override int GetConversationTypeNotificationLevel(
            RCIMConversationType type, RCIMGetConversationTypeNotificationLevelListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetConversationTypeNotificationLevel", $"type={type},callback={callback}");
            ios_get_conversation_type_notification_level_proxy ccallback =
                toIOSGetConversationTypeNotificationLevelProxy(callback);
            return NativeIOS.im_get_conversation_type_notification_level(type, ref ccallback);
        }
    
        public override int ChangeUltraGroupDefaultNotificationLevel(
            string targetId, RCIMPushNotificationLevel level,
            OnUltraGroupDefaultNotificationLevelChangedAction callback = null)
        {
            RCUnityLogger.getInstance().log("ChangeUltraGroupDefaultNotificationLevel",
                                            $"targetId={targetId},level={level},callback={callback}");
            ios_change_ultra_group_default_notification_level_proxy ccallback =
                toIOSChangeUltraGroupDefaultNotificationLevelProxy(callback);
            return NativeIOS.im_change_ultra_group_default_notification_level(targetId, level, ref ccallback);
        }
    
        public override int LoadUltraGroupDefaultNotificationLevel(string targetId)
        {
            RCUnityLogger.getInstance().log("LoadUltraGroupDefaultNotificationLevel", $"targetId={targetId}");
            return NativeIOS.im_load_ultra_group_default_notification_level(targetId);
        }
    
        public override int GetUltraGroupDefaultNotificationLevel(
            string targetId, RCIMGetUltraGroupDefaultNotificationLevelListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetUltraGroupDefaultNotificationLevel",
                                            $"targetId={targetId},callback={callback}");
            ios_get_ultra_group_default_notification_level_proxy ccallback =
                toIOSGetUltraGroupDefaultNotificationLevelProxy(callback);
            return NativeIOS.im_get_ultra_group_default_notification_level(targetId, ref ccallback);
        }
    
        public override int ChangeUltraGroupChannelDefaultNotificationLevel(
            string targetId, string channelId, RCIMPushNotificationLevel level,
            OnUltraGroupChannelDefaultNotificationLevelChangedAction callback = null)
        {
            RCUnityLogger.getInstance().log("ChangeUltraGroupChannelDefaultNotificationLevel",
                                            $"targetId={targetId},channelId={channelId},level={level},callback={callback}");
            ios_change_ultra_group_channel_default_notification_level_proxy ccallback =
                toIOSChangeUltraGroupChannelDefaultNotificationLevelProxy(callback);
            return NativeIOS.im_change_ultra_group_channel_default_notification_level(targetId, channelId, level,
                                                                                      ref ccallback);
        }
    
        public override int LoadUltraGroupChannelDefaultNotificationLevel(string targetId, string channelId)
        {
            RCUnityLogger.getInstance().log("LoadUltraGroupChannelDefaultNotificationLevel",
                                            $"targetId={targetId},channelId={channelId}");
            return NativeIOS.im_load_ultra_group_channel_default_notification_level(targetId, channelId);
        }
    
        public override int GetUltraGroupChannelDefaultNotificationLevel(
            string targetId, string channelId, RCIMGetUltraGroupChannelDefaultNotificationLevelListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetUltraGroupChannelDefaultNotificationLevel",
                                            $"targetId={targetId},channelId={channelId},callback={callback}");
            ios_get_ultra_group_channel_default_notification_level_proxy ccallback =
                toIOSGetUltraGroupChannelDefaultNotificationLevelProxy(callback);
            return NativeIOS.im_get_ultra_group_channel_default_notification_level(targetId, channelId, ref ccallback);
        }
    
        public override int ChangePushContentShowStatus(bool showContent,
                                                        OnPushContentShowStatusChangedAction callback = null)
        {
            RCUnityLogger.getInstance().log("ChangePushContentShowStatus",
                                            $"showContent={showContent},callback={callback}");
            ios_change_push_content_show_status_proxy ccallback = toIOSChangePushContentShowStatusProxy(callback);
            return NativeIOS.im_change_push_content_show_status(showContent, ref ccallback);
        }
    
        public override int ChangePushLanguage(string language, OnPushLanguageChangedAction callback = null)
        {
            RCUnityLogger.getInstance().log("ChangePushLanguage", $"language={language},callback={callback}");
            ios_change_push_language_proxy ccallback = toIOSChangePushLanguageProxy(callback);
            return NativeIOS.im_change_push_language(language, ref ccallback);
        }
    
        public override int ChangePushReceiveStatus(bool receive, OnPushReceiveStatusChangedAction callback = null)
        {
            RCUnityLogger.getInstance().log("ChangePushReceiveStatus", $"receive={receive},callback={callback}");
            ios_change_push_receive_status_proxy ccallback = toIOSChangePushReceiveStatusProxy(callback);
            return NativeIOS.im_change_push_receive_status(receive, ref ccallback);
        }
    
        public override int SendGroupMessageToDesignatedUsers(RCIMMessage message, List<string> userIds,
                                                              RCIMSendGroupMessageToDesignatedUsersListener callback = null)
        {
            RCUnityLogger.getInstance().log("SendGroupMessageToDesignatedUsers",
                                            $"message={message},userIds={userIds},callback={callback}");
            ios_class_warpper message_wapper = NativeConvert.toMessageWapper(message);
            ios_send_group_message_to_designated_users_proxy ccallback =
                toIOSSendGroupMessageToDesignatedUsersProxy(callback);
            var ret = NativeIOS.im_send_group_message_to_designated_users(ref message_wapper, userIds.ToArray(),
                                                                          userIds.Count, ref ccallback);
            NativeConvert.freeMessageWapper(ref message_wapper);
            return ret;
        }
    
        public override int LoadMessageCount(RCIMConversationType type, string targetId, string channelId)
        {
            RCUnityLogger.getInstance().log("LoadMessageCount", $"type={type},targetId={targetId},channelId={channelId}");
            return NativeIOS.im_load_message_count(type, targetId, channelId);
        }
    
        public override int GetMessageCount(RCIMConversationType type, string targetId, string channelId,
                                            RCIMGetMessageCountListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetMessageCount",
                                            $"type={type},targetId={targetId},channelId={channelId},callback={callback}");
            ios_get_message_count_proxy ccallback = toIOSGetMessageCountProxy(callback);
            return NativeIOS.im_get_message_count(type, targetId, channelId, ref ccallback);
        }
    
        public override int LoadTopConversations(List<RCIMConversationType> conversationTypes, string channelId)
        {
            RCUnityLogger.getInstance().log("LoadTopConversations",
                                            $"conversationTypes={conversationTypes},channelId={channelId}");
            return NativeIOS.im_load_top_conversations(conversationTypes.ToArray(), conversationTypes.Count, channelId);
        }
    
        public override int GetTopConversations(List<RCIMConversationType> conversationTypes, string channelId,
                                                RCIMGetTopConversationsListener callback = null)
        {
            RCUnityLogger.getInstance().log(
                "GetTopConversations", $"conversationTypes={conversationTypes},channelId={channelId},callback={callback}");
            ios_get_top_conversations_proxy ccallback = toIOSGetTopConversationsProxy(callback);
            return NativeIOS.im_get_top_conversations(conversationTypes.ToArray(), conversationTypes.Count, channelId,
                                                      ref ccallback);
        }
    
        public override int SyncUltraGroupReadStatus(string targetId, string channelId, long timestamp,
                                                     OnUltraGroupReadStatusSyncedAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "SyncUltraGroupReadStatus",
                $"targetId={targetId},channelId={channelId},timestamp={timestamp},callback={callback}");
            ios_sync_ultra_group_read_status_proxy ccallback = toIOSSyncUltraGroupReadStatusProxy(callback);
            return NativeIOS.im_sync_ultra_group_read_status(targetId, channelId, timestamp, ref ccallback);
        }
    
        public override int LoadConversationsForAllChannel(RCIMConversationType type, string targetId)
        {
            RCUnityLogger.getInstance().log("LoadConversationsForAllChannel", $"type={type},targetId={targetId}");
            return NativeIOS.im_load_conversations_for_all_channel(type, targetId);
        }
    
        public override int GetConversationsForAllChannel(RCIMConversationType type, string targetId,
                                                          RCIMGetConversationsForAllChannelListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetConversationsForAllChannel",
                                            $"type={type},targetId={targetId},callback={callback}");
            ios_get_conversations_for_all_channel_proxy ccallback = toIOSGetConversationsForAllChannelProxy(callback);
            return NativeIOS.im_get_conversations_for_all_channel(type, targetId, ref ccallback);
        }
    
        public override int ModifyUltraGroupMessage(string messageUId, RCIMMessage message,
                                                    OnUltraGroupMessageModifiedAction callback = null)
        {
            RCUnityLogger.getInstance().log("ModifyUltraGroupMessage",
                                            $"messageUId={messageUId},message={message},callback={callback}");
            ios_class_warpper message_wapper = NativeConvert.toMessageWapper(message);
            ios_modify_ultra_group_message_proxy ccallback = toIOSModifyUltraGroupMessageProxy(callback);
            var ret = NativeIOS.im_modify_ultra_group_message(messageUId, ref message_wapper, ref ccallback);
            NativeConvert.freeMessageWapper(ref message_wapper);
            return ret;
        }
    
        public override int RecallUltraGroupMessage(RCIMMessage message, bool deleteRemote,
                                                    OnUltraGroupMessageRecalledAction callback = null)
        {
            RCUnityLogger.getInstance().log("RecallUltraGroupMessage",
                                            $"message={message},deleteRemote={deleteRemote},callback={callback}");
            ios_class_warpper message_wapper = NativeConvert.toMessageWapper(message);
            ios_recall_ultra_group_message_proxy ccallback = toIOSRecallUltraGroupMessageProxy(callback);
            var ret = NativeIOS.im_recall_ultra_group_message(ref message_wapper, deleteRemote, ref ccallback);
            NativeConvert.freeMessageWapper(ref message_wapper);
            return ret;
        }
    
        public override int ClearUltraGroupMessages(string targetId, string channelId, long timestamp,
                                                    RCIMMessageOperationPolicy policy,
                                                    OnUltraGroupMessagesClearedAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "ClearUltraGroupMessages",
                $"targetId={targetId},channelId={channelId},timestamp={timestamp},policy={policy},callback={callback}");
            ios_clear_ultra_group_messages_proxy ccallback = toIOSClearUltraGroupMessagesProxy(callback);
            return NativeIOS.im_clear_ultra_group_messages(targetId, channelId, timestamp, policy, ref ccallback);
        }
    
        public override int SendUltraGroupTypingStatus(string targetId, string channelId,
                                                       RCIMUltraGroupTypingStatus typingStatus,
                                                       OnUltraGroupTypingStatusSentAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "SendUltraGroupTypingStatus",
                $"targetId={targetId},channelId={channelId},typingStatus={typingStatus},callback={callback}");
            ios_send_ultra_group_typing_status_proxy ccallback = toIOSSendUltraGroupTypingStatusProxy(callback);
            return NativeIOS.im_send_ultra_group_typing_status(targetId, channelId, typingStatus, ref ccallback);
        }
    
        public override int ClearUltraGroupMessagesForAllChannel(
            string targetId, long timestamp, OnUltraGroupMessagesClearedForAllChannelAction callback = null)
        {
            RCUnityLogger.getInstance().log("ClearUltraGroupMessagesForAllChannel",
                                            $"targetId={targetId},timestamp={timestamp},callback={callback}");
            ios_clear_ultra_group_messages_for_all_channel_proxy ccallback =
                toIOSClearUltraGroupMessagesForAllChannelProxy(callback);
            return NativeIOS.im_clear_ultra_group_messages_for_all_channel(targetId, timestamp, ref ccallback);
        }
    
        public override int LoadBatchRemoteUltraGroupMessages(List<RCIMMessage> messages)
        {
            RCUnityLogger.getInstance().log("LoadBatchRemoteUltraGroupMessages", $"messages={messages}");
            ios_class_warpper[] cmessages = null;
            if (messages.Count > 0)
            {
                cmessages = new ios_class_warpper[messages.Count];
                for (int i = 0; i < messages.Count; i++)
                {
                    cmessages[i] = NativeConvert.toMessageWapper(messages[i]);
                }
            }
            var ret = NativeIOS.im_load_batch_remote_ultra_group_messages(ref cmessages, messages.Count);
            if (cmessages != null)
            {
                for (int i = 0; i < cmessages.Length; i++)
                {
                    NativeConvert.freeMessageWapper(ref cmessages[i]);
                }
            }
            return ret;
        }
    
        public override int GetBatchRemoteUltraGroupMessages(List<RCIMMessage> messages,
                                                             RCIMGetBatchRemoteUltraGroupMessagesListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetBatchRemoteUltraGroupMessages", $"messages={messages},callback={callback}");
            ios_class_warpper[] cmessages = null;
            if (messages.Count > 0)
            {
                cmessages = new ios_class_warpper[messages.Count];
                for (int i = 0; i < messages.Count; i++)
                {
                    cmessages[i] = NativeConvert.toMessageWapper(messages[i]);
                }
            }
            ios_get_batch_remote_ultra_group_messages_proxy ccallback =
                toIOSGetBatchRemoteUltraGroupMessagesProxy(callback);
            var ret = NativeIOS.im_get_batch_remote_ultra_group_messages(ref cmessages, messages.Count, ref ccallback);
            if (cmessages != null)
            {
                for (int i = 0; i < cmessages.Length; i++)
                {
                    NativeConvert.freeMessageWapper(ref cmessages[i]);
                }
            }
            return ret;
        }
    
        public override int UpdateUltraGroupMessageExpansion(string messageUId, Dictionary<string, string> expansion,
                                                             OnUltraGroupMessageExpansionUpdatedAction callback = null)
        {
            RCUnityLogger.getInstance().log("UpdateUltraGroupMessageExpansion",
                                            $"messageUId={messageUId},expansion={expansion},callback={callback}");
            var cexpansion = new List<ios_c_map_item>();
            foreach (var item in expansion)
            {
                ios_c_map_item cobject;
                cobject.key = item.Key.ToString();
                cobject.value = item.Value.ToString();
                cexpansion.Add(cobject);
            }
            ios_c_map_item[] expansion_array = null;
            if (cexpansion.Count > 0)
            {
                expansion_array = cexpansion.ToArray();
            }
            ios_update_ultra_group_message_expansion_proxy ccallback = toIOSUpdateUltraGroupMessageExpansionProxy(callback);
            return NativeIOS.im_update_ultra_group_message_expansion(messageUId, ref expansion_array,
                                                                     expansion_array.Length, ref ccallback);
        }
    
        public override int RemoveUltraGroupMessageExpansionForKeys(
            string messageUId, List<string> keys, OnUltraGroupMessageExpansionForKeysRemovedAction callback = null)
        {
            RCUnityLogger.getInstance().log("RemoveUltraGroupMessageExpansionForKeys",
                                            $"messageUId={messageUId},keys={keys},callback={callback}");
            ios_remove_ultra_group_message_expansion_for_keys_proxy ccallback =
                toIOSRemoveUltraGroupMessageExpansionForKeysProxy(callback);
            return NativeIOS.im_remove_ultra_group_message_expansion_for_keys(messageUId, keys.ToArray(), keys.Count,
                                                                              ref ccallback);
        }
    
        public override int ChangeLogLevel(RCIMLogLevel level)
        {
            RCUnityLogger.getInstance().log("ChangeLogLevel", $"level={level}");
            return NativeIOS.im_change_log_level(level);
        }
    
        public override long GetDeltaTime()
        {
            RCUnityLogger.getInstance().log("GetDeltaTime", $"");
            return NativeIOS.im_get_delta_time();
        }
    
#endregion
    }
}
#endif