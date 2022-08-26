using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using cn_rongcloud_im_unity;
using cn_rongcloud_im_unity_example;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MessageItem
{
    public GameObject item;
    public RCMessage Message { get; set; }
    public int idx { get; }

    public MessageItem(GameObject item, RCMessage rcMessage, int idx)
    {
        this.item = item;
        this.Message = rcMessage;
        this.idx = idx;
    }
}

public class ForwardParameter
{
    public RCConversationType type = RCConversationType.Group;
    public string targetId;
    public RCMessage Message { get; set; }
    public bool isInitialized;
}

public class ExpansionParameter
{
    public enum ExpansionAction
    {
        UPDATE,
        REMOVE
    };

    public RCMessage Message { get; set; }
    public Dictionary<string, string> ExpansionDictionary { get; set; }
    public List<string> Keys { get; set; }
    public ExpansionAction action { get; set; }

    public int idx;
    public bool readDicFlag;

    public ExpansionParameter()
    {
        ExpansionDictionary = new Dictionary<string, string>();
        Keys = new List<string>();
        action = ExpansionAction.UPDATE;
    }
}

public class ConversationPage : BaseScene
{
    #region Vars

    private static readonly string PRIVATE_PANEL = "panelPrivate";
    private static readonly string CHATROOM_PANEL = "panelChatRoom";
    private static readonly string GROUP_PANEL = "panelGroup";
    private static readonly string SEND_PANEL = "panelSend";

    private static readonly ConcurrentQueue<Action> RunOnMainThread = new ConcurrentQueue<Action>();
    public RCConversation CurrentConversation;
    private RCMessage LatestSendMessage;
    private RCMessage LatestReceiveMessage;
    private IList<RCMessage> _messages;
    private HashSet<RCMessage> _batchMessages; // 批量选择的消息集合，用于批量删除和插入消息

    private IList<RCMessage> _receiptRequestMsgList;

    // 最后一次获取消息的时间
    // android im sdk 5.1.3 bug, DateTime 首次应该传 0（sdk 注释说明），但是内部又将0判断为异常
    private Int64 _lastGetMessageTime = Util.CurrentTimeStamp() - 7 * 24 * 60 * 60 * 1000;

    // 最后一次获取消息的消息id
    private long _lastMessageId = -1;

    private string _currentTargetId = "sV2XrVzwG";

    private string sendMessageToast = String.Empty;

    private bool _canExpansionFlag = false;

    public GameObject MessagePanel;
    public GameObject SendPanel;
    public GameObject SendMessagePanel;
    public GameObject ConvPanelPrefab;
    public GameObject ChatRoomPanel;
    public GameObject PrivatePanel;

    private GameObject MessageTable;
    private int messageCount = 0;
    private Dictionary<int, MessageItem> MessagesDictionary;
    private Dictionary<string, GameObject> Controls;

    private GameObject SendPanelArea;
    private GameObject ConvPanelArea;


    private InputField MessageInputField;
    private InputField IdInputField;

    private Text eventStatusText;
    private Text convNtfText;

    private Canvas ForwardBox;
    private Canvas ExpansionBox;
    private Canvas SendCustMessageBox;
    private ForwardParameter forwardPara;
    private ExpansionParameter expansionPara;

    private string[] _allowedTypes;
 #endregion

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();


#if UNITY_IOS
        var pdfFileType =
            NativeFilePicker
                .ConvertExtensionToFileType("pdf"); // Returns "application/pdf" on Android and "com.adobe.pdf" on iOS
        Debug.Log("pdf's MIME/UTI is: " + pdfFileType);

        var mp3FileType = NativeFilePicker.ConvertExtensionToFileType("mp3");
        var amrFileType = NativeFilePicker.ConvertExtensionToFileType("amr");
        var aacFileType = NativeFilePicker.ConvertExtensionToFileType("aac");

        _allowedTypes = new string[] {"public.image", "public.movie", pdfFileType, mp3FileType, amrFileType, aacFileType};
#endif

        forwardPara = new ForwardParameter();
        expansionPara = new ExpansionParameter();

        ForwardBox = GameObject.Find("/ForwardBox").GetComponent<Canvas>();
        ForwardBox.gameObject.SetActive(true);

        SendCustMessageBox = GameObject.Find("/CustomMessage").GetComponent<Canvas>();
        SendCustMessageBox.gameObject.SetActive(true);
        ExpansionBox = GameObject.Find("/ExpansionBox").GetComponent<Canvas>();
        ExpansionBox.gameObject.SetActive(true);
        
        RunOnMainThread.Enqueue(() =>
        {
            ForwardBox.gameObject.SetActive(false);
            SendCustMessageBox.gameObject.SetActive(false);
            ExpansionBox.gameObject.SetActive(false);
        });

        _receiptRequestMsgList = new List<RCMessage>();
        _batchMessages = new HashSet<RCMessage>();

        eventStatusText = GameObject.FindWithTag("EventStatusText").GetComponent<Text>();
        eventStatusText.text = "";
        convNtfText = GameObject.Find("/Canvas/ConvNtfText").GetComponent<Text>();
        convNtfText.text = string.Empty;
        MessageTable = GameObject.Find("/Canvas/MessageArea/Viewport/Content");
        SendPanelArea = GameObject.Find("/Canvas/SendMessageArea/Viewport/Content");
        ConvPanelArea = GameObject.Find("/Canvas/ConvPanelArea/Viewport/Content");

        MessagesDictionary = new Dictionary<int, MessageItem>();
        Controls = new Dictionary<string, GameObject>();

        CurrentConversation = DemoContext.CurrentConversation;
        DisplayConvType(DemoContext.CurrentConversationType);
        DisplayTargetId(DemoContext.TargetId);

        DemoContext.SetActiveScene("ConversationPage", this);
        DemoContext.conversationPageScene = this;

        initControls();

        RCIMClient.Instance.OnSendMessageAttached += Instance_OnSendMessageAttached;
        RCIMClient.Instance.OnSendMessageSucceed += OnIMMessageSendSucceed;
        RCIMClient.Instance.OnSendMessageFailed += Instance_OnSendMessageFailed;
        RCIMClient.Instance.OnTypingStatusChanged += Instance_OnTypingStatusChanged;
        RCIMClient.Instance.OnReadReceiptRequest += OnReadReceiptRequest;
        RCIMClient.Instance.OnReadReceiptReceived += OnReadReceiptReceived;
        RCIMClient.Instance.OnReadReceiptResponse += OnReadReceiptResponse;
        RCIMClient.Instance.OnMessageExpansionUpdated += OnMessageExpansionUpdated;
        RCIMClient.Instance.OnMessageExpansionRemoved += OnMessageExpansionRemoved;
        RCIMClient.Instance.OnMessageReceived += OnMessageReceived;
        RCIMClient.Instance.OnMessageRecalled += OnMessageRecalled;

        if (CurrentConversationType() == RCConversationType.ChatRoom)
        {
            RCIMClient.Instance.OnChatRoomKVSync += OnChatRoomKVSync;
            RCIMClient.Instance.OnChatRoomKVUpdate += OnChatRoomKVUpdate;
            RCIMClient.Instance.OnChatRoomKVRemove += OnChatRoomKVRemove;

            RCIMClient.Instance.OnChatRoomJoining += (roomId) => { };
            RCIMClient.Instance.OnChatRoomJoined += (roomId) => { };
            RCIMClient.Instance.OnChatRoomQuited += (roomId) => { };
            RCIMClient.Instance.OnChatRoomReset += (roomId) => { };
            RCIMClient.Instance.OnChatRoomDestroyed += (roomId, destroyType) => { };
            RCIMClient.Instance.OnChatRoomError += (roomId, code) => { };
        }

        JoinRoom();
        GetConversationInfo();
        GetTextDraft();
        GetBlacklistStatus();
        GetConvNotifyStatus();
        OnClickRefreshMessages();
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

        if (ForwardBox != null && ForwardBox.isActiveAndEnabled)
        {
            if (!forwardPara.isInitialized)
            {
                forwardPara.isInitialized = true;
                ForwardBox.transform.Find("Content/Panel/TargetInputField").GetComponent<InputField>().text =
                    CurrentTargetId();
            }
        }

