using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMFileMessage : RCIMMediaMessage
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string name { get; set; }
    
        /// <summary>
        /// 文件类型
        /// </summary>
        public string fileType { get; set; }
    
        /// <summary>
        /// 文件大小，单位为 Byte
        /// </summary>
        public long size { get; set; }
    
        public override String ToString()
        {
            return $"{base.ToString()} name:{name} fileType:{fileType} size:{size}";
        }
    }
}
