#if UNITY_ANDROID

using System;
using System.Collections.Generic;
using UnityEngine;

namespace cn_rongcloud_im_unity
{
    class OperationCallbackProxy : AndroidJavaProxy
    {
        private OperationCallback _callback;

        public OperationCallbackProxy(OperationCallback callback)
            : base("cn.rongcloud.im.unity.RongUnityIM$CallbackProxy")
        {
            _callback = callback;
        }

        public void onSucceed()
        {
            _callback?.Invoke(RCErrorCode.Succeed);
        }

        public void onFailed(int code)
        {
            _callback?.Invoke((RCErrorCode) code);
        }
    }

    class BoolCallbackProxy : AndroidJavaProxy
    {
        private OperationCallbackWithResult<bool> _callback;

        public BoolCallbackProxy(OperationCallbackWithResult<bool> callback)
            : base("cn.rongcloud.im.unity.RongUnityIM$BoolCallbackProxy")
        {
            _callback = callback;
        }

        public void onSucceed(bool succeed)
        {
            _callback?.Invoke(RCErrorCode.Succeed, succeed);
        }

        public void onFailed(int code)
        {
            _callback?.Invoke((RCErrorCode) code, false);
        }
    }

    class IntCallbackProxy : AndroidJavaProxy
    {
        private OperationCallbackWithResult<int> _callback;

        public IntCallbackProxy(OperationCallbackWithResult<int> callback)
            : base("cn.rongcloud.im.unity.RongUnityIM$IntCallbackProxy")
        {
            _callback = callback;
        }

        public void onSucceed(int result)
        {
            _callback?.Invoke(RCErrorCode.Succeed, result);
        }

        public void onFailed(int code)
        {
            _callback?.Invoke((RCErrorCode) code, -1);
        }
    }

    class Int64CallbackProxy : AndroidJavaProxy
    {
        private OperationCallbackWithResult<Int64> _callback;

        public Int64CallbackProxy(OperationCallbackWithResult<Int64> callback)
            : base("cn.rongcloud.im.unity.RongUnityIM$LongCallbackProxy")
        {
            _callback = callback;
        }

        public void onSucceed(Int64 result)
        {
            _callback?.Invoke(RCErrorCode.Succeed, result);
        }

        public void onFailed(int code)
        {
            _callback?.Invoke((RCErrorCode) code, -1);
        }
    }

    class NotificationStatusCallbackProxy : AndroidJavaProxy
    {
        private OperationCallbackWithResult<RCConversationNotificationStatus> _callback;

        public NotificationStatusCallbackProxy(OperationCallbackWithResult<RCConversationNotificationStatus> callback)
            : base("cn.rongcloud.im.unity.RongUnityIM$NotificationStatusCallbackProxy")
        {
            _callback = callback;
        }

        public void onSucceed(int result)
        {
            _callback?.Invoke(RCErrorCode.Succeed, (RCConversationNotificationStatus) result);
        }

        public void onFailed(int code)
        {
            _callback?.Invoke((RCErrorCode) code, RCConversationNotificationStatus.Notify);
        }
    }

    class MessageCallbackProxy : AndroidJavaProxy
    {
        private OperationCallbackWithResult<RCMessage> _callback;

        public MessageCallbackProxy(OperationCallbackWithResult<RCMessage> callback)
            : base("cn.rongcloud.im.unity.RongUnityIM$MessageCallbackProxy")
        {
            _callback = callback;
        }

        public void onSucceed(AndroidJavaObject messageObject)
        {
            var rcMessage = RCReflectHelper.ToRCMessage(messageObject);
            _callback?.Invoke(RCErrorCode.Succeed, rcMessage);
        }

        public void onFailed(AndroidJavaObject codeObject)
        {
            var errorCode = RCReflectHelper.ToRCErrorCode(codeObject);
            _callback?.Invoke(errorCode, null);
        }
    }

    class RecallMessageProxy : AndroidJavaProxy
    {
        private OperationCallbackWithResult<RCRecallNotificationMessage> _callback;

