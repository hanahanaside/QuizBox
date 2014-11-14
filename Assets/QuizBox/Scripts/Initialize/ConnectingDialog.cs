using UnityEngine;
using System.Collections;

public class ConnectingDialog {

	public static void Show(){
		string title = "通信中";
		string message = "お待ちください";
		#if UNITY_ANDROID
		EtceteraAndroid.showProgressDialog(title,message);
		#endif
	}

	public static void Hide(){
		#if UNITY_ANDROID
		EtceteraAndroid.hideProgressDialog();
		#endif
	}
}
