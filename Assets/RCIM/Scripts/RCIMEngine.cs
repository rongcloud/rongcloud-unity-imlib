using System.Collections.Generic;
using System;
using UnityEngine;

namespace cn_rongcloud_im_unity
{
    public abstract class RCIMEngine
    {
        internal static RCIMEngine instance = null;
    
        private static readonly object syncObj = new object();
    
        /// <summary>
        /// 创建接口引擎
        /// </summary>
        /// <param name="appKey">开发者后台获取的 AppKey</param>
        /// <param name="options">引擎配置项</param>
        /// <returns></returns>
        public static RCIMEngine Create(string appKey, RCIMEngineOptions options = null)
        {
            if (instance == null)
            {
                lock (syncObj)
                {
                    if (null == instance)
                    {
#if UNITY_IOS
                        instance = new RCIMIOSEngine(appKey, options);
#elif UNITY_ANDROID
                        instance = new RCIMAndroidEngine(appKey, options);
#else
                        throw new Exception();
#endif
                    }
                }
            }
            return instance;
        }
    
        protected RCIMEngine()
        {
        }
    
#if UNITY_ANDROID
        /// <summary>
        /// 是否开启推送。该方法仅支持android且必须在create方法之前调用
        /// </summary>
        /// <param name="enable">true开启,false不开启。</param>
        public static void EnablePush(bool enable)
        {
            RCUnityLogger.getInstance().log("enablePush", $"enable={enable}");
            RCIMAndroidEngine.EnablePush(enable);
        }
    
        /// <summary>
        /// 是否禁用单进程模式。该方法仅支持android且必须在create方法之前调用
        /// </summary>
        /// <param name="disable">true禁用,false不禁用。</param>
        public static void DisableIPC(bool disable)
        {
            RCUnityLogger.getInstance().log("disableIPC", $"disable={disable}");
            RCIMAndroidEngine.DisableIPC(disable);
        }
#endif
        /// <summary>
        /// 销毁引擎
        /// </summary>
        public virtual void Destroy()
        {
            lock (syncObj)
            {
                instance = null;
            }
        }
    
#if UNITY_IOS
        /// <summary>
        /// 设置推送token，仅ios可用
        /// </summary>
        /// <param name="deviceToken">推送token</param>
        public abstract void SetDeviceToken(String deviceToken);
#endif
    
        /// <summary>
        /// 连接融云服务器，在整个应用程序全局，只需要调用一次。调用此接口返回非业务错误码时，SDK
        /// 会启动重连机制进行重连；如果仍没有连接成功，会在设备网络状态变化时再次进行重连
        /// </summary>
        /// <param name="token">调用 server api 获取到的 token</param>
        /// <param name="timeout">
        /// 连接超时时间，单位：秒。timeLimit <= 0，则 IM 将一直连接，直到连接成功或者无法连接（如 token 非法）。timeLimit >
        /// 0，则 IM 将最多连接 timeLimit 秒。如果在 timeLimit 秒内连接成功，后面再发生了网络变化或前后台切换，SDK
        /// 会自动重连； 如果在 timeLimit 秒无法连接成功则不再进行重连，通过 listener 告知连接超时，您需要再自行调用 connect
        /// 接口
        /// </param>
        /// <param name="callback">
        /// 链接事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onConnected">接口回调可以监听</seealso>
        public abstract int Connect(string token, int timeout, RCIMConnectListener callback = null);
    
        /// <summary>
        /// 断开链接。注：因为 SDK 在前后台切换或者网络出现异常都会自动重连，保证连接可靠性。 所以除非您的 App
        /// 逻辑需要登出，否则一般不需要调用此方法进行手动断开
        /// </summary>
        /// <param name="receivePush">
        /// 退出后是否接收 push，true:断开后接收远程推送，false:断开后不再接收远程推送
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        public abstract int Disconnect(bool receivePush);
    
        /// <summary>
        /// 构建文本消息
        /// </summary>
        /// <param name="type">会话类型，</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="text">文本内容</param>
        /// <returns>文本消息实体</returns>
        public abstract RCIMTextMessage CreateTextMessage(RCIMConversationType type, string targetId, string channelId,
                                                          string text);
    
        /// <summary>
        /// 构建图片消息
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="path">图片消息的本地路径，必须为有效路径</param>
        /// <returns>图片消息实体</returns>
        public abstract RCIMImageMessage CreateImageMessage(RCIMConversationType type, string targetId, string channelId,
                                                            string path);
    
        /// <summary>
        /// 构建文件消息
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="path">文件消息的本地路径，必须为有效路径</param>
        /// <returns>文件消息实体</returns>
        public abstract RCIMFileMessage CreateFileMessage(RCIMConversationType type, string targetId, string channelId,
                                                          string path);
    
        /// <summary>
        /// 构建小视频消息
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="path">小视频消息的本地路径，必须为有效路径</param>
        /// <param name="duration">小视频消息的视频时长</param>
        /// <returns>视频消息实体</returns>
        public abstract RCIMSightMessage CreateSightMessage(RCIMConversationType type, string targetId, string channelId,
                                                            string path, int duration);
    
        /// <summary>
        /// 构建语音消息 (高清语音)
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="path">语音消息的本地路径，必须为有效路径</param>
        /// <param name="duration">语音消息的消息时长</param>
        /// <returns>语音消息的实体</returns>
        public abstract RCIMVoiceMessage CreateVoiceMessage(RCIMConversationType type, string targetId, string channelId,
                                                            string path, int duration);
    
        /// <summary>
        /// 构建引用消息
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="referenceMessage">引用的消息</param>
        /// <param name="text">引用的文本内容</param>
        /// <returns>引用消息实体</returns>
        public abstract RCIMReferenceMessage CreateReferenceMessage(RCIMConversationType type, string targetId,
                                                                    string channelId, RCIMMessage referenceMessage,
                                                                    string text);
    
        /// <summary>
        /// 构建 GIF 消息
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="path">GIF 消息的本地路径</param>
        /// <returns>GIF 消息实体</returns>
        public abstract RCIMGIFMessage CreateGIFMessage(RCIMConversationType type, string targetId, string channelId,
                                                        string path);
    
        /// <summary>
        /// 构建自定义消息
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="policy">消息的存储策略</param>
        /// <param name="messageIdentifier">消息的标识符，需唯一</param>
        /// <param name="fields">消息的内容键值对</param>
        /// <returns>自定义消息实体</returns>
        public abstract RCIMCustomMessage CreateCustomMessage(RCIMConversationType type, string targetId, string channelId,
                                                              RCIMCustomMessagePolicy policy, string messageIdentifier,
                                                              Dictionary<string, string> fields);
    
        /// <summary>
        /// 构建位置消息
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        /// <param name="poiName">POI 信息</param>
        /// <param name="thumbnailPath">缩略图本地路径，必须为有效路径</param>
        /// <returns>位置消息实体</returns>
        public abstract RCIMLocationMessage CreateLocationMessage(RCIMConversationType type, string targetId,
                                                                  string channelId, double longitude, double latitude,
                                                                  string poiName, string thumbnailPath);
    
        /// <summary>
        /// 发送普通消息
        /// </summary>
        /// <param name="message">发送的消息实体</param>
        /// <param name="callback">
        /// 发送消息的事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0
        /// 版本废弃该接口的其他回调方式。如果传入了 callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onMessageAttached">接口回调可以监听</seealso>
        /// <see cref="onMessageSent"/>
        public abstract int SendMessage(RCIMMessage message, RCIMSendMessageListener callback = null);
    
        /// <summary>
        /// 发送媒体消息
        /// </summary>
        /// <param name="message">发送的媒体消息实体</param>
        /// <param name="listener">发送媒体消息的事件监听</param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onMediaMessageSending">接口回调可以监听</seealso>
        /// <see cref="onMediaMessageAttached"/>
        /// <see cref="onMediaMessageSent"/>
        public abstract int SendMediaMessage(RCIMMediaMessage message, RCIMSendMediaMessageListener listener = null);
    
        /// <summary>
        /// 取消发送媒体消息
        /// </summary>
        /// <param name="message">需要取消发送的媒体消息实体</param>
        /// <param name="callback">
        /// 取消发送媒体消息的事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0
        /// 版本废弃该接口的其他回调方式。如果传入了 callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onSendingMediaMessageCanceled">接口回调可以监听</seealso>
        public abstract int CancelSendingMediaMessage(RCIMMediaMessage message,
                                                      OnCancelSendingMediaMessageCalledAction callback = null);
    
        /// <summary>
        /// 下载媒体消息
        /// </summary>
        /// <param name="message">需要下载的媒体消息实体</param>
        /// <param name="listener">下载媒体消息的事件监听</param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onMediaMessageDownloaded"></seealso>
        /// <see cref="onMediaMessageDownloading"/>
        public abstract int DownloadMediaMessage(RCIMMediaMessage message,
                                                 RCIMDownloadMediaMessageListener listener = null);
    
        /// <summary>
        /// 取消下载媒体消息
        /// </summary>
        /// <param name="message">需要取消下载的媒体消息实体</param>
        /// <param name="callback">
        /// 取消下载媒体消息的事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0
        /// 版本废弃该接口的其他回调方式。如果传入了 callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onDownloadingMediaMessageCanceled"></seealso>
        public abstract int CancelDownloadingMediaMessage(RCIMMediaMessage message,
                                                          OnCancelDownloadingMediaMessageCalledAction callback = null);
    
        /// <summary>
        /// 加载某个会话
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onConversationLoaded"></seealso>
        public abstract int LoadConversation(RCIMConversationType type, string targetId, string channelId);
    
