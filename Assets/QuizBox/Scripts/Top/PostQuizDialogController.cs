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
	public BackDialog backDialog;
	public GameObject postSuccessDialog;
	public UILabel successLabel;
	public PostCountDataKeeper postCountDataKeeper;

	void Start () {
		TouchScreenKeyboard.hideInput = true;
	}
	 
	public void OnPostButtonClicked () {
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
			dictionary.Add ("title", themeInput.label.text);
			dictionary.Add ("series", seriesInput.label.text);
			dictionary.Add ("question", questionInput.label.text);
			dictionary.Add ("answer", answerInput.label.text);
			dictionary.Add ("mistake1", mistake1Input.label.text);
			dictionary.Add ("mistake2", mistake2Input.label.text);
			dictionary.Add ("userid", "userId");
			dictionary.Add ("api_version", 1);
			string json = Json.Serialize (dictionary);
			WWW www = new WWW (POST_DATA_URL, Encoding.UTF8.GetBytes (json));
			StartCoroutine (PostData (www));

		}
	}
		
	public void OnCloseButtonClicked () {
		#if UNITY_EDITOR
		Application.LoadLevel("Top");
		#else
		backDialog.Show();
		#endif
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
		#if UNITY_ANDROID
		string progressTitle = "\u901a\u4fe1\u4e2d";
		string progressMessage = "\u304a\u5f85\u3061\u304f\u3060\u3055\u3044";
		EtceteraAndroid.showProgressDialog(progressTitle,progressMessage);
		#endif
		yield return www;

#if UNITY_IPHONE
		EtceteraBinding.hideActivityView();
#endif

		#if UNITY_ANDROID
		EtceteraAndroid.hideProgressDialog();
		#endif
		// check for errors
		if (www.error == null) {
			Debug.Log ("WWW Ok!: " + www.text);
			CheckResponse (www.text);
		} else {
			Debug.Log ("WWW Error: " + www.error);
			string title = "通信エラー";
			string message = "投稿に失敗しました";
			#if UNITY_IPHONE
			string[] buttons = {"OK"};
			EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
			#endif

			#if UNITY_ANDROID
			EtceteraAndroid.showAlert (title,message,"OK");
			#endif
		}
	}

	private void CheckResponse (string response) {
		Dictionary<string,object> dictionary = (Dictionary<string,object>)Json.Deserialize (response);
		bool result = (bool)dictionary ["result"];
		Debug.Log ("result = " + result);
		if (result) {
			postCountDataKeeper.UpdatePostCountData ();
			ShowSuccessDialog ();
			ResetInputLabel ();
		} else {
			string title = "通信エラー";
			string message = "投稿に失敗しました";
			#if UNITY_IPHONE
			string[] buttons = {"OK"};
			EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
			#endif

			#if UNITY_ANDROID
			EtceteraAndroid.showAlert(title,message,"OK");
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

		#if UNITY_ANDROID
		EtceteraAndroid.showAlert(title,message,"OK");
		#endif
	}

	private void ShowSuccessDialog () {
		ImobileManager.Instance.ShowRectangleAd ();
		postSuccessDialog.SetActive(true);
		PostCountData postCountData = PrefsManager.Instance.GetPostCountData ();
		string title = "\u6295\u7a3f\u3057\u307e\u3057\u305f";
		string message = "";
		if(postCountData.PostCount <=10){
			message = "1\u30dd\u30a4\u30f3\u30c8GET!!" + " (\u672c\u65e5" + postCountData.PostCount + "pt\u76ee)";
		}else {
			message = "1\u65e5\u306b\u7372\u5f97\u3067\u304d\u308bpt\u306f10pt\u307e\u3067\u3067\u3059";
		}
		successLabel.text = title + "\n" + message;
		transform.parent.gameObject.SetActive(false);
	}
}
