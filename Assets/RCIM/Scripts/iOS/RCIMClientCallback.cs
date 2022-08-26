//
//  Copyright © 2021 RongCloud. All rights reserved.
//

#if UNITY_IOS
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using AOT;
using UnityEngine;

namespace cn_rongcloud_im_unity
{
    public partial class RCIMClientIOS
    {
        #region callback

        [MonoPInvokeCallback(typeof(OnGeneralHandler))]
        private static void OnGeneralCallback(Int64 handle, RCErrorCode errCode)
        {
            Debug.Log($"OnGeneralCallback handle({handle}) called. errCode:{errCode}");
            var callback = RCIMUtils.TakeCallback<OperationCallback>(handle);
            if (callback != null)
            {
                callback(errCode);
            }
        }

        [MonoPInvokeCallback(typeof(OnGeneralHandlerInt))]
        private static void OnGeneralIntCallback(Int64 handle, RCErrorCode errCode, int result)
        {
            Debug.Log($"OnGeneralIntCallback handle ({handle}) called, errCode={errCode},count:{result}");
            var callback = RCIMUtils.TakeCallback<OperationCallbackWithResult<int>>(handle);
            if (callback != null)
            {
                callback(errCode, result);
            }
        }

        [MonoPInvokeCallback(typeof(OnGeneralHandlerLong))]
        private static void OnGeneralLongCallback(Int64 handle, RCErrorCode errCode, long result)
        {
            Debug.Log($"OnGeneralLongCallback handle({handle}) called. errCode:{errCode}, result:{result}");
            var callback = RCIMUtils.TakeCallback<OperationCallbackWithResult<long>>(handle);
            if (callback != null)
            {
                callback(errCode, result);
            }
        }

        [MonoPInvokeCallback(typeof(OnGeneralHandlerBool))]
        private static void OnGeneralBoolCallback(Int64 handle, RCErrorCode errCode, bool result)
        {
            Debug.Log($"OnGeneralBoolCallback handle({handle}) called. errCode:{errCode}, result:{result}");
            var callback = RCIMUtils.TakeCallback<OperationCallbackWithResult<bool>>(handle);
            if (callback != null)
            {
                callback(errCode, result);
            }
        }

        [MonoPInvokeCallback(typeof(OnGeneralHandlerString))]
        private static void OnGeneralStringCallback(Int64 handle, RCErrorCode errCode, string result)
        {
            Debug.Log($"OnGeneralStringCallback called, errCode={errCode},draftText:{result}");
            var callback = RCIMUtils.TakeCallback<OperationCallbackWithResult<string>>(handle);
            if (callback != null)
            {
                callback(errCode, result);
            }
        }

        [MonoPInvokeCallback(typeof(OnGeneralHandlerString))]
        private static void OnConnect(Int64 handle, RCErrorCode errCode, string userId)
        {
            Debug.Log($"OnConnect handle({handle}) called. userId={userId}");
            if (errCode == RCErrorCode.Succeed)
            {
                if (RCIMClient.Instance is RCIMClientIOS imClient) imClient.UserId = userId;
            }

            var callback = RCIMUtils.TakeCallback<OperationCallbackWithResult<string>>(handle);
            if (callback != null)
            {
                callback(errCode, userId);
            }
        }

        [MonoPInvokeCallback(typeof(OnImConnectionStatusChangedHandler))]
        private static void OnImConnectionStatusChangedListener(int status)
        {
            Debug.Log($"OnConnectionStatusChangedCallback called,{status}");
            var csStatus = RCIMUtils.convertConnectionStatusIOS(status);
            Instance.NotifyOnConnectionStatusChanged(csStatus);
        }

        [MonoPInvokeCallback(typeof(OnGeneralHandlerIntPtr))]
        private static void OnGetConversationCallback(Int64 handle, RCErrorCode errCode,
            IntPtr conversationPtr)
        {
            var rcConversation = RCIMUtils.toRCConversation(conversationPtr);
            Debug.Log(
                $"OnGetConversationCallback handle({handle}) called, errCode={errCode},conversation:{conversationPtr},rcConversation:{rcConversation}");
            var callback = RCIMUtils.TakeCallback<OperationCallbackWithResult<RCConversation>>(handle);
            if (callback != null)
            {
                callback(errCode, rcConversation);
            }
        }

