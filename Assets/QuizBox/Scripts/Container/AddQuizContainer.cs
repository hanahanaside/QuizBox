using UnityEngine;
using System.Collections;

public class AddQuizContainer : MonoSingleton<AddQuizContainer> {

	private AddQuizScrollView mAddQuizScrollView;

	public override void OnInitialize(){
		mAddQuizScrollView = GetComponentInChildren<AddQuizScrollView> ();
	}

	public void Show(){
		foreach(Transform child in transform){
			child.gameObject.SetActive (true);
		}
		mAddQuizScrollView.Show ();
	}

	public void Hide(){
		foreach(Transform child in transform){
			child.gameObject.SetActive (false);
		}
	}
}
