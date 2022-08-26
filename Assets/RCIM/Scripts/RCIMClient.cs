//
//  Copyright © 2021 RongCloud. All rights reserved.
//

using System.Collections.Generic;
using System;
using UnityEngine;

namespace cn_rongcloud_im_unity
{
    public abstract class RCIMClient
    {
        /// <summary>
        /// 连接状态变更监听事件
        /// </summary>
        public event OnConnectionStatusChangedDelegate OnConnectionStatusChanged;

        public event MessageAttachedDelegate OnSendMessageAttached;
        public event MessageSendSucceedDelegate OnSendMessageSucceed;
        public event MessageSendFailedDelegate OnSendMessageFailed;
        public event MediaMessageSendProgressDelegate OnSendMediaMessageProgress;
        public event MediaMessageSendCancelDelegate OnSendMediaMessageCanceled;
        public event OnMessageReceivedDelegate OnMessageReceived;

        public event OnTypingStatusChangedDelegate OnTypingStatusChanged;

        public event DownloadMediaMessageCompletedDelegate OnDownloadMediaMessageCompleted;
        public event DownloadMediaMessageProgressDelegate OnDownloadMediaMessageProgressed;
#if UNITY_ANDROID
        public event DownloadMediaMessagePausedDelegate OnDownloadMediaMessagePaused;
#endif
        public event DownloadMediaMessageFailedDelegate OnDownloadMediaMessageFailed;
        public event DownloadMediaMessageCancelDelegate OnDownloadMediaMessageCanceled;

        public event OnReadReceiptReceivedDelegate OnReadReceiptReceived;
        public event OnReadReceiptRequestDelegate OnReadReceiptRequest;
        public event OnReadReceiptResponseDelegate OnReadReceiptResponse;

        public event OnMessageExpansionRemovedDelegate OnMessageExpansionRemoved;
        public event OnMessageExpansionUpdatedDelegate OnMessageExpansionUpdated;


        public event OnChatRoomAdvancedActionJoiningDelegate OnChatRoomJoining;
        public event OnChatRoomAdvancedActionJoinedDelegate OnChatRoomJoined;
        public event OnChatRoomAdvancedActionResetDelegate OnChatRoomReset;
        public event OnChatRoomAdvancedActionQuitedDelegate OnChatRoomQuited;
        public event OnChatRoomAdvancedActionDestroyedDelegate OnChatRoomDestroyed;
        public event OnChatRoomAdvancedActionErrorDelegate OnChatRoomError;

        public event OnChatRoomKVSyncDelegate OnChatRoomKVSync;
        public event OnChatRoomKVUpdateDelegate OnChatRoomKVUpdate;
        public event OnChatRoomKVRemoveDelegate OnChatRoomKVRemove;

        //public event OnMessageBlockedDelegate OnMessageBlocked;

        /// <summary>
        /// 标签更改监听器, 当用户在其它端添加移除更新标签时会触发此监听器，用于多端之间的信息同步
        /// </summary>
        public event OnTagChangedDelegate OnTagChanged;

        /// <summary>
        ///  会话标签更改监听器, 
        /// </summary>
        public event OnConversationTagChangedDelegate OnConversationTagChanged;

        public event OnMessageRecalledDelegate OnMessageRecalled;

        private static RCIMClient instance = null;
        private static readonly object syncObj = new object();

        public static RCIMClient Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncObj)
                    {
                        if (null == instance)
                        {
#if UNITY_IOS
                            instance = new RCIMClientIOS();
#elif UNITY_ANDROID
                            instance = new RCIMClientAndroid();
#elif UNITY_STANDALONE_WIN
                            instance = new RCIMClientWin();
#else
                            throw new Exception();
#endif
                        }
                    }
                }

                return instance;
            }
        }

        public virtual void Destroy()
        {
            lock (syncObj)
            {
                instance = null;
            }
        }

#if UNITY_STANDALONE_WIN
        /// <summary>
        /// 获取im句柄
        /// </summary>
        /// <returns></returns>
        public abstract IntPtr Handler();
