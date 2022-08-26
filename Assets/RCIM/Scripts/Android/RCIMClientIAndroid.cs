//
//  Copyright © 2021 RongCloud. All rights reserved.
//

#if UNITY_ANDROID

using System;
using System.Collections.Generic;
using UnityEngine;

namespace cn_rongcloud_im_unity
{
    public class RCIMClientAndroid : RCIMClient
    {
        private bool mEnablePush = true;
        private bool mDisableIPC = false;

        private AndroidJavaClass _nativeClient = new AndroidJavaClass("io.rong.imlib.RongIMClient");
        private AndroidJavaClass _proxyClient = new AndroidJavaClass("cn.rongcloud.im.unity.RongUnityIM");
        private AndroidJavaClass _chatRoomClient = new AndroidJavaClass("io.rong.imlib.chatroom.base.RongChatRoomClient");
        private AndroidJavaClass _coreClient = new AndroidJavaClass("io.rong.imlib.RongCoreClient");

        private AndroidJavaObject GetContext()
        {
            AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject javaObject = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
            return javaObject;
        }

        private AndroidJavaObject GetInstance()
        {
            return _nativeClient.CallStatic<AndroidJavaObject>("getInstance");
        }

        private AndroidJavaObject CoreInstance()
        {
            return _coreClient.CallStatic<AndroidJavaObject>("getInstance");
        }

        private AndroidJavaObject ChatRoomInstance()
        {
            return _chatRoomClient.CallStatic<AndroidJavaObject>("getInstance");
        }

        internal RCIMClientAndroid()
        {
            GetInstance().CallStatic("setConnectionStatusListener", new ConnectionStatusCallbackProxy());
            GetInstance().CallStatic("setOnReceiveMessageListener", new ReceiveMessageCallbackProxy());
            GetInstance().CallStatic("setReadReceiptListener", new ReadReceiptListenerProxy());
            GetInstance().CallStatic("setTypingStatusListener", new TypingStatusListenerProxy());
            GetInstance().CallStatic("setOnRecallMessageListener", new OnRecallMessageLitener());
            GetInstance().Call("setKVStatusListener", new IMClientChatRoomKVCallbackProxy());
            _chatRoomClient.CallStatic("setChatRoomAdvancedActionListener", new ChatRoomAdvanceActionCallbackProxy());

            GetInstance().Call("setMessageExpansionListener", new IMClientMessageExpansionLitenerProxy());
            //GetInstance().Call("setMessageBlockListener", new IMClientMessageBlockListener());
            CoreInstance().Call("setTagListener", new CoreClientTagListenerProxy());
            CoreInstance().Call("setConversationTagListener", new CoreClientConversationTagListenerProxy());
        }

        public override void SetOfflineMessageDuration(int duration, OperationCallbackWithResult<long> callback)
        {
            _proxyClient.CallStatic("setOfflineMessageDuration", duration, new Int64CallbackProxy(callback));
        }

        public override void GetOfflineMessageDuration(OperationCallbackWithResult<long> callback)
        {
            _proxyClient.CallStatic("getOfflineMessageDuration", new Int64CallbackProxy(callback));
        }

        public override void SetAndroidPushConfig(RCAndroidPushConfig androidPushConfig)
        {
            var enableFCM = androidPushConfig.EnabledPushTypes.Contains(RCPushType.GoogleFCM);
            var enableGCM = androidPushConfig.EnabledPushTypes.Contains(RCPushType.GoogleGCM);
            var enableHuaWei = androidPushConfig.EnabledPushTypes.Contains(RCPushType.HuaWei);
            var enableVivo = androidPushConfig.EnabledPushTypes.Contains(RCPushType.Vivo);

            _proxyClient.CallStatic("SetAndroidPushConfig", enableFCM ? 1 : 0, enableGCM ? 1 : 0, enableHuaWei ? 1 : 0,
                enableVivo ? 1 : 0,
                androidPushConfig.MiAppId, androidPushConfig.MiAppKey,
                androidPushConfig.OppoAppKey, androidPushConfig.OppoAppSecret,
                androidPushConfig.MeiZuAppId, androidPushConfig.MeiZuAppKey,
                androidPushConfig.AppKey, androidPushConfig.PushNaviAddress);
        }

        public override void SetServerInfo(string naviServer, string fileServer)
        {
            _nativeClient.CallStatic("setServerInfo", naviServer, fileServer);
        }

