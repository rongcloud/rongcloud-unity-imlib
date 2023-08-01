#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMRemoveNotificationQuietHoursCallbackProxy : AndroidJavaProxy
    {
        OnNotificationQuietHoursRemovedAction listener;
    
        public RCIMRemoveNotificationQuietHoursCallbackProxy(OnNotificationQuietHoursRemovedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWRemoveNotificationQuietHoursCallback")
        {
            this.listener = listener;
        }
    
        public void onNotificationQuietHoursRemoved(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
