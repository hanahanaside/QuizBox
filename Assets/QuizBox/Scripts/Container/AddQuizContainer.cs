using UnityEngine;
using System.Collections;

public class AddQuizContainer : MonoSingleton<AddQuizContainer> {

	public AddQuizScrollView mAddQuizScrollView;

	public override void OnInitialize(){
		Debug.Log ("init");
		Hide ();
	}

	public void Show(){
		foreach(Transform child in transform){
			child.gameObject.SetActive (true);
		}
		mAddQuizScrollView.ShowPopularCategory ();
	}

	public void Hide(){
		foreach(Transform child in transform){
			child.gameObject.SetActive (false);
		}
	}
}
