#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMGetDraftMessageCallbackProxy : AndroidJavaProxy
    {
        RCIMGetDraftMessageListener listener;
    
        public RCIMGetDraftMessageCallbackProxy(RCIMGetDraftMessageListener listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWGetDraftMessageCallback")
        {
            this.listener = listener;
        }
    
        public void onSuccess(string t)
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
