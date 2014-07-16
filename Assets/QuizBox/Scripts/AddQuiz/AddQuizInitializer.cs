using UnityEngine;
using MiniJSON;
using System.Collections;

public class AddQuizInitializer : MonoBehaviour
{

	public HttpClient httpClient;
	public UIGrid grid;
	public GameObject addQuizButtonPrefab;
	private const string JSON_URL = "http://quiz.ryodb.us/list/selled_projects.json";

	void OnEnable ()
	{
		HttpClient.responseEvent += ResponseCallback;
	}
	
	void OnDisable ()
	{
		HttpClient.responseEvent -= ResponseCallback;
	}

	// Use this for initialization
	void Start ()
	{
		WWW www = new WWW (JSON_URL);
		StartCoroutine (httpClient.Excute (www));
	}

	void ResponseCallback (string response)
	{
		Debug.Log ("callBack" + response);
		if (response != null) {
			IList jsonArray = (IList)Json.Deserialize (response);
			CreateScrollView (jsonArray);
		}
	}

	private void CreateScrollView (IList jsonArray)
	{
		foreach (object item in jsonArray) {
			IDictionary jsonObject = (IDictionary)item;
			SetButtons (jsonObject);
		}
	}
	
	private void SetButtons (IDictionary jsonObject)
	{
		bool publish = (bool)jsonObject ["publish"];
		if (publish) {
			long point = (long)jsonObject ["point"];
			string url = jsonObject ["quiz_management_url"].ToString ();
			string title = jsonObject ["title"].ToString ();
			AddQuiz addQuiz = new AddQuiz ();
			addQuiz.point = (int)point;
			addQuiz.url = url;
			addQuiz.title = title;
			GameObject addQuizButtonObject = Instantiate (addQuizButtonPrefab)as GameObject;
			grid.AddChild(addQuizButtonObject.transform);
			addQuizButtonObject.transform.localScale = new Vector3(1,1,1);
			AddQuizButtonController controller = addQuizButtonObject.GetComponentInChildren<AddQuizButtonController>();
			controller.Init(addQuiz);
		}
	}

}
