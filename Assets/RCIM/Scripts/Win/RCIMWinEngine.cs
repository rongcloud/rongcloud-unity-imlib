
#if UNITY_STANDALONE_WIN
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace cn_rongcloud_im_unity
{
    public partial class RCIMWinEngine : RCIMEngine
    {
        internal IntPtr im_client = IntPtr.Zero;

        public RCIMWinEngine(string appKey, RCIMEngineOptions options = null)
        {
            RCUnityLogger.getInstance().log("RCIMWinEngine", $"appKey={appKey},options={options}");
            Int32 errorCode = 0;
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(errorCode));
            Marshal.StructureToPtr(errorCode, ptr, false);
            im_client = NativeWin.rcim_init(appKey, "", "", ptr);
            Debug.Log("RCIMWinEngine" + errorCode);
            Marshal.FreeHGlobal(ptr);
            NativeWin.rcim_set_connection_callback(im_client, on_im_connect_status, IntPtr.Zero);
        }

        public override void Destroy()
        {
            RCUnityLogger.getInstance().log("destroy");
            if (im_client != IntPtr.Zero)
            {
                NativeWin.rcim_uninit(im_client);
                im_client = IntPtr.Zero;
            }
            base.Destroy();
        }

        public IntPtr Handler()
        {
            return im_client;
        }

        public override void GetMessageById(int messageId, OnGetMessageDelegate onGetMessage)
        {
            RCUnityLogger.getInstance().log("getMessageById", $"messageId={messageId}");
            
        }

        public override void GetMessageByUId(String messageUId, OnGetMessageDelegate onGetMessage)
        {
            RCUnityLogger.getInstance().log("getMessageByUId", $"messageUId={messageUId}");
            
        }

        #region Override RCIMIWEngine Method

        public override int Connect(string token, int timeout)
        {
            RCUnityLogger.getInstance().log("connect", $"token={token},timeout={timeout}");
            if (im_client != IntPtr.Zero)
            {
                return NativeWin.rcim_connect(im_client, token);
            }
            return -1;
        }
    
        public override int Disconnect(bool receivePush)
        {
            RCUnityLogger.getInstance().log("disconnect", $"receivePush={receivePush}");
            if (im_client != IntPtr.Zero)
            {
                return NativeWin.rcim_disconnect(im_client);
            }
            return -1;
        }
#endregion

        private string GetCurrentUserID()
        {
            if (im_client != IntPtr.Zero)
            {
                string user = "";
                IntPtr ptr = Marshal.AllocHGlobal(2048);
                int ret = NativeWin.rcim_get_user_id(im_client,ptr, 2048);
                if (ret == 0)
                {
                    user = Marshal.PtrToStringAnsi(ptr);
                }
                Marshal.FreeHGlobal(ptr);
                return user;
            }
            return "";
        }

        public override RCIMTextMessage CreateTextMessage(RCIMConversationType type, string targetId, string channelId, string text)
        {
            throw new NotImplementedException();
        }

        public override RCIMImageMessage CreateImageMessage(RCIMConversationType type, string targetId, string channelId, string path)
        {
            throw new NotImplementedException();
        }

        public override RCIMFileMessage CreateFileMessage(RCIMConversationType type, string targetId, string channelId, string path)
        {
            throw new NotImplementedException();
        }

        public override RCIMSightMessage CreateSightMessage(RCIMConversationType type, string targetId, string channelId, string path, int duration)
        {
            throw new NotImplementedException();
        }

        public override RCIMVoiceMessage CreateVoiceMessage(RCIMConversationType type, string targetId, string channelId, string path, int duration)
        {
            throw new NotImplementedException();
        }

        public override RCIMReferenceMessage CreateReferenceMessage(RCIMConversationType type, string targetId, string channelId, RCIMMessage referenceMessage, string text)
        {
            throw new NotImplementedException();
        }

        public override RCIMGIFMessage CreateGIFMessage(RCIMConversationType type, string targetId, string channelId, string path)
        {
            throw new NotImplementedException();
        }

        public override RCIMCustomMessage CreateCustomMessage(RCIMConversationType type, string targetId, string channelId, RCIMCustomMessagePolicy policy, string messageIdentifier, Dictionary<string, string> fields)
        {
            throw new NotImplementedException();
        }

        public override RCIMLocationMessage CreateLocationMessage(RCIMConversationType type, string targetId, string channelId, double longitude, double latitude, string poiName, string thumbnailPath)
        {
            throw new NotImplementedException();
        }

        public override int SendMessage(RCIMMessage message)
        {
            throw new NotImplementedException();
        }

        public override int SendMediaMessage(RCIMMediaMessage message)
        {
            throw new NotImplementedException();
        }

        public override int CancelSendingMediaMessage(RCIMMediaMessage message)
        {
            throw new NotImplementedException();
        }

        public override int DownloadMediaMessage(RCIMMediaMessage message)
        {
            throw new NotImplementedException();
        }

        public override int CancelDownloadingMediaMessage(RCIMMediaMessage message)
        {
            throw new NotImplementedException();
        }

        public override int LoadConversation(RCIMConversationType type, string targetId, string channelId)
        {
            throw new NotImplementedException();
        }

        public override int LoadConversations(List<RCIMConversationType> conversationTypes, string channelId, long startTime, int count)
        {
            throw new NotImplementedException();
        }

        public override int RemoveConversation(RCIMConversationType type, string targetId, string channelId)
        {
            throw new NotImplementedException();
        }

        public override int RemoveConversations(List<RCIMConversationType> conversationTypes, string channelId)
        {
            throw new NotImplementedException();
        }

        public override int LoadUnreadCount(RCIMConversationType type, string targetId, string channelId)
        {
            throw new NotImplementedException();
        }

        public override int LoadTotalUnreadCount(string channelId)
        {
            throw new NotImplementedException();
        }

        public override int LoadUnreadMentionedCount(RCIMConversationType type, string targetId, string channelId)
        {
            throw new NotImplementedException();
        }

        public override int LoadUltraGroupAllUnreadCount()
        {
            throw new NotImplementedException();
        }

        public override int LoadUltraGroupAllUnreadMentionedCount()
        {
            throw new NotImplementedException();
        }

        public override int LoadUltraGroupUnreadCount(string targetId)
        {
            throw new NotImplementedException();
        }

        public override int LoadUltraGroupUnreadMentionedCount(string targetId)
        {
            throw new NotImplementedException();
        }

        public override int LoadUnreadCountByConversationTypes(List<RCIMConversationType> conversationTypes, string channelId, bool contain)
        {
            throw new NotImplementedException();
        }

        public override int ClearUnreadCount(RCIMConversationType type, string targetId, string channelId, long timestamp)
        {
            throw new NotImplementedException();
        }

        public override int SaveDraftMessage(RCIMConversationType type, string targetId, string channelId, string draft)
        {
            throw new NotImplementedException();
        }

        public override int LoadDraftMessage(RCIMConversationType type, string targetId, string channelId)
        {
            throw new NotImplementedException();
        }

        public override int ClearDraftMessage(RCIMConversationType type, string targetId, string channelId)
        {
            throw new NotImplementedException();
        }

        public override int LoadBlockedConversations(List<RCIMConversationType> conversationTypes, string channelId)
        {
            throw new NotImplementedException();
        }

        public override int ChangeConversationTopStatus(RCIMConversationType type, string targetId, string channelId, bool top)
        {
            throw new NotImplementedException();
        }

        public override int LoadConversationTopStatus(RCIMConversationType type, string targetId, string channelId)
        {
            throw new NotImplementedException();
        }

        public override int SyncConversationReadStatus(RCIMConversationType type, string targetId, string channelId, long timestamp)
        {
            throw new NotImplementedException();
        }

        public override int SendTypingStatus(RCIMConversationType type, string targetId, string channelId, string currentType)
        {
            throw new NotImplementedException();
        }

        public override int LoadMessages(RCIMConversationType type, string targetId, string channelId, long sentTime, RCIMTimeOrder order, RCIMMessageOperationPolicy policy, int count)
        {
            throw new NotImplementedException();
        }

        public override int LoadFirstUnreadMessage(RCIMConversationType type, string targetId, string channelId)
        {
            throw new NotImplementedException();
        }

        public override int LoadUnreadMentionedMessages(RCIMConversationType type, string targetId, string channelId)
        {
            throw new NotImplementedException();
        }

        public override int InsertMessage(RCIMMessage message)
        {
            throw new NotImplementedException();
        }

        public override int InsertMessages(List<RCIMMessage> messages)
        {
            throw new NotImplementedException();
        }

        public override int ClearMessages(RCIMConversationType type, string targetId, string channelId, long timestamp, RCIMMessageOperationPolicy policy)
        {
            throw new NotImplementedException();
        }

        public override int DeleteLocalMessages(List<RCIMMessage> messages)
        {
            throw new NotImplementedException();
        }

        public override int DeleteMessages(RCIMConversationType type, string targetId, string channelId, List<RCIMMessage> messages)
        {
            throw new NotImplementedException();
        }

        public override int RecallMessage(RCIMMessage message)
        {
            throw new NotImplementedException();
        }

        public override int SendPrivateReadReceiptMessage(string targetId, string channelId, long timestamp)
        {
            throw new NotImplementedException();
        }

        public override int SendGroupReadReceiptRequest(RCIMMessage message)
        {
            throw new NotImplementedException();
        }

        public override int SendGroupReadReceiptResponse(string targetId, string channelId, List<RCIMMessage> messages)
        {
            throw new NotImplementedException();
        }

        public override int UpdateMessageExpansion(string messageUId, Dictionary<string, string> expansion)
        {
            throw new NotImplementedException();
        }

        public override int RemoveMessageExpansionForKeys(string messageUId, List<string> keys)
        {
            throw new NotImplementedException();
        }

        public override int ChangeMessageSentStatus(int messageId, RCIMSentStatus sentStatus)
        {
            throw new NotImplementedException();
        }

        public override int ChangeMessageReceiveStatus(int messageId, RCIMReceivedStatus receivedStatus)
        {
            throw new NotImplementedException();
        }

        public override int JoinChatRoom(string targetId, int messageCount, bool autoCreate)
        {
            throw new NotImplementedException();
        }

        public override int LeaveChatRoom(string targetId)
        {
            throw new NotImplementedException();
        }

        public override int LoadChatRoomMessages(string targetId, long timestamp, RCIMTimeOrder order, int count)
        {
            throw new NotImplementedException();
        }

        public override int AddChatRoomEntry(string targetId, string key, string value, bool deleteWhenLeft, bool overwrite)
        {
            throw new NotImplementedException();
        }

        public override int AddChatRoomEntries(string targetId, Dictionary<string, string> entries, bool deleteWhenLeft, bool overwrite)
        {
            throw new NotImplementedException();
        }

        public override int LoadChatRoomEntry(string targetId, string key)
        {
            throw new NotImplementedException();
        }

        public override int LoadAllChatRoomEntries(string targetId)
        {
            throw new NotImplementedException();
        }

        public override int RemoveChatRoomEntry(string targetId, string key, bool force)
        {
            throw new NotImplementedException();
        }

        public override int RemoveChatRoomEntries(string targetId, List<string> keys, bool force)
        {
            throw new NotImplementedException();
        }

        public override int AddToBlacklist(string userId)
        {
            throw new NotImplementedException();
        }

        public override int RemoveFromBlacklist(string userId)
        {
            throw new NotImplementedException();
        }

        public override int LoadBlacklistStatus(string userId)
        {
            throw new NotImplementedException();
        }

        public override int LoadBlacklist()
        {
            throw new NotImplementedException();
        }

        public override int SearchMessages(RCIMConversationType type, string targetId, string channelId, string keyword, long startTime, int count)
        {
            throw new NotImplementedException();
        }

        public override int SearchMessagesByTimeRange(RCIMConversationType type, string targetId, string channelId, string keyword, long startTime, long endTime, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override int SearchMessagesByUserId(string userId, RCIMConversationType type, string targetId, string channelId, long startTime, int count)
        {
            throw new NotImplementedException();
        }

        public override int SearchConversations(List<RCIMConversationType> conversationTypes, string channelId, List<RCIMMessageType> messageTypes, string keyword)
        {
            throw new NotImplementedException();
        }

        public override int ChangeNotificationQuietHours(string startTime, int spanMins, RCIMPushNotificationQuietHoursLevel level)
        {
            throw new NotImplementedException();
        }

        public override int RemoveNotificationQuietHours()
        {
            throw new NotImplementedException();
        }

        public override int LoadNotificationQuietHours()
        {
            throw new NotImplementedException();
        }

        public override int ChangeConversationNotificationLevel(RCIMConversationType type, string targetId, string channelId, RCIMPushNotificationLevel level)
        {
            throw new NotImplementedException();
        }

        public override int LoadConversationNotificationLevel(RCIMConversationType type, string targetId, string channelId)
        {
            throw new NotImplementedException();
        }

        public override int ChangeConversationTypeNotificationLevel(RCIMConversationType type, RCIMPushNotificationLevel level)
        {
            throw new NotImplementedException();
        }

        public override int LoadConversationTypeNotificationLevel(RCIMConversationType type)
        {
            throw new NotImplementedException();
        }

        public override int ChangeUltraGroupDefaultNotificationLevel(string targetId, RCIMPushNotificationLevel level)
        {
            throw new NotImplementedException();
        }

        public override int LoadUltraGroupDefaultNotificationLevel(string targetId)
        {
            throw new NotImplementedException();
        }

        public override int ChangeUltraGroupChannelDefaultNotificationLevel(string targetId, string channelId, RCIMPushNotificationLevel level)
        {
            throw new NotImplementedException();
        }

        public override int LoadUltraGroupChannelDefaultNotificationLevel(string targetId, string channelId)
        {
            throw new NotImplementedException();
        }

        public override int ChangePushContentShowStatus(bool showContent)
        {
            throw new NotImplementedException();
        }

        public override int ChangePushLanguage(string language)
        {
            throw new NotImplementedException();
        }

        public override int ChangePushReceiveStatus(bool receive)
        {
            throw new NotImplementedException();
        }

        public override int SendGroupMessageToDesignatedUsers(RCIMMessage message, List<string> userIds)
        {
            throw new NotImplementedException();
        }

        public override int LoadMessageCount(RCIMConversationType type, string targetId, string channelId)
        {
            throw new NotImplementedException();
        }

        public override int LoadTopConversations(List<RCIMConversationType> conversationTypes, string channelId)
        {
            throw new NotImplementedException();
        }

        public override int SyncUltraGroupReadStatus(string targetId, string channelId, long timestamp)
        {
            throw new NotImplementedException();
        }

        public override int LoadConversationsForAllChannel(RCIMConversationType type, string targetId)
        {
            throw new NotImplementedException();
        }

        public override int ModifyUltraGroupMessage(string messageUId, RCIMMessage message)
        {
            throw new NotImplementedException();
        }

        public override int RecallUltraGroupMessage(RCIMMessage message, bool deleteRemote)
        {
            throw new NotImplementedException();
        }

        public override int ClearUltraGroupMessages(string targetId, string channelId, long timestamp, RCIMMessageOperationPolicy policy)
        {
            throw new NotImplementedException();
        }

        public override int SendUltraGroupTypingStatus(string targetId, string channelId, RCIMUltraGroupTypingStatus typingStatus)
        {
            throw new NotImplementedException();
        }

        public override int ClearUltraGroupMessagesForAllChannel(string targetId, long timestamp)
        {
            throw new NotImplementedException();
        }

        public override int LoadBatchRemoteUltraGroupMessages(List<RCIMMessage> messages)
        {
            throw new NotImplementedException();
        }

        public override int UpdateUltraGroupMessageExpansion(string messageUId, Dictionary<string, string> expansion)
        {
            throw new NotImplementedException();
        }

        public override int RemoveUltraGroupMessageExpansion(string messageUId, List<string> keys)
        {
            throw new NotImplementedException();
        }

        public override int ChangeLogLevel(RCIMLogLevel level)
        {
            throw new NotImplementedException();
        }
    }
}

#endif
