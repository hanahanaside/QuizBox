using UnityEngine;
using System.Collections;

public class InitializeController : MonoBehaviour {

	public GameObject backgroundObject;

	void OnEnable () {
		DatabaseCreator.createdDatabaseEvent += OnDatabaseCreated;
		SelledProjectsManager.createdEvent += SelledProjectArrayCreatedEvent;
	}

	void OnDisable () {
		DatabaseCreator.createdDatabaseEvent -= OnDatabaseCreated;
		SelledProjectsManager.createdEvent -= SelledProjectArrayCreatedEvent;
	}

	void Start () {
		DatabaseCreator.instance.CreateDatabase ();
	}

	void OnDatabaseCreated () {
		Debug.Log ("OnDatabaseCreated");
		SelledProjectsManager.instance.Create ();
	}

	void SelledProjectArrayCreatedEvent (bool success) {
		Debug.Log ("SelledProjectArrayCreatedEvent " + success);
		if (success) {
			DatabaseCreator.instance.RenameDatabaseQuiz ();
		}
		SoundManager.Instance.PlaySESound (SoundManager.SE_CHANNEL.Hanauta);
		StartFadeoutAnimation ();
	}


	private void OnFadeoutAnimationFinished () {
		SoundManager.Instance.PlayBGM (SoundManager.BGM_CHANNEL.Main);
		Application.LoadLevel ("Top");
	}

	private void StartFadeoutAnimation () {
		TweenAlpha tweenAlpha = TweenAlpha.Begin (backgroundObject, 2.0f, 0);
		EventDelegate.Set (tweenAlpha.onFinished, OnFadeoutAnimationFinished);
	}
}
