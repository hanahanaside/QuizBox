using UnityEngine;
using System.Collections;

public class PrefsManager : MonoBehaviour
{

	private static PrefsManager sInstance;

	public static PrefsManager instance {
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

}
