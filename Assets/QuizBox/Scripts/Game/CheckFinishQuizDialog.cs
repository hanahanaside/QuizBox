using UnityEngine;
using System.Collections;

public class CheckFinishQuizDialog : MonoBehaviour {

	public static void Show(){
		string title = "\u30af\u30a4\u30ba\u3092\u7d42\u4e86\u3057\u307e\u3059\u304b\uff1f";
		string message = "\u7d42\u4e86\u3059\u308b\u3068\u518d\u958b\u3067\u304d\u307e\u305b\u3093";
		string positiveButton = "\u7d42\u4e86\u3059\u308b";
		string negativeButton = "\u30ad\u30e3\u30f3\u30bb\u30eb";
#if UNITY_IPHONE
		string[] buttons = {positiveButton,negativeButton};
		EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
#endif

#if UNITY_ANDROID

#endif
	}
}
