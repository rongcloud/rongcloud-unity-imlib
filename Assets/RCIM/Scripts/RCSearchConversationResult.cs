using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn_rongcloud_im_unity
{
    public class RCSearchConversationResult
    {
        public RCConversation Conversation { get; private set; }
        public int MatchCount { get; private set; }

        public RCSearchConversationResult(RCConversation conversation, int matchCount)
        {
            this.Conversation = conversation;
            this.MatchCount = matchCount;
        }
    }
}
