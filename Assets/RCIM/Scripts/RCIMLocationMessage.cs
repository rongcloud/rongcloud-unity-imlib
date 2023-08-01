using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMLocationMessage : RCIMMessage
    {
        /// <summary>
        /// 经度信息
        /// </summary>
        public double longitude { get; set; }
    
        /// <summary>
        /// 纬度信息
        /// </summary>
        public double latitude { get; set; }
    
        /// <summary>
        /// POI 信息
        /// </summary>
        public string poiName { get; set; }
    
        /// <summary>
        /// 缩略图地址
        /// </summary>
        public string thumbnailPath { get; set; }
    
        public override String ToString()
        {
            return $"{base.ToString()} longitude:{longitude} latitude:{latitude} poiName:{poiName} thumbnailPath:{thumbnailPath}";
        }
    }
}
