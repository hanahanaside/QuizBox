using UnityEngine;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class QuizListDao
{

	public const string ID_FIELD = "id";
	public const string TITLE_FIELD = "title";
	public const string QUIZ_URL_FIELD = "quiz_url";
	private static QuizListDao sInstance;

	public static QuizListDao instance {
		get {
			if (sInstance == null) {
				sInstance = new QuizListDao();
			}
			return sInstance;
		}
	}
	
	public IList<IDictionary> GetQuizList ()
	{
		SQLiteDB sqliteDB = new SQLiteDB ();
		string fileName = Application.persistentDataPath + "/quiz_box.db";
		sqliteDB.Open (fileName);
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, "select * from quiz_list;");
		IList<IDictionary> quizList = new List<IDictionary>();
		while (sqliteQuery.Step()) {
			int id = sqliteQuery.GetInteger (ID_FIELD);
			string title = sqliteQuery.GetString (TITLE_FIELD);
			string quizUrl = sqliteQuery.GetString(QUIZ_URL_FIELD);
			IDictionary quiz = new Dictionary<object,object>();
			quiz.Add (ID_FIELD, id);
			quiz.Add (TITLE_FIELD, title);
			quiz.Add(QUIZ_URL_FIELD,quizUrl);
			quizList.Add(quiz);
		}
		sqliteDB.Close ();
		return quizList;
	}

	public void Insert(string title,string quizUrl){
		SQLiteDB sqliteDB = new SQLiteDB ();
		string fileName = Application.persistentDataPath + "/quiz_box.db";
		sqliteDB.Open (fileName);
		StringBuilder sb = new StringBuilder();
		sb.Append("insert into quiz_list values(");
		sb.Append("null ,");
		sb.Append("'"+title+"',");
		sb.Append("'"+quizUrl+"'");
		sb.Append(");");
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sb.ToString());
		sqliteQuery.Step();
		sqliteDB.Close ();
	}

}
