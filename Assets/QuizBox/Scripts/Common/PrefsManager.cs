﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using MiniJSON;

public class PrefsManager {
	private const string USER_POINT_KEY = "user_point_key";
	private const string IS_REGISTERED = "isRegistered";
	private const string IS_REVIEWED = "isReviewed";
	private const string IS_BGM_ON = "isBGMOn";
	private const string IS_SE_ON = "isSEOn";
	private const string POST_COUNT_DATA = "postCountData";
	private const string POST_DATE_KEY = "postDateKey";
	private const string POST_COUNT_KEY = "postCountKey";
	private const string INSTALLED_DATE = "installedDate";
	private const string LOGIN_DATE = "loginDate";
	private const string DATABASE_VERSION = "databaseVersion";
	private const string PAUSE_INCENTIVE_INTERVAL_DATE = "pauseIncentiveIntervalDate";
	private static PrefsManager sInstance;

	public static PrefsManager Instance {
		get {
			if (sInstance == null) {
				sInstance = new PrefsManager ();
			}
			return sInstance;
		}
	}

	public void SaveUserPoint (int point) {
		PlayerPrefs.SetInt (USER_POINT_KEY, point);
		PlayerPrefs.Save ();
	}

	public int GetUserPoint () {
		return PlayerPrefs.GetInt (USER_POINT_KEY, 100);
	}

	public void AddUserPoint (int addPoint) {
		int userPoint = GetUserPoint ();
		userPoint += addPoint;
		SaveUserPoint (userPoint);
	}

	public void SaveRegistered () {
		PlayerPrefs.SetInt (IS_REGISTERED, 1);
		PlayerPrefs.Save ();
	}

	public bool isRegistered () {
		int isRegistered = PlayerPrefs.GetInt (IS_REGISTERED);
		if (isRegistered == 0) {
			return false;
		}
		return true;
	}

	public void SetReviewed () {
		PlayerPrefs.SetInt (IS_REVIEWED, 1);
		PlayerPrefs.Save ();
	}

	public bool GetReviewed () {
		if (PlayerPrefs.GetInt (IS_REVIEWED) == 0) {
			return false;
		}
		return true;
	}

	public void SaveBGMON (bool soundOn) {
		if (soundOn) {
			PlayerPrefs.SetInt (IS_BGM_ON, 0);
		} else {
			PlayerPrefs.SetInt (IS_BGM_ON, 1);
		}
		PlayerPrefs.Save ();
	}

	public bool IsBGMON () {
		int flag = PlayerPrefs.GetInt (IS_BGM_ON, 0);
		if (flag == 0) {
			return true;
		} else {
			return false;
		}
	}

	public void SaveSEON (bool seOn) {
		if (seOn) {
			PlayerPrefs.SetInt (IS_SE_ON, 0);
		} else {
			PlayerPrefs.SetInt (IS_SE_ON, 1);
		}
		PlayerPrefs.Save ();
		Debug.Log ("save " + seOn);
	}

	public bool IsSEON () {
		int flag = PlayerPrefs.GetInt (IS_SE_ON, 0);
		if (flag == 0) {
			return true;
		}
		return false;
	}

	public string InstalledDate {
		set {
			PlayerPrefs.SetString (INSTALLED_DATE, value);
			PlayerPrefs.Save ();
		}
		get {
			return PlayerPrefs.GetString (INSTALLED_DATE,DateTime.Now.ToString());
		}

	}

	public string LoginDate {
		set {
			PlayerPrefs.SetString (LOGIN_DATE, value);
			PlayerPrefs.Save ();
		}
		get {
			return PlayerPrefs.GetString (LOGIN_DATE);
		}
	}

	public int DatabaseVersion {
		set {
			PlayerPrefs.SetInt (DATABASE_VERSION, value);
			PlayerPrefs.Save ();
		}
		get {
			int databaseVersion = PlayerPrefs.GetInt (DATABASE_VERSION, 3);
			return databaseVersion;
		}
	}

	public void SavePostCountData (PostCountData postCountData) {
		Dictionary<string,object> dictionary = new Dictionary<string, object> ();
		dictionary.Add (POST_DATE_KEY, postCountData.PostDate);
		dictionary.Add (POST_COUNT_KEY, postCountData.PostCount);
		string json = Json.Serialize (dictionary);
		Debug.Log ("json = " + json);
		PlayerPrefs.SetString (POST_COUNT_DATA, json);
		PlayerPrefs.Save ();
	}

	public PostCountData GetPostCountData () {
		string json = PlayerPrefs.GetString (POST_COUNT_DATA);
		Debug.Log ("json = " + json);
		PostCountData postCountData = new PostCountData ();
		if (json == "") {
			postCountData.PostCount = 0;
			postCountData.PostDate = "";
			return postCountData;
		}
		Dictionary<string,object> dictionary = (Dictionary<string,object>)Json.Deserialize (json);
		Debug.Log ("2");
		long postCount = (long)dictionary [POST_COUNT_KEY];
		Debug.Log ("3");
		string postDate = dictionary [POST_DATE_KEY].ToString ();
		Debug.Log ("4");

		postCountData.PostCount = (int)postCount;
		postCountData.PostDate = postDate;
		Debug.Log ("return");
		return postCountData;
	}

	public string PauseIncentiveIntervalDate {
		set {
			PlayerPrefs.SetString (PAUSE_INCENTIVE_INTERVAL_DATE, value);
		}
		get {
			return PlayerPrefs.GetString (PAUSE_INCENTIVE_INTERVAL_DATE, "");
		}
	}
}
