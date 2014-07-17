using UnityEngine;
using System;
using System.Collections;

public class HttpClient : MonoBehaviour {

	public static event Action<string> responseEvent;

	public  IEnumerator Excute(WWW www){
		Debug.Log("excute");
		yield return StartCoroutine(ResponceCheckForTimeOutWWW(www, 5.0f));
		if(www.error != null){
			//レスポンスエラーの場合
			Debug.Log("error"+www.error);
			ResponseCallback(null);
		
		}else if(www.isDone){
			//リクエスト成功の場合
			Debug.Log("www ok"+www.text);
			ResponseCallback(www.text);
		}
	}

	private  IEnumerator ResponceCheckForTimeOutWWW(WWW www, float timeout)
	{
		float requestTime = Time.time;
		
		while(!www.isDone)
		{
			if(Time.time - requestTime < timeout)
				yield return null;
			else
			{
				Debug.LogWarning("TimeOut"); //タイムアウト
				ResponseCallback(null);
				break;
			}
		}
		
		yield return null;
	}

	private  void ResponseCallback(string response){
		if( responseEvent != null ){
			responseEvent(response);
		}
	}
}
