using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cn_rongcloud_im_wapper_unity_example
{
    [Serializable]
    public class Interface
    {
        public string title;
        public Parameter[] parameters;
        public string action;
    }
    
    [Serializable]
    public class Parameter
    {
        public string key;
        public string type;
        public string hint;
        public string lable;
    }
    
    public class IM_TEST_CONFIG
    {
        public static string[] modules = { "链接相关",   "会话相关",   "消息相关", "未读数相关",
                                           "聊天室相关", "超级群相关", "其他配置" };
    
        public static string channelIdString = "仅对超级群生效, 其他类型无需填写";
        public static string conversationTypes = "多个以英文 , 分割  eg: 1,2";
        public static string conversationType = "请输入会话类型  1:单聊, 2:群聊, 3:聊天室, 4:系统, 5:超级群";
        public static string editChannelIdString = "请输入频道 Id";
        public static string editTargetIdString = "请输入会话 Id";
        public static string pushContent = "请输入需要显示的推送内容";
        public static string pushData = "请输入携带的推送数据";
        public static string level =
            "0:全部消息通知（接收全部消息通知--显示指定关闭免打扰功能) 1:未设置(向上查询群或者APP级别设置)存量数据中0表示未设置2:群聊，超级群@所有人或者@成员列表有自己时通知；单聊代表消息不通知3:群聊，超级群@成员列表有自己时通知，@所有人不通知；单聊代表消息不通知4:群聊，超级群@所有人通知，其他情况都不通知；单聊代表消息不通知5:消息通知被屏蔽，即不接收消息通知";
        public static string policy =
            "请输入存储策略 0: 客户端不存储，支持离线消息机制，不计入未读消息数 1:客户端存储，支持离线消息机制，且存入服务端历史消息，计入未读消息 2:客户端不存储，服务端不存储，不计入未读消息数 3:客户端存储，支持离线消息机制，且存入服务端历史消息，不计入未读消息数";
    
        public static Dictionary<string, string> common_info =
            new Dictionary<string, string>() { { "channelIdString", channelIdString },
                                               { "conversationTypes", conversationTypes },
                                               { "conversationType", conversationType },
                                               { "editChannelIdString", editChannelIdString },
                                               { "editTargetIdString", editTargetIdString },
                                               { "pushContent", pushContent },
                                               { "pushData", pushData },
                                               { "level", level },
                                               { "policy", policy } };
    
#region 链接相关
        public static string initEngine =
            "{\"title\":\"初始化引擎\",\"parameters\":[{\"key\":\"appkey\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入 AppKey\"},{\"key\":\"naviServer\",\"type\":\"String\",\"hint\":\"非必填\",\"lable\":\"请输入导航地址\"},{\"key\":\"fileServer\",\"type\":\"String\",\"hint\":\"非必填\",\"lable\":\"请输入文件地址\"},{\"key\":\"statisticServer\",\"type\":\"String\",\"hint\":\"非必填\",\"lable\":\"请输入状态上传地址\"},{\"key\":\"kickReconnectDevice\",\"type\":\"number\",\"hint\":\"非必填 0:不踢出 1:踢出\",\"lable\":\"是否踢出其他重连设备\"},{\"key\":\"originalImageQuality\",\"type\":\"number\",\"hint\":\"非必填\",\"lable\":\"原图压缩比\"},{\"key\":\"originalImageMaxSize\",\"type\":\"number\",\"hint\":\"非必填\",\"lable\":\"原图大小限制 配置发送图片时，如果图片大小不超过则发送原图\"},{\"key\":\"thumbnailQuality\",\"type\":\"number\",\"hint\":\"非必填\",\"lable\":\"缩略图压缩比例\"},{\"key\":\"thumbnailMaxSize\",\"type\":\"number\",\"hint\":\"非必填\",\"lable\":\"缩略图压缩宽、高\"},{\"key\":\"thumbnailMinSize\",\"type\":\"number\",\"hint\":\"非必填\",\"lable\":\"缩略图压缩最小宽、高\"},{\"key\":\"sightCompressWidth\",\"type\":\"number\",\"hint\":\"非必填\",\"lable\":\"小视频压缩宽度,建议使用16的倍数\"},{\"key\":\"sightCompressHeight\",\"type\":\"number\",\"hint\":\"非必填\",\"lable\":\"小视频压缩高度，建议使用16的倍数\"},{\"key\":\"locationThumbnailQuality\",\"type\":\"number\",\"hint\":\"非必填\",\"lable\":\"位置消息缩略图压缩比例\"},{\"key\":\"locationThumbnailWidth\",\"type\":\"number\",\"hint\":\"非必填\",\"lable\":\"位置消息缩略图高度\"},{\"key\":\"locationThumbnailHeight\",\"type\":\"number\",\"hint\":\"非必填\",\"lable\":\"位置消息缩略图宽度\"}],\"action\":\"initEngine\"}";
        public static string destoryEngine = "{\"title\":\"销毁引擎\",\"action\":\"destroyEngine\"}";
        public static string setListener = "{\"title\":\"设置监听\",\"action\":\"setListener\"}";
        public static string connect =
            "{\"title\":\"连接\",\"parameters\":[{\"key\":\"token\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入 Token\"},{\"key\":\"timeout\",\"type\":\"number\",\"hint\":\"0 为一直重连, 非 0 为最多连接多少秒\",\"lable\":\"请输入超时时间\"}],\"action\":\"connect\"}";
        public static string disconnect =
            "{\"title\":\"断开连接\",\"parameters\":[{\"key\":\"receivePush\",\"type\":\"number\",\"hint\":\"0:不接收推送 , 1:接收推送\",\"lable\":\"断开后是否接收推送\"}],\"action\":\"disconnect\"}";
        public static string[] connect_module = { initEngine, destoryEngine, setListener, connect, disconnect };
#endregion
    
#region 会话相关
        public static string loadConversations =
            "{\"title\":\"获取会话列表\",\"parameters\":[{\"key\":\"conversationTypes\",\"type\":\"String\",\"hint\":\"COMMON.conversationTypes\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"channelId\",\"type\":\"String\",\"lable\":\"COMMON.editChannelIdString\",\"hint\":\"COMMON.channelIdString\"},{\"key\":\"count\",\"type\":\"number\",\"lable\":\"请输入查询数量\",\"hint\":\"大于 0 小于等于 50\"},{\"key\":\"startTime\",\"type\":\"number\",\"lable\":\"请输入开始时间(时间戳单位: 毫秒)\",\"hint\":\"0: 查询所有\"}],\"action\":\"getConversations\"}";
        public static string loadConversation =
            "{\"title\":\"获取某个会话\",\"parameters\":[{\"key\":\"type\",\"type\":\"number\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"}],\"action\":\"getConversation\"}";
        public static string removeConversation =
            "{\"title\":\"移除某个会话\",\"parameters\":[{\"key\":\"type\",\"type\":\"number\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"}],\"action\":\"removeConversation\"}";
        public static string removeConversations =
            "{\"title\":\"移除某些会话\",\"parameters\":[{\"key\":\"conversationTypes\",\"type\":\"number\",\"hint\":\"COMMON.conversationTypes\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"}],\"action\":\"removeConversations\"}";
        public static string saveDraftMessage =
            "{\"title\":\"保存草稿信息\",\"parameters\":[{\"key\":\"type\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"},{\"key\":\"draft\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入草稿内容\"}],\"action\":\"saveDraftMessage\"}";
        public static string loadDraftMessage =
            "{\"title\":\"加载某个会话的草稿信息\",\"parameters\":[{\"key\":\"type\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"}],\"action\":\"getDraftMessage\"}";
        public static string clearDraftMessage =
            "{\"title\":\"清除某个会话的草稿信息\",\"parameters\":[{\"key\":\"type\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"}],\"action\":\"clearDraftMessage\"}";
        public static string changeConversationNotificationLevel =
            "{\"title\":\"改变会话的提醒状态\",\"parameters\":[{\"key\":\"type\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"},{\"key\":\"level\",\"type\":\"number\",\"hint\":\"\",\"lable\":\"COMMON.level\"}],\"action\":\"changeConversationNotificationLevel\"}";
        public static string loadConversationNotificationLevel =
            "{\"title\":\"加载会话的提醒状态\",\"parameters\":[{\"key\":\"type\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"}],\"action\":\"getConversationNotificationLevel\"}";
        public static string loadBlockedConversations =
            "{\"title\":\"获取所有免打扰会话\",\"parameters\":[{\"key\":\"conversationTypes\",\"type\":\"String\",\"hint\":\"COMMON.conversationTypes\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"}],\"action\":\"getBlockedConversations\"}";
        public static string changeConversationTopStatus =
            "{\"title\":\"修改会话置顶状态\",\"parameters\":[{\"key\":\"type\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"},{\"key\":\"top\",\"type\":\"String\",\"hint\":\"0: 取消置顶  1: 置顶\",\"lable\":\"是否置顶\"}],\"action\":\"changeConversationTopStatus\"}";
        public static string loadConversationTopStatus =
            "{\"title\":\"加载会话置顶状态\",\"parameters\":[{\"key\":\"type\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"}],\"action\":\"getConversationTopStatus\"}";
        public static string syncConversationReadStatus =
            "{\"title\":\"同步会话阅读状态\",\"parameters\":[{\"key\":\"type\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"},{\"key\":\"timestamp\",\"type\":\"number\",\"hint\":\"会话中已读的最后一条消息的发送时间戳\",\"lable\":\"请输入同步的时间(时间戳单位: 毫秒)\"}],\"action\":\"syncConversationReadStatus\"}";
        public static string searchConversations =
            "{\"title\":\"搜索会话\",\"parameters\":[{\"key\":\"conversationTypes\",\"type\":\"String\",\"hint\":\"COMMON.conversationTypes\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"},{\"key\":\"messageTypes\",\"type\":\"String\",\"hint\":\"1:自定义, 2:文本\",\"lable\":\"请输入查询的消息类型,多个以英文,分割\"},{\"key\":\"keyword\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入关键字\"}],\"action\":\"searchConversations\"}";
        public static string loadMessageCount =
            "{\"title\":\"加载会话消息数\",\"parameters\":[{\"key\":\"type\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"}],\"action\":\"getMessageCount\"}";
        public static string loadTopConversations =
            "{\"title\":\"获取置顶会话\",\"parameters\":[{\"key\":\"conversationTypes\",\"type\":\"String\",\"hint\":\"COMMON.conversationTypes\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"}],\"action\":\"getTopConversations\"}";
        public static string changeConversationTypeNotificationLevel =
            "{\"title\":\"设置会话类型的消息提醒状态\",\"parameters\":[{\"key\":\"type\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入type\"},{\"key\":\"level\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入level\"}],\"action\":\"changeConversationTypeNotificationLevel\"}";
        public static string loadConversationTypeNotificationLevel =
            "{\"title\":\"获取会话类型的消息提醒状态\",\"parameters\":[{\"key\":\"type\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入type\"}],\"action\":\"getConversationTypeNotificationLevel\"}";
    
        public static string[] conversation_module = { loadConversations,
                                                       loadConversation,
                                                       removeConversation,
                                                       removeConversations,
                                                       saveDraftMessage,
                                                       loadDraftMessage,
                                                       clearDraftMessage,
                                                       changeConversationNotificationLevel,
                                                       loadConversationNotificationLevel,
                                                       loadBlockedConversations,
                                                       changeConversationTopStatus,
                                                       loadConversationTopStatus,
                                                       syncConversationReadStatus,
                                                       searchConversations,
                                                       loadMessageCount,
                                                       loadTopConversations };
