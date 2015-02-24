using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class Test : MonoBehaviour {

	private static string databaseFileName = "quiz_box.db";

	void Start(){
		string filePath = Application.persistentDataPath + "/" + databaseFileName;
		File.Delete (filePath);
		PlayerPrefs.DeleteAll ();
	}
		
}
