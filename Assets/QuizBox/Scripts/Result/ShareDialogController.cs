using UnityEngine;
using System.Collections;
using System.Text;

public class ShareDialogController : MonoBehaviour {

	#if UNITY_ANDROID
	private bool mTweeted = false;
	#endif

	#if UNITY_ANDROID
	void OnApplicationPause (bool pauseSatatus) {
		Debug.Log ("pause = " + pauseSatatus);
		if(pauseSatatus){
			return;
		}
		if (mTweeted) {
			mTweeted = false;
			PrefsManager.Instance.AddUserPoint (1);
			string title = "ツイートしました";
			string message = "1ptゲット！";
			EtceteraAndroid.showAlert (title,message,"OK");
		}
	}
	#endif

	void tweetSheetCompletedEvent (bool didSucceed) {
		Debug.Log ("tweetSheetCompletedEvent " + didSucceed);
	
		#if UNITY_IPHONE
		TwitterManager.tweetSheetCompletedEvent -= tweetSheetCompletedEvent;
		#endif
		#if UNITY_ANDROID
		EtceteraAndroid.hideProgressDialog();
		#endif
		if (didSucceed) {
			PrefsManager.Instance.AddUserPoint(1);
			TopController.instance.UpdateUserPointLabel ();
			GameObject.Find ("A_ShareButton").SetActive(false);
			ShowCompleteDialog ();
		}
	}

	public void OnCloseButtonClicked () {
		#if UNITY_IPHONE
		TwitterManager.tweetSheetCompletedEvent -= tweetSheetCompletedEvent;
		#endif
		Destroy (transform.parent.gameObject);
	}
		
	public void OnTweetButtonClicked () {
		SendTweet ();
		#if UNITY_ANDROID
		mTweeted = true;
		#endif
	}
		
	private void SendTweet(){
		Debug.Log ("SendTweet");
		int score = ScoreKeeper.instance.score;
		int size = QuizListManager.instance.quizList.Count;
		string result = size + "問中" + score + "問正解!!";
		StringBuilder sb = new StringBuilder ();
		sb.Append (SelectedQuiz.instance.Name + "｜" + QuizListManager.instance.modeName + "\u3067");
		sb.Append ("、" + result + "\n");
		sb.Append("\u3053\u306e\u30af\u30a4\u30ba\u30a2\u30d7\u30ea\u9762\u767d\u3044\u304b\u3089\u3084\u3063\u3066\u307f\u3066\uff01"+ "\n");
		sb.Append("→http://tt5.us/quizbox #クイズボックス");
		string imagePath = Application.persistentDataPath + "/" + "screenshot.png";
		#if UNITY_IPHONE
		TwitterManager.tweetSheetCompletedEvent += tweetSheetCompletedEvent;
		TwitterBinding.showTweetComposer(sb.ToString(),imagePath);
		#endif

		#if UNITY_ANDROID
		SocialConnector.Share(sb.ToString(),"",imagePath);
		#endif
	}

	private void ShowCompleteDialog () {
		string title = "\u30c4\u30a4\u30fc\u30c8\u6210\u529f!!";
		string message = "1\u30dd\u30a4\u30f3\u30c8\u8ffd\u52a0\u3057\u307e\u3057\u305f";
		#if UNITY_IPHONE
		string[] buttons = {"OK"};
		EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
		#endif

		#if UNITY_ANDROID
		EtceteraAndroid.showAlert (title,message,"OK");
		#endif
	}

}