        public override RCConnectionStatus GetConnectionStatus()
        {
            var iConnStatus = _proxyClient.CallStatic<int>("getCurrentConnectionStatus");

            return RCConnectionStatusConverter.Convert(iConnStatus);
        }

        public override void Init(string appKey)
        {
            _coreClient.CallStatic<AndroidJavaObject>("getInstance").Call("setSingleProcess", mDisableIPC);
            _nativeClient.CallStatic("init", GetContext(), appKey,mEnablePush);
            
            _proxyClient.CallStatic("registerMessageType");
        }

        public override void SetKickReconnectedDevice(bool kick)
        {
            GetInstance().Call("setReconnectKickEnable", kick);
        }

        public override Int64 GetDeltaTime()
        {
            return GetInstance().Call<Int64>("getDeltaTime");
        }

        public override void Connect(string token, OperationCallbackWithResult<string> callback)
        {
            _proxyClient.CallStatic("connect", token, new ConnectCallbackProxy(callback));
        }

        public override void Disconnect()
        {
            GetInstance().Call("disconnect");
        }

        public override void Logout()
        {
            GetInstance().Call("logout");
        }

        public override void SendMessage(RCConversationType type, string targetId, RCMessageContent content, bool disableNotification = false)
        {
            var javaContent = RCReflectHelper.FromRCMessageContent(content);
            _proxyClient.CallStatic("sendMessage", RCReflectHelper.FromConversationType(type), targetId, javaContent, new SendMessageCallbackProxy());
        }

        public override void SendMessage(RCMessage message, string pushContent = null, string pushData = null)
        {
            _proxyClient.CallStatic("sendMessage", RCReflectHelper.FromRCMessage(message), pushContent, pushData, new SendMessageCallbackProxy());
        }

        public override void SendMessageCarriesPush(RCConversationType type, string targetId, RCMessageContent content, string pushContent, string pushData, bool disableNotification = false)
        {
            var javaContent = RCReflectHelper.FromRCMessageContent(content);
            _proxyClient.CallStatic("sendMessage", RCReflectHelper.FromConversationType(type), targetId, javaContent, pushContent, pushData, new SendMessageCallbackProxy());
        }

        public override string GetCurrentUserID()
        {
            return _proxyClient.CallStatic<string>("getCurrentUserID");
        }

        public override void GetConversation(RCConversationType type, string targetId, OperationCallbackWithResult<RCConversation> callback)
        {
            var callbackProxy = new GetConversationCallbackProxy(callback);

            _proxyClient.CallStatic("getConversation", RCReflectHelper.FromConversationType(type), targetId, callbackProxy);
        }

        public override void RemoveConversation(RCConversationType type, string targetId, OperationCallback callback)
        {
            var callbackProxy = new OperationCallbackProxy(callback);

            _proxyClient.CallStatic("removeConversation", RCReflectHelper.FromConversationType(type), targetId, callbackProxy);
        }

        public override void GetConversationList(OperationCallbackWithResult<IList<RCConversation>> callback, params RCConversationType[] conversationTypes)
        {
            var callbackProxy = new GetConversationListCallbackProxy(callback);
            var iConvTypes = RCReflectHelper.ConvertRCConversationTypeToIntArray(conversationTypes);
            _proxyClient.CallStatic("getConversationList", callbackProxy, iConvTypes);
        }

        public override void GetConversationListByPage(OperationCallbackWithResult<IList<RCConversation>> callback, Int64 timestamp, int count, params RCConversationType[] conversationTypes)
        {
            var callbackProxy = new GetConversationListCallbackProxy(callback);

            var iConvTypes = RCReflectHelper.ConvertRCConversationTypeToIntArray(conversationTypes);
            _proxyClient.CallStatic("getConversationListByPage", callbackProxy, timestamp.ToString(), count, iConvTypes);
        }

        public override void GetConversationsFromTagByPage(string tagId, Int64 timestamp, int count, OperationCallbackWithResult<IList<RCConversation>> callback)
        {
            var proxy = new GetConversationListCallbackProxy(callback);
            _proxyClient.CallStatic("getConversationsFromTagByPage", tagId, timestamp.ToString(), count, proxy);
        }

        public override void SearchConversations(string keyword, RCConversationType[] conversationTypes, string[] objectNames, OperationCallbackWithResult<IList<RCSearchConversationResult>> callback)
        {
            if (conversationTypes == null || conversationTypes.Length == 0 || objectNames == null || objectNames.Length == 0)
            {
                callback?.BeginInvoke(RCErrorCode.InvalidParameter, null, null, null);
            }

            var callbackProxy = new SearchConversationListCallbackProxy(callback);

            var iConvTypes = RCReflectHelper.ConvertRCConversationTypeToIntArray(conversationTypes);
            _proxyClient.CallStatic("searchConversations", keyword, iConvTypes, RCReflectHelper.FromStringArray(objectNames), callbackProxy);
        }

