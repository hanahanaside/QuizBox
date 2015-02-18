using UnityEngine;
using System.Collections;
using System;

public class Test : MonoBehaviour {

	public string rectangleMediaId;
	public string rectangleSpotId;
	public string publisherId;

	public static string screenshotFilename = "someScreenshot.png";
	public UILabel label;

	void Awake () {

	}

	void Update(){
	
	}

	public void OnButtonClicked(){
		label.GetComponent<TypewriterEffect> ().ResetToBeginning();
		label.text = "bbbbbbbbbbbbb";
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
