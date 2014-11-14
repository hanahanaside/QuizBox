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

	void alertButtonClickedEvent(string clickedButton){
		Application.Quit ();
	}

	void alertCancelledEvent(){
		ShowErrorDialog ();
	}

	public void UpdateDatabase () {
		Debug.Log ("update database");
		int databaseVersion = PrefsManager.Instance.DatabaseVersion;
		switch(databaseVersion){
		case 0:
			//quiz id カラムを追加
			AddQuizIdField ();
			//order numberカラムを追加
			AddOrderNumberField ();
			//クイズIDをアップデート
			UpdateQuizIdField ();
			//order numberをアップデート
			UpdateOrderNumberField ();
			break;
		case 1:
			//order numberカラムを追加
			AddOrderNumberField ();
			//order numberをアップデート
			UpdateOrderNumberField ();
			//テーブルを作り直し
			IList<HistoryData> historyDataList = HistoryDataDao.instance.QueryHistoryDataList ();
			List<IDictionary> quizList = QuizListDao.instance.GetQuizList ();
			string baseFilePath = Application.streamingAssetsPath + "/" + "quiz_box.db";
			string filePath = Application.persistentDataPath + "/" + "quiz_box.db";
			File.Delete (filePath);
			File.Copy (baseFilePath, filePath); 
			foreach(HistoryData historyData in historyDataList){
				HistoryDataDao.instance.InsertHistoryData (historyData);
			}
			foreach(IDictionary quiz in quizList){

			}
			PrefsManager.Instance.DatabaseVersion = 2;
			updatedDatabaseEvent ();
			break;
		case 2:
			//やることなし
			updatedDatabaseEvent ();
			break;
		}
	}

	private void AddQuizIdField(){
		Debug.Log ("クイズIDカラムを追加");
		try{
			QuizListDao.instance.AddQuizIdField ();
		}catch(Exception e){
			Debug.Log ("error " + e);
		}
	}

	private void AddOrderNumberField(){
		Debug.Log ("Order Number カラムを追加");
		try{
			QuizListDao.instance.AddOrderNumberField();
		}catch(Exception e){
			Debug.Log ("error " + e);
		}
	}

	private void UpdateOrderNumberField(){
		IList<IDictionary> quizList = QuizListDao.instance.GetQuizList ();
		foreach(IDictionary quiz in quizList){
			quiz [QuizListDao.ORDER_NUMBER] = quiz [QuizListDao.ID_FIELD];
			QuizListDao.instance.UpdateQuiz (quiz);
		}
	}

	private void UpdateQuizIdField () {
		Debug.Log ("Update Quiz Id Field");

		WWWClient wwwClient = new WWWClient (this, JSON_URL);
		wwwClient.OnSuccess = (string response) => {
			UpdateQuizId (response);
			PrefsManager.Instance.DatabaseVersion = 2;
			updatedDatabaseEvent ();
		};
		wwwClient.OnFail = (string response) => {
			Debug.Log("onFail");
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
		IDictionary gintamaQuiz = quizList[0];
		gintamaQuiz [QuizListDao.QUIZ_ID_FIELD] = 73;
		QuizListDao.instance.UpdateQuiz (gintamaQuiz);
		foreach (IDictionary quiz in quizList) {
			CheckIndexOfTitle (jsonArray, quiz);
		}
	}

	//名前で検索して、対応するクイズIDを挿入する
	private void CheckIndexOfTitle (IList jsonArray, IDictionary quiz) {
		string quizTitle = (string)quiz[QuizListDao.TITLE_FIELD];
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

	private void ShowErrorDialog(){
		string title = "通信エラー";
		string message = "1度アプリを終了します";
		#if UNITY_IPHONE
		string[] buttons = {"OK"};
		EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
		#endif

		#if UNITY_ANDROID
		EtceteraAndroid.showAlert (title,message,"OK");
		#endif
	}
}
