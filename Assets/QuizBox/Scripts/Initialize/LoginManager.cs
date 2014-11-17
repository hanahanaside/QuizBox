using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using MiniJSON;

public class LoginManager : MonoBehaviour {
	private static readonly DateTime UNIX_EPOCH = new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
	private static readonly string URL = "https://ntp-a1.nict.go.jp/cgi-bin/json";
	public UILabel userPointLabel;

	void Awake () {
		Debug.Log ("check login");
		WWWClient wwwClient = new WWWClient (this, URL);
		wwwClient.OnSuccess = (string response) => {
			Dictionary<string,object> dic = (Dictionary<string,object>)Json.Deserialize (response);
			double unixTime = (double)dic ["st"];
			DateTime dtNow = UNIX_EPOCH.AddSeconds ((long)unixTime).ToLocalTime ();
			string loginDate = PrefsManager.Instance.LoginDate;
			if (string.IsNullOrEmpty (loginDate)) {
				ApplyBonus(dtNow);
				return;
			}
			DateTime dtLogin = DateTime.Parse (loginDate);
			TimeSpan ts = dtNow - dtLogin;
			if (ts.Days >= 1) {
				ApplyBonus(dtNow);
			}
		};
		wwwClient.GetData ();
	}

	private void ApplyBonus(DateTime dtNow){
		PrefsManager.Instance.AddUserPoint (5);
		userPointLabel.text = PrefsManager.Instance.GetUserPoint() + "pt";
		PrefsManager.Instance.LoginDate = dtNow.ToString ();
		#if !UNITY_EDITOR
		ShowLoginBonusDialog ();
		#endif
	}

	private void ShowLoginBonusDialog () {
		string title = "ログインボーナス";
		string message = "5ptゲット!!";
		#if UNITY_IPHONE
		string[] buttons = {"OK"};
		EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
		#endif
		#if UNITY_ANDROID
		EtceteraAndroid.showAlert (title,message,"OK");
		#endif
	}
}
