#if UNITY_ANDROID
using System.Collections.Generic;
using System;
using UnityEngine;

namespace cn_rongcloud_im_unity
{
    public class RCIMAndroidEngine : RCIMEngine
    {
        private static AndroidJavaClass EngineClass = new AndroidJavaClass("cn.rongcloud.im.wrapper.RCIMIWEngine");
        private static AndroidJavaObject Engine = null;
    
        public static void EnablePush(bool enable)
        {
            EngineClass.CallStatic("enablePush", enable);
        }
    
        public static void DisableIPC(bool disable)
        {
            EngineClass.CallStatic("disableIPC", disable);
        }
    
        public RCIMAndroidEngine(string appKey, RCIMEngineOptions options = null)
        {
            AndroidJavaClass player = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject context = player.GetStatic<AndroidJavaObject>("currentActivity");
    
            AndroidJavaObject _options = null;
            if (options != null)
                _options = EngineOptionsConverter.from(options).getAndroidObject();
            Engine = EngineClass.CallStatic<AndroidJavaObject>("create", context, appKey, _options);
            Engine.Call("setListener", new RCIMListenerProxy(this));
        }
    
        public override void Destroy()
        {
            if (Engine != null)
            {
                Engine.Call("destroy");
                Engine = null;
            }
            base.Destroy();
        }
    
        public override int Connect(string token, int timeout, RCIMConnectListener callback = null)
        {
            RCUnityLogger.getInstance().log("Connect", $"token={token},timeout={timeout},callback={callback}");
            AndroidJavaObject _callback = null;
            if (callback != null)
            {
                _callback = new AndroidJavaObject("cn.rongcloud.im.wrapper.unity.RCIMConnectCallbackProxy",
                                                  new RCIMConnectCallbackProxy(callback));
            }
            if (callback == null)
            {
                int ret = Engine.Call<int>("connect", token, timeout);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("connect", token, timeout, _callback);
                return ret;
            }
        }
    
        public override int Disconnect(bool receivePush)
        {
            RCUnityLogger.getInstance().log("Disconnect", $"receivePush={receivePush}");
            int ret = Engine.Call<int>("disconnect", receivePush);
            return ret;
        }
    