        public RecallMessageProxy(OperationCallbackWithResult<RCRecallNotificationMessage> callback)
            : base("cn.rongcloud.im.unity.RongUnityIM$RecallMessageCallbackProxy")
        {
            this._callback = callback;
        }

        public void onSucceed(AndroidJavaObject messageObject)
        {
            var rcMessage = RCReflectHelper.ToNotificationMessage(messageObject);
            _callback?.Invoke(RCErrorCode.Succeed, rcMessage);
        }

        public void onFailed(AndroidJavaObject codeObject)
        {
            var errorCode = RCReflectHelper.ToRCErrorCode(codeObject);
            _callback?.Invoke(errorCode, null);
        }
    }

    class OnRecallMessageLitener : AndroidJavaProxy
    {
        public OnRecallMessageLitener()
            : base("io.rong.imlib.RongIMClient$OnRecallMessageListener")
        {
            
        }

        public Boolean onMessageRecalled(AndroidJavaObject messageObject, AndroidJavaObject recallNotifyObject)
        {
            Debug.Log("onMessageRecalled");
            var rcMessage = RCReflectHelper.ToRCMessage(messageObject, true);
            var rcRecallNotifyMsg = RCReflectHelper.ToRCMessageContent(rcMessage.Content.ObjectName, recallNotifyObject);
            RCIMClient.Instance.NotifyOnMessageRecalled(rcMessage, rcRecallNotifyMsg as RCRecallNotificationMessage);
            return true;
        }
    }

    class GetConversationCallbackProxy : AndroidJavaProxy
    {
        private OperationCallbackWithResult<RCConversation> _callback;

        public GetConversationCallbackProxy(OperationCallbackWithResult<RCConversation> callback)
            : base("cn.rongcloud.im.unity.RongUnityIM$ConversationCallbackProxy")
        {
            _callback = callback;
        }

        public void onSucceed(AndroidJavaObject conversationObject)
        {
            var rcConversation = RCReflectHelper.ToRCConversation(conversationObject);
            _callback?.Invoke(RCErrorCode.Succeed, rcConversation);
        }

        public void onFailed(AndroidJavaObject codeObject)
        {
            var errorCode = RCReflectHelper.ToRCErrorCode(codeObject);
            _callback?.Invoke(errorCode, null);
        }
    }

    class GetConversationListCallbackProxy : AndroidJavaProxy
    {
        private OperationCallbackWithResult<IList<RCConversation>> _callback;

        public GetConversationListCallbackProxy(OperationCallbackWithResult<IList<RCConversation>> callback)
            : base("cn.rongcloud.im.unity.RongUnityIM$ConversationListCallbackProxy")
        {
            this._callback = callback;
        }

        public void onSucceed(AndroidJavaObject conversationObject)
        {
            var rcConversation = RCReflectHelper.ToRCConversationList(conversationObject);
            _callback?.Invoke(RCErrorCode.Succeed, rcConversation);
        }

        public void onFailed(int code)
        {
            _callback?.Invoke((RCErrorCode) code, null);
        }
    }

    class GetMessageListCallbackProxy : AndroidJavaProxy
    {

        private OperationCallbackWithResult<IList<RCMessage>> _callback;

        public GetMessageListCallbackProxy(OperationCallbackWithResult<IList<RCMessage>> callback)
            : base("cn.rongcloud.im.unity.RongUnityIM$MessageListCallbackProxy")
        {
            this._callback = callback;
        }

        public void onSucceed(AndroidJavaObject conversationObject)
        {
            var msgList = RCReflectHelper.ToRCMessageList(conversationObject);
            _callback?.Invoke(RCErrorCode.Succeed, msgList);
        }

        public void onFailed(int code)
        {
            _callback?.Invoke((RCErrorCode) code, null);
        }
    }

    class GetChatRoomMessageListCallbackProxy : AndroidJavaProxy
    {
        private OperationCallbackWithResult<RCChatRoomHistoryMessageResult> _callback;

        public GetChatRoomMessageListCallbackProxy(OperationCallbackWithResult<RCChatRoomHistoryMessageResult> callback)
            : base("cn.rongcloud.im.unity.RongUnityIM$ChatRoomMessageListCallbackProxy")
        {
            this._callback = callback;
        }

