#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMClearMessagesCallbackProxy : AndroidJavaProxy
    {
        OnMessagesClearedAction listener;
    
        public RCIMClearMessagesCallbackProxy(OnMessagesClearedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWClearMessagesCallback")
        {
            this.listener = listener;
        }
    
        public void onMessagesCleared(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
