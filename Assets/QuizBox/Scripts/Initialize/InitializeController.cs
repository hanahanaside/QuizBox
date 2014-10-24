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
		ConnectingDialog.Show ();
		TweenAlpha.Begin (backgroundObject,2.0f,1);
	}
		
	void OnDatabaseCreated(){
		Debug.Log ("OnDatabaseCreated");
		ConnectingDialog.Hide ();
		TweenAlpha tweenAlpha = TweenAlpha.Begin (backgroundObject,2.0f,0);
		EventDelegate.Set (tweenAlpha.onFinished,OnFadeoutAnimationFinished);
	}

	private void OnFadeoutAnimationFinished(){
		Application.LoadLevel ("Top");
	}
}