        [MonoPInvokeCallback(typeof(OnGeneralHandlerIntPtrList))]
        private static void OnGetConversationListCallback(Int64 handle, RCErrorCode errCode,
            IntPtr conversationListPtr, int length)
        {
            Debug.Log(
                $"OnGetConversationListCallback handle ({handle}) called, errCode={errCode},conversationList:{conversationListPtr}, length:{length}");

            var rcConversations = new List<RCConversation>();

            IntPtr ptr = conversationListPtr;
            int imConversationPtrSize = Marshal.SizeOf<im_conversation>();
            for (int i = 0; i < length; i++)
            {
                im_conversation conv = Marshal.PtrToStructure<im_conversation>(ptr);
                Debug.Log($"{length}:{i}, im_conversation is {conv}");
                RCConversation rcConversation = RCIMUtils.toRCConversation(ref conv);
                rcConversations.Add(rcConversation);
                ptr = (IntPtr) ((long) ptr + imConversationPtrSize);
            }

            var callback = RCIMUtils.TakeCallback<OperationCallbackWithResult<IList<RCConversation>>>(handle);
            if (callback != null)
            {
                callback(errCode, rcConversations);
            }
        }

        [MonoPInvokeCallback(typeof(OnGeneralHandlerIntPtrList))]
        private static void OnGetConversationTagInfoListCallback(Int64 handle, RCErrorCode errCode,
            IntPtr conversationListPtr, int length)
        {
            Debug.Log(
                $"OnGetConversationTagInfoListCallback handle ({handle}) called, errCode={errCode},conversationList:{conversationListPtr}, length:{length}");

            var callback = RCIMUtils.TakeCallback<OperationCallbackWithResult<IList<RCConversationTagInfo>>>(handle);
            if (callback != null)
            {
                var rcConversations = new List<RCConversationTagInfo>();
                if (errCode == RCErrorCode.Succeed)
                {
                    IntPtr ptr = conversationListPtr;
                    int imConversationPtrSize = Marshal.SizeOf<im_conversation_tag_info>();
                    for (int i = 0; i < length; i++)
                    {
                        var convTagInfo = Marshal.PtrToStructure<im_conversation_tag_info>(ptr);

                        RCConversationTagInfo rcConvTagInfo = convTagInfo.toRCConversationTagInfo();
                        rcConversations.Add(rcConvTagInfo);
                        ptr = (IntPtr) ((long) ptr + imConversationPtrSize);
                    }
                }

                callback(errCode, rcConversations);
            }
        }

        [MonoPInvokeCallback(typeof(OnGeneralHandlerInt))]
        private static void OnSetConversationNotificationStatusCallback(Int64 handle, RCErrorCode errCode,
            int status)
        {
            RCConversationNotificationStatus retStatus = (RCConversationNotificationStatus) status;
            Debug.Log(
                $"OnSetConversationNotificationStatusCallback handle ({handle}) called, errCode={errCode},status:{status}, retStatus:{retStatus}");
            var callback =
                RCIMUtils.TakeCallback<OperationCallbackWithResult<RCConversationNotificationStatus>>(handle);
            if (callback != null)
            {
                callback(errCode, retStatus);
            }
        }

        [MonoPInvokeCallback(typeof(OnImMessageReceivedHandler))]
        private static void OnImReceivedMessageListener(ref im_message msg, int left, bool offline, bool hasMsg,
            int cmdLeft)
        {
            RCMessage rcMsg = RCIMUtils.toRCMessage(ref msg);
            Instance.NotifyOnMessageReceived(rcMsg, left);
        }

        [MonoPInvokeCallback(typeof(OnImBlockedMessageInfoHandler))]
        private static void OnImBlockedMessageInfoListener(ref im_blocked_message_info imBlockedMessageInfo)
        {
            Debug.Log($"OnImBlockedMessageInfoCallback called. blocked info:{imBlockedMessageInfo}");

            BlockedMessageInfo blockedMessageInfo = RCIMUtils.toBlockedMessageInfo(ref imBlockedMessageInfo);
            Instance.NotifyMessageBlocked(blockedMessageInfo);
        }

