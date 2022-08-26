using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using AOT;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Serialization;

# if PLATFORM_ANDROID
using UnityEngine.Android;
# endif

using Newtonsoft.Json;
using cn_rongcloud_im_unity;
using UnityEngine.SceneManagement;

namespace cn_rongcloud_im_unity_example
{
    public class Example : MonoBehaviour
    {
        ~Example()
        {
            Console.WriteLine("An Instance of class destroyed");
        }

        // Start is called before the first frame update
        void Start()
        {
            //#if !UNITY_EDITOR
            RCIMClient.Instance.OnConnectionStatusChanged += OnConnectionStatusChanged;
            RCIMClient.Instance.OnMessageReceived += OnMessageReceived;
            RCIMClient.Instance.OnReadReceiptRequest += Instance_OnReadReceiptRequest;
            RCIMClient.Instance.OnDownloadMediaMessageProgressed += OnDownloadMediaProgress;
            RCIMClient.Instance.OnDownloadMediaMessageCanceled += OnDownloadMediaCanceled;
            RCIMClient.Instance.OnDownloadMediaMessageFailed += OnDownloadMediaFailed;
            RCIMClient.Instance.OnDownloadMediaMessageCompleted += OnDownloadMediaCompleted;
#if UNITY_ANDROID
            RCIMClient.Instance.OnDownloadMediaMessagePaused += OnDownloadMediaMessagePaused;
#endif

            DemoContext.SetActiveScene("Example", this);
            DemoContext.exampleScene = this;
            //#endif
            ConnectCanvasToastText = GameObject.Find("/Connect/Toast").GetComponent<Text>();

            RoomId = "IMTestRoom";
            OnState();
        }

        // Update is called once per frame
        void Update()
        {
            if (!RunOnMainThread.IsEmpty)
            {
                while (RunOnMainThread.TryDequeue(out var action))
                {
                    action?.Invoke();
                }
            }
        }

        private void OnDestroy()
        {
            RCIMClient.Instance.OnConnectionStatusChanged -= OnConnectionStatusChanged;
            RCIMClient.Instance.OnMessageReceived -= OnMessageReceived;
            RCIMClient.Instance.OnReadReceiptRequest -= Instance_OnReadReceiptRequest;
            RCIMClient.Instance.OnDownloadMediaMessageProgressed -= OnDownloadMediaProgress;
            RCIMClient.Instance.OnDownloadMediaMessageCanceled -= OnDownloadMediaCanceled;
            RCIMClient.Instance.OnDownloadMediaMessageFailed -= OnDownloadMediaFailed;
            RCIMClient.Instance.OnDownloadMediaMessageCompleted -= OnDownloadMediaCompleted;
#if UNITY_ANDROID
            RCIMClient.Instance.OnDownloadMediaMessagePaused -= OnDownloadMediaMessagePaused;
#endif
        }

        private void OnState()
        {
            RunOnMainThread.Enqueue(() =>
            {
                if (DemoContext.Connected)
                {
                    ChangeToConnected();
                }
                else
                {
                    ChangeToUnconnect();
                }
            });
        }

        private RCConversation GetSpecifiedConv()
        {
            var curCnvIdxInputField = GameObject.Find("/Connect/InputField").GetComponent<InputField>();
            var curCnvIdx = curCnvIdxInputField.text;
            var curConv = DemoContext.GetCurrentConversationByIdx(curCnvIdx);
            return curConv;
        }

        private void Instance_OnReadReceiptRequest(RCConversationType type, string targetId, string messageUid)
        {
            ShowToast($"OnReadReceiptRequest: {type} {targetId} {messageUid}");

            RCIMClient.Instance.GetMessageByUid(messageUid, (getMsgCode, msgFetched) =>
            {
                if (getMsgCode != RCErrorCode.Succeed)
                    return;
                RCIMClient.Instance.SendReadReceiptResponse(type, targetId, new List<RCMessage>() {msgFetched},
                    null);
            });
        }

        #region Button Click

        public void OnClickConnect()
        {
            if (DemoContext.Connected)
            {
                RCIMClient.Instance.Disconnect();
                ChangeToUnconnect();
            }
            else
            {
                ConnectIM(DemoContext.User3);
            }
        }

        public void OnClickConnect1()
        {
            if (DemoContext.Connected)
            {
                RCIMClient.Instance.Disconnect();
                ChangeToUnconnect();
            }
            else
            {
                ConnectIM(DemoContext.User2);
            }
        }

        public void OnClickLogOut()
        {
            RCIMClient.Instance.Logout();
        }

        public void OnClickChatRoom()
        {
            SceneManager.LoadScene("ChatRoom", LoadSceneMode.Single);
        }