#endif

        /// <summary>
        /// 设置私有部署的导航服务器和媒体服务器地址
        /// 可以支持设置 http://cn.xxx.com 或 https://cn.xxx.com 或 cn.xxx.com，
        /// 如果设置成 cn.xxx.com，SDK 会组装成 http:// 协议格式。
        /// 支持传入多个导航, 多个导航地址之间须以分号 ； 分隔。
        /// 注意：此方法在 Init(string) 前使用
        /// </summary>
        /// <param name="naviServer"></param>
        /// <param name="fileServer"></param>
        public abstract void SetServerInfo(string naviServer, string fileServer);

        /// <summary>
        /// 初始化融云 SDK，使用前调用一次即可。
        /// </summary>
        /// <param name="appKey">在开发者后台申请应用对应的 appkey</param>
        public abstract void Init(string appKey);

        /// <summary>
        /// 设置当前用户离线消息补偿时间
        /// 离线消息补偿时间是指某用户离线后，在下次登录时，服务端下发的离线消息对应的时间段。比如某应用的离线消息补偿时间是 2 天，用户离线 3 天，在第 4 天登录的时候，
        /// 服务端只会主动下发该用户第 2 天和第 3 天对应的离线消息；第 1 天的离线消息不会下发。
        /// 该功能首先需要客户提工单，在服务端开通此功能后，客户端调用该方法才生效
        /// duration 离线消息补偿时间，参数取值范围为int值1~7天。
        /// </summary>
        public abstract void SetOfflineMessageDuration(int duration, OperationCallbackWithResult<long> callback);

        /// <summary>
        /// 获取当前用户离线消息的存储时间，取值范围为int值1~7天
        /// </summary>
        public abstract void GetOfflineMessageDuration(OperationCallbackWithResult<long> callback);

        /// <summary>
        /// 设置 push 相关配置。在初始化 SDK 之前调用
        /// </summary>
        /// <param name="androidPushConfig"></param>
        public abstract void SetAndroidPushConfig(RCAndroidPushConfig androidPushConfig);

#if UNITY_IOS
        /// <summary>
        /// 仅 ios 使用，Android 请在 rc_configuration.xml 文件进行配置
        /// </summary>
        public abstract void SetImageCompressConfig(double maxSize, double minSize, double quality);
#endif

        public abstract RCConnectionStatus GetConnectionStatus();

        /// <summary>
        /// 设置断线重连时是否踢出重连设备
        /// </summary>
        /// <remarks> 
        /// 用户没有开通多设备登录功能的前提下，同一个账号在一台新设备上登录的时候，会把这个账号在之前登录的设备上踢出。
        /// 由于 SDK 有断线重连功能，存在下面情况：
        /// 用户在 A 设备登录，A 设备网络不稳定，没有连接成功，SDK 启动重连机制。
        /// 用户此时又在 B 设备登录，B 设备连接成功。
        /// A 设备网络稳定之后，用户在 A 设备连接成功，B 设备被踢出。
        /// 这个接口就是为这种情况加的。<br>
        /// 设置 enable 为 {@code true}
        /// 时，SDK 重连的时候发现此时已有别的设备连接成功，不再强行踢出已有设备，而是踢出重连设备。<br>
        /// <strong>注意：</strong> 1. 该功能需要提工单，在服务端开通此功能后，客户端调用该方法才生效。<br>
        /// 2. 此方法需要在 {@link #init(Context)}之前调用。
        /// </remarks>
        /// <param name="kick"></param>
        public abstract void SetKickReconnectedDevice(bool kick);

        /// <summary>
        /// 获取本地时间与服务器时间的时间差
        /// </summary>
        /// <remarks>
        /// 消息发送成功后，SDK 与服务器同步时间，消息所在数据库中存储的时间就是服务器时间。
        /// System.currentTimeMillis() - getDeltaTime() 可以获取服务器当前时间。
        /// </remarks>
        /// <returns></returns>
        public abstract Int64 GetDeltaTime();

        /// <summary>
        /// 连接融云服务器
        /// </summary>
        /// <param name="token"></param>
        /// <param name="callback"></param>
        public abstract void Connect(string token, OperationCallbackWithResult<string> callback);

        /// <summary>
        /// 断开连接，但保持推送消息通道
        /// </summary>
        public abstract void Disconnect();

        /// <summary>
        /// 断开连接，不再接收推送消息
        /// </summary>
        public abstract void Logout();

        /// <summary>
        /// 获取当前用户 ID
        /// </summary>
        /// <returns></returns>
        public abstract string GetCurrentUserID();

#if UNITY_ANDROID
        public abstract void EnablePush(bool enable);

        public abstract void DisableIPC(bool disable);
