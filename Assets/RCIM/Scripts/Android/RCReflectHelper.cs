using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace cn_rongcloud_im_unity
{
    internal class RCReflectHelper
    {
        private const string TextMsgObjectName = "RC:TxtMsg";
        private const string RichTextMsgObjectName = "RC:ImgTextMsg";
        private const string ImageMsgObjectName = "RC:ImgMsg";
        private const string VoiceMsgObjectName = "RC:VcMsg";
        // private const string LocationMsgObjectName = "RC:LBSMsg";
        private const string HQVoiceMessageObjectName = "RC:HQVCMsg";
        private const string SightMessageObjectName = "RC:SightMsg";
        private const string GifMessageObjectName = "RC:GIFMsg";
        private const string FileMessageObjectName = "RC:FileMsg";
        private const string ReferenceMessageObjectName = "RC:ReferenceMsg";
        private const string CombineMessageObjectName = "RC:CombineMsg";
        private const string GroupNotificationMessageObjectName = "RC:GrpNtf";
        private const string RecallCommandMessageObjectName = "RC:RcCmd";
        private const string RecallNotificationMessageObjectName = "RC:RcNtf";    
        private const string ReadReceiptMessageObjectName = "RC:ReadNtf";

        
        private const string WrapperCommandObjectName = "RC:IWCmdMsg";
        private const string WrapperStorageObjectName = "RC:IWStorageMsg";
        private const string WrapperNormalObjectName = "RC:IWNormalMsg";
        private const string WrapperStatusObjectName = "RC:IWStatusMsg";

        private static readonly string IMWrapperCommandMessageClassFullName = "cn.rongcloud.imlib.iw.message.RCCommandMessage";
        private static readonly string IMWrapperStorageMessageClassFullName = "cn.rongcloud.imlib.iw.message.RCStorageMessage";
        private static readonly string IMWrapperNormalMessageClassFullName = "cn.rongcloud.imlib.iw.message.RCNormalMessage";
        private static readonly string IMWrapperStatusMessageClassFullName = "cn.rongcloud.imlib.iw.message.RCStatusMessage";

        private static readonly string SystemUriClassFullName = "android.net.Uri";
        private static readonly string ArrayListFullName = "java.util.ArrayList";
        private static readonly string HashMapFullName = "java.util.HashMap";
        private static readonly string LongFullName = "java.lang.Long";


        private static readonly string IMClientUserInfoClassFullName = "io.rong.imlib.model.UserInfo";
        private static readonly string IMTagInfoClassFullName = "io.rong.imlib.model.TagInfo";
        private static readonly string IMMentionedInfoClassFullName = "io.rong.imlib.model.MentionedInfo";
        private static readonly string IMMentionedTypeClassFullName = "io.rong.imlib.model.MentionedInfo$MentionedType";
        private static readonly string IMReadReceiptInfoClassFullName = "io.rong.imlib.model.ReadReceiptInfo";

        private static readonly string IMConversationIdentifierClassFullName =
            "io.rong.imlib.model.ConversationIdentifier";

        private static readonly string IMHistoryMessageOptionClassFullName = "io.rong.imlib.model.HistoryMessageOption";

        private static readonly string IMHistoryMessageOptionPullOrderClassFullName =
            "io.rong.imlib.model.HistoryMessageOption$PullOrder";

        private static readonly string IMClientTextMessageClassFullName = "io.rong.message.TextMessage";
        private static readonly string IMClientRichContentMessageClassFullName = "io.rong.message.RichContentMessage";
        private static readonly string IMClientImageMessageClassFullName = "io.rong.message.ImageMessage";
        private static readonly string IMClientVoiceMessageClassFullName = "io.rong.message.VoiceMessage";
        private static readonly string IMClientHQVoiceMessageClassFullName = "io.rong.message.HQVoiceMessage";
        private static readonly string IMClientLocationMessageClassFullName = "io.rong.imlib.location.message.LocationMessage";
        private static readonly string IMClientSightMessageClassFullName = "io.rong.message.SightMessage";
        private static readonly string IMClientGifMessageClassFullName = "io.rong.message.GIFMessage";
        private static readonly string IMClientFileMessageClassFullName = "io.rong.message.FileMessage";
        private static readonly string IMClientReferenceMessageClassFullName = "io.rong.message.ReferenceMessage";
        private static readonly string IMClientCombineMessageClassFullName = "io.rong.message.CombineMessage";
        private static readonly string IMClientUnknownMessageClassFullName = "io.rong.imlib.model.UnknownMessage";
        private static readonly string IMClientMessageClassFullName = "io.rong.imlib.model.Message";
        private static readonly string IMClientMessageConfigFullName = "io.rong.imlib.model.MessageConfig";
        private static readonly string IMClientMessagePushConfigFullName = "io.rong.imlib.model.MessagePushConfig";
        private static readonly string IMClientMessageiOSConfigFullName = "io.rong.imlib.model.IOSConfig";
        private static readonly string IMClientMessageAndroidConfigFullName = "io.rong.imlib.model.AndroidConfig";
        private static readonly string IMClientMessageAndroidConfigHWImportanceFullName = "io.rong.imlib.model.AndroidConfig$ImportanceHW";

        private static readonly string IMClientMessageDirectionFullName =
            "io.rong.imlib.model.Message$MessageDirection";

        private static readonly string IMClientMessageSentStatusClassFullName =
            "io.rong.imlib.model.Message$SentStatus";

        private static readonly string IMClientMessageReceivedStatusClassFullName =
            "io.rong.imlib.model.Message$ReceivedStatus";

        private static readonly string IMClientConversationClassFullName = "io.rong.imlib.model.Conversation";

        private static readonly string IMClientConversationTypeClassFullName =
            "io.rong.imlib.model.Conversation$ConversationType";

        private static readonly string IMClientConversationNotificationStatusClassFullName =
            "io.rong.imlib.model.Conversation$ConversationNotificationStatus";

        private static readonly string IMClientChatRoomInfoClassFullName = "io.rong.imlib.model.ChatRoomInfo";

        private static readonly string IMClientChatRoomMemberOrderClassFullName =
            "io.rong.imlib.model.ChatRoomInfo$ChatRoomMemberOrder";

        private static AndroidJavaClass LongClass = new AndroidJavaClass(LongFullName);
        private static AndroidJavaClass SystemUriClass = new AndroidJavaClass(SystemUriClassFullName);
        private static AndroidJavaClass IMUserInfoClass = new AndroidJavaClass(IMClientUserInfoClassFullName);
        private static AndroidJavaClass IMMentionedInfoClass = new AndroidJavaClass(IMMentionedInfoClassFullName);
        private static AndroidJavaClass IMMentionedTypeClass = new AndroidJavaClass(IMMentionedTypeClassFullName);
        private static AndroidJavaClass IMSightMessageClass = new AndroidJavaClass(IMClientSightMessageClassFullName);
        private static AndroidJavaClass IMGifMessageClass = new AndroidJavaClass(IMClientGifMessageClassFullName);
        private static AndroidJavaClass IMLocationMessageClass = new AndroidJavaClass(IMClientLocationMessageClassFullName);
        private static AndroidJavaClass IMVoiceMessageClass = new AndroidJavaClass(IMClientVoiceMessageClassFullName);
        private static AndroidJavaClass IMHQVoiceMessageClass = new AndroidJavaClass(IMClientHQVoiceMessageClassFullName);
        private static AndroidJavaClass IMFileMessageClass = new AndroidJavaClass(IMClientFileMessageClassFullName);
        private static AndroidJavaClass IMTextMessageClass = new AndroidJavaClass(IMClientTextMessageClassFullName);
        private static AndroidJavaClass IMReferenceMessageClass = new AndroidJavaClass(IMClientReferenceMessageClassFullName);

        private static AndroidJavaClass IMRichContentMessageClass =
            new AndroidJavaClass(IMClientRichContentMessageClassFullName);

        private static AndroidJavaClass IMWrapperCommandMessageClass =
            new AndroidJavaClass(IMWrapperCommandMessageClassFullName);
        private static AndroidJavaClass IMWrapperStorageMessageClass =
            new AndroidJavaClass(IMWrapperStorageMessageClassFullName);
        private static AndroidJavaClass IMWrapperNormalMessageClass =
            new AndroidJavaClass(IMWrapperNormalMessageClassFullName);
        private static AndroidJavaClass IMWrapperStatusMessageClass =
            new AndroidJavaClass(IMWrapperStatusMessageClassFullName);

        private static AndroidJavaClass IMMessageClass = new AndroidJavaClass(IMClientMessageClassFullName);
        private static AndroidJavaClass IMMessageConfigClass = new AndroidJavaClass(IMClientMessageConfigFullName);
        private static AndroidJavaClass IMMessagePushConfigClass = new AndroidJavaClass(IMClientMessagePushConfigFullName);
        private static AndroidJavaClass IMMessageDirectionClass = new AndroidJavaClass(IMClientMessageDirectionFullName);

        private static AndroidJavaClass IMMessageSentStatusClass =
            new AndroidJavaClass(IMClientMessageSentStatusClassFullName);

        private static AndroidJavaClass IMMessageReceivedStatusClass =
            new AndroidJavaClass(IMClientMessageReceivedStatusClassFullName);

        private static AndroidJavaClass IMConversationClass = new AndroidJavaClass(IMClientConversationClassFullName);

        private static AndroidJavaClass IMConversationTypeClass =
            new AndroidJavaClass(IMClientConversationTypeClassFullName);

        private static AndroidJavaClass IMConversationNotificationStatusClass =
            new AndroidJavaClass(IMClientConversationNotificationStatusClassFullName);

        internal static AndroidJavaObject GetContext()
        {
            AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject javaObject = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
            return javaObject;
        }

        internal static AndroidJavaObject FromConversationType(RCConversationType type)
        {
            var conversationTypeObject = IMConversationTypeClass.CallStatic<AndroidJavaObject>("setValue", (int) type);
            return conversationTypeObject;
        }

        internal static AndroidJavaObject FromConversationTypeArray(RCConversationType[] conversationTypes)
        {
            if (conversationTypes == null || conversationTypes.Length == 0)
                return null;

            var _stringArrayList = new AndroidJavaObject(ArrayListFullName);

            for (int i = 0; i < conversationTypes.Length; i++)
            {
                _stringArrayList.Call<bool>("add", FromConversationType(conversationTypes[i]));
            }

            return _stringArrayList.Call<AndroidJavaObject>("toArray");
        }

        internal static AndroidJavaObject FromTagInfo(RCTagInfo tagInfo)
        {
            return new AndroidJavaObject(IMTagInfoClassFullName, tagInfo.TagId, tagInfo.TagName, tagInfo.Count,
                tagInfo.TimeStamp);
        }

        internal static int[] ConvertRCConversationTypeToIntArray(RCConversationType[] conversationTypes)
        {
            var temp = new int[conversationTypes.Length];
            for (int i = 0; i < conversationTypes.Length; i++)
            {
                temp[i] = (int) conversationTypes[i];
            }

            return temp;
        }

        internal static AndroidJavaObject FromSentStatus(RCSentStatus sentStatus)
        {
            return IMMessageSentStatusClass.CallStatic<AndroidJavaObject>("setValue", (int) sentStatus);
        }

        internal static AndroidJavaObject FromReceivedStatus(RCReceivedStatus receivedStatus)
        {
            if (null == receivedStatus)
            {
                return new AndroidJavaObject(IMClientMessageReceivedStatusClassFullName, 0);
            }

            return new AndroidJavaObject(IMClientMessageReceivedStatusClassFullName, receivedStatus.Flag);
        }

        internal static AndroidJavaObject FromConversationNotificationStatus(
            RCConversationNotificationStatus notificationStatus)
        {
            var conversationTypeObject =
                IMConversationNotificationStatusClass.CallStatic<AndroidJavaObject>("setValue",
                    (int) notificationStatus);
            return conversationTypeObject;
        }

        internal static AndroidJavaObject FromConversationIdentifierList(IList<RCConversationIdentifier> conversations)
        {
            if (conversations == null || conversations.Count == 0)
                return null;

            var _stringArrayList = new AndroidJavaObject(ArrayListFullName);

            foreach (var item in conversations)
            {
                _stringArrayList.Call<bool>("add", FromRCConversationIdentifier(item));
            }

            return _stringArrayList;
        }

        internal static AndroidJavaObject FromRCConversationIdentifier(RCConversationIdentifier conversation)
        {
            return new AndroidJavaObject(IMConversationIdentifierClassFullName, FromConversationType(conversation.Type),
                conversation.TargetId);
        }

        internal static AndroidJavaObject GetRCConversationIdentifier(RCConversationType type, string targetId)
        {
            return new AndroidJavaObject(IMConversationIdentifierClassFullName, FromConversationType(type), targetId);
        }

        internal static AndroidJavaObject FromRCMessageDirection(RCMessageDirection direction)
        {
            return IMMessageDirectionClass.CallStatic<AndroidJavaObject>("setValue", (int) direction);
        }

        internal static AndroidJavaObject FromRCReadReceiptInfo(RCReadReceiptInfo receiptInfo)
        {
            if (receiptInfo == null)
                return null;
            var receiptInfoObj = new AndroidJavaObject(IMReadReceiptInfoClassFullName);
            receiptInfoObj.Call("setHasRespond", receiptInfo.HasRespond);
            receiptInfoObj.Call("setIsReadReceiptMessage", receiptInfo.IsReceiptRequestMessage);
            if (receiptInfo.RespondUserIdList != null && receiptInfo.RespondUserIdList.Count > 0)
            {
                receiptInfoObj.Call("setRespondUserIdList", FromStringLongDictionary(receiptInfo.RespondUserIdList));
            }

            return receiptInfoObj;
        }

        internal static AndroidJavaObject FromRCMessageConfig(RCMessageConfig config)
        {
            if (config == null)
                return null;
            var configObj = new AndroidJavaObject(IMClientMessageConfigFullName);
            configObj.Call("setDisableNotification", config.DisableNotification);
            return configObj;
        }

        internal static AndroidJavaObject FromRCMessagePushConfig(RCMessagePushConfig config)
        {
            if (config == null) return null;

            var configObj = new AndroidJavaObject(IMClientMessagePushConfigFullName);
            configObj.Call("setDisablePushTitle", config.DisablePushTitle);
            configObj.Call("setPushTitle", config.PushTitle);
            configObj.Call("setPushContent", config.PushContent);
            configObj.Call("setPushData", config.PushData);
            configObj.Call("setForceShowDetailContent", config.ForceShowDetailContent);
            configObj.Call("setTemplateId", config.TemplateId);
            configObj.Call("setIOSConfig", FromRCMessageiOSPushConfig(config.iOSConfig));
            configObj.Call("setAndroidConfig", FromRCMessageiOSPushConfig(config.iOSConfig));
            return configObj;
        }

        internal static AndroidJavaObject FromRCMessageiOSPushConfig(RCIOSConfig iOSConfig)
        {
            if (iOSConfig == null) return null;

            return new AndroidJavaObject(IMClientMessageiOSConfigFullName, iOSConfig.ThreadId, iOSConfig.APNSCollapseId,
                iOSConfig.Category, iOSConfig.RichMediaUrl);
        }

        internal static AndroidJavaObject FromRCHWImportance(RCAndroidConfig.HwPushConfigImportance importance)
        {
            var strImportance = string.Empty;
            if (importance == RCAndroidConfig.HwPushConfigImportance.Normal)
            {
                strImportance = "NORMAL";
            }else if (importance == RCAndroidConfig.HwPushConfigImportance.Low)
            {
                strImportance = "LOW";
            }

            return new AndroidJavaObject(IMClientMessageAndroidConfigHWImportanceFullName, strImportance);
        }

        internal static RCAndroidConfig.VivoType ToRCVivoType(string strVivoType)
        {
            if (strVivoType == "0")
                return RCAndroidConfig.VivoType.Operate;
            return RCAndroidConfig.VivoType.System;
        }

        internal static String FromRCTypeVivo(RCAndroidConfig.VivoType vivoType)
        {
            if (vivoType == RCAndroidConfig.VivoType.Operate)
                return "0";
            if (vivoType == RCAndroidConfig.VivoType.System)
                return "1";
            return string.Empty;
        }

        internal static AndroidJavaObject FromRCMessageAndroidPushConfig(RCAndroidConfig androidConfig)
        {
            if (androidConfig == null) return null;

            var configObj = new AndroidJavaObject(IMClientMessageAndroidConfigFullName);
            configObj.Call("setNotificationId", androidConfig.NotificationId);
            configObj.Call("setChannelIdMi", androidConfig.ChannelIdMi);
            configObj.Call("setChannelIdHW", androidConfig.ChannelIdHW);
            configObj.Call("setImportanceHW", FromRCHWImportance(androidConfig.ImportanceHW));
            configObj.Call("setChannelIdOPPO", androidConfig.ChannelIdOPPO);
            configObj.Call("setTypeVivo", FromRCTypeVivo(androidConfig.TypeVivo));
            configObj.Call("setCollapseKeyFCM", androidConfig.CollapseKeyFCM);
            configObj.Call("setImageUrlFCM", androidConfig.ImageUrlFCM);
            return configObj;
        }

        internal static AndroidJavaObject FromRCMessage(RCMessage message)
        {
            var messageObject = IMMessageClass.CallStatic<AndroidJavaObject>("obtain", message.TargetId,
                FromConversationType(message.ConversationType),
                FromRCMessageContent(message.Content));
            messageObject.Call("setUId", message.MessageUId);
            messageObject.Call("setMessageId", (int) message.MessageId);
            messageObject.Call("setMessageDirection", FromRCMessageDirection(message.Direction));
            messageObject.Call("setSenderUserId", message.SenderUserId);
            messageObject.Call("setReceivedStatus", FromReceivedStatus(message.ReceivedStatus));
            messageObject.Call("setSentStatus", FromSentStatus(message.SentStatus));
            messageObject.Call("setReceivedTime", message.ReceivedTime);
            messageObject.Call("setSentTime", message.SentTime);
            messageObject.Call("setReadReceiptInfo", FromRCReadReceiptInfo(message.ReadReceiptInfo));
            messageObject.Call("setCanIncludeExpansion", message.CanIncludeExpansion);
            messageObject.Call("setExpansion", FromStringStringDictionary(message.ExpansionDic));
            messageObject.Call("setMessageConfig", FromRCMessageConfig(message.MessageConfig));
            messageObject.Call("setMessagePushConfig", FromRCMessagePushConfig(message.MessagePushConfig));
            return messageObject;
        }

        internal static AndroidJavaObject FromRCMessageList(IList<RCMessage> messageList)
        {
            if (messageList == null || messageList.Count == 0)
                return null;

            var _stringArrayList = new AndroidJavaObject(ArrayListFullName);

            foreach (var item in messageList)
            {
                _stringArrayList.Call<bool>("add", FromRCMessage(item));
            }

            return _stringArrayList;
        }

        public static AndroidJavaObject FromRCMessageContent(RCMessageContent content)
        {
            AndroidJavaObject msgContentObj = null;

            if (content is RCCustomMessage)
            {
                msgContentObj = BuildAndroidCustomMessageWrapperContent(content);
            }
            else
            {
                msgContentObj = BuildAndroidBuiltInMessageContent(content);
            }

            if (msgContentObj == null)
            {
                Debug.Log("convert RCMessageContent to AndroidJavaObject failed.");
                return msgContentObj;
            }

            if (content.SendUserInfo != null)
            {
                msgContentObj.Call("setUserInfo", FromUserInfo(content.SendUserInfo));
            }

            if (content.MentionedInfo != null)
            {
                msgContentObj.Call("setMentionedInfo", FromMentionedInfo(content.MentionedInfo));
            }

            msgContentObj.Call("setExtra", content.Extra);
            msgContentObj.Call("setDestruct", content.IsDestruct);
            msgContentObj.Call("setDestructTime", content.DestructDuration);

            return msgContentObj;
        }

        private static AndroidJavaObject BuildAndroidBuiltInMessageContent(RCMessageContent content)
        {
            AndroidJavaObject msgContentObj = null;
            if (content.ObjectName == TextMsgObjectName)
            {
                var txtContent = content as RCTextMessage;
                msgContentObj = new AndroidJavaObject(IMClientTextMessageClassFullName);
                msgContentObj.Call("setContent", txtContent.Content);
            }
            else if (content.ObjectName == RichTextMsgObjectName)
            {
                var richContent = content as RCRichContentMessage;
                msgContentObj = new AndroidJavaObject(IMClientRichContentMessageClassFullName);
                msgContentObj.Call("setTitle", richContent.Title);
                msgContentObj.Call("setContent", richContent.Digest);
                msgContentObj.Call("setImgUrl", richContent.ImageUrl);
                msgContentObj.Call("setUrl", richContent.Url);
            }
            else if (content.ObjectName == ImageMsgObjectName)
            {
                var imgContent = content as RCImageMessage;
                msgContentObj = new AndroidJavaObject(IMClientImageMessageClassFullName);
                msgContentObj.Call("setName", imgContent.Name);
                msgContentObj.Call("setLocalPath", FromUrlPath(imgContent.LocalPath));

                if (false == string.IsNullOrEmpty(imgContent.RemoteUrl))
                {
                    msgContentObj.Call("setRemoteUri", FromUrlPath(imgContent.RemoteUrl));
                }

                if (false == string.IsNullOrEmpty(imgContent.ThumbUri))
                {
                    msgContentObj.Call("setThumUri", FromUrlPath(imgContent.ThumbUri));
                }

                msgContentObj.Call("setUpLoadExp", imgContent.UploadFailed);
                msgContentObj.Call("setIsFull", imgContent.IsOriginal);
                msgContentObj.Call("setBase64", imgContent.Base64);
            }
            else if (content.ObjectName == VoiceMsgObjectName)
            {
                var voiceContent = content as RCVoiceMessage;
                msgContentObj = IMVoiceMessageClass.CallStatic<AndroidJavaObject>("obtain",
                    FromUrlPath(voiceContent.LocalPath), voiceContent.Duration);
                msgContentObj.Call("setBase64", voiceContent.Base64);
            }
            else if (content.ObjectName == HQVoiceMessageObjectName)
            {
                var hqVoiceContent = content as RCHQVoiceMessage;
                msgContentObj = IMHQVoiceMessageClass.CallStatic<AndroidJavaObject>("obtain",
                    FromUrlPath(hqVoiceContent.LocalPath), hqVoiceContent.Duration);
                msgContentObj.Call("setName", hqVoiceContent.Name);
                if (false == string.IsNullOrEmpty(hqVoiceContent.MediaUrl))
                {
                    msgContentObj.Call("setFileUrl", FromUrlPath(hqVoiceContent.MediaUrl));
                }
            }
            // else if (content.ObjectName == LocationMsgObjectName)
            // {
            //     var locationContent = content as RCLocationMessage;
            //     msgContentObj = IMLocationMessageClass.CallStatic<AndroidJavaObject>("obtain", locationContent.Latitude,
            //         locationContent.Longitude, locationContent.POI, FromUrlPath(locationContent.ThumbUrl));
            // }
            else if (content.ObjectName == SightMessageObjectName)
            {
                var sightContent = content as RCSightMessage;
                msgContentObj = IMSightMessageClass.CallStatic<AndroidJavaObject>("obtain",
                    GetContext(), FromUrlPath(sightContent.LocalPath), sightContent.Duration);
                // msgContentObj.Call("setName", sightContent.Name);
                // msgContentObj.Call("setLocalPath", FromUrlPath(sightContent.LocalPath));
                // msgContentObj.Call("setMediaUrl", FromUrlPath(sightContent.MediaUrl));
                //
                // msgContentObj.Call("setSize", sightContent.Size);
                // msgContentObj.Call("setDuration", sightContent.Duration);
                // msgContentObj.Call("setBase64", sightContent.Base64);
            }
            else if (content.ObjectName == GifMessageObjectName)
            {
                var gifContent = content as RCGifMessage;
                msgContentObj =
                    IMGifMessageClass.CallStatic<AndroidJavaObject>("obtain", FromUrlPath(gifContent.LocalPath));
                msgContentObj.Call("setName", gifContent.Name);
                msgContentObj.Call("setLocalUri", FromUrlPath(gifContent.LocalPath));
                if (false == string.IsNullOrEmpty(gifContent.MediaUrl))
                {
                    msgContentObj.Call("setMediaUrl", FromUrlPath(gifContent.MediaUrl));
                }

                msgContentObj.Call("setWidth", gifContent.Width);
                msgContentObj.Call("setHeight", gifContent.Height);
                msgContentObj.Call("setGifDataSize", gifContent.Size);
                msgContentObj.Call("setUpLoadExp", gifContent.UploadFailed);
            }
            else if (content.ObjectName == FileMessageObjectName)
            {
                var fileContent = content as RCFileMessage;
                msgContentObj =
                    IMFileMessageClass.CallStatic<AndroidJavaObject>("obtain", FromUrlPath(fileContent.LocalPath));
                msgContentObj.Call("setName", fileContent.Name);
                msgContentObj.Call("setLocalPath", FromUrlPath(fileContent.LocalPath));
                if (false == string.IsNullOrEmpty(fileContent.MediaUrl))
                {
                    msgContentObj.Call("setFileUrl", FromUrlPath(fileContent.MediaUrl));
                }

                msgContentObj.Call("setType", fileContent.Type);
                msgContentObj.Call("setSize", fileContent.Size);
            }
            else if (content.ObjectName == ReferenceMessageObjectName)
            {
                if (content is RCReferenceMessage referenceContent)
                {
                    var referredMsgContentObject = FromRCMessageContent(referenceContent.ReferredMsg);
                    msgContentObj = IMReferenceMessageClass.CallStatic<AndroidJavaObject>("obtainMessage",
                        referenceContent.ReferredMsgUserId, referredMsgContentObject, referenceContent.ReferredMsgUid);
                    if (false == string.IsNullOrEmpty(referenceContent.LocalPath))
                    {
                        msgContentObj.Call("setLocalPath", FromUrlPath(referenceContent.LocalPath));
                    }

                    if (false == string.IsNullOrEmpty(referenceContent.MediaUrl))
                    {
                        msgContentObj.Call("setFileUrl", FromUrlPath(referenceContent.MediaUrl));
                    }

                    msgContentObj.Call("setUserId", referenceContent.ReferredMsgUserId);
                    msgContentObj.Call("setEditSendText", referenceContent.Content);
                }
            }
            // else if (content.ObjectName == CombineMessageObjectName)
            // {
            // var combineContent = content as RCCombineMessage;
            // msgContentObj = new AndroidJavaObject(IMClientCombineMessageClassFullName);
            // msgContentObj.Call("setName", combineContent.Name);
            // msgContentObj.Call("setLocalPath", FromUrlPath(combineContent.LocalPath));
            // if (false == string.IsNullOrEmpty(combineContent.MediaUrl))
            // {
            //     msgContentObj.Call("setFileUrl", FromUrlPath(combineContent.MediaUrl));
            // }
            //
            // msgContentObj.Call("setTitle", combineContent.Title);
            // msgContentObj.Call("setConversationType", FromConversationType(combineContent.ConversationType));
            // msgContentObj.Call("setNameList", FromStringList(combineContent.NameList));
            // msgContentObj.Call("setSummaryList", FromStringList(combineContent.SummaryList));
            // }
            else
            {
                msgContentObj = new AndroidJavaObject(IMClientUnknownMessageClassFullName);
            }

            return msgContentObj;
        }

        internal static RCChatRoomDestroyType ToRCChatRoomDestroyType(AndroidJavaObject chatRoomDestroyType)
        {
            if (chatRoomDestroyType == null)
                return cn_rongcloud_im_unity.RCChatRoomDestroyType.Unknown;

            return (RCChatRoomDestroyType)chatRoomDestroyType.Call<int>("getType");
        }

        private static AndroidJavaObject BuildAndroidCustomMessageWrapperContent(RCMessageContent content)
        {
            AndroidJavaObject msgContentObj = null;
            var customMessage = content as RCCustomMessage;
            if (customMessage == null)
            {
                return msgContentObj;
            }

            if (customMessage.CustomMessageType == RCCustomMessageType.Command)
            {
                msgContentObj = new AndroidJavaObject(IMWrapperCommandMessageClassFullName, content.ObjectName,
                    FromStringStringDictionary(customMessage.CustomFields));
            }
            else if (customMessage.CustomMessageType == RCCustomMessageType.Storage)
            {
                msgContentObj = new AndroidJavaObject(IMWrapperStorageMessageClassFullName,
                    content.ObjectName, FromStringStringDictionary(customMessage.CustomFields));
            }
            else if (customMessage.CustomMessageType == RCCustomMessageType.Normal)
            {
                msgContentObj = new AndroidJavaObject(IMWrapperNormalMessageClassFullName,
                    content.ObjectName, FromStringStringDictionary(customMessage.CustomFields));
            }
            else if (customMessage.CustomMessageType == RCCustomMessageType.Status)
            {
                msgContentObj = new AndroidJavaObject(IMWrapperStatusMessageClassFullName, content.ObjectName,
                    FromStringStringDictionary(customMessage.CustomFields));
            }

            return msgContentObj;
        }

        internal static IDictionary<string, RCErrorCode> ToStringErrorCodeDictionary(
            AndroidJavaObject stringErrorCodeDicObj)
        {
            if (stringErrorCodeDicObj == null) return null;

            var dicKeySetObj = stringErrorCodeDicObj.Call<AndroidJavaObject>("keySet");

            var keySetSize = dicKeySetObj.Call<int>("size");
            var keyArray = dicKeySetObj.Call<string[]>("toArray");

            var keyList = new List<String>();
            for (int i = 0; i < keySetSize; i++)
            {
                keyList.Add(keyArray[i]);
            }

            var stringStringDic = new Dictionary<string, RCErrorCode>();
            foreach (var item in keyList)
            {
                var errorCodeObj = stringErrorCodeDicObj.Call<AndroidJavaObject>("get", item);

                stringStringDic.Add(item, ToRCErrorCode(errorCodeObj));
            }

            return stringStringDic;
        }

        internal static RCChatRoomInfo ToRCChatRoomInfo(AndroidJavaObject chatroomInfoObj)
        {
            if (chatroomInfoObj == null) return null;

            var roomId = chatroomInfoObj.Call<string>("getChatRoomId");
            var memberOrder = ToRCChatRoomMemberOrder(chatroomInfoObj.Call<AndroidJavaObject>("getMemberOrder"));
            var roomMembers = ToChatRoomMemberList(chatroomInfoObj.Call<AndroidJavaObject>("getMemberInfo"));
            var totalMemberCount = chatroomInfoObj.Call<int>("getTotalMemberCount");

            return new RCChatRoomInfo(roomId, memberOrder, roomMembers, totalMemberCount);
        }

        private static IList<RCChatRoomMemberInfo> ToChatRoomMemberList(AndroidJavaObject memberListObj)
        {
            var list = new List<RCChatRoomMemberInfo>();
            if (memberListObj == null)
                return list;

            var listSize = memberListObj.Call<int>("size");
            for (int i = 0; i < listSize; i++)
            {
                var itemObject = memberListObj.Call<AndroidJavaObject>("get", i);
                var memberInfo = ToRCChatRoomMemberInfo(itemObject);
                if (memberInfo == null) continue;
                list.Add(memberInfo);
            }

            return list;
        }

        public static AndroidJavaObject FromUserInfo(RCUserInfo userInfo)
        {
            if (userInfo == null) return null;

            var userInfoObject = new AndroidJavaObject(IMClientUserInfoClassFullName);
            userInfoObject.Call("setUserId", userInfo.UserId);
            userInfoObject.Call("setName", userInfo.Name);
            userInfoObject.Call("setPortraitUri", FromUrlPath(userInfo.PortraitUri));
            userInfoObject.Call("setExtra", userInfo.Extra);

            return userInfoObject;
        }

        public static AndroidJavaObject FromUrlPath(String urlPath)
        {
            return SystemUriClass.CallStatic<AndroidJavaObject>("parse", urlPath);
        }

        public static AndroidJavaObject FromMentionedInfo(RCMentionedInfo mentionedInfo)
        {
            var mentionedInfoObject = new AndroidJavaObject(IMMentionedInfoClassFullName);

            var mentionTypeObject =
                IMMentionedTypeClass.CallStatic<AndroidJavaObject>("valueOf", (int) mentionedInfo.Type);

            mentionedInfoObject.Call("setType", mentionTypeObject);
            mentionedInfoObject.Call("setMentionedUserIdList", FromStringList(mentionedInfo.UserIdList));

            mentionedInfoObject.Call("setMentionedContent", mentionedInfo.MentionedContent);

            return mentionedInfoObject;
        }

        // internal static AndroidJavaObject FromHistoryMessageOption(RCHistoryMessageOption option)
        // {
        //     var optionObj = new AndroidJavaObject(IMHistoryMessageOptionClassFullName, option.DateTime, option.Count,
        //         (int) option.PullOrder);
        //     return optionObj;
        // }

        internal static List<RCTypingStatus> ToTypingStatusList(AndroidJavaObject typingStatusSetObj)
        {
            var typingStatusList = new List<RCTypingStatus>();
            if (typingStatusSetObj == null)
            {
                return typingStatusList;
            }

            var statusObjArray = typingStatusSetObj.Call<AndroidJavaObject[]>("toArray");
            foreach (var statusObj in statusObjArray)
            {
                var userId = statusObj.Call<string>("getUserId");
                var typingContentType = statusObj.Call<string>("getTypingContentType");
                var sentTime = statusObj.Call<Int64>("getSentTime");

                typingStatusList.Add(new RCTypingStatus()
                {
                    UserId = userId,
                    TypingContentType = typingContentType,
                    SentTime = sentTime,
                });
            }

            return typingStatusList;
        }

        internal static RCBlackListStatus ToRCBlacklistStatus(AndroidJavaObject blackStatusObj)
        {
            if (blackStatusObj == null)
                throw new ArgumentNullException(nameof(blackStatusObj));
            var blackListStatus = RCBlackListStatus.InBlackList;
            var iBlacklistStatus = blackStatusObj.Call<int>("getValue");
            blackListStatus = (RCBlackListStatus) iBlacklistStatus;
            return blackListStatus;
        }

        internal static AndroidJavaObject FromStringList(IList<string> strings)
        {
            if (strings == null || strings.Count == 0)
                return null;

            var _stringArrayList = new AndroidJavaObject(ArrayListFullName);

            foreach (var item in strings)
            {
                _stringArrayList.Call<bool>("add", item);
            }

            return _stringArrayList;
        }

        internal static AndroidJavaObject FromStringArray(String[] stringArray)
        {
            if (stringArray == null || stringArray.Length == 0) return null;
            var stringList = new List<String>();
            foreach (var item in stringArray)
            {
                if (false == string.IsNullOrEmpty(item))
                {
                    stringList.Add(item);
                }
            }

            return FromStringList(stringList);
        }

        public static AndroidJavaObject FromInt64(Int64 longValue)
        {
            return LongClass.CallStatic<AndroidJavaObject>("valueOf", longValue);
        }

        internal static RCMessage ToRCMessage(AndroidJavaObject javaMsgObject, bool onlyForRecallMessage = false)
        {
            var rcMsg = new RCMessage();

            rcMsg.ConversationType = GetConversationType(javaMsgObject);

            var targetId = javaMsgObject.Call<string>("getTargetId");
            rcMsg.TargetId = targetId;

            var msgId = javaMsgObject.Call<int>("getMessageId");
            rcMsg.MessageId = msgId;

            var msgUid = javaMsgObject.Call<string>("getUId");
            rcMsg.MessageUId = msgUid;

            var msgDirectionObject = javaMsgObject.Call<AndroidJavaObject>("getMessageDirection");
            if (msgDirectionObject != null)
            {
                var iMsgDirection = msgDirectionObject.Call<int>("getValue");
                rcMsg.Direction = (RCMessageDirection) iMsgDirection;
            }

            var senderId = javaMsgObject.Call<string>("getSenderUserId");
            rcMsg.SenderUserId = senderId;

            var msgRecvStatusObject = javaMsgObject.Call<AndroidJavaObject>("getReceivedStatus");
            if (msgRecvStatusObject != null)
            {
                var iMsgRecvStatusFlag = msgRecvStatusObject.Call<int>("getFlag");
                rcMsg.ReceivedStatus = new RCReceivedStatus(iMsgRecvStatusFlag);
            }

            var msgSentStatusObject = javaMsgObject.Call<AndroidJavaObject>("getSentStatus");
            if (msgSentStatusObject != null)
            {
                var iMsgSentStatus = msgSentStatusObject.Call<int>("getValue");
                rcMsg.SentStatus = (RCSentStatus) iMsgSentStatus;
            }

            var receivedTime = javaMsgObject.Call<long>("getReceivedTime");
            rcMsg.ReceivedTime = receivedTime;

            var sentTime = javaMsgObject.Call<long>("getSentTime");
            rcMsg.SentTime = sentTime;

            var readTime = javaMsgObject.Call<long>("getReadTime");

            var objectName = javaMsgObject.Call<string>("getObjectName");
            rcMsg.ObjectName = objectName;
            rcMsg.Extra = GetExtra(javaMsgObject);

            var canIncludeExpansion = javaMsgObject.Call<bool>("isCanIncludeExpansion");
            rcMsg.CanIncludeExpansion = canIncludeExpansion;
            if (canIncludeExpansion)
            {
                rcMsg.ExpansionDic = GetMessageExpansion(javaMsgObject);
            }

            var readReceiptObject = javaMsgObject.Call<AndroidJavaObject>("getReadReceiptInfo");
            if (readReceiptObject != null)
            {
                var isReadReceiptMsg = readReceiptObject.Call<bool>("isReadReceiptMessage");
                var hasRespond = readReceiptObject.Call<bool>("hasRespond");
                var respondUserIdDicObject = readReceiptObject.Call<AndroidJavaObject>("getRespondUserIdList");
                var respondUserIdDic = ToStringLongDictionary(respondUserIdDicObject);

                var readReceiptInfo = new RCReadReceiptInfo()
                {
                    IsReceiptRequestMessage = isReadReceiptMsg,
                    HasRespond = hasRespond,
                    RespondUserIdList = respondUserIdDic
                };

                rcMsg.ReadReceiptInfo = readReceiptInfo;
            }

            var msgConfigObject = javaMsgObject.Call<AndroidJavaObject>("getMessageConfig");
            if (msgConfigObject != null)
            {
                var disableNotification = msgConfigObject.Call<bool>("isDisableNotification");
                rcMsg.MessageConfig = new RCMessageConfig()
                {
                    DisableNotification = disableNotification
                };
            }

            AttachMessagePushConfig(rcMsg, javaMsgObject);

            var msgContentObject = javaMsgObject.Call<AndroidJavaObject>("getContent");
            var rcMsgContent = ToRCMessageContent((onlyForRecallMessage ? "RC:RcNtf" : objectName), msgContentObject);

            var contentUserInfoObject = msgContentObject.Call<AndroidJavaObject>("getUserInfo");
            AttachMessageContentUserInfo(rcMsgContent, contentUserInfoObject);
            var contentMentionedInfoObject = msgContentObject.Call<AndroidJavaObject>("getMentionedInfo");
            AttachMessageContentMentionedInfo(rcMsgContent, contentMentionedInfoObject);

            rcMsg.Content = rcMsgContent;

            return rcMsg;
        }

        private static void AttachMessagePushConfig(RCMessage rcMsg, AndroidJavaObject javaMsgObject)
        {
            if (javaMsgObject == null) return;
            var msgPushConfigObject = javaMsgObject.Call<AndroidJavaObject>("getMessagePushConfig");
            if (msgPushConfigObject == null) return;

            var pushConfTemplateId = msgPushConfigObject.Call<string>("getTemplateId");
            var pushConfTitle = msgPushConfigObject.Call<string>("getPushTitle");
            var pushConfContent = msgPushConfigObject.Call<string>("getPushContent");
            var pushConfData = msgPushConfigObject.Call<string>("getPushData");
            var forceShowDetail = msgPushConfigObject.Call<bool>("isForceShowDetailContent");
            var disablePushTitle = msgPushConfigObject.Call<bool>("isDisablePushTitle");

            var rcPushConfig = new RCMessagePushConfig
            {
                TemplateId = pushConfTemplateId,
                PushTitle = pushConfTitle,
                PushContent = pushConfContent,
                PushData = pushConfData,
                ForceShowDetailContent = forceShowDetail,
                DisablePushTitle = disablePushTitle
            };

            var iOSConfigObject = msgPushConfigObject.Call<AndroidJavaObject>("getIOSConfig");
            if (iOSConfigObject != null)
            {
                var iOSConfigThreadId = iOSConfigObject.Call<string>("getThread_id");
                var iOSConfigAPNSCollapseId = iOSConfigObject.Call<string>("getApns_collapse_id");
                var iOSConfigCategory = iOSConfigObject.Call<string>("getCategory");
                var iOSConfigRichMediaUrl = iOSConfigObject.Call<string>("getRichMediaUri");

                var rcIOSPushConfig = new RCIOSConfig
                {
                    ThreadId = iOSConfigThreadId,
                    APNSCollapseId = iOSConfigAPNSCollapseId
                };
                rcPushConfig.iOSConfig = rcIOSPushConfig;
            }

            var androidConfigObject = msgPushConfigObject.Call<AndroidJavaObject>("getAndroidConfig");
            if (androidConfigObject != null)
            {
                var androidConfigNotifId = androidConfigObject.Call<string>("getNotificationId");
                var androidConfigChannelIdHW = androidConfigObject.Call<string>("getChannelIdHW");
                var androidConfigChannelIdMi = androidConfigObject.Call<string>("getChannelIdMi");
                var androidConfigChannelIdOppo = androidConfigObject.Call<string>("getChannelIdOPPO");
                var androidConfigChannelIdVivo = androidConfigObject.Call<string>("getTypeVivo");
                var androidConfigCollapseKeyFCM = androidConfigObject.Call<string>("getCollapseKeyFCM");
                var androidConfigImgUrlFCM = androidConfigObject.Call<string>("getImageUrlFCM");

                var rcAndroidPushConfig = new RCAndroidConfig
                {
                    NotificationId = androidConfigNotifId,
                    ChannelIdHW = androidConfigChannelIdHW,
                    ChannelIdMi = androidConfigChannelIdMi,
                    ChannelIdOPPO = androidConfigChannelIdOppo,
                    TypeVivo = ToRCVivoType(androidConfigChannelIdVivo),
                };
                rcPushConfig.AndroidConfig = rcAndroidPushConfig;
            }

            rcMsg.MessagePushConfig = rcPushConfig;
        }

        internal static RCChatRoomMemberOrder ToRCChatRoomMemberOrder(AndroidJavaObject orderObj)
        {
            var iOrder = orderObj.Call<int>("getValue");
            return (RCChatRoomMemberOrder) iOrder;
        }

        // removed cause ChatRoomMemberOrder has no public constructor. 
        // internal static AndroidJavaObject FromChatRoomMemberOrder(RCChatRoomMemberOrder order)
        // {
        //     return new AndroidJavaObject(IMClientChatRoomMemberOrderClassFullName, (int) order);
        // }

        internal static RCChatRoomMemberInfo ToRCChatRoomMemberInfo(AndroidJavaObject memberObj)
        {
            if (memberObj == null) return null;
            var memberId = memberObj.Call<string>("getUserId");
            var joinTime = memberObj.Call<Int64>("getJoinTime");

            return new RCChatRoomMemberInfo(memberId, joinTime);
        }

        private static string GetExtra(AndroidJavaObject javaMsgObject)
        {
            return javaMsgObject.Call<string>("getExtra");
        }

        private static string GetUrlAsString(AndroidJavaObject userInfoObject, string urlFieldName)
        {
            try
            {
                var contentUserPortraitObject = userInfoObject.Call<AndroidJavaObject>(urlFieldName);
                if (contentUserPortraitObject == null)
                    return String.Empty;
                var contentUserPortrait = contentUserPortraitObject.Call<string>("toString");
                return contentUserPortrait;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return string.Empty;
            }
        }

        internal static AndroidJavaObject FromStringStringDictionary(IDictionary<string, string> expansionDic)
        {
            if (expansionDic == null || expansionDic.Count == 0)
                return null;
            var javaMap = new AndroidJavaObject(HashMapFullName);
            var putMethod = AndroidJNIHelper.GetMethodID(javaMap.GetRawClass(), "put",
                "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");

            object[] args = new object[2];
            foreach (KeyValuePair<string, string> kvp in expansionDic)
            {
                using (var k = new AndroidJavaObject("java.lang.String", kvp.Key))
                {
                    using (var v = new AndroidJavaObject("java.lang.String", kvp.Value))
                    {
                        args[0] = k;
                        args[1] = v;
                        AndroidJNI.CallObjectMethod(javaMap.GetRawObject(), putMethod,
                            AndroidJNIHelper.CreateJNIArgArray(args));
                    }
                }
            }

            return javaMap;
        }

        internal static AndroidJavaObject FromStringLongDictionary(IDictionary<string, Int64> expansionDic)
        {
            var javaMap = new AndroidJavaObject(HashMapFullName);
            var putMethod = AndroidJNIHelper.GetMethodID(javaMap.GetRawClass(), "put",
                "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");

            object[] args = new object[2];
            foreach (KeyValuePair<string, Int64> kvp in expansionDic)
            {
                using (var k = new AndroidJavaObject("java.lang.String", kvp.Key))
                {
                    using (var v = new AndroidJavaObject("java.lang.Long", kvp.Value))
                    {
                        args[0] = k;
                        args[1] = v;
                        AndroidJNI.CallObjectMethod(javaMap.GetRawObject(), putMethod,
                            AndroidJNIHelper.CreateJNIArgArray(args));
                    }
                }
            }

            return javaMap;
        }
        private static Dictionary<String, String> GetMessageExpansion(AndroidJavaObject messageObject)
        {
            if (messageObject == null) return null;
            return ToStringStringDictionary(messageObject.Call<AndroidJavaObject>("getExpansion"));
        }

        private static Dictionary<String, String> GetCustomFields(AndroidJavaObject custMsgObject)
        {
            if (custMsgObject == null) return null;
            return ToStringStringDictionary(custMsgObject.Call<AndroidJavaObject>("getMessageFields"));
        }

        internal static Dictionary<string, long> ToStringLongDictionary(AndroidJavaObject stringLongDicObj)
        {
            if (stringLongDicObj == null) return null;

            var entrySetObj = stringLongDicObj.Call<AndroidJavaObject>("entrySet");

            var entrySetSize = entrySetObj.Call<int>("size");
            var entryObjectArray = entrySetObj.Call<AndroidJavaObject[]>("toArray");

            var stringLongDic = new Dictionary<string, Int64>();
            for (var i = 0; i < entrySetSize; i++)
            {
                var key = entryObjectArray[i].Call<String>("getKey");
                var keyValueStr = entryObjectArray[i].Call<String>("toString");
                var valueStr = keyValueStr.Replace(key, string.Empty).Replace("=", string.Empty);
                Int64.TryParse(valueStr, out var value);
                stringLongDic.Add(key, value);
            }

            return stringLongDic;
        }

        internal static Dictionary<string, string> ToStringStringDictionary(AndroidJavaObject stringStringDicObj)
        {
            if (stringStringDicObj == null) return null;

            var entrySetObject = stringStringDicObj.Call<AndroidJavaObject>("entrySet");

            var entrySetSize = entrySetObject.Call<int>("size");
            var entryObjectArray = entrySetObject.Call<AndroidJavaObject[]>("toArray");

            var stringStringDic = new Dictionary<string, string>();
            for (int i = 0; i < entrySetSize; i++)
            {
                var key = entryObjectArray[i].Call<String>("getKey");
                var keyValueStr = entryObjectArray[i].Call<String>("toString");
                Debug.Log($"keyValueStr {keyValueStr}");
                var keyValueArray = keyValueStr.Split('=');
                var val = string.Empty;
                if (keyValueArray.Length == 2)
                {
                    val = keyValueArray[1];
                }

                stringStringDic.Add(key, val);
            }

            return stringStringDic;
        }

        internal static BlockedMessageInfo ToBlockedMessageInfo(AndroidJavaObject blockedMessageInfoObj)
        {
            if (blockedMessageInfoObj == null) return null;
            var blockedMsgConversationType = GetConversationType(blockedMessageInfoObj);
            var blockedMsgTargetId = GetTargetId(blockedMessageInfoObj);
            var blockedMsgUid = GetBlockMsgUid(blockedMessageInfoObj);
            var blockedMsgBlockType = GetBlockType(blockedMessageInfoObj);
            var blockedMsgExtra = GetExtra(blockedMessageInfoObj);
            return new BlockedMessageInfo(blockedMsgConversationType, blockedMsgTargetId, blockedMsgUid,
                blockedMsgBlockType, blockedMsgExtra);
        }

        internal static RCMessageContent ToRCMessageContent(string objectName, AndroidJavaObject msgContentObject)
        {
            Debug.Log($"ToRCMessageContent for objectName {objectName}");
            if (String.IsNullOrEmpty(objectName) || msgContentObject == null)
                return null;
            var contentIsDestruct = msgContentObject.Call<bool>("isDestruct");
            var contentDestructTime = msgContentObject.Call<long>("getDestructTime");
            var extra = GetExtra(msgContentObject);

            if (objectName == TextMsgObjectName)
            {
                var txtMsgContent = msgContentObject.Call<string>("getContent");

                return new RCTextMessage()
                {
                    Extra = extra,
                    IsDestruct = contentIsDestruct,
                    DestructDuration = contentDestructTime,
                    Content = txtMsgContent,
                    ObjectName = objectName,
                };
            }
            else if (objectName == RichTextMsgObjectName)
            {
                var title = GetStringFromObject(msgContentObject, "getTitle");
                var digest = msgContentObject.Call<string>("getContent");
                var imgUrl = msgContentObject.Call<string>("getImgUrl");
                var url = msgContentObject.Call<string>("getUrl");

                return new RCRichContentMessage()
                {
                    Extra = extra,
                    IsDestruct = contentIsDestruct,
                    DestructDuration = contentDestructTime,
                    Title = title,
                    Digest = digest,
                    ImageUrl = imgUrl,
                    Url = url,
                    ObjectName = objectName,
                };
            }
            else if (objectName == ImageMsgObjectName)
            {
                var name = GetNameFromContentObject(msgContentObject);
                var localPath = GetUrlAsString(msgContentObject, "getLocalUri");
                var remoteUrl = GetUrlAsString(msgContentObject, "getRemoteUri");
                var thumbUrl = GetUrlAsString(msgContentObject, "getThumUri");
                var base64 = msgContentObject.Call<string>("getBase64");
                var uploadFailed = msgContentObject.Call<bool>("isUpLoadExp");
                var isOriginal = msgContentObject.Call<bool>("isFull");

                return new RCImageMessage()
                {
                    Extra = extra,
                    IsDestruct = contentIsDestruct,
                    DestructDuration = contentDestructTime,

                    Name = name,
                    LocalPath = localPath,
                    MediaUrl = remoteUrl,

                    ThumbUri = thumbUrl,

                    Base64 = base64,
                    IsOriginal = isOriginal,
                    UploadFailed = uploadFailed,
                };
            }
            else if (objectName == VoiceMsgObjectName)
            {
                var url = GetUrlAsString(msgContentObject, "getUri");
                var duration = msgContentObject.Call<int>("getDuration");
                var base64 = msgContentObject.Call<string>("getBase64");

                return new RCVoiceMessage()
                {
                    Extra = extra,
                    IsDestruct = contentIsDestruct,
                    DestructDuration = contentDestructTime,

                    LocalPath = url,
                    Duration = duration,
                    Base64 = base64,
                };
            }
            // else if (objectName == LocationMsgObjectName)
            // {
            //     var latitude = msgContentObject.Call<double>("getLat");
            //     var longitude = msgContentObject.Call<double>("getLng");
            //     var poi = msgContentObject.Call<string>("getPoi");
            //     var base64 = msgContentObject.Call<string>("getBase64");
            //     var imgUrl = GetUrlAsString(msgContentObject, "getImgUri");

            //     return new RCLocationMessage()
            //     {
            //         Extra = extra,
            //         IsDestruct = contentIsDestruct,
            //         DestructDuration = contentDestructTime,

            //         Latitude = latitude,
            //         Longitude = longitude,
            //         POI = poi,
            //         Base64 = base64,
            //         ThumbUrl = imgUrl
            //     };
            // }
            else if (objectName == HQVoiceMessageObjectName)
            {
                var name = GetNameFromContentObject(msgContentObject);
                var localPath = GetUrlAsString(msgContentObject, "getLocalPath");
                var remoteUrl = GetUrlAsString(msgContentObject, "getFileUrl");
                var duration = msgContentObject.Call<int>("getDuration");

                return new RCHQVoiceMessage()
                {
                    Extra = extra,
                    IsDestruct = contentIsDestruct,
                    DestructDuration = contentDestructTime,

                    Name = name,
                    LocalPath = localPath,
                    MediaUrl = remoteUrl,

                    Duration = duration,
                };
            }
            else if (objectName == SightMessageObjectName)
            {
                var name = GetNameFromContentObject(msgContentObject);
                var size = GetSizeFromContentObject(msgContentObject);
                var base64 = msgContentObject.Call<string>("getBase64");
                var thumbPath = GetUrlAsString(msgContentObject, "getThumbUri");
                var localPath = GetUrlAsString(msgContentObject, "getLocalPath");
                var remoteUrl = GetUrlAsString(msgContentObject, "getMediaUrl");
                var duration = msgContentObject.Call<int>("getDuration");
                var isOriginal = msgContentObject.Call<bool>("isOriginal");

                return new RCSightMessage()
                {
                    Extra = extra,
                    IsDestruct = contentIsDestruct,
                    DestructDuration = contentDestructTime,

                    Name = name,
                    LocalPath = localPath,
                    MediaUrl = remoteUrl,

                    ThumbUri = thumbPath,
                    Base64 = base64,
                    Duration = duration,
                    Size = size,
                    IsOriginal = isOriginal,
                };
            }
            else if (objectName == GifMessageObjectName)
            {
                var width = msgContentObject.Call<int>("getWidth");
                var height = msgContentObject.Call<int>("getHeight");
                var gifSize = msgContentObject.Call<Int64>("getGifDataSize");
                var name = GetNameFromContentObject(msgContentObject);
                var localPath = GetUrlAsString(msgContentObject, "getLocalPath");
                var remoteUrl = GetUrlAsString(msgContentObject, "getRemoteUri");

                return new RCGifMessage()
                {
                    Extra = extra,
                    IsDestruct = contentIsDestruct,
                    DestructDuration = contentDestructTime,

                    Name = name,
                    LocalPath = localPath,
                    MediaUrl = remoteUrl,

                    Width = width,
                    Height = height,
                    Size = gifSize,
                };
            }
            else if (objectName == FileMessageObjectName)
            {
                var name = GetNameFromContentObject(msgContentObject);
                var localPath = GetUrlAsString(msgContentObject, "getLocalPath");
                var remoteUrl = GetUrlAsString(msgContentObject, "getMediaUrl");
                var size = msgContentObject.Call<Int64>("getSize");
                var type = msgContentObject.Call<string>("getType");
                var progress = msgContentObject.Get<int>("progress");

                return new RCFileMessage()
                {
                    Extra = extra,
                    IsDestruct = contentIsDestruct,
                    DestructDuration = contentDestructTime,

                    Name = name,
                    LocalPath = localPath,
                    MediaUrl = remoteUrl,

                    Size = size,
                    Type = type,
                    Progress = progress
                };
            }
            else if (objectName == ReferenceMessageObjectName)
            {
                var name = GetNameFromContentObject(msgContentObject);
                var localPath = GetUrlAsString(msgContentObject, "getLocalPath");
                var remoteUrl = GetUrlAsString(msgContentObject, "getMediaUrl");

                var content = msgContentObject.Call<string>("getEditSendText");
                var referMsgUserId = msgContentObject.Call<string>("getUserId");
                var referMsgObjName = msgContentObject.Call<string>("getObjName");
                var referMsgUid = msgContentObject.Call<string>("getReferMsgUid");
                var referMsgObj = msgContentObject.Call<AndroidJavaObject>("getReferenceContent");

                return new RCReferenceMessage()
                {
                    Extra = extra,
                    IsDestruct = contentIsDestruct,
                    DestructDuration = contentDestructTime,

                    Name = name,
                    LocalPath = localPath,
                    MediaUrl = remoteUrl,

                    Content = content,
                    ReferredMsgUserId = referMsgUserId,
                    ReferredMsgObjectName = referMsgObjName,
                    ReferredMsgUid = referMsgUid,
                    ReferredMsg = ToRCMessageContent(referMsgObjName, referMsgObj)
                };
            }
            else if (objectName == WrapperCommandObjectName)
            {
                var custMsgType = GetStringFromObject(msgContentObject, "getMessageType");
                var custMsgFields = GetCustomFields(msgContentObject);
                return new RCCustomMessage(custMsgType, RCCustomMessageType.Command, custMsgFields)
                {
                    Extra = extra,
                    IsDestruct = contentIsDestruct,
                    DestructDuration = contentDestructTime,

                };
            }
            else if (objectName == WrapperStorageObjectName)
            {
                var custMsgType = GetStringFromObject(msgContentObject, "getMessageType");
                var custMsgFields = GetCustomFields(msgContentObject);
                return new RCCustomMessage(custMsgType, RCCustomMessageType.Storage, custMsgFields)
                {
                    Extra = extra,
                    IsDestruct = contentIsDestruct,
                    DestructDuration = contentDestructTime,

                };
            }
            else if (objectName == WrapperNormalObjectName)
            {
                var custMsgType = GetStringFromObject(msgContentObject, "getMessageType");
                var custMsgFields = GetCustomFields(msgContentObject);
                return new RCCustomMessage(custMsgType, RCCustomMessageType.Normal, custMsgFields)
                {
                    Extra = extra,
                    IsDestruct = contentIsDestruct,
                    DestructDuration = contentDestructTime,

                };
            }
            else if (objectName == WrapperStatusObjectName)
            {
                var custMsgType = GetStringFromObject(msgContentObject, "getMessageType");
                var custMsgFields = GetCustomFields(msgContentObject);
                return new RCCustomMessage(custMsgType, RCCustomMessageType.Status, custMsgFields)
                {
                    Extra = extra,
                    IsDestruct = contentIsDestruct,
                    DestructDuration = contentDestructTime,

                };
            }
            // else if (objectName == CombineMessageObjectName)
            // {
            // var name = GetNameFromContentObject(msgContentObject);
            // var localPath = GetUrlAsString(msgContentObject, "getLocalPath");
            // var remoteUrl = GetUrlAsString(msgContentObject, "getMediaUrl");
            //
            // var title = GetStringFromObject(msgContentObject, "getTitle");
            // var conversationType = GetConversationType(msgContentObject);
            // var nameList = ToStringList(msgContentObject.Call<AndroidJavaObject>("getNameList"));
            // var summaryList = ToStringList(msgContentObject.Call<AndroidJavaObject>("getSummaryList"));
            //
            // return new RCCombineMessage()
            // {
            //     Extra = extra,
            //     IsDestruct = contentIsDestruct,
            //     DestructDuration = contentDestructTime,
            //
            //     Name = name,
            //     LocalPath = localPath,
            //     MediaUrl = remoteUrl,
            //
            //     Title = title,
            //     ConversationType = conversationType,
            //     NameList = nameList,
            //     SummaryList = summaryList,
            // };
            // }
            else if (objectName == GroupNotificationMessageObjectName)
            {
                var operatorId = msgContentObject.Call<string>("getOperatorUserId");
                var operation = msgContentObject.Call<string>("getOperation");
                var data = msgContentObject.Call<string>("getData");
                var message = msgContentObject.Call<string>("getMessage");

                return new RCGroupNotificationMessage()
                {
                    Extra = extra,
                    IsDestruct = contentIsDestruct,
                    DestructDuration = contentDestructTime,

                    OperatorUserId = operatorId,
                    Operation = operation,
                    Data = data,
                    Message = message
                };
            }
            else if (objectName == RecallNotificationMessageObjectName)
            {
                var operatorId = msgContentObject.Call<string>("getOperatorId");
                var recallTime = msgContentObject.Call<Int64>("getRecallTime");
                var originalObjName = msgContentObject.Call<string>("getOriginalObjectName");
                var isAdmin = msgContentObject.Call<bool>("isAdmin");
                var isDelete = msgContentObject.Call<bool>("isDelete");
                var recallContent = msgContentObject.Call<string>("getRecallContent");
                var recallActionTime = msgContentObject.Call<Int64>("getRecallActionTime");

                return new RCRecallNotificationMessage()
                {
                    Extra = extra,
                    IsDestruct = contentIsDestruct,
                    DestructDuration = contentDestructTime,

                    OperatorId = operatorId,
                    RecallTime = recallTime,
                    OriginalObjectName = originalObjName,
                    RecallContent = recallContent,
                    RecallActionTime = recallActionTime,
                    IsAdmin = isAdmin,
                    Delete = isDelete,
                };
            }
            else if (objectName == ReadReceiptMessageObjectName)
            {
                var lastMessageSentTime = msgContentObject.Call<Int64>("getLastMessageSendTime");

                return new RCReadReceiptMessage()
                {
                    LastMessageSentTime = lastMessageSentTime
                };
            }
            else if (objectName == RecallCommandMessageObjectName)
            {
                var targetId = msgContentObject.Call<string>("getTargetId");
                var conversationType = (RCConversationType) msgContentObject.Call<int>("getConversationType");
                var messageUid = msgContentObject.Call<string>("getMessageUId");
                var sentTime = msgContentObject.Call<Int64>("getSentTime");
                var channelId = msgContentObject.Call<string>("getChannelId");
                var isAdmin = msgContentObject.Call<bool>("isAdmin");
                var isDelete = msgContentObject.Call<bool>("isDelete");

                return new RCRecallCommandMessage()
                {
                    Extra = extra,
                    IsDestruct = contentIsDestruct,
                    DestructDuration = contentDestructTime,

                    TargetId = targetId,
                    ConversationType = conversationType,
                    MessageUid = messageUid,
                    SentTime = sentTime,
                    ChannelId = channelId,
                    IsAdmin = isAdmin,
                    IsDelete = isDelete,
                };
            }

            return new RCUnknownMessage();
        }

        private static string GetStringFromObject(AndroidJavaObject obj, string methodName)
        {
            try
            {
                return obj == null ? string.Empty : obj.Call<string>(methodName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return string.Empty;
            }
        }

        private static string GetNameFromContentObject(AndroidJavaObject msgContentObject)
        {
            var name = string.Empty;
            try
            {
                name = msgContentObject.Call<string>("getName");
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

            return name;
        }

        internal static Int64 GetSizeFromContentObject(AndroidJavaObject msgContentObject)
        {
            try
            {
                return msgContentObject.Call<Int64>("getSize");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return 0;
        }

        internal static RCRecallNotificationMessage ToNotificationMessage(AndroidJavaObject notifyMsgObj)
        {
            if (notifyMsgObj == null) return null;

            var operatorId = notifyMsgObj.Call<string>("getOperatorId");
            var recallTime = notifyMsgObj.Call<long>("getRecallTime");
            var originalObjName = notifyMsgObj.Call<string>("getOriginalObjectName");
            var isAdmin = notifyMsgObj.Call<bool>("isAdmin");
            var isDelete = notifyMsgObj.Call<bool>("isDelete");
            var recallContent = notifyMsgObj.Call<string>("getRecallContent");
            var recallActionTime = notifyMsgObj.Call<long>("getRecallActionTime");

            var recallNotifyMsg = new RCRecallNotificationMessage
            {
                OperatorId = operatorId,
                RecallTime = recallTime,
                OriginalObjectName = originalObjName,
                IsAdmin = isAdmin,
                Delete = isDelete,
                RecallContent = recallContent,
                RecallActionTime = recallActionTime
            };

            return recallNotifyMsg;
        }

        internal static RCConversationType ToRCConversationType(AndroidJavaObject converTypeObj)
        {
            if (converTypeObj == null)
                throw new ArgumentNullException(nameof(converTypeObj));

            var iConvType = converTypeObj.Call<int>("getValue");

            return (RCConversationType) iConvType;
        }

        internal static RCConversation ToRCConversation(AndroidJavaObject javaConversationObject)
        {
            if (javaConversationObject == null) return null;

            var rcConversation = new RCConversation();
            rcConversation.ConversationType = GetConversationType(javaConversationObject);
            rcConversation.TargetId = GetTargetId(javaConversationObject);

            var channelId = javaConversationObject.Call<string>("getChannelId");
            var conversationTitle = javaConversationObject.Call<string>("getConversationTitle");

            var portraitUrl = javaConversationObject.Call<string>("getPortraitUrl");

            var unreadMsgCount = javaConversationObject.Call<int>("getUnreadMessageCount");
            rcConversation.UnreadMessageCount = unreadMsgCount;

            var isTop = javaConversationObject.Call<bool>("isTop");
            rcConversation.IsTop = isTop;
            rcConversation.ReceivedStatus = GetReceivedStatus(javaConversationObject);
            rcConversation.SentStatus = GetSentStatus(javaConversationObject);

            var receivedTime = javaConversationObject.Call<Int64>("getReceivedTime");
            rcConversation.ReceivedTime = receivedTime;

            var sentTime = javaConversationObject.Call<Int64>("getSentTime");
            rcConversation.SentTime = sentTime;

            var objName = javaConversationObject.Call<string>("getObjectName");
            rcConversation.ObjectName = objName;

            var senderUserId = javaConversationObject.Call<string>("getSenderUserId");
            rcConversation.SenderUserId = senderUserId;

            var senderUserName = javaConversationObject.Call<string>("getSenderUserName");

            var latestMsgId = javaConversationObject.Call<int>("getLatestMessageId");
            rcConversation.LatestMessageId = latestMsgId;

            var latestMsgContentObj = javaConversationObject.Call<AndroidJavaObject>("getLatestMessage");
            rcConversation.LatestMessageContent = ToRCMessageContent(objName, latestMsgContentObj);

            var converDraft = javaConversationObject.Call<string>("getDraft");
            rcConversation.Draft = converDraft;

            var converNotiStatusObj = javaConversationObject.Call<AndroidJavaObject>("getNotificationStatus");
            rcConversation.NotificationStatus =
                (RCConversationNotificationStatus) converNotiStatusObj.Call<int>("getValue");

            var mentionedCount = javaConversationObject.Call<int>("getMentionedCount");
            rcConversation.MentionedCount = mentionedCount;

            return rcConversation;
        }

        private static RCBlockType GetBlockType(AndroidJavaObject javaBlockMsgInfo)
        {
            var blockTypeObj = javaBlockMsgInfo.Call<AndroidJavaObject>("getType");
            return (RCBlockType) blockTypeObj.Get<int>("value");
        }

        private static string GetBlockMsgUid(AndroidJavaObject javaBlockedMsgInfo)
        {
            return javaBlockedMsgInfo.Call<string>("getBlockMsgUid");
        }

        private static string GetTargetId(AndroidJavaObject javaConversationObject)
        {
            return javaConversationObject.Call<string>("getTargetId");
        }

        private static RCSentStatus GetSentStatus(AndroidJavaObject javaConversationObject)
        {
            var msgSentStatusObject = javaConversationObject.Call<AndroidJavaObject>("getSentStatus");
            var iMsgSentStatus = msgSentStatusObject.Call<int>("getValue");
            return (RCSentStatus) iMsgSentStatus;
        }

        private static RCReceivedStatus GetReceivedStatus(AndroidJavaObject javaConversationObject)
        {
            var msgRecvStatusObject = javaConversationObject.Call<AndroidJavaObject>("getReceivedStatus");
            var iMsgRecvStatusFlag = msgRecvStatusObject.Call<int>("getFlag");
            return new RCReceivedStatus(iMsgRecvStatusFlag);
        }

        private static RCConversationType GetConversationType(AndroidJavaObject javaMsgObject)
        {
            var typeObject = javaMsgObject.Call<AndroidJavaObject>("getConversationType");
            var iConverType = typeObject.Call<int>("getValue");

            return (RCConversationType) iConverType;
        }

        internal static IList<RCMessage> ToRCMessageList(AndroidJavaObject messageListObject)
        {
            var list = new List<RCMessage>();

            if (messageListObject == null)
                return list;

            var listSize = messageListObject.Call<int>("size");
            for (int i = 0; i < listSize; i++)
            {
                var itemObject = messageListObject.Call<AndroidJavaObject>("get", i);
                var rcMsg = ToRCMessage(itemObject);
                list.Add(rcMsg);
            }

            return list;
        }

        internal static IList<RCTagInfo> ToRCTagInfoList(AndroidJavaObject tagListObj)
        {
            var list = new List<RCTagInfo>();
            if (tagListObj == null)
                return list;

            var listSize = tagListObj.Call<int>("size");
            for (int i = 0; i < listSize; i++)
            {
                var itemObject = tagListObj.Call<AndroidJavaObject>("get", i);
                var rcMsg = ToRCTagInfo(itemObject);
                list.Add(rcMsg);
            }

            return list;
        }

        internal static RCTagInfo ToRCTagInfo(AndroidJavaObject tagInfoObj)
        {
            var tagId = tagInfoObj.Call<string>("getTagId");
            var tagName = tagInfoObj.Call<string>("getTagName");
            var count = tagInfoObj.Call<int>("getCount");
            var timestamp = tagInfoObj.Call<Int64>("getTimestamp");

            return new RCTagInfo(tagId, tagName, count, timestamp);
        }

        internal static IList<RCConversation> ToRCConversationList(AndroidJavaObject converationListObject)
        {
            var list = new List<RCConversation>();
            if (converationListObject == null)
                return list;

            var listSize = converationListObject.Call<int>("size");
            for (int i = 0; i < listSize; i++)
            {
                var itemObject = converationListObject.Call<AndroidJavaObject>("get", i);
                var rcConversation = ToRCConversation(itemObject);
                list.Add(rcConversation);
            }

            return list;
        }

        internal static IList<RCConversationTagInfo> ToRCConversationTagInfoList(
            AndroidJavaObject conversationTagListObj)
        {
            var list = new List<RCConversationTagInfo>();
            if (conversationTagListObj == null)
                return list;

            var listSize = conversationTagListObj.Call<int>("size");
            for (int i = 0; i < listSize; i++)
            {
                var itemObject = conversationTagListObj.Call<AndroidJavaObject>("get", i);
                var rcConversation = ToRCConversationTagInfo(itemObject);
                list.Add(rcConversation);
            }

            return list;
        }

        internal static RCConversationTagInfo ToRCConversationTagInfo(AndroidJavaObject conversationTagObj)
        {
            var tagInfo = ToRCTagInfo(conversationTagObj.Call<AndroidJavaObject>("getTagInfo"));
            var isTop = conversationTagObj.Call<bool>("isTop");

            return new RCConversationTagInfo(tagInfo, isTop);
        }

        internal static IList<RCSearchConversationResult> ToRCSearchConversationList(
            AndroidJavaObject searchConverationListObject)
        {
            var list = new List<RCSearchConversationResult>();
            if (searchConverationListObject == null)
                return list;

            var listSize = searchConverationListObject.Call<int>("size");
            for (int i = 0; i < listSize; i++)
            {
                var itemObject = searchConverationListObject.Call<AndroidJavaObject>("get", i);
                var rcConversation = ToRCConversation(itemObject.Call<AndroidJavaObject>("getConversation"));
                var matchCount = itemObject.Call<int>("getMatchCount");
                list.Add(new RCSearchConversationResult(rcConversation, matchCount));
            }

            return list;
        }

        internal static RCErrorCode ToRCErrorCode(AndroidJavaObject errorCodeObject)
        {
            RCErrorCode errorCode = RCErrorCode.Succeed;
            errorCode = (RCErrorCode) errorCodeObject.Call<int>("getValue");
            return errorCode;
        }

        internal static RCConnectionStatus ToConnectionStatus(AndroidJavaObject statusObject)
        {
            RCConnectionStatus status = RCConnectionStatus.Connecting;
            if (statusObject == null)
                return status;

            var iConnStatus = statusObject.Call<int>("getValue");
            return RCConnectionStatusConverter.Convert(iConnStatus);
        }

        internal static IList<string> ToStringList(AndroidJavaObject stringListObject)
        {
            var list = new List<string>();
            if (stringListObject == null)
                return list;

            var listSize = stringListObject.Call<int>("size");
            for (int i = 0; i < listSize; i++)
            {
                var item = stringListObject.Call<string>("get", i);
                list.Add(item);
            }

            return list;
        }

        private static void AttachMessageContentUserInfo(RCMessageContent msgContent, AndroidJavaObject userInfoObject)
        {
            if (msgContent == null || userInfoObject == null)
                return;
            var contentUserId = userInfoObject.Call<string>("getUserId");
            var contentUserName =GetNameFromContentObject(userInfoObject);
            var contentUserExtra = GetExtra(userInfoObject);
            var contentUserPortrait = GetUrlAsString(userInfoObject, "getPortraitUri");
            var contentUserInfo = new RCUserInfo(contentUserId, contentUserName, contentUserPortrait, contentUserExtra);
            msgContent.SendUserInfo = contentUserInfo;
        }

        private static void AttachMessageContentMentionedInfo(RCMessageContent msgContent,
            AndroidJavaObject mentionedInfoObject)
        {
            if (msgContent == null || mentionedInfoObject == null)
                return;
            var mentionedTypeObject = mentionedInfoObject.Call<AndroidJavaObject>("getType");
            var iMentionedType = mentionedTypeObject.Call<int>("getValue");
            var mentionedUserIdListObject = mentionedInfoObject.Call<AndroidJavaObject>("getMentionedUserIdList");
            var mentionedUserIdList = ToStringList(mentionedUserIdListObject);

            var mentionedContent = mentionedInfoObject.Call<string>("getMentionedContent");

            var mentionedInfo =
                new RCMentionedInfo((MentionedType) iMentionedType, mentionedUserIdList, mentionedContent);
            msgContent.MentionedInfo = mentionedInfo;
        }
    }
}