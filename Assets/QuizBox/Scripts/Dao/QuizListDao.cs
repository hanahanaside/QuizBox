using UnityEngine;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class QuizListDao {

	public const string ID_FIELD = "id";
	public const string TITLE_FIELD = "title";
	public const string QUIZ_URL_FIELD = "quiz_url";
	public const string CHALLENGE_QUIZ_DATA_FIELD = "challenge_quiz_data";
	public const string CHALLENGE_QUIZ_COUNT = "challenge_quiz_count";
	public const string CHALLENGE_QUIZ_CORRECT = "challenge_quiz_correct";
	private static QuizListDao sInstance;

	public static QuizListDao instance {
		get {
			if (sInstance == null) {
				sInstance = new QuizListDao ();
			}
			return sInstance;
		}
	}
	
	public IList<IDictionary> GetQuizList () {
		SQLiteDB sqliteDB = new SQLiteDB ();
		string fileName = Application.persistentDataPath + "/quiz_box.db";
		sqliteDB.Open (fileName);
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, "select * from quiz_list;");
		IList<IDictionary> quizList = new List<IDictionary> ();
		while (sqliteQuery.Step()) {
			int id = sqliteQuery.GetInteger (ID_FIELD);
			string title = sqliteQuery.GetString (TITLE_FIELD);
			string quizUrl = sqliteQuery.GetString (QUIZ_URL_FIELD);
			IDictionary quiz = new Dictionary<object,object> ();
			quiz.Add (ID_FIELD, id);
			quiz.Add (TITLE_FIELD, title);
			quiz.Add (QUIZ_URL_FIELD, quizUrl);
			quizList.Add (quiz);
		}
		sqliteDB.Close ();
		return quizList;
	}

	public List<string> GetTitleList(){
		SQLiteDB sqliteDB = new SQLiteDB ();
		string fileName = Application.persistentDataPath + "/quiz_box.db";
		sqliteDB.Open (fileName);
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, "select title from quiz_list;");
		List<string> titleList = new List<string>();
		while (sqliteQuery.Step()) {
			string title = sqliteQuery.GetString (TITLE_FIELD);
			titleList.Add(title);
		}
		sqliteDB.Close ();
		return titleList;
	}

	public void Insert (string title, string quizUrl) {
		SQLiteDB sqliteDB = new SQLiteDB ();
		string fileName = Application.persistentDataPath + "/quiz_box.db";
		sqliteDB.Open (fileName);
		StringBuilder sb = new StringBuilder ();
		sb.Append ("insert into quiz_list values(");
		sb.Append ("null ,");
		sb.Append ("'" + title + "',");
		sb.Append ("'" + quizUrl + "',");
		sb.Append ("'null' ,");
		sb.Append ("0 ,");
		sb.Append ("0");
		sb.Append (");");
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sb.ToString ());
		sqliteQuery.Step ();
		sqliteDB.Close ();
	}

	public void UpdateChallengeData (string jsonString, int quizCount, int correctCount, int id) {
		Debug.Log ("Update");
		SQLiteDB sqliteDB = new SQLiteDB ();
		string fileName = Application.persistentDataPath + "/quiz_box.db";
		sqliteDB.Open (fileName);
		StringBuilder sb = new StringBuilder ();
		sb.Append ("update quiz_list set ");
		sb.Append (CHALLENGE_QUIZ_DATA_FIELD + " = '" + jsonString + "', ");
		sb.Append (CHALLENGE_QUIZ_CORRECT + " = " + correctCount + ", ");
		sb.Append (CHALLENGE_QUIZ_COUNT + " = " + quizCount + " ");
		sb.Append ("where " + ID_FIELD + " = " + id);
		sb.Append (";");
		Debug.Log ("sql = " + sb.ToString ());
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sb.ToString ());
		sqliteQuery.Step ();
		sqliteDB.Close ();
		Debug.Log ("finish");
	}

	public IDictionary GetChallengeData (int id) {
		SQLiteDB sqliteDB = new SQLiteDB ();
		string fileName = Application.persistentDataPath + "/quiz_box.db";
		sqliteDB.Open (fileName);
		StringBuilder sb = new StringBuilder ();
		sb.Append ("select " + CHALLENGE_QUIZ_DATA_FIELD + ", " + CHALLENGE_QUIZ_COUNT + ", " + CHALLENGE_QUIZ_CORRECT + " ");
		sb.Append ("from quiz_list where " + ID_FIELD + " = " + id + ";");
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sb.ToString ());
		IDictionary challengeQuizDictionary = new Dictionary<object,object> ();
		while (sqliteQuery.Step()) {
			string challengeData = sqliteQuery.GetString (CHALLENGE_QUIZ_DATA_FIELD);
			int challengeCount = sqliteQuery.GetInteger (CHALLENGE_QUIZ_COUNT);
			int correctCount = sqliteQuery.GetInteger (CHALLENGE_QUIZ_CORRECT);
			challengeQuizDictionary.Add (CHALLENGE_QUIZ_DATA_FIELD, challengeData);
			challengeQuizDictionary.Add (CHALLENGE_QUIZ_COUNT, challengeCount);
			challengeQuizDictionary.Add (CHALLENGE_QUIZ_CORRECT, correctCount);
		}
		sqliteDB.Close ();
		return challengeQuizDictionary;
	}

	public void RemoveChallengeData(int id){
		SQLiteDB sqliteDB = new SQLiteDB ();
		string fileName = Application.persistentDataPath + "/quiz_box.db";
		sqliteDB.Open (fileName);
		StringBuilder sb = new StringBuilder ();
		sb.Append("update quiz_list set "+CHALLENGE_QUIZ_DATA_FIELD +" = null where id = "+id);
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sb.ToString ());
		sqliteQuery.Step();
		sqliteDB.Close ();
	}
}
