using UnityEngine;
using System.Collections;

public class PrefsManager 
{
	private const string USER_POINT_KEY = "user_point_key";
	private static PrefsManager sInstance;

	public static PrefsManager instance {
		get {
			if (sInstance == null) {
				sInstance = new PrefsManager();
			}
			return sInstance;
		}
	}

	public void SaveUserPoint(int point){
		PlayerPrefs.SetInt(USER_POINT_KEY,point);
		PlayerPrefs.Save();
	}

	public int GetUserPoint(){
		return PlayerPrefs.GetInt(USER_POINT_KEY);
	}
}