        public void onSucceed(AndroidJavaObject conversationObject, string syncTimeObject)
        {
            var msgList = RCReflectHelper.ToRCMessageList(conversationObject);
            Int64.TryParse(syncTimeObject, out var syncTime);
            _callback?.Invoke(RCErrorCode.Succeed, new RCChatRoomHistoryMessageResult(msgList, syncTime));
        }

        public void onFailed(int code)
        {
            _callback?.Invoke((RCErrorCode) code, null);
        }
    }

    class SearchConversationListCallbackProxy : AndroidJavaProxy
    {
        private OperationCallbackWithResult<IList<RCSearchConversationResult>> _callback;

        public SearchConversationListCallbackProxy(
            OperationCallbackWithResult<IList<RCSearchConversationResult>> callback)
            : base("cn.rongcloud.im.unity.RongUnityIM$SearchConversationListCallbackProxy")
        {
            this._callback = callback;
        }

        public void onSucceed(AndroidJavaObject conversationObject)
        {
            var searchConvList = RCReflectHelper.ToRCSearchConversationList(conversationObject);
            _callback?.Invoke(RCErrorCode.Succeed, searchConvList);
        }

        public void onFailed(int code)
        {
            _callback?.Invoke((RCErrorCode) code, null);
        }
    }

    class GetStringCallbackProxy : AndroidJavaProxy
    {
        private OperationCallbackWithResult<string> _callback;

        public GetStringCallbackProxy(OperationCallbackWithResult<string> callback)
            : base("cn.rongcloud.im.unity.RongUnityIM$StringCallbackProxy")
        {
            this._callback = callback;
        }

        public void onSucceed(string str)
        {
            _callback?.Invoke(RCErrorCode.Succeed, str);
        }

        public void onFailed(int code)
        {
            _callback?.Invoke((RCErrorCode) code, "");
        }
    }

    class GetNotificationQuietHoursCallbackProxy : AndroidJavaProxy
    {
        private OperationCallbackWithResult<RCNotificationQuietHourInfo> _callback;

        public GetNotificationQuietHoursCallbackProxy(OperationCallbackWithResult<RCNotificationQuietHourInfo> callback)
            : base("cn.rongcloud.im.unity.RongUnityIM$GetNotificationQuietHoursCallbackProxy")
        {
            this._callback = callback;
        }

        public void onSucceed(string startTime, int spanMinutes)
        {
            _callback?.Invoke(RCErrorCode.Succeed, new RCNotificationQuietHourInfo(startTime, spanMinutes));
        }

        public void onFailed(int code)
        {
            _callback?.Invoke((RCErrorCode) code, null);
        }
    }

    class GetBlacklistStatusCallbackProxy : AndroidJavaProxy
    {
        private OperationCallbackWithResult<RCBlackListStatus> _callback;

        public GetBlacklistStatusCallbackProxy(OperationCallbackWithResult<RCBlackListStatus> callback)
            : base("cn.rongcloud.im.unity.RongUnityIM$BlacklistStatusCallbackProxy")
        {
            this._callback = callback;
        }

        public void onSucceed(AndroidJavaObject blackStatusObj)
        {
            _callback?.Invoke(RCErrorCode.Succeed, RCReflectHelper.ToRCBlacklistStatus(blackStatusObj));
        }

        public void onFailed(int code)
        {
            _callback?.Invoke((RCErrorCode) code, RCBlackListStatus.NotInBlackList);
        }
    }

    class GetBlacklistCallbackProxy : AndroidJavaProxy
    {
        private OperationCallbackWithResult<IList<String>> _callback;

        public GetBlacklistCallbackProxy(OperationCallbackWithResult<IList<String>> callback)
            : base("cn.rongcloud.im.unity.RongUnityIM$BlacklistCallbackProxy")
        {
            this._callback = callback;
        }

        public void onSucceed(AndroidJavaObject blackListObj)
        {
            _callback?.Invoke(RCErrorCode.Succeed, RCReflectHelper.ToStringList(blackListObj));
        }

