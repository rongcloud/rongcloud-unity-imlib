using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMGIFMessage : RCIMMediaMessage
    {
        /// <summary>
        /// GIF 图的大小，单位字节
        /// </summary>
        public long dataSize { get; set; }
    
        /// <summary>
        /// GIF 图的宽
        /// </summary>
        public long width { get; set; }
    
        /// <summary>
        /// GIF 图的高
        /// </summary>
        public long height { get; set; }
    
        public override String ToString()
        {
            return $"{base.ToString()} dataSize:{dataSize} width:{width} height:{height}";
        }
    }
}
