## 前期准备

融云 [Unity IMLib](https://github.com/rongcloud/rongcloud-unity-imlib) 是以 IMLib SDK 2.9.17 版本为基础实现的开源项目，支持 Android、iOS，开发者在集成使用过程中如遇到问题可提交到 GitHub 的 Issues 中，融云技术支持人员会在 1 个工作日内回复问题，谢谢您对融云的理解与支持。

### 注册开发者账号

请前往[融云官方网站](https://developer.rongcloud.cn/signup)注册开发者帐号。注册时，您需要提供真实的邮箱和手机号，以方便我们向您发送重要通知并在紧急时刻能够联系到您。
如果您没有提供正确可用的邮箱和手机号，我们随时可能关闭您的应用。

### 创建应用

注册了开发者帐号之后，在进行开发 App 之前，您需要前往[融云开发者控制台](https://developer.rongcloud.cn/signup)创建应用。
创建完应用之后，在您的应用中，会自动创建两套环境，即：开发环境和生产环境

![](https://user-images.githubusercontent.com/1709072/55678058-2bcd1280-5926-11e9-867f-b6964b1bdbaf.png)

开发环境专门用于开发测试，生产环境专门用于您的应用上线正式商用。两套环境相互隔离，每个环境都有相应的 App Key 和 App Secret。两个环境消息不互通。

### 获取 Token

Token 称为用户令牌，App Key 是您的 App 的唯一标识，Token 则是您 App 上的每一个用户的身份授权象征。
您可以通过提交 userId 等信息来获得一个该用户对应的 Token，并使用这个 Token 作为该用户的唯一身份凭证与其他用户进行通信。

Token 的主要作用是身份授权和安全，因此不能通过客户端直接访问融云服务器获取 Token，您必须通过 Server API 从融云服务器获取 Token 返回给您的 App，并在之后连接时使用。
详细描述请参考 [Server 开发指南](https://www.rongcloud.cn/docs/server.html#获取_Token_方法) 中的用户服务和获取 Token 方法小节。

> 为了方便您在集成和测试过程中使用，我们还提供了 API 调试工具，在您不能部署服务器程序时，可以直接通过传入 userId 和 name 来获得 Token。
> 请访问融云开发者平台，打开您想测试的应用，在左侧菜单中选择 “[API 调试](https://developer.rongcloud.cn/apitool/qmZqBYUO1SBO+jDieKo=)”即可。

## SDK 集成

下载 [Unity IMLib](https://github.com/rongcloud/rongcloud-unity-imlib) 将 rongcloud_unity.unitypackage 导入 Unity 工程。
将 RongCloud.prefab 拖入您的启动场景。

![image](https://raw.githubusercontent.com/rongcloud/rongcloud-unity-imlib/master/Docs/images/dropprefab.png)

## 基本用法

### 事件监听

所有的事件监听方法均为方法名加 Event 。监听事件均在 RCEvent 类中。

例如： connect 方法的监听事件为 connectEvent ,返回数据均为 json 数据。

```csharp
    //设置监听
    RCEvent.connectEvent+=connectEvent;
     //移除监听
    RCEvent.connectEvent-=connectEvent;
```

### 初始化 SDK

您在使用融云 SDK 所有功能之前，您必须先调用此方法初始化 SDK。在 App 的整个生命周期中，您只需要将 SDK 初始化一次。

```csharp
using cn.rongcloud.sdk;

RongCloud.init(appKey);
```

### 连接服务器

在 App 整个生命周期，您只需要调用一次此方法与融云服务器建立连接。之后无论是网络出现异常或者 App 有前后台的切换等，SDK 都会负责自动重连。
SDK 针对 App 的前后台和各种网络状况，进行了连接和重连机制的优化，建议您调用一次 `connect` 即可，其余交给 SDK 处理。
除非您已经手动将连接断开，否则您不需要自己再手动重连。

```csharp
using cn.rongcloud.sdk;
void Start()
{
    //设置监听
    RCEvent.connectEvent+=connectEvent;
    RongCloud.connect(token);
}
void connectEvent(JsonData msg)
 {
     if(msg["status"].ToString()=="success"){
         Debug.Log("连接成功");
     }else if(msg["status"].ToString()=="error"){
         Debug.Log("连接失败："+(ErrorCode)int.Parse(msg["errorCode"].ToString()));
     }else{
         Debug.Log("Token 不正确或已过期");
     }
}

void OnDestroy()
{
    //移除监听
    RCEvent.connectEvent-=connectEvent;
}
```

### 监听连接状态

```csharp
void Start()
{
    //设置监听
    RCEvent.connectionStatusEvent+=connectionStatusEvent;
    RongCloud.connect(token);
}
void connectionStatusEvent(JsonData msg)
 {
     if(msg["status"].ToString()=="success"){
         Debug.Log("连接成功");
     }else if(msg["status"].ToString()=="error"){
         Debug.Log("连接失败："+(ErrorCode)int.Parse(msg["errorCode"].ToString()));
     }else{
         Debug.Log("Token 不正确或已过期");
     }
}
void OnDestroy()
{
    //移除监听
    RCEvent.connectionStatusEvent-=connectionStatusEvent;
}
```

### 断开连接

融云 SDK 提供以下两种断开连接的方法：

如果您想在断开和融云的连接后，有新消息时，仍然能够收到推送通知，调用 disconnect() 方法。

```csharp
RongCloud.disconnect();
```

如果断开连接后，有新消息时，不想收到任何推送通知，调用 logout() 方法。

```csharp
RongCloud.logout();
```

## 消息接口

### 发送消息

融云支持向单聊、群组及聊天室中发送文字消息、图片消息、语音消息、文件消息、富文本消息、地理位置消息。

#### 发送文本消息

```csharp
void Start()
{
    //设置监听
    RCEvent.sendMessageEvent+=sendMessageEvent;
    RongCloud.connect(token);
}
void sendMessageEvent(JsonData msg)
 {
     if(msg["status"].ToString()=="success"&&msg["eventId"]=="eventID"){
         Debug.Log("发送成功");
     }else {
         Debug.Log("发送失败："+(ErrorCode)int.Parse(msg["errorCode"].ToString()));
     }
}
void OnDestroy()
{
    //移除监听
    RCEvent.sendMessageEvent-=sendMessageEvent;
}
void sendMessage(){
    TextMessage tm = new TextMessage();
    var conversationType = ConversationType.CHATROOM;
    var targetId = "roomId"; // 根据会话类型的不同，可以是用户 ID、讨论组 ID、组群 ID 等
    tm.content = m_message.text;
    //eventID 每条消息Id唯一，用于判断该条消息是否发送成功
    RongCloud.sendMessage(conversationType, targetId, tm, "eventID");
}
```
####发送图片消息

```csharp
    ImageMessage imageMessage = new ImageMessage();
    imageMessage.local= "file:///image_path";
    Message message = new Message();
    message.content = fm;
    message.conversationType = ConversationType.GROUP;
    message.targetId = DiscussionID;
    RongCloud.sendMediaMessage(message, "", "", "eventId");
```

####发送文件消息

```csharp
    FileMessage fm = new FileMessage();
    fm.local = "file:///image_path" ;
    Message message = new Message();
    message.content = fm;
    message.conversationType = ConversationType.GROUP;
    message.targetId = DiscussionID;
    RongCloud.sendMediaMessage(message, "", "", "eventId");
```

####发送位置消息

```csharp
    LocationMessage locationMessage = new LocationMessage();
    locationMessage.name = "位置名称";
    locationMessage.latitude =223;
    locationMessage.longitude =333;
    locationMessage.extra = "";
    locationMessage.thumbnail="http://example.com/thum.jpg";
    var conversationType = ConversationType.CHATROOM;
    var targetId = "roomId"; // 根据会话类型的不同，可以是用户 ID、讨论组 ID、组群 ID 等
    //eventID 每条消息Id唯一，用于判断该条消息是否发送成功
    RongCloud.sendMessage(conversationType, targetId, locationMessage, "eventID");
```

####发送语音信息

融云 Unity SDK 不提供语音录制、转码功能，开发者需要自已实现语音消息录制、转码，通过融云内置的语音消息进行发送。

```csharp
    VoiceMessage voiceMessage = new VoiceMessage();
    voiceMessage.data= "audio raw data", // iOS 使用二进制数据的方式发送
    voiceMessage.local = "audio path", // Android 使用文件方式发送
    voiceMessage.duration = 9; // 语音持续时间，单位：秒
     var conversationType = ConversationType.CHATROOM;
    var targetId = "roomId"; // 根据会话类型的不同，可以是用户 ID、讨论组 ID、组群 ID 等
    //eventID 每条消息Id唯一，用于判断该条消息是否发送成功
    RongCloud.sendMessage(conversationType, targetId, voiceMessage, "eventID");
```

###插入消息

融云 SDK 支持往本地会话插入一条消息，而不往外发送。

```csharp
    TextMessage tm = new TextMessage();
    tm.content = "hello";
    //插入一条接收消息
    RongCloud.insertIncomingMessage(conversationType, targetId, senderUserId, ReceivedStatus.READ, tm);
    //插入一条发送消息
     RongCloud.insertOutgoingMessage(conversationType, targetId, SentStatus.FAILED, tm);
```

###发送 VoIP Push

通过 option 设置此消息是否发启 VoIP Push 通知，目前仅支持设置 isVoIPPush，如果对端设备是 iOS，设置 isVoIPPush 为 True，则会发送 VoIP Push，如对端为 Android 设备则正常发送此消息。

```csharp
    RongCloud.sendMessage(message, pushContent, pushData, true, "eventId");
```

###删除消息

清空某一会话的所有消息。

```csharp
    RongCloud.clearMessages(conversationType,targetId);
```

删除某一会话的所有消息，同时清理数据库空间，如果数据库特别大，该接口会相对耗时，建议在清理缓存时调用。

```csharp
    RongCloud.deleteMessages(conversationType,targetId);
```

根据 messageId，删除指定的一条或多条消息。

```csharp
    //ids 为 int[]
    RongCloud.deleteMessages(ids);
```

删除某会话指定时间之前的消息，可选择是否同时删除服务器端消息

```csharp  
    RongCloud.cleanHistoryMessages( conversationType,  targetId,  recordTime,  cleanRemote);
```

###获取本地消息历史

```csharp  
    //获取指定类型，targetId 的N条历史消息记录。通过此接口可以根据情况分段加载历史消息，节省网络资源，提高用户体验。
    RongCloud.getHistoryMessages( conversationType,  targetId,oldesMessageId,count);
    // 获取某一会话中特定类型消息的本地历史记录，比如可以通过此接口拉取会话中某一条消息之前的 10 条图片消息。
    RongCloud.getHistoryMessages(conversationType, targetId, objectName, baseMessageId, count,direction);
    // 获取某会话中指定消息的前 before 数量和 after 数量的消息。
    RongCloud.getHistoryMessages(conversationType,targetId,sentTime,before,after);
    //获取本地数据库中保存，特定类型，targetId 的N条历史消息记录。通过此接口可以根据情况分段加载历史消息，节省网络资源，提高用户体验。
    RongCloud.getHistoryMessages( conversationType,  targetId,objectName,oldesMessageId,count);

```

###获取服务端历史消息

提供单聊、群聊、客服的历史消息获取，您每次可以从服务器获取之前 20 条以内的消息历史记录，最多获取前 6 个月的历史消息。

该功能需要在开发者后台“应用/IM 服务/高级功能设置”中开通 IM 商用版后，开启单群聊消息云存储功能才能使用，开发环境下可免费使用。

[查看收费详情](http://www.rongcloud.cn/pricing#pay)

```csharp  
    RongCloud.getRemoteHistoryMessages(conversationType,targetId,dateTime,count);
```

###搜索消息

```csharp  
    RongCloud.searchMessages(conversationType,targetId,keyword,count,beginTime);
```

### 消息监听

通过添加消息监听器，可以监听到所有接收的消息。

```csharp
void Start()
{
    RCEvent.onReceiveMessageEvent += receiveMessage;
}
void receiveMessage(Message msg,int left)
{
    if (msg.objectName == ObjectName.Text)
    {
        TextMessage content = (TextMessage)msg.content;
        Debug.Log("content:"+content.content + "  sender:" + msg.senderUserId);
    }
    
}

```

##会话管理

###获取会话

```csharp  
    RongCloud.getConversation(conversationType,targetId);
```

###删除会话

```csharp  
    RongCloud.removeConversation(conversationType,targetId);
```

###置顶会话

```csharp  
    RongCloud.setConversationToTop(conversationType,targetId,true);
```

###搜索会话

```csharp  
    List<ConversationType> types = new List<ConversationType>();
    types.Add( ConversationType.GROUP);
    types.Add(ConversationType.DISCUSSION);
    List<string> names = new List<string>();
    names.Add(ObjectName.Text);
    names.Add(ObjectName.File);
    RongCloud.searchConversations(keyword,types,names);
```

###会话文本消息草稿

这些草稿信息仅存储于本地数据库中，不会上传服务器。

```csharp  
    // 设置会话文本消息草稿
    RongCloud.saveTextMessageDraft(conversationType, targetId, content);

    // 获取会话文本消息草稿
    RongCloud.getTextMessageDraft(conversationType, targetId);
```

###会话消息提醒

通过融云 SDK，您可以设置会话的提醒状态来实现免打扰功能。按照免打扰作用范围，分为两种类型：

- 设置单个会话的提醒状态。通过此方法，您可以屏蔽某个会话的通知提醒和推送。

```csharp  
    // 设置会话文本消息草稿
    RongCloud.setConversationNotificationStatus(conversationType, targetId, isBlocked);

    // 获取会话文本消息草稿
    RongCloud.getConversationNotificationStatus(conversationType, targetId);
```

- 设置所有会话的通知免打扰。可以设置某一个时间段免打扰，也可以设置全天免打扰。

```csharp  
    // 设置全局消息免打扰时段
    // startTime 的格式为 HH:MM:SS
    // spanMinutes 为设置的免打扰结束时间距离起始时间的间隔分钟数
    RongCloud.setNotificationQuietHours(startTime, spanMinutes);

    // 移除全局消息免打扰
    RongCloud.removeNotificationQuietHours();
```

## @ 功能

群组中支持 @ 消息功能，满足您 @ 指定用户或 @ 所有人的需求，只需要在 `MessageContent` 中添加 `mentionedInfo` 字段。

```csharp  
    TextMessage tm = new TextMessage();
    ConversationType conversationType = ConversationType.GROUP;
    string targetId = DiscussionID; // 根据会话类型的不同，可以是用户 ID、讨论组 ID、组群 ID 等
    tm.content = m_message.text;
    MentionedInfo info = new MentionedInfo();
    info.type = MentionedType.ALL;
    info.mentionedContent = "asfdasdfasdf";
    tm.mentionedInfo = info;
    RongCloud.sendMessage(conversationType, targetId, tm, "eventID");
```

收到 @ 消息时，在 `Conversation` 里的 `hasUnreadMentioned` 会被设为 `true`。

```csharp  
    RCEvent.getConversationEvent+=getConversationEvent;
    RongCloud.getConversation(conversationType, targetId);
    
    void getConversationEvent(JsonData msg)
    {
        if(msg["status"].ToString()=="success"){
            
            Debug.Log(msg["conversation"]["hasUnreadMentioned"]);
        }
    }
```

您可以用 `getUnreadMentionedMessages` 获取会话里所有未读 @ 消息

```csharp  
    RongCloud.getUnreadMentionedMessages(conversationType, targetId);
```

## 黑名单管理

您可以将用户加入、移出黑名单，也可以查询当前已经设置的黑名单。

```csharp  
    // 将用户添加至黑名单
    RongCloud.addToBlacklist(userId);

    // 将用户从黑名单移出
    RongCloud.removeFromBlacklist(userId);

    // 查询某个用户是否已经在黑名单中
    RongCloud.getBlacklistStatus(userId);

    // 获取所有黑名单
    RongCloud.getBlacklist();
```

## 组群业务

群组关系和群组列表由您的 App 维护，客户端的所有群组操作都需要请求您的 App Server，
您的 App Server 可以根据自己的逻辑进行管理和控制，然后通过 Server API 接口进行群组操作，并将结果返回给客户端。

以下展示了客户端进行群组操作的流程。

### 创建组群

![](https://user-images.githubusercontent.com/1709072/55683214-e7fefb00-596f-11e9-9669-01d6ab343f8f.png)

### 加入群组

![](https://user-images.githubusercontent.com/1709072/55683227-17156c80-5970-11e9-9f01-66ffd677ed2e.png)

### 退出群组

![](https://user-images.githubusercontent.com/1709072/55683232-272d4c00-5970-11e9-870e-530452b9c29a.png)

### 解散群组

![](https://user-images.githubusercontent.com/1709072/55683237-36ac9500-5970-11e9-91ef-dc8ebc3c51c2.png)

### 设置群组信息

![](https://user-images.githubusercontent.com/1709072/55683244-47f5a180-5970-11e9-95fc-4f68b4424fae.png)

### 获取群组成员列表

![](https://user-images.githubusercontent.com/1709072/55683252-5b087180-5970-11e9-8b5c-c2e2fe37c49e.png)

### 获取群组列表

![](https://user-images.githubusercontent.com/1709072/55683253-5d6acb80-5970-11e9-8e05-753c190242b4.png)

> 建议在登录成功之后从 App 服务器获取一次群组列表信息，以保证客户端和服务器的群组信息同步，提升用户体验。

## 聊天室业务

聊天室是指多个用户一起聊天，用户数量没有上限。和其它业务场景的主要区别如下：

- 用户退出聊天界面后即视为离开聊天室，不会再接收到任何聊天室消息。

- 聊天室消息不会保存到本地数据库，融云服务端最多保存聊天室最近的 50 条消息。客户端在调用加入聊天室接口时可以设置进入聊天室时的拉取消息数量。

- 聊天室的会话关系由融云负责建立并保持连接，通过 SDK 相关接口，可以让用户加入或者退出聊天室。

### 加入聊天室

```csharp  
    // 加入聊天室，如果聊天室不存在，会自动创建并加入。
    // messageCount 为默认拉取的消息数量，-1 时不拉取任何消息，0 时拉取 10 条消息，最多只能拉取 50 条
    RongCloud.joinChatRoom(chatRoomId, messageCount);
    // 加入已经存在的聊天室
    RongCloud.joinExistChatRoom(chatRoomId, messageCount);

```

### 退出聊天室

```csharp
    RongCloud.quitChatRoom(chatRoomId);
```

### 查询聊天室信息

查询聊天室中最早或最晚加入的 N 个成员信息，包括成员 id， 加入聊天室时间，以及当前聊天室总人数。


```csharp
    // 获取的成员数为 memberCount，排序方式按成员加入时间倒序排序
    RongCloud.getChatRoomInfo(chatRoomId, memberCount, ChatRoomMemberOrder.DESC);
```

### 获取服务器聊天室历史消息

> 该功能需开通后才能使用，详细请查看[聊天室消息云存储服务说明](https://www.rongcloud.cn/docs/payment.html#chatroom_message_cloud_storage)。

开通聊天室消息存储功能后，融云内置的文字、语音、图片、图文、位置、文件等消息会自动在服务器端进行存储。

```csharp
// 获取从 timestamp 开始的 count 条消息，按倒序排序
RongCloud.getChatroomHistoryMessages(
  chatRoomId,
  timestamp,
  count,
  TimestampOrder.DESC
);
```

## 客服业务

- 在进入到客服聊天界面时，调用 `startCustomService` 来启动客服服务。启动的状态要在回调里面处理，启动成功后会回调 `success`，
  并携带配置信息 `CustomServiceConfig` 。根据 `modeChanged` 回调来处理不同的键盘输入。在机器人优先模式下，需要在界面上加上转人工的按钮。

- 当 `quit` 时，离开客服会话或者提示客服服务已经结束。

- 当用户按下转人工服务时，调用 `switchToHumanMode` 来切换到人工服务。如果调用成功，`modeChanged` 回调返回服务类型。

- 当离开界面时，调用 `stopCustomeService` 来结束客服。

- 在适当的时机对客服进行评价，调用 `evaluateCustomService`，根据参数不同评价机器人或者人工。

- 当 `selectGroup` 时，App 需要弹出技能组选择界面供用户选择。

- 当用户选择技能组后，调用 `selectCustomerServiceGroup` 来启动对应技能组客服，
  如果用户没有选择，也必须调用 `selectCustomerServiceGroup` 来启动默认客服，groupId 此时为 `null`。

```csharp
/// <summary>
/// 启动客服服务
/// </summary>
/// <param name="kefuId">客服 id,用户去融云开发者后台申请开通后获得客服Id</param>
/// <param name="customServiceInfo">客服用户信息,包括用户基本信息，用户联系信息以及请求信息
/// 其中 nickName 不能为空, 如果为空,则上传当前用户userId</param>
public static void startCustomerService(string kefuId, CSCustomServiceInfo customServiceInfo)

/// <summary>
/// <p>切换到人工客服模式,切换的结果需要在 {@link ICustomServiceListener#onModeChanged(CustomServiceMode)} 方法回调中处理</p>
/// 如果客服没有分组, 则直接切人工模式;如果客服有分组,那么需要在回调{@link ICustomServiceListener#onSelectGroup(List)}
/// 中去弹出分组选择窗口并选择分组,之后在这个回调中调用 @see{@link RongIMClient#selectCustomServiceGroup(String, String)}
/// 根据客服 Id 和 分组 Id 去切换到人工模式
/// <p>客服模式 分为无服务,机器人模式,人工模式,机器人优先模式,人工优先模式</p>
/// </summary>
/// <param name="kefuId">客服 id,用户去融云开发者后台申请开通后获得客服Id</param>
public static void switchToHumanMode(string kefuId)

/// <summary>
/// 根据客服ID和分组ID转换人工模式
/// </summary>
/// <param name="kefuId">客服ID</param>
/// <param name="groupId">分组ID</param>
public static void selectCustomServiceGroup(string kefuId, string groupId)

/// <summary>
/// 对机器人客服评价，在机器人模式下使用此方法进行评价
/// </summary>
/// <param name="kefuId">客服 id,用户去融云开发者后台申请开通后获得客服Id</param>
/// <param name="isRobotResolved">robot 客服是否解决了您的问题. true 表示解决 ,false 表示未解决</param>
/// <param name="knowledgeId"> 机器人评价的消息id,同时满足以下4个条件,此参数有效,其余情况可以传空字符串.
/// 当参数有效时, 取出4中描述的 “sid” 对应的值就是需要传入的knowledgeId,
/// 1.机器人模式
/// 2.新收到的消息,不是从数据库中加载的历史消息
/// 3.CustomServiceConfig 的 robotSessionNoEva 为true  @see {@link io.rong.imlib.CustomServiceConfig }
/// 这个CustomServiceConfig 是客服启动成功后的回调onSuccess()带回的参数 @see { @link io.rong.imlib.ICustomServiceListener }
/// 4.MessageContent 的 Extra 中有“robotEva”和“sid”两个字段.</param>
public static void evaluateCustomService(string kefuId, bool isRobotResolved, string knowledgeId)

/// <summary>
/// 对人工客服评价，在人工模式下使用此方法进行评价
/// </summary>
/// <param name="kefuId">客服 id,用户去融云开发者后台申请开通后获得客服Id</param>
/// <param name="source">星级，范围 1-5，5为最高,1为最低</param>
/// <param name="solveStatus">解决状态，如果没有解决状态，这里可以随意赋值，SDK不会处理</param>
/// <param name="tagText">客户评价的标签</param>
/// <param name="suggest">客户的针对人工客服的意见和建议</param>
/// <param name="dialogId">对话ID，客服请求评价的对话ID</param>
/// <param name="extra">扩展内容</param>
public static void evaluateCustomService(string kefuId, int source,CSEvaSolveStatus solveStatus,string tagText, string suggest, string dialogId,string extra)

/// <summary>
/// 结束客服. 调用此方法后,将向客服发起结束请求
/// </summary>
/// <param name="kefuId">客服 id,用户去融云开发者后台申请开通后获得客服Id</param>
public static void stopCustomService(string kefuId)
```

## 高级功能

### 消息撤回

使用 `recallMessage` 接口可以撤回已发送的消息，撤回成功后，该消息在数据库中将被替换成 `RecallNotificationMessage`，您需要在成功回调里重新加载这条数据，刷新聊天界面。

```csharp
/// <summary>
/// 撤回消息
/// </summary>
/// <param name="messageId">将被撤回的消息 Id</param>
/// <param name="pushContent">推送内容</param>
RongCloud.recallMessage(int messageId, string pushContent)
```

### 单聊消息阅读回执

您可以在用户查看了单聊会话中的未读消息之后，向会话中发送已读回执，会话中的用户可以根据此回执，在 UI 中更新消息的显示。
其中，timestamp 为会话中用户已经阅读的最后一条消息的发送时间戳（Message 的 sentTime 属性），代表用户已经阅读了该会话中此消息之前的所有消息。

```csharp
RongCloud.sendReadReceiptMessage(conversationType, targetId, timestamp);
```

在接收端，您可以添加阅读回执消息监听函数。

```csharp
RCEvent.ReadReceiptListenerEvent+=ReadReceiptListenerEvent;
void ReadReceiptListenerEvent(JsonData msg){
    if(msg["type"]=="onReadReceiptReceived"){

    }
    if(msg["type"]=="onMessageReceiptRequest"){

    }
    if(msg["type"]=="onMessageReceiptResponse"){

    }
    
}
```

### 组群消息阅读回执

群组消息请求回执，对于需要阅读之后收到阅读回执的消息，可以调用这个接口来发送阅读回执请求。

```csharp
// 发送阅读回执请求
RongCloud.sendReadReceiptRequest(messageId);

// 收到消息已读回执请求的回调函数（收到此请求后，如果用户阅读了对应的消息，需要调用 sendMessageReadReceiptResponse 接口发送已读响应）
RCEvent.setReadReceiptListener+=setReadReceiptListener;
```

已读响应，当收到阅读回执请求之后，如果用户阅读了对应的消息，可以调用此接口来发送消息阅读回执响应。

```csharp
// 发送阅读回执
RongCloud.sendReadReceiptResponse(conversationType, targetId, messageList);

// 消息已读回执响应
RCEvent.ReadReceiptListenerEvent+=ReadReceiptListenerEvent;
```

### 多端阅读消息数同步

多端登录时，通知其它终端同步某个会话的阅读状态，请调用下面接口：

```csharp
// 其中 timestamp 为该会话中已读的最后一条消息的发送时间戳
RongCloud.syncConversationReadStatus(conversationType, targetId, timestamp);
```

### 输入状态提醒

您可以在用户正在输入的时候，向对方发送正在输入的状态。目前该功能只支持单聊。

其中，您可以在 `typingContentType` 中传入消息的类型名，会话中的其他用户输入状态监听中会收到此消息类型。
您可以通过此消息类型，自定义不同的输入状态提示（如：正在输入、正在讲话、正在拍摄等）。

在 6 秒之内，如果同一个用户在同一个会话中多次调用此接口发送正在输入的状态，为保证产品体验和网络优化，将只有最开始的一次生效。

```csharp
RongCloud.sendTypingStatus(conversationType, targetId, typingContentType);
```

在接收端，您可以设置输入状态的监听器。

当前会话正在输入的用户有变化时，会触发监听中的 onTypingStatusChanged()，回调里携带有当前正在输入的用户和消息类型。
对于单聊而言，当对方正在输入时，监听会触发一次；当对方不处于输入状态时，该监听还会触发一次，但是回调里并不携带用户和消息类型，开发者需要在此时取消正在输入的显示。

```csharp
RCEvent.onTypingStatusChangedEvent+=onTypingStatusChangedEvent;
```

### 群定向消息

此方法用于在群组中给部分用户发送消息，其它用户不会收到这条消息，建议向群中部分用户发送状态类消息时使用此功能。

注：群定向消息不存储到云端，通过“单群聊消息云存储”服务无法获取到定向消息。

```csharp
TextMessage tmsg = new TextMessage();
tmsg.content = m_inputMsg.text;
List<string> userList = new List<string>();
userList.Add("user2");
RongCloud.sendDirectionalMessage(ConversationType.GROUP, UnionID, tmsg, pushContent,pushData, userList, "eventId");
```

### 离线消息设置

设置当前用户离线消息存储时间，以天为单位。

```csharp
RongCloud.setOfflineMessageDuration(days);

RongCloud.getOfflineMessageDuration();
```