        public override void ClearConversations(OperationCallback callback, params RCConversationType[] conversationTypes)
        {
            var iConvTypes = RCReflectHelper.ConvertRCConversationTypeToIntArray(conversationTypes);
            _proxyClient.CallStatic("clearConversations", new OperationCallbackProxy(callback), iConvTypes);
        }

        public override void GetTextMessageDraft(RCConversationType type, string targetId, OperationCallbackWithResult<string> callback)
        {
            _proxyClient.CallStatic("getTextMessageDraft", RCReflectHelper.FromConversationType(type), targetId, new GetStringCallbackProxy(callback));
        }

        public override void SaveTextMessageDraft(RCConversationType type, string targetId, string textDraft, OperationCallback callback)
        {
            var callbackProxy = new OperationCallbackProxy(callback);

            _proxyClient.CallStatic("saveTextMessageDraft", RCReflectHelper.FromConversationType(type), targetId, textDraft, callbackProxy);
        }

        public override void ClearTextMessageDraft(RCConversationType type, string targetId, OperationCallback callback)
        {
            var callbackProxy = new OperationCallbackProxy(callback);

            _proxyClient.CallStatic("clearTextMessageDraft", RCReflectHelper.FromConversationType(type), targetId, callbackProxy);
        }

        public override void GetUnreadCount(RCConversationType type, string targetId, OperationCallbackWithResult<int> callback)
        {
            var callbackProxy = new IntCallbackProxy(callback);
            _proxyClient.CallStatic("getUnreadCount", RCReflectHelper.FromConversationType(type), targetId, callbackProxy);
        }

        public override void GetUnreadCountByTag(string tagId, bool containsDND,
            OperationCallbackWithResult<int> callback)
        {
            var callbackProxy = new IntCallbackProxy(callback);
            _proxyClient.CallStatic("getUnreadCountByTag", tagId, containsDND ? 1 : 0, callbackProxy);
        }

        public override void GetUnreadCountByConversationTypes(bool containsDND, OperationCallbackWithResult<int> callback, params RCConversationType[] conversationTypes)
        {
            var callbackProxy = new IntCallbackProxy(callback);

            var iConvTypes = RCReflectHelper.ConvertRCConversationTypeToIntArray(conversationTypes);
            _proxyClient.CallStatic("getUnreadCountByConversationTypes", iConvTypes, containsDND ? 1 : 0, callbackProxy);
        }

        public override void GetTotalUnreadCount(OperationCallbackWithResult<int> callback)
        {
            var callbackProxy = new IntCallbackProxy(callback);
            _proxyClient.CallStatic("getTotalUnreadCount", callbackProxy);
        }

        public override void ClearMessageUnreadStatus(RCConversationType type, string targetId, OperationCallback callback)
        {
            var callbackProxy = new OperationCallbackProxy(callback);
            _proxyClient.CallStatic("clearMessageUnreadStatus", RCReflectHelper.FromConversationType(type), targetId, callbackProxy);
        }

        public override void SyncConversationReadStatus(RCConversationType type, string targetId, Int64 timestamp, OperationCallback callback)
        {
            var callbackProxy = new OperationCallbackProxy(callback);
            _proxyClient.CallStatic("syncConversationReadStatus", RCReflectHelper.FromConversationType(type), targetId, timestamp.ToString(), callbackProxy);
        }

        public override void GetFirstUnreadMessage(RCConversationType type, string targetId, OperationCallbackWithResult<RCMessage> callback)
        {
            var callbackProxy = new MessageCallbackProxy(callback);
            _proxyClient.Call("getFirstUnreadMessage", RCReflectHelper.FromConversationType(type), targetId, callbackProxy);
        }

        public override void GetUnreadMentionedMessages(RCConversationType type, string targetId, OperationCallbackWithResult<IList<RCMessage>> callback)
        {
            var callbackProxy = new GetMessageListCallbackProxy(callback);
            _proxyClient.Call("getUnreadMentionedMessages", RCReflectHelper.FromConversationType(type), targetId, callbackProxy);
        }