        /// <summary>
        /// 获取某个会话
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="callback">
        /// 获取会话事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onConversationLoaded"></seealso>
        public abstract int GetConversation(RCIMConversationType type, string targetId, string channelId,
                                            RCIMGetConversationListener callback = null);
    
        /// <summary>
        /// 加载某些会话
        /// </summary>
        /// <param name="conversationTypes">会话类型</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="startTime">
        /// 时间戳（毫秒），获取小于此时间戳的会话，传 0 为查询最新数据
        /// </param>
        /// <param name="count">查询的数量， 0 < count <= 50</param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onConversationsLoaded"></seealso>
        public abstract int LoadConversations(List<RCIMConversationType> conversationTypes, string channelId,
                                              long startTime, int count);
    
        /// <summary>
        /// 获取某些会话
        /// </summary>
        /// <param name="conversationTypes">会话类型</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="startTime">
        /// 时间戳（毫秒），获取小于此时间戳的会话，传 0 为查询最新数据
        /// </param>
        /// <param name="count">查询的数量， 0 < count <= 50</param>
        /// <param name="callback">
        /// 获取会话列表事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0
        /// 版本废弃该接口的其他回调方式。如果传入了 callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onConversationsLoaded"></seealso>
        public abstract int GetConversations(List<RCIMConversationType> conversationTypes, string channelId, long startTime,
                                             int count, RCIMGetConversationsListener callback = null);
    
        /// <summary>
        /// 移除某个会话
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="callback">
        /// 移除会话事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onConversationRemoved"></seealso>
        public abstract int RemoveConversation(RCIMConversationType type, string targetId, string channelId,
                                               OnConversationRemovedAction callback = null);
    
        /// <summary>
        /// 根据会话类型移除会话
        /// </summary>
        /// <param name="conversationTypes">会话类型集合</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="callback">
        /// 移除会话列表事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0
        /// 版本废弃该接口的其他回调方式。如果传入了 callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onConversationsRemoved"></seealso>
        public abstract int RemoveConversations(List<RCIMConversationType> conversationTypes, string channelId,
                                                OnConversationsRemovedAction callback = null);
    
        /// <summary>
        /// 加载某个会话的未读数。注：不支持聊天室！
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUnreadCountLoaded"></seealso>
        public abstract int LoadUnreadCount(RCIMConversationType type, string targetId, string channelId);
    
        /// <summary>
        /// 获取某个会话的未读数。注：不支持聊天室！
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="callback">
        /// 获取会话未读数事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0
        /// 版本废弃该接口的其他回调方式。如果传入了 callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUnreadCountLoaded"></seealso>
        public abstract int GetUnreadCount(RCIMConversationType type, string targetId, string channelId,
                                           RCIMGetUnreadCountListener callback = null);
    
        /// <summary>
        /// 加载所有未读数
        /// </summary>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onTotalUnreadCountLoaded"></seealso>
        public abstract int LoadTotalUnreadCount(string channelId);
    
        /// <summary>
        /// 获取所有未读数
        /// </summary>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="callback">
        /// 获取所有未读数事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0
        /// 版本废弃该接口的其他回调方式。如果传入了 callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onTotalUnreadCountLoaded"></seealso>
        public abstract int GetTotalUnreadCount(string channelId, RCIMGetTotalUnreadCountListener callback = null);
    
        /// <summary>
        /// 加载会话中未读的 @ 消息数量。注：不支持聊天室！
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUnreadMentionedCountLoaded"></seealso>
        public abstract int LoadUnreadMentionedCount(RCIMConversationType type, string targetId, string channelId);
    
        /// <summary>
        /// 获取会话中未读的 @ 消息数量。注：不支持聊天室！
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="callback">
        /// 获取会话中未读的 @ 消息数量事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0
        /// 版本废弃该接口的其他回调方式。如果传入了 callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUnreadMentionedCountLoaded"></seealso>
        public abstract int GetUnreadMentionedCount(RCIMConversationType type, string targetId, string channelId,
                                                    RCIMGetUnreadMentionedCountListener callback = null);
    
        /// <summary>
        /// 加载当前用户加入的所有超级群会话的未读消息数的总和
        /// </summary>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUltraGroupAllUnreadCountLoaded"></seealso>
        public abstract int LoadUltraGroupAllUnreadCount();
    
        /// <summary>
        /// 获取当前用户加入的所有超级群会话的未读消息数的总和
        /// </summary>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUltraGroupAllUnreadCountLoaded"></seealso>
        public abstract int GetUltraGroupAllUnreadCount(RCIMGetUltraGroupAllUnreadCountListener callback = null);
    
        /// <summary>
        /// 加载当前用户加入的所有超级群会话中的未读 @ 消息数的总和
        /// </summary>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUltraGroupAllUnreadMentionedCountLoaded"></seealso>
        public abstract int LoadUltraGroupAllUnreadMentionedCount();
    
        /// <summary>
        /// 获取当前用户加入的所有超级群会话中的未读 @ 消息数的总和
        /// </summary>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUltraGroupAllUnreadMentionedCountLoaded"></seealso>
        public abstract int GetUltraGroupAllUnreadMentionedCount(
            RCIMGetUltraGroupAllUnreadMentionedCountListener callback = null);
    
        /// <summary>
        /// 获取指定会话的未读消息数
        /// </summary>
        /// <param name="targetId">会话 ID</param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUltraGroupUnreadCountLoaded"></seealso>
        public abstract int LoadUltraGroupUnreadCount(string targetId);
    
        /// <summary>
        /// 获取指定会话的未读消息数
        /// </summary>
        /// <param name="targetId">会话 ID</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUltraGroupUnreadCountLoaded"></seealso>
        public abstract int GetUltraGroupUnreadCount(string targetId, RCIMGetUltraGroupUnreadCountListener callback = null);
    
        /// <summary>
        /// 获取超级群会话中被 @ 的消息数
        /// </summary>
        /// <param name="targetId">会话 ID</param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUltraGroupUnreadMentionedCountLoaded"></seealso>
        public abstract int LoadUltraGroupUnreadMentionedCount(string targetId);
    
        /// <summary>
        /// 获取超级群会话中被 @ 的消息数
        /// </summary>
        /// <param name="targetId">会话 ID</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUltraGroupUnreadMentionedCountLoaded"></seealso>
        public abstract int GetUltraGroupUnreadMentionedCount(
            string targetId, RCIMGetUltraGroupUnreadMentionedCountListener callback = null);
    
        /// <summary>
        /// 根据会话类型加载未读数。注：不支持聊天室！
        /// </summary>
        /// <param name="conversationTypes">会话类型集合</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="contain">是否包含免打扰消息的未读消息数</param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUnreadCountByConversationTypesLoaded"></seealso>
        public abstract int LoadUnreadCountByConversationTypes(List<RCIMConversationType> conversationTypes,
                                                               string channelId, bool contain);
    
        /// <summary>
        /// 根据会话类型加载未读数。注：不支持聊天室！
        /// </summary>
        /// <param name="conversationTypes">会话类型集合</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="contain">是否包含免打扰消息的未读消息数</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUnreadCountByConversationTypesLoaded"></seealso>
        public abstract int GetUnreadCountByConversationTypes(
            List<RCIMConversationType> conversationTypes, string channelId, bool contain,
            RCIMGetUnreadCountByConversationTypesListener callback = null);
    
        /// <summary>
        /// 清除某个会话中的未读消息数。注：不支持聊天室！
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="timestamp">
        /// 该会话已阅读的最后一条消息的发送时间戳，清除所有传入当前最新时间戳
        /// </param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUnreadCountCleared"></seealso>
        public abstract int ClearUnreadCount(RCIMConversationType type, string targetId, string channelId, long timestamp,
                                             OnUnreadCountClearedAction callback = null);
    
        /// <summary>
        /// 保存会话草稿信息
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="draft">草稿的文字内容</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onDraftMessageSaved"></seealso>
        public abstract int SaveDraftMessage(RCIMConversationType type, string targetId, string channelId, string draft,
                                             OnDraftMessageSavedAction callback = null);
    
        /// <summary>
        /// 加载会话中的草稿信息
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onDraftMessageLoaded"></seealso>
        public abstract int LoadDraftMessage(RCIMConversationType type, string targetId, string channelId);
    
        /// <summary>
        /// 获取会话中的草稿信息
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onDraftMessageLoaded"></seealso>
        public abstract int GetDraftMessage(RCIMConversationType type, string targetId, string channelId,
                                            RCIMGetDraftMessageListener callback = null);
    
        /// <summary>
        /// 删除指定会话中的草稿信息
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onDraftMessageCleared"></seealso>
        public abstract int ClearDraftMessage(RCIMConversationType type, string targetId, string channelId,
                                              OnDraftMessageClearedAction callback = null);
    
        /// <summary>
        /// 加载免打扰的会话列表
        /// </summary>
        /// <param name="conversationTypes">会话类型集合</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onBlockedConversationsLoaded"></seealso>
        public abstract int LoadBlockedConversations(List<RCIMConversationType> conversationTypes, string channelId);
    
        /// <summary>
        /// 获取免打扰的会话列表
        /// </summary>
        /// <param name="conversationTypes">会话类型集合</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onBlockedConversationsLoaded"></seealso>
        public abstract int GetBlockedConversations(List<RCIMConversationType> conversationTypes, string channelId,
                                                    RCIMGetBlockedConversationsListener callback = null);
    
