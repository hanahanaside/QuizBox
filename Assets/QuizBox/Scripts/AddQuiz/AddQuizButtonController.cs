using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AddQuizButtonController : MonoBehaviour {

	public static event Action<SelledProject> clickedEvent;

	public GameObject newSprite;
	public UILabel titleLabel;
	public UILabel pointLabel;
	public UITexture logoTexture;

	private SelledProject mSelledProject;
	private UIGrid mGrid;

	public void Init (List<SelledProject> selledProjectList) {
		if (mGrid == null) {
			mGrid = GetComponentInParent<UIGrid> ();
		}
	
		int index = mGrid.GetIndex (transform);
		if(index >= selledProjectList.Count){
			Destroy (gameObject);
			return;
		}
		mSelledProject = selledProjectList [index];
		titleLabel.text = mSelledProject.title + "\n" + mSelledProject.quiz_count + "問)";
		pointLabel.text = mSelledProject.point + "pt";
		if (logoTexture.mainTexture == null) {
			LoadTexture ();
		}
	}

	public void OnButtonClick () {
		if (clickedEvent != null) {
			clickedEvent (mSelledProject); 
		}
	}

	public void ReloadTexture () {
		if (logoTexture.mainTexture == null) {
			LoadTexture ();
		}
	}

	public string GetTitle () {
		return mSelledProject.title;
	}

	private void LoadTexture () {
		WWWClient wwwClient = new WWWClient (this, "https://dl.dropboxusercontent.com/u/32529846/gold.png");
		wwwClient.SetTimeOutInterval (120.0f);
		wwwClient.OnSuccess = (WWW www) => {
			logoTexture.mainTexture = www.texture;

		};
		wwwClient.GetData ();

	}
}
