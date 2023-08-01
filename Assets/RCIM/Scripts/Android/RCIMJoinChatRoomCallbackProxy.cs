#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMJoinChatRoomCallbackProxy : AndroidJavaProxy
    {
        OnChatRoomJoinedAction listener;
    
        public RCIMJoinChatRoomCallbackProxy(OnChatRoomJoinedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWJoinChatRoomCallback")
        {
            this.listener = listener;
        }
    
        public void onChatRoomJoined(int code, string targetId)
        {
            if (listener != null)
                listener(code, targetId);
        }
    }
}
#endif
