#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMDownloadMediaMessageListenerProxy : AndroidJavaProxy
    {
        RCIMDownloadMediaMessageListener listener;
    
        public RCIMDownloadMediaMessageListenerProxy(RCIMDownloadMediaMessageListener listener)
            : base("cn.rongcloud.im.wrapper.listener.IRCIMIWDownloadMediaMessageListener")
        {
            this.listener = listener;
        }
    
        public void onMediaMessageDownloading(AndroidJavaObject message, int progress)
        {
            RCIMMediaMessage _message = null;
            if (message != null)
                _message = (RCIMMediaMessage)MediaMessageConverter.from(message).getCSharpObject();
            if (listener != null)
                listener.OnMediaMessageDownloading(_message, progress);
        }
    
        public void onDownloadingMediaMessageCanceled(AndroidJavaObject message)
        {
            RCIMMediaMessage _message = null;
            if (message != null)
                _message = (RCIMMediaMessage)MediaMessageConverter.from(message).getCSharpObject();
            if (listener != null)
                listener.OnDownloadingMediaMessageCanceled(_message);
        }
    
        public void onMediaMessageDownloaded(int code, AndroidJavaObject message)
        {
            RCIMMediaMessage _message = null;
            if (message != null)
                _message = (RCIMMediaMessage)MediaMessageConverter.from(message).getCSharpObject();
            if (listener != null)
                listener.OnMediaMessageDownloaded(code, _message);
        }
    }
}
#endif
