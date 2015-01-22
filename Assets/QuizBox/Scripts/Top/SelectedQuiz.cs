using UnityEngine;
using System.Collections;

public class SelectedQuiz : MonoBehaviour {

	private static SelectedQuiz sInstance;

	public int id{ get; set; }

	public string Name{ get; set; }

	public string quizUrl{ get; set; }

	public string boughtDate{ get; set; }

	public static SelectedQuiz instance {
		get {
			return sInstance;
		}
	}

	void Start () {
		DontDestroyOnLoad (gameObject);
		sInstance = this;
	}
	


}
