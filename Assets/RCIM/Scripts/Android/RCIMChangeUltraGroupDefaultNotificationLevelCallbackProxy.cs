#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMChangeUltraGroupDefaultNotificationLevelCallbackProxy : AndroidJavaProxy
    {
        OnUltraGroupDefaultNotificationLevelChangedAction listener;
    
        public RCIMChangeUltraGroupDefaultNotificationLevelCallbackProxy(
            OnUltraGroupDefaultNotificationLevelChangedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWChangeUltraGroupDefaultNotificationLevelCallback")
        {
            this.listener = listener;
        }
    
        public void onUltraGroupDefaultNotificationLevelChanged(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
