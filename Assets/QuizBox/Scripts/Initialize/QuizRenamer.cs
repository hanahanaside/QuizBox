using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using MiniJSON;

public class QuizRenamer : MonoBehaviour {

	private const string JSON_URL = "http://quiz.ryodb.us/list/selled_projects.json";

	public static event Action renamedQuizEvent;

	public void RenameQuiz () {
		Debug.Log ("Rename Quiz");
		WWWClient wwwClient = new WWWClient (this, JSON_URL);
		wwwClient.OnSuccess = (WWW www) => {
			RenameQuiz (www.text);
			renamedQuizEvent ();
		};
		wwwClient.OnFail = (WWW www) => {
			renamedQuizEvent ();
		};
		wwwClient.OnTimeOut = () => {
			renamedQuizEvent ();
		};
		wwwClient.GetData ();
	}

	private void RenameQuiz (string json) {
		IList jsonArray = (IList)Json.Deserialize (json);
		List<Quiz> quizList = QuizListDao.instance.GetQuizList ();
		foreach (Quiz quiz in quizList) {
			RenameQuiz (jsonArray, quiz);
		}
	}

	private void RenameQuiz (IList jsonArray, Quiz quiz) {
		Debug.Log ("Rename Quiz");
		int quizId = quiz.QuizId;
		foreach (Dictionary<string,object> jsonObject in jsonArray) {
			long jsonId = (long)jsonObject ["id"];
			if (quizId == jsonId) {
				Debug.Log ("Renamed " + quiz.Title + " to " + jsonObject[QuizListDao.TITLE_FIELD]);
				quiz.Title = (string)jsonObject [QuizListDao.TITLE_FIELD];
				QuizListDao.instance.UpdateTitle (quiz);
				break;
			}
		}
	}
}
