using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMMediaMessage : RCIMMessage
    {
        /// <summary>
        /// 本地路径
        /// </summary>
        public string local { get; set; }
    
        /// <summary>
        /// 远端路径
        /// </summary>
        public string remote { get; set; }
    
        public override String ToString()
        {
            return $"{base.ToString()} local:{local} remote:{remote}";
        }
    }
}
