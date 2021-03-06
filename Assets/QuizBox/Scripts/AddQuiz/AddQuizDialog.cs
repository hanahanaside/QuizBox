﻿using UnityEngine;
using System.Collections;

public static class AddQuizDialog {


	public static void Show (SelledProject addQuiz, int userPoint) {
		string quizTitle = addQuiz.title;
		string title = "\u6240\u6301pt : " + userPoint + "pt";
		string message = quizTitle + "を" + addQuiz.point + "pt\u3067\u8ffd\u52a0\u3057\u307e\u3059\u304b\uff1f";
		string positiveButton = "\u306f\u3044";
		string negativeButton = "\u3044\u3044\u3048";
#if UNITY_IPHONE
		string[] buttons = { positiveButton, negativeButton };
		EtceteraBinding.showAlertWithTitleMessageAndButtons (title, message, buttons);
#endif

		#if UNITY_ANDROID
		EtceteraAndroid.showAlert(title,message,positiveButton,negativeButton);
		#endif
	}
}
