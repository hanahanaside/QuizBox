using UnityEngine;
using System.Collections;

public class TopController : MonoBehaviour {

	public GameObject uiRoot;
	public UILabel userPointLabel;
	public GameObject[] dialogArray;
	public UISprite[] buttonFilterArray;
	private static TopController sInstance;
	private GameObject mCurrentDialog;

	void Start () {
		sInstance = this;
		ShowDialog (Instantiate (dialogArray [0])as GameObject);
		SetActiveButtonFilter (0);
		UpdateUserPointLabel ();
	}
		
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
		}
	}

	public static TopController Instance {
		get {
			return sInstance;
		}
	}
				
	public void OnTopClicked () {
		OnButtonClicked ();
		SetActiveButtonFilter (0);
		ShowDialog (Instantiate (dialogArray [0])as GameObject);
	}

	public void OnAddPointClicked () {

		if(GameObject.FindGameObjectWithTag("AddPointDialog") == null){
			GameObject addPointDialog = Instantiate (dialogArray [1])as GameObject;
			addPointDialog.transform.parent = uiRoot.transform;
			addPointDialog.transform.localScale = new Vector3 (1, 1, 1);
		}
	}

	public void OnPostQuizClicked () {
		Application.LoadLevel("PostQuiz");
	}

	public void OnResultClicked () {
		OnButtonClicked ();
		SetActiveButtonFilter (3);
		ShowDialog (Instantiate (dialogArray [3])as GameObject);
	}

	public void OnSettingClicked () {
		OnButtonClicked ();
		SetActiveButtonFilter (4);
		ShowDialog (Instantiate (dialogArray [4])as GameObject);
	}

	public void OnAddQuizClicked () {
		OnButtonClicked ();
		SetActiveButtonFilter (1);
		ShowDialog (Instantiate (dialogArray [5])as GameObject);
	}

	public void UpdateUserPointLabel () {
		int userPoint = PrefsManager.Instance.GetUserPoint ();
		userPointLabel.text = userPoint + "pt";
	}

	private void OnButtonClicked () {
		Destroy (mCurrentDialog);
	}

	private void ShowDialog (GameObject dialog) {
		dialog.transform.parent = uiRoot.transform;
		dialog.transform.localScale = new Vector3 (1f, 1f, 1f);
		mCurrentDialog = dialog;
	}

	private void SetActiveButtonFilter(int buttonId){
		foreach(UISprite buttonFilter in buttonFilterArray){
			buttonFilter.enabled = true;
		}
		buttonFilterArray [buttonId].enabled = false;
	}
}
