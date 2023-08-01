using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public interface RCIMGetChatRoomAllEntriesListener : RCIMObjectListener<Dictionary<string, string>>
    {
    }
}
