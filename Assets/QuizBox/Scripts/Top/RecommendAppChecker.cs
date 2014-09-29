using UnityEngine;
using System.Collections;
using MiniJSON;
using System;

public class RecommendAppChecker : MonoBehaviour {
	public RecommendAppDialog RecommendAppDialogPrefab;
	private static int count;
	// Use this for initialization

	#if UNITY_IPHONE
	void Start () {
		count++;
		if (count % 3 != 0) {
			return;
		}

		DateTime dtNow = DateTime.Now;
		string installedDate = PrefsManager.Instance.InstalledDate;
		DateTime dtInstalled = DateTime.Parse (installedDate);
		TimeSpan timeSpan = dtNow - dtInstalled;
		if (timeSpan.Days > 3) {
			RecommendAppDialog recommendAppDialog = Instantiate (RecommendAppDialogPrefab) as RecommendAppDialog;
			recommendAppDialog.Show ();
		}
	}
	#endif
}
