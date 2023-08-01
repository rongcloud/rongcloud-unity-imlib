#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMModifyUltraGroupMessageCallbackProxy : AndroidJavaProxy
    {
        OnUltraGroupMessageModifiedAction listener;
    
        public RCIMModifyUltraGroupMessageCallbackProxy(OnUltraGroupMessageModifiedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWModifyUltraGroupMessageCallback")
        {
            this.listener = listener;
        }
    
        public void onUltraGroupMessageModified(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
