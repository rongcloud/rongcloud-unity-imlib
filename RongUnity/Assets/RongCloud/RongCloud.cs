using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace cn.rongcloud.sdk
{
    public class RongCloud 
    {
        
        /// <summary>
        /// 初始化 SDK，只需要调用一次
        /// </summary>
        /// <param name="appKey">从融云开发者平台创建应用后获取到的 App Key</param>
        public static void init(string appKey)
        {
            if (IsEditor()) { return; }

#if UNITY_IOS
            RongCloudIOS.init(appKey);
#elif UNITY_ANDROID
            RongCloudAndroid.init(appKey);
#endif
        }
        /// <summary>
        /// 连接融云服务器，只需要调用一次
        /// 在 App 整个生命周期，您只需要调用一次此方法与融云服务器建立连接。
        ///之后无论是网络出现异常或者App有前后台的切换等，SDK都会负责自动重连。
        ///除非您已经手动将连接断开，否则您不需要自己再手动重连。
        ///通过 RCEvent.connectEvent 获取连接状态信息
        /// </summary>
        /// <param name="token">从服务端获取的用户身份令牌（Token）。</param>
        public static void connect(string token)
        {
            if (IsEditor()) { return ; }
#if UNITY_IOS
            RongCloudIOS.connect(token);
#elif UNITY_ANDROID
            RongCloudAndroid.connect(token);
#endif
        }
        /// <summary>
        /// 获取当前连接用户的信息。等同于 connect(String, ConnectCallback) 成功后返回的 userId。
        /// </summary>
        /// <returns>当前连接用户的信息。</returns>
        public static string getCurrentUserId()
        {
            if (IsEditor()) { return string.Empty; }

#if UNITY_IOS
            return RongCloudIOS.getCurrentUserID();
#elif UNITY_ANDROID
            return RongCloudAndroid.getCurrentUserId();
#endif
        }
        /// <summary>
        /// 得到当前连接状态
        /// </summary>
        /// <returns>连接状态枚举。</returns>
        public static ConnectionStatus getCurrentConnectionStatus()
        {
            if (IsEditor()) { return (ConnectionStatus)(- 1); }

#if UNITY_IOS
            return (ConnectionStatus)RongCloudIOS.getCurrentConnectionStatus();
#elif UNITY_ANDROID
            return (ConnectionStatus)RongCloudAndroid.getCurrentConnectionStatus();
#endif
        }
        /// <summary>
        /// 断开与融云服务器的连接，并且不再接收 Push 消息。
        /// 若想断开连接后仍然接受 Push 消息，可以调用 disconnect()
        /// </summary>
        public static void logout()
        {
            if (IsEditor()) { return; }

#if UNITY_IOS
            RongCloudIOS.logout();
#elif UNITY_ANDROID
            RongCloudAndroid.logout();
#endif
        }
        public static void sendMessage (string message, string eventId){
            if (IsEditor()) { return; }

#if UNITY_IOS
            RongCloudIOS.sendMessage(message, eventId);
#elif UNITY_ANDROID
            RongCloudAndroid.sendMessage(message, eventId);
#endif
        }

        public static void sendMessage(Message message,string pushContent,string pushData, string eventId)
        {
            string msg = JsonUtility.ToJson(message);
            sendMessage(msg, eventId);

        }
        /// <summary>
        /// 根据会话类型，发送消息。
        /// 在 RCEvent.sendMessageEvent 事件中获取发送成功、失败消息
        /// </summary>
        /// <param name="type">会话类型。</param>
        /// <param name="targetId">目标 Id。根据不同的 conversationType，可能是用户 Id、讨论组 Id、群组 Id 或聊天室 Id。</param>
        /// <param name="content"> 消息内容，例如 TextMessage, ImageMessage。</param>
        /// <param name="pushContent">当下发 push 消息时，在通知栏里会显示这个字段。 如果发送的是自定义消息，该字段必须填写，否则无法收到 push 消息。 如果发送 sdk 中默认的消息类型，例如 RC:TxtMsg, RC:VcMsg, RC:ImgMsg，则不需要填写，默认已经指定。</param>
        /// <param name="pushData">push 附加信息。如果设置该字段，用户在收到 push 消息时，能通过 PushNotificationMessage.getPushData() 方法获取。</param>
        /// <param name="eventId">发送消息的回调事件 Id</param>
        public static void sendMessage(ConversationType type, string targetId, MessageContent content, string eventId, string pushContent="", string pushData="")
        {

            JsonData data = new JsonData();

            data["conversationType"] = (int)type;
            data["targetId"] = targetId;
            data["pushContent"] = pushContent;
            data["pushData"] = pushData;
            data["content"] = JsonUtility.ToJson(content);
            sendMessage(data.ToJson(), eventId);
        }
        
        
        /// <summary>
        /// 加入聊天室。如果聊天室不存在，sdk 会创建聊天室并加入，如果已存在，则直接加入。加入聊天室时，可以选择拉取聊天室消息数目。
        /// 通过 RCEvent.joinChatRoomEvent 获取回调信息。
        /// </summary>
        /// <param name="chatRoomId">聊天室 Id。</param>
        /// <param name="defMessageCount">进入聊天室拉取消息数目，-1 时不拉取任何消息，0 时拉取 10 条消息，最多只能拉取 50 条。</param>
        public static void joinChatRoom(string chatRoomId, int defMessageCount)
        {
            if (IsEditor()) { return; }

#if UNITY_IOS
            RongCloudIOS.joinChatRoom(chatRoomId, defMessageCount);
#elif UNITY_ANDROID
            RongCloudAndroid.joinChatRoom(chatRoomId, defMessageCount);
#endif
        }
        /// <summary>
        /// 查询聊天室信息，通过 RCEvent.getChatRoomInfoEvent 获取查询数据
        /// </summary>
        /// <param name="chatRoomId">聊天室 Id。</param>
        /// <param name="defMemberCount">进入聊天室拉成员数目，最多 20 条。</param>
        /// <param name="order">按照何种顺序返回聊天室成员信息。升序, 返回最早加入的用户列表; 降序, 返回最晚加入的用户列表。</param>
        public static void getChatRoomInfo(string chatRoomId, int defMemberCount, ChatRoomMemberOrder order)
        {
            if (IsEditor()) { return; }

#if UNITY_IOS
            RongCloudIOS.getChatRoomInfo(chatRoomId, defMemberCount, order);
#elif UNITY_ANDROID
            RongCloudAndroid.getChatRoomInfo(chatRoomId, defMemberCount, order);
#endif
        }
    
        private static bool IsEditor()
        {
#if UNITY_EDITOR
            
            return true;
#else
            return false;
#endif
        }
    }
}