        public void OnClickGetConversationList()
        {
            if (!DemoContext.Connected)
            {
                //ShowToast("请先连接服务器");
                return;
            }

            RCIMClient.Instance.GetConversationList((error, conversationList) =>
                {
                    DemoContext.ConversationList = conversationList;
                    var toast =
                        $"GetConversationList called. {nameof(error)} {conversationList?.Count} {Environment.NewLine}";
                    if (conversationList != null)
                    {
                        foreach (var item in conversationList)
                        {
                            toast += $" {item} {Environment.NewLine}";
                        }
                    }

                    ShowToast(toast);
                },
                RCConversationType.ChatRoom, RCConversationType.Private, RCConversationType.Group);
        }

        public void OnClickClearConversationList()
        {
            if (!DemoContext.Connected)
            {
                //ShowToast("请先连接服务器");
                return;
            }

            RCIMClient.Instance.ClearConversations((error) =>
                {
                    DemoContext.ConversationList = null;
                    ShowToast(string.Format("ClearConversations callback called. {0}", error));
                },
                RCConversationType.ChatRoom, RCConversationType.Private, RCConversationType.Group);
        }

        public void OnClickGetConversationListByPage()
        {
            if (!DemoContext.Connected)
            {
                //ShowToast("请先连接服务器");
                return;
            }

            RCIMClient.Instance.GetConversationListByPage(
                (error, conversationList) =>
                {
                    DemoContext.ConversationList = conversationList;
                    var toast =
                        $"GetConversationListByPage called. {error} {conversationList?.Count} {Environment.NewLine}";
                    if (conversationList != null)
                    {
                        foreach (var item in conversationList)
                        {
                            toast += $" {item} {Environment.NewLine}";
                        }
                    }

                    ShowToast(toast);
                }, Util.CurrentTimeStamp(), 10, RCConversationType.ChatRoom,
                RCConversationType.Private,
                RCConversationType.Group);
        }

        public void OnClickGetUnreadCount()
        {
            if (!DemoContext.Connected)
            {
                //ShowToast("请先连接服务器");
                return;
            }

            var toast = String.Empty;
            var curConv = GetSpecifiedConv();
            if (curConv != null)
            {
                RCIMClient.Instance.GetUnreadCount(curConv.ConversationType, curConv.TargetId, (error2, unreadCount2) =>
                {
                    toast +=
                        $"GetUnreadCount byConversation {curConv.ConversationType} {curConv.TargetId} callback called. {error2} {unreadCount2} {Environment.NewLine}";
                    ShowToast(toast);
                });
            }

            RCIMClient.Instance.GetTotalUnreadCount((error, unreadCount) =>
            {
                toast += $"GetTotalUnreadCount callback called. {error} {unreadCount} {Environment.NewLine}";
                ShowToast(toast);
                RCIMClient.Instance.GetUnreadCountByConversationTypes(true,
                    (error1, unreadCount1) =>
                    {
                        toast +=
                            $"GetUnreadCountByConversationTypes callback called. {error1} {unreadCount1} {Environment.NewLine}";
                        ShowToast(toast);
                    }, RCConversationType.ChatRoom, RCConversationType.Private, RCConversationType.Group);
            });
        }

        public void OnClickClearConversationUnReadStatus()
        {
            var curConv = GetSpecifiedConv();
            if (curConv == null) return;

            RCIMClient.Instance.ClearMessageUnreadStatus(curConv.ConversationType, curConv.TargetId,
                (code) =>
                {
                    Debug.Log(
                        $"ClearMessageUnreadStatus: {curConv.ConversationType} {curConv.TargetId} callback called. {code} ");
                });
        }

        public void OnClickGetBlockedConversation()
        {
            if (!DemoContext.Connected)
            {
                return;
            }

            RCIMClient.Instance.GetBlockedConversationList((code, conversationList) =>
                {
                    var toast =
                        $"GetBlockedConversationList called. {code} {conversationList?.Count} {Environment.NewLine}";
                    if (conversationList != null)
                    {
                        foreach (var item in conversationList)
                        {
                            toast += $" {item} {Environment.NewLine}";
                        }
                    }

                    ShowToast(toast);
                }, RCConversationType.Private,
                RCConversationType.Group, RCConversationType.ChatRoom);
        }

        public void OnClickRemoveConversation()
        {
            if (!DemoContext.Connected || String.IsNullOrEmpty(RoomId))
            {
                //ShowToast("请先连接服务器");
                return;
            }

            var curConv = GetSpecifiedConv();
            if (curConv == null) return;

            RCIMClient.Instance.RemoveConversation(curConv.ConversationType, curConv.TargetId,
                (code) => { ShowToast(string.Format("RemoveConversation called. errCode={0}", code)); });
        }

