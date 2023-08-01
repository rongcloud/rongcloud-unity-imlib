using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleFileBrowser;

namespace cn_rongcloud_im_wapper_unity_example
{
    public class ExampleUtils : MonoBehaviour
    {
        public GameObject Loading;
        public CanvasGroup Toast;
        public Text ToastInfo;

        private int LoadingCount;
        private IEnumerator CurrentToast;

        private static ExampleUtils utils;

        private void Start()
        {
            utils = this;
        }

        public static void ShowLoading()
        {
            utils._ShowLoading();
        }

        public static void HideLoading()
        {
            utils._HideLoading();
        }

        public static void ShowToast(String toast, int duration = 2)
        {
            utils._ShowToast(toast, duration);
        }

        public static T GetValue<T>(Dictionary<String,object> dictionary, String key, T defultVal)
        {
            dictionary.TryGetValue(key, out object value);
            if (value is T v)
            {
                return v;
            }
            return defultVal;
        }

        public static void PickImage(Action<string> action)
        {
#if UNITY_ANDROID
            ShowLoading();
            FileBrowser.SetFilters(false, new FileBrowser.Filter("Images", ".jpg", ".png"));
            FileBrowser.ShowLoadDialog((string[] paths) =>
            {
                HideLoading();
                if (paths.Length > 0)
                {
                    Debug.Log("File path: " + paths[0]);
                    action(paths[0]);
                }
            }, () =>
            {
                HideLoading();
            }, FileBrowser.PickMode.Files);
#else
            ShowLoading();
            NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
            {
                HideLoading();
                if (path != null)
                {
                    Debug.Log("Image path: " + path);
#if UNITY_ANDROID
                    action($"file://{path}");
#else
                    action(path);
#endif
                }
            });

            if (permission != NativeGallery.Permission.Granted)
            {
                ShowToast("没有权限");
                HideLoading();
            }
            Debug.Log("Permission result: " + permission);
#endif
        }

        public static void PickVideo(Action<string,int> action)
        {
#if UNITY_ANDROID
            ShowLoading();
            FileBrowser.SetFilters(false, new FileBrowser.Filter("Videos", ".mp4", ".mkv", ".mov"));
            FileBrowser.ShowLoadDialog((string[] paths) =>
            {
                HideLoading();
                if (paths.Length > 0)
                {
                    Debug.Log("File path: " + paths[0]);
                    action(paths[0], 15);
                }
            }, () =>
            {
                HideLoading();
            }, FileBrowser.PickMode.Files);
#else
            ShowLoading();
            NativeGallery.Permission permission = NativeGallery.GetVideoFromGallery((path) =>
            {
                HideLoading();
                if (path != null)
                {
                    Debug.Log("Video path: " + path);
                    var property = NativeGallery.GetVideoProperties(path);
#if UNITY_ANDROID
                    action($"file://{path}", (int)property.duration / 1000);
#else
                    action(path, (int)property.duration/1000);
#endif
                }
            });

            if (permission != NativeGallery.Permission.Granted)
            {
                ShowToast("没有权限");
                HideLoading();
            }
            Debug.Log("Permission result: " + permission);
#endif
        }

        public static void PickAudio(Action<string, int> action)
        {
#if UNITY_ANDROID
            ShowLoading();
            FileBrowser.SetFilters(false, new FileBrowser.Filter("Audios", ".mp3", ".aac", ".amr"));
            FileBrowser.ShowLoadDialog((string[] paths) =>
            {
                HideLoading();
                if (paths.Length > 0)
                {
                    Debug.Log("File path: " + paths[0]);
                    action(paths[0],15);
                }
            }, () =>
            {
                HideLoading();
            }, FileBrowser.PickMode.Files);
#else
            PickFile(new[] {"mp3","aac","amr"}, (path) =>
            {
                action(path, 15);
            });
#endif
        }

        public static void PickFile(Action<string> action)
        {
#if UNITY_ANDROID
            ShowLoading();
            FileBrowser.SetFilters(true);
            FileBrowser.ShowLoadDialog((string[] paths) =>
            {
                HideLoading();
                if (paths.Length > 0)
                {
                    Debug.Log("File path: " + paths[0]);
                    action(paths[0]);
                }
            }, () =>
            {
                HideLoading();
            }, FileBrowser.PickMode.Files);
#else
            PickFile(new[] { "public.content",
                         "public.text",
                         "public.source-code",
                         "public.image",
                         "public.audiovisual-content",
                         "com.adobe.pdf",
                         "com.apple.keynote.key",
                         "com.microsoft.word.doc",
                         "com.microsoft.excel.xls",
                         "com.microsoft.powerpoint.ppt" }, action);
#endif
        }

        public static void PickFile(string type, Action<string> action)
        {
#if UNITY_ANDROID
            ShowLoading();
            FileBrowser.SetFilters(false, new FileBrowser.Filter("Custom", type));
            FileBrowser.ShowLoadDialog((string[] paths) =>
            {
                HideLoading();
                if (paths.Length > 0)
                {
                    Debug.Log("File path: " + paths[0]);
                    action(paths[0]);
                }
            }, () =>
            {
                HideLoading();
            }, FileBrowser.PickMode.Files);
#else
            ShowLoading();
            var fileType = NativeFilePicker.ConvertExtensionToFileType(type);
            NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
            {
                HideLoading();
                if (path != null)
                {
                    Debug.Log("File path: " + path);
#if UNITY_ANDROID
                    action($"file://{path}");
#else
                    action(path);
#endif
                }
            }, new string[] { fileType });

            if (permission != NativeFilePicker.Permission.Granted)
            {
                ShowToast("没有权限");
                HideLoading();
            }
            Debug.Log("Permission result: " + permission);
#endif
        }

        private static void PickFile(string[] allowedFileTypes, Action<string> action)
        {
            ShowLoading();
            NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
            {
                HideLoading();
                if (path != null)
                {
                    Debug.Log("File path: " + path);
#if UNITY_ANDROID
                    action($"file://{path}");
#else
                    action(path);
#endif
                }
            }, allowedFileTypes);

            if (permission != NativeFilePicker.Permission.Granted)
            {
                ShowToast("没有权限");
                HideLoading();
            }
            Debug.Log("Permission result: " + permission);
        }

        private void _ShowLoading()
        {
            LoadingCount++;
            if (!Loading.activeInHierarchy)
            {
                Loading.SetActive(true);
            }
        }

        private void _HideLoading()
        {
            LoadingCount--;
            if (LoadingCount <= 0 && Loading.activeInHierarchy)
            {
                LoadingCount = 0;
                Loading.SetActive(false);
            }
        }

        private void _ShowToast(String toast, int duration)
        {
            if (CurrentToast != null)
            {
                StopCoroutine(CurrentToast);
            }
            CurrentToast = MakeToast(toast, duration);
            StartCoroutine(CurrentToast);
        }

        private IEnumerator MakeToast(String toast, int duration)
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
    }
}