        /// <summary>
        /// 设置会话的置顶状态。若会话不存在，调用此方法 SDK 自动创建会话并置顶
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="top">是否置顶</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onConversationTopStatusChanged"></seealso>
        public abstract int ChangeConversationTopStatus(RCIMConversationType type, string targetId, string channelId,
                                                        bool top, OnConversationTopStatusChangedAction callback = null);
    
        /// <summary>
        /// 加载会话的置顶状态
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onConversationTopStatusLoaded"></seealso>
        public abstract int LoadConversationTopStatus(RCIMConversationType type, string targetId, string channelId);
    
        /// <summary>
        /// 获取会话的置顶状态
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onConversationTopStatusLoaded"></seealso>
        public abstract int GetConversationTopStatus(RCIMConversationType type, string targetId, string channelId,
                                                     RCIMGetConversationTopStatusListener callback = null);
    
        /// <summary>
        /// 同步会话阅读状态
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="timestamp">会话中已读的最后一条消息的发送时间戳</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onConversationReadStatusSynced"></seealso>
        public abstract int SyncConversationReadStatus(RCIMConversationType type, string targetId, string channelId,
                                                       long timestamp,
                                                       OnConversationReadStatusSyncedAction callback = null);
    
        /// <summary>
        /// 向会话中发送正在输入的状态，目前只支持单聊
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="currentType">当前的状态</param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        public abstract int SendTypingStatus(RCIMConversationType type, string targetId, string channelId,
                                             string currentType);
    
        /// <summary>
        /// 加载历史消息
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="sentTime">当前消息时间戳</param>
        /// <param name="order">
        /// 获取消息的方向。BEFORE：获取 sentTime 之前的消息 （时间递减），AFTER：获取 sentTime 之后的消息 （时间递增）
        /// </param>
        /// <param name="policy">
        /// 消息的加载策略。LOCAL：只加载本地，REMOTE：只加载远端，LOCAL_REMOTE：本地远端都加载
        /// </param>
        /// <param name="count">获取的消息数量，0 < count <= 20</param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onMessagesLoaded"></seealso>
        public abstract int LoadMessages(RCIMConversationType type, string targetId, string channelId, long sentTime,
                                         RCIMTimeOrder order, RCIMMessageOperationPolicy policy, int count);
    
        /// <summary>
        /// 加载历史消息
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="sentTime">当前消息时间戳</param>
        /// <param name="order">
        /// 获取消息的方向。BEFORE：获取 sentTime 之前的消息 （时间递减），AFTER：获取 sentTime 之后的消息 （时间递增）
        /// </param>
        /// <param name="policy">
        /// 消息的加载策略。LOCAL：只加载本地，REMOTE：只加载远端，LOCAL_REMOTE：本地远端都加载
        /// </param>
        /// <param name="count">获取的消息数量，0 < count <= 20</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onMessagesLoaded"></seealso>
        public abstract int GetMessages(RCIMConversationType type, string targetId, string channelId, long sentTime,
                                        RCIMTimeOrder order, RCIMMessageOperationPolicy policy, int count,
                                        RCIMGetMessagesListener callback = null);
    
        /// <summary>
        /// 根据消息 id 获取消息体（本地数据库索引唯一值）
        /// </summary>
        /// <param name="messageId">消息的 messageId，可在消息对象中获取</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        public abstract int GetMessageById(int messageId, RCIMGetMessageListener callback = null);
    
        /// <summary>
        /// 通过全局唯一 id 获取消息实体
        /// </summary>
        /// <param name="messageUId">
        /// 消息的 messageUid，可在消息对象中获取，且只有发送成功的消息才会有值
        /// </param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        public abstract int GetMessageByUId(string messageUId, RCIMGetMessageListener callback = null);
    
        /// <summary>
        /// 加载第一条未读消息
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onFirstUnreadMessageLoaded"></seealso>
        public abstract int LoadFirstUnreadMessage(RCIMConversationType type, string targetId, string channelId);
    
        /// <summary>
        /// 获取第一条未读消息
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onFirstUnreadMessageLoaded"></seealso>
        public abstract int GetFirstUnreadMessage(RCIMConversationType type, string targetId, string channelId,
                                                  RCIMGetFirstUnreadMessageListener callback = null);
    
        /// <summary>
        /// 加载会话中未读的 @ 消息
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUnreadMentionedMessagesLoaded"></seealso>
        public abstract int LoadUnreadMentionedMessages(RCIMConversationType type, string targetId, string channelId);
    
        /// <summary>
        /// 获取会话中未读的 @ 消息
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUnreadMentionedMessagesLoaded"></seealso>
        public abstract int GetUnreadMentionedMessages(RCIMConversationType type, string targetId, string channelId,
                                                       RCIMGetUnreadMentionedMessagesListener callback = null);
    
        /// <summary>
        /// 插入一条消息
        /// </summary>
        /// <param name="message">插入的消息</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onMessageInserted"></seealso>
        public abstract int InsertMessage(RCIMMessage message, OnMessageInsertedAction callback = null);
    
        /// <summary>
        /// 插入多条消息，不支持超级群
        /// </summary>
        /// <param name="messages">插入的消息集合</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onMessagesInserted"></seealso>
        public abstract int InsertMessages(List<RCIMMessage> messages, OnMessagesInsertedAction callback = null);
    
        /// <summary>
        /// 清除消息
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="timestamp">
        /// 清除消息截止时间戳，0 <= recordTime <= 当前会话最后一条消息的 sentTime, 0 清除所有消息，其他值清除小于等于
        /// recordTime 的消息
        /// </param>
        /// <param name="policy">清除的策略</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onMessagesCleared"></seealso>
        public abstract int ClearMessages(RCIMConversationType type, string targetId, string channelId, long timestamp,
                                          RCIMMessageOperationPolicy policy, OnMessagesClearedAction callback = null);
    
        /// <summary>
        /// 删除本地消息
        /// </summary>
        /// <param name="messages">消息集合</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onLocalMessagesDeleted"></seealso>
        public abstract int DeleteLocalMessages(List<RCIMMessage> messages, OnLocalMessagesDeletedAction callback = null);
    
        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="messages">消息集合</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onMessagesDeleted"></seealso>
        public abstract int DeleteMessages(RCIMConversationType type, string targetId, string channelId,
                                           List<RCIMMessage> messages, OnMessagesDeletedAction callback = null);
    
        /// <summary>
        /// 撤回消息
        /// </summary>
        /// <param name="message">需要被撤回的消息</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onMessageRecalled"></seealso>
        public abstract int RecallMessage(RCIMMessage message, OnMessageRecalledAction callback = null);
    
        /// <summary>
        /// 发送某个会话中的消息阅读回执
        /// </summary>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="timestamp">该会话中已读的最后一条消息的发送时间戳</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onPrivateReadReceiptMessageSent"></seealso>
        public abstract int SendPrivateReadReceiptMessage(string targetId, string channelId, long timestamp,
                                                          OnPrivateReadReceiptMessageSentAction callback = null);
    
        /// <summary>
        /// 发起群聊消息已读回执请求
        /// </summary>
        /// <param name="message">需要请求已读回执的消息</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onGroupReadReceiptRequestSent"></seealso>
        public abstract int SendGroupReadReceiptRequest(RCIMMessage message,
                                                        OnGroupReadReceiptRequestSentAction callback = null);
    
        /// <summary>
        /// 发送群聊已读回执
        /// </summary>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="messages">会话中需要发送已读回执的消息列表</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onGroupReadReceiptResponseSent"></seealso>
        public abstract int SendGroupReadReceiptResponse(string targetId, string channelId, List<RCIMMessage> messages,
                                                         OnGroupReadReceiptResponseSentAction callback = null);
    
        /// <summary>
        /// 更新消息扩展信息。每条消息携带扩展信息键值对最大值 300个，单次设置扩展信息键值对最大值 20个
        /// </summary>
        /// <param name="messageUId">
        /// 消息的 messageUid，可在消息对象中获取，且只有发送成功的消息才会有值
        /// </param>
        /// <param name="expansion">
        /// 要更新的消息扩展信息键值对，类型是 HashMap；Key 支持大小写英文字母、数字、部分特殊符号 + = - _
        /// 的组合方式，不支持汉字。Value 可以输入空格
        /// </param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onMessageExpansionUpdated"></seealso>
        public abstract int UpdateMessageExpansion(string messageUId, Dictionary<string, string> expansion,
                                                   OnMessageExpansionUpdatedAction callback = null);
    
        /// <summary>
        /// 删除消息扩展信息中特定的键值对
        /// </summary>
        /// <param name="messageUId">
        /// 消息的 messageUid，可在消息对象中获取，且只有发送成功的消息才会有值
        /// </param>
        /// <param name="keys">
        /// 消息扩展信息中待删除的 key 的列表，类型是 ArrayList
        /// </param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onMessageExpansionForKeysRemoved"></seealso>
        public abstract int RemoveMessageExpansionForKeys(string messageUId, List<string> keys,
                                                          OnMessageExpansionForKeysRemovedAction callback = null);
    
        /// <summary>
        /// 设置消息发送状态
        /// </summary>
        /// <param name="messageId">消息的 messageId，可在消息对象中获取</param>
        /// <param name="sentStatus">要修改的状态</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onMessageSentStatusChanged"></seealso>
        public abstract int ChangeMessageSentStatus(int messageId, RCIMSentStatus sentStatus,
                                                    OnMessageSentStatusChangedAction callback = null);
    
        /// <summary>
        /// 设置消息接收状态
        /// </summary>
        /// <param name="messageId">消息的 messageId，可在消息对象中获取</param>
        /// <param name="receivedStatus">要修改的状态</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onMessageReceiveStatusChanged"></seealso>
        public abstract int ChangeMessageReceiveStatus(int messageId, RCIMReceivedStatus receivedStatus,
                                                       OnMessageReceiveStatusChangedAction callback = null);
    
