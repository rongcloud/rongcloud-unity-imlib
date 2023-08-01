#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMGetBlacklistStatusCallbackProxy : AndroidJavaProxy
    {
        RCIMGetBlacklistStatusListener listener;
    
        public RCIMGetBlacklistStatusCallbackProxy(RCIMGetBlacklistStatusListener listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWGetBlacklistStatusCallback")
        {
            this.listener = listener;
        }
    
        public void onSuccess(AndroidJavaObject t)
        {
            RCIMBlacklistStatus _t = new BlacklistStatusConverter(t).getCSharpObject();
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
