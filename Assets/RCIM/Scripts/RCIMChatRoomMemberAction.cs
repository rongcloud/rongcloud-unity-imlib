using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMChatRoomMemberAction
    {
        /// <summary>
        /// 操作的用户
        /// </summary>
        public string userId { get; set; }
    
        /// <summary>
        /// 事件类型
        /// </summary>
        public RCIMChatRoomMemberActionType actionType { get; set; }
    
        public override String ToString()
        {
            return $"userId:{userId} actionType:{actionType}";
        }
    }
}
