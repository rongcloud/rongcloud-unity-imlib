#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMRecallUltraGroupMessageCallbackProxy : AndroidJavaProxy
    {
        OnUltraGroupMessageRecalledAction listener;
    
        public RCIMRecallUltraGroupMessageCallbackProxy(OnUltraGroupMessageRecalledAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWRecallUltraGroupMessageCallback")
        {
            this.listener = listener;
        }
    
        public void onUltraGroupMessageRecalled(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
