using UnityEngine;
using System.Collections;
using System;

public class Test : MonoBehaviour {

	public static string screenshotFilename = "someScreenshot.png";

	void Start(){

	}

	public void OnButtonClicked(){
		Debug.Log ("aa");
		Application.CaptureScreenshot( screenshotFilename );
		var pathToImage = Application.persistentDataPath + "/" + screenshotFilename;
		TwitterBinding.showTweetComposer( "I'm posting this from Unity with a fancy image: " + Time.deltaTime, pathToImage );

	}
}
