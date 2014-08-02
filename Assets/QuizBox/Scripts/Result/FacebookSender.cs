using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FacebookSender : MonoBehaviour {

	// common event handler used for all graph requests that logs the data to the console
	void completionHandler (string error, object result) {
		Debug.Log ("completionHandler");
		if (error != null)
			Debug.LogError (error);
		else
			Prime31.Utils.logObject (result);
	}

	void Start () {
#if UNITY_IPHONE
		FacebookBinding.init ();
#endif

#if UNITY_ANDROID
		FacebookAndroid.init();
#endif
	}

	public void ShowShareDialog () {
		Dictionary<string,object> dictionary = new Dictionary<string,object>();
		dictionary.Add("name","クイズボックス");
		dictionary.Add("description","desc");
		dictionary.Add("link","http://www.yahoo.co.jp/");
	//	dictionary.Add("caption","caption");
#if UNITY_IPHONE
		FacebookBinding.showFacebookShareDialog(dictionary);
#endif

#if UNITY_ANDROID
		FacebookAndroid.showFacebookShareDialog(dictionary);
#endif
	}

	public bool IsSessionValid () {
#if UNITY_IPHONE
		return FacebookBinding.isSessionValid();
#endif

#if UNITY_ANDROID
	return	FacebookAndroid.isSessionValid();
#endif

	}

	public void Login(){
		#if UNITY_IPHONE
		FacebookBinding.login();
#endif

#if UNITY_ANDROID
		FacebookAndroid.login();
#endif
	}

}
