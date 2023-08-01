#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMSendGroupReadReceiptResponseCallbackProxy : AndroidJavaProxy
    {
        OnGroupReadReceiptResponseSentAction listener;
    
        public RCIMSendGroupReadReceiptResponseCallbackProxy(OnGroupReadReceiptResponseSentAction listener)
            : base("cn.rongcloud.im.wrapper.callback.IRCIMIWSendGroupReadReceiptResponseCallback")
        {
            this.listener = listener;
        }
    
        public void onGroupReadReceiptResponseSent(int code, AndroidJavaObject message)
        {
            List<RCIMMessage> _message = null;
            if (message != null)
            {
                _message = new List<RCIMMessage>();
                AndroidJavaObject iterator = message.Call<AndroidJavaObject>("iterator");
                while (iterator.Call<bool>("hasNext"))
                {
                    AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                    if (value != null)
                        _message.Add((RCIMMessage)MessageConverter.from(value).getCSharpObject());
                }
            }
            if (listener != null)
                listener(code, _message);
        }
    }
}
#endif
