#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMClearDraftMessageCallbackProxy : AndroidJavaProxy
    {
        OnDraftMessageClearedAction listener;
    
        public RCIMClearDraftMessageCallbackProxy(OnDraftMessageClearedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWClearDraftMessageCallback")
        {
            this.listener = listener;
        }
    
        public void onDraftMessageCleared(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
