#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMGetConversationTopStatusCallbackProxy : AndroidJavaProxy
    {
        RCIMGetConversationTopStatusListener listener;
    
        public RCIMGetConversationTopStatusCallbackProxy(RCIMGetConversationTopStatusListener listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWGetConversationTopStatusCallback")
        {
            this.listener = listener;
        }
    
        public void onSuccess(Boolean t)
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
