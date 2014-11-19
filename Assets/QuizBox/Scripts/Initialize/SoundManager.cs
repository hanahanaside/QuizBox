using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public static int GOOD_SOUND_ID = 0;
	public static int BAD_SOUND_ID = 1;
	public static int HANAUTA_SOUND_ID = 2;

	public enum SE_CHANNEL {
		Good,
		Bad,
		Hanauta,
		Result
	};


	public enum BGM_CHANNEL
	{
		Main,
		Quiz}

	;

	public static int BGM_MAIN = 0;
	public static int BGM_QUIZ = 1;
	public static int BGM_RESULT = 2;
	public AudioClip[] bgmClipArray;
	public AudioClip[] SEclipArray;
	private AudioSource[] mSEsourceArray;
	private AudioSource mBGMAudioSource;
	private static SoundManager sInstance;

	void Start () {
		sInstance = this;
		DontDestroyOnLoad (gameObject);
		mBGMAudioSource = gameObject.AddComponent<AudioSource> ();
		mBGMAudioSource.loop = true;
		mSEsourceArray = new AudioSource[SEclipArray.Length];
		for (int i = 0; i < mSEsourceArray.Length; i++) {
			mSEsourceArray [i] = gameObject.AddComponent<AudioSource> ();    
			mSEsourceArray [i].clip = SEclipArray [i];
		}
		//リザルトの効果音の音量を下げる
		mSEsourceArray [3].volume = 0.3f;
	}

	public static SoundManager Instance {
		get {
			return sInstance;
		}
	}

	public void PlayBGM (BGM_CHANNEL bgmChannel) {
		if(!PrefsManager.Instance.IsBGMON()){
			return;
		}
		int channelId = (int)bgmChannel;
		mBGMAudioSource.clip = bgmClipArray [channelId];
		mBGMAudioSource.Play ();
	}

	public void StopBGM () {
		mBGMAudioSource.Stop ();
	}

	public void PlaySESound (SE_CHANNEL seChannel) {
		if (!PrefsManager.Instance.IsSEON ()) {
			return;
		}
		int seChannelId = (int)seChannel;
		mSEsourceArray [seChannelId].Play ();
	}
}
