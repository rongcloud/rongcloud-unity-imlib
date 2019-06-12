# rongcloud-unity-imlib

融云 Unity IMLib 是以 IMLib SDK 2.9.17 版本为基础实现的开源项目，支持 Android、iOS，开发者在集成使用过程中如遇到问题可提交到 GitHub 的 Issues 中，融云技术支持人员会在 1 个工作日内回复问题，谢谢您对融云的理解与支持。

## 文档

## 用法

### 初始化并连接融云服务

```c#
RongCloud.init(appKey);
RongCloud.connect(token);
```

### 监听消息

```c#
RCEvent.onReceiveMessageEvent += receiveMessage;
```

### 发送消息

```c#
TextMessage tm = new TextMessage();
tm.content = "消息发送测试！";
RongCloud.sendMessage(ConversationType.CHATROOM, "testRoom", tm,(eventID++).ToString());
```

更多示例请参考 [SampleScene]。

## 运行示例
导入 rongcloud_unity.unitypackage 包
打开 RongCloud/example/Scenes/SampleScene
在 RongCloudSdkExample.cs 中填入 appKey token 等信息。
## 项目结构
```
├── RongUnity (Unity 示例应用)
├── UnityPackage (Unity SDK 包)
├── lib
     ├── android (Android 原生模块)
     └── IOS   (ios 原生模块)

