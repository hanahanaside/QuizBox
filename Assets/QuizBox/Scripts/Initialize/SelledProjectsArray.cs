using UnityEngine;
using System.Collections;
using System;

public class SelledProjectsArray : MonoSingleton<SelledProjectsArray> {

	public static event Action<bool> createdEvent;

	private const string SELLED_PROJECTS_URL = "http://quiz.ryodb.us/list/selled_projects.json";

	private SelledProject[] mSelledProjectsArray;

	public override void OnInitialize(){
		DontDestroyOnLoad (gameObject);
	}

	public void Create () {
		Debug.Log ("create selled projects array");
		WWWClient wwwClient = new WWWClient (this, SELLED_PROJECTS_URL);
		wwwClient.OnSuccess = (WWW www) => {
			string json = www.text;
			mSelledProjectsArray = JsonFx.Json.JsonReader.Deserialize<SelledProject[]> (json);
			createdEvent (true);
		};
		wwwClient.OnFail = (WWW www) => {
			createdEvent (false);
		};
		wwwClient.OnTimeOut = () => {
			createdEvent (false);
		};
		wwwClient.GetData ();
	}

	public SelledProject Get (int index) {
		return mSelledProjectsArray [index];
	}

	public int Length {
		get {
			if (mSelledProjectsArray == null) {
				return 0;
			}
			return mSelledProjectsArray.Length;
		}
	}
}
