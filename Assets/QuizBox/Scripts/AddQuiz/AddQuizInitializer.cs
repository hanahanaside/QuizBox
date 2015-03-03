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
	private List<Quiz> mQuizList;
	private int mLoadingTextureIndex;
			
	void Start () {

	}

	void ResponseCallback (string response) {

		if (response == null) {
			NetworkErrorDialog dialog = new NetworkErrorDialog ();
			dialog.Show ();
		} else {
			IList addQuizButtonList = (IList)Json.Deserialize (response);
			CreateScrollView (addQuizButtonList);
		}
	}

	public void RemakeList () {
		List<Transform> childList = grid.GetChildList ();
		foreach (Transform child in childList) {
			Destroy (child.gameObject);
		}
		mQuizList = QuizListDao.instance.GetQuizList ();
		WWW www = new WWW (JSON_URL);
		StartCoroutine (httpClient.Excute (www));
	}

	private void CreateScrollView (IList jsonArray) {
		int maxId = GetMaxId (jsonArray);
		Debug.Log ("max id " + maxId);
		for (int i = 0; i < jsonArray.Count; i++) {
			object item = jsonArray [i];
			IDictionary jsonObject = (IDictionary)item;
			SetButtons (jsonObject, maxId);
		}
		scrollView.ResetPosition ();
	}

	private int GetMaxId (IList jsonArray) {
		int maxId = 0;
		foreach (object item in jsonArray) {
			IDictionary jsonObject = (IDictionary)item;
			bool publish = (bool)jsonObject ["publish"];
			if (!publish) {
				continue;
			}
			long quizId = (long)jsonObject ["id"];
			if (quizId > maxId) {
				maxId = (int)quizId;
			}
		}
		return maxId;
	}

	private void SetButtons (IDictionary jsonObject, int maxId) {
		bool publish = (bool)jsonObject ["publish"];
		string title = jsonObject ["title"].ToString ();
		if (!publish) {
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
		if (quizId > maxId - 5) {
			addQuiz.FlagNew = true;
		}

		GameObject addQuizButtonObject = Instantiate (addQuizButtonPrefab)as GameObject;
		grid.AddChild (addQuizButtonObject.transform);
		addQuizButtonObject.transform.localScale = new Vector3 (1, 1, 1);
		AddQuizButtonController controller = addQuizButtonObject.GetComponentInChildren<AddQuizButtonController> ();
//		controller.Init (addQuiz);
	}
}
