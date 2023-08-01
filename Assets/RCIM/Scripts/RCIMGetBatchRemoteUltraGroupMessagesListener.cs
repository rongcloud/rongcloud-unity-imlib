using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public interface RCIMGetBatchRemoteUltraGroupMessagesListener
    {
        /// <summary>
        ///
        /// </summary>
        void OnSuccess(List<RCIMMessage> matchedMessages, List<RCIMMessage> notMatchedMessages);
    
        /// <summary>
        ///
        /// </summary>
        void OnError(int code);
    }
}
