using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEditor.Callbacks;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif
public class RongCloudPostProcessBuild:ScriptableObject
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

       // ModifyPlist(path);

        Debug.Log("Xcode修改完毕");
    }
    private static void ModifyPBXProject(string path)
    {
        string projPath =PBXProject.GetPBXProjectPath(path);
        PBXProject proj = new PBXProject();

        proj.ReadFromString(File.ReadAllText(projPath));
        string target = proj.TargetGuidByName("Unity-iPhone");

        proj.AddFrameworkToProject(target, "AssetsLibrary.framework", false);
        proj.AddFrameworkToProject(target, "AudioToolbox.framework", false);
        proj.AddFrameworkToProject(target, "AVFoundation.framework", false);
        proj.AddFrameworkToProject(target, "CFNetwork.framework", false);
        proj.AddFrameworkToProject(target, "CoreAudio.framework", false);
        proj.AddFrameworkToProject(target, "CoreGraphics.framework", false);
        proj.AddFrameworkToProject(target, "CoreLocation.framework", false);
        proj.AddFrameworkToProject(target, "CoreMedia.framework", false);
        proj.AddFrameworkToProject(target, "CoreTelephony.framework", false);
        proj.AddFrameworkToProject(target, "CoreVideo.framework", false);
        proj.AddFrameworkToProject(target, "ImageIO.framework", false);
        proj.AddFrameworkToProject(target, "libc++.tbd", false);
        proj.AddFrameworkToProject(target, "libc++abi.tbd", false);
        proj.AddFrameworkToProject(target, "libsqlite3.tbd", false);
        proj.AddFrameworkToProject(target, "libstdc++.tbd", false);
        proj.AddFrameworkToProject(target, "libxml2.tbd", false);
        proj.AddFrameworkToProject(target, "libz.tbd", false);
        proj.AddFrameworkToProject(target, "MapKit.framework", false);
        proj.AddFrameworkToProject(target, "OpenGLES.framework", false);
        proj.AddFrameworkToProject(target, "QuartzCore.framework", false);
        proj.AddFrameworkToProject(target, "SystemConfiguration.framework", false);
        proj.AddFrameworkToProject(target, "UIKit.framework", false);
        proj.AddFrameworkToProject(target, "Photos.framework", false);
        proj.AddFrameworkToProject(target, "SafariServices.framework", false);


        proj.AddBuildProperty(target, "OTHER_LDFLAGS", "-ObjC");


        File.WriteAllText(projPath, proj.WriteToString());
    }
#endif
}
