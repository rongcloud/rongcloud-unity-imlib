using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using cn_rongcloud_im_unity;
using cn_rongcloud_im_unity_example;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConversationSetting : MonoBehaviour
{
    private static readonly ConcurrentQueue<Action> RunOnMainThread = new ConcurrentQueue<Action>();
    
    public GameObject ToastPrefab;
    private CanvasGroup _toast;
    private Text _toastInfo;
    private Toggle _toggleGetMessages;
    private Toggle _toggleGetMessagesByLastMsgId;
    private Toggle _toggleGetMessagesByLastMsgTime;
    private Toggle _toggleDeleteMessagesBeforeTime;

    private InputField _inputFieldTimeStamp;
    private InputField _inputFieldMessageId;

    public GameObject MessageListViewportContent;
    public GameObject MessageShortItem;
    private IList<GameObject> _messageGameObjects = new List<GameObject>();
    enum LoadMessageMode
    {
        Universal,
        ByLastMsgId,
        ByMsgSentTime,
        DeleteMsgBeforeTime,
    }

    private LoadMessageMode _loadMessageMode;
    
    // Start is called before the first frame update
    void Start()
    {
        _toast = GameObject.Find("/Toast/Toast").GetComponent<CanvasGroup>();
        _toastInfo = GameObject.Find("/Toast/Toast/Info").GetComponent<Text>();
        _toastInfo.text = "";
        _toast.alpha = 0;

        _toggleGetMessages = GameObject.Find("/Canvas/ToggleGroup/ToggleGetMessages").GetComponent<Toggle>();
        _toggleGetMessagesByLastMsgId =
            GameObject.Find("/Canvas/ToggleGroup/ToggleGetMessagesByLastMsgID").GetComponent<Toggle>();
        _toggleGetMessagesByLastMsgTime =
            GameObject.Find("/Canvas/ToggleGroup/ToggleGetMessagesByLastMsgTime").GetComponent<Toggle>();
        _toggleDeleteMessagesBeforeTime=
            GameObject.Find("/Canvas/ToggleGroup/ToggleDeleteMessagesBeforeTime").GetComponent<Toggle>();

        _inputFieldTimeStamp = GameObject.Find("/Canvas/InputFieldTimeStamp").GetComponent<InputField>();
        _inputFieldMessageId = GameObject.Find("/Canvas/InputFieldMessageId").GetComponent<InputField>();

        _inputFieldTimeStamp.text = Util.CurrentTimeStamp().ToString();
        _inputFieldMessageId.text = "-1";
        MessageListViewportContent = GameObject.Find("/Canvas/ScrollView/Viewport/Content");
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
    
    public void OnClickedGoHome()
    {
        SceneManager.LoadScene("ConversationPage", LoadSceneMode.Single);
    }
    
    public void OnClickConversationNotificationStatus()
    {
        RCIMClient.Instance.GetConversationNotificationStatus(DemoContext.CurrentConversationType, DemoContext.TargetId,
            (code, notifyStatus) =>
            {
                if (code != RCErrorCode.Succeed)
                {
                    print($"GetConversationNotificationStatus {code} {notifyStatus}{Environment.NewLine}");
                    return;
                }

                var newStatus = RCConversationNotificationStatus.Notify;
                if (notifyStatus == RCConversationNotificationStatus.Notify)
                {
                    newStatus = RCConversationNotificationStatus.DoNotDisturb;
                }

                RCIMClient.Instance.SetConversationNotificationStatus(DemoContext.CurrentConversationType,
                    DemoContext.TargetId, newStatus, (setConvNotifyCode, setStatus) =>
                    {
                        ShowToast($"设置会话通知状态: {setConvNotifyCode}, {setStatus}");
                    });
            });
    }

    public void OnClickClearMessageUnreadStatus()
    {
        RCIMClient.Instance.ClearMessageUnreadStatus(DemoContext.CurrentConversationType, DemoContext.TargetId,
            (code) => { ShowToast($"清除未读计数: {code} "); });
    }

    public void OnToggleGroupChanged()
    {
        Debug.Log("OnToggleGroupChanged");
        if (_toggleGetMessages.isOn)
        {
            _loadMessageMode = LoadMessageMode.Universal;
            _inputFieldTimeStamp.interactable = true;
            _inputFieldMessageId.interactable = false;
        }
        else if (_toggleGetMessagesByLastMsgId.isOn)
        {
            _loadMessageMode = LoadMessageMode.ByLastMsgId;
            _inputFieldTimeStamp.interactable = false;
            _inputFieldMessageId.interactable = true;
        }
        else if (_toggleGetMessagesByLastMsgTime.isOn)
        {
            _loadMessageMode = LoadMessageMode.ByMsgSentTime;
            _inputFieldTimeStamp.interactable = true;
            _inputFieldMessageId.interactable = false;
        }
        else if (_toggleDeleteMessagesBeforeTime.isOn)
        {
            _loadMessageMode = LoadMessageMode.DeleteMsgBeforeTime;
            _inputFieldTimeStamp.interactable = true;
            _inputFieldMessageId.interactable = false;
        }
    }

    private LoadMessageMode _lastLoadMode;
    private List<RCMessage> _messages = new List<RCMessage>();

    public void OnClickLoadMessage()
    {
        if (_lastLoadMode != _loadMessageMode)
        {
            _lastLoadMode = _loadMessageMode;
            _messages.Clear();
            ClearMessageList();
        }

        if (_loadMessageMode == LoadMessageMode.Universal)
        {
            var option = new RCHistoryMessageOption
            {
                DateTime = _messages.Count == 0 ? Int64.Parse(_inputFieldTimeStamp.text) : _messages.Last().SentTime,
                Count = 20,
                PullOrder = RCPullOrder.Des
            };
            Debug.Log($"GetMessages: {option.DateTime}");
            
            RCIMClient.Instance.GetMessages(DemoContext.CurrentConversationType, DemoContext.TargetId, option,
                (code, messageList) =>
                {
                    Debug.Log($"GetMessages: {code} {messageList?.Count}");
                    if (code != RCErrorCode.Succeed || messageList == null)
                        return;
                    _messages.AddRange(messageList);
                    AppendMessageList(messageList);
                });
        }
        else if (_loadMessageMode == LoadMessageMode.ByLastMsgId)
        {
            var lastMsgId = _messages.Count == 0 ? Int64.Parse(_inputFieldMessageId.text) : _messages.Last().MessageId;

            // 先加载本地消息
            RCIMClient.Instance.GetHistoryMessages(DemoContext.CurrentConversationType, DemoContext.TargetId, lastMsgId,
                20,
                (code, messageList) =>
                {
                    Debug.Log($"GetHistoryMessages by msgId {lastMsgId}: {code} {messageList?.Count}");
                    if (code != RCErrorCode.Succeed)
                        return;

                    if (messageList == null || messageList.Count == 0)
                    {
                        ShowToast($"没有更多本地消息，从远端加载");
                        var lastMsgSentTime = _messages.Count == 0 ? 0 : _messages.Last().SentTime;
                        // 本地消息加载完毕后，从远端服务器加载
                        RCIMClient.Instance.GetRemoteHistoryMessages(DemoContext.CurrentConversationType,
                            DemoContext.TargetId, lastMsgSentTime, 20,
                            (errorCode, remoteMessageList) =>
                            {
                                Debug.Log($"GetRemoteHistoryMessages: {errorCode} {remoteMessageList?.Count}");
                                if (errorCode != RCErrorCode.Succeed || remoteMessageList == null)
                                    return;

                                _messages.AddRange(remoteMessageList);
                                AppendMessageList(remoteMessageList);
                            });
                    }
                    else
                    {
                        _messages.AddRange(messageList);
                        AppendMessageList(messageList);
                    }
                });
        }
        else if (_lastLoadMode == LoadMessageMode.ByMsgSentTime)
        {
            var lastMsgSentTime = Int64.Parse(_inputFieldTimeStamp.text);

            RCIMClient.Instance.GetHistoryMessages(DemoContext.CurrentConversationType, DemoContext.TargetId,
                lastMsgSentTime,
                10, 10,
                (code, messageList) =>
                {
                    Debug.Log($"GetRemoteHistoryMessages: {code} {messageList?.Count}");
                    if (code != RCErrorCode.Succeed || messageList == null)
                        return;

                    _messages.AddRange(messageList);
                    AppendMessageList(messageList);
                });
        }
    }

    public void OnClickClearHistoryMessages()
    {
        Debug.Log($"{_loadMessageMode}");
        if (_loadMessageMode != LoadMessageMode.DeleteMsgBeforeTime)
        {
            return;
        }

        _lastLoadMode = _loadMessageMode;

        var toggleDeleteRemote = GameObject.Find("/Canvas/ToggleDeleteRemote").GetComponent<Toggle>();
        var dateTime = Int64.Parse(_inputFieldTimeStamp.text);
        RCIMClient.Instance.ClearHistoryMessages(DemoContext.CurrentConversationType, DemoContext.TargetId, dateTime,
            toggleDeleteRemote.isOn,
            code =>
            {
                ShowToast($"清除历史消息(清除远端{toggleDeleteRemote.isOn}) ：{code}");
                _messages.Clear();
                ClearMessageList();
            });
    }

    public void OnClickClearLocalMessages()
    {
        RCIMClient.Instance.ClearMessages(DemoContext.CurrentConversationType, DemoContext.TargetId, (code, result) =>
        {
            ShowToast($"清空历史消息：{code}");
            _messages.Clear();
            ClearMessageList();
        });
    }

    public void OnClickSearchMessages()
    {
        var keywordText = GameObject.Find("/Canvas/InputFieldKeyword").GetComponent<InputField>();
        if (String.IsNullOrEmpty(keywordText.text))
            return;

        RCIMClient.Instance.SearchMessages(DemoContext.CurrentConversationType, DemoContext.TargetId, keywordText.text,
            0, 0, (code, messageList) =>
            {
                _messages.Clear();
                ClearMessageList();

                Debug.Log($"SearchMessages: {code} {messageList?.Count}");
                ShowToast($"SearchMessages by {keywordText.text}：{code} {messageList?.Count}");
                if (code != RCErrorCode.Succeed || messageList == null)
                    return;

                _messages.AddRange(messageList);
                AppendMessageList(messageList);
            });
    }

    private void ClearMessageList()
    {
        RunOnMainThread.Enqueue(() =>
        {
            var children = (from Transform child in MessageListViewportContent.transform select child.gameObject).ToList();

            children.ForEach(Destroy);

            _messageGameObjects.Clear();
            var messageCountText = GameObject.Find("/Canvas/MessageCount").GetComponent<Text>();
            messageCountText.text = $"当前消息数量: {_messages?.Count}";
        });
    }

    private void AppendMessageList(IList<RCMessage> messages)
    {
        RunOnMainThread.Enqueue(() =>
        {
            if (messages == null) return;

            foreach (var item in messages)
            {
                AddTagListItem(item);
            }

            var messageCountText = GameObject.Find("/Canvas/MessageCount").GetComponent<Text>();
            messageCountText.text = $"消息数量: {_messages?.Count}";
        });
    }

    private void AddTagListItem(RCMessage tagInfo)
    {
        if (tagInfo == null) return;

        string msgContent = "";
        if (tagInfo.Content is RCUnknownMessage unknownMessage)
        {
            msgContent = "未知消息";
        }
        else if (tagInfo.Content is RCTextMessage textMessage)
        {
            msgContent = textMessage.Content;
        }
        else if (tagInfo.Content is RCImageMessage imageMessage)
        {
            msgContent = imageMessage.RemoteUrl;
        }
        else if (tagInfo.Content is RCVoiceMessage voiceMessage)
        {
            msgContent = $"音频消息 {voiceMessage.Duration}秒";
        }
        else if (tagInfo.Content is RCHQVoiceMessage hqVoiceMessage)
        {
            msgContent = $"高清音频 {hqVoiceMessage.Duration}秒 {hqVoiceMessage.MediaUrl}";
        }
        // else if (tagInfo.Content is RCLocationMessage locationMessage)
        // {
        //     msgContent = $"lat: {locationMessage.Latitude} lng: {locationMessage.Longitude}";
        // }
        else if (tagInfo.Content is RCSightMessage sightMessage)
        {
            msgContent = $"视频消息 {sightMessage.Duration} {sightMessage.MediaUrl}";
        }
        else if (tagInfo.Content is RCGifMessage gifMessage)
        {
            msgContent = $"gif {gifMessage.MediaUrl}";
        }
        else if (tagInfo.Content is RCFileMessage fileMessage)
        {
            msgContent =
                $"File {fileMessage.Name} type {fileMessage.Type} size {fileMessage.Size} {fileMessage.MediaUrl}";
        }
        else if (tagInfo.Content is RCReferenceMessage referenceMessage)
        {
            msgContent =
                $"content: {referenceMessage.Content} 原始消息 {referenceMessage.ReferredMsgObjectName} originMsgUid {referenceMessage.ReferredMsgUid}";
        }
        else if (tagInfo.Content is RCRichContentMessage richContentMessage)
        {
            msgContent = $"{richContentMessage.Title} {richContentMessage.Digest} {richContentMessage.Url}";
        }
        else if (tagInfo.Content is RCGroupNotificationMessage groupNotificationMessage)
        {
            msgContent =
                $"{groupNotificationMessage.OperatorUserId} {groupNotificationMessage.OperatorUserId} {groupNotificationMessage.Message}";
        }
        else if (tagInfo.Content is RCRecallCommandMessage recallCommandMessage)
        {
            msgContent =
                $"撤回命令消息, 对象{recallCommandMessage.ConversationType} {recallCommandMessage.TargetId} msgUid {recallCommandMessage.MessageUid}";
        }
        else if (tagInfo.Content is RCRecallNotificationMessage recallNotificationMessage)
        {
            msgContent =
                $"撤回通知消息，原始消息 {recallNotificationMessage.OriginalObjectName} {recallNotificationMessage.RecallContent}";
        }
        else
        {
            msgContent = tagInfo.Content?.ToString();
        }

        var item = GameObject.Instantiate(MessageShortItem, MessageListViewportContent.transform) as GameObject;
        item.transform.Find("MsgId").GetComponent<Text>().text = tagInfo.MessageId.ToString();
        item.transform.Find("MsgUId").GetComponent<Text>().text = tagInfo.MessageUId;
        item.transform.Find("MsgContent").GetComponent<Text>().text = msgContent;
        item.transform.Find("MsgObjectName").GetComponent<Text>().text = tagInfo.ObjectName;
        item.transform.Find("MsgSentTime").GetComponent<Text>().text =$"sentTime: {tagInfo.SentTime}";

        RectTransform rect = MessageListViewportContent.GetComponent<RectTransform>();
        RectTransform current = item.GetComponent<RectTransform>();
        if (_messageGameObjects.Count > 0)
        {
            RectTransform last = MessageListViewportContent.transform.GetChild(_messageGameObjects.Count - 1).gameObject
                .GetComponent<RectTransform>();
            item.GetComponent<RectTransform>().localPosition = new Vector3(last.localPosition.x,
                last.localPosition.y - last.rect.height - 10, last.localPosition.z);
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.rect.height + current.rect.height + 10);
        }
        else
        {
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, current.rect.height);
        }

        _messageGameObjects.Add(item);
    }

    private void ShowToast(string toast)
    {
        if (string.IsNullOrEmpty(toast))
            return;
        RunOnMainThread.Enqueue(async () =>
        {
            _toastInfo.text = toast;
            _toast.alpha = 1;
            await Task.Delay(3000);
            _toast.alpha = 0;
        });
    }
}
