﻿using UnityEngine;
using System.Collections;

public class TitleDialogManager : MonoBehaviour
{

	void OnEnable ()
	{
#if UNITY_IPHONE
		EtceteraManager.alertButtonClickedEvent += alertButtonClicked;
#endif
	}

	void OnDisable ()
	{
		#if UNITY_IPHONE
		EtceteraManager.alertButtonClickedEvent -= alertButtonClicked;
		#endif
	}

	public void ShowErrorDialog ()
	{

		#if UNITY_IPHONE
		string title = "\u901a\u4fe1\u306b\u5931\u6557\u3057\u307e\u3057\u305f";
		string message = "\u30af\u30a4\u30ba\u3092\u53d6\u5f97\u3067\u304d\u307e\u305b\u3093\u3067\u3057\u305f";
		string[] buttons = {"OK"};

		EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
		#endif
	}

	void alertButtonClicked (string text)
	{
		if (text == "OK") {
			Application.LoadLevel ("Top");
		}
	}
}