        /// <summary>
        /// 加入聊天室
        /// </summary>
        /// <param name="targetId">聊天室会话 ID</param>
        /// <param name="messageCount">
        /// 进入聊天室拉取消息数目，-1 时不拉取任何消息，0 时拉取 10 条消息，最多只能拉取 50
        /// </param>
        /// <param name="autoCreate">
        /// 是否创建聊天室，TRUE 如果聊天室不存在，sdk 会创建聊天室并加入，如果已存在，则直接加入
        /// </param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onChatRoomJoined"></seealso>
        public abstract int JoinChatRoom(string targetId, int messageCount, bool autoCreate,
                                         OnChatRoomJoinedAction callback = null);
    
        /// <summary>
        /// 退出聊天室
        /// </summary>
        /// <param name="targetId">聊天室会话 ID</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onChatRoomLeft"></seealso>
        public abstract int LeaveChatRoom(string targetId, OnChatRoomLeftAction callback = null);
    
        /// <summary>
        /// 加载聊天室历史消息记录。注：必须先开通聊天室消息云存储功能
        /// </summary>
        /// <param name="targetId">聊天室会话 ID</param>
        /// <param name="timestamp">起始的消息发送时间戳</param>
        /// <param name="order">拉取顺序 0:倒序，1:正序</param>
        /// <param name="count">要获取的消息数量，0 < count <= 50</param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onChatRoomMessagesLoaded"></seealso>
        public abstract int LoadChatRoomMessages(string targetId, long timestamp, RCIMTimeOrder order, int count);
    
        /// <summary>
        /// 获取聊天室历史消息记录。注：必须先开通聊天室消息云存储功能
        /// </summary>
        /// <param name="targetId">聊天室会话 ID</param>
        /// <param name="timestamp">起始的消息发送时间戳</param>
        /// <param name="order">拉取顺序 0:倒序，1:正序</param>
        /// <param name="count">要获取的消息数量，0 < count <= 50</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onChatRoomMessagesLoaded"></seealso>
        public abstract int GetChatRoomMessages(string targetId, long timestamp, RCIMTimeOrder order, int count,
                                                RCIMGetChatRoomMessagesListener callback = null);
    
        /// <summary>
        /// 设置聊天室自定义属性
        /// </summary>
        /// <param name="targetId">聊天室会话 ID</param>
        /// <param name="key">
        /// 聊天室属性名称，Key 支持大小写英文字母、数字、部分特殊符号 + = - _ 的组合方式，最大长度 128 个字符
        /// </param>
        /// <param name="value">聊天室属性对应的值，最大长度 4096 个字符</param>
        /// <param name="deleteWhenLeft">用户掉线或退出时，是否自动删除该 Key、Value 值</param>
        /// <param name="overwrite">如果当前 key 存在，是否进行覆盖</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onChatRoomEntryAdded"></seealso>
        public abstract int AddChatRoomEntry(string targetId, string key, string value, bool deleteWhenLeft, bool overwrite,
                                             OnChatRoomEntryAddedAction callback = null);
    
        /// <summary>
        /// 批量设置聊天室自定义属性
        /// </summary>
        /// <param name="targetId">聊天室会话 ID</param>
        /// <param name="entries">聊天室属性</param>
        /// <param name="deleteWhenLeft">用户掉线或退出时，是否自动删除该 Key、Value 值</param>
        /// <param name="overwrite">是否强制覆盖</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onChatRoomEntriesAdded"></seealso>
        public abstract int AddChatRoomEntries(string targetId, Dictionary<string, string> entries, bool deleteWhenLeft,
                                               bool overwrite, OnChatRoomEntriesAddedAction callback = null);
    
        /// <summary>
        /// 加载聊天室单个属性
        /// </summary>
        /// <param name="targetId">聊天室会话 ID</param>
        /// <param name="key">聊天室属性键值</param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onChatRoomEntryLoaded"></seealso>
        public abstract int LoadChatRoomEntry(string targetId, string key);
    
        /// <summary>
        /// 获取聊天室单个属性
        /// </summary>
        /// <param name="targetId">聊天室会话 ID</param>
        /// <param name="key">聊天室属性键值</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onChatRoomEntryLoaded"></seealso>
        public abstract int GetChatRoomEntry(string targetId, string key, RCIMGetChatRoomEntryListener callback = null);
    
        /// <summary>
        /// 加载聊天室所有属性
        /// </summary>
        /// <param name="targetId">聊天室会话 ID</param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onChatRoomAllEntriesLoaded"></seealso>
        public abstract int LoadChatRoomAllEntries(string targetId);
    
        /// <summary>
        /// 获取聊天室所有属性
        /// </summary>
        /// <param name="targetId">聊天室会话 ID</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onChatRoomAllEntriesLoaded"></seealso>
        public abstract int GetChatRoomAllEntries(string targetId, RCIMGetChatRoomAllEntriesListener callback = null);
    
        /// <summary>
        /// 删除聊天室自定义属性
        /// </summary>
        /// <param name="targetId">聊天室会话 ID</param>
        /// <param name="key">聊天室属性键值</param>
        /// <param name="force">是否强制删除</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onChatRoomEntryRemoved"></seealso>
        public abstract int RemoveChatRoomEntry(string targetId, string key, bool force,
                                                OnChatRoomEntryRemovedAction callback = null);
    
        /// <summary>
        /// 批量删除聊天室自定义属性
        /// </summary>
        /// <param name="targetId">聊天室会话 ID</param>
        /// <param name="keys">聊天室属性</param>
        /// <param name="force">是否强制覆盖</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onChatRoomEntriesRemoved"></seealso>
        public abstract int RemoveChatRoomEntries(string targetId, List<string> keys, bool force,
                                                  OnChatRoomEntriesRemovedAction callback = null);
    
        /// <summary>
        /// 将某个用户加入黑名单。当你把对方加入黑名单后，对方再发消息时，就会提示“已被加入黑名单，消息发送失败”。
        /// 但你依然可以发消息个对方
        /// </summary>
        /// <param name="userId">用户 Id</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onBlacklistAdded"></seealso>
        public abstract int AddToBlacklist(string userId, OnBlacklistAddedAction callback = null);
    
        /// <summary>
        /// 将某个用户从黑名单中移出
        /// </summary>
        /// <param name="userId">用户 Id</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onBlacklistRemoved"></seealso>
        public abstract int RemoveFromBlacklist(string userId, OnBlacklistRemovedAction callback = null);
    
        /// <summary>
        /// 获取某用户是否在黑名单中
        /// </summary>
        /// <param name="userId">用户 Id</param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onBlacklistStatusLoaded"></seealso>
        public abstract int LoadBlacklistStatus(string userId);
    
        /// <summary>
        /// 获取某用户是否在黑名单中
        /// </summary>
        /// <param name="userId">用户 Id</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onBlacklistStatusLoaded"></seealso>
        public abstract int GetBlacklistStatus(string userId, RCIMGetBlacklistStatusListener callback = null);
    
        /// <summary>
        /// 加载当前用户设置的黑名单列表
        /// </summary>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onBlacklistLoaded"></seealso>
        public abstract int LoadBlacklist();
    
        /// <summary>
        /// 获取当前用户设置的黑名单列表
        /// </summary>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onBlacklistLoaded"></seealso>
        public abstract int GetBlacklist(RCIMGetBlacklistListener callback = null);
    
        /// <summary>
        /// 根据关键字搜索指定会话中的消息
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="keyword">搜索的关键字</param>
        /// <param name="startTime">
        /// 查询 beginTime 之前的消息， 传 0 时从最新消息开始搜索，从该时间往前搜索
        /// </param>
        /// <param name="count">查询的数量，0 < count <= 50</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onMessagesSearched"></seealso>
        public abstract int SearchMessages(RCIMConversationType type, string targetId, string channelId, string keyword,
                                           long startTime, int count, RCIMSearchMessagesListener callback = null);
    
        /// <summary>
        /// 根据关键字搜索指定会话中某个时间段的消息
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="keyword">搜索的关键字</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="offset">偏移量</param>
        /// <param name="count">返回的搜索结果数量，0 < count <= 50</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onMessagesSearchedByTimeRange"></seealso>
        public abstract int SearchMessagesByTimeRange(RCIMConversationType type, string targetId, string channelId,
                                                      string keyword, long startTime, long endTime, int offset, int count,
                                                      RCIMSearchMessagesByTimeRangeListener callback = null);
    
        /// <summary>
        /// 根据用户 id 搜索指定会话中的消息
        /// </summary>
        /// <param name="userId">用户 id</param>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="startTime">
        /// 查询记录的起始时间， 传 0 时从最新消息开始搜索，从该时间往前搜索
        /// </param>
        /// <param name="count">返回的搜索结果数量 0 < count <= 50</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onMessagesSearchedByUserId"></seealso>
        public abstract int SearchMessagesByUserId(string userId, RCIMConversationType type, string targetId,
                                                   string channelId, long startTime, int count,
                                                   RCIMSearchMessagesByUserIdListener callback = null);
    
        /// <summary>
        /// 根据关键字搜索会话
        /// </summary>
        /// <param name="conversationTypes">会话类型集合</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="messageTypes">搜索的消息类型</param>
        /// <param name="keyword">搜索的关键字</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onConversationsSearched"></seealso>
        public abstract int SearchConversations(List<RCIMConversationType> conversationTypes, string channelId,
                                                List<RCIMMessageType> messageTypes, string keyword,
                                                RCIMSearchConversationsListener callback = null);
    
