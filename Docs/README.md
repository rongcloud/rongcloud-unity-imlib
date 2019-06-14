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

### 初始化 SDK

您在使用融云 SDK 所有功能之前，您必须先调用此方法初始化 SDK。在 App 的整个生命周期中，您只需要将 SDK 初始化一次。

```c
using cn.rongcloud.sdk;

RongCloud.init(appKey);
```

### 连接服务器

在 App 整个生命周期，您只需要调用一次此方法与融云服务器建立连接。之后无论是网络出现异常或者 App 有前后台的切换等，SDK 都会负责自动重连。
SDK 针对 App 的前后台和各种网络状况，进行了连接和重连机制的优化，建议您调用一次 `connect` 即可，其余交给 SDK 处理。
除非您已经手动将连接断开，否则您不需要自己再手动重连。

```c
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

```c
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

```c
RongCloud.logout();
```

## 消息接口

### 发送消息

融云支持向单聊、群组及聊天室中发送文字消息、图片消息、语音消息、文件消息、富文本消息、地理位置消息。

#### 发送文本消息

```c
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