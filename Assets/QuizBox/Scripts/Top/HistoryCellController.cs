using UnityEngine;
using System.Collections;
using System.Text;

public class HistoryCellController : MonoBehaviour {

	public UILabel historyLabel;
	private HistoryData mHistoryData;

	public void Init (HistoryData historyData) {
		mHistoryData = historyData;
		StringBuilder sb = new StringBuilder ();
		sb.Append (historyData.date + "\n");
		sb.Append (historyData.title + " | " + historyData.mode + "\n");
		sb.Append (historyData.result);
		historyLabel.text = sb.ToString ();
	}

	public void OnTweetButtonClicked () {
		StringBuilder sb = new StringBuilder ();
		sb.Append (mHistoryData.title + " | " + mHistoryData.mode + "\n");
		sb.Append (mHistoryData.result+ "\n");
		sb.Append("\u3053\u306e\u30af\u30a4\u30ba\u30a2\u30d7\u30ea\u9762\u767d\u3044\u304b\u3089\u3084\u3063\u3066\u307f\u3066\uff01"+ "\n");
		sb.Append("→http://tt5.us/quizbox #クイズボックス");
		string imagePath = Application.streamingAssetsPath + "/share_image.png";
#if UNITY_IPHONE
		TwitterBinding.showTweetComposer(sb.ToString(),imagePath);
#endif
	}
}
