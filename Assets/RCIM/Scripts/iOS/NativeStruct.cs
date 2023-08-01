#if UNITY_IOS
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace cn_rongcloud_im_unity
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_c_map_item
    {
        public string key;
        public string value;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_c_list
    {
        public IntPtr list;
        public long count;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ios_class_warpper
    {
        public IntPtr obj;
        public string type;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_android_push_options
    {
        public string notificationId;
        public string channelIdMi;
        public string channelIdHW;
        public string categoryHW;
        public string channelIdOPPO;
        public int pushTypeVIVO;
        public string collapseKeyFCM;
        public string imageUrlFCM;
        public int importanceHW;
        public string imageUrlHW;
        public string imageUrlMi;
        public string channelIdFCM;
        public string categoryVivo;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_message_push_options
    {
        [MarshalAs(UnmanagedType.U1)]
        public bool disableNotification;
        [MarshalAs(UnmanagedType.U1)]
        public bool disablePushTitle;
        public string pushTitle;
        public string pushContent;
        public string pushData;
        [MarshalAs(UnmanagedType.U1)]
        public bool forceShowDetailContent;
        public string templateId;
        [MarshalAs(UnmanagedType.U1)]
        public bool voIPPush;
        public IntPtr iOSPushOptions;
        public IntPtr androidPushOptions;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_ios_push_options
    {
        public string threadId;
        public string category;
        public string apnsCollapseId;
        public string richMediaUri;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_compress_options
    {
        public int originalImageQuality;
        public int originalImageSize;
        public int originalImageMaxSize;
        public int thumbnailQuality;
        public int thumbnailMaxSize;
        public int thumbnailMinSize;
        public int sightCompressWidth;
        public int sightCompressHeight;
        public int locationThumbnailQuality;
        public int locationThumbnailWidth;
        public int locationThumbnailHeight;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_engine_options
    {
        public string naviServer;
        public string fileServer;
        public string statisticServer;
        [MarshalAs(UnmanagedType.U1)]
        public bool kickReconnectDevice;
        public IntPtr compressOptions;
        public int logLevel;
        public IntPtr pushOptions;
        [MarshalAs(UnmanagedType.U1)]
        public bool enablePush;
        [MarshalAs(UnmanagedType.U1)]
        public bool enableIPC;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_unknown_message
    {
        public IntPtr m_parent;
        public string rawData;
        public string objectName;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_user_info
    {
        public string userId;
        public string name;
        public string portrait;
        public string alias;
        public string extra;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_custom_message
    {
        public IntPtr m_parent;
        public string identifier;
        public int policy;
        public IntPtr fields;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_message
    {
        public int conversationType;
        public int messageType;
        public string targetId;
        public string channelId;
        public long messageId;
        public string messageUId;
        [MarshalAs(UnmanagedType.U1)]
        public bool offLine;
        public IntPtr groupReadReceiptInfo;
        public long receivedTime;
        public long sentTime;
        public int receivedStatus;
        public int sentStatus;
        public string senderUserId;
        public int direction;
        public IntPtr userInfo;
        public IntPtr mentionedInfo;
        public IntPtr pushOptions;
        public string extra;
        public IntPtr expansion;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_image_message
    {
        public IntPtr m_parent;
        public string thumbnailBase64String;
        [MarshalAs(UnmanagedType.U1)]
        public bool original;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_file_message
    {
        public IntPtr m_parent;
        public string name;
        public string fileType;
        public long size;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_recall_notification_message
    {
        public IntPtr m_parent;
        [MarshalAs(UnmanagedType.U1)]
        public bool admin;
        [MarshalAs(UnmanagedType.U1)]
        public bool deleted;
        public long recallTime;
        public long recallActionTime;
        public IntPtr originalMessage;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_media_message
    {
        public IntPtr m_parent;
        public string local;
        public string remote;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_text_message
    {
        public IntPtr m_parent;
        public string text;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_gif_message
    {
        public IntPtr m_parent;
        public long dataSize;
        public long width;
        public long height;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_command_message
    {
        public IntPtr m_parent;
        public string name;
        public string data;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_voice_message
    {
        public IntPtr m_parent;
        public long duration;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_mentioned_info
    {
        public int type;
        public IntPtr userIdList;
        public string mentionedContent;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_command_notification_message
    {
        public IntPtr m_parent;
        public string name;
        public string data;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_sight_message
    {
        public IntPtr m_parent;
        public long duration;
        public long size;
        public string name;
        public string thumbnailBase64String;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_location_message
    {
        public IntPtr m_parent;
        public double longitude;
        public double latitude;
        public string poiName;
        public string thumbnailPath;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_reference_message
    {
        public IntPtr m_parent;
        public string text;
        public IntPtr referenceMessage;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_blocked_message_info
    {
        public int conversationType;
        public string targetId;
        public string blockedMsgUId;
        public int blockType;
        public string extra;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_typing_status
    {
        public string userId;
        public string contentType;
        public long sentTime;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_ultra_group_typing_status_info
    {
        public string targetId;
        public string channelId;
        public string userId;
        public long userNums;
        public int status;
        public long timestamp;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_group_read_receipt_info
    {
        [MarshalAs(UnmanagedType.U1)]
        public bool readReceiptMessage;
        [MarshalAs(UnmanagedType.U1)]
        public bool hasRespond;
        public IntPtr respondUserIds;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_chat_room_member_action
    {
        public string userId;
        public int actionType;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_search_conversation_result
    {
        public IntPtr conversation;
        public int count;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_conversation
    {
        public int conversationType;
        public string targetId;
        public string channelId;
        public long unreadCount;
        public long mentionedCount;
        [MarshalAs(UnmanagedType.U1)]
        public bool top;
        public string draft;
        public IntPtr lastMessage;
        public int notificationLevel;
        public long firstUnreadMsgSendTime;
        public long operationTime;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_push_options
    {
        public string idMI;
        public string appKeyMI;
        public string appIdMeizu;
        public string appKeyMeizu;
        public string appKeyOPPO;
        public string appSecretOPPO;
        [MarshalAs(UnmanagedType.U1)]
        public bool enableHWPush;
        [MarshalAs(UnmanagedType.U1)]
        public bool enableFCM;
        [MarshalAs(UnmanagedType.U1)]
        public bool enableVIVOPush;
    }
    
}
    
#endif