#if UNITY_IOS
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    internal class NativeConvert
    {
        internal static im_android_push_options toAndroidPushOptions(RCIMAndroidPushOptions options)
        {
            im_android_push_options cobject;
            cobject.notificationId = options.notificationId;
            cobject.channelIdMi = options.channelIdMi;
            cobject.channelIdHW = options.channelIdHW;
            cobject.categoryHW = options.categoryHW;
            cobject.channelIdOPPO = options.channelIdOPPO;
            cobject.pushTypeVIVO = (int)options.pushTypeVIVO;
            cobject.collapseKeyFCM = options.collapseKeyFCM;
            cobject.imageUrlFCM = options.imageUrlFCM;
            cobject.importanceHW = (int)options.importanceHW;
            cobject.imageUrlHW = options.imageUrlHW;
            cobject.imageUrlMi = options.imageUrlMi;
            cobject.channelIdFCM = options.channelIdFCM;
            cobject.categoryVivo = options.categoryVivo;
            return cobject;
        }
    
        internal static RCIMAndroidPushOptions fromAndroidPushOptions(ref im_android_push_options options)
        {
            RCIMAndroidPushOptions obj = new RCIMAndroidPushOptions();
            makeAndroidPushOptions(obj, ref options);
            return obj;
        }
    
        internal static void makeAndroidPushOptions(RCIMAndroidPushOptions options, ref im_android_push_options coptions)
        {
            if (options == null)
            {
                return;
            }
            options.notificationId = coptions.notificationId;
            options.channelIdMi = coptions.channelIdMi;
            options.channelIdHW = coptions.channelIdHW;
            options.categoryHW = coptions.categoryHW;
            options.channelIdOPPO = coptions.channelIdOPPO;
            options.pushTypeVIVO = (RCIMVIVOPushType)coptions.pushTypeVIVO;
            options.collapseKeyFCM = coptions.collapseKeyFCM;
            options.imageUrlFCM = coptions.imageUrlFCM;
            options.importanceHW = (RCIMImportanceHW)coptions.importanceHW;
            options.imageUrlHW = coptions.imageUrlHW;
            options.imageUrlMi = coptions.imageUrlMi;
            options.channelIdFCM = coptions.channelIdFCM;
            options.categoryVivo = coptions.categoryVivo;
        }
    
        internal static im_message_push_options toMessagePushOptions(RCIMMessagePushOptions options)
        {
            im_message_push_options cobject;
            cobject.disableNotification = options.disableNotification;
            cobject.disablePushTitle = options.disablePushTitle;
            cobject.pushTitle = options.pushTitle;
            cobject.pushContent = options.pushContent;
            cobject.pushData = options.pushData;
            cobject.forceShowDetailContent = options.forceShowDetailContent;
            cobject.templateId = options.templateId;
            cobject.voIPPush = options.voIPPush;
            if (options.iOSPushOptions != null)
            {
                im_ios_push_options cios_push_options = toIOSPushOptions(options.iOSPushOptions);
                IntPtr iOSPushOptions_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cios_push_options));
                Marshal.StructureToPtr(cios_push_options, iOSPushOptions_ptr, false);
                cobject.iOSPushOptions = iOSPushOptions_ptr;
            }
            else
            {
                cobject.iOSPushOptions = IntPtr.Zero;
            }
            if (options.androidPushOptions != null)
            {
                im_android_push_options candroid_push_options = toAndroidPushOptions(options.androidPushOptions);
                IntPtr androidPushOptions_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(candroid_push_options));
                Marshal.StructureToPtr(candroid_push_options, androidPushOptions_ptr, false);
                cobject.androidPushOptions = androidPushOptions_ptr;
            }
            else
            {
                cobject.androidPushOptions = IntPtr.Zero;
            }
            return cobject;
        }
    
        internal static RCIMMessagePushOptions fromMessagePushOptions(ref im_message_push_options options)
        {
            RCIMMessagePushOptions obj = new RCIMMessagePushOptions();
            makeMessagePushOptions(obj, ref options);
            return obj;
        }
    
        internal static void makeMessagePushOptions(RCIMMessagePushOptions options, ref im_message_push_options coptions)
        {
            if (options == null)
            {
                return;
            }
            RCIMIOSPushOptions iOSPushOptions_cls = null;
            if (coptions.iOSPushOptions != IntPtr.Zero)
            {
                var ref_iOSPushOptions = Marshal.PtrToStructure<im_ios_push_options>(coptions.iOSPushOptions);
                iOSPushOptions_cls = fromIOSPushOptions(ref ref_iOSPushOptions);
            }
            RCIMAndroidPushOptions androidPushOptions_cls = null;
            if (coptions.androidPushOptions != IntPtr.Zero)
            {
                var ref_androidPushOptions = Marshal.PtrToStructure<im_android_push_options>(coptions.androidPushOptions);
                androidPushOptions_cls = fromAndroidPushOptions(ref ref_androidPushOptions);
            }
            options.disableNotification = coptions.disableNotification;
            options.disablePushTitle = coptions.disablePushTitle;
            options.pushTitle = coptions.pushTitle;
            options.pushContent = coptions.pushContent;
            options.pushData = coptions.pushData;
            options.forceShowDetailContent = coptions.forceShowDetailContent;
            options.templateId = coptions.templateId;
            options.voIPPush = coptions.voIPPush;
            options.iOSPushOptions = iOSPushOptions_cls;
            options.androidPushOptions = androidPushOptions_cls;
        }
    
        internal static void freeMessagePushOptions(ref im_message_push_options options)
        {
            if (options.iOSPushOptions != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(options.iOSPushOptions);
            }
            if (options.androidPushOptions != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(options.androidPushOptions);
            }
        }
    
        internal static im_ios_push_options toIOSPushOptions(RCIMIOSPushOptions options)
        {
            im_ios_push_options cobject;
            cobject.threadId = options.threadId;
            cobject.category = options.category;
            cobject.apnsCollapseId = options.apnsCollapseId;
            cobject.richMediaUri = options.richMediaUri;
            return cobject;
        }
    
        internal static RCIMIOSPushOptions fromIOSPushOptions(ref im_ios_push_options options)
        {
            RCIMIOSPushOptions obj = new RCIMIOSPushOptions();
            makeIOSPushOptions(obj, ref options);
            return obj;
        }
    
        internal static void makeIOSPushOptions(RCIMIOSPushOptions options, ref im_ios_push_options coptions)
        {
            if (options == null)
            {
                return;
            }
            options.threadId = coptions.threadId;
            options.category = coptions.category;
            options.apnsCollapseId = coptions.apnsCollapseId;
            options.richMediaUri = coptions.richMediaUri;
        }
    
        internal static im_compress_options toCompressOptions(RCIMCompressOptions options)
        {
            im_compress_options cobject;
            cobject.originalImageQuality = options.originalImageQuality;
            cobject.originalImageSize = options.originalImageSize;
            cobject.originalImageMaxSize = options.originalImageMaxSize;
            cobject.thumbnailQuality = options.thumbnailQuality;
            cobject.thumbnailMaxSize = options.thumbnailMaxSize;
            cobject.thumbnailMinSize = options.thumbnailMinSize;
            cobject.sightCompressWidth = options.sightCompressWidth;
            cobject.sightCompressHeight = options.sightCompressHeight;
            cobject.locationThumbnailQuality = options.locationThumbnailQuality;
            cobject.locationThumbnailWidth = options.locationThumbnailWidth;
            cobject.locationThumbnailHeight = options.locationThumbnailHeight;
            return cobject;
        }
    
        internal static RCIMCompressOptions fromCompressOptions(ref im_compress_options options)
        {
            RCIMCompressOptions obj = new RCIMCompressOptions();
            makeCompressOptions(obj, ref options);
            return obj;
        }
    
        internal static void makeCompressOptions(RCIMCompressOptions options, ref im_compress_options coptions)
        {
            if (options == null)
            {
                return;
            }
            options.originalImageQuality = coptions.originalImageQuality;
            options.originalImageSize = coptions.originalImageSize;
            options.originalImageMaxSize = coptions.originalImageMaxSize;
            options.thumbnailQuality = coptions.thumbnailQuality;
            options.thumbnailMaxSize = coptions.thumbnailMaxSize;
            options.thumbnailMinSize = coptions.thumbnailMinSize;
            options.sightCompressWidth = coptions.sightCompressWidth;
            options.sightCompressHeight = coptions.sightCompressHeight;
            options.locationThumbnailQuality = coptions.locationThumbnailQuality;
            options.locationThumbnailWidth = coptions.locationThumbnailWidth;
            options.locationThumbnailHeight = coptions.locationThumbnailHeight;
        }
    
        internal static im_engine_options toEngineOptions(RCIMEngineOptions options)
        {
            im_engine_options cobject;
            cobject.naviServer = options.naviServer;
            cobject.fileServer = options.fileServer;
            cobject.statisticServer = options.statisticServer;
            cobject.kickReconnectDevice = options.kickReconnectDevice;
            if (options.compressOptions != null)
            {
                im_compress_options ccompress_options = toCompressOptions(options.compressOptions);
                IntPtr compressOptions_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(ccompress_options));
                Marshal.StructureToPtr(ccompress_options, compressOptions_ptr, false);
                cobject.compressOptions = compressOptions_ptr;
            }
            else
            {
                cobject.compressOptions = IntPtr.Zero;
            }
            cobject.logLevel = (int)options.logLevel;
            if (options.pushOptions != null)
            {
                im_push_options cpush_options = toPushOptions(options.pushOptions);
                IntPtr pushOptions_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cpush_options));
                Marshal.StructureToPtr(cpush_options, pushOptions_ptr, false);
                cobject.pushOptions = pushOptions_ptr;
            }
            else
            {
                cobject.pushOptions = IntPtr.Zero;
            }
            cobject.enablePush = options.enablePush;
            cobject.enableIPC = options.enableIPC;
            return cobject;
        }
    
        internal static RCIMEngineOptions fromEngineOptions(ref im_engine_options options)
        {
            RCIMEngineOptions obj = new RCIMEngineOptions();
            makeEngineOptions(obj, ref options);
            return obj;
        }
    
        internal static void makeEngineOptions(RCIMEngineOptions options, ref im_engine_options coptions)
        {
            if (options == null)
            {
                return;
            }
            RCIMCompressOptions compressOptions_cls = null;
            if (coptions.compressOptions != IntPtr.Zero)
            {
                var ref_compressOptions = Marshal.PtrToStructure<im_compress_options>(coptions.compressOptions);
                compressOptions_cls = fromCompressOptions(ref ref_compressOptions);
            }
            RCIMPushOptions pushOptions_cls = null;
            if (coptions.pushOptions != IntPtr.Zero)
            {
                var ref_pushOptions = Marshal.PtrToStructure<im_push_options>(coptions.pushOptions);
                pushOptions_cls = fromPushOptions(ref ref_pushOptions);
            }
            options.naviServer = coptions.naviServer;
            options.fileServer = coptions.fileServer;
            options.statisticServer = coptions.statisticServer;
            options.kickReconnectDevice = coptions.kickReconnectDevice;
            options.compressOptions = compressOptions_cls;
            options.logLevel = (RCIMLogLevel)coptions.logLevel;
            options.pushOptions = pushOptions_cls;
            options.enablePush = coptions.enablePush;
            options.enableIPC = coptions.enableIPC;
        }
    
        internal static void freeEngineOptions(ref im_engine_options options)
        {
            if (options.compressOptions != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(options.compressOptions);
            }
            if (options.pushOptions != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(options.pushOptions);
            }
        }
    
        internal static im_unknown_message toUnknownMessage(RCIMUnknownMessage message)
        {
            im_unknown_message cobject;
            im_message cparent = toMessage(message);
            IntPtr parent_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cparent));
            Marshal.StructureToPtr(cparent, parent_ptr, false);
            cobject.m_parent = parent_ptr;
            cobject.rawData = message.rawData;
            cobject.objectName = message.objectName;
            return cobject;
        }
    
        internal static RCIMUnknownMessage fromUnknownMessage(ref im_unknown_message message)
        {
            RCIMUnknownMessage obj = new RCIMUnknownMessage();
            makeUnknownMessage(obj, ref message);
            return obj;
        }
    
        internal static void makeUnknownMessage(RCIMUnknownMessage message, ref im_unknown_message cmessage)
        {
            if (message == null)
            {
                return;
            }
            if (cmessage.m_parent != IntPtr.Zero)
            {
                im_message m_parent = Marshal.PtrToStructure<im_message>(cmessage.m_parent);
                makeMessage(message, ref m_parent);
            }
            message.rawData = cmessage.rawData;
            message.objectName = cmessage.objectName;
        }
    
        internal static void freeUnknownMessage(ref im_unknown_message message)
        {
            if (message.m_parent != IntPtr.Zero)
            {
                im_message free_m_parent = Marshal.PtrToStructure<im_message>(message.m_parent);
                freeMessage(ref free_m_parent);
            }
            if (message.m_parent != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(message.m_parent);
            }
        }
    
        internal static im_user_info toUserInfo(RCIMUserInfo info)
        {
            im_user_info cobject;
            cobject.userId = info.userId;
            cobject.name = info.name;
            cobject.portrait = info.portrait;
            cobject.alias = info.alias;
            cobject.extra = info.extra;
            return cobject;
        }
    
        internal static RCIMUserInfo fromUserInfo(ref im_user_info info)
        {
            RCIMUserInfo obj = new RCIMUserInfo();
            makeUserInfo(obj, ref info);
            return obj;
        }
    
        internal static void makeUserInfo(RCIMUserInfo info, ref im_user_info cinfo)
        {
            if (info == null)
            {
                return;
            }
            info.userId = cinfo.userId;
            info.name = cinfo.name;
            info.portrait = cinfo.portrait;
            info.alias = cinfo.alias;
            info.extra = cinfo.extra;
        }
    
        internal static im_custom_message toCustomMessage(RCIMCustomMessage message)
        {
            im_custom_message cobject;
            im_message cparent = toMessage(message);
            IntPtr parent_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cparent));
            Marshal.StructureToPtr(cparent, parent_ptr, false);
            cobject.m_parent = parent_ptr;
            cobject.identifier = message.identifier;
            cobject.policy = (int)message.policy;
            if (message.fields != null)
            {
                List<ios_c_map_item> fields_list = new List<ios_c_map_item>();
                foreach (var item in message.fields)
                {
                    ios_c_map_item citem;
                    citem.key = item.Key.ToString();
                    citem.value = item.Value.ToString();
                    fields_list.Add(citem);
                }
                cobject.fields = NativeUtils.GetStructMapPointer<ios_c_map_item>(ref fields_list);
            }
            else
            {
                cobject.fields = IntPtr.Zero;
            }
            return cobject;
        }
    
        internal static RCIMCustomMessage fromCustomMessage(ref im_custom_message message)
        {
            RCIMCustomMessage obj = new RCIMCustomMessage();
            makeCustomMessage(obj, ref message);
            return obj;
        }
    
        internal static void makeCustomMessage(RCIMCustomMessage message, ref im_custom_message cmessage)
        {
            if (message == null)
            {
                return;
            }
            if (cmessage.m_parent != IntPtr.Zero)
            {
                im_message m_parent = Marshal.PtrToStructure<im_message>(cmessage.m_parent);
                makeMessage(message, ref m_parent);
            }
            message.identifier = cmessage.identifier;
            message.policy = (RCIMCustomMessagePolicy)cmessage.policy;
            message.fields = NativeUtils.GetObjectMapByPtr<string, string>(cmessage.fields);
        }
    
        internal static void freeCustomMessage(ref im_custom_message message)
        {
            if (message.m_parent != IntPtr.Zero)
            {
                im_message free_m_parent = Marshal.PtrToStructure<im_message>(message.m_parent);
                freeMessage(ref free_m_parent);
            }
            if (message.m_parent != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(message.m_parent);
            }
            NativeUtils.FreeStructListByPtr(message.fields);
            if (message.fields != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(message.fields);
            }
        }
    
        internal static im_message toMessage(RCIMMessage message)
        {
            im_message cobject;
            cobject.conversationType = (int)message.conversationType;
            cobject.messageType = (int)message.messageType;
            cobject.targetId = message.targetId;
            cobject.channelId = message.channelId;
            cobject.messageId = (long)message.messageId;
            cobject.messageUId = message.messageUId;
            cobject.offLine = message.offLine;
            if (message.groupReadReceiptInfo != null)
            {
                im_group_read_receipt_info cgroup_read_receipt_info = toGroupReadReceiptInfo(message.groupReadReceiptInfo);
                IntPtr groupReadReceiptInfo_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cgroup_read_receipt_info));
                Marshal.StructureToPtr(cgroup_read_receipt_info, groupReadReceiptInfo_ptr, false);
                cobject.groupReadReceiptInfo = groupReadReceiptInfo_ptr;
            }
            else
            {
                cobject.groupReadReceiptInfo = IntPtr.Zero;
            }
            cobject.receivedTime = message.receivedTime;
            cobject.sentTime = message.sentTime;
            cobject.receivedStatus = (int)message.receivedStatus;
            cobject.sentStatus = (int)message.sentStatus;
            cobject.senderUserId = message.senderUserId;
            cobject.direction = (int)message.direction;
            if (message.userInfo != null)
            {
                im_user_info cuser_info = toUserInfo(message.userInfo);
                IntPtr userInfo_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cuser_info));
                Marshal.StructureToPtr(cuser_info, userInfo_ptr, false);
                cobject.userInfo = userInfo_ptr;
            }
            else
            {
                cobject.userInfo = IntPtr.Zero;
            }
            if (message.mentionedInfo != null)
            {
                im_mentioned_info cmentioned_info = toMentionedInfo(message.mentionedInfo);
                IntPtr mentionedInfo_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cmentioned_info));
                Marshal.StructureToPtr(cmentioned_info, mentionedInfo_ptr, false);
                cobject.mentionedInfo = mentionedInfo_ptr;
            }
            else
            {
                cobject.mentionedInfo = IntPtr.Zero;
            }
            if (message.pushOptions != null)
            {
                im_message_push_options cmessage_push_options = toMessagePushOptions(message.pushOptions);
                IntPtr pushOptions_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cmessage_push_options));
                Marshal.StructureToPtr(cmessage_push_options, pushOptions_ptr, false);
                cobject.pushOptions = pushOptions_ptr;
            }
            else
            {
                cobject.pushOptions = IntPtr.Zero;
            }
            cobject.extra = message.extra;
            if (message.expansion != null)
            {
                List<ios_c_map_item> expansion_list = new List<ios_c_map_item>();
                foreach (var item in message.expansion)
                {
                    ios_c_map_item citem;
                    citem.key = item.Key.ToString();
                    citem.value = item.Value.ToString();
                    expansion_list.Add(citem);
                }
                cobject.expansion = NativeUtils.GetStructMapPointer<ios_c_map_item>(ref expansion_list);
            }
            else
            {
                cobject.expansion = IntPtr.Zero;
            }
            return cobject;
        }
    
        internal static RCIMMessage fromMessage(ref im_message message)
        {
            RCIMMessage obj = new RCIMMessage();
            makeMessage(obj, ref message);
            return obj;
        }
    
        internal static void makeMessage(RCIMMessage message, ref im_message cmessage)
        {
            if (message == null)
            {
                return;
            }
            RCIMGroupReadReceiptInfo groupReadReceiptInfo_cls = null;
            if (cmessage.groupReadReceiptInfo != IntPtr.Zero)
            {
                var ref_groupReadReceiptInfo =
                    Marshal.PtrToStructure<im_group_read_receipt_info>(cmessage.groupReadReceiptInfo);
                groupReadReceiptInfo_cls = fromGroupReadReceiptInfo(ref ref_groupReadReceiptInfo);
            }
            RCIMUserInfo userInfo_cls = null;
            if (cmessage.userInfo != IntPtr.Zero)
            {
                var ref_userInfo = Marshal.PtrToStructure<im_user_info>(cmessage.userInfo);
                userInfo_cls = fromUserInfo(ref ref_userInfo);
            }
            RCIMMentionedInfo mentionedInfo_cls = null;
            if (cmessage.mentionedInfo != IntPtr.Zero)
            {
                var ref_mentionedInfo = Marshal.PtrToStructure<im_mentioned_info>(cmessage.mentionedInfo);
                mentionedInfo_cls = fromMentionedInfo(ref ref_mentionedInfo);
            }
            RCIMMessagePushOptions pushOptions_cls = null;
            if (cmessage.pushOptions != IntPtr.Zero)
            {
                var ref_pushOptions = Marshal.PtrToStructure<im_message_push_options>(cmessage.pushOptions);
                pushOptions_cls = fromMessagePushOptions(ref ref_pushOptions);
            }
            message.conversationType = (RCIMConversationType)cmessage.conversationType;
            message.messageType = (RCIMMessageType)cmessage.messageType;
            message.targetId = cmessage.targetId;
            message.channelId = cmessage.channelId;
            message.messageId = (int)cmessage.messageId;
            message.messageUId = cmessage.messageUId;
            message.offLine = cmessage.offLine;
            message.groupReadReceiptInfo = groupReadReceiptInfo_cls;
            message.receivedTime = cmessage.receivedTime;
            message.sentTime = cmessage.sentTime;
            message.receivedStatus = (RCIMReceivedStatus)cmessage.receivedStatus;
            message.sentStatus = (RCIMSentStatus)cmessage.sentStatus;
            message.senderUserId = cmessage.senderUserId;
            message.direction = (RCIMMessageDirection)cmessage.direction;
            message.userInfo = userInfo_cls;
            message.mentionedInfo = mentionedInfo_cls;
            message.pushOptions = pushOptions_cls;
            message.extra = cmessage.extra;
            message.expansion = NativeUtils.GetObjectMapByPtr<string, string>(cmessage.expansion);
        }
    
        internal static void freeMessage(ref im_message message)
        {
            if (message.groupReadReceiptInfo != IntPtr.Zero)
            {
                im_group_read_receipt_info free_groupReadReceiptInfo =
                    Marshal.PtrToStructure<im_group_read_receipt_info>(message.groupReadReceiptInfo);
                freeGroupReadReceiptInfo(ref free_groupReadReceiptInfo);
            }
            if (message.groupReadReceiptInfo != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(message.groupReadReceiptInfo);
            }
            if (message.userInfo != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(message.userInfo);
            }
            if (message.mentionedInfo != IntPtr.Zero)
            {
                im_mentioned_info free_mentionedInfo = Marshal.PtrToStructure<im_mentioned_info>(message.mentionedInfo);
                freeMentionedInfo(ref free_mentionedInfo);
            }
            if (message.mentionedInfo != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(message.mentionedInfo);
            }
            if (message.pushOptions != IntPtr.Zero)
            {
                im_message_push_options free_pushOptions =
                    Marshal.PtrToStructure<im_message_push_options>(message.pushOptions);
                freeMessagePushOptions(ref free_pushOptions);
            }
            if (message.pushOptions != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(message.pushOptions);
            }
            NativeUtils.FreeStructListByPtr(message.expansion);
            if (message.expansion != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(message.expansion);
            }
        }
    
        internal static ios_class_warpper toMessageWapper(RCIMMessage message)
        {
            ios_class_warpper message_wapper;
            if (message is RCIMUnknownMessage)
            {
                im_unknown_message cmessage = toUnknownMessage((RCIMUnknownMessage)message);
                IntPtr message_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cmessage));
                Marshal.StructureToPtr(cmessage, message_ptr, false);
                message_wapper.obj = message_ptr;
                message_wapper.type = "RCIMIWUnknownMessage";
            }
            else if (message is RCIMCustomMessage)
            {
                im_custom_message cmessage = toCustomMessage((RCIMCustomMessage)message);
                IntPtr message_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cmessage));
                Marshal.StructureToPtr(cmessage, message_ptr, false);
                message_wapper.obj = message_ptr;
                message_wapper.type = "RCIMIWCustomMessage";
            }
            else if (message is RCIMRecallNotificationMessage)
            {
                im_recall_notification_message cmessage =
                    toRecallNotificationMessage((RCIMRecallNotificationMessage)message);
                IntPtr message_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cmessage));
                Marshal.StructureToPtr(cmessage, message_ptr, false);
                message_wapper.obj = message_ptr;
                message_wapper.type = "RCIMIWRecallNotificationMessage";
            }
            else if (message is RCIMMediaMessage)
            {
                im_media_message cmessage = toMediaMessage((RCIMMediaMessage)message);
                IntPtr message_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cmessage));
                Marshal.StructureToPtr(cmessage, message_ptr, false);
                message_wapper.obj = message_ptr;
                message_wapper.type = "RCIMIWMediaMessage";
            }
            else if (message is RCIMTextMessage)
            {
                im_text_message cmessage = toTextMessage((RCIMTextMessage)message);
                IntPtr message_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cmessage));
                Marshal.StructureToPtr(cmessage, message_ptr, false);
                message_wapper.obj = message_ptr;
                message_wapper.type = "RCIMIWTextMessage";
            }
            else if (message is RCIMCommandMessage)
            {
                im_command_message cmessage = toCommandMessage((RCIMCommandMessage)message);
                IntPtr message_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cmessage));
                Marshal.StructureToPtr(cmessage, message_ptr, false);
                message_wapper.obj = message_ptr;
                message_wapper.type = "RCIMIWCommandMessage";
            }
            else if (message is RCIMCommandNotificationMessage)
            {
                im_command_notification_message cmessage =
                    toCommandNotificationMessage((RCIMCommandNotificationMessage)message);
                IntPtr message_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cmessage));
                Marshal.StructureToPtr(cmessage, message_ptr, false);
                message_wapper.obj = message_ptr;
                message_wapper.type = "RCIMIWCommandNotificationMessage";
            }
            else if (message is RCIMLocationMessage)
            {
                im_location_message cmessage = toLocationMessage((RCIMLocationMessage)message);
                IntPtr message_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cmessage));
                Marshal.StructureToPtr(cmessage, message_ptr, false);
                message_wapper.obj = message_ptr;
                message_wapper.type = "RCIMIWLocationMessage";
            }
            else if (message is RCIMReferenceMessage)
            {
                im_reference_message cmessage = toReferenceMessage((RCIMReferenceMessage)message);
                IntPtr message_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cmessage));
                Marshal.StructureToPtr(cmessage, message_ptr, false);
                message_wapper.obj = message_ptr;
                message_wapper.type = "RCIMIWReferenceMessage";
            }
            else
            {
                im_message cmessage = toMessage(message);
                IntPtr message_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cmessage));
                Marshal.StructureToPtr(cmessage, message_ptr, false);
                message_wapper.obj = message_ptr;
                message_wapper.type = "RCIMIWMessage";
            }
            return message_wapper;
        }
    
        internal static RCIMMessage fromMessageWapper(ref ios_class_warpper message)
        {
            if (message.obj == IntPtr.Zero)
            {
                return null;
            }
            RCIMMessage message_wapper = null;
            if (message.type == "RCIMIWUnknownMessage")
            {
                im_unknown_message cmessage = Marshal.PtrToStructure<im_unknown_message>(message.obj);
                message_wapper = fromUnknownMessage(ref cmessage);
            }
            else if (message.type == "RCIMIWCustomMessage")
            {
                im_custom_message cmessage = Marshal.PtrToStructure<im_custom_message>(message.obj);
                message_wapper = fromCustomMessage(ref cmessage);
            }
            else if (message.type == "RCIMIWRecallNotificationMessage")
            {
                im_recall_notification_message cmessage =
                    Marshal.PtrToStructure<im_recall_notification_message>(message.obj);
                message_wapper = fromRecallNotificationMessage(ref cmessage);
            }
            else if (message.type == "RCIMIWMediaMessage")
            {
                im_media_message cmessage = Marshal.PtrToStructure<im_media_message>(message.obj);
                message_wapper = fromMediaMessage(ref cmessage);
            }
            else if (message.type == "RCIMIWTextMessage")
            {
                im_text_message cmessage = Marshal.PtrToStructure<im_text_message>(message.obj);
                message_wapper = fromTextMessage(ref cmessage);
            }
            else if (message.type == "RCIMIWCommandMessage")
            {
                im_command_message cmessage = Marshal.PtrToStructure<im_command_message>(message.obj);
                message_wapper = fromCommandMessage(ref cmessage);
            }
            else if (message.type == "RCIMIWCommandNotificationMessage")
            {
                im_command_notification_message cmessage =
                    Marshal.PtrToStructure<im_command_notification_message>(message.obj);
                message_wapper = fromCommandNotificationMessage(ref cmessage);
            }
            else if (message.type == "RCIMIWLocationMessage")
            {
                im_location_message cmessage = Marshal.PtrToStructure<im_location_message>(message.obj);
                message_wapper = fromLocationMessage(ref cmessage);
            }
            else if (message.type == "RCIMIWReferenceMessage")
            {
                im_reference_message cmessage = Marshal.PtrToStructure<im_reference_message>(message.obj);
                message_wapper = fromReferenceMessage(ref cmessage);
            }
            else
            {
                im_message cmessage = Marshal.PtrToStructure<im_message>(message.obj);
                message_wapper = fromMessage(ref cmessage);
            }
            return message_wapper;
        }
    
        internal static void freeMessageWapper(ref ios_class_warpper message)
        {
            if (message.obj == IntPtr.Zero)
            {
                return;
            }
            if (message.type == "RCIMIWUnknownMessage")
            {
                im_unknown_message cmessage = Marshal.PtrToStructure<im_unknown_message>(message.obj);
                freeUnknownMessage(ref cmessage);
            }
            else if (message.type == "RCIMIWCustomMessage")
            {
                im_custom_message cmessage = Marshal.PtrToStructure<im_custom_message>(message.obj);
                freeCustomMessage(ref cmessage);
            }
            else if (message.type == "RCIMIWRecallNotificationMessage")
            {
                im_recall_notification_message cmessage =
                    Marshal.PtrToStructure<im_recall_notification_message>(message.obj);
                freeRecallNotificationMessage(ref cmessage);
            }
            else if (message.type == "RCIMIWMediaMessage")
            {
                im_media_message cmessage = Marshal.PtrToStructure<im_media_message>(message.obj);
                freeMediaMessage(ref cmessage);
            }
            else if (message.type == "RCIMIWTextMessage")
            {
                im_text_message cmessage = Marshal.PtrToStructure<im_text_message>(message.obj);
                freeTextMessage(ref cmessage);
            }
            else if (message.type == "RCIMIWCommandMessage")
            {
                im_command_message cmessage = Marshal.PtrToStructure<im_command_message>(message.obj);
                freeCommandMessage(ref cmessage);
            }
            else if (message.type == "RCIMIWCommandNotificationMessage")
            {
                im_command_notification_message cmessage =
                    Marshal.PtrToStructure<im_command_notification_message>(message.obj);
                freeCommandNotificationMessage(ref cmessage);
            }
            else if (message.type == "RCIMIWLocationMessage")
            {
                im_location_message cmessage = Marshal.PtrToStructure<im_location_message>(message.obj);
                freeLocationMessage(ref cmessage);
            }
            else if (message.type == "RCIMIWReferenceMessage")
            {
                im_reference_message cmessage = Marshal.PtrToStructure<im_reference_message>(message.obj);
                freeReferenceMessage(ref cmessage);
            }
            else
            {
                im_message cmessage = Marshal.PtrToStructure<im_message>(message.obj);
                freeMessage(ref cmessage);
            }
            Marshal.FreeHGlobal(message.obj);
        }
    
        internal static im_image_message toImageMessage(RCIMImageMessage message)
        {
            im_image_message cobject;
            im_media_message cparent = toMediaMessage(message);
            IntPtr parent_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cparent));
            Marshal.StructureToPtr(cparent, parent_ptr, false);
            cobject.m_parent = parent_ptr;
            cobject.thumbnailBase64String = message.thumbnailBase64String;
            cobject.original = message.original;
            return cobject;
        }
    
        internal static RCIMImageMessage fromImageMessage(ref im_image_message message)
        {
            RCIMImageMessage obj = new RCIMImageMessage();
            makeImageMessage(obj, ref message);
            return obj;
        }
    
        internal static void makeImageMessage(RCIMImageMessage message, ref im_image_message cmessage)
        {
            if (message == null)
            {
                return;
            }
            if (cmessage.m_parent != IntPtr.Zero)
            {
                im_media_message m_parent = Marshal.PtrToStructure<im_media_message>(cmessage.m_parent);
                makeMediaMessage(message, ref m_parent);
            }
            message.thumbnailBase64String = cmessage.thumbnailBase64String;
            message.original = cmessage.original;
        }
    
        internal static void freeImageMessage(ref im_image_message message)
        {
            if (message.m_parent != IntPtr.Zero)
            {
                im_media_message free_m_parent = Marshal.PtrToStructure<im_media_message>(message.m_parent);
                freeMediaMessage(ref free_m_parent);
            }
            if (message.m_parent != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(message.m_parent);
            }
        }
    
        internal static im_file_message toFileMessage(RCIMFileMessage message)
        {
            im_file_message cobject;
            im_media_message cparent = toMediaMessage(message);
            IntPtr parent_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cparent));
            Marshal.StructureToPtr(cparent, parent_ptr, false);
            cobject.m_parent = parent_ptr;
            cobject.name = message.name;
            cobject.fileType = message.fileType;
            cobject.size = message.size;
            return cobject;
        }
    
        internal static RCIMFileMessage fromFileMessage(ref im_file_message message)
        {
            RCIMFileMessage obj = new RCIMFileMessage();
            makeFileMessage(obj, ref message);
            return obj;
        }
    
        internal static void makeFileMessage(RCIMFileMessage message, ref im_file_message cmessage)
        {
            if (message == null)
            {
                return;
            }
            if (cmessage.m_parent != IntPtr.Zero)
            {
                im_media_message m_parent = Marshal.PtrToStructure<im_media_message>(cmessage.m_parent);
                makeMediaMessage(message, ref m_parent);
            }
            message.name = cmessage.name;
            message.fileType = cmessage.fileType;
            message.size = cmessage.size;
        }
    
        internal static void freeFileMessage(ref im_file_message message)
        {
            if (message.m_parent != IntPtr.Zero)
            {
                im_media_message free_m_parent = Marshal.PtrToStructure<im_media_message>(message.m_parent);
                freeMediaMessage(ref free_m_parent);
            }
            if (message.m_parent != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(message.m_parent);
            }
        }
    
        internal static im_recall_notification_message toRecallNotificationMessage(RCIMRecallNotificationMessage message)
        {
            im_recall_notification_message cobject;
            im_message cparent = toMessage(message);
            IntPtr parent_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cparent));
            Marshal.StructureToPtr(cparent, parent_ptr, false);
            cobject.m_parent = parent_ptr;
            cobject.admin = message.admin;
            cobject.deleted = message.deleted;
            cobject.recallTime = message.recallTime;
            cobject.recallActionTime = message.recallActionTime;
            if (message.originalMessage != null)
            {
                ios_class_warpper cmessage = toMessageWapper(message.originalMessage);
                IntPtr originalMessage_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cmessage));
                Marshal.StructureToPtr(cmessage, originalMessage_ptr, false);
                cobject.originalMessage = originalMessage_ptr;
            }
            else
            {
                cobject.originalMessage = IntPtr.Zero;
            }
            return cobject;
        }
    
        internal static RCIMRecallNotificationMessage fromRecallNotificationMessage(
            ref im_recall_notification_message message)
        {
            RCIMRecallNotificationMessage obj = new RCIMRecallNotificationMessage();
            makeRecallNotificationMessage(obj, ref message);
            return obj;
        }
    
        internal static void makeRecallNotificationMessage(RCIMRecallNotificationMessage message,
                                                           ref im_recall_notification_message cmessage)
        {
            if (message == null)
            {
                return;
            }
            RCIMMessage originalMessage_cls = null;
            if (cmessage.originalMessage != IntPtr.Zero)
            {
                var ref_originalMessage = Marshal.PtrToStructure<ios_class_warpper>(cmessage.originalMessage);
                originalMessage_cls = fromMessageWapper(ref ref_originalMessage);
            }
            if (cmessage.m_parent != IntPtr.Zero)
            {
                im_message m_parent = Marshal.PtrToStructure<im_message>(cmessage.m_parent);
                makeMessage(message, ref m_parent);
            }
            message.admin = cmessage.admin;
            message.deleted = cmessage.deleted;
            message.recallTime = cmessage.recallTime;
            message.recallActionTime = cmessage.recallActionTime;
            message.originalMessage = originalMessage_cls;
        }
    
        internal static void freeRecallNotificationMessage(ref im_recall_notification_message message)
        {
            if (message.m_parent != IntPtr.Zero)
            {
                im_message free_m_parent = Marshal.PtrToStructure<im_message>(message.m_parent);
                freeMessage(ref free_m_parent);
            }
            if (message.m_parent != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(message.m_parent);
            }
            if (message.originalMessage != IntPtr.Zero)
            {
                ios_class_warpper free_originalMessage = Marshal.PtrToStructure<ios_class_warpper>(message.originalMessage);
                freeMessageWapper(ref free_originalMessage);
            }
            if (message.originalMessage != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(message.originalMessage);
            }
        }
    
        internal static im_media_message toMediaMessage(RCIMMediaMessage message)
        {
            im_media_message cobject;
            im_message cparent = toMessage(message);
            IntPtr parent_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cparent));
            Marshal.StructureToPtr(cparent, parent_ptr, false);
            cobject.m_parent = parent_ptr;
            cobject.local = message.local;
            cobject.remote = message.remote;
            return cobject;
        }
    
        internal static RCIMMediaMessage fromMediaMessage(ref im_media_message message)
        {
            RCIMMediaMessage obj = new RCIMMediaMessage();
            makeMediaMessage(obj, ref message);
            return obj;
        }
    
        internal static void makeMediaMessage(RCIMMediaMessage message, ref im_media_message cmessage)
        {
            if (message == null)
            {
                return;
            }
            if (cmessage.m_parent != IntPtr.Zero)
            {
                im_message m_parent = Marshal.PtrToStructure<im_message>(cmessage.m_parent);
                makeMessage(message, ref m_parent);
            }
            message.local = cmessage.local;
            message.remote = cmessage.remote;
        }
    
        internal static void freeMediaMessage(ref im_media_message message)
        {
            if (message.m_parent != IntPtr.Zero)
            {
                im_message free_m_parent = Marshal.PtrToStructure<im_message>(message.m_parent);
                freeMessage(ref free_m_parent);
            }
            if (message.m_parent != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(message.m_parent);
            }
        }
    
        internal static ios_class_warpper toMediaMessageWapper(RCIMMediaMessage message)
        {
            ios_class_warpper message_wapper;
            if (message is RCIMImageMessage)
            {
                im_image_message cmessage = toImageMessage((RCIMImageMessage)message);
                IntPtr message_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cmessage));
                Marshal.StructureToPtr(cmessage, message_ptr, false);
                message_wapper.obj = message_ptr;
                message_wapper.type = "RCIMIWImageMessage";
            }
            else if (message is RCIMFileMessage)
            {
                im_file_message cmessage = toFileMessage((RCIMFileMessage)message);
                IntPtr message_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cmessage));
                Marshal.StructureToPtr(cmessage, message_ptr, false);
                message_wapper.obj = message_ptr;
                message_wapper.type = "RCIMIWFileMessage";
            }
            else if (message is RCIMGIFMessage)
            {
                im_gif_message cmessage = toGIFMessage((RCIMGIFMessage)message);
                IntPtr message_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cmessage));
                Marshal.StructureToPtr(cmessage, message_ptr, false);
                message_wapper.obj = message_ptr;
                message_wapper.type = "RCIMIWGIFMessage";
            }
            else if (message is RCIMVoiceMessage)
            {
                im_voice_message cmessage = toVoiceMessage((RCIMVoiceMessage)message);
                IntPtr message_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cmessage));
                Marshal.StructureToPtr(cmessage, message_ptr, false);
                message_wapper.obj = message_ptr;
                message_wapper.type = "RCIMIWVoiceMessage";
            }
            else if (message is RCIMSightMessage)
            {
                im_sight_message cmessage = toSightMessage((RCIMSightMessage)message);
                IntPtr message_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cmessage));
                Marshal.StructureToPtr(cmessage, message_ptr, false);
                message_wapper.obj = message_ptr;
                message_wapper.type = "RCIMIWSightMessage";
            }
            else
            {
                im_media_message cmessage = toMediaMessage(message);
                IntPtr message_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cmessage));
                Marshal.StructureToPtr(cmessage, message_ptr, false);
                message_wapper.obj = message_ptr;
                message_wapper.type = "RCIMIWMediaMessage";
            }
            return message_wapper;
        }
    
        internal static RCIMMediaMessage fromMediaMessageWapper(ref ios_class_warpper message)
        {
            if (message.obj == IntPtr.Zero)
            {
                return null;
            }
            RCIMMediaMessage message_wapper = null;
            if (message.type == "RCIMIWImageMessage")
            {
                im_image_message cmessage = Marshal.PtrToStructure<im_image_message>(message.obj);
                message_wapper = fromImageMessage(ref cmessage);
            }
            else if (message.type == "RCIMIWFileMessage")
            {
                im_file_message cmessage = Marshal.PtrToStructure<im_file_message>(message.obj);
                message_wapper = fromFileMessage(ref cmessage);
            }
            else if (message.type == "RCIMIWGIFMessage")
            {
                im_gif_message cmessage = Marshal.PtrToStructure<im_gif_message>(message.obj);
                message_wapper = fromGIFMessage(ref cmessage);
            }
            else if (message.type == "RCIMIWVoiceMessage")
            {
                im_voice_message cmessage = Marshal.PtrToStructure<im_voice_message>(message.obj);
                message_wapper = fromVoiceMessage(ref cmessage);
            }
            else if (message.type == "RCIMIWSightMessage")
            {
                im_sight_message cmessage = Marshal.PtrToStructure<im_sight_message>(message.obj);
                message_wapper = fromSightMessage(ref cmessage);
            }
            else
            {
                im_media_message cmessage = Marshal.PtrToStructure<im_media_message>(message.obj);
                message_wapper = fromMediaMessage(ref cmessage);
            }
            return message_wapper;
        }
    
        internal static void freeMediaMessageWapper(ref ios_class_warpper message)
        {
            if (message.obj == IntPtr.Zero)
            {
                return;
            }
            if (message.type == "RCIMIWImageMessage")
            {
                im_image_message cmessage = Marshal.PtrToStructure<im_image_message>(message.obj);
                freeImageMessage(ref cmessage);
            }
            else if (message.type == "RCIMIWFileMessage")
            {
                im_file_message cmessage = Marshal.PtrToStructure<im_file_message>(message.obj);
                freeFileMessage(ref cmessage);
            }
            else if (message.type == "RCIMIWGIFMessage")
            {
                im_gif_message cmessage = Marshal.PtrToStructure<im_gif_message>(message.obj);
                freeGIFMessage(ref cmessage);
            }
            else if (message.type == "RCIMIWVoiceMessage")
            {
                im_voice_message cmessage = Marshal.PtrToStructure<im_voice_message>(message.obj);
                freeVoiceMessage(ref cmessage);
            }
            else if (message.type == "RCIMIWSightMessage")
            {
                im_sight_message cmessage = Marshal.PtrToStructure<im_sight_message>(message.obj);
                freeSightMessage(ref cmessage);
            }
            else
            {
                im_media_message cmessage = Marshal.PtrToStructure<im_media_message>(message.obj);
                freeMediaMessage(ref cmessage);
            }
            Marshal.FreeHGlobal(message.obj);
        }
    
        internal static im_text_message toTextMessage(RCIMTextMessage message)
        {
            im_text_message cobject;
            im_message cparent = toMessage(message);
            IntPtr parent_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cparent));
            Marshal.StructureToPtr(cparent, parent_ptr, false);
            cobject.m_parent = parent_ptr;
            cobject.text = message.text;
            return cobject;
        }
    
        internal static RCIMTextMessage fromTextMessage(ref im_text_message message)
        {
            RCIMTextMessage obj = new RCIMTextMessage();
            makeTextMessage(obj, ref message);
            return obj;
        }
    
        internal static void makeTextMessage(RCIMTextMessage message, ref im_text_message cmessage)
        {
            if (message == null)
            {
                return;
            }
            if (cmessage.m_parent != IntPtr.Zero)
            {
                im_message m_parent = Marshal.PtrToStructure<im_message>(cmessage.m_parent);
                makeMessage(message, ref m_parent);
            }
            message.text = cmessage.text;
        }
    
        internal static void freeTextMessage(ref im_text_message message)
        {
            if (message.m_parent != IntPtr.Zero)
            {
                im_message free_m_parent = Marshal.PtrToStructure<im_message>(message.m_parent);
                freeMessage(ref free_m_parent);
            }
            if (message.m_parent != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(message.m_parent);
            }
        }
    
        internal static im_gif_message toGIFMessage(RCIMGIFMessage message)
        {
            im_gif_message cobject;
            im_media_message cparent = toMediaMessage(message);
            IntPtr parent_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cparent));
            Marshal.StructureToPtr(cparent, parent_ptr, false);
            cobject.m_parent = parent_ptr;
            cobject.dataSize = message.dataSize;
            cobject.width = message.width;
            cobject.height = message.height;
            return cobject;
        }
    
        internal static RCIMGIFMessage fromGIFMessage(ref im_gif_message message)
        {
            RCIMGIFMessage obj = new RCIMGIFMessage();
            makeGIFMessage(obj, ref message);
            return obj;
        }
    
        internal static void makeGIFMessage(RCIMGIFMessage message, ref im_gif_message cmessage)
        {
            if (message == null)
            {
                return;
            }
            if (cmessage.m_parent != IntPtr.Zero)
            {
                im_media_message m_parent = Marshal.PtrToStructure<im_media_message>(cmessage.m_parent);
                makeMediaMessage(message, ref m_parent);
            }
            message.dataSize = cmessage.dataSize;
            message.width = cmessage.width;
            message.height = cmessage.height;
        }
    
        internal static void freeGIFMessage(ref im_gif_message message)
        {
            if (message.m_parent != IntPtr.Zero)
            {
                im_media_message free_m_parent = Marshal.PtrToStructure<im_media_message>(message.m_parent);
                freeMediaMessage(ref free_m_parent);
            }
            if (message.m_parent != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(message.m_parent);
            }
        }
    
        internal static im_command_message toCommandMessage(RCIMCommandMessage message)
        {
            im_command_message cobject;
            im_message cparent = toMessage(message);
            IntPtr parent_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cparent));
            Marshal.StructureToPtr(cparent, parent_ptr, false);
            cobject.m_parent = parent_ptr;
            cobject.name = message.name;
            cobject.data = message.data;
            return cobject;
        }
    
        internal static RCIMCommandMessage fromCommandMessage(ref im_command_message message)
        {
            RCIMCommandMessage obj = new RCIMCommandMessage();
            makeCommandMessage(obj, ref message);
            return obj;
        }
    
        internal static void makeCommandMessage(RCIMCommandMessage message, ref im_command_message cmessage)
        {
            if (message == null)
            {
                return;
            }
            if (cmessage.m_parent != IntPtr.Zero)
            {
                im_message m_parent = Marshal.PtrToStructure<im_message>(cmessage.m_parent);
                makeMessage(message, ref m_parent);
            }
            message.name = cmessage.name;
            message.data = cmessage.data;
        }
    
        internal static void freeCommandMessage(ref im_command_message message)
        {
            if (message.m_parent != IntPtr.Zero)
            {
                im_message free_m_parent = Marshal.PtrToStructure<im_message>(message.m_parent);
                freeMessage(ref free_m_parent);
            }
            if (message.m_parent != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(message.m_parent);
            }
        }
    
        internal static im_voice_message toVoiceMessage(RCIMVoiceMessage message)
        {
            im_voice_message cobject;
            im_media_message cparent = toMediaMessage(message);
            IntPtr parent_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cparent));
            Marshal.StructureToPtr(cparent, parent_ptr, false);
            cobject.m_parent = parent_ptr;
            cobject.duration = (long)message.duration;
            return cobject;
        }
    
        internal static RCIMVoiceMessage fromVoiceMessage(ref im_voice_message message)
        {
            RCIMVoiceMessage obj = new RCIMVoiceMessage();
            makeVoiceMessage(obj, ref message);
            return obj;
        }
    
        internal static void makeVoiceMessage(RCIMVoiceMessage message, ref im_voice_message cmessage)
        {
            if (message == null)
            {
                return;
            }
            if (cmessage.m_parent != IntPtr.Zero)
            {
                im_media_message m_parent = Marshal.PtrToStructure<im_media_message>(cmessage.m_parent);
                makeMediaMessage(message, ref m_parent);
            }
            message.duration = (int)cmessage.duration;
        }
    
        internal static void freeVoiceMessage(ref im_voice_message message)
        {
            if (message.m_parent != IntPtr.Zero)
            {
                im_media_message free_m_parent = Marshal.PtrToStructure<im_media_message>(message.m_parent);
                freeMediaMessage(ref free_m_parent);
            }
            if (message.m_parent != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(message.m_parent);
            }
        }
    
        internal static im_mentioned_info toMentionedInfo(RCIMMentionedInfo info)
        {
            im_mentioned_info cobject;
            cobject.type = (int)info.type;
            if (info.userIdList != null)
            {
                cobject.userIdList = NativeUtils.GetStringListPointer(info.userIdList);
            }
            else
            {
                cobject.userIdList = IntPtr.Zero;
            }
            cobject.mentionedContent = info.mentionedContent;
            return cobject;
        }
    
        internal static RCIMMentionedInfo fromMentionedInfo(ref im_mentioned_info info)
        {
            RCIMMentionedInfo obj = new RCIMMentionedInfo();
            makeMentionedInfo(obj, ref info);
            return obj;
        }
    
        internal static void makeMentionedInfo(RCIMMentionedInfo info, ref im_mentioned_info cinfo)
        {
            if (info == null)
            {
                return;
            }
            List<string> userIdList_list = null;
            if (cinfo.userIdList != IntPtr.Zero)
            {
                userIdList_list = NativeUtils.GetStringListByPtr(cinfo.userIdList);
            }
            info.type = (RCIMMentionedType)cinfo.type;
            info.userIdList = userIdList_list;
            info.mentionedContent = cinfo.mentionedContent;
        }
    
        internal static void freeMentionedInfo(ref im_mentioned_info info)
        {
            NativeUtils.FreeStringListByPtr(info.userIdList);
            if (info.userIdList != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(info.userIdList);
            }
        }
    
        internal static im_command_notification_message toCommandNotificationMessage(RCIMCommandNotificationMessage message)
        {
            im_command_notification_message cobject;
            im_message cparent = toMessage(message);
            IntPtr parent_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cparent));
            Marshal.StructureToPtr(cparent, parent_ptr, false);
            cobject.m_parent = parent_ptr;
            cobject.name = message.name;
            cobject.data = message.data;
            return cobject;
        }
    
        internal static RCIMCommandNotificationMessage fromCommandNotificationMessage(
            ref im_command_notification_message message)
        {
            RCIMCommandNotificationMessage obj = new RCIMCommandNotificationMessage();
            makeCommandNotificationMessage(obj, ref message);
            return obj;
        }
    
        internal static void makeCommandNotificationMessage(RCIMCommandNotificationMessage message,
                                                            ref im_command_notification_message cmessage)
        {
            if (message == null)
            {
                return;
            }
            if (cmessage.m_parent != IntPtr.Zero)
            {
                im_message m_parent = Marshal.PtrToStructure<im_message>(cmessage.m_parent);
                makeMessage(message, ref m_parent);
            }
            message.name = cmessage.name;
            message.data = cmessage.data;
        }
    
        internal static void freeCommandNotificationMessage(ref im_command_notification_message message)
        {
            if (message.m_parent != IntPtr.Zero)
            {
                im_message free_m_parent = Marshal.PtrToStructure<im_message>(message.m_parent);
                freeMessage(ref free_m_parent);
            }
            if (message.m_parent != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(message.m_parent);
            }
        }
    
        internal static im_sight_message toSightMessage(RCIMSightMessage message)
        {
            im_sight_message cobject;
            im_media_message cparent = toMediaMessage(message);
            IntPtr parent_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cparent));
            Marshal.StructureToPtr(cparent, parent_ptr, false);
            cobject.m_parent = parent_ptr;
            cobject.duration = (long)message.duration;
            cobject.size = message.size;
            cobject.name = message.name;
            cobject.thumbnailBase64String = message.thumbnailBase64String;
            return cobject;
        }
    
        internal static RCIMSightMessage fromSightMessage(ref im_sight_message message)
        {
            RCIMSightMessage obj = new RCIMSightMessage();
            makeSightMessage(obj, ref message);
            return obj;
        }
    
        internal static void makeSightMessage(RCIMSightMessage message, ref im_sight_message cmessage)
        {
            if (message == null)
            {
                return;
            }
            if (cmessage.m_parent != IntPtr.Zero)
            {
                im_media_message m_parent = Marshal.PtrToStructure<im_media_message>(cmessage.m_parent);
                makeMediaMessage(message, ref m_parent);
            }
            message.duration = (int)cmessage.duration;
            message.size = cmessage.size;
            message.name = cmessage.name;
            message.thumbnailBase64String = cmessage.thumbnailBase64String;
        }
    
        internal static void freeSightMessage(ref im_sight_message message)
        {
            if (message.m_parent != IntPtr.Zero)
            {
                im_media_message free_m_parent = Marshal.PtrToStructure<im_media_message>(message.m_parent);
                freeMediaMessage(ref free_m_parent);
            }
            if (message.m_parent != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(message.m_parent);
            }
        }
    
        internal static im_location_message toLocationMessage(RCIMLocationMessage message)
        {
            im_location_message cobject;
            im_message cparent = toMessage(message);
            IntPtr parent_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cparent));
            Marshal.StructureToPtr(cparent, parent_ptr, false);
            cobject.m_parent = parent_ptr;
            cobject.longitude = message.longitude;
            cobject.latitude = message.latitude;
            cobject.poiName = message.poiName;
            cobject.thumbnailPath = message.thumbnailPath;
            return cobject;
        }
    
        internal static RCIMLocationMessage fromLocationMessage(ref im_location_message message)
        {
            RCIMLocationMessage obj = new RCIMLocationMessage();
            makeLocationMessage(obj, ref message);
            return obj;
        }
    
        internal static void makeLocationMessage(RCIMLocationMessage message, ref im_location_message cmessage)
        {
            if (message == null)
            {
                return;
            }
            if (cmessage.m_parent != IntPtr.Zero)
            {
                im_message m_parent = Marshal.PtrToStructure<im_message>(cmessage.m_parent);
                makeMessage(message, ref m_parent);
            }
            message.longitude = cmessage.longitude;
            message.latitude = cmessage.latitude;
            message.poiName = cmessage.poiName;
            message.thumbnailPath = cmessage.thumbnailPath;
        }
    
        internal static void freeLocationMessage(ref im_location_message message)
        {
            if (message.m_parent != IntPtr.Zero)
            {
                im_message free_m_parent = Marshal.PtrToStructure<im_message>(message.m_parent);
                freeMessage(ref free_m_parent);
            }
            if (message.m_parent != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(message.m_parent);
            }
        }
    
        internal static im_reference_message toReferenceMessage(RCIMReferenceMessage message)
        {
            im_reference_message cobject;
            im_message cparent = toMessage(message);
            IntPtr parent_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cparent));
            Marshal.StructureToPtr(cparent, parent_ptr, false);
            cobject.m_parent = parent_ptr;
            cobject.text = message.text;
            if (message.referenceMessage != null)
            {
                ios_class_warpper cmessage = toMessageWapper(message.referenceMessage);
                IntPtr referenceMessage_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cmessage));
                Marshal.StructureToPtr(cmessage, referenceMessage_ptr, false);
                cobject.referenceMessage = referenceMessage_ptr;
            }
            else
            {
                cobject.referenceMessage = IntPtr.Zero;
            }
            return cobject;
        }
    
        internal static RCIMReferenceMessage fromReferenceMessage(ref im_reference_message message)
        {
            RCIMReferenceMessage obj = new RCIMReferenceMessage();
            makeReferenceMessage(obj, ref message);
            return obj;
        }
    
        internal static void makeReferenceMessage(RCIMReferenceMessage message, ref im_reference_message cmessage)
        {
            if (message == null)
            {
                return;
            }
            RCIMMessage referenceMessage_cls = null;
            if (cmessage.referenceMessage != IntPtr.Zero)
            {
                var ref_referenceMessage = Marshal.PtrToStructure<ios_class_warpper>(cmessage.referenceMessage);
                referenceMessage_cls = fromMessageWapper(ref ref_referenceMessage);
            }
            if (cmessage.m_parent != IntPtr.Zero)
            {
                im_message m_parent = Marshal.PtrToStructure<im_message>(cmessage.m_parent);
                makeMessage(message, ref m_parent);
            }
            message.text = cmessage.text;
            message.referenceMessage = referenceMessage_cls;
        }
    
        internal static void freeReferenceMessage(ref im_reference_message message)
        {
            if (message.m_parent != IntPtr.Zero)
            {
                im_message free_m_parent = Marshal.PtrToStructure<im_message>(message.m_parent);
                freeMessage(ref free_m_parent);
            }
            if (message.m_parent != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(message.m_parent);
            }
            if (message.referenceMessage != IntPtr.Zero)
            {
                ios_class_warpper free_referenceMessage =
                    Marshal.PtrToStructure<ios_class_warpper>(message.referenceMessage);
                freeMessageWapper(ref free_referenceMessage);
            }
            if (message.referenceMessage != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(message.referenceMessage);
            }
        }
    
        internal static im_blocked_message_info toBlockedMessageInfo(RCIMBlockedMessageInfo info)
        {
            im_blocked_message_info cobject;
            cobject.conversationType = (int)info.conversationType;
            cobject.targetId = info.targetId;
            cobject.blockedMsgUId = info.blockedMsgUId;
            cobject.blockType = (int)info.blockType;
            cobject.extra = info.extra;
            return cobject;
        }
    
        internal static RCIMBlockedMessageInfo fromBlockedMessageInfo(ref im_blocked_message_info info)
        {
            RCIMBlockedMessageInfo obj = new RCIMBlockedMessageInfo();
            makeBlockedMessageInfo(obj, ref info);
            return obj;
        }
    
        internal static void makeBlockedMessageInfo(RCIMBlockedMessageInfo info, ref im_blocked_message_info cinfo)
        {
            if (info == null)
            {
                return;
            }
            info.conversationType = (RCIMConversationType)cinfo.conversationType;
            info.targetId = cinfo.targetId;
            info.blockedMsgUId = cinfo.blockedMsgUId;
            info.blockType = (RCIMMessageBlockType)cinfo.blockType;
            info.extra = cinfo.extra;
        }
    
        internal static im_typing_status toTypingStatus(RCIMTypingStatus status)
        {
            im_typing_status cobject;
            cobject.userId = status.userId;
            cobject.contentType = status.contentType;
            cobject.sentTime = status.sentTime;
            return cobject;
        }
    
        internal static RCIMTypingStatus fromTypingStatus(ref im_typing_status status)
        {
            RCIMTypingStatus obj = new RCIMTypingStatus();
            makeTypingStatus(obj, ref status);
            return obj;
        }
    
        internal static void makeTypingStatus(RCIMTypingStatus status, ref im_typing_status cstatus)
        {
            if (status == null)
            {
                return;
            }
            status.userId = cstatus.userId;
            status.contentType = cstatus.contentType;
            status.sentTime = cstatus.sentTime;
        }
    
        internal static im_ultra_group_typing_status_info toUltraGroupTypingStatusInfo(RCIMUltraGroupTypingStatusInfo info)
        {
            im_ultra_group_typing_status_info cobject;
            cobject.targetId = info.targetId;
            cobject.channelId = info.channelId;
            cobject.userId = info.userId;
            cobject.userNums = (long)info.userNums;
            cobject.status = (int)info.status;
            cobject.timestamp = info.timestamp;
            return cobject;
        }
    
        internal static RCIMUltraGroupTypingStatusInfo fromUltraGroupTypingStatusInfo(
            ref im_ultra_group_typing_status_info info)
        {
            RCIMUltraGroupTypingStatusInfo obj = new RCIMUltraGroupTypingStatusInfo();
            makeUltraGroupTypingStatusInfo(obj, ref info);
            return obj;
        }
    
        internal static void makeUltraGroupTypingStatusInfo(RCIMUltraGroupTypingStatusInfo info,
                                                            ref im_ultra_group_typing_status_info cinfo)
        {
            if (info == null)
            {
                return;
            }
            info.targetId = cinfo.targetId;
            info.channelId = cinfo.channelId;
            info.userId = cinfo.userId;
            info.userNums = (int)cinfo.userNums;
            info.status = (RCIMUltraGroupTypingStatus)cinfo.status;
            info.timestamp = cinfo.timestamp;
        }
    
        internal static im_group_read_receipt_info toGroupReadReceiptInfo(RCIMGroupReadReceiptInfo info)
        {
            im_group_read_receipt_info cobject;
            cobject.readReceiptMessage = info.readReceiptMessage;
            cobject.hasRespond = info.hasRespond;
            if (info.respondUserIds != null)
            {
                List<ios_c_map_item> respondUserIds_list = new List<ios_c_map_item>();
                foreach (var item in info.respondUserIds)
                {
                    ios_c_map_item citem;
                    citem.key = item.Key.ToString();
                    citem.value = item.Value.ToString();
                    respondUserIds_list.Add(citem);
                }
                cobject.respondUserIds = NativeUtils.GetStructMapPointer<ios_c_map_item>(ref respondUserIds_list);
            }
            else
            {
                cobject.respondUserIds = IntPtr.Zero;
            }
            return cobject;
        }
    
        internal static RCIMGroupReadReceiptInfo fromGroupReadReceiptInfo(ref im_group_read_receipt_info info)
        {
            RCIMGroupReadReceiptInfo obj = new RCIMGroupReadReceiptInfo();
            makeGroupReadReceiptInfo(obj, ref info);
            return obj;
        }
    
        internal static void makeGroupReadReceiptInfo(RCIMGroupReadReceiptInfo info, ref im_group_read_receipt_info cinfo)
        {
            if (info == null)
            {
                return;
            }
            info.readReceiptMessage = cinfo.readReceiptMessage;
            info.hasRespond = cinfo.hasRespond;
            info.respondUserIds = NativeUtils.GetObjectMapByPtr<string, long>(cinfo.respondUserIds);
        }
    
        internal static void freeGroupReadReceiptInfo(ref im_group_read_receipt_info info)
        {
            NativeUtils.FreeStructListByPtr(info.respondUserIds);
            if (info.respondUserIds != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(info.respondUserIds);
            }
        }
    
        internal static im_chat_room_member_action toChatRoomMemberAction(RCIMChatRoomMemberAction action)
        {
            im_chat_room_member_action cobject;
            cobject.userId = action.userId;
            cobject.actionType = (int)action.actionType;
            return cobject;
        }
    
        internal static RCIMChatRoomMemberAction fromChatRoomMemberAction(ref im_chat_room_member_action action)
        {
            RCIMChatRoomMemberAction obj = new RCIMChatRoomMemberAction();
            makeChatRoomMemberAction(obj, ref action);
            return obj;
        }
    
        internal static void makeChatRoomMemberAction(RCIMChatRoomMemberAction action,
                                                      ref im_chat_room_member_action caction)
        {
            if (action == null)
            {
                return;
            }
            action.userId = caction.userId;
            action.actionType = (RCIMChatRoomMemberActionType)caction.actionType;
        }
    
        internal static im_search_conversation_result toSearchConversationResult(RCIMSearchConversationResult result)
        {
            im_search_conversation_result cobject;
            if (result.conversation != null)
            {
                im_conversation cconversation = toConversation(result.conversation);
                IntPtr conversation_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cconversation));
                Marshal.StructureToPtr(cconversation, conversation_ptr, false);
                cobject.conversation = conversation_ptr;
            }
            else
            {
                cobject.conversation = IntPtr.Zero;
            }
            cobject.count = result.count;
            return cobject;
        }
    
        internal static RCIMSearchConversationResult fromSearchConversationResult(ref im_search_conversation_result result)
        {
            RCIMSearchConversationResult obj = new RCIMSearchConversationResult();
            makeSearchConversationResult(obj, ref result);
            return obj;
        }
    
        internal static void makeSearchConversationResult(RCIMSearchConversationResult result,
                                                          ref im_search_conversation_result cresult)
        {
            if (result == null)
            {
                return;
            }
            RCIMConversation conversation_cls = null;
            if (cresult.conversation != IntPtr.Zero)
            {
                var ref_conversation = Marshal.PtrToStructure<im_conversation>(cresult.conversation);
                conversation_cls = fromConversation(ref ref_conversation);
            }
            result.conversation = conversation_cls;
            result.count = cresult.count;
        }
    
        internal static void freeSearchConversationResult(ref im_search_conversation_result result)
        {
            if (result.conversation != IntPtr.Zero)
            {
                im_conversation free_conversation = Marshal.PtrToStructure<im_conversation>(result.conversation);
                freeConversation(ref free_conversation);
            }
            if (result.conversation != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(result.conversation);
            }
        }
    
        internal static im_conversation toConversation(RCIMConversation conversation)
        {
            im_conversation cobject;
            cobject.conversationType = (int)conversation.conversationType;
            cobject.targetId = conversation.targetId;
            cobject.channelId = conversation.channelId;
            cobject.unreadCount = (long)conversation.unreadCount;
            cobject.mentionedCount = (long)conversation.mentionedCount;
            cobject.top = conversation.top;
            cobject.draft = conversation.draft;
            if (conversation.lastMessage != null)
            {
                ios_class_warpper cmessage = toMessageWapper(conversation.lastMessage);
                IntPtr lastMessage_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(cmessage));
                Marshal.StructureToPtr(cmessage, lastMessage_ptr, false);
                cobject.lastMessage = lastMessage_ptr;
            }
            else
            {
                cobject.lastMessage = IntPtr.Zero;
            }
            cobject.notificationLevel = (int)conversation.notificationLevel;
            cobject.firstUnreadMsgSendTime = conversation.firstUnreadMsgSendTime;
            cobject.operationTime = conversation.operationTime;
            return cobject;
        }
    
        internal static RCIMConversation fromConversation(ref im_conversation conversation)
        {
            RCIMConversation obj = new RCIMConversation();
            makeConversation(obj, ref conversation);
            return obj;
        }
    
        internal static void makeConversation(RCIMConversation conversation, ref im_conversation cconversation)
        {
            if (conversation == null)
            {
                return;
            }
            RCIMMessage lastMessage_cls = null;
            if (cconversation.lastMessage != IntPtr.Zero)
            {
                var ref_lastMessage = Marshal.PtrToStructure<ios_class_warpper>(cconversation.lastMessage);
                lastMessage_cls = fromMessageWapper(ref ref_lastMessage);
            }
            conversation.conversationType = (RCIMConversationType)cconversation.conversationType;
            conversation.targetId = cconversation.targetId;
            conversation.channelId = cconversation.channelId;
            conversation.unreadCount = (int)cconversation.unreadCount;
            conversation.mentionedCount = (int)cconversation.mentionedCount;
            conversation.top = cconversation.top;
            conversation.draft = cconversation.draft;
            conversation.lastMessage = lastMessage_cls;
            conversation.notificationLevel = (RCIMPushNotificationLevel)cconversation.notificationLevel;
            conversation.firstUnreadMsgSendTime = cconversation.firstUnreadMsgSendTime;
            conversation.operationTime = cconversation.operationTime;
        }
    
        internal static void freeConversation(ref im_conversation conversation)
        {
            if (conversation.lastMessage != IntPtr.Zero)
            {
                ios_class_warpper free_lastMessage = Marshal.PtrToStructure<ios_class_warpper>(conversation.lastMessage);
                freeMessageWapper(ref free_lastMessage);
            }
            if (conversation.lastMessage != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(conversation.lastMessage);
            }
        }
    
        internal static im_push_options toPushOptions(RCIMPushOptions options)
        {
            im_push_options cobject;
            cobject.idMI = options.idMI;
            cobject.appKeyMI = options.appKeyMI;
            cobject.appIdMeizu = options.appIdMeizu;
            cobject.appKeyMeizu = options.appKeyMeizu;
            cobject.appKeyOPPO = options.appKeyOPPO;
            cobject.appSecretOPPO = options.appSecretOPPO;
            cobject.enableHWPush = options.enableHWPush;
            cobject.enableFCM = options.enableFCM;
            cobject.enableVIVOPush = options.enableVIVOPush;
            return cobject;
        }
    
        internal static RCIMPushOptions fromPushOptions(ref im_push_options options)
        {
            RCIMPushOptions obj = new RCIMPushOptions();
            makePushOptions(obj, ref options);
            return obj;
        }
    
        internal static void makePushOptions(RCIMPushOptions options, ref im_push_options coptions)
        {
            if (options == null)
            {
                return;
            }
            options.idMI = coptions.idMI;
            options.appKeyMI = coptions.appKeyMI;
            options.appIdMeizu = coptions.appIdMeizu;
            options.appKeyMeizu = coptions.appKeyMeizu;
            options.appKeyOPPO = coptions.appKeyOPPO;
            options.appSecretOPPO = coptions.appSecretOPPO;
            options.enableHWPush = coptions.enableHWPush;
            options.enableFCM = coptions.enableFCM;
            options.enableVIVOPush = coptions.enableVIVOPush;
        }
    }
}
#endif
