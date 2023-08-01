#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMRemoveChatRoomEntriesCallbackProxy : AndroidJavaProxy
    {
        OnChatRoomEntriesRemovedAction listener;
    
        public RCIMRemoveChatRoomEntriesCallbackProxy(OnChatRoomEntriesRemovedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWRemoveChatRoomEntriesCallback")
        {
            this.listener = listener;
        }
    
        public void onChatRoomEntriesRemoved(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
