#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMClearUnreadCountCallbackProxy : AndroidJavaProxy
    {
        OnUnreadCountClearedAction listener;
    
        public RCIMClearUnreadCountCallbackProxy(OnUnreadCountClearedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWClearUnreadCountCallback")
        {
            this.listener = listener;
        }
    
        public void onUnreadCountCleared(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
