using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResultController : MonoBehaviour {

	public GameObject shareDialogPrefab;
	public GameObject uiRoot;
	private bool mEscapeClicked = false;

	void Start(){
		SoundManager.Instance.StopBGM ();
		SoundManager.Instance.PlaySESound (SoundManager.SE_CHANNEL.Result);
	}

	void Update () {
		if(mEscapeClicked){
			return;
		}
		if (Input.GetKey (KeyCode.Escape)) {
			mEscapeClicked = true;
			OnTopClick ();
		}
	}

	public void OnRetryClick () {
		DestroyEffect ();
		Reset ();
		QuizListManager.instance.Retry ();
		SoundManager.Instance.PlayBGM (SoundManager.BGM_CHANNEL.Quiz);
		Application.LoadLevel ("Game");
	}

	public void OnTopClick () {
		DestroyEffect ();
		Reset ();
		SoundManager.Instance.PlayBGM (SoundManager.BGM_CHANNEL.Main);
		Application.LoadLevel ("Title");
	
	}

	public void OnShareButtonClicked () {
		ShowShareDialog ();
	}

	private void ShowShareDialog(){
		GameObject shareDialog = Instantiate (shareDialogPrefab) as GameObject;
		shareDialog.transform.parent = uiRoot.transform;
		shareDialog.transform.localScale = new Vector3 (1, 1, 1);
	}

	private void Reset () {
		ScoreKeeper.instance.score = 0;
	}

	private void DestroyEffect(){
		Destroy (GameObject.FindWithTag ("Effect"));
	}
}
