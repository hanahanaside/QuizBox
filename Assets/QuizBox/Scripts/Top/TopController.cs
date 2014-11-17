using UnityEngine;
using System.Collections;

public class TopController : MonoBehaviour {

	enum Dialog
	{
		QuizTopic,
		AddQuiz,
		History,
		Setting,
		AddPoint}
	;

	public GameObject uiRoot;
	public UILabel userPointLabel;
	public GameObject[] dialogArray;
	public UISprite[] buttonFilterArray;
	private static TopController sInstance;
	private GameObject mCurrentDialog;

	void Start () {
		sInstance = this;
		ShowDialog (Dialog.QuizTopic);
		SetActiveButtonFilter (Dialog.QuizTopic);
		UpdateUserPointLabel ();
		ImobileManager.Instance.ShowBannerAd ();
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
		mCurrentDialog.SetActive (false);
		SetActiveButtonFilter (Dialog.QuizTopic);
		ShowDialog (Dialog.QuizTopic);
	}

	public void OnAddPointClicked () {
		dialogArray [(int)Dialog.AddPoint].SetActive (true);
	}

	public void OnPostQuizClicked () {
		Application.LoadLevel ("PostQuiz");
	}

	public void OnResultClicked () {
		mCurrentDialog.SetActive (false);
		SetActiveButtonFilter (Dialog.History);
		ShowDialog (Dialog.History);
	}

	public void OnSettingClicked () {
		mCurrentDialog.SetActive (false);
		SetActiveButtonFilter (Dialog.Setting);
		ShowDialog (Dialog.Setting);
	}

	public void OnAddQuizClicked () {
		mCurrentDialog.SetActive (false);
		SetActiveButtonFilter (Dialog.AddQuiz);
		ShowDialog (Dialog.AddQuiz);
	}

	public void UpdateUserPointLabel () {
		int userPoint = PrefsManager.Instance.GetUserPoint ();
		userPointLabel.text = userPoint + "pt";
	}

	private void ShowDialog (Dialog dialog) {
		Debug.Log ("show dialog " + ((int)dialog));
		GameObject currentDialog = dialogArray[(int)dialog];
		currentDialog.SetActive (true);
		mCurrentDialog = currentDialog;
	}

	private void SetActiveButtonFilter (Dialog dialog) {
		int buttonId = (int)dialog;
		foreach (UISprite buttonFilter in buttonFilterArray) {
			buttonFilter.enabled = true;
		}
		buttonFilterArray [buttonId].enabled = false;
	}
}
