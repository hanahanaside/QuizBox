using UnityEngine;
using System.Collections;

public class TopController : MonoSingleton<TopController> {

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
	private GameObject mCurrentDialogObject;
	private Dialog mCurrentDialog;

	void OnEnable(){
		AddPointDialogController.ClosedAddPointDialogEvent += ClosedAddPointDialogEvent;
	}

	void OnDisable(){
		AddPointDialogController.ClosedAddPointDialogEvent -= ClosedAddPointDialogEvent;
	}

	void Start () {
		ShowDialog (Dialog.QuizTopic);
		mCurrentDialog = Dialog.QuizTopic;
		SetActiveButtonFilter (Dialog.QuizTopic);
		UpdateUserPointLabel ();
		ImobileManager.instance.ShowBannerAd ();
	}

	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
		}
	}

	void ClosedAddPointDialogEvent(){
		mCurrentDialogObject.SetActive (true);
	}

	public void OnTopClicked () {
		if(mCurrentDialog == Dialog.QuizTopic){
			return;
		}
		mCurrentDialogObject.SetActive (false);
		SetActiveButtonFilter (Dialog.QuizTopic);
		ShowDialog (Dialog.QuizTopic);
		mCurrentDialog = Dialog.QuizTopic;
	}

	public void OnAddPointClicked () {
		mCurrentDialogObject.SetActive (false);
		ShowDialog (Dialog.AddPoint);
		mCurrentDialog = Dialog.AddPoint;
	}

	public void OnPostQuizClicked () {
		Application.LoadLevel ("PostQuiz");
	}

	public void OnResultClicked () {
		if(mCurrentDialog == Dialog.History){
			return;
		}
		mCurrentDialogObject.SetActive (false);
		SetActiveButtonFilter (Dialog.History);
		ShowDialog (Dialog.History);
		mCurrentDialog = Dialog.History;
	}

	public void OnSettingClicked () {
		if(mCurrentDialog == Dialog.Setting){
			return;
		}
		mCurrentDialogObject.SetActive (false);
		SetActiveButtonFilter (Dialog.Setting);
		ShowDialog (Dialog.Setting);
		mCurrentDialog = Dialog.Setting;
	}

	public void OnAddQuizClicked () {
		if(mCurrentDialog == Dialog.AddQuiz){
			return;
		}
		mCurrentDialogObject.SetActive (false);
		SetActiveButtonFilter (Dialog.AddQuiz);
		AddQuizContainer.instance.Show ();
		mCurrentDialog = Dialog.AddQuiz;
	}

	public void UpdateUserPointLabel () {
		int userPoint = PrefsManager.Instance.GetUserPoint ();
		userPointLabel.text = userPoint + "pt";
	}

	private void ShowDialog (Dialog dialog) {
		Debug.Log ("show dialog " + ((int)dialog));
		GameObject currentDialog = dialogArray[(int)dialog];
		currentDialog.SetActive (true);
		mCurrentDialogObject = currentDialog;
	}

	private void SetActiveButtonFilter (Dialog dialog) {
		int buttonId = (int)dialog;
		foreach (UISprite buttonFilter in buttonFilterArray) {
			buttonFilter.enabled = true;
		}
		buttonFilterArray [buttonId].enabled = false;
	}
}
