using UnityEngine;
using System.Collections;
using System;

public class IncentiveButtonController : MonoBehaviour {

	public UILabel timeLabel;
	public GameObject clickButtonObject;
	private const float INTERVAL_TIME = 61.0f;
	private float mIncentiveIntervalTime;

	void Start () {
		DateTime dtNow = DateTime.Now;
		string installedDate = PrefsManager.Instance.InstalledDate;
		DateTime dtInstalled = DateTime.Parse (installedDate);
		TimeSpan timeSpan = dtNow - dtInstalled;

		if (timeSpan.TotalMinutes >1) {
			//show
			transform.parent.gameObject.SetActive (true);
		} else {
			transform.parent.gameObject.SetActive (false);
			return;
		}



		string pauseIncentiveIntervalDate = PrefsManager.Instance.PauseIncentiveIntervalDate;
		if(string.IsNullOrEmpty(pauseIncentiveIntervalDate)){
			SaveTime ();
			mIncentiveIntervalTime = INTERVAL_TIME;
			UpdateTimeLabel ();
			return;
		}
		CalcTime ();
	}

	void OnApplicationPause(bool pauseStatus){
		Debug.Log ("pause");
		if(!pauseStatus){
			CalcTime ();
		}
	}

	void Update () {
		if (clickButtonObject.activeSelf) {
			return;
		}
		mIncentiveIntervalTime -= Time.deltaTime;
		UpdateTimeLabel ();
		if (mIncentiveIntervalTime < 0) {
			timeLabel.gameObject.SetActive (false);
			clickButtonObject.SetActive (true);
		}
	}

	public void OnClickButtonClicked () {
		ImobileManager.instance.ShowInterstitialAd ();
		SaveTime ();
		CalcTime ();
		clickButtonObject.SetActive (false);
		timeLabel.gameObject.SetActive (true);
		PrefsManager.Instance.AddUserPoint (1);
		TopController.instance.UpdateUserPointLabel ();
		string title = "ボーナス!!";
		string message = "1ptゲット!!";
		#if UNITY_IPHONE
		string[] buttons = {"OK"};
		EtceteraBinding.showAlertWithTitleMessageAndButtons (title,message,buttons);
		#endif

		#if UNITY_ANDROID
		EtceteraAndroid.showAlert(title,message,"OK");
		#endif
	}

	private void UpdateTimeLabel(){
		timeLabel.text = "あと" + (int)mIncentiveIntervalTime + "秒";
	}

	private void SaveTime(){
		Debug.Log ("save");
		PrefsManager.Instance.PauseIncentiveIntervalDate = DateTime.Now.ToString ();
	}

	private void CalcTime(){
		string pauseIncentiveIntervalDate = PrefsManager.Instance.PauseIncentiveIntervalDate;
		DateTime dtNow = DateTime.Now;
		DateTime dtPause = DateTime.Parse (pauseIncentiveIntervalDate);
		Debug.Log ("now = " + dtNow);
		Debug.Log ("pause = " +dtPause);
		TimeSpan ts = dtNow - dtPause;
		Debug.Log ("ts = " + ts.TotalSeconds);
		mIncentiveIntervalTime = INTERVAL_TIME - (float)ts.TotalSeconds;
		UpdateTimeLabel ();
	}
}
