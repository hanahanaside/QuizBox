using UnityEngine;
using MiniJSON;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	public GameObject quizSetter;
	public QuizKeeper quizKeeper;
	public ScoreKeeper scoreKeeper;
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
		titleLabel.text = SelectedQuiz.instance.name;
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
		if (clickedButton == "\u7d42\u4e86\u3059\u308b") {
			Application.LoadLevel ("Title");
		}
		if (clickedButton == "\u30bb\u30fc\u30d6\u3057\u3066\u7d42\u4e86") {
			//save
			Debug.Log ("save");
			string jsonString = QuizListManager.instance.jsonString;
			int id = SelectedQuiz.instance.id;
			int quizCount = quizKeeper.questionNumber;
			int correctCount = scoreKeeper.score;
			QuizListDao.instance.UpdateChallengeData (jsonString, quizCount, correctCount, id);
			Application.LoadLevel ("Title");
		}
		#if UNITY_ANDROID
		mBackClicked = false;
		#endif
	}

}
