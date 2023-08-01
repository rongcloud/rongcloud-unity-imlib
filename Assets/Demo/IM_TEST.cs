using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using cn_rongcloud_im_unity;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

namespace cn_rongcloud_im_wapper_unity_example
{
    public class IM_TEST : MonoBehaviour
    {
        public GameObject SectionItem_Prefab;
        public GameObject ButtonItem_Prefab;
        public GameObject InputItem_Prefab;
        public GameObject ResultItem_Prefab;

        private GameObject interface_list;
        private GameObject params_input;
        private GameObject params_input_list;
        private GameObject result_check;
        private GameObject result_check_list;

        delegate bool OnParamsConfirmDelegate(Dictionary<String, object> arg);

        private Dictionary<string, Interface> interfaces = new Dictionary<string, Interface>();
        private OnParamsConfirmDelegate params_input_action { set; get; }
        private List<String> results = new List<String>();

        void Start()
        {
            RCUnityLogger.getInstance().setEnable(true);
            interface_list = GameObject.Find("/Canvas/Scroll View/Viewport/Content");
            params_input = GameObject.Find("/Input");
            params_input_list = GameObject.Find("/Input/Scroll View/Viewport/Content");
            result_check = GameObject.Find("/Result");
            result_check_list = GameObject.Find("/Result/Scroll View/Viewport/Content");

            params_input.SetActive(false);
            result_check.SetActive(false);

            EngineManager.Instance.ResultHandle = OnEventResultHandle;

            float height = 0;

            foreach (var item in IM_TEST_CONFIG.modules)
            {
                GameObject section = Instantiate(SectionItem_Prefab, interface_list.transform);
                section.name = item;
                section.transform.Find("Text").GetComponent<Text>().text = item;
                section.GetComponent<RectTransform>().localPosition = new Vector3(0, height, 0);

                height -= section.GetComponent<RectTransform>().rect.height;

                if (IM_TEST_CONFIG.modules_info.ContainsKey(item))
                {
                    float itemHeight = -section.GetComponent<RectTransform>().rect.height;
                    string[] interface_strs = IM_TEST_CONFIG.modules_info[item];
                    foreach (var interface_str in interface_strs)
                    {
                        Interface inter = JsonUtility.FromJson<Interface>(interface_str);
                        interfaces.Add(inter.title, inter);
                        GameObject button = Instantiate(ButtonItem_Prefab, section.transform);
                        button.name = inter.title;
                        button.transform.Find("Button/Text").GetComponent<Text>().text = inter.title;
                        button.GetComponent<RectTransform>().localPosition = new Vector3(0, itemHeight, 0);
                        Button action = button.transform.GetComponentInChildren<Button>();
                        action.onClick.AddListener(() =>
                        {
                            OnClickInterfaceAction(inter.title);
                        });
                        itemHeight -= button.GetComponent<RectTransform>().rect.height;
                        height -= button.GetComponent<RectTransform>().rect.height;
                    }
                }
            }

            interface_list.GetComponent<RectTransform>().sizeDelta = new Vector2(0, -height);
        }

        void OnClickInterfaceAction(string title)
        {
            Debug.Log("点击了" + title);
            Interface old = interfaces[title];
            Interface inter = new Interface
            {
                title = old.title,
                parameters = old.parameters,
                action = old.action
            };
            if (IM_TEST_CONFIG.cb_method_list.Contains(inter.action))
            {
                List<Parameter> parameters;
                if (inter.parameters != null)
                {
                    parameters = new List<Parameter>(inter.parameters);
                    parameters.Add(IM_TEST_CONFIG.cb_param);
                    inter.parameters = parameters.ToArray();
                }
                else
                {
                    parameters = new List<Parameter>();
                    parameters.Add(IM_TEST_CONFIG.cb_param);
                    inter.parameters = parameters.ToArray();
                }

            }
            if (inter.parameters != null && inter.parameters.Length > 0)
            {
                ResetParamsInput(inter, (arg) =>
                {
                    Type type = typeof(EngineManager);
                    MethodInfo method = type.GetMethod(inter.action);
                    Debug.Log("传入参数" + JsonConvert.SerializeObject(arg));
                    object[] args = { arg };
                    object ret = method.Invoke(EngineManager.Instance, args);
                    if (ret != null) {
                        return (bool)ret;
                    }else{
                        return true;
                    }
                });
            }
            else
            {
                Type type = typeof(EngineManager);                
                MethodInfo method = type.GetMethod(inter.action);
                method.Invoke(EngineManager.Instance, new object[] { });
            }
        }

        public void OnClickParamsInputCancel()
        {
            params_input.SetActive(false);
        }

