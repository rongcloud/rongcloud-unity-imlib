using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn_rongcloud_im_unity
{
    public class BlockedMessageInfo
    {
        public RCConversationType Type { get; internal set; }
        public string TargetId { get; internal set; }
        public string MessageUid { get; private set; }
        public RCBlockType BlockType { get; internal set; }
        public string Extra { get; internal set; }

        private BlockedMessageInfo()
        {

        }
        public BlockedMessageInfo(RCConversationType type, string targetId, string blockMsgUid, RCBlockType blockType, string extra) : this()
        {
            this.Type = type;
            this.TargetId = targetId;
            this.MessageUid = blockMsgUid;
            this.BlockType = blockType;
            this.Extra = extra;
        }
    }
}