#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMChangePushContentShowStatusCallbackProxy : AndroidJavaProxy
    {
        OnPushContentShowStatusChangedAction listener;
    
        public RCIMChangePushContentShowStatusCallbackProxy(OnPushContentShowStatusChangedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWChangePushContentShowStatusCallback")
        {
            this.listener = listener;
        }
    
        public void onPushContentShowStatusChanged(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