#endregion
    
#region 消息相关
        public static string sendTextMessage =
            "{\"title\":\"发送文本消息\",\"parameters\":[{\"key\":\"type\",\"type\":\"number\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"},{\"key\":\"text\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入发送文本\"},{\"key\":\"pushContent\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.pushContent\"},{\"key\":\"pushData\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.pushData\"},{\"key\":\"Mentioned\",\"type\":\"number\",\"hint\":\"\",\"lable\":\"选择@类型0: @ 所有人,1: @指定的用户\"},{\"key\":\"MentionedUser\",\"type\":\"String\",\"hint\":\"多个以英文,分割 eg: user1,user2 \",\"lable\":\"请输入 @的用户 \"},{\"key\":\"MentionedText\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入 @文本 \"}],\"action\":\"sendTextMessage\"}";
        public static string sendImageMessage =
            "{\"title\":\"发送图片消息\",\"parameters\":[{\"key\":\"type\",\"type\":\"number\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"},{\"key\":\"pushContent\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.pushContent\"},{\"key\":\"pushData\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.pushData\"}],\"action\":\"sendImageMessage\"}";
        public static string sendFileMessage =
            "{\"title\":\"发送文件消息\",\"parameters\":[{\"key\":\"type\",\"type\":\"number\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"},{\"key\":\"pushContent\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.pushContent\"},{\"key\":\"pushData\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.pushData\"}],\"action\":\"sendFileMessage\"}";
        public static string sendSightMessage =
            "{\"title\":\"发送小视频消息\",\"parameters\":[{\"key\":\"type\",\"type\":\"number\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"},{\"key\":\"pushContent\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.pushContent\"},{\"key\":\"pushData\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.pushData\"}],\"action\":\"sendSightMessage\"}";
        public static string sendVoiceMessage =
            "{\"title\":\"发送语音消息\",\"parameters\":[{\"key\":\"type\",\"type\":\"number\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"},{\"key\":\"pushContent\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.pushContent\"},{\"key\":\"pushData\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.pushData\"}],\"action\":\"sendVoiceMessage\"}";
        public static string sendReferenceMessage =
            "{\"title\":\"发送引用消息\",\"parameters\":[{\"key\":\"type\",\"type\":\"number\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"},{\"key\":\"messageId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入引用的消息 Id\"},{\"key\":\"text\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入引用文本\"},{\"key\":\"pushContent\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.pushContent\"},{\"key\":\"pushData\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.pushData\"}],\"action\":\"sendReferenceMessage\"}";
        public static string sendGIFMessage =
            "{\"title\":\"发送GIF消息\",\"parameters\":[{\"key\":\"type\",\"type\":\"number\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"},{\"key\":\"pushContent\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.pushContent\"},{\"key\":\"pushData\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.pushData\"}],\"action\":\"sendGIFMessage\"}";
        public static string sendCustomMessage =
            "{\"title\":\"发送自定义消息\",\"parameters\":[{\"key\":\"type\",\"type\":\"number\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"},{\"key\":\"policy\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.policy\"},{\"key\":\"messageIdentifier\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入标识符\"},{\"key\":\"keys\",\"type\":\"String\",\"hint\":\"多个以英文 , 分割  eg: key1,key2\",\"lable\":\"请输入自定义内容的键\"},{\"key\":\"values\",\"type\":\"String\",\"hint\":\"多个以英文 , 分割  eg: value1,value2\",\"lable\":\"请输入自定义内容的值\"},{\"key\":\"pushContent\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.pushContent\"},{\"key\":\"pushData\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.pushData\"}],\"action\":\"sendCustomMessage\"}";
        public static string sendLocationMessage =
            "{\"title\":\"发送位置消息\",\"parameters\":[{\"key\":\"type\",\"type\":\"number\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"},{\"key\":\"location\",\"type\":\"String\",\"hint\":\"以英文 , 分割  eg: 经度,纬度\",\"lable\":\"位置经纬度\"},{\"key\":\"poiName\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"POI 信息\"}],\"action\":\"sendLocationMessage\"}";
        public static string cancelSendingMediaMessage =
            "{\"title\":\"取消发送中的媒体消息\",\"parameters\":[{\"key\":\"messageId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入取消发送的消息 Id\"}],\"action\":\"cancelSendingMediaMessage\"}";
        public static string downloadMediaMessage =
            "{\"title\":\"下载媒体消息\",\"parameters\":[{\"key\":\"messageId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入下载的消息 Id\"}],\"action\":\"downloadMediaMessage\"}";
        public static string cancelDownloadingMediaMessage =
            "{\"title\":\"取消下载媒体消息\",\"parameters\":[{\"key\":\"messageId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入引用的消息 Id\"}],\"action\":\"cancelDownloadingMediaMessage\"}";
        public static string sendTypingStatus =
            "{\"title\":\"发送输入状态消息\",\"parameters\":[{\"key\":\"type\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"},{\"key\":\"currentType\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入当前的状态\"}],\"action\":\"sendTypingStatus\"}";
        public static string loadMessages =
            "{\"title\":\"加载消息\",\"parameters\":[{\"key\":\"type\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"},{\"key\":\"sentTime\",\"type\":\"number\",\"hint\":\"\",\"lable\":\"请输入开始时间(时间戳单位: 毫秒)\"},{\"key\":\"order\",\"type\":\"number\",\"hint\":\"\",\"lable\":\"请输入时间策略  0:获取小于 sentTime 的消息 1:获取大于 sentTime 的消息\"},{\"key\":\"policy\",\"type\":\"number\",\"hint\":\"0: 仅本地 1: 仅远端  2: 本地和远端\",\"lable\":\"请输入加载策略\"},{\"key\":\"count\",\"type\":\"number\",\"hint\":\"最多获取 20 条\",\"lable\":\"请输入获取数量\"}],\"action\":\"getMessages\"}";
        public static string getMessageById =
            "{\"title\":\"根据消息ID加载消息\",\"parameters\":[{\"key\":\"messageId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入messageId\"}],\"action\":\"getMessageById\"}";
        public static string getMessageByUId =
            "{\"title\":\"根据远端 UID 加载消息\",\"parameters\":[{\"key\":\"messageUId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入messageUId\"}],\"action\":\"getMessageByUId\"}";
        public static string loadFirstUnreadMessage =
            "{\"title\":\"加载某个会话的第一条未读消息\",\"parameters\":[{\"key\":\"type\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"}],\"action\":\"getFirstUnreadMessage\"}";
        public static string loadUnreadMentionedMessages =
            "{\"title\":\"加载某个会话所有未读的@消息\",\"parameters\":[{\"key\":\"type\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"}],\"action\":\"getUnreadMentionedMessages\"}";
        public static string insertMessage =
            "{\"title\":\"插入一条消息\",\"parameters\":[{\"key\":\"type\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"}],\"action\":\"insertMessage\"}";
        public static string insertMessages =
            "{\"title\":\"插入多条消息\",\"parameters\":[{\"key\":\"type\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"}],\"action\":\"insertMessages\"}";
        public static string clearMessages =
            "{\"title\":\"删除消息\",\"parameters\":[{\"key\":\"type\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editChannelIdString\"},{\"key\":\"timestamp\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"清除消息截止时间戳,0 <= recordTime <= 当前会话最后一条消息的 sentTime, 0 清除所有消息，其他值清除小于等于 recordTime 的消息\"},{\"key\":\"policy\",\"type\":\"String\",\"hint\":\"0: 仅本地 1: 仅远端  2: 本地和远端\",\"lable\":\"请输入policy\"}],\"action\":\"clearMessages\"}";
        public static string deleteLocalMessages =
            "{\"title\":\"删除本地消息\",\"parameters\":[{\"key\":\"messageIds\",\"type\":\"String\",\"hint\":\"多个以英文 , 分割  eg: 1,2\",\"lable\":\"请输入需要删除的消息Id\"}],\"action\":\"deleteLocalMessages\"}";
        public static string deleteMessages =
            "{\"title\":\"删除消息(本地远端同时删除)\",\"parameters\":[{\"key\":\"type\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editChannelIdString\"},{\"key\":\"messageIds\",\"type\":\"String\",\"hint\":\"多个以英文 , 分割  eg: 1,2\",\"lable\":\"请输入需要删除的消息 Id\"}],\"action\":\"deleteMessages\"}";
        public static string recallMessage =
            "{\"title\":\"撤回某条消息\",\"parameters\":[{\"key\":\"messageId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入撤回的消息 Id\"}],\"action\":\"recallMessage\"}";
        public static string sendPrivateReadReceiptMessage =
            "{\"title\":\"发送单聊的已读回执\",\"parameters\":[{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editChannelIdString\"},{\"key\":\"timestamp\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"该会话中已读的最后一条消息的发送时间戳\"}],\"action\":\"sendPrivateReadReceiptMessage\"}";
        public static string sendGroupReadReceiptRequest =
            "{\"title\":\"发送群聊的已读回执请求\",\"parameters\":[{\"key\":\"messageId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入需要回执的消息 Id\"}],\"action\":\"sendGroupReadReceiptRequest\"}";
        public static string sendGroupReadReceiptResponse =
            "{\"title\":\"发送群聊的已读回执响应\",\"parameters\":[{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editChannelIdString\"},{\"key\":\"messageIds\",\"type\":\"String\",\"hint\":\"多个以英文 , 分割  eg: 1,2\",\"lable\":\"请输入响应的消息 Id\"}],\"action\":\"sendGroupReadReceiptResponse\"}";
        public static string updateMessageExpansion =
            "{\"title\":\"更新消息扩展\",\"parameters\":[{\"key\":\"messageUId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入messageUId\"},{\"key\":\"keys\",\"type\":\"String\",\"hint\":\"Key 支持大小写英文字母、数字、部分特殊符号 + = - _ 的组合方式，不支持汉字\",\"lable\":\"请输入keys\"},{\"key\":\"values\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入values\"}],\"action\":\"updateMessageExpansion\"}";
        public static string removeMessageExpansionForKeys =
            "{\"title\":\"根据 key 移除消息扩展\",\"parameters\":[{\"key\":\"messageUId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入messageUId\"},{\"key\":\"keys\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入keys\"}],\"action\":\"removeMessageExpansionForKeys\"}";
        public static string changeMessageSentStatus =
            "{\"title\":\"修改消息的发送状态\",\"parameters\":[{\"key\":\"messageId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入messageId\"},{\"key\":\"sentStatus\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入sentStatus\"}],\"action\":\"changeMessageSentStatus\"}";
        public static string changeMessageReceiveStatus =
            "{\"title\":\"修改消息的接收状态\",\"parameters\":[{\"key\":\"messageId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入messageId\"},{\"key\":\"receivedStatus\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入receivedStatus\"}],\"action\":\"changeMessageReceiveStatus\"}";
        public static string searchMessages =
            "{\"title\":\"搜索消息\",\"parameters\":[{\"key\":\"type\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editChannelIdString\"},{\"key\":\"keyword\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入keyword\"},{\"key\":\"startTime\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入startTime\"},{\"key\":\"count\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入count\"}],\"action\":\"searchMessages\"}";
        public static string searchMessagesByTimeRange =
            "{\"title\":\"根据时间段搜索消息\",\"parameters\":[{\"key\":\"type\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editChannelIdString\"},{\"key\":\"keyword\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入keyword\"},{\"key\":\"startTime\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入startTime\"},{\"key\":\"endTime\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入endTime\"},{\"key\":\"offset\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入offset\"},{\"key\":\"count\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入count\"}],\"action\":\"searchMessagesByTimeRange\"}";
        public static string searchMessagesByUserId =
            "{\"title\":\"根据用户ID 搜索消息\",\"parameters\":[{\"key\":\"userId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入userId\"},{\"key\":\"type\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editChannelIdString\"},{\"key\":\"startTime\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入startTime\"},{\"key\":\"count\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入count\"}],\"action\":\"searchMessagesByUserId\"}";
        public static string sendGroupMessageToDesignatedUsers =
            "{\"title\":\"发送群聊定向消息\",\"parameters\":[{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editChannelIdString\"},{\"key\":\"userIds\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入userIds\"}],\"action\":\"sendGroupMessageToDesignatedUsers\"}";
        public static string[] message_module = { sendTextMessage,
                                                  sendImageMessage,
                                                  sendFileMessage,
                                                  sendVoiceMessage,
                                                  sendSightMessage,
                                                  sendReferenceMessage,
                                                  sendGIFMessage,
                                                  sendCustomMessage,
                                                  sendLocationMessage,
                                                  cancelSendingMediaMessage,
                                                  downloadMediaMessage,
                                                  cancelDownloadingMediaMessage,
                                                  sendTypingStatus,
                                                  loadMessages,
                                                  getMessageById,
                                                  getMessageByUId,
                                                  loadFirstUnreadMessage,
                                                  loadUnreadMentionedMessages,
                                                  insertMessage,
                                                  insertMessages,
                                                  clearMessages,
                                                  deleteLocalMessages,
                                                  deleteMessages,
                                                  recallMessage,
                                                  sendPrivateReadReceiptMessage,
                                                  sendGroupReadReceiptRequest,
                                                  sendGroupReadReceiptResponse,
                                                  updateMessageExpansion,
                                                  removeMessageExpansionForKeys,
                                                  changeMessageSentStatus,
                                                  changeMessageReceiveStatus,
                                                  searchMessages,
                                                  searchMessagesByTimeRange,
                                                  searchMessagesByUserId,
                                                  sendGroupMessageToDesignatedUsers };
