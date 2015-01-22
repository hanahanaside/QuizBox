using UnityEngine;
using System.Collections;
using System.Text;

public class NetworkErrorDialog {

	public NetworkErrorDialog(){
	
	}

	public void Show(){
		string title = "通信エラー";
		StringBuilder sb = new StringBuilder ();
		sb.Append ("通信に失敗しました。\n");
		sb.Append ("下記をお試しください。\n");
		sb.Append ("①電波の良いところでリトライ\n");
		sb.Append ("②時間をおいてリトライ\n");
		sb.Append ("③再起動してリトライ");
		string message = sb.ToString ();

		#if UNITY_IPHONE
		string[] buttons = { "OK" };
		EtceteraBinding.showAlertWithTitleMessageAndButtons (title, message, buttons);
		#endif

		#if UNITY_ANDROID
		EtceteraAndroid.showAlert(title,message,"OK");
		#endif

	}

}
