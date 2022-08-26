using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn_rongcloud_im_unity
{
    public enum MentionedType
    {
        None = 0,
        All = 1,
        Part = 2
    }

    public class RCMentionedInfo
    {
        public MentionedType Type { get; private set; }
        public IList<String> UserIdList { get; private set; }
        public String MentionedContent { get; private set; }

        private RCMentionedInfo()
        {

        }

        public RCMentionedInfo(MentionedType type, IList<string> userIdList, string mentionedContent)
        {
            this.Type = type;
            this.UserIdList = userIdList;
            this.MentionedContent = mentionedContent;
        }

        public override string ToString()
        {
            return $"{Type} {MentionedContent} mentionedUser {UserIdList?.Count}";
        }
    }
}
