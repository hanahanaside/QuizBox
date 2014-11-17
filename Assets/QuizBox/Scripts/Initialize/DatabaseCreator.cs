using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class DatabaseCreator : MonoBehaviour {
	public static event Action createdDatabaseEvent;
	public DatabaseUpdater databaseUpdater;
	public QuizRenamer quizRenamer;

	private static string databaseFileName = "quiz_box.db";

	void OnEnable(){
		DatabaseUpdater.updatedDatabaseEvent += OnUpdated;
		QuizRenamer.renamedQuizEvent += OnRenamed;
	}

	void OnDisable(){
		DatabaseUpdater.updatedDatabaseEvent -= OnUpdated;
		QuizRenamer.renamedQuizEvent -= OnRenamed;
	}


	void Start () {
		CreateDatabase ();
	}

	void OnUpdated(){
		quizRenamer.RenameQuiz ();
	}

	void OnRenamed(){
		CreatedDatabase ();
	}

	private void CreateDatabase () {
		string baseFilePath = Application.streamingAssetsPath + "/" + databaseFileName;
		string filePath = Application.persistentDataPath + "/" + databaseFileName;

		#if UNITY_IPHONE
		if (!File.Exists (filePath)) {
			File.Copy (baseFilePath, filePath); 
			QuizListDao.instance.InitBoughtDate ();
			Debug.Log ("create Database");
		}

		databaseUpdater.UpdateDatabase();
		#endif

		#if UNITY_ANDROID 
		#if UNITY_EDITOR
		baseFilePath = "file://"+Path.Combine (Application.streamingAssetsPath, databaseFileName);
		#endif
		Debug.Log("file exists = " + File.Exists(filePath));
		if(File.Exists(filePath)){
			CreatedDatabase();
		}else {
			StartCoroutine(CreateAndroidDatabase(baseFilePath,filePath));
		}
#endif
	}
	#if UNITY_ANDROID
	private IEnumerator CreateAndroidDatabase (string baseFilePath,string filePath)
	{
		Debug.Log ("CreateAndroidDatabase");
		Debug.Log ("baseFilePath = " + baseFilePath);
		Debug.Log ("filePath = " + filePath);
		WWW www = new WWW (baseFilePath);
		yield return www;
		File.WriteAllBytes (filePath, www.bytes);
		QuizListDao.instance.InitBoughtDate();
		databaseUpdater.UpdateDatabase();
	}
	#endif

	private void CreatedDatabase () {
		Debug.Log ("create finished");
		#if !UNITY_EDITOR
	//	CheckRenameQuiz();
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

	private void CheckRenameQuiz () {
		Debug.Log ("check rename quiz");
		DateTime dtNow = DateTime.Now;
		string installedDate = PrefsManager.Instance.InstalledDate;
		if (string.IsNullOrEmpty (installedDate)) {
			PrefsManager.Instance.InstalledDate = dtNow.ToString ();
			return;
		}
		DateTime dtInstalled = DateTime.Parse (installedDate);
		TimeSpan timeSpan = dtNow - dtInstalled;
		Debug.Log ("days = " + timeSpan.Days);
		if (timeSpan.Days >= 3) {
			RenameToGintamaQuiz ();
		} 
	}

	private void RenameToGintamaQuiz () {
		Debug.Log ("Rename");
		IList<IDictionary> quizList = QuizListDao.instance.GetQuizList ();
		foreach(IDictionary quiz in quizList){
			string title = (string)quiz[QuizListDao.TITLE_FIELD];
			if(title == "金魂クイズ"){
				quiz[QuizListDao.TITLE_FIELD] = "銀魂クイズ";
				QuizListDao.instance.UpdateQuiz (quiz);
				break;
			}
		}
	}
}
