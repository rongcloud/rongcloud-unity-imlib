#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMRecallMessageCallbackProxy : AndroidJavaProxy
    {
        OnMessageRecalledAction listener;
    
        public RCIMRecallMessageCallbackProxy(OnMessageRecalledAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWRecallMessageCallback")
        {
            this.listener = listener;
        }
    
        public void onMessageRecalled(int code, AndroidJavaObject message)
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
