#if UNITY_IOS
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace cn_rongcloud_im_unity
{
#region Delegate
    
#region IRCIMIWListener
    internal delegate void OnMessageReceived(IntPtr message, int left, bool offline, bool hasPackage);
    
    internal delegate void OnConnectionStatusChanged(RCIMConnectionStatus status);
    
    internal delegate void OnConversationTopStatusSynced(RCIMConversationType type, string targetId, string channelId,
                                                         bool top);
    
    internal delegate void OnConversationNotificationLevelSynced(RCIMConversationType type, string targetId,
                                                                 string channelId, RCIMPushNotificationLevel level);
    
    internal delegate void OnRemoteMessageRecalled(IntPtr message);
    
    internal delegate void OnPrivateReadReceiptReceived(string targetId, string channelId, long timestamp);
    
    internal delegate void OnRemoteMessageExpansionUpdated(ref ios_c_list expansion, IntPtr message);
    
    internal delegate void OnRemoteMessageExpansionForKeyRemoved(IntPtr message, ref ios_c_list keys);
    
    internal delegate void OnChatRoomMemberChanged(string targetId, ref ios_c_list actions);
    
    internal delegate void OnTypingStatusChanged(RCIMConversationType type, string targetId, string channelId,
                                                 ref ios_c_list userTypingStatus);
    
    internal delegate void OnConversationReadStatusSyncMessageReceived(RCIMConversationType type, string targetId,
                                                                       long timestamp);
    
    internal delegate void OnChatRoomEntriesSynced(string roomId);
    
    internal delegate void OnChatRoomEntriesChanged(RCIMChatRoomEntriesOperationType operationType, string roomId,
                                                    ref ios_c_list entries);
    
    internal delegate void OnRemoteUltraGroupMessageExpansionUpdated(ref ios_c_list messages);
    
    internal delegate void OnRemoteUltraGroupMessageModified(ref ios_c_list messages);
    
    internal delegate void OnRemoteUltraGroupMessageRecalled(ref ios_c_list messages);
    
    internal delegate void OnUltraGroupReadTimeReceived(string targetId, string channelId, long timestamp);
    
    internal delegate void OnUltraGroupTypingStatusChanged(ref ios_c_list info);
    
    internal delegate void OnMessageBlocked(IntPtr info);
    
    internal delegate void OnChatRoomStatusChanged(string targetId, RCIMChatRoomStatus status);
    
    internal delegate void OnGroupMessageReadReceiptRequestReceived(string targetId, string messageUId);
    
    internal delegate void OnGroupMessageReadReceiptResponseReceived(string targetId, string messageUId,
                                                                     ref ios_c_list respondUserIds);
    
    internal delegate void OnConnected(int code, string userId);
    
    internal delegate void OnDatabaseOpened(int code);
    
    internal delegate void OnConversationLoaded(int code, RCIMConversationType type, string targetId, string channelId,
                                                IntPtr conversation);
    
    internal delegate void OnConversationsLoaded(int code, ref ios_c_list conversationTypes, string channelId,
                                                 long startTime, int count, ref ios_c_list conversations);
    
    internal delegate void OnConversationRemoved(int code, RCIMConversationType type, string targetId, string channelId);
    
    internal delegate void OnConversationsRemoved(int code, ref ios_c_list conversationTypes, string channelId);
    
    internal delegate void OnTotalUnreadCountLoaded(int code, string channelId, int count);
    
    internal delegate void OnUnreadCountLoaded(int code, RCIMConversationType type, string targetId, string channelId,
                                               int count);
    
    internal delegate void OnUnreadCountByConversationTypesLoaded(int code, ref ios_c_list conversationTypes,
                                                                  string channelId, bool contain, int count);
    
    internal delegate void OnUnreadMentionedCountLoaded(int code, RCIMConversationType type, string targetId,
                                                        string channelId, int count);
    
    internal delegate void OnUltraGroupAllUnreadCountLoaded(int code, int count);
    
    internal delegate void OnUltraGroupAllUnreadMentionedCountLoaded(int code, int count);
    
    internal delegate void OnUltraGroupConversationsSynced();
    
    internal delegate void OnUnreadCountCleared(int code, RCIMConversationType type, string targetId, string channelId,
                                                long timestamp);
    
    internal delegate void OnDraftMessageSaved(int code, RCIMConversationType type, string targetId, string channelId,
                                               string draft);
    
    internal delegate void OnDraftMessageCleared(int code, RCIMConversationType type, string targetId, string channelId);
    
    internal delegate void OnDraftMessageLoaded(int code, RCIMConversationType type, string targetId, string channelId,
                                                string draft);
    
    internal delegate void OnBlockedConversationsLoaded(int code, ref ios_c_list conversationTypes, string channelId,
                                                        ref ios_c_list conversations);
    
    internal delegate void OnConversationTopStatusChanged(int code, RCIMConversationType type, string targetId,
                                                          string channelId, bool top);
    
    internal delegate void OnConversationTopStatusLoaded(int code, RCIMConversationType type, string targetId,
                                                         string channelId, bool top);
    
    internal delegate void OnConversationReadStatusSynced(int code, RCIMConversationType type, string targetId,
                                                          string channelId, long timestamp);
    
    internal delegate void OnMessageAttached(IntPtr message);
    
    internal delegate void OnMessageSent(int code, IntPtr message);
    
    internal delegate void OnMediaMessageAttached(IntPtr message);
    
    internal delegate void OnMediaMessageSending(IntPtr message, int progress);
    
    internal delegate void OnSendingMediaMessageCanceled(int code, IntPtr message);
    
    internal delegate void OnMediaMessageSent(int code, IntPtr message);
    
    internal delegate void OnMediaMessageDownloading(IntPtr message, int progress);
    
    internal delegate void OnMediaMessageDownloaded(int code, IntPtr message);
    
    internal delegate void OnDownloadingMediaMessageCanceled(int code, IntPtr message);
    
    internal delegate void OnMessagesLoaded(int code, RCIMConversationType type, string targetId, string channelId,
                                            long sentTime, RCIMTimeOrder order, ref ios_c_list messages);
    
    internal delegate void OnUnreadMentionedMessagesLoaded(int code, RCIMConversationType type, string targetId,
                                                           string channelId, ref ios_c_list messages);
    
    internal delegate void OnFirstUnreadMessageLoaded(int code, RCIMConversationType type, string targetId,
                                                      string channelId, IntPtr message);
    
    internal delegate void OnMessageInserted(int code, IntPtr message);
    
    internal delegate void OnMessagesInserted(int code, ref ios_c_list messages);
    
    internal delegate void OnMessagesCleared(int code, RCIMConversationType type, string targetId, string channelId,
                                             long timestamp);
    
    internal delegate void OnLocalMessagesDeleted(int code, ref ios_c_list messages);
    
    internal delegate void OnMessagesDeleted(int code, RCIMConversationType type, string targetId, string channelId,
                                             ref ios_c_list messages);
    
    internal delegate void OnMessageRecalled(int code, IntPtr message);
    
    internal delegate void OnPrivateReadReceiptMessageSent(int code, string targetId, string channelId, long timestamp);
    
    internal delegate void OnMessageExpansionUpdated(int code, string messageUId, ref ios_c_list expansion);
    
    internal delegate void OnMessageExpansionForKeysRemoved(int code, string messageUId, ref ios_c_list keys);
    
    internal delegate void OnMessageReceiveStatusChanged(int code, long messageId);
    
    internal delegate void OnMessageSentStatusChanged(int code, long messageId);
    
    internal delegate void OnChatRoomJoined(int code, string targetId);
    
    internal delegate void OnChatRoomJoining(string targetId);
    
    internal delegate void OnChatRoomLeft(int code, string targetId);
    
    internal delegate void OnChatRoomMessagesLoaded(int code, string targetId, ref ios_c_list messages, long syncTime);
    
    internal delegate void OnChatRoomEntryAdded(int code, string targetId, string key);
    
    internal delegate void OnChatRoomEntriesAdded(int code, string targetId, ref ios_c_list entries,
                                                  ref ios_c_list errorEntries);
    
    internal delegate void OnChatRoomEntryLoaded(int code, string targetId, ref ios_c_list entry);
    
    internal delegate void OnChatRoomAllEntriesLoaded(int code, string targetId, ref ios_c_list entries);
    
    internal delegate void OnChatRoomEntryRemoved(int code, string targetId, string key);
    
    internal delegate void OnChatRoomEntriesRemoved(int code, string targetId, ref ios_c_list keys);
    
    internal delegate void OnBlacklistAdded(int code, string userId);
    
    internal delegate void OnBlacklistRemoved(int code, string userId);
    
    internal delegate void OnBlacklistStatusLoaded(int code, string userId, RCIMBlacklistStatus status);
    
    internal delegate void OnBlacklistLoaded(int code, ref ios_c_list userIds);
    
    internal delegate void OnMessagesSearched(int code, RCIMConversationType type, string targetId, string channelId,
                                              string keyword, long startTime, int count, ref ios_c_list messages);
    
    internal delegate void OnMessagesSearchedByTimeRange(int code, RCIMConversationType type, string targetId,
                                                         string channelId, string keyword, long startTime, long endTime,
                                                         int offset, int count, ref ios_c_list messages);
    
    internal delegate void OnMessagesSearchedByUserId(int code, string userId, RCIMConversationType type, string targetId,
                                                      string channelId, long startTime, int count, ref ios_c_list messages);
    
    internal delegate void OnConversationsSearched(int code, ref ios_c_list conversationTypes, string channelId,
                                                   ref ios_c_list messageTypes, string keyword,
                                                   ref ios_c_list conversations);
    
    internal delegate void OnGroupReadReceiptRequestSent(int code, IntPtr message);
    
    internal delegate void OnGroupReadReceiptResponseSent(int code, string targetId, string channelId,
                                                          ref ios_c_list messages);
    
    internal delegate void OnNotificationQuietHoursChanged(int code, string startTime, int spanMinutes,
                                                           RCIMPushNotificationQuietHoursLevel level);
    
    internal delegate void OnNotificationQuietHoursRemoved(int code);
    
    internal delegate void OnNotificationQuietHoursLoaded(int code, string startTime, int spanMinutes,
                                                          RCIMPushNotificationQuietHoursLevel level);
    
    internal delegate void OnConversationNotificationLevelChanged(int code, RCIMConversationType type, string targetId,
                                                                  string channelId, RCIMPushNotificationLevel level);
    
    internal delegate void OnConversationNotificationLevelLoaded(int code, RCIMConversationType type, string targetId,
                                                                 string channelId, RCIMPushNotificationLevel level);
    
    internal delegate void OnConversationTypeNotificationLevelChanged(int code, RCIMConversationType type,
                                                                      RCIMPushNotificationLevel level);
    
    internal delegate void OnConversationTypeNotificationLevelLoaded(int code, RCIMConversationType type,
                                                                     RCIMPushNotificationLevel level);
    
    internal delegate void OnUltraGroupDefaultNotificationLevelChanged(int code, string targetId,
                                                                       RCIMPushNotificationLevel level);
    
    internal delegate void OnUltraGroupDefaultNotificationLevelLoaded(int code, string targetId,
                                                                      RCIMPushNotificationLevel level);
    
    internal delegate void OnUltraGroupChannelDefaultNotificationLevelChanged(int code, string targetId, string channelId,
                                                                              RCIMPushNotificationLevel level);
    
    internal delegate void OnUltraGroupChannelDefaultNotificationLevelLoaded(int code, string targetId, string channelId,
                                                                             RCIMPushNotificationLevel level);
    
    internal delegate void OnPushContentShowStatusChanged(int code, bool showContent);
    
    internal delegate void OnPushLanguageChanged(int code, string language);
    
    internal delegate void OnPushReceiveStatusChanged(int code, bool receive);
    
    internal delegate void OnMessageCountLoaded(int code, RCIMConversationType type, string targetId, string channelId,
                                                int count);
    
    internal delegate void OnTopConversationsLoaded(int code, ref ios_c_list conversationTypes, string channelId,
                                                    ref ios_c_list conversations);
    
    internal delegate void OnGroupMessageToDesignatedUsersAttached(IntPtr message);
    
    internal delegate void OnGroupMessageToDesignatedUsersSent(int code, IntPtr message);
    
    internal delegate void OnUltraGroupReadStatusSynced(int code, string targetId, string channelId, long timestamp);
    
    internal delegate void OnConversationsLoadedForAllChannel(int code, RCIMConversationType type, string targetId,
                                                              ref ios_c_list conversations);
    
    internal delegate void OnUltraGroupUnreadMentionedCountLoaded(int code, string targetId, int count);
    
    internal delegate void OnUltraGroupUnreadCountLoaded(int code, string targetId, int count);
    
    internal delegate void OnUltraGroupMessageModified(int code, string messageUId);
    
    internal delegate void OnUltraGroupMessageRecalled(int code, IntPtr message, bool deleteRemote);
    
    internal delegate void OnUltraGroupMessagesCleared(int code, string targetId, string channelId, long timestamp,
                                                       RCIMMessageOperationPolicy policy);
    
    internal delegate void OnUltraGroupMessagesClearedForAllChannel(int code, string targetId, long timestamp);
    
    internal delegate void OnUltraGroupTypingStatusSent(int code, string targetId, string channelId,
                                                        RCIMUltraGroupTypingStatus typingStatus);
    
    internal delegate void OnBatchRemoteUltraGroupMessagesLoaded(int code, ref ios_c_list matchedMessages,
                                                                 ref ios_c_list notMatchedMessages);
    
    internal delegate void OnUltraGroupMessageExpansionUpdated(int code, ref ios_c_list expansion, string messageUId);
    
    internal delegate void OnUltraGroupMessageExpansionForKeysRemoved(int code, string messageUId, ref ios_c_list keys);
    
#endregion
    
#endregion
    
#region Callback Proxy
    
#region RCIMIWConnectCallback
    internal delegate void IOSConnectProxyOnConnected(int code, string userId, Int64 handle);
    
    internal delegate void IOSConnectProxyOnDatabaseOpened(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_connect_proxy
    {
        public Int64 handle;
        public IOSConnectProxyOnConnected onConnected;
        public IOSConnectProxyOnDatabaseOpened onDatabaseOpened;
    }
    
#endregion
#region RCIMIWSendMessageCallback
    internal delegate void IOSSendMessageProxyOnMessageSaved(IntPtr message, Int64 handle);
    
    internal delegate void IOSSendMessageProxyOnMessageSent(int code, IntPtr message, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_send_message_proxy
    {
        public Int64 handle;
        public IOSSendMessageProxyOnMessageSaved onMessageSaved;
        public IOSSendMessageProxyOnMessageSent onMessageSent;
    }
    
#endregion
#region RCIMIWSendMediaMessageListener
    internal delegate void IOSSendMediaMessageProxyOnMediaMessageSaved(IntPtr message, Int64 handle);
    
    internal delegate void IOSSendMediaMessageProxyOnMediaMessageSending(IntPtr message, int progress, Int64 handle);
    
    internal delegate void IOSSendMediaMessageProxyOnSendingMediaMessageCanceled(IntPtr message, Int64 handle);
    
    internal delegate void IOSSendMediaMessageProxyOnMediaMessageSent(int code, IntPtr message, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_send_media_message_proxy
    {
        public Int64 handle;
        public IOSSendMediaMessageProxyOnMediaMessageSaved onMediaMessageSaved;
        public IOSSendMediaMessageProxyOnMediaMessageSending onMediaMessageSending;
        public IOSSendMediaMessageProxyOnSendingMediaMessageCanceled onSendingMediaMessageCanceled;
        public IOSSendMediaMessageProxyOnMediaMessageSent onMediaMessageSent;
    }
    
#endregion
#region RCIMIWCancelSendingMediaMessageCallback
    internal delegate void IOSCancelSendingMediaMessageProxyOnCancelSendingMediaMessageCalled(int code, IntPtr message,
                                                                                              Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_cancel_sending_media_message_proxy
    {
        public Int64 handle;
        public IOSCancelSendingMediaMessageProxyOnCancelSendingMediaMessageCalled onCancelSendingMediaMessageCalled;
    }
    
#endregion
#region RCIMIWDownloadMediaMessageListener
    internal delegate void IOSDownloadMediaMessageProxyOnMediaMessageDownloading(IntPtr message, int progress,
                                                                                 Int64 handle);
    
    internal delegate void IOSDownloadMediaMessageProxyOnDownloadingMediaMessageCanceled(IntPtr message, Int64 handle);
    
    internal delegate void IOSDownloadMediaMessageProxyOnMediaMessageDownloaded(int code, IntPtr message, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_download_media_message_proxy
    {
        public Int64 handle;
        public IOSDownloadMediaMessageProxyOnMediaMessageDownloading onMediaMessageDownloading;
        public IOSDownloadMediaMessageProxyOnDownloadingMediaMessageCanceled onDownloadingMediaMessageCanceled;
        public IOSDownloadMediaMessageProxyOnMediaMessageDownloaded onMediaMessageDownloaded;
    }
    
#endregion
#region RCIMIWCancelDownloadingMediaMessageCallback
    internal delegate void IOSCancelDownloadingMediaMessageProxyOnCancelDownloadingMediaMessageCalled(int code,
                                                                                                      IntPtr message,
                                                                                                      Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_cancel_downloading_media_message_proxy
    {
        public Int64 handle;
        public IOSCancelDownloadingMediaMessageProxyOnCancelDownloadingMediaMessageCalled
            onCancelDownloadingMediaMessageCalled;
    }
    
#endregion
#region RCIMIWGetConversationCallback
    internal delegate void IOSGetConversationProxyOnSuccess(IntPtr t, Int64 handle);
    
    internal delegate void IOSGetConversationProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_conversation_proxy
    {
        public Int64 handle;
        public IOSGetConversationProxyOnSuccess onSuccess;
        public IOSGetConversationProxyOnError onError;
    }
    
#endregion
#region RCIMIWGetConversationsCallback
    internal delegate void IOSGetConversationsProxyOnSuccess(ref ios_c_list t, Int64 handle);
    
    internal delegate void IOSGetConversationsProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_conversations_proxy
    {
        public Int64 handle;
        public IOSGetConversationsProxyOnSuccess onSuccess;
        public IOSGetConversationsProxyOnError onError;
    }
    
#endregion
#region RCIMIWRemoveConversationCallback
    internal delegate void IOSRemoveConversationProxyOnConversationRemoved(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_remove_conversation_proxy
    {
        public Int64 handle;
        public IOSRemoveConversationProxyOnConversationRemoved onConversationRemoved;
    }
    
#endregion
#region RCIMIWRemoveConversationsCallback
    internal delegate void IOSRemoveConversationsProxyOnConversationsRemoved(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_remove_conversations_proxy
    {
        public Int64 handle;
        public IOSRemoveConversationsProxyOnConversationsRemoved onConversationsRemoved;
    }
    
#endregion
#region RCIMIWGetUnreadCountCallback
    internal delegate void IOSGetUnreadCountProxyOnSuccess(int t, Int64 handle);
    
    internal delegate void IOSGetUnreadCountProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_unread_count_proxy
    {
        public Int64 handle;
        public IOSGetUnreadCountProxyOnSuccess onSuccess;
        public IOSGetUnreadCountProxyOnError onError;
    }
    
#endregion
#region RCIMIWGetTotalUnreadCountCallback
    internal delegate void IOSGetTotalUnreadCountProxyOnSuccess(int t, Int64 handle);
    
    internal delegate void IOSGetTotalUnreadCountProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_total_unread_count_proxy
    {
        public Int64 handle;
        public IOSGetTotalUnreadCountProxyOnSuccess onSuccess;
        public IOSGetTotalUnreadCountProxyOnError onError;
    }
    
#endregion
#region RCIMIWGetUnreadMentionedCountCallback
    internal delegate void IOSGetUnreadMentionedCountProxyOnSuccess(int t, Int64 handle);
    
    internal delegate void IOSGetUnreadMentionedCountProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_unread_mentioned_count_proxy
    {
        public Int64 handle;
        public IOSGetUnreadMentionedCountProxyOnSuccess onSuccess;
        public IOSGetUnreadMentionedCountProxyOnError onError;
    }
    
#endregion
#region RCIMIWGetUltraGroupAllUnreadCountCallback
    internal delegate void IOSGetUltraGroupAllUnreadCountProxyOnSuccess(int t, Int64 handle);
    
    internal delegate void IOSGetUltraGroupAllUnreadCountProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_ultra_group_all_unread_count_proxy
    {
        public Int64 handle;
        public IOSGetUltraGroupAllUnreadCountProxyOnSuccess onSuccess;
        public IOSGetUltraGroupAllUnreadCountProxyOnError onError;
    }
    
#endregion
#region RCIMIWGetUltraGroupAllUnreadMentionedCountCallback
    internal delegate void IOSGetUltraGroupAllUnreadMentionedCountProxyOnSuccess(int t, Int64 handle);
    
    internal delegate void IOSGetUltraGroupAllUnreadMentionedCountProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_ultra_group_all_unread_mentioned_count_proxy
    {
        public Int64 handle;
        public IOSGetUltraGroupAllUnreadMentionedCountProxyOnSuccess onSuccess;
        public IOSGetUltraGroupAllUnreadMentionedCountProxyOnError onError;
    }
    
#endregion
#region RCIMIWGetUltraGroupUnreadCountCallback
    internal delegate void IOSGetUltraGroupUnreadCountProxyOnSuccess(int t, Int64 handle);
    
    internal delegate void IOSGetUltraGroupUnreadCountProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_ultra_group_unread_count_proxy
    {
        public Int64 handle;
        public IOSGetUltraGroupUnreadCountProxyOnSuccess onSuccess;
        public IOSGetUltraGroupUnreadCountProxyOnError onError;
    }
    
#endregion
#region RCIMIWGetUltraGroupUnreadMentionedCountCallback
    internal delegate void IOSGetUltraGroupUnreadMentionedCountProxyOnSuccess(int t, Int64 handle);
    
    internal delegate void IOSGetUltraGroupUnreadMentionedCountProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_ultra_group_unread_mentioned_count_proxy
    {
        public Int64 handle;
        public IOSGetUltraGroupUnreadMentionedCountProxyOnSuccess onSuccess;
        public IOSGetUltraGroupUnreadMentionedCountProxyOnError onError;
    }
    
#endregion
#region RCIMIWGetUnreadCountByConversationTypesCallback
    internal delegate void IOSGetUnreadCountByConversationTypesProxyOnSuccess(int t, Int64 handle);
    
    internal delegate void IOSGetUnreadCountByConversationTypesProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_unread_count_by_conversation_types_proxy
    {
        public Int64 handle;
        public IOSGetUnreadCountByConversationTypesProxyOnSuccess onSuccess;
        public IOSGetUnreadCountByConversationTypesProxyOnError onError;
    }
    
#endregion
#region RCIMIWClearUnreadCountCallback
    internal delegate void IOSClearUnreadCountProxyOnUnreadCountCleared(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_clear_unread_count_proxy
    {
        public Int64 handle;
        public IOSClearUnreadCountProxyOnUnreadCountCleared onUnreadCountCleared;
    }
    
#endregion
#region RCIMIWSaveDraftMessageCallback
    internal delegate void IOSSaveDraftMessageProxyOnDraftMessageSaved(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_save_draft_message_proxy
    {
        public Int64 handle;
        public IOSSaveDraftMessageProxyOnDraftMessageSaved onDraftMessageSaved;
    }
    
#endregion
#region RCIMIWGetDraftMessageCallback
    internal delegate void IOSGetDraftMessageProxyOnSuccess(string t, Int64 handle);
    
    internal delegate void IOSGetDraftMessageProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_draft_message_proxy
    {
        public Int64 handle;
        public IOSGetDraftMessageProxyOnSuccess onSuccess;
        public IOSGetDraftMessageProxyOnError onError;
    }
    
#endregion
#region RCIMIWClearDraftMessageCallback
    internal delegate void IOSClearDraftMessageProxyOnDraftMessageCleared(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_clear_draft_message_proxy
    {
        public Int64 handle;
        public IOSClearDraftMessageProxyOnDraftMessageCleared onDraftMessageCleared;
    }
    
#endregion
#region RCIMIWGetBlockedConversationsCallback
    internal delegate void IOSGetBlockedConversationsProxyOnSuccess(ref ios_c_list t, Int64 handle);
    
    internal delegate void IOSGetBlockedConversationsProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_blocked_conversations_proxy
    {
        public Int64 handle;
        public IOSGetBlockedConversationsProxyOnSuccess onSuccess;
        public IOSGetBlockedConversationsProxyOnError onError;
    }
    
#endregion
#region RCIMIWChangeConversationTopStatusCallback
    internal delegate void IOSChangeConversationTopStatusProxyOnConversationTopStatusChanged(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_change_conversation_top_status_proxy
    {
        public Int64 handle;
        public IOSChangeConversationTopStatusProxyOnConversationTopStatusChanged onConversationTopStatusChanged;
    }
    
#endregion
#region RCIMIWGetConversationTopStatusCallback
    internal delegate void IOSGetConversationTopStatusProxyOnSuccess(Boolean t, Int64 handle);
    
    internal delegate void IOSGetConversationTopStatusProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_conversation_top_status_proxy
    {
        public Int64 handle;
        public IOSGetConversationTopStatusProxyOnSuccess onSuccess;
        public IOSGetConversationTopStatusProxyOnError onError;
    }
    
#endregion
#region RCIMIWSyncConversationReadStatusCallback
    internal delegate void IOSSyncConversationReadStatusProxyOnConversationReadStatusSynced(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_sync_conversation_read_status_proxy
    {
        public Int64 handle;
        public IOSSyncConversationReadStatusProxyOnConversationReadStatusSynced onConversationReadStatusSynced;
    }
    
#endregion
#region RCIMIWGetMessagesCallback
    internal delegate void IOSGetMessagesProxyOnSuccess(ref ios_c_list t, Int64 handle);
    
    internal delegate void IOSGetMessagesProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_messages_proxy
    {
        public Int64 handle;
        public IOSGetMessagesProxyOnSuccess onSuccess;
        public IOSGetMessagesProxyOnError onError;
    }
    
#endregion
#region RCIMIWGetMessageCallback
    internal delegate void IOSGetMessageProxyOnSuccess(IntPtr t, Int64 handle);
    
    internal delegate void IOSGetMessageProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_message_proxy
    {
        public Int64 handle;
        public IOSGetMessageProxyOnSuccess onSuccess;
        public IOSGetMessageProxyOnError onError;
    }
    
#endregion
#region RCIMIWGetFirstUnreadMessageCallback
    internal delegate void IOSGetFirstUnreadMessageProxyOnSuccess(IntPtr t, Int64 handle);
    
    internal delegate void IOSGetFirstUnreadMessageProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_first_unread_message_proxy
    {
        public Int64 handle;
        public IOSGetFirstUnreadMessageProxyOnSuccess onSuccess;
        public IOSGetFirstUnreadMessageProxyOnError onError;
    }
    
#endregion
#region RCIMIWGetUnreadMentionedMessagesCallback
    internal delegate void IOSGetUnreadMentionedMessagesProxyOnSuccess(ref ios_c_list t, Int64 handle);
    
    internal delegate void IOSGetUnreadMentionedMessagesProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_unread_mentioned_messages_proxy
    {
        public Int64 handle;
        public IOSGetUnreadMentionedMessagesProxyOnSuccess onSuccess;
        public IOSGetUnreadMentionedMessagesProxyOnError onError;
    }
    
#endregion
#region RCIMIWInsertMessageCallback
    internal delegate void IOSInsertMessageProxyOnMessageInserted(int code, IntPtr message, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_insert_message_proxy
    {
        public Int64 handle;
        public IOSInsertMessageProxyOnMessageInserted onMessageInserted;
    }
    
#endregion
#region RCIMIWInsertMessagesCallback
    internal delegate void IOSInsertMessagesProxyOnMessagesInserted(int code, ref ios_c_list messages, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_insert_messages_proxy
    {
        public Int64 handle;
        public IOSInsertMessagesProxyOnMessagesInserted onMessagesInserted;
    }
    
#endregion
#region RCIMIWClearMessagesCallback
    internal delegate void IOSClearMessagesProxyOnMessagesCleared(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_clear_messages_proxy
    {
        public Int64 handle;
        public IOSClearMessagesProxyOnMessagesCleared onMessagesCleared;
    }
    
#endregion
#region RCIMIWDeleteLocalMessagesCallback
    internal delegate void IOSDeleteLocalMessagesProxyOnLocalMessagesDeleted(int code, ref ios_c_list messages,
                                                                             Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_delete_local_messages_proxy
    {
        public Int64 handle;
        public IOSDeleteLocalMessagesProxyOnLocalMessagesDeleted onLocalMessagesDeleted;
    }
    
#endregion
#region RCIMIWDeleteMessagesCallback
    internal delegate void IOSDeleteMessagesProxyOnMessagesDeleted(int code, ref ios_c_list messages, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_delete_messages_proxy
    {
        public Int64 handle;
        public IOSDeleteMessagesProxyOnMessagesDeleted onMessagesDeleted;
    }
    
#endregion
#region RCIMIWRecallMessageCallback
    internal delegate void IOSRecallMessageProxyOnMessageRecalled(int code, IntPtr message, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_recall_message_proxy
    {
        public Int64 handle;
        public IOSRecallMessageProxyOnMessageRecalled onMessageRecalled;
    }
    
#endregion
#region RCIMIWSendPrivateReadReceiptMessageCallback
    internal delegate void IOSSendPrivateReadReceiptMessageProxyOnPrivateReadReceiptMessageSent(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_send_private_read_receipt_message_proxy
    {
        public Int64 handle;
        public IOSSendPrivateReadReceiptMessageProxyOnPrivateReadReceiptMessageSent onPrivateReadReceiptMessageSent;
    }
    
#endregion
#region RCIMIWSendGroupReadReceiptRequestCallback
    internal delegate void IOSSendGroupReadReceiptRequestProxyOnGroupReadReceiptRequestSent(int code, IntPtr message,
                                                                                            Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_send_group_read_receipt_request_proxy
    {
        public Int64 handle;
        public IOSSendGroupReadReceiptRequestProxyOnGroupReadReceiptRequestSent onGroupReadReceiptRequestSent;
    }
    
#endregion
#region RCIMIWSendGroupReadReceiptResponseCallback
    internal delegate void IOSSendGroupReadReceiptResponseProxyOnGroupReadReceiptResponseSent(int code,
                                                                                              ref ios_c_list message,
                                                                                              Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_send_group_read_receipt_response_proxy
    {
        public Int64 handle;
        public IOSSendGroupReadReceiptResponseProxyOnGroupReadReceiptResponseSent onGroupReadReceiptResponseSent;
    }
    
#endregion
#region RCIMIWUpdateMessageExpansionCallback
    internal delegate void IOSUpdateMessageExpansionProxyOnMessageExpansionUpdated(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_update_message_expansion_proxy
    {
        public Int64 handle;
        public IOSUpdateMessageExpansionProxyOnMessageExpansionUpdated onMessageExpansionUpdated;
    }
    
#endregion
#region RCIMIWRemoveMessageExpansionForKeysCallback
    internal delegate void IOSRemoveMessageExpansionForKeysProxyOnMessageExpansionForKeysRemoved(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_remove_message_expansion_for_keys_proxy
    {
        public Int64 handle;
        public IOSRemoveMessageExpansionForKeysProxyOnMessageExpansionForKeysRemoved onMessageExpansionForKeysRemoved;
    }
    
#endregion
#region RCIMIWChangeMessageSentStatusCallback
    internal delegate void IOSChangeMessageSentStatusProxyOnMessageSentStatusChanged(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_change_message_sent_status_proxy
    {
        public Int64 handle;
        public IOSChangeMessageSentStatusProxyOnMessageSentStatusChanged onMessageSentStatusChanged;
    }
    
#endregion
#region RCIMIWChangeMessageReceivedStatusCallback
    internal delegate void IOSChangeMessageReceivedStatusProxyOnMessageReceiveStatusChanged(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_change_message_received_status_proxy
    {
        public Int64 handle;
        public IOSChangeMessageReceivedStatusProxyOnMessageReceiveStatusChanged onMessageReceiveStatusChanged;
    }
    
#endregion
#region RCIMIWJoinChatRoomCallback
    internal delegate void IOSJoinChatRoomProxyOnChatRoomJoined(int code, string targetId, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_join_chat_room_proxy
    {
        public Int64 handle;
        public IOSJoinChatRoomProxyOnChatRoomJoined onChatRoomJoined;
    }
    
#endregion
#region RCIMIWLeaveChatRoomCallback
    internal delegate void IOSLeaveChatRoomProxyOnChatRoomLeft(int code, string targetId, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_leave_chat_room_proxy
    {
        public Int64 handle;
        public IOSLeaveChatRoomProxyOnChatRoomLeft onChatRoomLeft;
    }
    
#endregion
#region RCIMIWGetChatRoomMessagesCallback
    internal delegate void IOSGetChatRoomMessagesProxyOnSuccess(ref ios_c_list t, Int64 handle);
    
    internal delegate void IOSGetChatRoomMessagesProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_chat_room_messages_proxy
    {
        public Int64 handle;
        public IOSGetChatRoomMessagesProxyOnSuccess onSuccess;
        public IOSGetChatRoomMessagesProxyOnError onError;
    }
    
#endregion
#region RCIMIWAddChatRoomEntryCallback
    internal delegate void IOSAddChatRoomEntryProxyOnChatRoomEntryAdded(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_add_chat_room_entry_proxy
    {
        public Int64 handle;
        public IOSAddChatRoomEntryProxyOnChatRoomEntryAdded onChatRoomEntryAdded;
    }
    
#endregion
#region RCIMIWAddChatRoomEntriesCallback
    internal delegate void IOSAddChatRoomEntriesProxyOnChatRoomEntriesAdded(int code, ref ios_c_list errors, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_add_chat_room_entries_proxy
    {
        public Int64 handle;
        public IOSAddChatRoomEntriesProxyOnChatRoomEntriesAdded onChatRoomEntriesAdded;
    }
    
#endregion
#region RCIMIWGetChatRoomEntryCallback
    internal delegate void IOSGetChatRoomEntryProxyOnSuccess(ref ios_c_list t, Int64 handle);
    
    internal delegate void IOSGetChatRoomEntryProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_chat_room_entry_proxy
    {
        public Int64 handle;
        public IOSGetChatRoomEntryProxyOnSuccess onSuccess;
        public IOSGetChatRoomEntryProxyOnError onError;
    }
    
#endregion
#region RCIMIWGetChatRoomAllEntriesCallback
    internal delegate void IOSGetChatRoomAllEntriesProxyOnSuccess(ref ios_c_list t, Int64 handle);
    
    internal delegate void IOSGetChatRoomAllEntriesProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_chat_room_all_entries_proxy
    {
        public Int64 handle;
        public IOSGetChatRoomAllEntriesProxyOnSuccess onSuccess;
        public IOSGetChatRoomAllEntriesProxyOnError onError;
    }
    
#endregion
#region RCIMIWRemoveChatRoomEntryCallback
    internal delegate void IOSRemoveChatRoomEntryProxyOnChatRoomEntryRemoved(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_remove_chat_room_entry_proxy
    {
        public Int64 handle;
        public IOSRemoveChatRoomEntryProxyOnChatRoomEntryRemoved onChatRoomEntryRemoved;
    }
    
#endregion
#region RCIMIWRemoveChatRoomEntriesCallback
    internal delegate void IOSRemoveChatRoomEntriesProxyOnChatRoomEntriesRemoved(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_remove_chat_room_entries_proxy
    {
        public Int64 handle;
        public IOSRemoveChatRoomEntriesProxyOnChatRoomEntriesRemoved onChatRoomEntriesRemoved;
    }
    
#endregion
#region RCIMIWAddToBlacklistCallback
    internal delegate void IOSAddToBlacklistProxyOnBlacklistAdded(int code, string userId, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_add_to_blacklist_proxy
    {
        public Int64 handle;
        public IOSAddToBlacklistProxyOnBlacklistAdded onBlacklistAdded;
    }
    
#endregion
#region RCIMIWRemoveFromBlacklistCallback
    internal delegate void IOSRemoveFromBlacklistProxyOnBlacklistRemoved(int code, string userId, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_remove_from_blacklist_proxy
    {
        public Int64 handle;
        public IOSRemoveFromBlacklistProxyOnBlacklistRemoved onBlacklistRemoved;
    }
    
#endregion
#region RCIMIWGetBlacklistStatusCallback
    internal delegate void IOSGetBlacklistStatusProxyOnSuccess(RCIMBlacklistStatus t, Int64 handle);
    
    internal delegate void IOSGetBlacklistStatusProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_blacklist_status_proxy
    {
        public Int64 handle;
        public IOSGetBlacklistStatusProxyOnSuccess onSuccess;
        public IOSGetBlacklistStatusProxyOnError onError;
    }
    
#endregion
#region RCIMIWGetBlacklistCallback
    internal delegate void IOSGetBlacklistProxyOnSuccess(ref ios_c_list t, Int64 handle);
    
    internal delegate void IOSGetBlacklistProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_blacklist_proxy
    {
        public Int64 handle;
        public IOSGetBlacklistProxyOnSuccess onSuccess;
        public IOSGetBlacklistProxyOnError onError;
    }
    
#endregion
#region RCIMIWSearchMessagesCallback
    internal delegate void IOSSearchMessagesProxyOnSuccess(ref ios_c_list t, Int64 handle);
    
    internal delegate void IOSSearchMessagesProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_search_messages_proxy
    {
        public Int64 handle;
        public IOSSearchMessagesProxyOnSuccess onSuccess;
        public IOSSearchMessagesProxyOnError onError;
    }
    
#endregion
#region RCIMIWSearchMessagesByTimeRangeCallback
    internal delegate void IOSSearchMessagesByTimeRangeProxyOnSuccess(ref ios_c_list t, Int64 handle);
    
    internal delegate void IOSSearchMessagesByTimeRangeProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_search_messages_by_time_range_proxy
    {
        public Int64 handle;
        public IOSSearchMessagesByTimeRangeProxyOnSuccess onSuccess;
        public IOSSearchMessagesByTimeRangeProxyOnError onError;
    }
    
#endregion
#region RCIMIWSearchMessagesByUserIdCallback
    internal delegate void IOSSearchMessagesByUserIdProxyOnSuccess(ref ios_c_list t, Int64 handle);
    
    internal delegate void IOSSearchMessagesByUserIdProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_search_messages_by_user_id_proxy
    {
        public Int64 handle;
        public IOSSearchMessagesByUserIdProxyOnSuccess onSuccess;
        public IOSSearchMessagesByUserIdProxyOnError onError;
    }
    
#endregion
#region RCIMIWSearchConversationsCallback
    internal delegate void IOSSearchConversationsProxyOnSuccess(ref ios_c_list t, Int64 handle);
    
    internal delegate void IOSSearchConversationsProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_search_conversations_proxy
    {
        public Int64 handle;
        public IOSSearchConversationsProxyOnSuccess onSuccess;
        public IOSSearchConversationsProxyOnError onError;
    }
    
#endregion
#region RCIMIWChangeNotificationQuietHoursCallback
    internal delegate void IOSChangeNotificationQuietHoursProxyOnNotificationQuietHoursChanged(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_change_notification_quiet_hours_proxy
    {
        public Int64 handle;
        public IOSChangeNotificationQuietHoursProxyOnNotificationQuietHoursChanged onNotificationQuietHoursChanged;
    }
    
#endregion
#region RCIMIWRemoveNotificationQuietHoursCallback
    internal delegate void IOSRemoveNotificationQuietHoursProxyOnNotificationQuietHoursRemoved(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_remove_notification_quiet_hours_proxy
    {
        public Int64 handle;
        public IOSRemoveNotificationQuietHoursProxyOnNotificationQuietHoursRemoved onNotificationQuietHoursRemoved;
    }
    
#endregion
#region RCIMIWGetNotificationQuietHoursCallback
    internal delegate void IOSGetNotificationQuietHoursProxyOnSuccess(string startTime, int spanMinutes,
                                                                      RCIMPushNotificationQuietHoursLevel level,
                                                                      Int64 handle);
    
    internal delegate void IOSGetNotificationQuietHoursProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_notification_quiet_hours_proxy
    {
        public Int64 handle;
        public IOSGetNotificationQuietHoursProxyOnSuccess onSuccess;
        public IOSGetNotificationQuietHoursProxyOnError onError;
    }
    
#endregion
#region RCIMIWChangeConversationNotificationLevelCallback
    internal delegate void IOSChangeConversationNotificationLevelProxyOnConversationNotificationLevelChanged(int code,
                                                                                                             Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_change_conversation_notification_level_proxy
    {
        public Int64 handle;
        public IOSChangeConversationNotificationLevelProxyOnConversationNotificationLevelChanged
            onConversationNotificationLevelChanged;
    }
    
#endregion
#region RCIMIWGetConversationNotificationLevelCallback
    internal delegate void IOSGetConversationNotificationLevelProxyOnSuccess(RCIMPushNotificationLevel t, Int64 handle);
    
    internal delegate void IOSGetConversationNotificationLevelProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_conversation_notification_level_proxy
    {
        public Int64 handle;
        public IOSGetConversationNotificationLevelProxyOnSuccess onSuccess;
        public IOSGetConversationNotificationLevelProxyOnError onError;
    }
    
#endregion
#region RCIMIWChangeConversationTypeNotificationLevelCallback
    internal delegate void IOSChangeConversationTypeNotificationLevelProxyOnConversationTypeNotificationLevelChanged(
        int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_change_conversation_type_notification_level_proxy
    {
        public Int64 handle;
        public IOSChangeConversationTypeNotificationLevelProxyOnConversationTypeNotificationLevelChanged
            onConversationTypeNotificationLevelChanged;
    }
    
#endregion
#region RCIMIWGetConversationTypeNotificationLevelCallback
    internal delegate void IOSGetConversationTypeNotificationLevelProxyOnSuccess(RCIMPushNotificationLevel t, Int64 handle);
    
    internal delegate void IOSGetConversationTypeNotificationLevelProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_conversation_type_notification_level_proxy
    {
        public Int64 handle;
        public IOSGetConversationTypeNotificationLevelProxyOnSuccess onSuccess;
        public IOSGetConversationTypeNotificationLevelProxyOnError onError;
    }
    
#endregion
#region RCIMIWChangeUltraGroupDefaultNotificationLevelCallback
    internal delegate void IOSChangeUltraGroupDefaultNotificationLevelProxyOnUltraGroupDefaultNotificationLevelChanged(
        int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_change_ultra_group_default_notification_level_proxy
    {
        public Int64 handle;
        public IOSChangeUltraGroupDefaultNotificationLevelProxyOnUltraGroupDefaultNotificationLevelChanged
            onUltraGroupDefaultNotificationLevelChanged;
    }
    
#endregion
#region RCIMIWGetUltraGroupDefaultNotificationLevelCallback
    internal delegate void IOSGetUltraGroupDefaultNotificationLevelProxyOnSuccess(RCIMPushNotificationLevel t,
                                                                                  Int64 handle);
    
    internal delegate void IOSGetUltraGroupDefaultNotificationLevelProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_ultra_group_default_notification_level_proxy
    {
        public Int64 handle;
        public IOSGetUltraGroupDefaultNotificationLevelProxyOnSuccess onSuccess;
        public IOSGetUltraGroupDefaultNotificationLevelProxyOnError onError;
    }
    
#endregion
#region RCIMIWChangeUltraGroupChannelDefaultNotificationLevelCallback
    internal delegate void
    IOSChangeUltraGroupChannelDefaultNotificationLevelProxyOnUltraGroupChannelDefaultNotificationLevelChanged(int code,
                                                                                                              Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_change_ultra_group_channel_default_notification_level_proxy
    {
        public Int64 handle;
        public IOSChangeUltraGroupChannelDefaultNotificationLevelProxyOnUltraGroupChannelDefaultNotificationLevelChanged
            onUltraGroupChannelDefaultNotificationLevelChanged;
    }
    
#endregion
#region RCIMIWGetUltraGroupChannelDefaultNotificationLevelCallback
    internal delegate void IOSGetUltraGroupChannelDefaultNotificationLevelProxyOnSuccess(RCIMPushNotificationLevel t,
                                                                                         Int64 handle);
    
    internal delegate void IOSGetUltraGroupChannelDefaultNotificationLevelProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_ultra_group_channel_default_notification_level_proxy
    {
        public Int64 handle;
        public IOSGetUltraGroupChannelDefaultNotificationLevelProxyOnSuccess onSuccess;
        public IOSGetUltraGroupChannelDefaultNotificationLevelProxyOnError onError;
    }
    
#endregion
#region RCIMIWChangePushContentShowStatusCallback
    internal delegate void IOSChangePushContentShowStatusProxyOnPushContentShowStatusChanged(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_change_push_content_show_status_proxy
    {
        public Int64 handle;
        public IOSChangePushContentShowStatusProxyOnPushContentShowStatusChanged onPushContentShowStatusChanged;
    }
    
#endregion
#region RCIMIWChangePushLanguageCallback
    internal delegate void IOSChangePushLanguageProxyOnPushLanguageChanged(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_change_push_language_proxy
    {
        public Int64 handle;
        public IOSChangePushLanguageProxyOnPushLanguageChanged onPushLanguageChanged;
    }
    
#endregion
#region RCIMIWChangePushReceiveStatusCallback
    internal delegate void IOSChangePushReceiveStatusProxyOnPushReceiveStatusChanged(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_change_push_receive_status_proxy
    {
        public Int64 handle;
        public IOSChangePushReceiveStatusProxyOnPushReceiveStatusChanged onPushReceiveStatusChanged;
    }
    
#endregion
#region RCIMIWSendGroupMessageToDesignatedUsersCallback
    internal delegate void IOSSendGroupMessageToDesignatedUsersProxyOnMessageSaved(IntPtr message, Int64 handle);
    
    internal delegate void IOSSendGroupMessageToDesignatedUsersProxyOnMessageSent(int code, IntPtr message, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_send_group_message_to_designated_users_proxy
    {
        public Int64 handle;
        public IOSSendGroupMessageToDesignatedUsersProxyOnMessageSaved onMessageSaved;
        public IOSSendGroupMessageToDesignatedUsersProxyOnMessageSent onMessageSent;
    }
    
#endregion
#region RCIMIWGetMessageCountCallback
    internal delegate void IOSGetMessageCountProxyOnSuccess(int t, Int64 handle);
    
    internal delegate void IOSGetMessageCountProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_message_count_proxy
    {
        public Int64 handle;
        public IOSGetMessageCountProxyOnSuccess onSuccess;
        public IOSGetMessageCountProxyOnError onError;
    }
    
#endregion
#region RCIMIWGetTopConversationsCallback
    internal delegate void IOSGetTopConversationsProxyOnSuccess(ref ios_c_list t, Int64 handle);
    
    internal delegate void IOSGetTopConversationsProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_top_conversations_proxy
    {
        public Int64 handle;
        public IOSGetTopConversationsProxyOnSuccess onSuccess;
        public IOSGetTopConversationsProxyOnError onError;
    }
    
#endregion
#region RCIMIWSyncUltraGroupReadStatusCallback
    internal delegate void IOSSyncUltraGroupReadStatusProxyOnUltraGroupReadStatusSynced(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_sync_ultra_group_read_status_proxy
    {
        public Int64 handle;
        public IOSSyncUltraGroupReadStatusProxyOnUltraGroupReadStatusSynced onUltraGroupReadStatusSynced;
    }
    
#endregion
#region RCIMIWGetConversationsForAllChannelCallback
    internal delegate void IOSGetConversationsForAllChannelProxyOnSuccess(ref ios_c_list t, Int64 handle);
    
    internal delegate void IOSGetConversationsForAllChannelProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_conversations_for_all_channel_proxy
    {
        public Int64 handle;
        public IOSGetConversationsForAllChannelProxyOnSuccess onSuccess;
        public IOSGetConversationsForAllChannelProxyOnError onError;
    }
    
#endregion
#region RCIMIWModifyUltraGroupMessageCallback
    internal delegate void IOSModifyUltraGroupMessageProxyOnUltraGroupMessageModified(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_modify_ultra_group_message_proxy
    {
        public Int64 handle;
        public IOSModifyUltraGroupMessageProxyOnUltraGroupMessageModified onUltraGroupMessageModified;
    }
    
#endregion
#region RCIMIWRecallUltraGroupMessageCallback
    internal delegate void IOSRecallUltraGroupMessageProxyOnUltraGroupMessageRecalled(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_recall_ultra_group_message_proxy
    {
        public Int64 handle;
        public IOSRecallUltraGroupMessageProxyOnUltraGroupMessageRecalled onUltraGroupMessageRecalled;
    }
    
#endregion
#region RCIMIWClearUltraGroupMessagesCallback
    internal delegate void IOSClearUltraGroupMessagesProxyOnUltraGroupMessagesCleared(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_clear_ultra_group_messages_proxy
    {
        public Int64 handle;
        public IOSClearUltraGroupMessagesProxyOnUltraGroupMessagesCleared onUltraGroupMessagesCleared;
    }
    
#endregion
#region RCIMIWSendUltraGroupTypingStatusCallback
    internal delegate void IOSSendUltraGroupTypingStatusProxyOnUltraGroupTypingStatusSent(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_send_ultra_group_typing_status_proxy
    {
        public Int64 handle;
        public IOSSendUltraGroupTypingStatusProxyOnUltraGroupTypingStatusSent onUltraGroupTypingStatusSent;
    }
    
#endregion
#region RCIMIWClearUltraGroupMessagesForAllChannelCallback
    internal delegate void IOSClearUltraGroupMessagesForAllChannelProxyOnUltraGroupMessagesClearedForAllChannel(
        int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_clear_ultra_group_messages_for_all_channel_proxy
    {
        public Int64 handle;
        public IOSClearUltraGroupMessagesForAllChannelProxyOnUltraGroupMessagesClearedForAllChannel
            onUltraGroupMessagesClearedForAllChannel;
    }
    
#endregion
#region RCIMIWGetBatchRemoteUltraGroupMessagesCallback
    internal delegate void IOSGetBatchRemoteUltraGroupMessagesProxyOnSuccess(ref ios_c_list matchedMessages,
                                                                             ref ios_c_list notMatchedMessages,
                                                                             Int64 handle);
    
    internal delegate void IOSGetBatchRemoteUltraGroupMessagesProxyOnError(int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_get_batch_remote_ultra_group_messages_proxy
    {
        public Int64 handle;
        public IOSGetBatchRemoteUltraGroupMessagesProxyOnSuccess onSuccess;
        public IOSGetBatchRemoteUltraGroupMessagesProxyOnError onError;
    }
    
#endregion
#region RCIMIWUpdateUltraGroupMessageExpansionCallback
    internal delegate void IOSUpdateUltraGroupMessageExpansionProxyOnUltraGroupMessageExpansionUpdated(int code,
                                                                                                       Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_update_ultra_group_message_expansion_proxy
    {
        public Int64 handle;
        public IOSUpdateUltraGroupMessageExpansionProxyOnUltraGroupMessageExpansionUpdated
            onUltraGroupMessageExpansionUpdated;
    }
    
#endregion
#region RCIMIWRemoveUltraGroupMessageExpansionForKeysCallback
    internal delegate void IOSRemoveUltraGroupMessageExpansionForKeysProxyOnUltraGroupMessageExpansionForKeysRemoved(
        int code, Int64 handle);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_remove_ultra_group_message_expansion_for_keys_proxy
    {
        public Int64 handle;
        public IOSRemoveUltraGroupMessageExpansionForKeysProxyOnUltraGroupMessageExpansionForKeysRemoved
            onUltraGroupMessageExpansionForKeysRemoved;
    }
    
#endregion
    
#endregion
}
    
#endif