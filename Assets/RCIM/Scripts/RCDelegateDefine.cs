//
//  Copyright © 2021 RongCloud. All rights reserved.
//

using System;
using System.Collections.Generic;
using UnityEngine;

namespace cn_rongcloud_im_unity
{

    public delegate void OnConnectionStatusChangedDelegate(RCConnectionStatus connectionStatus);
    public delegate void OnTagChangedDelegate();
    public delegate void OnConversationTagChangedDelegate();
    public delegate void OnTypingStatusChangedDelegate(RCConversationType conversationType, string targetId, List<RCTypingStatus> typeStatusList);

    public delegate void MessageAttachedDelegate(RCMessage message);
    public delegate void MessageSendSucceedDelegate(RCMessage message);
    public delegate void MessageSendFailedDelegate(RCErrorCode errorCode, RCMessage message);

    public delegate void MediaMessageSendProgressDelegate(RCMessage message, int progress);

    public delegate void MediaMessageSendCancelDelegate(RCMessage message);

    public delegate void DownloadMediaMessageCompletedDelegate(RCMessage message);
    public delegate void DownloadMediaMessageFailedDelegate(RCErrorCode errorCode, RCMessage message);
    public delegate void DownloadMediaMessageProgressDelegate(RCMessage message, int progress);

    public delegate void DownloadMediaMessagePausedDelegate(RCErrorCode code, RCMessage message);
    public delegate void DownloadMediaMessageCancelDelegate(RCMessage message);

    public delegate void OnMessageReceivedDelegate(RCMessage msg, int left);

    public delegate void OnReadReceiptReceivedDelegate(RCConversationType type, String targetId, String senderUserId, Int64 timestamp);
    public delegate void OnReadReceiptRequestDelegate(RCConversationType type, string targetId, string messageUid);
    public delegate void OnReadReceiptResponseDelegate(RCConversationType type, string targetId, string messageUid, IDictionary<string, Int64> respondUserIdList);

    public delegate void OnMessageExpansionRemovedDelegate(IList<string> keyList, RCMessage message);
    public delegate void OnMessageExpansionUpdatedDelegate(IDictionary<string, string> expansionChanged, RCMessage message);

    public delegate void OnMessageRecalledDelegate(RCMessage message, RCRecallNotificationMessage notification);
    public delegate void OnMessageBlockedDelegate(BlockedMessageInfo blockedMessageInfo);

    /// <summary>
    /// 加入IM房间回调
    /// </summary>
    /// <param name="roomId"></param>
    public delegate void OnJoinChatRoomDelegate(string roomId, RCOperationStatus status);

    /// <summary>
    /// 退出IM房间回调
    /// </summary>
    /// <param name="code"></param>
    public delegate void OnQuitChatRoomDelegate(string roomId, RCOperationStatus code);

    public delegate void OnChatRoomAdvancedActionJoiningDelegate(string roomId);
    public delegate void OnChatRoomAdvancedActionJoinedDelegate(string roomId);
    public delegate void OnChatRoomAdvancedActionResetDelegate(string roomId);
    public delegate void OnChatRoomAdvancedActionQuitedDelegate(string roomId);
    public delegate void OnChatRoomAdvancedActionDestroyedDelegate(string roomId, RCChatRoomDestroyType destroyType);
    public delegate void OnChatRoomAdvancedActionErrorDelegate(string roomId, RCErrorCode code);

    public delegate void OnChatRoomKVSyncDelegate(string roomId);
    public delegate void OnChatRoomKVUpdateDelegate(string roomId, IDictionary<string, string> roomKVDic);
    public delegate void OnChatRoomKVRemoveDelegate(string roomId, IDictionary<string, string> roomKVDic);
}