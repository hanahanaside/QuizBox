using UnityEngine;
using System.Collections;

public class Referee : MonoBehaviour {
	public GameObject answerDialog;
	public UISprite correctSprite;
	public UILabel answerLabel;
	public UIAtlas atlas;
	
	public void Judge (string selectedText) {
		IDictionary quizDictionary = QuizKeeper.instance.quizDictionary;
		string answer = (string)quizDictionary ["answer"];
		if (selectedText == answer) {
			SoundManager.Instance.PlaySESound(SoundManager.GOOD_SOUND_ID);
			ShowAnswerDialog (answer, true);
			ScoreKeeper.instance.score = ScoreKeeper.instance.score + 1;
			Debug.Log ("success");
		} else {
			SoundManager.Instance.PlaySESound(SoundManager.BAD_SOUND_ID);
			ShowAnswerDialog (answer, false);
			Debug.Log ("fail");
		}
	}

	private void ShowAnswerDialog (string answer, bool isCorrect) {
		ImobileManager.Instance.ShowRectangleAd ();
		answerDialog.SetActive (true);
		answerLabel.text = "正解は「" + answer + "」";
		string spriteName;
		if (isCorrect) {
			spriteName = "correct_answer";
		} else {
			spriteName = "incorrect_answer";
		}
		correctSprite.spriteName = spriteName;
		int width = atlas.GetSprite (spriteName).width;
		int height = atlas.GetSprite (spriteName).height;
		correctSprite.SetDimensions (width, height);
	}

}
