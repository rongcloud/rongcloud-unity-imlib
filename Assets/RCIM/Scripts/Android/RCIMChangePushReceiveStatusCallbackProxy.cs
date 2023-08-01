#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMChangePushReceiveStatusCallbackProxy : AndroidJavaProxy
    {
        OnPushReceiveStatusChangedAction listener;
    
        public RCIMChangePushReceiveStatusCallbackProxy(OnPushReceiveStatusChangedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWChangePushReceiveStatusCallback")
        {
            this.listener = listener;
        }
    
        public void onPushReceiveStatusChanged(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
