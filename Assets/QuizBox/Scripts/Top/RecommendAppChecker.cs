using UnityEngine;
using System.Collections;
using MiniJSON;

public class RecommendAppChecker : MonoBehaviour {
	public RecommendAppDialog RecommendAppDialogPrefab;
	private static int count;
	#if UNITY_IPHONE
	// Use this for initialization
	void Start ()
	{
		if(!OnSaleChecker.CheckOnSale()){
			return;
		}
		count++;
		Debug.Log("count = "+count);
		if (count % 3 == 0) {
			RecommendAppDialog recommendAppDialog = Instantiate(RecommendAppDialogPrefab) as RecommendAppDialog;
			recommendAppDialog.Show();
		}
	}
	#endif
}
