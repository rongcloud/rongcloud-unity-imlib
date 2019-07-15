#ifndef __RongCloudUnityUtils
#define __RongCloudUnityUtils
#import <Foundation/Foundation.h>
#import <RongIMLib/RongIMLib.h>
#import "UnityInterface.h"
@interface RongCloudUnityUtils:NSObject
+(void)SendMessage:(NSString *) callback sendMessage:(id) msg;
+(char*) MakeStringCopy:(NSString*) nstring;
+(NSDictionary *)createDict:(NSString *)key
                   forValue:(NSObject *)value;
+(NSString *) jsonStringWithObject:(id) object;
+(NSString *) jsonStringWithDictionary:(NSDictionary *)dictionary;
+(NSString *) jsonStringWithArray:(NSArray *)array;
+(NSString *) jsonStringWithString:(NSString *) string;
+ (NSDictionary *)dictionaryWithJsonString:(NSString *)jsonString;
+ (NSDictionary*)getDictionaryObjectData:(id)obj;
+(NSArray *) arrayDictWithArrayObject:(NSArray *) obj;
+ (id)getObjectInternal:(id)obj;
+ (RCMessageContent *)toMessageContent:(NSDictionary *)content;
+(void)sendCommSuccessCallBack:(NSString *)callback;
+(void)sendCommErrorCallBack:(NSString *)callback errCode:(NSObject *)code;
+(void) onSendMessageError:(NSString*) eventId messageId:(long) messageId errcode:(int) errcode;
+(NSArray*)getArraryFromString:(NSString*) str;
+(NSString *) MakeStringFromChar:(char *)charStr;
+(void)sendCommErrorCallBack:(NSString *)callback errCode:(NSObject *)code eventId:(char*)eventId;
+(void)SendBoolMessage:(NSString *)callback result:(BOOL)result;
+(NSArray*)getDictionaryByArray:(NSArray *) array;
@end
#endif
