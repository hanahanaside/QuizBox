using UnityEngine;
using System.Collections;

public class StartChallengeDialog : MonoBehaviour {

	public static void Show(){
		string title = "\u5168"+QuizListManager.instance.allQuizListCount+"\u554f\u3067\u6b63\u89e3\u6570\u3092\u7af6\u304a\u3046!!";
		string message = "\u6311\u6226\u3057\u307e\u3059\u304b\uff1f";
		string positiveButton = "\u6311\u6226\u3059\u308b";
		string negativeButton = "\u30ad\u30e3\u30f3\u30bb\u30eb";

#if UNITY_IPHONE
		string[] buttons = {positiveButton,negativeButton};
		EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
#endif

#if UNITY_ANDROID
		EtceteraAndroid.showAlert(title,message,positiveButton,negativeButton);
#endif
	}
}
