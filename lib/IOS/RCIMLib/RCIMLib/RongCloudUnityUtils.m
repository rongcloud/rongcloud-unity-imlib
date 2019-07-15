#import <Foundation/Foundation.h>
#import "RongCloudUnityUtils.h"
#import <objc/runtime.h>
@implementation RongCloudUnityUtils
- (id)init {
    self = [super init];
    if (nil == self) {
        return nil;
    }
    return self;
}

+(void)SendMessage:(NSString *) callback
       sendMessage:(id) msg{
    NSDictionary *dict=[[NSMutableDictionary alloc] init];
    [dict setValue:callback forKey:@"callback"];
    [dict setValue:msg forKey:@"data"];
    const char* message=[self MakeStringCopy:[self jsonStringWithDictionary:dict]];
    UnitySendMessage("RongCloud","CommCallback",message);
}
+(void)SendBoolMessage:(NSString *)callback
                result:(BOOL)result
{
    NSDictionary *retMsg=[RongCloudUnityUtils createDict:@"status" forValue:@"success"];
    [retMsg setValue:@(result) forKey:@"result"];
    [RongCloudUnityUtils SendMessage:callback sendMessage:retMsg];
}

+(char*) MakeStringCopy:(NSString*) nstring{
    if((!nstring)||(nil==nstring)||(nstring==(id)[NSNull null])||(0==nstring.length)){
        return NULL;
    }
    const char* string=[nstring UTF8String];
    if(string==NULL){
        return  NULL;
    }
    char* res=(char*)malloc(strlen(string)+1);
    strcpy(res,string);
    return res;
}
+(NSString *) MakeStringFromChar:(char *)charStr{
    return [NSString stringWithUTF8String:charStr];
}
+(NSDictionary *)createDict:(NSString *)key
                   forValue:(NSObject *)value{
    NSDictionary *dict=[[NSMutableDictionary alloc] init];
    [dict setValue:value forKey:key];
    return dict;
}
/**
 未知类型（仅限字典/数组/字符串）
 
 @param object 字典/数组/字符串
 @return 字符串
 */
+(NSString *) jsonStringWithObject:(id) object{
    NSString *value = nil;
    if (!object) {
        return value;
    }
    if ([object isKindOfClass:[NSString class]]) {
        value = [self jsonStringWithString:object];
    }else if([object isKindOfClass:[NSDictionary class]]){
        value = [self jsonStringWithDictionary:object];
    }else if([object isKindOfClass:[NSArray class]]){
        value = [self jsonStringWithArray:object];
    }else if([object isKindOfClass:[NSObject class]]){
        value=[self jsonStringWithDictionary:[self getDictionaryObjectData:object]];
    }
    return value;
}
+(NSArray *) arrayDictWithArrayObject:(NSArray *) obj{
    NSMutableArray *arrays=[NSMutableArray array];
    for(id valueObj in obj){
        [arrays addObject:[self getDictionaryObjectData:valueObj]];
    }
    return arrays;
}
/**
 字符串类型转JSON
 
 @param string 字符串类型
 @return 返回字符串
 */
+(NSString *) jsonStringWithString:(NSString *) string{
    return [NSString stringWithFormat:@"%@",
            [[string stringByReplacingOccurrencesOfString:@"\n" withString:@"\\n"] stringByReplacingOccurrencesOfString:@"\"" withString:@"\\\""]
            ];
}
/**
 数组类型转JSON
 
 @param array 数组类型
 @return 返回字符串
 */
+(NSString *) jsonStringWithArray:(NSArray *)array{
    NSMutableString *reString = [NSMutableString string];
    [reString appendString:@"["];
    NSMutableArray *values = [NSMutableArray array];
    for (id valueObj in array) {
        NSString *value = [self jsonStringWithObject:valueObj];
        if (value) {
            [values addObject:[NSString stringWithFormat:@"%@",value]];
        }
    }
    [reString appendFormat:@"%@",[values componentsJoinedByString:@","]];
    [reString appendString:@"]"];
    return reString;
}

