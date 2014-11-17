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
	public const string ORDER_NUMBER = "order_number";
	private static QuizListDao sInstance;

	public static QuizListDao instance {
		get {
			if (sInstance == null) {
				sInstance = new QuizListDao ();
			}
			return sInstance;
		}
	}

	public void UpdateQuiz (IDictionary quiz) {
		SQLiteDB sqliteDB = OpenDatabase ();
		StringBuilder sb = new StringBuilder ();
		sb.Append ("update quiz_list set ");
		sb.Append (TITLE_FIELD + " = '" + quiz [TITLE_FIELD] + "',");
		sb.Append (QUIZ_URL_FIELD + " = '" + quiz [QUIZ_URL_FIELD] + "',");
		sb.Append (BOUGHT_DATE_FIELD + " = '" + quiz [BOUGHT_DATE_FIELD] + "', ");
		sb.Append (QUIZ_ID_FIELD + " = " + quiz [QUIZ_ID_FIELD] + " ");
	//	sb.Append (ORDER_NUMBER + " = " + quiz [ORDER_NUMBER] + " ");
		sb.Append ("where " + ID_FIELD + " = " + quiz [ID_FIELD] + ";");
		Debug.Log ("sql = " + sb.ToString());
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sb.ToString ());
		sqliteQuery.Step ();
		sqliteDB.Close ();
	}

	public List<Quiz> GetQuizList () {
		SQLiteDB sqliteDB = OpenDatabase ();
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, "select * from quiz_list;");
		List<IDictionary> quizList = new List<IDictionary> ();
		while (sqliteQuery.Step ()) {
			Quiz quiz = new Quiz ();
			quiz.Id = sqliteQuery.GetInteger (ID_FIELD);
			quiz.Title = sqliteQuery.GetString (TITLE_FIELD);
			quiz.QuizUrl = sqliteQuery.GetString (QUIZ_URL_FIELD);
			quiz.BoughtDate = sqliteQuery.GetString (BOUGHT_DATE_FIELD);
			quiz.QuizId = sqliteQuery.GetInteger (QUIZ_ID_FIELD);
			quizList.Add (quiz);
		}
		sqliteDB.Close ();
		return quizList;
	}

	public List<string> GetTitleList () {
		SQLiteDB sqliteDB = OpenDatabase ();
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, "select title from quiz_list;");
		List<string> titleList = new List<string> ();
		while (sqliteQuery.Step ()) {
			string title = sqliteQuery.GetString (TITLE_FIELD);
			titleList.Add (title);
		}
		sqliteDB.Close ();
		return titleList;
	}

	public void Insert (string title, string quizUrl, int quizId) {
		// order numberを最後にする
		List<IDictionary> quizList = QuizListDao.instance.GetQuizList ();
		int orderNumber = quizList.Count + 1;

		SQLiteDB sqliteDB = OpenDatabase ();
		StringBuilder sb = new StringBuilder ();
		sb.Append ("insert into quiz_list values(");
		sb.Append ("null ,");//id
		sb.Append ("'" + title + "',");//title
		sb.Append ("'" + quizUrl + "',");//quiz url
		sb.Append ("'null' ,");
		sb.Append ("0 ,");
		sb.Append ("0 ,");
		sb.Append ("'" + DateTime.Now.ToString ("yyyy/MM/dd") + "',");
		sb.Append (quizId + ",");
		sb.Append ("" + orderNumber);//order number
		sb.Append (");");
		QuerySQL (sqliteDB, sb.ToString ());
	}

	public void Insert(Quiz quiz){
		SQLiteDB sqliteDB = OpenDatabase ();
		StringBuilder sb = new StringBuilder ();
		sb.Append ("insert into quiz_list values(");
		sb.Append ("null ,");//id
		sb.Append ("'" + quiz.Title + "',");//title
		sb.Append ("'" + quiz.QuizUrl + "',");//quiz url

	
	//		sb.Append ("'" + quiz[QuizListDao.CHALLENGE_QUIZ_DATA_FIELD] + "'" + " ,");//challenge data
		sb.Append ("'a'" + " ,");//challenge data
	//	sb.Append (quiz[QuizListDao.CHALLENGE_QUIZ_COUNT] + " ,");//challenge count
	//	sb.Append (quiz[QuizListDao.CHALLENGE_QUIZ_CORRECT] + " ,");//challenge correct

		sb.Append ( quiz.ChallengeQuizCount + " ,");//challenge count
		sb.Append ( quiz.ChallengeQuizCorrect+ " ,");//challenge correct
		sb.Append ("'" + quiz.BoughtDate + "',");//bought date
		sb.Append (quiz.QuizId);//quiz id
	//	sb.Append ("" + quiz[QuizListDao.ORDER_NUMBER]);//order number
		sb.Append (");");
//		sb.Append ("insert into quiz_list values(");
//		sb.Append ("null ,");//id
//		sb.Append ("'" + "title" + "',");//title
//		sb.Append ("'" + "quizUrl" + "',");//quiz url
//		sb.Append ("'null' ,");
//		sb.Append ("0 ,");
//		sb.Append ("0 ,");
//		sb.Append ("'" + DateTime.Now.ToString ("yyyy/MM/dd") + "',");
//		sb.Append (0 + ",");
//		sb.Append ("" + 0);//order number
//		sb.Append (");");

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

	public void AddQuizIdField () {
		SQLiteDB sqliteDB = OpenDatabase ();
		string sql = "alter table quiz_list add column quiz_id integer default 0;";
		QuerySQL (sqliteDB, sql);
	}

	public void AddOrderNumberField () {
		SQLiteDB sqliteDB = OpenDatabase ();
		string sql = "alter table quiz_list add column order_number integer default 0;";
		QuerySQL (sqliteDB, sql);
	}

	public IDictionary GetChallengeData (int id) {
		SQLiteDB sqliteDB = OpenDatabase ();
		StringBuilder sb = new StringBuilder ();
		sb.Append ("select " + CHALLENGE_QUIZ_DATA_FIELD + ", " + CHALLENGE_QUIZ_COUNT + ", " + CHALLENGE_QUIZ_CORRECT + " ");
		sb.Append ("from quiz_list where " + ID_FIELD + " = " + id + ";");
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sb.ToString ());
		IDictionary challengeQuizDictionary = new Dictionary<object,object> ();
		while (sqliteQuery.Step ()) {
			string challengeData = sqliteQuery.GetString (CHALLENGE_QUIZ_DATA_FIELD);
			Debug.Log ("challenge data = " + challengeData);
			int challengeCount = sqliteQuery.GetInteger (CHALLENGE_QUIZ_COUNT);
			int correctCount = sqliteQuery.GetInteger (CHALLENGE_QUIZ_CORRECT);
			challengeQuizDictionary.Add (CHALLENGE_QUIZ_DATA_FIELD, challengeData);
			challengeQuizDictionary.Add (CHALLENGE_QUIZ_COUNT, challengeCount);
			challengeQuizDictionary.Add (CHALLENGE_QUIZ_CORRECT, correctCount);
		}
		sqliteDB.Close ();
		return challengeQuizDictionary;
	}

	public void RemoveChallengeData (int id) {
		SQLiteDB sqliteDB = OpenDatabase ();
		StringBuilder sb = new StringBuilder ();
		sb.Append ("update quiz_list set " + CHALLENGE_QUIZ_DATA_FIELD + " = null where id = " + id);
		QuerySQL (sqliteDB, sb.ToString ());
	}

	public void InitBoughtDate () {
		SQLiteDB sqliteDB = OpenDatabase ();
		StringBuilder sb = new StringBuilder ();
		sb.Append ("update quiz_list set " + BOUGHT_DATE_FIELD + " = '" + DateTime.Now.ToString ("yyyy/MM/dd") + "'");
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
