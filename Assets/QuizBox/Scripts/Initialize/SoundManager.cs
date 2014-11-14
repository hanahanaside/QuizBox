using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public static int  GOOD_SOUND_ID = 0;
	public static int BAD_SOUND_ID = 1;
	public static int HANAUTA_SOUND_ID = 2;
	public AudioClip[] SEclipArray;
	private AudioSource[] SEsourceArray;
	private static SoundManager sInstance;
	
	void Start () {
		sInstance = this;
		DontDestroyOnLoad (gameObject);
		SEsourceArray = new AudioSource[SEclipArray.Length];
		for (int i = 0; i <SEsourceArray.Length; i++) {
			SEsourceArray [i] = gameObject.AddComponent<AudioSource> ();    
			SEsourceArray [i].clip = SEclipArray [i];
		}
	}

	public static SoundManager Instance {
		get {
			return sInstance;
		}
	}

	public void PlaySESound (int id) {
		if (!PrefsManager.Instance.IsSoundON ()) {
			return;
		}
		SEsourceArray [id].Play ();
	}
}
