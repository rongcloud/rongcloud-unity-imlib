using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace cn_rongcloud_im_unity
{
    public class RCIMPushOptions
    {
        /// <summary>
        ///
        /// </summary>
        public string idMI { get; set; }
    
        /// <summary>
        ///
        /// </summary>
        public string appKeyMI { get; set; }
    
        /// <summary>
        ///
        /// </summary>
        public string appIdMeizu { get; set; }
    
        /// <summary>
        ///
        /// </summary>
        public string appKeyMeizu { get; set; }
    
        /// <summary>
        ///
        /// </summary>
        public string appKeyOPPO { get; set; }
    
        /// <summary>
        ///
        /// </summary>
        public string appSecretOPPO { get; set; }
    
        /// <summary>
        ///
        /// </summary>
        public bool enableHWPush { get; set; }
    
        /// <summary>
        ///
        /// </summary>
        public bool enableFCM { get; set; }
    
        /// <summary>
        ///
        /// </summary>
        public bool enableVIVOPush { get; set; }
    
        public override String ToString()
        {
            return $"idMI:{idMI} appKeyMI:{appKeyMI} appIdMeizu:{appIdMeizu} appKeyMeizu:{appKeyMeizu} appKeyOPPO:{appKeyOPPO} appSecretOPPO:{appSecretOPPO} enableHWPush:{enableHWPush} enableFCM:{enableFCM} enableVIVOPush:{enableVIVOPush}";
        }
    }
}