        public void onFailed(int code)
        {
            _callback?.Invoke((RCErrorCode) code, null);
        }
    }

    class GetTagListCallbackProxy : AndroidJavaProxy
    {
        private OperationCallbackWithResult<IList<RCTagInfo>> _callback;

        public GetTagListCallbackProxy(OperationCallbackWithResult<IList<RCTagInfo>> callback)
            : base("cn.rongcloud.im.unity.RongUnityIM$GetTagsCallbackProxy")
        {
            this._callback = callback;
        }

        public void onSucceed(AndroidJavaObject tagListObj)
        {
            _callback?.Invoke(RCErrorCode.Succeed, RCReflectHelper.ToRCTagInfoList(tagListObj));
        }

        public void onFailed(int code)
        {
            _callback?.Invoke((RCErrorCode) code, null);
        }
    }

    class GetConversationTagInfoListCallbackProxy : AndroidJavaProxy
    {
        private OperationCallbackWithResult<IList<RCConversationTagInfo>> _callback;

        public GetConversationTagInfoListCallbackProxy(
            OperationCallbackWithResult<IList<RCConversationTagInfo>> callback)
            : base("cn.rongcloud.im.unity.RongUnityIM$GetTagsFromConversationCallbackProxy")
        {
            this._callback = callback;
        }

        public void onSucceed(AndroidJavaObject tagListObj)
        {
            _callback?.Invoke(RCErrorCode.Succeed, RCReflectHelper.ToRCConversationTagInfoList(tagListObj));
        }

        public void onFailed(int code)
        {
            _callback?.Invoke((RCErrorCode) code, null);
        }
    }

    class ConnectCallbackProxy : AndroidJavaProxy
    {
        private OperationCallbackWithResult<string> _callback;

        public ConnectCallbackProxy(OperationCallbackWithResult<string> callback)
            : base("cn.rongcloud.im.unity.RongUnityIM$ConnectCallbackProxy")
        {
            this._callback = callback;
        }

        public void onSucceed(string userId)
        {
            _callback?.Invoke(RCErrorCode.Succeed, userId);
        }

        public void onFailed(int code)
        {
            _callback?.Invoke((RCErrorCode) code, "");
        }
    }

    class ConnectionStatusCallbackProxy : AndroidJavaProxy
    {
        public ConnectionStatusCallbackProxy()
            : base("io.rong.imlib.RongIMClient$ConnectionStatusListener")
        {
        }

        public void onChanged(AndroidJavaObject statusObject)
        {
            var status = RCReflectHelper.ToConnectionStatus(statusObject);
            RCIMClient.Instance.NotifyOnConnectionStatusChanged(status);
        }
    }

    class ReceiveMessageCallbackProxy : AndroidJavaProxy
    {
        public ReceiveMessageCallbackProxy()
            : base("io.rong.imlib.RongIMClient$OnReceiveMessageListener")
        {
        }

        public bool onReceived(AndroidJavaObject message, int msgLeft)
        {
            var rcMsg = RCReflectHelper.ToRCMessage(message);
            Debug.Log($"ReceiveMessageCallbackProxy called, left: {msgLeft} {rcMsg}");

            RCIMClient.Instance?.NotifyOnMessageReceived(rcMsg, msgLeft);
            return true;
        }
    }

    class ReadReceiptListenerProxy : AndroidJavaProxy
    {
        public ReadReceiptListenerProxy()
            : base("io.rong.imlib.RongIMClient$ReadReceiptListener")
        {
        }

        public void onReadReceiptReceived(AndroidJavaObject messageObj)
        {
            var msg = RCReflectHelper.ToRCMessage(messageObj);
            if (msg == null) return;
            var readReceiptMsg = msg.Content as RCReadReceiptMessage;
            if (readReceiptMsg == null) return;

            RCIMClient.Instance.NotifyReadReceiptReceived(msg.ConversationType, msg.TargetId, msg.SenderUserId, readReceiptMsg.LastMessageSentTime);
        }

