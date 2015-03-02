using UnityEngine;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System;

public class QuizListDao {
	public const string ID_FIELD = "id";
	public const string TITLE_FIELD = "title";
	public const string QUIZ_URL_FIELD = "quiz_url";
	public const string CHALLENGE_QUIZ_DATA_FIELD = "challenge_quiz_data";
	public const string CHALLENGE_QUIZ_COUNT = "challenge_quiz_count";
	public const string CHALLENGE_QUIZ_CORRECT = "challenge_quiz_correct";
	public const string BOUGHT_DATE_FIELD = "bought_date";
	public const string QUIZ_ID_FIELD = "quiz_id";
	public const string ORDER_NUMBER_FIELD = "order_number";
	private static QuizListDao sInstance;

	public static QuizListDao instance {
		get {
			if (sInstance == null) {
				sInstance = new QuizListDao ();
			}
			return sInstance;
		}
	}

	public void UpdateTitle (Quiz quiz) {
		SQLiteDB sqliteDB = OpenDatabase ();
		StringBuilder sb = new StringBuilder ();
		sb.Append ("update quiz_list set ");
		sb.Append (TITLE_FIELD + " = '" + quiz.Title + "' ");
		sb.Append ("where " + ID_FIELD + " = " + quiz.Id + ";");
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sb.ToString ());
		sqliteQuery.Step ();
		sqliteDB.Close ();
	}

	public void UpdateRecord(Quiz quiz){
		SQLiteDB sqliteDB = OpenDatabase ();
		StringBuilder sb = new StringBuilder ();
		sb.Append ("update quiz_list set ");
		sb.Append (TITLE_FIELD + " = '" + quiz.Title + "', ");
		sb.Append (QUIZ_URL_FIELD + " = '" + quiz.QuizUrl + "', ");
		sb.Append (CHALLENGE_QUIZ_DATA_FIELD + " = '" + quiz.ChallengeQuizData + "', ");
		sb.Append (CHALLENGE_QUIZ_COUNT + " = " + quiz.ChallengeQuizCount + ", ");
		sb.Append (CHALLENGE_QUIZ_CORRECT + " = " + quiz.ChallengeQuizCount + ", ");
		sb.Append (BOUGHT_DATE_FIELD + " = '" + quiz.BoughtDate + "', ");
		sb.Append (QUIZ_ID_FIELD + " = " + quiz.QuizId+ ", ");
		sb.Append (ORDER_NUMBER_FIELD + " = " + quiz.OrderNumber + " ");
		sb.Append ("where " + ID_FIELD + " = " + quiz.Id + ";");
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sb.ToString ());
		sqliteQuery.Step ();
		sqliteDB.Close ();
	}

	public void UpdateOrderNumber (Quiz quiz) {
		SQLiteDB sqliteDB = OpenDatabase ();
		StringBuilder sb = new StringBuilder ();
		sb.Append ("update quiz_list set ");
		sb.Append (ORDER_NUMBER_FIELD + " = " + quiz.OrderNumber + " ");
		sb.Append ("where " + ID_FIELD + " = " + quiz.Id + ";");
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sb.ToString ());
		sqliteQuery.Step ();
		sqliteDB.Close ();
	}

	public List<Quiz> GetQuizList () {
		SQLiteDB sqliteDB = OpenDatabase ();
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, "select * from quiz_list;");
		List<Quiz> quizList = new List<Quiz> ();
		while (sqliteQuery.Step ()) {
			Quiz quiz = new Quiz ();
			//カラムがない場合アリ
			try {
				quiz.Id = sqliteQuery.GetInteger (ID_FIELD);
				quiz.Title = sqliteQuery.GetString (TITLE_FIELD);
				quiz.QuizUrl = sqliteQuery.GetString (QUIZ_URL_FIELD);
				quiz.BoughtDate = sqliteQuery.GetString (BOUGHT_DATE_FIELD);
				quiz.QuizId = sqliteQuery.GetInteger (QUIZ_ID_FIELD);
				quiz.ChallengeQuizCorrect = sqliteQuery.GetInteger (CHALLENGE_QUIZ_CORRECT);
				quiz.ChallengeQuizCount = sqliteQuery.GetInteger (CHALLENGE_QUIZ_COUNT);
				quiz.ChallengeQuizData = sqliteQuery.GetString (CHALLENGE_QUIZ_DATA_FIELD);
				quiz.OrderNumber = sqliteQuery.GetInteger (ORDER_NUMBER_FIELD);
			} catch (Exception e) {
				Debug.LogError (e.Message);
			} finally {
				quizList.Add (quiz);
			}
		}
		sqliteDB.Close ();
		return quizList;
	}

	public void Insert (Quiz quiz) {			
		SQLiteDB sqliteDB = OpenDatabase ();
		StringBuilder sb = new StringBuilder ();
		sb.Append ("insert into quiz_list values(");
		sb.Append ("null ,");
		sb.Append ("'" + quiz.Title + "',");
		sb.Append ("'" + quiz.QuizUrl + "',");
		sb.Append ("'" + quiz.ChallengeQuizData + "' ,");
		sb.Append (quiz.ChallengeQuizCount + " ,");
		sb.Append (quiz.ChallengeQuizCorrect + " ,");
		sb.Append ("'" + quiz.BoughtDate + "',");
		sb.Append (quiz.QuizId + " ,");
		sb.Append (quiz.OrderNumber);
		sb.Append (");");
		QuerySQL (sqliteDB, sb.ToString ());
	}

	public void UpdateChallengeData (string jsonString, int quizCount, int correctCount, int id) {
		Debug.Log ("Update");
		SQLiteDB sqliteDB = OpenDatabase ();
		StringBuilder sb = new StringBuilder ();
		sb.Append ("update quiz_list set ");
		sb.Append (CHALLENGE_QUIZ_DATA_FIELD + " = '" + jsonString + "', ");
		sb.Append (CHALLENGE_QUIZ_CORRECT + " = " + correctCount + ", ");
		sb.Append (CHALLENGE_QUIZ_COUNT + " = " + quizCount + " ");
		sb.Append ("where " + ID_FIELD + " = " + id);
		sb.Append (";");
		Debug.Log ("sql = " + sb.ToString ());
		QuerySQL (sqliteDB, sb.ToString ());
		Debug.Log ("finish");
	}

	public IDictionary GetChallengeDataById (int id) {
		SQLiteDB sqliteDB = OpenDatabase ();
		StringBuilder sb = new StringBuilder ();
		sb.Append ("select " + CHALLENGE_QUIZ_DATA_FIELD + ", " + CHALLENGE_QUIZ_COUNT + ", " + CHALLENGE_QUIZ_CORRECT + " ");
		sb.Append ("from quiz_list where " + ID_FIELD + " = " + id + ";");
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sb.ToString ());
		IDictionary challengeQuizDictionary = new Dictionary<object,object> ();
		try {
			while (sqliteQuery.Step ()) {
				string challengeData = sqliteQuery.GetString (CHALLENGE_QUIZ_DATA_FIELD);
				int challengeCount = sqliteQuery.GetInteger (CHALLENGE_QUIZ_COUNT);
				int correctCount = sqliteQuery.GetInteger (CHALLENGE_QUIZ_CORRECT);
				challengeQuizDictionary.Add (CHALLENGE_QUIZ_DATA_FIELD, challengeData);
				challengeQuizDictionary.Add (CHALLENGE_QUIZ_COUNT, challengeCount);
				challengeQuizDictionary.Add (CHALLENGE_QUIZ_CORRECT, correctCount);
			}
		} catch (Exception e) {
			Debug.Log (e.Message);
			sqliteDB.Close ();
			return null;
		}

		sqliteDB.Close ();
		return challengeQuizDictionary;
	}

	public void RemoveChallengeData (int id) {
		SQLiteDB sqliteDB = OpenDatabase ();
		StringBuilder sb = new StringBuilder ();
		sb.Append ("update quiz_list set " + CHALLENGE_QUIZ_DATA_FIELD + " = '' where id = " + id);
		QuerySQL (sqliteDB, sb.ToString ());
	}

	private SQLiteDB OpenDatabase () {
		SQLiteDB sqliteDB = new SQLiteDB ();
		string fileName = Application.persistentDataPath + "/quiz_box.db";
		sqliteDB.Open (fileName);
		return sqliteDB;
	}

	private void QuerySQL (SQLiteDB sqliteDB, string sql) {
		Debug.Log ("sql = " + sql);
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sql);
		sqliteQuery.Step ();
		sqliteDB.Close ();
	}
}
