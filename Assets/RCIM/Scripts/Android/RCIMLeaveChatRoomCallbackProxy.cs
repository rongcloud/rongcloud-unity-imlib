#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMLeaveChatRoomCallbackProxy : AndroidJavaProxy
    {
        OnChatRoomLeftAction listener;
    
        public RCIMLeaveChatRoomCallbackProxy(OnChatRoomLeftAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWLeaveChatRoomCallback")
        {
            this.listener = listener;
        }
    
        public void onChatRoomLeft(int code, string targetId)
        {
            if (listener != null)
                listener(code, targetId);
        }
    }
}
#endif
