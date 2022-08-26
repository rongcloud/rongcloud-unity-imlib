using System;
using System.Collections;
using System.Collections.Generic;
using cn_rongcloud_im_unity;
using UnityEngine;
using UnityEngine.UI;

namespace cn_rongcloud_im_unity_example
{
    public abstract class BaseScene : MonoBehaviour
    {
        public GameObject ToastPrefab;
        private CanvasGroup Toast;
        private Text ToastInfo;
        private IEnumerator CurrentToast;

        protected void ShowToast(string toast)
        {
            ShowToast(toast, 2);
        }

        protected void ShowToast(string toast, int duration)
        {
            if (CurrentToast != null)
            {
                StopCoroutine(CurrentToast);
            }

            CurrentToast = MakeToast(toast, duration);
            StartCoroutine(CurrentToast);
        }

        private IEnumerator MakeToast(string toast, int duration)
        {
            ToastInfo.text = toast;
            yield return fadeInAndOut(true, 0.5f);

            float counter = 0;
            while (counter < duration)
            {
                counter += Time.deltaTime;
                yield return null;
            }

            yield return fadeInAndOut(false, 0.5f);
        }

        private IEnumerator fadeInAndOut(bool fadeIn, float duration)
        {
            float from, to;
            if (fadeIn)
            {
                from = 0f;
                to = 1f;
            }
            else
            {
                from = 1f;
                to = 0f;
            }

            float counter = 0f;
            while (counter < duration)
            {
                counter += Time.deltaTime;
                float alpha = Mathf.Lerp(from, to, counter / duration);
                Toast.alpha = alpha;
                yield return null;
            }
        }

        protected void Start()
        {
            Toast = GameObject.Find("/Toast/Toast").GetComponent<CanvasGroup>();
            ToastInfo = GameObject.Find("/Toast/Toast/Info").GetComponent<Text>();
            ToastInfo.text = "";
            Toast.alpha = 0;
        }

        public abstract void print(string text, bool clearScreen = false);
        
       
    }
}