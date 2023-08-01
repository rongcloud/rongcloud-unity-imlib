#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMGetFirstUnreadMessageCallbackProxy : AndroidJavaProxy
    {
        RCIMGetFirstUnreadMessageListener listener;
    
        public RCIMGetFirstUnreadMessageCallbackProxy(RCIMGetFirstUnreadMessageListener listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWGetFirstUnreadMessageCallback")
        {
            this.listener = listener;
        }
    
        public void onSuccess(AndroidJavaObject t)
        {
            RCIMMessage _t = null;
            if (t != null)
                _t = (RCIMMessage)MessageConverter.from(t).getCSharpObject();
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
