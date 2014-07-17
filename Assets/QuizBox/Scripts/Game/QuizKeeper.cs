using UnityEngine;
using System.Collections;

public class QuizKeeper : MonoBehaviour
{

	public IDictionary quizDictionary{ get; set; }

	public int questionNumber{ get; set; }

	private static QuizKeeper sInstance;

	public static QuizKeeper instance {
		get {
			return sInstance;
		}
	}

	void Awake ()
	{
		sInstance = this;
		questionNumber = 1;
		setQuiz ();
	}

	public void UpdateQuiz ()
	{
		questionNumber++;
		setQuiz ();
	}

	private void setQuiz ()
	{
		quizDictionary = (IDictionary)QuizListManager.instance.quizList [questionNumber - 1];
	}
}
