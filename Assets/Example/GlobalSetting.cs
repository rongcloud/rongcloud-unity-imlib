using cn_rongcloud_im_unity;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cn_rongcloud_im_unity_example;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalSetting : MonoBehaviour
{
    private static readonly ConcurrentQueue<Action> RunOnMainThread = new ConcurrentQueue<Action>();

    public GameObject ToastPrefab;
    private CanvasGroup Toast;
    private Text ToastInfo;

#if UNITY_IPHONE
    string strToken = null;
    bool tokenSent;
#endif
    // Start is called before the first frame update
    void Start()
    {
        Toast = GameObject.Find("/Toast/Toast").GetComponent<CanvasGroup>();
        ToastInfo = GameObject.Find("/Toast/Toast/Info").GetComponent<Text>();
        ToastInfo.text = "";
        Toast.alpha = 0;
        
#if UNITY_ANDROID
        GameObject.Find("/Setting/ScrollView/Viewport/Content/InputFieldTypingUpdateInterval")
            .GetComponent<InputField>().interactable = false;
        GameObject.Find("/Setting/ScrollView/Viewport/Content/btnSetTypingUpdateSeconds")
            .GetComponent<Button>().interactable = false;
        
        GameObject.Find("/Setting/ScrollView/Viewport/Content/SliderImageCompressQuality")
            .GetComponent<Slider>().interactable = false;
        GameObject.Find("/Setting/ScrollView/Viewport/Content/btniOSSetImgCompressConfig")
            .GetComponent<Button>().interactable = false;
#endif

        GameObject.Find("/Setting/ScrollView/Viewport/Content/InputFieldStartTime")
            .GetComponent<InputField>().text = DateTime.Now.ToString("HH:mm:ss");
        GameObject.Find("/Setting/ScrollView/Viewport/Content/InputFieldStartTime")
            .GetComponent<InputField>().Select();



#if UNITY_IPHONE
        tokenSent = false;
        UnityEngine.iOS.NotificationServices.RegisterForNotifications(
            UnityEngine.iOS.NotificationType.Alert |
            UnityEngine.iOS.NotificationType.Badge |
            UnityEngine.iOS.NotificationType.Sound);
#endif
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_IPHONE
    if (!tokenSent)
    {
        byte[] token = UnityEngine.iOS.NotificationServices.deviceToken;
        if (token != null)
        {
            strToken = System.BitConverter.ToString(token);
            strToken = strToken.Replace("-", "");
            tokenSent = true;
        }
    }
#endif

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

    public void OnClickGetOfflineMsgDuration()
    {
        RCIMClient.Instance.GetOfflineMessageDuration((code, durationOfDays) =>
        {
            var toast = $"获取离线消息天数：{code}, {durationOfDays} 天";
            ShowToast(toast);
        });
    }

    public void OnClickSetOfflineMsgDuration()
    {
        var targetId = GameObject.Find("/Setting/ScrollView/Viewport/Content/InputFieldOfflineDuration/Text")
            .GetComponent<Text>();

        if (string.IsNullOrEmpty(targetId.text) || !Int32.TryParse(targetId.text, out var duration))
        {
            return;
        }

        RCIMClient.Instance.SetOfflineMessageDuration(duration,
            (code, durationOfDays) => { ShowToast($"设置离线消息天数： {code}"); });
    }

    public void OnClickSetKickReconnectedDevice()
    {
        var targetId = GameObject.Find("/Setting/ScrollView/Viewport/Content/ToggleKickLastConnectedDevice")
            .GetComponent<Toggle>();

        RCIMClient.Instance.SetKickReconnectedDevice(targetId.isOn);
        ShowToast($"设置踢出最后重连设备: {targetId.isOn}");
    }

    public void OnClickGetDeltaTime()
    {
        var deltaTime = RCIMClient.Instance.GetDeltaTime();
        ShowToast($"本地与服务器时间差: {deltaTime} 毫秒");
        GameObject.Find("/Setting/ScrollView/Viewport/Content/TextDeltaTime").GetComponent<Text>()
            .text = $"本地与服务器时间差: {deltaTime} 毫秒";
    }

    public void OnClickGetCurrentUserId()
    {
        var currentUserId = RCIMClient.Instance.GetCurrentUserID();
        ShowToast($"当前用户ID: {currentUserId}");
        GameObject.Find("/Setting/ScrollView/Viewport/Content/TextCurrUid").GetComponent<Text>()
            .text = $"当前用户ID: {currentUserId}";
    }

    public void OnClickGetNotifyQuietHours()
    {
        RCIMClient.Instance.GetNotificationQuietHours((code, quietHour) =>
        {
            RunOnMainThread.Enqueue(() =>
            {
                GameObject.Find("/Setting/ScrollView/Viewport/Content/TextQuietHour").GetComponent<Text>()
                    .text = $"当前免打扰时间设置: 起始时间 {quietHour.StartTime} 顺延 {quietHour.SpanMinutes} 分钟";
                ShowToast($"当前免打扰时间设置: {code} {Environment.NewLine} {quietHour}");
            });
        });
    }

    public void OnSliderChanged_DelayMinutes()
    {
        var rate = GameObject.Find("/Setting/ScrollView/Viewport/Content/SliderSpanMinutes")
            .GetComponent<Slider>().value;
        var strStartTime = GameObject.Find("/Setting/ScrollView/Viewport/Content/InputFieldStartTime")
            .GetComponent<InputField>().text;
        GameObject.Find("/Setting/ScrollView/Viewport/Content/TextDelayMinutes")
            .GetComponent<Text>().text = $"免打扰时间： {strStartTime} 开始并顺延 {(int)rate} 分钟";
    }

    public void OnClickSetNotifyQuietHours()
    {
        var targetId = GameObject.Find("/Setting/ScrollView/Viewport/Content/SliderSpanMinutes")
            .GetComponent<Slider>();
        var strStartTime = GameObject.Find("/Setting/ScrollView/Viewport/Content/InputFieldStartTime")
            .GetComponent<InputField>().text;

        if (false == DateTime.TryParse(strStartTime, out var startTime))
        {
            return;
        }

        RCIMClient.Instance.SetNotificationQuietHours(strStartTime, (int) targetId.value,
            code => { ShowToast($"设置免打扰时间: {code}"); });
    }

    public void OnClickRemoveNotifyQuietHours()
    {
        RCIMClient.Instance.RemoveNotificationQuietHours(code => { ShowToast($"移除免打扰时间设置: {code}"); });
    }
    
    public async void OnClickDisConnect()
    {
        RCIMClient.Instance.Disconnect();
        ShowToast($"当前连接状态：{RCIMClient.Instance.GetConnectionStatus()} 即将退出登录");
        await Task.Delay(3000);
        SceneManager.LoadScene("Login", LoadSceneMode.Single);
    }

    public void OnClickSetDeviceToken()
    {
#if UNITY_IPHONE
        if(strToken!=null)
            RCIMClient.Instance.setDeviceToken(strToken);
        Debug.Log("setDeviceToken token="+ strToken);
#endif
    }

    public async void OnClickLogout()
    {
        RCIMClient.Instance.Logout();
        ShowToast($"当前连接状态：{RCIMClient.Instance.GetConnectionStatus()}");
        await Task.Delay(3000);
        SceneManager.LoadScene("Login", LoadSceneMode.Single);
    }

    public void OnClickGetBlacklist()
    {
        RCIMClient.Instance.GetBlackList((code, blockedUserList) =>
        {
            DemoContext.BlockedUserList = blockedUserList;

            var builder = new StringBuilder();
            if (blockedUserList != null)
            {
                foreach (var blockedUser in blockedUserList)
                {
                    builder.Append(blockedUser).Append(", ");
                }
            }

            RunOnMainThread.Enqueue(() =>
            {
                ShowToast($"GetBlackList: {code} blocked user count {blockedUserList?.Count}");
                GameObject.Find("/Setting/ScrollView/Viewport/Content/TextBlacklist").GetComponent<Text>()
                        .text =
                    $"{code} 当前黑名单: {(string.IsNullOrEmpty(builder.ToString()) ? "列表为空" : builder.ToString())}";
            });
        });
    }

    public void OnSliderChanged_ImageCompressRate()
    {
        var rate = GameObject.Find("/Setting/ScrollView/Viewport/Content/SliderImageCompressQuality")
            .GetComponent<Slider>().value;
        GameObject.Find("/Setting/ScrollView/Viewport/Content/TextImgCompressRate")
            .GetComponent<Text>().text = $"{rate} %";
    }

    public void OnClickSetiOSImgCompressConfig()
    {
        var targetId = GameObject.Find("/Setting/ScrollView/Viewport/Content/SliderImageCompressQuality")
            .GetComponent<Slider>();
#if UNITY_IOS
        RCIMClient.Instance.SetImageCompressConfig(1920.0, 320.0, (targetId.value/ 100));
        ShowToast($"SetImageCompressConfig: maxSize {1920.0} minSize {320.0} quality {(targetId.value/ 100)}");
#else
        ShowToast($"SetImageCompressConfig: not available on current platform");
#endif
    }

    public void OnClickSetiOSTypingUpdateInterval()
    {
        var targetId = GameObject.Find("/Setting/ScrollView/Viewport/Content/InputFieldOfflineDuration/Text")
            .GetComponent<Text>();
        if (string.IsNullOrEmpty(targetId.text) || !Int32.TryParse(targetId.text, out var duration))
        {
            return;
        }
#if UNITY_IOS
        RCIMClient.Instance.SetTypingUpdateInterval(duration);
        ShowToast($"SetTypingUpdateInterval: duration 秒");
#else
        ShowToast($"SetTypingUpdateInterval: not available on current platform");
#endif
    }

    private void ShowToast(string toast)
    {
        if (string.IsNullOrEmpty(toast))
            return;
        RunOnMainThread.Enqueue(async () =>
        {
            ToastInfo.text = toast;
            Toast.alpha = 1;
            await Task.Delay(3000);
            Toast.alpha = 0;
        });
    }
}
