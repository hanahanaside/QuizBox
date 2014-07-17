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
		#if UNITY_IPHONE
		string baseFilePath = Application.streamingAssetsPath + "/" + databaseFileName;
		string filePath = Application.persistentDataPath + "/" + databaseFileName;
		if(!File.Exists(filePath)){
		File.Delete(filePath);
			File.Copy( baseFilePath, filePath); 
			Debug.Log("create Database");
		}
		CreatedDatabase();
		#endif

		#if UNITY_ANDROID
		string baseFilePath = Path.Combine (Application.streamingAssetsPath, databaseFileName);
		string filePath = Application.persistentDataPath + "/" + databaseFileName;
#if UNITY_EDITOR
		File.Delete(filePath);
		Debug.Log("delete");
		baseFilePath = "file://"+Path.Combine (Application.streamingAssetsPath, databaseFileName);
#endif
		File.Delete(filePath);
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