        [MonoPInvokeCallback(typeof(OnImRecallMessageHandler))]
        private static void OnImRecallMessageDidReceived(ref im_message imMsg,
            ref im_recall_notification_message recallNtfMsg)
        {
            RCMessage rcMsg = RCIMUtils.toRCMessage(ref imMsg);
            RCRecallNotificationMessage rcRecallNtfMsg = recallNtfMsg.toRCRecallNotificationMessage();
            Instance.NotifyOnMessageRecalled(rcMsg, rcRecallNtfMsg);
        }

        [MonoPInvokeCallback(typeof(OnImMessageExpansionDidUpdateHandler))]
        private static void OnImMessageExpansionDidUpdateListener(IntPtr keysPtr, IntPtr valuesPtr, int count,
            ref im_message imMsg)
        {
            Debug.Log($"OnImMessageExpansionDidUpdateCallback called. k:{keysPtr},v:{valuesPtr},m:{imMsg}");
            int ptrSize = Marshal.SizeOf<IntPtr>();
            var keysList = new List<string>();
            var valueList = new List<string>();

            for (int i = 0; i < count; i++)
            {
                var ptrKey = Marshal.PtrToStructure<IntPtr>(keysPtr + (ptrSize * i));
                var key = Marshal.PtrToStringAnsi(ptrKey);

                var ptrValue = Marshal.PtrToStructure<IntPtr>(valuesPtr + (ptrSize * i));
                var value = Marshal.PtrToStringAnsi(ptrValue);

                keysList.Add(key);
                valueList.Add(value);
            }

            Dictionary<string, string> changedDic = new Dictionary<string, string>();
            for (int i = 0; i < count; i++)
            {
                changedDic.Add(keysList[i], valueList[i]);
            }

            var rcMsg = RCIMUtils.toRCMessage(ref imMsg);
            Instance.NotifyMessageExpansionUpdate(changedDic, rcMsg);
        }

        [MonoPInvokeCallback(typeof(OnImMessageExpansionDidRemoveHandler))]
        private static void OnImMessageExpansionDidRemoveListener(IntPtr keysPtr, int count, ref im_message imMsg)
        {
            Debug.Log($"OnImMessageExpansionDidRemoveCallback called. k:{keysPtr}, m:{imMsg}");
            int ptrSize = Marshal.SizeOf<IntPtr>();
            var keysList = new List<string>();
            for (int i = 0; i < count; i++)
            {
                var ptrKey = Marshal.PtrToStructure<IntPtr>(keysPtr + (ptrSize * i));
                var key = Marshal.PtrToStringAnsi(ptrKey);
                keysList.Add(key);
            }

            var rcMsg = RCIMUtils.toRCMessage(ref imMsg);
            Instance.NotifyMessageExpansionRemoved(keysList, rcMsg);
        }

        [MonoPInvokeCallback(typeof(OnImTypingStatusChangedHandler))]
        private static void OnImTypingStatusChangedListener(RCConversationType conversationType, string targetId,
            IntPtr typeStatusListPtr, int count)
        {
            Debug.Log(
                $"OnImTypingStatusChangedListener called. {conversationType}, {targetId}, {typeStatusListPtr}, {count}");

            var rcTypingList = new List<RCTypingStatus>();
            int typingStatusSize = Marshal.SizeOf<im_typing_status>();
            IntPtr ptr = typeStatusListPtr;
            for (int i = 0; i < count; i++)
            {
                im_typing_status typingStatus = Marshal.PtrToStructure<im_typing_status>(ptr + (typingStatusSize * i));
                var rcTypingStatus = RCIMUtils.toRCTypingStatus(ref typingStatus);
                rcTypingList.Add(rcTypingStatus);
            }

            Instance.NotifyTypingStatusChanged(conversationType, targetId, rcTypingList);
        }

        [MonoPInvokeCallback(typeof(OnResultImGetNotificationQuietHoursHandler))]
        private static void OnImGetNotificationQuietHoursCallback(Int64 handle, RCErrorCode errCode, string startTime,
            int spansMin)
        {
            Debug.Log(
                $"OnImGetNotificationQuietHoursCallback handle({handle}) called. errCode:{errCode}, start time:{startTime}, span min:{spansMin}");
            var callback = RCIMUtils.TakeCallback<OperationCallbackWithResult<RCNotificationQuietHourInfo>>(handle);
            if (callback != null)
            {
                var notificationQuietInfo = new RCNotificationQuietHourInfo(startTime, spansMin);
                callback(errCode, notificationQuietInfo);
            }
        }

