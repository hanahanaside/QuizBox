using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using MiniJSON;

public class PostCountDataKeeper : MonoBehaviour {
	public UILabel userPointLabel;
	private static readonly DateTime UNIX_EPOCH = new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
	private static readonly string URL = "https://ntp-a1.nict.go.jp/cgi-bin/json";
	private PostCountData mPostCountData;
	private static PostCountDataKeeper sInstance;

	void Start () {
		sInstance = this;
		mPostCountData = PrefsManager.Instance.GetPostCountData ();
		WWWClient wwwClient = new WWWClient (this, URL);
		wwwClient.OnSuccess = (string response) => {
			Dictionary<string,object> dic = (Dictionary<string,object>)Json.Deserialize (response);
			double unixTime = (double)dic ["st"];
			DateTime dtNow = UNIX_EPOCH.AddSeconds ((long)unixTime).ToLocalTime ();
			if (string.IsNullOrEmpty (mPostCountData.PostDate)) {
				mPostCountData.PostDate = dtNow.ToString ();
				PrefsManager.Instance.SavePostCountData (mPostCountData);
				return;
			}
			DateTime dtPost = DateTime.Parse (mPostCountData.PostDate);
			TimeSpan ts = dtNow - dtPost;
			if (ts.Days >= 1) {
				// reset postData
				mPostCountData.PostCount = 0;
				mPostCountData.PostDate = dtNow.ToString ();
				PrefsManager.Instance.SavePostCountData (mPostCountData);
			}
		};
		wwwClient.GetData ();

	}

	public static PostCountDataKeeper Instance{
		get{
			return sInstance;
		}
	}

	public void UpdatePostCountData () {
		mPostCountData.PostCount++;
		if (mPostCountData.PostCount <= 10) {
			PrefsManager.Instance.AddUserPoint (1);
		}
		PrefsManager.Instance.SavePostCountData (mPostCountData);
		userPointLabel.text = "" + PrefsManager.Instance.GetUserPoint();
	}
}
