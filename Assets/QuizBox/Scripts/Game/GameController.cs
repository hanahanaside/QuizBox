using UnityEngine;
using MiniJSON;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
	public GameObject referee;
	public GameObject quizSetter;
	public UILabel[] buttonLabelArray;
	
	public void OnButtonClick ()
	{
		string buttonName = UIButton.current.name;
		string selectedText = "";
		if (buttonName == "Button1") {
			selectedText = buttonLabelArray[0].text;
		}
		if (buttonName == "Button2") {
			selectedText = buttonLabelArray[1].text;
		}
		if (buttonName == "Button3") {
			selectedText = buttonLabelArray[2].text;
		}

		quizSetter.GetComponent<QuizSetter>().StopTyping();
		referee.GetComponent<Referee> ().Judge (selectedText);

	}


}
