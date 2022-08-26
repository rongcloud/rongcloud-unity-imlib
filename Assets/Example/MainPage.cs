using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using cn_rongcloud_im_unity;
using cn_rongcloud_im_unity_example;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class MainPage : MonoBehaviour
{
    private Text _toastText;
    private Text _connStatusText;
    public GameObject _messageListViewportContent;
    public GameObject ConversationItem;

    private static readonly ConcurrentQueue<Action> RunOnMainThread = new ConcurrentQueue<Action>();

    private IList<GameObject> _conversationList = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        _toastText = GameObject.Find("/ConversationList/Toast").GetComponent<Text>();
        _connStatusText = GameObject.Find("/ConversationList/ConnStatus").GetComponent<Text>();
        _messageListViewportContent = GameObject.Find("/ConversationList/ScrollViewer/Viewport/Content");

        RCIMClient.Instance.OnConnectionStatusChanged += OnConnectionStatusChanged;
        RCIMClient.Instance.OnMessageReceived += OnMessageReceived;
        RCIMClient.Instance.OnReadReceiptRequest += Instance_OnReadReceiptRequest;
//         RCIMClient.Instance.OnDownloadMediaMessageProgressed += OnDownloadMediaProgress;
//         RCIMClient.Instance.OnDownloadMediaMessageCanceled += OnDownloadMediaCanceled;
//         RCIMClient.Instance.OnDownloadMediaMessageFailed += OnDownloadMediaFailed;
//         RCIMClient.Instance.OnDownloadMediaMessageCompleted += OnDownloadMediaCompleted;
// #if UNITY_ANDROID
//         RCIMClient.Instance.OnDownloadMediaMessagePaused += OnDownloadMediaMessagePaused;
// #endif
        var currStatus = RCIMClient.Instance.GetConnectionStatus();
        OnConnectionStatusChanged(currStatus);
        LoadConversationList();
    }
    private void OnDestroy()
    {
        RCIMClient.Instance.OnConnectionStatusChanged -= OnConnectionStatusChanged;
        RCIMClient.Instance.OnMessageReceived -= OnMessageReceived;
        RCIMClient.Instance.OnReadReceiptRequest -= Instance_OnReadReceiptRequest;
//         RCIMClient.Instance.OnDownloadMediaMessageProgressed -= OnDownloadMediaProgress;
//         RCIMClient.Instance.OnDownloadMediaMessageCanceled -= OnDownloadMediaCanceled;
//         RCIMClient.Instance.OnDownloadMediaMessageFailed -= OnDownloadMediaFailed;
//         RCIMClient.Instance.OnDownloadMediaMessageCompleted -= OnDownloadMediaCompleted;
// #if UNITY_ANDROID
//         RCIMClient.Instance.OnDownloadMediaMessagePaused -= OnDownloadMediaMessagePaused;
// #endif
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

    public void OnClickStartConversation()
    {
        SceneManager.LoadScene("SendMessage", LoadSceneMode.Single);
    }
    
    public void OnClickedSearchConversation()
    {
        SceneManager.LoadScene("SearchConversation", LoadSceneMode.Single);
    }
    
    public void OnClickedGlobalSetting()
    {
        DemoContext.SetActiveScene("", null);
        SceneManager.LoadScene("GlobalSetting", LoadSceneMode.Single);
    }
    
    public void OnClickedTag()
    {
        DemoContext.SetActiveScene("", null);
        SceneManager.LoadScene("Tag", LoadSceneMode.Single);
    }

    private void LoadConversationList()
    {
        RCIMClient.Instance.GetConversationListByPage(
            (error, conversationList) =>
            {
                RefreshConversationList(conversationList);
            }, 0, 10,
            RCConversationType.ChatRoom,
            RCConversationType.Private,
            RCConversationType.Group);
    }

    private void RefreshConversationList(IList<RCConversation> conversationList)
    {
        RunOnMainThread.Enqueue(() =>
        {
            DemoContext.ConversationList = conversationList;

            var children = new List<GameObject>();
            foreach (Transform child in _messageListViewportContent.transform)
            {
                children.Add(child.gameObject);
            }

            children.ForEach(Destroy);

            _conversationList.Clear();
            if (conversationList == null) return;

            foreach (var item in conversationList)
            {
                AddConversationListItem(item);
            }
        });
    }

    public void OnClickLoadConversationList()
    {
        LoadConversationList();
    }

    public void OnClickGetBlockedConversation()
    {
        RCIMClient.Instance.GetBlockedConversationList((code, conversationList) =>
            {
                var toast =
                    $"GetBlockedConversationList called. {code} {conversationList?.Count} {Environment.NewLine}";

                if (code != RCErrorCode.Succeed)
                    return;
                RefreshConversationList(conversationList);

                ShowToast(toast);
            }, RCConversationType.Private,
            RCConversationType.Group, RCConversationType.ChatRoom);
    }

    public void OnClickGetConversationList()
    {
        var dropdownConversationType = GameObject.Find("/ConversationList/DropdownTargetType").GetComponent<Dropdown>();
        var type = RCConversationType.Private;
        if (0 == dropdownConversationType.value)
        {
            type = RCConversationType.Private;
        }
        else if (1 == dropdownConversationType.value)
        {
            type = RCConversationType.Group;
        }
        else if (2 == dropdownConversationType.value)
        {
            type = RCConversationType.ChatRoom;
        }
        RCIMClient.Instance.GetConversationList((error, conversationList) =>
            {
                if(error!= RCErrorCode.Succeed)
                    return;
                RefreshConversationList(conversationList);
            },
            type);
    }
    
    public void OnClickRemoveConversationByType()
    {
        var dropdownConversationType = GameObject.Find("/ConversationList/DropdownTargetType").GetComponent<Dropdown>();
        var type = RCConversationType.Private;
        if (0 == dropdownConversationType.value)
        {
            type = RCConversationType.Private;
        }
        else if (1 == dropdownConversationType.value)
        {
            type = RCConversationType.Group;
        }
        else if (2 == dropdownConversationType.value)
        {
            type = RCConversationType.ChatRoom;
        }

        RCIMClient.Instance.ClearConversations(code =>
        {
            if (code != RCErrorCode.Succeed)
                return;
            LoadConversationList();
        }, type);
    }

    private void AddConversationListItem(RCConversation conversation)
    {
        if (conversation == null) return;
        
        GameObject item = GameObject.Instantiate(ConversationItem, _messageListViewportContent.transform) as GameObject;
        item.transform.Find("Type").GetComponent<Text>().text = conversation.ConversationType.ToString();
        item.transform.Find("TargetId").GetComponent<Text>().text = conversation.TargetId;
        item.transform.Find("TopStatus").GetComponent<Text>().text = conversation.IsTop ? "已置顶" : "";
        item.transform.Find("UnreadCount").GetComponent<Text>().text = $"未读 {conversation.UnreadMessageCount}";

        if (conversation.ReceivedTime != 0)
        {
            item.transform.Find("Timestamp").GetComponent<Text>().text = Util.FormatTs(conversation.ReceivedTime);
        }
        else if (conversation.SentTime != 0)
        {
            item.transform.Find("Timestamp").GetComponent<Text>().text = Util.FormatTs(conversation.SentTime);
        }

        item.transform.Find("ToggleTopStatus").GetComponent<Button>().onClick.AddListener(() =>
        {
            RCIMClient.Instance.SetConversationToTop(conversation.ConversationType, conversation.TargetId,
                !conversation.IsTop,
                code =>
                {
                    if (code != RCErrorCode.Succeed) return;
                    LoadConversationList();
                });
        });
        item.transform.Find("Remove").GetComponent<Button>().onClick.AddListener(() =>
        {
            RCIMClient.Instance.RemoveConversation(conversation.ConversationType, conversation.TargetId,
                code =>
                {
                    if (code != RCErrorCode.Succeed) return;
                    LoadConversationList();
                });
        });
        item.transform.Find("enterConversation").GetComponent<Button>().onClick.AddListener(() =>
        {
            DemoContext.CurrentConversation = conversation;
            SceneManager.LoadScene("ConversationPage", LoadSceneMode.Single);
        });

        RectTransform rect = _messageListViewportContent.GetComponent<RectTransform>();
        RectTransform current = item.GetComponent<RectTransform>();
        if (_conversationList.Count > 0)
        {
            RectTransform last = _messageListViewportContent.transform.GetChild(_conversationList.Count - 1).gameObject
                .GetComponent<RectTransform>();
            item.GetComponent<RectTransform>().localPosition = new Vector3(last.localPosition.x,
                last.localPosition.y - last.rect.height - 10, last.localPosition.z);
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.rect.height + current.rect.height + 10);
        }
        else
        {
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, current.rect.height);
        }

        _conversationList.Add(item);
    }

    private void ShowToast(String tip)
    {
        if (String.IsNullOrEmpty(tip))
            return;
        RunOnMainThread.Enqueue(() => { _toastText.text = tip; });
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
    
    private void OnConnectionStatusChanged(RCConnectionStatus connectionStatus)
    {
        RunOnMainThread.Enqueue(() =>
        {
            _connStatusText.text = connectionStatus.ToString();
        });
    }

    private void OnMessageReceived(RCMessage message, int left)
    {
        LoadConversationList();

        var objectName = message.ObjectName;
        if (objectName == "RC:SightMsg")
        {
            if (DemoContext.exampleScene != null)
            {
                var example = DemoContext.exampleScene;
                // RunOnMainThread.Enqueue(() => { example.DownloadMediaMessage(message); });
            }
        }

        // 发送已读回执
        if (message.ConversationType == RCConversationType.Private)
        {
            RCConversationType type = message.ConversationType;
            var targetId = message.SenderUserId;
            var timeStamp = Util.CurrentTimeStamp();
            RCIMClient.Instance.SendReadReceiptMessage(type, targetId, timeStamp, null);
        }
    }
}
