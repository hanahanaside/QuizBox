using UnityEngine;
using System.Collections;

public class TitleDialogManager : MonoBehaviour {

	void OnEnable () {
#if UNITY_IPHONE
		EtceteraManager.alertButtonClickedEvent += alertButtonClicked;
#endif
		#if UNITY_ANDROID
		EtceteraAndroidManager.alertButtonClickedEvent += alertButtonClicked;
		#endif
	}

	void OnDisable () {
		#if UNITY_IPHONE
		EtceteraManager.alertButtonClickedEvent -= alertButtonClicked;
		#endif

		#if UNITY_ANDROID
		EtceteraAndroidManager.alertButtonClickedEvent -= alertButtonClicked;
		#endif

	}

	public void ShowErrorDialog () {
		string title = "\u901a\u4fe1\u306b\u5931\u6557\u3057\u307e\u3057\u305f";
		string message = "\u30af\u30a4\u30ba\u3092\u53d6\u5f97\u3067\u304d\u307e\u305b\u3093\u3067\u3057\u305f";

		#if UNITY_IPHONE
		string[] buttons = {"OK"};
		EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
		#endif

#if UNITY_ANDROID
		EtceteraAndroid.showAlert(title,message,"OK");
#endif
	}

	void alertButtonClicked (string text) {
		if (text == "OK") {
			Application.LoadLevel ("Top");
		}
	}

	#if UNITY_ANDROID
	void alertCancelled(){
		Application.LoadLevel ("Top");
	}
	#endif
}