        /// <summary>
        /// 刚加入聊天室时 KV 同步完成的回调
        /// </summary>
        /// <param name="roomId"></param>
        [MonoPInvokeCallback(typeof(OnImChatRoomKvDidSyncedHandler))]
        private static void OnImChatRoomKvDidSyncedListener(string roomId)
        {
            Debug.Log($"OnImChatRoomKvDidSyncedListener called. roomId:{roomId}");
            Instance.NotifyOnChatRoomKVSync(roomId);
        }

        /// <summary>
        /// 聊天室 KV 变化的回调
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="keysPtr"></param>
        /// <param name="valuesPtr"></param>
        /// <param name="kvCount"></param>
        [MonoPInvokeCallback(typeof(OnImChatRoomKvAddRemovedHandler))]
        private static void OnImChatRoomKvDidUpdatedListener(string roomId, IntPtr keysPtr, IntPtr valuesPtr,
            int kvCount)
        {
            Debug.Log($"OnImChatRoomKvDidUpdatedListener called. roomId:{roomId},{keysPtr},{valuesPtr},{kvCount}");

            RCIMUtils.PtrToStringList(keysPtr, kvCount, out var keysList);
            RCIMUtils.PtrToStringList(valuesPtr, kvCount, out var valuesList);
            if (keysList != null && valuesList != null)
            {
                var dic = new Dictionary<string, string>();
                for (int i = 0; i < kvCount; i++)
                {
                    dic.Add(keysList[i], valuesList[i]);
                }

                Instance.NotifyOnChatRoomKVUpdate(roomId, dic);
            }
        }

        /// <summary>
        /// 聊天室 KV 被删除的回调
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="keysPtr"></param>
        /// <param name="valuesPtr"></param>
        /// <param name="kvCount"></param>
        [MonoPInvokeCallback(typeof(OnImChatRoomKvAddRemovedHandler))]
        private static void OnImChatRoomKvDidRemovedListener(string roomId, IntPtr keysPtr, IntPtr valuesPtr,
            int kvCount)
        {
            Debug.Log($"OnImChatRoomKvDidRemovedListener called. roomId:{roomId},{keysPtr},{valuesPtr},{kvCount}");

            RCIMUtils.PtrToStringList(keysPtr, kvCount, out var keysList);
            RCIMUtils.PtrToStringList(valuesPtr, kvCount, out var valuesList);
            if (keysList != null && valuesList != null)
            {
                var dic = new Dictionary<string, string>();
                for (int i = 0; i < kvCount; i++)
                {
                    dic.Add(keysList[i], valuesList[i]);
                }

                Instance.NotifyOnChatRoomKVRemove(roomId, dic);
            }
        }

        [MonoPInvokeCallback(typeof(OnImChatRoomMemberActionChangedHandler))]
        private static void OnImChatRoomMemberActionChangedListener(string roomId, IntPtr memberActions, int count)
        {
            Debug.Log($"OnImChatRoomMemberActionChangedListener called.");
            // todo: C#
        }

        [MonoPInvokeCallback(typeof(OnImChatRoomStatusChanged_Joining_Handler))]
        private static void OnImChatRoomStatusChanged_Joining_Listener(string roomId)
        {
            Debug.Log($"OnImChatRoomStatusChanged_Joining_Listener called. {roomId}");
            Instance.NotifyOnChatRoomJoining(roomId);
        }

        [MonoPInvokeCallback(typeof(OnImChatRoomStatusChanged_Joined_Handler))]
        private static void OnImChatRoomStatusChanged_Joined_Listener(string roomId)
        {
            Debug.Log($"OnImChatRoomStatusChanged_Joined_Listener called. {roomId}");
            Instance.NotifyOnChatRoomJoined(roomId);
        }

        [MonoPInvokeCallback(typeof(OnImChatRoomStatusChanged_Reset_Handler))]
        private static void OnImChatRoomStatusChanged_Reset_Listener(string roomId)
        {
            Debug.Log($"OnImChatRoomStatusChanged_Reset_Listener called. {roomId}");
            Instance.NotifyOnChatRoomReset(roomId);
        }

