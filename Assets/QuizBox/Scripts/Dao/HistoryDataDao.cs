using UnityEngine;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class HistoryDataDao 
{
	private const string TABLE_NAME = "history_data";
	private const string ID_FIELD = "id";
	private const string AVERAGE_FIELD = "average";
	private const string TITLE_FIELD = "title";
	private const string MODE_FIELD = "mode";
	private const string RESULT_FIELD = "result";
	private const string DATE_FIELD = "date"; 
	private const string FLAG_TWEET_FIELD = "flag_tweet";
	private static HistoryDataDao sInstance;

	public static HistoryDataDao instance{
		get{
			if(sInstance == null){
				sInstance = new HistoryDataDao();
			}
			return sInstance;
		}
	}

	public void InsertHistoryData (HistoryData historyData)
	{
		SQLiteDB sqliteDB = OpenDB ();
		string sql = CreateInsertSQL (historyData);
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sql);
		sqliteQuery.Step();
		sqliteDB.Close ();
	}

	public void UpdateHistoryData(HistoryData historyData){
		SQLiteDB sqliteDB = OpenDB ();
		StringBuilder sb = new StringBuilder ();
		sb.Append ("update " + TABLE_NAME + " ");
		sb.Append ("set " + FLAG_TWEET_FIELD + " = " + historyData.flagTweet + " ");
		sb.Append ("where " + ID_FIELD + " = " + historyData.id + ";");
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sb.ToString());
		sqliteQuery.Step();
		sqliteDB.Close ();
	}

	public IList<HistoryData> QueryHistoryDataList ()
	{
		IList<HistoryData> historyDataList = new List<HistoryData> ();
		SQLiteDB sqliteDB = OpenDB ();
		string sql = "select * from " + TABLE_NAME + ";";
		SQLiteQuery sqliteQuery = new SQLiteQuery (sqliteDB, sql);
		while (sqliteQuery.Step()) {
			HistoryData historyData = new HistoryData ();
			historyData.id = sqliteQuery.GetInteger (ID_FIELD);
			historyData.Average = sqliteQuery.GetDouble(AVERAGE_FIELD);
			historyData.title = sqliteQuery.GetString (TITLE_FIELD);
			historyData.mode = sqliteQuery.GetString (MODE_FIELD);
			historyData.result = sqliteQuery.GetString (RESULT_FIELD);
			historyData.date = sqliteQuery.GetString (DATE_FIELD);
			historyData.flagTweet = sqliteQuery.GetInteger (FLAG_TWEET_FIELD);
			historyDataList.Add (historyData);
		}
		return historyDataList;
	}

	private SQLiteDB OpenDB ()
	{
		SQLiteDB sqliteDB = new SQLiteDB ();
		string fileName = Application.persistentDataPath + "/quiz_box.db";
		sqliteDB.Open (fileName);
		return sqliteDB;
	}

	private string CreateInsertSQL (HistoryData historyData)
	{
		StringBuilder sb = new StringBuilder ();
		sb.Append ("insert into " + TABLE_NAME + " values(");
		sb.Append ("null ,");
		sb.Append(historyData.Average + " ,");
		sb.Append ("'" + historyData.title + "',");
		sb.Append ("'" + historyData.mode + "',");
		sb.Append ("'" + historyData.result + "',");
		sb.Append ("'" + historyData.date + "',");
		sb.Append (historyData.flagTweet);
		sb.Append (");");
		return sb.ToString ();
	}
}
