#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMSendMessageCallbackProxy : AndroidJavaProxy
    {
        RCIMSendMessageListener listener;
    
        public RCIMSendMessageCallbackProxy(RCIMSendMessageListener listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWSendMessageCallback")
        {
            this.listener = listener;
        }
    
        public void onMessageSaved(AndroidJavaObject message)
        {
            RCIMMessage _message = null;
            if (message != null)
                _message = (RCIMMessage)MessageConverter.from(message).getCSharpObject();
            if (listener != null)
                listener.OnMessageSaved(_message);
        }
    
        public void onMessageSent(int code, AndroidJavaObject message)
        {
            RCIMMessage _message = null;
            if (message != null)
                _message = (RCIMMessage)MessageConverter.from(message).getCSharpObject();
            if (listener != null)
                listener.OnMessageSent(code, _message);
        }
    }
}
#endif
