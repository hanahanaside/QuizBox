using UnityEngine;
using MiniJSON;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class PostQuizDialogController : MonoBehaviour {

	private const string DEFAULT_TEXT = "\u4f8b ) \u3053\u3068\u308f\u3056\u30af\u30a4\u30ba";
	private const string POST_DATA_URL = "http://quizbox.tt5.us/api/post_user_quiz";
	public UILabel themeLabel;
	public UILabel seriesLabel;
	public UILabel questionLabel;
	public UILabel answerLabel;
	public UILabel mistake1Label;
	public UILabel mistake2Label;
	public UIToggle checkMarkToggle;

	public void OnPostButtonClicked () {
//		if (themeLabel.text == DEFAULT_TEXT) {
//			ShowErrorDialog ("\u30c6\u30fc\u30de\u304c\u5165\u529b\u3055\u308c\u3066\u3044\u307e\u305b\u3093");
//		} else if (seriesLabel.text == DEFAULT_TEXT) {
//			ShowErrorDialog ("\u30b7\u30ea\u30fc\u30ba\u304c\u5165\u529b\u3055\u308c\u3066\u3044\u307e\u305b\u3093");
//		} else if (questionLabel.text == DEFAULT_TEXT) {
//			ShowErrorDialog ("\u554f\u984c\u304c\u5165\u529b\u3055\u308c\u3066\u3044\u307e\u305b\u3093");
//		} else if (answerLabel.text == DEFAULT_TEXT) {
//			ShowErrorDialog ("\u6b63\u89e3\u304c\u5165\u529b\u3055\u308c\u3066\u3044\u307e\u305b\u3093");
//		} else if (!checkMarkToggle.value) {
//			ShowErrorDialog ("\u5229\u7528\u898f\u7d04\u306b\u540c\u610f\u3057\u3066\u304f\u3060\u3055\u3044");
//		} else {
//			Dictionary<string,object> dictionary = new Dictionary<string,object> ();
//			dictionary.Add ("title", "title");
//			dictionary.Add ("series", "series");
//			dictionary.Add ("question", "question");
//			dictionary.Add ("answer", "answer");
//			dictionary.Add ("mistake1", "mistake1");
//			dictionary.Add ("mistake2", "mistake2");
//			dictionary.Add ("userid", "userid");
//	//		dictionary.Add ("api_version", 1);
//			string json = Json.Serialize (dictionary);
//			WWW www = new WWW (POST_DATA_URL, Encoding.UTF8.GetBytes (json));
//			StartCoroutine (PostData (www));
//		}

		Dictionary<string,object> dictionary = new Dictionary<string,object> ();
		dictionary.Add ("title", "title");
		dictionary.Add ("series", "series");
		dictionary.Add ("question", "question");
		dictionary.Add ("answer", "answer");
		dictionary.Add ("mistake1", "mistake1");
		dictionary.Add ("mistake2", "mistake2");
		dictionary.Add ("userid", "userid");
		dictionary.Add ("api_version", 1);
		string json = Json.Serialize (dictionary);
		WWW www = new WWW (POST_DATA_URL, Encoding.UTF8.GetBytes (json));
		StartCoroutine (PostData (www));
	}

	private IEnumerator PostData (WWW www) {
		Debug.Log ("PostData");
#if UNITY_IPHONE
		FenceInstanceKeeper.Instance.SetActive(true);
		EtceteraBinding.showActivityView();
#endif
		yield return www;

		#if UNITY_IPHONE
		EtceteraBinding.hideActivityView();
		FenceInstanceKeeper.Instance.SetActive(false);
#endif
		// check for errors
		if (www.error == null) {
			Debug.Log ("WWW Ok!: " + www.text);
			CheckResponse (www.text);
		} else {
			Debug.Log ("WWW Error: " + www.error);
		}
	}

	private void CheckResponse (string response) {
		Dictionary<string,object> dictionary = (Dictionary<string,object>)Json.Deserialize (response);
		bool result = (bool)dictionary ["result"];
		Debug.Log ("result = " + result);
		if (result) {
			PrefsManager.Instance.AddUserPoint(1);
			TopController.Instance.UPdateUserPointLabel();
			string title = "\u6295\u7a3f\u3057\u307e\u3057\u305f";
			string message = "1\u30dd\u30a4\u30f3\u30c8GET!!";
			#if UNITY_IPHONE
			string[] buttons = {"OK"};
			EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
			#endif
		} else {
			string title = "\u6295\u7a3f\u306b\u5931\u6557\u3057\u307e\u3057\u305f";
			string message = "\u518d\u5ea6\u6295\u7a3f\u3057\u3066\u304f\u3060\u3055\u3044";
			#if UNITY_IPHONE
			string[] buttons = {"OK"};
			EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
			#endif
		}
	}

	private void ShowErrorDialog (string message) {
		string title = "\u30a8\u30e9\u30fc";
#if UNITY_IPHONE
		string[] buttons = {"OK"};
		EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
#endif
	}
}