        /// <summary>
        /// 屏蔽某个时间段的消息提醒
        /// </summary>
        /// <param name="startTime">开始消息免打扰时间，格式为 HH:MM:SS</param>
        /// <param name="spanMinutes">
        /// 需要消息免打扰分钟数，0 < spanMinutes < 1440（ 比如，您设置的起始时间是 00：00， 结束时间为 01:00，则
        /// spanMinutes 为 60 分钟。设置为 1439 代表全天免打扰 （23  60 + 59 = 1439 ））
        /// </param>
        /// <param name="level">消息通知级别</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onNotificationQuietHoursChanged"></seealso>
        public abstract int ChangeNotificationQuietHours(string startTime, int spanMinutes,
                                                         RCIMPushNotificationQuietHoursLevel level,
                                                         OnNotificationQuietHoursChangedAction callback = null);
    
        /// <summary>
        /// 删除已设置的全局时间段消息提醒屏蔽
        /// </summary>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onNotificationQuietHoursRemoved"></seealso>
        public abstract int RemoveNotificationQuietHours(OnNotificationQuietHoursRemovedAction callback = null);
    
        /// <summary>
        /// 加载已设置的时间段消息提醒屏蔽
        /// </summary>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onNotificationQuietHoursLoaded"></seealso>
        public abstract int LoadNotificationQuietHours();
    
        /// <summary>
        /// 获取已设置的时间段消息提醒屏蔽
        /// </summary>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onNotificationQuietHoursLoaded"></seealso>
        public abstract int GetNotificationQuietHours(RCIMGetNotificationQuietHoursListener callback = null);
    
        /// <summary>
        /// 设置会话的消息提醒状态。注：超级群调用该接口，channelId 为空时，相当于设置了 channelId
        /// 为空的频道的免打扰，不会屏蔽整个超级群会话下所有频道的免打扰
        /// </summary>
        /// <param name="type">
        /// 会话类型。请注意以下限制：<ul><li>*超级群会话类型*：如在 2022.09.01
        /// 之前开通超级群业务，默认不支持为单个超级群会话*所有消息*设置免打扰级别（“所有消息”指所有频道中的消息和不属于任何频道的消息）。该接口仅设置指定超级群会话（`targetId`）中*不属于任何频道的消息*的免打扰状态级别。如需修改请提交工单。</li><li>*聊天室会话类型*：不支持，因为聊天室消息默认不支持消息推送提醒。</li></ul>
        /// </param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">
        /// 超级群的会话频道 ID。其他类型传 null 即可。<ul><li>如果传入频道
        /// ID，则针对该指定频道设置消息免打扰级别。如果不指定频道 ID，则对所有超级群消息生效。</li><li>*注意*：2022.09.01
        /// 之前开通超级群业务的客户，如果不指定频道 ID，则默认传 ""
        /// 空字符串，即仅针对指定超级群会话（`targetId`）中*不属于任何频道的消息*设置免打扰状态级别。如需修改请提交工单。</p></li></ul>
        /// </param>
        /// <param name="level">消息通知级别</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onConversationNotificationLevelChanged"></seealso>
        public abstract int ChangeConversationNotificationLevel(
            RCIMConversationType type, string targetId, string channelId, RCIMPushNotificationLevel level,
            OnConversationNotificationLevelChangedAction callback = null);
    
        /// <summary>
        /// 加载会话的消息提醒状态
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onConversationNotificationLevelLoaded"></seealso>
        public abstract int LoadConversationNotificationLevel(RCIMConversationType type, string targetId, string channelId);
    
        /// <summary>
        /// 获取会话的消息提醒状态
        /// </summary>
        /// <param name="type">
        /// 会话类型。请注意以下限制：<ul><li>*超级群会话类型*：如在 2022.09.01
        /// 之前开通超级群业务，默认不支持为单个超级群会话*所有消息*设置免打扰级别（“所有消息”指所有频道中的消息和不属于任何频道的消息）。该接口仅设置指定超级群会话（`targetId`）中*不属于任何频道的消息*的免打扰状态级别。如需修改请提交工单。</li><li>*聊天室会话类型*：不支持，因为聊天室消息默认不支持消息推送提醒。</li></ul>
        /// </param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">
        /// 超级群的会话频道 ID。其他类型传 null 即可。<ul><li>如果传入频道
        /// ID，则针对该指定频道设置消息免打扰级别。如果不指定频道 ID，则对所有超级群消息生效。</li><li>*注意*：2022.09.01
        /// 之前开通超级群业务的客户，如果不指定频道 ID，则默认传 ""
        /// 空字符串，即仅针对指定超级群会话（`targetId`）中*不属于任何频道的消息*设置免打扰状态级别。如需修改请提交工单。</p></li></ul>
        /// </param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onConversationNotificationLevelLoaded"></seealso>
        public abstract int GetConversationNotificationLevel(RCIMConversationType type, string targetId, string channelId,
                                                             RCIMGetConversationNotificationLevelListener callback = null);
    
        /// <summary>
        /// 设置会话类型的消息提醒状态。注：如要移除消息提醒状态，设置level为RCIMIWPushNotificationLevelDefault
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="level">消息通知级别</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onConversationTypeNotificationLevelChanged"></seealso>
        public abstract int ChangeConversationTypeNotificationLevel(
            RCIMConversationType type, RCIMPushNotificationLevel level,
            OnConversationTypeNotificationLevelChangedAction callback = null);
    
        /// <summary>
        /// 获取会话类型的消息提醒状态
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <returns>
        /// [onConversationTypeNotificationLevelLoaded]。@deprecated 用 {@link
        /// #getConversationTypeNotificationLevel(RCIMIWConversationType,
        /// IRCIMIWGetConversationTypeNotificationLevelCallback)} 代替
        /// </returns>
        public abstract int LoadConversationTypeNotificationLevel(RCIMConversationType type);
    
        /// <summary>
        /// 获取会话类型的消息提醒状态
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// [onConversationTypeNotificationLevelLoaded]
        /// </returns>
        public abstract int GetConversationTypeNotificationLevel(
            RCIMConversationType type, RCIMGetConversationTypeNotificationLevelListener callback = null);
    
        /// <summary>
        /// 设置超级群的默认消息状态。一般由管理员设置的接口，针对超级群的所有群成员生效，针对超级群下所有频道生效，优先级较低。如果群成员自己超级群的免打扰级别，那么以群成员自己设置的为准
        /// </summary>
        /// <param name="targetId">会话 ID</param>
        /// <param name="level">消息通知级别</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUltraGroupDefaultNotificationLevelChanged"></seealso>
        public abstract int ChangeUltraGroupDefaultNotificationLevel(
            string targetId, RCIMPushNotificationLevel level,
            OnUltraGroupDefaultNotificationLevelChangedAction callback = null);
    
        /// <summary>
        /// 获取超级群的默认消息状态
        /// </summary>
        /// <param name="targetId">会话 ID</param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUltraGroupDefaultNotificationLevelLoaded"></seealso>
        public abstract int LoadUltraGroupDefaultNotificationLevel(string targetId);
    
        /// <summary>
        /// 获取超级群的默认消息状态
        /// </summary>
        /// <param name="targetId">会话 ID</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUltraGroupDefaultNotificationLevelLoaded"></seealso>
        public abstract int GetUltraGroupDefaultNotificationLevel(
            string targetId, RCIMGetUltraGroupDefaultNotificationLevelListener callback = null);
    
        /// <summary>
        /// 设置超级群频道的默认消息状态
        /// </summary>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用</param>
        /// <param name="level">消息通知级别</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUltraGroupChannelDefaultNotificationLevelChanged"></seealso>
        public abstract int ChangeUltraGroupChannelDefaultNotificationLevel(
            string targetId, string channelId, RCIMPushNotificationLevel level,
            OnUltraGroupChannelDefaultNotificationLevelChangedAction callback = null);
    
        /// <summary>
        /// 获取超级群频道的默认消息状态
        /// </summary>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用</param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUltraGroupChannelDefaultNotificationLevelLoaded"></seealso>
        public abstract int LoadUltraGroupChannelDefaultNotificationLevel(string targetId, string channelId);
    
        /// <summary>
        /// 获取超级群频道的默认消息状态
        /// </summary>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUltraGroupChannelDefaultNotificationLevelLoaded"></seealso>
        public abstract int GetUltraGroupChannelDefaultNotificationLevel(
            string targetId, string channelId, RCIMGetUltraGroupChannelDefaultNotificationLevelListener callback = null);
    
        /// <summary>
        /// 设置是否显示远程推送内容详情，此功能需要从服务端开启用户设置功能
        /// </summary>
        /// <param name="showContent">是否显示远程推送内容</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onPushContentShowStatusChanged"></seealso>
        public abstract int ChangePushContentShowStatus(bool showContent,
                                                        OnPushContentShowStatusChangedAction callback = null);
    
        /// <summary>
        /// 设置推送语言
        /// </summary>
        /// <param name="language">推送语言， 目前仅支持 en_us、zh_cn、ar_sa</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onPushLanguageChanged"></seealso>
        public abstract int ChangePushLanguage(string language, OnPushLanguageChangedAction callback = null);
    
        /// <summary>
        /// 设置是否接收远程推送。前提：移动端未在线，Web 、MAC/PC
        /// 终端在线，移动端是否接收远程推送。此功能需要从服务端开启用户设置功能
        /// </summary>
        /// <param name="receive">是否接收</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onPushReceiveStatusChanged"></seealso>
        public abstract int ChangePushReceiveStatus(bool receive, OnPushReceiveStatusChangedAction callback = null);
    
