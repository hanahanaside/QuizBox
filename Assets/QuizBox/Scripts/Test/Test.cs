using UnityEngine;
using System.Collections;
using System;

public class Test : MonoBehaviour {

	void Start(){
		Invoke ("A",2.0f);
	}

	private void A(){
		Debug.Log ("aa");
	}

}