#endif

        public abstract void GetConversation(RCConversationType type, string targetId,
            OperationCallbackWithResult<RCConversation> callback);

        public abstract void RemoveConversation(RCConversationType type, string targetId, OperationCallback callback);

        public abstract void GetConversationList(OperationCallbackWithResult<IList<RCConversation>> callback,
            params RCConversationType[] conversationTypes);

        public abstract void GetConversationListByPage(OperationCallbackWithResult<IList<RCConversation>> callback,
            Int64 timestamp, int count, params RCConversationType[] conversationTypes);

        public abstract void GetConversationsFromTagByPage(string tagId, Int64 timestamp, int count,
            OperationCallbackWithResult<IList<RCConversation>> callback);

        public abstract void SearchConversations(string keyword, RCConversationType[] conversationTypes,
            string[] objectNames, OperationCallbackWithResult<IList<RCSearchConversationResult>> callback);

        public abstract void ClearConversations(OperationCallback callback,
            params RCConversationType[] conversationTypes);

        public abstract void GetTextMessageDraft(RCConversationType type, string targetId,
            OperationCallbackWithResult<string> callback);

        public abstract void SaveTextMessageDraft(RCConversationType type, string targetId, string textDraft,
            OperationCallback callback);

        public abstract void ClearTextMessageDraft(RCConversationType type, string targetId,
            OperationCallback callback);

        public abstract void GetUnreadCount(RCConversationType type, string targetId,
            OperationCallbackWithResult<int> callback);

        public abstract void GetUnreadCountByTag(string tagId, bool containsDND,
            OperationCallbackWithResult<int> callback);

        public abstract void GetUnreadCountByConversationTypes(bool containsDND,
            OperationCallbackWithResult<int> callback, params RCConversationType[] conversationTypes);

        public abstract void GetTotalUnreadCount(OperationCallbackWithResult<int> callback);

        public abstract void ClearMessageUnreadStatus(RCConversationType type, string targetId,
            OperationCallback callback);

        public abstract void SyncConversationReadStatus(RCConversationType type, string targetId, Int64 timestamp,
            OperationCallback callback);

        public abstract void GetFirstUnreadMessage(RCConversationType type, string targetId,
            OperationCallbackWithResult<RCMessage> callback);

        public abstract void GetUnreadMentionedMessages(RCConversationType type, string targetId,
            OperationCallbackWithResult<IList<RCMessage>> callback);

        public abstract void SetConversationNotificationStatus(RCConversationType type, string targetId,
            RCConversationNotificationStatus notificationStatus,
            OperationCallbackWithResult<RCConversationNotificationStatus> callback);

        public abstract void GetConversationNotificationStatus(RCConversationType type, string targetId,
            OperationCallbackWithResult<RCConversationNotificationStatus> callback);

        public abstract void GetBlockedConversationList(OperationCallbackWithResult<IList<RCConversation>> callback,
            params RCConversationType[] conversationTypes);

        public abstract void SetConversationToTop(RCConversationType type, string targetId, bool isTop,
            OperationCallback callback);

        public abstract void SetConversationToTopInTag(RCConversationType type, string targetId, string tagId,
            bool isTop, OperationCallback callback);

        public abstract void GetConversationTopStatusInTag(RCConversationType type, string targetId, string tagId,
            OperationCallbackWithResult<bool> callback);

        public abstract void GetTopConversationList(OperationCallbackWithResult<IList<RCConversation>> callback,
            params RCConversationType[] conversationTypes);

#if UNITY_IOS
        public abstract void SetTypingUpdateInterval(int typingInterval);
#endif
        /// <summary>
        /// 发送本地输入状态。(目前只支持单聊)
        /// 注册<see cref="OnTypingStatusChanged"/>事件，监听对方输入状态
        /// </summary>
        /// <param name="type"></param>
        /// <param name="targetId"></param>
        /// <param name="typingContent"></param>
        public abstract void SendTypingStatus(RCConversationType type, string targetId, string typingContent);

        public abstract void GetMessage(Int64 messageId, OperationCallbackWithResult<RCMessage> callback);
        public abstract void GetMessageByUid(string messageUid, OperationCallbackWithResult<RCMessage> callback);

        /// <summary>
        /// 给目标发送指定类型消息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="targetId">房间编号</param>
        /// <param name="content">信息内容</param>
        /// <param name="disableNotification">本条消息禁用通知</param>
        public abstract void SendMessage(RCConversationType type, string targetId, RCMessageContent content,
            bool disableNotification = false);

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">要发送的消息体</param>
        /// <param name="pushContent">当下发远程推送消息时，在通知栏里会显示这个字段。
        /// 如果发送的是自定义消息，该字段必须填写，否则无法收到远程推送消息。
        /// 如果发送 sdk 中默认的消息类型，例如 RC:TxtMsg, RC:VcMsg, RC:ImgMsg，则不需要填写，默认已经指定
        /// </param>
        /// <param name="pushData">远程推送附加信息。如果设置该字段，用户在收到远程推送消息时，能通过 {@link io.rong.push.notification.PushNotificationMessage#getPushData()} 方法获取。</param>
        public abstract void SendMessage(RCMessage message, string pushContent = null, string pushData = null);

        public abstract void SendMessageCarriesPush(RCConversationType type, string targetId, RCMessageContent content,
            string pushContent, string pushData, bool disableNotification = false);

        public abstract void SendDirectionalMessage(RCConversationType type, string targetId, IList<string> userIdList,
            RCMessageContent content, string pushContent = null, string pushData = null);

        /// <summary>
        /// 发送多媒体消息
        /// </summary>
        /// <param name="message">发送前构造消息实体，消息实体中的 content 必须为多媒体消息</param>
        /// <param name="pushContent">远程推送附加信息</param>
        /// <param name="pushData">发送消息的回调</param>
        public abstract void SendMediaMessage(RCMessage message, string pushContent = null, string pushData = null);

        public abstract void DownloadMediaMessage(RCMessage message);
