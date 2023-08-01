#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMCancelDownloadingMediaMessageCallbackProxy : AndroidJavaProxy
    {
        OnCancelDownloadingMediaMessageCalledAction listener;
    
        public RCIMCancelDownloadingMediaMessageCallbackProxy(OnCancelDownloadingMediaMessageCalledAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWCancelDownloadingMediaMessageCallback")
        {
            this.listener = listener;
        }
    
        public void onCancelDownloadingMediaMessageCalled(int code, AndroidJavaObject message)
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