        public override void SetConversationNotificationStatus(RCConversationType type, string targetId,
            RCConversationNotificationStatus notificationStatus,
            OperationCallbackWithResult<RCConversationNotificationStatus> callback)
        {
            var callbackProxy = new NotificationStatusCallbackProxy(callback);
            _proxyClient.CallStatic("setConversationNotificationStatus", RCReflectHelper.FromConversationType(type),
                targetId, (int) notificationStatus, callbackProxy);
        }

        public override void GetConversationNotificationStatus(RCConversationType type, string targetId, OperationCallbackWithResult<RCConversationNotificationStatus> callback)
        {
            var callbackProxy = new NotificationStatusCallbackProxy(callback);
            _proxyClient.CallStatic("getConversationNotificationStatus", RCReflectHelper.FromConversationType(type), targetId, callbackProxy);
        }

        public override void GetBlockedConversationList(OperationCallbackWithResult<IList<RCConversation>> callback, params RCConversationType[] conversationTypes)
        {
            var callbackProxy = new GetConversationListCallbackProxy(callback);

            var iConvTypes = RCReflectHelper.ConvertRCConversationTypeToIntArray(conversationTypes);
            _proxyClient.CallStatic("getBlockedConversationList", callbackProxy, iConvTypes);
        }

        public override void SetConversationToTop(RCConversationType type, string targetId, bool isTop,
            OperationCallback callback)
        {
            var callbackProxy = new OperationCallbackProxy(callback);

            _proxyClient.CallStatic("setConversationToTop", RCReflectHelper.FromConversationType(type), targetId,
                isTop ? 1 : 0, callbackProxy);
        }

        public override void GetConversationTopStatusInTag(RCConversationType type, string targetId, string tagId, OperationCallbackWithResult<bool> callback)
        {
            var callbackProxy = new BoolCallbackProxy(callback);

            _proxyClient.CallStatic("getConversationTopStatusInTag", RCReflectHelper.FromConversationType(type), targetId, tagId, callbackProxy);
        }

        public override void GetTopConversationList(OperationCallbackWithResult<IList<RCConversation>> callback, params RCConversationType[] conversationTypes)
        {
            var callbackProxy = new GetConversationListCallbackProxy(callback);

            var iConvTypes = RCReflectHelper.ConvertRCConversationTypeToIntArray(conversationTypes);
            _proxyClient.CallStatic("getTopConversationList", callbackProxy, iConvTypes);
        }

        public override void SendTypingStatus(RCConversationType type, string targetId, string typingContent)
        {
            var typeObject = RCReflectHelper.FromConversationType(type);
            GetInstance().Call("sendTypingStatus", typeObject, targetId, typingContent);
        }

        public override void GetMessage(Int64 messageId, OperationCallbackWithResult<RCMessage> callback)
        {
            var callbackProxy = new MessageCallbackProxy(callback);

            _proxyClient.CallStatic("getMessageById", (int)messageId, callbackProxy);
        }

        public override void GetMessageByUid(string messageUid, OperationCallbackWithResult<RCMessage> callback)
        {
            var callbackProxy = new MessageCallbackProxy(callback);

            _proxyClient.CallStatic("getMessageByUid", messageUid, callbackProxy);
        }

        public override void SendDirectionalMessage(RCConversationType type, string targetId, IList<string> userIdList, RCMessageContent content, string pushContent = null, string pushData = null)
        {
            var typeObject = RCReflectHelper.FromConversationType(type);
            var contentObject = RCReflectHelper.FromRCMessageContent(content);
            var userIdArrayObject = RCReflectHelper.FromStringList(userIdList);

            var callbackProxy = new IMClientSendMessageCallbackProxy();

            GetInstance().Call("sendDirectionalMessage", typeObject, targetId, contentObject, userIdArrayObject, pushContent, pushData, callbackProxy);
        }

        public override void SendMediaMessage(RCMessage message, string pushContent = null, string pushData = null)
        {
            var messageObj = RCReflectHelper.FromRCMessage(message);
            var proxy = new IMClientSendMediaMessageCallbackProxy();
            GetInstance().Call("sendMediaMessage", messageObj, pushContent, pushData, proxy);
        }

        public override void DownloadMediaMessage(RCMessage message)
        {
            var messageObject = RCReflectHelper.FromRCMessage(message);
            var callbackProxy = new IMClientDownloadMediaMessageCallbackProxy();

            GetInstance().Call("downloadMediaMessage", messageObject, callbackProxy);
        }

#if UNITY_ANDROID
        public override void PauseDownloadMediaMessage(RCMessage message)
        {
            var messageObject = RCReflectHelper.FromRCMessage(message);
            var callbackProxy = new OperationCallbackProxy(errorCode =>
            {
                NotifyOnDownloadMediaMessagePaused(errorCode, message);
            });

            _proxyClient.CallStatic("pauseDownloadMediaMessage", messageObject, callbackProxy);
        }
#endif

