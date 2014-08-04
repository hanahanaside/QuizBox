using UnityEngine;
using System.Collections;
using System.Text;

public class HistoryCellController : MonoBehaviour {

	public UILabel historyLabel;
	private HistoryData mHistoryData;

	public void Init (HistoryData historyData) {
		StringBuilder sb = new StringBuilder ();
		sb.Append (historyData.date + "\n");
		sb.Append (historyData.title + " | " + historyData.mode + "\n");
		sb.Append (historyData.result);
		historyLabel.text = sb.ToString ();
	}

	public void OnTweetButtonClicked () {

	}
}
