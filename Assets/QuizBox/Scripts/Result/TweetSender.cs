using UnityEngine;
using System.Text;
using System.Collections;

public class TweetSender : MonoBehaviour {

	public static string SHARE_FILE_NAME = "screenshot.png";
	public string consumerKey;
	public string consumerSecret;
	private static TweetSender sInstance;
	
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
		sInstance = this;
		DontDestroyOnLoad (gameObject);
		#if UNITY_IPHONE
		TwitterBinding.init(consumerKey,consumerSecret);
		#endif
		
		#if UNITY_ANDROID
		TwitterAndroid.init( consumerKey, consumerSecret );
		#endif

	}

	public static TweetSender Instance{
		get{
			return sInstance;
		}
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
		ShowOKDialog (title, message);
	}
	
	void loginFailed (string error) {
		Debug.Log ("Twitter login failed: " + error);

		#if UNITY_IPHONE

		#endif

		#if UNITY_ANDROID
		EtceteraAndroid.hideProgressDialog();
		#endif
		ShowErrorDialog ();
	}

	void requestDidFailEvent (string error) {
		Debug.Log ("requestDidFailEvent: " + error);
		#if UNITY_ANDROID
		EtceteraAndroid.hideProgressDialog();
		#endif
		ShowErrorDialog ();  
	}

	private void ShowErrorDialog () {
		string title = "\u30c4\u30a4\u30fc\u30c8\u5931\u6557";
		string message = "\u518d\u5ea6\u304a\u3053\u306a\u3063\u3066\u304f\u3060\u3055\u3044 ";
		ShowOKDialog (title, message);
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
		TwitterAndroid.postStatusUpdate (param);
	}
	#endif

	private void ShowCompleteDialog () {
		PrefsManager.Instance.AddUserPoint(1);
		TopController.Instance.UpdateUserPointLabel ();
		string title = "\u30c4\u30a4\u30fc\u30c8\u6210\u529f!!";
		string message = "1\u30dd\u30a4\u30f3\u30c8\u8ffd\u52a0\u3057\u307e\u3057\u305f";
		ShowOKDialog (title, message);
	}

	public void SendTweet () {
		Debug.Log ("SendTweet");
		int score = ScoreKeeper.instance.score;
		int size = QuizListManager.instance.quizList.Count;
		string result = size + "問中" + score + "問正解!!";
		StringBuilder sb = new StringBuilder ();
		sb.Append (SelectedQuiz.instance.Name + "|" + QuizListManager.instance.modeName + "\u3067");
		sb.Append ("、" + result + "\n");
		sb.Append("\u3053\u306e\u30af\u30a4\u30ba\u30a2\u30d7\u30ea\u9762\u767d\u3044\u304b\u3089\u3084\u3063\u3066\u307f\u3066\uff01"+ "\n");
		sb.Append("→http://tt5.us/quizbox #クイズボックス");
		string imagePath = Application.persistentDataPath + "/" + SHARE_FILE_NAME;
		#if UNITY_IPHONE

		TwitterBinding.showTweetComposer(sb.ToString(),imagePath);
#endif
	
#if UNITY_ANDROID
		SocialConnector.Share(sb.ToString(),"",imagePath);
#endif
	}

	public bool IsLoggedIn () {
		#if UNITY_IPHONE
		return TwitterBinding.isLoggedIn();
		#endif

		#if UNITY_ANDROID
		return TwitterAndroid.isLoggedIn();
		#endif
	}

	public void ShowLoginDialog () {
		#if UNITY_IPHONE
		TwitterBinding.showLoginDialog();
#endif

		#if UNITY_ANDROID
		EtceteraAndroid.showProgressDialog("\u30ed\u30b0\u30a4\u30f3\u3057\u3066\u3044\u307e\u3059","\u30ed\u30b0\u30a4\u30f3\u3057\u3066\u3044\u307e\u3059");
		TwitterAndroid.showLoginDialog();
#endif
	}

	void promptCancelledEvent () {
		Debug.Log ("promptCancelledEvent");
	}

	private void ShowOKDialog (string title, string message) {
		#if UNITY_IPHONE
		string[] buttons = {"OK"};
		EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
		#endif
		#if UNITY_ANDROID
		EtceteraAndroid.showAlert(title,message,"OK");
		#endif

	}

}
