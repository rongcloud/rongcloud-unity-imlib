using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMImageMessage : RCIMMediaMessage
    {
        /// <summary>
        /// 图片的缩略图数据
        /// </summary>
        public string thumbnailBase64String { get; set; }
    
        /// <summary>
        /// 是否为原图
        /// </summary>
        public bool original { get; set; }
    
        public override String ToString()
        {
            return $"{base.ToString()} thumbnailBase64String:{thumbnailBase64String} original:{original}";
        }
    }
}
