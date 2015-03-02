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

	private SelledProject mSelledProject;

	void OnEnable(){
		if(mAddQuiz == null){
			return;
		}
		if(logoTexture.mainTexture == null){
			Debug.Log ("null");
			LoadTexture ();
		}
	}

	public void Init(SelledProject selledProject){
		mSelledProject = selledProject;
		titleLabel.text = mSelledProject.title + "\n" + mSelledProject.quiz_count + "問)";
		pointLabel.text = mSelledProject.point + "pt";
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