        public override void CancelDownloadMediaMessage(RCMessage message)
        {
            var messageObject = RCReflectHelper.FromRCMessage(message);
            var callbackProxy = new OperationCallbackProxy((errorCode) =>
            {
                if (errorCode == RCErrorCode.Succeed)
                    base.NotifyDownloadMediaMessageCanceled(message);
            });

            _proxyClient.CallStatic("cancelDownloadMediaMessage", messageObject, callbackProxy);
        }

        public override void BatchInsertMessage(IList<RCMessage> messageList, OperationCallbackWithResult<bool> callback)
        {
            AndroidJavaObject messageObjectList = RCReflectHelper.FromRCMessageList(messageList);
            var callbackProxy = new BoolCallbackProxy(callback);
            _proxyClient.CallStatic("batchInsertMessage", messageObjectList, callbackProxy);
        }

        public override void InsertIncomingMessage(RCConversationType type, string targetId, string senderId, RCReceivedStatus receivedStatus, RCMessageContent content, Int64 sentTime, OperationCallbackWithResult<RCMessage> callback)
        {
            var converTypeObj = RCReflectHelper.FromConversationType(type);
            var recvStatusObj = RCReflectHelper.FromReceivedStatus(receivedStatus);
            var contentObj = RCReflectHelper.FromRCMessageContent(content);

            var callbackProxy = new MessageCallbackProxy(callback);
            _proxyClient.CallStatic("insertIncomingMessage", converTypeObj, targetId, senderId, recvStatusObj, contentObj, sentTime.ToString(), callbackProxy);
        }

        public override void InsertOutgoingMessage(RCConversationType type, string targetId, RCSentStatus sentStatus, RCMessageContent content, Int64 sentTime, OperationCallbackWithResult<RCMessage> callback)
        {
            var converTypeObj = RCReflectHelper.FromConversationType(type);
            var sentStatusObj = RCReflectHelper.FromSentStatus(sentStatus);
            var contentObj = RCReflectHelper.FromRCMessageContent(content);

            var callbackProxy = new MessageCallbackProxy(callback);
            _proxyClient.CallStatic("insertOutgoingMessage", converTypeObj, targetId, sentStatusObj, contentObj, sentTime.ToString(), callbackProxy);
        }

        public override void RecallMessage(RCMessage message, string pushContent, OperationCallbackWithResult<RCRecallNotificationMessage> callback)
        {
            var msgObj = RCReflectHelper.FromRCMessage(message);

            var callbackProxy = new RecallMessageProxy(callback);
            _proxyClient.CallStatic("recallMessage", msgObj, pushContent, callbackProxy);
        }

        public override void ForwardMessageByStep(RCConversationType type, string targetId, RCMessage message)
        {
            var msgContent = message.Content;
            msgContent.SendUserInfo = null;

            SendMessage(type, targetId, message.Content);
        }

        public override void GetMessages(RCConversationType type, string targetId, RCHistoryMessageOption option, OperationCallbackWithResult<IList<RCMessage>> callback)
        {
            var proxy = new GetMessageListCallbackProxy(callback);
            _proxyClient.CallStatic("getMessages", RCReflectHelper.FromConversationType(type), targetId, option.DateTime.ToString(), option.Count, (int)option.PullOrder, proxy);
        }

        public override void GetHistoryMessages(RCConversationType type, string targetId, Int64 lastMessageId, int count, OperationCallbackWithResult<IList<RCMessage>> callback)
        {
            var proxy = new GetMessageListCallbackProxy(callback);
            _proxyClient.CallStatic("getHistoryMessages", RCReflectHelper.FromConversationType(type), targetId, (int)lastMessageId, count, proxy);
        }

        public override void GetHistoryMessages(RCConversationType type, string targetId, Int64 sentTime, int beforeCount, int afterCount, OperationCallbackWithResult<IList<RCMessage>> callback)
        {
            var proxy = new GetMessageListCallbackProxy(callback);
            _proxyClient.CallStatic("getHistoryMessages", RCReflectHelper.FromConversationType(type), targetId, sentTime.ToString(), beforeCount, afterCount, proxy);
        }

