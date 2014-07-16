﻿using UnityEngine;
using System.Collections;

public class CellTop : MonoBehaviour
{

	private int mId;
	private string mName;
	private string mQuizUrl;

	public void OnClick(){
		SelectedQuiz.instance.id = mId;
		SelectedQuiz.instance.name = mName;
		SelectedQuiz.instance.quizUrl = mQuizUrl;
		Application.LoadLevel("Title");
	}

	public int id {
		get {
			return mId;
		}
		set{
			mId = value;
		}
	}

	public string name {
		get {
			return mName;
		}
		set {
			mName = value;
			gameObject.GetComponentInChildren<UILabel>().text = mName;
		}
	}

	public string quizUrl{
		get{
			return mQuizUrl;
		}
		set{
			mQuizUrl = value;
		}
	}
}
