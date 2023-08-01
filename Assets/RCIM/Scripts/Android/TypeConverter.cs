#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    
    public class Converter
    {
        protected object c_intance;
        protected AndroidJavaObject j_intance;
        private static AndroidJavaClass UtilsClass = new AndroidJavaClass("cn.rongcloud.im.wrapper.unity.Utils");
        public static bool isInstance(AndroidJavaObject obj, string javaClass)
        {
            return UtilsClass.CallStatic<bool>("isInstanceOf", obj, javaClass);
        }
    
        public AndroidJavaObject getAndroidObject()
        {
            return this.j_intance;
        }
        public object getCSharpObject()
        {
            return this.c_intance;
        }
    
        public virtual void toAndroidObject()
        {
        }
        public virtual void toCSharpObject()
        {
        }
    }
    public class AndroidPushOptionsConverter : Converter
    {
        public AndroidPushOptionsConverter(RCIMAndroidPushOptions options, string javaClass)
        {
            this.c_intance = options;
            this.j_intance = new AndroidJavaObject(javaClass);
            this.toAndroidObject();
        }
    
        public AndroidPushOptionsConverter(AndroidJavaObject options, object _c_intance)
        {
            this.j_intance = options;
            this.c_intance = _c_intance;
            this.toCSharpObject();
        }
    
        public static AndroidPushOptionsConverter from(RCIMAndroidPushOptions options)
        {
            AndroidPushOptionsConverter converter =
                new AndroidPushOptionsConverter(options, "cn.rongcloud.im.wrapper.options.RCIMIWAndroidPushOptions");
            return converter;
        }
    
        public static AndroidPushOptionsConverter from(AndroidJavaObject options)
        {
            AndroidPushOptionsConverter converter = new AndroidPushOptionsConverter(options, new RCIMAndroidPushOptions());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMAndroidPushOptions __instance = (RCIMAndroidPushOptions)this.c_intance;
            string _notificationId = __instance.notificationId;
            string _channelIdMi = __instance.channelIdMi;
            string _channelIdHW = __instance.channelIdHW;
            string _categoryHW = __instance.categoryHW;
            string _channelIdOPPO = __instance.channelIdOPPO;
            RCIMVIVOPushType _pushTypeVIVO = __instance.pushTypeVIVO;
            string _collapseKeyFCM = __instance.collapseKeyFCM;
            string _imageUrlFCM = __instance.imageUrlFCM;
            RCIMImportanceHW _importanceHW = __instance.importanceHW;
            string _imageUrlHW = __instance.imageUrlHW;
            string _imageUrlMi = __instance.imageUrlMi;
            string _channelIdFCM = __instance.channelIdFCM;
            string _categoryVivo = __instance.categoryVivo;
    
            if (_notificationId != null)
                j_intance.Set<string>("notificationId", _notificationId);
            if (_channelIdMi != null)
                j_intance.Set<string>("channelIdMi", _channelIdMi);
            if (_channelIdHW != null)
                j_intance.Set<string>("channelIdHW", _channelIdHW);
            if (_categoryHW != null)
                j_intance.Set<string>("categoryHW", _categoryHW);
            if (_channelIdOPPO != null)
                j_intance.Set<string>("channelIdOPPO", _channelIdOPPO);
            j_intance.Set<AndroidJavaObject>("pushTypeVIVO", new VIVOPushTypeConverter(_pushTypeVIVO).getAndroidObject());
            if (_collapseKeyFCM != null)
                j_intance.Set<string>("collapseKeyFCM", _collapseKeyFCM);
            if (_imageUrlFCM != null)
                j_intance.Set<string>("imageUrlFCM", _imageUrlFCM);
            j_intance.Set<AndroidJavaObject>("importanceHW", new ImportanceHWConverter(_importanceHW).getAndroidObject());
            if (_imageUrlHW != null)
                j_intance.Set<string>("imageUrlHW", _imageUrlHW);
            if (_imageUrlMi != null)
                j_intance.Set<string>("imageUrlMi", _imageUrlMi);
            if (_channelIdFCM != null)
                j_intance.Set<string>("channelIdFCM", _channelIdFCM);
            if (_categoryVivo != null)
                j_intance.Set<string>("categoryVivo", _categoryVivo);
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMAndroidPushOptions __instance = (RCIMAndroidPushOptions)this.c_intance;
            __instance.notificationId = j_intance.Get<string>("notificationId");
            __instance.channelIdMi = j_intance.Get<string>("channelIdMi");
            __instance.channelIdHW = j_intance.Get<string>("channelIdHW");
            __instance.categoryHW = j_intance.Get<string>("categoryHW");
            __instance.channelIdOPPO = j_intance.Get<string>("channelIdOPPO");
            AndroidJavaObject _pushTypeVIVO = j_intance.Get<AndroidJavaObject>("pushTypeVIVO");
            if (_pushTypeVIVO != null)
                __instance.pushTypeVIVO = new VIVOPushTypeConverter(_pushTypeVIVO).getCSharpObject();
            __instance.collapseKeyFCM = j_intance.Get<string>("collapseKeyFCM");
            __instance.imageUrlFCM = j_intance.Get<string>("imageUrlFCM");
            AndroidJavaObject _importanceHW = j_intance.Get<AndroidJavaObject>("importanceHW");
            if (_importanceHW != null)
                __instance.importanceHW = new ImportanceHWConverter(_importanceHW).getCSharpObject();
            __instance.imageUrlHW = j_intance.Get<string>("imageUrlHW");
            __instance.imageUrlMi = j_intance.Get<string>("imageUrlMi");
            __instance.channelIdFCM = j_intance.Get<string>("channelIdFCM");
            __instance.categoryVivo = j_intance.Get<string>("categoryVivo");
        }
    }
    
    public class MessagePushOptionsConverter : Converter
    {
        public MessagePushOptionsConverter(RCIMMessagePushOptions options, string javaClass)
        {
            this.c_intance = options;
            this.j_intance = new AndroidJavaObject(javaClass);
            this.toAndroidObject();
        }
    
        public MessagePushOptionsConverter(AndroidJavaObject options, object _c_intance)
        {
            this.j_intance = options;
            this.c_intance = _c_intance;
            this.toCSharpObject();
        }
    
        public static MessagePushOptionsConverter from(RCIMMessagePushOptions options)
        {
            MessagePushOptionsConverter converter =
                new MessagePushOptionsConverter(options, "cn.rongcloud.im.wrapper.options.RCIMIWMessagePushOptions");
            return converter;
        }
    
        public static MessagePushOptionsConverter from(AndroidJavaObject options)
        {
            MessagePushOptionsConverter converter = new MessagePushOptionsConverter(options, new RCIMMessagePushOptions());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMMessagePushOptions __instance = (RCIMMessagePushOptions)this.c_intance;
            bool _disableNotification = __instance.disableNotification;
            bool _disablePushTitle = __instance.disablePushTitle;
            string _pushTitle = __instance.pushTitle;
            string _pushContent = __instance.pushContent;
            string _pushData = __instance.pushData;
            bool _forceShowDetailContent = __instance.forceShowDetailContent;
            string _templateId = __instance.templateId;
            bool _voIPPush = __instance.voIPPush;
            RCIMIOSPushOptions _iOSPushOptions = __instance.iOSPushOptions;
            RCIMAndroidPushOptions _androidPushOptions = __instance.androidPushOptions;
    
            j_intance.Set<bool>("disableNotification", _disableNotification);
            j_intance.Set<bool>("disablePushTitle", _disablePushTitle);
            if (_pushTitle != null)
                j_intance.Set<string>("pushTitle", _pushTitle);
            if (_pushContent != null)
                j_intance.Set<string>("pushContent", _pushContent);
            if (_pushData != null)
                j_intance.Set<string>("pushData", _pushData);
            j_intance.Set<bool>("forceShowDetailContent", _forceShowDetailContent);
            if (_templateId != null)
                j_intance.Set<string>("templateId", _templateId);
            j_intance.Set<bool>("voIPPush", _voIPPush);
            if (_iOSPushOptions != null)
                j_intance.Set<AndroidJavaObject>("iOSPushOptions",
                                                 IOSPushOptionsConverter.from(_iOSPushOptions).getAndroidObject());
    
            if (_androidPushOptions != null)
                j_intance.Set<AndroidJavaObject>("androidPushOptions",
                                                 AndroidPushOptionsConverter.from(_androidPushOptions).getAndroidObject());
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMMessagePushOptions __instance = (RCIMMessagePushOptions)this.c_intance;
            __instance.disableNotification = j_intance.Get<bool>("disableNotification");
            __instance.disablePushTitle = j_intance.Get<bool>("disablePushTitle");
            __instance.pushTitle = j_intance.Get<string>("pushTitle");
            __instance.pushContent = j_intance.Get<string>("pushContent");
            __instance.pushData = j_intance.Get<string>("pushData");
            __instance.forceShowDetailContent = j_intance.Get<bool>("forceShowDetailContent");
            __instance.templateId = j_intance.Get<string>("templateId");
            __instance.voIPPush = j_intance.Get<bool>("voIPPush");
            AndroidJavaObject _iOSPushOptions = j_intance.Get<AndroidJavaObject>("iOSPushOptions");
            if (_iOSPushOptions != null)
                __instance.iOSPushOptions =
                    (RCIMIOSPushOptions)IOSPushOptionsConverter.from(_iOSPushOptions).getCSharpObject();
            AndroidJavaObject _androidPushOptions = j_intance.Get<AndroidJavaObject>("androidPushOptions");
            if (_androidPushOptions != null)
                __instance.androidPushOptions =
                    (RCIMAndroidPushOptions)AndroidPushOptionsConverter.from(_androidPushOptions).getCSharpObject();
        }
    }
    
    public class IOSPushOptionsConverter : Converter
    {
        public IOSPushOptionsConverter(RCIMIOSPushOptions options, string javaClass)
        {
            this.c_intance = options;
            this.j_intance = new AndroidJavaObject(javaClass);
            this.toAndroidObject();
        }
    
        public IOSPushOptionsConverter(AndroidJavaObject options, object _c_intance)
        {
            this.j_intance = options;
            this.c_intance = _c_intance;
            this.toCSharpObject();
        }
    
        public static IOSPushOptionsConverter from(RCIMIOSPushOptions options)
        {
            IOSPushOptionsConverter converter =
                new IOSPushOptionsConverter(options, "cn.rongcloud.im.wrapper.options.RCIMIWIOSPushOptions");
            return converter;
        }
    
        public static IOSPushOptionsConverter from(AndroidJavaObject options)
        {
            IOSPushOptionsConverter converter = new IOSPushOptionsConverter(options, new RCIMIOSPushOptions());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMIOSPushOptions __instance = (RCIMIOSPushOptions)this.c_intance;
            string _threadId = __instance.threadId;
            string _category = __instance.category;
            string _apnsCollapseId = __instance.apnsCollapseId;
            string _richMediaUri = __instance.richMediaUri;
    
            if (_threadId != null)
                j_intance.Set<string>("threadId", _threadId);
            if (_category != null)
                j_intance.Set<string>("category", _category);
            if (_apnsCollapseId != null)
                j_intance.Set<string>("apnsCollapseId", _apnsCollapseId);
            if (_richMediaUri != null)
                j_intance.Set<string>("richMediaUri", _richMediaUri);
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMIOSPushOptions __instance = (RCIMIOSPushOptions)this.c_intance;
            __instance.threadId = j_intance.Get<string>("threadId");
            __instance.category = j_intance.Get<string>("category");
            __instance.apnsCollapseId = j_intance.Get<string>("apnsCollapseId");
            __instance.richMediaUri = j_intance.Get<string>("richMediaUri");
        }
    }
    
    public class CompressOptionsConverter : Converter
    {
        public CompressOptionsConverter(RCIMCompressOptions options, string javaClass)
        {
            this.c_intance = options;
            this.j_intance = new AndroidJavaObject(javaClass);
            this.toAndroidObject();
        }
    
        public CompressOptionsConverter(AndroidJavaObject options, object _c_intance)
        {
            this.j_intance = options;
            this.c_intance = _c_intance;
            this.toCSharpObject();
        }
    
        public static CompressOptionsConverter from(RCIMCompressOptions options)
        {
            CompressOptionsConverter converter =
                new CompressOptionsConverter(options, "cn.rongcloud.im.wrapper.options.RCIMIWCompressOptions");
            return converter;
        }
    
        public static CompressOptionsConverter from(AndroidJavaObject options)
        {
            CompressOptionsConverter converter = new CompressOptionsConverter(options, new RCIMCompressOptions());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMCompressOptions __instance = (RCIMCompressOptions)this.c_intance;
            int _originalImageQuality = __instance.originalImageQuality;
            int _originalImageSize = __instance.originalImageSize;
            int _originalImageMaxSize = __instance.originalImageMaxSize;
            int _thumbnailQuality = __instance.thumbnailQuality;
            int _thumbnailMaxSize = __instance.thumbnailMaxSize;
            int _thumbnailMinSize = __instance.thumbnailMinSize;
            int _sightCompressWidth = __instance.sightCompressWidth;
            int _sightCompressHeight = __instance.sightCompressHeight;
            int _locationThumbnailQuality = __instance.locationThumbnailQuality;
            int _locationThumbnailWidth = __instance.locationThumbnailWidth;
            int _locationThumbnailHeight = __instance.locationThumbnailHeight;
    
            j_intance.Set<int>("originalImageQuality", _originalImageQuality);
            j_intance.Set<int>("originalImageSize", _originalImageSize);
            j_intance.Set<int>("originalImageMaxSize", _originalImageMaxSize);
            j_intance.Set<int>("thumbnailQuality", _thumbnailQuality);
            j_intance.Set<int>("thumbnailMaxSize", _thumbnailMaxSize);
            j_intance.Set<int>("thumbnailMinSize", _thumbnailMinSize);
            j_intance.Set<int>("sightCompressWidth", _sightCompressWidth);
            j_intance.Set<int>("sightCompressHeight", _sightCompressHeight);
            j_intance.Set<int>("locationThumbnailQuality", _locationThumbnailQuality);
            j_intance.Set<int>("locationThumbnailWidth", _locationThumbnailWidth);
            j_intance.Set<int>("locationThumbnailHeight", _locationThumbnailHeight);
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMCompressOptions __instance = (RCIMCompressOptions)this.c_intance;
            __instance.originalImageQuality = j_intance.Get<int>("originalImageQuality");
            __instance.originalImageSize = j_intance.Get<int>("originalImageSize");
            __instance.originalImageMaxSize = j_intance.Get<int>("originalImageMaxSize");
            __instance.thumbnailQuality = j_intance.Get<int>("thumbnailQuality");
            __instance.thumbnailMaxSize = j_intance.Get<int>("thumbnailMaxSize");
            __instance.thumbnailMinSize = j_intance.Get<int>("thumbnailMinSize");
            __instance.sightCompressWidth = j_intance.Get<int>("sightCompressWidth");
            __instance.sightCompressHeight = j_intance.Get<int>("sightCompressHeight");
            __instance.locationThumbnailQuality = j_intance.Get<int>("locationThumbnailQuality");
            __instance.locationThumbnailWidth = j_intance.Get<int>("locationThumbnailWidth");
            __instance.locationThumbnailHeight = j_intance.Get<int>("locationThumbnailHeight");
        }
    }
    
    public class EngineOptionsConverter : Converter
    {
        public EngineOptionsConverter(RCIMEngineOptions options, string javaClass)
        {
            this.c_intance = options;
            this.j_intance = new AndroidJavaObject(javaClass);
            this.toAndroidObject();
        }
    
        public EngineOptionsConverter(AndroidJavaObject options, object _c_intance)
        {
            this.j_intance = options;
            this.c_intance = _c_intance;
            this.toCSharpObject();
        }
    
        public static EngineOptionsConverter from(RCIMEngineOptions options)
        {
            EngineOptionsConverter converter =
                new EngineOptionsConverter(options, "cn.rongcloud.im.wrapper.options.RCIMIWEngineOptions");
            return converter;
        }
    
        public static EngineOptionsConverter from(AndroidJavaObject options)
        {
            EngineOptionsConverter converter = new EngineOptionsConverter(options, new RCIMEngineOptions());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMEngineOptions __instance = (RCIMEngineOptions)this.c_intance;
            string _naviServer = __instance.naviServer;
            string _fileServer = __instance.fileServer;
            string _statisticServer = __instance.statisticServer;
            bool _kickReconnectDevice = __instance.kickReconnectDevice;
            RCIMCompressOptions _compressOptions = __instance.compressOptions;
            RCIMLogLevel _logLevel = __instance.logLevel;
            RCIMPushOptions _pushOptions = __instance.pushOptions;
            bool _enablePush = __instance.enablePush;
            bool _enableIPC = __instance.enableIPC;
    
            if (_naviServer != null)
                j_intance.Set<string>("naviServer", _naviServer);
            if (_fileServer != null)
                j_intance.Set<string>("fileServer", _fileServer);
            if (_statisticServer != null)
                j_intance.Set<string>("statisticServer", _statisticServer);
            j_intance.Set<bool>("kickReconnectDevice", _kickReconnectDevice);
            if (_compressOptions != null)
                j_intance.Set<AndroidJavaObject>("compressOptions",
                                                 CompressOptionsConverter.from(_compressOptions).getAndroidObject());
    
            j_intance.Set<AndroidJavaObject>("logLevel", new LogLevelConverter(_logLevel).getAndroidObject());
            if (_pushOptions != null)
                j_intance.Set<AndroidJavaObject>("pushOptions", PushOptionsConverter.from(_pushOptions).getAndroidObject());
    
            j_intance.Set<bool>("enablePush", _enablePush);
            j_intance.Set<bool>("enableIPC", _enableIPC);
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMEngineOptions __instance = (RCIMEngineOptions)this.c_intance;
            __instance.naviServer = j_intance.Get<string>("naviServer");
            __instance.fileServer = j_intance.Get<string>("fileServer");
            __instance.statisticServer = j_intance.Get<string>("statisticServer");
            __instance.kickReconnectDevice = j_intance.Get<bool>("kickReconnectDevice");
            AndroidJavaObject _compressOptions = j_intance.Get<AndroidJavaObject>("compressOptions");
            if (_compressOptions != null)
                __instance.compressOptions =
                    (RCIMCompressOptions)CompressOptionsConverter.from(_compressOptions).getCSharpObject();
            AndroidJavaObject _logLevel = j_intance.Get<AndroidJavaObject>("logLevel");
            if (_logLevel != null)
                __instance.logLevel = new LogLevelConverter(_logLevel).getCSharpObject();
            AndroidJavaObject _pushOptions = j_intance.Get<AndroidJavaObject>("pushOptions");
            if (_pushOptions != null)
                __instance.pushOptions = (RCIMPushOptions)PushOptionsConverter.from(_pushOptions).getCSharpObject();
            __instance.enablePush = j_intance.Get<bool>("enablePush");
            __instance.enableIPC = j_intance.Get<bool>("enableIPC");
        }
    }
    
    public class UnknownMessageConverter : MessageConverter
    {
        public UnknownMessageConverter(RCIMUnknownMessage message, string javaClass) : base(message, javaClass)
        {
        }
    
        public UnknownMessageConverter(AndroidJavaObject message, object _c_intance) : base(message, _c_intance)
        {
        }
    
        public static UnknownMessageConverter from(RCIMUnknownMessage message)
        {
            UnknownMessageConverter converter =
                new UnknownMessageConverter(message, "cn.rongcloud.im.wrapper.messages.RCIMIWUnknownMessage");
            return converter;
        }
    
        public static UnknownMessageConverter from(AndroidJavaObject message)
        {
            UnknownMessageConverter converter = new UnknownMessageConverter(message, new RCIMUnknownMessage());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMUnknownMessage __instance = (RCIMUnknownMessage)this.c_intance;
            string _rawData = __instance.rawData;
            string _objectName = __instance.objectName;
    
            if (_rawData != null)
                j_intance.Set<string>("rawData", _rawData);
            if (_objectName != null)
                j_intance.Set<string>("objectName", _objectName);
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMUnknownMessage __instance = (RCIMUnknownMessage)this.c_intance;
            __instance.rawData = j_intance.Get<string>("rawData");
            __instance.objectName = j_intance.Get<string>("objectName");
        }
    }
    
    public class UserInfoConverter : Converter
    {
        public UserInfoConverter(RCIMUserInfo info, string javaClass)
        {
            this.c_intance = info;
            this.j_intance = new AndroidJavaObject(javaClass);
            this.toAndroidObject();
        }
    
        public UserInfoConverter(AndroidJavaObject info, object _c_intance)
        {
            this.j_intance = info;
            this.c_intance = _c_intance;
            this.toCSharpObject();
        }
    
        public static UserInfoConverter from(RCIMUserInfo info)
        {
            UserInfoConverter converter = new UserInfoConverter(info, "cn.rongcloud.im.wrapper.messages.RCIMIWUserInfo");
            return converter;
        }
    
        public static UserInfoConverter from(AndroidJavaObject info)
        {
            UserInfoConverter converter = new UserInfoConverter(info, new RCIMUserInfo());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMUserInfo __instance = (RCIMUserInfo)this.c_intance;
            string _userId = __instance.userId;
            string _name = __instance.name;
            string _portrait = __instance.portrait;
            string _alias = __instance.alias;
            string _extra = __instance.extra;
    
            if (_userId != null)
                j_intance.Set<string>("userId", _userId);
            if (_name != null)
                j_intance.Set<string>("name", _name);
            if (_portrait != null)
                j_intance.Set<string>("portrait", _portrait);
            if (_alias != null)
                j_intance.Set<string>("alias", _alias);
            if (_extra != null)
                j_intance.Set<string>("extra", _extra);
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMUserInfo __instance = (RCIMUserInfo)this.c_intance;
            __instance.userId = j_intance.Get<string>("userId");
            __instance.name = j_intance.Get<string>("name");
            __instance.portrait = j_intance.Get<string>("portrait");
            __instance.alias = j_intance.Get<string>("alias");
            __instance.extra = j_intance.Get<string>("extra");
        }
    }
    
    public class CustomMessageConverter : MessageConverter
    {
        public CustomMessageConverter(RCIMCustomMessage message, string javaClass) : base(message, javaClass)
        {
        }
    
        public CustomMessageConverter(AndroidJavaObject message, object _c_intance) : base(message, _c_intance)
        {
        }
    
        public static CustomMessageConverter from(RCIMCustomMessage message)
        {
            CustomMessageConverter converter =
                new CustomMessageConverter(message, "cn.rongcloud.im.wrapper.messages.RCIMIWCustomMessage");
            return converter;
        }
    
        public static CustomMessageConverter from(AndroidJavaObject message)
        {
            CustomMessageConverter converter = new CustomMessageConverter(message, new RCIMCustomMessage());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMCustomMessage __instance = (RCIMCustomMessage)this.c_intance;
            string _identifier = __instance.identifier;
            RCIMCustomMessagePolicy _policy = __instance.policy;
            Dictionary<string, string> _fields = __instance.fields;
    
            if (_identifier != null)
                j_intance.Set<string>("identifier", _identifier);
            j_intance.Set<AndroidJavaObject>("policy", new CustomMessagePolicyConverter(_policy).getAndroidObject());
            if (_fields != null)
            {
                AndroidJavaObject _map = new AndroidJavaObject("java.util.HashMap");
                foreach (var item in _fields)
                    _map.Call<string>("put", item.Key, item.Value);
                j_intance.Set<AndroidJavaObject>("fields", _map);
            }
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMCustomMessage __instance = (RCIMCustomMessage)this.c_intance;
            __instance.identifier = j_intance.Get<string>("identifier");
            AndroidJavaObject _policy = j_intance.Get<AndroidJavaObject>("policy");
            if (_policy != null)
                __instance.policy = new CustomMessagePolicyConverter(_policy).getCSharpObject();
            AndroidJavaObject _fields = j_intance.Get<AndroidJavaObject>("fields");
            if (_fields != null)
            {
                Dictionary<string, string> fields = new Dictionary<string, string>();
                AndroidJavaObject iterator =
                    _fields.Call<AndroidJavaObject>("entrySet").Call<AndroidJavaObject>("iterator");
                while (iterator.Call<bool>("hasNext"))
                {
                    AndroidJavaObject item = iterator.Call<AndroidJavaObject>("next");
                    string key = item.Call<string>("getKey");
                    string value = item.Call<string>("getValue");
                    fields.Add(key, value);
                }
                __instance.fields = fields;
            }
        }
    }
    
    public class MessageConverter : Converter
    {
        public MessageConverter(RCIMMessage message, string javaClass)
        {
            this.c_intance = message;
            this.j_intance = new AndroidJavaObject(javaClass);
            this.toAndroidObject();
        }
    
        public MessageConverter(AndroidJavaObject message, object _c_intance)
        {
            this.j_intance = message;
            this.c_intance = _c_intance;
            this.toCSharpObject();
        }
    
        public static MessageConverter from(RCIMMessage message)
        {
            MessageConverter converter = null;
            if (message is RCIMUnknownMessage)
                converter = UnknownMessageConverter.from((RCIMUnknownMessage)message);
            else if (message is RCIMCustomMessage)
                converter = CustomMessageConverter.from((RCIMCustomMessage)message);
            else if (message is RCIMRecallNotificationMessage)
                converter = RecallNotificationMessageConverter.from((RCIMRecallNotificationMessage)message);
            else if (message is RCIMMediaMessage)
                converter = MediaMessageConverter.from((RCIMMediaMessage)message);
            else if (message is RCIMTextMessage)
                converter = TextMessageConverter.from((RCIMTextMessage)message);
            else if (message is RCIMCommandMessage)
                converter = CommandMessageConverter.from((RCIMCommandMessage)message);
            else if (message is RCIMCommandNotificationMessage)
                converter = CommandNotificationMessageConverter.from((RCIMCommandNotificationMessage)message);
            else if (message is RCIMLocationMessage)
                converter = LocationMessageConverter.from((RCIMLocationMessage)message);
            else if (message is RCIMReferenceMessage)
                converter = ReferenceMessageConverter.from((RCIMReferenceMessage)message);
            else if (message is RCIMMessage)
                converter = new MessageConverter(message, "cn.rongcloud.im.wrapper.messages.RCIMIWMessage");
            return converter;
        }
    
        public static MessageConverter from(AndroidJavaObject message)
        {
            MessageConverter converter = null;
            if (isInstance(message, "cn.rongcloud.im.wrapper.messages.RCIMIWUnknownMessage"))
                converter = UnknownMessageConverter.from(message);
            else if (isInstance(message, "cn.rongcloud.im.wrapper.messages.RCIMIWCustomMessage"))
                converter = CustomMessageConverter.from(message);
            else if (isInstance(message, "cn.rongcloud.im.wrapper.messages.RCIMIWRecallNotificationMessage"))
                converter = RecallNotificationMessageConverter.from(message);
            else if (isInstance(message, "cn.rongcloud.im.wrapper.messages.RCIMIWMediaMessage"))
                converter = MediaMessageConverter.from(message);
            else if (isInstance(message, "cn.rongcloud.im.wrapper.messages.RCIMIWTextMessage"))
                converter = TextMessageConverter.from(message);
            else if (isInstance(message, "cn.rongcloud.im.wrapper.messages.RCIMIWCommandMessage"))
                converter = CommandMessageConverter.from(message);
            else if (isInstance(message, "cn.rongcloud.im.wrapper.messages.RCIMIWCommandNotificationMessage"))
                converter = CommandNotificationMessageConverter.from(message);
            else if (isInstance(message, "cn.rongcloud.im.wrapper.messages.RCIMIWLocationMessage"))
                converter = LocationMessageConverter.from(message);
            else if (isInstance(message, "cn.rongcloud.im.wrapper.messages.RCIMIWReferenceMessage"))
                converter = ReferenceMessageConverter.from(message);
            else if (isInstance(message, "cn.rongcloud.im.wrapper.messages.RCIMIWMessage"))
                converter = new MessageConverter(message, new RCIMMessage());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMMessage __instance = (RCIMMessage)this.c_intance;
            RCIMConversationType _conversationType = __instance.conversationType;
            RCIMMessageType _messageType = __instance.messageType;
            string _targetId = __instance.targetId;
            string _channelId = __instance.channelId;
            int _messageId = __instance.messageId;
            string _messageUId = __instance.messageUId;
            bool _offLine = __instance.offLine;
            RCIMGroupReadReceiptInfo _groupReadReceiptInfo = __instance.groupReadReceiptInfo;
            long _receivedTime = __instance.receivedTime;
            long _sentTime = __instance.sentTime;
            RCIMReceivedStatus _receivedStatus = __instance.receivedStatus;
            RCIMSentStatus _sentStatus = __instance.sentStatus;
            string _senderUserId = __instance.senderUserId;
            RCIMMessageDirection _direction = __instance.direction;
            RCIMUserInfo _userInfo = __instance.userInfo;
            RCIMMentionedInfo _mentionedInfo = __instance.mentionedInfo;
            RCIMMessagePushOptions _pushOptions = __instance.pushOptions;
            string _extra = __instance.extra;
            Dictionary<string, string> _expansion = __instance.expansion;
    
            j_intance.Set<AndroidJavaObject>("conversationType",
                                             new ConversationTypeConverter(_conversationType).getAndroidObject());
            j_intance.Set<AndroidJavaObject>("messageType", new MessageTypeConverter(_messageType).getAndroidObject());
            if (_targetId != null)
                j_intance.Set<string>("targetId", _targetId);
            if (_channelId != null)
                j_intance.Set<string>("channelId", _channelId);
            j_intance.Set<int>("messageId", _messageId);
            if (_messageUId != null)
                j_intance.Set<string>("messageUId", _messageUId);
            j_intance.Set<bool>("offLine", _offLine);
            if (_groupReadReceiptInfo != null)
                j_intance.Set<AndroidJavaObject>(
                    "groupReadReceiptInfo", GroupReadReceiptInfoConverter.from(_groupReadReceiptInfo).getAndroidObject());
    
            j_intance.Set<long>("receivedTime", _receivedTime);
            j_intance.Set<long>("sentTime", _sentTime);
            j_intance.Set<AndroidJavaObject>("receivedStatus",
                                             new ReceivedStatusConverter(_receivedStatus).getAndroidObject());
            j_intance.Set<AndroidJavaObject>("sentStatus", new SentStatusConverter(_sentStatus).getAndroidObject());
            if (_senderUserId != null)
                j_intance.Set<string>("senderUserId", _senderUserId);
            j_intance.Set<AndroidJavaObject>("direction", new MessageDirectionConverter(_direction).getAndroidObject());
            if (_userInfo != null)
                j_intance.Set<AndroidJavaObject>("userInfo", UserInfoConverter.from(_userInfo).getAndroidObject());
    
            if (_mentionedInfo != null)
                j_intance.Set<AndroidJavaObject>("mentionedInfo",
                                                 MentionedInfoConverter.from(_mentionedInfo).getAndroidObject());
    
            if (_pushOptions != null)
                j_intance.Set<AndroidJavaObject>("pushOptions",
                                                 MessagePushOptionsConverter.from(_pushOptions).getAndroidObject());
    
            if (_extra != null)
                j_intance.Set<string>("extra", _extra);
            if (_expansion != null)
            {
                AndroidJavaObject _map = new AndroidJavaObject("java.util.HashMap");
                foreach (var item in _expansion)
                    _map.Call<string>("put", item.Key, item.Value);
                j_intance.Set<AndroidJavaObject>("expansion", _map);
            }
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMMessage __instance = (RCIMMessage)this.c_intance;
            AndroidJavaObject _conversationType = j_intance.Get<AndroidJavaObject>("conversationType");
            if (_conversationType != null)
                __instance.conversationType = new ConversationTypeConverter(_conversationType).getCSharpObject();
            AndroidJavaObject _messageType = j_intance.Get<AndroidJavaObject>("messageType");
            if (_messageType != null)
                __instance.messageType = new MessageTypeConverter(_messageType).getCSharpObject();
            __instance.targetId = j_intance.Get<string>("targetId");
            __instance.channelId = j_intance.Get<string>("channelId");
            __instance.messageId = j_intance.Get<int>("messageId");
            __instance.messageUId = j_intance.Get<string>("messageUId");
            __instance.offLine = j_intance.Get<bool>("offLine");
            AndroidJavaObject _groupReadReceiptInfo = j_intance.Get<AndroidJavaObject>("groupReadReceiptInfo");
            if (_groupReadReceiptInfo != null)
                __instance.groupReadReceiptInfo =
                    (RCIMGroupReadReceiptInfo)GroupReadReceiptInfoConverter.from(_groupReadReceiptInfo).getCSharpObject();
            __instance.receivedTime = j_intance.Get<long>("receivedTime");
            __instance.sentTime = j_intance.Get<long>("sentTime");
            AndroidJavaObject _receivedStatus = j_intance.Get<AndroidJavaObject>("receivedStatus");
            if (_receivedStatus != null)
                __instance.receivedStatus = new ReceivedStatusConverter(_receivedStatus).getCSharpObject();
            AndroidJavaObject _sentStatus = j_intance.Get<AndroidJavaObject>("sentStatus");
            if (_sentStatus != null)
                __instance.sentStatus = new SentStatusConverter(_sentStatus).getCSharpObject();
            __instance.senderUserId = j_intance.Get<string>("senderUserId");
            AndroidJavaObject _direction = j_intance.Get<AndroidJavaObject>("direction");
            if (_direction != null)
                __instance.direction = new MessageDirectionConverter(_direction).getCSharpObject();
            AndroidJavaObject _userInfo = j_intance.Get<AndroidJavaObject>("userInfo");
            if (_userInfo != null)
                __instance.userInfo = (RCIMUserInfo)UserInfoConverter.from(_userInfo).getCSharpObject();
            AndroidJavaObject _mentionedInfo = j_intance.Get<AndroidJavaObject>("mentionedInfo");
            if (_mentionedInfo != null)
                __instance.mentionedInfo = (RCIMMentionedInfo)MentionedInfoConverter.from(_mentionedInfo).getCSharpObject();
            AndroidJavaObject _pushOptions = j_intance.Get<AndroidJavaObject>("pushOptions");
            if (_pushOptions != null)
                __instance.pushOptions =
                    (RCIMMessagePushOptions)MessagePushOptionsConverter.from(_pushOptions).getCSharpObject();
            __instance.extra = j_intance.Get<string>("extra");
            AndroidJavaObject _expansion = j_intance.Get<AndroidJavaObject>("expansion");
            if (_expansion != null)
            {
                Dictionary<string, string> expansion = new Dictionary<string, string>();
                AndroidJavaObject iterator =
                    _expansion.Call<AndroidJavaObject>("entrySet").Call<AndroidJavaObject>("iterator");
                while (iterator.Call<bool>("hasNext"))
                {
                    AndroidJavaObject item = iterator.Call<AndroidJavaObject>("next");
                    string key = item.Call<string>("getKey");
                    string value = item.Call<string>("getValue");
                    expansion.Add(key, value);
                }
                __instance.expansion = expansion;
            }
        }
    }
    
    public class ImageMessageConverter : MediaMessageConverter
    {
        public ImageMessageConverter(RCIMImageMessage message, string javaClass) : base(message, javaClass)
        {
        }
    
        public ImageMessageConverter(AndroidJavaObject message, object _c_intance) : base(message, _c_intance)
        {
        }
    
        public static ImageMessageConverter from(RCIMImageMessage message)
        {
            ImageMessageConverter converter =
                new ImageMessageConverter(message, "cn.rongcloud.im.wrapper.messages.RCIMIWImageMessage");
            return converter;
        }
    
        public static ImageMessageConverter from(AndroidJavaObject message)
        {
            ImageMessageConverter converter = new ImageMessageConverter(message, new RCIMImageMessage());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMImageMessage __instance = (RCIMImageMessage)this.c_intance;
            string _thumbnailBase64String = __instance.thumbnailBase64String;
            bool _original = __instance.original;
    
            if (_thumbnailBase64String != null)
                j_intance.Set<string>("thumbnailBase64String", _thumbnailBase64String);
            j_intance.Set<bool>("original", _original);
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMImageMessage __instance = (RCIMImageMessage)this.c_intance;
            __instance.thumbnailBase64String = j_intance.Get<string>("thumbnailBase64String");
            __instance.original = j_intance.Get<bool>("original");
        }
    }
    
    public class FileMessageConverter : MediaMessageConverter
    {
        public FileMessageConverter(RCIMFileMessage message, string javaClass) : base(message, javaClass)
        {
        }
    
        public FileMessageConverter(AndroidJavaObject message, object _c_intance) : base(message, _c_intance)
        {
        }
    
        public static FileMessageConverter from(RCIMFileMessage message)
        {
            FileMessageConverter converter =
                new FileMessageConverter(message, "cn.rongcloud.im.wrapper.messages.RCIMIWFileMessage");
            return converter;
        }
    
        public static FileMessageConverter from(AndroidJavaObject message)
        {
            FileMessageConverter converter = new FileMessageConverter(message, new RCIMFileMessage());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMFileMessage __instance = (RCIMFileMessage)this.c_intance;
            string _name = __instance.name;
            string _fileType = __instance.fileType;
            long _size = __instance.size;
    
            if (_name != null)
                j_intance.Set<string>("name", _name);
            if (_fileType != null)
                j_intance.Set<string>("fileType", _fileType);
            j_intance.Set<long>("size", _size);
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMFileMessage __instance = (RCIMFileMessage)this.c_intance;
            __instance.name = j_intance.Get<string>("name");
            __instance.fileType = j_intance.Get<string>("fileType");
            __instance.size = j_intance.Get<long>("size");
        }
    }
    
    public class RecallNotificationMessageConverter : MessageConverter
    {
        public RecallNotificationMessageConverter(RCIMRecallNotificationMessage message, string javaClass)
            : base(message, javaClass)
        {
        }
    
        public RecallNotificationMessageConverter(AndroidJavaObject message, object _c_intance) : base(message, _c_intance)
        {
        }
    
        public static RecallNotificationMessageConverter from(RCIMRecallNotificationMessage message)
        {
            RecallNotificationMessageConverter converter = new RecallNotificationMessageConverter(
                message, "cn.rongcloud.im.wrapper.messages.RCIMIWRecallNotificationMessage");
            return converter;
        }
    
        public static RecallNotificationMessageConverter from(AndroidJavaObject message)
        {
            RecallNotificationMessageConverter converter =
                new RecallNotificationMessageConverter(message, new RCIMRecallNotificationMessage());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMRecallNotificationMessage __instance = (RCIMRecallNotificationMessage)this.c_intance;
            bool _admin = __instance.admin;
            bool _deleted = __instance.deleted;
            long _recallTime = __instance.recallTime;
            long _recallActionTime = __instance.recallActionTime;
            RCIMMessage _originalMessage = __instance.originalMessage;
    
            j_intance.Set<bool>("admin", _admin);
            j_intance.Set<bool>("deleted", _deleted);
            j_intance.Set<long>("recallTime", _recallTime);
            j_intance.Set<long>("recallActionTime", _recallActionTime);
            if (_originalMessage != null)
                j_intance.Set<AndroidJavaObject>("originalMessage",
                                                 MessageConverter.from(_originalMessage).getAndroidObject());
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMRecallNotificationMessage __instance = (RCIMRecallNotificationMessage)this.c_intance;
            __instance.admin = j_intance.Get<bool>("admin");
            __instance.deleted = j_intance.Get<bool>("deleted");
            __instance.recallTime = j_intance.Get<long>("recallTime");
            __instance.recallActionTime = j_intance.Get<long>("recallActionTime");
            AndroidJavaObject _originalMessage = j_intance.Get<AndroidJavaObject>("originalMessage");
            if (_originalMessage != null)
                __instance.originalMessage = (RCIMMessage)MessageConverter.from(_originalMessage).getCSharpObject();
        }
    }
    
    public class MediaMessageConverter : MessageConverter
    {
        public MediaMessageConverter(RCIMMediaMessage message, string javaClass) : base(message, javaClass)
        {
        }
    
        public MediaMessageConverter(AndroidJavaObject message, object _c_intance) : base(message, _c_intance)
        {
        }
    
        public static MediaMessageConverter from(RCIMMediaMessage message)
        {
            MediaMessageConverter converter = null;
            if (message is RCIMImageMessage)
                converter = ImageMessageConverter.from((RCIMImageMessage)message);
            else if (message is RCIMFileMessage)
                converter = FileMessageConverter.from((RCIMFileMessage)message);
            else if (message is RCIMGIFMessage)
                converter = GIFMessageConverter.from((RCIMGIFMessage)message);
            else if (message is RCIMVoiceMessage)
                converter = VoiceMessageConverter.from((RCIMVoiceMessage)message);
            else if (message is RCIMSightMessage)
                converter = SightMessageConverter.from((RCIMSightMessage)message);
            else if (message is RCIMMediaMessage)
                converter = new MediaMessageConverter(message, "cn.rongcloud.im.wrapper.messages.RCIMIWMediaMessage");
            return converter;
        }
    
        public static MediaMessageConverter from(AndroidJavaObject message)
        {
            MediaMessageConverter converter = null;
            if (isInstance(message, "cn.rongcloud.im.wrapper.messages.RCIMIWImageMessage"))
                converter = ImageMessageConverter.from(message);
            else if (isInstance(message, "cn.rongcloud.im.wrapper.messages.RCIMIWFileMessage"))
                converter = FileMessageConverter.from(message);
            else if (isInstance(message, "cn.rongcloud.im.wrapper.messages.RCIMIWGIFMessage"))
                converter = GIFMessageConverter.from(message);
            else if (isInstance(message, "cn.rongcloud.im.wrapper.messages.RCIMIWVoiceMessage"))
                converter = VoiceMessageConverter.from(message);
            else if (isInstance(message, "cn.rongcloud.im.wrapper.messages.RCIMIWSightMessage"))
                converter = SightMessageConverter.from(message);
            else if (isInstance(message, "cn.rongcloud.im.wrapper.messages.RCIMIWMediaMessage"))
                converter = new MediaMessageConverter(message, new RCIMMediaMessage());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMMediaMessage __instance = (RCIMMediaMessage)this.c_intance;
            string _local = __instance.local;
            string _remote = __instance.remote;
    
            if (_local != null)
                j_intance.Set<string>("local", _local);
            if (_remote != null)
                j_intance.Set<string>("remote", _remote);
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMMediaMessage __instance = (RCIMMediaMessage)this.c_intance;
            __instance.local = j_intance.Get<string>("local");
            __instance.remote = j_intance.Get<string>("remote");
        }
    }
    
    public class TextMessageConverter : MessageConverter
    {
        public TextMessageConverter(RCIMTextMessage message, string javaClass) : base(message, javaClass)
        {
        }
    
        public TextMessageConverter(AndroidJavaObject message, object _c_intance) : base(message, _c_intance)
        {
        }
    
        public static TextMessageConverter from(RCIMTextMessage message)
        {
            TextMessageConverter converter =
                new TextMessageConverter(message, "cn.rongcloud.im.wrapper.messages.RCIMIWTextMessage");
            return converter;
        }
    
        public static TextMessageConverter from(AndroidJavaObject message)
        {
            TextMessageConverter converter = new TextMessageConverter(message, new RCIMTextMessage());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMTextMessage __instance = (RCIMTextMessage)this.c_intance;
            string _text = __instance.text;
    
            if (_text != null)
                j_intance.Set<string>("text", _text);
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMTextMessage __instance = (RCIMTextMessage)this.c_intance;
            __instance.text = j_intance.Get<string>("text");
        }
    }
    
    public class GIFMessageConverter : MediaMessageConverter
    {
        public GIFMessageConverter(RCIMGIFMessage message, string javaClass) : base(message, javaClass)
        {
        }
    
        public GIFMessageConverter(AndroidJavaObject message, object _c_intance) : base(message, _c_intance)
        {
        }
    
        public static GIFMessageConverter from(RCIMGIFMessage message)
        {
            GIFMessageConverter converter =
                new GIFMessageConverter(message, "cn.rongcloud.im.wrapper.messages.RCIMIWGIFMessage");
            return converter;
        }
    
        public static GIFMessageConverter from(AndroidJavaObject message)
        {
            GIFMessageConverter converter = new GIFMessageConverter(message, new RCIMGIFMessage());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMGIFMessage __instance = (RCIMGIFMessage)this.c_intance;
            long _dataSize = __instance.dataSize;
            long _width = __instance.width;
            long _height = __instance.height;
    
            j_intance.Set<long>("dataSize", _dataSize);
            j_intance.Set<long>("width", _width);
            j_intance.Set<long>("height", _height);
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMGIFMessage __instance = (RCIMGIFMessage)this.c_intance;
            __instance.dataSize = j_intance.Get<long>("dataSize");
            __instance.width = j_intance.Get<long>("width");
            __instance.height = j_intance.Get<long>("height");
        }
    }
    
    public class CommandMessageConverter : MessageConverter
    {
        public CommandMessageConverter(RCIMCommandMessage message, string javaClass) : base(message, javaClass)
        {
        }
    
        public CommandMessageConverter(AndroidJavaObject message, object _c_intance) : base(message, _c_intance)
        {
        }
    
        public static CommandMessageConverter from(RCIMCommandMessage message)
        {
            CommandMessageConverter converter =
                new CommandMessageConverter(message, "cn.rongcloud.im.wrapper.messages.RCIMIWCommandMessage");
            return converter;
        }
    
        public static CommandMessageConverter from(AndroidJavaObject message)
        {
            CommandMessageConverter converter = new CommandMessageConverter(message, new RCIMCommandMessage());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMCommandMessage __instance = (RCIMCommandMessage)this.c_intance;
            string _name = __instance.name;
            string _data = __instance.data;
    
            if (_name != null)
                j_intance.Set<string>("name", _name);
            if (_data != null)
                j_intance.Set<string>("data", _data);
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMCommandMessage __instance = (RCIMCommandMessage)this.c_intance;
            __instance.name = j_intance.Get<string>("name");
            __instance.data = j_intance.Get<string>("data");
        }
    }
    
    public class VoiceMessageConverter : MediaMessageConverter
    {
        public VoiceMessageConverter(RCIMVoiceMessage message, string javaClass) : base(message, javaClass)
        {
        }
    
        public VoiceMessageConverter(AndroidJavaObject message, object _c_intance) : base(message, _c_intance)
        {
        }
    
        public static VoiceMessageConverter from(RCIMVoiceMessage message)
        {
            VoiceMessageConverter converter =
                new VoiceMessageConverter(message, "cn.rongcloud.im.wrapper.messages.RCIMIWVoiceMessage");
            return converter;
        }
    
        public static VoiceMessageConverter from(AndroidJavaObject message)
        {
            VoiceMessageConverter converter = new VoiceMessageConverter(message, new RCIMVoiceMessage());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMVoiceMessage __instance = (RCIMVoiceMessage)this.c_intance;
            int _duration = __instance.duration;
    
            j_intance.Set<int>("duration", _duration);
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMVoiceMessage __instance = (RCIMVoiceMessage)this.c_intance;
            __instance.duration = j_intance.Get<int>("duration");
        }
    }
    
    public class MentionedInfoConverter : Converter
    {
        public MentionedInfoConverter(RCIMMentionedInfo info, string javaClass)
        {
            this.c_intance = info;
            this.j_intance = new AndroidJavaObject(javaClass);
            this.toAndroidObject();
        }
    
        public MentionedInfoConverter(AndroidJavaObject info, object _c_intance)
        {
            this.j_intance = info;
            this.c_intance = _c_intance;
            this.toCSharpObject();
        }
    
        public static MentionedInfoConverter from(RCIMMentionedInfo info)
        {
            MentionedInfoConverter converter =
                new MentionedInfoConverter(info, "cn.rongcloud.im.wrapper.messages.RCIMIWMentionedInfo");
            return converter;
        }
    
        public static MentionedInfoConverter from(AndroidJavaObject info)
        {
            MentionedInfoConverter converter = new MentionedInfoConverter(info, new RCIMMentionedInfo());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMMentionedInfo __instance = (RCIMMentionedInfo)this.c_intance;
            RCIMMentionedType _type = __instance.type;
            List<string> _userIdList = __instance.userIdList;
            string _mentionedContent = __instance.mentionedContent;
    
            j_intance.Set<AndroidJavaObject>("type", new MentionedTypeConverter(_type).getAndroidObject());
            if (_userIdList != null)
            {
                AndroidJavaObject _list = new AndroidJavaObject("java.util.ArrayList");
                foreach (var item in _userIdList)
                    _list.Call<bool>("add", item);
                j_intance.Set<AndroidJavaObject>("userIdList", _list);
            }
            if (_mentionedContent != null)
                j_intance.Set<string>("mentionedContent", _mentionedContent);
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMMentionedInfo __instance = (RCIMMentionedInfo)this.c_intance;
            AndroidJavaObject _type = j_intance.Get<AndroidJavaObject>("type");
            if (_type != null)
                __instance.type = new MentionedTypeConverter(_type).getCSharpObject();
            AndroidJavaObject _userIdList = j_intance.Get<AndroidJavaObject>("userIdList");
            if (_userIdList != null)
            {
                List<string> userIdList = new List<string>();
                AndroidJavaObject iterator = _userIdList.Call<AndroidJavaObject>("iterator");
                while (iterator.Call<bool>("hasNext"))
                {
                    userIdList.Add(iterator.Call<string>("next"));
                }
                __instance.userIdList = userIdList;
            }
            __instance.mentionedContent = j_intance.Get<string>("mentionedContent");
        }
    }
    
    public class CommandNotificationMessageConverter : MessageConverter
    {
        public CommandNotificationMessageConverter(RCIMCommandNotificationMessage message, string javaClass)
            : base(message, javaClass)
        {
        }
    
        public CommandNotificationMessageConverter(AndroidJavaObject message, object _c_intance) : base(message, _c_intance)
        {
        }
    
        public static CommandNotificationMessageConverter from(RCIMCommandNotificationMessage message)
        {
            CommandNotificationMessageConverter converter = new CommandNotificationMessageConverter(
                message, "cn.rongcloud.im.wrapper.messages.RCIMIWCommandNotificationMessage");
            return converter;
        }
    
        public static CommandNotificationMessageConverter from(AndroidJavaObject message)
        {
            CommandNotificationMessageConverter converter =
                new CommandNotificationMessageConverter(message, new RCIMCommandNotificationMessage());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMCommandNotificationMessage __instance = (RCIMCommandNotificationMessage)this.c_intance;
            string _name = __instance.name;
            string _data = __instance.data;
    
            if (_name != null)
                j_intance.Set<string>("name", _name);
            if (_data != null)
                j_intance.Set<string>("data", _data);
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMCommandNotificationMessage __instance = (RCIMCommandNotificationMessage)this.c_intance;
            __instance.name = j_intance.Get<string>("name");
            __instance.data = j_intance.Get<string>("data");
        }
    }
    
    public class SightMessageConverter : MediaMessageConverter
    {
        public SightMessageConverter(RCIMSightMessage message, string javaClass) : base(message, javaClass)
        {
        }
    
        public SightMessageConverter(AndroidJavaObject message, object _c_intance) : base(message, _c_intance)
        {
        }
    
        public static SightMessageConverter from(RCIMSightMessage message)
        {
            SightMessageConverter converter =
                new SightMessageConverter(message, "cn.rongcloud.im.wrapper.messages.RCIMIWSightMessage");
            return converter;
        }
    
        public static SightMessageConverter from(AndroidJavaObject message)
        {
            SightMessageConverter converter = new SightMessageConverter(message, new RCIMSightMessage());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMSightMessage __instance = (RCIMSightMessage)this.c_intance;
            int _duration = __instance.duration;
            long _size = __instance.size;
            string _name = __instance.name;
            string _thumbnailBase64String = __instance.thumbnailBase64String;
    
            j_intance.Set<int>("duration", _duration);
            j_intance.Set<long>("size", _size);
            if (_name != null)
                j_intance.Set<string>("name", _name);
            if (_thumbnailBase64String != null)
                j_intance.Set<string>("thumbnailBase64String", _thumbnailBase64String);
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMSightMessage __instance = (RCIMSightMessage)this.c_intance;
            __instance.duration = j_intance.Get<int>("duration");
            __instance.size = j_intance.Get<long>("size");
            __instance.name = j_intance.Get<string>("name");
            __instance.thumbnailBase64String = j_intance.Get<string>("thumbnailBase64String");
        }
    }
    
    public class LocationMessageConverter : MessageConverter
    {
        public LocationMessageConverter(RCIMLocationMessage message, string javaClass) : base(message, javaClass)
        {
        }
    
        public LocationMessageConverter(AndroidJavaObject message, object _c_intance) : base(message, _c_intance)
        {
        }
    
        public static LocationMessageConverter from(RCIMLocationMessage message)
        {
            LocationMessageConverter converter =
                new LocationMessageConverter(message, "cn.rongcloud.im.wrapper.messages.RCIMIWLocationMessage");
            return converter;
        }
    
        public static LocationMessageConverter from(AndroidJavaObject message)
        {
            LocationMessageConverter converter = new LocationMessageConverter(message, new RCIMLocationMessage());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMLocationMessage __instance = (RCIMLocationMessage)this.c_intance;
            double _longitude = __instance.longitude;
            double _latitude = __instance.latitude;
            string _poiName = __instance.poiName;
            string _thumbnailPath = __instance.thumbnailPath;
    
            j_intance.Set<double>("longitude", _longitude);
            j_intance.Set<double>("latitude", _latitude);
            if (_poiName != null)
                j_intance.Set<string>("poiName", _poiName);
            if (_thumbnailPath != null)
                j_intance.Set<string>("thumbnailPath", _thumbnailPath);
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMLocationMessage __instance = (RCIMLocationMessage)this.c_intance;
            __instance.longitude = j_intance.Get<double>("longitude");
            __instance.latitude = j_intance.Get<double>("latitude");
            __instance.poiName = j_intance.Get<string>("poiName");
            __instance.thumbnailPath = j_intance.Get<string>("thumbnailPath");
        }
    }
    
    public class ReferenceMessageConverter : MessageConverter
    {
        public ReferenceMessageConverter(RCIMReferenceMessage message, string javaClass) : base(message, javaClass)
        {
        }
    
        public ReferenceMessageConverter(AndroidJavaObject message, object _c_intance) : base(message, _c_intance)
        {
        }
    
        public static ReferenceMessageConverter from(RCIMReferenceMessage message)
        {
            ReferenceMessageConverter converter =
                new ReferenceMessageConverter(message, "cn.rongcloud.im.wrapper.messages.RCIMIWReferenceMessage");
            return converter;
        }
    
        public static ReferenceMessageConverter from(AndroidJavaObject message)
        {
            ReferenceMessageConverter converter = new ReferenceMessageConverter(message, new RCIMReferenceMessage());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMReferenceMessage __instance = (RCIMReferenceMessage)this.c_intance;
            string _text = __instance.text;
            RCIMMessage _referenceMessage = __instance.referenceMessage;
    
            if (_text != null)
                j_intance.Set<string>("text", _text);
            if (_referenceMessage != null)
                j_intance.Set<AndroidJavaObject>("referenceMessage",
                                                 MessageConverter.from(_referenceMessage).getAndroidObject());
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMReferenceMessage __instance = (RCIMReferenceMessage)this.c_intance;
            __instance.text = j_intance.Get<string>("text");
            AndroidJavaObject _referenceMessage = j_intance.Get<AndroidJavaObject>("referenceMessage");
            if (_referenceMessage != null)
                __instance.referenceMessage = (RCIMMessage)MessageConverter.from(_referenceMessage).getCSharpObject();
        }
    }
    
    public class BlockedMessageInfoConverter : Converter
    {
        public BlockedMessageInfoConverter(RCIMBlockedMessageInfo info, string javaClass)
        {
            this.c_intance = info;
            this.j_intance = new AndroidJavaObject(javaClass);
            this.toAndroidObject();
        }
    
        public BlockedMessageInfoConverter(AndroidJavaObject info, object _c_intance)
        {
            this.j_intance = info;
            this.c_intance = _c_intance;
            this.toCSharpObject();
        }
    
        public static BlockedMessageInfoConverter from(RCIMBlockedMessageInfo info)
        {
            BlockedMessageInfoConverter converter =
                new BlockedMessageInfoConverter(info, "cn.rongcloud.im.wrapper.constants.RCIMIWBlockedMessageInfo");
            return converter;
        }
    
        public static BlockedMessageInfoConverter from(AndroidJavaObject info)
        {
            BlockedMessageInfoConverter converter = new BlockedMessageInfoConverter(info, new RCIMBlockedMessageInfo());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMBlockedMessageInfo __instance = (RCIMBlockedMessageInfo)this.c_intance;
            RCIMConversationType _conversationType = __instance.conversationType;
            string _targetId = __instance.targetId;
            string _blockedMsgUId = __instance.blockedMsgUId;
            RCIMMessageBlockType _blockType = __instance.blockType;
            string _extra = __instance.extra;
    
            j_intance.Set<AndroidJavaObject>("conversationType",
                                             new ConversationTypeConverter(_conversationType).getAndroidObject());
            if (_targetId != null)
                j_intance.Set<string>("targetId", _targetId);
            if (_blockedMsgUId != null)
                j_intance.Set<string>("blockedMsgUId", _blockedMsgUId);
            j_intance.Set<AndroidJavaObject>("blockType", new MessageBlockTypeConverter(_blockType).getAndroidObject());
            if (_extra != null)
                j_intance.Set<string>("extra", _extra);
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMBlockedMessageInfo __instance = (RCIMBlockedMessageInfo)this.c_intance;
            AndroidJavaObject _conversationType = j_intance.Get<AndroidJavaObject>("conversationType");
            if (_conversationType != null)
                __instance.conversationType = new ConversationTypeConverter(_conversationType).getCSharpObject();
            __instance.targetId = j_intance.Get<string>("targetId");
            __instance.blockedMsgUId = j_intance.Get<string>("blockedMsgUId");
            AndroidJavaObject _blockType = j_intance.Get<AndroidJavaObject>("blockType");
            if (_blockType != null)
                __instance.blockType = new MessageBlockTypeConverter(_blockType).getCSharpObject();
            __instance.extra = j_intance.Get<string>("extra");
        }
    }
    
    public class TypingStatusConverter : Converter
    {
        public TypingStatusConverter(RCIMTypingStatus status, string javaClass)
        {
            this.c_intance = status;
            this.j_intance = new AndroidJavaObject(javaClass);
            this.toAndroidObject();
        }
    
        public TypingStatusConverter(AndroidJavaObject status, object _c_intance)
        {
            this.j_intance = status;
            this.c_intance = _c_intance;
            this.toCSharpObject();
        }
    
        public static TypingStatusConverter from(RCIMTypingStatus status)
        {
            TypingStatusConverter converter =
                new TypingStatusConverter(status, "cn.rongcloud.im.wrapper.constants.RCIMIWTypingStatus");
            return converter;
        }
    
        public static TypingStatusConverter from(AndroidJavaObject status)
        {
            TypingStatusConverter converter = new TypingStatusConverter(status, new RCIMTypingStatus());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMTypingStatus __instance = (RCIMTypingStatus)this.c_intance;
            string _userId = __instance.userId;
            string _contentType = __instance.contentType;
            long _sentTime = __instance.sentTime;
    
            if (_userId != null)
                j_intance.Set<string>("userId", _userId);
            if (_contentType != null)
                j_intance.Set<string>("contentType", _contentType);
            j_intance.Set<long>("sentTime", _sentTime);
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMTypingStatus __instance = (RCIMTypingStatus)this.c_intance;
            __instance.userId = j_intance.Get<string>("userId");
            __instance.contentType = j_intance.Get<string>("contentType");
            __instance.sentTime = j_intance.Get<long>("sentTime");
        }
    }
    
    public class UltraGroupTypingStatusInfoConverter : Converter
    {
        public UltraGroupTypingStatusInfoConverter(RCIMUltraGroupTypingStatusInfo info, string javaClass)
        {
            this.c_intance = info;
            this.j_intance = new AndroidJavaObject(javaClass);
            this.toAndroidObject();
        }
    
        public UltraGroupTypingStatusInfoConverter(AndroidJavaObject info, object _c_intance)
        {
            this.j_intance = info;
            this.c_intance = _c_intance;
            this.toCSharpObject();
        }
    
        public static UltraGroupTypingStatusInfoConverter from(RCIMUltraGroupTypingStatusInfo info)
        {
            UltraGroupTypingStatusInfoConverter converter = new UltraGroupTypingStatusInfoConverter(
                info, "cn.rongcloud.im.wrapper.constants.RCIMIWUltraGroupTypingStatusInfo");
            return converter;
        }
    
        public static UltraGroupTypingStatusInfoConverter from(AndroidJavaObject info)
        {
            UltraGroupTypingStatusInfoConverter converter =
                new UltraGroupTypingStatusInfoConverter(info, new RCIMUltraGroupTypingStatusInfo());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMUltraGroupTypingStatusInfo __instance = (RCIMUltraGroupTypingStatusInfo)this.c_intance;
            string _targetId = __instance.targetId;
            string _channelId = __instance.channelId;
            string _userId = __instance.userId;
            int _userNums = __instance.userNums;
            RCIMUltraGroupTypingStatus _status = __instance.status;
            long _timestamp = __instance.timestamp;
    
            if (_targetId != null)
                j_intance.Set<string>("targetId", _targetId);
            if (_channelId != null)
                j_intance.Set<string>("channelId", _channelId);
            if (_userId != null)
                j_intance.Set<string>("userId", _userId);
            j_intance.Set<int>("userNums", _userNums);
            j_intance.Set<AndroidJavaObject>("status", new UltraGroupTypingStatusConverter(_status).getAndroidObject());
            j_intance.Set<long>("timestamp", _timestamp);
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMUltraGroupTypingStatusInfo __instance = (RCIMUltraGroupTypingStatusInfo)this.c_intance;
            __instance.targetId = j_intance.Get<string>("targetId");
            __instance.channelId = j_intance.Get<string>("channelId");
            __instance.userId = j_intance.Get<string>("userId");
            __instance.userNums = j_intance.Get<int>("userNums");
            AndroidJavaObject _status = j_intance.Get<AndroidJavaObject>("status");
            if (_status != null)
                __instance.status = new UltraGroupTypingStatusConverter(_status).getCSharpObject();
            __instance.timestamp = j_intance.Get<long>("timestamp");
        }
    }
    
    public class GroupReadReceiptInfoConverter : Converter
    {
        public GroupReadReceiptInfoConverter(RCIMGroupReadReceiptInfo info, string javaClass)
        {
            this.c_intance = info;
            this.j_intance = new AndroidJavaObject(javaClass);
            this.toAndroidObject();
        }
    
        public GroupReadReceiptInfoConverter(AndroidJavaObject info, object _c_intance)
        {
            this.j_intance = info;
            this.c_intance = _c_intance;
            this.toCSharpObject();
        }
    
        public static GroupReadReceiptInfoConverter from(RCIMGroupReadReceiptInfo info)
        {
            GroupReadReceiptInfoConverter converter =
                new GroupReadReceiptInfoConverter(info, "cn.rongcloud.im.wrapper.constants.RCIMIWGroupReadReceiptInfo");
            return converter;
        }
    
        public static GroupReadReceiptInfoConverter from(AndroidJavaObject info)
        {
            GroupReadReceiptInfoConverter converter =
                new GroupReadReceiptInfoConverter(info, new RCIMGroupReadReceiptInfo());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMGroupReadReceiptInfo __instance = (RCIMGroupReadReceiptInfo)this.c_intance;
            bool _readReceiptMessage = __instance.readReceiptMessage;
            bool _hasRespond = __instance.hasRespond;
            Dictionary<string, long> _respondUserIds = __instance.respondUserIds;
    
            j_intance.Set<bool>("readReceiptMessage", _readReceiptMessage);
            j_intance.Set<bool>("hasRespond", _hasRespond);
            if (_respondUserIds != null)
            {
                AndroidJavaObject _map = new AndroidJavaObject("java.util.HashMap");
                foreach (var item in _respondUserIds)
                {
                    AndroidJavaObject _num = new AndroidJavaObject("java.lang.Long", item.Value);
                    _map.Call<AndroidJavaObject>("put", item.Key, _num);
                }
                j_intance.Set<AndroidJavaObject>("respondUserIds", _map);
            }
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMGroupReadReceiptInfo __instance = (RCIMGroupReadReceiptInfo)this.c_intance;
            __instance.readReceiptMessage = j_intance.Get<bool>("readReceiptMessage");
            __instance.hasRespond = j_intance.Get<bool>("hasRespond");
            AndroidJavaObject _respondUserIds = j_intance.Get<AndroidJavaObject>("respondUserIds");
            if (_respondUserIds != null)
            {
                Dictionary<string, long> respondUserIds = new Dictionary<string, long>();
                AndroidJavaObject iterator =
                    _respondUserIds.Call<AndroidJavaObject>("entrySet").Call<AndroidJavaObject>("iterator");
                while (iterator.Call<bool>("hasNext"))
                {
                    AndroidJavaObject item = iterator.Call<AndroidJavaObject>("next");
                    string key = item.Call<string>("getKey");
                    AndroidJavaObject value = item.Call<AndroidJavaObject>("getValue");
                    respondUserIds.Add(key, value.Call<long>("longValue"));
                }
                __instance.respondUserIds = respondUserIds;
            }
        }
    }
    
    public class ChatRoomMemberActionConverter : Converter
    {
        public ChatRoomMemberActionConverter(RCIMChatRoomMemberAction action, string javaClass)
        {
            this.c_intance = action;
            this.j_intance = new AndroidJavaObject(javaClass);
            this.toAndroidObject();
        }
    
        public ChatRoomMemberActionConverter(AndroidJavaObject action, object _c_intance)
        {
            this.j_intance = action;
            this.c_intance = _c_intance;
            this.toCSharpObject();
        }
    
        public static ChatRoomMemberActionConverter from(RCIMChatRoomMemberAction action)
        {
            ChatRoomMemberActionConverter converter =
                new ChatRoomMemberActionConverter(action, "cn.rongcloud.im.wrapper.chatroom.RCIMIWChatRoomMemberAction");
            return converter;
        }
    
        public static ChatRoomMemberActionConverter from(AndroidJavaObject action)
        {
            ChatRoomMemberActionConverter converter =
                new ChatRoomMemberActionConverter(action, new RCIMChatRoomMemberAction());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMChatRoomMemberAction __instance = (RCIMChatRoomMemberAction)this.c_intance;
            string _userId = __instance.userId;
            RCIMChatRoomMemberActionType _actionType = __instance.actionType;
    
            if (_userId != null)
                j_intance.Set<string>("userId", _userId);
            j_intance.Set<AndroidJavaObject>("actionType",
                                             new ChatRoomMemberActionTypeConverter(_actionType).getAndroidObject());
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMChatRoomMemberAction __instance = (RCIMChatRoomMemberAction)this.c_intance;
            __instance.userId = j_intance.Get<string>("userId");
            AndroidJavaObject _actionType = j_intance.Get<AndroidJavaObject>("actionType");
            if (_actionType != null)
                __instance.actionType = new ChatRoomMemberActionTypeConverter(_actionType).getCSharpObject();
        }
    }
    
    public class SearchConversationResultConverter : Converter
    {
        public SearchConversationResultConverter(RCIMSearchConversationResult result, string javaClass)
        {
            this.c_intance = result;
            this.j_intance = new AndroidJavaObject(javaClass);
            this.toAndroidObject();
        }
    
        public SearchConversationResultConverter(AndroidJavaObject result, object _c_intance)
        {
            this.j_intance = result;
            this.c_intance = _c_intance;
            this.toCSharpObject();
        }
    
        public static SearchConversationResultConverter from(RCIMSearchConversationResult result)
        {
            SearchConversationResultConverter converter = new SearchConversationResultConverter(
                result, "cn.rongcloud.im.wrapper.conversation.RCIMIWSearchConversationResult");
            return converter;
        }
    
        public static SearchConversationResultConverter from(AndroidJavaObject result)
        {
            SearchConversationResultConverter converter =
                new SearchConversationResultConverter(result, new RCIMSearchConversationResult());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMSearchConversationResult __instance = (RCIMSearchConversationResult)this.c_intance;
            RCIMConversation _conversation = __instance.conversation;
            int _count = __instance.count;
    
            if (_conversation != null)
                j_intance.Set<AndroidJavaObject>("conversation",
                                                 ConversationConverter.from(_conversation).getAndroidObject());
    
            j_intance.Set<int>("count", _count);
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMSearchConversationResult __instance = (RCIMSearchConversationResult)this.c_intance;
            AndroidJavaObject _conversation = j_intance.Get<AndroidJavaObject>("conversation");
            if (_conversation != null)
                __instance.conversation = (RCIMConversation)ConversationConverter.from(_conversation).getCSharpObject();
            __instance.count = j_intance.Get<int>("count");
        }
    }
    
    public class ConversationConverter : Converter
    {
        public ConversationConverter(RCIMConversation conversation, string javaClass)
        {
            this.c_intance = conversation;
            this.j_intance = new AndroidJavaObject(javaClass);
            this.toAndroidObject();
        }
    
        public ConversationConverter(AndroidJavaObject conversation, object _c_intance)
        {
            this.j_intance = conversation;
            this.c_intance = _c_intance;
            this.toCSharpObject();
        }
    
        public static ConversationConverter from(RCIMConversation conversation)
        {
            ConversationConverter converter =
                new ConversationConverter(conversation, "cn.rongcloud.im.wrapper.conversation.RCIMIWConversation");
            return converter;
        }
    
        public static ConversationConverter from(AndroidJavaObject conversation)
        {
            ConversationConverter converter = new ConversationConverter(conversation, new RCIMConversation());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMConversation __instance = (RCIMConversation)this.c_intance;
            RCIMConversationType _conversationType = __instance.conversationType;
            string _targetId = __instance.targetId;
            string _channelId = __instance.channelId;
            int _unreadCount = __instance.unreadCount;
            int _mentionedCount = __instance.mentionedCount;
            bool _top = __instance.top;
            string _draft = __instance.draft;
            RCIMMessage _lastMessage = __instance.lastMessage;
            RCIMPushNotificationLevel _notificationLevel = __instance.notificationLevel;
            long _firstUnreadMsgSendTime = __instance.firstUnreadMsgSendTime;
            long _operationTime = __instance.operationTime;
    
            j_intance.Set<AndroidJavaObject>("conversationType",
                                             new ConversationTypeConverter(_conversationType).getAndroidObject());
            if (_targetId != null)
                j_intance.Set<string>("targetId", _targetId);
            if (_channelId != null)
                j_intance.Set<string>("channelId", _channelId);
            j_intance.Set<int>("unreadCount", _unreadCount);
            j_intance.Set<int>("mentionedCount", _mentionedCount);
            j_intance.Set<bool>("top", _top);
            if (_draft != null)
                j_intance.Set<string>("draft", _draft);
            if (_lastMessage != null)
                j_intance.Set<AndroidJavaObject>("lastMessage", MessageConverter.from(_lastMessage).getAndroidObject());
    
            j_intance.Set<AndroidJavaObject>("notificationLevel",
                                             new PushNotificationLevelConverter(_notificationLevel).getAndroidObject());
            j_intance.Set<long>("firstUnreadMsgSendTime", _firstUnreadMsgSendTime);
            j_intance.Set<long>("operationTime", _operationTime);
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMConversation __instance = (RCIMConversation)this.c_intance;
            AndroidJavaObject _conversationType = j_intance.Get<AndroidJavaObject>("conversationType");
            if (_conversationType != null)
                __instance.conversationType = new ConversationTypeConverter(_conversationType).getCSharpObject();
            __instance.targetId = j_intance.Get<string>("targetId");
            __instance.channelId = j_intance.Get<string>("channelId");
            __instance.unreadCount = j_intance.Get<int>("unreadCount");
            __instance.mentionedCount = j_intance.Get<int>("mentionedCount");
            __instance.top = j_intance.Get<bool>("top");
            __instance.draft = j_intance.Get<string>("draft");
            AndroidJavaObject _lastMessage = j_intance.Get<AndroidJavaObject>("lastMessage");
            if (_lastMessage != null)
                __instance.lastMessage = (RCIMMessage)MessageConverter.from(_lastMessage).getCSharpObject();
            AndroidJavaObject _notificationLevel = j_intance.Get<AndroidJavaObject>("notificationLevel");
            if (_notificationLevel != null)
                __instance.notificationLevel = new PushNotificationLevelConverter(_notificationLevel).getCSharpObject();
            __instance.firstUnreadMsgSendTime = j_intance.Get<long>("firstUnreadMsgSendTime");
            __instance.operationTime = j_intance.Get<long>("operationTime");
        }
    }
    
    public class PushOptionsConverter : Converter
    {
        public PushOptionsConverter(RCIMPushOptions options, string javaClass)
        {
            this.c_intance = options;
            this.j_intance = new AndroidJavaObject(javaClass);
            this.toAndroidObject();
        }
    
        public PushOptionsConverter(AndroidJavaObject options, object _c_intance)
        {
            this.j_intance = options;
            this.c_intance = _c_intance;
            this.toCSharpObject();
        }
    
        public static PushOptionsConverter from(RCIMPushOptions options)
        {
            PushOptionsConverter converter =
                new PushOptionsConverter(options, "cn.rongcloud.im.wrapper.options.RCIMIWPushOptions");
            return converter;
        }
    
        public static PushOptionsConverter from(AndroidJavaObject options)
        {
            PushOptionsConverter converter = new PushOptionsConverter(options, new RCIMPushOptions());
            return converter;
        }
    
        public override void toAndroidObject()
        {
            base.toAndroidObject();
            RCIMPushOptions __instance = (RCIMPushOptions)this.c_intance;
            string _idMI = __instance.idMI;
            string _appKeyMI = __instance.appKeyMI;
            string _appIdMeizu = __instance.appIdMeizu;
            string _appKeyMeizu = __instance.appKeyMeizu;
            string _appKeyOPPO = __instance.appKeyOPPO;
            string _appSecretOPPO = __instance.appSecretOPPO;
            bool _enableHWPush = __instance.enableHWPush;
            bool _enableFCM = __instance.enableFCM;
            bool _enableVIVOPush = __instance.enableVIVOPush;
    
            if (_idMI != null)
                j_intance.Set<string>("idMI", _idMI);
            if (_appKeyMI != null)
                j_intance.Set<string>("appKeyMI", _appKeyMI);
            if (_appIdMeizu != null)
                j_intance.Set<string>("appIdMeizu", _appIdMeizu);
            if (_appKeyMeizu != null)
                j_intance.Set<string>("appKeyMeizu", _appKeyMeizu);
            if (_appKeyOPPO != null)
                j_intance.Set<string>("appKeyOPPO", _appKeyOPPO);
            if (_appSecretOPPO != null)
                j_intance.Set<string>("appSecretOPPO", _appSecretOPPO);
            j_intance.Set<bool>("enableHWPush", _enableHWPush);
            j_intance.Set<bool>("enableFCM", _enableFCM);
            j_intance.Set<bool>("enableVIVOPush", _enableVIVOPush);
        }
    
        public override void toCSharpObject()
        {
            base.toCSharpObject();
            RCIMPushOptions __instance = (RCIMPushOptions)this.c_intance;
            __instance.idMI = j_intance.Get<string>("idMI");
            __instance.appKeyMI = j_intance.Get<string>("appKeyMI");
            __instance.appIdMeizu = j_intance.Get<string>("appIdMeizu");
            __instance.appKeyMeizu = j_intance.Get<string>("appKeyMeizu");
            __instance.appKeyOPPO = j_intance.Get<string>("appKeyOPPO");
            __instance.appSecretOPPO = j_intance.Get<string>("appSecretOPPO");
            __instance.enableHWPush = j_intance.Get<bool>("enableHWPush");
            __instance.enableFCM = j_intance.Get<bool>("enableFCM");
            __instance.enableVIVOPush = j_intance.Get<bool>("enableVIVOPush");
        }
    }
    
    public class ImportanceHWConverter
    {
        private AndroidJavaClass javaClass = new AndroidJavaClass("cn.rongcloud.im.wrapper.constants.RCIMIWImportanceHW");
        private RCIMImportanceHW c_intance;
        private AndroidJavaObject j_intance;
        public ImportanceHWConverter(AndroidJavaObject w)
        {
            this.j_intance = w;
            int index = w.Call<int>("ordinal");
            c_intance = (RCIMImportanceHW)Enum.Parse(typeof(RCIMImportanceHW), index.ToString());
        }
    
        public ImportanceHWConverter(RCIMImportanceHW w)
        {
            this.c_intance = w;
            AndroidJavaObject[] values = javaClass.CallStatic<AndroidJavaObject[]>("values");
            j_intance = values[(int)w];
        }
    
        public AndroidJavaObject getAndroidObject()
        {
            return this.j_intance;
        }
    
        public RCIMImportanceHW getCSharpObject()
        {
            return this.c_intance;
        }
    }
    
    public class MessageOperationPolicyConverter
    {
        private AndroidJavaClass javaClass =
            new AndroidJavaClass("cn.rongcloud.im.wrapper.constants.RCIMIWMessageOperationPolicy");
        private RCIMMessageOperationPolicy c_intance;
        private AndroidJavaObject j_intance;
        public MessageOperationPolicyConverter(AndroidJavaObject policy)
        {
            this.j_intance = policy;
            int index = policy.Call<int>("ordinal");
            c_intance = (RCIMMessageOperationPolicy)Enum.Parse(typeof(RCIMMessageOperationPolicy), index.ToString());
        }
    
        public MessageOperationPolicyConverter(RCIMMessageOperationPolicy policy)
        {
            this.c_intance = policy;
            AndroidJavaObject[] values = javaClass.CallStatic<AndroidJavaObject[]>("values");
            j_intance = values[(int)policy];
        }
    
        public AndroidJavaObject getAndroidObject()
        {
            return this.j_intance;
        }
    
        public RCIMMessageOperationPolicy getCSharpObject()
        {
            return this.c_intance;
        }
    }
    
    public class VIVOPushTypeConverter
    {
        private AndroidJavaClass javaClass = new AndroidJavaClass("cn.rongcloud.im.wrapper.constants.RCIMIWVIVOPushType");
        private RCIMVIVOPushType c_intance;
        private AndroidJavaObject j_intance;
        public VIVOPushTypeConverter(AndroidJavaObject type)
        {
            this.j_intance = type;
            int index = type.Call<int>("ordinal");
            c_intance = (RCIMVIVOPushType)Enum.Parse(typeof(RCIMVIVOPushType), index.ToString());
        }
    
        public VIVOPushTypeConverter(RCIMVIVOPushType type)
        {
            this.c_intance = type;
            AndroidJavaObject[] values = javaClass.CallStatic<AndroidJavaObject[]>("values");
            j_intance = values[(int)type];
        }
    
        public AndroidJavaObject getAndroidObject()
        {
            return this.j_intance;
        }
    
        public RCIMVIVOPushType getCSharpObject()
        {
            return this.c_intance;
        }
    }
    
    public class SentStatusConverter
    {
        private AndroidJavaClass javaClass = new AndroidJavaClass("cn.rongcloud.im.wrapper.constants.RCIMIWSentStatus");
        private RCIMSentStatus c_intance;
        private AndroidJavaObject j_intance;
        public SentStatusConverter(AndroidJavaObject status)
        {
            this.j_intance = status;
            int index = status.Call<int>("ordinal");
            c_intance = (RCIMSentStatus)Enum.Parse(typeof(RCIMSentStatus), index.ToString());
        }
    
        public SentStatusConverter(RCIMSentStatus status)
        {
            this.c_intance = status;
            AndroidJavaObject[] values = javaClass.CallStatic<AndroidJavaObject[]>("values");
            j_intance = values[(int)status];
        }
    
        public AndroidJavaObject getAndroidObject()
        {
            return this.j_intance;
        }
    
        public RCIMSentStatus getCSharpObject()
        {
            return this.c_intance;
        }
    }
    
    public class PushNotificationQuietHoursLevelConverter
    {
        private AndroidJavaClass javaClass =
            new AndroidJavaClass("cn.rongcloud.im.wrapper.constants.RCIMIWPushNotificationQuietHoursLevel");
        private RCIMPushNotificationQuietHoursLevel c_intance;
        private AndroidJavaObject j_intance;
        public PushNotificationQuietHoursLevelConverter(AndroidJavaObject level)
        {
            this.j_intance = level;
            int index = level.Call<int>("ordinal");
            c_intance = (RCIMPushNotificationQuietHoursLevel)Enum.Parse(typeof(RCIMPushNotificationQuietHoursLevel),
                                                                        index.ToString());
        }
    
        public PushNotificationQuietHoursLevelConverter(RCIMPushNotificationQuietHoursLevel level)
        {
            this.c_intance = level;
            AndroidJavaObject[] values = javaClass.CallStatic<AndroidJavaObject[]>("values");
            j_intance = values[(int)level];
        }
    
        public AndroidJavaObject getAndroidObject()
        {
            return this.j_intance;
        }
    
        public RCIMPushNotificationQuietHoursLevel getCSharpObject()
        {
            return this.c_intance;
        }
    }
    
    public class MessageDirectionConverter
    {
        private AndroidJavaClass javaClass =
            new AndroidJavaClass("cn.rongcloud.im.wrapper.constants.RCIMIWMessageDirection");
        private RCIMMessageDirection c_intance;
        private AndroidJavaObject j_intance;
        public MessageDirectionConverter(AndroidJavaObject direction)
        {
            this.j_intance = direction;
            int index = direction.Call<int>("ordinal");
            c_intance = (RCIMMessageDirection)Enum.Parse(typeof(RCIMMessageDirection), index.ToString());
        }
    
        public MessageDirectionConverter(RCIMMessageDirection direction)
        {
            this.c_intance = direction;
            AndroidJavaObject[] values = javaClass.CallStatic<AndroidJavaObject[]>("values");
            j_intance = values[(int)direction];
        }
    
        public AndroidJavaObject getAndroidObject()
        {
            return this.j_intance;
        }
    
        public RCIMMessageDirection getCSharpObject()
        {
            return this.c_intance;
        }
    }
    
    public class ReceivedStatusConverter
    {
        private AndroidJavaClass javaClass = new AndroidJavaClass("cn.rongcloud.im.wrapper.constants.RCIMIWReceivedStatus");
        private RCIMReceivedStatus c_intance;
        private AndroidJavaObject j_intance;
        public ReceivedStatusConverter(AndroidJavaObject status)
        {
            this.j_intance = status;
            int index = status.Call<int>("ordinal");
            c_intance = (RCIMReceivedStatus)Enum.Parse(typeof(RCIMReceivedStatus), index.ToString());
        }
    
        public ReceivedStatusConverter(RCIMReceivedStatus status)
        {
            this.c_intance = status;
            AndroidJavaObject[] values = javaClass.CallStatic<AndroidJavaObject[]>("values");
            j_intance = values[(int)status];
        }
    
        public AndroidJavaObject getAndroidObject()
        {
            return this.j_intance;
        }
    
        public RCIMReceivedStatus getCSharpObject()
        {
            return this.c_intance;
        }
    }
    
    public class ChatRoomMemberActionTypeConverter
    {
        private AndroidJavaClass javaClass =
            new AndroidJavaClass("cn.rongcloud.im.wrapper.constants.RCIMIWChatRoomMemberActionType");
        private RCIMChatRoomMemberActionType c_intance;
        private AndroidJavaObject j_intance;
        public ChatRoomMemberActionTypeConverter(AndroidJavaObject type)
        {
            this.j_intance = type;
            int index = type.Call<int>("ordinal");
            c_intance = (RCIMChatRoomMemberActionType)Enum.Parse(typeof(RCIMChatRoomMemberActionType), index.ToString());
        }
    
        public ChatRoomMemberActionTypeConverter(RCIMChatRoomMemberActionType type)
        {
            this.c_intance = type;
            AndroidJavaObject[] values = javaClass.CallStatic<AndroidJavaObject[]>("values");
            j_intance = values[(int)type];
        }
    
        public AndroidJavaObject getAndroidObject()
        {
            return this.j_intance;
        }
    
        public RCIMChatRoomMemberActionType getCSharpObject()
        {
            return this.c_intance;
        }
    }
    
    public class PushNotificationLevelConverter
    {
        private AndroidJavaClass javaClass =
            new AndroidJavaClass("cn.rongcloud.im.wrapper.constants.RCIMIWPushNotificationLevel");
        private RCIMPushNotificationLevel c_intance;
        private AndroidJavaObject j_intance;
        public PushNotificationLevelConverter(AndroidJavaObject level)
        {
            this.j_intance = level;
            int index = level.Call<int>("ordinal");
            c_intance = (RCIMPushNotificationLevel)Enum.Parse(typeof(RCIMPushNotificationLevel), index.ToString());
        }
    
        public PushNotificationLevelConverter(RCIMPushNotificationLevel level)
        {
            this.c_intance = level;
            AndroidJavaObject[] values = javaClass.CallStatic<AndroidJavaObject[]>("values");
            j_intance = values[(int)level];
        }
    
        public AndroidJavaObject getAndroidObject()
        {
            return this.j_intance;
        }
    
        public RCIMPushNotificationLevel getCSharpObject()
        {
            return this.c_intance;
        }
    }
    
    public class MessageTypeConverter
    {
        private AndroidJavaClass javaClass = new AndroidJavaClass("cn.rongcloud.im.wrapper.constants.RCIMIWMessageType");
        private RCIMMessageType c_intance;
        private AndroidJavaObject j_intance;
        public MessageTypeConverter(AndroidJavaObject type)
        {
            this.j_intance = type;
            int index = type.Call<int>("ordinal");
            c_intance = (RCIMMessageType)Enum.Parse(typeof(RCIMMessageType), index.ToString());
        }
    
        public MessageTypeConverter(RCIMMessageType type)
        {
            this.c_intance = type;
            AndroidJavaObject[] values = javaClass.CallStatic<AndroidJavaObject[]>("values");
            j_intance = values[(int)type];
        }
    
        public AndroidJavaObject getAndroidObject()
        {
            return this.j_intance;
        }
    
        public RCIMMessageType getCSharpObject()
        {
            return this.c_intance;
        }
    }
    
    public class MessageBlockTypeConverter
    {
        private AndroidJavaClass javaClass =
            new AndroidJavaClass("cn.rongcloud.im.wrapper.constants.RCIMIWMessageBlockType");
        private RCIMMessageBlockType c_intance;
        private AndroidJavaObject j_intance;
        public MessageBlockTypeConverter(AndroidJavaObject type)
        {
            this.j_intance = type;
            int index = type.Call<int>("ordinal");
            c_intance = (RCIMMessageBlockType)Enum.Parse(typeof(RCIMMessageBlockType), index.ToString());
        }
    
        public MessageBlockTypeConverter(RCIMMessageBlockType type)
        {
            this.c_intance = type;
            AndroidJavaObject[] values = javaClass.CallStatic<AndroidJavaObject[]>("values");
            j_intance = values[(int)type];
        }
    
        public AndroidJavaObject getAndroidObject()
        {
            return this.j_intance;
        }
    
        public RCIMMessageBlockType getCSharpObject()
        {
            return this.c_intance;
        }
    }
    
    public class TimeOrderConverter
    {
        private AndroidJavaClass javaClass = new AndroidJavaClass("cn.rongcloud.im.wrapper.constants.RCIMIWTimeOrder");
        private RCIMTimeOrder c_intance;
        private AndroidJavaObject j_intance;
        public TimeOrderConverter(AndroidJavaObject order)
        {
            this.j_intance = order;
            int index = order.Call<int>("ordinal");
            c_intance = (RCIMTimeOrder)Enum.Parse(typeof(RCIMTimeOrder), index.ToString());
        }
    
        public TimeOrderConverter(RCIMTimeOrder order)
        {
            this.c_intance = order;
            AndroidJavaObject[] values = javaClass.CallStatic<AndroidJavaObject[]>("values");
            j_intance = values[(int)order];
        }
    
        public AndroidJavaObject getAndroidObject()
        {
            return this.j_intance;
        }
    
        public RCIMTimeOrder getCSharpObject()
        {
            return this.c_intance;
        }
    }
    
    public class CustomMessagePolicyConverter
    {
        private AndroidJavaClass javaClass =
            new AndroidJavaClass("cn.rongcloud.im.wrapper.constants.RCIMIWCustomMessagePolicy");
        private RCIMCustomMessagePolicy c_intance;
        private AndroidJavaObject j_intance;
        public CustomMessagePolicyConverter(AndroidJavaObject policy)
        {
            this.j_intance = policy;
            int index = policy.Call<int>("ordinal");
            c_intance = (RCIMCustomMessagePolicy)Enum.Parse(typeof(RCIMCustomMessagePolicy), index.ToString());
        }
    
        public CustomMessagePolicyConverter(RCIMCustomMessagePolicy policy)
        {
            this.c_intance = policy;
            AndroidJavaObject[] values = javaClass.CallStatic<AndroidJavaObject[]>("values");
            j_intance = values[(int)policy];
        }
    
        public AndroidJavaObject getAndroidObject()
        {
            return this.j_intance;
        }
    
        public RCIMCustomMessagePolicy getCSharpObject()
        {
            return this.c_intance;
        }
    }
    
    public class ChatRoomStatusConverter
    {
        private AndroidJavaClass javaClass = new AndroidJavaClass("cn.rongcloud.im.wrapper.constants.RCIMIWChatRoomStatus");
        private RCIMChatRoomStatus c_intance;
        private AndroidJavaObject j_intance;
        public ChatRoomStatusConverter(AndroidJavaObject status)
        {
            this.j_intance = status;
            int index = status.Call<int>("ordinal");
            c_intance = (RCIMChatRoomStatus)Enum.Parse(typeof(RCIMChatRoomStatus), index.ToString());
        }
    
        public ChatRoomStatusConverter(RCIMChatRoomStatus status)
        {
            this.c_intance = status;
            AndroidJavaObject[] values = javaClass.CallStatic<AndroidJavaObject[]>("values");
            j_intance = values[(int)status];
        }
    
        public AndroidJavaObject getAndroidObject()
        {
            return this.j_intance;
        }
    
        public RCIMChatRoomStatus getCSharpObject()
        {
            return this.c_intance;
        }
    }
    
    public class ConversationTypeConverter
    {
        private AndroidJavaClass javaClass =
            new AndroidJavaClass("cn.rongcloud.im.wrapper.constants.RCIMIWConversationType");
        private RCIMConversationType c_intance;
        private AndroidJavaObject j_intance;
        public ConversationTypeConverter(AndroidJavaObject type)
        {
            this.j_intance = type;
            int index = type.Call<int>("ordinal");
            c_intance = (RCIMConversationType)Enum.Parse(typeof(RCIMConversationType), index.ToString());
        }
    
        public ConversationTypeConverter(RCIMConversationType type)
        {
            this.c_intance = type;
            AndroidJavaObject[] values = javaClass.CallStatic<AndroidJavaObject[]>("values");
            j_intance = values[(int)type];
        }
    
        public AndroidJavaObject getAndroidObject()
        {
            return this.j_intance;
        }
    
        public RCIMConversationType getCSharpObject()
        {
            return this.c_intance;
        }
    }
    
    public class ErrorCodeConverter
    {
        private AndroidJavaClass javaClass = new AndroidJavaClass("cn.rongcloud.im.wrapper.constants.RCIMIWErrorCode");
        private RCIMErrorCode c_intance;
        private AndroidJavaObject j_intance;
        public ErrorCodeConverter(AndroidJavaObject code)
        {
            this.j_intance = code;
            int index = code.Call<int>("ordinal");
            c_intance = (RCIMErrorCode)Enum.Parse(typeof(RCIMErrorCode), index.ToString());
        }
    
        public ErrorCodeConverter(RCIMErrorCode code)
        {
            this.c_intance = code;
            AndroidJavaObject[] values = javaClass.CallStatic<AndroidJavaObject[]>("values");
            j_intance = values[(int)code];
        }
    
        public AndroidJavaObject getAndroidObject()
        {
            return this.j_intance;
        }
    
        public RCIMErrorCode getCSharpObject()
        {
            return this.c_intance;
        }
    }
    
    public class UltraGroupTypingStatusConverter
    {
        private AndroidJavaClass javaClass =
            new AndroidJavaClass("cn.rongcloud.im.wrapper.constants.RCIMIWUltraGroupTypingStatus");
        private RCIMUltraGroupTypingStatus c_intance;
        private AndroidJavaObject j_intance;
        public UltraGroupTypingStatusConverter(AndroidJavaObject status)
        {
            this.j_intance = status;
            int index = status.Call<int>("ordinal");
            c_intance = (RCIMUltraGroupTypingStatus)Enum.Parse(typeof(RCIMUltraGroupTypingStatus), index.ToString());
        }
    
        public UltraGroupTypingStatusConverter(RCIMUltraGroupTypingStatus status)
        {
            this.c_intance = status;
            AndroidJavaObject[] values = javaClass.CallStatic<AndroidJavaObject[]>("values");
            j_intance = values[(int)status];
        }
    
        public AndroidJavaObject getAndroidObject()
        {
            return this.j_intance;
        }
    
        public RCIMUltraGroupTypingStatus getCSharpObject()
        {
            return this.c_intance;
        }
    }
    
    public class MentionedTypeConverter
    {
        private AndroidJavaClass javaClass = new AndroidJavaClass("cn.rongcloud.im.wrapper.constants.RCIMIWMentionedType");
        private RCIMMentionedType c_intance;
        private AndroidJavaObject j_intance;
        public MentionedTypeConverter(AndroidJavaObject type)
        {
            this.j_intance = type;
            int index = type.Call<int>("ordinal");
            c_intance = (RCIMMentionedType)Enum.Parse(typeof(RCIMMentionedType), index.ToString());
        }
    
        public MentionedTypeConverter(RCIMMentionedType type)
        {
            this.c_intance = type;
            AndroidJavaObject[] values = javaClass.CallStatic<AndroidJavaObject[]>("values");
            j_intance = values[(int)type];
        }
    
        public AndroidJavaObject getAndroidObject()
        {
            return this.j_intance;
        }
    
        public RCIMMentionedType getCSharpObject()
        {
            return this.c_intance;
        }
    }
    
    public class ChatRoomEntriesOperationTypeConverter
    {
        private AndroidJavaClass javaClass =
            new AndroidJavaClass("cn.rongcloud.im.wrapper.constants.RCIMIWChatRoomEntriesOperationType");
        private RCIMChatRoomEntriesOperationType c_intance;
        private AndroidJavaObject j_intance;
        public ChatRoomEntriesOperationTypeConverter(AndroidJavaObject type)
        {
            this.j_intance = type;
            int index = type.Call<int>("ordinal");
            c_intance =
                (RCIMChatRoomEntriesOperationType)Enum.Parse(typeof(RCIMChatRoomEntriesOperationType), index.ToString());
        }
    
        public ChatRoomEntriesOperationTypeConverter(RCIMChatRoomEntriesOperationType type)
        {
            this.c_intance = type;
            AndroidJavaObject[] values = javaClass.CallStatic<AndroidJavaObject[]>("values");
            j_intance = values[(int)type];
        }
    
        public AndroidJavaObject getAndroidObject()
        {
            return this.j_intance;
        }
    
        public RCIMChatRoomEntriesOperationType getCSharpObject()
        {
            return this.c_intance;
        }
    }
    
    public class LogLevelConverter
    {
        private AndroidJavaClass javaClass = new AndroidJavaClass("cn.rongcloud.im.wrapper.constants.RCIMIWLogLevel");
        private RCIMLogLevel c_intance;
        private AndroidJavaObject j_intance;
        public LogLevelConverter(AndroidJavaObject level)
        {
            this.j_intance = level;
            int index = level.Call<int>("ordinal");
            c_intance = (RCIMLogLevel)Enum.Parse(typeof(RCIMLogLevel), index.ToString());
        }
    
        public LogLevelConverter(RCIMLogLevel level)
        {
            this.c_intance = level;
            AndroidJavaObject[] values = javaClass.CallStatic<AndroidJavaObject[]>("values");
            j_intance = values[(int)level];
        }
    
        public AndroidJavaObject getAndroidObject()
        {
            return this.j_intance;
        }
    
        public RCIMLogLevel getCSharpObject()
        {
            return this.c_intance;
        }
    }
    
    public class BlacklistStatusConverter
    {
        private AndroidJavaClass javaClass =
            new AndroidJavaClass("cn.rongcloud.im.wrapper.constants.RCIMIWBlacklistStatus");
        private RCIMBlacklistStatus c_intance;
        private AndroidJavaObject j_intance;
        public BlacklistStatusConverter(AndroidJavaObject status)
        {
            this.j_intance = status;
            int index = status.Call<int>("ordinal");
            c_intance = (RCIMBlacklistStatus)Enum.Parse(typeof(RCIMBlacklistStatus), index.ToString());
        }
    
        public BlacklistStatusConverter(RCIMBlacklistStatus status)
        {
            this.c_intance = status;
            AndroidJavaObject[] values = javaClass.CallStatic<AndroidJavaObject[]>("values");
            j_intance = values[(int)status];
        }
    
        public AndroidJavaObject getAndroidObject()
        {
            return this.j_intance;
        }
    
        public RCIMBlacklistStatus getCSharpObject()
        {
            return this.c_intance;
        }
    }
    
    public class ConnectionStatusConverter
    {
        private AndroidJavaClass javaClass =
            new AndroidJavaClass("cn.rongcloud.im.wrapper.constants.RCIMIWConnectionStatus");
        private RCIMConnectionStatus c_intance;
        private AndroidJavaObject j_intance;
        public ConnectionStatusConverter(AndroidJavaObject status)
        {
            this.j_intance = status;
            int index = status.Call<int>("ordinal");
            c_intance = (RCIMConnectionStatus)Enum.Parse(typeof(RCIMConnectionStatus), index.ToString());
        }
    
        public ConnectionStatusConverter(RCIMConnectionStatus status)
        {
            this.c_intance = status;
            AndroidJavaObject[] values = javaClass.CallStatic<AndroidJavaObject[]>("values");
            j_intance = values[(int)status];
        }
    
        public AndroidJavaObject getAndroidObject()
        {
            return this.j_intance;
        }
    
        public RCIMConnectionStatus getCSharpObject()
        {
            return this.c_intance;
        }
    }
    
}
#endif
