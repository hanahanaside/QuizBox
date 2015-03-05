using UnityEngine;
using System.Collections;

public class QuizTopicContainer : MonoSingleton<QuizTopicContainer> , IContainer {


	public override void OnInitialize(){

	}

	public void Show(){
		foreach (Transform child in transform) {
			child.gameObject.SetActive (true);
		}
		QuizTopicDialogManager.instance.Show ();
	}

	public void Hide(){
		QuizTopicDialogManager.instance.UpdateOrderNumber ();
		foreach (Transform child in transform) {
			child.gameObject.SetActive (false);
		}
	}
}
