using UnityEngine;
using System.Collections;

public class AddPointContainer : MonoSingleton<AddPointContainer> ,IContainer {

	public void Show(){
		foreach (Transform child in transform) {
			child.gameObject.SetActive (true);
		}
	}

	public void Hide(){
		foreach (Transform child in transform) {
			child.gameObject.SetActive (false);
		}
	}

}
