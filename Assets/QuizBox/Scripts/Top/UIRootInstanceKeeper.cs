using UnityEngine;
using System.Collections;

public class UIRootInstanceKeeper : MonoBehaviour {

	private static GameObject uiRoot;

	public static GameObject Instance {
		get {
			return uiRoot;
		}
	}

	// Use this for initialization
	void Start () {
		uiRoot = gameObject;
	}
	
}
