using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class DatabaseCreator : MonoBehaviour
{
	public static event Action createdDatabaseEvent;
	private static string databaseFileName = "quiz_box.db";

	void Start(){
		CreateDatabase();
	}

	// Use this for initialization
	private void CreateDatabase ()
	{
		string baseFilePath = Application.streamingAssetsPath + "/" + databaseFileName;
		string filePath = Application.persistentDataPath + "/" + databaseFileName;
		#if UNITY_IPHONE
		if(!File.Exists(filePath)){
			File.Copy( baseFilePath, filePath); 
			QuizListDao.instance.InitBoughtDate();
			Debug.Log("create Database");
		}
		CreatedDatabase();
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

	private void CreatedDatabase()
	{
		Debug.Log ("create finished");
		if( createdDatabaseEvent != null ){
			createdDatabaseEvent();
		}
	}

}
