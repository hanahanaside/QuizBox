using UnityEngine;
using System.Collections;

public class ResultController : MonoBehaviour
{
	void Update ()
	{
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel ("Top");
		}
	}
	
	public void OnRetryClick(){
		Reset ();
		Application.LoadLevel ("Game");
	}

	public void OnTopClick(){
		Reset ();
		Application.LoadLevel ("Top");
	
	}

	public void OnTwitterClick(){

	}

	public void OnFaceBookClick(){

	}

	private void Reset ()
	{
		ScoreKeeper.instance.score = 0;
	}
}
