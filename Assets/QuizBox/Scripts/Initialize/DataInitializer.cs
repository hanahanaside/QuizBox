using UnityEngine;
using System.Collections;
using System.IO;

public class DataInitializer : MonoBehaviour {

	private static string databaseFileName = "quiz_box.db";

	void Awake () {
		#if UNITY_EDITOR
		string filePath = Application.persistentDataPath + "/" + databaseFileName;
		File.Delete (filePath);
		PrefsManager.Instance.DatabaseVersion = 0;
		#endif
	}
}
