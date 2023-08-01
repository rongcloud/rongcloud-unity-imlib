#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMRemoveConversationsCallbackProxy : AndroidJavaProxy
    {
        OnConversationsRemovedAction listener;
    
        public RCIMRemoveConversationsCallbackProxy(OnConversationsRemovedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWRemoveConversationsCallback")
        {
            this.listener = listener;
        }
    
        public void onConversationsRemoved(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
