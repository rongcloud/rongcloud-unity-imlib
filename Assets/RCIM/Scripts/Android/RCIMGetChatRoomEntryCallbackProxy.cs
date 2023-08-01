#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMGetChatRoomEntryCallbackProxy : AndroidJavaProxy
    {
        RCIMGetChatRoomEntryListener listener;
    
        public RCIMGetChatRoomEntryCallbackProxy(RCIMGetChatRoomEntryListener listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWGetChatRoomEntryCallback")
        {
            this.listener = listener;
        }
    
        public void onSuccess(AndroidJavaObject t)
        {
            Dictionary<string, string> _t = null;
            if (t != null)
            {
                _t = new Dictionary<string, string>();
                AndroidJavaObject iterator = t.Call<AndroidJavaObject>("entrySet").Call<AndroidJavaObject>("iterator");
                while (iterator.Call<bool>("hasNext"))
                {
                    AndroidJavaObject item = iterator.Call<AndroidJavaObject>("next");
                    string key = item.Call<string>("getKey");
                    string value = item.Call<string>("getValue");
                    _t.Add(key, value);
                }
            }
            if (listener != null)
                listener.OnSuccess(_t);
        }
    
        public void onError(int code)
        {
            if (listener != null)
                listener.OnError(code);
        }
    }
}
#endif
