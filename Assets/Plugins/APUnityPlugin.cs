using UnityEngine;
using System.Runtime.InteropServices;

public class APUnityPlugin {

#if UNITY_ANDROID
    private static AndroidJavaClass plugin = new AndroidJavaClass("com.amoad.amoadsdk.unity.WallPlugin");
    private static AndroidJavaClass unityplayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
#endif

    [DllImport("__Internal")]
    private static extern void showAppliPromotionWall_(string orientation, bool isClose, bool onStatusArea, string appKey);
    /// <summary>
    /// ウォール広告を表示
    /// </summary>
    /// <param name="setting"></param>
    public static void ShowAppliPromotionWall(APUnityPluginSetting setting) {
        Debug.Log("APUnityPlugin.ShowAppliPromotionWall");
        if (Application.platform == RuntimePlatform.IPhonePlayer) {
            string appKey = setting.parameters.Contains(APUnityPluginSetting.iOSAppkey) ? setting.parameters[APUnityPluginSetting.iOSAppkey].ToString() : "";
            string orientation = setting.parameters.Contains(APUnityPluginSetting.orientation) ? setting.parameters[APUnityPluginSetting.orientation].ToString() : "";
            bool isClose = setting.parameters.Contains(APUnityPluginSetting.isClose) ? (bool)setting.parameters[APUnityPluginSetting.isClose] : false;
            bool onStatusArea = setting.parameters.Contains(APUnityPluginSetting.onStatusArea) ? (bool)setting.parameters[APUnityPluginSetting.onStatusArea] : false;

            showAppliPromotionWall_(orientation, isClose, onStatusArea, appKey);
        } else if (Application.platform == RuntimePlatform.Android) {
#if UNITY_ANDROID
            AndroidJavaObject activity = unityplayer.GetStatic<AndroidJavaObject>("currentActivity");
            string appKey = setting.parameters.Contains(APUnityPluginSetting.AndroidAppKey) ? setting.parameters[APUnityPluginSetting.AndroidAppKey].ToString() : "";

            plugin.CallStatic("showWall", activity, appKey);
#endif

        }
    }

    [DllImport("__Internal")]
    private static extern void showTriggerImageAd_(string triggerId, int locateX, int locateY, bool onStatusArea, string orientation, string triggerImgFailImageFileName, string appKey);
    /// <summary>
    /// Wall誘導枠（イメージ方式）を表示
    /// </summary>
    /// <param name="setting"></param>
    public static void showTriggerImageAd(APUnityPluginSetting setting) {
        Debug.Log("----- showTriggerImageAd -----");
        if (Application.platform == RuntimePlatform.IPhonePlayer) {
            Debug.Log("" + setting.parameters[APUnityPluginSetting.iOSTriggerKey].ToString());
            showTriggerImageAd_(
                setting.parameters.Contains(APUnityPluginSetting.iOSTriggerKey) ? setting.parameters[APUnityPluginSetting.iOSTriggerKey].ToString() : ""
                , setting.parameters.Contains(APUnityPluginSetting.locateX) ? (int)setting.parameters[APUnityPluginSetting.locateX] : 0
                , setting.parameters.Contains(APUnityPluginSetting.locateY) ? (int)setting.parameters[APUnityPluginSetting.locateY] : 0
                , setting.parameters.Contains(APUnityPluginSetting.onStatusArea) ? (bool)setting.parameters[APUnityPluginSetting.onStatusArea] : false
                , setting.parameters.Contains(APUnityPluginSetting.orientation) ? setting.parameters[APUnityPluginSetting.orientation].ToString() : ""
                , setting.parameters.Contains(APUnityPluginSetting.TriggerImgFailImageFileName) ? setting.parameters[APUnityPluginSetting.TriggerImgFailImageFileName].ToString() : ""
                , setting.parameters.Contains(APUnityPluginSetting.iOSAppkey) ? setting.parameters[APUnityPluginSetting.iOSAppkey].ToString() : ""
                );

        } else if (Application.platform == RuntimePlatform.Android) {
#if UNITY_ANDROID
            AndroidJavaObject activity = unityplayer.GetStatic<AndroidJavaObject>("currentActivity");
                plugin.CallStatic(
                    "showTriggerForUnity"
                    , setting.parameters.Contains(APUnityPluginSetting.AndroidAppKey) ? setting.parameters[APUnityPluginSetting.AndroidAppKey].ToString() : ""
                    , setting.parameters.Contains(APUnityPluginSetting.AndroidTriggerKey) ? setting.parameters[APUnityPluginSetting.AndroidTriggerKey].ToString() : ""
                    , setting.parameters.Contains(APUnityPluginSetting.locateX) ? (int)setting.parameters[APUnityPluginSetting.locateX] : 0
                    , setting.parameters.Contains(APUnityPluginSetting.locateY) ? (int)setting.parameters[APUnityPluginSetting.locateY] : 0

                    , setting.parameters.Contains(APUnityPluginSetting.width_dp) ? (int)setting.parameters[APUnityPluginSetting.width_dp] : 0
                    , setting.parameters.Contains(APUnityPluginSetting.height_dp) ? (int)setting.parameters[APUnityPluginSetting.height_dp] : 0

                    , setting.parameters.Contains(APUnityPluginSetting.trigger_badge_width_dp) ? (int)setting.parameters[APUnityPluginSetting.trigger_badge_width_dp] : 0
                    , setting.parameters.Contains(APUnityPluginSetting.trigger_badge_height_dp) ? (int)setting.parameters[APUnityPluginSetting.trigger_badge_height_dp] : 0

                    , setting.parameters.Contains(APUnityPluginSetting.TriggerImgFailImageFileName) ? setting.parameters[APUnityPluginSetting.TriggerImgFailImageFileName].ToString() : ""
                    , activity
                    );

//                plugin.CallStatic(
//                    "showTriggerForUnity"
//                    , setting.parameters.Contains(APUnityPluginSetting.AndroidAppKey) ? setting.parameters[APUnityPluginSetting.AndroidAppKey].ToString() : ""
//                    , setting.parameters.Contains(APUnityPluginSetting.AndroidTriggerKey) ? setting.parameters[APUnityPluginSetting.AndroidTriggerKey].ToString() : ""
//                    , setting.parameters.Contains(APUnityPluginSetting.AndroidLayoutName) ? setting.parameters[APUnityPluginSetting.AndroidLayoutName].ToString() : null
//                    , setting.parameters.Contains(APUnityPluginSetting.TriggerImgFailImageFileName) ? setting.parameters[APUnityPluginSetting.TriggerImgFailImageFileName].ToString() : ""
//                    , setting.parameters.Contains(APUnityPluginSetting.locateX) ? (int)setting.parameters[APUnityPluginSetting.locateX] : 0
//                    , setting.parameters.Contains(APUnityPluginSetting.locateY) ? (int)setting.parameters[APUnityPluginSetting.locateY] : 0
//                    , activity
//                    );

#endif

            }
    }