        public override void GetRemoteHistoryMessages(RCConversationType type, string targetId, long dateTime, int count, OperationCallbackWithResult<IList<RCMessage>> callback)
        {
            var proxy = new GetMessageListCallbackProxy(callback);
            _proxyClient.CallStatic("getRemoteHistoryMessages", RCReflectHelper.FromConversationType(type), targetId, dateTime.ToString(), count, proxy);
        }

        public override void SearchMessages(RCConversationType type, string targetId, string keyword, int count, long beginTime, OperationCallbackWithResult<IList<RCMessage>> callback)
        {
            var proxy = new GetMessageListCallbackProxy(callback);
            _proxyClient.CallStatic("searchMessages", RCReflectHelper.FromConversationType(type), targetId, keyword, count, beginTime.ToString(), proxy);
        }

        public override void DeleteMessages(Int64[] messageIdList, OperationCallbackWithResult<bool> callback)
        {
            var proxy = new BoolCallbackProxy(callback);
            var tempMessageIdList = new int[messageIdList.Length];
            for (int i = 0; i < messageIdList.Length; i++)
            {
                tempMessageIdList[i] = (int) messageIdList[i];
            }

            _proxyClient.CallStatic("deleteMessages", tempMessageIdList, proxy);
        }

        public override void ClearMessages(RCConversationType type, string targetId, OperationCallbackWithResult<bool> callback)
        {
            var proxy = new BoolCallbackProxy(callback);
            _proxyClient.CallStatic("clearMessages", RCReflectHelper.FromConversationType(type), targetId, proxy);
        }

        public override void ClearHistoryMessages(RCConversationType type, string targetId, Int64 recordTime,
            bool clearRemote, OperationCallback callback)
        {
            var proxy = new OperationCallbackProxy(callback);
            _proxyClient.CallStatic("clearHistoryMessages", RCReflectHelper.FromConversationType(type), targetId,
                recordTime.ToString(), clearRemote.ToString(), proxy);
        }

        public override void DeleteRemoteMessages(RCConversationType type, string targetId, IList<RCMessage> messageList, OperationCallback callback)
        {
            var msgListObj = RCReflectHelper.FromRCMessageList(messageList);

            var proxy = new OperationCallbackProxy(callback);
            _proxyClient.CallStatic("deleteRemoteMessages", RCReflectHelper.FromConversationType(type), targetId, msgListObj, proxy);
        }

        public override void SetMessageExtra(Int64 messageId, string extra, OperationCallbackWithResult<bool> callback)
        {
            var proxy = new BoolCallbackProxy(callback);

            _proxyClient.CallStatic("setMessageExtra", (int)messageId, extra, proxy);
        }

        public override void SetMessageReceivedStatus(Int64 messageId, RCReceivedStatus status, OperationCallbackWithResult<bool> callback)
        {
            var recvStatusObj = RCReflectHelper.FromReceivedStatus(status);
            var proxy = new BoolCallbackProxy(callback);

            _proxyClient.CallStatic("setMessageReceivedStatus", (int)messageId, recvStatusObj, proxy);
        }

        public override void SetMessageSentStatus(Int64 messageId, RCSentStatus status, OperationCallbackWithResult<bool> callback)
        {
            var sentStatusObj = RCReflectHelper.FromSentStatus(status);
            var proxy = new BoolCallbackProxy(callback);

            _proxyClient.CallStatic("setMessageSentStatus", (int)messageId, sentStatusObj, proxy);
        }

        public override void SendReadReceiptMessage(RCConversationType type, string targetId, long timestamp, OperationCallback callback)
        {
            var proxy = new IMClientSendReadReceiptCallbackProxy(callback);
            GetInstance().Call("sendReadReceiptMessage", RCReflectHelper.FromConversationType(type), targetId, timestamp);
        }

        public override void SendReadReceiptRequest(RCMessage message, OperationCallback callback)
        {
            var proxy = new OperationCallbackProxy(callback);
            var messageObj = RCReflectHelper.FromRCMessage(message);
            _proxyClient.CallStatic("sendReadReceiptRequest", messageObj, proxy);
        }

        public override void SendReadReceiptResponse(RCConversationType type, string targetId, IList<RCMessage> messageList, OperationCallback callback)
        {
            var proxy = new OperationCallbackProxy(callback);
            _proxyClient.CallStatic("sendReadReceiptResponse", RCReflectHelper.FromConversationType(type), targetId, RCReflectHelper.FromRCMessageList(messageList), proxy);
        }

