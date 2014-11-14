using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ResultInitializer : MonoBehaviour {

	public UIAtlas resultAtlas;
	public GameObject[] resultLabelArray;
	public GameObject medal;
	public GameObject medalEffectPrefab;
	public GameObject uiRoot;
	private double mAverage;

	// Use this for initialization
	void Start () {
		int score = ScoreKeeper.instance.score;
		int size = QuizListManager.instance.quizList.Count;
		string result = size + "問中" + score + "問正解!!";

		HistoryData historyData = new HistoryData ();
		historyData.Average = ((double)score / (double)size) * 100;
		historyData.result = result;
		historyData.date = DateTime.Now.ToString ("yyyy/MM/dd (ddd) HH:mm:ss");
		historyData.title = SelectedQuiz.instance.name;
		historyData.mode = QuizListManager.instance.modeName;
	
		HistoryDataDao.instance.InsertHistoryData (historyData);

		mAverage = ((double)score / (double)size) * 100;
		resultLabelArray [0].GetComponent<UILabel> ().text = SelectedQuiz.instance.name;
		resultLabelArray [1].GetComponent<UILabel> ().text = QuizListManager.instance.modeName;
		resultLabelArray [2].GetComponent<UILabel> ().text = result;
		resultLabelArray [3].GetComponent<UILabel> ().text = "\u6b63\u89e3\u7387 : " + Math.Round (mAverage) + "%";

		foreach (GameObject item in resultLabelArray) {
			iTweenEvent.GetEvent (item, "EntranceEvent").Play ();
		}

		DateTime dtNow = DateTime.Now;
		string installedDate = PrefsManager.Instance.InstalledDate;
		DateTime dtInstalled = DateTime.Parse (installedDate);
		TimeSpan timeSpan = dtNow - dtInstalled;
		int unlockDaySpan = 0;
		#if UNITY_IPHONE
		unlockDaySpan = 1;
		#endif
		if (timeSpan.TotalDays >= unlockDaySpan) {
			StartCoroutine (OpenWallAd ());
		} 

	}

	void OnLabelEventCompleted () {
		string spriteName = "";
		if (mAverage >= 100) {
			spriteName = "01.gold";
		} else if (mAverage >= 90) {
			spriteName = "02.silver";
		} else if (mAverage >= 80) {
			spriteName = "03.bronze";
		} else if (mAverage < 33) {
			spriteName = "04.0";
		}
		Debug.Log ("spriteName = " + spriteName);
		if (spriteName == "") {
			Invoke ("CaptureScreenshot", 1.0f);
		}else {
			UISpriteData spriteData = resultAtlas.GetSprite (spriteName);
			UISprite sprite = medal.GetComponent<UISprite> ();
			sprite.spriteName = spriteName;
			sprite.width = spriteData.width;
			sprite.height = spriteData.height;
			medal.SetActive (true);
		}
	}

	void OnMedalEventCompleted () {
		GameObject medalEffect = Instantiate (medalEffectPrefab) as GameObject;
		medalEffect.transform.parent = uiRoot.transform;
		medalEffect.transform.localScale = new Vector3 (1, 1, 1);
		medalEffect.transform.localPosition = medal.transform.localPosition;
		Invoke ("CaptureScreenshot", 1.0f);
	}

	private void CaptureScreenshot () {
		Application.CaptureScreenshot (TweetSender.SHARE_FILE_NAME);
	}

	private IEnumerator OpenWallAd () {
		yield return new WaitForSeconds (3.0f);
		var parameters = new APUnityPluginSetting();
		parameters.setParam(APUnityPluginSetting.iOSAppkey, "KAQ2AGURBDTNY54F");
		parameters.setParam(APUnityPluginSetting.onStatusArea, true);
		parameters.setParam(APUnityPluginSetting.orientation, "UIInterfaceOrientationLandscapeLeft");
		parameters.setParam(APUnityPluginSetting.isClose, true);
		parameters.setParam(APUnityPluginSetting.AndroidAppKey, "HORGV6VG3484JCEW");
		APUnityPlugin.ShowAppliPromotionWall(parameters);
	}
	
}
