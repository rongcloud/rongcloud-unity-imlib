using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace cn.rongcloud.sdk
{
    public delegate void CallBackData(JsonData data);
    
    public class RCEvent
    {
        public static event CallBackData connectionStatusEvent;     //连接状态事件监听                                                               
        public static event Action<Message,int> onReceiveMessageEvent;     //接收消息事件监听  
        public static event CallBackData connectEvent;      //connect 方法事件
        public static event CallBackData sendMessageEvent;  //发送消息事件
        public static event Action<ChatRoomInfo> getChatRoomInfoEvent;    //得到聊天室消息事件
        public static event CallBackData joinChatRoomEvent;     //加入聊天室事件
        private static RCEvent instance;

        public static RCEvent GetInstance()
        {
            if (instance == null)
            {
                instance = new RCEvent();
            }
            return instance;

        }
        /// <summary>
        /// setConnectionStatusListener
        /// </summary>
        /// <param name="data"></param>
        public void ConnectionStatus(JsonData data)
        {
            if (connectionStatusEvent != null)
                connectionStatusEvent(data);
        }
        public void OnReceiveMessage(JsonData data)
        {
           
            if (onReceiveMessageEvent != null)
            {
                Message msg = new Message(data["message"]);
               
                onReceiveMessageEvent(msg,int.Parse(data["left"].ToString()));
            }
                
        }
        public void Connect(JsonData data)
        {
            if (connectEvent != null)
                connectEvent(data);
        }

        public void SendMessage(JsonData data)
        {
            if (sendMessageEvent != null)
                sendMessageEvent(data);
        }
        public void GetChatRoomInfo(JsonData data)
        {
            if (getChatRoomInfoEvent != null)
                getChatRoomInfoEvent(new ChatRoomInfo(data));
            
        }

        public void JoinChatRoom(JsonData data)
        {
            if (joinChatRoomEvent != null)
                joinChatRoomEvent(data);
        }

        
    }
}

