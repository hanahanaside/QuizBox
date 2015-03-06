using UnityEngine;
using System.Collections;
using System;

public class AddQuizContainer : MonoSingleton<AddQuizContainer> , IContainer{

	private AddQuizScrollView mAddQuizScrollView;
	private SelledProject mSelectedQuiz;

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

	void OnClickAddQuiz (SelledProject selledProject) {
		mSelectedQuiz = selledProject;
		Debug.Log ("click");
		int needPoint = mSelectedQuiz.point;
		int userPoint = PrefsManager.Instance.GetUserPoint ();
		if (userPoint < needPoint) {
			ShortPointDialog.Show ();
		} else {
			AddQuizDialog.Show (mSelectedQuiz, userPoint);
		}
		#if UNITY_EDITOR
		alertButtonClickedEvent("はい");
		#endif
	}

	void alertButtonClickedEvent (string clickedButton) {
		Debug.Log ("alertButtonClickedEvent: " + clickedButton);
		if (clickedButton == "はい") {
			//insert quiz
			Quiz quiz = new Quiz ();
			quiz.Title = mSelectedQuiz.title;
			quiz.QuizUrl = mSelectedQuiz.quiz_management_url;
			quiz.QuizId = mSelectedQuiz.id;
			quiz.BoughtDate = DateTime.Now.ToString ("yyyy/MM/dd");
			QuizListDao.instance.Insert (quiz);
			int userPoint = PrefsManager.Instance.GetUserPoint ();
			userPoint -= mSelectedQuiz.point;
			PrefsManager.Instance.SaveUserPoint (userPoint);
			TopController.instance.UpdateUserPointLabel ();
			QuizTopicDialogManager.instance.AddChild (quiz);
			string title = "\u8ffd\u52a0\u5b8c\u4e86";
			string message = mSelectedQuiz.title + "\u3092\u8ffd\u52a0\u3057\u307e\u3057\u305f";
			OkDialog.Show (title, message); 
			SelledProjectsManager.instance.Remove (mSelectedQuiz);
			mAddQuizScrollView.RemoveButton (mSelectedQuiz);
		}
	}


	public override void OnInitialize () {
		mAddQuizScrollView = GetComponentInChildren<AddQuizScrollView> ();
	}

	public void Show () {
		foreach (Transform child in transform) {
			child.gameObject.SetActive (true);
		}
		mAddQuizScrollView.ShowPopularGrid ();
	}

	public void Hide () {
		foreach (Transform child in transform) {
			child.gameObject.SetActive (false);
		}
	}

	public void PopularButtonClicked(){
		mAddQuizScrollView.ShowPopularGrid ();
	}

	public void BoysComicButtonClicked(){
		mAddQuizScrollView.ShowBoysComicGrid ();
	}

	public void EtcComicButtonClicked(){
		mAddQuizScrollView.ShowGirlsComicGrid ();
	}

	public void PraticalButtonClicked(){
		mAddQuizScrollView.ShowPraticalGrid ();
	}

	public void IdolButtonClicked(){
		mAddQuizScrollView.ShowIdolGrid ();
	}
}
