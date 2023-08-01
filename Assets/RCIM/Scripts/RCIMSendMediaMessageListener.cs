using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public interface RCIMSendMediaMessageListener
    {
        /// <summary>
        ///
        /// </summary>
        void OnMediaMessageSaved(RCIMMediaMessage message);
    
        /// <summary>
        ///
        /// </summary>
        void OnMediaMessageSending(RCIMMediaMessage message, int progress);
    
        /// <summary>
        ///
        /// </summary>
        void OnSendingMediaMessageCanceled(RCIMMediaMessage message);
    
        /// <summary>
        ///
        /// </summary>
        void OnMediaMessageSent(int code, RCIMMediaMessage message);
    }
}
