using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMSearchConversationResult
    {
        /// <summary>
        /// 获取会话的实体，用来容纳和存储客户端的会话信息，对应会话列表中的会话
        /// </summary>
        public RCIMConversation conversation { get; set; }
    
        /// <summary>
        /// 获取匹配会话数量
        /// </summary>
        public int count { get; set; }
    
        public override String ToString()
        {
            return $"conversation:{conversation} count:{count}";
        }
    }
}
