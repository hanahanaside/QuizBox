﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ResultInitializer : MonoBehaviour {

	public UILabel resultLabel;

	// Use this for initialization
	void Start () {
		int score = ScoreKeeper.instance.score;
		int size = QuizListManager.instance.quizList.Count;
		string result = size + "問中" + score + "問正解!!";
		resultLabel.text = result;
		HistoryData historyData = new HistoryData ();
		historyData.result = result;
		historyData.date = DateTime.Now.ToString ();
		historyData.title = SelectedQuiz.instance.name;
		historyData.mode = QuizListManager.instance.modeName;
	
		HistoryDataDao.instance.InsertHistoryData (historyData);

		System.Text.StringBuilder sb = new System.Text.StringBuilder ();
		sb.Append ("result = " + result + "\n");
		sb.Append ("date = " + DateTime.Now.ToString ());
		sb.Append ("title = " + SelectedQuiz.instance.name + "\n");
		sb.Append ("mode = " + QuizListManager.instance.modeName);
		resultLabel.text = sb.ToString();
	}
	
}
