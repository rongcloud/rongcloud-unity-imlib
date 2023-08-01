#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMAddChatRoomEntriesCallbackProxy : AndroidJavaProxy
    {
        OnChatRoomEntriesAddedAction listener;
    
        public RCIMAddChatRoomEntriesCallbackProxy(OnChatRoomEntriesAddedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWAddChatRoomEntriesCallback")
        {
            this.listener = listener;
        }
    
        public void onChatRoomEntriesAdded(int code, AndroidJavaObject errors)
        {
            Dictionary<string, int> _errors = null;
            if (errors != null)
            {
                _errors = new Dictionary<string, int>();
                AndroidJavaObject iterator = errors.Call<AndroidJavaObject>("entrySet").Call<AndroidJavaObject>("iterator");
                while (iterator.Call<bool>("hasNext"))
                {
                    AndroidJavaObject item = iterator.Call<AndroidJavaObject>("next");
                    string key = item.Call<string>("getKey");
                    AndroidJavaObject value = item.Call<AndroidJavaObject>("getValue");
                    if (value != null)
                        _errors.Add(key, value.Call<int>("intValue"));
                }
            }
            if (listener != null)
                listener(code, _errors);
        }
    }
}
#endif
