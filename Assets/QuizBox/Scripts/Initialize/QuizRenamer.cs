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
		wwwClient.OnSuccess = (string response) => {
			RenameQuiz (response);
			renamedQuizEvent ();
		};
		wwwClient.OnFail = (string response) => {
			renamedQuizEvent ();
		};
		wwwClient.OnTimeOut = () => {
			renamedQuizEvent ();
		};
		wwwClient.GetData ();
	}

	private void RenameQuiz (string json) {
		IList jsonArray = (IList)Json.Deserialize (json);
		IList<IDictionary> quizList = QuizListDao.instance.GetQuizList ();
		foreach (IDictionary quiz in quizList) {
			RenameQuiz (jsonArray, quiz);
		}
	}

	private void RenameQuiz (IList jsonArray, IDictionary quiz) {
		Debug.Log ("Rename Quiz");
		int quizId = (int)quiz [QuizListDao.QUIZ_ID_FIELD];
		foreach (Dictionary<string,object> jsonObject in jsonArray) {
			long jsonId = (long)jsonObject ["id"];
			if (quizId == jsonId) {
				Debug.Log ("Renamed " + quiz [QuizListDao.TITLE_FIELD] + " to " + jsonObject[QuizListDao.TITLE_FIELD]);
				quiz [QuizListDao.TITLE_FIELD] = (string)jsonObject [QuizListDao.TITLE_FIELD];
				QuizListDao.instance.UpdateQuiz (quiz);
				break;
			}
		}
	}
}
