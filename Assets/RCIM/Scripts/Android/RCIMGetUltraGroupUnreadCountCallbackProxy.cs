#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMGetUltraGroupUnreadCountCallbackProxy : AndroidJavaProxy
    {
        RCIMGetUltraGroupUnreadCountListener listener;
    
        public RCIMGetUltraGroupUnreadCountCallbackProxy(RCIMGetUltraGroupUnreadCountListener listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWGetUltraGroupUnreadCountCallback")
        {
            this.listener = listener;
        }
    
        public void onSuccess(int t)
        {
            if (listener != null)
                listener.OnSuccess(t);
        }
    
        public void onError(int code)
        {
            if (listener != null)
                listener.OnError(code);
        }
    }
}
#endif
