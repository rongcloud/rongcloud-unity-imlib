#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMSendUltraGroupTypingStatusCallbackProxy : AndroidJavaProxy
    {
        OnUltraGroupTypingStatusSentAction listener;
    
        public RCIMSendUltraGroupTypingStatusCallbackProxy(OnUltraGroupTypingStatusSentAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWSendUltraGroupTypingStatusCallback")
        {
            this.listener = listener;
        }
    
        public void onUltraGroupTypingStatusSent(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
