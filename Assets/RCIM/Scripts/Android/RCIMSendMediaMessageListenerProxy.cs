#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMSendMediaMessageListenerProxy : AndroidJavaProxy
    {
        RCIMSendMediaMessageListener listener;
    
        public RCIMSendMediaMessageListenerProxy(RCIMSendMediaMessageListener listener)
            : base("cn.rongcloud.im.wrapper.listener.IRCIMIWSendMediaMessageListener")
        {
            this.listener = listener;
        }
    
        public void onMediaMessageSaved(AndroidJavaObject message)
        {
            RCIMMediaMessage _message = null;
            if (message != null)
                _message = (RCIMMediaMessage)MediaMessageConverter.from(message).getCSharpObject();
            if (listener != null)
                listener.OnMediaMessageSaved(_message);
        }
    
        public void onMediaMessageSending(AndroidJavaObject message, int progress)
        {
            RCIMMediaMessage _message = null;
            if (message != null)
                _message = (RCIMMediaMessage)MediaMessageConverter.from(message).getCSharpObject();
            if (listener != null)
                listener.OnMediaMessageSending(_message, progress);
        }
    
        public void onSendingMediaMessageCanceled(AndroidJavaObject message)
        {
            RCIMMediaMessage _message = null;
            if (message != null)
                _message = (RCIMMediaMessage)MediaMessageConverter.from(message).getCSharpObject();
            if (listener != null)
                listener.OnSendingMediaMessageCanceled(_message);
        }
    
        public void onMediaMessageSent(int code, AndroidJavaObject message)
        {
            RCIMMediaMessage _message = null;
            if (message != null)
                _message = (RCIMMediaMessage)MediaMessageConverter.from(message).getCSharpObject();
            if (listener != null)
                listener.OnMediaMessageSent(code, _message);
        }
    }
}
#endif
