using UnityEngine;
using MiniJSON;
using System.Collections;
using System.Collections.Generic;

public class AddQuizInitializer : MonoBehaviour {
	public HttpClient httpClient;
	public UIGrid grid;
	public GameObject addQuizButtonPrefab;
	public OkDialog okDialogPrefab;
	public UIScrollView scrollView;
	private const string JSON_URL = "http://quiz.ryodb.us/list/selled_projects.json";
	private IList mAddQuizButtonList = null;
	private List<Quiz> mQuizList;

	void OnEnable () {
		Debug.Log ("enable");
		HttpClient.responseEvent += ResponseCallback;
	}

	void OnDisable () {
		Debug.Log ("disable");
		HttpClient.responseEvent -= ResponseCallback;
	}
	// Use this for initialization
	void Start () {
		Debug.Log ("start");
		ShowProgressDialog ();
		mQuizList = QuizListDao.instance.GetQuizList ();
		WWW www = new WWW (JSON_URL);
		StartCoroutine (httpClient.Excute (www));
	}

	void ResponseCallback (string response) {

		if (response == null) {
			//error
			DismissProgressDialog ();
			NetworkErrorDialog dialog = new NetworkErrorDialog ();
			dialog.Show ();
		} else {
			mAddQuizButtonList = (IList)Json.Deserialize (response);
			CreateScrollView (mAddQuizButtonList);
		}
	} 

	public void RemakeList(){
		ShowProgressDialog ();
		List<Transform> childList = grid.GetChildList();
		foreach(Transform child in childList){
			Destroy (child.gameObject);
		}
		mQuizList = QuizListDao.instance.GetQuizList ();
		WWW www = new WWW (JSON_URL);
		StartCoroutine (httpClient.Excute (www));
	}

	private void CreateScrollView (IList jsonArray) {
		foreach (object item in jsonArray) {
			IDictionary jsonObject = (IDictionary)item;
			SetButtons (jsonObject);
		}
		scrollView.ResetPosition ();
		DismissProgressDialog ();
	}

	private void SetButtons (IDictionary jsonObject) {
		bool publish = (bool)jsonObject ["publish"];
		string title = jsonObject ["title"].ToString ();
		if (!publish) {
			return;
		}
		if (CheckDuplicateQuiz (jsonObject)) {
			return;
		}
		long point = (long)jsonObject ["point"];
		string url = jsonObject ["quiz_management_url"].ToString ();
		long quizCount = (long)jsonObject ["quiz_count"];
		long quizId = (long)jsonObject ["id"];

		AddQuiz addQuiz = new AddQuiz ();
		addQuiz.point = (int)point;
		addQuiz.url = url;
		addQuiz.title = title;
		addQuiz.quizCount = (int)quizCount;
		addQuiz.QuizId = (int)quizId;
		GameObject addQuizButtonObject = Instantiate (addQuizButtonPrefab)as GameObject;
		grid.AddChild (addQuizButtonObject.transform);
		addQuizButtonObject.transform.localScale = new Vector3 (1, 1, 1);
		addQuizButtonObject.BroadcastMessage ("Init", addQuiz);
	}

	private bool CheckDuplicateQuiz (IDictionary jsonObject) {
		long jsonObjectId = (long)jsonObject ["id"];
		foreach (Quiz quiz in mQuizList) {
			int quizId = quiz.QuizId;
			if (jsonObjectId == quizId) {
				return true;
			}
		}
		return false;
	}

	private void ShowProgressDialog(){
		string title = "\u304a\u5f85\u3061\u304f\u3060\u3055\u3044";
		FenceInstanceKeeper.Instance.SetActive (true);
		#if UNITY_IOS
		EtceteraBinding.showBezelActivityViewWithLabel (title);
		#endif

		#if UNITY_ANDROID
		string message = "\u554f\u984c\u3092\u53d6\u5f97\u3057\u3066\u3044\u307e\u3059";
		EtceteraAndroid.showProgressDialog(title,message);
		#endif
	}

	private void DismissProgressDialog(){
		FenceInstanceKeeper.Instance.SetActive (false);
		#if UNITY_IOS
		EtceteraBinding.hideActivityView ();
		#endif

		#if UNITY_ANDROID
		EtceteraAndroid.hideProgressDialog();
		#endif
	}
}
