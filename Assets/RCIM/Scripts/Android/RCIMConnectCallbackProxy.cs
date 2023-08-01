#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMConnectCallbackProxy : AndroidJavaProxy
    {
        RCIMConnectListener listener;
    
        public RCIMConnectCallbackProxy(RCIMConnectListener listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWConnectCallback")
        {
            this.listener = listener;
        }
    
        public void onConnected(int code, string userId)
        {
            if (listener != null)
                listener.OnConnected(code, userId);
        }
    
        public void onDatabaseOpened(int code)
        {
            if (listener != null)
                listener.OnDatabaseOpened(code);
        }
    }
}
#endif
