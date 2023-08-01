#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMGetNotificationQuietHoursCallbackProxy : AndroidJavaProxy
    {
        RCIMGetNotificationQuietHoursListener listener;
    
        public RCIMGetNotificationQuietHoursCallbackProxy(RCIMGetNotificationQuietHoursListener listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWGetNotificationQuietHoursCallback")
        {
            this.listener = listener;
        }
    
        public void onSuccess(string startTime, int spanMinutes, AndroidJavaObject level)
        {
            RCIMPushNotificationQuietHoursLevel _level =
                new PushNotificationQuietHoursLevelConverter(level).getCSharpObject();
            if (listener != null)
                listener.OnSuccess(startTime, spanMinutes, _level);
        }
    
        public void onError(int code)
        {
            if (listener != null)
                listener.OnError(code);
        }
    }
}
#endif
