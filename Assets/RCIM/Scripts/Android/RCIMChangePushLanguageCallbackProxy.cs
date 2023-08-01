#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMChangePushLanguageCallbackProxy : AndroidJavaProxy
    {
        OnPushLanguageChangedAction listener;
    
        public RCIMChangePushLanguageCallbackProxy(OnPushLanguageChangedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWChangePushLanguageCallback")
        {
            this.listener = listener;
        }
    
        public void onPushLanguageChanged(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
