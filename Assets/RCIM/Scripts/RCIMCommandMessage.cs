using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMCommandMessage : RCIMMessage
    {
        /// <summary>
        /// 命令的名称
        /// </summary>
        public string name { get; set; }
    
        /// <summary>
        /// 命令的扩展数据，可以为任意字符串，如存放您定义的json数据
        /// </summary>
        public string data { get; set; }
    
        public override String ToString()
        {
            return $"{base.ToString()} name:{name} data:{data}";
        }
    }
}