        public void OnClickGetConversationInfo()
        {
            if (!DemoContext.Connected || String.IsNullOrEmpty(RoomId))
            {
                //ShowToast("请先连接服务器");
                return;
            }

            var curConv = GetSpecifiedConv();
            if (curConv == null) return;

            RCIMClient.Instance.GetConversation(curConv.ConversationType, curConv.TargetId,
                (code, conversation) => { ShowToast($"GetConversation called. errCode={code}, {conversation}"); });
        }

        public void OnClickGetToppedConversationList()
        {
            if (!DemoContext.Connected)
            {
                return;
            }

            RCIMClient.Instance.GetTopConversationList((code, toppedConversations) =>
                {
                    var toast =
                        $"GetTopConversationList called. {code} {toppedConversations?.Count} {Environment.NewLine}";
                    if (toppedConversations != null)
                    {
                        foreach (var item in toppedConversations)
                        {
                            toast += $" {item} {Environment.NewLine}";
                        }
                    }

                    ShowToast(toast);
                }, RCConversationType.Private,
                RCConversationType.Group, RCConversationType.ChatRoom);
        }

        public void OnClickSearchConversation()
        {
            var converTypes = new RCConversationType[]
            {
                RCConversationType.Private,
                RCConversationType.Group,
                RCConversationType.ChatRoom
            };
            var objectNames = new string[]
            {
                "RC:TxtMsg",
                "RC:SightMsg"
            };
            RCIMClient.Instance.SearchConversations("1", converTypes, objectNames, (code, result) =>
            {
                var toast =
                    $"SearchConversations called. {code} {result?.Count} {Environment.NewLine}";
                if (result != null)
                {
                    foreach (var item in result)
                    {
                        toast += $" {item} {Environment.NewLine}";
                    }
                }

                ShowToast(toast);
            });
        }

        public void OnClickGetConnectionStatus()
        {
            var connStatus = RCIMClient.Instance.GetConnectionStatus();
            ShowToast($"Current Connection: {connStatus}");
        }

        public void OnClickedGlobalSetting()
        {
            DemoContext.SetActiveScene("", null);
            SceneManager.LoadScene("GlobalSetting", LoadSceneMode.Single);
        }

        public void OnClickedConversation()
        {
            SceneManager.LoadScene("ConversationPage", LoadSceneMode.Single);
        }

        public void OnClickTag()
        {
            DemoContext.SetActiveScene("", null);
            SceneManager.LoadScene("Tag", LoadSceneMode.Single);
        }

        #endregion

        #region UI Event

        private static void OnConnectionStatusChanged(RCConnectionStatus connectionStatus)
        {
            if (DemoContext.exampleScene != null)
            {
                DemoContext.exampleScene.OnIMConnectStatusChanged(connectionStatus);
            }
        }

        private  void OnMessageReceived(RCMessage message, int left)
        {
            var objectName = message.ObjectName;
            if (objectName == "RC:SightMsg")
            {
                if (DemoContext.exampleScene != null)
                {
                    var example = DemoContext.exampleScene;
                    RunOnMainThread.Enqueue(() => { example.DownloadMediaMessage(message); });
                }
            }

            printOutputText($"received message: {message}");

            // 发送已读回执
            if (message.ConversationType == RCConversationType.Private)
            {
                RCConversationType type = message.ConversationType;
                var targetId = message.SenderUserId;
                var timeStamp = Util.CurrentTimeStamp();
                RCIMClient.Instance.SendReadReceiptMessage(type, targetId, timeStamp,
                    (errCode) => { printOutputText($"SendReadReceiptMessage callback: code={errCode}"); }); 
            }
        }

        private void OnDownloadMediaProgress(RCMessage message, int progress)
        {
            printOutputText($"download progress {progress}");
            if (progress > 10)
            {
                printOutputText($"will cancel download message. {message.MessageId}{Environment.NewLine}");
                RCIMClient.Instance.CancelDownloadMediaMessage(message);
            }
        }

        private void OnDownloadMediaCanceled(RCMessage message)
        {
            printOutputText($"download canceled {message}");
        }

        private void OnDownloadMediaFailed(RCErrorCode errorCode, RCMessage message)
        {
            printOutputText($"download failed. code={errorCode}, msg:{message}");
        }

        private void OnDownloadMediaCompleted(RCMessage message)
        {
            printOutputText($"download completed. {message}");
        }

#if UNITY_ANDROID
        private void OnDownloadMediaMessagePaused(RCErrorCode code, RCMessage message)
        {
           printOutputText($"download paused. code={code}, msg:{message}"); 
        }
#endif

        #endregion

        #region IM Callbacks

        private void OnIMConnectStatusChanged(RCConnectionStatus connectionStatus)
        {
            ShowToast($"IMConnStatus: {connectionStatus}");

            RunOnMainThread.Enqueue(() =>
            {
                if (connectionStatus == RCConnectionStatus.Connected)
                {
                    ChangeToConnected();
                }
                else if (connectionStatus != RCConnectionStatus.Connecting)
                {
                    int code = (int) connectionStatus;
                    //ShowToast("IM Connect Error, Code = " + code);
                    ChangeToUnconnect();
                }
            });
        }

