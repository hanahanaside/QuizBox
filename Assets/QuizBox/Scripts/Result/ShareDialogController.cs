using UnityEngine;
using System.Collections;

public class ShareDialogController : MonoBehaviour {
	public FacebookSender facebookSender;
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

	public void OnCloseButtonClicked () {
		Destroy (transform.parent.gameObject);
	}

	public void OnFacebookButtonClicked () {
		Debug.Log ("on facebook click");
		#if UNITY_ANDROID
		Debug.Log ("session = " + FacebookAndroid.isSessionValid ());
		FacebookAndroid.login ();
		#endif
		facebookSender.ShowFacebookComposer ();
	}

	public void OnTweetButtonClicked () {
		TweetSender.Instance.SendTweet ();
		mTweeted = true;
	}
}
