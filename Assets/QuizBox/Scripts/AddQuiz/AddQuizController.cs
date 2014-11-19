using UnityEngine;
using MiniJSON;
using System.Collections;
using System;

public class AddQuizController : MonoBehaviour {

	public AddQuizDialog addQuizDialogPrefab;
	public ShortPointDialog shortPointDialogPrefab;
	public OkDialog okDialogPrefab;
	public AddQuizInitializer addQuizInitializer;
	private AddQuiz mSelectedQuiz;

	void OnEnable () {
		AddQuizButtonController.clickedEvent += OnClickAddQuiz;

#if UNITY_IPHONE
		EtceteraManager.alertButtonClickedEvent += alertButtonClickedEvent;
#endif

#if UNITY_ANDROID
		EtceteraAndroidManager.alertButtonClickedEvent += alertButtonClickedEvent;
#endif
	}
	
	void OnDisable () {
		AddQuizButtonController.clickedEvent -= OnClickAddQuiz;

		#if UNITY_IPHONE
		EtceteraManager.alertButtonClickedEvent -= alertButtonClickedEvent;
		#endif

#if UNITY_ANDROID
		EtceteraAndroidManager.alertButtonClickedEvent -= alertButtonClickedEvent;
#endif
	}

	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel ("Top");
		}
	}

	public void OnBackButtonClick () {
		Application.LoadLevel ("Top");
	}

	void OnClickAddQuiz (AddQuizButtonController addQuizButtonController) {
		mSelectedQuiz = addQuizButtonController.SelectedQuiz;
		#if UNITY_EDITOR
		alertButtonClickedEvent("はい");
		#else
		int needPoint = mSelectedQuiz.point;
		int userPoint = PrefsManager.Instance.GetUserPoint ();
		if (userPoint < needPoint) {
		ShortPointDialog shortPointDialog = Instantiate (shortPointDialogPrefab) as ShortPointDialog;
		shortPointDialog.Show ();
		}else{
		AddQuizDialog addQuizDialog = Instantiate (addQuizDialogPrefab)as AddQuizDialog;
		addQuizDialog.Show (mSelectedQuiz, userPoint);
		}
		#endif
	}

	void alertButtonClickedEvent (string clickedButton) {
		Debug.Log ("alertButtonClickedEvent: " + clickedButton);
		if (clickedButton == "はい") {
			//insert quiz
			Quiz quiz = new Quiz ();
			quiz.Title = mSelectedQuiz.title;
			quiz.QuizUrl = mSelectedQuiz.url;
			quiz.QuizId = mSelectedQuiz.QuizId;
			quiz.BoughtDate = DateTime.Now.ToString ();
			QuizListDao.instance.Insert (quiz);
			int userPoint = PrefsManager.Instance.GetUserPoint ();
			userPoint -= mSelectedQuiz.point;
			PrefsManager.Instance.SaveUserPoint (userPoint);
			TopController.Instance.UpdateUserPointLabel ();
			string title = "\u8ffd\u52a0\u5b8c\u4e86";
			string message = mSelectedQuiz.title + "\u3092\u8ffd\u52a0\u3057\u307e\u3057\u305f";
			OkDialog okDialog = Instantiate (okDialogPrefab)as OkDialog;
			okDialog.Show (title, message); 
			addQuizInitializer.RemakeList ();
		}
	}
}
