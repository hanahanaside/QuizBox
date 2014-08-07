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
		int userPoint = PrefsManager.Instance.GetUserPoint ();
		userPointLabel.text = userPoint + "pt";
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

	public void SetCurrentDialog(GameObject dialog){
		mCurrentDialog = dialog;
	}

	public void OnTopClicked () {
		OnButtonClicked ();
		ShowDialog (Instantiate (dialogArray [0])as GameObject);
	}

	public void OnAddPointClicked () {
		OnButtonClicked ();
		ShowDialog (Instantiate (dialogArray [5])as GameObject);
	}

	public void OnPostQuizClicked () {
		OnButtonClicked ();
		ShowDialog (Instantiate (dialogArray [2])as GameObject);
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
		ShowDialog (Instantiate (dialogArray [1])as GameObject);
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
