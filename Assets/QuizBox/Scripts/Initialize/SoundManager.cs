using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public static int  GOOD_SOUND_ID = 0;
	public static int BAD_SOUND_ID = 1;
	public static int HANAUTA_SOUND_ID = 2;
	public static int BGM_MAIN =0;
	public static int BGM_QUIZ = 1;
	public static int BGM_RESULT = 2;

	public AudioClip[] SEclipArray;
	public AudioClip[] bgmClipArray;
	private AudioSource[] SEsourceArray;
	private AudioSource mBGMAudioSource;
	private static SoundManager sInstance;
	
	void Awake () {
		sInstance = this;
		DontDestroyOnLoad (gameObject);
		mBGMAudioSource = gameObject.AddComponent<AudioSource> ();
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

	public void PlayBGM(int bgmClipId){
		if (!PrefsManager.Instance.IsSoundON ()) {
			return;
		}
		mBGMAudioSource.clip = bgmClipArray[bgmClipId];
		mBGMAudioSource.Play();
	}

	public void StopBGM(){
		mBGMAudioSource.Stop();
	}
}
