using UnityEngine;
using System.Collections;
using System.Text;

public class HistoryCellController : MonoBehaviour {

	public UILabel historyLabel;
	public UISprite medalSprite;
	public GameObject tweetButton;
	private HistoryData mHistoryData;
	private bool mTweeted = false;

	#if UNITY_ANDROID
	void OnApplicationPause (bool pauseSatatus) {
		Debug.Log ("pause = " + pauseSatatus);
		if (!pauseSatatus && mTweeted) {
			mTweeted = false;
		PrefsManager.Instance.AddUserPoint (1);
		}
	}
	#endif 

	void tweetSheetCompletedEvent (bool didSucceed) {
		Debug.Log ("tweetSheetCompletedEvent " + didSucceed);

		#if UNITY_IPHONE
		TwitterManager.tweetSheetCompletedEvent -= tweetSheetCompletedEvent;
		#endif
		#if UNITY_ANDROID
		EtceteraAndroid.hideProgressDialog();
		#endif
		if (didSucceed) {
			PrefsManager.Instance.AddUserPoint(1);
			TopController.Instance.UpdateUserPointLabel ();
			tweetButton.SetActive (false);
			ShowCompleteDialog ();
		}
	}

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
		if(historyData.flagTweet == 1){
			tweetButton.SetActive (false);
		}
	}

	public void OnTweetButtonClicked () {
		StringBuilder sb = new StringBuilder ();
		sb.Append (mHistoryData.title + " | " + mHistoryData.mode + "\n");
		sb.Append (mHistoryData.result+ "\n");
		sb.Append("\u3053\u306e\u30af\u30a4\u30ba\u30a2\u30d7\u30ea\u9762\u767d\u3044\u304b\u3089\u3084\u3063\u3066\u307f\u3066\uff01"+ "\n");
		sb.Append("→http://tt5.us/quizbox #クイズボックス");
#if UNITY_IPHONE
		TwitterManager.tweetSheetCompletedEvent += tweetSheetCompletedEvent;
		TwitterBinding.showTweetComposer(sb.ToString());
#endif

		#if UNITY_ANDROID
		SocialConnector.Share(sb.ToString());
		mTweeted = true;
		#endif
	}

	private void ShowCompleteDialog () {
		string title = "\u30c4\u30a4\u30fc\u30c8\u6210\u529f!!";
		string message = "1\u30dd\u30a4\u30f3\u30c8\u8ffd\u52a0\u3057\u307e\u3057\u305f";
		#if UNITY_IPHONE
		string[] buttons = {"OK"};
		EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
		#endif

		#if UNITY_ANDROID
		EtceteraAndroid.showAlert (title,message,"OK");
		#endif
	}

}
