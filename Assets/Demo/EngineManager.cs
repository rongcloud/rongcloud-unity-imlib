using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cn_rongcloud_im_unity;
using System;

namespace cn_rongcloud_im_wapper_unity_example
{
    public partial class EngineManager
    {
        public delegate void OnEventResultHandle(Dictionary<String, object> result);

        public OnEventResultHandle ResultHandle { set; get; }

        private static RCIMEngine engine;

        private static EngineManager manager;
        private static readonly object syncObj = new object();

        public static EngineManager Instance
        {
            get
            {
                if (manager == null)
                {
                    lock (syncObj)
                    {
                        manager = new EngineManager();
                    }
                }
                return manager;
            }
        }

        public bool initEngine(Dictionary<String, object> arg)
        {
            if (engine != null)
            {
                ExampleUtils.ShowToast("引擎已初始化");
                Dictionary<String, object> eresult = new Dictionary<String, object>();
                eresult["method"] = "initEngine";
                eresult["input"] = arg;
                eresult["ret"] = "code:-1 msg:引擎已存在";
                ResultHandle?.Invoke(eresult);
                return true;
            }
            if (!arg.ContainsKey("appkey"))
            {
                ExampleUtils.ShowToast("appkey 为空");
                return false;
            }
            string appkey = (string)arg["appkey"];
            RCIMEngineOptions options = new RCIMEngineOptions();
            if (arg.ContainsKey("naviServer"))
            {
                options.naviServer = (string)arg["naviServer"];
            }
            if (arg.ContainsKey("fileServer"))
            {
                options.fileServer = (string)arg["fileServer"];
            }
            if (arg.ContainsKey("statisticServer"))
            {
                options.statisticServer = (string)arg["statisticServer"];
            }
            if (arg.ContainsKey("kickReconnectDevice"))
            {
                int kickReconnectDevice = int.Parse((string)arg["kickReconnectDevice"]);
                options.kickReconnectDevice = kickReconnectDevice == 0 ? true : false;
            }
            RCIMCompressOptions compressOptions = new RCIMCompressOptions();
            if (arg.ContainsKey("originalImageQuality"))
            {
                compressOptions.originalImageQuality = int.Parse((string)arg["originalImageQuality"]);
            }
            if (arg.ContainsKey("originalImageMaxSize"))
            {
                compressOptions.originalImageMaxSize = int.Parse((string)arg["originalImageMaxSize"]);
            }
            if (arg.ContainsKey("thumbnailQuality"))
            {
                compressOptions.thumbnailQuality = int.Parse((string)arg["thumbnailQuality"]);
            }
            if (arg.ContainsKey("thumbnailMaxSize"))
            {
                compressOptions.thumbnailMaxSize = int.Parse((string)arg["thumbnailMaxSize"]);
            }
            if (arg.ContainsKey("thumbnailMinSize"))
            {
                compressOptions.thumbnailMinSize = int.Parse((string)arg["thumbnailMinSize"]);
            }
            if (arg.ContainsKey("sightCompressWidth"))
            {
                compressOptions.sightCompressWidth = int.Parse((string)arg["sightCompressWidth"]);
            }
            if (arg.ContainsKey("sightCompressHeight"))
            {
                compressOptions.sightCompressHeight = int.Parse((string)arg["sightCompressHeight"]);
            }
            if (arg.ContainsKey("locationThumbnailQuality"))
            {
                compressOptions.locationThumbnailQuality = int.Parse((string)arg["locationThumbnailQuality"]);
            }
            if (arg.ContainsKey("locationThumbnailHeight"))
            {
                compressOptions.locationThumbnailHeight = int.Parse((string)arg["locationThumbnailHeight"]);
            }
            if (arg.ContainsKey("locationThumbnailWidth"))
            {
                compressOptions.locationThumbnailWidth = int.Parse((string)arg["locationThumbnailWidth"]);
            }
            options.compressOptions = compressOptions;
            RCIMPushOptions pushOptions = new RCIMPushOptions
            {
                idMI = Config.idMI,
                appKeyMI = Config.appKeyMI,
                appIdMeizu = Config.appIdMeizu,
                appKeyMeizu = Config.appKeyMeizu,
                appKeyOPPO = Config.appKeyOPPO,
                appSecretOPPO = Config.appSecretOPPO,
                enableHWPush = true,
                enableFCM = false,
                enableVIVOPush = true
            };
            options.pushOptions = pushOptions;
            engine = RCIMEngine.Create(appkey, options);
            ExampleUtils.ShowToast("引擎初始化成功");
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "initEngine";
            result["input"] = arg;
            result["ret"] = "code:0 msg:引擎初始化成功";
            ResultHandle?.Invoke(result);
            return true;
        }

