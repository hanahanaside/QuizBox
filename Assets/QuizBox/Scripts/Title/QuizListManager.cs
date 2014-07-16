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

	public IList quizList{ get; set; }

	public static QuizListManager instance {
		get {
			return sInstance;
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
		string id = SelectedQuiz.instance.id;
		string jsonUrl = "http://ryodb.us/projects/" + id + "/quizzes.json";
		WWW www = new WWW (jsonUrl);
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
		foreach(object quizObject in allQuizList){
			IDictionary quizDictionary = (IDictionary)quizObject;
			string seriesName = quizDictionary["series"].ToString();
			if(seriesName == selectedSeriesName){
				quizList.Add(quizDictionary);
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
			string json = www.text;
			Debug.Log ("WWW Ok!: ");
			Debug.Log ("json = " + json);
			IDictionary jsonObject = (IDictionary)Json.Deserialize (json);
			allQuizList = (IList)jsonObject ["updated_quiz"];
			mSeriesList = (IList)jsonObject["series_orderd"];
			Debug.Log ("count = " + allQuizList.Count);
			titleInitializer.OnLoadFinished (true);
		} else {
			Debug.Log ("WWW Error: " + www.error);
			titleInitializer.OnLoadFinished (false);
		}
	}

	public IList SeriesList{
		get{
			return mSeriesList;
		}
	}
}
