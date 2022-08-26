using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using cn_rongcloud_im_unity;
using cn_rongcloud_im_unity_example;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    private Text _toastText;
    
    private static readonly ConcurrentQueue<Action> RunOnMainThread = new ConcurrentQueue<Action>();

    // Start is called before the first frame update
    void Start()
    {
        if (!DemoContext.AppInitialized)
        {
            RCIMClient.Instance.SetServerInfo(ExampleConfig.NavServer, ExampleConfig.FileServer);

            var config = RCAndroidPushConfig.Builder.Create()
                .EnableMiPush("", "")
                .EnableOppoPush("", "")
                .EnableMeiZuPush("", "")
                .EnableFCMPush(false)
                .EnableVivoPush(true)
                .EnableHuaWeiPush(true).Build();
            RCIMClient.Instance.SetAndroidPushConfig(config);
            RCIMClient.Instance.Init(ExampleConfig.AppKey);
            DemoContext.AppInitialized = true;
        }

        _toastText = GameObject.Find("/Login/Toast").GetComponent<Text>();
        
        var appKeyInput = GameObject.Find("/Login/InputFieldAppKey").GetComponent<InputField>();
        appKeyInput.text = ExampleConfig.AppKey;
        appKeyInput.Select();
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

    public void OnClickLogin()
    {
        var loginUserId = GameObject.Find("/Login/InputField").GetComponent<InputField>();


        if(string.IsNullOrEmpty(ExampleConfig.AppKey))
        {
            ShowToast("请输入AppKey");
            return;
        }

        if (string.IsNullOrEmpty(loginUserId.text))
        {
            ShowToast("请输入用户ID");
            return;
        }



        var appKeyInput = GameObject.Find("/Login/InputFieldAppKey").GetComponent<InputField>();
        if (false == string.IsNullOrEmpty((appKeyInput.text)))
        {
            ExampleConfig.AppKey = appKeyInput.text;
        }

        GetTokenAndLogin(loginUserId.text);
    }

    public void OnClickSecretLogin()
    {
        GetTokenAndLogin(DemoContext.DefaultLoginUserId);
    }

    private void GetTokenAndLogin(string loginUserId)
    {
        // String current = Util.CurrentTimeStamp().ToString();
        // String id = ExampleConfig.Prefix + current;
        String url = ExampleConfig.Host + "/token/" + loginUserId;
        String json = "{\"key\":\"" + ExampleConfig.AppKey + "\"}";
        Post(url, json, (bool error, String result) =>
        {
            if (error)
            {
                ShowToast($"http login failed, {result}");
                return;
            }

            DemoContext.CurrentUser = JsonUtility.FromJson<LoginData>(result);
            RCIMClient.Instance.Connect(DemoContext.CurrentUser.Token, (errorCode, userId) =>
            {
                if (errorCode == RCErrorCode.Succeed)
                {
                    ShowToast($"登录成功...");

                    DemoContext.Connected = true;
                    RunOnMainThread.Enqueue(() =>
                    {
                        SceneManager.LoadScene("MainPage", LoadSceneMode.Single);
                    });
                }

                ShowToast($"App connect callback called. {errorCode}, {userId}");
            });
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

    private void ShowToast(String tip)
    {
        if (String.IsNullOrEmpty(tip))
            return;
        RunOnMainThread.Enqueue(() => { _toastText.text = tip; });
    }
}
