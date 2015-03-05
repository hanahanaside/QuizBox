using UnityEngine;
using System.Collections;

public static class OkDialog {

	public static void Show(string title,string message){
#if UNITY_IPHONE
		string[] buttons = {"OK"};
		EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
#endif

#if UNITY_ANDROID
		EtceteraAndroid.showAlert(title,message,"OK");
#endif
	}
}
