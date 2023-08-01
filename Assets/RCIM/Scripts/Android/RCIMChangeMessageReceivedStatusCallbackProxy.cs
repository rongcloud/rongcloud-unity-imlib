#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMChangeMessageReceivedStatusCallbackProxy : AndroidJavaProxy
    {
        OnMessageReceiveStatusChangedAction listener;
    
        public RCIMChangeMessageReceivedStatusCallbackProxy(OnMessageReceiveStatusChangedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWChangeMessageReceivedStatusCallback")
        {
            this.listener = listener;
        }
    
        public void onMessageReceiveStatusChanged(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
