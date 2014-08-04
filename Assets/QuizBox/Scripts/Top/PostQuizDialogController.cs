using UnityEngine;
using System.Collections;

public class PostQuizDialogController : MonoBehaviour {

	private const string DEFAULT_TEXT = "\u4f8b ) \u3053\u3068\u308f\u3056\u30af\u30a4\u30ba";
	public UILabel themeLabel;
	public UILabel seriesLabel;
	public UILabel questionLabel;
	public UILabel answerLabel;
	public UIToggle checkMarkToggle;

	public void OnPostButtonClicked () {
		if (themeLabel.text == DEFAULT_TEXT) {
			ShowErrorDialog ("\u30c6\u30fc\u30de\u304c\u5165\u529b\u3055\u308c\u3066\u3044\u307e\u305b\u3093");
		} else if (seriesLabel.text == DEFAULT_TEXT) {
			ShowErrorDialog ("\u30b7\u30ea\u30fc\u30ba\u304c\u5165\u529b\u3055\u308c\u3066\u3044\u307e\u305b\u3093");
		} else if (questionLabel.text == DEFAULT_TEXT) {
			ShowErrorDialog ("\u554f\u984c\u304c\u5165\u529b\u3055\u308c\u3066\u3044\u307e\u305b\u3093");
		} else if (answerLabel.text == DEFAULT_TEXT) {
			ShowErrorDialog ("\u6b63\u89e3\u304c\u5165\u529b\u3055\u308c\u3066\u3044\u307e\u305b\u3093");
		} else if (!checkMarkToggle.value) {
			ShowErrorDialog ("\u5229\u7528\u898f\u7d04\u306b\u540c\u610f\u3057\u3066\u304f\u3060\u3055\u3044");
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
