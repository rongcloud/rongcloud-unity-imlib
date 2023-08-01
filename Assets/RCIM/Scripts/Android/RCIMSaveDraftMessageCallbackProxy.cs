#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMSaveDraftMessageCallbackProxy : AndroidJavaProxy
    {
        OnDraftMessageSavedAction listener;
    
        public RCIMSaveDraftMessageCallbackProxy(OnDraftMessageSavedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWSaveDraftMessageCallback")
        {
            this.listener = listener;
        }
    
        public void onDraftMessageSaved(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
