#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMInsertMessagesCallbackProxy : AndroidJavaProxy
    {
        OnMessagesInsertedAction listener;
    
        public RCIMInsertMessagesCallbackProxy(OnMessagesInsertedAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWInsertMessagesCallback")
        {
            this.listener = listener;
        }
    
        public void onMessagesInserted(int code, AndroidJavaObject messages)
        {
            List<RCIMMessage> _messages = null;
            if (messages != null)
            {
                _messages = new List<RCIMMessage>();
                AndroidJavaObject iterator = messages.Call<AndroidJavaObject>("iterator");
                while (iterator.Call<bool>("hasNext"))
                {
                    AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                    if (value != null)
                        _messages.Add((RCIMMessage)MessageConverter.from(value).getCSharpObject());
                }
            }
            if (listener != null)
                listener(code, _messages);
        }
    }
}
#endif
