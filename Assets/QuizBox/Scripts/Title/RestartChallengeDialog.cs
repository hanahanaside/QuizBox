using UnityEngine;
using System.Collections;

public class RestartChallengeDialog : MonoBehaviour {

	public static void Show(){
		string title = "中断したクイズがあります";
		string message = "再開しますか？";
		string positiveButton = "再開する";
		string negativeButton = "最初からやり直す";
		
		#if UNITY_IPHONE
		string[] buttons = {positiveButton,negativeButton};
		EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
		#endif

#if UNITY_ANDROID
		EtceteraAndroid.showAlert(title,message,positiveButton,negativeButton);
#endif
	}

}