        public void onMessageReceiptRequest(AndroidJavaObject convTypeObj, string targetId, string messageUid)
        {
            RCIMClient.Instance.NotifyReadReceiptRequest(RCReflectHelper.ToRCConversationType(convTypeObj), targetId,
                messageUid);
        }

        public void onMessageReceiptResponse(AndroidJavaObject convTypeObj, string targetId, string messageUid,
            AndroidJavaObject respondUserIdListObj)
        {
            RCIMClient.Instance.NotifyReadReceiptResponse(RCReflectHelper.ToRCConversationType(convTypeObj), targetId,
                messageUid, RCReflectHelper.ToStringLongDictionary(respondUserIdListObj));
        }
    }

    class TypingStatusListenerProxy : AndroidJavaProxy
    {
        public TypingStatusListenerProxy()
            : base("io.rong.imlib.RongIMClient$TypingStatusListener")
        {
        }

        public void onTypingStatusChanged(AndroidJavaObject convTypeObj, string targetId,
            AndroidJavaObject typingStatusSetObj)
        {
            RCIMClient.Instance.NotifyTypingStatusChanged(RCReflectHelper.ToRCConversationType(convTypeObj), targetId,
                RCReflectHelper.ToTypingStatusList(typingStatusSetObj));
        }
    }

    class GetChatRoomInfoCallbackProxy : AndroidJavaProxy
    {
        private OperationCallbackWithResult<RCChatRoomInfo> _callback;

        public GetChatRoomInfoCallbackProxy(OperationCallbackWithResult<RCChatRoomInfo> callback)
            : base("cn.rongcloud.im.unity.RongUnityIM$GetChatRoomInfoCallbackProxy")
        {
            this._callback = callback;
        }

        public void onSucceed(AndroidJavaObject chatroomInfoObj)
        {
            this._callback?.BeginInvoke(RCErrorCode.Succeed, RCReflectHelper.ToRCChatRoomInfo(chatroomInfoObj), null,
                null);
        }

        public void onFailed(AndroidJavaObject errorCodeObj)
        {
            this._callback?.BeginInvoke(RCReflectHelper.ToRCErrorCode(errorCodeObj), null, null, null);
        }
    }

    class SetChatRoomEntriesCallbackProxy : AndroidJavaProxy
    {
        private OperationCallbackWithResult<IDictionary<String, RCErrorCode>> _callback;

        public SetChatRoomEntriesCallbackProxy(OperationCallbackWithResult<IDictionary<String, RCErrorCode>> callback)
            : base("cn.rongcloud.im.unity.RongUnityIM$SetChatRoomEntriesCallbackProxy")
        {
            this._callback = callback;
        }

        public void onSucceed()
        {
            this._callback?.BeginInvoke(RCErrorCode.Succeed, null, null, null);
        }

        public void onFailed(AndroidJavaObject codeObj, AndroidJavaObject setEntriesResultObj)
        {
            this._callback?.BeginInvoke(RCReflectHelper.ToRCErrorCode(codeObj),
                RCReflectHelper.ToStringErrorCodeDictionary(setEntriesResultObj), null, null);
        }
    }

    class GetChatRoomEntryCallbackProxy : AndroidJavaProxy
    {
        private OperationCallbackWithResult<IDictionary<string, string>> _callback;

        public GetChatRoomEntryCallbackProxy(OperationCallbackWithResult<IDictionary<string, string>> callback)
            : base("cn.rongcloud.im.unity.RongUnityIM$GetChatRoomEntryCallbackProxy")
        {
            _callback = callback;
        }

        public void onSucceed(AndroidJavaObject stringstringMapObj)
        {
            this._callback?.BeginInvoke(RCErrorCode.Succeed,
                RCReflectHelper.ToStringStringDictionary(stringstringMapObj), null, null);
        }

        public void onFailed(AndroidJavaObject errorCodeObj)
        {
            this._callback?.BeginInvoke(RCReflectHelper.ToRCErrorCode(errorCodeObj), null, null, null);
        }
    }

    class KVStatusCallbackProxy : AndroidJavaProxy
    {
        public KVStatusCallbackProxy()
            : base("cn.rongcloud.im.unity.RongUnityIM$KVStatusCallbackProxy")
        {
        }

