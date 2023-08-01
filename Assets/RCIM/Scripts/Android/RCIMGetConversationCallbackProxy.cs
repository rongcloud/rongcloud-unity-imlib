#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMGetConversationCallbackProxy : AndroidJavaProxy
    {
        RCIMGetConversationListener listener;
    
        public RCIMGetConversationCallbackProxy(RCIMGetConversationListener listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWGetConversationCallback")
        {
            this.listener = listener;
        }
    
        public void onSuccess(AndroidJavaObject t)
        {
            RCIMConversation _t = null;
            if (t != null)
                _t = (RCIMConversation)ConversationConverter.from(t).getCSharpObject();
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