        [MonoPInvokeCallback(typeof(OnImChatRoomStatusChanged_Quited_Handler))]
        private static void OnImChatRoomStatusChanged_Quited_Listener(string roomId)
        {
            Debug.Log($"OnImChatRoomStatusChanged_Quited_Listener called. {roomId}");
            Instance.NotifyOnChatRoomQuited(roomId);
        }

        [MonoPInvokeCallback(typeof(OnImChatRoomStatusChanged_Destroyed_Handler))]
        private static void OnImChatRoomStatusChanged_Destroyed_Listener(string roomId, int destroyType)
        {
            Debug.Log($"OnImChatRoomStatusChanged_Quited_Listener called. {roomId} {destroyType}");
            Instance.NotifyOnChatRoomDestroyed(roomId, (RCChatRoomDestroyType)destroyType);
        }

        [MonoPInvokeCallback(typeof(OnImChatRoomStatusChanged_Error_Handler))]
        private static void OnImChatRoomStatusChanged_Error_Listener(string roomId, int errorCode)
        {
            Debug.Log($"OnImChatRoomStatusChanged_Error_Listener called. {roomId} {errorCode}");
            Instance.NotifyOnChatRoomError(roomId, (RCErrorCode)errorCode);
        }

        [MonoPInvokeCallback(typeof(OnGeneralHandlerIntPtr))]
        private static void OnGetChatRoomInfoCallback(Int64 handle, RCErrorCode errCode,
            IntPtr chatRoomInfoPtr)
        {
            Debug.Log($"OnGetChatRoomInfoCallback handle({handle}) called. errCode:{errCode}");
            var callback = RCIMUtils.TakeCallback<OperationCallbackWithResult<RCChatRoomInfo>>(handle);
            if (callback != null)
            {
                if (errCode == RCErrorCode.Succeed)
                {
                    im_chat_room_info imChatRoomInfo = Marshal.PtrToStructure<im_chat_room_info>(chatRoomInfoPtr);
                    RCChatRoomInfo roomInfo = imChatRoomInfo.toRCChatRoomInfo();
                    callback(errCode, roomInfo);
                }
                else
                {
                    callback(errCode, null);
                }
            }
        }

        [MonoPInvokeCallback(typeof(OnImChatRoomGetEntryHandler))]
        private static void OnImChatRoomGetEntryCallback(Int64 handle, RCErrorCode errCode, string roomId,
            string key, string value)
        {
            Debug.Log($"OnImChatRoomGetEntryCallback handle({handle}) called. errCode:{errCode}," +
                      $" roomId:{roomId}, key:{key}, value:{value}");
            var callback = RCIMUtils.TakeCallback<OperationCallbackWithResult<IDictionary<string, string>>>(handle);
            if (callback != null)
            {
                if (errCode == RCErrorCode.Succeed)
                {
                    var entry = new Dictionary<string, string>();
                    entry.Add(key, value);
                    callback(RCErrorCode.Succeed, entry);
                }
                else
                {
                    callback(errCode, null);
                }
            }
        }

        [MonoPInvokeCallback(typeof(OnImChatRoomGetAllEntriesHandler))]
        private static void OnImChatRoomGetAllEntriesCallback(Int64 handle, RCErrorCode errCode, string roomId,
            IntPtr keysPtr, IntPtr valuesPtr, int kvCount)
        {
            Debug.Log($"OnImChatRoomGetAllEntriesCallback handle({handle}) called. " +
                      $"errCode:{errCode}, roomId:{roomId}, kvCount:{kvCount}");
            var callback = RCIMUtils.TakeCallback<OperationCallbackWithResult<IDictionary<string, string>>>(handle);
            if (callback != null)
            {
                if (errCode == RCErrorCode.Succeed)
                {
                    var entries = RCIMUtils.KeyValuesToDictionary(keysPtr, valuesPtr, kvCount);
                    callback(errCode, entries);
                }
                else
                {
                    callback(errCode, null);
                }
            }
        }