        public void onDidSync(string roomId)
        {
            Debug.Log(string.Format("KVStatusCallbackProxy onDidSync called,{0}", roomId));
            //RCIMClient.Instance.OnChatRoomKVDidSync?.Invoke(roomId);
        }

        public void onDidUpdate(string roomId, string json)
        {
            Debug.Log(string.Format("KVStatusCallbackProxy onDidUpdate called,{0}", roomId));
            //var kv = Utility.JsonToKVList(json);
            //RCIMClient.Instance.OnChatRoomKVDidUpdate?.Invoke(roomId, kv);
        }

        public void onDidRemove(string roomId, string json)
        {
            Debug.Log(string.Format("KVStatusCallbackProxy onDidRemove called,{0}", roomId));
            //var kv = Utility.JsonToKVList(json);
            //RCIMClient.Instance.OnChatRoomKVDidRemove?.Invoke(roomId, kv);
        }
    }

    class SendMessageCallbackProxy : AndroidJavaProxy
    {
        public SendMessageCallbackProxy()
            : base("cn.rongcloud.im.unity.RongUnityIM$SendMessageCallbackProxy")
        {
        }

        public void onAttached(AndroidJavaObject message)
        {
            var rcMsg = RCReflectHelper.ToRCMessage(message);
            RCIMClient.Instance.NotifyOnSendMessageAttached(rcMsg);
        }

        public void onSucceed(AndroidJavaObject message)
        {
            var rcMsg = RCReflectHelper.ToRCMessage(message);
            RCIMClient.Instance.NotifyOnSendMessageSucceed(rcMsg);
        }

        public void onFailed(AndroidJavaObject message, int errorCode)
        {
            var rcMsg = RCReflectHelper.ToRCMessage(message);
            RCIMClient.Instance.NotifyOnSendMessageFailed(rcMsg, (RCErrorCode) errorCode);
        }
    }

    class IMClientMessageBlockListener : AndroidJavaProxy
    {
        public IMClientMessageBlockListener()
            : base("io.rong.imlib.IRongCoreListener$MessageBlockListener")
        {
        }

        public void onMessageBlock(AndroidJavaObject blockedMessageInfoObj)
        {
            RCIMClient.Instance.NotifyMessageBlocked(RCReflectHelper.ToBlockedMessageInfo(blockedMessageInfoObj));
        }
    }

    class IMClientSendMessageCallbackProxy : AndroidJavaProxy
    {
        public IMClientSendMessageCallbackProxy()
            : base("io.rong.imlib.IRongCallback$ISendMessageCallback")
        {
        }

        public void onAttached(AndroidJavaObject message)
        {
            var rcMsg = RCReflectHelper.ToRCMessage(message);
            RCIMClient.Instance.NotifyOnSendMessageAttached(rcMsg);
        }

        public void onSuccess(AndroidJavaObject message)
        {
            var rcMsg = RCReflectHelper.ToRCMessage(message);
            RCIMClient.Instance.NotifyOnSendMessageSucceed(rcMsg);
        }

        public void onError(AndroidJavaObject message, AndroidJavaObject errorCodeObject)
        {
            var rcMsg = RCReflectHelper.ToRCMessage(message);
            var rcErrorCode = RCReflectHelper.ToRCErrorCode(errorCodeObject);
            RCIMClient.Instance.NotifyOnSendMessageFailed(rcMsg, rcErrorCode);
        }
    }

    class IMClientSendMediaMessageCallbackProxy : AndroidJavaProxy
    {
        public IMClientSendMediaMessageCallbackProxy()
            : base("io.rong.imlib.IRongCallback$ISendMediaMessageCallback")
        {
        }

        public void onAttached(AndroidJavaObject message)
        {
            var rcMsg = RCReflectHelper.ToRCMessage(message);
            RCIMClient.Instance.NotifyOnSendMessageAttached(rcMsg);
        }

