#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMRemoveChatRoomEntryCallbackProxy : AndroidJavaProxy
    {
        OnChatRoomEntryRemovedAction listener;
    
        public RCIMRemoveChatRoomEntryCallbackProxy(OnChatRoomEntryRemovedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWRemoveChatRoomEntryCallback")
        {
            this.listener = listener;
        }
    
        public void onChatRoomEntryRemoved(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
