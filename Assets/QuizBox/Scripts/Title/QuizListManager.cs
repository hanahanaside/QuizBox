using UnityEngine;
using MiniJSON;
using System.Collections;
using System.Collections.Generic;

public class QuizListManager : MonoBehaviour {
	public HttpClient httpClient;
	public TitleInitializer titleInitializer;
	private static QuizListManager sInstance;
	private static IList allQuizList;
	private static IList mSeriesList;
	private bool created = false;
	private static string sJsonString;
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
			return sJsonString;
		}
	}

	public string modeName {
		get {
			return mModeName;
		}
	}
	
	void Awake () {
//		if(sInstance == null){
//
//			sInstance = this;
//			quizList = new List<IDictionary> ();
//			DontDestroyOnLoad (gameObject);
//
//
//		}

		if (!created) {
			sInstance = this;
			quizList = new List<IDictionary> ();
			DontDestroyOnLoad (gameObject);
			created = true;
		}
	}

	public void InitQuizList () {
		Debug.Log ("url = " + SelectedQuiz.instance.quizUrl);
		if(allQuizList == null){
			string title = "\u304a\u5f85\u3061\u304f\u3060\u3055\u3044";
			#if UNITY_IOS
			EtceteraBinding.showBezelActivityViewWithLabel(title);
			#endif
			
			#if UNITY_ANDROID
			string message = "\u554f\u984c\u3092\u53d6\u5f97\u3057\u3066\u3044\u307e\u3059";
			EtceteraAndroid.showProgressDialog(title,message);
			#endif

			HttpClient.responseEvent += ResponseCallback;
			WWW www = new WWW (SelectedQuiz.instance.quizUrl);
			StartCoroutine (httpClient.Excute (www));
		}else {
			titleInitializer.OnLoadFinished(true);
		}
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
		IList allSeriesList = new List<IDictionary> ();
		foreach (object quizObject in allQuizList) {
			IDictionary quizDictionary = (IDictionary)quizObject;
			string seriesName = quizDictionary ["series"].ToString ();
			if (seriesName == selectedSeriesName) {
				allSeriesList.Add (quizDictionary);
			}
		}
		IList indexNumberList = new List<int> ();
		while (indexNumberList.Count <15) {
			int number = Random.Range (0, allSeriesList.Count);
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
	}

	public void PlayChallenteModeResume (string jsonString, int questionCount, int correctCount) {
		Debug.Log ("resume");
		Debug.Log ("jsonString = " + jsonString);
		mModeName = "チャレンジモード";
		mQuestionCount = questionCount;
		mCorrectCount = correctCount;
		IDictionary jsonObject = (IDictionary)Json.Deserialize (jsonString);
		quizList = (IList)jsonObject ["updated_quiz"];
	}

	void ResponseCallback (string response) {
		Debug.Log ("ResponseCallback");
		HttpClient.responseEvent -= ResponseCallback;
		#if UNITY_IOS
		EtceteraBinding.hideActivityView();
		#endif
		
		#if UNITY_ANDROID
		EtceteraAndroid.hideProgressDialog();
		#endif

		if (string.IsNullOrEmpty(response)) {
			//error
			titleInitializer.OnLoadFinished (false);
		} else {
			sJsonString = response;
			sJsonString = sJsonString.Replace ("'", "");
			IDictionary jsonObject = (IDictionary)Json.Deserialize (sJsonString);
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

	public void ReleaseQuizList(){
		allQuizList = null;
	}

	public void Retry(){
		quizList = new List<IDictionary> ();
		if(mModeName == "クイックモード"){
			//quick mode
			PlayQuickMode();
		}else if(mModeName == "チャレンジモード"){
			//challenge mode
			PlayChallengeMode();
		}else {
			//series mode
			PlaySeriesMode(mModeName);
		}

	}

	private void ResetCount () {
		mQuestionCount = 1;
		mCorrectCount = 0;
	}
}