        public void onSuccess(AndroidJavaObject message)
        {
            var rcMsg = RCReflectHelper.ToRCMessage(message);
            RCIMClient.Instance.NotifyOnSendMessageSucceed(rcMsg);
        }

        public void onError(AndroidJavaObject message, AndroidJavaObject errorCodeObject)
        {
            var rcMsg = RCReflectHelper.ToRCMessage(message);
            var rcErrorCode = RCReflectHelper.ToRCErrorCode(errorCodeObject);
            RCIMClient.Instance.NotifyOnSendMessageFailed(rcMsg, rcErrorCode);
        }

        public void onProgress(AndroidJavaObject message, int progress)
        {
            var rcMsg = RCReflectHelper.ToRCMessage(message);
            RCIMClient.Instance.NotifyOnSendMediaMessageProgress(rcMsg, progress);
        }

        public void onCanceled(AndroidJavaObject message)
        {
            var rcMsg = RCReflectHelper.ToRCMessage(message);
            RCIMClient.Instance.NotifyOnSendMediaMessageCanceled(rcMsg);
        }
    }

    class IMClientSendReadReceiptCallbackProxy : AndroidJavaProxy
    {
        OperationCallback _callback;

        public IMClientSendReadReceiptCallbackProxy(OperationCallback callback)
            : base("io.rong.imlib.IRongCallback$ISendMessageCallback")
        {
            this._callback = callback;
        }

        public void onAttached(AndroidJavaObject message)
        {
        }

        public void onSuccess(AndroidJavaObject message)
        {
            _callback?.BeginInvoke(RCErrorCode.Succeed, null, null);
        }

        public void onError(AndroidJavaObject message, AndroidJavaObject errorCodeObject)
        {
            var rcErrorCode = RCReflectHelper.ToRCErrorCode(errorCodeObject);

            _callback?.BeginInvoke(rcErrorCode, null, null);
        }
    }

    class IMClientDownloadMediaMessageCallbackProxy : AndroidJavaProxy
    {
        public IMClientDownloadMediaMessageCallbackProxy()
            : base("io.rong.imlib.IRongCallback$IDownloadMediaMessageCallback")
        {
        }

        public void onSuccess(AndroidJavaObject message)
        {
            Debug.Log($"IMClientDownloadMediaMessageCallbackProxy onSucced: {message}");
            var rcMsg = RCReflectHelper.ToRCMessage(message);
            RCIMClient.Instance.NotifyDownloadMediaMessageCompleted(rcMsg);
        }

        public void onProgress(AndroidJavaObject message, int progress)
        {
            Debug.Log($"IMClientDownloadMediaMessageCallbackProxy onAttached: {message}");
            var rcMsg = RCReflectHelper.ToRCMessage(message);
            RCIMClient.Instance.NotifyDownloadMediaMessageProgressed(rcMsg, progress);
        }

        public void onError(AndroidJavaObject message, AndroidJavaObject errorCodeObject)
        {
            Debug.Log($"IMClientDownloadMediaMessageCallbackProxy onFailed: {errorCodeObject} {message}");
            var rcMsg = RCReflectHelper.ToRCMessage(message);
            var rcErrorCode = RCReflectHelper.ToRCErrorCode(errorCodeObject);
            RCIMClient.Instance.NotifyDownloadMediaMessageFailed(rcErrorCode, rcMsg);
        }

        public void onCanceled(AndroidJavaObject message)
        {
            Debug.Log($"IMClientDownloadMediaMessageCallbackProxy onCanceled: {message}");
            var rcMsg = RCReflectHelper.ToRCMessage(message);
            RCIMClient.Instance.NotifyDownloadMediaMessageCanceled(rcMsg);
        }
    }

    class ChatRoomAdvanceActionCallbackProxy : AndroidJavaProxy
    {
        public ChatRoomAdvanceActionCallbackProxy()
           : base("io.rong.imlib.chatroom.base.RongChatRoomClient$ChatRoomAdvancedActionListener")
        {
        }
        public void onJoining(String roomId)
        {
            Debug.Log($"ChatRoomAdvanceActionCallbackProxy onJoining: {roomId}");
            RCIMClient.Instance.NotifyOnChatRoomJoining(roomId);
        }

