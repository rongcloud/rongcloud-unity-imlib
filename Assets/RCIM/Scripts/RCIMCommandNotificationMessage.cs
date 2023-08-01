using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMCommandNotificationMessage : RCIMMessage
    {
        /// <summary>
        /// 命令提醒的名称
        /// </summary>
        public string name { get; set; }
    
        /// <summary>
        /// 命令提醒消息的扩展数据，可以为任意字符串，如存放您定义的 json 数据
        /// </summary>
        public string data { get; set; }
    
        public override String ToString()
        {
            return $"{base.ToString()} name:{name} data:{data}";
        }
    }
}
