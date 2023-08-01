#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMCancelSendingMediaMessageCallbackProxy : AndroidJavaProxy
    {
        OnCancelSendingMediaMessageCalledAction listener;
    
        public RCIMCancelSendingMediaMessageCallbackProxy(OnCancelSendingMediaMessageCalledAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWCancelSendingMediaMessageCallback")
        {
            this.listener = listener;
        }
    
        public void onCancelSendingMediaMessageCalled(int code, AndroidJavaObject message)
        {
            RCIMMediaMessage _message = null;
            if (message != null)
                _message = (RCIMMediaMessage)MediaMessageConverter.from(message).getCSharpObject();
            if (listener != null)
                listener(code, _message);
        }
    }
}
#endif
