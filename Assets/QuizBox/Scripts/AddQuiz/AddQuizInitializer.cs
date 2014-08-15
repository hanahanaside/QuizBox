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
	private static IList sAddQuizButtonList = null;
	private List<string> mTitleList;

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
		mTitleList = QuizListDao.instance.GetTitleList ();
		if (sAddQuizButtonList != null) {
			CreateScrollView(sAddQuizButtonList);
			return;
		}
		string title = "\u304a\u5f85\u3061\u304f\u3060\u3055\u3044";
		FenceInstanceKeeper.Instance.SetActive (true);
		#if UNITY_IOS
		EtceteraBinding.showBezelActivityViewWithLabel(title);
		#endif
		
		#if UNITY_ANDROID
		string message = "\u554f\u984c\u3092\u53d6\u5f97\u3057\u3066\u3044\u307e\u3059";
		EtceteraAndroid.showProgressDialog(title,message);
		#endif

		WWW www = new WWW (JSON_URL);
		StartCoroutine (httpClient.Excute (www));
	}

	void ResponseCallback (string response) {
		FenceInstanceKeeper.Instance.SetActive (false);
		#if UNITY_IOS
		EtceteraBinding.hideActivityView();
		#endif
		
		#if UNITY_ANDROID
		EtceteraAndroid.hideProgressDialog();
		#endif

		if (response == null) {
			//error
			string title = "\u901a\u4fe1\u306b\u5931\u6557\u3057\u307e\u3057\u305f";
			string message = "\u30af\u30a4\u30ba\u3092\u53d6\u5f97\u3067\u304d\u307e\u305b\u3093\u3067\u3057\u305f";
			OkDialog okDialog = Instantiate (okDialogPrefab)as OkDialog;
			okDialog.Show (title, message);
		} else {
			sAddQuizButtonList = (IList)Json.Deserialize (response);
			CreateScrollView (sAddQuizButtonList);
		}
	}

	private void CreateScrollView (IList jsonArray) {
		foreach (object item in jsonArray) {
			IDictionary jsonObject = (IDictionary)item;
			SetButtons (jsonObject);
		}
		scrollView.ResetPosition ();
	}
	
	private void SetButtons (IDictionary jsonObject) {
		bool publish = (bool)jsonObject ["publish"];
		string title = jsonObject ["title"].ToString ();
		if (!publish) {
			return;
		}
		if (!CheckNotDuplicateTitle (title)) {
			return;
		}
		long point = (long)jsonObject ["point"];
		string url = jsonObject ["quiz_management_url"].ToString ();
		long quizCount = (long)jsonObject["quiz_count"];

		AddQuiz addQuiz = new AddQuiz ();
		addQuiz.point = (int)point;
		addQuiz.url = url;
		addQuiz.title = title;
		addQuiz.quizCount = (int)quizCount;
		GameObject addQuizButtonObject = Instantiate (addQuizButtonPrefab)as GameObject;
		grid.AddChild (addQuizButtonObject.transform);
		addQuizButtonObject.transform.localScale = new Vector3 (1, 1, 1);
		addQuizButtonObject.BroadcastMessage ("Init", addQuiz);
	}

	private bool CheckNotDuplicateTitle (string text) {
		foreach (string title in mTitleList) {
			if (title == text) {
				return false;
			}
		}
		return true;
	}
}
