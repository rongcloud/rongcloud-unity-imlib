using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Mime;
using cn_rongcloud_im_unity;
using cn_rongcloud_im_unity_example;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartConversation : MonoBehaviour
{    
    private Text _toastText;
    private static readonly ConcurrentQueue<Action> RunOnMainThread = new ConcurrentQueue<Action>();

    // Start is called before the first frame update
    void Start()
    {
        _toastText = GameObject.Find("/Canvas/Toast").GetComponent<Text>();
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

    public void OnClickSendHi()
    {
        var typeCtrl = GameObject.Find("/Canvas/TargetType").GetComponent<Dropdown>();
        var targetId = GameObject.Find("/Canvas/TargetId/Text").GetComponent<Text>();

        if (string.IsNullOrEmpty(targetId.text))
        {
            ShowToast("targetId can not be empty!");
            return;
        }

        RCConversationType type = RCConversationType.Private;
        if (typeCtrl.value == 0)
        {
            type = RCConversationType.Private;
        }
        else if (typeCtrl.value == 1)
        {
            type = RCConversationType.Group;
        }
        else if (typeCtrl.value == 2)
        {
            type = RCConversationType.ChatRoom;
            
            RCIMClient.Instance.JoinChatRoom(targetId.text, (errCode) =>
            {
                if (errCode != RCErrorCode.Succeed)
                {
                    ShowToast("Join Chatroom failed.");
                    return;
                }
                
                RunOnMainThread.Enqueue(() =>
                {
                    var textContent = new RCTextMessage($"Hi, {targetId}");
                    RCIMClient.Instance.SendMessage(type, targetId.text, textContent);

                    RCIMClient.Instance.GetConversation(type, targetId.text, (code, conversation) =>
                    {
                        if (code != RCErrorCode.Succeed)
                            return;
                        RunOnMainThread.Enqueue(() =>
                        {
                            DemoContext.CurrentConversation = conversation;
                            SceneManager.LoadScene("ConversationPage", LoadSceneMode.Single);
                        });
                    });  
                });
            });

            return;
            
        }

        var textContent1 = new RCTextMessage($"Hi, {targetId}");
        textContent1.SendUserInfo = new RCUserInfo("ooo", "ppp", "111", "sss");
        RCIMClient.Instance.SendMessage(type, targetId.text, textContent1);

        RCIMClient.Instance.GetConversation(type, targetId.text, (code, conversation) =>
        {
            if (code != RCErrorCode.Succeed)
                return;
            RunOnMainThread.Enqueue(() =>
            {
                DemoContext.CurrentConversation = conversation;
                SceneManager.LoadScene("ConversationPage", LoadSceneMode.Single);
            });
        });
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