#if UNITY_ANDROID
        public abstract void PauseDownloadMediaMessage(RCMessage message);
#endif
        public abstract void CancelDownloadMediaMessage(RCMessage message);

        public abstract void BatchInsertMessage(IList<RCMessage> messageList,
            OperationCallbackWithResult<bool> callback);

        public abstract void InsertIncomingMessage(RCConversationType type, string targetId, string senderId,
            RCReceivedStatus receivedStatus, RCMessageContent content, Int64 sentTime,
            OperationCallbackWithResult<RCMessage> callback);

        public abstract void InsertOutgoingMessage(RCConversationType type, string targetId, RCSentStatus sentStatus,
            RCMessageContent content, Int64 sendTime, OperationCallbackWithResult<RCMessage> callback);

        public abstract void RecallMessage(RCMessage message, string pushContent,
            OperationCallbackWithResult<RCRecallNotificationMessage> callback);

        public abstract void ForwardMessageByStep(RCConversationType type, string targetId, RCMessage message);

        /// <summary>
        /// 获取指定会话历史消息
        /// </summary>
        /// <remarks>
        /// 此方法先从本地获取历史消息，本地有缺失的情况下会从服务端同步缺失的部分；从服务端同步失败的时候会返回非 0 的 errorCode，同时把本地能取到的消息回调上去。<br>
        /// * 必须开通历史消息云存储功能。
        /// </remarks>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 id</param>
        /// <param name="option"><see cref="RCHistoryMessageOption"/></param>
        /// <param name="callback"></param>
        public abstract void GetMessages(RCConversationType type, string targetId, RCHistoryMessageOption option,
            OperationCallbackWithResult<IList<RCMessage>> callback);

        /// <summary>
        /// 获取会话中，从指定消息之前、指定数量的、指定消息类型的最新消息实体
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 id。根据不同的 conversationType，可能是用户 id、讨论组 id、群组 id</param>
        /// <param name="lastMessageId">最后一条消息的 id。获取此消息之前的 count 条消息，没有消息第一次调用应设置为 -1</param>
        /// <param name="count">要获取的消息数量</param>
        /// <param name="callback">获取历史消息的回调，按照时间顺序从新到旧排列。</param>
        public abstract void GetHistoryMessages(RCConversationType type, string targetId, Int64 lastMessageId,
            int count, OperationCallbackWithResult<IList<RCMessage>> callback);

        /// <summary>
        /// 在会话中搜索指定消息的前 beforeCount 数量和 afterCount 数量的消息。
        /// 返回的消息列表中会包含指定的消息。消息列表时间顺序从新到旧。
        /// </summary>
        /// <param name="type">指定的会话类型</param>
        /// <param name="targetId">指定的会话 id</param>
        /// <param name="sentTime">指定消息的发送时间，不能为 0</param>
        /// <param name="beforeCount">指定消息的前部分消息数量</param>
        /// <param name="afterCount">指定消息的后部分消息数量</param>
        /// <param name="callback">结果回调</param>
        public abstract void GetHistoryMessages(RCConversationType type, string targetId, Int64 sentTime,
            int beforeCount, int afterCount, OperationCallbackWithResult<IList<RCMessage>> callback);

        /// <summary>
        /// 从服务器端获取指定时间之前的历史消息
        /// </summary>
        /// <remarks>
        /// 区别于 {@link #getHistoryMessages}，该接口是从融云服务器中拉取。从服务端拉取消息后，客户端会做排重，返回排重后的数据。通常用于更换新设备后，拉取历史消息。
        /// 使用的时候，建议优先通过 {@link #getHistoryMessages(Conversation.ConversationType, String, int, int, ResultCallback)} 从本地数据库拉取历史消息，
        /// 当本地数据库没有历史消息后，再通过此接口获取服务器历史消息，时间戳传入本地数据库里最早的消息时间戳。
        /// 注意：
        /// 1. 此功能需要在融云开发者后台开启历史消息云存储功能。
        /// * 2. 当本地数据库中已存在将要获取的消息时，此接口不会再返回数据。
        /// </remarks>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">目标会话 id。根据不同的 conversationType，可能是用户 id、讨论组 id、群组 id</param>
        /// <param name="dateTime">从该时间点开始获取消息。即：消息中的 sentTime {@link Message#getSentTime()}；如果本地库中没有消息，第一次可传 0，否则传入最早消息的sentTime，获取最新 count 条</param>
        /// <param name="count">需要获取的消息数量， 0 < count <= 20</param>
        /// <param name="callback">结果回调</param>
        public abstract void GetRemoteHistoryMessages(RCConversationType type, string targetId, Int64 dateTime,
            int count, OperationCallbackWithResult<IList<RCMessage>> callback);

        public abstract void SearchMessages(RCConversationType type, string targetId, string keyword, int count,
            Int64 beginTime, OperationCallbackWithResult<IList<RCMessage>> callback);

        /// <summary>
        /// 删除指定的多条消息
        /// </summary>
        /// <param name="messageIdList">要删除的消息 id 数组</param>
        /// <param name="callback"></param>
        public abstract void DeleteMessages(Int64[] messageIdList, OperationCallbackWithResult<bool> callback);

        /// <summary>
        /// 删除某个会话中的所有消息。注意：不支持聊天室！
        /// </summary>
        /// <param name="type"></param>
        /// <param name="targetId"></param>
        /// <param name="callback"></param>
        public abstract void ClearMessages(RCConversationType type, string targetId,
            OperationCallbackWithResult<bool> callback);

        /// <summary>
        /// 删除指定时间戳之前的消息，可选择是否同时删除服务器端消息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="targetId"></param>
        /// <param name="recordTime">清除消息截止时间戳，【0 <= recordTime <= 当前会话最后一条消息的 sentTime,0 清除所有消息，其他值清除小于等于 recordTime 的消息】。</param>
        /// <param name="clearRemote">是否删除服务器端消息</param>
        /// <param name="callback"></param>
        public abstract void ClearHistoryMessages(RCConversationType type, string targetId, Int64 recordTime,
            bool clearRemote, OperationCallback callback);

        /// <summary>
        /// 批量删除某个会话中的指定远端消息（同时删除对应的本地消息）。
        /// </summary>
        /// <param name="type"></param>
        /// <param name="targetId"></param>
        /// <param name="messageList"></param>
        /// <param name="callback"></param>
        public abstract void DeleteRemoteMessages(RCConversationType type, string targetId,
            IList<RCMessage> messageList, OperationCallback callback);

        /// <summary>
        /// 设置本地消息的附加信息
        /// </summary>
        /// <remarks>
        /// 用于扩展消息的使用场景。设置后可以通过 {@link #getHistoryMessages} 取出带附加信息的消息。<br>
        /// <strong>注意：只能用于本地使用，无法同步给远程用户。</strong>
        /// </remarks>
        /// <param name="messageId">消息 id</param>
        /// <param name="extra">附加信息，最大 1024 字节</param>
        /// <param name="callback">是否设置成功的回调</param>
        public abstract void SetMessageExtra(Int64 messageId, string extra, OperationCallbackWithResult<bool> callback);

        /// <summary>
        /// 设置消息接收状态
        /// </summary>
        /// <param name="messageId">消息 id</param>
        /// <param name="status">接收到的消息状态。参考<see cref="RCReceivedStatus"/></param>
        /// <param name="callback">是否设置成功的回调</param>
        public abstract void SetMessageReceivedStatus(Int64 messageId, RCReceivedStatus status,
            OperationCallbackWithResult<bool> callback);

        /// <summary>
        /// 设置消息发送状态
        /// </summary>
        /// <param name="messageId">消息 id</param>
        /// <param name="rCSentStatus">接收到的消息状态。参考<see cref="RCSentStatus"/></param>
        /// <param name="callback">是否设置成功的回调</param>
        public abstract void SetMessageSentStatus(Int64 messageId, RCSentStatus rCSentStatus,
            OperationCallbackWithResult<bool> callback);

        public abstract void SendReadReceiptMessage(RCConversationType type, string targetId, Int64 timestamp,
            OperationCallback callback);

        public abstract void SendReadReceiptRequest(RCMessage message, OperationCallback callback);

        public abstract void SendReadReceiptResponse(RCConversationType type, string targetId,
            IList<RCMessage> messageList, OperationCallback callback);

        public abstract void UpdateMessageExpansion(string messageUid, IDictionary<string, string> expansionDic,
            OperationCallback callback);

        public abstract void RemoveMessageExpansionForKey(string messageUid, IList<string> keyList,
            OperationCallback callback);

        public abstract void AddTag(RCTagInfo tagInfo, OperationCallback callback);
        public abstract void UpdateTag(RCTagInfo tagInfo, OperationCallback callback);
        public abstract void RemoveTag(RCTagInfo tagInfo, OperationCallback callback);
        public abstract void GetTags(OperationCallbackWithResult<IList<RCTagInfo>> callback);

        public abstract void AddConversationsToTag(string tagId, IList<RCConversationIdentifier> conversations,
            OperationCallback callback);

        public abstract void GetTagsFromConversation(RCConversationType type, string targetId,
            OperationCallbackWithResult<IList<RCConversationTagInfo>> callback);

        public abstract void RemoveConversationFromTag(string tagId,
            IList<RCConversationIdentifier> conversationIdentifiers, OperationCallback callback);

        /// <summary>
        /// 加入聊天室，如果不存在则创建该聊天室
        /// </summary>
        /// <param name="roomId">房间编号</param>
        /// <param name="callback"></param>
        /// <param name="msgCount">拉取信息条数</param>
        public abstract void JoinChatRoom(string roomId, OperationCallback callback, int msgCount = 10);

        /// <summary>
        /// 加入已存在的聊天室
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="callback"></param>
        /// <param name="msgCount"></param>
        public abstract void JoinExistChatRoom(string roomId, OperationCallback callback, int msgCount = 10);

        /// <summary>
        /// 退出当前聊天室
        /// </summary>
        /// <param name="roomId">房间编号</param>
        public abstract void QuitChatRoom(string roomId, OperationCallback callback);

        /// <summary>
        /// 获取聊天室的信息（包含部分成员信息和当前聊天室中的成员总数）
        /// </summary>
        /// <param name="roomId">聊天室 id</param>
        /// <param name="memberCount">需要获取的成员信息的数量（目前获取到的聊天室信息中仅包含不多于 20人 的成员信息，即 0 <= count <= 20，传入 0 获取到的聊天室信息将或仅包含成员总数，不包含具体的成员列表）。</param>
        /// <param name="order">按照何种顺序返回聊天室成员信息。升序返回最早加入的用户列表； 降序返回最晚加入的用户列表。</param>
        /// <param name="callback"></param>
        public abstract void GetChatRoomInfo(string roomId, int memberCount, RCChatRoomMemberOrder order,
            OperationCallbackWithResult<RCChatRoomInfo> callback);

        /// <summary>
        /// 获取聊天室历史消息
        /// * 从服务器端获取聊天室之前的历史消息，如果指定时间 0，则从存储的第一条消息开始拉取。<br>
        /// * <Strong>注意：必须先开通聊天室消息云存储功能。</Strong>
        /// </summary>
        /// <param name="targetId">会话 id</param>
        /// <param name="recordTime">起始的消息发送时间戳，单位: 毫秒</param>
        /// <param name="count">需要获取的消息数量，0 < count <= 200</param>
        /// <param name="asc">拉取顺序: true升序, 按照时间戳从小到大; false 降序, 按照时间戳从大到小; </param>
        /// <param name="callback"></param>
        public abstract void GetChatRoomHistoryMessages(string targetId, Int64 recordTime, int count, bool asc,
            OperationCallbackWithResult<RCChatRoomHistoryMessageResult> callback);

        /// <summary>
        /// 设置聊天室自定义属性.
        /// key 不存在，增加属性； key 已存在，只能更新当前用户设置的属性值。注意：必须先开通聊天室状态存储功能。
        /// </summary>
        /// <param name="roomId">房间编号</param>
        /// <param name="key">聊天室属性名称，Key 支持大小写英文字母、数字、部分特殊符号 + = - _ 的组合方式，最大长度 128 个字符。</param>
        /// <param name="value">聊天室属性对应的值，最大长度 4096 个字符。</param>
        /// <param name="sendNotification">是否发送通知</param>
        /// <param name="autoRemove">退出后是否删除</param>
        /// <param name="notificationExtra">通知的自定义字段，通知消息 {@link ChatRoomKVNotiMessage}  中会包含此字段，最大长度 2 kb</param>
        public abstract void SetChatRoomEntry(string roomId, string key, string value, bool sendNotification,
            bool autoRemove, string notificationExtra, OperationCallback callback);

        ///// <summary>
        ///// 批量设置聊天室自定义属性
        ///// </summary>
        ///// <param name="roomId">聊天室 id</param>
        ///// <param name="chatRoomEntryDic">聊天室属性: 
        ///// 1. chatRoomEntryMap集合大小最大限制为{@code KV_MAX_NUMBER_LIMIT},超过限制返回错误码{@link io.rong.imlib.IRongCoreEnum.CoreErrorCode#KV_STORE_OUT_OF_LIMIT}
        ///// 2. key 支持大小写英文字母、数字、部分特殊符号 + = - _ 的组合方式，最大长度 128 个字符，value 聊天室属性对应的值，最大长度 4096 个字符
        ///// </param>
        ///// <param name="autoRemove">用户掉线或退出时，是否自动删除该 Key、Value 值</param>
        ///// <param name="overwrite">是否强制覆盖</param>
        ///// <param name="callback"></param>
        //public abstract void SetChatRoomEntries(string roomId, IDictionary<string, string> chatRoomEntryDic, bool autoRemove, bool overwrite, OperationCallbackWithResult<IDictionary<String, RCErrorCode>> callback);

        /// <summary>
        /// 强制设置聊天室自定义属性
        /// </summary>
        /// <param name="roomId">聊天室 id</param>
        /// <param name="key">聊天室属性名称， Key 支持大小写英文字母、数字、部分特殊符号 + = - _ 的组合方式，最大长度 128 个字符。</param>
        /// <param name="value">聊天室属性对应的值，最大长度 4096 个字符。</param>
        /// <param name="sendNotification">是否需要发送通知，如果需要发送通知，SDK 接收通知消息</param>
        /// <param name="autoRemove">退出后是否删除</param>
        /// <param name="notificationExtra">通知自定义字段，通知消息 {@link ChatRoomKVNotiMessage}  中会包含此字段，最大长度 2 kb。</param>
        /// <param name="callback"></param>
        public abstract void ForceSetChatRoomEntry(string roomId, string key, string value, bool sendNotification,
            bool autoRemove, string notificationExtra, OperationCallback callback);

        /// <summary>
        /// 移除 KV
        /// </summary>
        /// <param name="roomId">房间编号</param>
        /// <param name="key">键</param>
        /// <param name="sendNotification">是否发送通知</param>
        /// <param name="notificationExtra">额外信息</param>
        public abstract void RemoveChatRoomEntry(string roomId, string key, bool sendNotification,
            string notificationExtra, OperationCallback callback);

        //public abstract void RemoveChatRoomEntries(string roomId, IList<string> entryKeyList, Boolean force, OperationCallbackWithResult<IDictionary<String, RCErrorCode>> callback);

        /// <summary>
        /// 强制移除 KV
        /// </summary>
        /// <param name="roomId">房间编号</param>
        /// <param name="key">键</param>
        /// <param name="sendNotification">是否发送通知</param>
        /// <param name="notificationExtra">额外信息</param>
        public abstract void ForceRemoveChatRoomEntry(string roomId, string key, bool sendNotification,
            string notificationExtra, OperationCallback callback);

        /// <summary>
        /// 获取 KV
        /// </summary>
        /// <param name="roomId">房间编号</param>
        /// <param name="key">键</param>
        public abstract void GetChatRoomEntry(string roomId, string key,
            OperationCallbackWithResult<IDictionary<string, string>> callback);

        /// <summary>
        /// 获取全部 KV
        /// </summary>
        /// <param name="roomId">房间编号</param>
        public abstract void GetChatRoomAllEntries(string roomId,
            OperationCallbackWithResult<IDictionary<string, string>> callback);

        public abstract void AddToBlackList(string userId, OperationCallback callback);
        public abstract void RemoveFromBlackList(string userId, OperationCallback callback);
        public abstract void GetBlackList(OperationCallbackWithResult<IList<string>> callback);
        public abstract void GetBlackListStatus(string userId, OperationCallbackWithResult<RCBlackListStatus> callback);

        /// <summary>
        /// 获取消息通知免打扰时间
        /// </summary>
        /// <param name="callback"></param>
        public abstract void GetNotificationQuietHours(
            OperationCallbackWithResult<RCNotificationQuietHourInfo> callback);

        /// <summary>
        /// 设置消息通知免打扰时间
        /// </summary>
        /// <param name="startTime">起始时间 格式 HH:MM:SS</param>
        /// <param name="spanMinutes">设置的免打扰结束时间距离起始时间的间隔分钟数。 0 &lt; spanMinutes &lt; 1440。比如，您设置的起始时间是 00：00， 结束时间为 23：59，则 spanMinutes 为 23 * 60 + 59 = 1339 分钟。</param>
        /// <param name="callback"></param>
        public abstract void SetNotificationQuietHours(string startTime, int spanMinutes, OperationCallback callback);

        /// <summary>
        /// 移除消息通知免打扰时间
        /// </summary>
        /// <param name="callback"></param>
        public abstract void RemoveNotificationQuietHours(OperationCallback callback);

