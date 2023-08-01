using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public interface RCIMDownloadMediaMessageListener
    {
        /// <summary>
        ///
        /// </summary>
        void OnMediaMessageDownloading(RCIMMediaMessage message, int progress);
    
        /// <summary>
        ///
        /// </summary>
        void OnDownloadingMediaMessageCanceled(RCIMMediaMessage message);
    
        /// <summary>
        ///
        /// </summary>
        void OnMediaMessageDownloaded(int code, RCIMMediaMessage message);
    }
}
