using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResultController : MonoBehaviour {

	public TweetSender tweetSender;
	public FacebookSenderIOS facebookSenderIOS;

	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel ("Top");
		}
	}
	
	public void OnRetryClick () {
		Reset ();
		Application.LoadLevel ("Game");
	}

	public void OnTopClick () {
		Reset ();
		Application.LoadLevel ("Top");
	
	}

	public void OnTwitterClick () {

#if UNITY_IPHONE
		if(!TwitterBinding.isLoggedIn()){
			TwitterBinding.showLoginDialog();
			return;
		}
#endif

#if UNITY_ANDROID
		if(!TwitterAndroid.isLoggedIn()){
			EtceteraAndroid.showProgressDialog("\u30ed\u30b0\u30a4\u30f3\u3057\u3066\u3044\u307e\u3059","\u30ed\u30b0\u30a4\u30f3\u3057\u3066\u3044\u307e\u3059");
			TwitterAndroid.showLoginDialog();
			return;
		}
#endif
		int score = ScoreKeeper.instance.score;
		int size = QuizListManager.instance.quizList.Count;
		string tweetText = size + "問中" + score + "問正解!!";
		tweetSender.SendTweet (tweetText);
	}
	
	public void OnFaceBookClick () {
		Debug.Log("facebook");
		// Note: requesting publish permissions here will result in a crash. Only read permissions are permitted.
		var permissions = new string[] { "email" };
		FacebookBinding.loginWithReadPermissions( permissions );

		// parameters are optional. See Facebook's documentation for all the dialogs and parameters that they support
		var parameters = new Dictionary<string,string>
		{
			{ "link", "http://prime31.com" },
			{ "name", "link name goes here" },
			{ "picture", "http://prime31.com/assets/images/prime31logo.png" },
			{ "caption", "the caption for the image is here" }
		};
		FacebookBinding.showDialog( "stream.publish", parameters );
	}

	private void Reset () {
		ScoreKeeper.instance.score = 0;
	}
}
