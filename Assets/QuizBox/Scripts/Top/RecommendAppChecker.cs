using UnityEngine;
using System.Collections;
using MiniJSON;

public class RecommendAppChecker : MonoBehaviour
{
	public RecommendAppDialog RecommendAppDialogPrefab;
	#if UNITY_IPHONE
	// Use this for initialization
	void Start ()
	{
		if(!OnSaleChecker.CheckOnSale()){
			return;
		}
		int recommendCount = PrefsManager.Instance.GetRecommendCount ();
		recommendCount++;
		PrefsManager.Instance.SaveRecommendCount (recommendCount);
		Debug.Log("recommendCount = "+recommendCount);
		if (recommendCount % 3 == 0) {
			RecommendAppDialog recommendAppDialog = Instantiate(RecommendAppDialogPrefab) as RecommendAppDialog;
			recommendAppDialog.Show();
		}
	}
	#endif
}
