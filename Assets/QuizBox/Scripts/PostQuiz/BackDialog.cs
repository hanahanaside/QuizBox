using UnityEngine;
using System.Collections;

public class BackDialog : MonoBehaviour {

	private const string TITLE = "\u3082\u3069\u308a\u307e\u3059\u304b\uff1f";
	private const string MESSAGE  = "\u6295\u7a3f\u306f\u7834\u68c4\u3055\u308c\u307e\u3059";
	private const string POSITIVE_BUTTON = "\u3082\u3069\u308b";
	private const string NEGATIVE_BUTTON = "\u30ad\u30e3\u30f3\u30bb\u30eb";

	void OnEnable () {
#if UNITY_IPHONE
		EtceteraManager.alertButtonClickedEvent += alertButtonClicked;
#endif
	}

	void OnDisable () {
		#if UNITY_IPHONE
		EtceteraManager.alertButtonClickedEvent -= alertButtonClicked;
		#endif
	}

	public void Show(){
		#if UNITY_IPHONE
		string[] buttons = {POSITIVE_BUTTON,NEGATIVE_BUTTON};
		EtceteraBinding.showAlertWithTitleMessageAndButtons(TITLE,MESSAGE,buttons);
		#endif
	}

	void alertButtonClicked (string clickedButton) {
		if (clickedButton == POSITIVE_BUTTON) {
			Application.LoadLevel ("Top");
		}
	}
}
