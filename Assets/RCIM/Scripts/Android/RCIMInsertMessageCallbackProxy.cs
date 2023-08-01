#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMInsertMessageCallbackProxy : AndroidJavaProxy
    {
        OnMessageInsertedAction listener;
    
        public RCIMInsertMessageCallbackProxy(OnMessageInsertedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWInsertMessageCallback")
        {
            this.listener = listener;
        }
    
        public void onMessageInserted(int code, AndroidJavaObject message)
        {
            RCIMMessage _message = null;
            if (message != null)
                _message = (RCIMMessage)MessageConverter.from(message).getCSharpObject();
            if (listener != null)
                listener(code, _message);
        }
    }
}
#endif
