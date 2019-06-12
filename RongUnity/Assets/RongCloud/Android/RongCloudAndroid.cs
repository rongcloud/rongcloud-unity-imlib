using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
namespace cn.rongcloud.sdk
{
#if UNITY_ANDROID
    public class RongCloudAndroid
    {
        private static AndroidJavaClass ajcRongcloud = new AndroidJavaClass("cn.rongcloud.imlib.unity.RongCloud");
        private static AndroidJavaObject ajoCurrentActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");

        public static void init(string appkey)
        {
            ajcRongcloud.CallStatic("init", ajoCurrentActivity, appkey );
        }
        public static void connect(string token)
        {
            ajcRongcloud.CallStatic("connect", token);
        }
        public static string getCurrentUserId()
        {
            return ajcRongcloud.CallStatic<string>("getCurrentUserId");
        }
        public static int getCurrentConnectionStatus()
        {
            return ajcRongcloud.CallStatic<int>("getCurrentConnectionStatus");
        }
        public static void logout()
        {
            ajcRongcloud.CallStatic("logout");
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventId"></param>
    public static void sendMessage(string message,string eventId)
        {
            ajcRongcloud.CallStatic("sendMessage", message, eventId);
        }
        
        public static void joinChatRoom(string targetId,int messageCount)
        {
            ajcRongcloud.CallStatic("joinChatRoom", targetId, messageCount);
        }
        public static void getChatRoomInfo(string targetId,int memberCount,ChatRoomMemberOrder order)
        {
            ajcRongcloud.CallStatic("getChatRoomInfo", targetId, memberCount, (int)order);
        }
    }
#endif
}

