#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMGetConversationsCallbackProxy : AndroidJavaProxy
    {
        RCIMGetConversationsListener listener;
    
        public RCIMGetConversationsCallbackProxy(RCIMGetConversationsListener listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWGetConversationsCallback")
        {
            this.listener = listener;
        }
    
        public void onSuccess(AndroidJavaObject t)
        {
            List<RCIMConversation> _t = null;
            if (t != null)
            {
                _t = new List<RCIMConversation>();
                AndroidJavaObject iterator = t.Call<AndroidJavaObject>("iterator");
                while (iterator.Call<bool>("hasNext"))
                {
                    AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                    if (value != null)
                        _t.Add((RCIMConversation)ConversationConverter.from(value).getCSharpObject());
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
