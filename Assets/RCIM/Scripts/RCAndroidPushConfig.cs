using System;
using System.Collections.Generic;

namespace cn_rongcloud_im_unity
{
    public enum RCPushType
    {
        UnKnown,
        Rong,
        HuaWei,
        XiaoMi,
        GoogleFCM,
        GoogleGCM,
        MeiZu,
        Vivo,
        Oppo,
    }

    public class RCAndroidPushConfig
    {
        public String MiAppId { get; private set; }
        public String MiAppKey { get; private set; }

        public String MeiZuAppId { get; private set; }
        public String MeiZuAppKey { get; private set; }

        public String OppoAppKey { get; private set; }
        public String OppoAppSecret { get; private set; }

        public List<RCPushType> EnabledPushTypes { get; private set; } = new List<RCPushType>();

        public String AppKey { get; private set; }
        public String PushNaviAddress { get; private set; }

        private RCAndroidPushConfig()
        {

        }

        public class Builder
        {
            private String mMiAppId;
            private String mMiAppKey;

            private String mMeiZuAppId;
            private String mMeiZuAppKey;

            private String mOppoAppKey;
            private String mOppoAppSecret;

            private List<RCPushType> _mEnabledPushTypes = new List<RCPushType>()
            {
                RCPushType.Rong
            };

            private String mAppKey;
            private String mPushNaviAddress;

            private Builder()
            {

            }

            public static Builder Create()
            {
                return new Builder();
            }

            public Builder SetAppKey(String appKey)
            {
                if (String.IsNullOrEmpty(appKey))
                    return this;
                this.mAppKey = appKey;
                return this;
            }

            public Builder SetPushNaviAddress(String pushNaviAddress)
            {
                if (String.IsNullOrEmpty(pushNaviAddress))
                    return this;
                this.mPushNaviAddress = pushNaviAddress;
                return this;
            }

            public Builder EnableHuaWeiPush(bool enable)
            {
                if (enable)
                {
                    this._mEnabledPushTypes.Add(RCPushType.HuaWei);
                }

                return this;
            }

            public Builder EnableGCMPush(bool enable)
            {
                if (enable)
                {
                    this._mEnabledPushTypes.Add(RCPushType.GoogleGCM);
                }

                return this;
            }

            public Builder EnableFCMPush(bool enable)
            {
                if (enable)
                {
                    this._mEnabledPushTypes.Add(RCPushType.GoogleFCM);
                }

                return this;
            }

            public Builder EnableMiPush(String miAppId, String miAppKey)
            {
                if (String.IsNullOrEmpty(miAppId) || String.IsNullOrEmpty(miAppKey))
                    return this;
                this.mMiAppId = miAppId;
                this.mMiAppKey = miAppKey;
                this._mEnabledPushTypes.Add(RCPushType.XiaoMi);
                return this;
            }

            public Builder EnableMeiZuPush(String meizuAppId, String meizuAppKey)
            {
                if (String.IsNullOrEmpty(meizuAppId) || String.IsNullOrEmpty(meizuAppKey))
                    return this;
                this.mMeiZuAppId = meizuAppId;
                this.mMeiZuAppKey = meizuAppKey;
                this._mEnabledPushTypes.Add(RCPushType.MeiZu);
                return this;
            }

            public Builder EnableOppoPush(String oppoAppKey, String oppoAppSecret)
            {
                if (String.IsNullOrEmpty(oppoAppKey) || String.IsNullOrEmpty(oppoAppSecret))
                    return this;
                this.mOppoAppKey = oppoAppKey;
                this.mOppoAppSecret = oppoAppSecret;
                this._mEnabledPushTypes.Add(RCPushType.Oppo);
                return this;
            }
            
            public Builder EnableVivoPush(bool enable)
            {
                if (enable)
                {
                    this._mEnabledPushTypes.Add(RCPushType.Vivo);
                }

                return this;
            }

            public RCAndroidPushConfig Build()
            {
                var pushConfig = new RCAndroidPushConfig
                {
                    AppKey = this.mAppKey,
                    PushNaviAddress = this.mPushNaviAddress,
                    MiAppId = this.mMiAppId,
                    MiAppKey = this.mMiAppKey,
                    MeiZuAppId = this.mMeiZuAppId,
                    MeiZuAppKey = this.mMeiZuAppKey,
                    OppoAppKey = this.mOppoAppKey,
                    OppoAppSecret = this.mOppoAppSecret,
                    EnabledPushTypes = this._mEnabledPushTypes
                };

                return pushConfig;
            }
        }
    }
}