using UnityEngine;
using System.Collections;

public class SelectedQuiz : MonoBehaviour
{

	private static SelectedQuiz sInstance;

	public int id{ get; set; }

	public string name{ get; set; }

	public string quizUrl{get;set;}

	public string boughtDate{get;set;}

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