    [DllImport("__Internal")]
    private static extern void hideAllTriggerImageAd_();
    /// <summary>
    /// Wall誘導枠（イメージ方式）をすべて消去
    /// </summary>
    public static void hideAllTriggerImageAd() {
        Debug.Log("----- hideAllTriggerImgAd -----");
        if (Application.platform == RuntimePlatform.IPhonePlayer) {
            hideAllTriggerImageAd_();
        } else if (Application.platform == RuntimePlatform.Android) {
#if UNITY_ANDROID
            AndroidJavaObject activity = unityplayer.GetStatic<AndroidJavaObject>("currentActivity");
            plugin.CallStatic("hideAllTriggerImageAd", activity);
#endif
        }
    }

    [DllImport("__Internal")]
    private static extern void popupDisp_(string orientation, string triggerId, bool onStatusArea, string callback, string appKey);
    /// <summary>
    /// Wall誘導枠（ポップアップ方式）を表示
    /// </summary>
    public static void popupDisp(APUnityPluginSetting setting) {
        if (Application.platform == RuntimePlatform.IPhonePlayer) {
            string appKey = setting.parameters.Contains(APUnityPluginSetting.iOSAppkey) ? setting.parameters[APUnityPluginSetting.iOSAppkey].ToString() : "";
            string orientation = setting.parameters.Contains(APUnityPluginSetting.orientation) ? setting.parameters[APUnityPluginSetting.orientation].ToString() : "";
            string triggerId = setting.parameters.Contains(APUnityPluginSetting.iOSTriggerKey) ? setting.parameters[APUnityPluginSetting.iOSTriggerKey].ToString() : "";
            bool onStatusArea = setting.parameters.Contains(APUnityPluginSetting.onStatusArea) ? (bool)setting.parameters[APUnityPluginSetting.onStatusArea] : false;
            string callback = setting.parameters.Contains(APUnityPluginSetting.callbackObjectName) ? setting.parameters[APUnityPluginSetting.callbackObjectName].ToString() : "";
            popupDisp_(orientation, triggerId, onStatusArea, callback, appKey);
        } else if (Application.platform == RuntimePlatform.Android) {
#if UNITY_ANDROID
            AndroidJavaObject activity = unityplayer.GetStatic<AndroidJavaObject>("currentActivity");
            string appKey = setting.parameters.Contains(APUnityPluginSetting.AndroidAppKey) ? setting.parameters[APUnityPluginSetting.AndroidAppKey].ToString() : "";
            string triggerId = setting.parameters.Contains(APUnityPluginSetting.AndroidTriggerKey) ? setting.parameters[APUnityPluginSetting.AndroidTriggerKey].ToString() : "";
            plugin.CallStatic("startPopupForUnity", activity, appKey, triggerId);
#endif
        }
    }

    [DllImport("__Internal")]
    private static extern bool isFirstTimeInToday_();
    /// <summary>
    /// 端末内時計において、本日一度もWall広告を表示していない場合はtrue
    /// </summary>
    /// <returns></returns>
    public static bool IsFirstTimeInToday() {
        if (Application.platform == RuntimePlatform.IPhonePlayer) {
            return isFirstTimeInToday_();
        } else if (Application.platform == RuntimePlatform.Android) {
#if UNITY_ANDROID
            AndroidJavaObject activity = unityplayer.GetStatic<AndroidJavaObject>("currentActivity");
            bool ret = plugin.CallStatic<bool>("isFirstOnToday", activity);
            Debug.Log("----- [" + ret + "] -----");
            return ret;
#endif
        }
        return false;
    }


    [DllImport("__Internal")]
    private static extern void sendUUID_();
    /// <summary>
    /// コンバージョン計測用メソッド
    /// </summary>
    public static void SendUUID() {
        if (Application.platform == RuntimePlatform.IPhonePlayer) {
            sendUUID_();
        } else if (Application.platform == RuntimePlatform.Android) {
#if UNITY_ANDROIDs
            AndroidJavaObject activity = unityplayer.GetStatic<AndroidJavaObject>("currentActivity");
            plugin.CallStatic("sendConversion", activity);
#endif
        }
    }

}
