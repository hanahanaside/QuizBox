using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class APUnityPluginSetting {
    public static readonly string iOSAppkey = "iOSAppKey";
    public static readonly string iOSTriggerKey = "iOSTriggerKey";
    public static readonly string AndroidAppKey = "AndroidAppKey";
    public static readonly string AndroidTriggerKey = "AndroidTriggerKey";
    public static readonly string locateX = "locateX";
    public static readonly string locateY = "locateY";
    public static readonly string onStatusArea = "onStatusArea";
    public static readonly string orientation = "orientation";
    public static readonly string isClose = "isClose";
    public static readonly string TriggerImgFailImageFileName = "TriggerImgFailImageFileName";
    public static readonly string AndroidLayoutName = "AndroidLayoutName";
    public static readonly string callbackObjectName = "callbackObjectName";

    public static readonly string width_dp = "width_dp";
    public static readonly string height_dp = "height_dp";
    public static readonly string trigger_badge_width_dp = "trigger_badge_width_dp";
    public static readonly string trigger_badge_height_dp = "trigger_badge_height_dp";

    public Hashtable parameters { get; private set; }

    public APUnityPluginSetting() {
        parameters = new Hashtable();
    }

    public void setParams(Hashtable parameters) {
        foreach (DictionaryEntry item in parameters) {
            parameters.Add(item.Key, item.Value);
        }
    }

    public void setParam(string key, object value) {
        parameters.Add(key, value);
    }

}
