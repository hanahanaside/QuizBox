using UnityEngine;
using System.Collections;

public class TopController : MonoBehaviour {

	public GameObject uiRoot;
	public UILabel userPointLabel;
	public GameObject[] dialogArray;
	private static TopController sInstance;
	private GameObject mCurrentDialog;

	void Start () {
		sInstance = this;
		ShowDialog (Instantiate (dialogArray [0])as GameObject);
		SetUserPointLabel ();
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

	public void SetCurrentDialog (GameObject dialog) {
		mCurrentDialog = dialog;
	}

	public void UPdateUserPointLabel () {
		SetUserPointLabel ();
	}

	public void OnTopClicked () {
		OnButtonClicked ();
		ShowDialog (Instantiate (dialogArray [0])as GameObject);
	}

	public void OnAddPointClicked () {
		GameObject addPointDialog = Instantiate (dialogArray [1])as GameObject;
		addPointDialog.transform.parent = uiRoot.transform;
		addPointDialog.transform.localScale = new Vector3 (1, 1, 1);
	}

	public void OnPostQuizClicked () {
		Application.LoadLevel("PostQuiz");
	}

	public void OnResultClicked () {
		OnButtonClicked ();
		ShowDialog (Instantiate (dialogArray [3])as GameObject);
	}

	public void OnSettingClicked () {
		OnButtonClicked ();
		ShowDialog (Instantiate (dialogArray [4])as GameObject);
	}

	public void OnAddQuizClicked () {
		OnButtonClicked ();
		ShowDialog (Instantiate (dialogArray [5])as GameObject);
	}

	private void SetUserPointLabel () {
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
}
