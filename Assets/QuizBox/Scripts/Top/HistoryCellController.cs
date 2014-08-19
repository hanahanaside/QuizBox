using UnityEngine;
using System.Collections;
using System.Text;

public class HistoryCellController : MonoBehaviour {

	public UILabel historyLabel;
	public UISprite medalSprite;
	private HistoryData mHistoryData;

	public void Init (HistoryData historyData) {
		mHistoryData = historyData;
		StringBuilder sb = new StringBuilder ();
		sb.Append (historyData.date + "\n");
		sb.Append (historyData.title + " | " + historyData.mode + "\n");
		sb.Append (historyData.result);
		historyLabel.text = sb.ToString ();
		double average = mHistoryData.Average;
		Debug.Log("average = "+ average);
		if(average >=100){
			medalSprite.spriteName = "01.gold";
		}else if(average >= 90){
			medalSprite.spriteName = "02.silver";
		}else if(average >= 80){
			medalSprite.spriteName = "03.bronze";
		}else if(average <=15){
			medalSprite.spriteName = "04.0";
		}else {
			medalSprite.enabled = false;
		}
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
