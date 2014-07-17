using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HistoryInitializer : MonoBehaviour
{

	void Start ()
	{
		IList<HistoryData> historyDataList = HistoryDataDao.instance.QueryHistoryDataList ();
		foreach (HistoryData historyData in historyDataList) {

		}
	}
}
