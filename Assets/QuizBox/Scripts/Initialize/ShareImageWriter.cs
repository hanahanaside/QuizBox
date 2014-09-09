using UnityEngine;
using System.Collections;
using System.IO;

public class ShareImageWriter : MonoBehaviour {

	#if UNITY_ANDROID

	private const string FILE_NAME = "share_image.png";

	void Start () {
		string filePath = Application.persistentDataPath +  "/"+ FILE_NAME;
		if(!File.Exists(filePath)){
			DontDestroyOnLoad (gameObject);
			StartCoroutine (WriteFile());
		}
	}

	private IEnumerator WriteFile(){
		Debug.Log ("write file");
		string baseFilePath = Application.streamingAssetsPath + "/" + FILE_NAME;
		WWW www = new WWW (baseFilePath);
		yield return www;
		string filePath = Application.persistentDataPath +  "/" + FILE_NAME;
		File.WriteAllBytes (filePath, www.bytes);
		Destroy (gameObject);
	}

	#endif
}
