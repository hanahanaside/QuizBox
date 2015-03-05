using UnityEngine;
using System.Collections;

public class SettingContainer : MonoSingleton<SettingContainer> , IContainer {

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
