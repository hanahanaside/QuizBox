using UnityEngine;
using MiniJSON;
using System.Collections;
using System.Collections.Generic;

public class QuizListManager : MonoBehaviour {
	public HttpClient httpClient;
	private static QuizListManager sInstance;
	private IList allQuizList;
	private IList mSeriesList;
	private bool created = false;
	private string mJsonString;
	private string mModeName;
	private int mQuestionCount;
	private int mCorrectCount;

	public IList quizList{ get; set; }

	public static QuizListManager instance {
		get {
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
			return mJsonString;
		}
	}

	public string modeName {
		get {
			return mModeName;
		}
	}
	
	void Awake () {
		if (!created) {
			sInstance = this;
			quizList = new List<IDictionary> ();
			DontDestroyOnLoad (gameObject);
			created = true;
		}
	}

	public void InitQuizList () {
		Debug.Log ("url = " + SelectedQuiz.instance.quizUrl);
		HttpClient.responseEvent += ResponseCallback;
		WWW www = new WWW (SelectedQuiz.instance.quizUrl);
		StartCoroutine (httpClient.Excute (www));
	}

	public void PlayQuickMode () {
		mModeName = "クイックモード";
		ResetCount ();
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

	private bool CheckNotDuplicate (IList indexNumberList, int number) {
		foreach (int indexNumber in indexNumberList) {
			if (indexNumber == number) {
				return false;
			}
		}
		return true;
	}

	public void PlaySeriesMode (string selectedSeriesName) {
		mModeName = selectedSeriesName;
		ResetCount ();
		foreach (object quizObject in allQuizList) {
			IDictionary quizDictionary = (IDictionary)quizObject;
			string seriesName = quizDictionary ["series"].ToString ();
			if (seriesName == selectedSeriesName) {
				quizList.Add (quizDictionary);
			}
		}
	}
	
	public void PlayChallengeMode () {
		mModeName = "チャレンジモード";
		ResetCount ();
		quizList = allQuizList;
	}

	public void PlayChallenteModeResume (string jsonString, int questionCount, int correctCount) {
		mModeName = "チャレンジモード";
		mQuestionCount = questionCount;
		mCorrectCount = correctCount;
		IDictionary jsonObject = (IDictionary)Json.Deserialize (jsonString);
		quizList = (IList)jsonObject ["updated_quiz"];
	}

	void ResponseCallback (string response) {
		Debug.Log ("ResponseCallback");
		HttpClient.responseEvent -= ResponseCallback;
		TitleInitializer titleInitializer = GameObject.Find ("TitleInitializer").GetComponent<TitleInitializer> ();
		#if UNITY_IOS
		EtceteraBinding.hideActivityView();
		#endif
		
		#if UNITY_ANDROID
		EtceteraAndroid.hideProgressDialog();
		#endif

		if (response == null) {
			//error
			titleInitializer.OnLoadFinished (false);
		} else {
			mJsonString = response;
			mJsonString = mJsonString.Replace ("'", "");
			IDictionary jsonObject = (IDictionary)Json.Deserialize (mJsonString);
			allQuizList = (IList)jsonObject ["updated_quiz"];
			mSeriesList = (IList)jsonObject ["series_orderd"];
			Debug.Log ("count = " + allQuizList.Count);
			titleInitializer.OnLoadFinished (true);

		}
	}

	public IList SeriesList {
		get {
			return mSeriesList;
		}
	}

	private void ResetCount () {
		mQuestionCount = 1;
		mCorrectCount = 0;
	}
}
