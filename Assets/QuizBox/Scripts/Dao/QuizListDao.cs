using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuizListDao : MonoBehaviour
{

	public const string ID_FIELD = "id";
	public const string NAME_FIELD = "name";
	private static QuizListDao sInstance;

	public static QuizListDao instance {
		get {
			return sInstance;
		}
	}

	void Awake ()
	{
		if (sInstance == null) {
			sInstance = this;
			DontDestroyOnLoad (gameObject);
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
			string id = sqliteQuery.GetString (ID_FIELD);
			string name = sqliteQuery.GetString (NAME_FIELD);
			IDictionary quiz = new Dictionary<string,string>();
			quiz.Add (ID_FIELD, id);
			quiz.Add (NAME_FIELD, name);
			quizList.Add(quiz);
		}
		sqliteDB.Close ();
		return quizList;
	}
}