        /// <summary>
        /// 给指定的群成员发送消息
        /// </summary>
        /// <param name="message">要发送的消息</param>
        /// <param name="userIds">群成员集合</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onGroupMessageToDesignatedUsersAttached"></seealso>
        /// <see cref="onGroupMessageToDesignatedUsersSent"/>
        public abstract int SendGroupMessageToDesignatedUsers(
            RCIMMessage message, List<string> userIds, RCIMSendGroupMessageToDesignatedUsersListener callback = null);
    
        /// <summary>
        /// 加载指定会话的消息总数
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onMessageCountLoaded"></seealso>
        public abstract int LoadMessageCount(RCIMConversationType type, string targetId, string channelId);
    
        /// <summary>
        /// 获取指定会话的消息总数
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onMessageCountLoaded"></seealso>
        public abstract int GetMessageCount(RCIMConversationType type, string targetId, string channelId,
                                            RCIMGetMessageCountListener callback = null);
    
        /// <summary>
        /// 根据会话类型,加载置顶会话列表
        /// </summary>
        /// <param name="conversationTypes">会话类型集合</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onTopConversationsLoaded"></seealso>
        public abstract int LoadTopConversations(List<RCIMConversationType> conversationTypes, string channelId);
    
        /// <summary>
        /// 根据会话类型,获取置顶会话列表
        /// </summary>
        /// <param name="conversationTypes">会话类型集合</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用，其他会话类型传 null 即可</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onTopConversationsLoaded"></seealso>
        public abstract int GetTopConversations(List<RCIMConversationType> conversationTypes, string channelId,
                                                RCIMGetTopConversationsListener callback = null);
    
        /// <summary>
        /// 上报超级群的已读时间
        /// </summary>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用</param>
        /// <param name="timestamp">已读时间</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUltraGroupReadStatusSynced"></seealso>
        public abstract int SyncUltraGroupReadStatus(string targetId, string channelId, long timestamp,
                                                     OnUltraGroupReadStatusSyncedAction callback = null);
    
        /// <summary>
        /// 获取特定会话下所有频道的会话列表，只支持超级群
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onConversationsLoadedForAllChannel"></seealso>
        public abstract int LoadConversationsForAllChannel(RCIMConversationType type, string targetId);
    
        /// <summary>
        /// 获取特定会话下所有频道的会话列表，只支持超级群
        /// </summary>
        /// <param name="type">会话类型</param>
        /// <param name="targetId">会话 ID</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onConversationsLoadedForAllChannel"></seealso>
        public abstract int GetConversationsForAllChannel(RCIMConversationType type, string targetId,
                                                          RCIMGetConversationsForAllChannelListener callback = null);
    
        /// <summary>
        /// 修改超级群消息
        /// </summary>
        /// <param name="messageUId">
        /// 消息的 messageUid，可在消息对象中获取，且只有发送成功的消息才会有值
        /// </param>
        /// <param name="message">要修改的 message</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUltraGroupMessageModified"></seealso>
        public abstract int ModifyUltraGroupMessage(string messageUId, RCIMMessage message,
                                                    OnUltraGroupMessageModifiedAction callback = null);
    
        /// <summary>
        /// 撤回超级群消息
        /// </summary>
        /// <param name="message">需要撤回的消息</param>
        /// <param name="deleteRemote">是否删除远端消息</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUltraGroupMessageRecalled"></seealso>
        public abstract int RecallUltraGroupMessage(RCIMMessage message, bool deleteRemote,
                                                    OnUltraGroupMessageRecalledAction callback = null);
    
        /// <summary>
        /// 删除超级群指定时间之前的消息
        /// </summary>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="policy">清除策略</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUltraGroupMessagesCleared"></seealso>
        public abstract int ClearUltraGroupMessages(string targetId, string channelId, long timestamp,
                                                    RCIMMessageOperationPolicy policy,
                                                    OnUltraGroupMessagesClearedAction callback = null);
    
        /// <summary>
        /// 发送超级群输入状态
        /// </summary>
        /// <param name="targetId">会话 ID</param>
        /// <param name="channelId">频道 ID，仅支持超级群使用</param>
        /// <param name="typingStatus">输入状态</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUltraGroupTypingStatusSent"></seealso>
        public abstract int SendUltraGroupTypingStatus(string targetId, string channelId,
                                                       RCIMUltraGroupTypingStatus typingStatus,
                                                       OnUltraGroupTypingStatusSentAction callback = null);
    
        /// <summary>
        /// 删除超级群所有频道指定时间之前的消息
        /// </summary>
        /// <param name="targetId">会话 ID</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUltraGroupMessagesClearedForAllChannel"></seealso>
        public abstract int ClearUltraGroupMessagesForAllChannel(
            string targetId, long timestamp, OnUltraGroupMessagesClearedForAllChannelAction callback = null);
    
        /// <summary>
        /// 从服务获取批量消息
        /// </summary>
        /// <param name="messages">获取的消息集合</param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onBatchRemoteUltraGroupMessagesLoaded"></seealso>
        public abstract int LoadBatchRemoteUltraGroupMessages(List<RCIMMessage> messages);
    
        /// <summary>
        /// 从服务获取批量消息
        /// </summary>
        /// <param name="messages">获取的消息集合</param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onBatchRemoteUltraGroupMessagesLoaded"></seealso>
        public abstract int GetBatchRemoteUltraGroupMessages(List<RCIMMessage> messages,
                                                             RCIMGetBatchRemoteUltraGroupMessagesListener callback = null);
    
        /// <summary>
        /// 更新超级群消息扩展信息
        /// </summary>
        /// <param name="messageUId">
        /// 消息的 messageUid，可在消息对象中获取，且只有发送成功的消息才会有值
        /// </param>
        /// <param name="expansion">
        /// 更新的消息扩展信息键值对，类型是 HashMap；Key 支持大小写英文字母、数字、部分特殊符号 + = - _
        /// 的组合方式，不支持汉字。Value 可以输入空格
        /// </param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUltraGroupMessageExpansionUpdated"></seealso>
        public abstract int UpdateUltraGroupMessageExpansion(string messageUId, Dictionary<string, string> expansion,
                                                             OnUltraGroupMessageExpansionUpdatedAction callback = null);
    
        /// <summary>
        /// 删除超级群消息扩展信息中特定的键值对
        /// </summary>
        /// <param name="messageUId">
        /// 消息的 messageUid，可在消息对象中获取，且只有发送成功的消息才会有值
        /// </param>
        /// <param name="keys">
        /// 消息扩展信息中待删除的 key 的列表，类型是 ArrayList
        /// </param>
        /// <param name="callback">
        /// 事件回调。SDK 从 5.3.1 版本开始支持 callback 方式回调。从 5.4.0 版本废弃该接口的其他回调方式。如果传入了
        /// callback 参数，仅触发 callback 回调
        /// </param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        /// <seealso cref="onUltraGroupMessageExpansionForKeysRemoved"></seealso>
        public abstract int RemoveUltraGroupMessageExpansionForKeys(
            string messageUId, List<string> keys, OnUltraGroupMessageExpansionForKeysRemovedAction callback = null);
    
        /// <summary>
        /// 修改日志等级
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <returns>
        /// 当次接口操作的状态码。0 代表调用成功 具体结果需要实现接口回调，非 0
        /// 代表当前接口调用操作失败，不会触发接口回调，详细错误参考错误码
        /// </returns>
        public abstract int ChangeLogLevel(RCIMLogLevel level);
    
        /// <summary>
        /// 获取本地时间与服务器时间的时间差。消息发送成功后，SDK
        /// 与服务器同步时间，消息所在数据库中存储的时间就是服务器时间。 System.currentTimeMillis() - getDeltaTime()
        /// 可以获取服务器当前时间
        /// </summary>
        /// <returns>本地时间与服务器时间的差值</returns>
        public abstract long GetDeltaTime();
    
        /// <summary>
        /// 收到消息的监听
        /// </summary>
        public OnMessageReceivedDelegate OnMessageReceived { set; internal get; }
    
        /// <summary>
        /// 网络状态变化
        /// </summary>
        public OnConnectionStatusChangedDelegate OnConnectionStatusChanged { set; internal get; }
    
        /// <summary>
        /// 会话状态置顶多端同步监听
        /// </summary>
        public OnConversationTopStatusSyncedDelegate OnConversationTopStatusSynced { set; internal get; }
    
        /// <summary>
        /// 会话状态免打扰多端同步监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnConversationNotificationLevelSyncedDelegate OnConversationNotificationLevelSynced { set; internal get; }
    
        /// <summary>
        /// 撤回消息监听器
        /// </summary>
        public OnRemoteMessageRecalledDelegate OnRemoteMessageRecalled { set; internal get; }
    
        /// <summary>
        /// 单聊中收到消息回执的回调
        /// </summary>
        public OnPrivateReadReceiptReceivedDelegate OnPrivateReadReceiptReceived { set; internal get; }
    
        /// <summary>
        /// 消息扩展信息更改的回调
        /// </summary>
        public OnRemoteMessageExpansionUpdatedDelegate OnRemoteMessageExpansionUpdated { set; internal get; }
    
        /// <summary>
        /// 消息扩展信息删除的回调
        /// </summary>
        public OnRemoteMessageExpansionForKeyRemovedDelegate OnRemoteMessageExpansionForKeyRemoved { set; internal get; }
    
        /// <summary>
        /// 聊天室用户进入、退出聊天室监听
        /// </summary>
        public OnChatRoomMemberChangedDelegate OnChatRoomMemberChanged { set; internal get; }
    
