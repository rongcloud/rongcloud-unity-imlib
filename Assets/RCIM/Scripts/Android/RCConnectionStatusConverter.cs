using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn_rongcloud_im_unity
{
    internal class RCConnectionStatusConverter
    {
        internal static RCConnectionStatus Convert(int originCode)
        {
            if (originCode == 0)
            {
                return RCConnectionStatus.Connected;
            }
            else if (originCode == 1)
            {
                return RCConnectionStatus.Connecting;
            }
            else if (originCode == 12)
            {
                return RCConnectionStatus.DisConnected;
            }
            else if (originCode == 3)
            {
                return RCConnectionStatus.KickedByOtherClient;
            }
            else if (originCode == -1)
            {
                return RCConnectionStatus.NetworkUnavailable;
            }
            else if (originCode == 4)
            {
                return RCConnectionStatus.TokenIncorrect;
            }
            else if (originCode == 6)
            {
                return RCConnectionStatus.UserBlocked;
            }
            else if (originCode == 13)
            {
                return RCConnectionStatus.Suspend;
            }
            else if (originCode == 14)
            {
                return RCConnectionStatus.Timeout;
            }

            return RCConnectionStatus.Unknown;
        }
    }
}
