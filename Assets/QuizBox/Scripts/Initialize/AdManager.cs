using UnityEngine;
using System.Collections;

public class AdManager : MonoBehaviour {

	public NendAdBanner rectangleAd;
	private static AdManager sInstance;
	
	void Start () {
		sInstance = this;
		DontDestroyOnLoad (gameObject);
	}
	
	public static AdManager Instance {
		get {
			return sInstance;
		}
	}

	public void ShowRectangleAd () {
#if !UNITY_EDITOR
		rectangleAd.Show ();
#endif
	}

	public void HideRectangleAd () {
#if !UNITY_EDITOR
		rectangleAd.Hide ();
#endif
	}
}