        public void onJoined(String roomId)
        {
            Debug.Log($"ChatRoomAdvanceActionCallbackProxy onJoined: {roomId}");
            RCIMClient.Instance.NotifyOnChatRoomJoined(roomId);
        }

        public void onReset(String roomId)
        {
            Debug.Log($"ChatRoomAdvanceActionCallbackProxy onReset: {roomId}");
            RCIMClient.Instance.NotifyOnChatRoomReset(roomId);
        }

        public void onQuited(String roomId)
        {
            Debug.Log($"ChatRoomAdvanceActionCallbackProxy onQuited: {roomId}");
            RCIMClient.Instance.NotifyOnChatRoomQuited(roomId);
        }

        public void onDestroyed(String roomId, AndroidJavaObject chatRoomDestroyType)
        {
            Debug.Log($"ChatRoomAdvanceActionCallbackProxy onDestroyed: {roomId}");
            RCIMClient.Instance.NotifyOnChatRoomDestroyed(roomId, RCReflectHelper.ToRCChatRoomDestroyType(chatRoomDestroyType));
        }

        public void onError(String roomId, AndroidJavaObject coreErrorCode)
        {
            Debug.Log($"ChatRoomAdvanceActionCallbackProxy onError: {roomId}");
            RCIMClient.Instance.NotifyOnChatRoomError(roomId, RCReflectHelper.ToRCErrorCode(coreErrorCode));
        }
    }

    class IMClientChatRoomKVCallbackProxy : AndroidJavaProxy
    {
        public IMClientChatRoomKVCallbackProxy()
            : base("io.rong.imlib.RongIMClient$KVStatusListener")
        {
        }

        public void onChatRoomKVSync(string roomId)
        {
            RCIMClient.Instance.NotifyOnChatRoomKVSync(roomId);
        }

        public void onChatRoomKVUpdate(string roomId, AndroidJavaObject entriesObj)
        {
            var entries = RCReflectHelper.ToStringStringDictionary(entriesObj);
            RCIMClient.Instance.NotifyOnChatRoomKVUpdate(roomId, entries);
        }

        public void onError(string roomId, AndroidJavaObject entriesObj)
        {
            var entries = RCReflectHelper.ToStringStringDictionary(entriesObj);
            RCIMClient.Instance.NotifyOnChatRoomKVRemove(roomId, entries);
        }
    }

    class IMClientMessageExpansionLitenerProxy : AndroidJavaProxy
    {
        public IMClientMessageExpansionLitenerProxy()
            : base("io.rong.imlib.RongIMClient$MessageExpansionListener")
        {

        }

        public void onMessageExpansionUpdate(AndroidJavaObject expansionDicObj, AndroidJavaObject messageObj)
        {
            var entries = RCReflectHelper.ToStringStringDictionary(expansionDicObj);
            var message = RCReflectHelper.ToRCMessage(messageObj);
            RCIMClient.Instance.NotifyMessageExpansionUpdate(entries, message);
        }

        public void onMessageExpansionRemove(AndroidJavaObject removeKeyListObj, AndroidJavaObject messageObj)
        {
            var keyList = RCReflectHelper.ToStringList(removeKeyListObj);
            var message = RCReflectHelper.ToRCMessage(messageObj);
            RCIMClient.Instance.NotifyMessageExpansionRemoved(keyList, message);
        }
    }

    class CoreClientTagListenerProxy : AndroidJavaProxy
    {
        public CoreClientTagListenerProxy()
            : base("io.rong.imlib.IRongCoreListener$TagListener")
        {

        }

        public void onTagChanged()
        {
            RCIMClient.Instance.NotifyOnTagChanged();
        }
    }

    class CoreClientConversationTagListenerProxy : AndroidJavaProxy
    {
        public CoreClientConversationTagListenerProxy()
            : base("io.rong.imlib.IRongCoreListener$ConversationTagListener")
        {

        }

        public void onConversationTagChanged()
        {
            RCIMClient.Instance.NotifyOnConversationTagChanged();
        }
    }
}

#endif