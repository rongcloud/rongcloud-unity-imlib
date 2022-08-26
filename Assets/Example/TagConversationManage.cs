using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cn_rongcloud_im_unity;
using cn_rongcloud_im_unity_example;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TagConversationManage : MonoBehaviour
{
    private static readonly ConcurrentQueue<Action> RunOnMainThread = new ConcurrentQueue<Action>();

    public GameObject TagConversationListViewportContent;
    public GameObject TagConversationItem;

    private IList<GameObject> _tagConversationListGameObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        TagConversationListViewportContent = GameObject.Find("/Canvas/ScrollView/Viewport/Content");
        LoadConversationListForTag();
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

    public void OnClickedNavBack()
    {
        SceneManager.LoadScene("Tag", LoadSceneMode.Single);
    }

    private void LoadConversationListForTag()
    {
        RefreshConversationList(DemoContext.TagConversationEditContext.ConversationSourceForTag);
    }

    private void RefreshConversationList(IList<RCConversation> conversations)
    {
        RunOnMainThread.Enqueue(() =>
        {
            var children =
                (from Transform child in TagConversationListViewportContent.transform select child.gameObject).ToList();

            children.ForEach(Destroy);

            _tagConversationListGameObjects.Clear();
            if (conversations == null) return;

            foreach (var item in conversations)
            {
                AddConversationItem(item);
            }
        });
    }

    private void AddConversationItem(RCConversation conversation)
    {
        if (conversation == null) return;

        GameObject item =
            GameObject.Instantiate(TagConversationItem, TagConversationListViewportContent.transform) as GameObject;
        item.transform.Find("ConversationType").GetComponent<Text>().text = conversation.ConversationType.ToString();
        item.transform.Find("TargetId").GetComponent<Text>().text = conversation.TargetId;
        item.transform.Find("PinUnPin").GetComponent<Button>().interactable = false;

        if (DemoContext.TagConversationEditContext.Mode == TagConversationContext.EditMode.SelectConversationForAdd)
        {
            item.transform.Find("AddConversationToTag").GetComponent<Button>().interactable = true;
            item.transform.Find("RemoveConversationFromTag").GetComponent<Button>().interactable = false;
        }
        else if (DemoContext.TagConversationEditContext.Mode ==
                 TagConversationContext.EditMode.SelectConversationForRemove)
        {
            item.transform.Find("AddConversationToTag").GetComponent<Button>().interactable = false;
            item.transform.Find("RemoveConversationFromTag").GetComponent<Button>().interactable = true;
        }
        else if (DemoContext.TagConversationEditContext.Mode == TagConversationContext.EditMode.ShowConversationsInTag)
        {
            item.transform.Find("AddConversationToTag").GetComponent<Button>().interactable = false;
            item.transform.Find("RemoveConversationFromTag").GetComponent<Button>().interactable = false;
            item.transform.Find("PinUnPin").GetComponent<Button>().interactable = true;

            RCIMClient.Instance.GetConversationTopStatusInTag(conversation.ConversationType, conversation.TargetId,
                DemoContext.TagConversationEditContext.CurrentTag.TagId,
                (code, isTopped) =>
                {
                    if (code != RCErrorCode.Succeed)
                        return;
                    RunOnMainThread.Enqueue(() =>
                    {
                        item.transform.Find("TopStatus").GetComponent<Text>().text = isTopped ? "已置顶" : "";
                    });
                });

            RCIMClient.Instance.GetTagsFromConversation(conversation.ConversationType, conversation.TargetId,
                (code, conversationTagList) =>
                {
                    if (code != RCErrorCode.Succeed)
                        return;
                    RunOnMainThread.Enqueue(() =>
                    {
                        var builder = new StringBuilder("Tag: ");
                        foreach (var tagInfo in conversationTagList)
                        {
                            builder.Append($"{tagInfo.TagInfo.TagName}, ");
                        }

                        RunOnMainThread.Enqueue(() =>
                        {
                            item.transform.Find("ConversationTags").GetComponent<Text>().text = builder.ToString();
                        });
                    });
                });
        }

        item.transform.Find("AddConversationToTag").GetComponent<Button>().onClick.AddListener(() =>
        {
            var identifiers = new List<RCConversationIdentifier>
            {
                new RCConversationIdentifier(conversation.ConversationType, conversation.TargetId)
            };
            RCIMClient.Instance.AddConversationsToTag(DemoContext.TagConversationEditContext.CurrentTag.TagId,
                identifiers,
                code =>
                {
                    if (code != RCErrorCode.Succeed) return;
                    LoadConversationListForTag();
                });
        });
        item.transform.Find("RemoveConversationFromTag").GetComponent<Button>().onClick.AddListener(() =>
        {
            var identifiers = new List<RCConversationIdentifier>
            {
                new RCConversationIdentifier(conversation.ConversationType, conversation.TargetId)
            };
            RCIMClient.Instance.RemoveConversationFromTag(DemoContext.TagConversationEditContext.CurrentTag.TagId,
                identifiers, (code) =>
                {
                    if (code != RCErrorCode.Succeed) return;
                    LoadConversationListForTag();
                });
        });
        item.transform.Find("PinUnPin").GetComponent<Button>().onClick.AddListener(() =>
        {
            var identifiers = new List<RCConversationIdentifier>
            {
                new RCConversationIdentifier(conversation.ConversationType, conversation.TargetId)
            };
            RCIMClient.Instance.GetConversationTopStatusInTag(conversation.ConversationType, conversation.TargetId,
                DemoContext.TagConversationEditContext.CurrentTag.TagId,
                (code, isTopped) =>
                {
                    if (code != RCErrorCode.Succeed)
                        return;
                    RCIMClient.Instance.SetConversationToTopInTag(conversation.ConversationType, conversation.TargetId,
                        DemoContext.TagConversationEditContext.CurrentTag.TagId,
                        !isTopped,
                        setTopCode =>
                        {
                            if (setTopCode != RCErrorCode.Succeed) return;
                            LoadConversationListForTag();
                        });
                });
        });

        RectTransform rect = TagConversationListViewportContent.GetComponent<RectTransform>();
        RectTransform current = item.GetComponent<RectTransform>();
        if (_tagConversationListGameObjects.Count > 0)
        {
            RectTransform last = TagConversationListViewportContent.transform.GetChild(_tagConversationListGameObjects.Count - 1)
                .gameObject
                .GetComponent<RectTransform>();
            item.GetComponent<RectTransform>().localPosition = new Vector3(last.localPosition.x,
                last.localPosition.y - last.rect.height - 10, last.localPosition.z);
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.rect.height + current.rect.height + 10);
        }
        else
        {
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, current.rect.height);
        }
        _tagConversationListGameObjects.Add(item);
    }
}
