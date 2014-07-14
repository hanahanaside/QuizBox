using UnityEngine;
using System.Collections;

public class Referee : MonoBehaviour
{
	public GameObject goodAnswerAnimationPrefab;
	public GameObject uiRoot;
	
	public void Judge (string selectedText)
	{
		IDictionary quizDictionary = QuizKeeper.instance.quizDictionary;
		string answer = (string)quizDictionary ["answer"];
		if (selectedText == answer) {
			GameObject goodAnswerAnimationObject = Instantiate (goodAnswerAnimationPrefab) as GameObject;
			goodAnswerAnimationObject.transform.parent = uiRoot.transform;
			goodAnswerAnimationObject.transform.localScale = new Vector3 (1, 1, 1);
			ScoreKeeper.instance.score = ScoreKeeper.instance.score + 1;
			Debug.Log ("success");
		} else {
			Debug.Log ("fail");
		}
		StartCoroutine (Next ());
	}

	private IEnumerator Next ()
	{
		yield return new WaitForSeconds (2.0f);
		if (QuizKeeper.instance.questionNumber >= QuizListManager.instance.quizList.Count) {
			Application.LoadLevel ("Result");
		} else {
			GameObject.Find ("QuizKeeper").GetComponent<QuizKeeper> ().UpdateQuiz ();
			GameObject.Find ("QuizSetter").GetComponent<QuizSetter> ().UpdateQuizComponents ();
		}

	}

}
