
#if UNITY_STANDALONE_WIN
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace cn_rongcloud_im_unity
{
    public partial class RCIMClientWin : RCIMClient
    {
        private OperationCallbackWithResult<string> OnIMConnectStatus { set; get; }

        private IntPtr im_client = IntPtr.Zero;

        public RCIMClientWin()
        {

        }

        public override void Destroy()
        {
            OnIMConnectStatus = null;
            if (im_client != IntPtr.Zero)
            {
                NativeWin.rcim_uninit(im_client);
                im_client = IntPtr.Zero;
            }
            
            base.Destroy();
        }

        #region override RCIMClient methods
        public override IntPtr Handler()
        {
            return im_client;
        }

        public override void Init(string appKey)
        {
            Int32 errorCode = 0;
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(errorCode));
            Marshal.StructureToPtr(errorCode, ptr, false);
            im_client = NativeWin.rcim_init(appKey, "", "", ptr);
            Debug.Log("IMClient≥ı ºªØ¥ÌŒÛ¬Î «" + errorCode);
            Marshal.FreeHGlobal(ptr);
            NativeWin.rcim_set_connection_callback(im_client, on_im_connect_status, IntPtr.Zero);
        }

        public override void Connect(string token, OperationCallbackWithResult<string> callback)
        {
            if (im_client != IntPtr.Zero)
            {
                OnIMConnectStatus = callback;
                NativeWin.rcim_connect(im_client, token);
            }
        }

        public override void Logout()
        {
            if (im_client != IntPtr.Zero)
            {
                NativeWin.rcim_disconnect(im_client);
            }
        }

        public override void Disconnect()
        {
            if (im_client != IntPtr.Zero)
            {
                NativeWin.rcim_disconnect(im_client);
            }
        }

        public override string GetCurrentUserID()
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

        public override void SendMessage(RCConversationType type, string targetId, RCMessageContent content,
            bool disableNotification = false)
        {
            
        }

        public override void SendMessage(RCMessage message, string pushContent = null, string pushData = null)
        {
            
        }

        public override void SendMessageCarriesPush(RCConversationType type, string targetId, RCMessageContent content,
            string pushContent, string pushData, bool disableNotification = false)
        {
            
        }

        public override RCConnectionStatus GetConnectionStatus()
        {
            return RCConnectionStatus.Connected;
        }

        public override void GetConversation(RCConversationType type, string targetId,
            OperationCallbackWithResult<RCConversation> callback)
        {
            
        }

        public override void RemoveConversation(RCConversationType type, string targetId, OperationCallback callback)
        {
            
        }

        public override void GetConversationList(OperationCallbackWithResult<IList<RCConversation>> callback,
            params RCConversationType[] conversationTypes)
        {
            
        }

        public override void GetConversationListByPage(OperationCallbackWithResult<IList<RCConversation>> callback,
            long timestamp, int count, params RCConversationType[] conversationTypes)
        {
            
        }

        public override void GetBlockedConversationList(OperationCallbackWithResult<IList<RCConversation>> callback,
            params RCConversationType[] conversationTypes)
        {
            
        }

        public override void GetTopConversationList(OperationCallbackWithResult<IList<RCConversation>> callback,
            params RCConversationType[] conversationTypes)
        {
           
        }

        public override void ClearConversations(OperationCallback callback,
            params RCConversationType[] conversationTypes)
        {
           
        }

        public override void GetTextMessageDraft(RCConversationType type, string targetId,
            OperationCallbackWithResult<string> callback)
        {
           
        }

        public override void SaveTextMessageDraft(RCConversationType type, string targetId, string textDraft,
            OperationCallback callback)
        {
            
        }

        public override void ClearTextMessageDraft(RCConversationType type, string targetId, OperationCallback callback)
        {
           
        }

        public override void GetUnreadCount(RCConversationType type, string targetId,
            OperationCallbackWithResult<int> callback)
        {
            
        }

        public override void GetUnreadCountByTag(string tagId, bool containsDND,
            OperationCallbackWithResult<int> callback)
        {
            

        }

        public override void GetUnreadCountByConversationTypes(bool containsDND,
            OperationCallbackWithResult<int> callback, params RCConversationType[] conversationTypes)
        {
            
        }

        public override void GetTotalUnreadCount(OperationCallbackWithResult<int> callback)
        {
            
        }

        public override void ClearMessageUnreadStatus(RCConversationType type, string targetId,
            OperationCallback callback)
        {
            
        }

        public override void SetConversationNotificationStatus(RCConversationType type, string targetId,
            RCConversationNotificationStatus notificationStatus,
            OperationCallbackWithResult<RCConversationNotificationStatus> callback)
        {
            
        }

        public override void GetConversationNotificationStatus(RCConversationType type, string targetId,
            OperationCallbackWithResult<RCConversationNotificationStatus> callback)
        {
           
        }

        public override void SetConversationToTop(RCConversationType type, string targetId, bool isTop,
            OperationCallback callback)
        {
            
        }

        public override void SetConversationToTopInTag(RCConversationType type, string targetId, string tagId,
            bool isTop, OperationCallback callback)
        {
            
        }

        public override void GetConversationTopStatusInTag(RCConversationType type, string targetId, string tagId,
            OperationCallbackWithResult<bool> callback)
        {
            
        }

        public override void SetOfflineMessageDuration(int duration, OperationCallbackWithResult<long> callback)
        {
            
        }

        public override void GetOfflineMessageDuration(OperationCallbackWithResult<long> callback)
        {
            
        }

        public override void SetAndroidPushConfig(RCAndroidPushConfig androidPushConfig)
        {

        }

        public override void GetMessage(Int64 messageId, OperationCallbackWithResult<RCMessage> callback)
        {
            
        }

        public override void GetMessageByUid(string messageUid, OperationCallbackWithResult<RCMessage> callback)
        {
            
        }

        public override void CancelDownloadMediaMessage(RCMessage message)
        {
            if (message != null)
            {
               
            }
        }

        public override void BatchInsertMessage(IList<RCMessage> messageList,
            OperationCallbackWithResult<bool> callback)
        {
            
        }

        public override void SearchConversations(string keyword, RCConversationType[] conversationTypes,
            string[] objectNames, OperationCallbackWithResult<IList<RCSearchConversationResult>> callback)
        {
            
        }

        public override void SendTypingStatus(RCConversationType type, string targetId, string typingContent)
        {
            
        }

        public override void SendDirectionalMessage(RCConversationType type, string targetId, IList<string> userIdList,
            RCMessageContent content, string pushContent = null, string pushData = null)
        {
            
        }

        public override void SendMediaMessage(RCMessage message, string pushContent = null, string pushData = null)
        {
           
        }

        public override void DownloadMediaMessage(RCMessage rcMsg)
        {
           
        }

        public override void InsertIncomingMessage(RCConversationType type, string targetId, string senderId,
            RCReceivedStatus receivedStatus, RCMessageContent content, long sentTime,
            OperationCallbackWithResult<RCMessage> callback)
        {
           
        }

        public override void InsertOutgoingMessage(RCConversationType type, string targetId, RCSentStatus sentStatus,
            RCMessageContent content, long sendTime, OperationCallbackWithResult<RCMessage> callback)
        {
            
        }

        public override void RecallMessage(RCMessage rcMsg, string pushContent,
            OperationCallbackWithResult<RCRecallNotificationMessage> callback)
        {
            
        }

        public override void ForwardMessageByStep(RCConversationType type, string targetId, RCMessage message)
        {
            
        }

        public override void GetChatRoomHistoryMessages(string targetId, long recordTime, int count, bool asc,
            OperationCallbackWithResult<RCChatRoomHistoryMessageResult> callback)
        {
            
        }

        public override void GetHistoryMessages(RCConversationType type, string targetId, Int64 lastMessageId,
            int count,
            OperationCallbackWithResult<IList<RCMessage>> callback)
        {
            
        }

        public override void GetHistoryMessages(RCConversationType type, string targetId, long sentTime,
            int beforeCount, int afterCount, OperationCallbackWithResult<IList<RCMessage>> callback)
        {
           
        }

        public override void GetRemoteHistoryMessages(RCConversationType type, string targetId, long dateTime,
            int count, OperationCallbackWithResult<IList<RCMessage>> callback)
        {
            
        }

        public override void SearchMessages(RCConversationType type, string targetId, string keyword, int count,
            long beginTime, OperationCallbackWithResult<IList<RCMessage>> callback)
        {
            
        }

        public override void DeleteMessages(Int64[] messageIdList, OperationCallbackWithResult<bool> callback)
        {
           
        }

        public override void ClearMessages(RCConversationType type, string targetId,
            OperationCallbackWithResult<bool> callback)
        {
            
        }

        public override void ClearHistoryMessages(RCConversationType type, string targetId, long recordTime,
            bool clearRemote, OperationCallback callback)
        {
            
        }

        public override void SetMessageReceivedStatus(Int64 messageId, RCReceivedStatus status,
            OperationCallbackWithResult<bool> callback)
        {
            
        }

        public override void SetMessageSentStatus(Int64 messageId, RCSentStatus sentStatus,
            OperationCallbackWithResult<bool> callback)
        {
            
        }

        public override void SendReadReceiptMessage(RCConversationType type, string targetId, long timestamp,
            OperationCallback callback)
        {
            
        }

        public override void SendReadReceiptRequest(RCMessage message, OperationCallback callback)
        {
            
        }

        public override void SendReadReceiptResponse(RCConversationType type, string targetId,
            IList<RCMessage> messageList, OperationCallback callback)
        {
            
        }

        public override void UpdateMessageExpansion(string messageUid, IDictionary<string, string> expansionDic,
            OperationCallback callback)
        {
            
        }

        public override void RemoveMessageExpansionForKey(string messageUid, IList<string> keyList,
            OperationCallback callback)
        {
            
        }

        public override void GetFirstUnreadMessage(RCConversationType type, string targetId,
            OperationCallbackWithResult<RCMessage> callback)
        {
            
        }

        public override void GetUnreadMentionedMessages(RCConversationType type, string targetId,
            OperationCallbackWithResult<IList<RCMessage>> callback)
        {
            
        }

        public override void SetMessageExtra(Int64 messageId, string extra, OperationCallbackWithResult<bool> callback)
        {
           
        }

        public override void DeleteRemoteMessages(RCConversationType type, string targetId,
            IList<RCMessage> messageList, OperationCallback callback)
        {
           
        }

        public override void AddToBlackList(string userId, OperationCallback callback)
        {
            
        }

        public override void RemoveFromBlackList(string userId, OperationCallback callback)
        {
            
        }

        public override void GetBlackList(OperationCallbackWithResult<IList<string>> callback)
        {
            
        }

        public override void GetBlackListStatus(string userId, OperationCallbackWithResult<RCBlackListStatus> callback)
        {
            
        }

        public override void SetKickReconnectedDevice(bool kick)
        {
           
        }

        public override long GetDeltaTime()
        {
            return 0;
        }

        public override void SyncConversationReadStatus(RCConversationType type, string targetId, long timestamp,
            OperationCallback callback)
        {
            
        }

        public override void GetMessages(RCConversationType type, string targetId, RCHistoryMessageOption option,
            OperationCallbackWithResult<IList<RCMessage>> callback)
        {
            
        }

        public override void JoinChatRoom(string roomId, OperationCallback callback, int msgCount = 10)
        {
           
        }

        public override void QuitChatRoom(string roomId, OperationCallback callback)
        {
            
        }

        public override void JoinExistChatRoom(string roomId, OperationCallback callback, int msgCount = 10)
        {
            
        }

        public override void GetChatRoomInfo(string roomId, int memberCount, RCChatRoomMemberOrder order,
            OperationCallbackWithResult<RCChatRoomInfo> callback)
        {
            
        }

        public override void SetChatRoomEntry(string roomId, string key, string value, bool sendNotification,
            bool autoRemove, string notificationExtra, OperationCallback callback)
        {
           
        }

        public override void ForceSetChatRoomEntry(string roomId, string key, string value, bool sendNotification,
            bool autoRemove, string notificationExtra, OperationCallback callback)
        {
           
        }

        public override void RemoveChatRoomEntry(string roomId, string key, bool sendNotification,
            string notificationExtra, OperationCallback callback)
        {
            
        }

        public override void ForceRemoveChatRoomEntry(string roomId, string key, bool sendNotification,
            string notificationExtra, OperationCallback callback)
        {
           
        }

        public override void GetChatRoomEntry(string roomId, string key,
            OperationCallbackWithResult<IDictionary<string, string>> callback)
        {
           
        }

        public override void GetChatRoomAllEntries(string roomId,
            OperationCallbackWithResult<IDictionary<string, string>> callback)
        {
            
        }

        public override void GetNotificationQuietHours(
            OperationCallbackWithResult<RCNotificationQuietHourInfo> callback)
        {
            
        }

        public override void SetNotificationQuietHours(string startTime, int spanMinutes, OperationCallback callback)
        {
           
        }

        public override void RemoveNotificationQuietHours(OperationCallback callback)
        {
            
        }

        public override void AddTag(RCTagInfo tagInfo, OperationCallback callback)
        {
           
        }

        public override void UpdateTag(RCTagInfo tagInfo, OperationCallback callback)
        {
            
        }

        public override void RemoveTag(RCTagInfo tagInfo, OperationCallback callback)
        {
            
        }

        public override void GetTags(OperationCallbackWithResult<IList<RCTagInfo>> callback)
        {
            
        }

        public override void AddConversationsToTag(string tagId, IList<RCConversationIdentifier> conversations,
            OperationCallback callback)
        {
            
        }

        public override void GetConversationsFromTagByPage(string tagId, long timestamp, int count,
            OperationCallbackWithResult<IList<RCConversation>> callback)
        {
            
        }

        public override void GetTagsFromConversation(RCConversationType type, string targetId,
            OperationCallbackWithResult<IList<RCConversationTagInfo>> callback)
        {
            
        }

        public override void RemoveConversationFromTag(string tagId,
            IList<RCConversationIdentifier> conversationIdentifiers, OperationCallback callback)
        {
            
        }

        public override void SetServerInfo(string naviServer, string fileServer)
        {
            
        }

        #endregion
    }
}

#endif