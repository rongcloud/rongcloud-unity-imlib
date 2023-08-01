#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMAddToBlacklistCallbackProxy : AndroidJavaProxy
    {
        OnBlacklistAddedAction listener;
    
        public RCIMAddToBlacklistCallbackProxy(OnBlacklistAddedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWAddToBlacklistCallback")
        {
            this.listener = listener;
        }
    
        public void onBlacklistAdded(int code, string userId)
        {
            if (listener != null)
                listener(code, userId);
        }
    }
}
#endif
