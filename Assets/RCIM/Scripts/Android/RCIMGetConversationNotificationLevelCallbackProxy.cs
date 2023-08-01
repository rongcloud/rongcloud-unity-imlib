#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMGetConversationNotificationLevelCallbackProxy : AndroidJavaProxy
    {
        RCIMGetConversationNotificationLevelListener listener;
    
        public RCIMGetConversationNotificationLevelCallbackProxy(RCIMGetConversationNotificationLevelListener listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWGetConversationNotificationLevelCallback")
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