        public override RCIMTextMessage CreateTextMessage(RCIMConversationType type, string targetId, string channelId,
                                                          string text)
        {
            RCUnityLogger.getInstance().log("CreateTextMessage",
                                            $"type={type},targetId={targetId},channelId={channelId},text={text}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            AndroidJavaObject ret = Engine.Call<AndroidJavaObject>("createTextMessage", _type, targetId, channelId, text);
            if (ret == null)
                return null;
            else
                return (RCIMTextMessage)TextMessageConverter.from(ret).getCSharpObject();
        }
    
        public override RCIMImageMessage CreateImageMessage(RCIMConversationType type, string targetId, string channelId,
                                                            string path)
        {
            RCUnityLogger.getInstance().log("CreateImageMessage",
                                            $"type={type},targetId={targetId},channelId={channelId},path={path}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            AndroidJavaObject ret = Engine.Call<AndroidJavaObject>("createImageMessage", _type, targetId, channelId, path);
            if (ret == null)
                return null;
            else
                return (RCIMImageMessage)ImageMessageConverter.from(ret).getCSharpObject();
        }
    
        public override RCIMFileMessage CreateFileMessage(RCIMConversationType type, string targetId, string channelId,
                                                          string path)
        {
            RCUnityLogger.getInstance().log("CreateFileMessage",
                                            $"type={type},targetId={targetId},channelId={channelId},path={path}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            AndroidJavaObject ret = Engine.Call<AndroidJavaObject>("createFileMessage", _type, targetId, channelId, path);
            if (ret == null)
                return null;
            else
                return (RCIMFileMessage)FileMessageConverter.from(ret).getCSharpObject();
        }
    
        public override RCIMSightMessage CreateSightMessage(RCIMConversationType type, string targetId, string channelId,
                                                            string path, int duration)
        {
            RCUnityLogger.getInstance().log(
                "CreateSightMessage",
                $"type={type},targetId={targetId},channelId={channelId},path={path},duration={duration}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            AndroidJavaObject ret =
                Engine.Call<AndroidJavaObject>("createSightMessage", _type, targetId, channelId, path, duration);
            if (ret == null)
                return null;
            else
                return (RCIMSightMessage)SightMessageConverter.from(ret).getCSharpObject();
        }
    
        public override RCIMVoiceMessage CreateVoiceMessage(RCIMConversationType type, string targetId, string channelId,
                                                            string path, int duration)
        {
            RCUnityLogger.getInstance().log(
                "CreateVoiceMessage",
                $"type={type},targetId={targetId},channelId={channelId},path={path},duration={duration}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            AndroidJavaObject ret =
                Engine.Call<AndroidJavaObject>("createVoiceMessage", _type, targetId, channelId, path, duration);
            if (ret == null)
                return null;
            else
                return (RCIMVoiceMessage)VoiceMessageConverter.from(ret).getCSharpObject();
        }
    
        public override RCIMReferenceMessage CreateReferenceMessage(RCIMConversationType type, string targetId,
                                                                    string channelId, RCIMMessage referenceMessage,
                                                                    string text)
        {
            RCUnityLogger.getInstance().log(
                "CreateReferenceMessage",
                $"type={type},targetId={targetId},channelId={channelId},referenceMessage={referenceMessage},text={text}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            AndroidJavaObject _referenceMessage = MessageConverter.from(referenceMessage).getAndroidObject();
            AndroidJavaObject ret = Engine.Call<AndroidJavaObject>("createReferenceMessage", _type, targetId, channelId,
                                                                   _referenceMessage, text);
            if (ret == null)
                return null;
            else
                return (RCIMReferenceMessage)ReferenceMessageConverter.from(ret).getCSharpObject();
        }
    
        public override RCIMGIFMessage CreateGIFMessage(RCIMConversationType type, string targetId, string channelId,
                                                        string path)
        {
            RCUnityLogger.getInstance().log("CreateGIFMessage",
                                            $"type={type},targetId={targetId},channelId={channelId},path={path}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            AndroidJavaObject ret = Engine.Call<AndroidJavaObject>("createGIFMessage", _type, targetId, channelId, path);
            if (ret == null)
                return null;
            else
                return (RCIMGIFMessage)GIFMessageConverter.from(ret).getCSharpObject();
        }
    
        public override RCIMCustomMessage CreateCustomMessage(RCIMConversationType type, string targetId, string channelId,
                                                              RCIMCustomMessagePolicy policy, string messageIdentifier,
                                                              Dictionary<string, string> fields)
        {
            RCUnityLogger.getInstance().log(
                "CreateCustomMessage",
                $"type={type},targetId={targetId},channelId={channelId},policy={policy},messageIdentifier={messageIdentifier},fields={fields}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            AndroidJavaObject _policy = new CustomMessagePolicyConverter(policy).getAndroidObject();
            AndroidJavaObject _fields = new AndroidJavaObject("java.util.HashMap");
            if (fields != null)
                foreach (var item in fields)
                    _fields.Call<string>("put", item.Key, item.Value);
            AndroidJavaObject ret = Engine.Call<AndroidJavaObject>("createCustomMessage", _type, targetId, channelId,
                                                                   _policy, messageIdentifier, _fields);
            if (ret == null)
                return null;
            else
                return (RCIMCustomMessage)CustomMessageConverter.from(ret).getCSharpObject();
        }
    
        public override RCIMLocationMessage CreateLocationMessage(RCIMConversationType type, string targetId,
                                                                  string channelId, double longitude, double latitude,
                                                                  string poiName, string thumbnailPath)
        {
            RCUnityLogger.getInstance().log(
                "CreateLocationMessage",
                $"type={type},targetId={targetId},channelId={channelId},longitude={longitude},latitude={latitude},poiName={poiName},thumbnailPath={thumbnailPath}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            AndroidJavaObject ret = Engine.Call<AndroidJavaObject>("createLocationMessage", _type, targetId, channelId,
                                                                   longitude, latitude, poiName, thumbnailPath);
            if (ret == null)
                return null;
            else
                return (RCIMLocationMessage)LocationMessageConverter.from(ret).getCSharpObject();
        }
    
        public override int SendMessage(RCIMMessage message, RCIMSendMessageListener callback = null)
        {
            RCUnityLogger.getInstance().log("SendMessage", $"message={message},callback={callback}");
            AndroidJavaObject _message = MessageConverter.from(message).getAndroidObject();
            AndroidJavaObject _callback = null;
            if (callback != null)
            {
                _callback = new AndroidJavaObject("cn.rongcloud.im.wrapper.unity.RCIMSendMessageCallbackProxy",
                                                  new RCIMSendMessageCallbackProxy(callback));
            }
            if (callback == null)
            {
                int ret = Engine.Call<int>("sendMessage", _message);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("sendMessage", _message, _callback);
                return ret;
            }
        }
    
        public override int SendMediaMessage(RCIMMediaMessage message, RCIMSendMediaMessageListener listener = null)
        {
            RCUnityLogger.getInstance().log("SendMediaMessage", $"message={message},listener={listener}");
            AndroidJavaObject _message = MediaMessageConverter.from(message).getAndroidObject();
            AndroidJavaObject _listener = null;
            if (listener != null)
            {
                _listener = new AndroidJavaObject("cn.rongcloud.im.wrapper.unity.RCIMSendMediaMessageListenerProxy",
                                                  new RCIMSendMediaMessageListenerProxy(listener));
            }
            if (listener == null)
            {
                int ret = Engine.Call<int>("sendMediaMessage", _message);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("sendMediaMessage", _message, _listener);
                return ret;
            }
        }
    
        public override int CancelSendingMediaMessage(RCIMMediaMessage message,
                                                      OnCancelSendingMediaMessageCalledAction callback = null)
        {
            RCUnityLogger.getInstance().log("CancelSendingMediaMessage", $"message={message},callback={callback}");
            AndroidJavaObject _message = MediaMessageConverter.from(message).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("cancelSendingMediaMessage", _message);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("cancelSendingMediaMessage", _message,
                                           new RCIMCancelSendingMediaMessageCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int DownloadMediaMessage(RCIMMediaMessage message, RCIMDownloadMediaMessageListener listener = null)
        {
            RCUnityLogger.getInstance().log("DownloadMediaMessage", $"message={message},listener={listener}");
            AndroidJavaObject _message = MediaMessageConverter.from(message).getAndroidObject();
            AndroidJavaObject _listener = null;
            if (listener != null)
            {
                _listener = new AndroidJavaObject("cn.rongcloud.im.wrapper.unity.RCIMDownloadMediaMessageListenerProxy",
                                                  new RCIMDownloadMediaMessageListenerProxy(listener));
            }
            if (listener == null)
            {
                int ret = Engine.Call<int>("downloadMediaMessage", _message);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("downloadMediaMessage", _message, _listener);
                return ret;
            }
        }
    
        public override int CancelDownloadingMediaMessage(RCIMMediaMessage message,
                                                          OnCancelDownloadingMediaMessageCalledAction callback = null)
        {
            RCUnityLogger.getInstance().log("CancelDownloadingMediaMessage", $"message={message},callback={callback}");
            AndroidJavaObject _message = MediaMessageConverter.from(message).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("cancelDownloadingMediaMessage", _message);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("cancelDownloadingMediaMessage", _message,
                                           new RCIMCancelDownloadingMediaMessageCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int LoadConversation(RCIMConversationType type, string targetId, string channelId)
        {
            RCUnityLogger.getInstance().log("LoadConversation", $"type={type},targetId={targetId},channelId={channelId}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            int ret = Engine.Call<int>("loadConversation", _type, targetId, channelId);
            return ret;
        }
    
        public override int GetConversation(RCIMConversationType type, string targetId, string channelId,
                                            RCIMGetConversationListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetConversation",
                                            $"type={type},targetId={targetId},channelId={channelId},callback={callback}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("getConversation", _type, targetId, channelId);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("getConversation", _type, targetId, channelId,
                                           new RCIMGetConversationCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int LoadConversations(List<RCIMConversationType> conversationTypes, string channelId,
                                              long startTime, int count)
        {
            RCUnityLogger.getInstance().log(
                "LoadConversations",
                $"conversationTypes={conversationTypes},channelId={channelId},startTime={startTime},count={count}");
            AndroidJavaObject _conversationTypes = new AndroidJavaObject("java.util.ArrayList");
            if (conversationTypes != null)
                foreach (RCIMConversationType item in conversationTypes)
                    _conversationTypes.Call<bool>("add", new ConversationTypeConverter(item).getAndroidObject());
            int ret = Engine.Call<int>("loadConversations", _conversationTypes, channelId, startTime, count);
            return ret;
        }
    
        public override int GetConversations(List<RCIMConversationType> conversationTypes, string channelId, long startTime,
                                             int count, RCIMGetConversationsListener callback = null)
        {
            RCUnityLogger.getInstance().log(
                "GetConversations",
                $"conversationTypes={conversationTypes},channelId={channelId},startTime={startTime},count={count},callback={callback}");
            AndroidJavaObject _conversationTypes = new AndroidJavaObject("java.util.ArrayList");
            if (conversationTypes != null)
                foreach (RCIMConversationType item in conversationTypes)
                    _conversationTypes.Call<bool>("add", new ConversationTypeConverter(item).getAndroidObject());
            if (callback == null)
            {
                int ret = Engine.Call<int>("getConversations", _conversationTypes, channelId, startTime, count);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("getConversations", _conversationTypes, channelId, startTime, count,
                                           new RCIMGetConversationsCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int RemoveConversation(RCIMConversationType type, string targetId, string channelId,
                                               OnConversationRemovedAction callback = null)
        {
            RCUnityLogger.getInstance().log("RemoveConversation",
                                            $"type={type},targetId={targetId},channelId={channelId},callback={callback}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("removeConversation", _type, targetId, channelId);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("removeConversation", _type, targetId, channelId,
                                           new RCIMRemoveConversationCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int RemoveConversations(List<RCIMConversationType> conversationTypes, string channelId,
                                                OnConversationsRemovedAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "RemoveConversations", $"conversationTypes={conversationTypes},channelId={channelId},callback={callback}");
            AndroidJavaObject _conversationTypes = new AndroidJavaObject("java.util.ArrayList");
            if (conversationTypes != null)
                foreach (RCIMConversationType item in conversationTypes)
                    _conversationTypes.Call<bool>("add", new ConversationTypeConverter(item).getAndroidObject());
            if (callback == null)
            {
                int ret = Engine.Call<int>("removeConversations", _conversationTypes, channelId);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("removeConversations", _conversationTypes, channelId,
                                           new RCIMRemoveConversationsCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int LoadUnreadCount(RCIMConversationType type, string targetId, string channelId)
        {
            RCUnityLogger.getInstance().log("LoadUnreadCount", $"type={type},targetId={targetId},channelId={channelId}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            int ret = Engine.Call<int>("loadUnreadCount", _type, targetId, channelId);
            return ret;
        }
    
        public override int GetUnreadCount(RCIMConversationType type, string targetId, string channelId,
                                           RCIMGetUnreadCountListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetUnreadCount",
                                            $"type={type},targetId={targetId},channelId={channelId},callback={callback}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("getUnreadCount", _type, targetId, channelId);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("getUnreadCount", _type, targetId, channelId,
                                           new RCIMGetUnreadCountCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int LoadTotalUnreadCount(string channelId)
        {
            RCUnityLogger.getInstance().log("LoadTotalUnreadCount", $"channelId={channelId}");
            int ret = Engine.Call<int>("loadTotalUnreadCount", channelId);
            return ret;
        }
    
        public override int GetTotalUnreadCount(string channelId, RCIMGetTotalUnreadCountListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetTotalUnreadCount", $"channelId={channelId},callback={callback}");
            if (callback == null)
            {
                int ret = Engine.Call<int>("getTotalUnreadCount", channelId);
                return ret;
            }
            else
            {
                int ret =
                    Engine.Call<int>("getTotalUnreadCount", channelId, new RCIMGetTotalUnreadCountCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int LoadUnreadMentionedCount(RCIMConversationType type, string targetId, string channelId)
        {
            RCUnityLogger.getInstance().log("LoadUnreadMentionedCount",
                                            $"type={type},targetId={targetId},channelId={channelId}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            int ret = Engine.Call<int>("loadUnreadMentionedCount", _type, targetId, channelId);
            return ret;
        }
    
        public override int GetUnreadMentionedCount(RCIMConversationType type, string targetId, string channelId,
                                                    RCIMGetUnreadMentionedCountListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetUnreadMentionedCount",
                                            $"type={type},targetId={targetId},channelId={channelId},callback={callback}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("getUnreadMentionedCount", _type, targetId, channelId);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("getUnreadMentionedCount", _type, targetId, channelId,
                                           new RCIMGetUnreadMentionedCountCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int LoadUltraGroupAllUnreadCount()
        {
            RCUnityLogger.getInstance().log("LoadUltraGroupAllUnreadCount", $"");
            int ret = Engine.Call<int>("loadUltraGroupAllUnreadCount");
            return ret;
        }
    
        public override int GetUltraGroupAllUnreadCount(RCIMGetUltraGroupAllUnreadCountListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetUltraGroupAllUnreadCount", $"callback={callback}");
            if (callback == null)
            {
                int ret = Engine.Call<int>("getUltraGroupAllUnreadCount");
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("getUltraGroupAllUnreadCount",
                                           new RCIMGetUltraGroupAllUnreadCountCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int LoadUltraGroupAllUnreadMentionedCount()
        {
            RCUnityLogger.getInstance().log("LoadUltraGroupAllUnreadMentionedCount", $"");
            int ret = Engine.Call<int>("loadUltraGroupAllUnreadMentionedCount");
            return ret;
        }
    
        public override int GetUltraGroupAllUnreadMentionedCount(
            RCIMGetUltraGroupAllUnreadMentionedCountListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetUltraGroupAllUnreadMentionedCount", $"callback={callback}");
            if (callback == null)
            {
                int ret = Engine.Call<int>("getUltraGroupAllUnreadMentionedCount");
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("getUltraGroupAllUnreadMentionedCount",
                                           new RCIMGetUltraGroupAllUnreadMentionedCountCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int LoadUltraGroupUnreadCount(string targetId)
        {
            RCUnityLogger.getInstance().log("LoadUltraGroupUnreadCount", $"targetId={targetId}");
            int ret = Engine.Call<int>("loadUltraGroupUnreadCount", targetId);
            return ret;
        }
    
        public override int GetUltraGroupUnreadCount(string targetId, RCIMGetUltraGroupUnreadCountListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetUltraGroupUnreadCount", $"targetId={targetId},callback={callback}");
            if (callback == null)
            {
                int ret = Engine.Call<int>("getUltraGroupUnreadCount", targetId);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("getUltraGroupUnreadCount", targetId,
                                           new RCIMGetUltraGroupUnreadCountCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int LoadUltraGroupUnreadMentionedCount(string targetId)
        {
            RCUnityLogger.getInstance().log("LoadUltraGroupUnreadMentionedCount", $"targetId={targetId}");
            int ret = Engine.Call<int>("loadUltraGroupUnreadMentionedCount", targetId);
            return ret;
        }
    
        public override int GetUltraGroupUnreadMentionedCount(string targetId,
                                                              RCIMGetUltraGroupUnreadMentionedCountListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetUltraGroupUnreadMentionedCount",
                                            $"targetId={targetId},callback={callback}");
            if (callback == null)
            {
                int ret = Engine.Call<int>("getUltraGroupUnreadMentionedCount", targetId);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("getUltraGroupUnreadMentionedCount", targetId,
                                           new RCIMGetUltraGroupUnreadMentionedCountCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int LoadUnreadCountByConversationTypes(List<RCIMConversationType> conversationTypes,
                                                               string channelId, bool contain)
        {
            RCUnityLogger.getInstance().log(
                "LoadUnreadCountByConversationTypes",
                $"conversationTypes={conversationTypes},channelId={channelId},contain={contain}");
            AndroidJavaObject _conversationTypes = new AndroidJavaObject("java.util.ArrayList");
            if (conversationTypes != null)
                foreach (RCIMConversationType item in conversationTypes)
                    _conversationTypes.Call<bool>("add", new ConversationTypeConverter(item).getAndroidObject());
            int ret = Engine.Call<int>("loadUnreadCountByConversationTypes", _conversationTypes, channelId, contain);
            return ret;
        }
    
        public override int GetUnreadCountByConversationTypes(List<RCIMConversationType> conversationTypes,
                                                              string channelId, bool contain,
                                                              RCIMGetUnreadCountByConversationTypesListener callback = null)
        {
            RCUnityLogger.getInstance().log(
                "GetUnreadCountByConversationTypes",
                $"conversationTypes={conversationTypes},channelId={channelId},contain={contain},callback={callback}");
            AndroidJavaObject _conversationTypes = new AndroidJavaObject("java.util.ArrayList");
            if (conversationTypes != null)
                foreach (RCIMConversationType item in conversationTypes)
                    _conversationTypes.Call<bool>("add", new ConversationTypeConverter(item).getAndroidObject());
            if (callback == null)
            {
                int ret = Engine.Call<int>("getUnreadCountByConversationTypes", _conversationTypes, channelId, contain);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("getUnreadCountByConversationTypes", _conversationTypes, channelId, contain,
                                           new RCIMGetUnreadCountByConversationTypesCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int ClearUnreadCount(RCIMConversationType type, string targetId, string channelId, long timestamp,
                                             OnUnreadCountClearedAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "ClearUnreadCount",
                $"type={type},targetId={targetId},channelId={channelId},timestamp={timestamp},callback={callback}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("clearUnreadCount", _type, targetId, channelId, timestamp);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("clearUnreadCount", _type, targetId, channelId, timestamp,
                                           new RCIMClearUnreadCountCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int SaveDraftMessage(RCIMConversationType type, string targetId, string channelId, string draft,
                                             OnDraftMessageSavedAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "SaveDraftMessage",
                $"type={type},targetId={targetId},channelId={channelId},draft={draft},callback={callback}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("saveDraftMessage", _type, targetId, channelId, draft);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("saveDraftMessage", _type, targetId, channelId, draft,
                                           new RCIMSaveDraftMessageCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int LoadDraftMessage(RCIMConversationType type, string targetId, string channelId)
        {
            RCUnityLogger.getInstance().log("LoadDraftMessage", $"type={type},targetId={targetId},channelId={channelId}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            int ret = Engine.Call<int>("loadDraftMessage", _type, targetId, channelId);
            return ret;
        }
    
        public override int GetDraftMessage(RCIMConversationType type, string targetId, string channelId,
                                            RCIMGetDraftMessageListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetDraftMessage",
                                            $"type={type},targetId={targetId},channelId={channelId},callback={callback}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("getDraftMessage", _type, targetId, channelId);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("getDraftMessage", _type, targetId, channelId,
                                           new RCIMGetDraftMessageCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int ClearDraftMessage(RCIMConversationType type, string targetId, string channelId,
                                              OnDraftMessageClearedAction callback = null)
        {
            RCUnityLogger.getInstance().log("ClearDraftMessage",
                                            $"type={type},targetId={targetId},channelId={channelId},callback={callback}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("clearDraftMessage", _type, targetId, channelId);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("clearDraftMessage", _type, targetId, channelId,
                                           new RCIMClearDraftMessageCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int LoadBlockedConversations(List<RCIMConversationType> conversationTypes, string channelId)
        {
            RCUnityLogger.getInstance().log("LoadBlockedConversations",
                                            $"conversationTypes={conversationTypes},channelId={channelId}");
            AndroidJavaObject _conversationTypes = new AndroidJavaObject("java.util.ArrayList");
            if (conversationTypes != null)
                foreach (RCIMConversationType item in conversationTypes)
                    _conversationTypes.Call<bool>("add", new ConversationTypeConverter(item).getAndroidObject());
            int ret = Engine.Call<int>("loadBlockedConversations", _conversationTypes, channelId);
            return ret;
        }
    
        public override int GetBlockedConversations(List<RCIMConversationType> conversationTypes, string channelId,
                                                    RCIMGetBlockedConversationsListener callback = null)
        {
            RCUnityLogger.getInstance().log(
                "GetBlockedConversations",
                $"conversationTypes={conversationTypes},channelId={channelId},callback={callback}");
            AndroidJavaObject _conversationTypes = new AndroidJavaObject("java.util.ArrayList");
            if (conversationTypes != null)
                foreach (RCIMConversationType item in conversationTypes)
                    _conversationTypes.Call<bool>("add", new ConversationTypeConverter(item).getAndroidObject());
            if (callback == null)
            {
                int ret = Engine.Call<int>("getBlockedConversations", _conversationTypes, channelId);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("getBlockedConversations", _conversationTypes, channelId,
                                           new RCIMGetBlockedConversationsCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int ChangeConversationTopStatus(RCIMConversationType type, string targetId, string channelId,
                                                        bool top, OnConversationTopStatusChangedAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "ChangeConversationTopStatus",
                $"type={type},targetId={targetId},channelId={channelId},top={top},callback={callback}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("changeConversationTopStatus", _type, targetId, channelId, top);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("changeConversationTopStatus", _type, targetId, channelId, top,
                                           new RCIMChangeConversationTopStatusCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int LoadConversationTopStatus(RCIMConversationType type, string targetId, string channelId)
        {
            RCUnityLogger.getInstance().log("LoadConversationTopStatus",
                                            $"type={type},targetId={targetId},channelId={channelId}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            int ret = Engine.Call<int>("loadConversationTopStatus", _type, targetId, channelId);
            return ret;
        }
    
        public override int GetConversationTopStatus(RCIMConversationType type, string targetId, string channelId,
                                                     RCIMGetConversationTopStatusListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetConversationTopStatus",
                                            $"type={type},targetId={targetId},channelId={channelId},callback={callback}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("getConversationTopStatus", _type, targetId, channelId);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("getConversationTopStatus", _type, targetId, channelId,
                                           new RCIMGetConversationTopStatusCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int SyncConversationReadStatus(RCIMConversationType type, string targetId, string channelId,
                                                       long timestamp, OnConversationReadStatusSyncedAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "SyncConversationReadStatus",
                $"type={type},targetId={targetId},channelId={channelId},timestamp={timestamp},callback={callback}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("syncConversationReadStatus", _type, targetId, channelId, timestamp);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("syncConversationReadStatus", _type, targetId, channelId, timestamp,
                                           new RCIMSyncConversationReadStatusCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int SendTypingStatus(RCIMConversationType type, string targetId, string channelId,
                                             string currentType)
        {
            RCUnityLogger.getInstance().log(
                "SendTypingStatus", $"type={type},targetId={targetId},channelId={channelId},currentType={currentType}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            int ret = Engine.Call<int>("sendTypingStatus", _type, targetId, channelId, currentType);
            return ret;
        }
    
        public override int LoadMessages(RCIMConversationType type, string targetId, string channelId, long sentTime,
                                         RCIMTimeOrder order, RCIMMessageOperationPolicy policy, int count)
        {
            RCUnityLogger.getInstance().log(
                "LoadMessages",
                $"type={type},targetId={targetId},channelId={channelId},sentTime={sentTime},order={order},policy={policy},count={count}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            AndroidJavaObject _order = new TimeOrderConverter(order).getAndroidObject();
            AndroidJavaObject _policy = new MessageOperationPolicyConverter(policy).getAndroidObject();
            int ret = Engine.Call<int>("loadMessages", _type, targetId, channelId, sentTime, _order, _policy, count);
            return ret;
        }
    
        public override int GetMessages(RCIMConversationType type, string targetId, string channelId, long sentTime,
                                        RCIMTimeOrder order, RCIMMessageOperationPolicy policy, int count,
                                        RCIMGetMessagesListener callback = null)
        {
            RCUnityLogger.getInstance().log(
                "GetMessages",
                $"type={type},targetId={targetId},channelId={channelId},sentTime={sentTime},order={order},policy={policy},count={count},callback={callback}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            AndroidJavaObject _order = new TimeOrderConverter(order).getAndroidObject();
            AndroidJavaObject _policy = new MessageOperationPolicyConverter(policy).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("getMessages", _type, targetId, channelId, sentTime, _order, _policy, count);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("getMessages", _type, targetId, channelId, sentTime, _order, _policy, count,
                                           new RCIMGetMessagesCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int GetMessageById(int messageId, RCIMGetMessageListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetMessageById", $"messageId={messageId},callback={callback}");
            if (callback == null)
            {
                int ret = Engine.Call<int>("getMessageById", messageId);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("getMessageById", messageId, new RCIMGetMessageCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int GetMessageByUId(string messageUId, RCIMGetMessageListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetMessageByUId", $"messageUId={messageUId},callback={callback}");
            if (callback == null)
            {
                int ret = Engine.Call<int>("getMessageByUId", messageUId);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("getMessageByUId", messageUId, new RCIMGetMessageCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int LoadFirstUnreadMessage(RCIMConversationType type, string targetId, string channelId)
        {
            RCUnityLogger.getInstance().log("LoadFirstUnreadMessage",
                                            $"type={type},targetId={targetId},channelId={channelId}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            int ret = Engine.Call<int>("loadFirstUnreadMessage", _type, targetId, channelId);
            return ret;
        }
    
        public override int GetFirstUnreadMessage(RCIMConversationType type, string targetId, string channelId,
                                                  RCIMGetFirstUnreadMessageListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetFirstUnreadMessage",
                                            $"type={type},targetId={targetId},channelId={channelId},callback={callback}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("getFirstUnreadMessage", _type, targetId, channelId);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("getFirstUnreadMessage", _type, targetId, channelId,
                                           new RCIMGetFirstUnreadMessageCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int LoadUnreadMentionedMessages(RCIMConversationType type, string targetId, string channelId)
        {
            RCUnityLogger.getInstance().log("LoadUnreadMentionedMessages",
                                            $"type={type},targetId={targetId},channelId={channelId}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            int ret = Engine.Call<int>("loadUnreadMentionedMessages", _type, targetId, channelId);
            return ret;
        }
    
        public override int GetUnreadMentionedMessages(RCIMConversationType type, string targetId, string channelId,
                                                       RCIMGetUnreadMentionedMessagesListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetUnreadMentionedMessages",
                                            $"type={type},targetId={targetId},channelId={channelId},callback={callback}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("getUnreadMentionedMessages", _type, targetId, channelId);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("getUnreadMentionedMessages", _type, targetId, channelId,
                                           new RCIMGetUnreadMentionedMessagesCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int InsertMessage(RCIMMessage message, OnMessageInsertedAction callback = null)
        {
            RCUnityLogger.getInstance().log("InsertMessage", $"message={message},callback={callback}");
            AndroidJavaObject _message = MessageConverter.from(message).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("insertMessage", _message);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("insertMessage", _message, new RCIMInsertMessageCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int InsertMessages(List<RCIMMessage> messages, OnMessagesInsertedAction callback = null)
        {
            RCUnityLogger.getInstance().log("InsertMessages", $"messages={messages},callback={callback}");
            AndroidJavaObject _messages = new AndroidJavaObject("java.util.ArrayList");
            if (messages != null)
                foreach (RCIMMessage item in messages)
                    if (item != null)
                        _messages.Call<bool>("add", MessageConverter.from(item).getAndroidObject());
            if (callback == null)
            {
                int ret = Engine.Call<int>("insertMessages", _messages);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("insertMessages", _messages, new RCIMInsertMessagesCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int ClearMessages(RCIMConversationType type, string targetId, string channelId, long timestamp,
                                          RCIMMessageOperationPolicy policy, OnMessagesClearedAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "ClearMessages",
                $"type={type},targetId={targetId},channelId={channelId},timestamp={timestamp},policy={policy},callback={callback}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            AndroidJavaObject _policy = new MessageOperationPolicyConverter(policy).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("clearMessages", _type, targetId, channelId, timestamp, _policy);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("clearMessages", _type, targetId, channelId, timestamp, _policy,
                                           new RCIMClearMessagesCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int DeleteLocalMessages(List<RCIMMessage> messages, OnLocalMessagesDeletedAction callback = null)
        {
            RCUnityLogger.getInstance().log("DeleteLocalMessages", $"messages={messages},callback={callback}");
            AndroidJavaObject _messages = new AndroidJavaObject("java.util.ArrayList");
            if (messages != null)
                foreach (RCIMMessage item in messages)
                    if (item != null)
                        _messages.Call<bool>("add", MessageConverter.from(item).getAndroidObject());
            if (callback == null)
            {
                int ret = Engine.Call<int>("deleteLocalMessages", _messages);
                return ret;
            }
            else
            {
                int ret =
                    Engine.Call<int>("deleteLocalMessages", _messages, new RCIMDeleteLocalMessagesCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int DeleteMessages(RCIMConversationType type, string targetId, string channelId,
                                           List<RCIMMessage> messages, OnMessagesDeletedAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "DeleteMessages",
                $"type={type},targetId={targetId},channelId={channelId},messages={messages},callback={callback}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            AndroidJavaObject _messages = new AndroidJavaObject("java.util.ArrayList");
            if (messages != null)
                foreach (RCIMMessage item in messages)
                    if (item != null)
                        _messages.Call<bool>("add", MessageConverter.from(item).getAndroidObject());
            if (callback == null)
            {
                int ret = Engine.Call<int>("deleteMessages", _type, targetId, channelId, _messages);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("deleteMessages", _type, targetId, channelId, _messages,
                                           new RCIMDeleteMessagesCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int RecallMessage(RCIMMessage message, OnMessageRecalledAction callback = null)
        {
            RCUnityLogger.getInstance().log("RecallMessage", $"message={message},callback={callback}");
            AndroidJavaObject _message = MessageConverter.from(message).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("recallMessage", _message);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("recallMessage", _message, new RCIMRecallMessageCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int SendPrivateReadReceiptMessage(string targetId, string channelId, long timestamp,
                                                          OnPrivateReadReceiptMessageSentAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "SendPrivateReadReceiptMessage",
                $"targetId={targetId},channelId={channelId},timestamp={timestamp},callback={callback}");
            if (callback == null)
            {
                int ret = Engine.Call<int>("sendPrivateReadReceiptMessage", targetId, channelId, timestamp);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("sendPrivateReadReceiptMessage", targetId, channelId, timestamp,
                                           new RCIMSendPrivateReadReceiptMessageCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int SendGroupReadReceiptRequest(RCIMMessage message,
                                                        OnGroupReadReceiptRequestSentAction callback = null)
        {
            RCUnityLogger.getInstance().log("SendGroupReadReceiptRequest", $"message={message},callback={callback}");
            AndroidJavaObject _message = MessageConverter.from(message).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("sendGroupReadReceiptRequest", _message);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("sendGroupReadReceiptRequest", _message,
                                           new RCIMSendGroupReadReceiptRequestCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int SendGroupReadReceiptResponse(string targetId, string channelId, List<RCIMMessage> messages,
                                                         OnGroupReadReceiptResponseSentAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "SendGroupReadReceiptResponse",
                $"targetId={targetId},channelId={channelId},messages={messages},callback={callback}");
            AndroidJavaObject _messages = new AndroidJavaObject("java.util.ArrayList");
            if (messages != null)
                foreach (RCIMMessage item in messages)
                    if (item != null)
                        _messages.Call<bool>("add", MessageConverter.from(item).getAndroidObject());
            if (callback == null)
            {
                int ret = Engine.Call<int>("sendGroupReadReceiptResponse", targetId, channelId, _messages);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("sendGroupReadReceiptResponse", targetId, channelId, _messages,
                                           new RCIMSendGroupReadReceiptResponseCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int UpdateMessageExpansion(string messageUId, Dictionary<string, string> expansion,
                                                   OnMessageExpansionUpdatedAction callback = null)
        {
            RCUnityLogger.getInstance().log("UpdateMessageExpansion",
                                            $"messageUId={messageUId},expansion={expansion},callback={callback}");
            AndroidJavaObject _expansion = new AndroidJavaObject("java.util.HashMap");
            if (expansion != null)
                foreach (var item in expansion)
                    _expansion.Call<string>("put", item.Key, item.Value);
            if (callback == null)
            {
                int ret = Engine.Call<int>("updateMessageExpansion", messageUId, _expansion);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("updateMessageExpansion", messageUId, _expansion,
                                           new RCIMUpdateMessageExpansionCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int RemoveMessageExpansionForKeys(string messageUId, List<string> keys,
                                                          OnMessageExpansionForKeysRemovedAction callback = null)
        {
            RCUnityLogger.getInstance().log("RemoveMessageExpansionForKeys",
                                            $"messageUId={messageUId},keys={keys},callback={callback}");
            AndroidJavaObject _keys = new AndroidJavaObject("java.util.ArrayList");
            if (keys != null)
                foreach (string item in keys)
                    _keys.Call<bool>("add", item);
            if (callback == null)
            {
                int ret = Engine.Call<int>("removeMessageExpansionForKeys", messageUId, _keys);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("removeMessageExpansionForKeys", messageUId, _keys,
                                           new RCIMRemoveMessageExpansionForKeysCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int ChangeMessageSentStatus(int messageId, RCIMSentStatus sentStatus,
                                                    OnMessageSentStatusChangedAction callback = null)
        {
            RCUnityLogger.getInstance().log("ChangeMessageSentStatus",
                                            $"messageId={messageId},sentStatus={sentStatus},callback={callback}");
            AndroidJavaObject _sentStatus = new SentStatusConverter(sentStatus).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("changeMessageSentStatus", messageId, _sentStatus);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("changeMessageSentStatus", messageId, _sentStatus,
                                           new RCIMChangeMessageSentStatusCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int ChangeMessageReceiveStatus(int messageId, RCIMReceivedStatus receivedStatus,
                                                       OnMessageReceiveStatusChangedAction callback = null)
        {
            RCUnityLogger.getInstance().log("ChangeMessageReceiveStatus",
                                            $"messageId={messageId},receivedStatus={receivedStatus},callback={callback}");
            AndroidJavaObject _receivedStatus = new ReceivedStatusConverter(receivedStatus).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("changeMessageReceiveStatus", messageId, _receivedStatus);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("changeMessageReceiveStatus", messageId, _receivedStatus,
                                           new RCIMChangeMessageReceivedStatusCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int JoinChatRoom(string targetId, int messageCount, bool autoCreate,
                                         OnChatRoomJoinedAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "JoinChatRoom",
                $"targetId={targetId},messageCount={messageCount},autoCreate={autoCreate},callback={callback}");
            if (callback == null)
            {
                int ret = Engine.Call<int>("joinChatRoom", targetId, messageCount, autoCreate);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("joinChatRoom", targetId, messageCount, autoCreate,
                                           new RCIMJoinChatRoomCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int LeaveChatRoom(string targetId, OnChatRoomLeftAction callback = null)
        {
            RCUnityLogger.getInstance().log("LeaveChatRoom", $"targetId={targetId},callback={callback}");
            if (callback == null)
            {
                int ret = Engine.Call<int>("leaveChatRoom", targetId);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("leaveChatRoom", targetId, new RCIMLeaveChatRoomCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int LoadChatRoomMessages(string targetId, long timestamp, RCIMTimeOrder order, int count)
        {
            RCUnityLogger.getInstance().log("LoadChatRoomMessages",
                                            $"targetId={targetId},timestamp={timestamp},order={order},count={count}");
            AndroidJavaObject _order = new TimeOrderConverter(order).getAndroidObject();
            int ret = Engine.Call<int>("loadChatRoomMessages", targetId, timestamp, _order, count);
            return ret;
        }
    
        public override int GetChatRoomMessages(string targetId, long timestamp, RCIMTimeOrder order, int count,
                                                RCIMGetChatRoomMessagesListener callback = null)
        {
            RCUnityLogger.getInstance().log(
                "GetChatRoomMessages",
                $"targetId={targetId},timestamp={timestamp},order={order},count={count},callback={callback}");
            AndroidJavaObject _order = new TimeOrderConverter(order).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("getChatRoomMessages", targetId, timestamp, _order, count);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("getChatRoomMessages", targetId, timestamp, _order, count,
                                           new RCIMGetChatRoomMessagesCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int AddChatRoomEntry(string targetId, string key, string value, bool deleteWhenLeft, bool overwrite,
                                             OnChatRoomEntryAddedAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "AddChatRoomEntry",
                $"targetId={targetId},key={key},value={value},deleteWhenLeft={deleteWhenLeft},overwrite={overwrite},callback={callback}");
            if (callback == null)
            {
                int ret = Engine.Call<int>("addChatRoomEntry", targetId, key, value, deleteWhenLeft, overwrite);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("addChatRoomEntry", targetId, key, value, deleteWhenLeft, overwrite,
                                           new RCIMAddChatRoomEntryCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int AddChatRoomEntries(string targetId, Dictionary<string, string> entries, bool deleteWhenLeft,
                                               bool overwrite, OnChatRoomEntriesAddedAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "AddChatRoomEntries",
                $"targetId={targetId},entries={entries},deleteWhenLeft={deleteWhenLeft},overwrite={overwrite},callback={callback}");
            AndroidJavaObject _entries = new AndroidJavaObject("java.util.HashMap");
            if (entries != null)
                foreach (var item in entries)
                    _entries.Call<string>("put", item.Key, item.Value);
            if (callback == null)
            {
                int ret = Engine.Call<int>("addChatRoomEntries", targetId, _entries, deleteWhenLeft, overwrite);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("addChatRoomEntries", targetId, _entries, deleteWhenLeft, overwrite,
                                           new RCIMAddChatRoomEntriesCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int LoadChatRoomEntry(string targetId, string key)
        {
            RCUnityLogger.getInstance().log("LoadChatRoomEntry", $"targetId={targetId},key={key}");
            int ret = Engine.Call<int>("loadChatRoomEntry", targetId, key);
            return ret;
        }
    
        public override int GetChatRoomEntry(string targetId, string key, RCIMGetChatRoomEntryListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetChatRoomEntry", $"targetId={targetId},key={key},callback={callback}");
            if (callback == null)
            {
                int ret = Engine.Call<int>("getChatRoomEntry", targetId, key);
                return ret;
            }
            else
            {
                int ret =
                    Engine.Call<int>("getChatRoomEntry", targetId, key, new RCIMGetChatRoomEntryCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int LoadChatRoomAllEntries(string targetId)
        {
            RCUnityLogger.getInstance().log("LoadChatRoomAllEntries", $"targetId={targetId}");
            int ret = Engine.Call<int>("loadChatRoomAllEntries", targetId);
            return ret;
        }
    
        public override int GetChatRoomAllEntries(string targetId, RCIMGetChatRoomAllEntriesListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetChatRoomAllEntries", $"targetId={targetId},callback={callback}");
            if (callback == null)
            {
                int ret = Engine.Call<int>("getChatRoomAllEntries", targetId);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("getChatRoomAllEntries", targetId,
                                           new RCIMGetChatRoomAllEntriesCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int RemoveChatRoomEntry(string targetId, string key, bool force,
                                                OnChatRoomEntryRemovedAction callback = null)
        {
            RCUnityLogger.getInstance().log("RemoveChatRoomEntry",
                                            $"targetId={targetId},key={key},force={force},callback={callback}");
            if (callback == null)
            {
                int ret = Engine.Call<int>("removeChatRoomEntry", targetId, key, force);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("removeChatRoomEntry", targetId, key, force,
                                           new RCIMRemoveChatRoomEntryCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int RemoveChatRoomEntries(string targetId, List<string> keys, bool force,
                                                  OnChatRoomEntriesRemovedAction callback = null)
        {
            RCUnityLogger.getInstance().log("RemoveChatRoomEntries",
                                            $"targetId={targetId},keys={keys},force={force},callback={callback}");
            AndroidJavaObject _keys = new AndroidJavaObject("java.util.ArrayList");
            if (keys != null)
                foreach (string item in keys)
                    _keys.Call<bool>("add", item);
            if (callback == null)
            {
                int ret = Engine.Call<int>("removeChatRoomEntries", targetId, _keys, force);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("removeChatRoomEntries", targetId, _keys, force,
                                           new RCIMRemoveChatRoomEntriesCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int AddToBlacklist(string userId, OnBlacklistAddedAction callback = null)
        {
            RCUnityLogger.getInstance().log("AddToBlacklist", $"userId={userId},callback={callback}");
            if (callback == null)
            {
                int ret = Engine.Call<int>("addToBlacklist", userId);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("addToBlacklist", userId, new RCIMAddToBlacklistCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int RemoveFromBlacklist(string userId, OnBlacklistRemovedAction callback = null)
        {
            RCUnityLogger.getInstance().log("RemoveFromBlacklist", $"userId={userId},callback={callback}");
            if (callback == null)
            {
                int ret = Engine.Call<int>("removeFromBlacklist", userId);
                return ret;
            }
            else
            {
                int ret =
                    Engine.Call<int>("removeFromBlacklist", userId, new RCIMRemoveFromBlacklistCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int LoadBlacklistStatus(string userId)
        {
            RCUnityLogger.getInstance().log("LoadBlacklistStatus", $"userId={userId}");
            int ret = Engine.Call<int>("loadBlacklistStatus", userId);
            return ret;
        }
    
        public override int GetBlacklistStatus(string userId, RCIMGetBlacklistStatusListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetBlacklistStatus", $"userId={userId},callback={callback}");
            if (callback == null)
            {
                int ret = Engine.Call<int>("getBlacklistStatus", userId);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("getBlacklistStatus", userId, new RCIMGetBlacklistStatusCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int LoadBlacklist()
        {
            RCUnityLogger.getInstance().log("LoadBlacklist", $"");
            int ret = Engine.Call<int>("loadBlacklist");
            return ret;
        }
    
        public override int GetBlacklist(RCIMGetBlacklistListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetBlacklist", $"callback={callback}");
            if (callback == null)
            {
                int ret = Engine.Call<int>("getBlacklist");
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("getBlacklist", new RCIMGetBlacklistCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int SearchMessages(RCIMConversationType type, string targetId, string channelId, string keyword,
                                           long startTime, int count, RCIMSearchMessagesListener callback = null)
        {
            RCUnityLogger.getInstance().log(
                "SearchMessages",
                $"type={type},targetId={targetId},channelId={channelId},keyword={keyword},startTime={startTime},count={count},callback={callback}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("searchMessages", _type, targetId, channelId, keyword, startTime, count);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("searchMessages", _type, targetId, channelId, keyword, startTime, count,
                                           new RCIMSearchMessagesCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int SearchMessagesByTimeRange(RCIMConversationType type, string targetId, string channelId,
                                                      string keyword, long startTime, long endTime, int offset, int count,
                                                      RCIMSearchMessagesByTimeRangeListener callback = null)
        {
            RCUnityLogger.getInstance().log(
                "SearchMessagesByTimeRange",
                $"type={type},targetId={targetId},channelId={channelId},keyword={keyword},startTime={startTime},endTime={endTime},offset={offset},count={count},callback={callback}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("searchMessagesByTimeRange", _type, targetId, channelId, keyword, startTime,
                                           endTime, offset, count);
                return ret;
            }
            else
            {
                int ret =
                    Engine.Call<int>("searchMessagesByTimeRange", _type, targetId, channelId, keyword, startTime, endTime,
                                     offset, count, new RCIMSearchMessagesByTimeRangeCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int SearchMessagesByUserId(string userId, RCIMConversationType type, string targetId,
                                                   string channelId, long startTime, int count,
                                                   RCIMSearchMessagesByUserIdListener callback = null)
        {
            RCUnityLogger.getInstance().log(
                "SearchMessagesByUserId",
                $"userId={userId},type={type},targetId={targetId},channelId={channelId},startTime={startTime},count={count},callback={callback}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("searchMessagesByUserId", userId, _type, targetId, channelId, startTime, count);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("searchMessagesByUserId", userId, _type, targetId, channelId, startTime, count,
                                           new RCIMSearchMessagesByUserIdCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int SearchConversations(List<RCIMConversationType> conversationTypes, string channelId,
                                                List<RCIMMessageType> messageTypes, string keyword,
                                                RCIMSearchConversationsListener callback = null)
        {
            RCUnityLogger.getInstance().log(
                "SearchConversations",
                $"conversationTypes={conversationTypes},channelId={channelId},messageTypes={messageTypes},keyword={keyword},callback={callback}");
            AndroidJavaObject _conversationTypes = new AndroidJavaObject("java.util.ArrayList");
            if (conversationTypes != null)
                foreach (RCIMConversationType item in conversationTypes)
                    _conversationTypes.Call<bool>("add", new ConversationTypeConverter(item).getAndroidObject());
            AndroidJavaObject _messageTypes = new AndroidJavaObject("java.util.ArrayList");
            if (messageTypes != null)
                foreach (RCIMMessageType item in messageTypes)
                    _messageTypes.Call<bool>("add", new MessageTypeConverter(item).getAndroidObject());
            if (callback == null)
            {
                int ret = Engine.Call<int>("searchConversations", _conversationTypes, channelId, _messageTypes, keyword);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("searchConversations", _conversationTypes, channelId, _messageTypes, keyword,
                                           new RCIMSearchConversationsCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int ChangeNotificationQuietHours(string startTime, int spanMinutes,
                                                         RCIMPushNotificationQuietHoursLevel level,
                                                         OnNotificationQuietHoursChangedAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "ChangeNotificationQuietHours",
                $"startTime={startTime},spanMinutes={spanMinutes},level={level},callback={callback}");
            AndroidJavaObject _level = new PushNotificationQuietHoursLevelConverter(level).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("changeNotificationQuietHours", startTime, spanMinutes, _level);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("changeNotificationQuietHours", startTime, spanMinutes, _level,
                                           new RCIMChangeNotificationQuietHoursCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int RemoveNotificationQuietHours(OnNotificationQuietHoursRemovedAction callback = null)
        {
            RCUnityLogger.getInstance().log("RemoveNotificationQuietHours", $"callback={callback}");
            if (callback == null)
            {
                int ret = Engine.Call<int>("removeNotificationQuietHours");
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("removeNotificationQuietHours",
                                           new RCIMRemoveNotificationQuietHoursCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int LoadNotificationQuietHours()
        {
            RCUnityLogger.getInstance().log("LoadNotificationQuietHours", $"");
            int ret = Engine.Call<int>("loadNotificationQuietHours");
            return ret;
        }
    
        public override int GetNotificationQuietHours(RCIMGetNotificationQuietHoursListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetNotificationQuietHours", $"callback={callback}");
            if (callback == null)
            {
                int ret = Engine.Call<int>("getNotificationQuietHours");
                return ret;
            }
            else
            {
                int ret =
                    Engine.Call<int>("getNotificationQuietHours", new RCIMGetNotificationQuietHoursCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int ChangeConversationNotificationLevel(
            RCIMConversationType type, string targetId, string channelId, RCIMPushNotificationLevel level,
            OnConversationNotificationLevelChangedAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "ChangeConversationNotificationLevel",
                $"type={type},targetId={targetId},channelId={channelId},level={level},callback={callback}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            AndroidJavaObject _level = new PushNotificationLevelConverter(level).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("changeConversationNotificationLevel", _type, targetId, channelId, _level);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("changeConversationNotificationLevel", _type, targetId, channelId, _level,
                                           new RCIMChangeConversationNotificationLevelCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int LoadConversationNotificationLevel(RCIMConversationType type, string targetId, string channelId)
        {
            RCUnityLogger.getInstance().log("LoadConversationNotificationLevel",
                                            $"type={type},targetId={targetId},channelId={channelId}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            int ret = Engine.Call<int>("loadConversationNotificationLevel", _type, targetId, channelId);
            return ret;
        }
    
        public override int GetConversationNotificationLevel(RCIMConversationType type, string targetId, string channelId,
                                                             RCIMGetConversationNotificationLevelListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetConversationNotificationLevel",
                                            $"type={type},targetId={targetId},channelId={channelId},callback={callback}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("getConversationNotificationLevel", _type, targetId, channelId);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("getConversationNotificationLevel", _type, targetId, channelId,
                                           new RCIMGetConversationNotificationLevelCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int ChangeConversationTypeNotificationLevel(
            RCIMConversationType type, RCIMPushNotificationLevel level,
            OnConversationTypeNotificationLevelChangedAction callback = null)
        {
            RCUnityLogger.getInstance().log("ChangeConversationTypeNotificationLevel",
                                            $"type={type},level={level},callback={callback}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            AndroidJavaObject _level = new PushNotificationLevelConverter(level).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("changeConversationTypeNotificationLevel", _type, _level);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("changeConversationTypeNotificationLevel", _type, _level,
                                           new RCIMChangeConversationTypeNotificationLevelCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int LoadConversationTypeNotificationLevel(RCIMConversationType type)
        {
            RCUnityLogger.getInstance().log("LoadConversationTypeNotificationLevel", $"type={type}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            int ret = Engine.Call<int>("loadConversationTypeNotificationLevel", _type);
            return ret;
        }
    
        public override int GetConversationTypeNotificationLevel(
            RCIMConversationType type, RCIMGetConversationTypeNotificationLevelListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetConversationTypeNotificationLevel", $"type={type},callback={callback}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("getConversationTypeNotificationLevel", _type);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("getConversationTypeNotificationLevel", _type,
                                           new RCIMGetConversationTypeNotificationLevelCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int ChangeUltraGroupDefaultNotificationLevel(
            string targetId, RCIMPushNotificationLevel level,
            OnUltraGroupDefaultNotificationLevelChangedAction callback = null)
        {
            RCUnityLogger.getInstance().log("ChangeUltraGroupDefaultNotificationLevel",
                                            $"targetId={targetId},level={level},callback={callback}");
            AndroidJavaObject _level = new PushNotificationLevelConverter(level).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("changeUltraGroupDefaultNotificationLevel", targetId, _level);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("changeUltraGroupDefaultNotificationLevel", targetId, _level,
                                           new RCIMChangeUltraGroupDefaultNotificationLevelCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int LoadUltraGroupDefaultNotificationLevel(string targetId)
        {
            RCUnityLogger.getInstance().log("LoadUltraGroupDefaultNotificationLevel", $"targetId={targetId}");
            int ret = Engine.Call<int>("loadUltraGroupDefaultNotificationLevel", targetId);
            return ret;
        }
    
        public override int GetUltraGroupDefaultNotificationLevel(
            string targetId, RCIMGetUltraGroupDefaultNotificationLevelListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetUltraGroupDefaultNotificationLevel",
                                            $"targetId={targetId},callback={callback}");
            if (callback == null)
            {
                int ret = Engine.Call<int>("getUltraGroupDefaultNotificationLevel", targetId);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("getUltraGroupDefaultNotificationLevel", targetId,
                                           new RCIMGetUltraGroupDefaultNotificationLevelCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int ChangeUltraGroupChannelDefaultNotificationLevel(
            string targetId, string channelId, RCIMPushNotificationLevel level,
            OnUltraGroupChannelDefaultNotificationLevelChangedAction callback = null)
        {
            RCUnityLogger.getInstance().log("ChangeUltraGroupChannelDefaultNotificationLevel",
                                            $"targetId={targetId},channelId={channelId},level={level},callback={callback}");
            AndroidJavaObject _level = new PushNotificationLevelConverter(level).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("changeUltraGroupChannelDefaultNotificationLevel", targetId, channelId, _level);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("changeUltraGroupChannelDefaultNotificationLevel", targetId, channelId, _level,
                                           new RCIMChangeUltraGroupChannelDefaultNotificationLevelCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int LoadUltraGroupChannelDefaultNotificationLevel(string targetId, string channelId)
        {
            RCUnityLogger.getInstance().log("LoadUltraGroupChannelDefaultNotificationLevel",
                                            $"targetId={targetId},channelId={channelId}");
            int ret = Engine.Call<int>("loadUltraGroupChannelDefaultNotificationLevel", targetId, channelId);
            return ret;
        }
    
        public override int GetUltraGroupChannelDefaultNotificationLevel(
            string targetId, string channelId, RCIMGetUltraGroupChannelDefaultNotificationLevelListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetUltraGroupChannelDefaultNotificationLevel",
                                            $"targetId={targetId},channelId={channelId},callback={callback}");
            if (callback == null)
            {
                int ret = Engine.Call<int>("getUltraGroupChannelDefaultNotificationLevel", targetId, channelId);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("getUltraGroupChannelDefaultNotificationLevel", targetId, channelId,
                                           new RCIMGetUltraGroupChannelDefaultNotificationLevelCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int ChangePushContentShowStatus(bool showContent,
                                                        OnPushContentShowStatusChangedAction callback = null)
        {
            RCUnityLogger.getInstance().log("ChangePushContentShowStatus",
                                            $"showContent={showContent},callback={callback}");
            if (callback == null)
            {
                int ret = Engine.Call<int>("changePushContentShowStatus", showContent);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("changePushContentShowStatus", showContent,
                                           new RCIMChangePushContentShowStatusCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int ChangePushLanguage(string language, OnPushLanguageChangedAction callback = null)
        {
            RCUnityLogger.getInstance().log("ChangePushLanguage", $"language={language},callback={callback}");
            if (callback == null)
            {
                int ret = Engine.Call<int>("changePushLanguage", language);
                return ret;
            }
            else
            {
                int ret =
                    Engine.Call<int>("changePushLanguage", language, new RCIMChangePushLanguageCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int ChangePushReceiveStatus(bool receive, OnPushReceiveStatusChangedAction callback = null)
        {
            RCUnityLogger.getInstance().log("ChangePushReceiveStatus", $"receive={receive},callback={callback}");
            if (callback == null)
            {
                int ret = Engine.Call<int>("changePushReceiveStatus", receive);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("changePushReceiveStatus", receive,
                                           new RCIMChangePushReceiveStatusCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int SendGroupMessageToDesignatedUsers(RCIMMessage message, List<string> userIds,
                                                              RCIMSendGroupMessageToDesignatedUsersListener callback = null)
        {
            RCUnityLogger.getInstance().log("SendGroupMessageToDesignatedUsers",
                                            $"message={message},userIds={userIds},callback={callback}");
            AndroidJavaObject _message = MessageConverter.from(message).getAndroidObject();
            AndroidJavaObject _userIds = new AndroidJavaObject("java.util.ArrayList");
            if (userIds != null)
                foreach (string item in userIds)
                    _userIds.Call<bool>("add", item);
            AndroidJavaObject _callback = null;
            if (callback != null)
            {
                _callback = new AndroidJavaObject(
                    "cn.rongcloud.im.wrapper.unity.RCIMSendGroupMessageToDesignatedUsersCallbackProxy",
                    new RCIMSendGroupMessageToDesignatedUsersCallbackProxy(callback));
            }
            if (callback == null)
            {
                int ret = Engine.Call<int>("sendGroupMessageToDesignatedUsers", _message, _userIds);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("sendGroupMessageToDesignatedUsers", _message, _userIds, _callback);
                return ret;
            }
        }
    
        public override int LoadMessageCount(RCIMConversationType type, string targetId, string channelId)
        {
            RCUnityLogger.getInstance().log("LoadMessageCount", $"type={type},targetId={targetId},channelId={channelId}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            int ret = Engine.Call<int>("loadMessageCount", _type, targetId, channelId);
            return ret;
        }
    
        public override int GetMessageCount(RCIMConversationType type, string targetId, string channelId,
                                            RCIMGetMessageCountListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetMessageCount",
                                            $"type={type},targetId={targetId},channelId={channelId},callback={callback}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("getMessageCount", _type, targetId, channelId);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("getMessageCount", _type, targetId, channelId,
                                           new RCIMGetMessageCountCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int LoadTopConversations(List<RCIMConversationType> conversationTypes, string channelId)
        {
            RCUnityLogger.getInstance().log("LoadTopConversations",
                                            $"conversationTypes={conversationTypes},channelId={channelId}");
            AndroidJavaObject _conversationTypes = new AndroidJavaObject("java.util.ArrayList");
            if (conversationTypes != null)
                foreach (RCIMConversationType item in conversationTypes)
                    _conversationTypes.Call<bool>("add", new ConversationTypeConverter(item).getAndroidObject());
            int ret = Engine.Call<int>("loadTopConversations", _conversationTypes, channelId);
            return ret;
        }
    
        public override int GetTopConversations(List<RCIMConversationType> conversationTypes, string channelId,
                                                RCIMGetTopConversationsListener callback = null)
        {
            RCUnityLogger.getInstance().log(
                "GetTopConversations", $"conversationTypes={conversationTypes},channelId={channelId},callback={callback}");
            AndroidJavaObject _conversationTypes = new AndroidJavaObject("java.util.ArrayList");
            if (conversationTypes != null)
                foreach (RCIMConversationType item in conversationTypes)
                    _conversationTypes.Call<bool>("add", new ConversationTypeConverter(item).getAndroidObject());
            if (callback == null)
            {
                int ret = Engine.Call<int>("getTopConversations", _conversationTypes, channelId);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("getTopConversations", _conversationTypes, channelId,
                                           new RCIMGetTopConversationsCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int SyncUltraGroupReadStatus(string targetId, string channelId, long timestamp,
                                                     OnUltraGroupReadStatusSyncedAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "SyncUltraGroupReadStatus",
                $"targetId={targetId},channelId={channelId},timestamp={timestamp},callback={callback}");
            if (callback == null)
            {
                int ret = Engine.Call<int>("syncUltraGroupReadStatus", targetId, channelId, timestamp);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("syncUltraGroupReadStatus", targetId, channelId, timestamp,
                                           new RCIMSyncUltraGroupReadStatusCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int LoadConversationsForAllChannel(RCIMConversationType type, string targetId)
        {
            RCUnityLogger.getInstance().log("LoadConversationsForAllChannel", $"type={type},targetId={targetId}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            int ret = Engine.Call<int>("loadConversationsForAllChannel", _type, targetId);
            return ret;
        }
    
        public override int GetConversationsForAllChannel(RCIMConversationType type, string targetId,
                                                          RCIMGetConversationsForAllChannelListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetConversationsForAllChannel",
                                            $"type={type},targetId={targetId},callback={callback}");
            AndroidJavaObject _type = new ConversationTypeConverter(type).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("getConversationsForAllChannel", _type, targetId);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("getConversationsForAllChannel", _type, targetId,
                                           new RCIMGetConversationsForAllChannelCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int ModifyUltraGroupMessage(string messageUId, RCIMMessage message,
                                                    OnUltraGroupMessageModifiedAction callback = null)
        {
            RCUnityLogger.getInstance().log("ModifyUltraGroupMessage",
                                            $"messageUId={messageUId},message={message},callback={callback}");
            AndroidJavaObject _message = MessageConverter.from(message).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("modifyUltraGroupMessage", messageUId, _message);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("modifyUltraGroupMessage", messageUId, _message,
                                           new RCIMModifyUltraGroupMessageCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int RecallUltraGroupMessage(RCIMMessage message, bool deleteRemote,
                                                    OnUltraGroupMessageRecalledAction callback = null)
        {
            RCUnityLogger.getInstance().log("RecallUltraGroupMessage",
                                            $"message={message},deleteRemote={deleteRemote},callback={callback}");
            AndroidJavaObject _message = MessageConverter.from(message).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("recallUltraGroupMessage", _message, deleteRemote);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("recallUltraGroupMessage", _message, deleteRemote,
                                           new RCIMRecallUltraGroupMessageCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int ClearUltraGroupMessages(string targetId, string channelId, long timestamp,
                                                    RCIMMessageOperationPolicy policy,
                                                    OnUltraGroupMessagesClearedAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "ClearUltraGroupMessages",
                $"targetId={targetId},channelId={channelId},timestamp={timestamp},policy={policy},callback={callback}");
            AndroidJavaObject _policy = new MessageOperationPolicyConverter(policy).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("clearUltraGroupMessages", targetId, channelId, timestamp, _policy);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("clearUltraGroupMessages", targetId, channelId, timestamp, _policy,
                                           new RCIMClearUltraGroupMessagesCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int SendUltraGroupTypingStatus(string targetId, string channelId,
                                                       RCIMUltraGroupTypingStatus typingStatus,
                                                       OnUltraGroupTypingStatusSentAction callback = null)
        {
            RCUnityLogger.getInstance().log(
                "SendUltraGroupTypingStatus",
                $"targetId={targetId},channelId={channelId},typingStatus={typingStatus},callback={callback}");
            AndroidJavaObject _typingStatus = new UltraGroupTypingStatusConverter(typingStatus).getAndroidObject();
            if (callback == null)
            {
                int ret = Engine.Call<int>("sendUltraGroupTypingStatus", targetId, channelId, _typingStatus);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("sendUltraGroupTypingStatus", targetId, channelId, _typingStatus,
                                           new RCIMSendUltraGroupTypingStatusCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int ClearUltraGroupMessagesForAllChannel(
            string targetId, long timestamp, OnUltraGroupMessagesClearedForAllChannelAction callback = null)
        {
            RCUnityLogger.getInstance().log("ClearUltraGroupMessagesForAllChannel",
                                            $"targetId={targetId},timestamp={timestamp},callback={callback}");
            if (callback == null)
            {
                int ret = Engine.Call<int>("clearUltraGroupMessagesForAllChannel", targetId, timestamp);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("clearUltraGroupMessagesForAllChannel", targetId, timestamp,
                                           new RCIMClearUltraGroupMessagesForAllChannelCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int LoadBatchRemoteUltraGroupMessages(List<RCIMMessage> messages)
        {
            RCUnityLogger.getInstance().log("LoadBatchRemoteUltraGroupMessages", $"messages={messages}");
            AndroidJavaObject _messages = new AndroidJavaObject("java.util.ArrayList");
            if (messages != null)
                foreach (RCIMMessage item in messages)
                    if (item != null)
                        _messages.Call<bool>("add", MessageConverter.from(item).getAndroidObject());
            int ret = Engine.Call<int>("loadBatchRemoteUltraGroupMessages", _messages);
            return ret;
        }
    
        public override int GetBatchRemoteUltraGroupMessages(List<RCIMMessage> messages,
                                                             RCIMGetBatchRemoteUltraGroupMessagesListener callback = null)
        {
            RCUnityLogger.getInstance().log("GetBatchRemoteUltraGroupMessages", $"messages={messages},callback={callback}");
            AndroidJavaObject _messages = new AndroidJavaObject("java.util.ArrayList");
            if (messages != null)
                foreach (RCIMMessage item in messages)
                    if (item != null)
                        _messages.Call<bool>("add", MessageConverter.from(item).getAndroidObject());
            if (callback == null)
            {
                int ret = Engine.Call<int>("getBatchRemoteUltraGroupMessages", _messages);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("getBatchRemoteUltraGroupMessages", _messages,
                                           new RCIMGetBatchRemoteUltraGroupMessagesCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int UpdateUltraGroupMessageExpansion(string messageUId, Dictionary<string, string> expansion,
                                                             OnUltraGroupMessageExpansionUpdatedAction callback = null)
        {
            RCUnityLogger.getInstance().log("UpdateUltraGroupMessageExpansion",
                                            $"messageUId={messageUId},expansion={expansion},callback={callback}");
            AndroidJavaObject _expansion = new AndroidJavaObject("java.util.HashMap");
            if (expansion != null)
                foreach (var item in expansion)
                    _expansion.Call<string>("put", item.Key, item.Value);
            if (callback == null)
            {
                int ret = Engine.Call<int>("updateUltraGroupMessageExpansion", messageUId, _expansion);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("updateUltraGroupMessageExpansion", messageUId, _expansion,
                                           new RCIMUpdateUltraGroupMessageExpansionCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int RemoveUltraGroupMessageExpansionForKeys(
            string messageUId, List<string> keys, OnUltraGroupMessageExpansionForKeysRemovedAction callback = null)
        {
            RCUnityLogger.getInstance().log("RemoveUltraGroupMessageExpansionForKeys",
                                            $"messageUId={messageUId},keys={keys},callback={callback}");
            AndroidJavaObject _keys = new AndroidJavaObject("java.util.ArrayList");
            if (keys != null)
                foreach (string item in keys)
                    _keys.Call<bool>("add", item);
            if (callback == null)
            {
                int ret = Engine.Call<int>("removeUltraGroupMessageExpansionForKeys", messageUId, _keys);
                return ret;
            }
            else
            {
                int ret = Engine.Call<int>("removeUltraGroupMessageExpansionForKeys", messageUId, _keys,
                                           new RCIMRemoveUltraGroupMessageExpansionForKeysCallbackProxy(callback));
                return ret;
            }
        }
    
        public override int ChangeLogLevel(RCIMLogLevel level)
        {
            RCUnityLogger.getInstance().log("ChangeLogLevel", $"level={level}");
            AndroidJavaObject _level = new LogLevelConverter(level).getAndroidObject();
            int ret = Engine.Call<int>("changeLogLevel", _level);
            return ret;
        }
    
        public override long GetDeltaTime()
        {
            RCUnityLogger.getInstance().log("GetDeltaTime", $"");
            long ret = Engine.Call<long>("getDeltaTime");
            return ret;
        }
    
        class RCIMListenerProxy : AndroidJavaProxy
        {
            RCIMAndroidEngine engine;
            public RCIMListenerProxy(RCIMAndroidEngine engine) : base("cn.rongcloud.im.wrapper.listener.IRCIMIWListener")
            {
                this.engine = engine;
            }
    
            public void onMessageReceived(AndroidJavaObject message, int left, bool offline, bool hasPackage)
            {
                RCIMMessage _message = null;
                if (message != null)
                    _message = (RCIMMessage)MessageConverter.from(message).getCSharpObject();
                RCUnityLogger.getInstance().log(
                    "OnMessageReceived", $"message={_message},left={left},offline={offline},hasPackage={hasPackage}");
                engine.OnMessageReceived?.Invoke(_message, left, offline, hasPackage);
            }
    
            public void onConnectionStatusChanged(AndroidJavaObject status)
            {
                RCIMConnectionStatus _status = new ConnectionStatusConverter(status).getCSharpObject();
                RCUnityLogger.getInstance().log("OnConnectionStatusChanged", $"status={_status}");
                engine.OnConnectionStatusChanged?.Invoke(_status);
            }
    
            public void onConversationTopStatusSynced(AndroidJavaObject type, string targetId, string channelId, bool top)
            {
                RCIMConversationType _type = new ConversationTypeConverter(type).getCSharpObject();
                RCUnityLogger.getInstance().log("OnConversationTopStatusSynced",
                                                $"type={_type},targetId={targetId},channelId={channelId},top={top}");
                engine.OnConversationTopStatusSynced?.Invoke(_type, targetId, channelId, top);
            }
    
            public void onConversationNotificationLevelSynced(AndroidJavaObject type, string targetId, string channelId,
                                                              AndroidJavaObject level)
            {
                RCIMConversationType _type = new ConversationTypeConverter(type).getCSharpObject();
                RCIMPushNotificationLevel _level = new PushNotificationLevelConverter(level).getCSharpObject();
                RCUnityLogger.getInstance().log("OnConversationNotificationLevelSynced",
                                                $"type={_type},targetId={targetId},channelId={channelId},level={_level}");
                engine.OnConversationNotificationLevelSynced?.Invoke(_type, targetId, channelId, _level);
            }
    
            public void onRemoteMessageRecalled(AndroidJavaObject message)
            {
                RCIMMessage _message = null;
                if (message != null)
                    _message = (RCIMMessage)MessageConverter.from(message).getCSharpObject();
                RCUnityLogger.getInstance().log("OnRemoteMessageRecalled", $"message={_message}");
                engine.OnRemoteMessageRecalled?.Invoke(_message);
            }
    
            public void onPrivateReadReceiptReceived(string targetId, string channelId, long timestamp)
            {
                RCUnityLogger.getInstance().log("OnPrivateReadReceiptReceived",
                                                $"targetId={targetId},channelId={channelId},timestamp={timestamp}");
                engine.OnPrivateReadReceiptReceived?.Invoke(targetId, channelId, timestamp);
            }
    
            public void onRemoteMessageExpansionUpdated(AndroidJavaObject expansion, AndroidJavaObject message)
            {
                Dictionary<string, string> _expansion = null;
                if (expansion != null)
                {
                    _expansion = new Dictionary<string, string>();
                    AndroidJavaObject iterator =
                        expansion.Call<AndroidJavaObject>("entrySet").Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject item = iterator.Call<AndroidJavaObject>("next");
                        string key = item.Call<string>("getKey");
                        string value = item.Call<string>("getValue");
                        _expansion.Add(key, value);
                    }
                }
                RCIMMessage _message = null;
                if (message != null)
                    _message = (RCIMMessage)MessageConverter.from(message).getCSharpObject();
                RCUnityLogger.getInstance().log("OnRemoteMessageExpansionUpdated",
                                                $"expansion={_expansion},message={_message}");
                engine.OnRemoteMessageExpansionUpdated?.Invoke(_expansion, _message);
            }
    
            public void onRemoteMessageExpansionForKeyRemoved(AndroidJavaObject message, AndroidJavaObject keys)
            {
                RCIMMessage _message = null;
                if (message != null)
                    _message = (RCIMMessage)MessageConverter.from(message).getCSharpObject();
                List<string> _keys = null;
                if (keys != null)
                {
                    _keys = new List<string>();
                    AndroidJavaObject iterator = keys.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        _keys.Add(iterator.Call<string>("next"));
                    }
                }
                RCUnityLogger.getInstance().log("OnRemoteMessageExpansionForKeyRemoved",
                                                $"message={_message},keys={_keys}");
                engine.OnRemoteMessageExpansionForKeyRemoved?.Invoke(_message, _keys);
            }
    
            public void onChatRoomMemberChanged(string targetId, AndroidJavaObject actions)
            {
                List<RCIMChatRoomMemberAction> _actions = null;
                if (actions != null)
                {
                    _actions = new List<RCIMChatRoomMemberAction>();
                    AndroidJavaObject iterator = actions.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _actions.Add(
                                (RCIMChatRoomMemberAction)ChatRoomMemberActionConverter.from(value).getCSharpObject());
                    }
                }
                RCUnityLogger.getInstance().log("OnChatRoomMemberChanged", $"targetId={targetId},actions={_actions}");
                engine.OnChatRoomMemberChanged?.Invoke(targetId, _actions);
            }
    
            public void onTypingStatusChanged(AndroidJavaObject type, string targetId, string channelId,
                                              AndroidJavaObject userTypingStatus)
            {
                RCIMConversationType _type = new ConversationTypeConverter(type).getCSharpObject();
                List<RCIMTypingStatus> _userTypingStatus = null;
                if (userTypingStatus != null)
                {
                    _userTypingStatus = new List<RCIMTypingStatus>();
                    AndroidJavaObject iterator = userTypingStatus.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _userTypingStatus.Add((RCIMTypingStatus)TypingStatusConverter.from(value).getCSharpObject());
                    }
                }
                RCUnityLogger.getInstance().log(
                    "OnTypingStatusChanged",
                    $"type={_type},targetId={targetId},channelId={channelId},userTypingStatus={_userTypingStatus}");
                engine.OnTypingStatusChanged?.Invoke(_type, targetId, channelId, _userTypingStatus);
            }
    
            public void onConversationReadStatusSyncMessageReceived(AndroidJavaObject type, string targetId, long timestamp)
            {
                RCIMConversationType _type = new ConversationTypeConverter(type).getCSharpObject();
                RCUnityLogger.getInstance().log("OnConversationReadStatusSyncMessageReceived",
                                                $"type={_type},targetId={targetId},timestamp={timestamp}");
                engine.OnConversationReadStatusSyncMessageReceived?.Invoke(_type, targetId, timestamp);
            }
    
            public void onChatRoomEntriesSynced(string roomId)
            {
                RCUnityLogger.getInstance().log("OnChatRoomEntriesSynced", $"roomId={roomId}");
                engine.OnChatRoomEntriesSynced?.Invoke(roomId);
            }
    
            public void onChatRoomEntriesChanged(AndroidJavaObject operationType, string roomId, AndroidJavaObject entries)
            {
                RCIMChatRoomEntriesOperationType _operationType =
                    new ChatRoomEntriesOperationTypeConverter(operationType).getCSharpObject();
                Dictionary<string, string> _entries = null;
                if (entries != null)
                {
                    _entries = new Dictionary<string, string>();
                    AndroidJavaObject iterator =
                        entries.Call<AndroidJavaObject>("entrySet").Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject item = iterator.Call<AndroidJavaObject>("next");
                        string key = item.Call<string>("getKey");
                        string value = item.Call<string>("getValue");
                        _entries.Add(key, value);
                    }
                }
                RCUnityLogger.getInstance().log("OnChatRoomEntriesChanged",
                                                $"operationType={_operationType},roomId={roomId},entries={_entries}");
                engine.OnChatRoomEntriesChanged?.Invoke(_operationType, roomId, _entries);
            }
    
            public void onRemoteUltraGroupMessageExpansionUpdated(AndroidJavaObject messages)
            {
                List<RCIMMessage> _messages = null;
                if (messages != null)
                {
                    _messages = new List<RCIMMessage>();
                    AndroidJavaObject iterator = messages.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _messages.Add((RCIMMessage)MessageConverter.from(value).getCSharpObject());
                    }
                }
                RCUnityLogger.getInstance().log("OnRemoteUltraGroupMessageExpansionUpdated", $"messages={_messages}");
                engine.OnRemoteUltraGroupMessageExpansionUpdated?.Invoke(_messages);
            }
    
            public void onRemoteUltraGroupMessageModified(AndroidJavaObject messages)
            {
                List<RCIMMessage> _messages = null;
                if (messages != null)
                {
                    _messages = new List<RCIMMessage>();
                    AndroidJavaObject iterator = messages.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _messages.Add((RCIMMessage)MessageConverter.from(value).getCSharpObject());
                    }
                }
                RCUnityLogger.getInstance().log("OnRemoteUltraGroupMessageModified", $"messages={_messages}");
                engine.OnRemoteUltraGroupMessageModified?.Invoke(_messages);
            }
    
            public void onRemoteUltraGroupMessageRecalled(AndroidJavaObject messages)
            {
                List<RCIMMessage> _messages = null;
                if (messages != null)
                {
                    _messages = new List<RCIMMessage>();
                    AndroidJavaObject iterator = messages.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _messages.Add((RCIMMessage)MessageConverter.from(value).getCSharpObject());
                    }
                }
                RCUnityLogger.getInstance().log("OnRemoteUltraGroupMessageRecalled", $"messages={_messages}");
                engine.OnRemoteUltraGroupMessageRecalled?.Invoke(_messages);
            }
    
            public void onUltraGroupReadTimeReceived(string targetId, string channelId, long timestamp)
            {
                RCUnityLogger.getInstance().log("OnUltraGroupReadTimeReceived",
                                                $"targetId={targetId},channelId={channelId},timestamp={timestamp}");
                engine.OnUltraGroupReadTimeReceived?.Invoke(targetId, channelId, timestamp);
            }
    
            public void onUltraGroupTypingStatusChanged(AndroidJavaObject info)
            {
                List<RCIMUltraGroupTypingStatusInfo> _info = null;
                if (info != null)
                {
                    _info = new List<RCIMUltraGroupTypingStatusInfo>();
                    AndroidJavaObject iterator = info.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _info.Add((RCIMUltraGroupTypingStatusInfo)UltraGroupTypingStatusInfoConverter.from(value)
                                          .getCSharpObject());
                    }
                }
                RCUnityLogger.getInstance().log("OnUltraGroupTypingStatusChanged", $"info={_info}");
                engine.OnUltraGroupTypingStatusChanged?.Invoke(_info);
            }
    
            public void onMessageBlocked(AndroidJavaObject info)
            {
                RCIMBlockedMessageInfo _info = null;
                if (info != null)
                    _info = (RCIMBlockedMessageInfo)BlockedMessageInfoConverter.from(info).getCSharpObject();
                RCUnityLogger.getInstance().log("OnMessageBlocked", $"info={_info}");
                engine.OnMessageBlocked?.Invoke(_info);
            }
    
            public void onChatRoomStatusChanged(string targetId, AndroidJavaObject status)
            {
                RCIMChatRoomStatus _status = new ChatRoomStatusConverter(status).getCSharpObject();
                RCUnityLogger.getInstance().log("OnChatRoomStatusChanged", $"targetId={targetId},status={_status}");
                engine.OnChatRoomStatusChanged?.Invoke(targetId, _status);
            }
    
            public void onGroupMessageReadReceiptRequestReceived(string targetId, string messageUId)
            {
                RCUnityLogger.getInstance().log("OnGroupMessageReadReceiptRequestReceived",
                                                $"targetId={targetId},messageUId={messageUId}");
                engine.OnGroupMessageReadReceiptRequestReceived?.Invoke(targetId, messageUId);
            }
    
            public void onGroupMessageReadReceiptResponseReceived(string targetId, string messageUId,
                                                                  AndroidJavaObject respondUserIds)
            {
                Dictionary<string, long> _respondUserIds = null;
                if (respondUserIds != null)
                {
                    _respondUserIds = new Dictionary<string, long>();
                    AndroidJavaObject iterator =
                        respondUserIds.Call<AndroidJavaObject>("entrySet").Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject item = iterator.Call<AndroidJavaObject>("next");
                        string key = item.Call<string>("getKey");
                        AndroidJavaObject value = item.Call<AndroidJavaObject>("getValue");
                        if (value != null)
                            _respondUserIds.Add(key, value.Call<long>("longValue"));
                    }
                }
                RCUnityLogger.getInstance().log(
                    "OnGroupMessageReadReceiptResponseReceived",
                    $"targetId={targetId},messageUId={messageUId},respondUserIds={_respondUserIds}");
                engine.OnGroupMessageReadReceiptResponseReceived?.Invoke(targetId, messageUId, _respondUserIds);
            }
    
            public void onConnected(int code, string userId)
            {
                RCUnityLogger.getInstance().log("OnConnected", $"code={code},userId={userId}");
                engine.OnConnected?.Invoke(code, userId);
            }
    
            public void onDatabaseOpened(int code)
            {
                RCUnityLogger.getInstance().log("OnDatabaseOpened", $"code={code}");
                engine.OnDatabaseOpened?.Invoke(code);
            }
    
            public void onConversationLoaded(int code, AndroidJavaObject type, string targetId, string channelId,
                                             AndroidJavaObject conversation)
            {
                RCIMConversationType _type = new ConversationTypeConverter(type).getCSharpObject();
                RCIMConversation _conversation = null;
                if (conversation != null)
                    _conversation = (RCIMConversation)ConversationConverter.from(conversation).getCSharpObject();
                RCUnityLogger.getInstance().log(
                    "OnConversationLoaded",
                    $"code={code},type={_type},targetId={targetId},channelId={channelId},conversation={_conversation}");
                engine.OnConversationLoaded?.Invoke(code, _type, targetId, channelId, _conversation);
            }
    
            public void onConversationsLoaded(int code, AndroidJavaObject conversationTypes, string channelId,
                                              long startTime, int count, AndroidJavaObject conversations)
            {
                List<RCIMConversationType> _conversationTypes = null;
                if (conversationTypes != null)
                {
                    _conversationTypes = new List<RCIMConversationType>();
                    AndroidJavaObject iterator = conversationTypes.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _conversationTypes.Add(new ConversationTypeConverter(value).getCSharpObject());
                    }
                }
                List<RCIMConversation> _conversations = null;
                if (conversations != null)
                {
                    _conversations = new List<RCIMConversation>();
                    AndroidJavaObject iterator = conversations.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _conversations.Add((RCIMConversation)ConversationConverter.from(value).getCSharpObject());
                    }
                }
                RCUnityLogger.getInstance().log(
                    "OnConversationsLoaded",
                    $"code={code},conversationTypes={_conversationTypes},channelId={channelId},startTime={startTime},count={count},conversations={_conversations}");
                engine.OnConversationsLoaded?.Invoke(code, _conversationTypes, channelId, startTime, count, _conversations);
            }
    
            public void onConversationRemoved(int code, AndroidJavaObject type, string targetId, string channelId)
            {
                RCIMConversationType _type = new ConversationTypeConverter(type).getCSharpObject();
                RCUnityLogger.getInstance().log("OnConversationRemoved",
                                                $"code={code},type={_type},targetId={targetId},channelId={channelId}");
                engine.OnConversationRemoved?.Invoke(code, _type, targetId, channelId);
            }
    
            public void onConversationsRemoved(int code, AndroidJavaObject conversationTypes, string channelId)
            {
                List<RCIMConversationType> _conversationTypes = null;
                if (conversationTypes != null)
                {
                    _conversationTypes = new List<RCIMConversationType>();
                    AndroidJavaObject iterator = conversationTypes.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _conversationTypes.Add(new ConversationTypeConverter(value).getCSharpObject());
                    }
                }
                RCUnityLogger.getInstance().log(
                    "OnConversationsRemoved", $"code={code},conversationTypes={_conversationTypes},channelId={channelId}");
                engine.OnConversationsRemoved?.Invoke(code, _conversationTypes, channelId);
            }
    
            public void onTotalUnreadCountLoaded(int code, string channelId, int count)
            {
                RCUnityLogger.getInstance().log("OnTotalUnreadCountLoaded",
                                                $"code={code},channelId={channelId},count={count}");
                engine.OnTotalUnreadCountLoaded?.Invoke(code, channelId, count);
            }
    
            public void onUnreadCountLoaded(int code, AndroidJavaObject type, string targetId, string channelId, int count)
            {
                RCIMConversationType _type = new ConversationTypeConverter(type).getCSharpObject();
                RCUnityLogger.getInstance().log(
                    "OnUnreadCountLoaded",
                    $"code={code},type={_type},targetId={targetId},channelId={channelId},count={count}");
                engine.OnUnreadCountLoaded?.Invoke(code, _type, targetId, channelId, count);
            }
    
            public void onUnreadCountByConversationTypesLoaded(int code, AndroidJavaObject conversationTypes,
                                                               string channelId, bool contain, int count)
            {
                List<RCIMConversationType> _conversationTypes = null;
                if (conversationTypes != null)
                {
                    _conversationTypes = new List<RCIMConversationType>();
                    AndroidJavaObject iterator = conversationTypes.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _conversationTypes.Add(new ConversationTypeConverter(value).getCSharpObject());
                    }
                }
                RCUnityLogger.getInstance().log(
                    "OnUnreadCountByConversationTypesLoaded",
                    $"code={code},conversationTypes={_conversationTypes},channelId={channelId},contain={contain},count={count}");
                engine.OnUnreadCountByConversationTypesLoaded?.Invoke(code, _conversationTypes, channelId, contain, count);
            }
    
            public void onUnreadMentionedCountLoaded(int code, AndroidJavaObject type, string targetId, string channelId,
                                                     int count)
            {
                RCIMConversationType _type = new ConversationTypeConverter(type).getCSharpObject();
                RCUnityLogger.getInstance().log(
                    "OnUnreadMentionedCountLoaded",
                    $"code={code},type={_type},targetId={targetId},channelId={channelId},count={count}");
                engine.OnUnreadMentionedCountLoaded?.Invoke(code, _type, targetId, channelId, count);
            }
    
            public void onUltraGroupAllUnreadCountLoaded(int code, int count)
            {
                RCUnityLogger.getInstance().log("OnUltraGroupAllUnreadCountLoaded", $"code={code},count={count}");
                engine.OnUltraGroupAllUnreadCountLoaded?.Invoke(code, count);
            }
    
            public void onUltraGroupAllUnreadMentionedCountLoaded(int code, int count)
            {
                RCUnityLogger.getInstance().log("OnUltraGroupAllUnreadMentionedCountLoaded", $"code={code},count={count}");
                engine.OnUltraGroupAllUnreadMentionedCountLoaded?.Invoke(code, count);
            }
    
            public void onUltraGroupConversationsSynced()
            {
                RCUnityLogger.getInstance().log("OnUltraGroupConversationsSynced", $"");
                engine.OnUltraGroupConversationsSynced?.Invoke();
            }
    
            public void onUnreadCountCleared(int code, AndroidJavaObject type, string targetId, string channelId,
                                             long timestamp)
            {
                RCIMConversationType _type = new ConversationTypeConverter(type).getCSharpObject();
                RCUnityLogger.getInstance().log(
                    "OnUnreadCountCleared",
                    $"code={code},type={_type},targetId={targetId},channelId={channelId},timestamp={timestamp}");
                engine.OnUnreadCountCleared?.Invoke(code, _type, targetId, channelId, timestamp);
            }
    
            public void onDraftMessageSaved(int code, AndroidJavaObject type, string targetId, string channelId,
                                            string draft)
            {
                RCIMConversationType _type = new ConversationTypeConverter(type).getCSharpObject();
                RCUnityLogger.getInstance().log(
                    "OnDraftMessageSaved",
                    $"code={code},type={_type},targetId={targetId},channelId={channelId},draft={draft}");
                engine.OnDraftMessageSaved?.Invoke(code, _type, targetId, channelId, draft);
            }
    
            public void onDraftMessageCleared(int code, AndroidJavaObject type, string targetId, string channelId)
            {
                RCIMConversationType _type = new ConversationTypeConverter(type).getCSharpObject();
                RCUnityLogger.getInstance().log("OnDraftMessageCleared",
                                                $"code={code},type={_type},targetId={targetId},channelId={channelId}");
                engine.OnDraftMessageCleared?.Invoke(code, _type, targetId, channelId);
            }
    
            public void onDraftMessageLoaded(int code, AndroidJavaObject type, string targetId, string channelId,
                                             string draft)
            {
                RCIMConversationType _type = new ConversationTypeConverter(type).getCSharpObject();
                RCUnityLogger.getInstance().log(
                    "OnDraftMessageLoaded",
                    $"code={code},type={_type},targetId={targetId},channelId={channelId},draft={draft}");
                engine.OnDraftMessageLoaded?.Invoke(code, _type, targetId, channelId, draft);
            }
    
            public void onBlockedConversationsLoaded(int code, AndroidJavaObject conversationTypes, string channelId,
                                                     AndroidJavaObject conversations)
            {
                List<RCIMConversationType> _conversationTypes = null;
                if (conversationTypes != null)
                {
                    _conversationTypes = new List<RCIMConversationType>();
                    AndroidJavaObject iterator = conversationTypes.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _conversationTypes.Add(new ConversationTypeConverter(value).getCSharpObject());
                    }
                }
                List<RCIMConversation> _conversations = null;
                if (conversations != null)
                {
                    _conversations = new List<RCIMConversation>();
                    AndroidJavaObject iterator = conversations.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _conversations.Add((RCIMConversation)ConversationConverter.from(value).getCSharpObject());
                    }
                }
                RCUnityLogger.getInstance().log(
                    "OnBlockedConversationsLoaded",
                    $"code={code},conversationTypes={_conversationTypes},channelId={channelId},conversations={_conversations}");
                engine.OnBlockedConversationsLoaded?.Invoke(code, _conversationTypes, channelId, _conversations);
            }
    
            public void onConversationTopStatusChanged(int code, AndroidJavaObject type, string targetId, string channelId,
                                                       bool top)
            {
                RCIMConversationType _type = new ConversationTypeConverter(type).getCSharpObject();
                RCUnityLogger.getInstance().log(
                    "OnConversationTopStatusChanged",
                    $"code={code},type={_type},targetId={targetId},channelId={channelId},top={top}");
                engine.OnConversationTopStatusChanged?.Invoke(code, _type, targetId, channelId, top);
            }
    
            public void onConversationTopStatusLoaded(int code, AndroidJavaObject type, string targetId, string channelId,
                                                      bool top)
            {
                RCIMConversationType _type = new ConversationTypeConverter(type).getCSharpObject();
                RCUnityLogger.getInstance().log(
                    "OnConversationTopStatusLoaded",
                    $"code={code},type={_type},targetId={targetId},channelId={channelId},top={top}");
                engine.OnConversationTopStatusLoaded?.Invoke(code, _type, targetId, channelId, top);
            }
    
            public void onConversationReadStatusSynced(int code, AndroidJavaObject type, string targetId, string channelId,
                                                       long timestamp)
            {
                RCIMConversationType _type = new ConversationTypeConverter(type).getCSharpObject();
                RCUnityLogger.getInstance().log(
                    "OnConversationReadStatusSynced",
                    $"code={code},type={_type},targetId={targetId},channelId={channelId},timestamp={timestamp}");
                engine.OnConversationReadStatusSynced?.Invoke(code, _type, targetId, channelId, timestamp);
            }
    
            public void onMessageAttached(AndroidJavaObject message)
            {
                RCIMMessage _message = null;
                if (message != null)
                    _message = (RCIMMessage)MessageConverter.from(message).getCSharpObject();
                RCUnityLogger.getInstance().log("OnMessageAttached", $"message={_message}");
                engine.OnMessageAttached?.Invoke(_message);
            }
    
            public void onMessageSent(int code, AndroidJavaObject message)
            {
                RCIMMessage _message = null;
                if (message != null)
                    _message = (RCIMMessage)MessageConverter.from(message).getCSharpObject();
                RCUnityLogger.getInstance().log("OnMessageSent", $"code={code},message={_message}");
                engine.OnMessageSent?.Invoke(code, _message);
            }
    
            public void onMediaMessageAttached(AndroidJavaObject message)
            {
                RCIMMediaMessage _message = null;
                if (message != null)
                    _message = (RCIMMediaMessage)MediaMessageConverter.from(message).getCSharpObject();
                RCUnityLogger.getInstance().log("OnMediaMessageAttached", $"message={_message}");
                engine.OnMediaMessageAttached?.Invoke(_message);
            }
    
            public void onMediaMessageSending(AndroidJavaObject message, int progress)
            {
                RCIMMediaMessage _message = null;
                if (message != null)
                    _message = (RCIMMediaMessage)MediaMessageConverter.from(message).getCSharpObject();
                RCUnityLogger.getInstance().log("OnMediaMessageSending", $"message={_message},progress={progress}");
                engine.OnMediaMessageSending?.Invoke(_message, progress);
            }
    
            public void onSendingMediaMessageCanceled(int code, AndroidJavaObject message)
            {
                RCIMMediaMessage _message = null;
                if (message != null)
                    _message = (RCIMMediaMessage)MediaMessageConverter.from(message).getCSharpObject();
                RCUnityLogger.getInstance().log("OnSendingMediaMessageCanceled", $"code={code},message={_message}");
                engine.OnSendingMediaMessageCanceled?.Invoke(code, _message);
            }
    
            public void onMediaMessageSent(int code, AndroidJavaObject message)
            {
                RCIMMediaMessage _message = null;
                if (message != null)
                    _message = (RCIMMediaMessage)MediaMessageConverter.from(message).getCSharpObject();
                RCUnityLogger.getInstance().log("OnMediaMessageSent", $"code={code},message={_message}");
                engine.OnMediaMessageSent?.Invoke(code, _message);
            }
    
            public void onMediaMessageDownloading(AndroidJavaObject message, int progress)
            {
                RCIMMediaMessage _message = null;
                if (message != null)
                    _message = (RCIMMediaMessage)MediaMessageConverter.from(message).getCSharpObject();
                RCUnityLogger.getInstance().log("OnMediaMessageDownloading", $"message={_message},progress={progress}");
                engine.OnMediaMessageDownloading?.Invoke(_message, progress);
            }
    
            public void onMediaMessageDownloaded(int code, AndroidJavaObject message)
            {
                RCIMMediaMessage _message = null;
                if (message != null)
                    _message = (RCIMMediaMessage)MediaMessageConverter.from(message).getCSharpObject();
                RCUnityLogger.getInstance().log("OnMediaMessageDownloaded", $"code={code},message={_message}");
                engine.OnMediaMessageDownloaded?.Invoke(code, _message);
            }
    
            public void onDownloadingMediaMessageCanceled(int code, AndroidJavaObject message)
            {
                RCIMMediaMessage _message = null;
                if (message != null)
                    _message = (RCIMMediaMessage)MediaMessageConverter.from(message).getCSharpObject();
                RCUnityLogger.getInstance().log("OnDownloadingMediaMessageCanceled", $"code={code},message={_message}");
                engine.OnDownloadingMediaMessageCanceled?.Invoke(code, _message);
            }
    
            public void onMessagesLoaded(int code, AndroidJavaObject type, string targetId, string channelId, long sentTime,
                                         AndroidJavaObject order, AndroidJavaObject messages)
            {
                RCIMConversationType _type = new ConversationTypeConverter(type).getCSharpObject();
                RCIMTimeOrder _order = new TimeOrderConverter(order).getCSharpObject();
                List<RCIMMessage> _messages = null;
                if (messages != null)
                {
                    _messages = new List<RCIMMessage>();
                    AndroidJavaObject iterator = messages.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _messages.Add((RCIMMessage)MessageConverter.from(value).getCSharpObject());
                    }
                }
                RCUnityLogger.getInstance().log(
                    "OnMessagesLoaded",
                    $"code={code},type={_type},targetId={targetId},channelId={channelId},sentTime={sentTime},order={_order},messages={_messages}");
                engine.OnMessagesLoaded?.Invoke(code, _type, targetId, channelId, sentTime, _order, _messages);
            }
    
            public void onUnreadMentionedMessagesLoaded(int code, AndroidJavaObject type, string targetId, string channelId,
                                                        AndroidJavaObject messages)
            {
                RCIMConversationType _type = new ConversationTypeConverter(type).getCSharpObject();
                List<RCIMMessage> _messages = null;
                if (messages != null)
                {
                    _messages = new List<RCIMMessage>();
                    AndroidJavaObject iterator = messages.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _messages.Add((RCIMMessage)MessageConverter.from(value).getCSharpObject());
                    }
                }
                RCUnityLogger.getInstance().log(
                    "OnUnreadMentionedMessagesLoaded",
                    $"code={code},type={_type},targetId={targetId},channelId={channelId},messages={_messages}");
                engine.OnUnreadMentionedMessagesLoaded?.Invoke(code, _type, targetId, channelId, _messages);
            }
    
            public void onFirstUnreadMessageLoaded(int code, AndroidJavaObject type, string targetId, string channelId,
                                                   AndroidJavaObject message)
            {
                RCIMConversationType _type = new ConversationTypeConverter(type).getCSharpObject();
                RCIMMessage _message = null;
                if (message != null)
                    _message = (RCIMMessage)MessageConverter.from(message).getCSharpObject();
                RCUnityLogger.getInstance().log(
                    "OnFirstUnreadMessageLoaded",
                    $"code={code},type={_type},targetId={targetId},channelId={channelId},message={_message}");
                engine.OnFirstUnreadMessageLoaded?.Invoke(code, _type, targetId, channelId, _message);
            }
    
            public void onMessageInserted(int code, AndroidJavaObject message)
            {
                RCIMMessage _message = null;
                if (message != null)
                    _message = (RCIMMessage)MessageConverter.from(message).getCSharpObject();
                RCUnityLogger.getInstance().log("OnMessageInserted", $"code={code},message={_message}");
                engine.OnMessageInserted?.Invoke(code, _message);
            }
    
            public void onMessagesInserted(int code, AndroidJavaObject messages)
            {
                List<RCIMMessage> _messages = null;
                if (messages != null)
                {
                    _messages = new List<RCIMMessage>();
                    AndroidJavaObject iterator = messages.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _messages.Add((RCIMMessage)MessageConverter.from(value).getCSharpObject());
                    }
                }
                RCUnityLogger.getInstance().log("OnMessagesInserted", $"code={code},messages={_messages}");
                engine.OnMessagesInserted?.Invoke(code, _messages);
            }
    
            public void onMessagesCleared(int code, AndroidJavaObject type, string targetId, string channelId,
                                          long timestamp)
            {
                RCIMConversationType _type = new ConversationTypeConverter(type).getCSharpObject();
                RCUnityLogger.getInstance().log(
                    "OnMessagesCleared",
                    $"code={code},type={_type},targetId={targetId},channelId={channelId},timestamp={timestamp}");
                engine.OnMessagesCleared?.Invoke(code, _type, targetId, channelId, timestamp);
            }
    
            public void onLocalMessagesDeleted(int code, AndroidJavaObject messages)
            {
                List<RCIMMessage> _messages = null;
                if (messages != null)
                {
                    _messages = new List<RCIMMessage>();
                    AndroidJavaObject iterator = messages.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _messages.Add((RCIMMessage)MessageConverter.from(value).getCSharpObject());
                    }
                }
                RCUnityLogger.getInstance().log("OnLocalMessagesDeleted", $"code={code},messages={_messages}");
                engine.OnLocalMessagesDeleted?.Invoke(code, _messages);
            }
    
            public void onMessagesDeleted(int code, AndroidJavaObject type, string targetId, string channelId,
                                          AndroidJavaObject messages)
            {
                RCIMConversationType _type = new ConversationTypeConverter(type).getCSharpObject();
                List<RCIMMessage> _messages = null;
                if (messages != null)
                {
                    _messages = new List<RCIMMessage>();
                    AndroidJavaObject iterator = messages.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _messages.Add((RCIMMessage)MessageConverter.from(value).getCSharpObject());
                    }
                }
                RCUnityLogger.getInstance().log(
                    "OnMessagesDeleted",
                    $"code={code},type={_type},targetId={targetId},channelId={channelId},messages={_messages}");
                engine.OnMessagesDeleted?.Invoke(code, _type, targetId, channelId, _messages);
            }
    
            public void onMessageRecalled(int code, AndroidJavaObject message)
            {
                RCIMMessage _message = null;
                if (message != null)
                    _message = (RCIMMessage)MessageConverter.from(message).getCSharpObject();
                RCUnityLogger.getInstance().log("OnMessageRecalled", $"code={code},message={_message}");
                engine.OnMessageRecalled?.Invoke(code, _message);
            }
    
            public void onPrivateReadReceiptMessageSent(int code, string targetId, string channelId, long timestamp)
            {
                RCUnityLogger.getInstance().log(
                    "OnPrivateReadReceiptMessageSent",
                    $"code={code},targetId={targetId},channelId={channelId},timestamp={timestamp}");
                engine.OnPrivateReadReceiptMessageSent?.Invoke(code, targetId, channelId, timestamp);
            }
    
            public void onMessageExpansionUpdated(int code, string messageUId, AndroidJavaObject expansion)
            {
                Dictionary<string, string> _expansion = null;
                if (expansion != null)
                {
                    _expansion = new Dictionary<string, string>();
                    AndroidJavaObject iterator =
                        expansion.Call<AndroidJavaObject>("entrySet").Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject item = iterator.Call<AndroidJavaObject>("next");
                        string key = item.Call<string>("getKey");
                        string value = item.Call<string>("getValue");
                        _expansion.Add(key, value);
                    }
                }
                RCUnityLogger.getInstance().log("OnMessageExpansionUpdated",
                                                $"code={code},messageUId={messageUId},expansion={_expansion}");
                engine.OnMessageExpansionUpdated?.Invoke(code, messageUId, _expansion);
            }
    
            public void onMessageExpansionForKeysRemoved(int code, string messageUId, AndroidJavaObject keys)
            {
                List<string> _keys = null;
                if (keys != null)
                {
                    _keys = new List<string>();
                    AndroidJavaObject iterator = keys.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        _keys.Add(iterator.Call<string>("next"));
                    }
                }
                RCUnityLogger.getInstance().log("OnMessageExpansionForKeysRemoved",
                                                $"code={code},messageUId={messageUId},keys={_keys}");
                engine.OnMessageExpansionForKeysRemoved?.Invoke(code, messageUId, _keys);
            }
    
            public void onMessageReceiveStatusChanged(int code, long messageId)
            {
                RCUnityLogger.getInstance().log("OnMessageReceiveStatusChanged", $"code={code},messageId={messageId}");
                engine.OnMessageReceiveStatusChanged?.Invoke(code, messageId);
            }
    
            public void onMessageSentStatusChanged(int code, long messageId)
            {
                RCUnityLogger.getInstance().log("OnMessageSentStatusChanged", $"code={code},messageId={messageId}");
                engine.OnMessageSentStatusChanged?.Invoke(code, messageId);
            }
    
            public void onChatRoomJoined(int code, string targetId)
            {
                RCUnityLogger.getInstance().log("OnChatRoomJoined", $"code={code},targetId={targetId}");
                engine.OnChatRoomJoined?.Invoke(code, targetId);
            }
    
            public void onChatRoomJoining(string targetId)
            {
                RCUnityLogger.getInstance().log("OnChatRoomJoining", $"targetId={targetId}");
                engine.OnChatRoomJoining?.Invoke(targetId);
            }
    
            public void onChatRoomLeft(int code, string targetId)
            {
                RCUnityLogger.getInstance().log("OnChatRoomLeft", $"code={code},targetId={targetId}");
                engine.OnChatRoomLeft?.Invoke(code, targetId);
            }
    
            public void onChatRoomMessagesLoaded(int code, string targetId, AndroidJavaObject messages, long syncTime)
            {
                List<RCIMMessage> _messages = null;
                if (messages != null)
                {
                    _messages = new List<RCIMMessage>();
                    AndroidJavaObject iterator = messages.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _messages.Add((RCIMMessage)MessageConverter.from(value).getCSharpObject());
                    }
                }
                RCUnityLogger.getInstance().log(
                    "OnChatRoomMessagesLoaded",
                    $"code={code},targetId={targetId},messages={_messages},syncTime={syncTime}");
                engine.OnChatRoomMessagesLoaded?.Invoke(code, targetId, _messages, syncTime);
            }
    
            public void onChatRoomEntryAdded(int code, string targetId, string key)
            {
                RCUnityLogger.getInstance().log("OnChatRoomEntryAdded", $"code={code},targetId={targetId},key={key}");
                engine.OnChatRoomEntryAdded?.Invoke(code, targetId, key);
            }
    
            public void onChatRoomEntriesAdded(int code, string targetId, AndroidJavaObject entries,
                                               AndroidJavaObject errorEntries)
            {
                Dictionary<string, string> _entries = null;
                if (entries != null)
                {
                    _entries = new Dictionary<string, string>();
                    AndroidJavaObject iterator =
                        entries.Call<AndroidJavaObject>("entrySet").Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject item = iterator.Call<AndroidJavaObject>("next");
                        string key = item.Call<string>("getKey");
                        string value = item.Call<string>("getValue");
                        _entries.Add(key, value);
                    }
                }
                Dictionary<string, int> _errorEntries = null;
                if (errorEntries != null)
                {
                    _errorEntries = new Dictionary<string, int>();
                    AndroidJavaObject iterator =
                        errorEntries.Call<AndroidJavaObject>("entrySet").Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject item = iterator.Call<AndroidJavaObject>("next");
                        string key = item.Call<string>("getKey");
                        AndroidJavaObject value = item.Call<AndroidJavaObject>("getValue");
                        if (value != null)
                            _errorEntries.Add(key, value.Call<int>("intValue"));
                    }
                }
                RCUnityLogger.getInstance().log(
                    "OnChatRoomEntriesAdded",
                    $"code={code},targetId={targetId},entries={_entries},errorEntries={_errorEntries}");
                engine.OnChatRoomEntriesAdded?.Invoke(code, targetId, _entries, _errorEntries);
            }
    
            public void onChatRoomEntryLoaded(int code, string targetId, AndroidJavaObject entry)
            {
                Dictionary<string, string> _entry = null;
                if (entry != null)
                {
                    _entry = new Dictionary<string, string>();
                    AndroidJavaObject iterator =
                        entry.Call<AndroidJavaObject>("entrySet").Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject item = iterator.Call<AndroidJavaObject>("next");
                        string key = item.Call<string>("getKey");
                        string value = item.Call<string>("getValue");
                        _entry.Add(key, value);
                    }
                }
                RCUnityLogger.getInstance().log("OnChatRoomEntryLoaded", $"code={code},targetId={targetId},entry={_entry}");
                engine.OnChatRoomEntryLoaded?.Invoke(code, targetId, _entry);
            }
    
            public void onChatRoomAllEntriesLoaded(int code, string targetId, AndroidJavaObject entries)
            {
                Dictionary<string, string> _entries = null;
                if (entries != null)
                {
                    _entries = new Dictionary<string, string>();
                    AndroidJavaObject iterator =
                        entries.Call<AndroidJavaObject>("entrySet").Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject item = iterator.Call<AndroidJavaObject>("next");
                        string key = item.Call<string>("getKey");
                        string value = item.Call<string>("getValue");
                        _entries.Add(key, value);
                    }
                }
                RCUnityLogger.getInstance().log("OnChatRoomAllEntriesLoaded",
                                                $"code={code},targetId={targetId},entries={_entries}");
                engine.OnChatRoomAllEntriesLoaded?.Invoke(code, targetId, _entries);
            }
    
            public void onChatRoomEntryRemoved(int code, string targetId, string key)
            {
                RCUnityLogger.getInstance().log("OnChatRoomEntryRemoved", $"code={code},targetId={targetId},key={key}");
                engine.OnChatRoomEntryRemoved?.Invoke(code, targetId, key);
            }
    
            public void onChatRoomEntriesRemoved(int code, string targetId, AndroidJavaObject keys)
            {
                List<string> _keys = null;
                if (keys != null)
                {
                    _keys = new List<string>();
                    AndroidJavaObject iterator = keys.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        _keys.Add(iterator.Call<string>("next"));
                    }
                }
                RCUnityLogger.getInstance().log("OnChatRoomEntriesRemoved",
                                                $"code={code},targetId={targetId},keys={_keys}");
                engine.OnChatRoomEntriesRemoved?.Invoke(code, targetId, _keys);
            }
    
            public void onBlacklistAdded(int code, string userId)
            {
                RCUnityLogger.getInstance().log("OnBlacklistAdded", $"code={code},userId={userId}");
                engine.OnBlacklistAdded?.Invoke(code, userId);
            }
    
            public void onBlacklistRemoved(int code, string userId)
            {
                RCUnityLogger.getInstance().log("OnBlacklistRemoved", $"code={code},userId={userId}");
                engine.OnBlacklistRemoved?.Invoke(code, userId);
            }
    
            public void onBlacklistStatusLoaded(int code, string userId, AndroidJavaObject status)
            {
                RCIMBlacklistStatus _status = new BlacklistStatusConverter(status).getCSharpObject();
                RCUnityLogger.getInstance().log("OnBlacklistStatusLoaded", $"code={code},userId={userId},status={_status}");
                engine.OnBlacklistStatusLoaded?.Invoke(code, userId, _status);
            }
    
            public void onBlacklistLoaded(int code, AndroidJavaObject userIds)
            {
                List<string> _userIds = null;
                if (userIds != null)
                {
                    _userIds = new List<string>();
                    AndroidJavaObject iterator = userIds.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        _userIds.Add(iterator.Call<string>("next"));
                    }
                }
                RCUnityLogger.getInstance().log("OnBlacklistLoaded", $"code={code},userIds={_userIds}");
                engine.OnBlacklistLoaded?.Invoke(code, _userIds);
            }
    
            public void onMessagesSearched(int code, AndroidJavaObject type, string targetId, string channelId,
                                           string keyword, long startTime, int count, AndroidJavaObject messages)
            {
                RCIMConversationType _type = new ConversationTypeConverter(type).getCSharpObject();
                List<RCIMMessage> _messages = null;
                if (messages != null)
                {
                    _messages = new List<RCIMMessage>();
                    AndroidJavaObject iterator = messages.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _messages.Add((RCIMMessage)MessageConverter.from(value).getCSharpObject());
                    }
                }
                RCUnityLogger.getInstance().log(
                    "OnMessagesSearched",
                    $"code={code},type={_type},targetId={targetId},channelId={channelId},keyword={keyword},startTime={startTime},count={count},messages={_messages}");
                engine.OnMessagesSearched?.Invoke(code, _type, targetId, channelId, keyword, startTime, count, _messages);
            }
    
            public void onMessagesSearchedByTimeRange(int code, AndroidJavaObject type, string targetId, string channelId,
                                                      string keyword, long startTime, long endTime, int offset, int count,
                                                      AndroidJavaObject messages)
            {
                RCIMConversationType _type = new ConversationTypeConverter(type).getCSharpObject();
                List<RCIMMessage> _messages = null;
                if (messages != null)
                {
                    _messages = new List<RCIMMessage>();
                    AndroidJavaObject iterator = messages.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _messages.Add((RCIMMessage)MessageConverter.from(value).getCSharpObject());
                    }
                }
                RCUnityLogger.getInstance().log(
                    "OnMessagesSearchedByTimeRange",
                    $"code={code},type={_type},targetId={targetId},channelId={channelId},keyword={keyword},startTime={startTime},endTime={endTime},offset={offset},count={count},messages={_messages}");
                engine.OnMessagesSearchedByTimeRange?.Invoke(code, _type, targetId, channelId, keyword, startTime, endTime,
                                                             offset, count, _messages);
            }
    
            public void onMessagesSearchedByUserId(int code, string userId, AndroidJavaObject type, string targetId,
                                                   string channelId, long startTime, int count, AndroidJavaObject messages)
            {
                RCIMConversationType _type = new ConversationTypeConverter(type).getCSharpObject();
                List<RCIMMessage> _messages = null;
                if (messages != null)
                {
                    _messages = new List<RCIMMessage>();
                    AndroidJavaObject iterator = messages.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _messages.Add((RCIMMessage)MessageConverter.from(value).getCSharpObject());
                    }
                }
                RCUnityLogger.getInstance().log(
                    "OnMessagesSearchedByUserId",
                    $"code={code},userId={userId},type={_type},targetId={targetId},channelId={channelId},startTime={startTime},count={count},messages={_messages}");
                engine.OnMessagesSearchedByUserId?.Invoke(code, userId, _type, targetId, channelId, startTime, count,
                                                          _messages);
            }
    
            public void onConversationsSearched(int code, AndroidJavaObject conversationTypes, string channelId,
                                                AndroidJavaObject messageTypes, string keyword,
                                                AndroidJavaObject conversations)
            {
                List<RCIMConversationType> _conversationTypes = null;
                if (conversationTypes != null)
                {
                    _conversationTypes = new List<RCIMConversationType>();
                    AndroidJavaObject iterator = conversationTypes.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _conversationTypes.Add(new ConversationTypeConverter(value).getCSharpObject());
                    }
                }
                List<RCIMMessageType> _messageTypes = null;
                if (messageTypes != null)
                {
                    _messageTypes = new List<RCIMMessageType>();
                    AndroidJavaObject iterator = messageTypes.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _messageTypes.Add(new MessageTypeConverter(value).getCSharpObject());
                    }
                }
                List<RCIMSearchConversationResult> _conversations = null;
                if (conversations != null)
                {
                    _conversations = new List<RCIMSearchConversationResult>();
                    AndroidJavaObject iterator = conversations.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _conversations.Add((RCIMSearchConversationResult)SearchConversationResultConverter.from(value)
                                                   .getCSharpObject());
                    }
                }
                RCUnityLogger.getInstance().log(
                    "OnConversationsSearched",
                    $"code={code},conversationTypes={_conversationTypes},channelId={channelId},messageTypes={_messageTypes},keyword={keyword},conversations={_conversations}");
                engine.OnConversationsSearched?.Invoke(code, _conversationTypes, channelId, _messageTypes, keyword,
                                                       _conversations);
            }
    
            public void onGroupReadReceiptRequestSent(int code, AndroidJavaObject message)
            {
                RCIMMessage _message = null;
                if (message != null)
                    _message = (RCIMMessage)MessageConverter.from(message).getCSharpObject();
                RCUnityLogger.getInstance().log("OnGroupReadReceiptRequestSent", $"code={code},message={_message}");
                engine.OnGroupReadReceiptRequestSent?.Invoke(code, _message);
            }
    
            public void onGroupReadReceiptResponseSent(int code, string targetId, string channelId,
                                                       AndroidJavaObject messages)
            {
                List<RCIMMessage> _messages = null;
                if (messages != null)
                {
                    _messages = new List<RCIMMessage>();
                    AndroidJavaObject iterator = messages.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _messages.Add((RCIMMessage)MessageConverter.from(value).getCSharpObject());
                    }
                }
                RCUnityLogger.getInstance().log(
                    "OnGroupReadReceiptResponseSent",
                    $"code={code},targetId={targetId},channelId={channelId},messages={_messages}");
                engine.OnGroupReadReceiptResponseSent?.Invoke(code, targetId, channelId, _messages);
            }
    
            public void onNotificationQuietHoursChanged(int code, string startTime, int spanMinutes,
                                                        AndroidJavaObject level)
            {
                RCIMPushNotificationQuietHoursLevel _level =
                    new PushNotificationQuietHoursLevelConverter(level).getCSharpObject();
                RCUnityLogger.getInstance().log(
                    "OnNotificationQuietHoursChanged",
                    $"code={code},startTime={startTime},spanMinutes={spanMinutes},level={_level}");
                engine.OnNotificationQuietHoursChanged?.Invoke(code, startTime, spanMinutes, _level);
            }
    
            public void onNotificationQuietHoursRemoved(int code)
            {
                RCUnityLogger.getInstance().log("OnNotificationQuietHoursRemoved", $"code={code}");
                engine.OnNotificationQuietHoursRemoved?.Invoke(code);
            }
    
            public void onNotificationQuietHoursLoaded(int code, string startTime, int spanMinutes, AndroidJavaObject level)
            {
                RCIMPushNotificationQuietHoursLevel _level =
                    new PushNotificationQuietHoursLevelConverter(level).getCSharpObject();
                RCUnityLogger.getInstance().log(
                    "OnNotificationQuietHoursLoaded",
                    $"code={code},startTime={startTime},spanMinutes={spanMinutes},level={_level}");
                engine.OnNotificationQuietHoursLoaded?.Invoke(code, startTime, spanMinutes, _level);
            }
    
            public void onConversationNotificationLevelChanged(int code, AndroidJavaObject type, string targetId,
                                                               string channelId, AndroidJavaObject level)
            {
                RCIMConversationType _type = new ConversationTypeConverter(type).getCSharpObject();
                RCIMPushNotificationLevel _level = new PushNotificationLevelConverter(level).getCSharpObject();
                RCUnityLogger.getInstance().log(
                    "OnConversationNotificationLevelChanged",
                    $"code={code},type={_type},targetId={targetId},channelId={channelId},level={_level}");
                engine.OnConversationNotificationLevelChanged?.Invoke(code, _type, targetId, channelId, _level);
            }
    
            public void onConversationNotificationLevelLoaded(int code, AndroidJavaObject type, string targetId,
                                                              string channelId, AndroidJavaObject level)
            {
                RCIMConversationType _type = new ConversationTypeConverter(type).getCSharpObject();
                RCIMPushNotificationLevel _level = new PushNotificationLevelConverter(level).getCSharpObject();
                RCUnityLogger.getInstance().log(
                    "OnConversationNotificationLevelLoaded",
                    $"code={code},type={_type},targetId={targetId},channelId={channelId},level={_level}");
                engine.OnConversationNotificationLevelLoaded?.Invoke(code, _type, targetId, channelId, _level);
            }
    
            public void onConversationTypeNotificationLevelChanged(int code, AndroidJavaObject type,
                                                                   AndroidJavaObject level)
            {
                RCIMConversationType _type = new ConversationTypeConverter(type).getCSharpObject();
                RCIMPushNotificationLevel _level = new PushNotificationLevelConverter(level).getCSharpObject();
                RCUnityLogger.getInstance().log("OnConversationTypeNotificationLevelChanged",
                                                $"code={code},type={_type},level={_level}");
                engine.OnConversationTypeNotificationLevelChanged?.Invoke(code, _type, _level);
            }
    
            public void onConversationTypeNotificationLevelLoaded(int code, AndroidJavaObject type, AndroidJavaObject level)
            {
                RCIMConversationType _type = new ConversationTypeConverter(type).getCSharpObject();
                RCIMPushNotificationLevel _level = new PushNotificationLevelConverter(level).getCSharpObject();
                RCUnityLogger.getInstance().log("OnConversationTypeNotificationLevelLoaded",
                                                $"code={code},type={_type},level={_level}");
                engine.OnConversationTypeNotificationLevelLoaded?.Invoke(code, _type, _level);
            }
    
            public void onUltraGroupDefaultNotificationLevelChanged(int code, string targetId, AndroidJavaObject level)
            {
                RCIMPushNotificationLevel _level = new PushNotificationLevelConverter(level).getCSharpObject();
                RCUnityLogger.getInstance().log("OnUltraGroupDefaultNotificationLevelChanged",
                                                $"code={code},targetId={targetId},level={_level}");
                engine.OnUltraGroupDefaultNotificationLevelChanged?.Invoke(code, targetId, _level);
            }
    
            public void onUltraGroupDefaultNotificationLevelLoaded(int code, string targetId, AndroidJavaObject level)
            {
                RCIMPushNotificationLevel _level = new PushNotificationLevelConverter(level).getCSharpObject();
                RCUnityLogger.getInstance().log("OnUltraGroupDefaultNotificationLevelLoaded",
                                                $"code={code},targetId={targetId},level={_level}");
                engine.OnUltraGroupDefaultNotificationLevelLoaded?.Invoke(code, targetId, _level);
            }
    
            public void onUltraGroupChannelDefaultNotificationLevelChanged(int code, string targetId, string channelId,
                                                                           AndroidJavaObject level)
            {
                RCIMPushNotificationLevel _level = new PushNotificationLevelConverter(level).getCSharpObject();
                RCUnityLogger.getInstance().log("OnUltraGroupChannelDefaultNotificationLevelChanged",
                                                $"code={code},targetId={targetId},channelId={channelId},level={_level}");
                engine.OnUltraGroupChannelDefaultNotificationLevelChanged?.Invoke(code, targetId, channelId, _level);
            }
    
            public void onUltraGroupChannelDefaultNotificationLevelLoaded(int code, string targetId, string channelId,
                                                                          AndroidJavaObject level)
            {
                RCIMPushNotificationLevel _level = new PushNotificationLevelConverter(level).getCSharpObject();
                RCUnityLogger.getInstance().log("OnUltraGroupChannelDefaultNotificationLevelLoaded",
                                                $"code={code},targetId={targetId},channelId={channelId},level={_level}");
                engine.OnUltraGroupChannelDefaultNotificationLevelLoaded?.Invoke(code, targetId, channelId, _level);
            }
    
            public void onPushContentShowStatusChanged(int code, bool showContent)
            {
                RCUnityLogger.getInstance().log("OnPushContentShowStatusChanged", $"code={code},showContent={showContent}");
                engine.OnPushContentShowStatusChanged?.Invoke(code, showContent);
            }
    
            public void onPushLanguageChanged(int code, string language)
            {
                RCUnityLogger.getInstance().log("OnPushLanguageChanged", $"code={code},language={language}");
                engine.OnPushLanguageChanged?.Invoke(code, language);
            }
    
            public void onPushReceiveStatusChanged(int code, bool receive)
            {
                RCUnityLogger.getInstance().log("OnPushReceiveStatusChanged", $"code={code},receive={receive}");
                engine.OnPushReceiveStatusChanged?.Invoke(code, receive);
            }
    
            public void onMessageCountLoaded(int code, AndroidJavaObject type, string targetId, string channelId, int count)
            {
                RCIMConversationType _type = new ConversationTypeConverter(type).getCSharpObject();
                RCUnityLogger.getInstance().log(
                    "OnMessageCountLoaded",
                    $"code={code},type={_type},targetId={targetId},channelId={channelId},count={count}");
                engine.OnMessageCountLoaded?.Invoke(code, _type, targetId, channelId, count);
            }
    
            public void onTopConversationsLoaded(int code, AndroidJavaObject conversationTypes, string channelId,
                                                 AndroidJavaObject conversations)
            {
                List<RCIMConversationType> _conversationTypes = null;
                if (conversationTypes != null)
                {
                    _conversationTypes = new List<RCIMConversationType>();
                    AndroidJavaObject iterator = conversationTypes.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _conversationTypes.Add(new ConversationTypeConverter(value).getCSharpObject());
                    }
                }
                List<RCIMConversation> _conversations = null;
                if (conversations != null)
                {
                    _conversations = new List<RCIMConversation>();
                    AndroidJavaObject iterator = conversations.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _conversations.Add((RCIMConversation)ConversationConverter.from(value).getCSharpObject());
                    }
                }
                RCUnityLogger.getInstance().log(
                    "OnTopConversationsLoaded",
                    $"code={code},conversationTypes={_conversationTypes},channelId={channelId},conversations={_conversations}");
                engine.OnTopConversationsLoaded?.Invoke(code, _conversationTypes, channelId, _conversations);
            }
    
            public void onGroupMessageToDesignatedUsersAttached(AndroidJavaObject message)
            {
                RCIMMessage _message = null;
                if (message != null)
                    _message = (RCIMMessage)MessageConverter.from(message).getCSharpObject();
                RCUnityLogger.getInstance().log("OnGroupMessageToDesignatedUsersAttached", $"message={_message}");
                engine.OnGroupMessageToDesignatedUsersAttached?.Invoke(_message);
            }
    
            public void onGroupMessageToDesignatedUsersSent(int code, AndroidJavaObject message)
            {
                RCIMMessage _message = null;
                if (message != null)
                    _message = (RCIMMessage)MessageConverter.from(message).getCSharpObject();
                RCUnityLogger.getInstance().log("OnGroupMessageToDesignatedUsersSent", $"code={code},message={_message}");
                engine.OnGroupMessageToDesignatedUsersSent?.Invoke(code, _message);
            }
    
            public void onUltraGroupReadStatusSynced(int code, string targetId, string channelId, long timestamp)
            {
                RCUnityLogger.getInstance().log(
                    "OnUltraGroupReadStatusSynced",
                    $"code={code},targetId={targetId},channelId={channelId},timestamp={timestamp}");
                engine.OnUltraGroupReadStatusSynced?.Invoke(code, targetId, channelId, timestamp);
            }
    
            public void onConversationsLoadedForAllChannel(int code, AndroidJavaObject type, string targetId,
                                                           AndroidJavaObject conversations)
            {
                RCIMConversationType _type = new ConversationTypeConverter(type).getCSharpObject();
                List<RCIMConversation> _conversations = null;
                if (conversations != null)
                {
                    _conversations = new List<RCIMConversation>();
                    AndroidJavaObject iterator = conversations.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _conversations.Add((RCIMConversation)ConversationConverter.from(value).getCSharpObject());
                    }
                }
                RCUnityLogger.getInstance().log(
                    "OnConversationsLoadedForAllChannel",
                    $"code={code},type={_type},targetId={targetId},conversations={_conversations}");
                engine.OnConversationsLoadedForAllChannel?.Invoke(code, _type, targetId, _conversations);
            }
    
            public void onUltraGroupUnreadMentionedCountLoaded(int code, string targetId, int count)
            {
                RCUnityLogger.getInstance().log("OnUltraGroupUnreadMentionedCountLoaded",
                                                $"code={code},targetId={targetId},count={count}");
                engine.OnUltraGroupUnreadMentionedCountLoaded?.Invoke(code, targetId, count);
            }
    
            public void onUltraGroupUnreadCountLoaded(int code, string targetId, int count)
            {
                RCUnityLogger.getInstance().log("OnUltraGroupUnreadCountLoaded",
                                                $"code={code},targetId={targetId},count={count}");
                engine.OnUltraGroupUnreadCountLoaded?.Invoke(code, targetId, count);
            }
    
            public void onUltraGroupMessageModified(int code, string messageUId)
            {
                RCUnityLogger.getInstance().log("OnUltraGroupMessageModified", $"code={code},messageUId={messageUId}");
                engine.OnUltraGroupMessageModified?.Invoke(code, messageUId);
            }
    
            public void onUltraGroupMessageRecalled(int code, AndroidJavaObject message, bool deleteRemote)
            {
                RCIMMessage _message = null;
                if (message != null)
                    _message = (RCIMMessage)MessageConverter.from(message).getCSharpObject();
                RCUnityLogger.getInstance().log("OnUltraGroupMessageRecalled",
                                                $"code={code},message={_message},deleteRemote={deleteRemote}");
                engine.OnUltraGroupMessageRecalled?.Invoke(code, _message, deleteRemote);
            }
    
            public void onUltraGroupMessagesCleared(int code, string targetId, string channelId, long timestamp,
                                                    AndroidJavaObject policy)
            {
                RCIMMessageOperationPolicy _policy = new MessageOperationPolicyConverter(policy).getCSharpObject();
                RCUnityLogger.getInstance().log(
                    "OnUltraGroupMessagesCleared",
                    $"code={code},targetId={targetId},channelId={channelId},timestamp={timestamp},policy={_policy}");
                engine.OnUltraGroupMessagesCleared?.Invoke(code, targetId, channelId, timestamp, _policy);
            }
    
            public void onUltraGroupMessagesClearedForAllChannel(int code, string targetId, long timestamp)
            {
                RCUnityLogger.getInstance().log("OnUltraGroupMessagesClearedForAllChannel",
                                                $"code={code},targetId={targetId},timestamp={timestamp}");
                engine.OnUltraGroupMessagesClearedForAllChannel?.Invoke(code, targetId, timestamp);
            }
    
            public void onUltraGroupTypingStatusSent(int code, string targetId, string channelId,
                                                     AndroidJavaObject typingStatus)
            {
                RCIMUltraGroupTypingStatus _typingStatus =
                    new UltraGroupTypingStatusConverter(typingStatus).getCSharpObject();
                RCUnityLogger.getInstance().log(
                    "OnUltraGroupTypingStatusSent",
                    $"code={code},targetId={targetId},channelId={channelId},typingStatus={_typingStatus}");
                engine.OnUltraGroupTypingStatusSent?.Invoke(code, targetId, channelId, _typingStatus);
            }
    
            public void onBatchRemoteUltraGroupMessagesLoaded(int code, AndroidJavaObject matchedMessages,
                                                              AndroidJavaObject notMatchedMessages)
            {
                List<RCIMMessage> _matchedMessages = null;
                if (matchedMessages != null)
                {
                    _matchedMessages = new List<RCIMMessage>();
                    AndroidJavaObject iterator = matchedMessages.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _matchedMessages.Add((RCIMMessage)MessageConverter.from(value).getCSharpObject());
                    }
                }
                List<RCIMMessage> _notMatchedMessages = null;
                if (notMatchedMessages != null)
                {
                    _notMatchedMessages = new List<RCIMMessage>();
                    AndroidJavaObject iterator = notMatchedMessages.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject value = iterator.Call<AndroidJavaObject>("next");
                        if (value != null)
                            _notMatchedMessages.Add((RCIMMessage)MessageConverter.from(value).getCSharpObject());
                    }
                }
                RCUnityLogger.getInstance().log(
                    "OnBatchRemoteUltraGroupMessagesLoaded",
                    $"code={code},matchedMessages={_matchedMessages},notMatchedMessages={_notMatchedMessages}");
                engine.OnBatchRemoteUltraGroupMessagesLoaded?.Invoke(code, _matchedMessages, _notMatchedMessages);
            }
    
            public void onUltraGroupMessageExpansionUpdated(int code, AndroidJavaObject expansion, string messageUId)
            {
                Dictionary<string, string> _expansion = null;
                if (expansion != null)
                {
                    _expansion = new Dictionary<string, string>();
                    AndroidJavaObject iterator =
                        expansion.Call<AndroidJavaObject>("entrySet").Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        AndroidJavaObject item = iterator.Call<AndroidJavaObject>("next");
                        string key = item.Call<string>("getKey");
                        string value = item.Call<string>("getValue");
                        _expansion.Add(key, value);
                    }
                }
                RCUnityLogger.getInstance().log("OnUltraGroupMessageExpansionUpdated",
                                                $"code={code},expansion={_expansion},messageUId={messageUId}");
                engine.OnUltraGroupMessageExpansionUpdated?.Invoke(code, _expansion, messageUId);
            }
    
            public void onUltraGroupMessageExpansionForKeysRemoved(int code, string messageUId, AndroidJavaObject keys)
            {
                List<string> _keys = null;
                if (keys != null)
                {
                    _keys = new List<string>();
                    AndroidJavaObject iterator = keys.Call<AndroidJavaObject>("iterator");
                    while (iterator.Call<bool>("hasNext"))
                    {
                        _keys.Add(iterator.Call<string>("next"));
                    }
                }
                RCUnityLogger.getInstance().log("OnUltraGroupMessageExpansionForKeysRemoved",
                                                $"code={code},messageUId={messageUId},keys={_keys}");
                engine.OnUltraGroupMessageExpansionForKeysRemoved?.Invoke(code, messageUId, _keys);
            }
        }
    }
}
    
#endif