using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMMentionedInfo
    {
        /// <summary>
        /// @ 提醒的类型
        /// </summary>
        public RCIMMentionedType type { get; set; }
    
        /// <summary>
        /// @ 的用户 ID 列表
        /// </summary>
        public List<string> userIdList { get; set; }
    
        /// <summary>
        /// 包含 @ 提醒的消息，本地通知和远程推送显示的内容
        /// </summary>
        public string mentionedContent { get; set; }
    
        public override String ToString()
        {
            return $"type:{type} userIdList:{userIdList} mentionedContent:{mentionedContent}";
        }
    }
}
