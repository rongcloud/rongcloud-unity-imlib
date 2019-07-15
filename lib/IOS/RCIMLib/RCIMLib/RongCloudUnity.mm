#import "RongCloudUnity.h"
static RongCloudUnity *defalutInstance=nil;
@implementation RongCloudUnity

- (id)init {
 
    self = [super init];
    if (nil == self) {
        return nil;
    }
    
    return self;
    
    
}

+(void)Instance
{
    defalutInstance=[[RongCloudUnity alloc] init];
    
    [RCIMClient.sharedRCIMClient setReceiveMessageDelegate:defalutInstance object:nil];
    [RCIMClient.sharedRCIMClient setRCConnectionStatusChangeDelegate:defalutInstance];
    [RCIMClient.sharedRCIMClient setRCTypingStatusDelegate:defalutInstance];
    [RCIMClient.sharedRCIMClient setChatRoomStatusDelegate:defalutInstance];
    [RCIMClient.sharedRCIMClient setRCLogInfoDelegate:defalutInstance];
}

- (void)onReceived:(RCMessage *)message
              left:(int)nLeft
            object:(id)object {
    NSDictionary *callbackmsg=[RongCloudUnityUtils createDict:@"message" forValue:[RongCloudUnityUtils getDictionaryObjectData:message]];
    [callbackmsg setValue:@(nLeft) forKey:@"left"];
    [RongCloudUnityUtils SendMessage:@"OnReceiveMessageListerner" sendMessage:callbackmsg];
}

 - (void)onConnectionStatusChanged:(RCConnectionStatus)status{
     NSNumber* mstatus=@(status);
     [RongCloudUnityUtils SendMessage:@"ConnectionStatus" sendMessage:[RongCloudUnityUtils createDict:@"status" forValue:mstatus]];
 }

- (void)onTypingStatusChanged:(RCConversationType)conversationType targetId:(NSString *)targetId status:(NSArray *)userTypingStatusList {
    NSDictionary* retMsg=[RongCloudUnityUtils createDict:@"conversationType" forValue:@(conversationType)];
    [retMsg setValue:targetId forKey:@"targetId"];
    [retMsg setValue:[RongCloudUnityUtils getDictionaryByArray:userTypingStatusList] forKey:@"statusList"];
 
    [RongCloudUnityUtils SendMessage:@"onTypingStatusChanged" sendMessage:retMsg];
}

- (void)onChatRoomJoinFailed:(NSString *)chatroomId errorCode:(RCErrorCode)errorCode {
    NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"type" forValue:@"onChatRoomJoinFailed"];
    [retMsg setValue:@(errorCode) forKey:@"errorCode"];
    [retMsg setValue:chatroomId forKey:@"chatroomId"];
    [RongCloudUnityUtils SendMessage:@"ChatRoomStatusListener" sendMessage:retMsg];
}

- (void)onChatRoomJoined:(NSString *)chatroomId {
    NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"type" forValue:@"onChatRoomJoined"];
    [retMsg setValue:chatroomId forKey:@"chatroomId"];
    [RongCloudUnityUtils SendMessage:@"ChatRoomStatusListener" sendMessage:retMsg];
}

- (void)onChatRoomJoining:(NSString *)chatroomId {
    NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"type" forValue:@"onChatRoomJoining"];
    [retMsg setValue:chatroomId forKey:@"chatroomId"];
    [RongCloudUnityUtils SendMessage:@"ChatRoomStatusListener" sendMessage:retMsg];
}