        public void destroyEngine()
        {
            if (engine != null)
            {
                engine.Destroy();
                engine = null;
                ExampleUtils.ShowToast("引擎已销毁");
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "destoryEngine";
                result["ret"] = "code:0 msg:引擎已销毁";
                ResultHandle?.Invoke(result);
            }
            else
            {
                ExampleUtils.ShowToast("引擎还未初始化");
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "destoryEngine";
                result["ret"] = "code:-1 msg:引擎还未初始化";
                ResultHandle?.Invoke(result);
            }
        }

#if UNITY_IOS
        public void setDeviceToken(string token)
        {
            engine.SetDeviceToken(token);
        }
#endif

        public void setListener()
        {
            if (engine != null)
            {
                setEngineListener();
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "setListener";
                result["ret"] = "code:0";
                ResultHandle?.Invoke(result);
            }
            else
            {
                ExampleUtils.ShowToast("引擎还未初始化");
                Dictionary<String, object> result = new Dictionary<String, object>();
                result["method"] = "setListener";
                result["ret"] = "code:-1 msg:引擎还未初始化";
                ResultHandle?.Invoke(result);
            }
        }

        public bool sendTextMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            if (!arg.ContainsKey("text"))
            {
                ExampleUtils.ShowToast("text 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType)int.Parse((string)arg["type"]);
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            string text = (string)arg["text"];
            RCIMTextMessage textMessage = engine.CreateTextMessage(type, targetId, channelId, text);
            if (textMessage == null)
            {
                ExampleUtils.ShowToast("消息创建失败");
                return false;
            }
            string pushContent = ExampleUtils.GetValue<string>(arg, "pushContent", null);
            string pushData = ExampleUtils.GetValue<string>(arg, "pushData", null);
            RCIMMessagePushOptions pushOptions = new RCIMMessagePushOptions()
            {
                disableNotification = false,
                pushContent = pushContent,
                pushData = pushData
            };
            textMessage.pushOptions = pushOptions;

            if (arg.ContainsKey("Mentioned"))
            {
                RCIMMentionedType mentionedType = (RCIMMentionedType)int.Parse((string)arg["Mentioned"]);
                List<String> users = null;
                string mention = ExampleUtils.GetValue<string>(arg, "MentionedUser", null);
                if (mention != null)
                {
                    users = new List<String>(mention.Split(','));
                }
                string mentionedText = ExampleUtils.GetValue<string>(arg, "MentionedText", null);
                textMessage.mentionedInfo = new RCIMMentionedInfo()
                {
                    type = mentionedType,
                    userIdList = users,
                    mentionedContent = mentionedText
                };
            }

            return sendMessage(textMessage,arg);
        }

        public bool sendImageMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            ExampleUtils.PickImage((path) =>
            {
                RCIMConversationType type = (RCIMConversationType)int.Parse((string)arg["type"]);
                string targetId = (string)arg["targetId"];
                string channelId = ExampleUtils.GetValue(arg, "channelId", "");
                RCIMImageMessage message = engine.CreateImageMessage(type, targetId, channelId, path);
                sendMessage(message, arg);
            });
            return true;
        }

        public bool sendFileMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            ExampleUtils.PickFile((path) =>
            {
                RCIMConversationType type = (RCIMConversationType)int.Parse((string)arg["type"]);
                string targetId = (string)arg["targetId"];
                string channelId = ExampleUtils.GetValue(arg, "channelId", "");
                RCIMFileMessage message = engine.CreateFileMessage(type, targetId, channelId, path);
                sendMessage(message, arg);
            });
            return true;
        }

