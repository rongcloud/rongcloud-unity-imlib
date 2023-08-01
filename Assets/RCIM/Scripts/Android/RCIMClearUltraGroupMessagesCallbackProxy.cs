#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMClearUltraGroupMessagesCallbackProxy : AndroidJavaProxy
    {
        OnUltraGroupMessagesClearedAction listener;
    
        public RCIMClearUltraGroupMessagesCallbackProxy(OnUltraGroupMessagesClearedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWClearUltraGroupMessagesCallback")
        {
            this.listener = listener;
        }
    
        public void onUltraGroupMessagesCleared(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
