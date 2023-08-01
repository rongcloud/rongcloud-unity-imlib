#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMSyncConversationReadStatusCallbackProxy : AndroidJavaProxy
    {
        OnConversationReadStatusSyncedAction listener;
    
        public RCIMSyncConversationReadStatusCallbackProxy(OnConversationReadStatusSyncedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWSyncConversationReadStatusCallback")
        {
            this.listener = listener;
        }
    
        public void onConversationReadStatusSynced(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