/**
 字典类型转JSON
 
 @param dictionary 字典数据
 @return 返回字符串
 */
+(NSString *) jsonStringWithDictionary:(NSDictionary *)dictionary{
    NSError *parseError = nil;
    
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dictionary options:NSJSONWritingPrettyPrinted error:&parseError];
    
    return [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
}
+ (NSDictionary *)dictionaryWithJsonString:(NSString *)jsonString
{
    if (jsonString == nil) {
        return nil;
    }
    
    NSData *jsonData = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
    NSError *err;
    NSDictionary *dic = [NSJSONSerialization JSONObjectWithData:jsonData
                                                        options:NSJSONReadingMutableContainers
                                                          error:&err];
    if(err)
    {
        NSLog(@"json解析失败：%@",err);
        return nil;
    }
    return dic;
}
+ (NSDictionary*)getDictionaryObjectData:(id)obj
{
    NSMutableDictionary *dic = [NSMutableDictionary dictionary];
    unsigned int propsCount;
    objc_property_t *props = class_copyPropertyList([obj class], &propsCount);//获得属性列表
    for(int i = 0;i < propsCount; i++)
    {
        objc_property_t prop = props[i];
        
        NSString *propName = [NSString stringWithUTF8String:property_getName(prop)];//获得属性的名称
        id value = [obj valueForKey:propName];//kvc读值
        if(value == nil)
        {
            value = [NSNull null];
        }
        else
        {
            value = [self getObjectInternal:value];//自定义处理数组，字典，其他类
        }
        [dic setObject:value forKey:propName];
    }
    return dic;
}
+ (id)getObjectInternal:(id)obj
{
    if([obj isKindOfClass:[NSString class]]
       || [obj isKindOfClass:[NSNumber class]]
       || [obj isKindOfClass:[NSNull class]])
    {
        return obj;
    }
    
    if([obj isKindOfClass:[NSArray class]])
    {
        NSArray *objarr = obj;
        NSMutableArray *arr = [NSMutableArray arrayWithCapacity:objarr.count];
        for(int i = 0;i < objarr.count; i++)
        {
            [arr setObject:[self getObjectInternal:[objarr objectAtIndex:i]] atIndexedSubscript:i];
        }
        return arr;
    }
    
    if([obj isKindOfClass:[NSDictionary class]])
    {
        NSDictionary *objdic = obj;
        NSMutableDictionary *dic = [NSMutableDictionary dictionaryWithCapacity:[objdic count]];
        for(NSString *key in objdic.allKeys)
        {
            [dic setObject:[self getObjectInternal:[objdic objectForKey:key]] forKey:key];
        }
        return dic;
    }
    return [self getDictionaryObjectData:obj];
}
+ (RCMessageContent *)toMessageContent:(NSDictionary *)content {
    NSString *objectName = content[@"objectName"];
    RCMessageContent *messageContent;
    
    
    if ([objectName isEqualToString:@"RC:TxtMsg"]) {
        RCTextMessage *text = [RCTextMessage messageWithContent:content[@"content"]];
        text.extra = content[@"extra"];
        messageContent = text;
    } else if ([objectName isEqualToString:@"RC:ImgMsg"]) {
        NSString *local = content[@"local"];
        RCImageMessage *image = [RCImageMessage
                                 messageWithImageURI:[local stringByReplacingOccurrencesOfString:@"file://" withString:@""]];
        image.extra = content[@"extra"];
        messageContent = image;
    } else if ([objectName isEqualToString:@"RC:FileMsg"]) {
        NSString *local = content[@"local"];
        RCFileMessage *file = [RCFileMessage
                               messageWithFile:[local stringByReplacingOccurrencesOfString:@"file://" withString:@""]];
        file.extra = content[@"extra"];
        messageContent = file;
    } else if ([objectName isEqualToString:@"RC:LBSMsg"]) {
        CLLocationCoordinate2D coordinate = CLLocationCoordinate2DMake(
                                                                       [content[@"latitude"] doubleValue], [content[@"longitude"] doubleValue]);
        RCLocationMessage *location = [RCLocationMessage messageWithLocationImage:nil
                                                                         location:coordinate
                                                                     locationName:content[@"name"]];
        location.extra = content[@"extra"];
        messageContent = location;
    } else if ([objectName isEqualToString:@"RC:VcMsg"]) {
        NSData *data = [[NSData alloc] initWithBase64EncodedString:content[@"data"] options:0];
        RCVoiceMessage *voice = [RCVoiceMessage messageWithAudio:data
                                                        duration:[content[@"duration"] intValue]];
        voice.extra = content[@"extra"];
        messageContent = voice;
    } else if ([objectName isEqualToString:@"RC:CmdMsg"]) {
        messageContent = [RCCommandMessage messageWithName:content[@"name"] data:content[@"data"]];
    }
    
    if (messageContent) {
        NSDictionary *userInfo =[self dictionaryWithJsonString:content[@"userInfo"]];
        if (userInfo) {
            messageContent.senderUserInfo = [[RCUserInfo alloc] initWithUserId:userInfo[@"userId"]
                                                                          name:userInfo[@"name"]
                                                                      portrait:userInfo[@"portraitUrl"]];
        }
        
        NSDictionary *mentionedInfo = [self dictionaryWithJsonString:content[@"mentionedInfo"]];
        if (mentionedInfo) {
            messageContent.mentionedInfo =
            [[RCMentionedInfo alloc] initWithMentionedType:(RCMentionedType)[mentionedInfo[@"type"] intValue]
                                                userIdList:mentionedInfo[@"userIdList"]
                                          mentionedContent:mentionedInfo[@"mentionedContent"]];
        }
    }
 //   NSLog(@"content:%@",[self jsonStringWithObject:messageContent]);
    return messageContent;
}
+(void)sendCommSuccessCallBack:(NSString *)callback{
    [self SendMessage:callback sendMessage:[self createDict:@"status" forValue:@"success"]];
}
+(void)sendCommErrorCallBack:(NSString *)callback errCode:(NSObject *)code{
    NSDictionary* msg=[self createDict:@"status" forValue:@"error"];
    [msg setValue:code forKey:@"errorCode"];
    [self SendMessage:callback sendMessage:msg];
}
+(void)sendCommErrorCallBack:(NSString *)callback errCode:(NSObject *)code eventId:(char*)eventId{
    NSDictionary* msg=[self createDict:@"status" forValue:@"error"];
    [msg setValue:code forKey:@"errorCode"];
    [msg setValue:[self MakeStringFromChar:eventId] forKey:@"eventId"];
    [self SendMessage:callback sendMessage:msg];
}
+(void) onSendMessageError:(NSString*) eventId messageId:(long) messageId errcode:(int) errcode{
    NSDictionary *msg=[RongCloudUnityUtils createDict:@"state" forValue:@"error"];
    [msg setValue:eventId forKey:@"eventId"];
    [msg setValue:@(messageId) forKey:@"messageId"];
    [msg setValue:@(errcode) forKey:@"errorCode"];
    [RongCloudUnityUtils SendMessage:@"SendMessageCallback" sendMessage:msg];
}
+(NSArray*)getArraryFromString:(NSString*) str{
    NSData* data=[str dataUsingEncoding:NSUTF8StringEncoding];
    NSError *e;
    return [NSJSONSerialization JSONObjectWithData:data options:nil error:&e];
}
+(NSArray*)getDictionaryByArray:(NSArray *) array{
    NSMutableArray *arr = [NSMutableArray arrayWithCapacity:array.count];
    for(int i=0;i<array.count;i++){
        [arr addObject:[self getDictionaryObjectData:array[i]]];
    }
    return arr;
}
@end

