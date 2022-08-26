//
//  Copyright © 2021 RongCloud. All rights reserved.
//

#ifndef RONG_UNITY_UTILS_H_
#define RONG_UNITY_UTILS_H_

#import <Foundation/Foundation.h>
#import <RongChatRoom/RongChatRoom.h>

#include "RongUnityIM.h"

@interface RongUnityUtils : NSObject

/*!
 工具
 */
+ (NSUInteger)writeDictionary:(NSDictionary *)Dic
                       toKeys:(const char **&)Keys
                    andValues:(const char **&)Values;

+ (NSUInteger)writeArray:(NSArray *)array toItems:(const char **&)items;

+ (NSUInteger)writeArray:(NSArray *)array
                 toItems:(void *&)items
                itemSize:(int)itemSize
              itemEncode:(void (^)(void *imObject, id rcObject))encodeBlock;

/*
 消息
 */
+ (struct im_user_info *)makeImUserInfoFrom:(RCUserInfo *)rcUserInfo;

+ (void)freeImUserInfo:(struct im_user_info *)imUserInfo;

+ (struct im_mentioned_info *)makeImMentionedInfoFrom:(RCMentionedInfo *)rcMentionedInfo;

+ (void)freeImMentionedInfo:(struct im_mentioned_info *)imMentionedInfo;

// 从 SDK ObjC 类消息类型生成对应的 im_*_message C 结构对象
+ (void *)makeImContentFrom:(RCMessageContent *)rcMessageContent;

+ (void)freeImMessageContent:(void *)imMessageContent withObjectName:(NSString *)objectName;

+ (struct im_read_receipt_info *)makeImReadReceiptInfoFrom:(RCReadReceiptInfo *)rcReadReceiptInfo;

+ (void)freeImReadReceiptInfo:(struct im_read_receipt_info *)imReadReceiptInfo;

+ (struct im_ios_config *)makeImIOSConfigFrom:(RCiOSConfig *)rcIOSConfig;

+ (void)freeImIOSConfig:(struct im_ios_config *)imIOSConfig;

+ (struct im_push_config *)makeImPushConfigFrom:(RCMessagePushConfig *)rcPushConfig;

+ (void)freeImPushConfig:(struct im_push_config *)imPushConfig;

+ (struct im_message *)makeImMessageFrom:(RCMessage *)rcMessage;

+ (void)freeImMessageInternal:(struct im_message *)imMessage;

+ (void)freeImMessage:(struct im_message *)imMessage;

+ (struct im_message *)makeArrayOfImMessageFrom:(NSArray<RCMessage *> *)rcMsgArray;

+ (void)freeArrayOfImMessage:(struct im_message *)imMsgArray length:(NSInteger)length;

+ (RCMessage *)fromIMessage:(struct im_message *)imMessage isSend:(BOOL)isSend;

+ (RCMessageContent *)fromImMessageContent:(void *)imContent objectName:(NSString *)objectName;

+ (RCUserInfo *)fromImUserInfo:(struct im_user_info *)imUserInfo;

+ (RCMentionedInfo *)fromImMentionedInfo:(struct im_mentioned_info *)mentionedInfo;

+ (BOOL)convertRcMessage:(RCMessage *)message toImMessage:(struct im_message *)imMessage;

/*!
 typing status
 */
+ (struct im_typing_status *)makeImTypingStatusFrom:(RCUserTypingStatus *)rcTypingStatus;

+ (void)freeImTypingStatus:(struct im_typing_status *)imTypingStatus;

+ (BOOL)convertRCUserTypingStatus:(RCUserTypingStatus *)rcTyping
                 toImTypingStatus:(struct im_typing_status *)imTyping;

+ (struct im_typing_status *)makeArrayOfImTypingStatusFrom:
    (NSArray<RCUserTypingStatus *> *)rcTypingArray;

+ (void)freeArrayOfImTypingStatus:(struct im_typing_status *)imTypingArray length:(NSInteger)length;

#if IOS_SDK_VER_UP_5133
/*!
 敏感词
 */
+ (struct im_blocked_message_info *)makeImBlockedMessageInfoFrom:
    (RCBlockedMessageInfo *)rcBlockedMessageInfo;

+ (void)freeImBlockedMessageInfo:(struct im_blocked_message_info *)imBlockedMessageInfo;
#endif

/*
 会话
 */
+ (struct im_conversation *)makeImConversationFrom:(RCConversation *)rcConversation;

+ (struct im_conversation *)makeArrayOfImConversationFrom:
    (NSArray<RCConversation *> *)rcArrayConversation;

+ (void)freeImConversation:(struct im_conversation *)imConversation;

+ (void)freeArrayOfImConversation:(struct im_conversation *)imArrayConversation
                           length:(NSInteger)length;

+ (struct im_search_conversation_result *)makeArrayOfImSearchConversationResultFrom:
    (NSArray<RCSearchConversationResult *> *)rcArrayResult;

+ (void)freeArrayOfImSearchConversationResult:(struct im_search_conversation_result *)imArrayResult
                                       length:(NSInteger)length;

/*
 聊天室
 */
+ (struct im_chat_room_info *)makeImChatRoomInfoFrom:(RCChatRoomInfo *)rcChatRoomInfo;

+ (void)freeImChatRoomInfo:(struct im_chat_room_info *)imChatRoomInfo;

#if IOS_SDK_VER_UP_5133
+ (struct im_chat_room_member_action *)makeImChatRoomMemberActionFrom:
    (RCChatRoomMemberAction *)rcChatRoomMemberAction;

+ (void)freeImChatRoomMemberAction:(struct im_chat_room_member_action *)imChatRoomMemberAction;

+ (struct im_chat_room_member_action *)makeArrayOfImChatRoomMemberActionFrom:
    (NSArray<RCChatRoomMemberAction *> *)rcArrayMemberAction;
#endif

+ (void)freeArrayOfImChatRoomMemberAction:
            (struct im_chat_room_member_action *)imChatRoomMemberActionArray
                                   length:(NSInteger)length;

/*
 标签
 */

+ (struct im_tag_info *)makeImTagInfoFrom:(RCTagInfo *)rcTagInfo;

+ (void)freeImTagInfo:(struct im_tag_info *)imTagInfo;

+ (BOOL)convertRcTagInfo:(RCTagInfo *)rcTagInfo toImTagInfo:(im_tag_info *)imTagInfo;

+ (struct im_tag_info *)makeArrayOfImTagInfoFrom:(NSArray<RCTagInfo *> *)rcTagInfoArray;

+ (void)freeArrayOfImTagInfo:(struct im_tag_info *)imTagInfoArray length:(NSInteger)length;

+ (struct im_conversation_tag_info *)makeArrayOfImConversationTagInfoFrom:
    (NSArray<RCConversationTagInfo *> *)rcArrayConvTagInfo;

+ (void)freeArrayOfImConversationTagInfo:(struct im_conversation_tag_info *)imConvTagInfoArray
                                  length:(NSInteger)length;
@end

#endif /* RONG_UNITY_UTILS_H_ */
