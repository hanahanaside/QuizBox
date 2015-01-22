using UnityEngine;
using System.Collections;

public class AnswerDialogController : MonoBehaviour {

	public QuizKeeper quizKeeper;
	public QuizSetter quizSetter;

	public void OnNextButtonClicked () {
		ImobileManager.Instance.HideRectangleAd ();
		if (quizKeeper.questionNumber >= QuizListManager.instance.quizList.Count) {
			Application.LoadLevel ("Result");
		} else {
			quizKeeper.UpdateQuiz ();
			quizSetter.UpdateQuizComponents ();
			transform.parent.gameObject.SetActive (false);
		}
	}
}
