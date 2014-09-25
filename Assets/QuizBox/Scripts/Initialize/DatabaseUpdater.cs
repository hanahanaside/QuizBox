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
		string bundleVersion = PlayerSettings.bundleVersion;
		if(bundleVersion == "1.0"){
			UpdateToVersion1 ();
			updatedDatabaseEvent ();
		}
	}

	private void UpdateToVersion1(){

	}
}
