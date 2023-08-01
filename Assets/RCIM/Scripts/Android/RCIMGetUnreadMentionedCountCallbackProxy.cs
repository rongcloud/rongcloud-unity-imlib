#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMGetUnreadMentionedCountCallbackProxy : AndroidJavaProxy
    {
        RCIMGetUnreadMentionedCountListener listener;
    
        public RCIMGetUnreadMentionedCountCallbackProxy(RCIMGetUnreadMentionedCountListener listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWGetUnreadMentionedCountCallback")
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
