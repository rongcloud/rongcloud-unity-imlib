#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMChangeMessageSentStatusCallbackProxy : AndroidJavaProxy
    {
        OnMessageSentStatusChangedAction listener;
    
        public RCIMChangeMessageSentStatusCallbackProxy(OnMessageSentStatusChangedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWChangeMessageSentStatusCallback")
        {
            this.listener = listener;
        }
    
        public void onMessageSentStatusChanged(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
