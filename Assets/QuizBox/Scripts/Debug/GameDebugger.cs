using UnityEngine;
using System.Collections;

public class GameDebugger : MonoBehaviour {

	void Awake(){
		if( GameObject.FindGameObjectWithTag("SoundManager")== null){
			GameObject soundManagerPrefab = Resources.Load ("SoundManager") as GameObject;
			Instantiate (soundManagerPrefab);
			Debug.Log ("awake");
		}

	}
}
