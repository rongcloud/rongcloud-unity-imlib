#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMSyncUltraGroupReadStatusCallbackProxy : AndroidJavaProxy
    {
        OnUltraGroupReadStatusSyncedAction listener;
    
        public RCIMSyncUltraGroupReadStatusCallbackProxy(OnUltraGroupReadStatusSyncedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWSyncUltraGroupReadStatusCallback")
        {
            this.listener = listener;
        }
    
        public void onUltraGroupReadStatusSynced(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
