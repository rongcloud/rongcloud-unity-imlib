#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMAddChatRoomEntryCallbackProxy : AndroidJavaProxy
    {
        OnChatRoomEntryAddedAction listener;
    
        public RCIMAddChatRoomEntryCallbackProxy(OnChatRoomEntryAddedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWAddChatRoomEntryCallback")
        {
            this.listener = listener;
        }
    
        public void onChatRoomEntryAdded(int code)
        {
            if (listener != null)
                listener(code);
        }
    }
}
#endif
