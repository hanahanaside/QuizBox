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
		rectangleAd.Show ();
	}

	public void HideRectangleAd () {
		rectangleAd.Hide ();
	}
}
