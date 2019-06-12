using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace cn.rongcloud.sdk
{
#if UNITY_IOS
    public class RongCloudIOS
    {
        [DllImport("__Internal")]
        private static extern void _init(string appKey);

        [DllImport("__Internal")]
        private static extern void _connect(string appToken);
        [DllImport("__Internal")]
        private static extern string _getCurrentUserID();
        [DllImport("__Internal")]
        private static extern int _getCurrentConnectionStatus();
        [DllImport("__Internal")]
        private static extern void _logout();
        [DllImport("__Internal")]
        private static extern void _joinChatRoom(string targetId,int messageCount);
        [DllImport("__Internal")]
        private static extern void _getChatRoomInfo(string targetId, int memberCount,int order);
        [DllImport("__Internal")]
        private static extern void _sendMessage(string message, string eventId);


        public static void init(string appKey)
        {
            _init(appKey);
        }

        public static void connect(string appToken)
        {
            _connect(appToken);
        }

        public static string getCurrentUserID()
        {
            return _getCurrentUserID();
        }

        public static int getCurrentConnectionStatus()
        {
            return _getCurrentConnectionStatus();
        }

        public static void logout()
        {
            _logout();
        }

        public static void joinChatRoom(string targetId,int messageCofunt)
        {
            _joinChatRoom(targetId, messageCofunt);
        }

        public static void getChatRoomInfo(string targetId,int memberCount,ChatRoomMemberOrder order)
        {
            _getChatRoomInfo(targetId, memberCount, (int)order);
        }
        public static void sendMessage(string message, string eventId)
        {
            _sendMessage(message, eventId);
        }
    }
#endif
}
