using UnityEngine;
using System.Collections;

public static class ShortPointDialog {

	public static void Show(){
		string title = "ポイントが足りません";
		string message = "ポイントを追加してください";

#if UNITY_IPHONE
		string[] buttons = {"OK"};
		EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
#endif

#if UNITY_ANDROID
		string positiveButton = "\u8cfc\u5165\u3059\u308b";
		string negativeButton = "\u30ad\u30e3\u30f3\u30bb\u30eb";
		EtceteraAndroid.showAlert(title,message,positiveButton,negativeButton);
#endif
	}
}
