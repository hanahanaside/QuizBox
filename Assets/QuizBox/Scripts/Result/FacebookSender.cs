using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;

public class FacebookSender : MonoBehaviour {

	void OnEnable () {
		FacebookManager.facebookComposerCompletedEvent += composerCompletedEvent;
	}

	void OnDisable () {
		FacebookManager.facebookComposerCompletedEvent -= composerCompletedEvent;
	}

	void Start () {
#if UNITY_IPHONE
		FacebookBinding.init ();
#endif

#if UNITY_ANDROID
		FacebookAndroid.init();
#endif
	}

	public void ShowFacebookComposer () {
		Debug.Log("show composer");
		int score = ScoreKeeper.instance.score;
		int size = QuizListManager.instance.quizList.Count;
		string result = size + "問中" + score + "問正解!!";
		string imagePath = Application.streamingAssetsPath + "/share_image.png";

#if UNITY_IPHONE
			StringBuilder sb = new StringBuilder ();
			sb.Append (SelectedQuiz.instance.name + "|" + QuizListManager.instance.modeName + "\u3067");
			sb.Append ("、" + result + "\n");
			sb.Append ("\u3053\u306e\u30af\u30a4\u30ba\u30a2\u30d7\u30ea\u9762\u767d\u3044\u304b\u3089\u3084\u3063\u3066\u307f\u3066\uff01" + "\n");
			sb.Append("http://tt5.us/quizbox");
			FacebookBinding.showFacebookComposer(sb.ToString());
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

	public void Login () {
		#if UNITY_IPHONE
		FacebookBinding.login();
#endif

#if UNITY_ANDROID
		FacebookAndroid.login();
#endif
	}

	void composerCompletedEvent (bool result) {
		Debug.Log ("composer result = " + result);
		if (result) {
			string title = "\u6210\u529f!!";
			string message = "facebook\u306b\u6295\u7a3f\u3057\u307e\u3057\u305f";
#if UNITY_IPHONE
			string[] buttons = {"OK"};
			EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
#endif
		} else {
			string title = "\u5931\u6557";
			string message = "facebook\u306b\u6295\u7a3f\u3067\u304d\u307e\u305b\u3093\u3067\u3057\u305f";
#if UNITY_IPHONE
			string[] buttons = {"OK"};
			EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
#endif
		}
	}

}
