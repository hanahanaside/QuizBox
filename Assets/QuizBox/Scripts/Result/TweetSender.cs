using UnityEngine;
using System.Collections;

public class TweetSender : MonoBehaviour {

	public string consumerKey;
	public string consumerSecret;
#if UNITY_ANDROID
	private string mTweetText;
#endif

	void OnEnable () {
		TwitterManager.tweetSheetCompletedEvent += tweetSheetCompletedEvent;
		TwitterManager.requestDidFinishEvent += requestDidFinishEvent;
		TwitterManager.requestDidFailEvent += requestDidFailEvent;
		TwitterManager.loginSucceededEvent += loginSucceeded;
		TwitterManager.loginFailedEvent += loginFailed;

#if UNITY_ANDROID
		EtceteraAndroidManager.promptFinishedWithTextEvent += promptFinishedWithTextEvent;
		EtceteraAndroidManager.promptCancelledEvent += promptCancelledEvent;
#endif
	}
	
	void OnDisable () {
		TwitterManager.tweetSheetCompletedEvent -= tweetSheetCompletedEvent;
		TwitterManager.requestDidFinishEvent -= requestDidFinishEvent;
		TwitterManager.requestDidFailEvent -= requestDidFailEvent;
		TwitterManager.loginSucceededEvent -= loginSucceeded;
		TwitterManager.loginFailedEvent -= loginFailed;
		#if UNITY_ANDROID
		EtceteraAndroidManager.promptFinishedWithTextEvent -= promptFinishedWithTextEvent;
		EtceteraAndroidManager.promptCancelledEvent -= promptCancelledEvent;
		#endif
	}

	void Start () {
		#if UNITY_IPHONE
		TwitterBinding.init(consumerKey,consumerSecret);
		#endif
		
		#if UNITY_ANDROID
		TwitterAndroid.init( consumerKey, consumerSecret );
		#endif

	}

	void loginSucceeded (string username) {
		Debug.Log ("Successfully logged in to Twitter: " + username);
		string title = "\u30ed\u30b0\u30a4\u30f3\u6210\u529f!!";
		string message = "\u3082\u30461\u5ea6\u30c4\u30a4\u30fc\u30c8\u3057\u3066\u304f\u3060\u3055\u3044";

		#if UNITY_IPHONE

		#endif

		#if UNITY_ANDROID
		EtceteraAndroid.hideProgressDialog();
		#endif
		ShowOKDialog(title,message);
	}
	
	void loginFailed (string error) {
		Debug.Log ("Twitter login failed: " + error);

		#if UNITY_IPHONE

		#endif

		#if UNITY_ANDROID
		EtceteraAndroid.hideProgressDialog();
		#endif
		ShowErrorDialog();
	}

	void requestDidFailEvent (string error) {
		Debug.Log ("requestDidFailEvent: " + error);
		#if UNITY_ANDROID
		EtceteraAndroid.hideProgressDialog();
		#endif
		ShowErrorDialog();  
	}

	private void ShowErrorDialog () {
		string title = "\u30c4\u30a4\u30fc\u30c8\u5931\u6557";
		string message = "\u518d\u5ea6\u304a\u3053\u306a\u3063\u3066\u304f\u3060\u3055\u3044 ";
		ShowOKDialog(title,message);
	}

	void requestDidFinishEvent (object result) {
		Debug.Log ("requestDidFinishEvent");
		#if UNITY_ANDROID
		EtceteraAndroid.hideProgressDialog();
		#endif
		if (result != null) {
			Prime31.Utils.logObject (result);
			ShowCompleteDialog ();
		}
	}
	
	void tweetSheetCompletedEvent (bool didSucceed) {
		Debug.Log ("tweetSheetCompletedEvent " + didSucceed);
		#if UNITY_ANDROID
		EtceteraAndroid.hideProgressDialog();
		#endif
		if (didSucceed) {
			ShowCompleteDialog ();
		}
	}
	
	#if UNITY_ANDROID
	void promptFinishedWithTextEvent (string param) {
		Debug.Log ("promptFinishedWithTextEvent: " + param);
		TwitterAndroid.postStatusUpdate (mTweetText);
	}
	#endif

	private void ShowCompleteDialog () {
		string title = "\u30c4\u30a4\u30fc\u30c8\u6210\u529f!!";
		string message = "\u30c4\u30a4\u30fc\u30c8\u3057\u307e\u3057\u305f";
		ShowOKDialog(title,message);
	}

	public void SendTweet (string tweetText) {
		Debug.Log ("SendTweet");
#if UNITY_IPHONE
		TwitterBinding.showTweetComposer(tweetText);
#endif
	
#if UNITY_ANDROID
		mTweetText = tweetText;
		EtceteraAndroid.showAlertPrompt("Twitter" ,"Message","",mTweetText,"\u30b7\u30a7\u30a2\u3059\u308b","cancel");
#endif
	}

	void promptCancelledEvent () {
		Debug.Log ("promptCancelledEvent");
	}

	private void ShowOKDialog(string title,string message){
		#if UNITY_IPHONE
		string[] buttons = {"OK"};
		EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
		#endif
		#if UNITY_ANDROID
		EtceteraAndroid.showAlert(title,message,"OK");
		#endif

	}

}
