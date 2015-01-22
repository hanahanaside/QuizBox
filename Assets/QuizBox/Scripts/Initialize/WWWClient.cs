using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class WWWClient {
	public delegate void RequestFinishedDelegate (WWW www);

	public delegate void TimeOutDelegate ();

	public float timeOutInterval = 10.0f;
	private RequestFinishedDelegate mOnSuccess;
	private RequestFinishedDelegate mOnFail;
	private TimeOutDelegate mOnTimeOut;
	private MonoBehaviour mMonoBehaviour;
	private WWW mWWW;
	private Dictionary<string,string> mHeader;
	private string mURL;
	private bool mIsTimeOut;

	public WWWClient (MonoBehaviour monoBehaviour, string url)
	{
		mMonoBehaviour = monoBehaviour;
		mURL = url;
		mHeader = new Dictionary<string, string> ();
	}

	public RequestFinishedDelegate OnSuccess {
		set{ mOnSuccess = value; }
	}

	public RequestFinishedDelegate OnFail {
		set{ mOnFail = value; }
	}

	public TimeOutDelegate OnTimeOut {
		set{ mOnTimeOut = value; }
	}

	public void AddJsonHeader () {
		mHeader.Add ("Content-Type", "application/json");
	}

	public void SetTimeOutInterval(float time){
		timeOutInterval = time;
	}
		
	public void GetData () {
		mMonoBehaviour.StartCoroutine (GetCoroutine ());
	}
		
	private IEnumerator GetCoroutine () {
		mWWW = new WWW (mURL);
		yield return mMonoBehaviour.StartCoroutine (CheckTimeout ());
		CheckResponse ();
	}

	private void CheckResponse () {
		if (mIsTimeOut) {
			CallBackTimeOut ();
		} else if (mWWW.error == null) {
			CallBackSuccess ();
		} else {
			CallBackFail ();
		}
		mWWW.Dispose ();
	}

	private void CallBackTimeOut () {
		Debug.Log ("TimeOut");
		if (mOnTimeOut != null) {
			mOnTimeOut ();
		}
	}

	private void CallBackSuccess () {
		Debug.Log ("www ok");
		if (mOnSuccess != null) {
			mOnSuccess (mWWW);
		}
	}

	private void CallBackFail () {
		Debug.Log ("www error");
		Debug.Log (mWWW.text);
		if (mOnFail != null) {
			mOnFail (mWWW);
		}
	}

	private  IEnumerator CheckTimeout () {
		float startRequestTime = Time.time;
		while (!mWWW.isDone) {
			if (Time.time - startRequestTime < timeOutInterval)
				yield return null;
			else {
				//タイムアウト
				mIsTimeOut = true;
				break;
			}
		}
		yield return null;
	}
}
