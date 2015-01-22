using UnityEngine;
using System;
using System.Collections;

public class AddQuizButtonController : MonoBehaviour {

	public static event Action<AddQuizButtonController> clickedEvent;

	public GameObject newSprite;
	public UILabel titleLabel;
	public UILabel pointLabel;
	private AddQuiz mAddQuiz;

	public void Init (AddQuiz addQuiz) {
		mAddQuiz = addQuiz;
		titleLabel.text = mAddQuiz.title + "\n(" + mAddQuiz.quizCount + "\u554f)";
		pointLabel.text = mAddQuiz.point + "pt";
		if(mAddQuiz.FlagNew){
			//show new label
			newSprite.SetActive (true);
		}
	}

	public AddQuiz SelectedQuiz{
		get{
			return mAddQuiz;
		}
	}

	public void OnButtonClick () {
		if (clickedEvent != null) {
			clickedEvent (this); 
		}
	}
}
