using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn_rongcloud_im_unity
{
    public class RCReadReceiptInfo
    {
        public bool IsReceiptRequestMessage { get; set; }
        public bool HasRespond { get; set; }

        public Dictionary<string, Int64> RespondUserIdList { get; set; }
    }
}
