using UnityEngine;
using System.Collections;

public class PrefsManager 
{

	private static PrefsManager sInstance;

	public static PrefsManager instance {
		get {
			if (sInstance == null) {
				sInstance = new PrefsManager();
			}
			return sInstance;
		}
	}
}
