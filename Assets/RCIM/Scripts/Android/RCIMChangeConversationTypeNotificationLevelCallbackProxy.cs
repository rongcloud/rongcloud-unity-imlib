#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMChangeConversationTypeNotificationLevelCallbackProxy : AndroidJavaProxy
    {
        OnConversationTypeNotificationLevelChangedAction listener;
    
        public RCIMChangeConversationTypeNotificationLevelCallbackProxy(
            OnConversationTypeNotificationLevelChangedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWChangeConversationTypeNotificationLevelCallback")
        {
            this.listener = listener;
        }
    
        public void onConversationTypeNotificationLevelChanged(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
