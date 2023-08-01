#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMChangeUltraGroupChannelDefaultNotificationLevelCallbackProxy : AndroidJavaProxy
    {
        OnUltraGroupChannelDefaultNotificationLevelChangedAction listener;
    
        public RCIMChangeUltraGroupChannelDefaultNotificationLevelCallbackProxy(
            OnUltraGroupChannelDefaultNotificationLevelChangedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWChangeUltraGroupChannelDefaultNotificationLevelCallback")
        {
            this.listener = listener;
        }
    
        public void onUltraGroupChannelDefaultNotificationLevelChanged(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
