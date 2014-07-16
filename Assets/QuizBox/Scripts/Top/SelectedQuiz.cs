using UnityEngine;
using System.Collections;

public class SelectedQuiz : MonoBehaviour
{

	private static SelectedQuiz sInstance;

	public string id{ get; set; }

	public string name{ get; set; }

	private bool created = false;

	public static SelectedQuiz instance {
		get {
			return sInstance;
		}
	}

	void Start ()
	{
		if(!created){
			DontDestroyOnLoad (gameObject);
			sInstance = this;
			created = true;
		}
	}
	


}
