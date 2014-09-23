using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResultController : MonoBehaviour {

	public GameObject shareDialogPrefab;
	public GameObject uiRoot;

	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel ("Top");
		}
	}
	
	public void OnRetryClick () {
		Reset ();
		QuizListManager.instance.Retry();
		Application.LoadLevel ("Game");
	}

	public void OnTopClick () {
		Reset ();
		QuizListManager.instance.ReleaseQuizList ();
		Application.LoadLevel ("Top");
	
	}

	public void OnShareButtonClicked(){
		GameObject shareDialog = Instantiate(shareDialogPrefab) as GameObject;
		shareDialog.transform.parent = uiRoot.transform;
		shareDialog.transform.localScale = new Vector3(1,1,1);
	}

	private void Reset () {
		ScoreKeeper.instance.score = 0;
	}

}
