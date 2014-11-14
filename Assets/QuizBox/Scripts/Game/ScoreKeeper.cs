using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	private static ScoreKeeper sInstance;

	public int score{ get; set; }

	public static ScoreKeeper instance {
		get {
			if(sInstance == null){
				sInstance = new ScoreKeeper ();
			}
			return sInstance;
		}
	}

	void Awake () {
		sInstance = this;
		DontDestroyOnLoad (gameObject);
	//	score = QuizListManager.instance.correctCount;
	}
}
