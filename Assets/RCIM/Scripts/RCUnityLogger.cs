using UnityEngine;

namespace cn_rongcloud_im_unity
{
    public class RCUnityLogger
    {
        private bool enable = false;
        private static RCUnityLogger _instance = null;

        public static RCUnityLogger getInstance()
        {
            if (_instance == null)
                _instance = new RCUnityLogger();

            return _instance;
        }

        public void setEnable(bool enable)
        {
            this.enable = enable;
        }

        public void log(string logStr)
        {
            if (this.enable)
                Debug.Log(logStr);
        }

        public void log(string method, string logStr)
        {
            if (this.enable)
                this.log($"{method}:{logStr}");
        }
    }
}