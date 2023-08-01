using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public interface RCIMConnectListener
    {
        /// <summary>
        ///
        /// </summary>
        void OnConnected(int code, string userId);
    
        /// <summary>
        ///
        /// </summary>
        void OnDatabaseOpened(int code);
    }
}
