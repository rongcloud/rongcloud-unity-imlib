using cn_rongcloud_im_unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace cn_rongcloud_im_unity_example
{
    public class LoginData
    {
        [JsonProperty("code")] public int Code { get; set; }

        public string Name { get; set; }

        [SerializeField] private String userId;

        public String Id
        {
            get { return userId; }
            set { userId = value; }
        }

        [SerializeField] private String token;

        public String Token
        {
            get { return token; }
            set { token = value; }
        }
    }

    internal static class DemoContext
    {
        public static bool AppInitialized { get; set; }

        /// <summary>
        /// 当前显示的 scene 名称
        /// </summary>
        public static string activeSceneName { get; private set; }

        private static RCConversation curConversation;

        public static RCConversation CurrentConversation
        {
            get => curConversation;
            set
            {
                curConversation = value;
                if (value == null)
                {
                    return;
                }

                CurrentConversationType = value.ConversationType;
                if (CurrentConversationType == RCConversationType.Group)
                {
                    GroupTargetId = value.TargetId;
                }
                else if (CurrentConversationType == RCConversationType.Private)
                {
                    PrivateTargetId = value.TargetId;
                }
                else if (CurrentConversationType == RCConversationType.ChatRoom)
                {
                    ChatRoomId = value.TargetId;
                }
            }
        }

        public static RCConversationType CurrentConversationType { get; set; }
        public static IList<RCConversation> ConversationList { get; set; }
        public static IList<RCTagInfo> TagList { get; set; }

        public static TagConversationContext TagConversationEditContext { get; set; } = new TagConversationContext();
        
        public static IList<string> BlockedUserList { get; set; }
        public static LoginData CurrentUser { get; set; }

        public static String DefaultLoginUserId
        {
            get
            {
#if UNITY_ANDROID
                return "TzXjE1t9O";
#elif UNITY_IOS
                return "QcVQkcuCd";
#endif
                return "";
            }
        }

        public static LoginData User1 { get; set; } = new LoginData()
        {
            Name = "zzq sealtalk 2",
            Id = "VHA2oBZwJ",
            Token = "7hph4IJbO464AOSV16StmuF+lwb3rejusiLNEhp1WUE=@h4mx.cn.rongnav.com;h4mx.cn.rongcfg.com",
        };

        public static LoginData User3 { get; set; } = new LoginData()
        {
            Name = "zzq sealtalk 152",
            Id = "TzXjE1t9O",
            Token = "DH8fVTp9tEDLhTQ6VopBoOF+lwb3rejupJMxrQgnVNE=@h4mx.cn.rongnav.com;h4mx.cn.rongcfg.com",
        };

        public static LoginData User2 { get; set; } = new LoginData()
        {
            Name = "haifeng sealtalk 1",
            Id = "QcVQkcuCd",
            Token = "wvfdY3BAPaM9ivfw8vBb9eF+lwb3rejuPmROX/B/5A0=@h4mx.cn.rongnav.com;h4mx.cn.rongcfg.com",
        };

        public static LoginData User4 { get; set; } = new LoginData()
        {
            Name = "haifeng sealtalk 2",
            Id = "sV2XrVzwG",
            Token = "cMY9/O1xJrMry5weKd9KmuF+lwb3rejuSCmV+AIZgxQ=@h4mx.cn.rongnav.com;h4mx.cn.rongcfg.com",
        };

        public static string TargetId
        {
            get
            {
                if (CurrentConversationType == RCConversationType.Group)
                {
                    return GroupTargetId;
                }
                else if (CurrentConversationType == RCConversationType.Private)
                {
                    return PrivateTargetId;
                }
                else if (CurrentConversationType == RCConversationType.ChatRoom)
                {
                    return ChatRoomId;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public static string GroupTargetId { get; set; }

        public static string ChatRoomId { get; set; }

        private static string privateTargetId = "sV2XrVzwG";

        public static string PrivateTargetId
        {
            get => privateTargetId;
            set => privateTargetId = value;
        }

        public static void SetDefaultPrivateId()
        {
            if (CurrentUser == User2)
            {
                privateTargetId = "sV2XrVzwG";
            }
            else if (CurrentUser == User3)
            {
                privateTargetId = "VHA2oBZwJ";
            }
            else
            {
                privateTargetId = "";
            }
        }

        public static bool Connected { get; set; }

        public static MonoBehaviour activeScene { get; private set; }
        public static Example exampleScene { get; set; }
        public static ConversationPage conversationPageScene { get; set; }

        public static void SetActiveScene(string name, MonoBehaviour behaviour)
        {
            activeSceneName = name;
            activeScene = behaviour;
        }

        public static RCConversation GetCurrentConversationByIdx(string idxStr)
        {
            var idx = 0;
            if (!Int32.TryParse(idxStr, out idx))
            {
                return null;
            }

            if (ConversationList != null && idx >= 0 && idx < ConversationList.Count)
            {
                return ConversationList[idx];
            }

            return null;
        }
    }

    internal static class ChatRoomState
    {
        public static bool IsJoined { get; set; }
        public static string RoomId { get; set; }
    }
}