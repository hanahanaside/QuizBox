using UnityEngine;
using System.Collections;

public class GameFeatManager : MonoBehaviour {

	private static GameFeatManager sInstance;

	// Use this for initialization
	void Start () {
		sInstance = this;
		GFUnityPlugin.activate("8276", false, false, false);
	}
	
	public static GameFeatManager Instance{
		get{
			return sInstance;
		}
	}

	public void ShowWall(){
		GFUnityPlugin.start("8276");
	}
}
