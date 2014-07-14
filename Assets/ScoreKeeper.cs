using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour
{

	private static ScoreKeeper sInstance;

	public int score{ get; set; }

	public static ScoreKeeper instance {
		get {
			return sInstance;
		}
	}

	void Awake ()
	{
		if (sInstance == null) {
			sInstance = this;
			DontDestroyOnLoad (gameObject);
		}
	}
}
