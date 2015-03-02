using UnityEngine;
using System.Collections;
using System.IO;

public class DeleteAllData : MonoBehaviour {

	private static string databaseFileName = "quiz_box.db";

	// Use this for initialization
	void Start () {
		string filePath = Application.persistentDataPath + "/" + databaseFileName;
		File.Delete (filePath);
		PlayerPrefs.DeleteAll ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
