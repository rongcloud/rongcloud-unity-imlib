#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMRemoveMessageExpansionForKeysCallbackProxy : AndroidJavaProxy
    {
        OnMessageExpansionForKeysRemovedAction listener;
    
        public RCIMRemoveMessageExpansionForKeysCallbackProxy(OnMessageExpansionForKeysRemovedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWRemoveMessageExpansionForKeysCallback")
        {
            this.listener = listener;
        }
    
        public void onMessageExpansionForKeysRemoved(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
