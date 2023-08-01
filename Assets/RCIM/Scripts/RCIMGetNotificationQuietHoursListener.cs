using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public interface RCIMGetNotificationQuietHoursListener
    {
        /// <summary>
        ///
        /// </summary>
        void OnSuccess(string startTime, int spanMinutes, RCIMPushNotificationQuietHoursLevel level);
    
        /// <summary>
        ///
        /// </summary>
        void OnError(int code);
    }
}
