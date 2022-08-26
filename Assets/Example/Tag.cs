using cn_rongcloud_im_unity;
using cn_rongcloud_im_unity_example;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tag : MonoBehaviour
{
    private static readonly ConcurrentQueue<Action> RunOnMainThread = new ConcurrentQueue<Action>();

    public GameObject TagListViewportContent;
    public GameObject TagItem;
    private IList<GameObject> _tagList = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        RCIMClient.Instance.OnTagChanged += Instance_OnTagChanged;
        RCIMClient.Instance.OnConversationTagChanged += Instance_OnConversationTagChanged;
        TagListViewportContent = GameObject.Find("/Tag/ScrollView/Viewport/Content");
        LoadTagList();
    }

    private void Instance_OnConversationTagChanged()
    {
        ShowToast("Conversation Tag Changed");
    }

    private void Instance_OnTagChanged()
    {
        ShowToast("Tag Changed");
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
        SceneManager.LoadScene("MainPage", LoadSceneMode.Single);
    }

    public void OnClickAddTag()
    {
        var newTagName = GameObject.Find("/Tag/InputFieldTagName").GetComponent<InputField>().text;

        if (String.IsNullOrEmpty(newTagName))
            return;
        var plainGuid = Guid.NewGuid().ToString().Replace("-", "");
        var newTag = new RCTagInfo(plainGuid.Substring(0, 10), newTagName);
        RCIMClient.Instance.AddTag(newTag, (code) =>
        {
            if (code != RCErrorCode.Succeed)
                return;
            LoadTagList();
        });
    }

    public void OnClickGetTags()
    {
        LoadTagList();
    }

    public void OnClickGetTagsFromConversation()
    {
        if (DemoContext.ConversationList == null || DemoContext.ConversationList.Count == 0)
            return;
        var firstConvr = DemoContext.ConversationList.FirstOrDefault();
        RCIMClient.Instance.GetTagsFromConversation(firstConvr.ConversationType, firstConvr.TargetId, (code, converTagList) =>
        {
            var tagListString = string.Empty;
            foreach (var converTag in converTagList)
            {
                tagListString += $"{converTag} , {Environment.NewLine}";
            }
            ShowToast($"GetTagsFromFirstConversation: {firstConvr} {code} {Environment.NewLine} {tagListString}");
        });
    }

    private void LoadTagList()
    {
        RCIMClient.Instance.GetTags((code, tagList) =>
        {
            RefreshTagList(tagList);
        });
    }
    
    private void RefreshTagList(IList<RCTagInfo> conversationList)
    {
        RunOnMainThread.Enqueue(() =>
        {
            var children = (from Transform child in TagListViewportContent.transform select child.gameObject).ToList();

            children.ForEach(Destroy);

            _tagList.Clear();
            if (conversationList == null) return;

            foreach (var item in conversationList)
            {
                AddTagListItem(item);
            }
        });
    }

    private void AddTagListItem(RCTagInfo tagInfo)
    {
        if (tagInfo == null) return;

        GameObject item = GameObject.Instantiate(TagItem, TagListViewportContent.transform) as GameObject;
        item.transform.Find("TagId").GetComponent<Text>().text = tagInfo.TagId;
        item.transform.Find("TagName").GetComponent<InputField>().text = tagInfo.TagName;
        item.transform.Find("TagName").GetComponent<InputField>().Select();
        item.transform.Find("TagConversationCount").GetComponent<Text>().text = $"包含会话 {tagInfo.Count} 个";
        item.transform.Find("Timestamp").GetComponent<Text>().text = Util.FormatTs(tagInfo.TimeStamp);

        RCIMClient.Instance.GetUnreadCountByTag(tagInfo.TagId, true,
            (code, unreadCount) =>
            {
                RunOnMainThread.Enqueue(() =>
                    {
                        item.transform.Find("TagUnreadCount").GetComponent<Text>().text = $"Tag 未读数 {unreadCount}";
                    });
            });
        RectTransform rect = TagListViewportContent.GetComponent<RectTransform>();
        RectTransform current = item.GetComponent<RectTransform>();
        if (_tagList.Count > 0)
        {
            RectTransform last = TagListViewportContent.transform.GetChild(_tagList.Count - 1).gameObject
                .GetComponent<RectTransform>();
            item.GetComponent<RectTransform>().localPosition = new Vector3(last.localPosition.x,
                last.localPosition.y - last.rect.height - 10, last.localPosition.z);
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.rect.height + current.rect.height + 10);
        }
        else
        {
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, current.rect.height);
        }

        item.transform.Find("UpdateTag").GetComponent<Button>().onClick.AddListener(() =>
        {
            var newTagName = item.transform.Find("TagName").GetComponent<InputField>().text;
            if (string.IsNullOrEmpty(newTagName))
                return;
            var newTag = new RCTagInfo(tagInfo.TagId, newTagName);
            RCIMClient.Instance.UpdateTag(newTag, (code) => { LoadTagList(); });
        });
        item.transform.Find("RemoveTag").GetComponent<Button>().onClick.AddListener(() =>
        {
            RCIMClient.Instance.RemoveTag(tagInfo, (code) => { LoadTagList(); });
        });
        item.transform.Find("AddConversationToTag").GetComponent<Button>().onClick.AddListener(() =>
        {
            RCIMClient.Instance.GetConversationListByPage((code, conversationList) =>
                {
                    if (code != RCErrorCode.Succeed)
                        return;
                    DemoContext.TagConversationEditContext.CurrentTag = tagInfo;
                    DemoContext.TagConversationEditContext.ConversationSourceForTag = conversationList;
                    DemoContext.TagConversationEditContext.Mode =
                        TagConversationContext.EditMode.SelectConversationForAdd;
                    RunOnMainThread.Enqueue(() =>
                    {
                        SceneManager.LoadScene("TagConversationManage", LoadSceneMode.Single);
                    });
                }, 0, 10,
                RCConversationType.Private, RCConversationType.Group, RCConversationType.ChatRoom);
        });
        item.transform.Find("RemoveConversationFromTag").GetComponent<Button>().onClick.AddListener(() =>
        {
            RCIMClient.Instance.GetConversationsFromTagByPage(tagInfo.TagId, 0, 50, (code, conversationList) =>
            {
                if (code != RCErrorCode.Succeed)
                    return;
                DemoContext.TagConversationEditContext.CurrentTag = tagInfo;
                DemoContext.TagConversationEditContext.ConversationSourceForTag = conversationList;
                DemoContext.TagConversationEditContext.Mode =
                    TagConversationContext.EditMode.SelectConversationForRemove;
                RunOnMainThread.Enqueue(() =>
                {
                    SceneManager.LoadScene("TagConversationManage", LoadSceneMode.Single);
                });
            });
        });
        item.transform.Find("ShowConversationInTag").GetComponent<Button>().onClick.AddListener(() =>
        {
            RCIMClient.Instance.GetConversationsFromTagByPage(tagInfo.TagId, 0, 50, (code, conversationList) =>
            {
                if (code != RCErrorCode.Succeed)
                    return;
                DemoContext.TagConversationEditContext.CurrentTag = tagInfo;
                DemoContext.TagConversationEditContext.ConversationSourceForTag = conversationList;
                DemoContext.TagConversationEditContext.Mode =
                    TagConversationContext.EditMode.ShowConversationsInTag;
                RunOnMainThread.Enqueue(() =>
                {
                    SceneManager.LoadScene("TagConversationManage", LoadSceneMode.Single);
                });
            });
        });
        _tagList.Add(item);
    }

    private static void ShowToast(string toast)
    {
        if (string.IsNullOrEmpty(toast))
            return;
        RunOnMainThread.Enqueue(() =>
        {
            var settingTip = GameObject.Find("/Tag/Toast").GetComponent<Text>();
            settingTip.text = toast;
        });
    }
}
