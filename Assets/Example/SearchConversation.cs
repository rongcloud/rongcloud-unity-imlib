using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using cn_rongcloud_im_unity;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SearchConversation : MonoBehaviour
{
    private Text _toastText;
    private static readonly ConcurrentQueue<Action> RunOnMainThread = new ConcurrentQueue<Action>();

    public GameObject SearchConversationItem;
    private IList<GameObject> _searchConversationListGameObjects = new List<GameObject>();
    public GameObject _searchConversationListViewportContent;

    // Start is called before the first frame update
    void Start()
    {
        _toastText = GameObject.Find("/Canvas/Toast").GetComponent<Text>();
        _searchConversationListViewportContent = GameObject.Find("/Canvas/ScrollView/Viewport/Content");
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

    public void OnClickSearch()
    {
        var targetId = GameObject.Find("/Canvas/TargetId/Text").GetComponent<Text>();

        if (string.IsNullOrEmpty(targetId.text))
        {
            ShowToast("targetId can not be empty!");
            return;
        }

        RCIMClient.Instance.SearchConversations(targetId.text,
            new[]
            {
                RCConversationType.Private, RCConversationType.Group, RCConversationType.ChatRoom
            }, new[] {"RC:TxtMsg"},
            (code, searchResult) =>
            {
                ShowToast($"SearchConversations: {code} {searchResult?.Count} 个命中会话");
                ShowSearchConversationList(searchResult);
            });
    }

    private void ShowSearchConversationList(IList<RCSearchConversationResult> searchConversationResults)
    {
        RunOnMainThread.Enqueue(() =>
        {
            var children = new List<GameObject>();
            foreach (Transform child in _searchConversationListViewportContent.transform)
            {
                children.Add(child.gameObject);
            }

            children.ForEach(Destroy);

            _searchConversationListGameObjects.Clear();
            if (searchConversationResults == null) return;

            foreach (var item in searchConversationResults)
            {
                AddConversationListItem(item);
            }
        });
    }

    private void AddConversationListItem(RCSearchConversationResult conversation)
    {
        if (conversation == null) return;

        GameObject item =
            GameObject.Instantiate(SearchConversationItem, _searchConversationListViewportContent.transform) as
                GameObject;
        item.transform.Find("ConversationType").GetComponent<Text>().text =
            conversation.Conversation.ConversationType.ToString();
        item.transform.Find("TargetId").GetComponent<Text>().text = conversation.Conversation.TargetId;
        item.transform.Find("MatchCount").GetComponent<Text>().text = $"命中消息 {conversation.MatchCount} 个";

        RectTransform rect = _searchConversationListViewportContent.GetComponent<RectTransform>();
        RectTransform current = item.GetComponent<RectTransform>();
        if (_searchConversationListGameObjects.Count > 0)
        {
            RectTransform last = _searchConversationListViewportContent.transform
                .GetChild(_searchConversationListGameObjects.Count - 1).gameObject
                .GetComponent<RectTransform>();
            item.GetComponent<RectTransform>().localPosition = new Vector3(last.localPosition.x,
                last.localPosition.y - last.rect.height - 10, last.localPosition.z);
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.rect.height + current.rect.height + 10);
        }
        else
        {
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, current.rect.height);
        }

        _searchConversationListGameObjects.Add(item);
    }
    
    public void OnClickGoHome()
    {
        SceneManager.LoadScene("MainPage", LoadSceneMode.Single);
    }

    private void ShowToast(String tip)
    {
        if (String.IsNullOrEmpty(tip))
            return;
        RunOnMainThread.Enqueue(() => { _toastText.text = tip; });
    }
}
