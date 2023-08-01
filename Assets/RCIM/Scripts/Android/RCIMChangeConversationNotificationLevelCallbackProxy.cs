#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMChangeConversationNotificationLevelCallbackProxy : AndroidJavaProxy
    {
        OnConversationNotificationLevelChangedAction listener;
    
        public RCIMChangeConversationNotificationLevelCallbackProxy(OnConversationNotificationLevelChangedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWChangeConversationNotificationLevelCallback")
        {
            this.listener = listener;
        }
    
        public void onConversationNotificationLevelChanged(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
