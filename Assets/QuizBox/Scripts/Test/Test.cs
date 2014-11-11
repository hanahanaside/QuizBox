using UnityEngine;
using System.Collections;
using System;

public class Test : MonoBehaviour {

	private float mTime;
	private bool mPress;

	void OnPress(bool isDown){
		Debug.Log ("press " + isDown);
		mPress = isDown;
		if(isDown){
			mTime = 2.0f;
		}
	}

	void Update(){
		if(mPress){
			mTime -= Time.deltaTime;
			Debug.Log ("time = " + mTime);
		}
	}
}
