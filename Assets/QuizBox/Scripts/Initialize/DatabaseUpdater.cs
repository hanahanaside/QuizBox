using UnityEngine;
using System.Collections;
using System;
using UnityEditor;

public class DatabaseUpdater {

	public static event Action updatedDatabaseEvent;
	private static DatabaseUpdater sInstance;

	public static DatabaseUpdater Instance{
		get{
			if(sInstance == null){
				sInstance = new DatabaseUpdater ();
			}
			return sInstance;
		}
	}

	public void UpdateDatabase(){
		if(!QuizListDao.instance.QuizIdFieldExist ()){
			UpdateToVersion1 ();
			QuizListDao.instance.QuizIdFieldExist ();
			updatedDatabaseEvent ();
		}else {
			updatedDatabaseEvent ();
		}

	}

	private void UpdateToVersion1(){
		Debug.Log ("Update to ver1");
		QuizListDao.instance.AddQuizIdField ();
	}
}
