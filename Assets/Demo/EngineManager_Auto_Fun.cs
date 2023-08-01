using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cn_rongcloud_im_unity;
using System.Threading;

/// <summary>
/// 自动生成的方法调用
/// </summary>
namespace cn_rongcloud_im_wapper_unity_example
{
    public partial class EngineManager
    {
    
        public class GetMessageListenerProxy : RCIMGetMessageListener
        {
            public void OnSuccess(RCIMMessage t)
            {
                Action(t);
            }
            public void OnError(int code)
            {
                Action(null);
            }
            public GetMessageListenerProxy(Action<RCIMMessage> action)
            {
                Action = action;
            }
            private Action<RCIMMessage> Action;
        }
    
        public bool cancelSendingMediaMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("messageId"))
            {
                ExampleUtils.ShowToast("messageId 为空");
                return false;
            }
            string messageId = (string)arg["messageId"];
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            engine.GetMessageById(int.Parse(messageId),
                                  new GetMessageListenerProxy(
                                      (msg) =>
                                      {
                                          if (msg == null)
                                          {
                                              ExampleUtils.ShowToast("未查询到引用的message");
                                              return;
                                          }
                                          if (msg is RCIMMediaMessage)
                                          {
                                              OnCancelSendingMediaMessageCalledAction action = null;
                                              if (useCallback != "0")
                                              {
                                                  action = (int code, RCIMMediaMessage message) =>
                                                  {
                                                      Dictionary<String, object> result = new Dictionary<String, object>();
                                                      result["method"] = "cancelSendingMediaMessageCallback";
                                                      result["code"] = code;
                                                      result["message"] = message;
                                                      ResultHandle?.Invoke(result);
                                                  };
                                              }
                                              int ret = engine.CancelSendingMediaMessage((RCIMMediaMessage)msg, action);
                                              Dictionary<String, object> result = new Dictionary<String, object>();
                                              result["method"] = "cancelSendingMediaMessage";
                                              result["input"] = arg;
                                              result["ret"] = ret.ToString();
                                              ResultHandle?.Invoke(result);
                                          }
                                          else
                                          {
                                              ExampleUtils.ShowToast("当前消息不是媒体消息");
                                          }
                                      }));
            return true;
        }
    
        public bool downloadMediaMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("messageId"))
            {
                ExampleUtils.ShowToast("messageId 为空");
                return false;
            }
            string messageId = (string)arg["messageId"];
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            engine.GetMessageById(
                int.Parse(messageId),
                new GetMessageListenerProxy((msg) =>
                                            {
                                                if (msg == null)
                                                {
                                                    ExampleUtils.ShowToast("未查询到引用的message");
                                                    return;
                                                }
                                                if (msg is RCIMMediaMessage)
                                                {
                                                    RCIMDownloadMediaMessageListener listener = null;
                                                    if (useCallback != "0")
                                                    {
                                                        listener = new RCIMDownloadMediaMessageListenerProxy(ResultHandle);
                                                    }
                                                    int ret = engine.DownloadMediaMessage((RCIMMediaMessage)msg, listener);
                                                    Dictionary<String, object> result = new Dictionary<String, object>();
                                                    result["method"] = "downloadMediaMessage";
                                                    result["input"] = arg;
                                                    result["ret"] = ret;
                                                    ResultHandle?.Invoke(result);
                                                }
                                                else
                                                {
                                                    ExampleUtils.ShowToast("当前消息不是媒体消息");
                                                }
                                            }));
            return true;
        }
    
        public bool cancelDownloadingMediaMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("messageId"))
            {
                ExampleUtils.ShowToast("messageId 为空");
                return false;
            }
            string messageId = (string)arg["messageId"];
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            engine.GetMessageById(int.Parse(messageId),
                                  new GetMessageListenerProxy(
                                      (msg) =>
                                      {
                                          if (msg == null)
                                          {
                                              ExampleUtils.ShowToast("未查询到引用的message");
                                              return;
                                          }
                                          if (msg is RCIMMediaMessage)
                                          {
                                              OnCancelDownloadingMediaMessageCalledAction action = null;
                                              if (useCallback != "0")
                                              {
                                                  action = (int code, RCIMMediaMessage message) =>
                                                  {
                                                      Dictionary<String, object> result = new Dictionary<String, object>();
                                                      result["method"] = "cancelDownloadingMediaMessageCallback";
                                                      result["code"] = code;
                                                      result["message"] = message;
                                                      ResultHandle?.Invoke(result);
                                                  };
                                              }
                                              int ret = engine.CancelDownloadingMediaMessage((RCIMMediaMessage)msg, action);
                                              Dictionary<String, object> result = new Dictionary<String, object>();
                                              result["method"] = "cancelDownloadingMediaMessage";
                                              result["input"] = arg;
                                              result["ret"] = ret;
                                              ResultHandle?.Invoke(result);
                                          }
                                          else
                                          {
                                              ExampleUtils.ShowToast("当前消息不是媒体消息");
                                          }
                                      }));
            return true;
        }
    
        public bool insertMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMTextMessage message = engine.CreateTextMessage(type, targetId, channelId, "这是一条插入的消息");
            message.direction = RCIMMessageDirection.RECEIVE;
            message.receivedStatus = RCIMReceivedStatus.UNREAD;
            OnMessageInsertedAction action = null;
            if (useCallback != "0")
            {
                action = (int code, RCIMMessage message) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "insertMessageCallback";
                    result["code"] = code;
                    result["message"] = message;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.InsertMessage(message, action);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "insertMessage";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        public bool insertMessages(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMTextMessage message1 = engine.CreateTextMessage(type, targetId, channelId, "这是一条插入的消息-01");
            message1.direction = RCIMMessageDirection.RECEIVE;
            message1.receivedStatus = RCIMReceivedStatus.UNREAD;
            RCIMTextMessage message2 = engine.CreateTextMessage(type, targetId, channelId, "这是一条插入的消息-02");
            message2.direction = RCIMMessageDirection.RECEIVE;
            message2.receivedStatus = RCIMReceivedStatus.UNREAD;
            OnMessagesInsertedAction action = null;
            if (useCallback != "0")
            {
                action = (int code, List<RCIMMessage> messages) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "insertMessagesCallback";
                    result["code"] = code;
                    result["messages"] = messages;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.InsertMessages(new List<RCIMMessage> { message1, message2 }, action);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "insertMessages";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        public bool deleteLocalMessages(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("messageIds"))
            {
                ExampleUtils.ShowToast("messageIds 为空");
                return false;
            }
            string[] messageIds = ((string)arg["messageIds"]).Split(',');
            if (messageIds.Length == 0)
            {
                ExampleUtils.ShowToast("messageIds 为空");
                return false;
            }
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            getMessagesByIds(messageIds, (messages) =>
                                         {
                                             OnLocalMessagesDeletedAction action = null;
                                             if (useCallback != "0")
                                             {
                                                 action = (int code, List<RCIMMessage> messages) =>
                                                 {
                                                     Dictionary<String, object> result = new Dictionary<String, object>();
                                                     result["method"] = "deleteLocalMessagesCallback";
                                                     result["code"] = code;
                                                     result["messages"] = messages;
                                                     ResultHandle?.Invoke(result);
                                                 };
                                             }
                                             int ret = engine.DeleteLocalMessages(messages, action);
                                             Dictionary<String, object> result = new Dictionary<String, object>();
                                             result["method"] = "deleteLocalMessages";
                                             result["input"] = arg;
                                             result["ret"] = ret;
                                             ResultHandle?.Invoke(result);
                                         });
            return true;
        }
    
        public bool deleteMessages(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            if (!arg.ContainsKey("messageIds"))
            {
                ExampleUtils.ShowToast("messageIds 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            string[] messageIds = ((string)arg["messageIds"]).Split(',');
            if (messageIds.Length == 0)
            {
                ExampleUtils.ShowToast("messageIds 为空");
                return false;
            }
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            getMessagesByIds(messageIds, (messages) =>
                                         {
                                             OnMessagesDeletedAction action = null;
                                             if (useCallback != "0")
                                             {
                                                 action = (int code, List<RCIMMessage> messages) =>
                                                 {
                                                     Dictionary<String, object> result = new Dictionary<String, object>();
                                                     result["method"] = "deleteMessagesCallback";
                                                     result["code"] = code;
                                                     result["type"] = type;
                                                     result["targetId"] = targetId;
                                                     result["channelId"] = channelId;
                                                     result["messages"] = messages;
                                                     ResultHandle?.Invoke(result);
                                                 };
                                             }
                                             int ret = engine.DeleteMessages(type, targetId, channelId, messages, action);
                                             Dictionary<String, object> result = new Dictionary<String, object>();
                                             result["method"] = "deleteMessages";
                                             result["input"] = arg;
                                             result["ret"] = ret;
                                             ResultHandle?.Invoke(result);
                                         });
            return true;
        }
    
        public bool recallMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("messageId"))
            {
                ExampleUtils.ShowToast("messageId 为空");
                return false;
            }
            string messageId = (string)arg["messageId"];
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            engine.GetMessageById(int.Parse(messageId),
                                  new GetMessageListenerProxy((msg) =>
                                                              {
                                                                  if (msg == null)
                                                                  {
                                                                      ExampleUtils.ShowToast("未查询到引用的message");
                                                                  }
                                                                  else
                                                                  {
                                                                      OnMessageRecalledAction action = null;
                                                                      if (useCallback != "0")
                                                                      {
                                                                          action = (int code, RCIMMessage message) =>
                                                                          {
                                                                              Dictionary<String, object> result =
                                                                                  new Dictionary<String, object>();
                                                                              result["method"] = "recallMessageCallback";
                                                                              result["code"] = code;
                                                                              result["message"] = message;
                                                                              ResultHandle?.Invoke(result);
                                                                          };
                                                                      }
                                                                      int ret = engine.RecallMessage(msg, action);
                                                                      Dictionary<String, object> result =
                                                                          new Dictionary<String, object>();
                                                                      result["method"] = "recallMessage";
                                                                      result["input"] = arg;
                                                                      result["ret"] = ret;
                                                                      ResultHandle?.Invoke(result);
                                                                  }
                                                              }));
            return true;
        }
    
        public bool sendGroupReadReceiptRequest(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("messageId"))
            {
                ExampleUtils.ShowToast("messageId 为空");
                return false;
            }
            string messageId = (string)arg["messageId"];
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            engine.GetMessageById(int.Parse(messageId),
                                  new GetMessageListenerProxy(
                                      (msg) =>
                                      {
                                          if (msg == null)
                                          {
                                              ExampleUtils.ShowToast("未查询到引用的message");
                                          }
                                          else
                                          {
                                              OnGroupReadReceiptRequestSentAction action = null;
                                              if (useCallback != "0")
                                              {
                                                  action = (int code, RCIMMessage message) =>
                                                  {
                                                      Dictionary<String, object> result = new Dictionary<String, object>();
                                                      result["method"] = "sendGroupReadReceiptRequestCallback";
                                                      result["code"] = code;
                                                      result["message"] = message;
                                                      ResultHandle?.Invoke(result);
                                                  };
                                              }
                                              int ret = engine.SendGroupReadReceiptRequest(msg, action);
                                              Dictionary<String, object> result = new Dictionary<String, object>();
                                              result["method"] = "sendGroupReadReceiptRequest";
                                              result["input"] = arg;
                                              result["ret"] = ret;
                                              ResultHandle?.Invoke(result);
                                          }
                                      }));
            return true;
        }
    
        public bool sendGroupReadReceiptResponse(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("messageIds"))
            {
                ExampleUtils.ShowToast("messageIds 为空");
                return false;
            }
            string[] messageIds = ((string)arg["messageIds"]).Split(',');
            if (messageIds.Length == 0)
            {
                ExampleUtils.ShowToast("messageIds 为空");
                return false;
            }
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            getMessagesByIds(messageIds, (messages) =>
                                         {
                                             OnGroupReadReceiptResponseSentAction action = null;
                                             if (useCallback != "0")
                                             {
                                                 action = (int code, List<RCIMMessage> message) =>
                                                 {
                                                     Dictionary<String, object> result = new Dictionary<String, object>();
                                                     result["method"] = "sendGroupReadReceiptResponseCallback";
                                                     result["code"] = code;
                                                     result["messages"] = messages;
                                                     ResultHandle?.Invoke(result);
                                                 };
                                             }
                                             int ret =
                                                 engine.SendGroupReadReceiptResponse(targetId, channelId, messages, action);
                                             Dictionary<String, object> result = new Dictionary<String, object>();
                                             result["method"] = "sendGroupReadReceiptResponse";
                                             result["input"] = arg;
                                             result["ret"] = ret;
                                             ResultHandle?.Invoke(result);
                                         });
            return true;
        }
    
        public bool sendGroupMessageToDesignatedUsers(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            if (!arg.ContainsKey("userIds"))
            {
                ExampleUtils.ShowToast("userIds 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMTextMessage message =
                engine.CreateTextMessage(RCIMConversationType.GROUP, targetId, channelId, "这是一条群定向消息");
            string[] userIds = ((string)arg["userIds"]).Split(',');
            RCIMSendGroupMessageToDesignatedUsersListener listener = null;
            if (useCallback != "0")
            {
                listener = new RCIMSendGroupMessageToDesignatedUsersListenerProxy(ResultHandle);
            }
            int ret = engine.SendGroupMessageToDesignatedUsers(message, new List<string>(userIds), listener);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "sendGroupMessageToDesignatedUsers";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        public bool modifyUltraGroupMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("messageUId"))
            {
                ExampleUtils.ShowToast("messageUId 为空");
                return false;
            }
            string messageUId = (string)arg["messageUId"];
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMTextMessage message =
                engine.CreateTextMessage(RCIMConversationType.ULTRA_GROUP, "1", "1", "这个是超级群修改消息的内容");
            OnUltraGroupMessageModifiedAction action = null;
            if (useCallback != "0")
            {
                action = (int code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "modifyUltraGroupMessageCallback";
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.ModifyUltraGroupMessage(messageUId, message, action);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "modifyUltraGroupMessage";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        public bool loadBatchRemoteUltraGroupMessages(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("messageIds"))
            {
                ExampleUtils.ShowToast("messageIds 为空");
                return false;
            }
            string[] messageIds = ((string)arg["messageIds"]).Split(',');
            if (messageIds.Length == 0)
            {
                ExampleUtils.ShowToast("messageIds 为空");
                return false;
            }
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            getMessagesByIds(messageIds, (messages) =>
                                         {
                                             RCIMGetBatchRemoteUltraGroupMessagesListener listener = null;
                                             if (useCallback != "0")
                                             {
                                                 listener =
                                                     new RCIMGetBatchRemoteUltraGroupMessagesListenerProxy(ResultHandle);
                                             }
                                             int ret = engine.GetBatchRemoteUltraGroupMessages(messages, listener);
                                             Dictionary<String, object> result = new Dictionary<String, object>();
                                             result["method"] = "loadBatchRemoteUltraGroupMessages";
                                             result["input"] = arg;
                                             result["ret"] = ret;
                                             ResultHandle?.Invoke(result);
                                         });
            return true;
        }
    
        public bool recallUltraGroupMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("messageId"))
            {
                ExampleUtils.ShowToast("messageId 为空");
                return false;
            }
            if (!arg.ContainsKey("deleteRemote"))
            {
                ExampleUtils.ShowToast("deleteRemote 为空");
                return false;
            }
            int deleteRemoteInt = int.Parse((string)arg["deleteRemote"]);
            bool deleteRemote = deleteRemoteInt != 0;
            string messageId = (string)arg["messageId"];
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            engine.GetMessageById(int.Parse(messageId),
                                  new GetMessageListenerProxy(
                                      (msg) =>
                                      {
                                          OnUltraGroupMessageRecalledAction action = null;
                                          if (useCallback != "0")
                                          {
                                              action = (int code) =>
                                              {
                                                  Dictionary<String, object> result = new Dictionary<String, object>();
                                                  result["method"] = "recallUltraGroupMessageCallback";
                                                  result["code"] = code;
                                                  ResultHandle?.Invoke(result);
                                              };
                                          }
                                          int ret = engine.RecallUltraGroupMessage(msg, deleteRemote, action);
                                          Dictionary<String, object> result = new Dictionary<String, object>();
                                          result["method"] = "recallUltraGroupMessage";
                                          result["input"] = arg;
                                          result["ret"] = ret;
                                          ResultHandle?.Invoke(result);
                                      }));
            return true;
        }
    
        public bool getBatchRemoteUltraGroupMessages(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("messageIds"))
            {
                ExampleUtils.ShowToast("messageIds 为空");
                return false;
            }
            string[] messageIds = ((string)arg["messageIds"]).Split(',');
            if (messageIds.Length == 0)
            {
                ExampleUtils.ShowToast("messageIds 为空");
                return false;
            }
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            getMessagesByIds(messageIds, (messages) =>
                                         {
                                             RCIMGetBatchRemoteUltraGroupMessagesListener listener = null;
                                             if (useCallback != "0")
                                             {
                                                 listener =
                                                     new RCIMGetBatchRemoteUltraGroupMessagesListenerProxy(ResultHandle);
                                             }
                                             int ret = engine.GetBatchRemoteUltraGroupMessages(messages, listener);
                                             Dictionary<String, object> result = new Dictionary<String, object>();
                                             result["method"] = "getBatchRemoteUltraGroupMessages";
                                             result["input"] = arg;
                                             result["ret"] = ret;
                                             ResultHandle?.Invoke(result);
                                         });
            return true;
        }
    
        private void getMessagesByIds(string[] messageIds, Action<List<RCIMMessage>> action)
        {
            List<RCIMMessage> messages = new List<RCIMMessage>();
            int index = 0;
            foreach (var item in messageIds)
            {
                engine.GetMessageById(int.Parse(item), new GetMessageListenerProxy(
                                                           (msg) =>
                                                           {
                                                               index += 1;
                                                               if (msg != null)
                                                               {
                                                                   messages.Add(msg);
                                                               }
                                                               else
                                                               {
                                                                   ExampleUtils.ShowToast("未查询到消息 messageId:" + item);
                                                               }
                                                               if (index == messageIds.Length)
                                                               {
                                                                   action(messages);
                                                               }
                                                           }));
            }
        }
    
        /*
    //fun_connect_call
    public class RCIMConnectListenerProxy : RCIMConnectListener
    {
    public void OnDatabaseOpened(int code)
    {
        //...
    }
    public void OnConnected(int code,string userId)
    {
        //...
    }
    }
    int ret = engine.Connect(token,timeout,new RCIMConnectListenerProxy());
    //fun_connect_call
    */
    
        public bool connect(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("token"))
            {
                ExampleUtils.ShowToast("token 为空");
                return false;
            }
            string token = (string)arg["token"];
            if (!arg.ContainsKey("timeout"))
            {
                ExampleUtils.ShowToast("timeout 为空");
                return false;
            }
            int timeout = int.Parse((string)arg["timeout"]);
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMConnectListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMConnectListenerProxy(ResultHandle);
            }
            int ret = engine.Connect(token, timeout, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "connect";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_disconnect_call
        int ret = engine.Disconnect(receivePush);
        //fun_disconnect_call
        */
    
        public bool disconnect(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("receivePush"))
            {
                ExampleUtils.ShowToast("receivePush 为空");
                return false;
            }
            bool receivePush = int.Parse((string)arg["receivePush"]) != 0;
            int ret = engine.Disconnect(receivePush);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "disconnect";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_createTextMessage_call
        RCIMTextMessage ret = engine.CreateTextMessage(type,targetId,channelId,text);
        //fun_createTextMessage_call
        */
    
        public bool createTextMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("text"))
            {
                ExampleUtils.ShowToast("text 为空");
                return false;
            }
            string text = (string)arg["text"];
            RCIMTextMessage ret = engine.CreateTextMessage(type, targetId, channelId, text);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "createTextMessage";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_createImageMessage_call
        RCIMImageMessage ret = engine.CreateImageMessage(type,targetId,channelId,path);
        //fun_createImageMessage_call
        */
    
        public bool createImageMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("path"))
            {
                ExampleUtils.ShowToast("path 为空");
                return false;
            }
            string path = (string)arg["path"];
            RCIMImageMessage ret = engine.CreateImageMessage(type, targetId, channelId, path);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "createImageMessage";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_createFileMessage_call
        RCIMFileMessage ret = engine.CreateFileMessage(type,targetId,channelId,path);
        //fun_createFileMessage_call
        */
    
        public bool createFileMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("path"))
            {
                ExampleUtils.ShowToast("path 为空");
                return false;
            }
            string path = (string)arg["path"];
            RCIMFileMessage ret = engine.CreateFileMessage(type, targetId, channelId, path);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "createFileMessage";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_createSightMessage_call
        RCIMSightMessage ret = engine.CreateSightMessage(type,targetId,channelId,path,duration);
        //fun_createSightMessage_call
        */
    
        public bool createSightMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("path"))
            {
                ExampleUtils.ShowToast("path 为空");
                return false;
            }
            string path = (string)arg["path"];
            if (!arg.ContainsKey("duration"))
            {
                ExampleUtils.ShowToast("duration 为空");
                return false;
            }
            int duration = int.Parse((string)arg["duration"]);
            RCIMSightMessage ret = engine.CreateSightMessage(type, targetId, channelId, path, duration);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "createSightMessage";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_createVoiceMessage_call
        RCIMVoiceMessage ret = engine.CreateVoiceMessage(type,targetId,channelId,path,duration);
        //fun_createVoiceMessage_call
        */
    
        public bool createVoiceMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("path"))
            {
                ExampleUtils.ShowToast("path 为空");
                return false;
            }
            string path = (string)arg["path"];
            if (!arg.ContainsKey("duration"))
            {
                ExampleUtils.ShowToast("duration 为空");
                return false;
            }
            int duration = int.Parse((string)arg["duration"]);
            RCIMVoiceMessage ret = engine.CreateVoiceMessage(type, targetId, channelId, path, duration);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "createVoiceMessage";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_createReferenceMessage_call
        RCIMReferenceMessage ret = engine.CreateReferenceMessage(type,targetId,channelId,referenceMessage,text);
        //fun_createReferenceMessage_call
        */
    
        public bool createReferenceMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("referenceMessage"))
            {
                ExampleUtils.ShowToast("referenceMessage 为空");
                return false;
            }
            RCIMMessage referenceMessage = (RCIMMessage)arg["referenceMessage"];
            if (!arg.ContainsKey("text"))
            {
                ExampleUtils.ShowToast("text 为空");
                return false;
            }
            string text = (string)arg["text"];
            RCIMReferenceMessage ret = engine.CreateReferenceMessage(type, targetId, channelId, referenceMessage, text);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "createReferenceMessage";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_createGIFMessage_call
        RCIMGIFMessage ret = engine.CreateGIFMessage(type,targetId,channelId,path);
        //fun_createGIFMessage_call
        */
    
        public bool createGIFMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("path"))
            {
                ExampleUtils.ShowToast("path 为空");
                return false;
            }
            string path = (string)arg["path"];
            RCIMGIFMessage ret = engine.CreateGIFMessage(type, targetId, channelId, path);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "createGIFMessage";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_createCustomMessage_call
        RCIMCustomMessage ret = engine.CreateCustomMessage(type,targetId,channelId,policy,messageIdentifier,fields);
        //fun_createCustomMessage_call
        */
    
        public bool createCustomMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("policy"))
            {
                ExampleUtils.ShowToast("policy 为空");
                return false;
            }
            RCIMCustomMessagePolicy policy = (RCIMCustomMessagePolicy) int.Parse((string)arg["policy"]);
            if (!arg.ContainsKey("messageIdentifier"))
            {
                ExampleUtils.ShowToast("messageIdentifier 为空");
                return false;
            }
            string messageIdentifier = (string)arg["messageIdentifier"];
            Dictionary<string, string> fields = new Dictionary<string, string>();
            string[] keys = ExampleUtils.GetValue(arg, "keys", "").Split(',');
            string[] values = ExampleUtils.GetValue(arg, "values", "").Split(',');
            for (int i = 0; i < keys.Length; i++)
            {
                if (i < values.Length)
                {
                    fields.Add(keys[i], values[i]);
                }
            }
            RCIMCustomMessage ret =
                engine.CreateCustomMessage(type, targetId, channelId, policy, messageIdentifier, fields);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "createCustomMessage";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_createLocationMessage_call
        RCIMLocationMessage ret =
        engine.CreateLocationMessage(type,targetId,channelId,longitude,latitude,poiName,thumbnailPath);
        //fun_createLocationMessage_call
        */
    
        public bool createLocationMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("longitude"))
            {
                ExampleUtils.ShowToast("longitude 为空");
                return false;
            }
            double longitude = (double)arg["longitude"];
            if (!arg.ContainsKey("latitude"))
            {
                ExampleUtils.ShowToast("latitude 为空");
                return false;
            }
            double latitude = (double)arg["latitude"];
            if (!arg.ContainsKey("poiName"))
            {
                ExampleUtils.ShowToast("poiName 为空");
                return false;
            }
            string poiName = (string)arg["poiName"];
            if (!arg.ContainsKey("thumbnailPath"))
            {
                ExampleUtils.ShowToast("thumbnailPath 为空");
                return false;
            }
            string thumbnailPath = (string)arg["thumbnailPath"];
            RCIMLocationMessage ret =
                engine.CreateLocationMessage(type, targetId, channelId, longitude, latitude, poiName, thumbnailPath);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "createLocationMessage";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_sendMessage_call
        public class RCIMSendMessageListenerProxy : RCIMSendMessageListener
        {
            public void OnMessageSaved(RCIMMessage message)
            {
                //...
            }
            public void OnMessageSent(int code,RCIMMessage message)
            {
                //...
            }
        }
        int ret = engine.SendMessage(message,new RCIMSendMessageListenerProxy());
        //fun_sendMessage_call
        */
    
        public bool sendMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("message"))
            {
                ExampleUtils.ShowToast("message 为空");
                return false;
            }
            RCIMMessage message = (RCIMMessage)arg["message"];
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMSendMessageListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMSendMessageListenerProxy(ResultHandle);
            }
            int ret = engine.SendMessage(message, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "sendMessage";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_sendMediaMessage_call
        public class RCIMSendMediaMessageListenerProxy : RCIMSendMediaMessageListener
        {
            public void OnMediaMessageSaved(RCIMMediaMessage message)
            {
                //...
            }
            public void OnMediaMessageSending(RCIMMediaMessage message,int progress)
            {
                //...
            }
            public void OnSendingMediaMessageCanceled(RCIMMediaMessage message)
            {
                //...
            }
            public void OnMediaMessageSent(int code,RCIMMediaMessage message)
            {
                //...
            }
        }
        int ret = engine.SendMediaMessage(message,new RCIMSendMediaMessageListenerProxy());
        //fun_sendMediaMessage_call
        */
    
        public bool sendMediaMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("message"))
            {
                ExampleUtils.ShowToast("message 为空");
                return false;
            }
            RCIMMediaMessage message = (RCIMMediaMessage)arg["message"];
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMSendMediaMessageListener listener = null;
            if (useCallback != "0")
            {
                listener = new RCIMSendMediaMessageListenerProxy(ResultHandle);
            }
            int ret = engine.SendMediaMessage(message, listener);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "sendMediaMessage";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_cancelSendingMediaMessage_call
        int ret = engine.CancelSendingMediaMessage(message,(code,message) =>
        {
            //...
        });
        //fun_cancelSendingMediaMessage_call
        */
    
        /*
        //fun_downloadMediaMessage_call
        public class RCIMDownloadMediaMessageListenerProxy : RCIMDownloadMediaMessageListener
        {
            public void OnMediaMessageDownloading(RCIMMediaMessage message,int progress)
            {
                //...
            }
            public void OnDownloadingMediaMessageCanceled(RCIMMediaMessage message)
            {
                //...
            }
            public void OnMediaMessageDownloaded(int code,RCIMMediaMessage message)
            {
                //...
            }
        }
        int ret = engine.DownloadMediaMessage(message,new RCIMDownloadMediaMessageListenerProxy());
        //fun_downloadMediaMessage_call
        */
    
        /*
        //fun_cancelDownloadingMediaMessage_call
        int ret = engine.CancelDownloadingMediaMessage(message,(code,message) =>
        {
            //...
        });
        //fun_cancelDownloadingMediaMessage_call
        */
    
        /*
        //fun_loadConversation_call
        int ret = engine.LoadConversation(type,targetId,channelId);
        //fun_loadConversation_call
        */
    
        public bool loadConversation(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            int ret = engine.LoadConversation(type, targetId, channelId);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "loadConversation";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getConversation_call
        public class RCIMGetConversationListenerProxy : RCIMGetConversationListener
        {
            public void OnSuccess(RCIMConversation t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetConversation(type,targetId,channelId,new RCIMGetConversationListenerProxy());
        //fun_getConversation_call
        */
    
        public bool getConversation(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetConversationListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetConversationListenerProxy(ResultHandle);
            }
            int ret = engine.GetConversation(type, targetId, channelId, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getConversation";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_loadConversations_call
        int ret = engine.LoadConversations(conversationTypes,channelId,startTime,count);
        //fun_loadConversations_call
        */
    
        public bool loadConversations(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("conversationTypes"))
            {
                ExampleUtils.ShowToast("conversationTypes 为空");
                return false;
            }
            string[] conversationTypes_strs = ((string)arg["conversationTypes"]).Split(',');
            List<RCIMConversationType> conversationTypes = new List<RCIMConversationType>();
            foreach (var item in conversationTypes_strs)
            {
                conversationTypes.Add((RCIMConversationType) int.Parse(item));
            }
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("startTime"))
            {
                ExampleUtils.ShowToast("startTime 为空");
                return false;
            }
            long startTime = long.Parse((string)arg["startTime"]);
            if (!arg.ContainsKey("count"))
            {
                ExampleUtils.ShowToast("count 为空");
                return false;
            }
            int count = int.Parse((string)arg["count"]);
            int ret = engine.LoadConversations(conversationTypes, channelId, startTime, count);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "loadConversations";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getConversations_call
        public class RCIMGetConversationsListenerProxy : RCIMGetConversationsListener
        {
            public void OnSuccess(List<RCIMConversation> t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetConversations(conversationTypes,channelId,startTime,count,new
        RCIMGetConversationsListenerProxy());
        //fun_getConversations_call
        */
    
        public bool getConversations(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("conversationTypes"))
            {
                ExampleUtils.ShowToast("conversationTypes 为空");
                return false;
            }
            string[] conversationTypes_strs = ((string)arg["conversationTypes"]).Split(',');
            List<RCIMConversationType> conversationTypes = new List<RCIMConversationType>();
            foreach (var item in conversationTypes_strs)
            {
                conversationTypes.Add((RCIMConversationType) int.Parse(item));
            }
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("startTime"))
            {
                ExampleUtils.ShowToast("startTime 为空");
                return false;
            }
            long startTime = long.Parse((string)arg["startTime"]);
            if (!arg.ContainsKey("count"))
            {
                ExampleUtils.ShowToast("count 为空");
                return false;
            }
            int count = int.Parse((string)arg["count"]);
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetConversationsListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetConversationsListenerProxy(ResultHandle);
            }
            int ret = engine.GetConversations(conversationTypes, channelId, startTime, count, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getConversations";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_removeConversation_call
        int ret = engine.RemoveConversation(type,targetId,channelId,(code) =>
        {
            //...
        });
        //fun_removeConversation_call
        */
    
        public bool removeConversation(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnConversationRemovedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWRemoveConversationCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.RemoveConversation(type, targetId, channelId, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "removeConversation";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_removeConversations_call
        int ret = engine.RemoveConversations(conversationTypes,channelId,(code) =>
        {
            //...
        });
        //fun_removeConversations_call
        */
    
        public bool removeConversations(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("conversationTypes"))
            {
                ExampleUtils.ShowToast("conversationTypes 为空");
                return false;
            }
            string[] conversationTypes_strs = ((string)arg["conversationTypes"]).Split(',');
            List<RCIMConversationType> conversationTypes = new List<RCIMConversationType>();
            foreach (var item in conversationTypes_strs)
            {
                conversationTypes.Add((RCIMConversationType) int.Parse(item));
            }
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnConversationsRemovedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWRemoveConversationsCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.RemoveConversations(conversationTypes, channelId, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "removeConversations";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_loadUnreadCount_call
        int ret = engine.LoadUnreadCount(type,targetId,channelId);
        //fun_loadUnreadCount_call
        */
    
        public bool loadUnreadCount(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            int ret = engine.LoadUnreadCount(type, targetId, channelId);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "loadUnreadCount";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getUnreadCount_call
        public class RCIMGetUnreadCountListenerProxy : RCIMGetUnreadCountListener
        {
            public void OnSuccess(int t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetUnreadCount(type,targetId,channelId,new RCIMGetUnreadCountListenerProxy());
        //fun_getUnreadCount_call
        */
    
        public bool getUnreadCount(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetUnreadCountListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetUnreadCountListenerProxy(ResultHandle);
            }
            int ret = engine.GetUnreadCount(type, targetId, channelId, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getUnreadCount";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_loadTotalUnreadCount_call
        int ret = engine.LoadTotalUnreadCount(channelId);
        //fun_loadTotalUnreadCount_call
        */
    
        public bool loadTotalUnreadCount(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            int ret = engine.LoadTotalUnreadCount(channelId);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "loadTotalUnreadCount";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getTotalUnreadCount_call
        public class RCIMGetTotalUnreadCountListenerProxy : RCIMGetTotalUnreadCountListener
        {
            public void OnSuccess(int t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetTotalUnreadCount(channelId,new RCIMGetTotalUnreadCountListenerProxy());
        //fun_getTotalUnreadCount_call
        */
    
        public bool getTotalUnreadCount(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetTotalUnreadCountListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetTotalUnreadCountListenerProxy(ResultHandle);
            }
            int ret = engine.GetTotalUnreadCount(channelId, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getTotalUnreadCount";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_loadUnreadMentionedCount_call
        int ret = engine.LoadUnreadMentionedCount(type,targetId,channelId);
        //fun_loadUnreadMentionedCount_call
        */
    
        public bool loadUnreadMentionedCount(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            int ret = engine.LoadUnreadMentionedCount(type, targetId, channelId);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "loadUnreadMentionedCount";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getUnreadMentionedCount_call
        public class RCIMGetUnreadMentionedCountListenerProxy : RCIMGetUnreadMentionedCountListener
        {
            public void OnSuccess(int t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetUnreadMentionedCount(type,targetId,channelId,new RCIMGetUnreadMentionedCountListenerProxy());
        //fun_getUnreadMentionedCount_call
        */
    
        public bool getUnreadMentionedCount(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetUnreadMentionedCountListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetUnreadMentionedCountListenerProxy(ResultHandle);
            }
            int ret = engine.GetUnreadMentionedCount(type, targetId, channelId, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getUnreadMentionedCount";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_loadUltraGroupAllUnreadCount_call
        int ret = engine.LoadUltraGroupAllUnreadCount();
        //fun_loadUltraGroupAllUnreadCount_call
        */
    
        public bool loadUltraGroupAllUnreadCount()
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            int ret = engine.LoadUltraGroupAllUnreadCount();
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "loadUltraGroupAllUnreadCount";
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getUltraGroupAllUnreadCount_call
        public class RCIMGetUltraGroupAllUnreadCountListenerProxy : RCIMGetUltraGroupAllUnreadCountListener
        {
            public void OnSuccess(int t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetUltraGroupAllUnreadCount(new RCIMGetUltraGroupAllUnreadCountListenerProxy());
        //fun_getUltraGroupAllUnreadCount_call
        */
    
        public bool getUltraGroupAllUnreadCount(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetUltraGroupAllUnreadCountListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetUltraGroupAllUnreadCountListenerProxy(ResultHandle);
            }
            int ret = engine.GetUltraGroupAllUnreadCount(callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getUltraGroupAllUnreadCount";
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_loadUltraGroupAllUnreadMentionedCount_call
        int ret = engine.LoadUltraGroupAllUnreadMentionedCount();
        //fun_loadUltraGroupAllUnreadMentionedCount_call
        */
    
        public bool loadUltraGroupAllUnreadMentionedCount()
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            int ret = engine.LoadUltraGroupAllUnreadMentionedCount();
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "loadUltraGroupAllUnreadMentionedCount";
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getUltraGroupAllUnreadMentionedCount_call
        public class RCIMGetUltraGroupAllUnreadMentionedCountListenerProxy :
        RCIMGetUltraGroupAllUnreadMentionedCountListener
        {
            public void OnSuccess(int t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetUltraGroupAllUnreadMentionedCount(new RCIMGetUltraGroupAllUnreadMentionedCountListenerProxy());
        //fun_getUltraGroupAllUnreadMentionedCount_call
        */
    
        public bool getUltraGroupAllUnreadMentionedCount(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetUltraGroupAllUnreadMentionedCountListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetUltraGroupAllUnreadMentionedCountListenerProxy(ResultHandle);
            }
            int ret = engine.GetUltraGroupAllUnreadMentionedCount(callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getUltraGroupAllUnreadMentionedCount";
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_loadUltraGroupUnreadCount_call
        int ret = engine.LoadUltraGroupUnreadCount(targetId);
        //fun_loadUltraGroupUnreadCount_call
        */
    
        public bool loadUltraGroupUnreadCount(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            int ret = engine.LoadUltraGroupUnreadCount(targetId);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "loadUltraGroupUnreadCount";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getUltraGroupUnreadCount_call
        public class RCIMGetUltraGroupUnreadCountListenerProxy : RCIMGetUltraGroupUnreadCountListener
        {
            public void OnSuccess(int t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetUltraGroupUnreadCount(targetId,new RCIMGetUltraGroupUnreadCountListenerProxy());
        //fun_getUltraGroupUnreadCount_call
        */
    
        public bool getUltraGroupUnreadCount(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetUltraGroupUnreadCountListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetUltraGroupUnreadCountListenerProxy(ResultHandle);
            }
            int ret = engine.GetUltraGroupUnreadCount(targetId, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getUltraGroupUnreadCount";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_loadUltraGroupUnreadMentionedCount_call
        int ret = engine.LoadUltraGroupUnreadMentionedCount(targetId);
        //fun_loadUltraGroupUnreadMentionedCount_call
        */
    
        public bool loadUltraGroupUnreadMentionedCount(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            int ret = engine.LoadUltraGroupUnreadMentionedCount(targetId);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "loadUltraGroupUnreadMentionedCount";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getUltraGroupUnreadMentionedCount_call
        public class RCIMGetUltraGroupUnreadMentionedCountListenerProxy : RCIMGetUltraGroupUnreadMentionedCountListener
        {
            public void OnSuccess(int t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetUltraGroupUnreadMentionedCount(targetId,new
        RCIMGetUltraGroupUnreadMentionedCountListenerProxy());
        //fun_getUltraGroupUnreadMentionedCount_call
        */
    
        public bool getUltraGroupUnreadMentionedCount(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetUltraGroupUnreadMentionedCountListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetUltraGroupUnreadMentionedCountListenerProxy(ResultHandle);
            }
            int ret = engine.GetUltraGroupUnreadMentionedCount(targetId, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getUltraGroupUnreadMentionedCount";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_loadUnreadCountByConversationTypes_call
        int ret = engine.LoadUnreadCountByConversationTypes(conversationTypes,channelId,contain);
        //fun_loadUnreadCountByConversationTypes_call
        */
    
        public bool loadUnreadCountByConversationTypes(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("conversationTypes"))
            {
                ExampleUtils.ShowToast("conversationTypes 为空");
                return false;
            }
            string[] conversationTypes_strs = ((string)arg["conversationTypes"]).Split(',');
            List<RCIMConversationType> conversationTypes = new List<RCIMConversationType>();
            foreach (var item in conversationTypes_strs)
            {
                conversationTypes.Add((RCIMConversationType) int.Parse(item));
            }
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("contain"))
            {
                ExampleUtils.ShowToast("contain 为空");
                return false;
            }
            bool contain = int.Parse((string)arg["contain"]) != 0;
            int ret = engine.LoadUnreadCountByConversationTypes(conversationTypes, channelId, contain);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "loadUnreadCountByConversationTypes";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getUnreadCountByConversationTypes_call
        public class RCIMGetUnreadCountByConversationTypesListenerProxy : RCIMGetUnreadCountByConversationTypesListener
        {
            public void OnSuccess(int t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetUnreadCountByConversationTypes(conversationTypes,channelId,contain,new
        RCIMGetUnreadCountByConversationTypesListenerProxy());
        //fun_getUnreadCountByConversationTypes_call
        */
    
        public bool getUnreadCountByConversationTypes(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("conversationTypes"))
            {
                ExampleUtils.ShowToast("conversationTypes 为空");
                return false;
            }
            string[] conversationTypes_strs = ((string)arg["conversationTypes"]).Split(',');
            List<RCIMConversationType> conversationTypes = new List<RCIMConversationType>();
            foreach (var item in conversationTypes_strs)
            {
                conversationTypes.Add((RCIMConversationType) int.Parse(item));
            }
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("contain"))
            {
                ExampleUtils.ShowToast("contain 为空");
                return false;
            }
            bool contain = int.Parse((string)arg["contain"]) != 0;
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetUnreadCountByConversationTypesListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetUnreadCountByConversationTypesListenerProxy(ResultHandle);
            }
            int ret = engine.GetUnreadCountByConversationTypes(conversationTypes, channelId, contain, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getUnreadCountByConversationTypes";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_clearUnreadCount_call
        int ret = engine.ClearUnreadCount(type,targetId,channelId,timestamp,(code) =>
        {
            //...
        });
        //fun_clearUnreadCount_call
        */
    
        public bool clearUnreadCount(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("timestamp"))
            {
                ExampleUtils.ShowToast("timestamp 为空");
                return false;
            }
            long timestamp = long.Parse((string)arg["timestamp"]);
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnUnreadCountClearedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWClearUnreadCountCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.ClearUnreadCount(type, targetId, channelId, timestamp, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "clearUnreadCount";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_saveDraftMessage_call
        int ret = engine.SaveDraftMessage(type,targetId,channelId,draft,(code) =>
        {
            //...
        });
        //fun_saveDraftMessage_call
        */
    
        public bool saveDraftMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("draft"))
            {
                ExampleUtils.ShowToast("draft 为空");
                return false;
            }
            string draft = (string)arg["draft"];
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnDraftMessageSavedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWSaveDraftMessageCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.SaveDraftMessage(type, targetId, channelId, draft, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "saveDraftMessage";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_loadDraftMessage_call
        int ret = engine.LoadDraftMessage(type,targetId,channelId);
        //fun_loadDraftMessage_call
        */
    
        public bool loadDraftMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            int ret = engine.LoadDraftMessage(type, targetId, channelId);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "loadDraftMessage";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getDraftMessage_call
        public class RCIMGetDraftMessageListenerProxy : RCIMGetDraftMessageListener
        {
            public void OnSuccess(string t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetDraftMessage(type,targetId,channelId,new RCIMGetDraftMessageListenerProxy());
        //fun_getDraftMessage_call
        */
    
        public bool getDraftMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetDraftMessageListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetDraftMessageListenerProxy(ResultHandle);
            }
            int ret = engine.GetDraftMessage(type, targetId, channelId, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getDraftMessage";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_clearDraftMessage_call
        int ret = engine.ClearDraftMessage(type,targetId,channelId,(code) =>
        {
            //...
        });
        //fun_clearDraftMessage_call
        */
    
        public bool clearDraftMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnDraftMessageClearedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWClearDraftMessageCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.ClearDraftMessage(type, targetId, channelId, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "clearDraftMessage";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_loadBlockedConversations_call
        int ret = engine.LoadBlockedConversations(conversationTypes,channelId);
        //fun_loadBlockedConversations_call
        */
    
        public bool loadBlockedConversations(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("conversationTypes"))
            {
                ExampleUtils.ShowToast("conversationTypes 为空");
                return false;
            }
            string[] conversationTypes_strs = ((string)arg["conversationTypes"]).Split(',');
            List<RCIMConversationType> conversationTypes = new List<RCIMConversationType>();
            foreach (var item in conversationTypes_strs)
            {
                conversationTypes.Add((RCIMConversationType) int.Parse(item));
            }
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            int ret = engine.LoadBlockedConversations(conversationTypes, channelId);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "loadBlockedConversations";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getBlockedConversations_call
        public class RCIMGetBlockedConversationsListenerProxy : RCIMGetBlockedConversationsListener
        {
            public void OnSuccess(List<RCIMConversation> t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetBlockedConversations(conversationTypes,channelId,new
        RCIMGetBlockedConversationsListenerProxy());
        //fun_getBlockedConversations_call
        */
    
        public bool getBlockedConversations(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("conversationTypes"))
            {
                ExampleUtils.ShowToast("conversationTypes 为空");
                return false;
            }
            string[] conversationTypes_strs = ((string)arg["conversationTypes"]).Split(',');
            List<RCIMConversationType> conversationTypes = new List<RCIMConversationType>();
            foreach (var item in conversationTypes_strs)
            {
                conversationTypes.Add((RCIMConversationType) int.Parse(item));
            }
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetBlockedConversationsListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetBlockedConversationsListenerProxy(ResultHandle);
            }
            int ret = engine.GetBlockedConversations(conversationTypes, channelId, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getBlockedConversations";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_changeConversationTopStatus_call
        int ret = engine.ChangeConversationTopStatus(type,targetId,channelId,top,(code) =>
        {
            //...
        });
        //fun_changeConversationTopStatus_call
        */
    
        public bool changeConversationTopStatus(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("top"))
            {
                ExampleUtils.ShowToast("top 为空");
                return false;
            }
            bool top = int.Parse((string)arg["top"]) != 0;
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnConversationTopStatusChangedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWChangeConversationTopStatusCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.ChangeConversationTopStatus(type, targetId, channelId, top, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "changeConversationTopStatus";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_loadConversationTopStatus_call
        int ret = engine.LoadConversationTopStatus(type,targetId,channelId);
        //fun_loadConversationTopStatus_call
        */
    
        public bool loadConversationTopStatus(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            int ret = engine.LoadConversationTopStatus(type, targetId, channelId);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "loadConversationTopStatus";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getConversationTopStatus_call
        public class RCIMGetConversationTopStatusListenerProxy : RCIMGetConversationTopStatusListener
        {
            public void OnSuccess(Boolean t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetConversationTopStatus(type,targetId,channelId,new RCIMGetConversationTopStatusListenerProxy());
        //fun_getConversationTopStatus_call
        */
    
        public bool getConversationTopStatus(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetConversationTopStatusListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetConversationTopStatusListenerProxy(ResultHandle);
            }
            int ret = engine.GetConversationTopStatus(type, targetId, channelId, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getConversationTopStatus";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_syncConversationReadStatus_call
        int ret = engine.SyncConversationReadStatus(type,targetId,channelId,timestamp,(code) =>
        {
            //...
        });
        //fun_syncConversationReadStatus_call
        */
    
        public bool syncConversationReadStatus(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("timestamp"))
            {
                ExampleUtils.ShowToast("timestamp 为空");
                return false;
            }
            long timestamp = long.Parse((string)arg["timestamp"]);
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnConversationReadStatusSyncedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWSyncConversationReadStatusCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.SyncConversationReadStatus(type, targetId, channelId, timestamp, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "syncConversationReadStatus";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_sendTypingStatus_call
        int ret = engine.SendTypingStatus(type,targetId,channelId,currentType);
        //fun_sendTypingStatus_call
        */
    
        public bool sendTypingStatus(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("currentType"))
            {
                ExampleUtils.ShowToast("currentType 为空");
                return false;
            }
            string currentType = (string)arg["currentType"];
            int ret = engine.SendTypingStatus(type, targetId, channelId, currentType);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "sendTypingStatus";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_loadMessages_call
        int ret = engine.LoadMessages(type,targetId,channelId,sentTime,order,policy,count);
        //fun_loadMessages_call
        */
    
        public bool loadMessages(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("sentTime"))
            {
                ExampleUtils.ShowToast("sentTime 为空");
                return false;
            }
            long sentTime = long.Parse((string)arg["sentTime"]);
            if (!arg.ContainsKey("order"))
            {
                ExampleUtils.ShowToast("order 为空");
                return false;
            }
            RCIMTimeOrder order = (RCIMTimeOrder) int.Parse((string)arg["order"]);
            if (!arg.ContainsKey("policy"))
            {
                ExampleUtils.ShowToast("policy 为空");
                return false;
            }
            RCIMMessageOperationPolicy policy = (RCIMMessageOperationPolicy) int.Parse((string)arg["policy"]);
            if (!arg.ContainsKey("count"))
            {
                ExampleUtils.ShowToast("count 为空");
                return false;
            }
            int count = int.Parse((string)arg["count"]);
            int ret = engine.LoadMessages(type, targetId, channelId, sentTime, order, policy, count);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "loadMessages";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getMessages_call
        public class RCIMGetMessagesListenerProxy : RCIMGetMessagesListener
        {
            public void OnSuccess(List<RCIMMessage> t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetMessages(type,targetId,channelId,sentTime,order,policy,count,new
        RCIMGetMessagesListenerProxy());
        //fun_getMessages_call
        */
    
        public bool getMessages(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("sentTime"))
            {
                ExampleUtils.ShowToast("sentTime 为空");
                return false;
            }
            long sentTime = long.Parse((string)arg["sentTime"]);
            if (!arg.ContainsKey("order"))
            {
                ExampleUtils.ShowToast("order 为空");
                return false;
            }
            RCIMTimeOrder order = (RCIMTimeOrder) int.Parse((string)arg["order"]);
            if (!arg.ContainsKey("policy"))
            {
                ExampleUtils.ShowToast("policy 为空");
                return false;
            }
            RCIMMessageOperationPolicy policy = (RCIMMessageOperationPolicy) int.Parse((string)arg["policy"]);
            if (!arg.ContainsKey("count"))
            {
                ExampleUtils.ShowToast("count 为空");
                return false;
            }
            int count = int.Parse((string)arg["count"]);
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetMessagesListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetMessagesListenerProxy(ResultHandle);
            }
            int ret = engine.GetMessages(type, targetId, channelId, sentTime, order, policy, count, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getMessages";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getMessageById_call
        public class RCIMGetMessageListenerProxy : RCIMGetMessageListener
        {
            public void OnSuccess(RCIMMessage t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetMessageById(messageId,new RCIMGetMessageListenerProxy());
        //fun_getMessageById_call
        */
    
        public bool getMessageById(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("messageId"))
            {
                ExampleUtils.ShowToast("messageId 为空");
                return false;
            }
            int messageId = int.Parse((string)arg["messageId"]);
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetMessageListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetMessageListenerProxy(ResultHandle);
            }
            int ret = engine.GetMessageById(messageId, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getMessageById";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getMessageByUId_call
        public class RCIMGetMessageListenerProxy : RCIMGetMessageListener
        {
            public void OnSuccess(RCIMMessage t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetMessageByUId(messageUId,new RCIMGetMessageListenerProxy());
        //fun_getMessageByUId_call
        */
    
        public bool getMessageByUId(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("messageUId"))
            {
                ExampleUtils.ShowToast("messageUId 为空");
                return false;
            }
            string messageUId = (string)arg["messageUId"];
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetMessageListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetMessageListenerProxy(ResultHandle);
            }
            int ret = engine.GetMessageByUId(messageUId, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getMessageByUId";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_loadFirstUnreadMessage_call
        int ret = engine.LoadFirstUnreadMessage(type,targetId,channelId);
        //fun_loadFirstUnreadMessage_call
        */
    
        public bool loadFirstUnreadMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            int ret = engine.LoadFirstUnreadMessage(type, targetId, channelId);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "loadFirstUnreadMessage";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getFirstUnreadMessage_call
        public class RCIMGetFirstUnreadMessageListenerProxy : RCIMGetFirstUnreadMessageListener
        {
            public void OnSuccess(RCIMMessage t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetFirstUnreadMessage(type,targetId,channelId,new RCIMGetFirstUnreadMessageListenerProxy());
        //fun_getFirstUnreadMessage_call
        */
    
        public bool getFirstUnreadMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetFirstUnreadMessageListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetFirstUnreadMessageListenerProxy(ResultHandle);
            }
            int ret = engine.GetFirstUnreadMessage(type, targetId, channelId, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getFirstUnreadMessage";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_loadUnreadMentionedMessages_call
        int ret = engine.LoadUnreadMentionedMessages(type,targetId,channelId);
        //fun_loadUnreadMentionedMessages_call
        */
    
        public bool loadUnreadMentionedMessages(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            int ret = engine.LoadUnreadMentionedMessages(type, targetId, channelId);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "loadUnreadMentionedMessages";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getUnreadMentionedMessages_call
        public class RCIMGetUnreadMentionedMessagesListenerProxy : RCIMGetUnreadMentionedMessagesListener
        {
            public void OnSuccess(List<RCIMMessage> t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetUnreadMentionedMessages(type,targetId,channelId,new
        RCIMGetUnreadMentionedMessagesListenerProxy());
        //fun_getUnreadMentionedMessages_call
        */
    
        public bool getUnreadMentionedMessages(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetUnreadMentionedMessagesListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetUnreadMentionedMessagesListenerProxy(ResultHandle);
            }
            int ret = engine.GetUnreadMentionedMessages(type, targetId, channelId, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getUnreadMentionedMessages";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_insertMessage_call
        int ret = engine.InsertMessage(message,(code,message) =>
        {
            //...
        });
        //fun_insertMessage_call
        */
    
        /*
        //fun_insertMessages_call
        int ret = engine.InsertMessages(messages,(code,messages) =>
        {
            //...
        });
        //fun_insertMessages_call
        */
    
        /*
        //fun_clearMessages_call
        int ret = engine.ClearMessages(type,targetId,channelId,timestamp,policy,(code) =>
        {
            //...
        });
        //fun_clearMessages_call
        */
    
        public bool clearMessages(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("timestamp"))
            {
                ExampleUtils.ShowToast("timestamp 为空");
                return false;
            }
            long timestamp = long.Parse((string)arg["timestamp"]);
            if (!arg.ContainsKey("policy"))
            {
                ExampleUtils.ShowToast("policy 为空");
                return false;
            }
            RCIMMessageOperationPolicy policy = (RCIMMessageOperationPolicy) int.Parse((string)arg["policy"]);
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnMessagesClearedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWClearMessagesCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.ClearMessages(type, targetId, channelId, timestamp, policy, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "clearMessages";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_deleteLocalMessages_call
        int ret = engine.DeleteLocalMessages(messages,(code,messages) =>
        {
            //...
        });
        //fun_deleteLocalMessages_call
        */
    
        /*
        //fun_deleteMessages_call
        int ret = engine.DeleteMessages(type,targetId,channelId,messages,(code,messages) =>
        {
            //...
        });
        //fun_deleteMessages_call
        */
    
        /*
        //fun_recallMessage_call
        int ret = engine.RecallMessage(message,(code,message) =>
        {
            //...
        });
        //fun_recallMessage_call
        */
    
        /*
        //fun_sendPrivateReadReceiptMessage_call
        int ret = engine.SendPrivateReadReceiptMessage(targetId,channelId,timestamp,(code) =>
        {
            //...
        });
        //fun_sendPrivateReadReceiptMessage_call
        */
    
        public bool sendPrivateReadReceiptMessage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("timestamp"))
            {
                ExampleUtils.ShowToast("timestamp 为空");
                return false;
            }
            long timestamp = long.Parse((string)arg["timestamp"]);
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnPrivateReadReceiptMessageSentAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWSendPrivateReadReceiptMessageCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.SendPrivateReadReceiptMessage(targetId, channelId, timestamp, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "sendPrivateReadReceiptMessage";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_sendGroupReadReceiptRequest_call
        int ret = engine.SendGroupReadReceiptRequest(message,(code,message) =>
        {
            //...
        });
        //fun_sendGroupReadReceiptRequest_call
        */
    
        /*
        //fun_sendGroupReadReceiptResponse_call
        int ret = engine.SendGroupReadReceiptResponse(targetId,channelId,messages,(code,message) =>
        {
            //...
        });
        //fun_sendGroupReadReceiptResponse_call
        */
    
        /*
        //fun_updateMessageExpansion_call
        int ret = engine.UpdateMessageExpansion(messageUId,expansion,(code) =>
        {
            //...
        });
        //fun_updateMessageExpansion_call
        */
    
        public bool updateMessageExpansion(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("messageUId"))
            {
                ExampleUtils.ShowToast("messageUId 为空");
                return false;
            }
            string messageUId = (string)arg["messageUId"];
            Dictionary<string, string> expansion = new Dictionary<string, string>();
            string[] keys = ExampleUtils.GetValue(arg, "keys", "").Split(',');
            string[] values = ExampleUtils.GetValue(arg, "values", "").Split(',');
            for (int i = 0; i < keys.Length; i++)
            {
                if (i < values.Length)
                {
                    expansion.Add(keys[i], values[i]);
                }
            }
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnMessageExpansionUpdatedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWUpdateMessageExpansionCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.UpdateMessageExpansion(messageUId, expansion, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "updateMessageExpansion";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_removeMessageExpansionForKeys_call
        int ret = engine.RemoveMessageExpansionForKeys(messageUId,keys,(code) =>
        {
            //...
        });
        //fun_removeMessageExpansionForKeys_call
        */
    
        public bool removeMessageExpansionForKeys(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("messageUId"))
            {
                ExampleUtils.ShowToast("messageUId 为空");
                return false;
            }
            string messageUId = (string)arg["messageUId"];
            if (!arg.ContainsKey("keys"))
            {
                ExampleUtils.ShowToast("keys 为空");
                return false;
            }
            string[] keys_strs = ((string)arg["keys"]).Split(',');
            List<String> keys = new List<String>(keys_strs);
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnMessageExpansionForKeysRemovedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWRemoveMessageExpansionForKeysCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.RemoveMessageExpansionForKeys(messageUId, keys, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "removeMessageExpansionForKeys";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_changeMessageSentStatus_call
        int ret = engine.ChangeMessageSentStatus(messageId,sentStatus,(code) =>
        {
            //...
        });
        //fun_changeMessageSentStatus_call
        */
    
        public bool changeMessageSentStatus(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("messageId"))
            {
                ExampleUtils.ShowToast("messageId 为空");
                return false;
            }
            int messageId = int.Parse((string)arg["messageId"]);
            if (!arg.ContainsKey("sentStatus"))
            {
                ExampleUtils.ShowToast("sentStatus 为空");
                return false;
            }
            RCIMSentStatus sentStatus = (RCIMSentStatus) int.Parse((string)arg["sentStatus"]);
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnMessageSentStatusChangedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWChangeMessageSentStatusCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.ChangeMessageSentStatus(messageId, sentStatus, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "changeMessageSentStatus";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_changeMessageReceiveStatus_call
        int ret = engine.ChangeMessageReceiveStatus(messageId,receivedStatus,(code) =>
        {
            //...
        });
        //fun_changeMessageReceiveStatus_call
        */
    
        public bool changeMessageReceiveStatus(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("messageId"))
            {
                ExampleUtils.ShowToast("messageId 为空");
                return false;
            }
            int messageId = int.Parse((string)arg["messageId"]);
            if (!arg.ContainsKey("receivedStatus"))
            {
                ExampleUtils.ShowToast("receivedStatus 为空");
                return false;
            }
            RCIMReceivedStatus receivedStatus = (RCIMReceivedStatus) int.Parse((string)arg["receivedStatus"]);
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnMessageReceiveStatusChangedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWChangeMessageReceivedStatusCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.ChangeMessageReceiveStatus(messageId, receivedStatus, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "changeMessageReceiveStatus";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_joinChatRoom_call
        int ret = engine.JoinChatRoom(targetId,messageCount,autoCreate,(code,targetId) =>
        {
            //...
        });
        //fun_joinChatRoom_call
        */
    
        public bool joinChatRoom(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            if (!arg.ContainsKey("messageCount"))
            {
                ExampleUtils.ShowToast("messageCount 为空");
                return false;
            }
            int messageCount = int.Parse((string)arg["messageCount"]);
            if (!arg.ContainsKey("autoCreate"))
            {
                ExampleUtils.ShowToast("autoCreate 为空");
                return false;
            }
            bool autoCreate = int.Parse((string)arg["autoCreate"]) != 0;
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnChatRoomJoinedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code, targetId) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWJoinChatRoomCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    result["targetId"] = targetId;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.JoinChatRoom(targetId, messageCount, autoCreate, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "joinChatRoom";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_leaveChatRoom_call
        int ret = engine.LeaveChatRoom(targetId,(code,targetId) =>
        {
            //...
        });
        //fun_leaveChatRoom_call
        */
    
        public bool leaveChatRoom(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnChatRoomLeftAction callback = null;
            if (useCallback != "0")
            {
                callback = (code, targetId) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWLeaveChatRoomCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    result["targetId"] = targetId;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.LeaveChatRoom(targetId, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "leaveChatRoom";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_loadChatRoomMessages_call
        int ret = engine.LoadChatRoomMessages(targetId,timestamp,order,count);
        //fun_loadChatRoomMessages_call
        */
    
        public bool loadChatRoomMessages(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            if (!arg.ContainsKey("timestamp"))
            {
                ExampleUtils.ShowToast("timestamp 为空");
                return false;
            }
            long timestamp = long.Parse((string)arg["timestamp"]);
            if (!arg.ContainsKey("order"))
            {
                ExampleUtils.ShowToast("order 为空");
                return false;
            }
            RCIMTimeOrder order = (RCIMTimeOrder) int.Parse((string)arg["order"]);
            if (!arg.ContainsKey("count"))
            {
                ExampleUtils.ShowToast("count 为空");
                return false;
            }
            int count = int.Parse((string)arg["count"]);
            int ret = engine.LoadChatRoomMessages(targetId, timestamp, order, count);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "loadChatRoomMessages";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getChatRoomMessages_call
        public class RCIMGetChatRoomMessagesListenerProxy : RCIMGetChatRoomMessagesListener
        {
            public void OnSuccess(List<RCIMMessage> t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetChatRoomMessages(targetId,timestamp,order,count,new RCIMGetChatRoomMessagesListenerProxy());
        //fun_getChatRoomMessages_call
        */
    
        public bool getChatRoomMessages(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            if (!arg.ContainsKey("timestamp"))
            {
                ExampleUtils.ShowToast("timestamp 为空");
                return false;
            }
            long timestamp = long.Parse((string)arg["timestamp"]);
            if (!arg.ContainsKey("order"))
            {
                ExampleUtils.ShowToast("order 为空");
                return false;
            }
            RCIMTimeOrder order = (RCIMTimeOrder) int.Parse((string)arg["order"]);
            if (!arg.ContainsKey("count"))
            {
                ExampleUtils.ShowToast("count 为空");
                return false;
            }
            int count = int.Parse((string)arg["count"]);
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetChatRoomMessagesListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetChatRoomMessagesListenerProxy(ResultHandle);
            }
            int ret = engine.GetChatRoomMessages(targetId, timestamp, order, count, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getChatRoomMessages";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_addChatRoomEntry_call
        int ret = engine.AddChatRoomEntry(targetId,key,value,deleteWhenLeft,overwrite,(code) =>
        {
            //...
        });
        //fun_addChatRoomEntry_call
        */
    
        public bool addChatRoomEntry(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            if (!arg.ContainsKey("key"))
            {
                ExampleUtils.ShowToast("key 为空");
                return false;
            }
            string key = (string)arg["key"];
            if (!arg.ContainsKey("value"))
            {
                ExampleUtils.ShowToast("value 为空");
                return false;
            }
            string value = (string)arg["value"];
            if (!arg.ContainsKey("deleteWhenLeft"))
            {
                ExampleUtils.ShowToast("deleteWhenLeft 为空");
                return false;
            }
            bool deleteWhenLeft = int.Parse((string)arg["deleteWhenLeft"]) != 0;
            if (!arg.ContainsKey("overwrite"))
            {
                ExampleUtils.ShowToast("overwrite 为空");
                return false;
            }
            bool overwrite = int.Parse((string)arg["overwrite"]) != 0;
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnChatRoomEntryAddedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWAddChatRoomEntryCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.AddChatRoomEntry(targetId, key, value, deleteWhenLeft, overwrite, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "addChatRoomEntry";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_addChatRoomEntries_call
        int ret = engine.AddChatRoomEntries(targetId,entries,deleteWhenLeft,overwrite,(code,errors) =>
        {
            //...
        });
        //fun_addChatRoomEntries_call
        */
    
        public bool addChatRoomEntries(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            Dictionary<string, string> entries = new Dictionary<string, string>();
            string[] keys = ExampleUtils.GetValue(arg, "keys", "").Split(',');
            string[] values = ExampleUtils.GetValue(arg, "values", "").Split(',');
            for (int i = 0; i < keys.Length; i++)
            {
                if (i < values.Length)
                {
                    entries.Add(keys[i], values[i]);
                }
            }
            if (!arg.ContainsKey("deleteWhenLeft"))
            {
                ExampleUtils.ShowToast("deleteWhenLeft 为空");
                return false;
            }
            bool deleteWhenLeft = int.Parse((string)arg["deleteWhenLeft"]) != 0;
            if (!arg.ContainsKey("overwrite"))
            {
                ExampleUtils.ShowToast("overwrite 为空");
                return false;
            }
            bool overwrite = int.Parse((string)arg["overwrite"]) != 0;
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnChatRoomEntriesAddedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code, errors) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWAddChatRoomEntriesCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    result["errors"] = errors;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.AddChatRoomEntries(targetId, entries, deleteWhenLeft, overwrite, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "addChatRoomEntries";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_loadChatRoomEntry_call
        int ret = engine.LoadChatRoomEntry(targetId,key);
        //fun_loadChatRoomEntry_call
        */
    
        public bool loadChatRoomEntry(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            if (!arg.ContainsKey("key"))
            {
                ExampleUtils.ShowToast("key 为空");
                return false;
            }
            string key = (string)arg["key"];
            int ret = engine.LoadChatRoomEntry(targetId, key);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "loadChatRoomEntry";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getChatRoomEntry_call
        public class RCIMGetChatRoomEntryListenerProxy : RCIMGetChatRoomEntryListener
        {
            public void OnSuccess(Dictionary<string, string> t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetChatRoomEntry(targetId,key,new RCIMGetChatRoomEntryListenerProxy());
        //fun_getChatRoomEntry_call
        */
    
        public bool getChatRoomEntry(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            if (!arg.ContainsKey("key"))
            {
                ExampleUtils.ShowToast("key 为空");
                return false;
            }
            string key = (string)arg["key"];
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetChatRoomEntryListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetChatRoomEntryListenerProxy(ResultHandle);
            }
            int ret = engine.GetChatRoomEntry(targetId, key, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getChatRoomEntry";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_loadChatRoomAllEntries_call
        int ret = engine.LoadChatRoomAllEntries(targetId);
        //fun_loadChatRoomAllEntries_call
        */
    
        public bool loadChatRoomAllEntries(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            int ret = engine.LoadChatRoomAllEntries(targetId);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "loadChatRoomAllEntries";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getChatRoomAllEntries_call
        public class RCIMGetChatRoomAllEntriesListenerProxy : RCIMGetChatRoomAllEntriesListener
        {
            public void OnSuccess(Dictionary<string, string> t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetChatRoomAllEntries(targetId,new RCIMGetChatRoomAllEntriesListenerProxy());
        //fun_getChatRoomAllEntries_call
        */
    
        public bool getChatRoomAllEntries(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetChatRoomAllEntriesListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetChatRoomAllEntriesListenerProxy(ResultHandle);
            }
            int ret = engine.GetChatRoomAllEntries(targetId, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getChatRoomAllEntries";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_removeChatRoomEntry_call
        int ret = engine.RemoveChatRoomEntry(targetId,key,force,(code) =>
        {
            //...
        });
        //fun_removeChatRoomEntry_call
        */
    
        public bool removeChatRoomEntry(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            if (!arg.ContainsKey("key"))
            {
                ExampleUtils.ShowToast("key 为空");
                return false;
            }
            string key = (string)arg["key"];
            if (!arg.ContainsKey("force"))
            {
                ExampleUtils.ShowToast("force 为空");
                return false;
            }
            bool force = int.Parse((string)arg["force"]) != 0;
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnChatRoomEntryRemovedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWRemoveChatRoomEntryCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.RemoveChatRoomEntry(targetId, key, force, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "removeChatRoomEntry";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_removeChatRoomEntries_call
        int ret = engine.RemoveChatRoomEntries(targetId,keys,force,(code) =>
        {
            //...
        });
        //fun_removeChatRoomEntries_call
        */
    
        public bool removeChatRoomEntries(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            if (!arg.ContainsKey("keys"))
            {
                ExampleUtils.ShowToast("keys 为空");
                return false;
            }
            string[] keys_strs = ((string)arg["keys"]).Split(',');
            List<String> keys = new List<String>(keys_strs);
            if (!arg.ContainsKey("force"))
            {
                ExampleUtils.ShowToast("force 为空");
                return false;
            }
            bool force = int.Parse((string)arg["force"]) != 0;
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnChatRoomEntriesRemovedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWRemoveChatRoomEntriesCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.RemoveChatRoomEntries(targetId, keys, force, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "removeChatRoomEntries";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_addToBlacklist_call
        int ret = engine.AddToBlacklist(userId,(code,userId) =>
        {
            //...
        });
        //fun_addToBlacklist_call
        */
    
        public bool addToBlacklist(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("userId"))
            {
                ExampleUtils.ShowToast("userId 为空");
                return false;
            }
            string userId = (string)arg["userId"];
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnBlacklistAddedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code, userId) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWAddToBlacklistCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    result["userId"] = userId;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.AddToBlacklist(userId, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "addToBlacklist";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_removeFromBlacklist_call
        int ret = engine.RemoveFromBlacklist(userId,(code,userId) =>
        {
            //...
        });
        //fun_removeFromBlacklist_call
        */
    
        public bool removeFromBlacklist(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("userId"))
            {
                ExampleUtils.ShowToast("userId 为空");
                return false;
            }
            string userId = (string)arg["userId"];
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnBlacklistRemovedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code, userId) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWRemoveFromBlacklistCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    result["userId"] = userId;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.RemoveFromBlacklist(userId, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "removeFromBlacklist";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_loadBlacklistStatus_call
        int ret = engine.LoadBlacklistStatus(userId);
        //fun_loadBlacklistStatus_call
        */
    
        public bool loadBlacklistStatus(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("userId"))
            {
                ExampleUtils.ShowToast("userId 为空");
                return false;
            }
            string userId = (string)arg["userId"];
            int ret = engine.LoadBlacklistStatus(userId);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "loadBlacklistStatus";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getBlacklistStatus_call
        public class RCIMGetBlacklistStatusListenerProxy : RCIMGetBlacklistStatusListener
        {
            public void OnSuccess(RCIMBlacklistStatus t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetBlacklistStatus(userId,new RCIMGetBlacklistStatusListenerProxy());
        //fun_getBlacklistStatus_call
        */
    
        public bool getBlacklistStatus(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("userId"))
            {
                ExampleUtils.ShowToast("userId 为空");
                return false;
            }
            string userId = (string)arg["userId"];
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetBlacklistStatusListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetBlacklistStatusListenerProxy(ResultHandle);
            }
            int ret = engine.GetBlacklistStatus(userId, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getBlacklistStatus";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_loadBlacklist_call
        int ret = engine.LoadBlacklist();
        //fun_loadBlacklist_call
        */
    
        public bool loadBlacklist()
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            int ret = engine.LoadBlacklist();
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "loadBlacklist";
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getBlacklist_call
        public class RCIMGetBlacklistListenerProxy : RCIMGetBlacklistListener
        {
            public void OnSuccess(List<string> t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetBlacklist(new RCIMGetBlacklistListenerProxy());
        //fun_getBlacklist_call
        */
    
        public bool getBlacklist(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetBlacklistListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetBlacklistListenerProxy(ResultHandle);
            }
            int ret = engine.GetBlacklist(callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getBlacklist";
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_searchMessages_call
        public class RCIMSearchMessagesListenerProxy : RCIMSearchMessagesListener
        {
            public void OnSuccess(List<RCIMMessage> t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.SearchMessages(type,targetId,channelId,keyword,startTime,count,new
        RCIMSearchMessagesListenerProxy());
        //fun_searchMessages_call
        */
    
        public bool searchMessages(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("keyword"))
            {
                ExampleUtils.ShowToast("keyword 为空");
                return false;
            }
            string keyword = (string)arg["keyword"];
            if (!arg.ContainsKey("startTime"))
            {
                ExampleUtils.ShowToast("startTime 为空");
                return false;
            }
            long startTime = long.Parse((string)arg["startTime"]);
            if (!arg.ContainsKey("count"))
            {
                ExampleUtils.ShowToast("count 为空");
                return false;
            }
            int count = int.Parse((string)arg["count"]);
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMSearchMessagesListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMSearchMessagesListenerProxy(ResultHandle);
            }
            int ret = engine.SearchMessages(type, targetId, channelId, keyword, startTime, count, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "searchMessages";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_searchMessagesByTimeRange_call
        public class RCIMSearchMessagesByTimeRangeListenerProxy : RCIMSearchMessagesByTimeRangeListener
        {
            public void OnSuccess(List<RCIMMessage> t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.SearchMessagesByTimeRange(type,targetId,channelId,keyword,startTime,endTime,offset,count,new
        RCIMSearchMessagesByTimeRangeListenerProxy());
        //fun_searchMessagesByTimeRange_call
        */
    
        public bool searchMessagesByTimeRange(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("keyword"))
            {
                ExampleUtils.ShowToast("keyword 为空");
                return false;
            }
            string keyword = (string)arg["keyword"];
            if (!arg.ContainsKey("startTime"))
            {
                ExampleUtils.ShowToast("startTime 为空");
                return false;
            }
            long startTime = long.Parse((string)arg["startTime"]);
            if (!arg.ContainsKey("endTime"))
            {
                ExampleUtils.ShowToast("endTime 为空");
                return false;
            }
            long endTime = long.Parse((string)arg["endTime"]);
            if (!arg.ContainsKey("offset"))
            {
                ExampleUtils.ShowToast("offset 为空");
                return false;
            }
            int offset = int.Parse((string)arg["offset"]);
            if (!arg.ContainsKey("count"))
            {
                ExampleUtils.ShowToast("count 为空");
                return false;
            }
            int count = int.Parse((string)arg["count"]);
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMSearchMessagesByTimeRangeListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMSearchMessagesByTimeRangeListenerProxy(ResultHandle);
            }
            int ret = engine.SearchMessagesByTimeRange(type, targetId, channelId, keyword, startTime, endTime, offset,
                                                       count, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "searchMessagesByTimeRange";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_searchMessagesByUserId_call
        public class RCIMSearchMessagesByUserIdListenerProxy : RCIMSearchMessagesByUserIdListener
        {
            public void OnSuccess(List<RCIMMessage> t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.SearchMessagesByUserId(userId,type,targetId,channelId,startTime,count,new
        RCIMSearchMessagesByUserIdListenerProxy());
        //fun_searchMessagesByUserId_call
        */
    
        public bool searchMessagesByUserId(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("userId"))
            {
                ExampleUtils.ShowToast("userId 为空");
                return false;
            }
            string userId = (string)arg["userId"];
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("startTime"))
            {
                ExampleUtils.ShowToast("startTime 为空");
                return false;
            }
            long startTime = long.Parse((string)arg["startTime"]);
            if (!arg.ContainsKey("count"))
            {
                ExampleUtils.ShowToast("count 为空");
                return false;
            }
            int count = int.Parse((string)arg["count"]);
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMSearchMessagesByUserIdListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMSearchMessagesByUserIdListenerProxy(ResultHandle);
            }
            int ret = engine.SearchMessagesByUserId(userId, type, targetId, channelId, startTime, count, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "searchMessagesByUserId";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_searchConversations_call
        public class RCIMSearchConversationsListenerProxy : RCIMSearchConversationsListener
        {
            public void OnSuccess(List<RCIMSearchConversationResult> t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.SearchConversations(conversationTypes,channelId,messageTypes,keyword,new
        RCIMSearchConversationsListenerProxy());
        //fun_searchConversations_call
        */
    
        public bool searchConversations(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("conversationTypes"))
            {
                ExampleUtils.ShowToast("conversationTypes 为空");
                return false;
            }
            string[] conversationTypes_strs = ((string)arg["conversationTypes"]).Split(',');
            List<RCIMConversationType> conversationTypes = new List<RCIMConversationType>();
            foreach (var item in conversationTypes_strs)
            {
                conversationTypes.Add((RCIMConversationType) int.Parse(item));
            }
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("messageTypes"))
            {
                ExampleUtils.ShowToast("messageTypes 为空");
                return false;
            }
            string[] messageTypes_strs = ((string)arg["messageTypes"]).Split(',');
            List<RCIMMessageType> messageTypes = new List<RCIMMessageType>();
            foreach (var item in messageTypes_strs)
            {
                messageTypes.Add((RCIMMessageType) int.Parse(item));
            }
            if (!arg.ContainsKey("keyword"))
            {
                ExampleUtils.ShowToast("keyword 为空");
                return false;
            }
            string keyword = (string)arg["keyword"];
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMSearchConversationsListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMSearchConversationsListenerProxy(ResultHandle);
            }
            int ret = engine.SearchConversations(conversationTypes, channelId, messageTypes, keyword, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "searchConversations";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_changeNotificationQuietHours_call
        int ret = engine.ChangeNotificationQuietHours(startTime,spanMinutes,level,(code) =>
        {
            //...
        });
        //fun_changeNotificationQuietHours_call
        */
    
        public bool changeNotificationQuietHours(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("startTime"))
            {
                ExampleUtils.ShowToast("startTime 为空");
                return false;
            }
            string startTime = (string)arg["startTime"];
            if (!arg.ContainsKey("spanMinutes"))
            {
                ExampleUtils.ShowToast("spanMinutes 为空");
                return false;
            }
            int spanMinutes = int.Parse((string)arg["spanMinutes"]);
            if (!arg.ContainsKey("level"))
            {
                ExampleUtils.ShowToast("level 为空");
                return false;
            }
            RCIMPushNotificationQuietHoursLevel level =
                (RCIMPushNotificationQuietHoursLevel) int.Parse((string)arg["level"]);
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnNotificationQuietHoursChangedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWChangeNotificationQuietHoursCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.ChangeNotificationQuietHours(startTime, spanMinutes, level, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "changeNotificationQuietHours";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_removeNotificationQuietHours_call
        int ret = engine.RemoveNotificationQuietHours((code) =>
        {
            //...
        });
        //fun_removeNotificationQuietHours_call
        */
    
        public bool removeNotificationQuietHours(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnNotificationQuietHoursRemovedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWRemoveNotificationQuietHoursCallback";
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.RemoveNotificationQuietHours(callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "removeNotificationQuietHours";
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_loadNotificationQuietHours_call
        int ret = engine.LoadNotificationQuietHours();
        //fun_loadNotificationQuietHours_call
        */
    
        public bool loadNotificationQuietHours()
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            int ret = engine.LoadNotificationQuietHours();
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "loadNotificationQuietHours";
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getNotificationQuietHours_call
        public class RCIMGetNotificationQuietHoursListenerProxy : RCIMGetNotificationQuietHoursListener
        {
            public void OnSuccess(string startTime,int spanMinutes,RCIMPushNotificationQuietHoursLevel level)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetNotificationQuietHours(new RCIMGetNotificationQuietHoursListenerProxy());
        //fun_getNotificationQuietHours_call
        */
    
        public bool getNotificationQuietHours(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetNotificationQuietHoursListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetNotificationQuietHoursListenerProxy(ResultHandle);
            }
            int ret = engine.GetNotificationQuietHours(callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getNotificationQuietHours";
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_changeConversationNotificationLevel_call
        int ret = engine.ChangeConversationNotificationLevel(type,targetId,channelId,level,(code) =>
        {
            //...
        });
        //fun_changeConversationNotificationLevel_call
        */
    
        public bool changeConversationNotificationLevel(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("level"))
            {
                ExampleUtils.ShowToast("level 为空");
                return false;
            }
            RCIMPushNotificationLevel level = (RCIMPushNotificationLevel) int.Parse((string)arg["level"]);
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnConversationNotificationLevelChangedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWChangeConversationNotificationLevelCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.ChangeConversationNotificationLevel(type, targetId, channelId, level, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "changeConversationNotificationLevel";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_loadConversationNotificationLevel_call
        int ret = engine.LoadConversationNotificationLevel(type,targetId,channelId);
        //fun_loadConversationNotificationLevel_call
        */
    
        public bool loadConversationNotificationLevel(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            int ret = engine.LoadConversationNotificationLevel(type, targetId, channelId);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "loadConversationNotificationLevel";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getConversationNotificationLevel_call
        public class RCIMGetConversationNotificationLevelListenerProxy : RCIMGetConversationNotificationLevelListener
        {
            public void OnSuccess(RCIMPushNotificationLevel t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetConversationNotificationLevel(type,targetId,channelId,new
        RCIMGetConversationNotificationLevelListenerProxy());
        //fun_getConversationNotificationLevel_call
        */
    
        public bool getConversationNotificationLevel(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetConversationNotificationLevelListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetConversationNotificationLevelListenerProxy(ResultHandle);
            }
            int ret = engine.GetConversationNotificationLevel(type, targetId, channelId, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getConversationNotificationLevel";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_changeConversationTypeNotificationLevel_call
        int ret = engine.ChangeConversationTypeNotificationLevel(type,level,(code) =>
        {
            //...
        });
        //fun_changeConversationTypeNotificationLevel_call
        */
    
        public bool changeConversationTypeNotificationLevel(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("level"))
            {
                ExampleUtils.ShowToast("level 为空");
                return false;
            }
            RCIMPushNotificationLevel level = (RCIMPushNotificationLevel) int.Parse((string)arg["level"]);
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnConversationTypeNotificationLevelChangedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWChangeConversationTypeNotificationLevelCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.ChangeConversationTypeNotificationLevel(type, level, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "changeConversationTypeNotificationLevel";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_loadConversationTypeNotificationLevel_call
        int ret = engine.LoadConversationTypeNotificationLevel(type);
        //fun_loadConversationTypeNotificationLevel_call
        */
    
        public bool loadConversationTypeNotificationLevel(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            int ret = engine.LoadConversationTypeNotificationLevel(type);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "loadConversationTypeNotificationLevel";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getConversationTypeNotificationLevel_call
        public class RCIMGetConversationTypeNotificationLevelListenerProxy :
        RCIMGetConversationTypeNotificationLevelListener
        {
            public void OnSuccess(RCIMPushNotificationLevel t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetConversationTypeNotificationLevel(type,new
        RCIMGetConversationTypeNotificationLevelListenerProxy());
        //fun_getConversationTypeNotificationLevel_call
        */
    
        public bool getConversationTypeNotificationLevel(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetConversationTypeNotificationLevelListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetConversationTypeNotificationLevelListenerProxy(ResultHandle);
            }
            int ret = engine.GetConversationTypeNotificationLevel(type, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getConversationTypeNotificationLevel";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_changeUltraGroupDefaultNotificationLevel_call
        int ret = engine.ChangeUltraGroupDefaultNotificationLevel(targetId,level,(code) =>
        {
            //...
        });
        //fun_changeUltraGroupDefaultNotificationLevel_call
        */
    
        public bool changeUltraGroupDefaultNotificationLevel(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            if (!arg.ContainsKey("level"))
            {
                ExampleUtils.ShowToast("level 为空");
                return false;
            }
            RCIMPushNotificationLevel level = (RCIMPushNotificationLevel) int.Parse((string)arg["level"]);
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnUltraGroupDefaultNotificationLevelChangedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWChangeUltraGroupDefaultNotificationLevelCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.ChangeUltraGroupDefaultNotificationLevel(targetId, level, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "changeUltraGroupDefaultNotificationLevel";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_loadUltraGroupDefaultNotificationLevel_call
        int ret = engine.LoadUltraGroupDefaultNotificationLevel(targetId);
        //fun_loadUltraGroupDefaultNotificationLevel_call
        */
    
        public bool loadUltraGroupDefaultNotificationLevel(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            int ret = engine.LoadUltraGroupDefaultNotificationLevel(targetId);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "loadUltraGroupDefaultNotificationLevel";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getUltraGroupDefaultNotificationLevel_call
        public class RCIMGetUltraGroupDefaultNotificationLevelListenerProxy :
        RCIMGetUltraGroupDefaultNotificationLevelListener
        {
            public void OnSuccess(RCIMPushNotificationLevel t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetUltraGroupDefaultNotificationLevel(targetId,new
        RCIMGetUltraGroupDefaultNotificationLevelListenerProxy());
        //fun_getUltraGroupDefaultNotificationLevel_call
        */
    
        public bool getUltraGroupDefaultNotificationLevel(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetUltraGroupDefaultNotificationLevelListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetUltraGroupDefaultNotificationLevelListenerProxy(ResultHandle);
            }
            int ret = engine.GetUltraGroupDefaultNotificationLevel(targetId, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getUltraGroupDefaultNotificationLevel";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_changeUltraGroupChannelDefaultNotificationLevel_call
        int ret = engine.ChangeUltraGroupChannelDefaultNotificationLevel(targetId,channelId,level,(code) =>
        {
            //...
        });
        //fun_changeUltraGroupChannelDefaultNotificationLevel_call
        */
    
        public bool changeUltraGroupChannelDefaultNotificationLevel(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("level"))
            {
                ExampleUtils.ShowToast("level 为空");
                return false;
            }
            RCIMPushNotificationLevel level = (RCIMPushNotificationLevel) int.Parse((string)arg["level"]);
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnUltraGroupChannelDefaultNotificationLevelChangedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWChangeUltraGroupChannelDefaultNotificationLevelCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.ChangeUltraGroupChannelDefaultNotificationLevel(targetId, channelId, level, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "changeUltraGroupChannelDefaultNotificationLevel";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_loadUltraGroupChannelDefaultNotificationLevel_call
        int ret = engine.LoadUltraGroupChannelDefaultNotificationLevel(targetId,channelId);
        //fun_loadUltraGroupChannelDefaultNotificationLevel_call
        */
    
        public bool loadUltraGroupChannelDefaultNotificationLevel(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            int ret = engine.LoadUltraGroupChannelDefaultNotificationLevel(targetId, channelId);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "loadUltraGroupChannelDefaultNotificationLevel";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getUltraGroupChannelDefaultNotificationLevel_call
        public class RCIMGetUltraGroupChannelDefaultNotificationLevelListenerProxy :
        RCIMGetUltraGroupChannelDefaultNotificationLevelListener
        {
            public void OnSuccess(RCIMPushNotificationLevel t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetUltraGroupChannelDefaultNotificationLevel(targetId,channelId,new
        RCIMGetUltraGroupChannelDefaultNotificationLevelListenerProxy());
        //fun_getUltraGroupChannelDefaultNotificationLevel_call
        */
    
        public bool getUltraGroupChannelDefaultNotificationLevel(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetUltraGroupChannelDefaultNotificationLevelListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetUltraGroupChannelDefaultNotificationLevelListenerProxy(ResultHandle);
            }
            int ret = engine.GetUltraGroupChannelDefaultNotificationLevel(targetId, channelId, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getUltraGroupChannelDefaultNotificationLevel";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_changePushContentShowStatus_call
        int ret = engine.ChangePushContentShowStatus(showContent,(code) =>
        {
            //...
        });
        //fun_changePushContentShowStatus_call
        */
    
        public bool changePushContentShowStatus(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("showContent"))
            {
                ExampleUtils.ShowToast("showContent 为空");
                return false;
            }
            bool showContent = int.Parse((string)arg["showContent"]) != 0;
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnPushContentShowStatusChangedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWChangePushContentShowStatusCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.ChangePushContentShowStatus(showContent, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "changePushContentShowStatus";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_changePushLanguage_call
        int ret = engine.ChangePushLanguage(language,(code) =>
        {
            //...
        });
        //fun_changePushLanguage_call
        */
    
        public bool changePushLanguage(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("language"))
            {
                ExampleUtils.ShowToast("language 为空");
                return false;
            }
            string language = (string)arg["language"];
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnPushLanguageChangedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWChangePushLanguageCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.ChangePushLanguage(language, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "changePushLanguage";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_changePushReceiveStatus_call
        int ret = engine.ChangePushReceiveStatus(receive,(code) =>
        {
            //...
        });
        //fun_changePushReceiveStatus_call
        */
    
        public bool changePushReceiveStatus(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("receive"))
            {
                ExampleUtils.ShowToast("receive 为空");
                return false;
            }
            bool receive = int.Parse((string)arg["receive"]) != 0;
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnPushReceiveStatusChangedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWChangePushReceiveStatusCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.ChangePushReceiveStatus(receive, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "changePushReceiveStatus";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_sendGroupMessageToDesignatedUsers_call
        public class RCIMSendGroupMessageToDesignatedUsersListenerProxy : RCIMSendGroupMessageToDesignatedUsersListener
        {
            public void OnMessageSaved(RCIMMessage message)
            {
                //...
            }
            public void OnMessageSent(int code,RCIMMessage message)
            {
                //...
            }
        }
        int ret = engine.SendGroupMessageToDesignatedUsers(message,userIds,new
        RCIMSendGroupMessageToDesignatedUsersListenerProxy());
        //fun_sendGroupMessageToDesignatedUsers_call
        */
    
        /*
        //fun_loadMessageCount_call
        int ret = engine.LoadMessageCount(type,targetId,channelId);
        //fun_loadMessageCount_call
        */
    
        public bool loadMessageCount(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            int ret = engine.LoadMessageCount(type, targetId, channelId);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "loadMessageCount";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getMessageCount_call
        public class RCIMGetMessageCountListenerProxy : RCIMGetMessageCountListener
        {
            public void OnSuccess(int t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetMessageCount(type,targetId,channelId,new RCIMGetMessageCountListenerProxy());
        //fun_getMessageCount_call
        */
    
        public bool getMessageCount(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetMessageCountListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetMessageCountListenerProxy(ResultHandle);
            }
            int ret = engine.GetMessageCount(type, targetId, channelId, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getMessageCount";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_loadTopConversations_call
        int ret = engine.LoadTopConversations(conversationTypes,channelId);
        //fun_loadTopConversations_call
        */
    
        public bool loadTopConversations(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("conversationTypes"))
            {
                ExampleUtils.ShowToast("conversationTypes 为空");
                return false;
            }
            string[] conversationTypes_strs = ((string)arg["conversationTypes"]).Split(',');
            List<RCIMConversationType> conversationTypes = new List<RCIMConversationType>();
            foreach (var item in conversationTypes_strs)
            {
                conversationTypes.Add((RCIMConversationType) int.Parse(item));
            }
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            int ret = engine.LoadTopConversations(conversationTypes, channelId);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "loadTopConversations";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getTopConversations_call
        public class RCIMGetTopConversationsListenerProxy : RCIMGetTopConversationsListener
        {
            public void OnSuccess(List<RCIMConversation> t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetTopConversations(conversationTypes,channelId,new RCIMGetTopConversationsListenerProxy());
        //fun_getTopConversations_call
        */
    
        public bool getTopConversations(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("conversationTypes"))
            {
                ExampleUtils.ShowToast("conversationTypes 为空");
                return false;
            }
            string[] conversationTypes_strs = ((string)arg["conversationTypes"]).Split(',');
            List<RCIMConversationType> conversationTypes = new List<RCIMConversationType>();
            foreach (var item in conversationTypes_strs)
            {
                conversationTypes.Add((RCIMConversationType) int.Parse(item));
            }
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetTopConversationsListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetTopConversationsListenerProxy(ResultHandle);
            }
            int ret = engine.GetTopConversations(conversationTypes, channelId, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getTopConversations";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_syncUltraGroupReadStatus_call
        int ret = engine.SyncUltraGroupReadStatus(targetId,channelId,timestamp,(code) =>
        {
            //...
        });
        //fun_syncUltraGroupReadStatus_call
        */
    
        public bool syncUltraGroupReadStatus(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("timestamp"))
            {
                ExampleUtils.ShowToast("timestamp 为空");
                return false;
            }
            long timestamp = long.Parse((string)arg["timestamp"]);
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnUltraGroupReadStatusSyncedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWSyncUltraGroupReadStatusCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.SyncUltraGroupReadStatus(targetId, channelId, timestamp, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "syncUltraGroupReadStatus";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_loadConversationsForAllChannel_call
        int ret = engine.LoadConversationsForAllChannel(type,targetId);
        //fun_loadConversationsForAllChannel_call
        */
    
        public bool loadConversationsForAllChannel(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            int ret = engine.LoadConversationsForAllChannel(type, targetId);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "loadConversationsForAllChannel";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getConversationsForAllChannel_call
        public class RCIMGetConversationsForAllChannelListenerProxy : RCIMGetConversationsForAllChannelListener
        {
            public void OnSuccess(List<RCIMConversation> t)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetConversationsForAllChannel(type,targetId,new RCIMGetConversationsForAllChannelListenerProxy());
        //fun_getConversationsForAllChannel_call
        */
    
        public bool getConversationsForAllChannel(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("type"))
            {
                ExampleUtils.ShowToast("type 为空");
                return false;
            }
            RCIMConversationType type = (RCIMConversationType) int.Parse((string)arg["type"]);
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            RCIMGetConversationsForAllChannelListener callback = null;
            if (useCallback != "0")
            {
                callback = new RCIMGetConversationsForAllChannelListenerProxy(ResultHandle);
            }
            int ret = engine.GetConversationsForAllChannel(type, targetId, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getConversationsForAllChannel";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_modifyUltraGroupMessage_call
        int ret = engine.ModifyUltraGroupMessage(messageUId,message,(code) =>
        {
            //...
        });
        //fun_modifyUltraGroupMessage_call
        */
    
        /*
        //fun_recallUltraGroupMessage_call
        int ret = engine.RecallUltraGroupMessage(message,deleteRemote,(code) =>
        {
            //...
        });
        //fun_recallUltraGroupMessage_call
        */
    
        /*
        //fun_clearUltraGroupMessages_call
        int ret = engine.ClearUltraGroupMessages(targetId,channelId,timestamp,policy,(code) =>
        {
            //...
        });
        //fun_clearUltraGroupMessages_call
        */
    
        public bool clearUltraGroupMessages(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("timestamp"))
            {
                ExampleUtils.ShowToast("timestamp 为空");
                return false;
            }
            long timestamp = long.Parse((string)arg["timestamp"]);
            if (!arg.ContainsKey("policy"))
            {
                ExampleUtils.ShowToast("policy 为空");
                return false;
            }
            RCIMMessageOperationPolicy policy = (RCIMMessageOperationPolicy) int.Parse((string)arg["policy"]);
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnUltraGroupMessagesClearedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWClearUltraGroupMessagesCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.ClearUltraGroupMessages(targetId, channelId, timestamp, policy, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "clearUltraGroupMessages";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_sendUltraGroupTypingStatus_call
        int ret = engine.SendUltraGroupTypingStatus(targetId,channelId,typingStatus,(code) =>
        {
            //...
        });
        //fun_sendUltraGroupTypingStatus_call
        */
    
        public bool sendUltraGroupTypingStatus(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            string channelId = ExampleUtils.GetValue(arg, "channelId", "");
            if (!arg.ContainsKey("typingStatus"))
            {
                ExampleUtils.ShowToast("typingStatus 为空");
                return false;
            }
            RCIMUltraGroupTypingStatus typingStatus = (RCIMUltraGroupTypingStatus) int.Parse((string)arg["typingStatus"]);
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnUltraGroupTypingStatusSentAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWSendUltraGroupTypingStatusCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.SendUltraGroupTypingStatus(targetId, channelId, typingStatus, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "sendUltraGroupTypingStatus";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_clearUltraGroupMessagesForAllChannel_call
        int ret = engine.ClearUltraGroupMessagesForAllChannel(targetId,timestamp,(code) =>
        {
            //...
        });
        //fun_clearUltraGroupMessagesForAllChannel_call
        */
    
        public bool clearUltraGroupMessagesForAllChannel(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("targetId"))
            {
                ExampleUtils.ShowToast("targetId 为空");
                return false;
            }
            string targetId = (string)arg["targetId"];
            if (!arg.ContainsKey("timestamp"))
            {
                ExampleUtils.ShowToast("timestamp 为空");
                return false;
            }
            long timestamp = long.Parse((string)arg["timestamp"]);
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnUltraGroupMessagesClearedForAllChannelAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWClearUltraGroupMessagesForAllChannelCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.ClearUltraGroupMessagesForAllChannel(targetId, timestamp, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "clearUltraGroupMessagesForAllChannel";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_loadBatchRemoteUltraGroupMessages_call
        int ret = engine.LoadBatchRemoteUltraGroupMessages(messages);
        //fun_loadBatchRemoteUltraGroupMessages_call
        */
    
        /*
        //fun_getBatchRemoteUltraGroupMessages_call
        public class RCIMGetBatchRemoteUltraGroupMessagesListenerProxy : RCIMGetBatchRemoteUltraGroupMessagesListener
        {
            public void OnSuccess(List<RCIMMessage> matchedMessages,List<RCIMMessage> notMatchedMessages)
            {
                //...
            }
            public void OnError(int code)
            {
                //...
            }
        }
        int ret = engine.GetBatchRemoteUltraGroupMessages(messages,new RCIMGetBatchRemoteUltraGroupMessagesListenerProxy());
        //fun_getBatchRemoteUltraGroupMessages_call
        */
    
        /*
        //fun_updateUltraGroupMessageExpansion_call
        int ret = engine.UpdateUltraGroupMessageExpansion(messageUId,expansion,(code) =>
        {
            //...
        });
        //fun_updateUltraGroupMessageExpansion_call
        */
    
        public bool updateUltraGroupMessageExpansion(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("messageUId"))
            {
                ExampleUtils.ShowToast("messageUId 为空");
                return false;
            }
            string messageUId = (string)arg["messageUId"];
            Dictionary<string, string> expansion = new Dictionary<string, string>();
            string[] keys = ExampleUtils.GetValue(arg, "keys", "").Split(',');
            string[] values = ExampleUtils.GetValue(arg, "values", "").Split(',');
            for (int i = 0; i < keys.Length; i++)
            {
                if (i < values.Length)
                {
                    expansion.Add(keys[i], values[i]);
                }
            }
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnUltraGroupMessageExpansionUpdatedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWUpdateUltraGroupMessageExpansionCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.UpdateUltraGroupMessageExpansion(messageUId, expansion, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "updateUltraGroupMessageExpansion";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_removeUltraGroupMessageExpansionForKeys_call
        int ret = engine.RemoveUltraGroupMessageExpansionForKeys(messageUId,keys,(code) =>
        {
            //...
        });
        //fun_removeUltraGroupMessageExpansionForKeys_call
        */
    
        public bool removeUltraGroupMessageExpansionForKeys(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("messageUId"))
            {
                ExampleUtils.ShowToast("messageUId 为空");
                return false;
            }
            string messageUId = (string)arg["messageUId"];
            if (!arg.ContainsKey("keys"))
            {
                ExampleUtils.ShowToast("keys 为空");
                return false;
            }
            string[] keys_strs = ((string)arg["keys"]).Split(',');
            List<String> keys = new List<String>(keys_strs);
            string useCallback = ExampleUtils.GetValue(arg, "use_cb", "1");
            OnUltraGroupMessageExpansionForKeysRemovedAction callback = null;
            if (useCallback != "0")
            {
                callback = (code) =>
                {
                    Dictionary<String, object> result = new Dictionary<String, object>();
                    result["method"] = "IRCIMIWRemoveUltraGroupMessageExpansionForKeysCallback";
                    result["input"] = arg;
                    result["code"] = code;
                    ResultHandle?.Invoke(result);
                };
            }
            int ret = engine.RemoveUltraGroupMessageExpansionForKeys(messageUId, keys, callback);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "removeUltraGroupMessageExpansionForKeys";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_changeLogLevel_call
        int ret = engine.ChangeLogLevel(level);
        //fun_changeLogLevel_call
        */
    
        public bool changeLogLevel(Dictionary<String, object> arg)
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            if (!arg.ContainsKey("level"))
            {
                ExampleUtils.ShowToast("level 为空");
                return false;
            }
            RCIMLogLevel level = (RCIMLogLevel) int.Parse((string)arg["level"]);
            int ret = engine.ChangeLogLevel(level);
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "changeLogLevel";
            result["input"] = arg;
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    
        /*
        //fun_getDeltaTime_call
        long ret = engine.GetDeltaTime();
        //fun_getDeltaTime_call
        */
    
        public bool getDeltaTime()
        {
            if (engine == null)
            {
                ExampleUtils.ShowToast("引擎未初始化");
                return false;
            }
            long ret = engine.GetDeltaTime();
            Dictionary<String, object> result = new Dictionary<String, object>();
            result["method"] = "getDeltaTime";
            result["ret"] = ret;
            ResultHandle?.Invoke(result);
            return true;
        }
    }
}
