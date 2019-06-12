using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
namespace cn.rongcloud.sdk
{
    public class RongCloudListerner : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void CommCallback(string msg)
        {
            Debug.LogError(msg);
            JsonData msgdata = JsonMapper.ToObject(msg);
            string cmd = msgdata["callback"].ToString();
            JsonData data = msgdata["data"];
            switch (cmd)
            {
                case "ConnectionStatus":  //连接状态
                    RCEvent.GetInstance().ConnectionStatus(data);
                    break;
                case "OnReceiveMessageListerner":
                    RCEvent.GetInstance().OnReceiveMessage(data);
                    break;
                case "connect":
                    RCEvent.GetInstance().Connect(data);
                    break;
                case "SendMessageCallback":
                    RCEvent.GetInstance().SendMessage(data);
                    break;
                case "getChatRoomInfo":
                    RCEvent.GetInstance().GetChatRoomInfo(data);
                    break;
                case "joinChatRoom":
                    RCEvent.GetInstance().JoinChatRoom(data);
                    break;
                default:
                    break;
            }
        }
    }
}

