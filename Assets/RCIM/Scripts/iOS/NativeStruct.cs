//
//  Copyright © 2021 RongCloud. All rights reserved.
//

#if UNITY_IOS
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace cn_rongcloud_im_unity
{
    #region structs

    #region 消息

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_user_info
    {
        public string userId;
        public string name;
        public string portraitUri;
        public string alias;
        public string extra;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_mentioned_info
    {
        public int type;
        public IntPtr userIds;
        public int userIdCount;
        public string content;
        [MarshalAs(UnmanagedType.U1)] public bool isMentionedMe;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_message_content
    {
        public IntPtr userInfo;
        public IntPtr mentionedInfo;
        public int destructDuration;
        public string objectName;
        public string extra;
        public string rawJsonData;
        public int persistent; // iOS sdk 指明此消息类型在本地是否存储、是否计入未读消息数
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_media_message_content
    {
        public IntPtr userInfo; // 用户携带用户信息
        public IntPtr mentionedInfo; // 用于携带提醒消息
        public int destructDuration; // 默认0，表示非阅后即焚
        public string objectName;
        public string extra; // 用于扩展
        public string rawJsonData; // 对于解码正确的消息，此字段值为 NULL
        public int persistent;
        public string localPath; // 媒体内容的本地路径（此属性必须有值）
        public string remoteUrl; // 媒体内容上传服务器后的网络地址（上传成功后 SDK 会为该属性赋值）

        public string name; // 媒体内容的文件名（如不传使用 SDK 中 downloadMediaMessage
        // 方法下载后会默认生成一个名称）
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_ios_config
    {
        public string threadId;
        public string category;
        public string apnsCollapseId;
        public string richMediaUri;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_push_config
    {
        [MarshalAs(UnmanagedType.U1)] public bool disablePushTitle;
        public string pushTitle;
        public string pushContent;
        public string pushData;
        [MarshalAs(UnmanagedType.U1)] public bool forceShowDetailContent;
        public string templateId;
        public IntPtr iOSConfig; // im_ios_config
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_read_receipt_info
    {
        [MarshalAs(UnmanagedType.U1)] public bool isReceiptRequestMessage; // 是否需要回执消息
        [MarshalAs(UnmanagedType.U1)] public bool hasRespond; // 是否已经发送回执
        public int userIdsLen; // 发送回执的用户列表长度
        public IntPtr userIdKeys; // 发送回执的用户 id 列表 Key 值数组
        public IntPtr userIdValues; // 发送回执的用户 id 列表 Value 值数组
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_message
    {
        public int conversationType;
        public string targetId;
        public int direction;
        public string senderId;
        public int receivedStatus;
        public int sentStatus;
        public Int64 receivedTime;
        public Int64 sentTime;
        public string objectName;
        public IntPtr content;
        public string extra;
        public Int64 messageId;
        public string messageUid;
        public IntPtr readReceiptInfo; // im_read_receipt_info
        [MarshalAs(UnmanagedType.U1)] public bool canIncludeExpansion;
        public int expansionCount; // 可扩展信息包含的键值对长度
        public IntPtr expansionKeys; // 指向可扩展信息 key 的指针数组
        public IntPtr expansionValues; // 指向可扩展信息 value 的指针数组
        [MarshalAs(UnmanagedType.U1)] public bool disableNotification;
        [MarshalAs(UnmanagedType.U1)] public bool isOffline;
        public IntPtr pushConfig; // im_push_config

        public void setPushConfig(im_push_config imPushConfig)
        {
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(imPushConfig));
            Marshal.StructureToPtr(imPushConfig, ptr, false);
            this.pushConfig = ptr;
        }
    }

    /// <summary>
    ///  消息被拦截类型
    /// </summary>
    internal enum im_enum_message_block_type
    {
        IM_MESSAGE_BLOCK_TYPE_NONE = 0, // 没有设置敏感词类型
        IM_MESSAGE_BLOCK_TYPE_GLOBAL = 1, // 全局敏感词：命中了融云内置的全局敏感词
        IM_MESSAGE_BLOCK_TYPE_CUSTOM = 2, // 自定义敏感词拦截：命中了客户在融云自定义的敏感词
        IM_MESSAGE_BLOCK_TYPE_THIRD_PARTY = 3, // 第三方审核拦截：命中了第三方（数美）
        // 或模板路由决定不下发的状态
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_blocked_message_info
    {
        public int type; // 会话类型
        public string targetId; // 会话 ID
        public string blockedMsgUId; // 被拦截的消息 ID
        public im_enum_message_block_type blockType; // 拦截原因
        public string extra; // 附加信息
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_typing_status
    {
        public string userId;
        public string typingContentType;
        public Int64 sentTime;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_text_message
    {
        public IntPtr msgContent;
        public string text;

        public im_text_message(IntPtr msgContent_, string text_)
        {
            msgContent = msgContent_;
            text = text_;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_rich_content_message
    {
        public IntPtr msgContent;
        public string title; // 图文消息的标题
        public string digest; // 图文消息的内容摘要
        public string imageURL; // 图文消息图片 URL
        public string url; // 图文消息中包含的需要跳转到的URL

        public im_rich_content_message(RCRichContentMessage rcMsg)
        {
            title = rcMsg.Title;
            digest = rcMsg.Digest;
            imageURL = rcMsg.ImageUrl;
            url = rcMsg.Url;
            msgContent = RCIMUtils.convertRCMessageContent(rcMsg);
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_typing_message
    {
        public IntPtr msgContent;
        public string data;
        public string typingContentType;

        public im_typing_message(IntPtr msgContent_, string data_, string type_)
        {
            msgContent = msgContent_;
            data = data_;
            typingContentType = type_;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_status_message
    {
        public IntPtr msgContent;
        public string statusType;
        public string statusContent;

        public im_status_message(IntPtr msgContent_, string statusType_, string statusContent_)
        {
            msgContent = msgContent_;
            statusType = statusType_;
            statusContent = statusContent_;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_image_message
    {
        public IntPtr msgContent;
        public string imageUrl; // 图片消息的 URL 地址
        public string localPath; // 图片的本地路径
        [MarshalAs(UnmanagedType.U1)] public bool isFull; // 是否发送原图
        public string extra; // 图片消息的附加信息
        public string thumbnailBase64String;

        public im_image_message(RCImageMessage rcImageMsg)
        {
            imageUrl = rcImageMsg.RemoteUrl;
            thumbnailBase64String = rcImageMsg.Base64;
            extra = rcImageMsg.Extra;
            localPath = rcImageMsg.LocalPath;
            isFull = rcImageMsg.IsOriginal;
            msgContent = RCIMUtils.convertRCMediaMessageContent(rcImageMsg);
        }

        public RCImageMessage toRcImageMessage()
        {
            var rcImageMsg = new RCImageMessage(this.localPath, this.isFull)
            {
                RemoteUrl = this.imageUrl,
                Base64 = this.thumbnailBase64String,
                Extra = this.extra
            };
            RCIMUtils.writeRCMessageContentInfo(rcImageMsg, msgContent);
            return rcImageMsg;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_voice_message
    {
        public IntPtr msgContent;
        public string localPath;
        public string wavDataBase64; // 语音数据的 Base64 编码
        public Int64 duration; // 语音消息时长，以秒为单位

        public im_voice_message(RCVoiceMessage rcVoiceMsg)
        {
            msgContent = RCIMUtils.convertRCMessageContent(rcVoiceMsg);
            localPath = rcVoiceMsg.LocalPath;
            wavDataBase64 = rcVoiceMsg.Base64;
            duration = rcVoiceMsg.Duration;
        }

        public RCVoiceMessage toRCVoiceMessage()
        {
            var rcVoiceMsg = new RCVoiceMessage()
            {
                Base64 = this.wavDataBase64,
                LocalPath = localPath,
                Duration = (int) this.duration
            };

            RCIMUtils.writeRCMessageContentInfo(rcVoiceMsg, msgContent);
            return rcVoiceMsg;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_hq_voice_message
    {
        public IntPtr msgContent;
        public string localFileUrl;
        public Int64 duration; // 语音消息的时长，以秒为单位

        public im_hq_voice_message(RCHQVoiceMessage rcHqVoiceMsg)
        {
            msgContent = RCIMUtils.convertRCMediaMessageContent(rcHqVoiceMsg);
            duration = rcHqVoiceMsg.Duration;
            localFileUrl = rcHqVoiceMsg.LocalPath;
        }

        public RCHQVoiceMessage toRCHQVoiceMessage()
        {
            var rcHqVoiceMsg = new RCHQVoiceMessage()
            {
                Duration = (int) duration,
                FileUrl = localFileUrl
            };

            RCIMUtils.writeRCMediaMessageContentInfo(rcHqVoiceMsg, msgContent);
            return rcHqVoiceMsg;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_sight_message
    {
        public IntPtr msgContent;
        public string localPath; // 本地 URL 地址
        public string sightUrl; // 网络 URL 地址
        public int duration; // 视频时长，以秒为单位
        public string name; // 小视频文件名
        public Int64 size; // 文件大小
        public string thumbnailBase64; // 缩略图的 base64 编码

        public im_sight_message(RCSightMessage rcSightMessage)
        {
            msgContent = RCIMUtils.convertRCMediaMessageContent(rcSightMessage);
            localPath = rcSightMessage.LocalPath;
            sightUrl = rcSightMessage.MediaUrl;
            duration = rcSightMessage.Duration;
            name = rcSightMessage.Name;
            size = rcSightMessage.Size;
            thumbnailBase64 = rcSightMessage.Base64;
        }

        public RCSightMessage toRCSightMessage()
        {
            var rcSightMsg = new RCSightMessage()
            {
                LocalPath = localPath,
                MediaUrl = sightUrl,
                Duration = duration,
                Name = name,
                Size = size,
                Base64 = thumbnailBase64
            };
            RCIMUtils.writeRCMediaMessageContentInfo(rcSightMsg, msgContent);
            return rcSightMsg;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_gif_message
    {
        public IntPtr msgContent;
        public Int64 width; // GIF 图的宽
        public Int64 height; // GIF 图的高
        public string gifUri; // GIF 本地路径

        public Int64 gifDataSize; // GIF 图的大小，单位字节
        // public byte[] gifData; // GIF 图的数据

        public im_gif_message(RCGifMessage rcGifMsg)
        {
            msgContent = RCIMUtils.convertRCMediaMessageContent(rcGifMsg);
            width = rcGifMsg.Width;
            height = rcGifMsg.Height;
            gifUri = rcGifMsg.LocalPath;
            gifDataSize = rcGifMsg.Size;
            // gifData = null;
        }

        public RCGifMessage toRCGifMessage()
        {
            var rcGifMsg = new RCGifMessage()
            {
                Width = (int) width,
                Height = (int) height,
                LocalPath = gifUri,
                Size = (int) gifDataSize
            };
            RCIMUtils.writeRCMediaMessageContentInfo(rcGifMsg, msgContent);
            return rcGifMsg;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_file_message
    {
        public IntPtr msgContent;
        public string name; // 文件名
        public Int64 size; // 文件大小，单位为 Byte
        public string type; // 文件类型
        public string fileUrl; // 文件的网络地址
        public string localPath; // 文件的本地路径

        public im_file_message(RCFileMessage rcFileMsg)
        {
            msgContent = RCIMUtils.convertRCMediaMessageContent(rcFileMsg);
            name = rcFileMsg.Name;
            size = rcFileMsg.Size;
            type = rcFileMsg.Type;
            fileUrl = rcFileMsg.MediaUrl;
            localPath = rcFileMsg.LocalPath;
        }

        public RCFileMessage toRCFileMessage()
        {
            var rcFileMsg = new RCFileMessage()
            {
                Name = name,
                Size = size,
                Type = type,
                MediaUrl = fileUrl,
                LocalPath = localPath
            };
            RCIMUtils.writeRCMediaMessageContentInfo(rcFileMsg, msgContent);
            return rcFileMsg;
        }
    }

    // [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    // internal struct im_location_message
    // {
    //     public IntPtr msgContent;
    //     public Double latitude; // 地理位置的二维坐标
    //     public Double longitude; // 地理位置的二维坐标
    //     public string locationName; // 地理位置的名称
    //     public string thumbnailImageBase64; // 地理位置的缩略图的 base64 
    //     public string extra; // 地理位置的附加信息

    //     public im_location_message(RCLocationMessage rcLocationMsg)
    //     {
    //         msgContent = RCIMUtils.convertRCMessageContent(rcLocationMsg);
    //         latitude = rcLocationMsg.Latitude;
    //         longitude = rcLocationMsg.Longitude;
    //         locationName = null;
    //         thumbnailImageBase64 = rcLocationMsg.Base64;
    //         extra = rcLocationMsg.Extra;
    //     }

    //     public RCLocationMessage toRCLocationMessage()
    //     {
    //         var rcLocationMsg = new RCLocationMessage()
    //         {
    //             Latitude = latitude,
    //             Longitude = longitude,
    //             Base64 = thumbnailImageBase64,
    //             Extra = extra
    //         };
    //         RCIMUtils.writeRCMessageContentInfo(rcLocationMsg, msgContent);
    //         return rcLocationMsg;
    //     }
    // }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_custom_message
    {
        // message content 基类数据
        public IntPtr msgContent;
        public int customMessageType; // 自定义消息类型: 0 command, 1 storage, 2 normal, 3 status

        public int customFieldsCount; // 自定义消息字段数
        public IntPtr customFieldsKeys; // 指向自定义消息字段 key 的指针数组
        public IntPtr customFieldsValues; // 指向自定义消息字段 value 的指针数组

        public im_custom_message(RCCustomMessage rcCustomMsg)
        {
            msgContent = RCIMUtils.convertRCMessageContent(rcCustomMsg);

            customMessageType = (int)rcCustomMsg.CustomMessageType;
            customFieldsCount = rcCustomMsg.CustomFields.Count;

            customFieldsKeys = RCIMUtils.getKeysIntPtrFromDictionary(rcCustomMsg.CustomFields);
            customFieldsValues = RCIMUtils.getValuesIntPtrFromDictionary(rcCustomMsg.CustomFields);
        }

        public RCCustomMessage toRCCustomMessage()
        {
            var dictionary = new Dictionary<String, String>();

            if (customFieldsCount > 0)
            {
                RCIMUtils.PtrToStringList(customFieldsKeys, customFieldsCount, out var keysList);
                RCIMUtils.PtrToStringList(customFieldsValues, customFieldsCount, out var valuesList);

                for (int i = 0; i < customFieldsCount; i++)
                {
                    dictionary.Add(keysList[i], valuesList[i]);
                }
            }

            // RCIMUtils.writeRCMessageContentInfo 赋值 objectName
            var rcCustomMsg = new RCCustomMessage("", (RCCustomMessageType)customMessageType, dictionary);
            RCIMUtils.writeRCMessageContentInfo(rcCustomMsg, msgContent);
            return rcCustomMsg;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_reference_message
    {
        public IntPtr msgContent;
        public string content; // 引用文本
        public string referMsgUserId; // 被引用消息的发送者 ID
        public IntPtr referMsg; // 被引用消息体
        public string referMsgObjectName;
        public string referMsgUid; // 被引用消息的 messageUId。服务器消息唯一 ID（在同一个 Appkey 下全局唯一）

        public im_reference_message(RCReferenceMessage rcReferenceMsg)
        {
            msgContent = RCIMUtils.convertRCMediaMessageContent(rcReferenceMsg);
            content = rcReferenceMsg.Content;
            referMsgUserId = rcReferenceMsg.ReferredMsgUserId;
            referMsg = RCIMUtils.convertConcreteRCMessageContent(rcReferenceMsg.ReferredMsg);
            referMsgObjectName = rcReferenceMsg.ReferredMsg.ObjectName;
            referMsgUid = rcReferenceMsg.ReferredMsgUid;
        }

        public RCReferenceMessage toRCReferenceMessage()
        {
            var rcReferenceMsg = new RCReferenceMessage()
            {
                Content = content,
                ReferredMsgUserId = referMsgUserId,
                ReferredMsg = RCIMUtils.toConcreteRCMessageContent(referMsg, referMsgObjectName),
                ReferredMsgUid = referMsgUid
            };
            RCIMUtils.writeRCMediaMessageContentInfo(rcReferenceMsg, msgContent);
            return rcReferenceMsg;
        }
    }

    // [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    // internal struct im_combine_message
    // {
    //     public IntPtr msgContent;
    //     public IntPtr summaryList; // 转发的消息展示的缩略内容列表 (格式是发送者 ：缩略内容) string[]
    //     public int summaryListLen;
    //     public IntPtr nameList; // 转发的全部消息的发送者名称列表 （单聊是经过排重的，群聊是群组名称）string[]
    //     public int nameListLen;
    //     public int conversationType; // 转发的消息会话类型 （目前仅支持单聊和群聊）
    //     public string extra; // 转发的消息 消息的附加信息
    //
    //     public im_combine_message(RCCombineMessage rcCombineMsg)
    //     {
    //         msgContent = RCIMUtils.convertRCMediaMessageContent(rcCombineMsg);
    //         summaryListLen = RCIMUtils.StringListToPtr(rcCombineMsg.SummaryList, out summaryList);
    //         nameListLen = RCIMUtils.StringListToPtr(rcCombineMsg.NameList, out nameList);
    //         conversationType = (int) rcCombineMsg.ConversationType;
    //         extra = rcCombineMsg.Extra;
    //     }
    //
    //     public RCCombineMessage toRCCombineMessage()
    //     {
    //         var rcCombineMsg = new RCCombineMessage();
    //         RCIMUtils.PtrToStringList(summaryList, summaryListLen, out var SummaryList);
    //         rcCombineMsg.SummaryList = SummaryList;
    //         RCIMUtils.PtrToStringList(nameList, nameListLen, out var NameList);
    //         rcCombineMsg.NameList = NameList;
    //         rcCombineMsg.ConversationType = (RCConversationType) conversationType;
    //         rcCombineMsg.Extra = extra;
    //         RCIMUtils.writeRCMediaMessageContentInfo(rcCombineMsg, msgContent);
    //         return rcCombineMsg;
    //     }
    // }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_recall_notification_message
    {
        public IntPtr msgContent;
        public string operatorId; // 发起撤回操作的用户 ID
        public Int64 recallTime; // 撤回的时间（毫秒）
        public string originalObjectName; // 原消息的消息类型名
        [MarshalAs(UnmanagedType.U1)] public bool isAdmin; // 是否是管理员操作
        public string recallContent; // 撤回的文本消息的内容
        public Int64 recallActionTime; // 撤回动作的时间（毫秒）

        public im_recall_notification_message(RCRecallNotificationMessage rcRecallNotificationMsg)
        {
            msgContent = RCIMUtils.convertRCMessageContent(rcRecallNotificationMsg);
            operatorId = rcRecallNotificationMsg.OperatorId;
            recallTime = rcRecallNotificationMsg.RecallTime;
            originalObjectName = rcRecallNotificationMsg.OriginalObjectName;
            isAdmin = rcRecallNotificationMsg.IsAdmin;
            recallContent = rcRecallNotificationMsg.RecallContent;
            recallActionTime = rcRecallNotificationMsg.RecallActionTime;
        }

        public RCRecallNotificationMessage toRCRecallNotificationMessage()
        {
            var rcRecallNotificationMsg = new RCRecallNotificationMessage()
            {
                OperatorId = operatorId,
                RecallTime = recallTime,
                OriginalObjectName = originalObjectName,
                IsAdmin = isAdmin,
                RecallContent = recallContent,
                RecallActionTime = recallActionTime
            };
            RCIMUtils.writeRCMessageContentInfo(rcRecallNotificationMsg, msgContent);
            return rcRecallNotificationMsg;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_group_notification_message
    {
        public IntPtr msgContent;
        public string operation; // 群组通知的当前操作名
        public string operatorUserId; // 当前操作发起用户的用户 ID
        public string data; // 当前操作的目标对象
        public string message; // 当前操作的消息内容

        public im_group_notification_message(RCGroupNotificationMessage rcGroupNotificationMsg)
        {
            msgContent = RCIMUtils.convertRCMessageContent(rcGroupNotificationMsg);
            operation = rcGroupNotificationMsg.Operation;
            operatorUserId = rcGroupNotificationMsg.OperatorUserId;
            data = rcGroupNotificationMsg.Data;
            message = rcGroupNotificationMsg.Message;
        }

        public RCGroupNotificationMessage toRCGroupNotificationMessage()
        {
            var rcGroupNotificationMsg = new RCGroupNotificationMessage()
            {
                Operation = operation,
                OperatorUserId = operatorUserId,
                Data = data,
                Message = message
            };
            RCIMUtils.writeRCMessageContentInfo(rcGroupNotificationMsg, msgContent);
            return rcGroupNotificationMsg;
        }
    }

    internal enum im_enum_chat_room_member_action_type
    {
        IM_CHATROOM_MEMBER_QUIT = 0, // 聊天室成员退出
        IM_CHATROOM_MEMBER_JOIN = 1, // 聊天室成员加入
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_chat_room_member_action
    {
        public string memberId;
        public im_enum_chat_room_member_action_type action; // 成员加入或者退出
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_chat_room_member_action_message
    {
        public IntPtr msgContent;
        public IntPtr userList;
        public int userListLen;

        public im_chat_room_member_action_message(RCChatroomMemberActionMessage rcChatroomMemberActionMsg)
        {
            msgContent = RCIMUtils.convertRCMessageContent(rcChatroomMemberActionMsg);
            userListLen = RCIMUtils.StructureListToPtr(rcChatroomMemberActionMsg.MemberActions, out userList);
        }

        public RCChatroomMemberActionMessage toRCChatroomMemberActionMessage()
        {
            var rcChatroomMemberActionMsg = new RCChatroomMemberActionMessage();
            RCIMUtils.PtrToStructureList<RCChatroomMemberAction>(userList, userListLen,
                out var MemberActions);
            rcChatroomMemberActionMsg.MemberActions = MemberActions;
            RCIMUtils.writeRCMessageContentInfo(rcChatroomMemberActionMsg, msgContent);
            return rcChatroomMemberActionMsg;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_chat_room_kv_notification_message
    {
        public IntPtr msgContent;
        int type; // 聊天室操作的类型 RCChatroomKVNotificationType
        public string key; // 聊天室属性名称
        public string value; // 聊天室属性对应的值
        public string extra; // 通知消息的自定义字段，最大长度 2 kb

        public im_chat_room_kv_notification_message(RCChatroomKVNotificationMessage rcChatroomKvNotificationMsg)
        {
            msgContent = RCIMUtils.convertRCMessageContent(rcChatroomKvNotificationMsg);
            type = (int) rcChatroomKvNotificationMsg.Type;
            key = rcChatroomKvNotificationMsg.Key;
            value = rcChatroomKvNotificationMsg.Value;
            extra = rcChatroomKvNotificationMsg.Extra;
        }

        public RCChatroomKVNotificationMessage toRCChatroomKVNotificationMessage()
        {
            var rcChatroomKvNotificationMsg = new RCChatroomKVNotificationMessage()
            {
                Type = (RCChatroomKVNotificationType) type,
                Key = key,
                Value = value,
                Extra = extra
            };
            RCIMUtils.writeRCMessageContentInfo(rcChatroomKvNotificationMsg, msgContent);
            return rcChatroomKvNotificationMsg;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_chatroom_history_messages
    {
        public IntPtr imMsgList;
        public int imMsgListCount;
        public long syncTime;
    }

    #endregion

    #region 会话

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_conversation
    {
        public int type;
        public string targetId;
        public string channelId;
        public string title;
        public int unreadCount;
        [MarshalAs(UnmanagedType.U1)] public bool isTop;
        public int receivedStatus;
        public int sentStatus;
        public Int64 receivedTime;
        public Int64 sentTime;
        public string draft;
        public string objectName;
        public string senderUserId;
        public long latestMessageId;
        public IntPtr latestMessageContent; //
        public int latestMessageDirection;
        public string rawJsonString;
        public string latestMessageUid;
        [MarshalAs(UnmanagedType.U1)] public bool hasUnreadMentioned;
        public int mentionedCount;
        public int blockStatus;

        public override string ToString()
        {
            var str =
                $"type:{type},targetId:{targetId},receTime:{receivedTime},sendTime:{sentTime},senderUserId:{senderUserId},title:{title},objName:{objectName},latestMessage:{latestMessageContent},rawJson:{rawJsonString}";
            return str;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_conversation_identifier
    {
        public int type; // 会话类型
        public string targetId; // 会话 ID

        public im_conversation_identifier(RCConversationIdentifier rcConvId)
        {
            type = (int) rcConvId.Type;
            targetId = rcConvId.TargetId;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_conversation_tag_info
    {
        public IntPtr tagInfo; // im_tag_info 
        [MarshalAs(UnmanagedType.U1)] public bool isTop; // 会话是否置顶

        public RCConversationTagInfo toRCConversationTagInfo()
        {
            var imTagInfo = Marshal.PtrToStructure<im_tag_info>(tagInfo);
            var rcTagInfo = new RCTagInfo(imTagInfo.tagId, imTagInfo.tagName, imTagInfo.count, imTagInfo.timeStamp);
            var rcConvTagInfo = new RCConversationTagInfo(rcTagInfo, isTop);
            return rcConvTagInfo;
        }
    }

    #endregion

    #region 聊天室

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_chat_room_member_info
    {
        public string userId;
        public Int64 joinTime;

        public RCChatRoomMemberInfo toRCChatRoomMemberInfo()
        {
            var rcChatRoomMemberInfo = new RCChatRoomMemberInfo(userId, joinTime);
            return rcChatRoomMemberInfo;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_chat_room_info
    {
        public string chatRoomId;
        public int order;
        public IntPtr members; // im_chat_room_member_info array
        public int membersLen;
        public int totalMemeberCount;

        public RCChatRoomInfo toRCChatRoomInfo()
        {
            var rcMembers = new List<RCChatRoomMemberInfo>();
            if (members != IntPtr.Zero && membersLen > 0)
            {
                int memberInfoSize = Marshal.SizeOf<im_chat_room_member_info>();
                IntPtr ptr = members;
                for (int i = 0; i < membersLen; i++)
                {
                    var imMemberInfo = Marshal.PtrToStructure<im_chat_room_member_info>(ptr + (memberInfoSize * i));
                    var rcMemberInfo = imMemberInfo.toRCChatRoomMemberInfo();
                    rcMembers.Add(rcMemberInfo);
                }
            }

            RCChatRoomInfo rcChatRoomInfo =
                new RCChatRoomInfo(chatRoomId, (RCChatRoomMemberOrder) order, rcMembers, totalMemeberCount);

            return rcChatRoomInfo;
        }
    }

    #endregion

    #region 标签

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct im_tag_info
    {
        public string tagId; // 标签唯一标识，字符型，长度不超过 10 个字
        public string tagName; // 长度不超过 15 个字，标签名称可以重复
        public int count; // 匹配的会话个数
        public Int64 timeStamp; // 时间戳由协议栈提供

        public im_tag_info(RCTagInfo tagInfo)
        {
            tagId = tagInfo.TagId;
            tagName = tagInfo.TagName;
            timeStamp = tagInfo.TimeStamp;
            count = tagInfo.Count;
        }

        public RCTagInfo toRCTagInfo()
        {
            RCTagInfo rcTagInfo = new RCTagInfo(tagId, tagName, count, timeStamp);
            return rcTagInfo;
        }
    }

    #endregion

    #endregion
}
#endif