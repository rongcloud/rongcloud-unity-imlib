//
//  Copyright © 2021 RongCloud. All rights reserved.
//

#if UNITY_STANDALONE_WIN
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using AOT;
using UnityEngine;

namespace cn_rongcloud_im_unity
{
    public partial class RCIMWinEngine
    {
        /// <summary>
        /// 连接回调
        /// </summary>
        /// <param name="context"></param>
        /// <param name="code"></param>
        internal delegate void OnImConnectHandler(IntPtr context, int code);

        [MonoPInvokeCallback(typeof(OnImConnectHandler))]
        private static void on_im_connect_status(IntPtr context, int code)
        {
            instance?.OnConnected?.Invoke(code, ((RCIMWinEngine)instance).GetCurrentUserID());          
        }
    }
}
#endif