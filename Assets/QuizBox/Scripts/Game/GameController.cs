using UnityEngine;
using MiniJSON;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	public GameObject quizSetter;
	public QuizKeeper quizKeeper;
	public Referee referee;
	public UILabel titleLabel;
	public UILabel[] buttonLabelArray;

	#if UNITY_ANDROID
	private bool mBackClicked;
	#endif


	void OnEnable () {
#if UNITY_IPHONE
		EtceteraManager.alertButtonClickedEvent += AlertButtonClickedEvent;
#endif

#if UNITY_ANDROID
		EtceteraAndroidManager.alertButtonClickedEvent += AlertButtonClickedEvent;
#endif
	}

	void OnDisable () {
		#if UNITY_IPHONE
		EtceteraManager.alertButtonClickedEvent -= AlertButtonClickedEvent;
		#endif

		#if UNITY_ANDROID
		EtceteraAndroidManager.alertButtonClickedEvent -= AlertButtonClickedEvent;
		#endif

	}

	void Start () {
		titleLabel.text = SelectedQuiz.instance.Name;
	}

	void Update () {
		#if UNITY_ANDROID
		if (Input.GetKey (KeyCode.Escape) && !mBackClicked) {
			mBackClicked = true;
			OnBackButtonClick ();
			return;
		}
		#endif
	}
	
	public void OnAnswerButtonClick () {
		string buttonName = UIButton.current.name;
		string selectedText = "";
		if (buttonName == "Button1") {
			selectedText = buttonLabelArray [0].text;
		}
		if (buttonName == "Button2") {
			selectedText = buttonLabelArray [1].text;
		}
		if (buttonName == "Button3") {
			selectedText = buttonLabelArray [2].text;
		}

		quizSetter.GetComponent<QuizSetter> ().StopTyping ();
		referee.Judge (selectedText);

	}

	public void OnBackButtonClick () {
		#if UNITY_EDITOR
		Application.LoadLevel ("Title");
		#else
		if (QuizListManager.instance.quizList.Count == QuizListManager.instance.allQuizListCount) {
			//check save
			CheckSaveQuizDialog.Show ();
		} else {
			CheckFinishQuizDialog.Show ();
		}
		#endif
	}

	private void AlertButtonClickedEvent (string clickedButton) {
		Debug.Log (clickedButton);
		if (clickedButton == "終了する") {
			Application.LoadLevel ("Title");
		}
		if (clickedButton == "セーブして終了") {
			//save
			Debug.Log ("save");
			string jsonString = QuizListManager.instance.jsonString;
			int id = SelectedQuiz.instance.id;
			int quizCount = quizKeeper.questionNumber;
			int correctCount = ScoreKeeper.instance.score;
			QuizListDao.instance.UpdateChallengeData (jsonString, quizCount, correctCount, id);
			Application.LoadLevel ("Title");
		}
		#if UNITY_ANDROID
		mBackClicked = false;
		#endif
	}

}
