using UnityEngine;
using MiniJSON;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System;

public class PostQuizDialogController : MonoBehaviour {

	private const string DEFAULT_TEXT = "\u4f8b )";
	private const string POST_DATA_URL = "http://quizbox.tt5.us/api/post_user_quiz";
	public UIToggle checkMarkToggle;
	public UIInput themeInput;
	public UIInput seriesInput;
	public UIInput questionInput;
	public UIInput answerInput;
	public UIInput mistake1Input;
	public UIInput mistake2Input;
	public GameObject usePolicyDialogPrefab;
	public GameObject uiRoot;

	void Start () {
		TouchScreenKeyboard.hideInput = true;
	}
	 
	public void OnPostButtonClicked () {
		if (!CheckPostCountOK ()) { 
			string[] buttons = {"OK"};
			EtceteraBinding.showAlertWithTitleMessageAndButtons ("\u30a8\u30e9\u30fc", "\u6295\u7a3f\u306f1\u65e510\u56de\u307e\u3067\u3067\u3059", buttons);
			return;
		}
		if (themeInput.label.text.Contains (DEFAULT_TEXT) || themeInput.label.text == "") {
			ShowErrorDialog ("\u30c6\u30fc\u30de\u304c\u5165\u529b\u3055\u308c\u3066\u3044\u307e\u305b\u3093");
		} else if (seriesInput.label.text.Contains (DEFAULT_TEXT) || seriesInput.label.text == "") {
			ShowErrorDialog ("\u30b7\u30ea\u30fc\u30ba\u304c\u5165\u529b\u3055\u308c\u3066\u3044\u307e\u305b\u3093");
		} else if (questionInput.label.text.Contains (DEFAULT_TEXT) || questionInput.label.text == "") {
			ShowErrorDialog ("\u554f\u984c\u304c\u5165\u529b\u3055\u308c\u3066\u3044\u307e\u305b\u3093");
		} else if (answerInput.label.text.Contains (DEFAULT_TEXT) || answerInput.label.text == "") {
			ShowErrorDialog ("\u6b63\u89e3\u304c\u5165\u529b\u3055\u308c\u3066\u3044\u307e\u305b\u3093");
		} else if (mistake1Input.label.text.Contains (DEFAULT_TEXT) || mistake1Input.label.text == "") {
			ShowErrorDialog ("\u4e0d\u6b63\u89e31\u304c\u5165\u529b\u3055\u308c\u3066\u3044\u307e\u305b\u3093");
		} else if (mistake2Input.label.text.Contains (DEFAULT_TEXT) || mistake2Input.label.text == "") {
			ShowErrorDialog ("\u4e0d\u6b63\u89e32\u304c\u5165\u529b\u3055\u308c\u3066\u3044\u307e\u305b\u3093");
		} else if (!checkMarkToggle.value) {
			ShowErrorDialog ("\u5229\u7528\u898f\u7d04\u306b\u540c\u610f\u3057\u3066\u304f\u3060\u3055\u3044");
		} else {
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
	}

	private bool CheckPostCountOK () {
		PostCountData postCountData = PrefsManager.Instance.GetPostCountData ();
		string now = DateTime.Now.ToShortDateString ();
		if (now == postCountData.PostDate && postCountData.PostCount > 10) {
			return false;
		}
		return true;
	}

	public void OnCloseButtonClicked () {
		Application.LoadLevel ("Top");
	}

	public void OnUsePolicyClicked () {
		GameObject usePolicyDialog = Instantiate (usePolicyDialogPrefab) as GameObject;
		usePolicyDialog.transform.parent = uiRoot.transform;
		usePolicyDialog.transform.localScale = new Vector3 (1, 1, 1);
	}

	private IEnumerator PostData (WWW www) {
		Debug.Log ("PostData");
#if UNITY_IPHONE
		EtceteraBinding.showActivityView();
#endif
		yield return www;

#if UNITY_IPHONE
		EtceteraBinding.hideActivityView();
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
			PrefsManager.Instance.AddUserPoint (1);
			UpdatePostCountData ();
			TopController.Instance.UPdateUserPointLabel ();
			ShowSuccessDialog ();
			ResetInputLabel ();
		} else {
			string title = "\u6295\u7a3f\u306b\u5931\u6557\u3057\u307e\u3057\u305f";
			string message = "\u518d\u5ea6\u6295\u7a3f\u3057\u3066\u304f\u3060\u3055\u3044";
			#if UNITY_IPHONE
			string[] buttons = {"OK"};
			EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
			#endif
		}
	}

	private void ResetInputLabel () {
		questionInput.value = "";
		answerInput.value = "";
		mistake1Input.value = "";
		mistake2Input.value = "";
	}

	private void ShowErrorDialog (string message) {
		string title = "\u30a8\u30e9\u30fc";
#if UNITY_IPHONE
		string[] buttons = {"OK"};
		EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
#endif
	}

	private void ShowSuccessDialog () {
		PostCountData postCountData = PrefsManager.Instance.GetPostCountData ();
		string title = "\u6295\u7a3f\u3057\u307e\u3057\u305f";
		string message = "1\u30dd\u30a4\u30f3\u30c8GET!!" + " (\u672c\u65e5" + postCountData.PostCount + "pt\u76ee)";
		#if UNITY_IPHONE
		string[] buttons = {"OK"};
		EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
		#endif
	}

	private void UpdatePostCountData () {
		PostCountData postCountData = PrefsManager.Instance.GetPostCountData ();
		string now = DateTime.Now.ToShortDateString ();
		if (now == postCountData.PostDate) {
			postCountData.PostCount++;
		} else {
			postCountData.PostCount = 1;
			postCountData.PostDate = now;
		}
		Debug.Log ("postCount = " + postCountData.PostCount);
		Debug.Log ("postDate = " + postCountData.PostDate);
		PrefsManager.Instance.SavePostCountData (postCountData);
		UIInput i;

	}
}
