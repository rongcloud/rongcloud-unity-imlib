using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMIOSPushOptions
    {
        /// <summary>
        /// iOS 平台通知栏分组 ID 相同的 thread-id 推送分为一组 iOS10 开始支持
        /// </summary>
        public string threadId { get; set; }
    
        /// <summary>
        /// iOS 富文本推送的类型开发者自己定义，自己在 App 端进行解析判断，与 richMediaUri 一起使用，当设置 category
        /// 后，推送时默认携带 mutable-content 进行推送，属性值为 1。如果不设置后台默认取消息类型字符串，如RC:TxtMsg
        /// </summary>
        public string category { get; set; }
    
        /// <summary>
        /// iOS 平台通知覆盖 ID apnsCollapseId 相同时，新收到的通知会覆盖老的通知，最大 64 字节 iOS10 开始支持
        /// </summary>
        public string apnsCollapseId { get; set; }
    
        /// <summary>
        /// iOS 富文本推送内容的 URL，与 category 一起使用
        /// </summary>
        public string richMediaUri { get; set; }
    
        public override String ToString()
        {
            return $"threadId:{threadId} category:{category} apnsCollapseId:{apnsCollapseId} richMediaUri:{richMediaUri}";
        }
    }
}
