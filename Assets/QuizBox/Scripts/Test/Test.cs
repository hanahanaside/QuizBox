using UnityEngine;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {

	public FacebookSender facebookSender;
	public string result;

	private string _userId;
	private bool _canUserUseFacebookComposer = false;
	private bool _hasPublishPermission = false;
	private bool _hasPublishActions = false;
	


	void completionHandler (string error, object result) {
		if (error != null){
			Debug.Log("error");
			Debug.LogError (error);
		}
		else
			Prime31.Utils.logObject (result);
	}

	void Start()
	{
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

		// this is iOS 6 only!
		_canUserUseFacebookComposer = FacebookBinding.canUserUseFacebookComposer();
		
		// optionally enable logging of all requests that go through the Facebook class
		//Facebook.instance.debugRequests = true;
	}


	public void OnButtonClick () {
		Debug.Log ("can use = " + FacebookBinding.canUserUseFacebookComposer ());
		Debug.Log ("session valid = " + FacebookBinding.isSessionValid ());
		Facebook.instance.postMessage( "im posting this from Unity: " + Time.deltaTime, completionHandler );
	}

}
