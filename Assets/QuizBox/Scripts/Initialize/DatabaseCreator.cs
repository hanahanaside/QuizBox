using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class DatabaseCreator : MonoBehaviour {
	public static event Action createdDatabaseEvent;

	private static string databaseFileName = "quiz_box.db";

	void OnEnable(){
		DatabaseUpdater.updatedDatabaseEvent += OnUpdated;
	}

	void OnDisable(){
		DatabaseUpdater.updatedDatabaseEvent -= OnUpdated;
	}


	void Start () {
		CreateDatabase ();
	}

	void OnUpdated(){
		CreatedDatabase ();
	}

	private void CreateDatabase () {
		string baseFilePath = Application.streamingAssetsPath + "/" + databaseFileName;
		string filePath = Application.persistentDataPath + "/" + databaseFileName;
		#if UNITY_EDITOR
		File.Delete(filePath);
		#endif

		#if UNITY_IPHONE
		if (!File.Exists (filePath)) {
			File.Copy (baseFilePath, filePath); 
			QuizListDao.instance.InitBoughtDate ();
			Debug.Log ("create Database");
		}

		DatabaseUpdater.Instance.UpdateDatabase();
		#endif

		#if UNITY_ANDROID 
		#if UNITY_EDITOR
		baseFilePath = "file://"+Path.Combine (Application.streamingAssetsPath, databaseFileName);
		#endif
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
		CreatedDatabase();
	}
	#endif

	private void CreatedDatabase () {
		Debug.Log ("create finished");
		CheckRenameQuiz();
		if (createdDatabaseEvent != null) {
			createdDatabaseEvent ();
		}
	}

	private void CheckRenameQuiz () {
		DateTime dtNow = DateTime.Now;
		string installedDate = PrefsManager.Instance.InstalledDate;
		if (string.IsNullOrEmpty (installedDate)) {
			PrefsManager.Instance.InstalledDate = dtNow.ToString ();
			return;
		}
		DateTime dtInstalled = DateTime.Parse (installedDate);
		TimeSpan timeSpan = dtNow - dtInstalled;
		Debug.Log ("days = " + timeSpan.TotalDays);
		if (timeSpan.TotalDays >= 3) {
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