        #endregion

        #region Operations

        private void ChangeToUnconnect()
        {
            DemoContext.Connected = false;
            if (DemoContext.CurrentUser != null)
                refreshConnectText(DemoContext.CurrentUser.Id, false);
        }

        private void ChangeToConnected()
        {
            DemoContext.Connected = true;
            if (DemoContext.CurrentUser != null)
                refreshConnectText(DemoContext.CurrentUser.Id, true);
        }

        private void ConnectIM(LoginData user)
        {
            DemoContext.CurrentUser = user;
            RCIMClient.Instance.Connect(user.Token, (errorCode, userId) =>
            {
                if (errorCode == RCErrorCode.Succeed)
                {
                    RunOnMainThread.Enqueue(() => { refreshConnectText(userId, true); });
                }

                ShowToast(string.Format("App connect callback called. {0}, {1}", errorCode, userId));
            });
        }

        private void DownloadMediaMessage(RCMessage message)
        {
            printOutputText($"invoke Download media message ~~");
            RCIMClient.Instance.DownloadMediaMessage(message);
        }

        private void refreshConnectText(string userId, bool isConnected)
        {
            var connButtonPath = string.Empty;
            GameObject hiddenButton;
            Text hiddenButtonText;
            if (userId == DemoContext.User3.Id)
            {
                connButtonPath = "/Connect/Connect/Text";
                hiddenButton = GameObject.Find("/Connect/Connect1").GetComponent<Button>().gameObject;
                hiddenButtonText = GameObject.Find("/Connect/Connect1/Text").GetComponent<Text>();
            }
            else if (userId == DemoContext.User2.Id)
            {
                connButtonPath = "/Connect/Connect1/Text";
                hiddenButton = GameObject.Find("/Connect/Connect").GetComponent<Button>().gameObject;
                hiddenButtonText = GameObject.Find("/Connect/Connect/Text").GetComponent<Text>();
            }
            else
            {
                ShowToast("不是指定的登录id！");
                return;
            }

            var connText = GameObject.Find(connButtonPath).GetComponent<Text>();
            connText.text = isConnected ? "断开" : "连接";
            hiddenButtonText.text = "连接";
        }

        public static void printOutputText(string text)
        {
            if (DemoContext.activeScene is BaseScene)
            {
                var baseScene =
                    DemoContext.activeScene as BaseScene;
                baseScene.print(text);
            }
            else if (DemoContext.activeScene is Example)
            {
                var example = DemoContext.exampleScene;
                example.ShowToast(text);
            }
        }

        #endregion

        private void ShowToast(string tip)
        {
            if (String.IsNullOrEmpty(tip))
                return;
            RunOnMainThread.Enqueue(() =>
            {
                if (ConnectCanvasToastText != null)
                {
                    ConnectCanvasToastText.text = tip;
                }
                else
                {
                    Debug.Log("XXXXXXXXXX");
                }
            });
        }

        #region Utils

        private void Post(String url, String json, Action<bool, String> callback)
        {
            StartCoroutine(_Post(url, json, callback));
        }

        private IEnumerator _Post(String url, String json, Action<bool, String> callback)
        {
            using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
            {
                request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
                request.SetRequestHeader("content-type", "application/json;charset=utf-8");
                request.downloadHandler = new DownloadHandlerBuffer();
                yield return request.SendWebRequest();

                String result = "";
                if (request.isNetworkError || request.isHttpError)
                {
                    result = request.error;
                }
                else
                {
                    result = request.downloadHandler.text;
                }

                callback?.Invoke(request.isNetworkError || request.isHttpError, result);
            }
        }

        private void Get(String url, Action<bool, String> callback)
        {
            StartCoroutine(_Get(url, callback));
        }

        private IEnumerator _Get(String url, Action<bool, String> callback)
        {
            using (UnityWebRequest request = new UnityWebRequest(url, "GET"))
            {
                request.SetRequestHeader("content-type", "application/json;charset=utf-8");
                request.downloadHandler = new DownloadHandlerBuffer();
                yield return request.SendWebRequest();

                String result = "";
                if (request.isNetworkError || request.isHttpError)
                {
                    result = request.error;
                }
                else
                {
                    result = request.downloadHandler.text;
                }

                callback?.Invoke(request.isNetworkError || request.isHttpError, result);
            }
        }

        #endregion

        #region Properties

        private static readonly ConcurrentQueue<Action> RunOnMainThread = new ConcurrentQueue<Action>();

        private Text ConnectCanvasToastText;

        private string RoomId;

        #endregion
    }
}