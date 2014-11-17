using UnityEngine;
using System.Collections;
using System;

public class Test : MonoBehaviour {

	public string rectangleMediaId;
	public string rectangleSpotId;
	public string publisherId;

	public static string screenshotFilename = "someScreenshot.png";

	void Start () {
		IMobileSdkAdsUnityPlugin.registerInline (publisherId, rectangleMediaId, rectangleSpotId);
		IMobileSdkAdsUnityPlugin.start (rectangleSpotId);
	}

	void OnEnable(){
		Debug.Log ("enable");
	}

	void OnDisable(){
		Debug.Log ("disable");
	}

	void OnApplicationPause(bool pauseStatus){
		Debug.Log ("pause "+pauseStatus);
	}
		
}
