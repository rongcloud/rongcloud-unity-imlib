#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMGetBatchRemoteUltraGroupMessagesCallbackProxy : AndroidJavaProxy
    {
        RCIMGetBatchRemoteUltraGroupMessagesListener listener;
    
        public RCIMGetBatchRemoteUltraGroupMessagesCallbackProxy(RCIMGetBatchRemoteUltraGroupMessagesListener listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWGetBatchRemoteUltraGroupMessagesCallback")
        {
            this.listener = listener;
        }
    
        public void onSuccess(AndroidJavaObject matchedMessages, AndroidJavaObject notMatchedMessages)
        {
            List<RCIMMessage> _matchedMessages = null;
            if (matchedMessages != null)
            {
                _matchedMessages = new List<RCIMMessage>();
                AndroidJavaObject iterator = matchedMessages.Call<AndroidJavaObject>("iterator");
                while (iterator.Call<bool>("hasNext"))
                {
                    AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                    if (value != null)
                        _matchedMessages.Add((RCIMMessage)MessageConverter.from(value).getCSharpObject());
                }
            }
            List<RCIMMessage> _notMatchedMessages = null;
            if (notMatchedMessages != null)
            {
                _notMatchedMessages = new List<RCIMMessage>();
                AndroidJavaObject iterator = notMatchedMessages.Call<AndroidJavaObject>("iterator");
                while (iterator.Call<bool>("hasNext"))
                {
                    AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                    if (value != null)
                        _notMatchedMessages.Add((RCIMMessage)MessageConverter.from(value).getCSharpObject());
                }
            }
            if (listener != null)
                listener.OnSuccess(_matchedMessages, _notMatchedMessages);
        }
    
        public void onError(int code)
        {
            if (listener != null)
                listener.OnError(code);
        }
    }
}
#endif