#if UNITY_IOS
        public abstract void setDeviceToken(string deviceToken);
#endif
        internal void NotifyOnConnectionStatusChanged(RCConnectionStatus connStatus)
        {
            Debug.Log(string.Format("NotifyOnConnectionStatusChanged called,{0}", connStatus));
            OnConnectionStatusChanged?.DynamicInvoke(connStatus);
        }

        internal void NotifyOnSendMessageAttached(RCMessage msg)
        {
            Debug.Log(string.Format("NotifyOnSendMessageAttached called,{0}", msg));
            OnSendMessageAttached?.DynamicInvoke(msg);
        }

        internal void NotifyOnSendMessageSucceed(RCMessage msg)
        {
            Debug.Log(string.Format("NotifyOnSendMessageSucceed called,{0}", msg));
            OnSendMessageSucceed?.DynamicInvoke(msg);
        }

        internal void NotifyOnSendMessageFailed(RCMessage msg, RCErrorCode errorCode)
        {
            Debug.Log($"NotifyOnSendMessageFailed called,{errorCode} code {msg}");
            OnSendMessageFailed?.DynamicInvoke(errorCode, msg);
        }

        internal void NotifyOnSendMediaMessageProgress(RCMessage msg, int progress)
        {
            OnSendMediaMessageProgress?.DynamicInvoke(msg, progress);
        }

        internal void NotifyOnSendMediaMessageCanceled(RCMessage msg)
        {
            OnSendMediaMessageCanceled?.DynamicInvoke(msg);
        }

        internal void NotifyOnMessageReceived(RCMessage msg, int left)
        {
            Debug.Log(string.Format("NotifyOnMessageReceived called,{0}", msg));
            OnMessageReceived?.DynamicInvoke(msg, left);
        }

        internal void NotifyDownloadMediaMessageProgressed(RCMessage msg, int progress)
        {
            OnDownloadMediaMessageProgressed?.DynamicInvoke(msg, progress);
        }

