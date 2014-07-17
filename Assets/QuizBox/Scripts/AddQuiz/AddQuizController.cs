using UnityEngine;
using MiniJSON;
using System.Collections;

public class AddQuizController : MonoBehaviour
{

	public AddQuizDialog addQuizDialogPrefab;
	public ShortPointDialog shortPointDialogPrefab;
	public OkDialog okDialogPrefab;
	public GameObject addPointDialogPrefab;
	public GameObject uiRoot;
	private AddQuiz mSelectedQuiz;
	private int mUserPoint;

	void OnEnable ()
	{
		AddQuizButtonController.clickedEvent += OnClickAddQuiz;

#if UNITY_IPHONE
		EtceteraManager.alertButtonClickedEvent += alertButtonClickedEvent;
#endif

#if UNITY_ANDROID
		EtceteraAndroidManager.alertButtonClickedEvent += alertButtonClickedEvent;
#endif
	}
	
	void OnDisable ()
	{
		AddQuizButtonController.clickedEvent -= OnClickAddQuiz;

		#if UNITY_IPHONE
		EtceteraManager.alertButtonClickedEvent -= alertButtonClickedEvent;
		#endif

#if UNITY_ANDROID
		EtceteraAndroidManager.alertButtonClickedEvent -= alertButtonClickedEvent;
#endif
	}

	void Start ()
	{
		mUserPoint = PrefsManager.instance.GetUserPoint ();
	}

	void Update ()
	{
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel ("Top");
		}
	}

	public void OnBackButtonClick(){
		Application.LoadLevel("Top");
	}

	void OnClickAddQuiz (AddQuiz addQuiz)
	{
		int needPoint = addQuiz.point;
		if (mUserPoint < needPoint) {
			ShortPointDialog shortPointDialog = Instantiate (shortPointDialogPrefab) as ShortPointDialog;
			shortPointDialog.Show ();
		} else {
			mSelectedQuiz = addQuiz;
			AddQuizDialog addQuizDialog = Instantiate (addQuizDialogPrefab)as AddQuizDialog;
			addQuizDialog.Show (addQuiz, mUserPoint);
		}
	}

	void alertButtonClickedEvent (string clickedButton)
	{
		Debug.Log ("alertButtonClickedEvent: " + clickedButton);
		if (clickedButton == "\u306f\u3044") {
			//add quiz
			QuizListDao.instance.Insert (mSelectedQuiz.title, mSelectedQuiz.url);
			mUserPoint -= mSelectedQuiz.point;
			PrefsManager.instance.SaveUserPoint (mUserPoint);
			string title = "\u8ffd\u52a0\u5b8c\u4e86";
			string message = mSelectedQuiz.title + "\u3092\u8ffd\u52a0\u3057\u307e\u3057\u305f";
			OkDialog okDialog = Instantiate (okDialogPrefab)as OkDialog;
			okDialog.Show (title, message);
		}
		if (clickedButton == "\u8cfc\u5165\u3059\u308b") {
			Debug.Log ("create");
			//buy point
			GameObject addPointDialog = Instantiate (addPointDialogPrefab) as GameObject;
			addPointDialog.transform.parent = uiRoot.transform;
			addPointDialog.transform.localScale = new Vector3 (1, 1, 1);
		}
	}
}