        public void OnClickParamsInputConfirm()
        {
            Dictionary<String, object> result = new Dictionary<string, object>();
            var count = params_input_list.transform.childCount;
            for (int i = 0; i < count; i++)
            {
                Transform tf = params_input_list.transform.GetChild(i);
                InputField input = tf.GetComponentInChildren<InputField>();
                if (input.text != null && input.text.Length > 0)
                {
                    result.Add(tf.gameObject.name, input.text);
                }
            }
            bool? invoke = params_input_action?.Invoke(result);
            if (invoke.GetValueOrDefault(true)) {
                params_input.SetActive(false);
            }
        }

        public void OnClickResultOpen()
        {
            result_check.SetActive(true);
            var count = result_check_list.transform.childCount;
            for (int i = 0; i < count; i++)
            {
                Destroy(result_check_list.transform.GetChild(i).gameObject);
            }
            int index = results.Count;
            foreach (var item in results)
            {
                GameObject result = Instantiate(ResultItem_Prefab, result_check_list.transform);
                result.transform.GetComponent<Button>().onClick.AddListener(() =>
                {
                    GUIUtility.systemCopyBuffer = item;
                    ExampleUtils.ShowToast("已复制到剪切板");
                });
                result.transform.Find("Index").GetComponent<Text>().text = index.ToString();
                string json = item;
                try
                {
                    if (item.Length > 5000)
                    {
                        json = item.Substring(0, 5000);
                    }
                }
                catch (Exception ex)
                {
                    Debug.Log(ex);
                }
                result.transform.Find("Text").GetComponent<Text>().text = json;
                index -= 1;
            }
            StartCoroutine(LayoutResultList());
        }

        private IEnumerator LayoutResultList()
        {
            yield return new WaitForEndOfFrame();

            float height = 0;
            var count = result_check_list.transform.childCount;
            for (int i = 0; i < count; i++)
            {
                Transform tf = result_check_list.transform.GetChild(i);
                RectTransform trans = tf.GetComponent<RectTransform>();
                tf.GetComponent<RectTransform>().sizeDelta = new Vector2(0, trans.rect.height);
                tf.GetComponent<RectTransform>().localPosition = new Vector3(0, height, 0);
                height -= trans.rect.height;
            }

            result_check_list.GetComponent<RectTransform>().sizeDelta = new Vector2(0, -height);
        }

        public void OnClickResultClose()
        {
            result_check.SetActive(false);
        }

        public void OnClickResultClear()
        {
            results.Clear();
            var count = result_check_list.transform.childCount;
            for (int i = 0; i < count; i++)
            {
                Destroy(result_check_list.transform.GetChild(i).gameObject);
            }
            result_check_list.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
        }

        /// <summary>
        /// 展示参数输入界面
        /// </summary>
        /// <param name="inter"></param>
        private void ResetParamsInput(Interface inter, OnParamsConfirmDelegate action)
        {
            params_input.SetActive(true);
            params_input.transform.Find("Title").GetComponent<Text>().text = inter.title+"参数设置";
            params_input_action = action;
            var count = params_input_list.transform.childCount;
            for (int i = 0; i < count; i++)
            {
                Destroy(params_input_list.transform.GetChild(i).gameObject);
            }
            float height = 0;
            foreach (var item in inter.parameters)
            {
                GameObject input = Instantiate(InputItem_Prefab, params_input_list.transform);
                input.name = item.key;
                input.transform.Find("Text").GetComponent<Text>().text = GetText(item.lable);
                input.transform.Find("InputField/Placeholder").GetComponent<Text>().text = GetText(item.hint);
                if (item.type != "String")
                {
                    input.transform.Find("InputField").GetComponent<InputField>().contentType = InputField.ContentType.IntegerNumber;
                }
                #region 测试用
                if (inter.action == "initEngine")
                {
                    if (item.key == "appkey")
                    {
                        input.transform.Find("InputField").GetComponent<InputField>().text = Config.AppKey;
                    }
                }
                if (inter.action == "connect")
                {
                    if (item.key == "token")
                    {
                        input.transform.Find("InputField").GetComponent<InputField>().text = Config.Token;
                    }
                    if (item.key == "timeout")
                    {
                        input.transform.Find("InputField").GetComponent<InputField>().text = "0";
                    }
                }
                #endregion
                input.GetComponent<RectTransform>().localPosition = new Vector3(0, height, 0);

                height -= input.GetComponent<RectTransform>().rect.height;
            }
            params_input_list.GetComponent<RectTransform>().sizeDelta = new Vector2(0, -height);
        }

        private string GetText(string text)
        {
            if (text == null)
            {
                return "";
            }
            if (text.Contains("COMMON."))
            {
                var key = text.Replace("COMMON.", "");
                IM_TEST_CONFIG.common_info.TryGetValue(key, out string value);
                return value ?? text;
            }
            return text;
        }


        private void OnEventResultHandle(Dictionary<String,object> result)
        {
            string json = JsonConvert.SerializeObject(result, Formatting.Indented);
            results.Insert(0, json);
        }

    }
}