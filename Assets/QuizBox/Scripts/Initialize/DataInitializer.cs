using UnityEngine;
using System.Collections;
using System.IO;

public class DataInitializer : MonoBehaviour {

	void Awake () {
		#if UNITY_EDITOR
		string databaseFileName = "quiz_box.db";
		string filePath = Application.persistentDataPath + "/" + databaseFileName;
		File.Delete (filePath);
		PrefsManager.Instance.DatabaseVersion = 0;
		#endif
	}
}
