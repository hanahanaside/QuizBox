using UnityEngine;
using System;
using System.Collections;

public class AddQuizButtonController : MonoBehaviour {

	public static event Action<AddQuiz> clickedEvent;

	public UILabel titleLabel;
	public UILabel pointLabel;

	private AddQuiz mAddQuiz;

	public void Init(AddQuiz addQuiz){
		mAddQuiz = addQuiz;
		titleLabel.text = mAddQuiz.title;
		pointLabel.text = mAddQuiz.point + "pt";
	}

	public void OnButtonClick(){
		if(clickedEvent != null){
			clickedEvent(mAddQuiz);
		}
	}
}
