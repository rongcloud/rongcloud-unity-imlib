#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMGetUnreadCountByConversationTypesCallbackProxy : AndroidJavaProxy
    {
        RCIMGetUnreadCountByConversationTypesListener listener;
    
        public RCIMGetUnreadCountByConversationTypesCallbackProxy(RCIMGetUnreadCountByConversationTypesListener listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWGetUnreadCountByConversationTypesCallback")
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
