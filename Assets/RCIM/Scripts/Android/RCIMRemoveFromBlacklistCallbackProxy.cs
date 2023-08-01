#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMRemoveFromBlacklistCallbackProxy : AndroidJavaProxy
    {
        OnBlacklistRemovedAction listener;
    
        public RCIMRemoveFromBlacklistCallbackProxy(OnBlacklistRemovedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWRemoveFromBlacklistCallback")
        {
            this.listener = listener;
        }
    
        public void onBlacklistRemoved(int code, string userId)
        {
            if (listener != null)
                listener(code, userId);
        }
    }
}
#endif
