using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cn.rongcloud.sdk;
using UnityEngine.UI;
using LitJson;
public class RongCloudSdkExample : MonoBehaviour
{
    public Text m_txt;
    public InputField m_message;
    public Dropdown m_userID;
    private int eventID = 0;
    Dictionary<int, string> tokenList;
    string appKey = "";
    // Start is called before the first frame update
    void Start()
    {
        tokenList = new Dictionary<int, string>();
        tokenList.Add(1, "C5wpvVhmQvLM+AsjGNly3DXRx3bgpjzGSYViTKiqgRF5dSjO6H44VGJWa1m5kP+jEfFOUqfXKp1+jeD+WN78kFSHfHMPICiu");
        tokenList.Add(0, "B7aWXrrjJuZvUQVCpQxvK/felP0OtqTHtxnj+foXBLDzjx8bKffFEGtxQReTHhIxoLR2g5QCcQU=");
        RongCloud.init(appKey);
        RCEvent.getChatRoomInfoEvent += getChatRoomInfoReceive;
        RCEvent.connectionStatusEvent += getChatRoomInfo;
        RCEvent.joinChatRoomEvent += getChatRoomInfo;
        RCEvent.onReceiveMessageEvent += receiveMessage;
        RCEvent.sendMessageEvent += getChatRoomInfo;
        RCEvent.connectEvent += getChatRoomInfo;

    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnDestroy()
    {
        RCEvent.getChatRoomInfoEvent -= getChatRoomInfoReceive;
        RCEvent.connectionStatusEvent -= getChatRoomInfo;
        RCEvent.joinChatRoomEvent -= getChatRoomInfo;
        RCEvent.onReceiveMessageEvent -= receiveMessage;
        RCEvent.sendMessageEvent -= getChatRoomInfo;
        RCEvent.connectEvent -= getChatRoomInfo;
    }
    public void sendevent()
    {
        RCEvent.GetInstance().JoinChatRoom(null);
    }
    public void BtnConnect()
    {

        RongCloud.connect(tokenList[m_userID.value]);
    }
    public void BtnJoinChatRoom()
    {
        RongCloud.joinChatRoom("testRoom", 30);
    }
    public void BtnGetChatRoomInfo()
    {
        RongCloud.getChatRoomInfo("testRoom", 30, ChatRoomMemberOrder.RC_CHAT_ROOM_MEMBER_ASC);
    }

    public void BtnGetConnStatus()
    {
        m_txt.text+= "ConnStatus:" + RongCloud.getCurrentConnectionStatus()+"/n";
    }

    public void BtnLogOut()
    {
        RongCloud.logout();
    }

    public void BtnGetUserID()
    {
        m_txt.text+="UserID:"+ RongCloud.getCurrentUserId();
    }

    void getChatRoomInfo(JsonData msg)
    {
        m_txt.text += msg.ToJson();
    }
    void getChatRoomInfoReceive(ChatRoomInfo info)
    {
        m_txt.text += info.targetId +":::"+info.members.Count;
    }
    void receiveMessage(Message msg,int left)
    {
        if (msg.objectName == ObjectName.Text)
        {
            TextMessage content = (TextMessage)msg.content;
            m_txt.text += content.content + "  send:" + msg.senderUserId;
        }
        
    }
    public void BtnSendMessage()
    {
        TextMessage tm = new TextMessage();
        tm.content = m_message.text;
        RongCloud.sendMessage(ConversationType.CHATROOM, "testRoom", tm,(eventID++).ToString());
       
    }
}
