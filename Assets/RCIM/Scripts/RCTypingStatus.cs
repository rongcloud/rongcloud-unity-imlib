using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn_rongcloud_im_unity
{
    public class RCTypingStatus
    {
        public string UserId { get; set; }
        public string TypingContentType { get; set; }
        public Int64 SentTime { get; set; }

        public override string ToString()
        {
            return $"RCTypingStatus: userId {UserId} typingType {TypingContentType} sentTime {SentTime}";
        }
    }
}
