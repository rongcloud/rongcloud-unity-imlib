#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMChangeConversationTopStatusCallbackProxy : AndroidJavaProxy
    {
        OnConversationTopStatusChangedAction listener;
    
        public RCIMChangeConversationTopStatusCallbackProxy(OnConversationTopStatusChangedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWChangeConversationTopStatusCallback")
        {
            this.listener = listener;
        }
    
        public void onConversationTopStatusChanged(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
