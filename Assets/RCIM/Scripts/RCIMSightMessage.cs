using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMSightMessage : RCIMMediaMessage
    {
        /// <summary>
        /// 视频时长
        /// </summary>
        public int duration { get; set; }
    
        /// <summary>
        /// 视频大小
        /// </summary>
        public long size { get; set; }
    
        /// <summary>
        /// 视频的名称
        /// </summary>
        public string name { get; set; }
    
        /// <summary>
        /// 缩略图数据
        /// </summary>
        public string thumbnailBase64String { get; set; }
    
        public override String ToString()
        {
            return $"{base.ToString()} duration:{duration} size:{size} name:{name} thumbnailBase64String:{thumbnailBase64String}";
        }
    }
}
