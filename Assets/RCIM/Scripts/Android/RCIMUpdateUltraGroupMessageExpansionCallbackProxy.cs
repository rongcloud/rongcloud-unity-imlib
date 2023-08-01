#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMUpdateUltraGroupMessageExpansionCallbackProxy : AndroidJavaProxy
    {
        OnUltraGroupMessageExpansionUpdatedAction listener;
    
        public RCIMUpdateUltraGroupMessageExpansionCallbackProxy(OnUltraGroupMessageExpansionUpdatedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWUpdateUltraGroupMessageExpansionCallback")
        {
            this.listener = listener;
        }
    
        public void onUltraGroupMessageExpansionUpdated(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
