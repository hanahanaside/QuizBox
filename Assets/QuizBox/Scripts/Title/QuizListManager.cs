using UnityEngine;
using MiniJSON;
using System.Collections;
using System.Collections.Generic;

public class QuizListManager : MonoBehaviour
{

	private static QuizListManager sInstance;
	private IList allQuizList;
	private IList mSeriesList;
	private bool created = false;
	private string mJsonString;

	public IList quizList{ get; set; }

	public static QuizListManager instance {
		get {
			return sInstance;
		}
	}

	public int allQuizListCount{
		get{
			return allQuizList.Count;
		}
	}

	public string jsonString{
		get{
			return mJsonString;
		}
	}

	void Awake ()
	{
		if (!created) {
			sInstance = this;
			quizList = new List<IDictionary> ();
			DontDestroyOnLoad (gameObject);
			created = true;
		}
	}

	public void InitQuizList ()
	{
		Debug.Log ("url = " + SelectedQuiz.instance.quizUrl);
		WWW www = new WWW (SelectedQuiz.instance.quizUrl);
		StartCoroutine (GetJson (www));
	}

	public void PlayQuickMode ()
	{
		IList indexNumberList = new List<int> ();
		while (indexNumberList.Count <15) {
			int number = Random.Range (0, allQuizList.Count);
			if (CheckNotDuplicate (indexNumberList, number)) {
				indexNumberList.Add (number);
			}
		}
		foreach (int indexNumber in indexNumberList) {
			IDictionary quiz = (IDictionary)allQuizList [indexNumber];
			quizList.Add (quiz);
		}
	}

	private bool CheckNotDuplicate (IList indexNumberList, int number)
	{
		foreach (int indexNumber in indexNumberList) {
			if (indexNumber == number) {
				return false;
			}
		}
		return true;
	}

	public void PlaySeriesMode (string selectedSeriesName)
	{
		foreach (object quizObject in allQuizList) {
			IDictionary quizDictionary = (IDictionary)quizObject;
			string seriesName = quizDictionary ["series"].ToString ();
			if (seriesName == selectedSeriesName) {
				quizList.Add (quizDictionary);
			}
		}
	}

	public void PlayChallengeMode ()
	{
		quizList = allQuizList;
	}

	private IEnumerator GetJson (WWW www)
	{
		yield return www;

		TitleInitializer titleInitializer = GameObject.Find ("TitleInitializer").GetComponent<TitleInitializer> ();
		// check for errors
		if (www.error == null) {
			mJsonString = www.text;
			Debug.Log ("WWW Ok!: ");
			Debug.Log ("json = " + mJsonString);
			IDictionary jsonObject = (IDictionary)Json.Deserialize (mJsonString);
			allQuizList = (IList)jsonObject ["updated_quiz"];
			mSeriesList = (IList)jsonObject ["series_orderd"];
			Debug.Log ("count = " + allQuizList.Count);
			titleInitializer.OnLoadFinished (true);
		} else {
			Debug.Log ("WWW Error: " + www.error);
			titleInitializer.OnLoadFinished (false);
		}
	}

	public IList SeriesList {
		get {
			return mSeriesList;
		}
	}
}
