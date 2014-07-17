using UnityEngine;
using MiniJSON;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
	public GameObject referee;
	public GameObject quizSetter;
	public UILabel[] buttonLabelArray;

	void OnEnable ()
	{
#if UNITY_IPHONE
		EtceteraManager.alertButtonClickedEvent += AlertButtonClickedEvent;
#endif
	}

	void OnDisable ()
	{
		#if UNITY_IPHONE
		EtceteraManager.alertButtonClickedEvent -= AlertButtonClickedEvent;
		#endif

	}

	void Update ()
	{
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel ("Title");
		}
	}
	
	public void OnAnswerButtonClick ()
	{
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
		referee.GetComponent<Referee> ().Judge (selectedText);

	}

	public void OnBackButtonClick ()
	{
		if (QuizListManager.instance.quizList.Count == QuizListManager.instance.allQuizListCount) {
			//check save
			CheckSaveQuizDialog.Show();
		} else {
			CheckFinishQuizDialog.Show();
		}
	}

	private void AlertButtonClickedEvent (string clickedButton)
	{
		Debug.Log(clickedButton);
		if(clickedButton == "\u7d42\u4e86\u3059\u308b"){
			Application.LoadLevel("Title");
		}
		if(clickedButton == "\u30bb\u30fc\u30d6\u3057\u3066\u7d42\u4e86"){
			//save
			Application.LoadLevel("Title");
		}
	}

}