        public bool sendSightMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            ExampleUtils.PickVideo((path,duaration) =>
            {
                RCIMConversationType type = (RCIMConversationType)int.Parse((string)arg["type"]);
                string targetId = (string)arg["targetId"];
                string channelId = ExampleUtils.GetValue(arg, "channelId", "");
                RCIMSightMessage message = engine.CreateSightMessage(type, targetId, channelId, path, duaration);
                sendMessage(message, arg);
            });
            return true;
        }

        public bool sendVoiceMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            ExampleUtils.PickAudio((path,duration) =>
            {
                RCIMConversationType type = (RCIMConversationType)int.Parse((string)arg["type"]);
                string targetId = (string)arg["targetId"];
                string channelId = ExampleUtils.GetValue(arg, "channelId", "");
                RCIMVoiceMessage message = engine.CreateVoiceMessage(type, targetId, channelId, path, duration);
                sendMessage(message, arg);
            });
            return true;
        }

        public bool sendReferenceMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            if (!arg.ContainsKey("messageId"))
            {
                ExampleUtils.ShowToast("messageId 为空");
                return false;
            }

            if (!arg.ContainsKey("text"))
            {
                ExampleUtils.ShowToast("text 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType)int.Parse((string)arg["type"]);
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            string messageId = (string)arg["messageId"];
            string text = (string)arg["text"];

            engine.GetMessageById(int.Parse(messageId), new GetMessageListenerProxy((msg) =>
            {
                if (msg == null)
                {
                    ExampleUtils.ShowToast("未查询到引用的message");
                }
                else
                {
                    RCIMReferenceMessage message = engine.CreateReferenceMessage(type, targetId, channelId, msg, text);
                    sendMessage(message, arg);
                }
            }));
            return true;
        }

        public bool sendGIFMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            ExampleUtils.PickFile("gif", (path) =>
            {
                RCIMConversationType type = (RCIMConversationType)int.Parse((string)arg["type"]);
                string targetId = (string)arg["targetId"];
                string channelId = ExampleUtils.GetValue(arg, "channelId", "");
                RCIMGIFMessage message = engine.CreateGIFMessage(type, targetId, channelId, path);
                sendMessage(message, arg);
            });
            return true;
        }

        public bool sendCustomMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            if (!arg.ContainsKey("policy"))
            {
                ExampleUtils.ShowToast("policy 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType)int.Parse((string)arg["type"]);
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            RCIMCustomMessagePolicy policy = (RCIMCustomMessagePolicy)int.Parse((string)arg["policy"]);
            string messageIdentifier = ExampleUtils.GetValue(arg, "messageIdentifier", "");
            Dictionary<string, string> fields = new Dictionary<string, string>();
            string[] keys = ExampleUtils.GetValue(arg, "keys", "").Split(',');
            string[] values = ExampleUtils.GetValue(arg, "values", "").Split(',');
            for (int i = 0; i < keys.Length; i++)
            {
                if (i < values.Length)
                {
                    fields.Add(keys[i], values[i]);
                }                
            }
            RCIMCustomMessage message = engine.CreateCustomMessage(type, targetId, channelId, policy, messageIdentifier, fields);
            return sendMessage(message, arg);
        }

        public bool sendLocationMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            if (!arg.ContainsKey("location"))
            {
                ExampleUtils.ShowToast("location 为空");
                return false;
            }
            if (!arg.ContainsKey("poiName"))
            {
                ExampleUtils.ShowToast("poiname 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType)int.Parse((string)arg["type"]);
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            string location = (string)arg["location"];
            string poiName = (string)arg["poiName"];
            string[] array = location.Split(',');
            if (array.Length != 2)
            {
                ExampleUtils.ShowToast("location 输入错误");
                return false;
            }
            double lon = double.Parse(array[0]);
            double lat = double.Parse(array[1]);
            ExampleUtils.PickImage((path) =>
            {
                RCIMLocationMessage message = engine.CreateLocationMessage(type, targetId, channelId, lon, lat, poiName, path);
                sendMessage(message, arg);
            });
            return true;
        }

        private bool sendMessage(RCIMMessage message, Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (message == null)
            {
                ExampleUtils.ShowToast("message 不合法");
                return false;
            }
            if (message.conversationType == RCIMConversationType.PRIVATE || 
                message.conversationType == RCIMConversationType.GROUP ||
                message.conversationType == RCIMConversationType.ULTRA_GROUP)
            {
                message.expansion = new Dictionary<string, string>();
            }
            else
            {
                message.expansion = null;
            }
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            Dictionary<String, object> result = new Dictionary<String, object>();
            if (message is RCIMMediaMessage)
            {
                RCIMSendMediaMessageListener listener = null;
                if (useCallback != "0")
                {
                    listener = new RCIMSendMediaMessageListenerProxy(ResultHandle);
                }
                int ret = engine.SendMediaMessage((RCIMMediaMessage)message, listener);
                result["method"] = "sendMediaMessage";
                result["ret"] = ret;
            }
            else
            {
                RCIMSendMessageListener listener = null;
                if (useCallback == "1")
                {
                    listener = new RCIMSendMessageListenerProxy(ResultHandle);
                }
                int ret = engine.SendMessage(message, listener);
                result["method"] = "sendMessage";
                result["ret"] = ret;
            }
            result["input"] = arg;
            ResultHandle?.Invoke(result);  
            return true;         
        }
    }
}
