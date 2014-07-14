using UnityEngine;
using System.Collections;

public class SelectedQuiz : MonoBehaviour
{

	private static SelectedQuiz sInstance;

	public string id{ get; set; }

	public string name{ get; set; }

	public static SelectedQuiz instance {
		get {
			return sInstance;
		}
	}

	void Start ()
	{
		if (sInstance == null) {
			sInstance = this;
			DontDestroyOnLoad (gameObject);
		}
	}
	


}
