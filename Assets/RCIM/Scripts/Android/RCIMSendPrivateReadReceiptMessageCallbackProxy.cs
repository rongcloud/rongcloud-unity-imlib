#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMSendPrivateReadReceiptMessageCallbackProxy : AndroidJavaProxy
    {
        OnPrivateReadReceiptMessageSentAction listener;
    
        public RCIMSendPrivateReadReceiptMessageCallbackProxy(OnPrivateReadReceiptMessageSentAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWSendPrivateReadReceiptMessageCallback")
        {
            this.listener = listener;
        }
    
        public void onPrivateReadReceiptMessageSent(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
