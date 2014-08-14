using UnityEngine;
using System.Collections;

public class InitializeController : MonoBehaviour {

	void OnEnable(){
		DatabaseCreator.createdDatabaseEvent += OnDatabaseCreated;
	}

	void OnDisable(){
		DatabaseCreator.createdDatabaseEvent -= OnDatabaseCreated;
	}
	
	void Start () {

	}

	void OnDatabaseCreated(){
		Application.LoadLevel("Splash");
	}

}
