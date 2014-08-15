using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HistoryDialogInitializer : MonoBehaviour {

	public UIGrid grid;
	public GameObject historyCellPrefab;

	// Use this for initialization
	void Start () {
		IList<HistoryData> historyDataList = HistoryDataDao.instance.QueryHistoryDataList ();
		for(int i = historyDataList.Count-1; i>=0;i--){
			HistoryData historyData = historyDataList[i];
			GameObject historyCellObject = Instantiate (historyCellPrefab) as GameObject;
			grid.AddChild (historyCellObject.transform);
			historyCellObject.transform.localScale = new Vector3 (1, 1, 1);
			HistoryCellController controller = historyCellObject.GetComponentInChildren<HistoryCellController> ();
			controller.Init (historyData);
		}
	}
}
