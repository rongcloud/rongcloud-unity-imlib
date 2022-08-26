using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn_rongcloud_im_unity
{
    public class RCUserInfo
    {
        public string UserId { get; private set; }
        public string Name { get; private set; }
        public string PortraitUri { get; private set; }

        public string Extra { get; private set; }

        private RCUserInfo()
        {

        }

        public RCUserInfo(string userId, string name, string portraitUri, string extra)
        {
            this.UserId = userId;
            this.Name = name;
            this.PortraitUri = portraitUri;
            this.Extra = extra;
        }
        
        
    }
}
