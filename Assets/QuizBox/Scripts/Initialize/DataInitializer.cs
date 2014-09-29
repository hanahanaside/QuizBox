using UnityEngine;
using System.Collections;

public class DataInitializer : MonoBehaviour {

	void Awake(){
		PrefsManager.Instance.DatabaseVersion = 0;
	}
}
