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
}

