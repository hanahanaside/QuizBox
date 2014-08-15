using UnityEngine;
using System.Collections;

public class RestartChallengeDialog : MonoBehaviour {

	public static void Show(){
		string title = "\u4e2d\u65ad\u3057\u305f\u30af\u30a4\u30ba\u304c\u3042\u308a\u307e\u3059";
		string message = "\u518d\u958b\u3057\u307e\u3059\u304b\uff1f";
		string positiveButton = "\u518d\u958b\u3059\u308b";
		string negativeButton = "\u6700\u521d\u304b\u3089\u3084\u308a\u76f4\u3059";
		
		#if UNITY_IPHONE
		string[] buttons = {positiveButton,negativeButton};
		EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
		#endif

#if UNITY_ANDROID
		EtceteraAndroid.showAlert(title,message,positiveButton,negativeButton);
#endif
	}

}
