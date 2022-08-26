using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Game {

    /// <summary>
    /// Actions for command line build
    /// </summary>
    public class BuildActions {

        /// <summary>
        /// Get name from here or Config
        /// </summary>
        static string GAME_NAME = "UnityIM";

        /// <summary>
        /// App version
        /// </summary>
        static string VERSION = Application.version;

        /// <summary>
        /// Current project source path
        /// </summary>
        public static string APP_FOLDER = Directory.GetCurrentDirectory ();

        public static string IMLIB_FOLDER = string.Format("{0}/Builds/IMLib/", APP_FOLDER);

        /// <summary>
        /// Android files path
        /// </summary>
        public static string ANDROID_FOLDER = string.Format ("{0}/Builds/Android/", APP_FOLDER);

        /// <summary>
        /// Android dev filename
        /// </summary>
        public static string ANDROID_DEVELOPMENT_FILE = string.Format ("{0}/Builds/Android/{1}_debug.apk", APP_FOLDER, GAME_NAME);

        /// <summary>
        /// Android release filename
        /// </summary>
        public static string ANDROID_RELEASE_FILE = string.Format ("{0}/Builds/Android/{1}_release.apk", APP_FOLDER, GAME_NAME);

        /// <summary>
        /// iOS files path
        /// </summary>
        public static string IOS_FOLDER = string.Format ("{0}/Builds/iOS/", APP_FOLDER);

        /// <summary>
        /// iOS dev path
        /// </summary>
        public static string IOS_DEVELOPMENT_FOLDER = string.Format ("{0}/Builds/iOS/build/development", APP_FOLDER);

        /// <summary>
        /// iOS release path
        /// </summary>
        public static string IOS_RELEASE_FOLDER = string.Format ("{0}/Builds/iOS/build/release", APP_FOLDER);

        /// <summary>
        /// Get active scene list
        /// </summary>
        static string[] GetScenes () {
            List<string> scenes = new List<string> ();
            for (int i = 0; i < EditorBuildSettings.scenes.Length; i++) {
                if (EditorBuildSettings.scenes[i].enabled) {
                    scenes.Add (EditorBuildSettings.scenes[i].path);
                }
            }
            return scenes.ToArray ();
        }

        /// <summary>
        /// Run iOS development build
        /// </summary>
        static void iOSDevelopment () {
            PlayerSettings.SetScriptingBackend (BuildTargetGroup.iOS, ScriptingImplementation.IL2CPP);
            PlayerSettings.SetScriptingDefineSymbolsForGroup (BuildTargetGroup.iOS, "DEV");
            EditorUserBuildSettings.SwitchActiveBuildTarget (BuildTargetGroup.iOS, BuildTarget.iOS);
            EditorUserBuildSettings.development = true;
            BuildReport report = BuildPipeline.BuildPlayer (GetScenes(), IOS_FOLDER, BuildTarget.iOS, BuildOptions.None);
            int code = (report.summary.result == BuildResult.Succeeded) ? 0 : 1;
            EditorApplication.Exit (code);    
        }

        /// <summary>
        /// Run iOS release build
        /// </summary>
        static void iOSRelease () {
            PlayerSettings.SetScriptingBackend (BuildTargetGroup.iOS, ScriptingImplementation.IL2CPP);
            PlayerSettings.SetScriptingDefineSymbolsForGroup (BuildTargetGroup.iOS, "");
            EditorUserBuildSettings.SwitchActiveBuildTarget (BuildTargetGroup.iOS, BuildTarget.iOS);
            BuildReport report = BuildPipeline.BuildPlayer (GetScenes(), IOS_FOLDER, BuildTarget.iOS, BuildOptions.None);
            int code = (report.summary.result == BuildResult.Succeeded) ? 0 : 1;
            EditorApplication.Exit (code);    
        }

        /// <summary>
        /// Run android development build
        /// </summary>
        static void AndroidDevelopment () {
            PlayerSettings.SetScriptingBackend (BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
            PlayerSettings.SetScriptingDefineSymbolsForGroup (BuildTargetGroup.Android, "DEV");
            PlayerSettings.Android.keystorePass = "5173rongcloud";
            PlayerSettings.Android.keyaliasPass = "5173rongcloud";
            EditorUserBuildSettings.SwitchActiveBuildTarget (BuildTargetGroup.Android, BuildTarget.Android);
            EditorUserBuildSettings.exportAsGoogleAndroidProject = false;
            EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
            EditorUserBuildSettings.development = true;
            EditorUserBuildSettings.androidETC2Fallback = AndroidETC2Fallback.Quality32Bit;
            BuildReport report = BuildPipeline.BuildPlayer(GetScenes(), ANDROID_DEVELOPMENT_FILE, BuildTarget.Android, BuildOptions.None);
            int code = (report.summary.result == BuildResult.Succeeded) ? 0 : 1;
            EditorApplication.Exit (code);    
        }

        /// <summary>
        /// Run android release build
        /// </summary>
        static void AndroidRelease () {
            PlayerSettings.SetScriptingBackend (BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
            PlayerSettings.SetScriptingDefineSymbolsForGroup (BuildTargetGroup.Android, "");
            PlayerSettings.Android.keystorePass = "5173rongcloud";
            PlayerSettings.Android.keyaliasPass = "5173rongcloud";
            EditorUserBuildSettings.SwitchActiveBuildTarget (BuildTargetGroup.Android, BuildTarget.Android);
            EditorUserBuildSettings.exportAsGoogleAndroidProject = false;
            EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
            EditorUserBuildSettings.androidETC2Fallback = AndroidETC2Fallback.Quality32Bit;
            BuildReport report = BuildPipeline.BuildPlayer (GetScenes(), ANDROID_RELEASE_FILE, BuildTarget.Android, BuildOptions.None);
            int code = (report.summary.result == BuildResult.Succeeded) ? 0 : 1;
            EditorApplication.Exit (code);    
        }

        static async void ExportIMLibAsset()
        {
            var assetBundles = new List<AssetBundleBuild>();
            assetBundles.Add(new AssetBundleBuild()
            {
                assetBundleName = "Editor",
                assetNames = new string[] { "BuildActions.cs" }
            });
            assetBundles.Add(new AssetBundleBuild()
            {
                assetBundleName = "Plugins\\Android",
                assetNames = new string[] { "*.*" }
            });
            assetBundles.Add(new AssetBundleBuild()
            {
                assetBundleName = "Plugins\\iOS",
                assetNames = new string[] { "*.*" }
            });
            assetBundles.Add(new AssetBundleBuild()
            {
                assetBundleName = "Scripts\\RCClient",
                assetNames = new string[] { "*.*" }
            });
            var manifest = BuildPipeline.BuildAssetBundles(IMLIB_FOLDER, assetBundles.ToArray(), BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.Android | BuildTarget.iOS);
            EditorApplication.Exit(0);
        }

        static async void Wait()
        {
            while (true)
            {
                if (BuildPipeline.isBuildingPlayer)
                {
                    await System.Threading.Tasks.Task.Delay(3000);
                }
                else
                {
                    return;
                }
            }
        }
    }

}