#if UNITY_ANDROID
        internal void NotifyOnDownloadMediaMessagePaused(RCErrorCode code, RCMessage message)
        {
            OnDownloadMediaMessagePaused?.DynamicInvoke(code, message);
        }
#endif

        internal void NotifyDownloadMediaMessageCompleted(RCMessage msg)
        {
            OnDownloadMediaMessageCompleted?.DynamicInvoke(msg);
        }

        internal void NotifyDownloadMediaMessageFailed(RCErrorCode code, RCMessage msg)
        {
            OnDownloadMediaMessageFailed?.DynamicInvoke(code, msg);
        }

        internal void NotifyDownloadMediaMessageCanceled(RCMessage msg)
        {
            OnDownloadMediaMessageCanceled?.DynamicInvoke(msg);
        }

        internal void NotifyReadReceiptReceived(RCConversationType type, String targetId, String senderId, Int64 lastMsgSentTime)
        {
            OnReadReceiptReceived?.DynamicInvoke(type, targetId, senderId, lastMsgSentTime);
        }

        internal void NotifyReadReceiptRequest(RCConversationType type, string targetId, string messageUid)
        {
            if (false == string.IsNullOrEmpty(targetId) && false == string.IsNullOrEmpty(messageUid))
            {
                OnReadReceiptRequest?.DynamicInvoke(type, targetId, messageUid);
            }
        }

        internal void NotifyReadReceiptResponse(RCConversationType type, string targetId, string messageUid,
            IDictionary<string, Int64> respondUserIdList)
        {
            if (false == string.IsNullOrEmpty(targetId) && false == string.IsNullOrEmpty(messageUid) &&
                (null != respondUserIdList && respondUserIdList.Count > 0))
            {
                OnReadReceiptResponse?.DynamicInvoke(type, targetId, messageUid, respondUserIdList);
            }
        }

        internal void NotifyMessageExpansionUpdate(IDictionary<string, string> expansionChanged, RCMessage message)
        {
            OnMessageExpansionUpdated?.DynamicInvoke(expansionChanged, message);
        }

        internal void NotifyMessageExpansionRemoved(IList<string> keyList, RCMessage message)
        {
            OnMessageExpansionRemoved?.DynamicInvoke(keyList, message);
        }

        internal void NotifyMessageBlocked(BlockedMessageInfo blockedMessageInfo)
        {
            if (blockedMessageInfo == null)
                return;
            //OnMessageBlocked?.DynamicInvoke(blockedMessageInfo);
        }

        internal void NotifyTypingStatusChanged(RCConversationType conversationType, string targetId,
            List<RCTypingStatus> typeStatusList)
        {
            OnTypingStatusChanged?.DynamicInvoke(conversationType, targetId, typeStatusList);
        }

        internal void NotifyOnTagChanged()
        {
            OnTagChanged?.DynamicInvoke();
        }

        internal void NotifyOnConversationTagChanged()
        {
            OnConversationTagChanged?.DynamicInvoke();
        }


        internal void NotifyOnChatRoomJoining(string roomId)
        {
            OnChatRoomJoining?.DynamicInvoke(roomId);
        }
        internal void NotifyOnChatRoomJoined(string roomId)
        {
            OnChatRoomJoined?.DynamicInvoke(roomId);
        }
        internal void NotifyOnChatRoomReset(string roomId)
        {
            OnChatRoomReset?.DynamicInvoke(roomId);
        }
        internal void NotifyOnChatRoomQuited(string roomId)
        {
            OnChatRoomQuited?.DynamicInvoke(roomId);
        }
        internal void NotifyOnChatRoomDestroyed(string roomId, RCChatRoomDestroyType destroyType)
        {
            OnChatRoomDestroyed?.DynamicInvoke(roomId, destroyType);
        }
        internal void NotifyOnChatRoomError(string roomId, RCErrorCode error)
        {
            OnChatRoomError?.DynamicInvoke(roomId, error);
        }

        internal void NotifyOnChatRoomKVSync(string roomId)
        {
            OnChatRoomKVSync?.DynamicInvoke(roomId);
        }

        internal void NotifyOnChatRoomKVUpdate(string roomId, IDictionary<string, string> entries)
        {
            OnChatRoomKVUpdate?.DynamicInvoke(roomId, entries);
        }

        internal void NotifyOnChatRoomKVRemove(string roomId, IDictionary<string, string> entries)
        {
            OnChatRoomKVRemove?.DynamicInvoke(roomId, entries);
        }

        internal void NotifyOnMessageRecalled(RCMessage message, RCRecallNotificationMessage recallNotify)
        {
            OnMessageRecalled?.DynamicInvoke(message, recallNotify);
        }
    }
}