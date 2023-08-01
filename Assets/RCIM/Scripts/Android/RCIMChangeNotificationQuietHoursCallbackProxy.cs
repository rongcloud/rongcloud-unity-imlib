#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMChangeNotificationQuietHoursCallbackProxy : AndroidJavaProxy
    {
        OnNotificationQuietHoursChangedAction listener;
    
        public RCIMChangeNotificationQuietHoursCallbackProxy(OnNotificationQuietHoursChangedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWChangeNotificationQuietHoursCallback")
        {
            this.listener = listener;
        }
    
        public void onNotificationQuietHoursChanged(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