        /// <summary>
        /// 会话输入状态发生变化。对于单聊而言，当对方正在输入时，监听会触发一次；当对方不处于输入状态时，该监听还会触发一次，但回调里输入用户列表为空
        /// </summary>
        public OnTypingStatusChangedDelegate OnTypingStatusChanged { set; internal get; }
    
        /// <summary>
        /// 同步消息未读状态监听接口。多端登录，收到其它端清除某一会话未读数通知的时候
        /// </summary>
        public OnConversationReadStatusSyncMessageReceivedDelegate OnConversationReadStatusSyncMessageReceived {
            set; internal get; }
    
        /// <summary>
        /// 聊天室 KV 同步完成的回调
        /// </summary>
        public OnChatRoomEntriesSyncedDelegate OnChatRoomEntriesSynced { set; internal get; }
    
        /// <summary>
        /// 聊天室 KV 发生变化的回调
        /// </summary>
        public OnChatRoomEntriesChangedDelegate OnChatRoomEntriesChanged { set; internal get; }
    
        /// <summary>
        /// 超级群消息 kv 被更新
        /// </summary>
        public OnRemoteUltraGroupMessageExpansionUpdatedDelegate OnRemoteUltraGroupMessageExpansionUpdated { set;
                                                                                                             internal get; }
    
        /// <summary>
        /// 超级群消息被更改
        /// </summary>
        public OnRemoteUltraGroupMessageModifiedDelegate OnRemoteUltraGroupMessageModified { set; internal get; }
    
        /// <summary>
        /// 超级群消息被撤回
        /// </summary>
        public OnRemoteUltraGroupMessageRecalledDelegate OnRemoteUltraGroupMessageRecalled { set; internal get; }
    
        /// <summary>
        /// 超级群已读的监听
        /// </summary>
        public OnUltraGroupReadTimeReceivedDelegate OnUltraGroupReadTimeReceived { set; internal get; }
    
        /// <summary>
        /// 用户输入状态变化的回调。当客户端收到用户输入状态的变化时，会回调此接口，通知发生变化的会话以及当前正在输入的RCUltraGroupTypingStatusInfo列表
        /// </summary>
        public OnUltraGroupTypingStatusChangedDelegate OnUltraGroupTypingStatusChanged { set; internal get; }
    
        /// <summary>
        /// 发送含有敏感词消息被拦截的回调
        /// </summary>
        public OnMessageBlockedDelegate OnMessageBlocked { set; internal get; }
    
        /// <summary>
        /// 聊天室状态发生变化的监听
        /// </summary>
        public OnChatRoomStatusChangedDelegate OnChatRoomStatusChanged { set; internal get; }
    
        /// <summary>
        /// 收到群聊已读回执请求的监听
        /// </summary>
        public OnGroupMessageReadReceiptRequestReceivedDelegate OnGroupMessageReadReceiptRequestReceived { set;
                                                                                                           internal get; }
    
        /// <summary>
        /// 收到群聊已读回执响应的监听
        /// </summary>
        public OnGroupMessageReadReceiptResponseReceivedDelegate OnGroupMessageReadReceiptResponseReceived { set;
                                                                                                             internal get; }
    
        /// <summary>
        /// connect 的接口监听，收到链接结果的回调
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnConnectedDelegate OnConnected { set; internal get; }
    
        /// <summary>
        /// connect 的接口监听，数据库打开时发生的回调
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnDatabaseOpenedDelegate OnDatabaseOpened { set; internal get; }
    
        /// <summary>
        /// loadConversation 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnConversationLoadedDelegate OnConversationLoaded { set; internal get; }
    
        /// <summary>
        /// loadConversations 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnConversationsLoadedDelegate OnConversationsLoaded { set; internal get; }
    
        /// <summary>
        /// removeConversation 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnConversationRemovedDelegate OnConversationRemoved { set; internal get; }
    
        /// <summary>
        /// removeConversations 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnConversationsRemovedDelegate OnConversationsRemoved { set; internal get; }
    
        /// <summary>
        /// loadTotalUnreadCount 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnTotalUnreadCountLoadedDelegate OnTotalUnreadCountLoaded { set; internal get; }
    
        /// <summary>
        /// loadUnreadCount 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnUnreadCountLoadedDelegate OnUnreadCountLoaded { set; internal get; }
    
        /// <summary>
        /// loadUnreadCountByConversationTypes 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnUnreadCountByConversationTypesLoadedDelegate OnUnreadCountByConversationTypesLoaded { set; internal get; }
    
        /// <summary>
        /// loadUnreadMentionedCount 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnUnreadMentionedCountLoadedDelegate OnUnreadMentionedCountLoaded { set; internal get; }
    
        /// <summary>
        /// loadUltraGroupAllUnreadMentionedCount 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnUltraGroupAllUnreadCountLoadedDelegate OnUltraGroupAllUnreadCountLoaded { set; internal get; }
    
        /// <summary>
        /// loadUltraGroupAllUnreadMentionedCount 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnUltraGroupAllUnreadMentionedCountLoadedDelegate OnUltraGroupAllUnreadMentionedCountLoaded { set;
                                                                                                             internal get; }
    
        /// <summary>
        /// 超级群列表同步完成的回调
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnUltraGroupConversationsSyncedDelegate OnUltraGroupConversationsSynced { set; internal get; }
    
        /// <summary>
        /// clearUnreadCount 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnUnreadCountClearedDelegate OnUnreadCountCleared { set; internal get; }
    
        /// <summary>
        /// saveDraftMessage 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnDraftMessageSavedDelegate OnDraftMessageSaved { set; internal get; }
    
        /// <summary>
        /// clearDraftMessage 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnDraftMessageClearedDelegate OnDraftMessageCleared { set; internal get; }
    
        /// <summary>
        /// loadDraftMessage 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnDraftMessageLoadedDelegate OnDraftMessageLoaded { set; internal get; }
    
        /// <summary>
        /// loadBlockedConversations 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnBlockedConversationsLoadedDelegate OnBlockedConversationsLoaded { set; internal get; }
    
        /// <summary>
        /// changeConversationTopStatus 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnConversationTopStatusChangedDelegate OnConversationTopStatusChanged { set; internal get; }
    
        /// <summary>
        /// loadConversationTopStatus 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnConversationTopStatusLoadedDelegate OnConversationTopStatusLoaded { set; internal get; }
    
        /// <summary>
        /// syncConversationReadStatus 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnConversationReadStatusSyncedDelegate OnConversationReadStatusSynced { set; internal get; }
    
        /// <summary>
        /// sendMessage 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnMessageAttachedDelegate OnMessageAttached { set; internal get; }
    
        /// <summary>
        /// sendMessage 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnMessageSentDelegate OnMessageSent { set; internal get; }
    
        /// <summary>
        /// sendMediaMessage 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnMediaMessageAttachedDelegate OnMediaMessageAttached { set; internal get; }
    
        /// <summary>
        /// sendMediaMessage 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnMediaMessageSendingDelegate OnMediaMessageSending { set; internal get; }
    
        /// <summary>
        /// cancelSendingMediaMessage 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnSendingMediaMessageCanceledDelegate OnSendingMediaMessageCanceled { set; internal get; }
    
        /// <summary>
        /// sendMediaMessage 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnMediaMessageSentDelegate OnMediaMessageSent { set; internal get; }
    
        /// <summary>
        /// downloadMediaMessage 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnMediaMessageDownloadingDelegate OnMediaMessageDownloading { set; internal get; }
    
        /// <summary>
        /// downloadMediaMessage 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnMediaMessageDownloadedDelegate OnMediaMessageDownloaded { set; internal get; }
    
        /// <summary>
        /// cancelDownloadingMediaMessage 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnDownloadingMediaMessageCanceledDelegate OnDownloadingMediaMessageCanceled { set; internal get; }
    
        /// <summary>
        /// loadMessages 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnMessagesLoadedDelegate OnMessagesLoaded { set; internal get; }
    
        /// <summary>
        /// loadUnreadMentionedMessages 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnUnreadMentionedMessagesLoadedDelegate OnUnreadMentionedMessagesLoaded { set; internal get; }
    
        /// <summary>
        /// loadFirstUnreadMessage 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnFirstUnreadMessageLoadedDelegate OnFirstUnreadMessageLoaded { set; internal get; }
    
        /// <summary>
        /// insertMessage 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnMessageInsertedDelegate OnMessageInserted { set; internal get; }
    
        /// <summary>
        /// insertMessages 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnMessagesInsertedDelegate OnMessagesInserted { set; internal get; }
    
        /// <summary>
        /// clearMessages 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnMessagesClearedDelegate OnMessagesCleared { set; internal get; }
    
        /// <summary>
        /// deleteLocalMessages 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnLocalMessagesDeletedDelegate OnLocalMessagesDeleted { set; internal get; }
    
        /// <summary>
        /// deleteMessages 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnMessagesDeletedDelegate OnMessagesDeleted { set; internal get; }
    
        /// <summary>
        /// recallMessage 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnMessageRecalledDelegate OnMessageRecalled { set; internal get; }
    
        /// <summary>
        /// sendPrivateReadReceiptMessage 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnPrivateReadReceiptMessageSentDelegate OnPrivateReadReceiptMessageSent { set; internal get; }
    
        /// <summary>
        /// updateMessageExpansion 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnMessageExpansionUpdatedDelegate OnMessageExpansionUpdated { set; internal get; }
    
        /// <summary>
        /// removeMessageExpansionForKeys 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnMessageExpansionForKeysRemovedDelegate OnMessageExpansionForKeysRemoved { set; internal get; }
    
