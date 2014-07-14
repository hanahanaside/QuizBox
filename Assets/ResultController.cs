using UnityEngine;
using System.Collections;

public class ResultController : MonoBehaviour
{

	public UILabel resultLabel;

	// Use this for initialization
	void Start ()
	{
		int score = ScoreKeeper.instance.score;
		int size = QuizListManager.instance.quizList.Count;
		resultLabel.text = size + "問中" + score + "問正解!!";
		Debug.Log ("score = " + score);
	}
}
