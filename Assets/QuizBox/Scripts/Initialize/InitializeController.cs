using UnityEngine;
using System.Collections;

public class InitializeController : MonoBehaviour {

	public GameObject backgroundObject;

	void OnEnable(){
		DatabaseCreator.createdDatabaseEvent += OnDatabaseCreated;
	}

	void OnDisable(){
		DatabaseCreator.createdDatabaseEvent -= OnDatabaseCreated;
	}

	void Awake(){
		TweenAlpha.Begin (backgroundObject,2.0f,1);
	}
		
	void OnDatabaseCreated(){
		Debug.Log ("OnDatabaseCreated");
		SoundManager.Instance.PlaySESound (SoundManager.HANAUTA_SOUND_ID);
		Invoke ("StartFadeoutAnimation",3.0f);
	}

	private void OnFadeoutAnimationFinished(){
		Application.LoadLevel ("Top");
	}

	private void StartFadeoutAnimation(){
		TweenAlpha tweenAlpha = TweenAlpha.Begin (backgroundObject,2.0f,0);
		EventDelegate.Set (tweenAlpha.onFinished,OnFadeoutAnimationFinished);
	}
}