#endregion
    
#region 未读数相关
        public static string loadUnreadCount =
            "{\"title\":\"加载某个会话的未读数\",\"parameters\":[{\"key\":\"type\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"}],\"action\":\"getUnreadCount\"}";
        public static string loadTotalUnreadCount =
            "{\"title\":\"加载所有未读数\",\"parameters\":[{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"}],\"action\":\"getTotalUnreadCount\"}";
        public static string loadUnreadMentionedCount =
            "{\"title\":\"加载所有@未读数\",\"parameters\":[{\"key\":\"type\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"}],\"action\":\"getUnreadMentionedCount\"}";
        public static string loadUnreadCountByConversationTypes =
            "{\"title\":\"根据会话类型查询未读数\",\"parameters\":[{\"key\":\"conversationTypes\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入conversationTypes\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"},{\"key\":\"contain\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入contain\"}],\"action\":\"getUnreadCountByConversationTypes\"}";
        public static string clearUnreadCount =
            "{\"title\":\"清除某个会话未读数\",\"parameters\":[{\"key\":\"type\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"},{\"key\":\"timestamp\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入timestamp\"}],\"action\":\"clearUnreadCount\"}";
        public static string[] unread_module = { loadUnreadCount, loadTotalUnreadCount, loadUnreadCountByConversationTypes,
                                                 loadUnreadMentionedCount, clearUnreadCount };
#endregion
    
#region 聊天室相关
        public static string joinChatRoom =
            "{\"title\":\"加入聊天室\",\"parameters\":[{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入聊天室房间 ID\"},{\"key\":\"messageCount\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入加入时拉取的历史消息数\"},{\"key\":\"autoCreate\",\"type\":\"String\",\"hint\":\"0: 不创建 1: 创建\",\"lable\":\"聊天室不存在时是否自动创建\"}],\"action\":\"joinChatRoom\"}";
        public static string leaveChatRoom =
            "{\"title\":\"离开聊天室\",\"parameters\":[{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入聊天室房间 ID\"}],\"action\":\"leaveChatRoom\"}";
        public static string loadChatRoomMessages =
            "{\"title\":\"加载聊天室消息\",\"parameters\":[{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入聊天室房间 ID\"},{\"key\":\"timestamp\",\"type\":\"String\",\"hint\":\"起始的消息发送时间戳\",\"lable\":\"请输入timestamp\"},{\"key\":\"order\",\"type\":\"String\",\"hint\":\"拉取顺序 0:倒序,1:正序\",\"lable\":\"请输入order\"},{\"key\":\"count\",\"type\":\"String\",\"hint\":\"要获取的消息数量, 0 < count <= 50。\",\"lable\":\"请输入count\"}],\"action\":\"getChatRoomMessages\"}";
        public static string addChatRoomEntry =
            "{\"title\":\"添加聊天室 KV\",\"parameters\":[{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"key\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入key\"},{\"key\":\"value\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入value\"},{\"key\":\"deleteWhenLeft\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"用户掉线或退出时，是否自动删除该 Key、Value 值\"},{\"key\":\"overwrite\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"如果当前 key 存在，是否进行覆盖\"}],\"action\":\"addChatRoomEntry\"}";
        public static string addChatRoomEntries =
            "{\"title\":\"添加多个聊天室 kv\",\"parameters\":[{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"keys\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入keys\"},{\"key\":\"values\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入values\"},{\"key\":\"deleteWhenLeft\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"用户掉线或退出时，是否自动删除该 Key、Value 值\"},{\"key\":\"overwrite\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"如果当前 key 存在，是否进行覆盖\"}],\"action\":\"addChatRoomEntries\"}";
        public static string loadChatRoomEntry =
            "{\"title\":\"加载聊天室 KV \",\"parameters\":[{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"key\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入key\"}],\"action\":\"getChatRoomEntry\"}";
        public static string loadAllChatRoomEntries =
            "{\"title\":\"加载聊天室 所有KV \",\"parameters\":[{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"}],\"action\":\"getChatRoomAllEntries\"}";
        public static string removeChatRoomEntry =
            "{\"title\":\"移除聊天室 KV \",\"parameters\":[{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"key\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入key\"},{\"key\":\"force\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"是否强制删除\"}],\"action\":\"removeChatRoomEntry\"}";
        public static string removeChatRoomEntries =
            "{\"title\":\"移除多个聊天室 KV \",\"parameters\":[{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"keys\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入keys\"},{\"key\":\"force\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"是否强制删除\"}],\"action\":\"removeChatRoomEntries\"}";
        public static string[] room_module = {
            joinChatRoom,      leaveChatRoom,          loadChatRoomMessages, addChatRoomEntry,      addChatRoomEntries,
            loadChatRoomEntry, loadAllChatRoomEntries, removeChatRoomEntry,  removeChatRoomEntries,
        };
