#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMGetMessagesCallbackProxy : AndroidJavaProxy
    {
        RCIMGetMessagesListener listener;
    
        public RCIMGetMessagesCallbackProxy(RCIMGetMessagesListener listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWGetMessagesCallback")
        {
            this.listener = listener;
        }
    
        public void onSuccess(AndroidJavaObject t)
        {
            List<RCIMMessage> _t = null;
            if (t != null)
            {
                _t = new List<RCIMMessage>();
                AndroidJavaObject iterator = t.Call<AndroidJavaObject>("iterator");
                while (iterator.Call<bool>("hasNext"))
                {
                    AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                    if (value != null)
                        _t.Add((RCIMMessage)MessageConverter.from(value).getCSharpObject());
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