        [MonoPInvokeCallback(typeof(OnSendMessageHandler))]
        private static void OnSendMessageCallback(int type, long msgId, RCErrorCode errCode, ref im_message imMsg)
        {
            Debug.Log($"OnSendMessageCallback type:{type}, msgId:{msgId},errCode:{errCode}");
            RCMessage rcMsg = RCIMUtils.toRCMessage(ref imMsg);
            if (errCode == RCErrorCode.Succeed)
            {
                var callbackType = (im_send_message_callback_type) type;
                if (callbackType == im_send_message_callback_type.IM_SEND_MESSAGE_ATTACHED)
                {
                    Instance.NotifyOnSendMessageAttached(rcMsg);
                }
                else
                {
                    Instance.NotifyOnSendMessageSucceed(rcMsg);
                }
            }
            else
            {
                Instance.NotifyOnSendMessageFailed(null, errCode);
            }
        }

        [MonoPInvokeCallback(typeof(OnImMediaMessageSendProgressHandler))]
        private static void OnMediaMessageSendProgressListener(ref im_message imMsg, int progress)
        {
            Debug.Log($"OnMediaMessageSendProgressListener called. msgId:{imMsg.messageId} {progress}");
            var rcMsg = RCIMUtils.toRCMessage(ref imMsg);
            Instance.NotifyOnSendMediaMessageProgress(rcMsg, progress);
        }

        [MonoPInvokeCallback(typeof(OnImMediaMessageSendCancelHandler))]
        private static void OnMediaMessageSendCancelListener(ref im_message imMsg)
        {
            Debug.Log($"OnMediaMessageSendCancelListener called. msgId:{imMsg.messageId}");
            var rcMsg = RCIMUtils.toRCMessage(ref imMsg);
            Instance.NotifyOnSendMediaMessageCanceled(rcMsg);
        }

        [MonoPInvokeCallback(typeof(OnGeneralHandlerIntPtr))]
        private static void OnGetMessageCallback(Int64 handle, RCErrorCode errCode,
            IntPtr imMessagePtr)
        {
            RCMessage rcMsg = RCIMUtils.toRCMessage(imMessagePtr);
            Debug.Log(
                $"OnGetMessageCallback handle({handle} called. errCode={errCode},imMessagePtr:{imMessagePtr},rcMsg:{rcMsg}");
            var callback = RCIMUtils.TakeCallback<OperationCallbackWithResult<RCMessage>>(handle);
            if (callback != null)
            {
                callback(errCode, rcMsg);
            }
        }

        [MonoPInvokeCallback(typeof(OnGeneralHandlerIntPtrList))]
        private static void OnGetMessageListCallback(Int64 handle, RCErrorCode errCode, IntPtr imMsgListPtr,
            int imMsgListLength)
        {
            // todo: test
            Debug.Log(
                $"OnGetMessageListCallback handle({handle}) called. errCode={errCode}, IntPtr={imMsgListPtr}, length={imMsgListLength}");
            var callback = RCIMUtils.TakeCallback<OperationCallbackWithResult<IList<RCMessage>>>(handle);
            if (callback != null)
            {
                List<RCMessage> rcMsgList = new List<RCMessage>();
                int ptrImMsgSize = Marshal.SizeOf<im_message>();
                IntPtr ptr = imMsgListPtr;
                for (int i = 0; i < imMsgListLength; i++)
                {
                    var rcMsg = RCIMUtils.toRCMessage(ptr + (ptrImMsgSize * i));
                    rcMsgList.Add(rcMsg);
                }

                callback(errCode, rcMsgList);
            }
        }

        [MonoPInvokeCallback(typeof(OnImGetChatRoomHistoryMessagesHandler))]
        private static void OnImGetChatRoomHistoryMessagesCallback(
            Int64 handle, RCErrorCode errCode, IntPtr chatRoomHistoryMessagesPtr)
        {
            var callback = RCIMUtils.TakeCallback<OperationCallbackWithResult<RCChatRoomHistoryMessageResult>>(handle);
            if (callback != null)
            {
                if (chatRoomHistoryMessagesPtr != IntPtr.Zero)
                {
                    var imChatRoomHistory =
                        Marshal.PtrToStructure<im_chatroom_history_messages>(chatRoomHistoryMessagesPtr);

                    if (imChatRoomHistory.imMsgList != IntPtr.Zero && imChatRoomHistory.imMsgListCount > 0)
                    {
                        List<RCMessage> rcMsgList = new List<RCMessage>();
                        int ptrImMsgSize = Marshal.SizeOf<im_message>();
                        IntPtr ptr = imChatRoomHistory.imMsgList;
                        for (int i = 0; i < imChatRoomHistory.imMsgListCount; i++)
                        {
                            var rcMsg = RCIMUtils.toRCMessage(ptr + (ptrImMsgSize * i));
                            rcMsgList.Add(rcMsg);
                        }

                        RCChatRoomHistoryMessageResult result =
                            new RCChatRoomHistoryMessageResult(rcMsgList, imChatRoomHistory.syncTime);
                        callback(errCode, result);
                    }
                    else
                    {
                        RCChatRoomHistoryMessageResult result = new RCChatRoomHistoryMessageResult();
                        result.SyncTime = imChatRoomHistory.syncTime;
                        callback(errCode, result);
                    }
                }
                else
                {
                    callback(errCode, null);
                }
            }
        }

