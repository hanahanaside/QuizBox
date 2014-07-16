using UnityEngine;
using System.Collections;

public class Referee : MonoBehaviour
{
	public GameObject goodAnswerAnimationPrefab;
	public GameObject failedAnswerAnimationPrefab;
	public GameObject uiRoot;
	
	public void Judge (string selectedText)
	{
		IDictionary quizDictionary = QuizKeeper.instance.quizDictionary;
		string answer = (string)quizDictionary ["answer"];
		if (selectedText == answer) {
			StartAnswerAnimation(goodAnswerAnimationPrefab);
			ScoreKeeper.instance.score = ScoreKeeper.instance.score + 1;
			Debug.Log ("success");
		} else {
			StartAnswerAnimation(failedAnswerAnimationPrefab);
			Debug.Log ("fail");
		}
		StartCoroutine (Next ());
	}

	private void StartAnswerAnimation(GameObject animationPrefab){
		GameObject animationObject = Instantiate (animationPrefab) as GameObject;
		animationObject.transform.parent = uiRoot.transform;
		animationObject.transform.localScale = new Vector3 (1, 1, 1);
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
