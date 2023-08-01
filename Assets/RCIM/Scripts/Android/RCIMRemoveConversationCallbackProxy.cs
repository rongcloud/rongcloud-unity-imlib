#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMRemoveConversationCallbackProxy : AndroidJavaProxy
    {
        OnConversationRemovedAction listener;
    
        public RCIMRemoveConversationCallbackProxy(OnConversationRemovedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWRemoveConversationCallback")
        {
            this.listener = listener;
        }
    
        public void onConversationRemoved(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
