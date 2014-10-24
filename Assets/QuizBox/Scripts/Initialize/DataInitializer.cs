using UnityEngine;
using System.Collections;

public class DataInitializer : MonoBehaviour {

	void Awake(){
		#if UNITY_EDITOR
		PrefsManager.Instance.DatabaseVersion = 0;
		#endif
	}
}
