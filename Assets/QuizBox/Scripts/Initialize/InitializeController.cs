using UnityEngine;
using System.Collections;

public class InitializeController : MonoBehaviour {

	void OnEnable(){
		DatabaseCreator.createdDatabaseEvent += OnDatabaseCreated;
	}

	void OnDisable(){
		DatabaseCreator.createdDatabaseEvent -= OnDatabaseCreated;
	}

	void Awake(){
		ConnectingDialog.Show ();
	}
		
	void OnDatabaseCreated(){
		Debug.Log ("OnDatabaseCreated");
		ConnectingDialog.Hide ();
		Application.LoadLevel("Splash");
	}

}
