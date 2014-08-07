using UnityEngine;
using System.Collections;

public class PrefsManager {
	private const string USER_POINT_KEY = "user_point_key";
	private const string IS_REGISTERED = "isRegistered";
	private const string IS_REVIEWED = "isReviewed";
	private const string IS_SOUND_ON = "isSoundOn";
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

	public void SaveSoundON (bool soundOn) {
		if (soundOn) {
			PlayerPrefs.SetInt (IS_SOUND_ON, 0);
		} else {
			PlayerPrefs.SetInt (IS_SOUND_ON, 1);
		}
		PlayerPrefs.Save ();
	}

	public bool IsSoundON () {
		int flag = PlayerPrefs.GetInt (IS_SOUND_ON, 0);
		if (flag == 0) {
			return true;
		} else {
			return false;
		}
	}
}