- (void)onChatRoomQuited:(NSString *)chatroomId {
    NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"type" forValue:@"onChatRoomQuited"];
    [retMsg setValue:chatroomId forKey:@"chatroomId"];
    [RongCloudUnityUtils SendMessage:@"ChatRoomStatusListener" sendMessage:retMsg];
}
-(void)onMessageReceiptRequest:(RCConversationType)conversationType targetId:(NSString *)targetId messageUId:(NSString *)messageUId{
    NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"type" forValue:@"onMessageReceiptRequest"];
    [retMsg setValue:@(conversationType) forKey:@"conversationType"];
    [retMsg setValue:targetId forKey:@"targetId"];
    [retMsg setValue:messageUId forKey:@"messageUId"];
    [RongCloudUnityUtils SendMessage:@"onMessageReceiptRequest" sendMessage:retMsg];
}
-(void)onMessageRecalled:(long)messageId{
    NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"messageId" forValue:@(messageId)];
    [RongCloudUnityUtils SendMessage:@"setOnRecallMessageListener" sendMessage:retMsg];
}
-(void)onMessageReceiptResponse:(RCConversationType)conversationType targetId:(NSString *)targetId messageUId:(NSString *)messageUId readerList:(NSMutableDictionary *)userIdList{
    NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"type" forValue:@"onMessageReceiptResponse"];
    [retMsg setValue:@(conversationType) forKey:@"conversationType"];
    [retMsg setValue:targetId forKey:@"targetId"];
    [retMsg setValue:messageUId forKey:@"messageUId"];
    [retMsg setValue:userIdList forKey:@"userIdList"];
    [RongCloudUnityUtils SendMessage:@"onMessageReceiptResponse" sendMessage:retMsg];
}
- (void)didOccurLog:(NSString *)logInfo {
    NSDictionary* retMsg=[[NSMutableDictionary alloc] init];
    [retMsg setValue:logInfo forKey:@"log"];
    [RongCloudUnityUtils SendMessage:@"setRCLogInfoListener" sendMessage:retMsg];
}
- (void)onRealTimeLocationStatusChange:(RCRealTimeLocationStatus)status{
    NSDictionary* retMsg=[[NSMutableDictionary alloc] init];
    [retMsg setValue:@"onStatusChange" forKey:@"event"];
    [retMsg setValue:@(status) forKey:@"status"];
    [RongCloudUnityUtils SendMessage:@"addRealTimeLocationListener" sendMessage:retMsg];
}
- (void)onReceiveLocation:(CLLocation *)location
                     type:(RCRealTimeLocationType)type
               fromUserId:(NSString *)userId{
    NSDictionary* retMsg=[[NSMutableDictionary alloc] init];
    [retMsg setValue:@"onReceiveLocation" forKey:@"event"];
    [retMsg setValue:@(location.coordinate.latitude) forKey:@"latitude"];
    [retMsg setValue:@(location.coordinate.longitude) forKey:@"longitude"];
    [retMsg setValue:userId forKey:@"userId"];
    [retMsg setValue:@(type) forKey:@"type"];
    [RongCloudUnityUtils SendMessage:@"addRealTimeLocationListener" sendMessage:retMsg];
}
- (void)onParticipantsJoin:(NSString *)userId{
    NSDictionary* retMsg=[[NSMutableDictionary alloc] init];
    [retMsg setValue:@"onParticipantsJoin" forKey:@"event"];
    [retMsg setValue:userId forKey:@"userId"];
    [RongCloudUnityUtils SendMessage:@"addRealTimeLocationListener" sendMessage:retMsg];
}
- (void)onParticipantsQuit:(NSString *)userId{
    NSDictionary* retMsg=[[NSMutableDictionary alloc] init];
    [retMsg setValue:@"onParticipantsQuit" forKey:@"event"];
    [retMsg setValue:userId forKey:@"userId"];
    [RongCloudUnityUtils SendMessage:@"addRealTimeLocationListener" sendMessage:retMsg];
}
@end
extern "C"
{

    void _init(const char* key){
        NSString *appkey=[NSString stringWithUTF8String:key];
        [[RCIMClient sharedRCIMClient]initWithAppKey:appkey];

        [RongCloudUnity Instance];
        
     
        
       

    }
    void _connect(const char* token){
        NSString *apptoken=[NSString stringWithUTF8String:token];
        [[RCIMClient sharedRCIMClient]connectWithToken:apptoken
                                               success:^(NSString *userId) {
                                                   [RongCloudUnityUtils sendCommSuccessCallBack:@"connect"];
                                               } error:^(RCConnectErrorCode status) {
                                                   [RongCloudUnityUtils sendCommErrorCallBack:@"connect" errCode:@(status)];
                                               } tokenIncorrect:^{
                                                   
                                               }];
    }
    char* _getCurrentUserID(){
        return [RongCloudUnityUtils MakeStringCopy:[RCIMClient sharedRCIMClient].currentUserInfo.userId];
    }
    int _getCurrentConnectionStatus(){
        return [RCIMClient sharedRCIMClient].getCurrentNetworkStatus;
    }
    void _logout(){
        [[RCIMClient sharedRCIMClient]logout];
    }

    void _joinChatRoom(const char* targetId,int messageCount){
        [[RCIMClient sharedRCIMClient]joinChatRoom:[NSString stringWithUTF8String:targetId] messageCount:messageCount success:^{
            [RongCloudUnityUtils sendCommSuccessCallBack:@"joinChatRoom"];
        } error:^(RCErrorCode status) {
            [RongCloudUnityUtils sendCommErrorCallBack:@"joinChatRoom" errCode:@(status)];
        }];
    }
    void _getChatRoomInfo(const char * targetId,int memberCount,int order){
        [[RCIMClient sharedRCIMClient]getChatRoomInfo:[NSString stringWithUTF8String:targetId] count:memberCount order:(RCChatRoomMemberOrder)order success:^(RCChatRoomInfo *chatRoomInfo) {
            NSDictionary *msg=[RongCloudUnityUtils createDict:@"targetId" forValue:chatRoomInfo.targetId];
            [msg setValue:@(chatRoomInfo.totalMemberCount) forKey:@"totalMemberCount"];
            [msg setValue:@(chatRoomInfo.memberOrder) forKey:@"memberOrder"];
            [msg setValue:[RongCloudUnityUtils arrayDictWithArrayObject:chatRoomInfo.memberInfoArray] forKey:@"members"];
            [RongCloudUnityUtils SendMessage:@"getChatRoomInfo" sendMessage:msg];
            
        } error:^(RCErrorCode status) {
            [RongCloudUnityUtils sendCommErrorCallBack:@"getChatRoomInfo" errCode:@(status)];
        }];
    }
    void _sendMessage(const char * message,const char * eventId){
        NSDictionary *dict=[RongCloudUnityUtils dictionaryWithJsonString:[NSString stringWithUTF8String:message]];
        
        
        [[RCIMClient sharedRCIMClient]sendMessage:(RCConversationType)[dict[@"conversationType"] intValue]
                                         targetId:dict[@"targetId"]
                                          content:[RongCloudUnityUtils toMessageContent:[RongCloudUnityUtils dictionaryWithJsonString:dict[@"content"]]]
                                      pushContent:dict[@"pushContent"]
                                         pushData:dict[@"pushData"]
                                          success:^(long messageId) {
                                              NSDictionary *msg=[RongCloudUnityUtils createDict:@"state" forValue:@"success"];
                                              [msg setValue:@(messageId) forKey:@"messageId"];
                                              [msg setValue:[NSString stringWithUTF8String:eventId] forKey:@"eventId"];
                                              [RongCloudUnityUtils SendMessage:@"SendMessageCallback" sendMessage:msg];
                                          } error:^(RCErrorCode nErrorCode, long messageId) {
                                              [RongCloudUnityUtils onSendMessageError:[NSString stringWithUTF8String:eventId]
                                                                            messageId:messageId
                                                                              errcode:nErrorCode];
                                          }];
    }
    void _sendMessageVoip(const char * message,bool isVoip,const char * eventId){
        NSDictionary *dict=[RongCloudUnityUtils dictionaryWithJsonString:[NSString stringWithUTF8String:message]];
        RCSendMessageOption *option=[RCSendMessageOption new];
        option.isVoIPPush=isVoip;
        [[RCIMClient sharedRCIMClient]sendMessage:(RCConversationType)[dict[@"conversationType"] intValue]
                                         targetId:dict[@"targetId"]
                                          content:[RongCloudUnityUtils toMessageContent:[RongCloudUnityUtils dictionaryWithJsonString:dict[@"content"]]]
                                      pushContent:dict[@"pushContent"]
                                         pushData:dict[@"pushData"]
                                           option:option
                                          success:^(long messageId) {
                                              NSDictionary *msg=[RongCloudUnityUtils createDict:@"state" forValue:@"success"];
                                              [msg setValue:@(messageId) forKey:@"messageId"];
                                              [msg setValue:[NSString stringWithUTF8String:eventId] forKey:@"eventId"];
                                              [RongCloudUnityUtils SendMessage:@"SendMessageCallback" sendMessage:msg];
                                          } error:^(RCErrorCode nErrorCode, long messageId) {
                                              [RongCloudUnityUtils onSendMessageError:[NSString stringWithUTF8String:eventId]
                                                                            messageId:messageId
                                                                              errcode:nErrorCode];
                                          }];
    }
    void _disconnect(){
        [[RCIMClient sharedRCIMClient]disconnect];
    }
    void _getUnreadCount(int conversationType, char* targetId){
        int count=[[RCIMClient sharedRCIMClient]getUnreadCount:(RCConversationType)conversationType
                                                      targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]];
        NSDictionary *msg=[RongCloudUnityUtils createDict:@"state" forValue:@"success"];
        [msg setValue:@(count) forKey:@"count"];
        [RongCloudUnityUtils SendMessage:@"getUnreadCount" sendMessage:msg];
    }
    void _getUnreadCountbyTypes(char* conversationTypes){
        NSArray* array=[RongCloudUnityUtils getArraryFromString:[NSString stringWithUTF8String:conversationTypes]];
        
        int count= [[RCIMClient sharedRCIMClient]getUnreadCount:array];
        NSDictionary *msg=[RongCloudUnityUtils createDict:@"state" forValue:@"success"];
        [msg setValue:@(count) forKey:@"count"];
        [RongCloudUnityUtils SendMessage:@"getUnreadCount" sendMessage:msg];
    }
    void _getUnreadCountbyTypesContain(char* conversationTypes,bool isContain){
        NSArray* array=[RongCloudUnityUtils getArraryFromString:[NSString stringWithUTF8String:conversationTypes]];
        
        int count= [[RCIMClient sharedRCIMClient]getUnreadCount:array containBlocked:isContain];
        NSDictionary *msg=[RongCloudUnityUtils createDict:@"state" forValue:@"success"];
        [msg setValue:@(count) forKey:@"count"];
        [RongCloudUnityUtils SendMessage:@"getUnreadCount" sendMessage:msg];
    }
    void _clearMessagesUnreadStatus(int type,char* targetId,long time){
        bool ret;
        if(time==0)
            ret= [[RCIMClient sharedRCIMClient]clearMessagesUnreadStatus:(RCConversationType)type
                                                              targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]];
        else{
            ret=[[RCIMClient sharedRCIMClient]clearMessagesUnreadStatus:(RCConversationType)type
                                                               targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                                                   time:time];
        }
        [RongCloudUnityUtils SendBoolMessage:@"clearMessagesUnreadStatus" result:ret];
    }
    
    void _sendMediaMessage(char *data,char * eventId){
        NSDictionary *dict=[RongCloudUnityUtils dictionaryWithJsonString:[NSString stringWithUTF8String:data]];
        
        [[RCIMClient sharedRCIMClient]sendMediaMessage:(RCConversationType)[dict[@"conversationType"] intValue]
                                              targetId:dict[@"targetId"]
                                               content:[RongCloudUnityUtils toMessageContent:[RongCloudUnityUtils dictionaryWithJsonString:dict[@"content"]]]
                                           pushContent:dict[@"pushContent"]
                                              pushData:dict[@"pushData"]
                                              progress:^(int progress, long messageId) {
                                                  NSDictionary *msg=[RongCloudUnityUtils createDict:@"state" forValue:@"progress"];
                                                  [msg setValue:@(messageId) forKey:@"messageId"];
                                                  [msg setValue:@(progress) forKey:@"progress"];
                                                  [msg setValue:[NSString stringWithUTF8String:eventId] forKey:@"eventId"];
                                                  [RongCloudUnityUtils SendMessage:@"SendMessageCallback" sendMessage:msg];
                                              }
                                               success:^(long messageId) {
                                                   NSDictionary *msg=[RongCloudUnityUtils createDict:@"state" forValue:@"success"];
                                                   [msg setValue:@(messageId) forKey:@"messageId"];
                                                   [msg setValue:[NSString stringWithUTF8String:eventId] forKey:@"eventId"];
                                                   [RongCloudUnityUtils SendMessage:@"SendMessageCallback" sendMessage:msg];
                                               }
                                                 error:^(RCErrorCode errorCode, long messageId) {
                                                     [RongCloudUnityUtils onSendMessageError:[NSString stringWithUTF8String:eventId]
                                                                                   messageId:messageId
                                                                                     errcode:errorCode];
                                                 }
                                                cancel:^(long messageId) {
                                                    NSDictionary *msg=[RongCloudUnityUtils createDict:@"state" forValue:@"cancel"];
                                                    [msg setValue:@(messageId) forKey:@"messageId"];
                                                    [msg setValue:[NSString stringWithUTF8String:eventId] forKey:@"eventId"];
                                                    [RongCloudUnityUtils SendMessage:@"SendMessageCallback" sendMessage:msg];
                                                }];
       
    
    }
    bool _cancelSendMediaMessage(long messageId){
        return [[RCIMClient sharedRCIMClient]cancelSendMediaMessage:messageId];
    }
    void _downloadMediaFile(int type,char* targetId,int mediaType,char* mediaUrl,char* eventId){
        [[RCIMClient sharedRCIMClient]downloadMediaFile:(RCConversationType)type
                                               targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                              mediaType:(RCMediaType)mediaType
                                               mediaUrl:[RongCloudUnityUtils MakeStringFromChar:mediaUrl]
                                               progress:^(int progress) {
                                                   NSDictionary *msg=[RongCloudUnityUtils createDict:@"state" forValue:@"progress"];
                                                   [msg setValue:@(progress) forKey:@"progress"];
                                                   [msg setValue:[NSString stringWithUTF8String:eventId] forKey:@"eventId"];
                                                   [RongCloudUnityUtils SendMessage:@"downloadMediaFile" sendMessage:msg];
                                               } success:^(NSString *mediaPath) {
                                                   NSDictionary *msg=[RongCloudUnityUtils createDict:@"state" forValue:@"success"];
                                                   [msg setValue:mediaPath forKey:@"mediaPath"];
                                                   [msg setValue:[NSString stringWithUTF8String:eventId] forKey:@"eventId"];
                                                   [RongCloudUnityUtils SendMessage:@"downloadMediaFile" sendMessage:msg];
                                               } error:^(RCErrorCode errorCode) {
                                                   [RongCloudUnityUtils sendCommErrorCallBack:@"downloadMediaFile"
                                                                                      errCode:@(errorCode)
                                                                                      eventId:eventId];
                                               }];
    }
    void _downloadMediaMessage(long messageId){
        [[RCIMClient sharedRCIMClient]downloadMediaMessage:messageId
                                                  progress:^(int progress) {
                                                      NSDictionary *msg=[RongCloudUnityUtils createDict:@"state" forValue:@"progress"];
                                                      [msg setValue:@(progress) forKey:@"progress"];
                                                      [msg setValue:@(messageId) forKey:@"messageId"];
                                                      [RongCloudUnityUtils SendMessage:@"downloadMediaMessage" sendMessage:msg];
                                                    
                                                  } success:^(NSString *mediaPath) {
                                                      NSDictionary *msg=[RongCloudUnityUtils createDict:@"state" forValue:@"success"];
                                                      [msg setValue:mediaPath forKey:@"mediaPath"];
                                                      [msg setValue:@(messageId) forKey:@"messageId"];
                                                      [RongCloudUnityUtils SendMessage:@"downloadMediaMessage" sendMessage:msg];
                                                  } error:^(RCErrorCode errorCode) {
                                                      NSDictionary *msg=[RongCloudUnityUtils createDict:@"state" forValue:@"error"];
                                                      [msg setValue:@(errorCode) forKey:@"errorCode"];
                                                      [msg setValue:@(messageId) forKey:@"messageId"];
                                                      [RongCloudUnityUtils SendMessage:@"downloadMediaMessage" sendMessage:msg];
                                                  } cancel:^{
                                                      NSDictionary *msg=[RongCloudUnityUtils createDict:@"state" forValue:@"cancel"];
                                                      [msg setValue:@(messageId) forKey:@"messageId"];
                                                      [RongCloudUnityUtils SendMessage:@"SendMessageCallback" sendMessage:msg];
                                                  }];
    }
    bool _cancelDownloadMediaMessage(long messageId){
        return [[RCIMClient sharedRCIMClient]cancelDownloadMediaMessage:messageId];
    }
    void _sendDirectionalMessage(char* data,char* eventId){
        NSDictionary *dict=[RongCloudUnityUtils dictionaryWithJsonString:[NSString stringWithUTF8String:data]];
        
        
        [[RCIMClient sharedRCIMClient]sendDirectionalMessage:(RCConversationType)[dict[@"conversationType"] intValue]
                                                    targetId:dict[@"targetId"]
                                                toUserIdList:[RongCloudUnityUtils getArraryFromString:dict[@"userIds"]]
                                                     content:[RongCloudUnityUtils toMessageContent:[RongCloudUnityUtils dictionaryWithJsonString:dict[@"content"]]]
                                                 pushContent:dict[@"pushContent"]
                                                    pushData:dict[@"pushData"]
                                                     success:^(long messageId) {
                                                         
                                                         NSDictionary *msg=[RongCloudUnityUtils createDict:@"state" forValue:@"success"];
                                                         [msg setValue:@(messageId) forKey:@"messageId"];
                                                         [msg setValue:[NSString stringWithUTF8String:eventId] forKey:@"eventId"];
                                                         [RongCloudUnityUtils SendMessage:@"SendMessageCallback" sendMessage:msg];
                                                     }
                                                       error:^(RCErrorCode errorCode, long messageId) {
                                                           [RongCloudUnityUtils onSendMessageError:[NSString stringWithUTF8String:eventId]
                                                                                         messageId:messageId
                                                                                           errcode:errorCode];
                                                       }];
    }
    void  _insertOutgoingMessage(char * data,long sentTime){
        NSDictionary *dict=[RongCloudUnityUtils dictionaryWithJsonString:[NSString stringWithUTF8String:data]];
        RCMessage * message;
        if(sentTime){
            message=[[RCIMClient sharedRCIMClient]insertOutgoingMessage:(RCConversationType)[dict[@"conversationType"] intValue]
                                                               targetId:dict[@"targetId"]
                                                             sentStatus:(RCSentStatus)[dict[@"sentStatus"] intValue]
                                                                content:[RongCloudUnityUtils toMessageContent:[RongCloudUnityUtils dictionaryWithJsonString:dict[@"content"]]]];
        }else{
            message=[[RCIMClient sharedRCIMClient]insertOutgoingMessage:(RCConversationType)[dict[@"conversationType"] intValue]
                                                               targetId:dict[@"targetId"]
                                                             sentStatus:(RCSentStatus)[dict[@"sentStatus"] intValue]
                                                                content:[RongCloudUnityUtils toMessageContent:[RongCloudUnityUtils dictionaryWithJsonString:dict[@"content"]]]
                                                                sentTime:sentTime];
        }
        NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"success"];
        [retMsg setValue:[RongCloudUnityUtils getDictionaryObjectData:message] forKey:@"messages"];
        [RongCloudUnityUtils SendMessage:@"insertOutgoingMessage"
                             sendMessage:retMsg];
        
    }
    void _insertIncomingMessage(char * data,long sentTime){
        
        NSDictionary *dict=[RongCloudUnityUtils dictionaryWithJsonString:[NSString stringWithUTF8String:data]];
        RCMessage * message;
        if(sentTime){
            message=[[RCIMClient sharedRCIMClient]insertIncomingMessage:(RCConversationType)[dict[@"conversationType"] intValue]
                                                               targetId:dict[@"targetId"]
                                                           senderUserId:dict[@"senderUserId"]
                                                         receivedStatus:(RCReceivedStatus)[dict[@"receivedStatus"] intValue]
                                                                content:[RongCloudUnityUtils toMessageContent:[RongCloudUnityUtils dictionaryWithJsonString:dict[@"content"]]]];
        }else{
            message=[[RCIMClient sharedRCIMClient]insertIncomingMessage:(RCConversationType)[dict[@"conversationType"] intValue]
                                                               targetId:dict[@"targetId"]
                                                           senderUserId:dict[@"senderUserId"]
                                                         receivedStatus:(RCReceivedStatus)[dict[@"receivedStatus"] intValue]
                                                                content:[RongCloudUnityUtils toMessageContent:[RongCloudUnityUtils dictionaryWithJsonString:dict[@"content"]]]
                                                               sentTime:sentTime];
        }
        NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"success"];
        [retMsg setValue:[RongCloudUnityUtils getDictionaryObjectData:message] forKey:@"messages"];
        [RongCloudUnityUtils SendMessage:@"insertIncomingMessage"
                             sendMessage:retMsg];
        
    }
    void _deleteMessages(char *messageIds){
        NSArray* array=[RongCloudUnityUtils getArraryFromString:[NSString stringWithUTF8String:messageIds]];
        
        [RongCloudUnityUtils SendBoolMessage:@"deleteMessagesByIds" result:[[RCIMClient sharedRCIMClient]deleteMessages:array]];
    }
    void _deleteMessagesByType(int type,char* targetId){
        [[RCIMClient sharedRCIMClient]deleteMessages:(RCConversationType)type
                                            targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                             success:^{
                                                 [RongCloudUnityUtils sendCommSuccessCallBack:@"deleteMessages"];
                                            
                                             } error:^(RCErrorCode status) {
                                                 [RongCloudUnityUtils sendCommErrorCallBack:@"deleteMessages" errCode:@(status)];
                                             }];
    }
    void _clearMessages(int type,char* targetId){
        bool ret= [[RCIMClient sharedRCIMClient]clearMessages:(RCConversationType)type
                                                  targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]];
        [RongCloudUnityUtils SendBoolMessage:@"clearMessages" result:ret];
    }
    void _clearHistoryMessages(int type,char* targetId,long recordTime,bool clearRemote){
        [[RCIMClient sharedRCIMClient]clearHistoryMessages:(RCConversationType)(type)
                                                  targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                                recordTime:recordTime
                                               clearRemote:clearRemote
                                                   success:^{
                                                       [RongCloudUnityUtils sendCommSuccessCallBack:@"clearHistoryMessages"];
                                                   } error:^(RCErrorCode status) {
                                                       [RongCloudUnityUtils sendCommErrorCallBack:@"clearHistoryMessages" errCode:@(status)];
                                                   }];
    }
    void _getLatestMessages(int type,char* targetId,int count){
        NSArray * array=[[RCIMClient sharedRCIMClient]getLatestMessages:(RCConversationType)type
                                                               targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                                                  count:count];
        NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"success"];
        [retMsg setValue:[RongCloudUnityUtils getDictionaryByArray:array] forKey:@"messages"];
        [RongCloudUnityUtils SendMessage:@"getLatestMessages" sendMessage:retMsg];
    }
    void _getHistoryMessages(int type,char* targetId,char* objectName,long baseMessageId,int count,bool isForward){
        NSArray * messages;
        NSString* objName=[RongCloudUnityUtils MakeStringFromChar:objectName];
        if(objName&&objName.length>0){
            messages=[[RCIMClient sharedRCIMClient]getHistoryMessages:(RCConversationType)type
                                                             targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                                           objectName:objName
                                                      baseMessageId:baseMessageId
                                                            isForward:isForward
                                                                count:count];
        }else{
            messages=[[RCIMClient sharedRCIMClient]getHistoryMessages:(RCConversationType)type
                                                             targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                                      oldestMessageId:baseMessageId
                                                                count:count];
        }
        NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"success"];
        [retMsg setValue:[RongCloudUnityUtils getDictionaryByArray:messages] forKey:@"messages"];
        [RongCloudUnityUtils SendMessage:@"getHistoryMessages" sendMessage:retMsg];
    }
    void _getHistoryMessagesByTimestamp(int type,char* targetId,char* objectNames,long timestamp,int count,bool isForward,int afterCount){
        NSArray * messages;
        NSArray* objNames=[RongCloudUnityUtils getArraryFromString:[NSString stringWithUTF8String:objectNames]];
        if(objNames&&objNames.count>0){
            messages=[[RCIMClient sharedRCIMClient]getHistoryMessages:(RCConversationType)type
                                                             targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                                          objectNames:objNames
                                                             sentTime:timestamp
                                                            isForward:isForward
                                                                count:count];
        }else{
            messages=[[RCIMClient sharedRCIMClient]getHistoryMessages:(RCConversationType)type
                                                             targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                                             sentTime:timestamp
                                                          beforeCount:count
                                                           afterCount:afterCount];
        }
        NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"success"];
        [retMsg setValue:[RongCloudUnityUtils getDictionaryByArray:messages]  forKey:@"messages"];
        [RongCloudUnityUtils SendMessage:@"getHistoryMessages" sendMessage:retMsg];
    }
    void _getHistoryMessagesByBeforAfter(int type,char* targetId,long sentTime,int before,int after){
        NSArray* messages=[[RCIMClient sharedRCIMClient]getHistoryMessages:(RCConversationType)type
                                                                  targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                                                  sentTime:sentTime
                                                               beforeCount:before
                                                                afterCount:after];
        NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"success"];
        [retMsg setValue:[RongCloudUnityUtils getDictionaryByArray:messages]  forKey:@"messages"];
        [RongCloudUnityUtils SendMessage:@"getHistoryMessages" sendMessage:retMsg];
    }
    void _getRemoteHistoryMessages(int type,char* targetId,long recordTime,int count){
        [[RCIMClient sharedRCIMClient]getRemoteHistoryMessages:(RCConversationType)type
                                                      targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                                    recordTime:recordTime
                                                         count:count
                                                       success:^(NSArray *messages, BOOL isRemaining) {
                                                           NSDictionary* msg=[RongCloudUnityUtils createDict:@"status" forValue:@"success"];
                                                           [msg setValue:[RongCloudUnityUtils getDictionaryByArray:messages]  forKey:@"messages"];
                                                           [msg setValue:@(isRemaining) forKey:@"isRemaining"];
                                                           [RongCloudUnityUtils SendMessage:@"getRemoteHistoryMessages" sendMessage:msg];
                                                       } error:^(RCErrorCode status) {
                                                           [RongCloudUnityUtils sendCommErrorCallBack:@"getRemoteHistoryMessages" errCode:@(status)];
                                                       }];
    }
    void _clearRemoteHistoryMessages(int type,char* targetId,long recordTime){
        [[RCIMClient sharedRCIMClient]clearRemoteHistoryMessages:(RCConversationType)type
                                                        targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                                      recordTime:recordTime
                                                         success:^{
                                                             
                                                             [RongCloudUnityUtils sendCommSuccessCallBack:@"clearRemoteHistoryMessages"];
                                                         } error:^(RCErrorCode status) {
                                                             [RongCloudUnityUtils sendCommErrorCallBack:@"clearRemoteHistoryMessages" errCode:@(status)];
                                                         }];
    }
    void _deleteRemoteMessage(int type,char* targetId,char* messages){
        NSArray *msgsstr=[RongCloudUnityUtils getArraryFromString:[NSString stringWithUTF8String:messages]];
        
        [[RCIMClient sharedRCIMClient]deleteRemoteMessage:(RCConversationType)type
                                                 targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                                 messages:msgsstr
                                                  success:^{
                                                      [RongCloudUnityUtils sendCommSuccessCallBack:@"deleteRemoteMessage"];
                                                 } error:^(RCErrorCode status) {
                                                     [RongCloudUnityUtils sendCommErrorCallBack:@"deleteRemoteMessage" errCode:@(status)];
                                                 }];
    }
    
    bool _setMessageReceivedStatus(long messageId,int receivedStatus){
        return [[RCIMClient sharedRCIMClient]setMessageReceivedStatus:messageId
                                                       receivedStatus:(RCReceivedStatus)receivedStatus];
    }
    bool _setMessageSentStatus(long messageId,int sentStatus){
        return  [[RCIMClient sharedRCIMClient]setMessageSentStatus:messageId
                                                        sentStatus:(RCSentStatus)sentStatus];
    }
    void _searchMessages(int type,char* targetId,char* keyword,int count,long startTime){
        NSArray<RCMessage*>* messages= [[RCIMClient sharedRCIMClient]searchMessages:(RCConversationType)type
                                            targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                             keyword:[RongCloudUnityUtils MakeStringFromChar:keyword]
                                               count:count
                                           startTime:startTime];
        NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"success"];
        [retMsg setValue:[RongCloudUnityUtils getDictionaryByArray:messages] forKey:@"messages"];
        [RongCloudUnityUtils SendMessage:@"searchMessages" sendMessage:retMsg];
    }
    void _searchMessagesByUser(int type,char* targetId,char* userId,int count,long startTime){
        NSArray<RCMessage*>* messages= [[RCIMClient sharedRCIMClient]searchMessages:(RCConversationType)type
                                                                           targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                                                            userId:[RongCloudUnityUtils MakeStringFromChar:userId]
                                                                              count:count
                                                                          startTime:startTime];
        NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"success"];
        [retMsg setValue:[RongCloudUnityUtils getDictionaryByArray:messages] forKey:@"messages"];
        [RongCloudUnityUtils SendMessage:@"searchMessages" sendMessage:retMsg];
    }
    void _searchConversations(char* data,char* keyword){
        NSDictionary *msg=[RongCloudUnityUtils getDictionaryObjectData:[RongCloudUnityUtils MakeStringFromChar:data]];
        NSArray<NSNumber *> * types=@[@(3)];
        NSArray<NSString *> * names=@[@"RC:TxtMsg"];
        NSArray *array=[[RCIMClient sharedRCIMClient]searchConversations:types
                                              messageType:names
                                                  keyword:[RongCloudUnityUtils MakeStringFromChar:keyword]];
        NSLog(@"conversations:%zd",array.count);
        NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"success"];
        [retMsg setValue:[RongCloudUnityUtils getDictionaryByArray:array] forKey:@"conversations"];
        [RongCloudUnityUtils SendMessage:@"searchConversations" sendMessage:retMsg];
    }
    void _setOfflineMessageDuration(int duration){
        [[RCIMClient sharedRCIMClient]setOfflineMessageDuration:duration
                                                        success:^{
                                                            
                                                            [RongCloudUnityUtils sendCommSuccessCallBack:@"setOfflineMessageDuration"];
                                                        } failure:^(RCErrorCode nErrorCode) {
                                                            [RongCloudUnityUtils sendCommErrorCallBack:@"setOfflineMessageDuration"
                                                                                               errCode:@(nErrorCode)];
                                                        }];
    }
    void  _getConversation(int type,char* targetId){
        RCConversation *conversation= [[RCIMClient sharedRCIMClient]getConversation:(RCConversationType)type targetId:[NSString stringWithUTF8String:targetId]];
        NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"success"];
        [retMsg setValue:[RongCloudUnityUtils getDictionaryObjectData:conversation] forKey:@"conversation"];
        [RongCloudUnityUtils SendMessage:@"getConversation" sendMessage:retMsg];
    }
    void  _getConversationList(char* conversationTypes,int count,long timestamp){
        NSArray* array=[RongCloudUnityUtils getArraryFromString:[NSString stringWithUTF8String:conversationTypes]];
        NSArray * list;
        if(count==0)
            list=[[RCIMClient sharedRCIMClient]getConversationList:array];
        else{
            list=[[RCIMClient sharedRCIMClient]getConversationList:array
                                                             count:count
                                                         startTime:timestamp];
        }
        NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"success"];
        [retMsg setValue:[RongCloudUnityUtils getDictionaryByArray:list] forKey:@"conversations"];
        [RongCloudUnityUtils SendMessage:@"getConversationList" sendMessage:retMsg];
    }
    void _clearConversations(char * types){
        NSArray *ctypes=[RongCloudUnityUtils getArraryFromString:[RongCloudUnityUtils MakeStringFromChar:types]];
        bool ret=[[RCIMClient sharedRCIMClient]clearConversations:ctypes];
        [RongCloudUnityUtils SendBoolMessage:@"clearConversations" result:ret];
    }
    void _removeConversation(int type,char* targetId){
        bool ret=[[RCIMClient sharedRCIMClient]removeConversation:(RCConversationType)type
                                                         targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]];
        [RongCloudUnityUtils SendBoolMessage:@"clearConversations" result:ret];
    }
    void _setConversationToTop(int type,char* targetId,bool isTop){
        bool ret=[[RCIMClient sharedRCIMClient]setConversationToTop:(RCConversationType)type
                                                           targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                                              isTop:isTop];
        [RongCloudUnityUtils SendBoolMessage:@"setConversationToTop" result:ret];
    }
    void _getTotalUnreadCount(){
        int retcount=[[RCIMClient sharedRCIMClient]getTotalUnreadCount];
        NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"success"];
        [retMsg setValue:@(retcount) forKey:@"count"];
        [RongCloudUnityUtils SendMessage:@"getTotalUnreadCount" sendMessage:retMsg];
    }
    void _getTotalUnreadCountByType(char* types){
        NSArray * ctypes=[RongCloudUnityUtils getArraryFromString:[RongCloudUnityUtils MakeStringFromChar:types]];
        NSMutableArray<RCConversation *> *array=[NSMutableArray array];
        for(int i=0;i<ctypes.count;i++){
            NSDictionary* type=[RongCloudUnityUtils dictionaryWithJsonString:ctypes[i]];
            RCConversation *conversation=[RCConversation new];
            conversation.conversationTitle=type[@"conversationTitle"];
            conversation.conversationType=(RCConversationType)[type[@"conversationType"] intValue];
            conversation.targetId=type[@"targetId"];
            [array addObject:conversation];
        }
        
        int retcount=[[RCIMClient sharedRCIMClient]getTotalUnreadCount:array];
        NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"success"];
        [retMsg setValue:@(retcount) forKey:@"count"];
        [RongCloudUnityUtils SendMessage:@"getTotalUnreadCount" sendMessage:retMsg];
    }
    void _getTopConversationList(char * conversationTypes){
        NSArray * types=[RongCloudUnityUtils getArraryFromString:[RongCloudUnityUtils MakeStringFromChar:conversationTypes]];
        NSArray * list=[[RCIMClient sharedRCIMClient]getTopConversationList:types];
        NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"success"];
        [retMsg setValue:[RongCloudUnityUtils getDictionaryByArray:list] forKey:@"conversations"];
        [RongCloudUnityUtils SendMessage:@"getTopConversationList" sendMessage:retMsg];
    }
    
    void _getTextMessageDraft(int type,char* targetId){
        NSString* ret=[[RCIMClient sharedRCIMClient]getTextMessageDraft:(RCConversationType)type
                                                               targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]];
        NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"success"];
        [retMsg setValue:ret forKey:@"content"];
        [RongCloudUnityUtils SendMessage:@"getTextMessageDraft" sendMessage:retMsg];
    }
    void _saveTextMessageDraft(int type,char* targetId,char* content){
        BOOL ret=[[RCIMClient sharedRCIMClient]saveTextMessageDraft:(RCConversationType)type
                                                           targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                                            content:[RongCloudUnityUtils MakeStringFromChar:content]];
        [RongCloudUnityUtils SendBoolMessage:@"saveTextMessageDraft" result:ret];
    }
    void _setConversationNotificationStatus(int type,char* targetId,bool isBlocked){
        [[RCIMClient sharedRCIMClient]setConversationNotificationStatus:(RCConversationType)type
                                                               targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                                              isBlocked:isBlocked
                                                                success:^(RCConversationNotificationStatus nStatus) {
                                                                    NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"success"];
                                                                    [retMsg setValue:@(nStatus) forKey:@"NotificationStatus"];
                                                                    [RongCloudUnityUtils SendMessage:@"setConversationNotificationStatus" sendMessage:retMsg];
                                                                } error:^(RCErrorCode status) {
                                                                    [RongCloudUnityUtils sendCommErrorCallBack:@"setConversationNotificationStatus" errCode:@(status)];
                                                                }];
    }
    void _getConversationNotificationStatus(int type,char* targetId){
        [[RCIMClient sharedRCIMClient]getConversationNotificationStatus:(RCConversationType)type
                                                               targetId:[RongCloudUnityUtils MakeStringFromChar:targetId] success:^(RCConversationNotificationStatus nStatus) {
                                                                   NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"success"];
                                                                   [retMsg setValue:@(nStatus) forKey:@"NotificationStatus"];
                                                                   [RongCloudUnityUtils SendMessage:@"getConversationNotificationStatus" sendMessage:retMsg];
                                                               } error:^(RCErrorCode status) {
                                                                   [RongCloudUnityUtils sendCommErrorCallBack:@"getConversationNotificationStatus" errCode:@(status)];
                                                               }];
    }
    
    void _getBlockedConversationList(char * types){
        NSArray* typeList=[RongCloudUnityUtils getArraryFromString:[RongCloudUnityUtils MakeStringFromChar:types]];
        NSArray* array=[[RCIMClient sharedRCIMClient]getBlockedConversationList:typeList];
        NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"success"];
        [retMsg setValue:[RongCloudUnityUtils getDictionaryByArray:array] forKey:@"conversations"];
        [RongCloudUnityUtils SendMessage:@"getConversationNotificationStatus" sendMessage:retMsg];
    }
    void _setNotificationQuietHours(char* startTime,int spanMins){
        [[RCIMClient sharedRCIMClient]setNotificationQuietHours:[RongCloudUnityUtils MakeStringFromChar:startTime]
                                                       spanMins:spanMins
                                                        success:^{
                                                            [RongCloudUnityUtils sendCommSuccessCallBack:@"setNotificationQuietHours"];
                                                        } error:^(RCErrorCode status) {
                                                            [RongCloudUnityUtils sendCommErrorCallBack:@"setNotificationQuietHours"
                                                                                               errCode:@(status)];
                                                        }];
    }
    void _removeNotificationQuietHours(){
        [[RCIMClient sharedRCIMClient]removeNotificationQuietHours:^{
            [RongCloudUnityUtils sendCommSuccessCallBack:@"removeNotificationQuietHours"];
        } error:^(RCErrorCode status) {
            [RongCloudUnityUtils sendCommErrorCallBack:@"removeNotificationQuietHours"
                                               errCode:@(status)];
        }];
    }
    void _sendTypingStatus(int type,char* targetId,char* objectName){
        [[RCIMClient sharedRCIMClient]sendTypingStatus:(RCConversationType)type
                                              targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                           contentType:[RongCloudUnityUtils MakeStringFromChar:objectName]];
    }
    void _addToBlacklist(char* userId){
        [[RCIMClient sharedRCIMClient]addToBlacklist:[RongCloudUnityUtils MakeStringFromChar:userId]
                                             success:^{
                                                 [RongCloudUnityUtils sendCommSuccessCallBack:@"addToBlacklist"];
                                             } error:^(RCErrorCode status) {
                                                 [RongCloudUnityUtils sendCommErrorCallBack:@"addToBlacklist" errCode:@(status)];
                                             }];
    }
    void _removeFromBlacklist(char* userId){
        [[RCIMClient sharedRCIMClient]removeFromBlacklist:[RongCloudUnityUtils MakeStringFromChar:userId]
                                                  success:^{
                                                      [RongCloudUnityUtils sendCommSuccessCallBack:@"removeFromBlacklist"];
                                                  } error:^(RCErrorCode status) {
                                                      [RongCloudUnityUtils sendCommErrorCallBack:@"removeFromBlacklist" errCode:@(status)];
                                                  }];
    }
    void _getBlacklistStatus(char* userId){
        [[RCIMClient sharedRCIMClient]getBlacklistStatus:[RongCloudUnityUtils MakeStringFromChar:userId]
                                                 success:^(int bizStatus) {
                                                     NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"success"];
                                                     [retMsg setValue:@(bizStatus) forKey:@"BlacklistStatus"];
                                                     [RongCloudUnityUtils SendMessage:@"getBlacklistStatus" sendMessage:retMsg];
                                                 } error:^(RCErrorCode status) {
                                                     [RongCloudUnityUtils sendCommErrorCallBack:@"getBlacklistStatus" errCode:@(status)];
                                                 }];
    }
    void _getBlacklist(){
        [[RCIMClient sharedRCIMClient]getBlacklist:^(NSArray *blockUserIds) {
            NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"success"];
            [retMsg setValue:blockUserIds forKey:@"ids"];
            [RongCloudUnityUtils SendMessage:@"getBlacklist" sendMessage:retMsg];
        } error:^(RCErrorCode status) {
            [RongCloudUnityUtils sendCommErrorCallBack:@"getBlacklist" errCode:@(status)];
        }];
    }
    void _joinExistChatRoom(char* targetId,int messageCount){
        [[RCIMClient sharedRCIMClient]joinExistChatRoom:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                           messageCount:messageCount
                                                success:^{
                                                    [RongCloudUnityUtils sendCommSuccessCallBack:@"joinExistChatRoom"];
                                                } error:^(RCErrorCode status) {
                                                    [RongCloudUnityUtils sendCommErrorCallBack:@"joinExistChatRoom" errCode:@(status)];
                                                }];
    }
    
    void _quitChatRoom(char * targetId){
        [[RCIMClient sharedRCIMClient]quitChatRoom:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                           success:^{
                                               [RongCloudUnityUtils sendCommSuccessCallBack:@"quitChatRoom"];
                                           } error:^(RCErrorCode status) {
                                               [RongCloudUnityUtils sendCommErrorCallBack:@"quitChatRoom" errCode:@(status)];
                                           }];
    }
    void _getRemoteChatroomHistoryMessages(char* targetId,long recordTime,int count,int order){
        [[RCIMClient sharedRCIMClient]getRemoteChatroomHistoryMessages:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                                            recordTime:recordTime
                                                                 count:count
                                                                 order:(RCTimestampOrder)order
                                                               success:^(NSArray *messages, long long syncTime) {
                                                                   NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"success"];
                                                                   [retMsg setValue:[RongCloudUnityUtils getDictionaryByArray:messages] forKey:@"messages"];
                                                                   [retMsg setValue:@(syncTime) forKey:@"syncTime"];
                                                                   [RongCloudUnityUtils SendMessage:@"getChatroomHistoryMessages" sendMessage:retMsg];
                                                               } error:^(RCErrorCode status) {
                                                                   [RongCloudUnityUtils sendCommErrorCallBack:@"getChatroomHistoryMessages" errCode:@(status)];
                                                               }];
    }
    void _startCustomerService(char * kefuId,char *kefuInfo){
        RCCustomerServiceInfo *csInfo=[RCCustomerServiceInfo new];
        NSDictionary *info=[RongCloudUnityUtils dictionaryWithJsonString:[RongCloudUnityUtils MakeStringFromChar:kefuInfo]];
        csInfo.name=info[@"name"];
        csInfo.nickName=info[@"nickName"];
        csInfo.loginName=info[@"loginName"];
        [[RCIMClient sharedRCIMClient]startCustomerService:[RongCloudUnityUtils MakeStringFromChar:kefuId]
                                                      info:csInfo
                                                 onSuccess:^(RCCustomerServiceConfig *config) {
                                                     NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"success"];
                                                     [retMsg setValue:[RongCloudUnityUtils getDictionaryObjectData:config] forKey:@"config"];
                                                     [RongCloudUnityUtils SendMessage:@"startCustomerService" sendMessage:retMsg];
                                                 } onError:^(int errorCode, NSString *errMsg) {
                                                     NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"error"];
                                                     [retMsg setValue:errMsg forKey:@"errorMessage"];
                                                     [retMsg setValue:@(errorCode) forKey:@"errorCode"];
                                                     [RongCloudUnityUtils SendMessage:@"startCustomerService" sendMessage:retMsg];
                                                 } onModeType:^(RCCSModeType mode) {
                                                     NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"onModeChanged"];
                                                     [retMsg setValue:@(mode) forKey:@"mode"];
                                                     [RongCloudUnityUtils SendMessage:@"startCustomerService" sendMessage:retMsg];
                                                 } onPullEvaluation:^(NSString *dialogId) {
                                                     NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"onPullEvaluation"];
                                                     [retMsg setValue:dialogId forKey:@"dialogId"];
                                                     [RongCloudUnityUtils SendMessage:@"startCustomerService" sendMessage:retMsg];
                                                 } onSelectGroup:^(NSArray<RCCustomerServiceGroupItem *> *groupList) {
                                                     NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"onSelectGroup"];
                                                     [retMsg setValue:groupList forKey:@"groups"];
                                                     [RongCloudUnityUtils SendMessage:@"startCustomerService" sendMessage:retMsg];
                                                 } onQuit:^(NSString *quitMsg) {
                                                     NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"onQuit"];
                                                     [retMsg setValue:quitMsg forKey:@"message"];
                                                     [RongCloudUnityUtils SendMessage:@"startCustomerService" sendMessage:retMsg];
                                                 }];
    }
    
    void _stopCustomerService(char* kefuId){
        [[RCIMClient sharedRCIMClient]stopCustomerService:[RongCloudUnityUtils MakeStringFromChar:kefuId]];
    }
    void _switchToHumanMode(char *kefuId){
        [[RCIMClient sharedRCIMClient]switchToHumanMode:[RongCloudUnityUtils MakeStringFromChar:kefuId]];
    }
    void _selectCustomerServiceGroup(char* kefuId,char* groupId){
        [[RCIMClient sharedRCIMClient]selectCustomerServiceGroup:[RongCloudUnityUtils MakeStringFromChar:kefuId]
                                                     withGroupId:[RongCloudUnityUtils MakeStringFromChar:groupId]];
    }
    void _evaluateCustomerService(char* kefuId,char* knownlidgeId,BOOL isRobot,char* suggest){
        [[RCIMClient sharedRCIMClient]evaluateCustomerService:[RongCloudUnityUtils MakeStringFromChar:kefuId]
                                                 knownledgeId:[RongCloudUnityUtils MakeStringFromChar:knownlidgeId]
                                                   robotValue:isRobot
                                                      suggest:[RongCloudUnityUtils MakeStringFromChar:suggest]];
    }
   
    void _evaluateCustomerServiceForHuman(char* kefuId,char* dialogId,int value,char* suggest,int resolveStatus,char* tagText,char* extra){
        [[RCIMClient sharedRCIMClient]evaluateCustomerService:[RongCloudUnityUtils MakeStringFromChar:kefuId]
                                                     dialogId:[RongCloudUnityUtils MakeStringFromChar:dialogId]
                                                    starValue:value
                                                      suggest:[RongCloudUnityUtils MakeStringFromChar:suggest]
                                                resolveStatus:(RCCSResolveStatus)resolveStatus
                                                      tagText:[RongCloudUnityUtils MakeStringFromChar:tagText]
                                                        extra:[RongCloudUnityUtils dictionaryWithJsonString:[RongCloudUnityUtils MakeStringFromChar:extra]]];
    }
    void _evaluateCustomerServiceComm(char* kefuId,char* dialogId,int value,char* suggest,int resolveStatus){
        [[RCIMClient sharedRCIMClient]evaluateCustomerService:[RongCloudUnityUtils MakeStringFromChar:kefuId]
                                                     dialogId:[RongCloudUnityUtils MakeStringFromChar:dialogId]
                                                    starValue:value
                                                      suggest:[RongCloudUnityUtils MakeStringFromChar:suggest]
                                                resolveStatus:(RCCSResolveStatus)resolveStatus];
    }
    void _getUnreadMentionedMessages(int type ,char * targetId){
        NSArray* array=[[RCIMClient sharedRCIMClient]getUnreadMentionedMessages:(RCConversationType)type
                                                                       targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]];
        NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"success"];
        [retMsg setValue:[RongCloudUnityUtils getDictionaryByArray:array] forKey:@"messages"];
        [RongCloudUnityUtils SendMessage:@"getUnreadMentionedMessages" sendMessage:retMsg];
    }
    void _recallMessage(int id,char* pushContent){
        RCMessage* message=[[RCIMClient sharedRCIMClient]getMessage:id];
        [[RCIMClient sharedRCIMClient]recallMessage:message
                                        pushContent:[RongCloudUnityUtils MakeStringFromChar:pushContent]
                                            success:^(long messageId) {
                                                RCMessage* message=[[RCIMClient sharedRCIMClient]getMessage:messageId];
                                                NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"success"];
                                                [retMsg setValue:message.content forKey:@"content"];
                                                [RongCloudUnityUtils SendMessage:@"recallMessage" sendMessage:retMsg];
                                            } error:^(RCErrorCode errorcode) {
                                                [RongCloudUnityUtils sendCommErrorCallBack:@"recallMessage" errCode:@(errorcode)];
                                            }];
    }
    void _sendReadReceiptMessage(int type,char* targetId,long timestamp){
        [[RCIMClient sharedRCIMClient]sendReadReceiptMessage:(RCConversationType)type
                                                    targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                                        time:timestamp
                                                     success:^{
                                                         [RongCloudUnityUtils sendCommSuccessCallBack:@"sendReadReceiptMessage"];
                                                        } error:^(RCErrorCode nErrorCode) {
                                                            [RongCloudUnityUtils sendCommErrorCallBack:@"sendReadReceiptMessage" errCode:@(nErrorCode)];
                                                        }];
    }
    void _sendReadReceiptRequest(int messageId){
        RCMessage* message=[[RCIMClient sharedRCIMClient]getMessage:messageId];
        [[RCIMClient sharedRCIMClient]sendReadReceiptRequest:message
                                                     success:^{
                                                         [RongCloudUnityUtils sendCommSuccessCallBack:@"sendReadReceiptRequest"];
                                                     } error:^(RCErrorCode nErrorCode) {
                                                         [RongCloudUnityUtils sendCommErrorCallBack:@"sendReadReceiptRequest" errCode:@(nErrorCode)];
                                                     }];
    }
    void _sendReadReceiptResponse(int type,char* targetId,char* messageJson){
        NSArray * messages=[RongCloudUnityUtils getArraryFromString:[RongCloudUnityUtils MakeStringFromChar:messageJson]];
        [[RCIMClient sharedRCIMClient]sendReadReceiptResponse:(RCConversationType)type
                                                     targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                                  messageList:messages
                                                      success:^{
                                                          [RongCloudUnityUtils sendCommSuccessCallBack:@"sendReadReceiptResponse"];
                                                      } error:^(RCErrorCode nErrorCode) {
                                                          [RongCloudUnityUtils sendCommErrorCallBack:@"sendReadReceiptResponse" errCode:@(nErrorCode)];
                                                      }];
    }
    void _syncConversationReadStatus(int type,char* targetId,double timestamp){
        [[RCIMClient sharedRCIMClient]syncConversationReadStatus:(RCConversationType)type
                                                        targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                                            time:timestamp
                                                         success:^{
                                                             [RongCloudUnityUtils sendCommSuccessCallBack:@"syncConversationReadStatus"];
                                                         } error:^(RCErrorCode nErrorCode) {
                                                             [RongCloudUnityUtils sendCommErrorCallBack:@"syncConversationReadStatus" errCode:@(nErrorCode)];
                                                         }];
    }
    
    void _startRealTimeLocation(int type,char* targetId){
        CLLocationManager *manager;
        [manager requestWhenInUseAuthorization];
        [[RCRealTimeLocationManager sharedManager]getRealTimeLocationProxy:(RCConversationType)type
                                                                  targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                                                   success:^(id<RCRealTimeLocationProxy> locationShare) {
                                                                      [locationShare startRealTimeLocation];
                                                                      [RongCloudUnityUtils sendCommSuccessCallBack:@"startRealTimeLocation"];
                                                                  } error:^(RCRealTimeLocationErrorCode status) {
                                                                      [RongCloudUnityUtils sendCommErrorCallBack:@"startRealTimeLocation" errCode:@(status)];
                                                                  }];
    }
    void _joinRealTimeLocation(int type,char* targetId){
        [[RCRealTimeLocationManager sharedManager]getRealTimeLocationProxy:(RCConversationType)type
                                                                  targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                                                   success:^(id<RCRealTimeLocationProxy> locationShare) {
                                                                      [locationShare joinRealTimeLocation];
                                                                      [RongCloudUnityUtils sendCommSuccessCallBack:@"joinRealTimeLocation"];
                                                                  } error:^(RCRealTimeLocationErrorCode status) {
                                                                      [RongCloudUnityUtils sendCommErrorCallBack:@"joinRealTimeLocation" errCode:@(status)];
                                                                  }];
    }
    void _quitRealTimeLocation(int type,char* targetId){
        [[RCRealTimeLocationManager sharedManager]getRealTimeLocationProxy:(RCConversationType)type
                                                                  targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                                                   success:^(id<RCRealTimeLocationProxy> locationShare) {
                                                                       [locationShare quitRealTimeLocation];
                                                                       [RongCloudUnityUtils sendCommSuccessCallBack:@"quitRealTimeLocation"];
                                                                   } error:^(RCRealTimeLocationErrorCode status) {
                                                                       [RongCloudUnityUtils sendCommErrorCallBack:@"quitRealTimeLocation" errCode:@(status)];
                                                                   }];
    }
    void _getRealTimeLocationParticipants(int type,char* targetId){
        [[RCRealTimeLocationManager sharedManager]getRealTimeLocationProxy:(RCConversationType)type
                                                                  targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                                                   success:^(id<RCRealTimeLocationProxy> locationShare) {
                                                                       NSArray* array=[locationShare getParticipants];
                                                                       NSDictionary* retmsg =[RongCloudUnityUtils createDict:@"status" forValue:@"success"];
                                                                       [retmsg setValue:array forKey:@"ids"];
                                                                       [RongCloudUnityUtils SendMessage:@"getRealTimeLocationParticipants" sendMessage:retmsg];
                                                                   } error:^(RCRealTimeLocationErrorCode status) {
                                                                       [RongCloudUnityUtils sendCommErrorCallBack:@"getRealTimeLocationParticipants" errCode:@(status)];
                                                                   }];
    }
    void _getRealTimeLocationCurrentStatus(int type,char* targetId){
        [[RCRealTimeLocationManager sharedManager]getRealTimeLocationProxy:(RCConversationType)type
                                                                  targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                                                   success:^(id<RCRealTimeLocationProxy> locationShare) {
                                                                       RCRealTimeLocationStatus status=[locationShare getStatus];
                                                                       NSDictionary* retmsg =[[NSMutableDictionary alloc] init];
                                                                       [retmsg setValue:@(status) forKey:@"state"];
                                                                       [RongCloudUnityUtils SendMessage:@"getRealTimeLocationCurrentStatus" sendMessage:retmsg];
                                                                   } error:^(RCRealTimeLocationErrorCode status) {
                                                                   [RongCloudUnityUtils sendCommErrorCallBack:@"getRealTimeLocationCurrentStatus" errCode:@(status)];
                                                                   }];
    }
    
    void _addRealTimeLocationObserver(int type,char* targetId){
        [[RCRealTimeLocationManager sharedManager]getRealTimeLocationProxy:(RCConversationType)type
                                                                  targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                                                   success:^(id<RCRealTimeLocationProxy> locationShare) {
                                                                       [locationShare addRealTimeLocationObserver:defalutInstance];
                                                                   } error:^(RCRealTimeLocationErrorCode status) {
                                                                       NSDictionary* retmsg =[[NSMutableDictionary alloc] init];
                                                                       [retmsg setValue:@(status) forKey:@"errorCode"];
                                                                       [retmsg setValue:@"onError" forKey:@"event"];
                                                                       [RongCloudUnityUtils SendMessage:@"addRealTimeLocationListener" sendMessage:retmsg];
                                                                   }];
    }
    void _removeRealTimeLocationObserver(int type,char *targetId){
        [[RCRealTimeLocationManager sharedManager]getRealTimeLocationProxy:(RCConversationType)type
                                                                  targetId:[RongCloudUnityUtils MakeStringFromChar:targetId]
                                                                   success:^(id<RCRealTimeLocationProxy> locationShare) {
                                                                       [locationShare removeRealTimeLocationObserver:defalutInstance];
                                                                   } error:^(RCRealTimeLocationErrorCode status) {
                                                                      
                                                                   }];
    }
    void _getOfflineMessageDuration(){
        int time=[[RCIMClient sharedRCIMClient]getOfflineMessageDuration];
        NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"success"];
        [retMsg setValue:@(time) forKey:@"duration"];
        [RongCloudUnityUtils SendMessage:@"getOfflineMessageDuration" sendMessage:retMsg];
    }
    void _setDeviceToken(char* deviceToken){
        [[RCIMClient sharedRCIMClient]setDeviceToken:[RongCloudUnityUtils MakeStringFromChar:deviceToken]];
    }
}

