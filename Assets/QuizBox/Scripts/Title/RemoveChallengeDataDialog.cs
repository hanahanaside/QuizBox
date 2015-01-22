using UnityEngine;
using System.Collections;

public class RemoveChallengeDataDialog {

	public static string POSITIVE_BUTTON = "キャンセル";
	public static string NEGATIVE_BUTTON  = "削除して始める";

	public RemoveChallengeDataDialog(){

	}

	public void Show(){
		string title = "セーブデータを削除して最初から始めてもよろしいですか？";
		string message = "削除すると元には戻せません";
		#if UNITY_IPHONE
		string[] buttons = {POSITIVE_BUTTON,NEGATIVE_BUTTON};
		EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
		#endif

		#if UNITY_ANDROID
		EtceteraAndroid.showAlert (title,message,POSITIVE_BUTTON,NEGATIVE_BUTTON);
		#endif
	}
}
