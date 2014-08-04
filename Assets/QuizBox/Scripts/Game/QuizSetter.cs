using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;

public class QuizSetter : MonoBehaviour
{

	public UILabel questionLabel;
	public UILabel scoreLabel;
	public UILabel seriesLabel;
	public UILabel[] buttonLabelArray;
	public TypewriterEffect typeWriterEffect;
	private StringBuilder mStringBuilder;
	private int mIndexNumber;
	private char[] mCharArray;

	void Start ()
	{
		mStringBuilder = new StringBuilder ();
		UpdateQuizComponents ();

	}

	public void UpdateQuizComponents ()
	{
		IDictionary quizDictionary = QuizKeeper.instance.quizDictionary;
		string series = (string)quizDictionary ["series"];
		string question = (string)quizDictionary ["question"];
		string answer = (string)quizDictionary ["answer"];
		string missTake1 = (string)quizDictionary ["mistake1"];
		string missTake2 = (string)quizDictionary ["mistake2"];
		string[] choicesArray = {answer,missTake1,missTake2};
		choicesArray = Shuffle (choicesArray);
		for (int i = 0; i <choicesArray.Length; i++) {
			string choice = choicesArray [i];
			buttonLabelArray [i].text = choice;
		}
		scoreLabel.text = QuizKeeper.instance.questionNumber + "/" + QuizListManager.instance.quizList.Count + "(" + ScoreKeeper.instance.score + ")";
		seriesLabel.text = series;
		mCharArray = question.ToCharArray ();
		mIndexNumber = 0;
		mStringBuilder.Length = 0;
		enabled = true;
		StartCoroutine (TypeWrite ());
	}

	private IEnumerator TypeWrite ()
	{
		if (!enabled) {
			return true; 
		}
		char text = mCharArray [mIndexNumber];
		mStringBuilder.Append (text);
		questionLabel.text = mStringBuilder.ToString ();
		yield return new WaitForSeconds (0.05f);
		if (mIndexNumber < mCharArray.Length - 1) {
			mIndexNumber++;
			StartCoroutine (TypeWrite ());
		}
	}

	private string[] Shuffle (string[] stringArray)
	{
		System.Random random = new System.Random ();
		int n = stringArray.Length;
		while (n>1) {
			n--;
			int k = random.Next (n + 1);
			string tmp = stringArray [k];
			stringArray [k] = stringArray [n];
			stringArray [n] = tmp;
		}
		return stringArray;
	}

	public void StopTyping ()
	{
		//mThinking = false;
		enabled = false;
	}

}