        /// <summary>
        /// changeMessageReceiveStatus 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnMessageReceiveStatusChangedDelegate OnMessageReceiveStatusChanged { set; internal get; }
    
        /// <summary>
        /// changeMessageSentStatus 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnMessageSentStatusChangedDelegate OnMessageSentStatusChanged { set; internal get; }
    
        /// <summary>
        /// joinChatRoom 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnChatRoomJoinedDelegate OnChatRoomJoined { set; internal get; }
    
        /// <summary>
        /// 正在加入聊天室的回调
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnChatRoomJoiningDelegate OnChatRoomJoining { set; internal get; }
    
        /// <summary>
        /// leaveChatRoom 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnChatRoomLeftDelegate OnChatRoomLeft { set; internal get; }
    
        /// <summary>
        /// loadChatRoomMessages 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnChatRoomMessagesLoadedDelegate OnChatRoomMessagesLoaded { set; internal get; }
    
        /// <summary>
        /// addChatRoomEntry 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnChatRoomEntryAddedDelegate OnChatRoomEntryAdded { set; internal get; }
    
        /// <summary>
        /// addChatRoomEntries 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnChatRoomEntriesAddedDelegate OnChatRoomEntriesAdded { set; internal get; }
    
        /// <summary>
        /// loadChatRoomEntry 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnChatRoomEntryLoadedDelegate OnChatRoomEntryLoaded { set; internal get; }
    
        /// <summary>
        /// loadChatRoomAllEntries 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnChatRoomAllEntriesLoadedDelegate OnChatRoomAllEntriesLoaded { set; internal get; }
    
        /// <summary>
        /// removeChatRoomEntry 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnChatRoomEntryRemovedDelegate OnChatRoomEntryRemoved { set; internal get; }
    
        /// <summary>
        /// removeChatRoomEntries 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnChatRoomEntriesRemovedDelegate OnChatRoomEntriesRemoved { set; internal get; }
    
        /// <summary>
        /// addToBlacklist 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnBlacklistAddedDelegate OnBlacklistAdded { set; internal get; }
    
        /// <summary>
        /// removeFromBlacklist 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnBlacklistRemovedDelegate OnBlacklistRemoved { set; internal get; }
    
        /// <summary>
        /// loadBlacklistStatus 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnBlacklistStatusLoadedDelegate OnBlacklistStatusLoaded { set; internal get; }
    
        /// <summary>
        /// loadBlacklist 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnBlacklistLoadedDelegate OnBlacklistLoaded { set; internal get; }
    
        /// <summary>
        /// searchMessages 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnMessagesSearchedDelegate OnMessagesSearched { set; internal get; }
    
        /// <summary>
        /// searchMessagesByTimeRange 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnMessagesSearchedByTimeRangeDelegate OnMessagesSearchedByTimeRange { set; internal get; }
    
        /// <summary>
        /// searchMessagesByUserId 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnMessagesSearchedByUserIdDelegate OnMessagesSearchedByUserId { set; internal get; }
    
        /// <summary>
        /// searchConversations 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnConversationsSearchedDelegate OnConversationsSearched { set; internal get; }
    
        /// <summary>
        /// sendGroupReadReceiptRequest 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnGroupReadReceiptRequestSentDelegate OnGroupReadReceiptRequestSent { set; internal get; }
    
        /// <summary>
        /// sendGroupReadReceiptResponse 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnGroupReadReceiptResponseSentDelegate OnGroupReadReceiptResponseSent { set; internal get; }
    
        /// <summary>
        /// changeNotificationQuietHours 的接口回调
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnNotificationQuietHoursChangedDelegate OnNotificationQuietHoursChanged { set; internal get; }
    
        /// <summary>
        /// removeNotificationQuietHours 的接口回调
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnNotificationQuietHoursRemovedDelegate OnNotificationQuietHoursRemoved { set; internal get; }
    
        /// <summary>
        /// loadNotificationQuietHours 的接口回调
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnNotificationQuietHoursLoadedDelegate OnNotificationQuietHoursLoaded { set; internal get; }
    
        /// <summary>
        /// changeConversationNotificationLevel 的接口回调
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnConversationNotificationLevelChangedDelegate OnConversationNotificationLevelChanged { set; internal get; }
    
        /// <summary>
        /// loadConversationNotificationLevel 的接口回调
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnConversationNotificationLevelLoadedDelegate OnConversationNotificationLevelLoaded { set; internal get; }
    
        /// <summary>
        /// changeConversationTypeNotificationLevel 的接口回调
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnConversationTypeNotificationLevelChangedDelegate OnConversationTypeNotificationLevelChanged {
            set; internal get; }
    
        /// <summary>
        /// loadConversationTypeNotificationLevel 的接口回调
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnConversationTypeNotificationLevelLoadedDelegate OnConversationTypeNotificationLevelLoaded { set;
                                                                                                             internal get; }
    
        /// <summary>
        /// changeUltraGroupDefaultNotificationLevel 的接口回调
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnUltraGroupDefaultNotificationLevelChangedDelegate OnUltraGroupDefaultNotificationLevelChanged {
            set; internal get; }
    
        /// <summary>
        /// loadUltraGroupDefaultNotificationLevel 的接口回调
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnUltraGroupDefaultNotificationLevelLoadedDelegate OnUltraGroupDefaultNotificationLevelLoaded {
            set; internal get; }
    
        /// <summary>
        /// changeUltraGroupChannelDefaultNotificationLevel 的接口回调
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnUltraGroupChannelDefaultNotificationLevelChangedDelegate
            OnUltraGroupChannelDefaultNotificationLevelChanged { set; internal get; }
    
        /// <summary>
        /// loadUltraGroupChannelDefaultNotificationLevel 的接口回调
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnUltraGroupChannelDefaultNotificationLevelLoadedDelegate OnUltraGroupChannelDefaultNotificationLevelLoaded {
            set; internal get; }
    
        /// <summary>
        /// changePushContentShowStatus 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnPushContentShowStatusChangedDelegate OnPushContentShowStatusChanged { set; internal get; }
    
        /// <summary>
        /// changePushLanguage 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnPushLanguageChangedDelegate OnPushLanguageChanged { set; internal get; }
    
        /// <summary>
        /// changePushReceiveStatus 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnPushReceiveStatusChangedDelegate OnPushReceiveStatusChanged { set; internal get; }
    
        /// <summary>
        /// loadMessageCount 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnMessageCountLoadedDelegate OnMessageCountLoaded { set; internal get; }
    
        /// <summary>
        ///
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnTopConversationsLoadedDelegate OnTopConversationsLoaded { set; internal get; }
    
        /// <summary>
        /// sendGroupMessageToDesignatedUsers 的接口回调。消息存入数据库的回调
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnGroupMessageToDesignatedUsersAttachedDelegate OnGroupMessageToDesignatedUsersAttached { set;
                                                                                                         internal get; }
    
        /// <summary>
        /// sendGroupMessageToDesignatedUsers 的接口回调。消息发送完成的回调
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnGroupMessageToDesignatedUsersSentDelegate OnGroupMessageToDesignatedUsersSent { set; internal get; }
    
        /// <summary>
        /// syncUltraGroupReadStatus 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnUltraGroupReadStatusSyncedDelegate OnUltraGroupReadStatusSynced { set; internal get; }
    
        /// <summary>
        /// loadConversationsForAllChannel 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnConversationsLoadedForAllChannelDelegate OnConversationsLoadedForAllChannel { set; internal get; }
    
        /// <summary>
        /// loadUltraGroupUnreadMentionedCount 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnUltraGroupUnreadMentionedCountLoadedDelegate OnUltraGroupUnreadMentionedCountLoaded { set; internal get; }
    
        /// <summary>
        ///
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnUltraGroupUnreadCountLoadedDelegate OnUltraGroupUnreadCountLoaded { set; internal get; }
    
        /// <summary>
        /// modifyUltraGroupMessage 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnUltraGroupMessageModifiedDelegate OnUltraGroupMessageModified { set; internal get; }
    
        /// <summary>
        /// recallUltraGroupMessage 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnUltraGroupMessageRecalledDelegate OnUltraGroupMessageRecalled { set; internal get; }
    
        /// <summary>
        /// clearUltraGroupMessages 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnUltraGroupMessagesClearedDelegate OnUltraGroupMessagesCleared { set; internal get; }
    
        /// <summary>
        /// clearUltraGroupMessagesForAllChannel 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnUltraGroupMessagesClearedForAllChannelDelegate OnUltraGroupMessagesClearedForAllChannel { set;
                                                                                                           internal get; }
    
        /// <summary>
        /// sendUltraGroupTypingStatus 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnUltraGroupTypingStatusSentDelegate OnUltraGroupTypingStatusSent { set; internal get; }
    
        /// <summary>
        /// loadBatchRemoteUltraGroupMessages 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnBatchRemoteUltraGroupMessagesLoadedDelegate OnBatchRemoteUltraGroupMessagesLoaded { set; internal get; }
    
        /// <summary>
        /// updateUltraGroupMessageExpansion 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnUltraGroupMessageExpansionUpdatedDelegate OnUltraGroupMessageExpansionUpdated { set; internal get; }
    
        /// <summary>
        /// removeUltraGroupMessageExpansionForKeys 的接口监听
        /// </summary>
        [Obsolete("请使用方法对应的监听")]
        public OnUltraGroupMessageExpansionForKeysRemovedDelegate OnUltraGroupMessageExpansionForKeysRemoved {
            set; internal get; }
    }
}