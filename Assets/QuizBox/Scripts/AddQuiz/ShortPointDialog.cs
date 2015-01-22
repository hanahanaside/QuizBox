using UnityEngine;
using System.Collections;

public class ShortPointDialog : MonoBehaviour {

	void OnEnable(){
#if UNITY_IPHONE
		EtceteraManager.alertButtonClickedEvent += alertButtonClickedEvent;
#endif
	}

	void OnDisable(){
#if UNITY_IPHONE
		EtceteraManager.alertButtonClickedEvent -= alertButtonClickedEvent;
#endif
	}

	public void Show(){
		string title = "\u30dd\u30a4\u30f3\u30c8\u304c\u8db3\u308a\u307e\u305b\u3093";
		string message = "\u30dd\u30a4\u30f3\u30c8\u3092\u8ffd\u52a0\u3057\u3066\u304f\u3060\u3055\u3044";

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

	public void alertButtonClickedEvent(string clickedButton){
		TopController.Instance.OnAddPointClicked();
		Destroy(gameObject);
	}
}
