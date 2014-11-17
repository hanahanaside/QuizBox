using UnityEngine;
using System.Collections;
using System;
using MiniJSON;
using System.Collections.Generic;
using System.IO;

public class DatabaseUpdater : MonoBehaviour {
	private const string JSON_URL = "http://quiz.ryodb.us/list/selled_projects.json";

	public static event Action updatedDatabaseEvent;

	void OnEnable () {
		#if UNITY_IPHONE
		EtceteraManager.alertButtonClickedEvent += alertButtonClickedEvent;
		#endif

		#if UNITY_ANDROID
		EtceteraAndroidManager.alertButtonClickedEvent += alertButtonClickedEvent;
		EtceteraAndroidManager.alertCancelledEvent += alertCancelledEvent;
		#endif
	}

	void OnDisable () {
		#if UNITY_IPHONE
		EtceteraManager.alertButtonClickedEvent -= alertButtonClickedEvent;
		#endif

		#if UNITY_ANDROID
		EtceteraAndroidManager.alertButtonClickedEvent -= alertButtonClickedEvent;
		EtceteraAndroidManager.alertCancelledEvent -= alertCancelledEvent;
		#endif
	}

	void alertButtonClickedEvent (string clickedButton) {
		Application.Quit ();
	}

	void alertCancelledEvent () {
		ShowErrorDialog ();
	}

	public void UpdateDatabase () {
		Debug.Log ("update database");
		int databaseVersion = PrefsManager.Instance.DatabaseVersion;
		switch (databaseVersion) {
		case 0:
			//金魂クイズをインサート
			IDictionary kinkonQuiz = new Dictionary<string,object> ();
			kinkonQuiz [QuizListDao.TITLE_FIELD] = "金魂クイズ";
			kinkonQuiz [QuizListDao.QUIZ_URL_FIELD] = "http://ryodb.us/projects/5035e95766e5411652000001/quizzes.json";
			kinkonQuiz [QuizListDao.QUIZ_ID_FIELD] = 73;
			kinkonQuiz [QuizListDao.BOUGHT_DATE_FIELD] = DateTime.Now.ToString ();
			QuizListDao.instance.Insert (kinkonQuiz);
			PrefsManager.Instance.DatabaseVersion = 2;
			break;
		case 1:
			//ヒストリーデータを再構築
			IList<HistoryData> historyDataList = HistoryDataDao.instance.QueryHistoryDataList ();
			IList<IDictionary> quizList = QuizListDao.instance.GetQuizList ();
			string databaseFileName = "quiz_box.db";
			string baseFilePath = Application.streamingAssetsPath + "/" + databaseFileName;
			string filePath = Application.persistentDataPath + "/" + databaseFileName;
			File.Delete (filePath);
			File.Copy (baseFilePath, filePath); 
			foreach (HistoryData historyData in historyDataList) {
				HistoryDataDao.instance.InsertHistoryData (historyData);
			}
			foreach (IDictionary quiz in quizList) {
				QuizListDao.instance.Insert (quiz);
			}
			PrefsManager.Instance.DatabaseVersion = 2;
			break;
		case 2:
			//オーダーナンバーを追加
			break;
		}

		updatedDatabaseEvent ();
	}
		
	private void ShowErrorDialog () {
		string title = "通信エラー";
		string message = "1度アプリを終了します";
		#if UNITY_IPHONE
		string[] buttons = { "OK" };
		EtceteraBinding.showAlertWithTitleMessageAndButtons (title, message, buttons);
		#endif

		#if UNITY_ANDROID
		EtceteraAndroid.showAlert (title,message,"OK");
		#endif
	}
}
