using UnityEngine;
using System.Collections;

public class ReviewDialog : AlertDialog {

	public string appStoreUrl;
	public string googlePlayUrl;
	private const string TITLE = "ダウンロードありがとうございます!!";
	private const string MESSAGE = "このアプリにレビューをして頂けると幸いです。（良い評価を頂ければ、今後も頑張ってアプリ出していけます!!）";
	private const string POSITIVE_BUTTON = "協力する";
	private const string NEGATIVE_BUTTON = "表示しない";

	public override void alertButtonClicked (string text) {
		if (text == POSITIVE_BUTTON) {
			if (!PrefsManager.Instance.GetReviewed ()) {
				PrefsManager.Instance.SetReviewed ();
			}
			#if UNITY_IPHONE
			Application.OpenURL(appStoreUrl);
			#endif

			#if UNITY_ANDROID
			Application.OpenURL(googlePlayUrl);
			#endif
		}

		if (text == NEGATIVE_BUTTON) {
			//do not show
			PrefsManager.Instance.SetReviewed ();
		}
		Destroy (gameObject);
	}

	public override void Show () {
		#if UNITY_IPHONE
		string[] buttons = new string[] {POSITIVE_BUTTON,"後で",NEGATIVE_BUTTON};
		EtceteraBinding.showAlertWithTitleMessageAndButtons(TITLE, MESSAGE, buttons);
		#endif
		
		#if UNITY_ANDROID
		EtceteraAndroid.showAlert(TITLE,MESSAGE,POSITIVE_BUTTON,"後で");
		#endif
	}

}
