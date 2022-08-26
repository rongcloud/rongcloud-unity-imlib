//
//  Copyright © 2021 RongCloud. All rights reserved.
//

#if UNITY_STANDALONE_WIN
using System;
using System.Runtime.InteropServices;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace cn_rongcloud_im_unity
{
    internal static class RCIMUtils
    {
        #region Var

        private const string TextMsgObjectName = "RC:TxtMsg";
        private const string RichTextMsgObjectName = "RC:ImgTextMsg";
        private const string ImageMessageObjectName = "RC:ImgMsg";
        private const string GIFMessageObjectName = "RC:GIFMsg";
        private const string VoiceMessageObjectName = "RC:VcMsg";
        private const string HQVoiceMessageObjectName = "RC:HQVCMsg";

        private const string ReferenceMessageObjectName = "RC:ReferenceMsg";

        // private const string CombineMessageObjectName = "RC:CombineMsg";
        private const string SightMessageObjectName = "RC:SightMsg";
        private const string FileMessageObjectName = "RC:FileMsg";
        private const string RecallNotificationMessageObjectName = "RC:RcNtf";
        private const string GroupNotificationMessageObjectName = "RC:GrpNtf";
        private const string ChatroomKVNotificationMessageObjectName = "RC:chrmKVNotiMsg";
        private const string ChatRoomMemberActionMessageObjectName = "RC:ChrmMemChange";
        private const string UnknownMessageObjectName = "RC:UnknownMsg";

        // private const string LocationMessageObjectName = "RC:LBSMsg";

        private const string WrapperCommandObjectName = "RC:IWCmdMsg";
        private const string WrapperStorageObjectName = "RC:IWStorageMsg";
        private const string WrapperNormalObjectName = "RC:IWNormalMsg";
        private const string WrapperStatusObjectName = "RC:IWStatusMsg";

        #endregion

        #region callback map

        internal static ConcurrentDictionary<Int64, object> _CallbackObjMap = new ConcurrentDictionary<Int64, object>();

        static Int64 _seqId;

        static Int64 GetSeqId()
        {
            _seqId++;
            return _seqId;
        }

        internal static Int64 AddCallback<T>(T callback)
        {
            if (callback != null)
            {
                Int64 handle = GetSeqId();
                _CallbackObjMap[handle] = callback;
                return handle;
            }

            return -1;
        }

        internal static T GetCallback<T>(Int64 handle) where T : class
        {
            object callback = _CallbackObjMap[handle];
            if (callback != null)
            {
                T result = callback as T;
                return result;
            }

            return null;
        }

        internal static T TakeCallback<T>(Int64 handle) where T : class
        {
            _CallbackObjMap.TryRemove(handle, out var callback);
            return callback as T;
        }

        #endregion

        #region 连接状态枚举值转换

        internal static RCConnectionStatus convertConnectionStatusIOS(int originCode)
        {
            if (originCode == 0)
            {
                return RCConnectionStatus.Connected;
            }
            else if (originCode == 10)
            {
                return RCConnectionStatus.Connecting;
            }
            else if (originCode == 6)
            {
                return RCConnectionStatus.KickedByOtherClient;
            }
            else if (originCode == 1)
            {
                return RCConnectionStatus.NetworkUnavailable;
            }
            else if (originCode == 15)
            {
                return RCConnectionStatus.TokenIncorrect;
            }
            else if (originCode == 16)
            {
                return RCConnectionStatus.UserBlocked;
            }
            else if (originCode == 12)
            {
                return RCConnectionStatus.DisConnected;
            }
            else if (originCode == 13)
            {
                return RCConnectionStatus.Suspend;
            }
            else if (originCode == 14)
            {
                return RCConnectionStatus.Timeout;
            }

            return RCConnectionStatus.Unknown;
        }

        #endregion

        #region 消息

        private static void writeExpansionDicToImMsg(Dictionary<string, string> dic, ref im_message imMsg)
        {
            var count = dic.Count;
            if (count > 0)
            {
                IntPtr[] ptrKeyArray = new IntPtr[count];
                IntPtr[] ptrValueArray = new IntPtr[count];
                int i = 0;
                foreach (KeyValuePair<string, string> entry in dic)
                {
                    ptrKeyArray[i] = Marshal.StringToHGlobalAnsi(entry.Key);
                    ptrValueArray[i] = Marshal.StringToHGlobalAnsi(entry.Value);
                    i++;
                }

                int intPtrSize = Marshal.SizeOf<IntPtr>();
                int size = (intPtrSize * count);

                IntPtr ptrKey = Marshal.AllocHGlobal(size);
                IntPtr ptrValue = Marshal.AllocHGlobal(size);

                for (int j = 0; j < count; j++)
                {
                    Marshal.StructureToPtr(ptrKeyArray[j], ptrKey + (intPtrSize * j), false);
                    Marshal.StructureToPtr(ptrValueArray[j], ptrValue + (intPtrSize * j), false);
                }

                imMsg.expansionCount = count;
                imMsg.expansionKeys = ptrKey;
                imMsg.expansionValues = ptrValue;
            }
        }

        internal static IntPtr getKeysIntPtrFromDictionary(IDictionary<string, string> dic)
        {
            if (dic == null || dic.Count == 0)
                return IntPtr.Zero;
            var count = dic.Count;
            if (count > 0)
            {
                IntPtr[] ptrKeyArray = new IntPtr[count];
                int i = 0;
                foreach (KeyValuePair<string, string> entry in dic)
                {
                    ptrKeyArray[i] = Marshal.StringToHGlobalAnsi(entry.Key);
                    i++;
                }

                int intPtrSize = Marshal.SizeOf<IntPtr>();
                int size = (intPtrSize * count);

                IntPtr ptrKey = Marshal.AllocHGlobal(size);

                for (int j = 0; j < count; j++)
                {
                    Marshal.StructureToPtr(ptrKeyArray[j], ptrKey + (intPtrSize * j), false);
                }
                return ptrKey;
            }
            return IntPtr.Zero;
        }

        internal static IntPtr getValuesIntPtrFromDictionary(IDictionary<string, string> dic)
        {
            if (dic == null || dic.Count == 0)
                return IntPtr.Zero;
            var count = dic.Count;
            if (count > 0)
            {
                IntPtr[] ptrValueArray = new IntPtr[count];
                int i = 0;
                foreach (KeyValuePair<string, string> entry in dic)
                {
                    ptrValueArray[i] = Marshal.StringToHGlobalAnsi(entry.Value);
                    i++;
                }

                int intPtrSize = Marshal.SizeOf<IntPtr>();
                int size = (intPtrSize * count);

                IntPtr ptrValue = Marshal.AllocHGlobal(size);

                for (int j = 0; j < count; j++)
                {
                    Marshal.StructureToPtr(ptrValueArray[j], ptrValue + (intPtrSize * j), false);
                }
                return ptrValue;
            }
            return IntPtr.Zero;
        }

        internal static void PtrToStringList(IntPtr ptr, int count, out IList<string> outResultList)
        {
            var resultList = new List<string>();
            if (count > 0)
            {
                int intPtrSize = Marshal.SizeOf<IntPtr>();
                for (var i = 0; i < count; i++)
                {
                    IntPtr strPtr = Marshal.PtrToStructure<IntPtr>(ptr + (intPtrSize * i));
                    string strValue = Marshal.PtrToStringAnsi(strPtr);
                    resultList.Add(strValue);
                }
            }

            outResultList = resultList;
        }

        internal static void PtrToInt64List(IntPtr ptr, int count, out IList<Int64> outResultList)
        {
            var resultList = new List<Int64>();
            if (count > 0)
            {
                int intPtrSize = Marshal.SizeOf<IntPtr>();
                for (var i = 0; i < count; i++)
                {
                    IntPtr strPtr = Marshal.PtrToStructure<IntPtr>(ptr + (intPtrSize * i));
                    string strValue = Marshal.PtrToStringAnsi(strPtr);
                    Int64 value64 = Convert.ToInt64(strValue);
                    resultList.Add(value64);
                }
            }

            outResultList = resultList;
        }

        internal static int StringListToPtr(IList<string> strList, out IntPtr resultPtr)
        {
            int count = strList.Count;
            if (count == 0)
            {
                resultPtr = IntPtr.Zero;
                return 0;
            }

            List<String> list = new List<string>(strList);
            string[] strArray = list.ToArray();
            IntPtr[] ptrArray = new IntPtr[count];
            for (var i = 0; i < count; i++)
            {
                ptrArray[i] = Marshal.StringToHGlobalAnsi(strArray[i]);
            }

            int intPtrSize = Marshal.SizeOf<IntPtr>();
            int size = intPtrSize * ptrArray.Length;

            IntPtr ptr = Marshal.AllocHGlobal(size);
            for (var i = 0; i < ptrArray.Length; i++)
            {
                Marshal.StructureToPtr(ptrArray[i], ptr + (intPtrSize * i), false);
            }

            resultPtr = ptr;
            return count;
        }

        internal static void PtrToStructureList<T>(IntPtr ptr, int count, out IList<T> outResultList)
        {
            var resultList = new List<T>();
            if (count > 0)
            {
                int PtrSizeT = Marshal.SizeOf<T>();
                for (var i = 0; i < count; i++)
                {
                    T valueT = Marshal.PtrToStructure<T>(ptr + (PtrSizeT * i));
                    resultList.Add(valueT);
                }
            }

            outResultList = resultList;
        }

        internal static int StructureListToPtr<T>(IList<T> structureList, out IntPtr resultPtr)
        {
            int count = structureList.Count;
            if (count == 0)
            {
                resultPtr = IntPtr.Zero;
                return 0;
            }

            var list = new List<T>(structureList);
            var structureArray = list.ToArray();

            int PtrSizeT = Marshal.SizeOf<T>();
            int size = PtrSizeT * structureArray.Length;

            IntPtr ptr = Marshal.AllocHGlobal(size);
            for (var i = 0; i < structureArray.Length; i++)
            {
                Marshal.StructureToPtr(structureArray[i], ptr + (PtrSizeT * i), false);
            }

            resultPtr = ptr;
            return count;
        }

        /// <summary>
        /// 从 C 结构对象往 C# 对象赋值，给消息的基类的各字段赋值
        /// </summary>
        /// <param name="rcMessageContent">继承 RCMessageContent 类型的消息对象</param>
        /// <param name="imMsgContentPtr">指向 im_message_content 的指针</param>
        internal static void writeRCMessageContentInfo(RCMessageContent rcMessageContent,
            IntPtr imMsgContentPtr)
        {
            im_message_content imMsgContent = Marshal.PtrToStructure<im_message_content>(imMsgContentPtr);
            writeRCMessageContentInfo(rcMessageContent, ref imMsgContent);
        }

        internal static void writeRCMessageContentInfo(RCMessageContent rcMessageContent,
            ref im_message_content imMsgContent)
        {
            im_mentioned_info imMentionedInfo = Marshal.PtrToStructure<im_mentioned_info>(imMsgContent.mentionedInfo);
            im_user_info imUserInfo = Marshal.PtrToStructure<im_user_info>(imMsgContent.userInfo);

            rcMessageContent.MentionedInfo = toRCMentionedInfo(ref imMentionedInfo);
            rcMessageContent.SendUserInfo = toRCUserInfo(ref imUserInfo);
            rcMessageContent.DestructDuration = imMsgContent.destructDuration;
            rcMessageContent.IsDestruct = imMsgContent.destructDuration != 0;
            rcMessageContent.Extra = imMsgContent.extra;
        }

        internal static void writeRCMediaMessageContentInfo(RCMediaMessageContent rcMediaMessageContent,
            IntPtr imMediaMsgContentPtr)
        {
            im_media_message_content imMediaMsgContent =
                Marshal.PtrToStructure<im_media_message_content>(imMediaMsgContentPtr);
            writeRCMediaMessageContentInfo(rcMediaMessageContent, ref imMediaMsgContent);
        }

        internal static void writeRCMediaMessageContentInfo(RCMediaMessageContent rcMediaMessageContent,
            ref im_media_message_content imMediaMsgContent)
        {
            im_mentioned_info imMentionedInfo =
                Marshal.PtrToStructure<im_mentioned_info>(imMediaMsgContent.mentionedInfo);
            im_user_info imUserInfo = Marshal.PtrToStructure<im_user_info>(imMediaMsgContent.userInfo);

            rcMediaMessageContent.MentionedInfo = toRCMentionedInfo(ref imMentionedInfo);
            rcMediaMessageContent.SendUserInfo = toRCUserInfo(ref imUserInfo);
            rcMediaMessageContent.DestructDuration = imMediaMsgContent.destructDuration;
            rcMediaMessageContent.IsDestruct = imMediaMsgContent.destructDuration != 0;
            rcMediaMessageContent.Extra = imMediaMsgContent.extra;
            rcMediaMessageContent.LocalPath = imMediaMsgContent.localPath;
            rcMediaMessageContent.MediaUrl = imMediaMsgContent.remoteUrl;
            rcMediaMessageContent.Name = imMediaMsgContent.name;
        }

        private static Dictionary<string, string> getImMessageExpansionDic(ref im_message imMsg)
        {
            if (imMsg.canIncludeExpansion && imMsg.expansionCount > 0)
            {
                var result = KeyValuesToDictionary(imMsg.expansionKeys, imMsg.expansionValues, imMsg.expansionCount);
                return result;
            }

            return null;
        }

        internal static Dictionary<string, string> KeyValuesToDictionary(IntPtr ptrKey, IntPtr ptrValue, int count)
        {
            Dictionary<string, string> result = null;
            if (count > 0)
            {
                result = new Dictionary<string, string>();
                int intPtrSize = Marshal.SizeOf<IntPtr>();
                for (var i = 0; i < count; i++)
                {
                    IntPtr keyPtr = Marshal.PtrToStructure<IntPtr>(ptrKey + (intPtrSize * i));
                    string key = Marshal.PtrToStringAnsi(keyPtr);
                    if (key == null)
                    {
                        continue;
                    }

                    IntPtr valuePtr = Marshal.PtrToStructure<IntPtr>(ptrValue + (intPtrSize * i));
                    string value = Marshal.PtrToStringAnsi(valuePtr);
                    result.Add(key, value);
                }
            }

            return result;
        }

        private static Dictionary<string, Int64> KeyValuesToDicInt64(IntPtr ptrKey, IntPtr ptrValue, int length)
        {
            Dictionary<string, Int64> result = null;
            if (length > 0)
            {
                result = new Dictionary<string, Int64>();
                int intPtrSize = Marshal.SizeOf<IntPtr>();
                for (var i = 0; i < length; i++)
                {
                    IntPtr keyPtr = Marshal.PtrToStructure<IntPtr>(ptrKey + (intPtrSize * i));
                    string key = Marshal.PtrToStringAnsi(keyPtr);
                    if (key == null)
                    {
                        continue;
                    }

                    IntPtr valuePtr = Marshal.PtrToStructure<IntPtr>(ptrValue + (intPtrSize * i));
                    string value = Marshal.PtrToStringAnsi(valuePtr);
                    Int64 value64 = Convert.ToInt64(value);
                    result.Add(key, value64);
                }
            }

            return result;
        }


        private static RCUserInfo toRCUserInfo(ref im_user_info imUserInfo)
        {
            var rcUserInfo = new RCUserInfo(imUserInfo.userId, imUserInfo.name, imUserInfo.portraitUri,
                imUserInfo.extra);
            return rcUserInfo;
        }

        private static RCMentionedInfo toRCMentionedInfo(ref im_mentioned_info imMentionedInfo)
        {
            MentionedType type = (MentionedType) imMentionedInfo.type;
            var userIdList = new List<string>();
            IntPtr ptr = imMentionedInfo.userIds;
            if (imMentionedInfo.userIdCount > 0)
            {
                int intPtrSize = Marshal.SizeOf<IntPtr>();
                for (var i = 0; i < imMentionedInfo.userIdCount; i++)
                {
                    IntPtr userIdPtr = Marshal.PtrToStructure<IntPtr>(ptr + (intPtrSize * i));
                    string userId = Marshal.PtrToStringAnsi(userIdPtr);
                    userIdList.Add(userId);
                }
            }

            var rcMentionedInfo = new RCMentionedInfo(type, userIdList, imMentionedInfo.content);
            return rcMentionedInfo;
        }

        private static RCIOSConfig toRCIOSConfig(ref im_ios_config imIOSConfig)
        {
            RCIOSConfig rcIOSConfig = new RCIOSConfig
            {
                ThreadId = imIOSConfig.threadId,
                APNSCollapseId = imIOSConfig.apnsCollapseId
            };
            return rcIOSConfig;
        }

        private static RCIOSConfig toRCIOSConfig(IntPtr imIOSConfigPtr)
        {
            if (imIOSConfigPtr == IntPtr.Zero)
            {
                return null;
            }

            im_ios_config imIOSConfig = Marshal.PtrToStructure<im_ios_config>(imIOSConfigPtr);
            var rcIOSConfig = toRCIOSConfig(ref imIOSConfig);
            return rcIOSConfig;
        }

        private static RCMessagePushConfig toRCMessagePushConfig(ref im_push_config imPushConfig)
        {
            RCMessagePushConfig rcMessagePushConfig = new RCMessagePushConfig
            {
                PushTitle = imPushConfig.pushTitle,
                PushContent = imPushConfig.pushContent,
                PushData = imPushConfig.pushData,
                TemplateId = imPushConfig.templateId,
                ForceShowDetailContent = imPushConfig.forceShowDetailContent,
                DisablePushTitle = imPushConfig.disablePushTitle,
                AndroidConfig = null,
                iOSConfig = toRCIOSConfig(imPushConfig.iOSConfig)
            };
            return rcMessagePushConfig;
        }

        private static RCMessagePushConfig toRCMessagePushConfig(IntPtr imPushConfigPtr)
        {
            if (imPushConfigPtr == IntPtr.Zero)
            {
                return null;
            }

            im_push_config imPushConfig = Marshal.PtrToStructure<im_push_config>(imPushConfigPtr);
            var rcMessagePushConfig = toRCMessagePushConfig(ref imPushConfig);
            return rcMessagePushConfig;
        }

        private static RCReadReceiptInfo toRCReadReceiptInfo(ref im_read_receipt_info imReadReceiptInfo)
        {
            RCReadReceiptInfo rcReadReceiptInfo = new RCReadReceiptInfo()
            {
                IsReceiptRequestMessage = imReadReceiptInfo.isReceiptRequestMessage,
                HasRespond = imReadReceiptInfo.hasRespond,
                RespondUserIdList = KeyValuesToDicInt64(imReadReceiptInfo.userIdKeys, imReadReceiptInfo.userIdValues,
                    imReadReceiptInfo.userIdsLen)
            };

            return rcReadReceiptInfo;
        }

        private static RCReadReceiptInfo toRCReadReceiptInfo(IntPtr imReadReceiptPtr)
        {
            if (imReadReceiptPtr == IntPtr.Zero)
            {
                return null;
            }

            var imReadReceiptInfo = Marshal.PtrToStructure<im_read_receipt_info>(imReadReceiptPtr);
            return toRCReadReceiptInfo(ref imReadReceiptInfo);
        }

        internal static RCMessage toRCMessage(IntPtr imMsgPtr)
        {
            if (imMsgPtr == IntPtr.Zero)
            {
                return null;
            }

            im_message imMsg = Marshal.PtrToStructure<im_message>(imMsgPtr);
            return toRCMessage(ref imMsg);
        }

        internal static RCMessage toRCMessage(ref im_message imMsg)
        {
            RCMessage rcMsg = new RCMessage
            {
                ConversationType = (RCConversationType) imMsg.conversationType,
                TargetId = imMsg.targetId,
                MessageId = imMsg.messageId,
                MessageUId = imMsg.messageUid,
                Direction = (RCMessageDirection) imMsg.direction,
                SenderUserId = imMsg.senderId,
                ReceivedStatus = new RCReceivedStatus(imMsg.receivedStatus),
                SentStatus = (RCSentStatus) imMsg.sentStatus,
                ReceivedTime = imMsg.receivedTime,
                SentTime = imMsg.sentTime,
                ObjectName = imMsg.objectName,
                Content = toConcreteRCMessageContent(imMsg.content, imMsg.objectName),
                Extra = imMsg.extra,
                ReadReceiptInfo = toRCReadReceiptInfo(imMsg.readReceiptInfo),
                CanIncludeExpansion = imMsg.canIncludeExpansion,
                ExpansionDic = getImMessageExpansionDic(ref imMsg),
                MessagePushConfig = toRCMessagePushConfig(imMsg.pushConfig)
            };
            RCMessageConfig msgConfig = new RCMessageConfig
            {
                DisableNotification = imMsg.disableNotification
            };
            rcMsg.MessageConfig = msgConfig;

            return rcMsg;
        }

        internal static RCTextMessage toRCTextMessage(ref im_text_message imTextMsg)
        {
            var rcTextMsg = new RCTextMessage(imTextMsg.text);
            writeRCMessageContentInfo(rcTextMsg, imTextMsg.msgContent);
            return rcTextMsg;
        }

        internal static RCRichContentMessage toRCRichContentMessage(ref im_rich_content_message imRichContentMsg)
        {
            var rcRichContentMsg = new RCRichContentMessage()
            {
                Title = imRichContentMsg.title,
                Url = imRichContentMsg.url,
                Digest = imRichContentMsg.digest,
                ImageUrl = imRichContentMsg.imageURL
            };
            writeRCMessageContentInfo(rcRichContentMsg, imRichContentMsg.msgContent);
            return rcRichContentMsg;
        }

        internal static RCRecallNotificationMessage toRCRecallNotificationMessage(
            ref im_recall_notification_message imRecallNotifyMsg)
        {
            RCRecallNotificationMessage rcRecallNotifyMsg = new RCRecallNotificationMessage()
            {
                OperatorId = imRecallNotifyMsg.operatorId,
                RecallTime = imRecallNotifyMsg.recallTime,
                RecallActionTime = imRecallNotifyMsg.recallActionTime,
                OriginalObjectName = imRecallNotifyMsg.originalObjectName,
                IsAdmin = imRecallNotifyMsg.isAdmin,
                // Delete = imRecallNotifyMsg.
                RecallContent = imRecallNotifyMsg.recallContent
            };
            writeRCMessageContentInfo(rcRecallNotifyMsg, imRecallNotifyMsg.msgContent);
            return rcRecallNotifyMsg;
        }

        internal static RCMessageContent toConcreteRCMessageContent(IntPtr imConcreteMsgPtr, string objectName)
        {
            if (string.IsNullOrEmpty(objectName)) return null;
            if (imConcreteMsgPtr == IntPtr.Zero) return null;

            RCMessageContent rcMessageContent;
            Debug.Log($"objet name:{objectName}, ptr:{imConcreteMsgPtr}");
            if (objectName.Equals(TextMsgObjectName))
            {
                im_text_message imTextMsg
                    = Marshal.PtrToStructure<im_text_message>(imConcreteMsgPtr);
                rcMessageContent = toRCTextMessage(ref imTextMsg);
            }
            else if (objectName.Equals(RichTextMsgObjectName))
            {
                im_rich_content_message imRichContentMsg =
                    Marshal.PtrToStructure<im_rich_content_message>(imConcreteMsgPtr);
                rcMessageContent = toRCRichContentMessage(ref imRichContentMsg);
            }
            else if (objectName.Equals(RecallNotificationMessageObjectName))
            {
                im_recall_notification_message imRecallNotificationMessage =
                    Marshal.PtrToStructure<im_recall_notification_message>(imConcreteMsgPtr);
                rcMessageContent =
                    toRCRecallNotificationMessage(ref imRecallNotificationMessage);
            }
            else if (objectName.Equals(SightMessageObjectName))
            {
                im_sight_message imSightMessage = Marshal.PtrToStructure<im_sight_message>(imConcreteMsgPtr);
                rcMessageContent = imSightMessage.toRCSightMessage();
            }
            else if (objectName == HQVoiceMessageObjectName)
            {
                im_hq_voice_message imHqVoiceMsg = Marshal.PtrToStructure<im_hq_voice_message>(imConcreteMsgPtr);
                rcMessageContent = imHqVoiceMsg.toRCHQVoiceMessage();
            }
            else if (objectName == GroupNotificationMessageObjectName)
            {
                im_group_notification_message imGroupNotificationMessage =
                    Marshal.PtrToStructure<im_group_notification_message>(imConcreteMsgPtr);
                rcMessageContent = imGroupNotificationMessage.toRCGroupNotificationMessage();
            }
            else if (objectName == ImageMessageObjectName)
            {
                im_image_message imImageMessage = Marshal.PtrToStructure<im_image_message>(imConcreteMsgPtr);
                rcMessageContent = imImageMessage.toRcImageMessage();
            }
            else if (objectName == GIFMessageObjectName)
            {
                im_gif_message imGifMessage = Marshal.PtrToStructure<im_gif_message>(imConcreteMsgPtr);
                rcMessageContent = imGifMessage.toRCGifMessage();
            }
            else if (objectName == VoiceMessageObjectName)
            {
                im_voice_message imVoiceMessage = Marshal.PtrToStructure<im_voice_message>(imConcreteMsgPtr);
                rcMessageContent = imVoiceMessage.toRCVoiceMessage();
            }
            else if (objectName == ReferenceMessageObjectName)
            {
                im_reference_message imReferenceMessage =
                    Marshal.PtrToStructure<im_reference_message>(imConcreteMsgPtr);
                rcMessageContent = imReferenceMessage.toRCReferenceMessage();
            }
            // else if (objectName == CombineMessageObjectName)
            // {
            //     im_combine_message imCombineMessage = Marshal.PtrToStructure<im_combine_message>(imConcreteMsgPtr);
            //     rcMessageContent = imCombineMessage.toRCCombineMessage();
            // }
            else if (objectName == FileMessageObjectName)
            {
                im_file_message imFileMessage = Marshal.PtrToStructure<im_file_message>(imConcreteMsgPtr);
                rcMessageContent = imFileMessage.toRCFileMessage();
            }
            else if (objectName == ChatroomKVNotificationMessageObjectName)
            {
                im_chat_room_kv_notification_message imChatRoomKvNotificationMessage =
                    Marshal.PtrToStructure<im_chat_room_kv_notification_message>(imConcreteMsgPtr);
                rcMessageContent = imChatRoomKvNotificationMessage.toRCChatroomKVNotificationMessage();
            }
            else if (objectName == ChatRoomMemberActionMessageObjectName)
            {
                im_chat_room_member_action_message imChatRoomMemberActionMessage =
                    Marshal.PtrToStructure<im_chat_room_member_action_message>(imConcreteMsgPtr);
                rcMessageContent = imChatRoomMemberActionMessage.toRCChatroomMemberActionMessage();
            }
            else
            {
                im_custom_message im_custom_message = Marshal.PtrToStructure<im_custom_message>(imConcreteMsgPtr);
                if (im_custom_message.customMessageType > -1 && im_custom_message.customMessageType <= 3 && im_custom_message.customFieldsCount > 0 && im_custom_message.customFieldsKeys != IntPtr.Zero && im_custom_message.customFieldsValues != IntPtr.Zero)
                {
                    rcMessageContent = im_custom_message.toRCCustomMessage();
                }
                else
                {
                    Debug.Log($"not implement {objectName} message type");
                    rcMessageContent = new RCUnknownMessage();
                }
            }

            return rcMessageContent;
        }

        private static im_user_info convertRCUserInfo(RCUserInfo rcUserInfo)
        {
            im_user_info imUserInfo = default;
            if (rcUserInfo != null)
            {
                imUserInfo.userId = rcUserInfo.UserId;
                imUserInfo.name = rcUserInfo.Name;
                imUserInfo.portraitUri = rcUserInfo.PortraitUri;
                imUserInfo.alias = "";
                imUserInfo.extra = rcUserInfo.Extra;
            }

            return imUserInfo;
        }

        private static im_mentioned_info convertRCMentionedInfo(RCMentionedInfo rcMentionedInfo)
        {
            im_mentioned_info imMentionedInfo = default;
            if (rcMentionedInfo != null)
            {
                imMentionedInfo.type = (int) rcMentionedInfo.Type;
                imMentionedInfo.content = rcMentionedInfo.MentionedContent;
                imMentionedInfo.isMentionedMe = false; // todo: RCMentionedInfo add it
                if (rcMentionedInfo.UserIdList.Count > 0)
                {
                    List<String> list = new List<string>(rcMentionedInfo.UserIdList);
                    string[] userIds = list.ToArray();
                    var count = userIds.Length;
                    IntPtr[] ptrArray = new IntPtr[count];
                    for (var i = 0; i < count; i++)
                    {
                        ptrArray[i] = Marshal.StringToHGlobalAnsi(userIds[i]);
                    }

                    int intPtrSize = Marshal.SizeOf<IntPtr>();
                    int size = intPtrSize * ptrArray.Length;

                    IntPtr ptr = Marshal.AllocHGlobal(size);
                    for (var i = 0; i < ptrArray.Length; i++)
                    {
                        Marshal.StructureToPtr(ptrArray[i], ptr + (intPtrSize * i), false);
                    }

                    imMentionedInfo.userIds = ptr;
                    imMentionedInfo.userIdCount = list.Count;
                }
                else
                {
                    imMentionedInfo.userIds = IntPtr.Zero;
                    imMentionedInfo.userIdCount = 0;
                }
            }

            return imMentionedInfo;
        }

        private static im_ios_config convertRCIOSConfig(ref RCIOSConfig rcIOSConfig)
        {
            im_ios_config iosConfig = default;
            iosConfig.threadId = rcIOSConfig.ThreadId;
            iosConfig.apnsCollapseId = rcIOSConfig.APNSCollapseId;
            // todo:
            // iosConfig.category =
            // iosConfig.richMediaUri =
            return iosConfig;
        }

        private static IntPtr convertRCIOSConfigToImPtr(ref RCIOSConfig rcIOSConfig)
        {
            im_ios_config iosConfig = convertRCIOSConfig(ref rcIOSConfig);
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(iosConfig));
            Marshal.StructureToPtr(iosConfig, ptr, false);
            return ptr;
        }

        private static im_push_config convertRCMessagePushConfig(ref RCMessagePushConfig rcPushConfig)
        {
            im_push_config imPushConfig = default;
            imPushConfig.pushContent = rcPushConfig.PushContent;
            imPushConfig.pushTitle = rcPushConfig.PushTitle;
            imPushConfig.pushData = rcPushConfig.PushData;
            imPushConfig.disablePushTitle = rcPushConfig.DisablePushTitle;
            imPushConfig.forceShowDetailContent = rcPushConfig.ForceShowDetailContent;
            imPushConfig.templateId = rcPushConfig.TemplateId;
            if (rcPushConfig.iOSConfig != null)
            {
                RCIOSConfig rcIOSConfig = rcPushConfig.iOSConfig;
                imPushConfig.iOSConfig = convertRCIOSConfigToImPtr(ref rcIOSConfig);
            }

            return imPushConfig;
        }

        private static IntPtr convertRCMessagePushConfigToImPtr(ref RCMessagePushConfig rcPushConfig)
        {
            im_push_config imPushConfig = convertRCMessagePushConfig(ref rcPushConfig);
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(imPushConfig));
            Marshal.StructureToPtr(imPushConfig, ptr, false);
            return ptr;
        }

        internal static im_message convertRCMessage(RCMessage rcMsg)
        {
            im_message imMsg = default;
            if (rcMsg == null)
            {
                return imMsg;
            }

            imMsg.conversationType = (int) rcMsg.ConversationType;
            imMsg.targetId = rcMsg.TargetId;
            imMsg.messageId = rcMsg.MessageId;
            imMsg.messageUid = rcMsg.MessageUId;
            imMsg.direction = (int) rcMsg.Direction;
            imMsg.senderId = rcMsg.SenderUserId;
            if (rcMsg.ReceivedStatus != null)
            {
                imMsg.receivedStatus = rcMsg.ReceivedStatus.Flag;
            }

            imMsg.sentStatus = (int) rcMsg.SentStatus;
            imMsg.receivedTime = rcMsg.ReceivedTime;
            imMsg.sentTime = rcMsg.SentTime;
            imMsg.objectName = rcMsg.Content.ObjectName;
            imMsg.content = convertConcreteRCMessageContent(rcMsg.Content);
            imMsg.extra = rcMsg.Extra;
            imMsg.canIncludeExpansion = rcMsg.CanIncludeExpansion;
            if (rcMsg.ExpansionDic != null)
            {
                writeExpansionDicToImMsg(rcMsg.ExpansionDic, ref imMsg);
            }

            if (rcMsg.MessageConfig != null)
            {
                imMsg.disableNotification = rcMsg.MessageConfig.DisableNotification;
            }

            if (rcMsg.MessagePushConfig != null)
            {
                RCMessagePushConfig rcMessagePushConfig = rcMsg.MessagePushConfig;
                imMsg.pushConfig = convertRCMessagePushConfigToImPtr(ref rcMessagePushConfig);
            }

            return imMsg;
        }

        internal static IntPtr convertRCMessageContent(RCMessageContent rcContent)
        {
            im_user_info imUserInfo = convertRCUserInfo(rcContent.SendUserInfo);
            IntPtr imUserInfoPtr = Marshal.AllocHGlobal(Marshal.SizeOf(imUserInfo));
            Marshal.StructureToPtr(imUserInfo, imUserInfoPtr, false);
            Debug.Log($"imUserInfo: {imUserInfo}");

            im_mentioned_info imMentionedInfo = convertRCMentionedInfo(rcContent.MentionedInfo);
            IntPtr imMentionedPtr = Marshal.AllocHGlobal(Marshal.SizeOf(imMentionedInfo));
            Marshal.StructureToPtr(imMentionedInfo, imMentionedPtr, false);
            Debug.Log($"imMentionedInfo: {imMentionedInfo}");

            im_message_content imMsgContent = default;
            imMsgContent.userInfo = imUserInfoPtr;
            imMsgContent.mentionedInfo = imMentionedPtr;
            imMsgContent.objectName = rcContent.ObjectName;
            imMsgContent.destructDuration = (int) rcContent.DestructDuration;
            imMsgContent.extra = rcContent.Extra;
            IntPtr imMsgContentPtr = Marshal.AllocHGlobal(Marshal.SizeOf(imMsgContent));
            Marshal.StructureToPtr(imMsgContent, imMsgContentPtr, false);
            return imMsgContentPtr;
        }

        internal static IntPtr convertRCMediaMessageContent(RCMediaMessageContent rcMediaContent)
        {
            im_user_info imUserInfo = convertRCUserInfo(rcMediaContent.SendUserInfo);
            IntPtr imUserInfoPtr = Marshal.AllocHGlobal(Marshal.SizeOf(imUserInfo));
            Marshal.StructureToPtr(imUserInfo, imUserInfoPtr, false);
            Debug.Log($"imUserInfo: {imUserInfo}");

            im_mentioned_info imMentionedInfo = convertRCMentionedInfo(rcMediaContent.MentionedInfo);
            IntPtr imMentionedPtr = Marshal.AllocHGlobal(Marshal.SizeOf(imMentionedInfo));
            Marshal.StructureToPtr(imMentionedInfo, imMentionedPtr, false);
            Debug.Log($"imMentionedInfo: {imMentionedInfo}");

            im_media_message_content imMediaMsgContent = default;
            imMediaMsgContent.userInfo = imUserInfoPtr;
            imMediaMsgContent.mentionedInfo = imMentionedPtr;
            imMediaMsgContent.objectName = rcMediaContent.ObjectName;
            imMediaMsgContent.destructDuration = (int) rcMediaContent.DestructDuration;
            imMediaMsgContent.extra = rcMediaContent.Extra;
            imMediaMsgContent.localPath = rcMediaContent.LocalPath;
            imMediaMsgContent.remoteUrl = rcMediaContent.MediaUrl;
            imMediaMsgContent.name = rcMediaContent.Name;

            IntPtr imMsgContentPtr = Marshal.AllocHGlobal(Marshal.SizeOf(imMediaMsgContent));
            Marshal.StructureToPtr(imMediaMsgContent, imMsgContentPtr, false);
            return imMsgContentPtr;
        }

        internal static IntPtr convertConcreteRCMessageContent(RCMessageContent rcContent)
        {
            IntPtr imMsgContentPtr;
            // custome message
            if (rcContent is RCCustomMessage rcCustomMessage)
            {
                var im_custom_message = new im_custom_message(rcCustomMessage);
                imMsgContentPtr = Marshal.AllocHGlobal(Marshal.SizeOf<im_custom_message>());
                Marshal.StructureToPtr(im_custom_message, imMsgContentPtr, false);
            }
            else
            {
                // build in message
                if (rcContent.ObjectName == TextMsgObjectName)
                {
                    RCTextMessage rcTextMsg = rcContent as RCTextMessage;
                    im_text_message imTextMsg;
                    if (rcTextMsg == null)
                    {
                        throw new TypeAccessException();
                    }

                    imTextMsg.text = rcTextMsg.Content;
                    imTextMsg.msgContent = convertRCMessageContent(rcContent);

                    imMsgContentPtr = Marshal.AllocHGlobal(Marshal.SizeOf(imTextMsg));
                    Marshal.StructureToPtr(imTextMsg, imMsgContentPtr, false);
                }
                else if (rcContent.ObjectName == RichTextMsgObjectName)
                {
                    RCRichContentMessage rcRichContentMessage = rcContent as RCRichContentMessage;
                    if (rcRichContentMessage == null)
                    {
                        throw new TypeAccessException();
                    }

                    var imRichContentMsg = new im_rich_content_message(rcRichContentMessage);
                    imMsgContentPtr = Marshal.AllocHGlobal(Marshal.SizeOf<im_rich_content_message>());
                    Marshal.StructureToPtr(imRichContentMsg, imMsgContentPtr, false);
                }
                else if (rcContent.ObjectName == ImageMessageObjectName)
                {
                    RCImageMessage rcImageMessage = rcContent as RCImageMessage;
                    if (rcImageMessage == null)
                    {
                        throw new TypeAccessException();
                    }

                    var imImageMsg = new im_image_message(rcImageMessage);
                    imMsgContentPtr = Marshal.AllocHGlobal(Marshal.SizeOf<im_image_message>());
                    Marshal.StructureToPtr(imImageMsg, imMsgContentPtr, false);
                }
                else if (rcContent.ObjectName == GIFMessageObjectName)
                {
                    RCGifMessage rcGifMessage = rcContent as RCGifMessage;
                    if (rcGifMessage == null)
                    {
                        throw new TypeAccessException();
                    }

                    var imGifMsg = new im_gif_message(rcGifMessage);
                    imMsgContentPtr = Marshal.AllocHGlobal(Marshal.SizeOf<im_gif_message>());
                    Marshal.StructureToPtr(imGifMsg, imMsgContentPtr, false);
                }
                else if (rcContent.ObjectName == VoiceMessageObjectName)
                {
                    RCVoiceMessage rcVoiceMessage = rcContent as RCVoiceMessage;
                    if (rcVoiceMessage == null)
                    {
                        throw new TypeAccessException();
                    }

                    var imVoiceMsg = new im_voice_message(rcVoiceMessage);
                    imMsgContentPtr = Marshal.AllocHGlobal(Marshal.SizeOf<im_voice_message>());
                    Marshal.StructureToPtr(imVoiceMsg, imMsgContentPtr, false);
                }
                else if (rcContent.ObjectName == HQVoiceMessageObjectName)
                {
                    RCHQVoiceMessage rchqVoiceMessage = rcContent as RCHQVoiceMessage;
                    if (rchqVoiceMessage == null)
                    {
                        throw new TypeAccessException();
                    }

                    var imHqVoiceMsg = new im_hq_voice_message(rchqVoiceMessage);
                    imMsgContentPtr = Marshal.AllocHGlobal(Marshal.SizeOf<im_hq_voice_message>());
                    Marshal.StructureToPtr(imHqVoiceMsg, imMsgContentPtr, false);
                }
                else if (rcContent.ObjectName == ReferenceMessageObjectName)
                {
                    RCReferenceMessage rcReferenceMessage = rcContent as RCReferenceMessage;
                    if (rcReferenceMessage == null)
                    {
                        throw new TypeAccessException();
                    }

                    var imReferenceMsg = new im_reference_message(rcReferenceMessage);
                    imMsgContentPtr = Marshal.AllocHGlobal(Marshal.SizeOf<im_reference_message>());
                    Marshal.StructureToPtr(imReferenceMsg, imMsgContentPtr, false);
                }
                // else if (rcContent.ObjectName == CombineMessageObjectName)
                // {
                //     RCCombineMessage rcCombineMessage = rcContent as RCCombineMessage;
                //     if (rcCombineMessage == null)
                //     {
                //         throw new TypeAccessException();
                //     }
                //
                //     var imCombineMsg = new im_combine_message(rcCombineMessage);
                //     imMsgContentPtr = Marshal.AllocHGlobal(Marshal.SizeOf<im_combine_message>());
                //     Marshal.StructureToPtr(imCombineMsg, imMsgContentPtr, false);
                // }
                else if (rcContent.ObjectName == SightMessageObjectName)
                {
                    RCSightMessage rcSightMessage = rcContent as RCSightMessage;
                    if (rcSightMessage == null)
                    {
                        throw new TypeAccessException();
                    }

                    var imSightMsg = new im_sight_message(rcSightMessage);
                    imMsgContentPtr = Marshal.AllocHGlobal(Marshal.SizeOf<im_sight_message>());
                    Marshal.StructureToPtr(imSightMsg, imMsgContentPtr, false);
                }
                else if (rcContent.ObjectName == FileMessageObjectName)
                {
                    RCFileMessage rcFileMessage = rcContent as RCFileMessage;
                    if (rcFileMessage == null)
                    {
                        throw new TypeAccessException();
                    }

                    var imFileMsg = new im_file_message(rcFileMessage);
                    imMsgContentPtr = Marshal.AllocHGlobal(Marshal.SizeOf<im_file_message>());
                    Marshal.StructureToPtr(imFileMsg, imMsgContentPtr, false);
                }
                else if (rcContent.ObjectName == RecallNotificationMessageObjectName)
                {
                    RCRecallNotificationMessage rcRecallNotificationMessage = rcContent as RCRecallNotificationMessage;
                    if (rcRecallNotificationMessage == null)
                    {
                        throw new TypeAccessException();
                    }

                    var imRecallNtfMsg = new im_recall_notification_message(rcRecallNotificationMessage);
                    imMsgContentPtr = Marshal.AllocHGlobal(Marshal.SizeOf<im_recall_notification_message>());
                    Marshal.StructureToPtr(imRecallNtfMsg, imMsgContentPtr, false);
                }
                else if (rcContent.ObjectName == GroupNotificationMessageObjectName)
                {
                    RCGroupNotificationMessage rcGroupNotificationMessage = rcContent as RCGroupNotificationMessage;
                    if (rcGroupNotificationMessage == null)
                    {
                        throw new TypeAccessException();
                    }

                    var imGrpNtfMsg = new im_group_notification_message(rcGroupNotificationMessage);
                    imMsgContentPtr = Marshal.AllocHGlobal(Marshal.SizeOf<im_group_notification_message>());
                    Marshal.StructureToPtr(imGrpNtfMsg, imMsgContentPtr, false);
                }
                else if (rcContent.ObjectName == ChatroomKVNotificationMessageObjectName)
                {
                    RCChatroomKVNotificationMessage rcChatroomKvNotificationMessage =
                        rcContent as RCChatroomKVNotificationMessage;
                    if (rcChatroomKvNotificationMessage == null)
                    {
                        throw new TypeAccessException();
                    }

                    var imChatRoomKvNtfMsg = new im_chat_room_kv_notification_message(rcChatroomKvNotificationMessage);
                    imMsgContentPtr = Marshal.AllocHGlobal(Marshal.SizeOf<im_chat_room_kv_notification_message>());
                    Marshal.StructureToPtr(imChatRoomKvNtfMsg, imMsgContentPtr, false);
                }
                else if (rcContent.ObjectName == ChatRoomMemberActionMessageObjectName)
                {
                    RCChatroomMemberActionMessage rcChatroomMemberActionMessage =
                        rcContent as RCChatroomMemberActionMessage;
                    if (rcChatroomMemberActionMessage == null)
                    {
                        throw new TypeAccessException();
                    }

                    var imChatRoomMemberActionMsg = new im_chat_room_member_action_message(rcChatroomMemberActionMessage);
                    imMsgContentPtr = Marshal.AllocHGlobal(Marshal.SizeOf<im_chat_room_member_action_message>());
                    Marshal.StructureToPtr(imChatRoomMemberActionMsg, imMsgContentPtr, false);
                }
                // else if (rcContent.ObjectName == LocationMessageObjectName)
                // {
                //     RCLocationMessage rcLocMsg = rcContent as RCLocationMessage;
                //     if (rcLocMsg == null)
                //     {
                //         throw new TypeAccessException();
                //     }

                //     var imLocMsg = new im_location_message(rcLocMsg);
                //     imMsgContentPtr = Marshal.AllocHGlobal(Marshal.SizeOf<im_location_message>());
                //     Marshal.StructureToPtr(imLocMsg, imMsgContentPtr, false);
                // }
                else
                {
                    Debug.Log($"unknown message: objectName:{rcContent.ObjectName}");
                    imMsgContentPtr = IntPtr.Zero;
                }
            }

            return imMsgContentPtr;
        }

        internal static im_message createImMessage(RCConversationType type, string targetId, RCMessageContent content,
            bool disableNotification = false)
        {
            im_message imMsg = default;
            imMsg.conversationType = (int) type;
            imMsg.targetId = targetId;
            imMsg.direction = (int) RCMessageDirection.Send;
            imMsg.objectName = content.ObjectName;
            imMsg.content = convertConcreteRCMessageContent(content);
            imMsg.disableNotification = disableNotification;

            return imMsg;
        }

        private static void FreeImMentionedInfo(IntPtr mentionedInfoPtr)
        {
            im_mentioned_info imMentionedInfo = Marshal.PtrToStructure<im_mentioned_info>(mentionedInfoPtr);
            if (imMentionedInfo.userIdCount > 0 && imMentionedInfo.userIds != IntPtr.Zero)
            {
                var intPtrSize = Marshal.SizeOf<IntPtr>();
                for (var i = 0; i < imMentionedInfo.userIdCount; i++)
                {
                    var ptrUserId = Marshal.PtrToStructure<IntPtr>(imMentionedInfo.userIds + (intPtrSize * i));
                    Marshal.FreeHGlobal(ptrUserId);
                }

                Marshal.FreeHGlobal(imMentionedInfo.userIds);
            }
        }

        private static void FreeImConcreteMessage(string objectName, IntPtr msgPtr)
        {
            IntPtr mentionedInfoPtr;
            IntPtr contentPtr;

            if (objectName == TextMsgObjectName)
            {
                var imTextMsg = Marshal.PtrToStructure<im_text_message>(msgPtr);
                contentPtr = imTextMsg.msgContent;
                im_message_content imContent = Marshal.PtrToStructure<im_message_content>(contentPtr);
                mentionedInfoPtr = imContent.mentionedInfo;
                FreeImMentionedInfo(mentionedInfoPtr);
                Debug.Log($"free <im_text_message> text:{imTextMsg.text}");
            }
            else if (objectName == RichTextMsgObjectName)
            {
                var imRichMsg = Marshal.PtrToStructure<im_rich_content_message>(msgPtr);
                contentPtr = imRichMsg.msgContent;
                im_message_content imContent = Marshal.PtrToStructure<im_message_content>(contentPtr);
                mentionedInfoPtr = imContent.mentionedInfo;
                FreeImMentionedInfo(mentionedInfoPtr);
            }
            else if (objectName == ImageMessageObjectName)
            {
                var imImageMsg = Marshal.PtrToStructure<im_image_message>(msgPtr);
                contentPtr = imImageMsg.msgContent;
                im_media_message_content imContent = Marshal.PtrToStructure<im_media_message_content>(contentPtr);
                mentionedInfoPtr = imContent.mentionedInfo;
                FreeImMentionedInfo(mentionedInfoPtr);
            }
            else if (objectName == GIFMessageObjectName)
            {
                var imGifMsg = Marshal.PtrToStructure<im_gif_message>(msgPtr);
                contentPtr = imGifMsg.msgContent;
                im_media_message_content imContent = Marshal.PtrToStructure<im_media_message_content>(contentPtr);
                mentionedInfoPtr = imContent.mentionedInfo;
                FreeImMentionedInfo(mentionedInfoPtr);
            }
            else if (objectName == VoiceMessageObjectName)
            {
                var imVoiceMsg = Marshal.PtrToStructure<im_voice_message>(msgPtr);
                contentPtr = imVoiceMsg.msgContent;
                var imContent = Marshal.PtrToStructure<im_message_content>(contentPtr);
                mentionedInfoPtr = imContent.mentionedInfo;
                FreeImMentionedInfo(mentionedInfoPtr);
            }
            else if (objectName == HQVoiceMessageObjectName)
            {
                var imHqVoiceMsg = Marshal.PtrToStructure<im_hq_voice_message>(msgPtr);
                contentPtr = imHqVoiceMsg.msgContent;
                var imContent = Marshal.PtrToStructure<im_media_message_content>(contentPtr);
                mentionedInfoPtr = imContent.mentionedInfo;
                FreeImMentionedInfo(mentionedInfoPtr);
            }
            else if (objectName == ReferenceMessageObjectName)
            {
                var imRefMsg = Marshal.PtrToStructure<im_reference_message>(msgPtr);
                contentPtr = imRefMsg.msgContent;
                var imContent = Marshal.PtrToStructure<im_media_message_content>(contentPtr);
                mentionedInfoPtr = imContent.mentionedInfo;
                FreeImMentionedInfo(mentionedInfoPtr);

                FreeImConcreteMessage(imRefMsg.referMsgObjectName, imRefMsg.referMsg);
            }
            else if (objectName == SightMessageObjectName)
            {
                var imSighMsg = Marshal.PtrToStructure<im_sight_message>(msgPtr);
                contentPtr = imSighMsg.msgContent;
                var imContent = Marshal.PtrToStructure<im_media_message_content>(contentPtr);
                mentionedInfoPtr = imContent.mentionedInfo;
                FreeImMentionedInfo(mentionedInfoPtr);
            }
            else if (objectName == FileMessageObjectName)
            {
                var imFileMsg = Marshal.PtrToStructure<im_file_message>(msgPtr);
                contentPtr = imFileMsg.msgContent;
                var imContent = Marshal.PtrToStructure<im_media_message_content>(contentPtr);
                mentionedInfoPtr = imContent.mentionedInfo;
                FreeImMentionedInfo(mentionedInfoPtr);
            }
            else if (objectName == RecallNotificationMessageObjectName)
            {
                var imRecallNtfMsg = Marshal.PtrToStructure<im_recall_notification_message>(msgPtr);
                contentPtr = imRecallNtfMsg.msgContent;
                var imContent = Marshal.PtrToStructure<im_message_content>(contentPtr);
                mentionedInfoPtr = imContent.mentionedInfo;
                FreeImMentionedInfo(mentionedInfoPtr);
            }
            else if (objectName == GroupNotificationMessageObjectName)
            {
                var imGrpNtfMsg = Marshal.PtrToStructure<im_group_notification_message>(msgPtr);
                contentPtr = imGrpNtfMsg.msgContent;
                var imContent = Marshal.PtrToStructure<im_message_content>(contentPtr);
                mentionedInfoPtr = imContent.mentionedInfo;
                FreeImMentionedInfo(mentionedInfoPtr);
            }
            else if (objectName == ChatroomKVNotificationMessageObjectName)
            {
                var imChatKvNtfMsg = Marshal.PtrToStructure<im_chat_room_kv_notification_message>(msgPtr);
                contentPtr = imChatKvNtfMsg.msgContent;
                var imContent = Marshal.PtrToStructure<im_message_content>(contentPtr);
                mentionedInfoPtr = imContent.mentionedInfo;
                FreeImMentionedInfo(mentionedInfoPtr);
            }
            else if (objectName == ChatRoomMemberActionMessageObjectName)
            {
                var imChatMemberActionMsg = Marshal.PtrToStructure<im_chat_room_member_action_message>(msgPtr);
                contentPtr = imChatMemberActionMsg.msgContent;
                var imContent = Marshal.PtrToStructure<im_message_content>(contentPtr);
                mentionedInfoPtr = imContent.mentionedInfo;
                FreeImMentionedInfo(mentionedInfoPtr);
            }
            // else if (objectName == LocationMessageObjectName)
            // {
            //     var imLocationMsg = Marshal.PtrToStructure<im_location_message>(msgPtr);
            //     contentPtr = imLocationMsg.msgContent;
            //     var imContent = Marshal.PtrToStructure<im_message_content>(contentPtr);
            //     mentionedInfoPtr = imContent.mentionedInfo;
            //     FreeImMentionedInfo(mentionedInfoPtr);
            // }
            else if (objectName == WrapperCommandObjectName || objectName == WrapperStorageObjectName || objectName == WrapperNormalObjectName || objectName == WrapperStatusObjectName)
            {
                var imCustomMsg = Marshal.PtrToStructure<im_custom_message>(msgPtr);

                contentPtr = imCustomMsg.msgContent;
                var imContent = Marshal.PtrToStructure<im_message_content>(contentPtr);
                mentionedInfoPtr = imContent.mentionedInfo;
                FreeImMentionedInfo(mentionedInfoPtr);

                if (imCustomMsg.customFieldsCount > 0 && imCustomMsg.customFieldsKeys != IntPtr.Zero && imCustomMsg.customFieldsValues != IntPtr.Zero)
                {
                    var intPtrSize = Marshal.SizeOf<IntPtr>();
                    for (var i = 0; i < imCustomMsg.customFieldsCount; i++)
                    {
                        var ptrKey = Marshal.PtrToStructure<IntPtr>(imCustomMsg.customFieldsKeys + (intPtrSize * i));
                        var ptrValue = Marshal.PtrToStructure<IntPtr>(imCustomMsg.customFieldsValues + (intPtrSize * i));
                        Marshal.FreeHGlobal(ptrKey);
                        Marshal.FreeHGlobal(ptrValue);
                    }

                    Marshal.FreeHGlobal(imCustomMsg.customFieldsKeys);
                    Marshal.FreeHGlobal(imCustomMsg.customFieldsValues);
                }
            }
            else
            {
                Debug.Log($"not implement object name {objectName}");
            }
        }

        internal static void FreeImMessageContent(ref im_message imMsg)
        {
            Debug.Log($"FreeImMessageContent {imMsg}");
            IntPtr msgPtr = imMsg.content;
            IntPtr contentPtr = IntPtr.Zero;
            IntPtr userInfoPtr = IntPtr.Zero;
            IntPtr mentionedInfoPtr = IntPtr.Zero;

            FreeImConcreteMessage(imMsg.objectName, msgPtr);

            if (imMsg.expansionCount > 0 && imMsg.expansionKeys != IntPtr.Zero && imMsg.expansionValues != IntPtr.Zero)
            {
                var intPtrSize = Marshal.SizeOf<IntPtr>();
                for (var i = 0; i < imMsg.expansionCount; i++)
                {
                    var ptrKey = Marshal.PtrToStructure<IntPtr>(imMsg.expansionKeys + (intPtrSize * i));
                    var ptrValue = Marshal.PtrToStructure<IntPtr>(imMsg.expansionValues + (intPtrSize * i));
                    Marshal.FreeHGlobal(ptrKey);
                    Marshal.FreeHGlobal(ptrValue);
                }

                Marshal.FreeHGlobal(imMsg.expansionKeys);
                Marshal.FreeHGlobal(imMsg.expansionValues);
            }

            if (imMsg.pushConfig != IntPtr.Zero)
            {
                im_push_config imPushConfig = Marshal.PtrToStructure<im_push_config>(imMsg.pushConfig);
                Marshal.FreeHGlobal(imPushConfig.iOSConfig);
                Marshal.FreeHGlobal(imMsg.pushConfig);
            }

            Marshal.FreeHGlobal(userInfoPtr);
            Marshal.FreeHGlobal(mentionedInfoPtr);
            Marshal.FreeHGlobal(contentPtr);
            Marshal.FreeHGlobal(msgPtr);
        }

        #endregion

        internal static RCTypingStatus toRCTypingStatus(ref im_typing_status imTypingStatus)
        {
            var rcTypingStatus = new RCTypingStatus()
            {
                UserId = imTypingStatus.userId,
                TypingContentType = imTypingStatus.typingContentType
            };
            return rcTypingStatus;
        }

        #region 敏感词

        internal static BlockedMessageInfo toBlockedMessageInfo(ref im_blocked_message_info imBlockedMessageInfo)
        {
            RCConversationType type = (RCConversationType) imBlockedMessageInfo.type;
            RCBlockType blockType = (RCBlockType) imBlockedMessageInfo.blockType;
            BlockedMessageInfo blockedMessageInfo = new BlockedMessageInfo(type, imBlockedMessageInfo.targetId,
                imBlockedMessageInfo.blockedMsgUId, blockType, imBlockedMessageInfo.extra);

            return blockedMessageInfo;
        }

        #endregion

        #region 会话

        internal static RCConversation toRCConversation(IntPtr imConversationPtr)
        {
            if (imConversationPtr != IntPtr.Zero)
            {
                im_conversation imConversation = Marshal.PtrToStructure<im_conversation>(imConversationPtr);
                return toRCConversation(ref imConversation);
            }
            else
            {
                return null;
            }
        }

        internal static RCConversation toRCConversation(ref im_conversation imConversation)
        {
            var rcConversation = new RCConversation
            {
                ConversationType = (RCConversationType) imConversation.type,
                TargetId = imConversation.targetId,
                UnreadMessageCount = imConversation.unreadCount,
                ReceivedStatus = new RCReceivedStatus(imConversation.receivedStatus),
                SentStatus = (RCSentStatus) imConversation.sentStatus,
                SentTime = imConversation.sentTime,
                ReceivedTime = imConversation.receivedTime,
                IsTop = imConversation.isTop,
                ObjectName = imConversation.objectName,
                SenderUserId = imConversation.senderUserId,
                LatestMessageId = imConversation.latestMessageId,

                MentionedCount = imConversation.mentionedCount,
                Draft = imConversation.draft,
                NotificationStatus = (RCConversationNotificationStatus) imConversation.blockStatus
            };
            rcConversation.LatestMessageContent =
                RCIMUtils.toConcreteRCMessageContent(imConversation.latestMessageContent,
                    imConversation.objectName);

            return rcConversation;
        }

        #endregion

        #region 黑名单

        internal static RCBlackListStatus toBlackListStatus(int iosBlacklistCode)
        {
            if (iosBlacklistCode == 101)
            {
                return RCBlackListStatus.NotInBlackList;
            }

            return RCBlackListStatus.InBlackList;
        }

        #endregion
    }
}
#endif