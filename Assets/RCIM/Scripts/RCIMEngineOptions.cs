using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMEngineOptions
    {
        /// <summary>
        /// 私有部署的导航服务器地址
        /// </summary>
        public string naviServer { get; set; }
    
        /// <summary>
        /// 私有部署的媒体服务器地址，即文件和图片的上传地址。使用私有云时必须填写
        /// </summary>
        public string fileServer { get; set; }
    
        /// <summary>
        /// 配置数据上传地址
        /// </summary>
        public string statisticServer { get; set; }
    
        /// <summary>
        /// 设置断线重连时是否踢出重连设备。用户没有开通多设备登录功能的前提下，同一个账号在一台新设备上登录的时候，会把这个账号在之前登录的设备上踢出
        /// </summary>
        public bool kickReconnectDevice { get; set; }
    
        /// <summary>
        /// 压缩项配置
        /// </summary>
        public RCIMCompressOptions compressOptions { get; set; }
    
        /// <summary>
        /// 日志级别
        /// </summary>
        public RCIMLogLevel logLevel { get; set; }
    
        public RCIMPushOptions pushOptions { get; set; }
    
        public bool enablePush { get; set; }
    
        public bool enableIPC { get; set; }
    
        public override String ToString()
        {
            return $"naviServer:{naviServer} fileServer:{fileServer} statisticServer:{statisticServer} kickReconnectDevice:{kickReconnectDevice} compressOptions:{compressOptions} logLevel:{logLevel} pushOptions:{pushOptions} enablePush:{enablePush} enableIPC:{enableIPC}";
        }
    }
}
