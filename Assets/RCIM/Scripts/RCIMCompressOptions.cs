using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMCompressOptions
    {
        /// <summary>
        /// 原图压缩比
        /// </summary>
        public int originalImageQuality { get; set; }
    
        /// <summary>
        /// 原图最长边的最大宽度
        /// </summary>
        public int originalImageSize { get; set; }
    
        /// <summary>
        /// 原图大小限制 配置发送图片时，如果图片大小不超过则发送原图
        /// </summary>
        public int originalImageMaxSize { get; set; }
    
        /// <summary>
        /// 缩略图压缩比例
        /// </summary>
        public int thumbnailQuality { get; set; }
    
        /// <summary>
        /// 缩略图压缩宽、高
        /// </summary>
        public int thumbnailMaxSize { get; set; }
    
        /// <summary>
        /// 缩略图压缩最小宽、高
        /// </summary>
        public int thumbnailMinSize { get; set; }
    
        /// <summary>
        /// 小视频压缩宽度,建议使用16的倍数
        /// </summary>
        public int sightCompressWidth { get; set; }
    
        /// <summary>
        /// 小视频压缩高度，建议使用16的倍数
        /// </summary>
        public int sightCompressHeight { get; set; }
    
        /// <summary>
        ///
        /// </summary>
        public int locationThumbnailQuality { get; set; }
    
        /// <summary>
        ///
        /// </summary>
        public int locationThumbnailWidth { get; set; }
    
        /// <summary>
        ///
        /// </summary>
        public int locationThumbnailHeight { get; set; }
    
        public RCIMCompressOptions()
        {
            originalImageQuality = 85;
            originalImageSize = 1080;
            originalImageMaxSize = 200;
            thumbnailQuality = 30;
            thumbnailMaxSize = 240;
            thumbnailMinSize = 100;
            sightCompressWidth = 544;
            sightCompressHeight = 960;
            locationThumbnailQuality = 70;
            locationThumbnailWidth = 408;
            locationThumbnailHeight = 240;
        }
    
        public override String ToString()
        {
            return $"originalImageQuality:{originalImageQuality} originalImageSize:{originalImageSize} originalImageMaxSize:{originalImageMaxSize} thumbnailQuality:{thumbnailQuality} thumbnailMaxSize:{thumbnailMaxSize} thumbnailMinSize:{thumbnailMinSize} sightCompressWidth:{sightCompressWidth} sightCompressHeight:{sightCompressHeight} locationThumbnailQuality:{locationThumbnailQuality} locationThumbnailWidth:{locationThumbnailWidth} locationThumbnailHeight:{locationThumbnailHeight}";
        }
    }
}
