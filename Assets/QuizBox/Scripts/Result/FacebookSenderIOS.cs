using UnityEngine;
using System.Collections;

public class FacebookSenderIOS : MonoBehaviour {


	private string _userId;
	private bool _canUserUseFacebookComposer = false;
	private bool _hasPublishPermission = false;
	private bool _hasPublishActions = false;
	public static string screenshotFilename = "someScreenshot.png";

	// common event handler used for all graph requests that logs the data to the console
	void completionHandler (string error, object result) {
		Debug.Log("completionHandler");
		if (error != null)
			Debug.LogError (error);
		else
			Prime31.Utils.logObject (result);
	}

	void Start () {
		// dump custom data to log after a request completes
		FacebookManager.graphRequestCompletedEvent += result =>
		{
			Prime31.Utils.logObject( result );
		};
		
		// when the session opens or a reauth occurs we check the permissions to see if we can publish
		FacebookManager.sessionOpenedEvent += () =>
		{
			_hasPublishPermission = FacebookBinding.getSessionPermissions().Contains( "publish_stream" );
			_hasPublishActions = FacebookBinding.getSessionPermissions().Contains( "publish_actions" );
		};
		
		FacebookManager.reauthorizationSucceededEvent += () =>
		{
			_hasPublishPermission = FacebookBinding.getSessionPermissions().Contains( "publish_stream" );
			_hasPublishActions = FacebookBinding.getSessionPermissions().Contains( "publish_actions" );
		};
		
		// grab a screenshot for later use
		Application.CaptureScreenshot( screenshotFilename );
		
		// this is iOS 6 only!
		_canUserUseFacebookComposer = FacebookBinding.canUserUseFacebookComposer();
		
		// optionally enable logging of all requests that go through the Facebook class
		//Facebook.instance.debugRequests = true;

		FacebookBinding.init();
	}

	public void PostMessage(string message){
		Facebook.instance.postMessage( "im posting this from Unity: " + Time.deltaTime, completionHandler );
	}

}
