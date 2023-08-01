using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMMessagePushOptions
    {
        /// <summary>
        /// 是否发送通知
        /// </summary>
        public bool disableNotification { get; set; }
    
        /// <summary>
        /// 通知栏是否屏蔽通知标题。true 不显示通知标题，false
        /// 显示通知标题。默认情况下融云单聊消息通知标题为用户名、群聊消息为群名称，设置后不会再显示通知标题。此属性只针目标用户为iOS
        /// 平台时有效，Android 第三方推送平台的通知标题为必填项，所以暂不支持
        /// </summary>
        public bool disablePushTitle { get; set; }
    
        /// <summary>
        /// 推送标题。默认标题显示规则：内置消息：单聊通知标题显示为发送者名称，群聊通知标题显示为群名称。
        /// 自定义消息：默认不显示标题
        /// </summary>
        public string pushTitle { get; set; }
    
        /// <summary>
        /// 推送内容
        /// </summary>
        public string pushContent { get; set; }
    
        /// <summary>
        /// 远程推送附加信息.
        /// </summary>
        public string pushData { get; set; }
    
        /// <summary>
        /// 是否强制显示通知详情。 当目标用户设置推送不显示消息详情时， 可通过此功能，强制设置该条消息显示推送详情
        /// </summary>
        public bool forceShowDetailContent { get; set; }
    
        /// <summary>
        /// 推送模板 ID。设置后根据目标用户通过 setPushLanguageCode
        /// 设置的语言环境，匹配模板中设置的语言内容进行推送。未匹配成功时使用默认内容进行推送,模板内容在“开发者后台-自定义推送文案”中进行设置
        /// 注：RCMessagePushConfig 中的 Title 和 PushContent。优先级高于模板 ID（templateId）中对应的标题和推送内容
        /// </summary>
        public string templateId { get; set; }
    
        /// <summary>
        /// 如果对端设备是 iOS，设置 isVoIPPush 为 True，会走 VoIP 通道推送 Push
        /// </summary>
        public bool voIPPush { get; set; }
    
        /// <summary>
        /// iOS 平台相关配置
        /// </summary>
        public RCIMIOSPushOptions iOSPushOptions { get; set; }
    
        /// <summary>
        /// Android 平台相关配置
        /// </summary>
        public RCIMAndroidPushOptions androidPushOptions { get; set; }
    
        public override String ToString()
        {
            return $"disableNotification:{disableNotification} disablePushTitle:{disablePushTitle} pushTitle:{pushTitle} pushContent:{pushContent} pushData:{pushData} forceShowDetailContent:{forceShowDetailContent} templateId:{templateId} voIPPush:{voIPPush} iOSPushOptions:{iOSPushOptions} androidPushOptions:{androidPushOptions}";
        }
    }
}
