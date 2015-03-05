using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class DatabaseCreator : MonoSingleton<DatabaseCreator> {
	public static event Action createdDatabaseEvent;

	public DatabaseUpdater databaseUpdater;

	private static string databaseFileName = "quiz_box.db";

	void OnEnable () {
		DatabaseUpdater.updatedDatabaseEvent += OnUpdated;
	}

	void OnDisable () {
		DatabaseUpdater.updatedDatabaseEvent -= OnUpdated;
	}

	void OnUpdated () {
		CreatedDatabase ();
	}
		
	public void CreateDatabase () {
		string baseFilePath = Application.streamingAssetsPath + "/" + databaseFileName;
		string filePath = Application.persistentDataPath + "/" + databaseFileName;

		#if UNITY_IPHONE
		if (!File.Exists (filePath)) {
			File.Copy (baseFilePath, filePath); 
			InitBoughtDate ();
			Debug.Log ("create Database");
		}

		databaseUpdater.UpdateDatabase();
		#endif

		#if UNITY_ANDROID 
		#if UNITY_EDITOR
		baseFilePath = "file://" + Path.Combine (Application.streamingAssetsPath, databaseFileName);
		#endif
		Debug.Log ("file exists = " + File.Exists (filePath));
		if (File.Exists (filePath)) {
			databaseUpdater.UpdateDatabase ();
		} else {
			StartCoroutine (CreateAndroidDatabase (baseFilePath, filePath));
		}
#endif
	}

	#if UNITY_ANDROID
	private IEnumerator CreateAndroidDatabase (string baseFilePath, string filePath) {
		Debug.Log ("CreateAndroidDatabase");
		Debug.Log ("baseFilePath = " + baseFilePath);
		Debug.Log ("filePath = " + filePath);
		WWW www = new WWW (baseFilePath);
		yield return www;
		File.WriteAllBytes (filePath, www.bytes);
		InitBoughtDate ();
		databaseUpdater.UpdateDatabase ();
	}
	#endif

	private void CreatedDatabase () {
		Debug.Log ("create finished");
		#if !UNITY_EDITOR
		DateTime dtNow = DateTime.Now;
		string installedDate = PrefsManager.Instance.InstalledDate;
		if (string.IsNullOrEmpty (installedDate)) {
		PrefsManager.Instance.InstalledDate = dtNow.ToString ();
		}
		#endif
		if (createdDatabaseEvent != null) {
			createdDatabaseEvent ();
		}
	}

	private void InitBoughtDate () {
		List<Quiz> quizList = QuizListDao.instance.GetQuizList ();
		foreach (Quiz quiz in quizList) {
			quiz.BoughtDate = DateTime.Now.ToString ("yyyy/MM/dd");
			QuizListDao.instance.UpdateRecord (quiz);
		}
	}

	public void RenameDatabaseQuiz () {
		List<Quiz> quizList = QuizListDao.instance.GetQuizList ();
		foreach (Quiz quiz in quizList) {
			RenameFromSelledProjectsArray (quiz);
		}
	}

	//selled project array から該当のクイズを見つけてリネームする
	private void RenameFromSelledProjectsArray (Quiz quiz) {
		for (int i = 0; i < SelledProjectsManager.instance.Length; i++) {
			SelledProject selldProject = SelledProjectsManager.instance.Get (i);
			if (selldProject.id != quiz.QuizId) {
				continue;
			}
			//名前が既に同じだったら終了
			if (selldProject.title == quiz.Title) {
				break;
			}
			//リネーム
			Debug.Log ("Renamed " + quiz.Title + " to " + selldProject.title);
			quiz.Title = selldProject.title;
			QuizListDao.instance.UpdateTitle (quiz);
			break;
		}
	}
}