#endregion
    
#region 超级群相关
        public static string syncUltraGroupReadStatus =
            "{\"title\":\"上报超级群的已读时间\",\"parameters\":[{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"},{\"key\":\"timestamp\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入timestamp\"}],\"action\":\"syncUltraGroupReadStatus\"}";
        public static string loadConversationsForAllChannel =
            "{\"title\":\"获取特定会话下所有频道的会话列表\",\"parameters\":[{\"key\":\"type\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.conversationType\"},{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"}],\"action\":\"getConversationsForAllChannel\"}";
        public static string loadUltraGroupUnreadMentionedCount =
            "{\"title\":\"根据会话 id 获取所有子频道的 @ 未读消息总数\",\"parameters\":[{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"}],\"action\":\"getUltraGroupUnreadMentionedCount\"}";
        public static string modifyUltraGroupMessage =
            "{\"title\":\"消息修改\",\"parameters\":[{\"key\":\"messageUId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入messageUId\"}],\"action\":\"modifyUltraGroupMessage\"}";
        public static string recallUltraGroupMessage =
            "{\"title\":\"撤回消息\",\"parameters\":[{\"key\":\"messageId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入需要撤回的消息 id\"},{\"key\":\"deleteRemote\",\"type\":\"number\",\"hint\":\"0:不删除 1:删除\",\"lable\":\"是否需要删除远端消息\"}],\"action\":\"recallUltraGroupMessage\"}";
        public static string clearUltraGroupMessages =
            "{\"title\":\"根据时间戳清除超级群消息\",\"parameters\":[{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"},{\"key\":\"timestamp\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入timestamp\"},{\"key\":\"policy\",\"type\":\"String\",\"hint\":\"0:本地 1:远端 2:本地远端\",\"lable\":\"请输入清除策略\"}],\"action\":\"clearUltraGroupMessages\"}";
        public static string sendUltraGroupTypingStatus =
            "{\"title\":\"向会话中发送正在输入的状态\",\"parameters\":[{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"COMMON.channelIdString\",\"lable\":\"COMMON.editChannelIdString\"},{\"key\":\"typingStatus\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入typingStatus\"}],\"action\":\"sendUltraGroupTypingStatus\"}";
        public static string clearUltraGroupMessagesForAllChannel =
            "{\"title\":\"删除本地所有 channel 特定时间之前的消息\",\"parameters\":[{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.editTargetIdString\"},{\"key\":\"timestamp\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入timestamp\"}],\"action\":\"clearUltraGroupMessagesForAllChannel\"}";
        public static string loadBatchRemoteUltraGroupMessages =
            "{\"title\":\"获取同一个超级群下的批量服务消息（含所有频道）\",\"parameters\":[{\"key\":\"messageIds\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入 messageIds\"}],\"action\":\"loadBatchRemoteUltraGroupMessages\"}";
        public static string updateUltraGroupMessageExpansion =
            "{\"title\":\"更新消息扩展信息\",\"parameters\":[{\"key\":\"messageUId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入messageUId\"},{\"key\":\"keys\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入keys\"},{\"key\":\"values\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入values\"}],\"action\":\"updateUltraGroupMessageExpansion\"}";
        public static string removeUltraGroupMessageExpansion =
            "{\"title\":\"删除消息扩展信息中特定的键值对\",\"parameters\":[{\"key\":\"messageUId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入messageUId\"},{\"key\":\"keys\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入keys\"}],\"action\":\"removeUltraGroupMessageExpansionForKeys\"}";
        public static string changeUltraGroupDefaultNotificationLevel =
            "{\"title\":\"设置超级群的默认消息状态\",\"parameters\":[{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入targetId\"},{\"key\":\"level\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.level\"}],\"action\":\"changeUltraGroupDefaultNotificationLevel\"}";
        public static string loadUltraGroupDefaultNotificationLevel =
            "{\"title\":\"获取超级群的默认消息状态\",\"parameters\":[{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入targetId\"}],\"action\":\"getUltraGroupDefaultNotificationLevel\"}";
        public static string changeUltraGroupChannelDefaultNotificationLevel =
            "{\"title\":\"设置超级群频道的默认消息状态\",\"parameters\":[{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入targetId\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入channelId\"},{\"key\":\"level\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"COMMON.level\"}],\"action\":\"changeUltraGroupChannelDefaultNotificationLevel\"}";
        public static string loadUltraGroupChannelDefaultNotificationLevel =
            "{\"title\":\"获取超级群频道的默认消息状态\",\"parameters\":[{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入targetId\"},{\"key\":\"channelId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入channelId\"}],\"action\":\"getUltraGroupChannelDefaultNotificationLevel\"}";
        public static string loadUltraGroupAllUnreadCount =
            "{\"title\":\"所有超级群会话的未读消息数的总和\",\"action\":\"getUltraGroupAllUnreadCount\"}";
        public static string loadUltraGroupAllUnreadMentionedCount =
            "{\"title\":\"所有超级群会话中的未读 @ 消息数的总和\",\"action\":\"getUltraGroupAllUnreadMentionedCount\"}";
        public static string loadUltraGroupUnreadCount =
            "{\"title\":\"获取指定会话的未读消息数\",\"parameters\":[{\"key\":\"targetId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入targetId\"}],\"action\":\"getUltraGroupUnreadCount\"}";
        public static string[] ultra_module = {
            syncUltraGroupReadStatus,
            loadConversationsForAllChannel,
            loadUltraGroupUnreadMentionedCount,
            modifyUltraGroupMessage,
            recallUltraGroupMessage,
            clearUltraGroupMessages,
            sendUltraGroupTypingStatus,
            clearUltraGroupMessagesForAllChannel,
            loadBatchRemoteUltraGroupMessages,
            updateUltraGroupMessageExpansion,
            removeUltraGroupMessageExpansion,
            changeUltraGroupDefaultNotificationLevel,
            loadUltraGroupDefaultNotificationLevel,
            changeUltraGroupChannelDefaultNotificationLevel,
            loadUltraGroupChannelDefaultNotificationLevel,
            loadUltraGroupAllUnreadCount,
            loadUltraGroupAllUnreadMentionedCount,
            loadUltraGroupUnreadCount,
        };
#endregion
    
#region 其他配置
        public static string addToBlacklist =
            "{\"title\":\"添加到黑名单\",\"parameters\":[{\"key\":\"userId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入userId\"}],\"action\":\"addToBlacklist\"}";
        public static string removeFromBlacklist =
            "{\"title\":\"从黑名单移除\",\"parameters\":[{\"key\":\"userId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入userId\"}],\"action\":\"removeFromBlacklist\"}";
        public static string loadBlacklistStatus =
            "{\"title\":\"查询某用户的黑名单状态\",\"parameters\":[{\"key\":\"userId\",\"type\":\"String\",\"hint\":\"\",\"lable\":\"请输入userId\"}],\"action\":\"getBlacklistStatus\"}";
        public static string loadBlacklist = "{\"title\":\"加载黑名单列表\",\"action\":\"getBlacklist\"}";
        public static string changeNotificationQuietHours =
            "{\"title\":\"屏蔽某个时间段的消息提醒\",\"parameters\":[{\"key\":\"startTime\",\"type\":\"String\",\"hint\":\"开始消息免打扰时间，格式为 HH:MM:SS\",\"lable\":\"请输入startTime\"},{\"key\":\"spanMinutes\",\"type\":\"String\",\"hint\":\"需要消息免打扰分钟数,0 < spanMinutes < 1440\",\"lable\":\"请输入spanMinutes\"},{\"key\":\"level\",\"type\":\"String\",\"hint\":\"0:未设置1:群聊超级群仅@消息通知,单聊代表消息不通知2:消息通知被屏蔽\",\"lable\":\"请输入level\"}],\"action\":\"changeNotificationQuietHours\"}";
        public static string removeNotificationQuietHours =
            "{\"title\":\"移除免打扰时间段\",\"action\":\"removeNotificationQuietHours\"}";
        public static string loadNotificationQuietHours =
            "{\"title\":\"获取免打扰时间段\",\"action\":\"getNotificationQuietHours\"}";
        public static string changePushContentShowStatus =
            "{\"title\":\"修改推送内容的显示状态\",\"parameters\":[{\"key\":\"showContent\",\"type\":\"String\",\"hint\":\"是否显示远程推送内容 0:不显示 1:显示\",\"lable\":\"请输入showContent\"}],\"action\":\"changePushContentShowStatus\"}";
        public static string changePushLanguage =
            "{\"title\":\"修改推送语言\",\"parameters\":[{\"key\":\"language\",\"type\":\"String\",\"hint\":\"推送语言，目前仅支持 en_us、zh_cn、ar_sa\",\"lable\":\"请输入language\"}],\"action\":\"changePushLanguage\"}";
        public static string changePushReceiveStatus =
            "{\"title\":\"修改推送的接收状态\",\"parameters\":[{\"key\":\"receive\",\"type\":\"String\",\"hint\":\"是否接收 0:不接收 1:接收\",\"lable\":\"请输入receive\"}],\"action\":\"changePushReceiveStatus\"}";
        public static string changeLogLevel =
            "{\"title\":\"修改日志级别\",\"parameters\":[{\"key\":\"level\",\"type\":\"String\",\"hint\":\"0-5\",\"lable\":\"请输入level\"}],\"action\":\"changeLogLevel\"}";
        public static string[] other_module = { addToBlacklist,
                                                removeFromBlacklist,
                                                loadBlacklistStatus,
                                                loadBlacklist,
                                                changeNotificationQuietHours,
                                                removeNotificationQuietHours,
                                                loadNotificationQuietHours,
                                                changePushContentShowStatus,
                                                changePushLanguage,
                                                changePushReceiveStatus,
                                                changeLogLevel };
#endregion
    
        public static Dictionary<string, string[]> modules_info = new Dictionary<string, string[]>() {
            { "链接相关", connect_module },  { "会话相关", conversation_module }, { "消息相关", message_module },
            { "未读数相关", unread_module }, { "聊天室相关", room_module },       { "超级群相关", ultra_module },
            { "其他配置", other_module },
        };
    
        public static List<string> cb_method_list = new List<string> { "connect",
                                                                       "sendMessage",
                                                                       "sendMediaMessage",
                                                                       "cancelSendingMediaMessage",
                                                                       "downloadMediaMessage",
                                                                       "cancelDownloadingMediaMessage",
                                                                       "getConversation",
                                                                       "getConversations",
                                                                       "removeConversation",
                                                                       "removeConversations",
                                                                       "getUnreadCount",
                                                                       "getTotalUnreadCount",
                                                                       "getUnreadMentionedCount",
                                                                       "getUltraGroupAllUnreadCount",
                                                                       "getUltraGroupAllUnreadMentionedCount",
                                                                       "getUltraGroupUnreadCount",
                                                                       "getUltraGroupUnreadMentionedCount",
                                                                       "getUnreadCountByConversationTypes",
                                                                       "clearUnreadCount",
                                                                       "saveDraftMessage",
                                                                       "getDraftMessage",
                                                                       "clearDraftMessage",
                                                                       "getBlockedConversations",
                                                                       "changeConversationTopStatus",
                                                                       "getConversationTopStatus",
                                                                       "syncConversationReadStatus",
                                                                       "getMessages",
                                                                       "getMessageById",
                                                                       "getMessageByUId",
                                                                       "getFirstUnreadMessage",
                                                                       "getUnreadMentionedMessages",
                                                                       "insertMessage",
                                                                       "insertMessages",
                                                                       "clearMessages",
                                                                       "deleteLocalMessages",
                                                                       "deleteMessages",
                                                                       "recallMessage",
                                                                       "sendPrivateReadReceiptMessage",
                                                                       "sendGroupReadReceiptRequest",
                                                                       "sendGroupReadReceiptResponse",
                                                                       "updateMessageExpansion",
                                                                       "removeMessageExpansionForKeys",
                                                                       "changeMessageSentStatus",
                                                                       "changeMessageReceiveStatus",
                                                                       "joinChatRoom",
                                                                       "leaveChatRoom",
                                                                       "getChatRoomMessages",
                                                                       "addChatRoomEntry",
                                                                       "addChatRoomEntries",
                                                                       "getChatRoomEntry",
                                                                       "getChatRoomAllEntries",
                                                                       "removeChatRoomEntry",
                                                                       "removeChatRoomEntries",
                                                                       "addToBlacklist",
                                                                       "removeFromBlacklist",
                                                                       "getBlacklistStatus",
                                                                       "getBlacklist",
                                                                       "searchMessages",
                                                                       "searchMessagesByTimeRange",
                                                                       "searchMessagesByUserId",
                                                                       "searchConversations",
                                                                       "changeNotificationQuietHours",
                                                                       "removeNotificationQuietHours",
                                                                       "getNotificationQuietHours",
                                                                       "changeConversationNotificationLevel",
                                                                       "getConversationNotificationLevel",
                                                                       "changeConversationTypeNotificationLevel",
                                                                       "getConversationTypeNotificationLevel",
                                                                       "changeUltraGroupDefaultNotificationLevel",
                                                                       "getUltraGroupDefaultNotificationLevel",
                                                                       "changeUltraGroupChannelDefaultNotificationLevel",
                                                                       "getUltraGroupChannelDefaultNotificationLevel",
                                                                       "changePushContentShowStatus",
                                                                       "changePushLanguage",
                                                                       "changePushReceiveStatus",
                                                                       "sendGroupMessageToDesignatedUsers",
                                                                       "getMessageCount",
                                                                       "getTopConversations",
                                                                       "syncUltraGroupReadStatus",
                                                                       "getConversationsForAllChannel",
                                                                       "modifyUltraGroupMessage",
                                                                       "recallUltraGroupMessage",
                                                                       "clearUltraGroupMessages",
                                                                       "sendUltraGroupTypingStatus",
                                                                       "clearUltraGroupMessagesForAllChannel",
                                                                       "getBatchRemoteUltraGroupMessages",
                                                                       "updateUltraGroupMessageExpansion",
                                                                       "removeUltraGroupMessageExpansionForKeys" };
        public static Parameter cb_param =
            new Parameter { key = "use_cb", type = "number", hint = "0为不使用 非0为使用,默认不填为使用",
                            lable = "请输入是否使用callback" };
    }
}