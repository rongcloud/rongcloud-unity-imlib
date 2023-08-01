#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMGetUltraGroupDefaultNotificationLevelCallbackProxy : AndroidJavaProxy
    {
        RCIMGetUltraGroupDefaultNotificationLevelListener listener;
    
        public RCIMGetUltraGroupDefaultNotificationLevelCallbackProxy(
            RCIMGetUltraGroupDefaultNotificationLevelListener listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWGetUltraGroupDefaultNotificationLevelCallback")
        {
            this.listener = listener;
        }
    
        public void onSuccess(AndroidJavaObject t)
        {
            RCIMPushNotificationLevel _t = new PushNotificationLevelConverter(t).getCSharpObject();
            if (listener != null)
                listener.OnSuccess(_t);
        }
    
        public void onError(int code)
        {
            if (listener != null)
                listener.OnError(code);
        }
    }
}
#endif
