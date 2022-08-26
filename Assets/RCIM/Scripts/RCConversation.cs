using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn_rongcloud_im_unity
{
    public class RCConversation
    {
        public RCConversationType ConversationType { get; internal set; }

        public string TargetId { get; internal set; }

        public int UnreadMessageCount { get; internal set; }

        public RCReceivedStatus ReceivedStatus { get; internal set; }
        public RCSentStatus SentStatus { get; internal set; }

        public Int64 SentTime { get; internal set; }
        public Int64 ReceivedTime { get; internal set; }

        public bool IsTop { get; internal set; }

        public string ObjectName { get; internal set; }
        public string SenderUserId { get; internal set; }
        public Int64 LatestMessageId { get; internal set; }
        public RCMessageContent LatestMessageContent { get; internal set; }

        public int MentionedCount { get; internal set; }
        public string Draft { get; internal set; }
        public RCConversationNotificationStatus NotificationStatus { get; internal set; }

        public RCConversation()
        {

        }

        public RCConversation(RCConversationType type, string targetId)
        {
            this.ConversationType = type;
            this.TargetId = targetId;
        }

        public override string ToString()
        {
            return $"Conversation: type {ConversationType} targetId: {TargetId} content: [ {LatestMessageContent} ]";
        }
    }

    public class RCConversationIdentifier
    {
        public RCConversationType Type { get; set; }
        public string TargetId { get; set; }

        public RCConversationIdentifier(RCConversationType type, string targetId)
        {
            this.Type = type;
            this.TargetId = targetId;
        }
    }
}
