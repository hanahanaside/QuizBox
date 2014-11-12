using UnityEngine;
using System.Collections;
using System;
using MiniJSON;
using System.Collections.Generic;

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
			UpdateToVersion1 ();
			break;
		case 1:
			UpdateToVersion2 ();
			break;
		case 2:
			updatedDatabaseEvent ();
			break;
		}
	}

	private void UpdateToVersion1 () {
		Debug.Log ("Update to ver1");
		try {
			QuizListDao.instance.AddQuizIdField ();
		} catch (Exception e) {
			Debug.Log ("error " + e);
		}

		WWWClient wwwClient = new WWWClient (this, JSON_URL);
		wwwClient.OnSuccess = (string response) => {
			UpdateQuizId (response);
			PrefsManager.Instance.DatabaseVersion = 1;
			UpdateDatabase();
		};
		wwwClient.OnFail = (string response) => {
			Debug.Log ("onFail");
			#if !UNITY_EDITOR
			ShowErrorDialog();
			#endif
		};
		wwwClient.OnTimeOut = () => {
			#if !UNITY_EDITOR
			ShowErrorDialog();
			#endif
		};
		wwwClient.GetData ();
	}

	private void UpdateQuizId (string response) {
		Debug.Log ("update quiz id");
		IList jsonArray = (IList)Json.Deserialize (response);
		IList<IDictionary> quizList = QuizListDao.instance.GetQuizList ();
		//銀魂クイズのquizIdを73にする
		IDictionary gintamaQuiz = quizList [0];
		gintamaQuiz [QuizListDao.QUIZ_ID_FIELD] = 73;
		QuizListDao.instance.UpdateQuiz (gintamaQuiz);
		foreach (IDictionary quiz in quizList) {
			CheckIndexOfTitle (jsonArray, quiz);
		}
	}

	private void UpdateToVersion2 () {
		Debug.Log ("Update to ver2");
		QuizListDao.instance.AddOrderNumberField (); 
		PrefsManager.Instance.DatabaseVersion = 2;
		UpdateDatabase ();
	}

	//名前で検索して、対応するクイズIDを挿入する
	private void CheckIndexOfTitle (IList jsonArray, IDictionary quiz) {
		string quizTitle = (string)quiz [QuizListDao.TITLE_FIELD];
		Debug.Log ("quizTitle = " + quizTitle);
		foreach (Dictionary<string,object> jsonObject in jsonArray) {
			string jsonObjectTitle = (string)jsonObject ["title"];
			if (jsonObjectTitle.IndexOf (quizTitle) >= 0) {
				Debug.Log ("hit");
				long quizId = (long)jsonObject ["id"];
				Debug.Log ("id = " + quizId);
				quiz [QuizListDao.QUIZ_ID_FIELD] = (int)quizId;
				QuizListDao.instance.UpdateQuiz (quiz);
				break;
			}
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
}
