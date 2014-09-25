using UnityEngine;
using System.Collections;
using MiniJSON;
using System;

public class RecommendAppChecker : MonoBehaviour {
	public RecommendAppDialog RecommendAppDialogPrefab;
	public ReviewDialog reviewDialogPrefab;
	private static int count;
	#if UNITY_IPHONE
	// Use this for initialization
	void Start () {
		DateTime dtNow = DateTime.Now;
		string installedDate = PrefsManager.Instance.InstalledDate;
		DateTime dtInstalled = DateTime.Parse (installedDate);
		TimeSpan timeSpan = dtNow - dtInstalled;
		if (timeSpan.TotalDays < 3) {
			return;
		}
		count++;
		Debug.Log ("count = " + count);
		if (count % 3 == 0) {
			if (!PrefsManager.Instance.GetReviewed ()) {
				ReviewDialog reviewDialog = Instantiate (reviewDialogPrefab) as ReviewDialog;
				reviewDialog.Show ();
			} else {
				RecommendAppDialog recommendAppDialog = Instantiate (RecommendAppDialogPrefab) as RecommendAppDialog;
				recommendAppDialog.Show ();
			}
		}
	}
	#endif
}
