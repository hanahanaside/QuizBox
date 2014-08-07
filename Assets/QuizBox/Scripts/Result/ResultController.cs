using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResultController : MonoBehaviour {

	public TweetSender tweetSender;
	public FacebookSender facebookSender;
	
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel ("Top");
		}
	}
	
	public void OnRetryClick () {
		Reset ();
		Application.LoadLevel ("Game");
	}

	public void OnTopClick () {
		Reset ();
		Application.LoadLevel ("Top");
	
	}

	public void OnTwitterClick () {
		tweetSender.SendTweet ();
//		if (tweetSender.IsLoggedIn ()) {
//			tweetSender.SendTweet ();
//		} else {
//			tweetSender.ShowLoginDialog ();
//		}
	}
	
	public void OnFaceBookClick () {
		Debug.Log ("facebook");
		Debug.Log(""+facebookSender.IsSessionValid());

		facebookSender.ShowFacebookComposer();
//		if (facebookSender.IsSessionValid ()) {
//			facebookSender.ShowShareDialog();
//		} else {
//			facebookSender.Login ();
//		}
	}

	private void Reset () {
		ScoreKeeper.instance.score = 0;
	}
}
