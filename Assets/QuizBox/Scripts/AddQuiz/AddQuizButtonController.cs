using UnityEngine;
using System;
using System.Collections;

public class AddQuizButtonController : MonoBehaviour {

	public static event Action<AddQuizButtonController> clickedEvent;

	public GameObject newSprite;
	public UILabel titleLabel;
	public UILabel pointLabel;
	public UITexture logoTexture;
	private AddQuiz mAddQuiz;

	void OnEnable(){
		if(mAddQuiz == null){
			return;
		}
		if(logoTexture.mainTexture == null){
			Debug.Log ("null");
			LoadTexture ();
		}
	}

	public void Init (AddQuiz addQuiz) {
		mAddQuiz = addQuiz;
		titleLabel.text = mAddQuiz.title + "\n(" + mAddQuiz.quizCount + "\u554f)";
		pointLabel.text = mAddQuiz.point + "pt";
		if(mAddQuiz.FlagNew){
			//show new label
			newSprite.SetActive (true);
		}
		LoadTexture ();
	}

	public AddQuiz SelectedQuiz{
		get{
			return mAddQuiz;
		}
	}

	public void OnButtonClick () {
		if (clickedEvent != null) {
			clickedEvent (this); 
		}
	}
		
	public void ReloadTexture(){
		LoadTexture ();
	}

	private void LoadTexture(){
		if(logoTexture.mainTexture == null){
			WWWClient wwwClient = new WWWClient (this,"https://dl.dropboxusercontent.com/u/32529846/gold.png");
			wwwClient.SetTimeOutInterval (120.0f);
			wwwClient.OnSuccess = (WWW www) => {
				logoTexture.mainTexture = www.texture;

			};
			wwwClient.GetData ();
		}
	}
}
