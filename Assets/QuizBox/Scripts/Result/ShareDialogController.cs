using UnityEngine;
using System.Collections;

public class ShareDialogController : MonoBehaviour {

	public FacebookSender facebookSender;
	public TweetSender tweetSender;

	public void OnCloseButtonClicked(){
		Destroy(transform.parent.gameObject);
	}

	public void OnFacebookButtonClicked(){
		facebookSender.ShowFacebookComposer();
	}

	public void OnTweetButtonClicked(){
		tweetSender.SendTweet();
	}
}
