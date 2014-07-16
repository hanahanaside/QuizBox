using UnityEngine;
using System.Collections;

public class ShortPointDialog : MonoBehaviour {

	public void Show(){
		string title = "\u30dd\u30a4\u30f3\u30c8\u304c\u8db3\u308a\u307e\u305b\u3093";
		string message = "\u8cfc\u5165\u3057\u307e\u3059\u304b\uff1f";
		string positiveButton = "\u8cfc\u5165\u3059\u308b";
		string negativeButton = "\u30ad\u30e3\u30f3\u30bb\u30eb";

#if UNITY_ANDROID
		EtceteraAndroid.showAlert(title,message,positiveButton,negativeButton);
#endif
	}
}
