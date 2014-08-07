using UnityEngine;
using System.Collections;

public class FenceInstanceKeeper : MonoBehaviour {

	private static GameObject fence;

	// Use this for initialization
	void Start () {
		fence = gameObject;
		gameObject.SetActive(false);
	}
	
	public static GameObject Instance {
		get {
			return fence;
		}
	}
}
