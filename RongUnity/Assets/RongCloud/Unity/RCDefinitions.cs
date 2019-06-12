using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cn.rongcloud.sdk
{
    public class ObjectName
    {
        /**
   * 文本消息
   */
        public static string Text = "RC:TxtMsg";

        /**
         * 文件消息
         */
        public static string File = "RC:FileMsg";

        /**
         * 图片消息
         */
        public static string Image = "RC:ImgMsg";

        /**
         * 位置信息
         */
        public static string Location = "RC:LBSMsg";

        /**
         * 语音消息
         */
        public static string Voice = "RC:VcMsg";

        /**
         * 小视频消息
         */
        public static string Sight = "RC:SightMsg";

        /**
         * 命令消息
         */
        public static string Command = "RC:CmdMsg";

        /**
         * 公众服务单图文消息
         */
        public static string PublicServiceRich = "RC:PSImgTxtMsg";

        /**
         * 公众服务多图文消息
         */
        public static string PublicServiceMultiRich = "RC:PSMultiImgTxtMsg";

        /**
         * 好友通知消息
         */
        public static string ContactNotification = "RC:ContactNtf";

        /**
         * 资料通知消息
         */
        public static string ProfileNotification = "RC:ProfileNtf";

        /**
         * 通用命令通知消息
         */
        public static string CommandNotification = "RC:CmdNtf";

        /**
         * 提示条通知消息
         */
        public static string InformationNotification = "RC:InfoNtf";

        /**
         * 群组通知消息
         */
        public static string GroupNotification = "RC:GrpNtf";

        /**
         * 已读通知消息
         */
        public static string ReadReceipt = "RC:ReadNtf";

        /**
         * 公众服务命令消息
         */
        public static string PublicServiceCommand = "RC:PSCmd";

        /**
         * 对方正在输入状态消息
         */
        public static string TypingStatus = "RC:TypSts";

        /**
         * 群消息已读状态回执
         */
        public static string ReadReceiptResponse = "RC:RRRspMsg";
    }
    public enum MentionedType
    {
        /**
         * 提醒所有
         */
        ALL = 1,

        /**
         * 部分提醒
         */
        PART
    }
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo
    {
        public string userId;
        public string name;
        public string portraitUrl;
    }
    public class MentionedInfo
    {
        public MentionedType type;
        public string[] userIdList;
        public string mentionedContent;
    }
    public class MessageContent
    {
        public string objectName;
        public  UserInfo userInfo;
        public MentionedInfo mentionedInfo;
        /*
        public string content;
        public string extra;
        public string local;
        public bool isFull;
        public string thumbnail;
        public double latitude;
        public double longitude;
        public string name;
        public int duration;
        public string data;
        */
    }
    /// <summary>
    /// 文本消息
    /// </summary>
    public class TextMessage : MessageContent
    {
        public string content;
        public string extra;
        public TextMessage()
        {
            objectName = ObjectName.Text;
        }

    }
    /// <summary>
    /// 图片消息
    /// </summary>
    public class ImageMessage : MessageContent
    {
        public string local;
        public string remote;
        public bool isFull;
        public string thumbnail;
        public string extra;
        public ImageMessage()
        {
            objectName = ObjectName.Image;
        }
    }
    /// <summary>
    /// 文件消息
    /// </summary>
    public class FileMessage : MessageContent
    {
        public string local;
        public string remote;
        public string name;
        public string size;
        public string fileType;
        public string extra;
        public FileMessage()
        {
            objectName = ObjectName.File;
        }
    }
    /// <summary>
    /// 消息
    /// </summary>
    public class Message
    {
        public ConversationType conversationType;
        public string objectName;
        public long messageId;
        public string messageUId;
        public MessageDirection messageDirection;
        public string senderUserId;
        public string sendTime;
        public string targetId;
        public long receivedTime;
        public MessageContent content;
        public string extra;
        public Message() { }
        public Message(JsonData data)
        {
            Message msg = JsonUtility.FromJson<Message>(data.ToJson());
            conversationType = msg.conversationType;
            objectName = msg.objectName;
            messageId = msg.messageId;
            messageUId = msg.messageUId;
            messageDirection = msg.messageDirection;
            senderUserId = msg.senderUserId;
            sendTime = msg.sendTime;
            targetId = msg.targetId;
            receivedTime = msg.receivedTime;
            extra = msg.extra;
            JsonData contentJson = data["content"];
            switch (msg.objectName)
            {
                case "RC:TxtMsg":
                    TextMessage TMcontent = new TextMessage();
                    TMcontent.content = contentJson["content"].ToString();
                    TMcontent.extra = SaftGetValue("extra", contentJson);
                    content = TMcontent;
                    break;
                case "RC:ImgMsg":
                    ImageMessage IMtmp = new ImageMessage();
                    IMtmp.extra = SaftGetValue("extra", contentJson);
                    IMtmp.local = SaftGetValue("local", contentJson);
                    IMtmp.isFull = bool.Parse (contentJson["isFull"].ToString());
                    content = IMtmp;
                    break;
                case "RC:FileMsg":
                    FileMessage FMtmp = new FileMessage();
                    FMtmp.extra = SaftGetValue("extra", contentJson);
                    FMtmp.local = SaftGetValue("local", contentJson); 
                    content = FMtmp;
                    break;
                case "RC:LBSMsg":
                   
                    break;
                case "RC:VcMsg":
                    
                    break;
                case "RC:CmdMsg":
                    
                    break;
            }

        }
        string SaftGetValue(string key,JsonData data)
        {
            JsonData val = data[key];
            if (val == null)
                return "";
            return val.ToString();
        }
    }

    public class ChatRoomInfo
    {
        public string targetId;
        public int totalMemberCount;
        public ChatRoomMemberOrder memberOrder;
        public List<ChatRoomMemberInfo> members;
        public class ChatRoomMemberInfo
        {
            public string userId;
            public long joinTime;
        }
        public ChatRoomInfo() { }
        public ChatRoomInfo(JsonData data)
        {
            ChatRoomInfo info= JsonUtility.FromJson<ChatRoomInfo>(data.ToJson());
            targetId = info.targetId;
            totalMemberCount = info.totalMemberCount;
            memberOrder = info.memberOrder;
            JsonData ms = data["members"];
            members = new List<ChatRoomMemberInfo>();
            for(int i = 0; i < ms.Count; i++)
            {
                ChatRoomMemberInfo m = new ChatRoomMemberInfo();
                m.userId = ms[i]["userId"].ToString();
                m.joinTime =long.Parse( ms[i]["joinTime"].ToString());
                members.Add(m);
            }
        }

        
    }
    
    public enum ConversationType
    {
        /*!
     单聊
     */
        PRIVATE = 1,

        /*!
         讨论组
         */
        DISCUSSION = 2,

        /*!
         群组
         */
        GROUP = 3,

        /*!
         聊天室
         */
        CHATROOM = 4,

        /*!
         客服
         */
        CUSTOMERSERVICE = 5,

        /*!
         系统会话
         */
        SYSTEM = 6,

        /*!
         应用内公众服务会话

         @discussion
         客服2.0使用应用内公众服务会话（ConversationType_APPSERVICE）的方式实现。
         即客服2.0会话是其中一个应用内公众服务会话，这种方式我们目前不推荐，请尽快升级到新客服，升级方法请参考官网的客服文档。
         */
        APPSERVICE = 7,

        /*!
         跨应用公众服务会话
         */
        PUBLICSERVICE = 8,

        /*!
         推送服务会话
         */
        PUSHSERVICE = 9,

        /*!
         加密会话（仅对部分私有云用户开放，公有云用户不适用）
         */
        Encrypted = 11,

        /**
         * RTC 会话
         */
        RTC = 12,

        /*!
         无效类型
         */
        INVALID
    }
    public enum MessageDirection
    {
        SEND=1,
        RECEIVE
    }
    public enum ChatRoomMemberOrder
    {
        RC_CHAT_ROOM_MEMBER_ASC=1,
        RC_CHAT_ROOM_MEMBER_DESC
    }
    public enum ErrorCode
    {
        APP_NOT_CONNECT=-4,
        PARAMETER_ERROR=-3,
        IPC_DISCONNECT=-2,
        UNKNOWN=-1,
        CONNECTED=0,
        MSG_SEND_OVERFREQUENCY=20604,
        RC_OPERATION_BLOCKED=20605,
        RC_OPERATION_NOT_SUPPORT=20606,
        MSG_ROAMING_SERVICE_UNAVAILABLE=33007,
        NOT_IN_DISCUSSION=21406,
        RC_MSG_BLOCKED_SENSITIVE_WORD=21501,
        RC_MSG_REPLACED_SENSITIVE_WORD=21502,
        NOT_IN_GROUP=22406,
        FORBIDDEN_IN_GROUP=22408,
        NOT_IN_CHATROOM=23406,
        FORBIDDEN_IN_CHATROOM=23408,
        KICKED_FROM_CHATROOM=23409, 
        RC_CHATROOM_NOT_EXIST=23410,
        RC_CHATROOM_IS_FULL=23411,
        RC_CHATROOM_ILLEGAL_ARGUMENT=23412,
        REJECTED_BY_BLACKLIST=405,
        RC_NET_CHANNEL_INVALID=30001, 
        RC_NET_UNAVAILABLE=30002,
        RC_MSG_RESP_TIMEOUT=30003,
        RC_HTTP_SEND_FAIL=30004,
        RC_HTTP_REQ_TIMEOUT=30005,
        RC_HTTP_RECV_FAIL=30006, 
        RC_NAVI_RESOURCE_ERROR=30007, 
        RC_NODE_NOT_FOUND=30008, 
        RC_DOMAIN_NOT_RESOLVE=30009,
        RC_SOCKET_NOT_CREATED=30010,
        RC_SOCKET_DISCONNECTED=30011,
        RC_PING_SEND_FAIL=30012,
        RC_PONG_RECV_FAIL=30013,
        RC_MSG_SEND_FAIL=30014,
        RC_CONN_OVERFREQUENCY=30015,
        RC_MSG_SIZE_OUT_OF_LIMIT=30016, 
        RC_NETWORK_IS_DOWN_OR_UNREACHABLE=30019,
        RC_CONN_ACK_TIMEOUT=31000,
        RC_CONN_PROTO_VERSION_ERROR=31001,
        RC_CONN_ID_REJECT=31002, 
        RC_CONN_SERVER_UNAVAILABLE=31003,
        RC_CONN_USER_OR_PASSWD_ERROR=31004, 
        RC_CONN_NOT_AUTHRORIZED=31005,
        RC_CONN_REDIRECTED=31006,
        RC_CONN_PACKAGE_NAME_INVALID=31007,
        RC_CONN_APP_BLOCKED_OR_DELETED=31008,
        RC_CONN_USER_BLOCKED=31009, 
        RC_DISCONN_KICK=31010,
        RC_DISCONN_USER_BLOCKED=31011,
        RC_CONN_OTHER_DEVICE_LOGIN=31023, 
        RC_QUERY_ACK_NO_DATA=32001, 
        RC_MSG_DATA_INCOMPLETE=32002,
        RC_CONN_REFUSED=32061,
        RC_CONNECTION_RESET_BY_PEER=32054,
        BIZ_ERROR_CLIENT_NOT_INIT=33001,
        BIZ_ERROR_DATABASE_ERROR=33002,
        BIZ_ERROR_INVALID_PARAMETER=33003,
        BIZ_ERROR_NO_CHANNEL=33004,
        BIZ_ERROR_RECONNECT_SUCCESS=33005,
        BIZ_ERROR_CONNECTING=33006, 
        NOT_FOLLOWED=29106,
        PARAMETER_INVALID_CHATROOM=23412,
        ROAMING_SERVICE_UNAVAILABLE_CHATROOM=23414, 
        RC_RECALL_PARAMETER_INVALID=25101,
        RC_PUSHSETTING_PARAMETER_INVALID=26001,
        RC_PUSHSETTING_CONFIG_NOT_OPEN=26002, 
        RC_SIGHT_SERVICE_UNAVAILABLE=26101,
        RC_SIGHT_MSG_DURATION_LIMIT_EXCEED=34002
    }
    public  enum ConnectionStatus
    {
        NETWORK_UNAVAILABLE=-1,
        CONNECTED=0, 
        CONNECTING=1,
        DISCONNECTED=2, 
        KICKED_OFFLINE_BY_OTHER_CLIENT=3, 
        TOKEN_INCORRECT=4, 
        SERVER_INVALID=5,
        CONN_USER_BLOCKED=6
    }


}
