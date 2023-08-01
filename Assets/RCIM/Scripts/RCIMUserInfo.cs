using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMUserInfo
    {
        /// <summary>
        /// 用户 id
        /// </summary>
        public string userId { get; set; }
    
        /// <summary>
        /// 名称（昵称）
        /// </summary>
        public string name { get; set; }
    
        /// <summary>
        /// 用户头像地址
        /// </summary>
        public string portrait { get; set; }
    
        /// <summary>
        /// 备注
        /// </summary>
        public string alias { get; set; }
    
        /// <summary>
        /// 附加信息
        /// </summary>
        public string extra { get; set; }
    
        public override String ToString()
        {
            return $"userId:{userId} name:{name} portrait:{portrait} alias:{alias} extra:{extra}";
        }
    }
}
