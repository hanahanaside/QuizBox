using UnityEngine;
using System.Collections;

public class TopController : MonoBehaviour {

	public GameObject uiRoot;
	public UILabel userPointLabel;
	public GameObject[] dialogArray;
	private GameObject mCurrentDialog;

	void Start () {
		ShowDialog (Instantiate (dialogArray [0])as GameObject);
		int userPoint = PrefsManager.instance.GetUserPoint ();
		userPointLabel.text = userPoint + "pt";
	}

	public void OnTopClicked () {
		OnButtonClicked ();
		ShowDialog (Instantiate (dialogArray [0])as GameObject);
	}

	public void OnAddPointClicked () {

	}

	public void OnPostQuizClicked () {

	}

	public void OnSettingClicked () {

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
