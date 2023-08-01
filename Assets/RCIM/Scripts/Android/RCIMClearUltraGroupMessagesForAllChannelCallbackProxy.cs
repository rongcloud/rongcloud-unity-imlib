#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMClearUltraGroupMessagesForAllChannelCallbackProxy : AndroidJavaProxy
    {
        OnUltraGroupMessagesClearedForAllChannelAction listener;
    
        public RCIMClearUltraGroupMessagesForAllChannelCallbackProxy(
            OnUltraGroupMessagesClearedForAllChannelAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWClearUltraGroupMessagesForAllChannelCallback")
        {
            this.listener = listener;
        }
    
        public void onUltraGroupMessagesClearedForAllChannel(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
