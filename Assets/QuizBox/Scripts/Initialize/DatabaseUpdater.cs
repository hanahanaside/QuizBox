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
			List<Quiz> quizList = QuizListDao.instance.GetQuizList ();
			if(quizList.Count ==0){
				//金魂クイズをインサート
				Quiz kinkonQuiz = new Quiz ();
				kinkonQuiz.Title = "金魂クイズ";
				kinkonQuiz.QuizUrl = "http://ryodb.us/projects/5035e95766e5411652000001/quizzes.json";
				kinkonQuiz.QuizId = 73;
				kinkonQuiz.BoughtDate = DateTime.Now.ToString ("yyyy/MM/dd");
				kinkonQuiz.OrderNumber = 1;
				QuizListDao.instance.Insert (kinkonQuiz);
				PrefsManager.Instance.DatabaseVersion = 3;
			}else {
				//ver0のままアップデートした人なので既存のレコードにquiz id を入れてあげる
				RemakeDatabase ();
				InitQuizId ();
				InitOrderNumber ();
				PrefsManager.Instance.DatabaseVersion = 3;
			}
			break;
		case 1:
			//ヒストリーデータを再構築(tweet_flagを削除)ver1.9
			RemakeDatabase ();
			InitOrderNumber ();
			PrefsManager.Instance.DatabaseVersion = 3;
			break;
		case 2:
			//オーダーナンバーを追加 ver2.0
			RemakeDatabase ();
			InitOrderNumber ();
			PrefsManager.Instance.DatabaseVersion = 3;
			break;
		}
		updatedDatabaseEvent ();
	}

	private void InitOrderNumber () {
		List<Quiz> quizList = QuizListDao.instance.GetQuizList ();
		foreach (Quiz quiz in quizList) {
			quiz.OrderNumber = quiz.Id;
			QuizListDao.instance.UpdateOrderNumber (quiz);
		}
	}

	private void RemakeDatabase () {
		IList<HistoryData> historyDataList = HistoryDataDao.instance.QueryHistoryDataList ();
		List<Quiz> quizList = QuizListDao.instance.GetQuizList ();
		string databaseFileName = "quiz_box.db";
		string baseFilePath = Application.streamingAssetsPath + "/" + databaseFileName;
		string filePath = Application.persistentDataPath + "/" + databaseFileName;
		File.Delete (filePath);
		File.Copy (baseFilePath, filePath); 
		foreach (HistoryData historyData in historyDataList) {
			HistoryDataDao.instance.InsertHistoryData (historyData);
		}
		foreach (Quiz quiz in quizList) {
			QuizListDao.instance.Insert (quiz);
		}
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

	private void InitQuizId(){

	}
}
