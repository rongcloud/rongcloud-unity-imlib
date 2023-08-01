using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public interface RCIMSendMessageListener
    {
        /// <summary>
        ///
        /// </summary>
        void OnMessageSaved(RCIMMessage message);
    
        /// <summary>
        ///
        /// </summary>
        void OnMessageSent(int code, RCIMMessage message);
    }
}
