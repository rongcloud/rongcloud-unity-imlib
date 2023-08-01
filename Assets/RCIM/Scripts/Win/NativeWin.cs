
#if UNITY_STANDALONE_WIN
using System;
using System.Runtime.InteropServices;
using static cn_rongcloud_im_unity.RCIMWinEngine;

namespace cn_rongcloud_im_unity
{
    public class NativeWin
    {
        const string IM_Lib = "rongim";

        [DllImport(IM_Lib, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr rcim_init(string app_key, string navi_url, string conf_path, IntPtr error);

        [DllImport(IM_Lib, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int rcim_uninit(IntPtr client);

        [DllImport(IM_Lib, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int rcim_connect(IntPtr client, string user_token);

        [DllImport(IM_Lib, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int rcim_disconnect(IntPtr client);

        [DllImport(IM_Lib, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int rcim_set_connection_callback(IntPtr client, OnImConnectHandler callback, IntPtr context);

        [DllImport(IM_Lib, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int rcim_get_user_id(IntPtr client, IntPtr user_id, int size);

        [DllImport(IM_Lib, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int rcim_set_device_id(IntPtr client, string device_id);
    }
}
#endif
