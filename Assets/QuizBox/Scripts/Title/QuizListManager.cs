using UnityEngine;
using MiniJSON;
using System.Collections;
using System.Collections.Generic;
using System;

public class QuizListManager : MonoBehaviour {
	public event Action loadSucceedEvent;
	public event Action loadFailedEvent;
	private static QuizListManager sInstance;
	private static IList allQuizList;
	private static IList mSeriesList;
	private static string sJsonString;
	private string mModeName;
	private int mQuestionCount;
	private int mCorrectCount;

	public IList quizList{ get; set; }

	public static QuizListManager instance {
		get {
			if(sInstance == null){
				sInstance = new QuizListManager ();
			}
			return sInstance;
		}
	}

	public int allQuizListCount {
		get {
			return allQuizList.Count;
		}
	}

	public int questionCount {
		get {
			return mQuestionCount;
		}
		set {
			mQuestionCount = value;
		}
	}

	public int correctCount {
		get {
			return mCorrectCount;
		}
		set {
			mCorrectCount = value;
		}
	}

	public string jsonString {
		get {
			return sJsonString;
		}
	}

	public string modeName {
		get {
			return mModeName;
		}
	}

	void Awake () {
		sInstance = this;
		DontDestroyOnLoad (gameObject);
	}

	public void InitQuizList () {
		Debug.Log ("url = " + SelectedQuiz.instance.quizUrl);
		if (allQuizList == null) {
			WWWClient wwwClient = new WWWClient (this,SelectedQuiz.instance.quizUrl);
			wwwClient.OnSuccess = (string response) => {
				sJsonString = response;
				sJsonString = sJsonString.Replace ("'", "");
				IDictionary jsonObject = (IDictionary)Json.Deserialize (sJsonString);
				allQuizList = (IList)jsonObject ["updated_quiz"];
				mSeriesList = (IList)jsonObject ["series_orderd"];
				Debug.Log ("count = " + allQuizList.Count);
				loadSucceedEvent ();
			};
				
			wwwClient.OnFail = (string response) => {
				loadFailedEvent ();
			};

			wwwClient.OnTimeOut = () => {
				loadFailedEvent ();
			};
			wwwClient.GetData ();
		} else {
			loadSucceedEvent ();
		}
	}

	public void PlayQuickMode () {
		mModeName = "クイックモード";
		quizList = new List<IDictionary> ();
		ResetCount ();
		IList indexNumberList = new List<int> ();
		while (indexNumberList.Count < 15) {
			int number = UnityEngine.Random.Range (0, allQuizList.Count);
			if (CheckNotDuplicate (indexNumberList, number)) {
				indexNumberList.Add (number);
			}
		}
		foreach (int indexNumber in indexNumberList) {
			IDictionary quiz = (IDictionary)allQuizList [indexNumber];
			quizList.Add (quiz);
		}
	}

	private bool CheckNotDuplicate (IList indexNumberList, int number) {
		foreach (int indexNumber in indexNumberList) {
			if (indexNumber == number) {
				return false;
			}
		}
		return true;
	}

	public void PlaySeriesMode (string selectedSeriesName) {
		quizList = new List<IDictionary> ();
		mModeName = selectedSeriesName;
		ResetCount ();
		IList allSeriesList = new List<IDictionary> ();
		foreach (object quizObject in allQuizList) {
			IDictionary quizDictionary = (IDictionary)quizObject;
			string seriesName = quizDictionary ["series"].ToString ();
			if (seriesName == selectedSeriesName) {
				allSeriesList.Add (quizDictionary);
			}
		}
		IList indexNumberList = new List<int> ();
		while (indexNumberList.Count < 15) {
			int number = UnityEngine.Random.Range (0, allSeriesList.Count);
			if (CheckNotDuplicate (indexNumberList, number)) {
				indexNumberList.Add (number);
			}
		}
		foreach (int indexNumber in indexNumberList) {
			IDictionary quiz = (IDictionary)allSeriesList [indexNumber];
			quizList.Add (quiz);
		}
	}

	public void PlayChallengeMode () {
		mModeName = "チャレンジモード";
		ResetCount ();
		quizList = allQuizList;
		System.Random rng = new System.Random ();
		int n = quizList.Count;
		while (n > 1) {
			n--;
			int k = rng.Next (n + 1);
			IDictionary quiz = (IDictionary)quizList [k];
			quizList [k] = quizList [n];
			quizList [n] = quiz;
		}
	}

	public void PlayChallenteModeResume (string jsonString, int questionCount, int correctCount) {
		Debug.Log ("resume");
		mModeName = "チャレンジモード";
		mQuestionCount = questionCount;
		mCorrectCount = correctCount;
		IDictionary jsonObject = (IDictionary)Json.Deserialize (jsonString);
		quizList = (IList)jsonObject ["updated_quiz"];
	}
		

	public IList SeriesList {
		get {
			return mSeriesList;
		}
	}

	public void ReleaseQuizList () {
		allQuizList = null;
	}

	public void Retry () {
		if (mModeName == "クイックモード") {
			//quick mode
			PlayQuickMode ();
		} else if (mModeName == "チャレンジモード") {
			//challenge mode
			PlayChallengeMode ();
		} else {
			//series mode
			PlaySeriesMode (mModeName);
		}

	}

	private void ResetCount () {
		mQuestionCount = 1;
		mCorrectCount = 0;
	}
}
