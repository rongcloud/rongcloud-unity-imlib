using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEditor.Callbacks;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif


public class ImUnityBatchScript
{
    [MenuItem("RongCloudUnity/Update CocoaPods")]
    public static void test()
    {
        
    }
    
}
public class RongCloudPostProcessBuild : ScriptableObject
{
#if UNITY_IOS
    [PostProcessBuild]
    public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
    {
        if (BuildTarget.iOS != buildTarget)
        {
            return;
        }

        ModifyPBXProject(path);

        ModifyPlist(path);

        Debug.Log("Xcode修改完毕");
    }
    private static void ModifyPBXProject(string path)
    {
        Debug.Log($"path : {path}");
        string projPath = PBXProject.GetPBXProjectPath(path);
        PBXProject proj = new PBXProject();

        proj.ReadFromString(File.ReadAllText(projPath));
        string target = proj.GetUnityMainTargetGuid();

        proj.AddFrameworkToProject(target, "AVFoundation.framework", false);
        proj.AddFrameworkToProject(target, "AssetsLibrary.framework", false);
        proj.AddFrameworkToProject(target, "AudioToolbox.framework", false);
        proj.AddFrameworkToProject(target, "CFNetwork.framework", false);
        proj.AddFrameworkToProject(target, "CoreAudio.framework", false);
        proj.AddFrameworkToProject(target, "CoreGraphics.framework", false);
        proj.AddFrameworkToProject(target, "CoreLocation.framework", false);
        proj.AddFrameworkToProject(target, "CoreMedia.framework", false);
        proj.AddFrameworkToProject(target, "CoreTelephony.framework", false);
        proj.AddFrameworkToProject(target, "CoreVideo.framework", false);
        proj.AddFrameworkToProject(target, "GLKit.framework", false);
        proj.AddFrameworkToProject(target, "ImageIO.framework", false);
        proj.AddFrameworkToProject(target, "MapKit.framework", false);
        proj.AddFrameworkToProject(target, "OpenGLES.framework", false);
        proj.AddFrameworkToProject(target, "QuartzCore.framework", false);
        proj.AddFrameworkToProject(target, "SystemConfiguration.framework", false);
        proj.AddFrameworkToProject(target, "UIKit.framework", false);
        proj.AddFrameworkToProject(target, "VideoToolbox.framework", false);
        proj.AddFrameworkToProject(target, "WebKit.framework", false);
        proj.AddFrameworkToProject(target, "Photos.framework", false);
        proj.AddFrameworkToProject(target, "SafariServices.framework", false);
        proj.AddFrameworkToProject(target, "libc++.tbd", false);
        proj.AddFrameworkToProject(target, "libc++abi.tbd", false);
        proj.AddFrameworkToProject(target, "libsqlite3.tbd", false);
        proj.AddFrameworkToProject(target, "libstdc++.tbd", false);
        proj.AddFrameworkToProject(target, "libxml2.tbd", false);
        proj.AddFrameworkToProject(target, "libz.tbd", false);

        proj.AddBuildProperty(target, "OTHER_LDFLAGS", "-ObjC");
        proj.SetBuildProperty(target, "ENABLE_BITCODE", "NO");

        target = proj.GetUnityFrameworkTargetGuid();

        proj.AddFrameworkToProject(target, "AVFoundation.framework", false);
        proj.AddFrameworkToProject(target, "AssetsLibrary.framework", false);
        proj.AddFrameworkToProject(target, "AudioToolbox.framework", false);
        proj.AddFrameworkToProject(target, "CFNetwork.framework", false);
        proj.AddFrameworkToProject(target, "CoreAudio.framework", false);
        proj.AddFrameworkToProject(target, "CoreGraphics.framework", false);
        proj.AddFrameworkToProject(target, "CoreLocation.framework", false);
        proj.AddFrameworkToProject(target, "CoreMedia.framework", false);
        proj.AddFrameworkToProject(target, "CoreTelephony.framework", false);
        proj.AddFrameworkToProject(target, "CoreVideo.framework", false);
        proj.AddFrameworkToProject(target, "GLKit.framework", false);
        proj.AddFrameworkToProject(target, "ImageIO.framework", false);
        proj.AddFrameworkToProject(target, "MapKit.framework", false);
        proj.AddFrameworkToProject(target, "OpenGLES.framework", false);
        proj.AddFrameworkToProject(target, "QuartzCore.framework", false);
        proj.AddFrameworkToProject(target, "SystemConfiguration.framework", false);
        proj.AddFrameworkToProject(target, "UIKit.framework", false);
        proj.AddFrameworkToProject(target, "VideoToolbox.framework", false);
        proj.AddFrameworkToProject(target, "WebKit.framework", false);
        proj.AddFrameworkToProject(target, "Photos.framework", false);
        proj.AddFrameworkToProject(target, "SafariServices.framework", false);
        proj.AddFrameworkToProject(target, "libc++.tbd", false);
        proj.AddFrameworkToProject(target, "libc++abi.tbd", false);
        proj.AddFrameworkToProject(target, "libsqlite3.tbd", false);
        proj.AddFrameworkToProject(target, "libstdc++.tbd", false);
        proj.AddFrameworkToProject(target, "libxml2.tbd", false);
        proj.AddFrameworkToProject(target, "libz.tbd", false);

        proj.AddBuildProperty(target, "OTHER_LDFLAGS", "-ObjC");
        proj.SetBuildProperty(target, "ENABLE_BITCODE", "NO");

        File.WriteAllText(projPath, proj.WriteToString());
    }

    private static void ModifyPlist(string path)
    {
        string plistPath = path + "/Info.plist";
        PlistDocument plist = new PlistDocument();
        plist.ReadFromString(File.ReadAllText(plistPath));
        PlistElementDict rootDict = plist.root;
        rootDict.SetString("NSMicrophoneUsageDescription", "Uses Microphone");
        File.WriteAllText(plistPath, plist.WriteToString());
    }

#endif
}
