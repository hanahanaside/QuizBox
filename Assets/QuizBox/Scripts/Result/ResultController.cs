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

	void Update ()
	{
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel("Top");
		}
	}


	public void OnButtonClick(){
		Reset();
		string buttonName = UIButton.current.name;
		if(buttonName == "RetryButton"){
			Application.LoadLevel("Game");
		}
		if(buttonName == "TopButton"){
			Application.LoadLevel("Top");
		}
	}

	private void Reset(){
		ScoreKeeper.instance.score = 0;
	}
}
