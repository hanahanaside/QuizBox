using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour
{

	private static ScoreKeeper sInstance;

	public int score{ get; set; }

	private bool created = false;

	public static ScoreKeeper instance {
		get {
			return sInstance;
		}
	}

	void Awake ()
	{
		if (!created) {
			sInstance = this;
			DontDestroyOnLoad (gameObject);
			created = true;
		}
		score = QuizListManager.instance.correctCount;
	}
}