        public override void UpdateMessageExpansion(string messageUid, IDictionary<string, string> expansionDic, OperationCallback callback)
        {
            var proxy = new OperationCallbackProxy(callback);
            _proxyClient.CallStatic("updateMessageExpansion", RCReflectHelper.FromStringStringDictionary(expansionDic), messageUid, proxy);
        }

        public override void RemoveMessageExpansionForKey(string messageUid, IList<string> keyList, OperationCallback callback)
        {
            var proxy = new OperationCallbackProxy(callback);
            _proxyClient.CallStatic("removeMessageExpansion", RCReflectHelper.FromStringList(keyList), messageUid, proxy);
        }

        public override void AddToBlackList(string userId, OperationCallback callback)
        {
            var proxy = new OperationCallbackProxy(callback);
            _proxyClient.CallStatic("addToBlacklist", userId, proxy);
        }

        public override void RemoveFromBlackList(string userId, OperationCallback callback)
        {
            var proxy = new OperationCallbackProxy(callback);
            _proxyClient.CallStatic("removeFromBlacklist", userId, proxy);
        }

        public override void GetBlackList(OperationCallbackWithResult<IList<string>> callback)
        {
            var proxy = new GetBlacklistCallbackProxy(callback);
            _proxyClient.CallStatic("getBlacklist", proxy);
        }

        public override void GetBlackListStatus(string userId, OperationCallbackWithResult<RCBlackListStatus> callback)
        {
            var proxy = new GetBlacklistStatusCallbackProxy(callback);
            _proxyClient.CallStatic("getBlacklistStatus", userId, proxy);
        }

        public override void JoinChatRoom(string roomId, OperationCallback callback, int messageCount = 10)
        {
            _proxyClient.CallStatic("joinChatRoom", roomId, messageCount, new OperationCallbackProxy(callback));
        }

        public override void JoinExistChatRoom(string roomId, OperationCallback callback, int messageCount = 10)
        {
            _proxyClient.CallStatic("joinExistChatRoom", roomId, messageCount, new OperationCallbackProxy(callback));
        }

        public override void QuitChatRoom(string roomId, OperationCallback callback)
        {
            _proxyClient.CallStatic("quitChatRoom", roomId, new OperationCallbackProxy(callback));
        }

        public override void GetChatRoomInfo(string roomId, int memberCount, RCChatRoomMemberOrder order,
            OperationCallbackWithResult<RCChatRoomInfo> callback)
        {
            _proxyClient.CallStatic("getChatRoomInfo", roomId, memberCount, (int) order,
                new GetChatRoomInfoCallbackProxy(callback));
        }

        public override void GetChatRoomHistoryMessages(string targetId, Int64 recordTime, int count, bool asc,
            OperationCallbackWithResult<RCChatRoomHistoryMessageResult> callback)
        {
            _proxyClient.CallStatic("getChatRoomHistoryMessages", targetId, recordTime.ToString(), count, asc.ToString(), new GetChatRoomMessageListCallbackProxy(callback));
        }

        public override void SetChatRoomEntry(string roomId, string key, string value, bool sendNotification, bool autoRemove, string notificationExtra, OperationCallback callback)
        {
            _proxyClient.CallStatic("setChatRoomEntry", roomId, key, value, sendNotification, autoRemove, notificationExtra, new OperationCallbackProxy(callback));
        }

        //public override void SetChatRoomEntries(string roomId, IDictionary<string, string> chatRoomEntryDic, bool autoRemove, bool overwrite, OperationCallbackWithResult<IDictionary<String, RCErrorCode>> callback)
        //{
        //    _proxyClient.CallStatic("setChatRoomEntries", roomId, RCReflectHelper.FromStringStringDictionary(chatRoomEntryDic), autoRemove, overwrite, new SetChatRoomEntriesCallbackProxy(callback));
        //}

        public override void ForceSetChatRoomEntry(string roomId, string key, string value, bool sendNotification, bool autoRemove, string notificationExtra, OperationCallback callback)
        {
            _proxyClient.CallStatic("forceSetChatRoomEntry", roomId, key, value, sendNotification, autoRemove, notificationExtra, new OperationCallbackProxy(callback));
        }

        public override void RemoveChatRoomEntry(string roomId, string key, bool sendNotification, string notificationExtra, OperationCallback callback)
        {
            _proxyClient.CallStatic("removeChatRoomEntry", roomId, key, sendNotification, notificationExtra, new OperationCallbackProxy(callback));
        }

        //public override void RemoveChatRoomEntries(string roomId, IList<string> entryKeyList, Boolean force, OperationCallbackWithResult<IDictionary<String, RCErrorCode>> callback)
        //{
        //    _proxyClient.CallStatic("removeChatRoomEntries", roomId, RCReflectHelper.FromStringList(entryKeyList), force,  new SetChatRoomEntriesCallbackProxy(callback));
        //}

