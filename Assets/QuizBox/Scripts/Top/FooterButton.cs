using UnityEngine;
using System.Collections;

public class FooterButton : MonoBehaviour {

	private GameObject mFilterObject;

	void Awake(){
		mFilterObject = transform.FindChild ("Filter").gameObject;
	}

	public void ShowFilter(){
		mFilterObject.SetActive (true);
	}

	public void HideFilter(){
		mFilterObject.SetActive (false);
	}
}
