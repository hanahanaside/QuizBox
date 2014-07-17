using UnityEngine;
using System.Collections;

public class CheckSaveQuizDialog : MonoBehaviour {

	public static void Show(){
		string title = "\u7d42\u4e86\u3057\u307e\u3059\u304b\uff1f";
		string message = "\u9014\u4e2d\u7d4c\u904e\u306f\u30bb\u30fc\u30d6\u3055\u308c\u307e\u3059";
		string positiveButton = "\u30bb\u30fc\u30d6\u3057\u3066\u7d42\u4e86";
		string negativeButton = "\u30ad\u30e3\u30f3\u30bb\u30eb";
#if UNITY_IPHONE
		string[] buttons = {positiveButton,negativeButton};
		EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
#endif
	}
}