        public override void ForceRemoveChatRoomEntry(string roomId, string key, bool sendNotification, string notificationExtra, OperationCallback callback)
        {
            _proxyClient.CallStatic("forceRemoveChatRoomEntry", roomId, key, sendNotification, notificationExtra, new OperationCallbackProxy(callback));
        }

        public override void GetChatRoomEntry(string roomId, string key, OperationCallbackWithResult<IDictionary<string, string>> callback)
        {
            _proxyClient.CallStatic("getChatRoomEntry", roomId, key, new GetChatRoomEntryCallbackProxy(callback));
        }

        public override void GetChatRoomAllEntries(string roomId, OperationCallbackWithResult<IDictionary<string, string>> callback)
        {
            _proxyClient.CallStatic("getAllChatRoomEntries", roomId, new GetChatRoomEntryCallbackProxy(callback));
        }

        public override void GetNotificationQuietHours(OperationCallbackWithResult<RCNotificationQuietHourInfo> callback)
        {
            var proxy = new GetNotificationQuietHoursCallbackProxy(callback);
            _proxyClient.CallStatic("getNotificationQuietHours", proxy);
        }

        public override void SetNotificationQuietHours(string startTime, int spanMinutes, OperationCallback callback)
        {
            var proxy = new OperationCallbackProxy(callback);
            _proxyClient.CallStatic("setNotificationQuietHours", startTime, spanMinutes, proxy);
        }

        public override void RemoveNotificationQuietHours(OperationCallback callback)
        {
            var proxy = new OperationCallbackProxy(callback);
            _proxyClient.CallStatic("removeNotificationQuietHours", proxy);
        }

        public override void AddTag(RCTagInfo tagInfo, OperationCallback callback)
        {
            var proxy = new OperationCallbackProxy(callback);
            _proxyClient.CallStatic("addTag", RCReflectHelper.FromTagInfo(tagInfo), proxy);
        }

        public override void UpdateTag(RCTagInfo tagInfo, OperationCallback callback)
        {
            var proxy = new OperationCallbackProxy(callback);
            _proxyClient.CallStatic("updateTag", RCReflectHelper.FromTagInfo(tagInfo), proxy);
        }

        public override void RemoveTag(RCTagInfo tagInfo, OperationCallback callback)
        {
            var proxy = new OperationCallbackProxy(callback);
            _proxyClient.CallStatic("removeTag", tagInfo.TagId, proxy);
        }

        public override void GetTags(OperationCallbackWithResult<IList<RCTagInfo>> callback)
        {
            var proxy = new GetTagListCallbackProxy(callback);
            _proxyClient.CallStatic("getTags", proxy);
        }

        public override void AddConversationsToTag(string tagId, IList<RCConversationIdentifier> conversations, OperationCallback callback)
        {
            var proxy = new OperationCallbackProxy(callback);
            _proxyClient.CallStatic("addConversationsToTag", tagId, RCReflectHelper.FromConversationIdentifierList(conversations), proxy);
        }

        public override void GetTagsFromConversation(RCConversationType type, string targetId, OperationCallbackWithResult<IList<RCConversationTagInfo>> callback)
        {
            var proxy = new GetConversationTagInfoListCallbackProxy(callback);
            _proxyClient.CallStatic("getTagsFromConversation", RCReflectHelper.GetRCConversationIdentifier(type, targetId), proxy);
        }

        public override void RemoveConversationFromTag(string tagId, IList<RCConversationIdentifier> conversationIdentifiers, OperationCallback callback)
        {
            var proxy = new OperationCallbackProxy(callback);
            _proxyClient.CallStatic("removeConversationFromTag", tagId, RCReflectHelper.FromConversationIdentifierList(conversationIdentifiers), proxy);
        }

        public override void SetConversationToTopInTag(RCConversationType type, string targetId, string tagId, bool isTop, OperationCallback callback)
        {
            var callbackProxy = new OperationCallbackProxy(callback);

            _proxyClient.CallStatic("setConversationToTopInTag", RCReflectHelper.FromConversationType(type), targetId, tagId, isTop ? 1 : 0, callbackProxy);
        }

        public override void EnablePush(bool enable)
        {
            mEnablePush = enable;
        }

        public override void DisableIPC(bool disable)
        {
            mDisableIPC = disable;
        }
    }
}

#endif