        [MonoPInvokeCallback(typeof(OnGeneralHandlerIntPtrList))]
        private static void OnGetBlackListCallback(Int64 handle, RCErrorCode errCode, IntPtr blackListPtr,
            int blackListLength)
        {
            Debug.Log(
                $"OnGetBlackListCallback handle({handle}) called. errCode={errCode}, IntPtr={blackListPtr}, length={blackListLength}");
            RCIMUtils.PtrToStringList(blackListPtr, blackListLength, out var blackList);
            var callback = RCIMUtils.TakeCallback<OperationCallbackWithResult<IList<string>>>(handle);
            if (callback != null)
            {
                callback(errCode, blackList);
            }
        }

        [MonoPInvokeCallback(typeof(OnGeneralHandlerInt))]
        private static void OnGetBlackListStatusCallback(Int64 handle, RCErrorCode errCode, int status)
        {
            Debug.Log($"OnGetBlackListStatusCallback handle({handle}) called. errCode={errCode}, status={status}");
            RCBlackListStatus blacklistStatus = RCIMUtils.toBlackListStatus(status);

            var callback = RCIMUtils.TakeCallback<OperationCallbackWithResult<RCBlackListStatus>>(handle);
            if (callback != null)
            {
                callback(errCode, blacklistStatus);
            }
        }

        [MonoPInvokeCallback(typeof(OnGeneralHandlerIntPtr))]
        private static void OnRecallMessageCallback(Int64 handle, RCErrorCode errCode, IntPtr imPtr)
        {
            Debug.Log($"OnRecallMessageCallback handle({handle}) called. errCode={errCode}, IntPtr={imPtr}");
            var callback = RCIMUtils.TakeCallback<OperationCallbackWithResult<RCRecallNotificationMessage>>(handle);

            if (errCode == RCErrorCode.Succeed && imPtr != IntPtr.Zero)
            {
                var imRecallNotifyMsg = Marshal.PtrToStructure<im_recall_notification_message>(imPtr);
                var rcMsgContent = RCIMUtils.toRCRecallNotificationMessage(ref imRecallNotifyMsg);
                if (callback != null)
                {
                    callback(errCode, rcMsgContent);
                }
            }
            else
            {
                if (callback != null)
                {
                    callback(errCode, null);
                }
            }
        }

        [MonoPInvokeCallback(typeof(OnImReadReceiptReceivedHandler))]
        private static void OnImReadReceiptReceivedListener(RCConversationType type, String targetId, String senderId, Int64 lastMsgSentTime)
        {
            Debug.Log($"OnImReadReceiptReceivedListener called. {type} {targetId} {lastMsgSentTime}");
            Instance.NotifyReadReceiptReceived(type, targetId, senderId, lastMsgSentTime);
        }

        [MonoPInvokeCallback(typeof(OnImReadReceiptRequestHandler))]
        private static void OnImReadReceiptRequestListener(RCConversationType type, string targetId, string messageUid)
        {
            Debug.Log($"OnImReadReceiptRequestListener called. type:{type}, targetId:{targetId}, msgUid:{messageUid}");
            Instance.NotifyReadReceiptRequest(type, targetId, messageUid);
        }

