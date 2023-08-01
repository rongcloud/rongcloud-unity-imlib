using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMAndroidPushOptions
    {
        /// <summary>
        /// Android 平台 Push 唯一标识。目前支持小米、华为推送平台，默认开发者不需要进行设置。当消息产生推送时，消息的
        /// messageUId 作为 notificationId 使用
        /// </summary>
        public string notificationId { get; set; }
    
        /// <summary>
        /// 小米的渠道 ID。该条消息针对小米使用的推送渠道
        /// </summary>
        public string channelIdMi { get; set; }
    
        /// <summary>
        /// 华为的渠道 ID。该条消息针对华为使用的推送渠道，如开发者集成了华为推送，需要指定 channelId 时，可向 Android
        /// 端研发人员获取，channelId 由开发者自行创建
        /// </summary>
        public string channelIdHW { get; set; }
    
        /// <summary>
        ///
        /// </summary>
        public string categoryHW { get; set; }
    
        /// <summary>
        /// OPPO 的渠道 ID。该条消息针对 OPPO 使用的推送渠道，如开发者集成了 OPPO 推送，需要指定 channelId 时，可向 Android
        /// 端研发人员获取，channelId 由开发者自行创建
        /// </summary>
        public string channelIdOPPO { get; set; }
    
        /// <summary>
        /// VIVO 推送通道类型 开发者集成了 VIVO 推送，需要指定推送类型时，可进行设置
        /// </summary>
        public RCIMVIVOPushType pushTypeVIVO { get; set; }
    
        /// <summary>
        /// FCM 通知类型推送时所使用的分组 id
        /// </summary>
        public string collapseKeyFCM { get; set; }
    
        /// <summary>
        /// FCM 通知类型的推送所使用的通知图片 url
        /// </summary>
        public string imageUrlFCM { get; set; }
    
        /// <summary>
        /// 华为推送消息级别
        /// </summary>
        public RCIMImportanceHW importanceHW { get; set; }
    
        /// <summary>
        /// 华为通知栏消息右侧大图标
        /// URL，如果不设置，则不展示通知栏右侧图标。URL使用的协议必须是HTTPS协议，取值样例：https://example.com/image.png。图标文件须小于
        /// 512KB，图标建议规格大小：40dp x 40dp，弧角大小为 8dp，超出建议规格大小的图标会存在图片压缩或显示不全的情况
        /// </summary>
        public string imageUrlHW { get; set; }
    
        /// <summary>
        /// 小米 Large icon 链接。Large icon 可以出现在大图版和多字版消息中，显示在右边。国内版仅 MIUI12
        /// 以上版本支持，以下版本均不支持；国际版支持。图片要求：大小 120  120px，格式为 png 或者 jpg 格式
        /// </summary>
        public string imageUrlMi { get; set; }
    
        /// <summary>
        /// FCM 通知的频道 ID，该应用程序必须使用此频道 ID 创建一个频道，然后才能收到带有该频道 ID
        /// 的任何通知。如果您未在请求中发送此频道 ID，或者如果应用尚未创建提供的频道 ID，则 FCM 使用应用清单中指定的频道 ID
        /// </summary>
        public string channelIdFCM { get; set; }
    
        /// <summary>
        ///
        /// </summary>
        public string categoryVivo { get; set; }
    
        public override String ToString()
        {
            return $"notificationId:{notificationId} channelIdMi:{channelIdMi} channelIdHW:{channelIdHW} categoryHW:{categoryHW} channelIdOPPO:{channelIdOPPO} pushTypeVIVO:{pushTypeVIVO} collapseKeyFCM:{collapseKeyFCM} imageUrlFCM:{imageUrlFCM} importanceHW:{importanceHW} imageUrlHW:{imageUrlHW} imageUrlMi:{imageUrlMi} channelIdFCM:{channelIdFCM} categoryVivo:{categoryVivo}";
        }
    }
}
