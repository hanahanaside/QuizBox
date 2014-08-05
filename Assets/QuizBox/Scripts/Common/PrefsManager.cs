using UnityEngine;
using System.Collections;

public class PrefsManager {
	private const string USER_POINT_KEY = "user_point_key";
	private const string IS_REGISTERED = "isRegistered";
	private const string RECOMMEND_COUNT_KEY = "recommendCount";
	private const string IS_REVIEWED = "isReviewed";
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

	public void SaveRecommendCount (int recommendCount) {
		PlayerPrefs.SetInt (RECOMMEND_COUNT_KEY, recommendCount);
		PlayerPrefs.Save ();
	}
	
	public int GetRecommendCount () {
		return PlayerPrefs.GetInt (RECOMMEND_COUNT_KEY);
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
}