        [MonoPInvokeCallback(typeof(OnImReadReceiptResponseHandler))]
        private static void OnImReadReceiptResponseListener(RCConversationType type, string targetId, string messageUid,
            IntPtr keysPtr, IntPtr valuesPtr, int kvCount)
        {
            Debug.Log($"OnImReadReceiptResponseListener called. type:{type}, targetId:{targetId}, msgUid:{messageUid}");
            if (kvCount > 0)
            {
                RCIMUtils.PtrToStringList(keysPtr, kvCount, out var keyList);
                RCIMUtils.PtrToInt64List(valuesPtr, kvCount, out var valueList);
                var keyArray = keyList.ToArray();
                var valueArray = valueList.ToArray();

                var dic = new Dictionary<string, long>();
                for (int i = 0; i < kvCount; i++)
                {
                    dic.Add(keyArray[i], valueArray[i]);
                }

                Instance.NotifyReadReceiptResponse(type, targetId, messageUid, dic);
            }
        }

        [MonoPInvokeCallback(typeof(OnImDownloadMediaProgress))]
        private static void OnDownloadMediaMessageProgress(ref im_message imMsg, int progress)
        {
            Debug.Log($"Download media message id:{imMsg.messageId} progress:{progress}");
            RCMessage rcMsg = RCIMUtils.toRCMessage(ref imMsg);
            Instance.NotifyDownloadMediaMessageProgressed(rcMsg, progress);
        }

        [MonoPInvokeCallback(typeof(OnImDownloadMediaCompleted))]
        private static void OnDownloadMediaMessageCallbackSucceed(ref im_message imMsg, string mediaPath)
        {
            Debug.Log($"Download media message succeed. id:{imMsg.messageId}, path:{mediaPath}");
            // todo: test
            RCMessage rcMsg = RCIMUtils.toRCMessage(ref imMsg);
            Instance.NotifyDownloadMediaMessageCompleted(rcMsg);
        }

        [MonoPInvokeCallback(typeof(OnImDownloadMediaFailed))]
        private static void OnDownloadMediaMessageCallbackFailed(ref im_message imMsg, RCErrorCode errCode)
        {
            Debug.Log($"Download media message failed. id:{imMsg.messageId}, errCode:{errCode}");
            RCMessage rcMsg = RCIMUtils.toRCMessage(ref imMsg);
            Instance.NotifyDownloadMediaMessageFailed(errCode, rcMsg);
        }

        [MonoPInvokeCallback(typeof(OnImDownloadMediaCanceled))]
        private static void OnDownloadMediaMessageCallbackCanceled(ref im_message imMsg)
        {
            Debug.Log($"OnDownloadMediaMessageCallbackCanceled called. id:{imMsg.messageId}");
            RCMessage rcMsg = RCIMUtils.toRCMessage(ref imMsg);
            Instance.NotifyDownloadMediaMessageCanceled(rcMsg);
        }

        [MonoPInvokeCallback(typeof(OnGeneralHandlerIntPtrList))]
        private static void OnGetTagsCallback(Int64 handle, RCErrorCode errCode, IntPtr tagInfosPtr, int length)
        {
            Debug.Log($"OnGetTagsCallback handle({handle}) called. errCode:{errCode}, length:{length}");
            var callback = RCIMUtils.TakeCallback<OperationCallbackWithResult<IList<RCTagInfo>>>(handle);
            if (callback != null)
            {
                if (errCode == RCErrorCode.Succeed)
                {
                    RCIMUtils.PtrToStructureList<im_tag_info>(tagInfosPtr, length, out var imTagInfos);
                    List<RCTagInfo> rcTagInfos = new List<RCTagInfo>();

                    foreach (var imTagInfo in imTagInfos)
                    {
                        var rcTagInfo = imTagInfo.toRCTagInfo();
                        rcTagInfos.Add(rcTagInfo);
                    }

                    callback(RCErrorCode.Succeed, rcTagInfos);
                }
                else
                {
                    callback(errCode, null);
                }
            }
        }

        [MonoPInvokeCallback(typeof(OnImCallbackVoid))]
        private static void OnImTagChangedListener()
        {
            Debug.Log($"OnImTagChangedListener called.");
            Instance.NotifyOnTagChanged();
        }

        [MonoPInvokeCallback(typeof(OnImCallbackVoid))]
        private static void OnImConversationTagChangedListener()
        {
            Debug.Log($"OnImConversationTagChangedListener called.");
            Instance.NotifyOnConversationTagChanged();
        }

        #endregion
    }
}
#endif