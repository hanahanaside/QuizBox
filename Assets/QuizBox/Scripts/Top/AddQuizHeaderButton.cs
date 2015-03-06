using UnityEngine;
using System.Collections;

public class AddQuizHeaderButton : MonoBehaviour {

	private GameObject mFilterObject;

	void Awake(){
		mFilterObject = transform.FindChild ("Filter").gameObject;
	}

	public void ShowFilter(){
		Debug.Log ("show");
		mFilterObject.SetActive (true);
	}

	public void HideFilter(){
		mFilterObject.SetActive (false);
	}
}
