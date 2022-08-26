//
//  Copyright © 2021 RongCloud. All rights reserved.
//

#ifndef RONG_UNITY_IM_H_
#define RONG_UNITY_IM_H_

#import <Foundation/Foundation.h>

#define _CSHARP

extern "C" {

/*!
 默认回调函数
 */

typedef void (*OnImGeneralCallback)(int64_t handle, int errCode);

typedef struct im_general_callback_proxy {
  int64_t handle;
  OnImGeneralCallback callback;
} im_general_callback_proxy;

typedef void (*OnImGeneralIntPtrCallback)(int64_t handle, int errCode, void* intPtr);

typedef void (*OnImGeneralCallbackWithString)(int64_t handle, int errCode, const char* result);

typedef void (*OnImGeneralCallbackWithStringList)(int64_t handle,
                                                  int errCode,
                                                  const char** strObjectList,
                                                  int strObjectListLength);

typedef struct im_general_callback_with_stringlist_proxy {
  int64_t handle;
  OnImGeneralCallbackWithStringList callback;
} im_general_callback_with_stringlist_proxy;

typedef void (*OnImGeneralCallbackWithInt)(int64_t handle, int errCode, int result);

typedef struct im_general_callback_with_int_proxy {
  int64_t handle;
  OnImGeneralCallbackWithInt callback;
} im_general_callback_with_int_proxy;

typedef void (*OnImGeneralCallbackWithLong)(int64_t handle, int errCode, int64_t result);

typedef struct im_general_callback_with_long_proxy {
  int64_t handle;
  OnImGeneralCallbackWithLong callback;
} im_general_callback_with_long_proxy;

typedef void (*OnImGeneralCallbackWithBool)(int64_t handle, int errCode, bool result);

typedef struct im_general_callback_with_bool_proxy {
  int64_t handle;
  OnImGeneralCallbackWithBool callback;
} im_general_callback_with_bool_proxy;

#pragma mark - 连接
/*!
 连接
 */

typedef void (*OnImConnectionStatusChangedListener)(int status);

typedef void (*OnImConnectCallback)(int64_t handle, int errCode, const char* userId);

typedef struct im_connect_callback_proxy {
  int64_t handle;
  OnImConnectCallback callback;
} im_connect_callback_proxy;

#pragma mark - 消息
/*!
 消息
 */
typedef struct im_user_info {
  const char* userId;       // 用户 id
  const char* name;         // 用户名
  const char* portraitUri;  // 用户头像
  const char* alias;        // 备注信息
  const char* extra;        // 附加信息
} im_user_info;

typedef struct im_mentioned_info {
  int type;             // 消息类型
  char** userIds;       // 消息用户 id 列表
  int userIdsLen;       // 消息用户 id 列表长度
  const char* content;  // 消息内容
  BOOL isMentionedMe;   // 是否 @ 了我
} im_mentioned_info;

typedef struct im_message_content {
  im_user_info* user;            // 用户携带用户信息
  im_mentioned_info* mentioned;  // 用于携带提醒消息
  int destructDuration;          // 默认0，表示非阅后即焚
  const char* objectName;
  const char* extra;  // 用于扩展
  char* rawJsonData;  // 对于解码正确的消息，此字段值为 NULL
  int persistent;
} im_message_content;

/**
 约定：im_media_message_content 结构是 im_message_content 的扩展，前面的字段定义和内存布局和
 im_message_content 保持一致。
 */
typedef struct im_media_message_content {
  im_user_info* user;            // 用户携带用户信息
  im_mentioned_info* mentioned;  // 用于携带提醒消息
  int destructDuration;          // 默认0，表示非阅后即焚
  const char* objectName;
  const char* extra;  // 用于扩展
  char* rawJsonData;  // 对于解码正确的消息，此字段值为 NULL
  int persistent;
  const char* localPath;  //  媒体内容的本地路径（此属性必须有值）
  const char* remoteUrl;  // 媒体内容上传服务器后的网络地址（上传成功后 SDK 会为该属性赋值）
  const char* name;  // 媒体内容的文件名（如不传使用 SDK 中 downloadMediaMessage
                     // 方法下载后会默认生成一个名称）
} im_media_message_content;

typedef struct im_ios_config {
  const char* threadId;
  const char* category;
  const char* apnsCollapseId;
  const char* richMediaUri;
} im_ios_config;

typedef struct im_push_config {
  bool disablePushTitle;  // iOS
  const char* pushTitle;
  const char* pushContent;
  const char* pushData;
  bool forceShowDetailContent;
  const char* templateId;
  im_ios_config* iOSConfig;
} im_push_config;

typedef struct im_read_receipt_info {
  bool isReceiptRequestMessage;  // 是否需要回执消息
  bool hasRespond;               // 是否已经发送回执
  int userIdsLen;                // 发送回执的用户列表长度
  const char** userIdKeys;       // 发送回执的用户 id 列表 Key 值数组
  const char** userIdValues;     // 发送回执的用户 id 列表 Value 值数组
} im_read_receipt_info;

typedef struct im_message {
  int conversationType;  // 会话类型
  const char* targetId;  // 会话 id
  int direction;         // 消息方向
  const char* senderId;  // 发送者 id
  int receivedStatus;
  int sentStatus;  // 消息状态
  int64_t receivedTime;
  int64_t sentTime;  // 消息发送时间（服务端生成）
  const char* objectName;
  void* content;  // 消息内容,指向具体的消息类型的实例
  const char* extra;
  int64_t messageId;                      // 数据库中存储 id
  const char* messageUid;                 // 服务生成的消息唯一标识
  im_read_receipt_info* readReceiptInfo;  // 阅读回执信息
  bool canIncludeExpansion;               // 消息是否可以包含扩展信息
  int expansionCount;                     // 可扩展信息包含的键值对长度
  const char** expansionKeys;             // 指向可扩展信息 key 的指针数组
  const char** expansionValues;           // 指向可扩展信息 value 的指针数组

  // message config
  bool disableNotification;
  // end message config

  bool isOffline;  // 是否为离线消息
  im_push_config* pushConfig;
} im_message;

// 消息类型
typedef struct im_text_message {
  im_message_content* msgContent;
  const char* text;
} im_text_message;

typedef struct im_custom_message {
  im_message_content* msgContent;
  int customMessageType;
  int customFieldsCount; // 自定义消息字段数
  const char** customFieldsKeys;             // 指向自定义消息字段 key 的指针数组
  const char** customFieldsValues;           // 指向自定义消息字段 value 的指针数组
  
} im_custom_message;

typedef struct im_rich_content_message {
  im_message_content* msgContent;
  const char* title;     // 图文消息的标题
  const char* digest;    // 图文消息的内容摘要
  const char* imageURL;  // 图文消息图片 URL
  const char* url;       // 图文消息中包含的需要跳转到的URL
} im_rich_content_message;

// checkmsg
typedef struct im_typing_message {
  im_message_content* msgContent;
  const char* data;
  const char* typingContentType;
} im_typing_message;

// checkmsg
typedef struct im_status_message {
  im_message_content* msgContent;
  const char* statusType;
  const char* statusContent;
} im_status_message;

typedef struct im_image_message {
  im_media_message_content* msgContent;
  const char* imageUrl;   // 图片消息的 URL 地址
  const char* localPath;  // 图片的本地路径
  bool isFull;            // 是否发送原图
  const char* extra;      // 图片消息的附加信息
  char* thumbnailBase64String;
} im_image_message;

typedef struct im_voice_message {
  im_message_content* msgContent;
  const char* localPath;
  char* wavDataBase64;  // 语音数据的 Base64 编码
  int64_t duration;     // 语音消息时长，以秒为单位
} im_voice_message;

typedef struct im_hq_voice_message {
  im_media_message_content* msgContent;
  const char* localFileUrl;
  int64_t duration;  // 语音消息的时长，以秒为单位
} im_hq_voice_message;

typedef struct im_sight_message {
  im_media_message_content* msgContent;
  const char* localPath;        // 本地 URL 地址
  const char* sightUrl;         // 网络 URL 地址
  int duration;                 // 视频时长，以秒为单位
  const char* name;             // 小视频文件名
  int64_t size;                 // 文件大小
  const char* thumbnailBase64;  // 缩略图的 base64 编码
} im_sight_message;

typedef struct im_gif_message {
  im_media_message_content* msgContent;
  int64_t width;        // GIF 图的宽
  int64_t height;       // GIF 图的高
  const char* gifUri;   // GIF 本地路径
  int64_t gifDataSize;  // GIF 图的大小，单位字节
  //  const void* gifData;  // GIF 图的数据
} im_gif_message;

typedef struct im_file_message {
  im_media_message_content* msgContent;
  const char* name;       // 文件名
  int64_t size;           // 文件大小，单位为 Byte
  const char* type;       // 文件类型
  const char* fileUrl;    // 文件的网络地址
  const char* localPath;  // 文件的本地路径
} im_file_message;

// typedef struct im_location_message {
//   im_message_content* msgContent;
//   double latitude;             // 地理位置的二维坐标
//   double longitude;            // 地理位置的二维坐标
//   const char* locationName;    // 地理位置的名称
//   char* thumbnailImageBase64;  // 地理位置的缩略图的 Base64 编码
//   const char* extra;           // 地理位置的附加信息
// } im_location_message;

typedef struct im_reference_message {
  im_media_message_content* msgContent;  // 注意：这里和 android 的定义保持一致，使用媒体消息头结构
  const char* content;             // 引用文本
  const char* referMsgUserId;      // 被引用消息的发送者 ID
  void* referMsg;                  // 被引用消息体
  const char* referMsgObjectName;  // 被引用消息的 ObjectName
  const char* referMsgUid;         // 被引用消息的 messageUId。
                            // 服务器消息唯一 ID（在同一个 Appkey 下全局唯一）
} im_reference_message;

// typedef struct im_combine_message {
//   im_media_message_content* msgContent;
//   const char** summaryList;  // 转发的消息展示的缩略内容列表 (格式是发送者 ：缩略内容)
//   int summaryListLen;
//   const char** nameList;  // 转发的全部消息的发送者名称列表 （单聊是经过排重的，群聊是群组名称）
//   int nameListLen;
//   int conversationType;  // 转发的消息会话类型 （目前仅支持单聊和群聊）
//   const char* extra;     // 转发的消息 消息的附加信息
// } im_combine_message;

typedef struct im_recall_notification_message {
  im_message_content* msgContent;
  const char* operatorId;          // 发起撤回操作的用户 ID
  int64_t recallTime;              // 撤回的时间（毫秒）
  const char* originalObjectName;  // 原消息的消息类型名
  bool isAdmin;                    // 是否是管理员操作
  const char* recallContent;       // 撤回的文本消息的内容
  int64_t recallActionTime;        // 撤回动作的时间（毫秒）
} im_recall_notification_message;

typedef struct im_group_notification_message {
  im_message_content* msgContent;
  const char* operation;       // 群组通知的当前操作名
  const char* operatorUserId;  // 当前操作发起用户的用户 ID
  const char* data;            // 当前操作的目标对象
  const char* message;         // 当前操作的消息内容
} im_group_notification_message;

typedef void (*OnImReceivedMessageListener)(
    im_message* imMsg, int left, bool offline, bool hasMsg, int cmdLeft);

typedef enum im_send_message_callback_type {
  IM_SEND_MESSAGE = 0,
  IM_SEND_MESSAGE_ATTACHED = 1,
} im_send_message_callback_type;

typedef void (*OnImSendMessageCallback)(int32_t type,
                                        int64_t msgId,
                                        int errCode,
                                        im_message* imMsg);

typedef void (*OnImGetMessageCallback)(int64_t handle, int errCode, im_message* imMsg);

typedef struct im_get_message_callback_proxy {
  int64_t handle;
  OnImGetMessageCallback callback;
} im_get_message_callback_proxy;

typedef void (*OnImGetMessageListCallback)(int64_t handle,
                                           int errCode,
                                           im_message* imMsgList,
                                           int imMsgListLen);

typedef struct im_get_message_list_callback_proxy {
  int64_t handle;
  OnImGetMessageListCallback callback;
} im_get_message_list_callback_proxy;

typedef struct im_chatroom_histroy_messages {
  im_message* imMsgList;
  int imMsgListCount;
  int64_t syncTime;
} im_chatroom_histroy_messages;

typedef void (*OnImGetChatRoomHistoryMessagesCallback)(
    int64_t handle, int errCode, im_chatroom_histroy_messages* chatRoomHistoryMessages);

typedef struct im_get_chatroom_history_messages_callback_proxy {
  int64_t handle;
  OnImGetChatRoomHistoryMessagesCallback callback;
} im_get_chatroom_history_messages_callback_proxy;

typedef void (*OnImReadReceiptReceivedListener)(int type,
                                                const char* targetId,
                                                const char* senderId,
                                                int64_t lastMessageSentTime);

typedef void (*OnImReadReceiptRequestListener)(int type,
                                               const char* targetId,
                                               const char* messageUid);

typedef void (*OnImReadReceiptResponseListener)(int type,
                                                const char* targetId,
                                                const char* messageUid,
                                                const char** keys,
                                                const char** values,
                                                int kvCount);

typedef void (*OnImRecallMessageCallback)(int64_t handle,
                                          int errCode,
                                          im_recall_notification_message* imRecallNotifyMsg);

typedef struct im_recall_message_callback_proxy {
  int64_t handle;
  OnImRecallMessageCallback callback;
} im_recall_message_callback_proxy;

typedef void (*OnImRecallMessageListener)(im_message* imMsg,
                                          im_recall_notification_message* recallNtfMsg);

typedef void (*OnImCallbackVoid)(void);

typedef void (*OnImDownloadMessageProgress)(im_message* imMsg, int progress);
typedef void (*OnImDownloadMessageCompleted)(im_message* imMsg, const char* mediaPath);
typedef void (*OnImDownloadMessageFailed)(im_message* imMsg, int errCode);
typedef void (*OnImDownloadMessageCanceled)(im_message* imMsg);

typedef void (*OnImMediaMessageSendProgressListener)(im_message* imMsg, int progress);
typedef void (*OnImMediaMessageSendCancelListener)(im_message* message);

#pragma mark - 敏感词
/*!
 消息被拦截类型
 */
typedef enum im_enum_message_block_type {
  IM_MESSAGE_BLOCK_TYPE_NONE = 0,    // 没有设置敏感词类型
  IM_MESSAGE_BLOCK_TYPE_GLOBAL = 1,  // 全局敏感词：命中了融云内置的全局敏感词
  IM_MESSAGE_BLOCK_TYPE_CUSTOM = 2,  // 自定义敏感词拦截：命中了客户在融云自定义的敏感词
  IM_MESSAGE_BLOCK_TYPE_THIRD_PARTY = 3,  // 第三方审核拦截：命中了第三方（数美）
                                          // 或模板路由决定不下发的状态
} im_enum_message_block_type;

typedef struct im_blocked_message_info {
  int type;                              // 会话类型
  const char* targetId;                  // 会话 ID
  const char* blockedMsgUId;             // 被拦截的消息 ID
  im_enum_message_block_type blockType;  // 拦截原因
  const char* extra;                     // 附加信息
} im_blocked_message_info;

typedef void (*OnImBlockedMessageInfoListener)(im_blocked_message_info* imBlockedMsgInfo);

#pragma mark - 消息KV信息修改

typedef void (*OnImMessageExpansionDidUpdateListener)(const char** keys,
                                                      const char** values,
                                                      int kvCount,
                                                      im_message* imMsg);

typedef void (*OnImMessageExpansionDidRemoveListener)(const char** keys,
                                                      int count,
                                                      im_message* imMsg);

#pragma mark - typing status 回调

typedef struct im_typing_status {
  const char* userId;
  const char* typingContentType;
  int64_t sentTime;
} im_typing_status;

typedef void (*OnImTypingStatusChangedListener)(int conversationType,
                                                const char* userId,
                                                im_typing_status* typeStatusList,
                                                int count);

#pragma mark - 通知提醒

typedef void (*OnImGetNotificationQuietHoursCallback)(int64_t handle,
                                                      int errCode,
                                                      const char* startTime,
                                                      int spansMin);

typedef struct im_get_notification_quiet_hours_callback_proxy {
  int64_t handle;
  OnImGetNotificationQuietHoursCallback callback;
} im_get_notification_quiet_hours_callback_proxy;

#pragma mark - 会话

/*!
 会话
 */
typedef struct im_conversation {
  int type;
  const char* targetId;
  const char* channelId;
  const char* title;
  int unreadCount;
  bool isTop;
  int receivedStatus;
  int sentStatus;
  int64_t receivedTime;
  int64_t sentTime;
  const char* draft;
  const char* objectName;
  const char* senderUserId;
  long latestMessageId;
  void* latestMessageContent;  // 消息内容，指向具体类型的消息实例
  int latestMessageDirection;
  char* rawJsonString;
  const char* latestMessageUid;
  bool hasUnreadMentioned;
  int mentionedCount;
  int blockStatus;
} im_conversation;

typedef struct im_conversation_identifier {
  int type;              // 会话类型
  const char* targetId;  // 会话 ID
} im_conversation_identifier;

typedef void (*OnImGetConversationCallback)(int64_t handle,
                                            int errCode,
                                            im_conversation* conversation);

typedef struct im_get_conversation_callback_proxy {
  int64_t handle;
  OnImGetConversationCallback callback;
} im_get_conversation_callback_proxy;

typedef void (*OnImGetConversationListCallback)(int64_t handle,
                                                int errCode,
                                                im_conversation conversationList[],
                                                int length);

typedef struct im_get_conversation_list_callback_proxy {
  int64_t handle;
  OnImGetConversationListCallback callback;
} im_get_conversation_list_callback_proxy;

typedef struct im_get_text_message_draft_callback_proxy {
  int64_t handle;
  OnImGeneralCallbackWithString callback;
} im_get_text_message_draft_callback_proxy;

typedef struct im_get_unread_count_callback_proxy {
  int64_t handle;
  OnImGeneralCallbackWithInt callback;
} im_get_unread_count_callback_proxy;

typedef struct im_search_conversation_result {
  im_conversation* conversation;
  int matchCount;
} im_search_conversation_result;

typedef void (*OnImSearchConversationsCallback)(int64_t handle,
                                                int errCode,
                                                im_search_conversation_result searchResultList[],
                                                int length);

typedef struct im_search_conversations_callback_proxy {
  int64_t handle;
  OnImSearchConversationsCallback callback;
} im_search_conversations_callback_proxy;

#pragma mark - 聊天室
/*!
 聊天室
 */

typedef void (*OnImChatRoomKvDidSyncedListener)(const char* roomId);

typedef void (*OnImChatRoomKvAddRemovedListener)(const char* roomid,
                                                 const char** keys,
                                                 const char** values,
                                                 int kvCount);

typedef void (*OnImChatRoomRemoveEntryCallback)(int errCode);

typedef enum im_enum_chat_room_member_action_type {
  IM_CHATROOM_MEMBER_QUIT = 0,  // 聊天室成员退出
  IM_CHATROOM_MEMBER_JOIN = 1,  // 聊天室成员加入
} im_enum_chat_room_member_action_type;

typedef struct im_chat_room_member_action {
  const char* memberId;                              // 成员 ID
  enum im_enum_chat_room_member_action_type action;  // 成员加入或者退出
} im_chat_room_member_action;

typedef struct im_chat_room_member_action_message {
  im_message_content* msgContent;
  im_chat_room_member_action** userList;
  int userListLen;
} im_chat_room_member_action_message;

typedef void (*OnImChatRoomMemberActionChangedListener)(const char* roomId,
                                                        im_chat_room_member_action* memberActions,
                                                        int count);
typedef void (*OnImChatRoomStatusChangedListener_Joining)(const char* roomId);
typedef void (*OnImChatRoomStatusChangedListener_Joined)(const char* roomId);
typedef void (*OnImChatRoomStatusChangedListener_Reset)(const char* roomId);
typedef void (*OnImChatRoomStatusChangedListener_Quited)(const char* roomId);
typedef void (*OnImChatRoomStatusChangedListener_Destoryed)(const char* roomId, int destroyType);
typedef void (*OnImChatRoomStatusChangedListener_Error)(const char* roomId, int errorCode);

typedef struct im_chat_room_kv_notification_message {
  im_message_content* msgContent;
  int type;           // 聊天室操作的类型 RCChatroomKVNotificationType
  const char* key;    // 聊天室属性名称
  const char* value;  // 聊天室属性对应的值
  const char* extra;  // 通知消息的自定义字段，最大长度 2 kb
} im_chat_room_kv_notification_message;

typedef struct im_chat_room_member_info {
  const char* userId;
  int64_t joinTime;
} im_chat_room_member_info;

typedef struct im_chat_room_info {
  const char* chatRoomId;
  int order;
  im_chat_room_member_info* members;
  int membersLen;
  int totalMemeberCount;
} im_chat_room_info;

typedef void (*OnImGetChatRoomInfoCallback)(int64_t handle,
                                            int errCode,
                                            im_chat_room_info* imChatRoomInfo);

typedef struct im_get_chat_room_info_callback_proxy {
  int64_t handle;
  OnImGetChatRoomInfoCallback callback;
} im_get_chat_room_info_callback_proxy;

typedef void (*OnImGetChatRoomEntryCallback)(
    int64_t handle, int errCode, const char* roomId, const char* key, const char* value);

typedef struct im_get_chat_room_entry_callback_proxy {
  int64_t handle;
  OnImGetChatRoomEntryCallback callback;
} im_get_chat_room_entry_callback_proxy;

typedef void (*OnImGetChatRoomAllEntriesCallback)(int64_t handle,
                                                  int errCode,
                                                  const char* roomId,
                                                  const char** keys,
                                                  const char** values,
                                                  int kvCount);

typedef struct im_get_chat_room_all_entries_callback_proxy {
  int64_t handle;
  OnImGetChatRoomAllEntriesCallback callback;
} im_get_chat_room_all_entries_callback_proxy;

#pragma mark - 标签

typedef struct im_tag_info {
  const char* tagId;    // 标签唯一标识，字符型，长度不超过 10 个字
  const char* tagName;  // 长度不超过 15 个字，标签名称可以重复
  int count;            // 匹配的会话个数
  int64_t timeStamp;    // 时间戳由协议栈提供
} im_tag_info;

typedef void (*OnImGetTagsCallback)(int64_t handle, int errCode, im_tag_info* imTags, int count);

typedef struct im_get_tags_callback_proxy {
  int64_t handle;
  OnImGetTagsCallback callback;
} im_get_tags_callback_proxy;

struct im_tag_info;

typedef struct im_conversation_tag_info {
  im_tag_info* tagInfo;  // 标签 ID
  bool isTop;            // 会话是否置顶
} im_conversation_tag_info;

typedef void (*OnImGetConversationTagInfoListCallback)(
    int64_t handle, int errCode, im_conversation_tag_info conversationTagInfoList[], int length);

typedef struct im_get_conversation_tag_info_list_callback_proxy {
  int64_t handle;
  OnImGetConversationTagInfoListCallback callback;
} im_get_conversation_tag_info_list_callback_proxy;

typedef void (*OnImTagChangedListener)();

typedef void (*OnImConversationTagChangedListener)();

}  // extern "C"

@interface RongUnityIM : NSObject

@end

#endif
