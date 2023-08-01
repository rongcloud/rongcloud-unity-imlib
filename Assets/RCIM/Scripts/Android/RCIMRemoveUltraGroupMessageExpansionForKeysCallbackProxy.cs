#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMRemoveUltraGroupMessageExpansionForKeysCallbackProxy : AndroidJavaProxy
    {
        OnUltraGroupMessageExpansionForKeysRemovedAction listener;
    
        public RCIMRemoveUltraGroupMessageExpansionForKeysCallbackProxy(
            OnUltraGroupMessageExpansionForKeysRemovedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWRemoveUltraGroupMessageExpansionForKeysCallback")
        {
            this.listener = listener;
        }
    
        public void onUltraGroupMessageExpansionForKeysRemoved(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
