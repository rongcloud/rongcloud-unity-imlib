#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMGetBlacklistCallbackProxy : AndroidJavaProxy
    {
        RCIMGetBlacklistListener listener;
    
        public RCIMGetBlacklistCallbackProxy(RCIMGetBlacklistListener listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWGetBlacklistCallback")
        {
            this.listener = listener;
        }
    
        public void onSuccess(AndroidJavaObject t)
        {
            List<string> _t = null;
            if (t != null)
            {
                _t = new List<string>();
                AndroidJavaObject iterator = t.Call<AndroidJavaObject>("iterator");
                while (iterator.Call<bool>("hasNext"))
                {
                    _t.Add(iterator.Call<string>("next"));
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