        if (ExpansionBox != null && ExpansionBox.isActiveAndEnabled)
        {
            bool isRemove = ExpansionBox.transform.Find("Content/Panel/Toggle").GetComponent<Toggle>().isOn;
            if (isRemove)
            {
                ExpansionBox.transform.Find("Content/Panel/KeylistButton").GetComponent<Button>().interactable = true;
                ExpansionBox.transform.Find("Content/Panel/AddKeyValueButton").GetComponent<Button>().interactable =
                    false;
            }
            else
            {
                ExpansionBox.transform.Find("Content/Panel/KeylistButton").GetComponent<Button>().interactable = false;
                ExpansionBox.transform.Find("Content/Panel/AddKeyValueButton").GetComponent<Button>().interactable =
                    true;
            }

            if (expansionPara.readDicFlag)
            {
                expansionPara.readDicFlag = false;
                StringBuilder sb = new StringBuilder();
                var msg = expansionPara.Message;
                if (msg.ExpansionDic != null && msg.ExpansionDic.Count > 0)
                {
                    foreach (var keyValuePair in msg.ExpansionDic)
                    {
                        sb.Append($"{keyValuePair.Key},{keyValuePair.Value}|");
                    }
                }

                ExpansionBox.transform.Find("Content/Panel/DicText").GetComponent<Text>().text = sb.ToString();
            }
        }
    }

    private void OnDestroy()
    {
        RCIMClient.Instance.OnSendMessageAttached -= Instance_OnSendMessageAttached;
        RCIMClient.Instance.OnSendMessageSucceed -= OnIMMessageSendSucceed;
        RCIMClient.Instance.OnSendMessageFailed -= Instance_OnSendMessageFailed;
        RCIMClient.Instance.OnTypingStatusChanged -= Instance_OnTypingStatusChanged;
        RCIMClient.Instance.OnReadReceiptRequest -= OnReadReceiptRequest;
        RCIMClient.Instance.OnReadReceiptReceived -= OnReadReceiptReceived;
        RCIMClient.Instance.OnReadReceiptResponse -= OnReadReceiptResponse;
        RCIMClient.Instance.OnMessageExpansionUpdated -= OnMessageExpansionUpdated;
        RCIMClient.Instance.OnMessageExpansionRemoved -= OnMessageExpansionRemoved;
        RCIMClient.Instance.OnMessageReceived -= OnMessageReceived;
        RCIMClient.Instance.OnMessageRecalled -= OnMessageRecalled;

        if (CurrentConversationType() == RCConversationType.ChatRoom)
        {
            RCIMClient.Instance.OnChatRoomKVSync -= OnChatRoomKVSync;
            RCIMClient.Instance.OnChatRoomKVUpdate -= OnChatRoomKVUpdate;
            RCIMClient.Instance.OnChatRoomKVRemove -= OnChatRoomKVRemove;
        }
        QuitRoom();
    }

    private void updateChatRoomJoinQuitButton()
    {
        if (CurrentConversationType() != RCConversationType.ChatRoom)
        {
            return;
        }

        RunOnMainThread.Enqueue(() =>
        {
            var joinQuitText = GameObject.Find("/ChatRoom/JoinQuitRoom/Text").GetComponent<Text>();
            joinQuitText.text = ChatRoomState.IsJoined ? "离开聊天室" : "进入聊天室";
        });
    }

    private void DisplayConvType(RCConversationType type)
    {
        Text convType = GameObject.Find("/Canvas/ConversationType").GetComponent<Text>();
        if (type == RCConversationType.Group)
        {
            convType.text = "群聊";
        }
        else if (type == RCConversationType.Private)
        {
            convType.text = "私聊";
        }
        else if (type == RCConversationType.ChatRoom)
        {
            convType.text = "聊天室";
        }
    }

    private void DisplayTargetId(string targetId)
    {
        Text targetText = GameObject.Find("/Canvas/TargetId").GetComponent<Text>();
        targetText.text = targetId;
    }

    RCConversationType CurrentConversationType()
    {
        return DemoContext.CurrentConversationType;
    }

    private string CurrentTargetId()
    {
        return DemoContext.TargetId;
    }

    private void initControls()
    {
        _initConvPanel();
        _initSendPanel();
    }

    private void enabledReceiptRequestButton(bool enabled)
    {
        Transform convPanel = ConvPanelArea.transform.Find(GROUP_PANEL);
        convPanel.Find("ReceiptRequest").GetComponent<Button>().interactable = enabled;
    }

    private void enabledReceiptResponseButton(bool enabled)
    {
        Transform convPanel = ConvPanelArea.transform.Find(GROUP_PANEL);
        convPanel.Find("ReceiptResponse").GetComponent<Button>().interactable = enabled;
    }

    private void _initConvPanel()
    {
        if (CurrentConversation.ConversationType == RCConversationType.Group)
        {
            _CreateGroupPanel();
        }
        else if (CurrentConversation.ConversationType == RCConversationType.Private)
        {
            _CreatePrivatePanel();
        }
        else if (CurrentConversation.ConversationType == RCConversationType.ChatRoom)
        {
            _CreateChatRoomPanel();
        }
        else
        {
            throw new NotSupportedException();
        }
    }

    private void _CreateGroupPanel()
    {
        Transform convPanel;

        if (!Controls.ContainsKey(GROUP_PANEL))
        {
            GameObject item = GameObject.Instantiate(ConvPanelPrefab, ConvPanelArea.transform) as GameObject;
            item.name = GROUP_PANEL;

            Controls.Add(GROUP_PANEL, item);
            convPanel = item.transform;
        }
        else
        {
            convPanel = ConvPanelArea.transform.Find(GROUP_PANEL);
        }

        IdInputField = convPanel.Find("IdInputField").GetComponent<InputField>();

        Button GetMessage = convPanel.Find("GetMessageById").GetComponent<Button>();
        GetMessage.onClick.AddListener(OnClickGetMessageById);

        Button GetMessageByUid = convPanel.Find("GetMessageByUid").GetComponent<Button>();
        GetMessageByUid.onClick.AddListener(OnClickGetMessageByUid);

        Button ReceiptResponse = convPanel.Find("ReceiptResponse").GetComponent<Button>();
        ReceiptResponse.onClick.AddListener(OnReceiptResponse);
        ReceiptResponse.interactable = false;

        Button ReceiptRequest = convPanel.Find("ReceiptRequest").GetComponent<Button>();
        ReceiptRequest.onClick.AddListener(OnReceiptRequest);
        ReceiptRequest.interactable = false;


        Button BatchInsert = convPanel.Find("BatchInsert").GetComponent<Button>();
        BatchInsert.onClick.AddListener(OnClickInsertBatchMessages);

        Button InsertIncoming = convPanel.Find("InsertIncoming").GetComponent<Button>();
        InsertIncoming.onClick.AddListener(OnClickInsertIncomingMessage);

        Button InsertOutgoing = convPanel.Find("InsertOutgoing").GetComponent<Button>();
        InsertOutgoing.onClick.AddListener(OnClickInsertOutgoingMessage);

        _refreshConvPanel();

        Button BatchDelete = convPanel.Find("BatchDelete").GetComponent<Button>();
        BatchDelete.onClick.AddListener(() =>
        {
            if (_batchMessages.Count > 0)
            {
                var msgIds = new List<long>();
                foreach (var rcMessage in _batchMessages)
                {
                    msgIds.Add(rcMessage.MessageId);
                }

                DeleteMessagesWithMsgIdArray(msgIds.ToArray());
            }
            else
            {
                ShowToast("请先选择消息");
            }
        });
        
        Button deleteRemoteButton = convPanel.Find("BatchDeleteRemote").GetComponent<Button>();
        deleteRemoteButton.onClick.AddListener(() =>
        {
            if (_batchMessages.Count > 0)
            {
                var msgIds = new List<RCMessage>();
                foreach (var rcMessage in _batchMessages)
                {
                    msgIds.Add(rcMessage);
                }

                DeleteRemoteMessages(msgIds);
            }
            else
            {
                ShowToast("请先选择消息");
            }
        });
    }

    private void _CreatePrivatePanel()
    {
        Transform convPanel;

        if (!Controls.ContainsKey(PRIVATE_PANEL))
        {
            GameObject item = GameObject.Instantiate(PrivatePanel, ConvPanelArea.transform) as GameObject;
            item.name = PRIVATE_PANEL;

            Controls.Add(PRIVATE_PANEL, item);
            convPanel = item.transform;
        }
        else
        {
            convPanel = ConvPanelArea.transform.Find(PRIVATE_PANEL);
        }

        Button BlockTarget = convPanel.Find("BlockTarget").GetComponent<Button>();
        BlockTarget.onClick.AddListener(OnClickToggleBlackStatus);

        InputField typingInputField = convPanel.Find("TypingInputField").GetComponent<InputField>();
        Button SendTyping = convPanel.Find("SendTyping").GetComponent<Button>();
        SendTyping.onClick.AddListener(() =>
        {
            var typing = typingInputField.text;
            if (string.IsNullOrEmpty(typing))
            {
                typing = "正在输入消息...";
            }

            this.SendTyping(typing);
        });
    }

    private void _CreateChatRoomPanel()
    {
        Transform convPanel;

        if (!Controls.ContainsKey(CHATROOM_PANEL))
        {
            GameObject item = GameObject.Instantiate(ChatRoomPanel, ConvPanelArea.transform) as GameObject;
            item.name = CHATROOM_PANEL;

            Controls.Add(CHATROOM_PANEL, item);
            convPanel = item.transform;
        }
        else
        {
            convPanel = ConvPanelArea.transform.Find(CHATROOM_PANEL);
        }

        Text joinOrQuitText = convPanel.Find("JoinOrQuitChatRoom/Text").GetComponent<Text>();
        if (ChatRoomState.IsJoined)
        {
            joinOrQuitText.text = "离开聊天室";
        }
        else
        {
            joinOrQuitText.text = "进入聊天室";
        }

        convPanel.Find("JoinOrQuitChatRoom").GetComponent<Button>().onClick.AddListener(OnClickJoinQuitRoom);

        InputField keyInputField = convPanel.Find("EntryKeyInputField").GetComponent<InputField>();
        InputField valueInputField = convPanel.Find("EntryValueInputField").GetComponent<InputField>();
        convPanel.Find("SetChatRoomEntry").GetComponent<Button>().onClick.AddListener(() =>
        {
            if (!CheckJoinedRoom()) return;

            var chatRoomKey = keyInputField.text;
            var chatRoomValue = valueInputField.text;
            if (string.IsNullOrEmpty(chatRoomKey) || string.IsNullOrEmpty(chatRoomValue))
            {
                print($"请输入Entry的键值");
                return;
            }

            RCIMClient.Instance.SetChatRoomEntry(CurrentTargetId(), chatRoomKey, chatRoomValue, false, false, "",
                (errCode) =>
                {
                    print($"SetChatRoomEntry :{errCode} forKey:{chatRoomKey}{Environment.NewLine}");
                    if (errCode == RCErrorCode.Succeed)
                    {
                        RunOnMainThread.Enqueue(() =>
                        {
                            keyInputField.text = string.Empty;
                            valueInputField.text = string.Empty;
                        });
                    }
                });
        });

        convPanel.Find("QueryChatRoomEntry").GetComponent<Button>().onClick.AddListener(() =>
        {
            if (!CheckJoinedRoom()) return;

            var chatRoomEntryKey = keyInputField.text;
            if (string.IsNullOrEmpty(chatRoomEntryKey))
            {
                ShowToast("请输入Key");
                return;
            }

            RCIMClient.Instance.GetChatRoomEntry(CurrentTargetId(), chatRoomEntryKey, (errCode, entry) =>
            {
                print($"GetChatRoomEntry : {errCode}");
                if (errCode == RCErrorCode.Succeed && entry != null)
                {
                    RunOnMainThread.Enqueue(() =>
                    {
                        foreach (var item in entry)
                        {
                            print($"GetChatRoomEntry: {item.Key} => {item.Value}{Environment.NewLine}");
                        }
                    });
                }
            });
        });

        convPanel.Find("DeleteChatRoomEntry").GetComponent<Button>().onClick.AddListener(() =>
        {
            if (!CheckJoinedRoom()) return;

            var chatRoomKey = keyInputField.text;
            if (string.IsNullOrEmpty(chatRoomKey))
            {
                ShowToast("请输入Key值");
                return;
            }

            RCIMClient.Instance.RemoveChatRoomEntry(CurrentTargetId(), chatRoomKey, false, "",
                (errCode) => { print($"RemoveChatRoomEntry :{errCode} forKey:{chatRoomKey}{Environment.NewLine}"); });
        });

        convPanel.Find("QueryKVList").GetComponent<Button>().onClick.AddListener(() =>
        {
            if (!CheckJoinedRoom()) return;

            RCIMClient.Instance.GetChatRoomAllEntries(CurrentTargetId(), (errCode, AllEntries) =>
            {
                print($"GetChatRoomAllEntries :{errCode},{AllEntries}{Environment.NewLine}", true);
                if (errCode == RCErrorCode.Succeed && AllEntries != null)
                {
                    string output = "";
                    foreach (var entry in AllEntries)
                    {
                        output += $"entry:{entry.Key} => {entry.Value}{Environment.NewLine}";
                    }

                    print(output);
                }
            });
        });

        convPanel.Find("GetChatRoomInfo").GetComponent<Button>().onClick.AddListener(() =>
        {
            if (!CheckJoinedRoom()) return;

            var order = RCChatRoomMemberOrder.Desc;
            RCIMClient.Instance.GetChatRoomInfo(CurrentTargetId(), 5, order, (errCode, chatRoomInfo) =>
            {
                string output = $"GetChatRoomInfo [5,{order}]: {errCode}, {chatRoomInfo}{Environment.NewLine}";
                if (errCode == RCErrorCode.Succeed && chatRoomInfo != null)
                {
                    foreach (var m in chatRoomInfo.Members)
                    {
                        output += $"GetChatRoomInfo: {m.UserId}, {Util.FormatTs(m.JoinTime)}{Environment.NewLine}";
                    }

                    print(output);
                }
            });
        });
    }

    private void _refreshConvPanel()
    {
    }

    private void _initSendPanel()
    {
        Transform sendPanel;
        if (!Controls.ContainsKey(SEND_PANEL))
        {
            GameObject item = GameObject.Instantiate(SendPanel, SendPanelArea.transform) as GameObject;
            item.name = SEND_PANEL;
            Controls.Add(SEND_PANEL, item);
            sendPanel = item.transform;
        }
        else
        {
            sendPanel = SendPanelArea.transform.Find(SEND_PANEL);
        }

        MessageInputField = sendPanel.Find("InputField").GetComponent<InputField>();

        Button sendButton = sendPanel.Find("SendButton").GetComponent<Button>();
        sendButton.onClick.AddListener(OnClickSendTextMessage);

        Button IMG = sendPanel.Find("BtnImage").GetComponent<Button>();
        IMG.onClick.AddListener(OnClickSendImageMessage);

        Button GIF = sendPanel.Find("BtnGif").GetComponent<Button>();
        GIF.onClick.AddListener(OnClickSendGifMessage);

        Button VIDEO = sendPanel.Find("BtnVideo").GetComponent<Button>();
        VIDEO.onClick.AddListener(OnClickSendSightMessage);

        Button FILE = sendPanel.Find("BtnFile").GetComponent<Button>();
        FILE.onClick.AddListener(OnClickSendFileMessage);

        // Button LOCATION = sendPanel.Find("BtnLocation").GetComponent<Button>();
        // LOCATION.onClick.AddListener(OnClickSendLocationMessage);

        Button VOICE = sendPanel.Find("BtnVoice").GetComponent<Button>();
        VOICE.onClick.AddListener(OnClickSendVoiceMessage);

        Button HQVOICE = sendPanel.Find("BtnHqVoice").GetComponent<Button>();
        HQVOICE.onClick.AddListener(OnClickSendHQVoiceMessage);
        
        Button CustMessage = sendPanel.Find("BtnCustMessage").GetComponent<Button>();
        CustMessage.onClick.AddListener(OnClickSendCustomMessage);
    }

    private string _contentOfMsg(RCMessage rcMessage)
    {
        if (rcMessage.Content is RCMediaMessageContent)
        {
            var mediaContent = rcMessage.Content as RCMediaMessageContent;
            return mediaContent.MediaUrl;
        }
        else if (rcMessage.Content is RCTextMessage)
        {
            RCTextMessage textMessage = rcMessage.Content as RCTextMessage;
            return $"{textMessage.Content}{Environment.NewLine}[{rcMessage.MessageId},{rcMessage.MessageUId}]";
        }
        else if (rcMessage.Content is RCRecallNotificationMessage recallNotificationMessage)
        {
            return $"消息撤回通知消息，原始消息{recallNotificationMessage?.OriginalObjectName}";
        }
        else
        {
            return rcMessage.ToString();
        }
    }

    private string _propertyOfMsg(RCMessage rcMessage)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"sentStatus:{rcMessage.SentStatus}, ");
        sb.Append($"ReceivedStatus:");
        if (rcMessage.ReceivedStatus.IsRead) sb.Append("isRead|");
        if (rcMessage.ReceivedStatus.IsDownloaded) sb.Append("isDownloaded|");
        if (rcMessage.ReceivedStatus.IsListened) sb.Append("isListened|");
        if (rcMessage.ReceivedStatus.IsRetrieved) sb.Append("isRetrieved|");
        if (rcMessage.ReceivedStatus.IsMultipleReceived) sb.Append("isMultipleReceived|");

        var receiptInfo = rcMessage.ReadReceiptInfo;

        if (receiptInfo != null)
        {
            if (receiptInfo.HasRespond && receiptInfo.RespondUserIdList?.Count > 0)
            {
                sb.Append("Receipt: ");
                foreach (var UserAndIntPair in receiptInfo.RespondUserIdList)
                {
                    sb.Append($"({UserAndIntPair.Key},{UserAndIntPair.Value}) ");
                }
            }
        }

        return sb.ToString();
    }

    private void _setLastMessage(ref RCMessage msg)
    {
        _lastGetMessageTime = msg.ReceivedTime;
        _lastMessageId = msg.MessageId;
        LatestReceiveMessage = msg;
    }

    private bool _checkConversationInfo()
    {
        if (DemoContext.CurrentConversation == null)
        {
            ShowToast("未指定会话信息");
            return false;
        }

        return true;
    }

    private void _clearOutputText()
    {
        eventStatusText.text = string.Empty;
    }

    private bool CheckJoinedRoom()
    {
        if (!ChatRoomState.IsJoined)
        {
            ShowToast("请先进入聊天室");
            return false;
        }

        return true;
    }

    private void JoinRoom()
    {
        if (CurrentConversationType() != RCConversationType.ChatRoom) return;
        if (ChatRoomState.IsJoined) return;

        Transform convPanel = ConvPanelArea.transform.Find(CHATROOM_PANEL);
        Text joinOrQuitText = convPanel.Find("JoinOrQuitChatRoom/Text").GetComponent<Text>();

        RCIMClient.Instance.JoinChatRoom(CurrentTargetId(), (errCode) =>
        {
            print($"JoinChatRoom: {errCode}");
            if (errCode == RCErrorCode.Succeed)
            {
                ChatRoomState.IsJoined = true;
                RunOnMainThread.Enqueue(() => { joinOrQuitText.text = "离开聊天室"; });
            }
        });
    }

    private void QuitRoom()
    {
        if (CurrentConversationType() != RCConversationType.ChatRoom) return;
        if (!ChatRoomState.IsJoined) return;
        RCIMClient.Instance.QuitChatRoom(CurrentTargetId(), (errCode) =>
        {
            print($"JoinChatRoom: {errCode}");
            if (errCode == RCErrorCode.Succeed)
            {
                ChatRoomState.IsJoined = false;
            }
        });
    }

    private static void RequestStoragePermission()
    {
        var permission = NativeFilePicker.CheckPermission(true);
        if (permission != NativeFilePicker.Permission.Granted)
        {
            NativeFilePicker.RequestPermission();
        }
    }

    public override void print(string output, bool clearScreen = true)
    {
        RunOnMainThread.Enqueue(() => { eventStatusText.text = output; });
    }

    private void ClearDisplayScreen()
    {
        var children = new List<GameObject>();
        foreach (Transform child in MessageTable.transform) children.Add(child.gameObject);
        children.ForEach(Destroy);

        MessagesDictionary.Clear();
        messageCount = 0;
    }

    private void ClearDisplayMessages()
    {
        _lastGetMessageTime = Util.CurrentTimeStamp() - 7 * 24 * 60 * 60 * 1000;
        _lastMessageId = -1;
        ClearDisplayScreen();
        _refreshConvPanel();
    }

    private void UpdateMessageTable(bool refresh = true)
    {
        _clearOutputText();
        if (CurrentConversationType() == RCConversationType.ChatRoom)
        {
            GetChatRoomHistoryMessages(refresh);
        }
        else
        {
            GetMessagesWithOption(refresh);
        }
    }

    private void DeleteMessagesWithMsgIdArray(long[] msgIdArray)
    {
        RCIMClient.Instance.DeleteMessages(msgIdArray,
            (code, succeed) =>
            {
                StringBuilder sb = new StringBuilder();
                foreach (var l in msgIdArray)
                {
                    sb.AppendFormat($"{l},");
                }

                if (code == RCErrorCode.Succeed && succeed)
                {
                    RunOnMainThread.Enqueue(() =>
                    {
                        ClearDisplayMessages();
                        UpdateMessageTable();
                    });
                }

                RunOnMainThread.Enqueue(() =>
                {
                    string output = sb.ToString();
                    print($"DeleteMessages [{output}]: {code},{succeed}{Environment.NewLine}");
                });
            });
    }

    private void DeleteRemoteMessages(List<RCMessage> messages)
    {
        if (messages == null || messages.Count == 0)
            return;

        RCIMClient.Instance.DeleteRemoteMessages(CurrentConversationType(), CurrentTargetId(), messages,
            (code) =>
            {
                if (code == RCErrorCode.Succeed)
                {
                    RunOnMainThread.Enqueue(() =>
                    {
                        ClearDisplayMessages();
                        UpdateMessageTable();
                    });
                }
            });
    }

    private void ClearHistoryMessagesWithTimestamp(Int64 timeStamp)
    {
        var type = CurrentConversationType();
        var targetId = CurrentTargetId();
        RCIMClient.Instance.ClearHistoryMessages(type, targetId, timeStamp,
            true, (code) =>
            {
                print($"ClearHistoryMessages [{type},{targetId}]: {code}{Environment.NewLine}");
                if (code == RCErrorCode.Succeed)
                {
                    RunOnMainThread.Enqueue(() =>
                    {
                        ClearDisplayMessages();
                        UpdateMessageTable();
                    });
                }
            });
    }

    private void ClearMessagesWithTypeAndTargetId()
    {
        var type = CurrentConversationType();
        var targetId = CurrentTargetId();
        RCIMClient.Instance.ClearMessages(type, targetId,
            (code, succeed) =>
            {
                print($"ClearMessages [{type},{targetId}]: {code},{succeed}{Environment.NewLine}");
                if (code == RCErrorCode.Succeed && succeed)
                {
                    RunOnMainThread.Enqueue(() =>
                    {
                        ClearDisplayMessages();
                        UpdateMessageTable();
                    });
                }
            });
    }

    private void ClearMessageUnreadStatus()
    {
        var curConv = CurrentConversation;
        if (curConv == null) return;

        RCIMClient.Instance.ClearMessageUnreadStatus(curConv.ConversationType, curConv.TargetId,
            (code) => { print($"ClearMessageUnreadStatus: {curConv.ConversationType},{curConv.TargetId},{code} "); });
    }

    public void OnContext()
    {
        if (CurrentConversationType() == RCConversationType.ChatRoom)
        {
            SceneManager.LoadScene("ChatRoom", LoadSceneMode.Single);
        }
        else
        {
            DemoContext.SetActiveScene("", null);
            SceneManager.LoadScene("ConversationSetting", LoadSceneMode.Single);
        }
    }

    private void GetRemoteHistoryMessagesWithStampCount()
    {
        RCIMClient.Instance.GetRemoteHistoryMessages(CurrentConversationType(), CurrentTargetId(),
            _lastGetMessageTime,
            10, (code, messageList) =>
            {
                _messages = messageList;
                if (_messages.Count > 0)
                {
                    var msg = _messages.Last();
                    _setLastMessage(ref msg);
                    DisplayMessages(ref _messages);
                }

                if (code != RCErrorCode.Succeed)
                {
                    print($"err:{code}");
                }

                RunOnMainThread.Enqueue(_refreshConvPanel);
            });
    }

    private void GetChatRoomHistoryMessages(bool refresh = false)
    {
        long recordTime = refresh ? Util.CurrentTimeStamp() : _lastGetMessageTime;
        RCIMClient.Instance.GetHistoryMessages(CurrentConversationType(), CurrentTargetId(), recordTime, 10, 0,
            (errCode, result) =>
            {
                if (errCode == RCErrorCode.Succeed)
                {
                    var messages = result;

                    if (messages?.Count > 0)
                    {
                        _messages = messages;
                        var msg = messages.Last();
                        _setLastMessage(ref msg);
                        DisplayMessages(ref _messages);
                    }
                }

                print($"GetChatRoomHistoryMessages: {errCode}");
            });
    }

    private void GetMessagesWithOption(bool refresh = false)
    {
        var option = new RCHistoryMessageOption()
        {
            DateTime = refresh ? Util.CurrentTimeStamp() : _lastGetMessageTime,
            Count = 20,
            PullOrder = RCPullOrder.Des
        };
        RCIMClient.Instance.GetMessages(CurrentConversationType(), CurrentTargetId(), option,
            (code, messageList) =>
            {
                if (code != RCErrorCode.Succeed)
                {
                    print($"err:{code}");
                }

                if (messageList?.Count > 0)
                {
                    _messages = messageList;
                    var msg = messageList.Last();
                    _setLastMessage(ref msg);
                    DisplayMessages(ref _messages);
                }

                RunOnMainThread.Enqueue(_refreshConvPanel);
            });
    }

    public void TestClear()
    {
        ClearDisplayScreen();
    }

    private void GetConvNotifyStatus()
    {
        RCIMClient.Instance.GetConversationNotificationStatus(CurrentConversationType(), CurrentTargetId(),
            (code, notifyStatus) =>
            {
                if (code != RCErrorCode.Succeed)
                {
                    print($"GetConversationNotificationStatus {code} {notifyStatus}{Environment.NewLine}");
                }

                RunOnMainThread.Enqueue(() =>
                {
                    if (notifyStatus == RCConversationNotificationStatus.Notify)
                    {
                        convNtfText.text = string.Empty;
                    }
                    else
                    {
                        convNtfText.text = "免打扰";
                    }
                });
            });
    }

    private long _GetFirstMessageId(IList<RCMessage> msgList)
    {
        var msg = msgList.First();
        if (msg == null) return -1;
        return msg.MessageId;
    }

    private long _GetTimestamp(RCMessage msg)
    {
        if (msg.Direction == RCMessageDirection.Receive)
            return msg.ReceivedTime;
        else return msg.SentTime;
    }

    private void GetHistoryMessagesWithLastIdCount()
    {
        RCIMClient.Instance.GetHistoryMessages(CurrentConversationType(), CurrentTargetId(),
            -1, 20,
            (code, messageList) =>
            {
                if (code == RCErrorCode.Succeed)
                {
                    if (messageList.Count > 0)
                    {
                        long firstTimestamp = 0;
                        var firstMsg = _messages.First();
                        if (firstMsg != null)
                        {
                            firstTimestamp = _GetTimestamp(firstMsg);
                        }

                        var updateMsgs = new List<RCMessage>();
                        var msg = messageList.Last();
                        if (_GetTimestamp(msg) < firstTimestamp)
                        {
                            updateMsgs.AddRange(messageList);
                            updateMsgs.AddRange(_messages);
                        }
                        else
                        {
                            return;
                        }

                        RunOnMainThread.Enqueue(ClearDisplayScreen);
                        _messages = updateMsgs;
                        DisplayMessages(ref _messages);
                    }
                }
                else
                {
                    print($"拉取消息失败。{code}");
                }
            });
    }

    private void GetHistoryMessagesWithTimestampBeforeAfter()
    {
        RCIMClient.Instance.GetHistoryMessages(CurrentConversationType(), CurrentTargetId(),
            _lastGetMessageTime, 10,
            10, (code, messageList) =>
            {
                _messages = messageList;
                if (_messages.Count > 0)
                {
                    var msg = _messages.Last();
                    _setLastMessage(ref msg);
                    DisplayMessages(ref _messages);
                }

                if (code != RCErrorCode.Succeed)
                {
                    print($"err:{code}");
                }

                RunOnMainThread.Enqueue(_refreshConvPanel);
            });
    }

    private void TestDisplayMessages(IList<RCMessage> msgList)
    {
        foreach (var rcMessage in msgList.Reverse()) // resverse?
        {
            if (!(rcMessage.Content is RCCustomMessage) && (
                MessageTag.IsPersisted != (rcMessage.Content?.GetFlag() & MessageTag.IsPersisted) &&
                MessageTag.IsCounted != (rcMessage.Content?.GetFlag() & MessageTag.IsCounted)))
            {
                continue;
            }

            InsertMessage(rcMessage);
        }
    }

    private void DisplayMessages(ref IList<RCMessage> messages)
    {
        var msgList = messages;

        RunOnMainThread.Enqueue(() =>
        {
            // foreach (var rcMessage in msgList.Reverse())
            // {
            //     InsertMessage(rcMessage);
            // }
            ClearDisplayScreen();
            TestDisplayMessages(msgList);
            ScrollToTop();
        });
    }

    private int GetControlNum()
    {
        messageCount++;
        return messageCount;
    }

    private void ScrollToTop()
    {
        ScrollRect scrollRect = GameObject.Find("/Canvas/MessageArea").GetComponent<ScrollRect>();
        scrollRect.gameObject.SetActive(true);
        scrollRect.verticalNormalizedPosition = 0f;
    }

    private void UpdateSendAndReceiveText(GameObject item, RCMessage rcMsg)
    {
        Text privateRecvText = item.transform.Find("PrivateRecv").GetComponent<Text>();
        Text privateSendText = item.transform.Find("PrivateSend").GetComponent<Text>();
        if (CurrentConversationType() == RCConversationType.Private)
        {
            // receive status
            privateRecvText.gameObject.SetActive(true);
            if (rcMsg.ReceivedStatus.IsRead)
            {
                privateRecvText.text = "已读";
            }
            else if (rcMsg.ReceivedStatus.IsRetrieved)
            {
                privateRecvText.text = "已接收";
            }
            else
            {
                privateRecvText.text = rcMsg.ReceivedStatus.ToString();
            }

            // send status
            privateSendText.gameObject.SetActive(true);
            if (rcMsg.SentStatus == RCSentStatus.Sending)
            {
                privateSendText.text = "正在发送...";
            }
            else if (rcMsg.SentStatus == RCSentStatus.Received)
            {
                privateSendText.text = "已接收";
            }
            else if (rcMsg.SentStatus == RCSentStatus.Read)
            {
                privateSendText.text = "已读";
            }
            else
            {
                privateSendText.text = $"{rcMsg.SentStatus}";
            }
        }
        else
        {
            privateRecvText.gameObject.SetActive(false);
            privateSendText.gameObject.SetActive(false);
        }
    }

    private void InsertMessage(RCMessage rcMsg)
    {
        bool isSend = rcMsg.Direction == RCMessageDirection.Send;
        GameObject item;
        var id = GetControlNum();

        if (isSend)
        {
            item = GameObject.Instantiate(SendMessagePanel, MessageTable.transform) as GameObject;

            Button recallButton = item.transform.Find("Recall").GetComponent<Button>();
            recallButton.onClick.AddListener(() => { RecallMessage(rcMsg); });

            Button ReceiptRequest = item.transform.Find("ReceiptRequest").GetComponent<Button>();
            ReceiptRequest.onClick.AddListener(() => { SendReceiptRequest(ref rcMsg); });
            UpdateSendAndReceiveText(item, rcMsg);
        }
        else
        {
            item = GameObject.Instantiate(MessagePanel, MessageTable.transform) as GameObject;

            Button referenceButton = item.transform.Find("Reference").GetComponent<Button>();
            referenceButton.onClick.AddListener(() =>
            {
                if (rcMsg == null)
                    return;
                var lastContent = rcMsg.Content;
                var refMsg = new RCReferenceMessage(RCIMClient.Instance.GetCurrentUserID(), lastContent)
                {
                    Content = "a reference message",
                    ReferredMsgUid = rcMsg.MessageUId,
                };
                RCIMClient.Instance.SendMessage(CurrentConversationType(), CurrentTargetId(), refMsg);
            });

            Button deleteButton = item.transform.Find("Delete").GetComponent<Button>();
            long messageId = rcMsg.MessageId;
            deleteButton.onClick.AddListener(() => { OnClickDelMsgById(messageId); });

            Button copyUidButton = item.transform.Find("CopyUid").GetComponent<Button>();
            copyUidButton.onClick.AddListener(() => { IdInputField.text = rcMsg.MessageUId; });
        }

        item.name = $"message_panel_{id}";
        item.transform.Find("Selected").GetComponent<Toggle>().onValueChanged.AddListener(
            (isSelected) =>
            {
                if (isSelected)
                {
                    _batchMessages.Add(rcMsg);
                }
                else
                {
                    _batchMessages.Remove(rcMsg);
                }
            });

        Button forwardButton = item.transform.Find("Forward").GetComponent<Button>();
        forwardButton.onClick.AddListener(() => { ForwardMessage(ref rcMsg); });

        Button Expansion = item.transform.Find("Expansion").GetComponent<Button>();
        Expansion.onClick.AddListener(() =>
        {
            MessageItem msgItem1 = MessagesDictionary[id];
            if (msgItem1 != null)
            {
                ExpansionMessage(msgItem1);
            }
        });

        if (MessagesDictionary.Values.Count > 0)
        {
            RectTransform rect = MessageTable.GetComponent<RectTransform>();
            RectTransform last = MessageTable.transform.GetChild(MessagesDictionary.Values.Count - 1).gameObject
                .GetComponent<RectTransform>();
            var y = last.localPosition.y - last.rect.height - 10;
            RectTransform current = item.GetComponent<RectTransform>();
            if (isSend)
            {
                current.localPosition = new Vector3(rect.rect.width - current.rect.width, y, last.localPosition.z);
            }
            else
            {
                current.localPosition = new Vector3(10, y, last.localPosition.z);
            }

            rect.sizeDelta = new Vector2(rect.sizeDelta.x, current.rect.height - y);
        }

        MessageItem msgItem = new MessageItem(item, rcMsg, id);
        MessagesDictionary.Add(id, msgItem);
        var Output = item.transform;

        Output.Find("TargetId").GetComponent<Text>().text =
            string.IsNullOrEmpty(rcMsg.SenderUserId) ? "null" : rcMsg.SenderUserId;
        Output.Find("Content").GetComponent<Text>().text = _contentOfMsg(rcMsg);
        Output.Find("Property").GetComponent<Text>().text = _propertyOfMsg(rcMsg);
        Output.Find("MessageType").GetComponent<Text>().text = rcMsg.ObjectName;
        if (isSend)
        {
            Output.Find("TimeStamp").GetComponent<Text>().text = Util.FormatTs(rcMsg.SentTime);
            Output.Find("TimeStampNumber").GetComponent<Text>().text =
                rcMsg.SentTime.ToString("N", CultureInfo.InvariantCulture);
        }
        else
        {
            Output.Find("TimeStamp").GetComponent<Text>().text = Util.FormatTs(rcMsg.ReceivedTime);
            Output.Find("TimeStampNumber").GetComponent<Text>().text =
                rcMsg.ReceivedTime.ToString("N", CultureInfo.InvariantCulture);
        }
    }

    private void RecallMessage(RCMessage message)
    {
        if (message == null) return;

        // recall message
        RCIMClient.Instance.RecallMessage(message, "",
            (errCode, recallNtfMsg) =>
            {
                print(
                    $"RecallMessage [{message.MessageId}]: {errCode},recallNtfMsg:{recallNtfMsg}{Environment.NewLine}");
                if (errCode == RCErrorCode.Succeed)
                {
                    GetMessagesWithOption(true);
                }
            });
    }

    private void ForwardMessage(ref RCMessage msg)
    {
        ForwardBox.gameObject.SetActive(true);
        RCMessage aMsg = msg;
        forwardPara.Message = aMsg;
    }

    private void ExpansionMessage(MessageItem item)
    {
        RCMessage rcMsg = item.Message;
        if (rcMsg.CanIncludeExpansion)
        {
            expansionPara.Message = rcMsg;
            expansionPara.idx = item.idx;
            expansionPara.readDicFlag = true;
            ExpansionBox.gameObject.SetActive(true);
        }
        else
        {
            RunOnMainThread.Enqueue(() => { ShowToast("不能扩展此消息"); });
        }
    }

    private void ExpansionMessageUpdate()
    {
        if (expansionPara.Message == null)
        {
            return;
        }

        var message = expansionPara.Message;
        var dic = expansionPara.ExpansionDictionary;
        if (dic.Count == 0)
        {
            ShowToast($"请输入键值");
            return;
        }

        int idx = expansionPara.idx;

        RCIMClient.Instance.UpdateMessageExpansion(message.MessageUId, dic, (errCode) =>
        {
            string output = $"UpdateMessageExpansion :{errCode}{Environment.NewLine}";
            print(output);
            if (errCode == RCErrorCode.Succeed)
            {
                RCIMClient.Instance.GetMessage(message.MessageId, (getMsgCode, resultMsg) =>
                {
                    if (getMsgCode == RCErrorCode.Succeed)
                    {
                        MessageItem item = MessagesDictionary[idx];
                        item.Message = resultMsg;
                        MessagesDictionary[idx] = item;
                    }
                });
            }
        });
    }

    private void ExpansionMessageRemove()
    {
        if (expansionPara.Message == null)
        {
            return;
        }

        var message = expansionPara.Message;
        var keys = expansionPara.Keys;
        if (keys.Count == 0)
        {
            ShowToast($"请输入键值");
            return;
        }

        int idx = expansionPara.idx;
        RCIMClient.Instance.RemoveMessageExpansionForKey(message.MessageUId, keys,
            (errCode) =>
            {
                var output = $"RemoveMessageExpansionForKey :{errCode}{Environment.NewLine}";
                print(output);

                if (errCode == RCErrorCode.Succeed)
                {
                    RCIMClient.Instance.GetMessage(message.MessageId, (getMsgCode, resultMsg) =>
                    {
                        if (getMsgCode == RCErrorCode.Succeed)
                        {
                            MessageItem item = MessagesDictionary[idx];
                            item.Message = resultMsg;
                            MessagesDictionary[idx] = item;
                        }
                    });
                }
            });
    }

    private void SendReceiptRequest(ref RCMessage message)
    {
        // 发送阅读回执请求
        var msg = message;
        if (message.ConversationType == RCConversationType.Group && message.Direction == RCMessageDirection.Send)
        {
            RCIMClient.Instance.SendReadReceiptRequest(message,
                (code) =>
                {
                    sendMessageToast = $"SendReadReceiptRequest [{msg.MessageId}]: {code}{Environment.NewLine}";
                    print(sendMessageToast);
                });
        }
        else
        {
            ShowToast("不是群组，不能发回执请求");
        }
    }

    private void OnReceiptRequest()
    {
        enabledReceiptRequestButton(false);
        SendReceiptReadMessage();
    }

    private void OnReceiptResponse()
    {
        enabledReceiptResponseButton(false);
        RunOnMainThread.Enqueue(() =>
        {
            ClearDisplayScreen();
            // 加载历史消息
            GetMessagesWithOption(true);
        });
    }

    // 发送阅读消息回执
    private void SendReceiptReadMessage()
    {
        if (_receiptRequestMsgList.Count > 0)
        {
            RCIMClient.Instance.SendReadReceiptResponse(CurrentConversationType(), CurrentTargetId(),
                _receiptRequestMsgList,
                (errCode) =>
                {
                    print($"SendReadReceiptResponse: code={errCode}{Environment.NewLine}");
                    if (errCode == RCErrorCode.Succeed)
                    {
                        _receiptRequestMsgList.Clear();
                    }
                });
        }

        if (CurrentConversationType() == RCConversationType.Private)
        {
            var type = CurrentConversationType();
            var targetId = CurrentTargetId();
            // 阅读回执
            RCIMClient.Instance.SendReadReceiptMessage(type, targetId, Util.CurrentTimeStamp(),
                (code) => { print($"SendReadReceiptMessage [{type},{targetId}]: code={code}{Environment.NewLine}"); });
        }
    }

    private void SearchMessages(string keyword)
    {
        RCIMClient.Instance.SearchMessages(CurrentConversationType(), CurrentTargetId(), keyword, 10,
            Util.CurrentTimeStamp(),
            (code, messageList) =>
            {
                print($"SearchMessages:{code},{messageList}{Environment.NewLine}");
                if (code == RCErrorCode.Succeed && messageList.Count > 0)
                {
                    RunOnMainThread.Enqueue(() =>
                    {
                        ClearDisplayMessages();
                        DisplayMessages(ref messageList);
                    });
                }
            });
    }

    private void SendTyping(string typingText)
    {
        if (CurrentConversationType() != RCConversationType.Private)
        {
            ShowToast("只支持私聊类型");
            return;
        }

        //RC:TxtMsg
        RCIMClient.Instance.SendTypingStatus(CurrentConversationType(), CurrentTargetId(), typingText);
    }

    private void GetBlacklistStatus()
    {
        if (CurrentConversationType() != RCConversationType.Private)
        {
            return;
        }

        var convPanel = ConvPanelArea.transform.Find(PRIVATE_PANEL);
        if (convPanel == null) return;

        var blockStatusText = convPanel.Find("BlockStatus").GetComponent<Text>();
        blockStatusText.text = "";

        RCIMClient.Instance.GetBlackListStatus(CurrentTargetId(),
            (code, blockStatus) =>
            {
                RunOnMainThread.Enqueue(() =>
                {
                    var privateConvPanel = ConvPanelArea.transform.Find(PRIVATE_PANEL);
                    if (privateConvPanel == null) return;
                    var privateBlockStatusText = privateConvPanel.Find("BlockStatus").GetComponent<Text>();

                    if (code == RCErrorCode.Succeed && blockStatus == RCBlackListStatus.InBlackList)
                    {
                        privateBlockStatusText.text = "已加黑";
                    }
                    else
                    {
                        privateBlockStatusText.text = "";
                    }
                });
            });
    }

    private void GetConversationInfo()
    {
        if (!DemoContext.Connected)
        {
            Debug.Log($"not connect to server!");
            return;
        }

        RCIMClient.Instance.GetConversation(CurrentConversationType(), CurrentTargetId(),
            (errorCode, rcConversation) =>
            {
                print($"GetConversation errCode={errorCode}, conversation={rcConversation}{Environment.NewLine}");
            });
    }

    private void GetTextDraft()
    {
        RCIMClient.Instance.GetTextMessageDraft(CurrentConversationType(), CurrentTargetId(),
            (error, draft) =>
            {
                if (error == RCErrorCode.Succeed)
                {
                    RunOnMainThread.Enqueue(() => { MessageInputField.text = draft; });
                }

                var output = $"GetTextMessageDraft {error}, {draft}{Environment.NewLine}";
                print(output);
            });
    }

    #region OnClick

    private void OnClickGoHome()
    {
        SceneManager.LoadScene("MainPage", LoadSceneMode.Single);
    }


    public void OnClickSetSMessageExtra_1()
    {
        if (!DemoContext.Connected) return;

        int msgId = 1;
        var extra = "test extra";
        RCIMClient.Instance.SetMessageExtra(msgId, extra,
            (code, succeed) => { print($"SetMessageExtra [{msgId},{extra}]: {code},{succeed}{Environment.NewLine}"); });
    }

    private void OnClickSetSMessageSentStatus_2()
    {
        if (!DemoContext.Connected) return;

        var message = LatestSendMessage;
        if (message == null)
        {
            ShowToast($"请先发送消息");
            return;
        }

        RCIMClient.Instance.SetMessageSentStatus((int) message.MessageId, RCSentStatus.Read,
            (code, succeed) =>
            {
                print(
                    $"SetMessageSentStatus [{message.MessageId},{RCSentStatus.Read}]: {code},{succeed}{Environment.NewLine}");
            });
    }

    private void OnClickSetSMessageReceivedStatus_3()
    {
        if (!DemoContext.Connected) return;
        var message = LatestReceiveMessage;
        if (message == null)
        {
            ShowToast($"请先接收消息");
            return;
        }

        var recvStatus = new RCReceivedStatus(RCReceivedStatus.Read);
        RCIMClient.Instance.SetMessageReceivedStatus((int) message.MessageId, recvStatus,
            (code, succeed) =>
            {
                print(
                    $"SetMessageReceivedStatus [{message.MessageId},{recvStatus.Flag}]: {code},{succeed}{Environment.NewLine}");
            });
    }

    private void OnClickGetMessageById()
    {
        if (string.IsNullOrEmpty(IdInputField.text))
        {
            ShowToast("请输入消息id");
            return;
        }

        long messageId = Convert.ToInt64(IdInputField.text);
        RCIMClient.Instance.GetMessage(messageId, (code, msg) =>
        {
            if (code == RCErrorCode.Succeed)
            {
                RunOnMainThread.Enqueue(() =>
                {
                    InsertMessage(msg);
                    ScrollToTop();
                });
            }
            else
            {
                RunOnMainThread.Enqueue(() => { print($"GetMessage [{messageId}]: {code}"); });
            }
        });
    }

    private void OnClickGetMessageByUid()
    {
        if (string.IsNullOrEmpty(IdInputField.text))
        {
            ShowToast("请输入消息id");
            return;
        }

        var msgUid = IdInputField.text;
        RCIMClient.Instance.GetMessageByUid(msgUid, (byUidCode, byUidMsg) =>
        {
            if (byUidCode == RCErrorCode.Succeed)
            {
                RunOnMainThread.Enqueue(() => { InsertMessage(byUidMsg); });
            }
            else
            {
                RunOnMainThread.Enqueue(() => { print($"GetMessageByUid [{msgUid}]: {byUidCode}"); });
            }
        });
    }

    IEnumerator TestGetMessage(bool isRefresh)
    {
        yield return new WaitForEndOfFrame();
        GetMessagesWithOption(isRefresh);
    }

    private void OnClickDelMsgById(long messageId)
    {
        var ids = new Int64[] {messageId};

        RCIMClient.Instance.DeleteMessages(ids,
            (errCode, result) =>
            {
                if (errCode == RCErrorCode.Succeed)
                {
                    RunOnMainThread.Enqueue(() =>
                    {
                        ShowToast("成功删除消息");
                        ClearDisplayMessages();
                        StartCoroutine(TestGetMessage(true));
                    });
                }

                print($"DeleteMessages:{errCode},result={result}{Environment.NewLine}");
            });
    }

    private void OnClickDelMsgByType()
    {
        if (_messages.Count > 0)
        {
            RCConversationType type = CurrentConversationType();
            string targetId = CurrentTargetId();

            RCIMClient.Instance.DeleteRemoteMessages(type, targetId, _messages,
                (errCode) =>
                {
                    if (errCode == RCErrorCode.Succeed)
                    {
                        RunOnMainThread.Enqueue(() =>
                        {
                            ClearDisplayMessages();
                            OnClickRefreshMessages();
                        });
                    }

                    print($"DeleteRemoteMessages callback:{errCode}{Environment.NewLine}");
                });
        }
    }


    public void OnClickRefreshMessages()
    {
        UpdateMessageTable();
    }

    private void OnClickSendGifMessage()
    {
        RequestStoragePermission();
#if UNITY_ANDROID
        _allowedTypes = new[] {"image/*"};
#endif
        NativeFilePicker.PickFile((imagePath) =>
        {
            if (string.IsNullOrEmpty(imagePath) || false == imagePath.ToLower().Contains("gif"))
            {
                print($"image path is empty, or select an gif image file.");
                return;
            }

            var content = new RCGifMessage()
            {
#if UNITY_ANDROID
                LocalPath = $"file://{imagePath}",
#elif UNITY_IOS
                LocalPath = imagePath,
#endif
            };
            var message = new RCMessage()
            {
                ConversationType = CurrentConversationType(),
                TargetId = CurrentTargetId(),
                Content = content
            };
            RCIMClient.Instance.SendMediaMessage(message);
        }, _allowedTypes);
    }

    private void OnClickSendSightMessage()
    {
        RequestStoragePermission();
#if UNITY_ANDROID
        _allowedTypes = new[] {"video/*"};
#endif
        NativeFilePicker.PickFile((imagePath) =>
        {
            if (string.IsNullOrEmpty(imagePath))
            {
                print($"path is empty, or select an mp4 video file.");
                return;
            }

            Debug.Log($"picker file path: {imagePath}");

            var content = new RCSightMessage()
            {
#if UNITY_ANDROID
                LocalPath = $"file://{imagePath}",
#elif UNITY_IOS
                LocalPath = imagePath,
                // LocalPath = $"file://{imagePath}",
#endif
                Duration = 9
            };
            var message = new RCMessage()
            {
                ConversationType = CurrentConversationType(),
                TargetId = CurrentTargetId(),
                Content = content,
                ObjectName = content.ObjectName
            };
            RCIMClient.Instance.SendMediaMessage(message);
        }, _allowedTypes);
    }

    private void OnClickSendVoiceMessage()
    {
        RequestStoragePermission();
#if UNITY_ANDROID
        _allowedTypes = new[] {"audio/*"};
#endif
        NativeFilePicker.PickFile((imagePath) =>
        {
            if (string.IsNullOrEmpty(imagePath) || false == imagePath.Contains("amr"))
            {
                print($"image path is empty, select an amr file.");
                return;
            }

            var content = new RCVoiceMessage()
            {
#if UNITY_ANDROID
                LocalPath = $"file://{imagePath}",
#elif UNITY_IOS
                LocalPath = imagePath,
#endif
                Duration = 15
            };
            RCIMClient.Instance.SendMessage(CurrentConversationType(), CurrentTargetId(), content);
        }, _allowedTypes);
    }

    private void OnClickSendHQVoiceMessage()
    {
        RequestStoragePermission();
#if UNITY_ANDROID
        _allowedTypes = new[] {"audio/*"};
#endif
        NativeFilePicker.PickFile((imagePath) =>
        {
            if (string.IsNullOrEmpty(imagePath) || false == imagePath.Contains("aac"))
            {
                print($"image path is empty, select an aac file.");
                return;
            }

            var message = new RCMessage()
            {
                ConversationType = CurrentConversationType(),
                TargetId = CurrentTargetId(),
                Content = new RCHQVoiceMessage()
                {
#if UNITY_ANDROID
                    LocalPath = $"file://{imagePath}",
#elif UNITY_IOS
                    LocalPath = imagePath,
#endif
                    Duration = 15
                }
            };

            RCIMClient.Instance.SendMediaMessage(message);
        }, _allowedTypes);
    }

    private void OnClickSendCustomMessage()
    {
        SendCustMessageBox.gameObject.SetActive(true);
    }

    public void SendCustomMessage_OnDone()
    {
        var tag = SendCustMessageBox.transform.Find("Content/Panel/CustCommandToggle").GetComponent<Toggle>().group
            .ActiveToggles().First().tag;
        var custMsgFields = new Dictionary<String, String>()
        {
            {"custField_1", "abcd"}, {"custField_2", "bcde"}, {"custField_3", "1"}
        };

        RCCustomMessage custMsgContent = null;
        if (tag == "Cust_Msg_Cmd")
        {
            custMsgContent = new RCCustomMessage("Cust:CmdMsg", RCCustomMessageType.Command, custMsgFields);
        }
        else if (tag == "Cust_Msg_Storage")
        {
            custMsgContent = new RCCustomMessage("Cust:StorageMsg", RCCustomMessageType.Storage, custMsgFields);
        }
        else if (tag == "Cust_Msg_Normal")
        {
            custMsgContent = new RCCustomMessage("Cust:NormalMsg", RCCustomMessageType.Normal, custMsgFields);
        }
        else if (tag == "Cust_Msg_Status")
        {
            custMsgContent = new RCCustomMessage("Cust:StatusMsg", RCCustomMessageType.Status, custMsgFields);
        }


        RCIMClient.Instance.SendMessage(CurrentConversationType(), CurrentTargetId(), custMsgContent);
        SendCustomMessage_OnCancel();
    }

    public void SendCustomMessage_OnCancel()
    {
        SendCustMessageBox.gameObject.SetActive(false);
    }
    
    private void OnClickSendFileMessage()
    {
        RequestStoragePermission();
#if UNITY_ANDROID
        _allowedTypes = new[] {"*/*"};
#elif UNITY_IOS
        _allowedTypes = new[] {"public.content"};
#endif
        NativeFilePicker.PickFile((imagePath) =>
        {
            if (string.IsNullOrEmpty(imagePath))
            {
                print($"image path is empty, select a file.");
                return;
            }

            var content = new RCFileMessage(imagePath);

            var message = new RCMessage()
            {
                ConversationType = CurrentConversationType(),
                TargetId = CurrentTargetId(),
                Content = content,
                ObjectName = content.ObjectName
            };
            RCIMClient.Instance.SendMediaMessage(message);
        }, _allowedTypes);
    }

    // private void OnClickSendLocationMessage()
    // {
    //     var content = new RCLocationMessage()
    //     {
    //         Longitude = 110.0,
    //         Latitude = 37.8,
    //         POI = "POI",
    //         ThumbUrl =
    //             "https://rongcloud-spic.ronghub.com/XAANWl9STQULVwVBC2VOaFh0BUQLfgUGAQQAAQICDDIxNDA%3D.jpg?e=1638520783&token=livk5rb3__JZjCtEiMxXpQ8QscLxbNLehwhHySnX:uq1C_sd5nmH96INLPGrPSALMsQs= base64:"
    //     };
    //     RCIMClient.Instance.SendMessage(CurrentConversationType(), CurrentTargetId(), content);
    // }

    private void OnClickSendTextMessage()
    {
        var content = MessageInputField.text;
        if (String.IsNullOrEmpty(content))
        {
            ShowToast("请先输入消息");
            return;
        }

        var sendPanel = SendPanelArea.transform.Find(SEND_PANEL);
        bool canExpansion = sendPanel.Find("ExpansionToggle").GetComponent<Toggle>().isOn;
        sendPanel.Find("ExpansionToggle").GetComponent<Toggle>().SetIsOnWithoutNotify(false);

        MessageInputField.text = "";
        var message = new RCTextMessage()
        {
            Content = content,
            MentionedInfo = new RCMentionedInfo(MentionedType.Part, new List<string>
                {
                    "sssss", "bbbbb", "dddddd"
                },
                "@sss hello world!")
        };

        RCMessage rcMsg = new RCMessage()
        {
            ConversationType = CurrentConversationType(),
            TargetId = CurrentTargetId(),
            CanIncludeExpansion = canExpansion,
            Content = message,
            ObjectName = message.ObjectName
        };

        RCIMClient.Instance.SendMessage(rcMsg);
    }

    private void OnClickSendImageMessage()
    {
        RequestStoragePermission();
#if UNITY_ANDROID
        _allowedTypes = new[] {"image/*"};
#endif
        NativeFilePicker.PickFile((imagePath) =>
        {
            if (string.IsNullOrEmpty(imagePath))
            {
                print($"image path is empty, select an image file.");
                return;
            }

            var content = new RCImageMessage()
            {
#if UNITY_ANDROID
                ThumbUri = $"file://{imagePath}",
                LocalPath = $"file://{imagePath}",
#elif UNITY_IOS
                LocalPath = imagePath,
                ThumbUri = imagePath,
#endif
                IsOriginal = true
            };
            var message = new RCMessage()
            {
                ConversationType = CurrentConversationType(),
                TargetId = CurrentTargetId(),
                Content = content,
                ObjectName = content.ObjectName
            };
            RCIMClient.Instance.SendMediaMessage(message);
        }, _allowedTypes);
    }

    // public void OnClickSendCombineMessage()
    // {
    //     if (_messages?.FirstOrDefault() == null)
    //         return;
    //     var lastContent = _messages.First().Content;
    //     var refMsg = new RCCombineMessage()
    //     {
    //         Title = "RCCombineMessage",
    //         ConversationType = RCConversationType.Group,
    //         NameList = new List<string>() {"aaa", "bbb", "ccc"},
    //         SummaryList = new List<string>() {"sdbvsdfds", "sdfsdfsdfd", "ccvcvcvxc"},
    //     };
    //     RCIMClient.Instance.SendMessage(GetCurrentConversationType(), GetCurrentTargetId(), refMsg);
    // }


    private void OnClickInsertBatchMessages()
    {
        if (_batchMessages.Count > 0)
        {
            ClearDisplayMessages();
            RCIMClient.Instance.BatchInsertMessage(_batchMessages.ToList(),
                (code, result) =>
                {
                    RunOnMainThread.Enqueue(OnClickRefreshMessages);

                    print($"BatchInsertMessage :{code}{result}{Environment.NewLine}");
                });
        }
        else
        {
            ShowToast("请先获取消息");
        }
    }

    private void OnClickInsertIncomingMessage()
    {
        if (_messages.Count <= 0)
        {
            ShowToast($"请先获取消息");
            return;
        }

        var incomingMockMessage = new RCTextMessage("本地插入接收消息");
        // insert incoming
        RCIMClient.Instance.InsertIncomingMessage(CurrentConversationType(), CurrentTargetId(), CurrentTargetId(),
            new RCReceivedStatus(RCReceivedStatus.Read), incomingMockMessage, Util.CurrentTimeStamp(),
            (code, result) =>
            {
                if (code != RCErrorCode.Succeed)
                    return;
                GetMessagesWithOption(true);
            });
    }

    private void OnClickInsertOutgoingMessage()
    {
        if (_messages.Count <= 0)
        {
            ShowToast($"请先获取消息");
            return;
        }

        var incomingMockMessage = new RCTextMessage("本地插入发送消息");
        RCIMClient.Instance.InsertOutgoingMessage(CurrentConversationType(), CurrentTargetId(),
            RCSentStatus.Sent, incomingMockMessage, Util.CurrentTimeStamp(),
            (code, msg) =>
            {
                if (code != RCErrorCode.Succeed)
                    return;
                GetMessagesWithOption(true);
            });
    }

    private void OnClickToggleBlackStatus()
    {
        RCIMClient.Instance.GetBlackListStatus(CurrentTargetId(),
            (getBlackListStatusCode, blockStatus) =>
            {
                if (getBlackListStatusCode != RCErrorCode.Succeed)
                    return;
                if (blockStatus == RCBlackListStatus.NotInBlackList)
                {
                    RCIMClient.Instance.AddToBlackList(CurrentTargetId(),
                        code => { RunOnMainThread.Enqueue(GetBlacklistStatus); });
                }
                else
                {
                    RCIMClient.Instance.RemoveFromBlackList(CurrentTargetId(),
                        code => { RunOnMainThread.Enqueue(GetBlacklistStatus); });
                }
            });
    }

    public void OnClickSaveConversationDraft()
    {
        if (!_checkConversationInfo())
        {
            return;
        }

        var draft = MessageInputField.text;
        if (string.IsNullOrEmpty(draft))
        {
            return;
        }

        RCIMClient.Instance.SaveTextMessageDraft(CurrentConversationType(), CurrentTargetId(), draft, (error) =>
        {
            var output = $"SaveTextMessageDraft {draft}, {error}{Environment.NewLine}";
            print(output);
        });
    }

    public void OnClickClearConversationDraft()
    {
        if (!_checkConversationInfo())
        {
            return;
        }

        RCIMClient.Instance.ClearTextMessageDraft(CurrentConversationType(), CurrentTargetId(),
            (error) =>
            {
                string output = $"ClearTextMessageDraft: {error}{Environment.NewLine}";
                print(output);
                if (error == RCErrorCode.Succeed)
                {
                    RunOnMainThread.Enqueue(() => { MessageInputField.text = string.Empty; });
                }
            });
    }

    private void OnClickJoinQuitRoom()
    {
        if (CurrentConversationType() != RCConversationType.ChatRoom) return;

        Transform convPanel = ConvPanelArea.transform.Find(CHATROOM_PANEL);
        Text joinOrQuitText = convPanel.Find("JoinOrQuitChatRoom/Text").GetComponent<Text>();

        if (!ChatRoomState.IsJoined)
        {
            RCIMClient.Instance.JoinChatRoom(CurrentTargetId(), (errCode) =>
            {
                print($"JoinChatRoom: {errCode}");
                if (errCode == RCErrorCode.Succeed)
                {
                    ChatRoomState.IsJoined = true;
                    RunOnMainThread.Enqueue(() => { joinOrQuitText.text = "离开聊天室"; });
                }
            });
        }
        else
        {
            RCIMClient.Instance.QuitChatRoom(CurrentTargetId(), (errCode) =>
            {
                print($"QuitChatRoom: {errCode} ");
                if (errCode == RCErrorCode.Succeed)
                {
                    ChatRoomState.IsJoined = false;
                    RunOnMainThread.Enqueue(() => { joinOrQuitText.text = "进入聊天室"; });
                }
            });
        }
    }

    public void ForwardBox_OnDone()
    {
        var targetId = ForwardBox.transform.Find("Content/Panel/TargetInputField").GetComponent<InputField>().text;
        if (string.IsNullOrEmpty(targetId))
        {
            ShowToast("请输入目标 ID");
            return;
        }

        var tag = ForwardBox.transform.Find("Content/Panel/GroupToggle").GetComponent<Toggle>().group
            .ActiveToggles().First().tag;
        RCConversationType type = RCConversationType.Group;
        if (tag == "Forward_Private")
        {
            type = RCConversationType.Private;
        }
        else if (tag == "Forward_ChatRoom")
        {
            type = RCConversationType.ChatRoom;
        }

        RCIMClient.Instance.ForwardMessageByStep(type, targetId, forwardPara.Message);
        ForwardBox_OnCancel();
    }

    public void ForwardBox_OnCancel()
    {
        forwardPara = new ForwardParameter();
        ForwardBox.gameObject.SetActive(false);
    }

    public void ExpansionBox_Toggle(bool isOn)
    {
        bool isSelected = ExpansionBox.transform.Find("Content/Panel/Toggle").GetComponent<Toggle>().isOn;
        if (isSelected)
        {
            expansionPara.action = ExpansionParameter.ExpansionAction.REMOVE;
        }
        else
        {
            expansionPara.action = ExpansionParameter.ExpansionAction.UPDATE;
        }
    }

    public void ExpansionBox_OnDone()
    {
        if (expansionPara.action == ExpansionParameter.ExpansionAction.UPDATE)
        {
            ExpansionMessageUpdate();
        }
        else if (expansionPara.action == ExpansionParameter.ExpansionAction.REMOVE)
        {
            ExpansionMessageRemove();
        }

        expansionPara = new ExpansionParameter();
        ExpansionBox.gameObject.SetActive(false);
    }

    public void ExpansionBox_AddKeys()
    {
        InputField keyInput =
            ExpansionBox.transform.Find("Content/Panel/KeyInputField").GetComponent<InputField>();

        if (string.IsNullOrEmpty(keyInput.text))
        {
            ShowToast("请输入key值");
            return;
        }

        expansionPara.Keys.Add(keyInput.text);
        keyInput.text = string.Empty;
    }

    public void ExpansionBox_AddKeyValue()
    {
        InputField keyInput =
            ExpansionBox.transform.Find("Content/Panel/KeyInputField").GetComponent<InputField>();
        InputField valueInput =
            ExpansionBox.transform.Find("Content/Panel/ValueInputField").GetComponent<InputField>();

        if (string.IsNullOrEmpty(keyInput.text) || string.IsNullOrEmpty(valueInput.text))
        {
            ShowToast("请输入key和value");
            return;
        }

        expansionPara.ExpansionDictionary.Add(keyInput.text, valueInput.text);
        keyInput.text = string.Empty;
        valueInput.text = string.Empty;
    }

    #endregion

    #region Event

    private void Instance_OnSendMessageAttached(RCMessage message)
    {
        sendMessageToast = $"Event|SendAttached: {message}{Environment.NewLine}";
        print(sendMessageToast);
    }

    private void OnIMMessageSendSucceed(RCMessage message)
    {
        // 保存最后一条发送的消息
        LatestSendMessage = message;

        sendMessageToast += $"Event|SendSucceed: {message}{Environment.NewLine}";
        print(sendMessageToast);
        if (message.ConversationType != CurrentConversationType() || message.TargetId != CurrentTargetId())
            return;
        RunOnMainThread.Enqueue(() =>
        {
            InsertMessage(message);
            ScrollToTop();
        });
    }

    private void Instance_OnSendMessageFailed(RCErrorCode errorCode, RCMessage message)
    {
        sendMessageToast = $"Event|SendMessageFailed: {errorCode} {message}{Environment.NewLine}";
        print(sendMessageToast);
    }

    private void Instance_OnTypingStatusChanged(RCConversationType conversationType, string targetId,
        List<RCTypingStatus> typeStatusList)
    {
        var toast = $"OnTypingStatusChanged: {conversationType} {targetId}: {Environment.NewLine}";
        foreach (var item in typeStatusList)
        {
            toast += $"OnTypingStatusChanged: {item}: ";
        }

        print(toast);
    }

    private void OnReadReceiptRequest(RCConversationType type, string targetId, string messageUid)
    {
        print($"Event|OnReadReceiptRequest: {type},{targetId},{messageUid}{Environment.NewLine}");
        RCIMClient.Instance.GetMessageByUid(messageUid, (errCode, message) =>
        {
            if (errCode != RCErrorCode.Succeed)
            {
                print($"Error: GetMessageByUid {errCode}{Environment.NewLine}");
                return;
            }

            // 更新需要发送阅读回执的消息列表
            _receiptRequestMsgList.Add(message);
            RunOnMainThread.Enqueue(() => { enabledReceiptRequestButton(true); });
        });
    }

    private void OnReadReceiptReceived(RCConversationType type, String targetId, String senderUserId, Int64 timestamp)
    {
        print($"Event|OnReadReceiptReceived: {type} {targetId} {senderUserId} {timestamp} {Environment.NewLine}");
    }

    private void OnReadReceiptResponse(RCConversationType type, string targetId, string messageUid,
        IDictionary<string, Int64> respondUserIdList)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var kv in respondUserIdList)
        {
            sb.Append($"({kv.Key},{kv.Value}) ");
        }

        print($"Event|OnReadReceiptResponse: {type},{targetId},{messageUid},{sb.ToString()}{Environment.NewLine}");

        RunOnMainThread.Enqueue(() => { enabledReceiptResponseButton(true); });

        RCIMClient.Instance.GetMessageByUid(messageUid, (code, rcMsg) =>
        {
            if (code == RCErrorCode.Succeed && rcMsg != null)
            {
                RCIMClient.Instance.SetMessageSentStatus(rcMsg.MessageId, RCSentStatus.Read,
                    (setMsgSentStatusCode, succeed) =>
                    {
                        print(
                            $"SetMessageSentStatus [{rcMsg.MessageId},{RCSentStatus.Read}]: {setMsgSentStatusCode},{succeed}{Environment.NewLine}");
                    });
            }
        });
    }

    private void OnMessageExpansionUpdated(IDictionary<string, string> expansionChanged, RCMessage message)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var kv in expansionChanged)
        {
            sb.Append($"({kv.Key}, {kv.Value}) ");
        }

        print(
            $"Event|OnMessageExpansionUpdated:msgId:{message.MessageId},update {sb.ToString()}{Environment.NewLine}");
    }

    private void OnMessageExpansionRemoved(IList<string> keyList, RCMessage message)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var key in keyList)
        {
            sb.Append($"{key} ");
        }

        print(
            $"Event|OnMessageExpansionRemoved:msgId:{message.MessageId}, removed {sb.ToString()}{Environment.NewLine}");
    }

    private void OnMessageReceived(RCMessage message, int left)
    {
        LatestReceiveMessage = message;
        RCConversationType type = message.ConversationType;
        var targetId = message.SenderUserId;
        var timeStamp = Util.CurrentTimeStamp();


        print($"Event|OnMessageReceived: {message}{Environment.NewLine}");
        RunOnMainThread.Enqueue(() =>
        {
            if (message.ConversationType != CurrentConversationType() || message.TargetId != CurrentTargetId())
                return;
            
            SendReceiptReadMessage();
            
            InsertMessage(message);
            ScrollToTop();
        });
    }

    private void OnMessageRecalled(RCMessage message, RCRecallNotificationMessage recallNotify)
    {
        GetMessagesWithOption(true);
    }

    private void OnChatRoomKVSync(string roomId)
    {
        print($"Event|OnChatRoomKVSync: roomId={roomId}{Environment.NewLine}");
    }

    private void OnChatRoomKVUpdate(string roomId, IDictionary<string, string> roomKVDic)
    {
        print($"Event|OnChatRoomKVUpdate: roomId={roomId},{roomKVDic}{Environment.NewLine}");
        if (roomKVDic != null)
        {
            foreach (var kv in roomKVDic)
            {
                print($"chatroom kv update:{kv.Key},{kv.Value}{Environment.NewLine}");
            }
        }
    }

    private void OnChatRoomKVRemove(string roomId, IDictionary<string, string> roomKVDic)
    {
        print($"Event|OnChatRoomKVRemove: roomId={roomId},{roomKVDic}{Environment.NewLine}");
        if (roomKVDic != null)
        {
            foreach (var kv in roomKVDic)
            {
                print($"chatroom kv remove:{kv.Key},{kv.Value}{Environment.NewLine}");
            }
        }
    }

    #endregion
}