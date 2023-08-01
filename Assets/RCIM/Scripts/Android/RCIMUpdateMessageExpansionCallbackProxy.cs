#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMUpdateMessageExpansionCallbackProxy : AndroidJavaProxy
    {
        OnMessageExpansionUpdatedAction listener;
    
        public RCIMUpdateMessageExpansionCallbackProxy(OnMessageExpansionUpdatedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWUpdateMessageExpansionCallback")
        {
            this.listener = listener;
        }
    
        public void onMessageExpansionUpdated(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
